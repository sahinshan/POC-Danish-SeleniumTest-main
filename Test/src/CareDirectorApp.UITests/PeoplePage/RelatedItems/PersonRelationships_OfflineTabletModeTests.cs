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
    [Category("MobileOffline")]
    public class PersonRelationships_OfflineTabletModeTests : TestBase
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

            //navigate to the settings page
            mainMenu.NavigateToSettingsPage();

            //if the APP is in online mode change it to offline mode
            settingsPage.SetTheAppInOfflineMode();
        }

        [SetUp]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;


            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();


            //navigate back to the people page
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
        [Property("JiraIssueID", "CDV6-7255")]
        [Description("UI Test for person Relationships (Offline Mode) - 0002 - " +
            "Navigate to the Person Relationship area (person contains PersonRelationship records) - Validate the page content")]
        public void PersonRelationships_OfflineTestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid relationshipID = new Guid("d6221e18-1cb2-ea11-a2cd-005056926fe4"); //Audrey Abbott

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7256")]
        [Description("UI Test for person Relationships (Offline Mode) - 0004 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record - Validate that the Person Relationship record page field titles are displayed")]
        public void PersonRelationships_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Audrey Abbott";

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7257")]
        [Description("UI Test for person Relationships (Offline Mode) - 0005 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record - Validate that the Person Relationship record page fields are correctly displayed")]
        public void PersonRelationships_OfflineTestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Audrey Abbott";

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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


        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }



    }
}

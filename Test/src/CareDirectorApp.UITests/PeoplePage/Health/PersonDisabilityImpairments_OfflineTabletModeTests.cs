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
    [Category("MobileOffline")]
    public class PersonDisabilityImpairments_OfflineTabletModeTests : TestBase
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

        #region Person Disability/Impairments page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6920")]
        [Description("UI Test for Person Disability Impairments (OFfline Mode) - 0002 - " +
            "Navigate to the Person AlleDisability/Impairments area (person contains Disability/Impairments records) - Validate the page content")]
        public void PersonDisabilityImpairments_OfflineTestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid disabilityID = new Guid("ef86d8e5-09a4-ea11-a2cd-005056926fe4");
            Guid imparimentID = new Guid("fee5ac39-0aa4-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()

                .ValidateDisabilityCellText("Deaf", disabilityID.ToString())
                .ValidateImpairmentCellText("", disabilityID.ToString())
                .ValidateSeverityCellText("Moderate", disabilityID.ToString())
                .ValidateStartDateCellText("01/05/2020", disabilityID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", disabilityID.ToString())
                .ValidateCreatedOnCellText("01/06/2020 14:15", disabilityID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", disabilityID.ToString())
                .ValidateModifiedOnCellText("01/06/2020 14:24", disabilityID.ToString())

                .ValidateDisabilityCellText("", imparimentID.ToString())
                .ValidateImpairmentCellText("Dementia", imparimentID.ToString())
                .ValidateSeverityCellText("Mild", imparimentID.ToString())
                .ValidateStartDateCellText("02/05/2020", imparimentID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", imparimentID.ToString())
                .ValidateCreatedOnCellText("01/06/2020 14:17", imparimentID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", imparimentID.ToString())
                .ValidateModifiedOnCellText("01/06/2020 14:17", imparimentID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6921")]
        [Description("UI Test for Person Disability Impairments (OFfline Mode) - 0004 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Disability record - Validate that the Person Disability record page fields and titles are displayed")]
        public void PersonDisabilityImpairments_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("01/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:15:08")
                .ValidateDisabilityFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateSeverityFieldTitleVisible(true)
                .ValidateCVIRecievedDateFieldTitleVisible(true)
                .ValidateOnsetDateFieldTitleVisible(true)
                .ValidateImpairmentFieldTitleVisible(true)
                .ValidateDiagnosisDateFieldTitleVisible(true)
                .ValidateNotifiedDateFieldTitleVisible(true)
                .ValidateRegisteredDisabilityNoFieldTitleVisible(true)

                .ValidateDisabilityFieldText("Deaf")
                .ValidateStartDateFieldText("01/05/2020")
                .ValidateSeverityFieldText("Moderate")
                .ValidateCVIRecievedDateFieldText("02/05/2020")
                .ValidateOnsetDateFieldText("05/05/2020")
                .ValidateImpairmentFieldText("")
                .ValidateDiagnosisDateFieldText("01/09/2019")
                .ValidateNotifiedDateFieldText("04/05/2020")
                .ValidateRegisteredDisabilityNoFieldText("9876");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6922")]
        [Description("UI Test for Person Disability Impairments (OFfline Mode) - 0008 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Impairment record - Validate that the Person Impairment record page fields and titles are displayed")]
        public void PersonDisabilityImpairments_OfflineTestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("02/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:17:22")
                .ValidateDisabilityFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateSeverityFieldTitleVisible(true)
                .ValidateCVIRecievedDateFieldTitleVisible(true)
                .ValidateOnsetDateFieldTitleVisible(true)
                .ValidateImpairmentFieldTitleVisible(true)
                .ValidateDiagnosisDateFieldTitleVisible(true)
                .ValidateNotifiedDateFieldTitleVisible(true)
                .ValidateRegisteredDisabilityNoFieldTitleVisible(true)

                .ValidateDisabilityFieldText("")
                .ValidateStartDateFieldText("02/05/2020")
                .ValidateSeverityFieldText("Mild")
                .ValidateCVIRecievedDateFieldText("03/05/2020")
                .ValidateOnsetDateFieldText("06/05/2020")
                .ValidateImpairmentFieldText("Dementia")
                .ValidateDiagnosisDateFieldText("01/05/2020")
                .ValidateNotifiedDateFieldText("05/05/2020")
                .ValidateRegisteredDisabilityNoFieldText("567");
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

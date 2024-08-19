using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People
{
    /// <summary>
    /// All tests in this validate the mobile app when it is displayed in mobile mode (e.g. Smartphones with small screens)
    /// </summary>
    [TestFixture]
    public class PeoplePageTests2 : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper.AuthenticateUser("mobile_test_user_1", "Passw0rd_!");


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


            //if the error popup is open close it
            errorPopup
                .ClosePopupIfOpen();

            //if the warning popup is open close it
            warningPopup
                .CloseWarningPopupIfOpen();

            //navigate to the People page
            mainMenu
                .NavigateToPeoplePage();
        }

        

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7301")]
        [Description("UI Test for 'People' Scenario 12 - Validate expand button for a Person Record")]
        public void PeoplePage_TestMethod12()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn");
            Guid _personid = (Guid)fields["personid"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapExpandPersonRecordButton_MobileView(_personid.ToString())
                .ValidatePersonLabels_MobileView(_personid.ToString())
                .ValidateFullNameText_MobileView(_personid.ToString(), "Mathews MCSenna")
                .ValidateFullAddressText_MobileView(_personid.ToString(), "PNA, PNO, ST, VLg, TOW, CO, PC")
                .ValidateIdText_MobileView(_personid.ToString(), "2154808")
                .ValidateFirstNameText_MobileView(_personid.ToString(), "Mathews")
                .ValidateLastNameText_MobileView(_personid.ToString(), "MCSenna")
                .ValidateRepresentsHazzardText_MobileView(_personid.ToString(), "No")
                .ValidateGenderText_MobileView(_personid.ToString(), "Male")
                .ValidateDOBText_MobileView(_personid.ToString(), "01/05/2010")
                .ValidateNHSNoText_MobileView(_personid.ToString(), "987 654 3210")
                .ValidatePostCodeText_MobileView(_personid.ToString(), "PC")
                .ValidateCreatedByText_MobileView(_personid.ToString(), "Mobile Test User 1")
                .ValidateCreatedOnText_MobileView(_personid.ToString(), "14/05/2019 10:53")
                .ValidateModifiedByText_MobileView(_personid.ToString(), "Mobile Test User 1")
                .ValidateMOdifiedOnText_MobileView(_personid.ToString(), "15/05/2019 11:46")
                ;

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7302")]
        [Description("UI Test for 'People' Scenario 13 - Validate expand button for a Person Record")]
        public void PeoplePage_TestMethod13()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("", "MT.P.006", "Rozoner", "personid");
            Guid _personid = (Guid)fields["personid"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapExpandPersonRecordButton_MobileView(_personid.ToString())
                .ValidatePersonLabels_MobileView(_personid.ToString())
                .ValidateFullNameText_MobileView(_personid.ToString(), "Rozoner")
                .ValidateFullAddressTextViewNotVisible_MobileView(_personid.ToString())
                .ValidateIdText_MobileView(_personid.ToString(), "2154914")
                .ValidateFirstNameTextViewNotVisible_MobileView(_personid.ToString())
                .ValidateLastNameText_MobileView(_personid.ToString(), "Rozoner")
                .ValidateRepresentsHazzardText_MobileView(_personid.ToString(), "No")
                .ValidateGenderText_MobileView(_personid.ToString(), "Male")
                .ValidateDOBText_MobileView(_personid.ToString(), "01/05/2010")
                .ValidateNHSNoTextViewNotVisible_MobileView(_personid.ToString())
                .ValidatePostCodeTextViewNotVisible_MobileView(_personid.ToString())
                .ValidateCreatedByText_MobileView(_personid.ToString(), "Mobile Test User 1")
                .ValidateCreatedOnText_MobileView(_personid.ToString(), "16/05/2019 16:46")
                .ValidateModifiedByText_MobileView(_personid.ToString(), "Mobile Test User 1")
                .ValidateMOdifiedOnText_MobileView(_personid.ToString(), "16/05/2019 16:47");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7303")]
        [Description("UI Test for 'People' Scenario 22 - Validate related items in mobile mode")]
        public void PeoplePage_TestMethod22_1()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .ValidateActivitiesAreaElementsVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
                .ValidateHealthAreaElementsNotVisible_RelatedItems();

            Assert.Inconclusive("this test need to be finished with one last validation. check the arrow buttons on the right part of the activities, related items and health areas");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7304")]
        [Description("UI Test for 'People' Scenario 23 - Collapse Activities area in related items menu")]
        public void PeoplePage_TestMethod23()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                 .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .TapRelatedItemsArea_RelatedItems()
                .ValidateActivitiesAreaElementsNotVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsVisible_RelatedItems()
                .ValidateHealthAreaElementsNotVisible_RelatedItems();
                

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7305")]
        [Description("UI Test for 'People' Scenario 24 - Collapse and Expand Activities area in related items menu")]
        public void PeoplePage_TestMethod24()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .TapActivitiesArea_RelatedItems()//collapse
                .TapActivitiesArea_RelatedItems()//expand
                .ValidateActivitiesAreaElementsVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
                .ValidateHealthAreaElementsNotVisible_RelatedItems();

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        [Property("JiraIssueID", "")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

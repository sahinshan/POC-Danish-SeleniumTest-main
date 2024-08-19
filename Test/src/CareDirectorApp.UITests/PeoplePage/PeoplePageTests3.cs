using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People
{
    /// <summary>
    /// All tests in this validate the mobile app when it is NOT displayed in mobile mode
    /// </summary>
    [TestFixture]
    public class PeoplePageTests3 : TestBase
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
                   .InsertUserName("Mobile_Test_User_4")
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
                    .InsertUserName("Mobile_Test_User_4")
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
                .NavigateToPeoplePage()
                .WaitForPeoplePageToLoad();
        }

        
        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7306")]
        [Description("UI Test for 'People' Scenario 27 - Validate pagination info")]
        public void PeoplePage_TestMethod27()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .VerifyFirstPageButtonEnabled(false)
                .VerifyPreviousPageButtonEnabled(false)
                .VerifyPaginationButtonInfo("Page 1")
                .VerifyNextPageButtonEnabled(true);

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7307")]
        [Description("UI Test for 'People' Scenario 28 - Validate pagination info")]
        public void PeoplePage_TestMethod28()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapNextPageButton()
                .VerifyFirstPageButtonEnabled(true)
                .VerifyPreviousPageButtonEnabled(true)
                .VerifyPaginationButtonInfo("Page 2")
                .VerifyNextPageButtonEnabled(true);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7308")]
        [Description("UI Test for 'People' Scenario 29 - Validate pagination info")]
        public void PeoplePage_TestMethod29()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapNextPageButton()
                .TapPreviousPageButton()
                .VerifyFirstPageButtonEnabled(false)
                .VerifyPreviousPageButtonEnabled(false)
                .VerifyPaginationButtonInfo("Page 1")
                .VerifyNextPageButtonEnabled(true);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7309")]
        [Description("UI Test for 'People' Scenario 30 - Validate pagination info")]
        public void PeoplePage_TestMethod30()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapNextPageButton()
                .TapNextPageButton()
                .TapPreviousPageButton()
                .VerifyFirstPageButtonEnabled(true)
                .VerifyPreviousPageButtonEnabled(true)
                .VerifyPaginationButtonInfo("Page 2")
                .VerifyNextPageButtonEnabled(true);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7310")]
        [Description("UI Test for 'People' Scenario 31 - Validate pagination info")]
        public void PeoplePage_TestMethod31()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapNextPageButton()
                .TapNextPageButton()
                .TapFistPageButton()
                .VerifyFirstPageButtonEnabled(false)
                .VerifyPreviousPageButtonEnabled(false)
                .VerifyPaginationButtonInfo("Page 1")
                .VerifyNextPageButtonEnabled(true);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7311")]
        [Description("UI Test for 'People' Scenario 32 - Validate pagination info")]
        public void PeoplePage_TestMethod32()
        {
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapNextPageButton()
                .TapNextPageButton()
                .VerifyFirstPageButtonEnabled(true)
                .VerifyPreviousPageButtonEnabled(true)
                .VerifyPaginationButtonInfo("Page 2")
                .VerifyNextPageButtonEnabled(true);
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

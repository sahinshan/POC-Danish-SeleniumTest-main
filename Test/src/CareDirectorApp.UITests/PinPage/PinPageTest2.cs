using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests
{
    /// <summary>
    /// All tests in this class only need for the app to be started once in the emulator
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PinPageTests2 : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            uIHelper = new UIHelper();

            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, Xamarin.UITest.Configuration.AppDataMode.Clear);

            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
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

            //navigate to the settings page
            mainMenu
                .NavigateToSettingsPage()
                .WaitForSettingsPageToLoad()
                .TapChangePinButton();
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 12 - Go to change PIN page from settings page")]
        public void PinPage_TestMethod12()
        {
            //wait for the PIN page to load
            pinPage
                .WaitForChangePinPageToLoad();
                
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 13 - In the change PIN page insert an incorrect old PIN")]
        public void PinPage_TestMethod13()
        {
            //wait for the PIN page to load
            pinPage
                .WaitForChangePinPageToLoad()
                .TapButton1()
                .TapButton1()
                .TapButton1()
                .TapButton1()
                .TapButtonOK();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Wrong PIN. Try again");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 14 - Insert an incorrect old PIN, close error message")]
        public void PinPage_TestMethod14()
        {
            //insert the incorrect old PIN
            pinPage
                .WaitForChangePinPageToLoad()
                .TapButton1()
                .TapButton1()
                .TapButton1()
                .TapButton1()
                .TapButtonOK();
            
            //close the error message
            errorPopup
                .WaitForErrorPopupToLoad()
                .TapOnOKButton();

            //check that we still are in the Change PIN page
            pinPage
               .WaitForChangePinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 17 - Tap on the 'Forgot PIN? Enter password' button")]
        public void PinPage_TestMethod17()
        {
            //
            pinPage
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 18 - Tap on the 'Forgot PIN? Enter password' button - insert the password - validate the reirection to the new pin page")]
        public void PinPage_TestMethod18()
        {
            //
            pinPage
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad()
                .InsertPassword("Passw0rd_!")
                .TapCheckPasswordButton()
                .WaitForPinPageToLoad(true, false);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 20 - Tap on the 'Forgot PIN? Enter password' button - insert wrong password")]
        public void PinPage_TestMethod20()
        {
            pinPage
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad()
                .InsertPassword("IncorrectPassword!")
                .TapCheckPasswordButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Validation Error", "Password is incorrect");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 21 - Tap on the 'Forgot PIN? Enter password' button - Tap on the 'Insert Pin' button - Validate redirect back")]
        public void PinPage_TestMethod21()
        {
            pinPage
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad()
                .TapEnterPinButton()
                .WaitForChangePinPageToLoad();
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

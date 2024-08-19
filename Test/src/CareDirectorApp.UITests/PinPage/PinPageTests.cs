using System;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using NUnit.Framework;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests
{
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PinPageTests : TestBase
    {
        UIHelper uIHelper;

        [SetUp()]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            uIHelper = new UIHelper();

            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.Clear);
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 2 - Validate the 'Del.' button")]
        public void PinPage_TestMethod2()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton1()
                .TapButton2()
                .TapButton3()
                .TapButton4()
                .TapButton5()
                .TapButtonDel()
                .ValidatePinLabel("••••");



        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 3 - Insert pin code with less than 3 digits")]
        public void PinPage_TestMethod3()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton1()
                .TapButton2()
                .TapButton3()
                .TapButtonOK();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "PIN must be between 4 and 8 digits");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 4 - " +
            "Insert pin code with less than 3 digits - An Error Message should be displayed - " +
            "Close error message - Insert last digit and tap ok - " +
            "User should be redirected to the confirmation page")]
        public void PinPage_TestMethod4()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton1()
                .TapButton2()
                .TapButton3()
                .TapButtonOK();

            //wait for the error to be displayed
            errorPopup
                .WaitForErrorPopupToLoad()
                .TapOnOKButton();

            //insert last digit and wait for confirmation page
            pinPage
                .WaitForPinPageToLoad()
                .TapButton4()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 5 - Insert pin code with less than 3 digits, close error message, insert last digit and tap ok and confirm PIN")]
        public void PinPage_TestMethod5()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton1()
                .TapButton2()
                .TapButton3()
                .TapButtonOK();

            //wait for the error to be displayed
            errorPopup
                .WaitForErrorPopupToLoad()
                .TapOnOKButton();

            //insert last digit and wait for confirmation page
            pinPage
                .WaitForPinPageToLoad()
                .TapButton4()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton1()
                .TapButton2()
                .TapButton3()
                .TapButton4()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 6 - Insert and confirm pin code")]
        public void PinPage_TestMethod6()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
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
            
            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 7 - Insert and confirm pin code")]
        public void PinPage_TestMethod7()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonDel()
                .TapButtonDel()
                .TapButtonDel()
                .TapButtonDel()
                .TapButton4()
                .TapButton3()
                .TapButton2()
                .TapButton1()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton4()
                .TapButton3()
                .TapButton2()
                .TapButton1()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 8 - Confirmation pin code different than first pin code")]
        public void PinPage_TestMethod8()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton4()
                .TapButton3()
                .TapButton2()
                .TapButton1()
                .TapButtonOK();

            
            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Both PIN entries must be the same");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 9 - use back button on confirmation pin page")]
        public void PinPage_TestMethod9()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButtonBackButton()
                .WaitForPinPageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 10 - use back button and insert a new PIN code")]
        public void PinPage_TestMethod10()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButtonBackButton()
                .WaitForPinPageToLoad()
                .TapButton2()
                .TapButton3()
                .TapButton4()
                .TapButton5()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton2()
                .TapButton3()
                .TapButton4()
                .TapButton5()
                .TapButtonOK();

            homePage
                .WaitForHomePageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 11 - use back button and insert a new PIN code, in the confirmation page use the 1st inserted PIN code")]
        public void PinPage_TestMethod11()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButtonBackButton()
                .WaitForPinPageToLoad()
                .TapButton2()
                .TapButton3()
                .TapButton4()
                .TapButton5()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton6()
                .TapButton7()
                .TapButton8()
                .TapButton9()
                .TapButtonOK();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Both PIN entries must be the same");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 15 - change pin code - insert an initial incorrect pin - re-insert correct pin and change pin code")]
        public void PinPage_TestMethod15()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

            //insert an initial incorrect pin
            mainMenu
                .NavigateToSettingsPage()
                .WaitForSettingsPageToLoad()
                .TapChangePinButton()
                .WaitForChangePinPageToLoad()
                .TapButton1().TapButton1().TapButton1().TapButton1()
                .TapButtonOK();

            //close the error message
            errorPopup
                .WaitForErrorPopupToLoad()
                .TapOnOKButton();

            //insert the correct pin and change it
            pinPage
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForPinPageToLoad(true, false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad(false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK();

            settingsPage
                .WaitForSettingsPageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 15 - change pin code - direct change of Pin code")]
        public void PinPage_TestMethod16()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

            //change PIN
            mainMenu
                .NavigateToSettingsPage()
                .WaitForSettingsPageToLoad()
                .TapChangePinButton()
                .WaitForChangePinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForPinPageToLoad(true, false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad(false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK();
            
            settingsPage
                .WaitForSettingsPageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 19 - change pin code using password")]
        public void PinPage_TestMethod19()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

            //change PIN
            mainMenu
                .NavigateToSettingsPage()
                .WaitForSettingsPageToLoad()
                .TapChangePinButton()
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad()
                .InsertPassword("Passw0rd_!")
                .TapCheckPasswordButton()
                .WaitForPinPageToLoad(true, false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad(false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK();

            settingsPage
                .WaitForSettingsPageToLoad();

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Login' Scenario 22 - change pin code using password, revert decision to use old pin code instead")]
        public void PinPage_TestMethod22()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK();

            //user should reach the home page
            homePage
                .WaitForHomePageToLoad();

            //change PIN
            mainMenu
                .NavigateToSettingsPage()
                .WaitForSettingsPageToLoad()
                .TapChangePinButton()
                .WaitForChangePinPageToLoad()
                .TapForgotPinButton()
                .WaitForForgotPinPageToLoad()
                .TapEnterPinButton()
                .WaitForChangePinPageToLoad()
                .TapButton3().TapButton4().TapButton5().TapButton6()
                .TapButtonOK()
                .WaitForPinPageToLoad(true, false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK()
                .WaitForConfirmationPinPageToLoad(false)
                .TapButton4().TapButton5().TapButton6().TapButton7()
                .TapButtonOK();

            settingsPage
                .WaitForSettingsPageToLoad();

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

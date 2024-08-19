using System;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using NUnit.Framework;

namespace CareDirectorApp.UITests.Login
{
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class LoginPageTests : TestBase
    {
        UIHelper uIHelper;

        [SetUp]
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
        [Property("JiraIssueID", "CDV6-6628")]
        [Description("UI Test for 'Login' Scenario 1 - Validate the login page default content")]
        public void LoginPage_TestMethod1()
        {
            loginPage
                .WaitForLoginPageToLoad()
                .CheckIfStoredUserButtonVisible(false);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6629")]
        [Description("UI Test for 'Login' Scenario 2 - tap the login button with login and password textbox empty")]
        public void LoginPage_TestMethod2()
        {
            loginPage
                .TapLoginButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Validation Error", "Username, Password and Environment are required");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6630")]
        [Description("UI Test for 'Login' Scenario 3 - tap the login button with login and password textbox empty and close error message")]
        public void LoginPage_TestMethod3()
        {
            loginPage
                .TapLoginButton();
            
            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Validation Error", "Username, Password and Environment are required")
                .TapOnOKButton();

            loginPage
                .WaitForLoginPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6631")]
        [Description("UI Test for 'Login' Scenario 4 - tap the login button without password")]
        public void LoginPage_TestMethod4()
        {
            //set the default url
            this.SetDefaultEndpointURL();


            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .TapOnPasswordField()
                .TapLoginButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Validation Error", "Username, Password and Environment are required")
                .TapOnOKButton();

            loginPage
                .WaitForLoginPageToLoad()
                .ValidateUserNameText("Mobile_Test_User_1");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6632")]
        [Description("UI Test for 'Login' Scenario 5 - Insert username without service endpoint url")]
        public void LoginPage_TestMethod5()
        {
            loginPage
                .InsertUserName("Mobile_Test_User_1");

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "There is not service endpoint url. Please go to left menu and enter a service endpoint url. If you have any question, please contact your administrator.");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6633")]
        [Description("UI Test for 'Login' Scenario 5.1 - Insert username with incorrect service endpoint url")]
        public void LoginPage_TestMethod5_1()
        {
            //set the default url
            this.SetDefaultEndpointURL("https://incorrecturl/");

            loginPage
                .InsertUserName("invalidusername");

            errorPopup
               .WaitForErrorPopupToLoad()
               .ValidateErrorMessageTitleAndMessage("Error", "Unable to resolve host \"incorrecturl\": No address associated with hostname");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6634")]
        [Description("UI Test for 'Login' Scenario 6 - Insert non existing username ")]
        public void LoginPage_TestMethod6()
        {
            //set the default url
            this.SetDefaultEndpointURL();
            
            loginPage
                .InsertUserName("invalidusername");

            errorPopup
               .WaitForErrorPopupToLoad()
               .ValidateErrorMessageTitleAndMessage("Error", "The user name or password is incorrect.");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6635")]
        [Description("UI Test for 'Login' Scenario 7 - App should load the user environment")]
        public void LoginPage_TestMethod7()
        {
            //set the default url
            this.SetDefaultEndpointURL();

            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .TapOnPasswordField()
                .ValidateSelectedEnvironment("Health and Local Authority");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6636")]
        [Description("UI Test for 'Login' Scenario 8 - user with invalid password")]
        public void LoginPage_TestMethod8()
        {
            //set the default url
            this.SetDefaultEndpointURL();

            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("IncorrectPassword")
                .TapLoginButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Validation Error", "Invalid username or password.");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6637")]
        [Description("UI Test for 'Login' Scenario 9 - Login with valid user")]
        public void LoginPage_TestMethod9()
        {
            //set the default url
            this.SetDefaultEndpointURL();

            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            pinPage
                .WaitForPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6638")]
        [Description("UI Test for 'Login' Scenario 10 - Multiple endpoints testing")]
        public void LoginPage_TestMethod10()
        {
            //set the default url
            this.SetDefaultEndpointURL();

            //SET A 2nd ENDPOINT
            //navigate to the service endpoints to set the default endpoint
            mainMenu
                .WaitForMainMenuButtonToLoad()
                .NavigateToServiceEndpointsLink();
            
            //set the URL for the default endpoint
            serviceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .tapAddNewServiceEndpointButton()
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointName("CareDirectorEndpoint2")
                .InsertEndpointURL("https://secondendpointurl.careworks.ie")
                .SelectDefaultField(true)
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .tapBackButton()
                .WaitForLoginPageToLoad();

            //Insert a user name only valid for the Non Default endpoint
            loginPage
                .InsertUserName("Mobile_Test_User_1");

            
            //the login should not be valid for the default endpoint
            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Unable to resolve host \"secondendpointurl.careworks.ie\": No address associated with hostname");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6639")]
        [Description("UI Test for 'Login' Scenario 11 - Multiple endpoints testing")]
        public void LoginPage_TestMethod11()
        {
            //set the default url
            this.SetDefaultEndpointURL();

            //SET A 2ND ENDPOINT
            
            //navigate to the service endpoints to set the default endpoint
            mainMenu
                .WaitForMainMenuButtonToLoad()
                .NavigateToServiceEndpointsLink();

            //set the URL for the default endpoint
            serviceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .tapAddNewServiceEndpointButton()
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointName("CareDirectorEndpoint2")
                .InsertEndpointURL("https://secondendpointurl.careworks.ie")
                .SelectDefaultField(false)
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .tapBackButton()
                .WaitForLoginPageToLoad();

            //Try to login with user valid for the default endpoint
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();


            //the login should be valid for the default endpoint
            pinPage
                .WaitForPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6640")]
        [Description("UI Test for 'Login' Scenario 12 - Validate login info saved in login page after initial login")]
        public void LoginPage_TestMethod12()
        {
            //set the default url
            this.SetDefaultEndpointURL();
            

            //Login with test user account
            loginPage
                .InsertUserName("Mobile_Test_User_1")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();
            
            //the login should be valid for the default endpoint
            pinPage
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //wait for the login page to load
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6641")]
        [Description("UI Test for 'Login' Scenario 13 - login with a saved user")]
        public void LoginPage_TestMethod13()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Wait for the login page to load
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6642")]
        [Description("UI Test for 'Login' Scenario 14 - login with a saved user (with incorrect password)")]
        public void LoginPage_TestMethod14()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Wait for the login page to load
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .CheckIfStoredUserButtonVisible(false)
                .InsertPassword("incorrectpassword")
                .TapLoginButton();


            //an error message should be displayed
            errorPopup
               .WaitForErrorPopupToLoad()
               .ValidateErrorMessageTitleAndMessage("Validation Error", "Invalid username or password.");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6643")]
        [Description("UI Test for 'Login' Scenario 15 - Tap on change user button")]
        public void LoginPage_TestMethod15()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .TapChangeUserButton();

            //validate the warning window
            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Warning", "Are you sure you want to log in as a different user?");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6644")]
        [Description("UI Test for 'Login' Scenario 16 - Change User")]
        public void LoginPage_TestMethod16()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .TapChangeUserButton();

            //validate the warning window
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            loginPage
                .WaitForLoginPageToLoad()
                .CheckIfStoredUserButtonVisible(true);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6645")]
        [Description("UI Test for 'Login' Scenario 17 - Cancel change of user")]
        public void LoginPage_TestMethod17()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .CheckIfStoredUserButtonVisible(false)
                .TapChangeUserButton();

            //validate the warning window
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnNoButton();

            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .CheckIfStoredUserButtonVisible(false);
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6646")]
        [Description("UI Test for 'Login' Scenario 18 - Login with a different user")]
        public void LoginPage_TestMethod18()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .TapChangeUserButton();

            //confirm the user change
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            //login with a different user
            loginPage
                .WaitForLoginPageToLoad()
                .InsertUserName("Mobile_Test_User_2")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6647")]
        [Description("UI Test for 'Login' Scenario 19 - Tap 'Use last logged account' button")]
        public void LoginPage_TestMethod19()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .TapChangeUserButton();

            //confirm the user change
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            //Tap on the 'Use last logged account' button 
            loginPage
                .WaitForLoginPageToLoad()
                .TapStoredUserButton()
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "")
                .TapOnPasswordField()
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority"); //the environment only loads after we tap on the password field
            
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6648")]
        [Description("UI Test for 'Login' Scenario 20 - Tap 'Use last logged account' button and login with saved user")]
        public void LoginPage_TestMethod20()
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
                .WaitForPinPageToLoad();

            //close and re-open the app
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //Tap on the change user button
            loginPage
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "Health and Local Authority")
                .TapChangeUserButton();

            //confirm the user change
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            //Tap on the 'Use last logged account' button 
            loginPage
                .WaitForLoginPageToLoad()
                .TapStoredUserButton()
                .WaitForLoginPageToLoadWithPreviousLoginInfoSaved("Mobile Test User 1", "")
                .InsertPassword("Passw0rd_!")
                .TapLoginButton();

            //wait for the PIN page to load
            pinPage
                .WaitForPinPageToLoad();

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

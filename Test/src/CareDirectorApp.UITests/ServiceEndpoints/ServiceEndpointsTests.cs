using System;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using NUnit.Framework;

namespace CareDirectorApp.UITests
{
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class ServiceEndpointsTests : TestBase
    {

        [SetUp()]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            UIHelper uIHelper = new UIHelper();

            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, Xamarin.UITest.Configuration.AppDataMode.Clear);

            //the login page is the landing page, so we need for it to load
            LoginPage loginPage = new LoginPage(this._app);
            loginPage.WaitForLoginPageToLoad();
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 1 - Validate the Service Endpoints page")]
        public void ServiceEndpoints_TestMethod1()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .FindDefaultServiceEndpointLabels()
                .FindServiceEndpointValues("CareDirector", null, "Yes");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 2 - Tap back button on Service Endpoints page")]
        public void ServiceEndpoints_TestMethod2()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .FindDefaultServiceEndpointLabels()
                .tapBackButton()
                .WaitForLoginPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 3 - Open default service endpoint")]
        public void ServiceEndpoints_TestMethod3()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .FindDefaultServiceEndpointLabels()
                .TapOnServiceEndpoint("CareDirector")
                .WaitForServiceEndpointEditPageToLoad()
                .FindServiceEndpointValues("CareDirector", "", "Yes");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 4 - Tap delete button on default service endpoint")]
        public void ServiceEndpoints_TestMethod4()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .FindDefaultServiceEndpointLabels()
                .TapOnServiceEndpoint("CareDirector")
                .WaitForServiceEndpointEditPageToLoad()
                .TapServiceEndpointDeleteButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Can't delete default service endpoint url");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 5 - Close error popup after trying to delete default service endpoint")]
        public void ServiceEndpoints_TestMethod5()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            ServiceEndpointEditPage serviceEndpointEditPage = mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                .TapServiceEndpointDeleteButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .TapOnOKButton()
                .ValidateErrorPopupClosed();

            serviceEndpointEditPage.WaitForServiceEndpointEditPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 6 - Try to Save and Close an endpoint without a name")]
        public void ServiceEndpoints_TestMethod6()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("")
                .TapServiceEndpointSaveAndCloseButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 7 - Try to Save and Close an endpoint without a URL")]
        public void ServiceEndpoints_TestMethod7()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirector")
                 .InsertEndpointURL("")
                .TapServiceEndpointSaveAndCloseButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 8 - Try to Save an endpoint without a name")]
        public void ServiceEndpoints_TestMethod8()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("")
                .TapServiceEndpointSaveButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 9 - Try to Save an endpoint without a URL")]
        public void ServiceEndpoints_TestMethod9()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirector")
                 .InsertEndpointURL("")
                .TapServiceEndpointSaveButton();

            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup.WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 10 - Check that the default field picklist is disabled for the default Endpoint")]
        public void ServiceEndpoints_TestMethod10()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .CheckDefaultFieldDisabled();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 11 - Save and Close a default endpoint")]
        public void ServiceEndpoints_TestMethod11()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 12 - Save, Close and reopen a default endpoint")]
        public void ServiceEndpoints_TestMethod12()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveAndCloseButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .TapOnServiceEndpoint("CareDirectorEndpoint")
                 .WaitForServiceEndpointEditPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 13 - Save, Close and reopen a default endpoint")]
        public void ServiceEndpoints_TestMethod13()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 14 - Save a default endpoint and tap the back button")]
        public void ServiceEndpoints_TestMethod14()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveButton()
                 .TapBackButtonButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 15 - Edit endpoint and tap the back button without saving the record")]
        public void ServiceEndpoints_TestMethod15()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapBackButtonButton();

            WarningPopup warningPopup = new WarningPopup(this._app);
            warningPopup.WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Warning", "Are you sure you want to navigate back? Unsaved changes will be lost.");

        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 16 - Edit endpoint and tap the back button without saving the record, tap yes in warning message")]
        public void ServiceEndpoints_TestMethod16()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapBackButtonButton();

            WarningPopup warningPopup = new WarningPopup(this._app);
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            ServiceEndpointsPage serviceEndpointsPage = new ServiceEndpointsPage(this._app);
            serviceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .FindDefaultServiceEndpointLabels()
                .FindServiceEndpointValues("CareDirector", null, "Yes");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 17 - Edit endpoint and tap the back button without saving the record, tap no in warning message")]
        public void ServiceEndpoints_TestMethod17()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindDefaultServiceEndpointLabels()
                 .TapOnServiceEndpoint("CareDirector")
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapBackButtonButton();

            WarningPopup warningPopup = new WarningPopup(this._app);
            warningPopup
                .WaitForWarningPopupToLoad()
                .TapOnNoButton();

            ServiceEndpointEditPage serviceEndpointsPage = new ServiceEndpointEditPage(this._app);
            serviceEndpointsPage
                .WaitForServiceEndpointEditPageToLoad()
                .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");


        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 18 - Tap add button in service endpoints page")]
        public void ServiceEndpoints_TestMethod18()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .FindServiceEndpointValues("", "", "No");


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 19 - Save new endpoint without name or url")]
        public void ServiceEndpoints_TestMethod19()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .TapServiceEndpointSaveButton();


            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 20 - Save and close new endpoint without name or url")]
        public void ServiceEndpoints_TestMethod20()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .TapServiceEndpointSaveAndCloseButton();


            ErrorPopup errorPopup = new ErrorPopup(this._app);
            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Url or name cannot be null or empty");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 21 - Tap add new edpoint button - tap back button")]
        public void ServiceEndpoints_TestMethod21()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .TapBackButtonButton()
                 .WaitForServiceEndpointsPageToLoad();



        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 22 - Add new Endpoint and tap back button without saving")]
        public void ServiceEndpoints_TestMethod22()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapBackButtonButton();

            WarningPopup warningPopup = new WarningPopup(this._app);
            warningPopup.WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Warning", "Are you sure you want to navigate back? Unsaved changes will be lost.");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 23 - Add new endpoint, save and tap the back button")]
        public void ServiceEndpoints_TestMethod23()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveButton()
                 .TapBackButtonButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "No");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 24 - Add new endpoint (dafault = yes), save and tap the back button")]
        public void ServiceEndpoints_TestMethod24()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .SelectDefaultField(true)
                 .TapServiceEndpointSaveButton()
                 .TapBackButtonButton()
                 .FindServiceEndpointValues(1, "CareDirector", null, "No")
                 .FindServiceEndpointValues(2, "CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 25 - Add new endpoint, Save and close the record")]
        public void ServiceEndpoints_TestMethod25()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveAndCloseButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "No");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 26 - re-open saved endpoint")]
        public void ServiceEndpoints_TestMethod26()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .SelectDefaultField(true)
                 .TapServiceEndpointSaveAndCloseButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .TapOnServiceEndpoint("CareDirectorEndpoint")
                 .WaitForServiceEndpointEditPageToLoad()
                 .FindServiceEndpointValues("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 27 - Tap Delete button")]
        public void ServiceEndpoints_TestMethod27()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                 .NavigateToServiceEndpointsLink()
                 .WaitForServiceEndpointsPageToLoad()
                 .tapAddNewServiceEndpointButton()
                 .WaitForServiceEndpointEditPageToLoad()
                 .InsertEndpointName("CareDirectorEndpoint")
                 .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                 .TapServiceEndpointSaveAndCloseButton()
                 .WaitForServiceEndpointsPageToLoad()
                 .TapOnServiceEndpoint("CareDirectorEndpoint")
                 .WaitForServiceEndpointEditPageToLoad()
                 .TapServiceEndpointDeleteButton();

            WarningPopup warning = new WarningPopup(this._app);
            warning
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Deleting", "Are you sure you want to delete this item?");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 28 - Tap Delete button and cancel delete")]
        public void ServiceEndpoints_TestMethod28()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            ServiceEndpointEditPage editPage = mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .tapAddNewServiceEndpointButton()
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointName("CareDirectorEndpoint")
                .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .TapOnServiceEndpoint("CareDirectorEndpoint")
                .WaitForServiceEndpointEditPageToLoad()
                .TapServiceEndpointDeleteButton();

            WarningPopup warning = new WarningPopup(this._app);
            warning
                .WaitForWarningPopupToLoad()
                .TapOnNoButton();

            editPage
                .WaitForServiceEndpointEditPageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 29 - Tap Delete button and confirm delete")]
        public void ServiceEndpoints_TestMethod29()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            ServiceEndpointEditPage editPage = mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .tapAddNewServiceEndpointButton()
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointName("CareDirectorEndpoint")
                .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .TapOnServiceEndpoint("CareDirectorEndpoint")
                .WaitForServiceEndpointEditPageToLoad()
                .TapServiceEndpointDeleteButton();

            WarningPopup warning = new WarningPopup(this._app);
            warning
                .WaitForWarningPopupToLoad()
                .TapOnYesButton();

            ServiceEndpointsPage serviceEndpointsPage = new ServiceEndpointsPage(this._app);
            serviceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .ValidateServiceEndpointNotPresent("CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'Service Endpoints' Scenario 30 - update endpoint, set default to Yes")]
        public void ServiceEndpoints_TestMethod30()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToServiceEndpointsLink()
                .WaitForServiceEndpointsPageToLoad()
                .tapAddNewServiceEndpointButton()
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointName("CareDirectorEndpoint")
                .InsertEndpointURL("https://phoenixmobile.careworks.ie/")
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .TapOnServiceEndpoint("CareDirectorEndpoint")
                .WaitForServiceEndpointEditPageToLoad()
                .SelectDefaultField(true)
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .FindServiceEndpointValues(1, "CareDirector", null, "No")
                .FindServiceEndpointValues(2, "CareDirectorEndpoint", "https://phoenixmobile.careworks.ie/", "Yes");

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}

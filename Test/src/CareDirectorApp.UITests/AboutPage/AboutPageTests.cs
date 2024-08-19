using System;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using NUnit.Framework;

namespace CareDirectorApp.UITests.AboutPage
{
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class AboutPageTests : TestBase
    {

        [SetUp]
        public void TestInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            UIHelper uIHelper = new UIHelper();

            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, Xamarin.UITest.Configuration.AppDataMode.DoNotClear);

            //the login page is the landing page, so we need for it to load
            LoginPage loginPage = new LoginPage(this._app);
            loginPage.WaitForBasicLoginPageToLoad();
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Description("UI Test for 'About' Scenario 1 - Validate the About page content")]
        [Property("JiraIssueID", "CDV6-6388")]
        public void AboutPageTests_TestMethod1()
        {
            MainMenu mainMenu = new MainMenu(this._app);

            mainMenu
                .NavigateToAboutPage()
                .WaitForAboutPageToLoad();
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        //[Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

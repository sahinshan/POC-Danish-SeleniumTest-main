using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects;
using System.Configuration;

namespace Phoenix.Portal.UITests.Websites.Website17
{
    public abstract class FunctionalTest
    {
        public IWebDriver driver;
        public OpenQA.Selenium.Support.UI.WebDriverWait wait;
        public string appURL;
        public string DownloadsDirectory;
        public Phoenix.DBHelper.DatabaseHelper dbHelper;

        #region Pages

        public HomePage homePage;
        public MemberHomePage memberHomePage;
        public MainMenu mainMenu;
        public EmailVerificationPage emailVerificationPage;
        public RegistrationPage registrationPage;
        public RegistrationSuccessPage registrationSuccessPage;

        #endregion



        [SetUp()]
        public void SetupTest()
        {
            DownloadsDirectory = TestContext.CurrentContext.TestDirectory + "\\Downloads";

            string browser = ConfigurationManager.AppSettings["browser"];
            this.SetDriver(browser, DownloadsDirectory);

            appURL = ConfigurationManager.AppSettings["website17URL"];
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 0, 7));

            dbHelper = new DBHelper.DatabaseHelper();


            homePage = new HomePage(driver, wait, appURL);
            memberHomePage = new MemberHomePage(driver, wait, appURL);
            mainMenu = new MainMenu(driver, wait, appURL);
            emailVerificationPage = new EmailVerificationPage(driver, wait, appURL);
            registrationPage = new RegistrationPage(driver, wait, appURL);
            registrationSuccessPage = new RegistrationSuccessPage(driver, wait, appURL);
        }

        [TearDown()]
        public virtual void MyTestCleanup()
        {
            driver.Quit();
        }

        private void SetDriver(string browser, string DownloadsDir)
        {
            switch (browser)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddUserProfilePreference("download.default_directory", DownloadsDir);
                    options.AddUserProfilePreference("disable-popup-blocking", "true");

                    driver = new ChromeDriver(options);
                    break;
                case "Firefox":
                    FirefoxProfile profile = new FirefoxProfile();
                    profile.SetPreference("browser.download.folderList", 2);
                    profile.SetPreference("browser.download.dir", DownloadsDir);
                    profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/csv,application/java-archive, application/x-msexcel,application/excel,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/x-excel,application/vnd.ms-excel,image/png,image/jpeg,text/html,text/plain,application/msword,application/xml,application/vnd.microsoft.portable-executable");

                    // Creating FirefoxOptions to set profile
                    FirefoxOptions option = new FirefoxOptions();
                    option.Profile = profile;

                    driver = new FirefoxDriver(option);
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();

        }


    }
}

using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using Phoenix.Portal.UITestsFramework.Websites.ProviderPortal.PageObjects;
using System.Configuration;
using System.Collections.Generic;

namespace Phoenix.Portal.UITests.Websites.ProviderPortal
{
    public abstract class FunctionalTest
    {
        public IWebDriver driver;
        public OpenQA.Selenium.Support.UI.WebDriverWait wait;
        public Phoenix.WebAPIHelper.WebAppAPI.WebAPIHelper WebAPIHelper;
        public string appURL;
        public string DownloadsDirectory;
        public Phoenix.DBHelper.DatabaseHelper dbHelper;
        public Portal.UITestsFramework.FileSystem.FileIOHelper fileIOHelper;
        public Portal.UITestsFramework.FileSystem.PdfHelper pdfHelper;

        #region Pages

        public MainMenu mainMenu;
        public HomePage homePage;
        public MemberHomePage memberHomePage;
        public PersonFormsPage personFormsPage;

        #endregion



        [SetUp()]
        public void SetupTest()
        {
            fileIOHelper = new UITestsFramework.FileSystem.FileIOHelper();
            pdfHelper = new UITestsFramework.FileSystem.PdfHelper();

            DownloadsDirectory = TestContext.CurrentContext.TestDirectory + "\\Downloads";

            fileIOHelper.CreateDirectoryAndRemoveFiles(DownloadsDirectory);

            string browser = ConfigurationManager.AppSettings["browser"];
            this.SetDriver(browser, DownloadsDirectory);

            appURL = ConfigurationManager.AppSettings["QAProviderPortalURL"];
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 0, 7));

            dbHelper = new DBHelper.DatabaseHelper();
            WebAPIHelper = new WebAPIHelper.WebAppAPI.WebAPIHelper();

            mainMenu = new MainMenu(driver, wait, appURL);
            homePage = new HomePage(driver, wait, appURL);
            memberHomePage = new MemberHomePage(driver, wait, appURL);
            personFormsPage = new PersonFormsPage(driver, wait, appURL);
        }

        [TearDown()]
        public virtual void MyTestCleanup()
        {
            driver.Quit();

            var jiraIssueID = (string)TestContext.CurrentContext.Test.Properties["JiraIssueID"].FirstOrDefault();
            
            //if we have a jira id for the test then we will update its status in jira
            if (!string.IsNullOrEmpty(jiraIssueID))
            {
                bool testPassed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;

                var zapi = new AtlassianServiceAPI.Models.Zapi()
                {
                    AccessKey = ConfigurationManager.AppSettings["AccessKey"],
                    SecretKey = ConfigurationManager.AppSettings["SecretKey"],
                    User = ConfigurationManager.AppSettings["User"],
                };

                var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
                };

                AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

                string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

                if (testPassed)
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Portal", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                else
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Portal", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


            }
        }

        private void SetDriver(string browser, string DownloadsDir)
        {
            switch (browser)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddUserProfilePreference("download.default_directory", DownloadsDir);
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
                    options.AddArgument("--disable-blink-features=AutomationControlled");

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

        public string GetCurrentWindow()
        {
            return this.driver.CurrentWindowHandle;
        }

        public List<string> GetAllWindows()
        {
            return this.driver.WindowHandles.ToList();
        }

        public void SwitchWindow(string WindowHandle)
        {
            driver.SwitchTo().Window(WindowHandle);
        }

        public string GetCurrentWindowURL()
        {
            return this.driver.Url;
        }

        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description");

            Type t = this.GetType();

            foreach (var method in t.GetMethods().OrderBy(c => c.Name).ToList())
            {
                TestAttribute testMethod = null;
                DescriptionAttribute descAttr = null;
                PropertyAttribute propertyAttr = null;

                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is TestAttribute)
                        testMethod = attribute as TestAttribute;

                    if (attribute is DescriptionAttribute)
                        descAttr = attribute as DescriptionAttribute;

                    if (attribute is PropertyAttribute && (attribute as PropertyAttribute).Properties.ContainsKey("JiraIssueID"))
                        propertyAttr = attribute as PropertyAttribute;
                }

                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty((string)propertyAttr.Properties["JiraIssueID"][0]))
                {
                    sb.AppendLine((string)propertyAttr.Properties["JiraIssueID"][0]);
                    continue;
                }
                if (testMethod != null)
                {
                    sb.AppendLine(method.Name + "," + ((string)(descAttr.Properties["Description"][0])).Replace(",", ";"));
                    continue;
                }

            }

            Console.WriteLine(sb.ToString());
        }
    }
}

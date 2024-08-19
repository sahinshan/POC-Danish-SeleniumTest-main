using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemManagementPage : CommonMethods
    {
        public SystemManagementPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By AzureBlobConnectionsLink = By.XPath("//h3[text()='Azure Blob Connections']");
        readonly By EDMSRepositoriesLink = By.XPath("//h3[text()='EDMS Repositories']");
        readonly By CacheMonitorLink = By.XPath("//h3[text()='Cache Monitor']");
        readonly By SystemSettingLink = By.XPath("//h3[text()='System Settings']");
        readonly By ScheduledJobsLink = By.XPath("//h3[text()='Scheduled Jobs']");
        readonly By PublicHolidaysLink = By.XPath("//h3[text()='Public Holidays']");

        public SystemManagementPage WaitForSystemManagementPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "System Management\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?");

            return this;
        }

        public SystemManagementPage ClickAzureBlobConnectionsLink()
        {
            WaitForElementToBeClickable(AzureBlobConnectionsLink);
            Click(AzureBlobConnectionsLink);

            return this;
        }

        public SystemManagementPage ClickEDMSRepositoriesLink()
        {
            WaitForElementToBeClickable(EDMSRepositoriesLink);
            Click(EDMSRepositoriesLink);

            return this;
        }
        public SystemManagementPage ClickCacheMonitorLink()
        {
            WaitForElementToBeClickable(CacheMonitorLink);
            Click(CacheMonitorLink);

            return this;
        }

        public SystemManagementPage ClickSystemSettingLink()
        {
            WaitForElementToBeClickable(SystemSettingLink);
            Click(SystemSettingLink);

            return this;
        }

        public SystemManagementPage ClickScheduledJobsLink()
        {
            WaitForElementToBeClickable(ScheduledJobsLink);
            Click(ScheduledJobsLink);

            return this;
        }

        public SystemManagementPage ClickPublicHolidaysLink()
        {
            WaitForElementToBeClickable(PublicHolidaysLink);
            Click(PublicHolidaysLink);

            return this;
        }

    }
}

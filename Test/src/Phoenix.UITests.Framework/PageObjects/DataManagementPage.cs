using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DataManagementPage: CommonMethods
    {
        public DataManagementPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By DataImportAreaButton = By.XPath("//h2/button[text()='Data Import']");
        readonly By DuplicateDetectionAreaButton = By.XPath("//h2/button[text()='Duplicate Detection']");
        readonly By FileDestructionAreaButton = By.XPath("//h2/button[text()='File Destruction']");
        readonly By ThirdPartyDataSyncAreaButton = By.XPath("//h2/button[text()='Third Party Data Sync']");


        readonly By MergedRecordsButton = By.XPath("//h3[text()='Merged Records']");
        readonly By DuplicateRecordsButton = By.XPath("//h3[text()='Duplicate Records']");
        readonly By FileDestructionGDPRButton = By.XPath("//h3[text()='File Destruction (GDPR)']");


        public DataManagementPage WaitForDataManagementPageToLoad()
        {
            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Data Management\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?")
                throw new Exception("Page title do not equals: \"Data Management\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?\" ");

            return this;
        }


        public DataManagementPage ClickDuplicateDetectionAreaButton()
        {
            this.Click(DuplicateDetectionAreaButton);

            return this;
        }

        public DataManagementPage ClickFileDestructionAreaButton()
        {
            this.Click(FileDestructionAreaButton);

            return this;
        }

        public DataManagementPage ClickMergedRecordsButton()
        {
            this.WaitForElementToBeClickable(MergedRecordsButton);
            this.Click(MergedRecordsButton);

            return this;
        }

        public DataManagementPage ClickDuplicateRecordsButton()
        {
            this.WaitForElementToBeClickable(DuplicateRecordsButton);
            this.Click(DuplicateRecordsButton);

            return this;
        }

        public DataManagementPage ClickFileDestructionGDPRButton()
        {
            this.WaitForElementToBeClickable(FileDestructionGDPRButton);
            this.Click(FileDestructionGDPRButton);

            return this;
        }


    }
}

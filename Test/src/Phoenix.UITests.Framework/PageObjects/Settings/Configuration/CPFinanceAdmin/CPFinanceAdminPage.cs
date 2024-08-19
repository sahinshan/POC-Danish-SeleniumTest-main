using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CPFinanceAdminPage : CommonMethods
    {
        public CPFinanceAdminPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

        #region CP Finance Admin Options
        readonly By PersonContractsAreaButton = By.XPath("//h2/button[text()='Person Contracts']");
        readonly By ServicesButton = By.XPath("//h3[text()='Services']");
        readonly By ServicesDetailButton = By.XPath("//h3[text()='Services Detail']");        

        readonly By CareProviderInvoicingAreaButton = By.XPath("//h2/button[text()='Care Provider Invoicing']");
        readonly By ExtractNamesButton = By.XPath("//h3[text()='Extract Names']");
        readonly By ExtractTypesButton = By.XPath("//h3[text()='Extract Types']");
        readonly By FinanceExtractBatchSetupsButton = By.XPath("//h3[text()='Finance Extract Batch Setups']");
        readonly By FinanceInvoiceBatchSetupsButton = By.XPath("//h3[text()='Finance Invoice Batch Setups']");
        readonly By ContractServicesButton = By.XPath("//h3[text()='Contract Services']");


        #endregion


        public CPFinanceAdminPage WaitForCPFinanceAdminPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Finance Admin\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?")
                throw new Exception("Page title do not equals: \"Finance Admin\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?\" ");

            return this;
        }

        public CPFinanceAdminPage ClickPersonContractsAreaButton()
        {
            WaitForElementToBeClickable(PersonContractsAreaButton);
            ScrollToElement(PersonContractsAreaButton);
            Click(PersonContractsAreaButton);

            return this;
        }

        public CPFinanceAdminPage ClickServicesButton()
        {
            WaitForElementToBeClickable(ServicesButton);
            ScrollToElement(ServicesButton);
            Click(ServicesButton);

            return this;
        }

        public CPFinanceAdminPage ClickServicesDetailButton()
        {
            WaitForElementToBeClickable(ServicesDetailButton);
            ScrollToElement(ServicesDetailButton);
            Click(ServicesDetailButton);

            return this;
        }

        public CPFinanceAdminPage ClickCareProviderInvoicingAreaButton()
        {
            WaitForElementToBeClickable(CareProviderInvoicingAreaButton);
            ScrollToElement(CareProviderInvoicingAreaButton);
            Click(CareProviderInvoicingAreaButton);

            return this;
        }

        public CPFinanceAdminPage ClickExtractNamesButton()
        {
            WaitForElementToBeClickable(ExtractNamesButton);
            ScrollToElement(ExtractNamesButton);
            Click(ExtractNamesButton);

            return this;
        }

        public CPFinanceAdminPage ClickExtractTypesButton()
        {
            WaitForElementToBeClickable(ExtractTypesButton);
            ScrollToElement(ExtractTypesButton);
            Click(ExtractTypesButton);

            return this;
        }

        public CPFinanceAdminPage ClickFinanceExtractBatchSetupsButton()
        {
            WaitForElementToBeClickable(FinanceExtractBatchSetupsButton);
            ScrollToElement(FinanceExtractBatchSetupsButton);
            Click(FinanceExtractBatchSetupsButton);

            return this;
        }

        public CPFinanceAdminPage ClickFinanceInvoiceBatchSetupsButton()
        {
            WaitForElementToBeClickable(FinanceInvoiceBatchSetupsButton);
            ScrollToElement(FinanceInvoiceBatchSetupsButton);
            Click(FinanceInvoiceBatchSetupsButton);

            return this;
        }

        public CPFinanceAdminPage ClickContractServicesButton()
        {
            WaitForElementToBeClickable(ContractServicesButton);
            ScrollToElement(ContractServicesButton);
            Click(ContractServicesButton);

            return this;
        }
    }
}

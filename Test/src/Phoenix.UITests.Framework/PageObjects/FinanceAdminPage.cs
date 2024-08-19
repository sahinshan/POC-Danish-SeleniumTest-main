using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceAdminPage: CommonMethods
    {
        public FinanceAdminPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By financeProcessingAreaButton = By.XPath("//h2/button[text()='Finance Processing']");
        readonly By financialAssessmentAreaButton = By.XPath("//h2/button[text()='Financial Assessment']");
        readonly By serviceProvisionAreaButton = By.XPath("//h2/button[text()='Service Provision']");
        readonly By careProviderInvoicingAreaButton = By.XPath("//h2/button[text()='Care Provider Invoicing']");
        readonly By careProviderPayrollAreaButton = By.XPath("//h2/button[text()='Care Provider Payroll']");
        readonly By personContractsAreaButton = By.XPath("//h2/button[text()='Person Contracts']");

        readonly By schedulesSetupButton = By.XPath("//h3[text()='Schedules Setup']");
        readonly By chargingRulesSetupButton = By.XPath("//h3[text()='Charging Rules Setup']");
        readonly By incomeSupportSetupButton = By.XPath("//h3[text()='Income Support Setup']");
        readonly By nonResidentialPolicyRateSetupsButton = By.XPath("//h3[text()='Non-Residential Policy Rate Setups']");
        readonly By chargesForServicesSetupButton = By.XPath("//h3[text()='Charges for Services Setup']");
        readonly By financialDetailRatesSetupButton = By.XPath("//h3[text()='Financial Detail Rates Setup']");
        readonly By financialDetailDisregardsButton = By.XPath("//h3[text()='Financial Detail Disregards']");
        readonly By contractServicesButton = By.XPath("//h3[text()='Contract Services']");


        #region Care Provider Payroll

        readonly By masterPayArrangementsButton = By.XPath("//h3[text()='Master Pay Arrangements']");

        #endregion

        readonly By serviceElement1Button = By.XPath("//h3[text()='Service Element 1']");
        readonly By serviceUpratesButton = By.XPath("//h3[text()='Service Uprates']");

        readonly By financeInvoiceBatchSetupsButton = By.XPath("//h3[text()='Finance Invoice Batch Setups']");
        readonly By financeTransactionTriggersButton = By.XPath("//h3[text()='Finance Transaction Triggers']");


        public FinanceAdminPage WaitForFinanceAdminPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Finance Admin\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?")
                throw new Exception("Page title do not equals: \"Finance Admin\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?\" ");

            return this;
        }


        public FinanceAdminPage ClickFinancialAssessmentAreaButton()
        {
            WaitForElementToBeClickable(financialAssessmentAreaButton);
            MoveToElementInPage(financialAssessmentAreaButton);
            Click(financialAssessmentAreaButton);

            return this;
        }

        public FinanceAdminPage ClickServiceProvisionAreaButton()
        {
            WaitForElementToBeClickable(serviceProvisionAreaButton);
            Click(serviceProvisionAreaButton);

            return this;
        }

        public FinanceAdminPage ClickSchedulesSetupButton()
        {
            WaitForElementToBeClickable(schedulesSetupButton);
            MoveToElementInPage(schedulesSetupButton);
            Click(schedulesSetupButton);

            return this;
        }

        public FinanceAdminPage ClickChargingRuleSetupButton()
        {
            WaitForElementToBeClickable(chargingRulesSetupButton);
            MoveToElementInPage(chargingRulesSetupButton);
            Click(chargingRulesSetupButton);

            return this;
        }


        public FinanceAdminPage ClickIncomeSupportSetupButton()
        {
            WaitForElementToBeClickable(incomeSupportSetupButton);
            MoveToElementInPage(incomeSupportSetupButton);
            Click(incomeSupportSetupButton);

            return this;
        }

        public FinanceAdminPage ClickNonResidentialPolicyRateSetupsButton()
        {
            WaitForElementToBeClickable(nonResidentialPolicyRateSetupsButton);
            MoveToElementInPage(nonResidentialPolicyRateSetupsButton);
            Click(nonResidentialPolicyRateSetupsButton);

            return this;
        }

        public FinanceAdminPage ClickChargesForServicesSetupButton()
        {
            WaitForElementToBeClickable(chargesForServicesSetupButton);
            MoveToElementInPage(chargesForServicesSetupButton);
            Click(chargesForServicesSetupButton);

            return this;
        }

        public FinanceAdminPage ClickFinancialDetailRatesSetupButton()
        {
            WaitForElementToBeClickable(financialDetailRatesSetupButton);
            MoveToElementInPage(financialDetailRatesSetupButton);
            Click(financialDetailRatesSetupButton);

            return this;
        }

        public FinanceAdminPage ClickFinancialDetailDisregardsButton()
        {
            WaitForElementToBeClickable(financialDetailDisregardsButton);
            MoveToElementInPage(financialDetailDisregardsButton);
            Click(financialDetailDisregardsButton);

            return this;
        }

        public FinanceAdminPage ClickServiceElement1Button()
        {
            WaitForElementToBeClickable(serviceElement1Button);
            MoveToElementInPage(serviceElement1Button);
            Click(serviceElement1Button);

            return this;
        }

        public FinanceAdminPage ClickFinanceProcessingAreaButton()
        {
            WaitForElementToBeClickable(financeProcessingAreaButton);
            MoveToElementInPage(financeProcessingAreaButton);
            Click(financeProcessingAreaButton);

            return this;
        }

        public FinanceAdminPage ClickCareProviderInvoicingExpandButton()
        {
            WaitForElementToBeClickable(careProviderInvoicingAreaButton);
            MoveToElementInPage(careProviderInvoicingAreaButton);
            Click(careProviderInvoicingAreaButton);

            return this;
        }

        public FinanceAdminPage ClickCareProviderPayrollExpandButton()
        {
            WaitForElementToBeClickable(careProviderPayrollAreaButton);
            MoveToElementInPage(careProviderPayrollAreaButton);
            Click(careProviderPayrollAreaButton);

            return this;
        }

        public FinanceAdminPage ClickFinanceInvoiceBatchSetupsButton()
        {
            WaitForElementToBeClickable(financeInvoiceBatchSetupsButton);
            MoveToElementInPage(financeInvoiceBatchSetupsButton);
            Click(financeInvoiceBatchSetupsButton);

            return this;
        }

        public FinanceAdminPage ClickMasterPayArrangementsButton()
        {
            WaitForElementToBeClickable(masterPayArrangementsButton);
            MoveToElementInPage(masterPayArrangementsButton);
            Click(masterPayArrangementsButton);

            return this;
        }

        public FinanceAdminPage ClickServiceUpratesButton()
        {
            WaitForElementToBeClickable(serviceUpratesButton);
            MoveToElementInPage(serviceUpratesButton);
            Click(serviceUpratesButton);

            return this;
        }

        public FinanceAdminPage ClickFinanceTransactionTriggersButton()
        {
            WaitForElementToBeClickable(financeTransactionTriggersButton);
            MoveToElementInPage(financeTransactionTriggersButton);
            Click(financeTransactionTriggersButton);

            return this;
        }

        public FinanceAdminPage ClickContractServicesButton()
        {
            WaitForElementToBeClickable(personContractsAreaButton);
            MoveToElementInPage(personContractsAreaButton);
            Click(personContractsAreaButton);

            WaitForElementToBeClickable(contractServicesButton);
            MoveToElementInPage(contractServicesButton);
            Click(contractServicesButton);

            return this;
        }
    }
}

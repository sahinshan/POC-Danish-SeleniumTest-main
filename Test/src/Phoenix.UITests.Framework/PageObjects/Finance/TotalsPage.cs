using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
    public class TotalsPage : CommonMethods
    {
        public TotalsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By FinanceInvoiceTransactionTotalIFrame = By.Id("iframe_FinanceInvoiceTransactionTotal");
        readonly By pageHeader = By.XPath("//h1[@id = 'CWPageTitle']");
        readonly By backButton = By.Id("CWClose");

        readonly By numberOfRecords = By.Id("NoOfRecords");
        readonly By netAmount = By.Id("NetAmount");
        readonly By vatAmount = By.Id("VatAmount");
        readonly By grossAmount = By.Id("GrossAmount");
        readonly By actualUnits = By.Id("ActualUnits");
        readonly By totalUnits = By.Id("TotalUnits");

        public TotalsPage WaitForTotalsPageToLoad(string parentRecordIdSuffix)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(cwDialog_IFrame(parentRecordIdSuffix));
            SwitchToIframe(cwDialog_IFrame(parentRecordIdSuffix));

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElement(FinanceInvoiceTransactionTotalIFrame);
            SwitchToIframe(FinanceInvoiceTransactionTotalIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(backButton);
            WaitForElementVisible(numberOfRecords);
            WaitForElementVisible(netAmount);
            WaitForElementVisible(vatAmount);
            WaitForElementVisible(grossAmount);
            WaitForElementVisible(actualUnits);
            WaitForElementVisible(totalUnits);

            return this;
        }

        public TotalsPage ClickBackButton()
        {
            MoveToElementInPage(backButton);
            WaitForElementToBeClickable(backButton);            
            Click(backButton);

            return this;
        }

        public TotalsPage ValidateNumberOfRecordsValue(string ExpectedValue)
        {
            WaitForElementVisible(numberOfRecords);
            string ActualValue = GetElementValueByJavascript("NoOfRecords");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

        public TotalsPage ValidateNetAmountValue(string ExpectedValue)
        {
            WaitForElementVisible(netAmount);
            string ActualValue = GetElementValueByJavascript("NetAmount");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

        public TotalsPage ValidateVatAmountValue(string ExpectedValue)
        {
            WaitForElementVisible(vatAmount);
            string ActualValue = GetElementValueByJavascript("VatAmount");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

        public TotalsPage ValidateGrossAmountValue(string ExpectedValue)
        {
            WaitForElementVisible(grossAmount);
            string ActualValue = GetElementValueByJavascript("GrossAmount");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

        public TotalsPage ValidateActualUnitsValue(string ExpectedValue)
        {
            WaitForElementVisible(actualUnits);
            string ActualValue = GetElementValueByJavascript("ActualUnits");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

        public TotalsPage ValidateTotalUnitsValue(string ExpectedValue)
        {
            WaitForElementVisible(totalUnits);
            string ActualValue = GetElementValueByJavascript("TotalUnits");
            Assert.AreEqual(ExpectedValue, ActualValue);

            return this;
        }

    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContractServicesWithRatesPage : CommonMethods
    {
        public ContractServicesWithRatesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, 'iframe_CWDialog_')][contains(@src, 'provider')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int cellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + cellPosition + "]");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");

        public ContractServicesWithRatesPage WaitForContractServicesWithRatesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ExportToExcelButton);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public ContractServicesWithRatesPage SearchRecords(string SearchQuery, string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);

            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(recordRow(RecordId));

            return this;
        }


        public ContractServicesWithRatesPage SearchRecords(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);

            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public ContractServicesWithRatesPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ContractServicesWithRatesPage OpenProviderRecord(Guid RecordId)
        {
            return OpenRecord(RecordId.ToString());
        }

        public ContractServicesWithRatesPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ContractServicesWithRatesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);

            Click(refreshButton);

            return this;
        }

        public ContractServicesWithRatesPage ValidateRecordCellText(string recordID, int cellPosition, string ExpectedText)
        {
            WaitForElement(recordCell(recordID, cellPosition));
            MoveToElementInPage(recordCell(recordID, cellPosition));
            ValidateElementText(recordCell(recordID, cellPosition), ExpectedText);

            return this;
        }

        public ContractServicesWithRatesPage ValidateRecordCellText(Guid recordID, int cellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(recordID.ToString(), cellPosition, ExpectedText);
        }
    }
}

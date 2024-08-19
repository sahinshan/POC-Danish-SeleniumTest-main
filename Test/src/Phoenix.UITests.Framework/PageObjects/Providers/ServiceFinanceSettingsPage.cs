using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceFinanceSettingsPage : CommonMethods
    {
        public ServiceFinanceSettingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovided&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ServiceFinanceSettingsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Finance Settings']");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        By recordRow(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[2]");

        By recordRowCheckBox(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[1]/input");

        By RatePeriodRecordCell(string RatePeriodID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RatePeriodID + "']/td[" + CellPosition + "]");

        public ServiceFinanceSettingsPage WaitForServiceFinanceSettingsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(ServiceFinanceSettingsPageHeader);
            
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(BulkEditButton);
            WaitForElementVisible(DeleteButton);

            return this;
        }

        public ServiceFinanceSettingsPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceFinanceSettingsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ServiceFinanceSettingsPage SearchServiceFinanceSettingsRecord(string SearchQuery, string ServiceFinanceSettingsId)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElementVisible(recordRow(ServiceFinanceSettingsId));

            return this;
        }


        public ServiceFinanceSettingsPage SearchServiceProvidedRatePeriodRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public ServiceFinanceSettingsPage OpenServiceFinanceSettingsRecord(string ServiceProvidedId)
        {
            WaitForElementToBeClickable(recordRow(ServiceProvidedId));
            MoveToElementInPage(recordRow(ServiceProvidedId));
            Click(recordRow(ServiceProvidedId));

            return this;
        }

        public ServiceFinanceSettingsPage SelectServiceFinanceSettingsRecord(string ServiceProvidedId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(ServiceProvidedId));
            MoveToElementInPage(recordRowCheckBox(ServiceProvidedId));
            Click(recordRowCheckBox(ServiceProvidedId));

            return this;
        }

        public ServiceFinanceSettingsPage DeleteServiceFinanceSettingsRecord()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceFinanceSettingsPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ServiceFinanceSettingsPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServiceFinanceSettingsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ServiceFinanceSettingsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public ServiceFinanceSettingsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RatePeriodRecordCell(RecordID, CellPosition));
            ValidateElementText(RatePeriodRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
    }
}

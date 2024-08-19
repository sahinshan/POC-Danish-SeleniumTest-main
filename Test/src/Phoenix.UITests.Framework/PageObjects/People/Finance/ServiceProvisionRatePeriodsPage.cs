using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ServiceProvisionRatePeriodsPage : CommonMethods
    {
        public ServiceProvisionRatePeriodsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ServiceProvisionRatePeriodsPageHeader = By.XPath("//div[@id='CWToolbar']//h1[text()='Service Provision Rate Periods']");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string RatePeriodID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RatePeriodID + "']/td[2]");
        By recordRowCheckBox(string RatePeriodID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RatePeriodID + "']/td[1]/input");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        By RatePeriodRecordCell(string RatePeriodID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RatePeriodID + "']/td[" + CellPosition + "]");

        public ServiceProvisionRatePeriodsPage WaitForServiceProvisionRatePeriodsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(ServiceProvisionRatePeriodsPageHeader);

            WaitForElement(NewRecordButton);
            WaitForElement(ExportToExcelButton);
            WaitForElement(AssignRecordButton);
            WaitForElement(BulkEditButton);
            WaitForElement(DeleteButton);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ServiceProvisionRatePeriodsPage SearchServiceProvisionRatePeriodRecord(string SearchQuery, string ServiceProvisionRatePeriodId)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(ServiceProvisionRatePeriodId));

            return this;
        }

        public ServiceProvisionRatePeriodsPage SearchServiceProvisionRatePeriodRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ServiceProvisionRatePeriodsPage OpenServiceProvisionRatePeriodRecord(string ServiceProvisionRatePeriodId)
        {
            WaitForElementToBeClickable(recordRow(ServiceProvisionRatePeriodId));
            MoveToElementInPage(recordRow(ServiceProvisionRatePeriodId));
            Click(recordRow(ServiceProvisionRatePeriodId));

            return this;
        }

        public ServiceProvisionRatePeriodsPage SelectServiceProvisionRatePeriodRecord(string ServiceProvisionRatePeriodId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(ServiceProvisionRatePeriodId));
            MoveToElementInPage(recordRowCheckBox(ServiceProvisionRatePeriodId));
            Click(recordRowCheckBox(ServiceProvisionRatePeriodId));

            return this;
        }

        public ServiceProvisionRatePeriodsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceProvisionRatePeriodsPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ClickCreateNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ServiceProvisionRatePeriodsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RatePeriodRecordCell(RecordID, CellPosition));
            ValidateElementText(RatePeriodRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ServiceProvisionRatePeriodsPage ValidateSelectedViewText(string ExpectedText)
        {
            WaitForElementVisible(viewsPicklist);
            MoveToElementInPage(viewsPicklist);
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);

            return this;
        }
    }
}

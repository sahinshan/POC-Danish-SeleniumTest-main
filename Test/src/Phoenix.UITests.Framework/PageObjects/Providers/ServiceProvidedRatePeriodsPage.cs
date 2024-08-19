using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceProvidedRatePeriodsPage : CommonMethods
    {
        public ServiceProvidedRatePeriodsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovided&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ServiceProvidedRatePeriodsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Provided Rate Periods']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");
        By recordRow(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[2]");
        By recordRowCheckBox(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[1]/input");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        By RatePeriodRecordCell(string RatePeriodID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RatePeriodID + "']/td[" + CellPosition + "]");

        public ServiceProvidedRatePeriodsPage WaitForServicesProvidedPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(ServiceProvidedRatePeriodsPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ServiceProvidedRatePeriodsPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvidedRatePeriodsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public ServiceProvidedRatePeriodsPage SearchServiceProvidedRatePeriodRecord(string SearchQuery, string ServiceProvidedId)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(ServiceProvidedId));

            return this;
        }


        public ServiceProvidedRatePeriodsPage SearchServiceProvidedRatePeriodRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public ServiceProvidedRatePeriodsPage OpenServiceProvidedRatePeriodRecord(string ServiceProvidedId)
        {
            WaitForElementToBeClickable(recordRow(ServiceProvidedId));
            MoveToElementInPage(recordRow(ServiceProvidedId));
            Click(recordRow(ServiceProvidedId));

            return this;
        }

        public ServiceProvidedRatePeriodsPage SelectServiceProvidedRatePeriodsRecord(string ServiceProvidedId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(ServiceProvidedId));
            MoveToElementInPage(recordRowCheckBox(ServiceProvidedId));
            Click(recordRowCheckBox(ServiceProvidedId));

            return this;
        }

        public ServiceProvidedRatePeriodsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);
            return this;
        }

        public ServiceProvidedRatePeriodsPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ServiceProvidedRatePeriodsPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServiceProvidedRatePeriodsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ServiceProvidedRatePeriodsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public ServiceProvidedRatePeriodsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RatePeriodRecordCell(RecordID, CellPosition));
            ValidateElementText(RatePeriodRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
    }
}

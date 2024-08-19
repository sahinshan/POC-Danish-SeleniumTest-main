using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceProvidedRateSchedulesPage : CommonMethods
    {

        public ServiceProvidedRateSchedulesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovidedrateperiod&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ServiceProvidedRateSchedulesPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Provided Rate Schedules']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");
        By recordRow(string rateScheduleID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[2]");
        By recordRowCheckBox(string rateScheduleID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[1]/input");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        By RatePeriodRecordCell(string rateScheduleID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[" + CellPosition + "]");

        public ServiceProvidedRateSchedulesPage WaitForServicesProvidedRateSchedulesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(ServiceProvidedRateSchedulesPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ServiceProvidedRateSchedulesPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvidedRateSchedulesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public ServiceProvidedRateSchedulesPage SearchServiceProvidedRateScheduleRecord(string SearchQuery, string ServiceProvidedId)
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


        public ServiceProvidedRateSchedulesPage SearchServiceProvidedRateScheduleRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public ServiceProvidedRateSchedulesPage OpenServiceProvidedRateScheduleRecord(string ServiceProvidedRateScheduleId)
        {
            WaitForElementToBeClickable(recordRow(ServiceProvidedRateScheduleId));
            MoveToElementInPage(recordRow(ServiceProvidedRateScheduleId));
            Click(recordRow(ServiceProvidedRateScheduleId));

            return this;
        }

        public ServiceProvidedRateSchedulesPage SelectServiceProvidedRateScheduleRecord(string ServiceProvidedRateScheduleId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(ServiceProvidedRateScheduleId));
            MoveToElementInPage(recordRowCheckBox(ServiceProvidedRateScheduleId));
            Click(recordRowCheckBox(ServiceProvidedRateScheduleId));

            return this;
        }

        public ServiceProvidedRateSchedulesPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);
            return this;
        }

        public ServiceProvidedRateSchedulesPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ServiceProvidedRateSchedulesPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServiceProvidedRateSchedulesPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ServiceProvidedRateSchedulesPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public ServiceProvidedRateSchedulesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RatePeriodRecordCell(RecordID, CellPosition));
            ValidateElementText(RatePeriodRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}

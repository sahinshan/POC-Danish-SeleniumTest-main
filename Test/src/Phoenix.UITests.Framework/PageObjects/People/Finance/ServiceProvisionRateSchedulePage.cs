using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ServiceProvisionRateSchedulePage : CommonMethods
    {

        public ServiceProvisionRateSchedulePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovisionrateperiod&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ServiceProvisionRateSchedulesPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Provision Rate Schedule']");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string rateScheduleID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[2]");
        By recordRowCheckBox(string rateScheduleID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[1]/input");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By Details_Tab = By.Id("CWNavGroup_EditForm");
        readonly By RateSchedules_Tab = By.Id("CWNavGroup_ServiceProvisionRateSchedules");

        By RatePeriodRecordCell(string rateScheduleID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + rateScheduleID + "']/td[" + CellPosition + "]");

        public ServiceProvisionRateSchedulePage WaitForServicesProvisionRateSchedulesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(ServiceProvisionRateSchedulesPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ServiceProvisionRateSchedulePage WaitForServicesProvisionRateSchedulesMenuSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(Details_Tab);
            WaitForElement(RateSchedules_Tab);

            return this;
        }

        public ServiceProvisionRateSchedulePage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvisionRateSchedulePage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ServiceProvisionRateSchedulePage SearchServiceProvisionRateScheduleRecord(string SearchQuery, string ServiceProvisionId)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(ServiceProvisionId));

            return this;
        }


        public ServiceProvisionRateSchedulePage SearchServiceProvisionRateScheduleRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public ServiceProvisionRateSchedulePage OpenServiceProvisionRateScheduleRecord(string ServiceProvisionRateScheduleId)
        {
            WaitForElementToBeClickable(recordRow(ServiceProvisionRateScheduleId));
            MoveToElementInPage(recordRow(ServiceProvisionRateScheduleId));
            Click(recordRow(ServiceProvisionRateScheduleId));

            return this;
        }

        public ServiceProvisionRateSchedulePage SelectServiceProvisionRateScheduleRecord(string ServiceProvisionRateScheduleId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(ServiceProvisionRateScheduleId));
            MoveToElementInPage(recordRowCheckBox(ServiceProvisionRateScheduleId));
            Click(recordRowCheckBox(ServiceProvisionRateScheduleId));

            return this;
        }

        public ServiceProvisionRateSchedulePage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceProvisionRateSchedulePage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ServiceProvisionRateSchedulePage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServiceProvisionRateSchedulePage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ServiceProvisionRateSchedulePage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public ServiceProvisionRateSchedulePage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RatePeriodRecordCell(RecordID, CellPosition));
            ValidateElementText(RatePeriodRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public ServiceProvisionRateSchedulePage ClickDetailsTab()
        {
            WaitForElementToBeClickable(Details_Tab);
            MoveToElementInPage(Details_Tab);
            Click(Details_Tab);

            return this;
        }

    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServicesProvidedPage : CommonMethods
    {

        public ServicesProvidedPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By servicesProvidedPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Services Provided']");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        By ServicesProvidedRow(string ServiceProvidedID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[2]");
        By ServicesProvidedRowCheckBox(string ServiceProvidedID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[1]/input");
        By ServicesProvidedRecordCell(string ServiceProvidedID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[" + CellPosition + "]");



        public ServicesProvidedPage WaitForServicesProvidedPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);            
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(servicesProvidedPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ServicesProvidedPage ClickBackButton()
        {
            WaitForElement(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServicesProvidedPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public ServicesProvidedPage SearchServiceProvidedRecord(string SearchQuery, string ServiceProvidedId)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ServicesProvidedRow(ServiceProvidedId));

            return this;
        }


        public ServicesProvidedPage SearchServiceProvidedRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public ServicesProvidedPage OpenServiceProvidedRecord(string ServiceProvidedId)
        {
            WaitForElement(ServicesProvidedRow(ServiceProvidedId));
            MoveToElementInPage(ServicesProvidedRow(ServiceProvidedId));
            Click(ServicesProvidedRow(ServiceProvidedId));

            return this;
        }

        public ServicesProvidedPage SelectServiceProvidedRecord(string ServiceProvidedId)
        {
            WaitForElement(ServicesProvidedRowCheckBox(ServiceProvidedId));
            MoveToElementInPage(ServicesProvidedRowCheckBox(ServiceProvidedId));
            Click(ServicesProvidedRowCheckBox(ServiceProvidedId));

            return this;
        }

        public ServicesProvidedPage DeleteServiceProvidedRecord()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);
            return this;
        }

        public ServicesProvidedPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ServicesProvidedPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ServicesProvidedPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(ServicesProvidedRow(RecordId));

            return this;
        }

        public ServicesProvidedPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(ServicesProvidedRow(RecordId), 7);

            return this;
        }

        public ServicesProvidedPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(ServicesProvidedRecordCell(RecordID, CellPosition));
            ValidateElementText(ServicesProvidedRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}

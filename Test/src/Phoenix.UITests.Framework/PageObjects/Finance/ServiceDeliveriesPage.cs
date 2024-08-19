using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
    public class ServiceDeliveriesPage : CommonMethods
    {
        public ServiceDeliveriesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By serviceprovisionFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ServiceDeliveriesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        
        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");



        public ServiceDeliveriesPage WaitForServiceDeliveriesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(serviceprovisionFrame);
            SwitchToIframe(serviceprovisionFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElementVisible(ServiceDeliveriesPageHeader);

            return this;
        }

        public ServiceDeliveriesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementVisible(searchTextBox);
            WaitForElementToBeClickable(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ServiceDeliveriesPage ClickSearchButton()
        {
            MoveToElementInPage(searchButton);
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);            

            return this;
        }

        public ServiceDeliveriesPage ClickRefreshButton()
        {
            MoveToElementInPage(refreshButton);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);
            
            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }

        public ServiceDeliveriesPage SelectServiceElement2Record(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceDeliveriesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            MoveToElementInPage(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public ServiceDeliveriesPage ValidateRecordCellContent(Guid RecordId, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellContent(RecordId.ToString(), CellPosition, ExpectedText);
        }

        public ServiceDeliveriesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public ServiceDeliveriesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ServiceDeliveriesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public ServiceDeliveriesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            bool messageDisplayedStatus = GetElementVisibility(noRecordsMessage);
            Assert.IsTrue(messageDisplayedStatus);

            return this;
        }

        public ServiceDeliveriesPage ValidateRecordIsVisible(string RecordID)
        {
            MoveToElementInPage(RecordIdentifier(RecordID));
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            Assert.IsTrue(GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

        public ServiceDeliveriesPage ValidateRecordIsNotVisible(string RecordID)
        {
            WaitForElementNotVisible(RecordIdentifier(RecordID), 10);
            Assert.IsFalse(GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

    }
}

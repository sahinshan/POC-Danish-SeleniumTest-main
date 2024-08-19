using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceElement2Page : CommonMethods
    {
        public ServiceElement2Page(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceElement2 = By.Id("iframe_serviceelement2");

        readonly By ServiceElement2PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        
        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");



        public ServiceElement2Page WaitForServiceElement2PageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ServiceElement2);
            SwitchToIframe(iframe_ServiceElement2);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServiceElement2PageHeader);

            return this;
        }

        public ServiceElement2Page WaitForServiceElement2PageToLoadFromFinanceAdminArea()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServiceElement2PageHeader);

            return this;
        }


        public ServiceElement2Page InsertSearchQuery(string SearchQuery)
        {
            WaitForElementVisible(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ServiceElement2Page TapSearchButton()
        {
            MoveToElementInPage(searchButton);
            WaitForElementVisible(searchButton);
            Click(searchButton);            

            return this;
        }

        public ServiceElement2Page SelectServiceElement2Record(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceElement2Page ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            MoveToElementInPage(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public ServiceElement2Page ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public ServiceElement2Page OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ServiceElement2Page ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            bool messageDisplayedStatus = GetElementVisibility(noRecordsMessage);
            Assert.IsTrue(messageDisplayedStatus);

            return this;
        }

        public ServiceElement2Page ValidateRecordIsVisible(string RecordID)
        {
            MoveToElementInPage(RecordIdentifier(RecordID));
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            Assert.IsTrue(GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

        public ServiceElement2Page ValidateRecordIsNotVisible(string RecordID)
        {
            WaitForElementNotVisible(RecordIdentifier(RecordID), 10);
            Assert.IsFalse(GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceMappingsPage : CommonMethods
    {
        public ServiceMappingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_serviceelement1 = By.Id("iframe_serviceelement1");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceelement1&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By ServiceMappingsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By pageHeaderElementSortOrded(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");


        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");



        public ServiceMappingsPage WaitForServiceMappingsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_serviceelement1);
            SwitchToIframe(iframe_serviceelement1);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(ServiceMappingsPageHeader);

            return this;
        }



        public ServiceMappingsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public ServiceMappingsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);
            return this;
        }

        public ServiceMappingsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ServiceMappingsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public ServiceMappingsPage SelectServiceMappingRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceMappingsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public ServiceMappingsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public ServiceMappingsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ServiceMappingsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public ServiceMappingsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public ServiceMappingsPage ValidateHeaderCellSortOrdedAscending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortasc");

            return this;
        }

        public ServiceMappingsPage ValidateHeaderCellSortOrdedDescending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortdesc");

            return this;
        }

    }
}
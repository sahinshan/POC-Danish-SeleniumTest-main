using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class GLCodeMappingsPage : CommonMethods
    {
        public GLCodeMappingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_serviceelement1 = By.Id("iframe_serviceelement1");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceelement1&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By GLCodeMappingsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

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

        By recordRowCellText(int RowPosition, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RowPosition + "]/td[" + CellPosition + "]");


        public GLCodeMappingsPage WaitForGLCodeMappingsPageToLoad()
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

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(GLCodeMappingsPageHeader);
            WaitForElementVisible(newRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public GLCodeMappingsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public GLCodeMappingsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);
            return this;
        }

        public GLCodeMappingsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public GLCodeMappingsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }

        public GLCodeMappingsPage SelectGLCodeMappingRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public GLCodeMappingsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public GLCodeMappingsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public GLCodeMappingsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public GLCodeMappingsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public GLCodeMappingsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(pageHeaderElement(CellPosition));
            WaitForElementVisible(pageHeaderElement(CellPosition));
            ValidateElementText(pageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public GLCodeMappingsPage ValidateHeaderCellSortOrdedAscending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortasc");

            return this;
        }

        public GLCodeMappingsPage ValidateHeaderCellSortOrdedDescending(int CellPosition)
        {
            WaitForElementVisible(pageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(pageHeaderElementSortOrded(CellPosition), "class", "sortdesc");

            return this;
        }

        public GLCodeMappingsPage ValidateRecordCellText(int RowPosition, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordRowCellText(RowPosition, CellPosition));
            ValidateElementText(recordRowCellText(RowPosition, CellPosition), ExpectedText);

            return this;
        }

        public GLCodeMappingsPage ValidateRecordVisibility(string RecordID, bool Visibility)
        {
            Assert.AreEqual(Visibility, GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

    }
}
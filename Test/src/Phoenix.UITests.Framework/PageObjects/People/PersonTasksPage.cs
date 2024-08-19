using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonTasksPage : CommonMethods
    {
        public PersonTasksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_TasksFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Tasks']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]//a");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By refreshButton = By.Id("CWRefreshButton");






        public PersonTasksPage WaitForPersonTasksPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_TasksFrame);
            SwitchToIframe(CWNavItem_TasksFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Tasks");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
            WaitForElement(recordsAreaHeaderCell(11));
            WaitForElement(recordsAreaHeaderCell(12));

            ScrollToElementViaJavascript(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Subject");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Due");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Status");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Regarding");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "Reason");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Responsible Team");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Responsible User");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "Created By");
            ScrollToElement(recordsAreaHeaderCell(10));
            ValidateElementText(recordsAreaHeaderCell(10), "Created On");
            ScrollToElement(recordsAreaHeaderCell(11));
            ValidateElementText(recordsAreaHeaderCell(11), "Modified By");
            ScrollToElement(recordsAreaHeaderCell(12));
            ValidateElementText(recordsAreaHeaderCell(12), "Modified On");

            return this;
        }
        public PersonTasksPage WaitForPersonTasksPageToLoadEmpty()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_TasksFrame);
            SwitchToIframe(CWNavItem_TasksFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Tasks");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
            WaitForElement(recordsAreaHeaderCell(11));
            WaitForElement(recordsAreaHeaderCell(12));

            ValidateElementText(recordsAreaHeaderCell(2), "Subject");
            ValidateElementText(recordsAreaHeaderCell(3), "Due");
            ValidateElementText(recordsAreaHeaderCell(4), "Status");
            ValidateElementText(recordsAreaHeaderCell(5), "Regarding");

            return this;
        }
        public PersonTasksPage WaitForPersonTasksPageToLoadAfterSearch()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_TasksFrame);
            SwitchToIframe(CWNavItem_TasksFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Tasks");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));

            ValidateElementText(recordsAreaHeaderCell(2), "Subject");
            ValidateElementText(recordsAreaHeaderCell(3), "Created By");
            ValidateElementText(recordsAreaHeaderCell(4), "Created On");
            ValidateElementText(recordsAreaHeaderCell(5), "Modified By");
            ValidateElementText(recordsAreaHeaderCell(6), "Modified On");

            return this;
        }
        public PersonTasksPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }


        public PersonTasksPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonTasksPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }
        public PersonTasksPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonTasksPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(noRecorrdsMainMessage);
                WaitForElementVisible(noRecordsSubMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecorrdsMainMessage, 3);
                WaitForElementNotVisible(noRecordsSubMessage, 3);
            }

            return this;
        }


        public PersonTasksPage SearchPersonTaskRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(recordID));

            return this;
        }
        public PersonTasksPage SearchPersonTaskRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public PersonTasksPage OpenPersonTaskRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }



        public PersonTasksPage SelectPersonTaskRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public PersonTasksPage ValidateBulkEditButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BulkEditButton);
            else
                WaitForElementNotVisible(BulkEditButton, 5);


            return this;
        }


        public PersonTasksPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }
        public PersonTasksPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
        public PersonTasksPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonTasksPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }
    }
}

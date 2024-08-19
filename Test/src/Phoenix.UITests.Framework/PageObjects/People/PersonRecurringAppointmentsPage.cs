using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecurringAppointmentsPage : CommonMethods
    {
        public PersonRecurringAppointmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_AppointmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Recurring Appointments']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        
        





        public PersonRecurringAppointmentsPage WaitForPersonRecurringAppointmentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Recurring Appointments");

            WaitForElementToBeClickable(recordsAreaHeaderCell(2));
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
            WaitForElement(recordsAreaHeaderCell(13));

            ScrollToElement(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Subject");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Recurrence Pattern");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Start Date");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Start Time");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "End Time");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Location");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Status");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "Responsible Team");

            return this;
        }
        public PersonRecurringAppointmentsPage WaitForPersonRecurringAppointmentsPageToLoadEmpty()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Recurring Appointments");

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

            ValidateElementText(recordsAreaHeaderCell(2), "Subject".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Recurrence Pattern".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Start Date".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "Start Time".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "End Time".ToUpper());

            return this;
        }
        public PersonRecurringAppointmentsPage WaitForPersonRecurringAppointmentsPageToLoadAfterSearch()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Recurring Appointments");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));

            ValidateElementText(recordsAreaHeaderCell(2), "Subject".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Created By".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Created On".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "Modified By".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "Modified On".ToUpper());

            return this;
        }
        public PersonRecurringAppointmentsPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }


        public PersonRecurringAppointmentsPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonRecurringAppointmentsPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }
        public PersonRecurringAppointmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentsPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
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


        public PersonRecurringAppointmentsPage SearchPersonRecurringAppointmentRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(recordID));

            return this;
        }
        public PersonRecurringAppointmentsPage SearchPersonRecurringAppointmentRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public PersonRecurringAppointmentsPage OpenPersonRecurringAppointmentRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }



        public PersonRecurringAppointmentsPage SelectPersonRecurringAppointmentRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        



        public PersonRecurringAppointmentsPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }
        public PersonRecurringAppointmentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
        public PersonRecurringAppointmentsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }
    }
}

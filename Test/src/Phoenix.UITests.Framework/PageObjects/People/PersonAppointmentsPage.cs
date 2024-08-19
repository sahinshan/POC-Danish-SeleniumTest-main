using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAppointmentsPage : CommonMethods
    {
        public PersonAppointmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_AppointmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Appointments']");

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

        
        public PersonAppointmentsPage WaitForPersonAppointmentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Appointments");

            WaitForElementVisible(recordsAreaHeaderCell(2));
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
            WaitForElement(recordsAreaHeaderCell(14));

            ScrollToElement(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Subject");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Start Date");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Start Time");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "End Date");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "End Time");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Location");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Priority");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "Status");
            ScrollToElement(recordsAreaHeaderCell(10));
            ValidateElementText(recordsAreaHeaderCell(10), "Responsible Team");

            WaitForElementToBeClickable(quickSearchButton);

            return this;
        }

        public PersonAppointmentsPage WaitForPersonAppointmentsPageHeaderSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Appointments");

            WaitForElement(NewRecordButton);
            WaitForElement(ExportToExcelButton);
            WaitForElement(DeleteRecordButton);
            WaitForElementToBeClickable(quickSearchButton);

            return this;
        }

        public PersonAppointmentsPage WaitForPersonAppointmentsPageToLoadEmpty()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Appointments");

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
            ValidateElementText(recordsAreaHeaderCell(3), "Start Date");
            ValidateElementText(recordsAreaHeaderCell(4), "Start Time");
            ValidateElementText(recordsAreaHeaderCell(5), "End Date");
            ValidateElementText(recordsAreaHeaderCell(6), "End Time");

            return this;
        }

        public PersonAppointmentsPage WaitForPersonAppointmentsPageToLoadAfterSearch()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Appointments");

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

        public PersonAppointmentsPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public PersonAppointmentsPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }

        public PersonAppointmentsPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }

        public PersonAppointmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonAppointmentsPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
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

        public PersonAppointmentsPage SearchPersonAppointmentRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(recordID));

            return this;
        }

        public PersonAppointmentsPage SearchPersonAppointmentRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonAppointmentsPage OpenPersonAppointmentRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonAppointmentsPage OpenPersonAppointmentRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId.ToString()));
           Click(recordRow(RecordId.ToString()));

            return this;
        }

        public PersonAppointmentsPage SelectPersonAppointmentRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonAppointmentsPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }

        public PersonAppointmentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonAppointmentsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

    }
}

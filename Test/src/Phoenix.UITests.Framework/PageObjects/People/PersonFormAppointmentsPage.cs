using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormAppointmentsPage : CommonMethods
    {
        public PersonFormAppointmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
        readonly By CWNavItem_AppointmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Appointments']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Start Date']");
        readonly By StartTimeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Start Time']");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='End Date']");
        readonly By EndTimeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='End Time']");
        readonly By LocationHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Location']");
        readonly By PriorityHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Priority']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By StartDateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By StartTimeCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By EndDateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By EndTimeCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By LocationCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[7]");
        By PriorityCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");
        


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormAppointmentsPage WaitForPersonFormAppointmentsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personFormRecordIFrame);
            SwitchToIframe(personFormRecordIFrame);

            WaitForElement(CWNavItem_AppointmentFrame);
            SwitchToIframe(CWNavItem_AppointmentFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(StartDateHeader);
            WaitForElement(StartTimeHeader);
            WaitForElement(EndDateHeader);
            WaitForElement(EndTimeHeader);
            WaitForElement(LocationHeader);
            WaitForElement(PriorityHeader);

            return this;
        }

        public PersonFormAppointmentsPage SearchPersonFormAppointmentRecord(string SearchQuery, string PersonFormAppointmentID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(PersonFormAppointmentID));

            return this;
        }
        public PersonFormAppointmentsPage SearchPersonFormAppointmentRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormAppointmentsPage OpenPersonFormAppointmentRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonFormAppointmentsPage SelectPersonFormAppointmentRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormAppointmentsPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormAppointmentsPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormAppointmentsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormAppointmentsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormAppointmentsPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SubjectCell(RecordId));
            MoveToElementInPage(SubjectCell(RecordId));
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidateStartDateCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(StartDateCell(RecordId));
            MoveToElementInPage(StartDateCell(RecordId));
            ValidateElementText(StartDateCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidateStartTimeCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(StartTimeCell(RecordId));
            MoveToElementInPage(StartTimeCell(RecordId));
            ValidateElementText(StartTimeCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidateEndDateCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(EndDateCell(RecordId));
            MoveToElementInPage(EndDateCell(RecordId));
            ValidateElementText(EndDateCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidateEndTimeCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(EndTimeCell(RecordId));
            MoveToElementInPage(EndTimeCell(RecordId));
            ValidateElementText(EndTimeCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidateLocationCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(LocationCell(RecordId));
            MoveToElementInPage(LocationCell(RecordId));
            ValidateElementText(LocationCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormAppointmentsPage ValidatePriorityCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(PriorityCell(RecordId));
            MoveToElementInPage(PriorityCell(RecordId));
            ValidateElementText(PriorityCell(RecordId), ExpectedText);

            return this;
        }



    }
}

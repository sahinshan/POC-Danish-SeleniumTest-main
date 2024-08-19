using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhoneCallsPage : CommonMethods
    {
        public PersonPhoneCallsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); 
        readonly By CWNavItem_PhoneCallFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Phone Calls']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By RecipientHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Recipient']");
        readonly By DirectionHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Direction']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Status']");
        readonly By PhoneCallDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Phone Call Date']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible Team']");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible User']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By CallerCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By RecipientCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By DirectionCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By PriorityCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By ReasonCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[7]");
        By RegardingCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[9]");
        By PhoneCallNumberCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[10]");
        By PhoneCallDateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[11]");
        By ResponsibleTeamCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[12]");
        By ResponsibleUserCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[13]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");



        public PersonPhoneCallsPage WaitForPersonPhoneCallsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PhoneCallFrame);
            SwitchToIframe(CWNavItem_PhoneCallFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(DirectionHeader);
            WaitForElement(ResponsibleTeamHeader);
            WaitForElement(ResponsibleUserHeader);
            WaitForElement(RecipientHeader);
            WaitForElement(StatusHeader);
            WaitForElement(PhoneCallDateHeader);

            return this;
        }

        public PersonPhoneCallsPage SearchPersonPhoneCallRecord(string SearchQuery, string PersonPhoneCallID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(PersonPhoneCallID));

            return this;
        }

        public PersonPhoneCallsPage SearchPersonPhoneCallRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonPhoneCallsPage OpenPersonPhoneCallRecord(string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementVisible(SubjectCell(RecordId));
            MoveToElementInPage(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonPhoneCallsPage SelectPersonPhoneCallRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonPhoneCallsPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonPhoneCallsPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }
        public PersonPhoneCallsPage ClickExportToExcelButton()
        {
            Click(ExportToExcelButton);

            return this;
        }


        public PersonPhoneCallsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonPhoneCallsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonPhoneCallsPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SubjectCell(RecordId));
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateCallerCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(CallerCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateRecipientCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RecipientCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateDirectionCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DirectionCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidatePriorityCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(PriorityCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateReasonCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ReasonCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateRegardingCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RegardingCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidatePhoneCallNumberCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(PhoneCallNumberCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidatePhoneCallDateCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(PhoneCallDateCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateResponsibleTeamCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordId), ExpectedText);

            return this;
        }
        public PersonPhoneCallsPage ValidateResponsibleUserCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleUserCell(RecordId), ExpectedText);

            return this;
        }
        



    }
}

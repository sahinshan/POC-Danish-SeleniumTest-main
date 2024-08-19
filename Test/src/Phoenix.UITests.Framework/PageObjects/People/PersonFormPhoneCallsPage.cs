using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormPhoneCallsPage : CommonMethods
    {
        public PersonFormPhoneCallsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
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
        By RecipientCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By DirectionCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[9]");
        By PhoneCallDateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[11]");
        By ResponsibleTeamCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[12]");
        By ResponsibleUserCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[13]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormPhoneCallsPage WaitForPersonFormPhoneCallsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personFormRecordIFrame);
            SwitchToIframe(personFormRecordIFrame);

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

        public PersonFormPhoneCallsPage SearchPersonFormPhoneCallRecord(string SearchQuery, string PersonFormPhoneCallID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(PersonFormPhoneCallID));

            return this;
        }
        public PersonFormPhoneCallsPage SearchPersonFormPhoneCallRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormPhoneCallsPage OpenPersonFormPhoneCallRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonFormPhoneCallsPage SelectPersonFormPhoneCallRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormPhoneCallsPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormPhoneCallsPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormPhoneCallsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormPhoneCallsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormPhoneCallsPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidateDirectionCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DirectionCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidateResponsibleTeamCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidateResponsibleUserCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleUserCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidateRecipientCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RecipientCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormPhoneCallsPage ValidatePhoneCallDateCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(PhoneCallDateCell(RecordId), ExpectedText);

            return this;
        }



    }
}

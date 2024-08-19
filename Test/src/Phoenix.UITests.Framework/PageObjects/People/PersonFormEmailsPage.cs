using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormEmailsPage : CommonMethods
    {
        public PersonFormEmailsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
        readonly By CWNavItem_EmailFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Emails']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By RegardingHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Regarding']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible Team']");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible User']");
        readonly By DueHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Due']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Status']");
        readonly By ReasonHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Reason']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By RegardingCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By ResponsibleTeamCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By ResponsibleUserCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By DueCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[7]");
        By ReasonCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");
        


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormEmailsPage WaitForPersonFormEmailsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personFormRecordIFrame);
            SwitchToIframe(personFormRecordIFrame);

            WaitForElement(CWNavItem_EmailFrame);
            SwitchToIframe(CWNavItem_EmailFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(RegardingHeader);
            WaitForElement(ResponsibleTeamHeader);
            WaitForElement(ResponsibleUserHeader);
            WaitForElement(DueHeader);
            WaitForElement(StatusHeader);
            WaitForElement(ReasonHeader);

            return this;
        }

        public PersonFormEmailsPage SearchPersonFormEmailRecord(string SearchQuery, string PersonFormEmailID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(PersonFormEmailID));

            return this;
        }
        public PersonFormEmailsPage SearchPersonFormEmailRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormEmailsPage OpenPersonFormEmailRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonFormEmailsPage SelectPersonFormEmailRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormEmailsPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormEmailsPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormEmailsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormEmailsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormEmailsPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateRegardingCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RegardingCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateResponsibleTeamCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateResponsibleUserCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleUserCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateDueCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DueCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormEmailsPage ValidateReasonCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ReasonCell(RecordId), ExpectedText);

            return this;
        }



    }
}

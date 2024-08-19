using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormLettersPage : CommonMethods
    {
        public PersonFormLettersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
        readonly By CWNavItem_LetterFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Letters']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By DirectionHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Direction']");
        readonly By RecipientHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Recipient']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Status']");
        readonly By SentReceivedDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Sent/Received Date']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible Team']");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible User']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By DirectionCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By RecipientCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By SentReceivedDateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");
        By ResponsibleTeamCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[11]");
        By ResponsibleUserCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[12]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormLettersPage WaitForPersonFormLettersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personFormRecordIFrame);
            SwitchToIframe(personFormRecordIFrame);

            WaitForElement(CWNavItem_LetterFrame);
            SwitchToIframe(CWNavItem_LetterFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(DirectionHeader);
            WaitForElement(ResponsibleTeamHeader);
            WaitForElement(ResponsibleUserHeader);
            WaitForElement(RecipientHeader);
            WaitForElement(StatusHeader);
            WaitForElement(SentReceivedDateHeader);

            return this;
        }

        public PersonFormLettersPage SearchPersonFormLetterRecord(string SearchQuery, string PersonFormLetterID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(PersonFormLetterID));

            return this;
        }
        public PersonFormLettersPage SearchPersonFormLetterRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormLettersPage OpenPersonFormLetterRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonFormLettersPage SelectPersonFormLetterRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormLettersPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormLettersPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormLettersPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormLettersPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormLettersPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateDirectionCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DirectionCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateResponsibleTeamCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateResponsibleUserCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleUserCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateRecipientCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RecipientCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormLettersPage ValidateSentReceivedDateCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SentReceivedDateCell(RecordId), ExpectedText);

            return this;
        }



    }
}

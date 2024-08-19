using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormCaseNotesPage : CommonMethods
    {
        public PersonFormCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
        readonly By CWNavItem_PersonCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Form Case Notes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By DateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Date']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Status']");
        readonly By CreatedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Created By']");
        readonly By CreatedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Created On']");
        readonly By ModifiedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Modified By']");
        readonly By ModifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Modified On']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By DateCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By CreatedByCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By CreatedOnCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By ModifiedByCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[7]");
        By ModifiedOnCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormCaseNotesPage WaitForPersonFormCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonCaseNoteFrame);
            SwitchToIframe(CWNavItem_PersonCaseNoteFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(DateHeader);
            WaitForElement(StatusHeader);
            WaitForElement(CreatedByHeader);
            WaitForElement(CreatedOnHeader);
            WaitForElement(ModifiedByHeader);
            WaitForElement(ModifiedOnHeader);


            return this;
        }

        public PersonFormCaseNotesPage SearchPersonFormCaseNoteRecord(string SearchQuery, string personFormCaseNoteID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(personFormCaseNoteID));

            return this;
        }
        public PersonFormCaseNotesPage SearchPersonFormCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormCaseNotesPage OpenPersonFormCaseNoteRecord(string RecordId)
        {
            WaitForElementToBeClickable(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public PersonFormCaseNotesPage SelectPersonFormCaseNoteRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormCaseNotesPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormCaseNotesPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormCaseNotesPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormCaseNotesPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateDateCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DateCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateCreatedByCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(CreatedByCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateCreatedOnCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(CreatedOnCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateModifiedByCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ModifiedByCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormCaseNotesPage ValidateModifiedOnCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ModifiedOnCell(RecordId), ExpectedText);

            return this;
        }



    }
}

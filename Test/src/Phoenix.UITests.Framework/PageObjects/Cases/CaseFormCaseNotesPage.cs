using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormCaseNotesPage : CommonMethods
    {
        public CaseFormCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]"); 
        readonly By CWNavItem_CaseFormCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Case Form Case Notes']");


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



        public CaseFormCaseNotesPage WaitForCaseFormCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElement(CWNavItem_CaseFormCaseNoteFrame);
            SwitchToIframe(CWNavItem_CaseFormCaseNoteFrame);
            
            WaitForElementNotVisible("CWRefreshPanel", 100);

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

        public CaseFormCaseNotesPage SearchCaseFormCaseNoteRecord(string SearchQuery, string CaseFormCaseNoteID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(CaseFormCaseNoteID));

            return this;
        }
        public CaseFormCaseNotesPage SearchCaseFormCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CaseFormCaseNotesPage OpenCaseFormCaseNoteRecord(string RecordId)
        {
            WaitForElementToBeClickable(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public CaseFormCaseNotesPage SelectCaseFormCaseNoteRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public CaseFormCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public CaseFormCaseNotesPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public CaseFormCaseNotesPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public CaseFormCaseNotesPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public CaseFormCaseNotesPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateDateCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DateCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateCreatedByCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(CreatedByCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateCreatedOnCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(CreatedOnCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateModifiedByCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ModifiedByCell(RecordId), ExpectedText);

            return this;
        }
        public CaseFormCaseNotesPage ValidateModifiedOnCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ModifiedOnCell(RecordId), ExpectedText);

            return this;
        }



    }
}

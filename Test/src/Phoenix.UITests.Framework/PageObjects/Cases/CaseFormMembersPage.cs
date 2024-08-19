using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormMembersPage : CommonMethods
    {
        public CaseFormMembersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]"); 
        readonly By CWNavItem_CaseFormMemberFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Form (Case) Members']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

       


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



        public CaseFormMembersPage WaitForCaseFormMembersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseFormRecordIFrame);
            SwitchToIframe(caseFormRecordIFrame);

            WaitForElement(CWNavItem_CaseFormMemberFrame);
            SwitchToIframe(CWNavItem_CaseFormMemberFrame);

            WaitForElement(pageHeader);
            
           
            return this;
        }

        public CaseFormMembersPage SearchCaseFormLetterRecord(string SearchQuery, string CaseFormLetterID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(CaseFormLetterID));

            return this;
        }
        public CaseFormMembersPage SearchCaseFormLetterRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CaseFormMembersPage OpenCaseFormLetterRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            Click(SubjectCell(RecordId));

            return this;
        }

        public CaseFormMembersPage SelectCaseFormLetterRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public CaseFormMembersPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public CaseFormMembersPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public CaseFormMembersPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
       


    }
}

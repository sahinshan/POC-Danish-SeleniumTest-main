using OpenQA.Selenium;


namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFinancialAssessmentCaseNotesPage : CommonMethods
    {
        public PersonFinancialAssessmentCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessment&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWNavItem_PersonCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[contains(text(),'Person Physical Observation Case Notes')]");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By AssignButton = By.Id("TI_AssignRecordButton");


        readonly By description_TextAreaField = By.XPath("//*[@id='CWField_notes']");
      

        readonly By reason_RemoveButton = By.Id("CWClearLookup_activityreasonid");



        public PersonFinancialAssessmentCaseNotesPage WaitForPersonFinancialAssessmentCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonCaseNoteFrame);
            SwitchToIframe(CWNavItem_PersonCaseNoteFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            

            return this;
        }

        public PersonFinancialAssessmentCaseNotesPage SearchPersonFinacialAssessmentCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public PersonFinancialAssessmentCaseNotesPage OpenPersonFinacialAssessmentCaseNoteRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));
            return this;
        }

        public PersonFinancialAssessmentCaseNotesPage SelectPersonFinacialAssessmentCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


       

    }
}

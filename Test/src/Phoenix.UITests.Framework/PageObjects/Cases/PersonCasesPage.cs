using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCasesPage : CommonMethods
    {
        public PersonCasesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By PersonCasesPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Cases']");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By iframe_CWDialog1 = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&id')]");
        readonly By RelatedRecordPanelIFrame = By.Id("CWUrlPanel_IFrame");


        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowUser(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[text() ='Automation CDV6-17002 Test User 1']");
        By CaseRow(string RecordId) => By.XPath("//table/tbody/tr[@id='"+ RecordId +"']/td[1]");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");


        By record(string RecordId) => By.XPath("//*[@id='" + RecordId + "']/td[1]/input");

        By CaseRowCheckBox(string CaseID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + CaseID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");




        public PersonCasesPage WaitForPersonCasesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(RelatedRecordPanelIFrame);
            SwitchToIframe(RelatedRecordPanelIFrame);

            WaitForElement(PersonCasesPageHeader);
            ValidateElementText(PersonCasesPageHeader, "Cases");

            WaitForElement(NewRecordButton);

            return this;
        }


        public PersonCasesPage WaitForPersonCommunityCasesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog1);
            SwitchToIframe(iframe_CWDialog1);

            WaitForElement(RelatedRecordPanelIFrame);
            SwitchToIframe(RelatedRecordPanelIFrame);

            WaitForElement(PersonCasesPageHeader);
            ValidateElementText(PersonCasesPageHeader, "Cases");

            WaitForElement(NewRecordButton);

            return this;
        }

        public PersonCasesPage SearchCaseRecord(string SearchQuery, string CaseID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(CaseRow(CaseID));

            return this;
        }


        public PersonCasesPage SearchCaseRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }
        public PersonCasesPage OpenCaseRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonCasesPage OpenCaseIdRecord(string RecordId)
        {
            WaitForElement(recordRowUser(RecordId));
            ScrollToElement(recordRowUser(RecordId));
            driver.FindElement(recordRowUser(RecordId)).Click();
          

            return this;
        }

        public PersonCasesPage SelectCaseRecord(string CaseId)
        {
            WaitForElement(CaseRowCheckBox(CaseId));
            Click(CaseRowCheckBox(CaseId));

            return this;
        }

        public PersonCasesPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonCasesPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonCasesPage ClickExportToExcel()
        {
            Click(ExportToExcelButton);

            return this;
        }

        public PersonCasesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }
    }
}

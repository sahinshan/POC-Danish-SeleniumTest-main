using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseTasksPage : CommonMethods
    {
        public CaseTasksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWNavItem_TaskFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Tasks']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");






        public CaseTasksPage WaitForCaseTasksPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElement(CWNavItem_TaskFrame);
            SwitchToIframe(CWNavItem_TaskFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Tasks");

            return this;
        }

        public CaseTasksPage SearchCaseTaskRecord(string SearchQuery, string CaseID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(CaseID));

            return this;
        }

        public CaseTasksPage SearchCaseTaskRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CaseTasksPage OpenCaseTaskRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public CaseTasksPage SelectCaseTaskRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public CaseTasksPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public CaseTasksPage ClickBulkEditButton()
        {
            Click(BulkEditButton);

            return this;
        }
        public CaseTasksPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

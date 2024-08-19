using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class InpatientLeaveAwolCaseNotesPage : CommonMethods
    {
        public InpatientLeaveAwolCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By inpatientleaveawolIframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientleaveawol&')]");
        readonly By CWNavItem_InpatientLeaveAwolCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Case Notes (For Inpatient Leave Awol)']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public InpatientLeaveAwolCaseNotesPage WaitForInpatientLeaveAwolCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(inpatientleaveawolIframe);
            SwitchToIframe(inpatientleaveawolIframe);

            WaitForElement(CWNavItem_InpatientLeaveAwolCaseNoteFrame);
            SwitchToIframe(CWNavItem_InpatientLeaveAwolCaseNoteFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public InpatientLeaveAwolCaseNotesPage SearchInpatientLeaveAwolCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public InpatientLeaveAwolCaseNotesPage SearchInpatientLeaveAwolCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public InpatientLeaveAwolCaseNotesPage OpenInpatientLeaveAwolCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public InpatientLeaveAwolCaseNotesPage SelectInpatientLeaveAwolCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public InpatientLeaveAwolCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

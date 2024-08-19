using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCarePlanCaseNotesPage : CommonMethods
    {
        public PersonCarePlanCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personcareplanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplan&')]"); 
        readonly By CWNavItem_CaseNotesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Care Plan Case Notes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public PersonCarePlanCaseNotesPage WaitForPersonCarePlanCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(CWNavItem_CaseNotesFrame);
            SwitchToIframe(CWNavItem_CaseNotesFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public PersonCarePlanCaseNotesPage SearchPersonCarePlanCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public PersonCarePlanCaseNotesPage SearchPersonCarePlanCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonCarePlanCaseNotesPage OpenPersonCarePlanCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonCarePlanCaseNotesPage SelectPersonCarePlanCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonCarePlanCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

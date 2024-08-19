using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CourtDatesAndOutcomesCaseNotesPage : CommonMethods
    {
        public CourtDatesAndOutcomesCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By mhacourtdateoutcomeIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mhacourtdateoutcome&')]");
        readonly By CWNavItem_MHACourtDateOutcomeCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Court Dates and Outcomes Case Notes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public CourtDatesAndOutcomesCaseNotesPage WaitForCourtDatesAndOutcomesCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(mhacourtdateoutcomeIFrame);
            SwitchToIframe(mhacourtdateoutcomeIFrame);

            WaitForElement(CWNavItem_MHACourtDateOutcomeCaseNoteFrame);
            SwitchToIframe(CWNavItem_MHACourtDateOutcomeCaseNoteFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public CourtDatesAndOutcomesCaseNotesPage SearchCourtDateAndOutcomeCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public CourtDatesAndOutcomesCaseNotesPage SearchCourtDateAndOutcomeCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CourtDatesAndOutcomesCaseNotesPage OpenCourtDateAndOutcomeCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public CourtDatesAndOutcomesCaseNotesPage SelectCourtDateAndOutcomeCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CourtDatesAndOutcomesCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

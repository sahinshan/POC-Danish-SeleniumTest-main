using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage : CommonMethods
    {
        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By mharightsandrequestsIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mharightsandrequests&')]");
        readonly By CWNavItem_MHARightsAndRequestsCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Rights and Request for an IMHA and MHA Appeal Case Notes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage WaitForRightsAndRequestForAnIMHAAndMHAAppealCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(mharightsandrequestsIFrame);
            SwitchToIframe(mharightsandrequestsIFrame);

            WaitForElement(CWNavItem_MHARightsAndRequestsCaseNoteFrame);
            SwitchToIframe(CWNavItem_MHARightsAndRequestsCaseNoteFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage SearchRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage SearchRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage OpenRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage SelectRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class Section117EntitlementCaseNotesPage : CommonMethods
    {
        public Section117EntitlementCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By mhaaftercareentitlementIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mhaaftercareentitlement&')]");
        readonly By CWNavItem_MHAAftercareEntitlementCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Section 117 Entitlement Case Notes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public Section117EntitlementCaseNotesPage WaitForSection117EntitlementCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(mhaaftercareentitlementIFrame);
            SwitchToIframe(mhaaftercareentitlementIFrame);

            WaitForElement(CWNavItem_MHAAftercareEntitlementCaseNoteFrame);
            SwitchToIframe(CWNavItem_MHAAftercareEntitlementCaseNoteFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public Section117EntitlementCaseNotesPage SearchSection117EntitlementCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public Section117EntitlementCaseNotesPage SearchSection117EntitlementCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public Section117EntitlementCaseNotesPage OpenSection117EntitlementCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public Section117EntitlementCaseNotesPage SelectSection117EntitlementCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public Section117EntitlementCaseNotesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

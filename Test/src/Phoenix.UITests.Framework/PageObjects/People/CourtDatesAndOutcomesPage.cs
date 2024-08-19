using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CourtDatesAndOutcomesPage : CommonMethods
    {
        public CourtDatesAndOutcomesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personmhalegalstatusIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personmhalegalstatus&')]");
        readonly By CWNavItem_MHACourtDateOutcomeFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Court Dates and Outcomes']");


        readonly By viewSelector = By.Id("CWViewSelector");
        By viewSelectorOption(string Text) => By.XPath("//*[@id='SysView']/option[text()='" + Text + "']");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public CourtDatesAndOutcomesPage WaitForCourtDatesAndOutcomesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personmhalegalstatusIFrame);
            SwitchToIframe(personmhalegalstatusIFrame);

            WaitForElement(CWNavItem_MHACourtDateOutcomeFrame);
            SwitchToIframe(CWNavItem_MHACourtDateOutcomeFrame);

            WaitForElement(pageHeader);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public CourtDatesAndOutcomesPage SearchCourtDatesAndOutcomeRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public CourtDatesAndOutcomesPage SearchCourtDatesAndOutcomeRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CourtDatesAndOutcomesPage OpenCourtDatesAndOutcomeRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public CourtDatesAndOutcomesPage SelectCourtDatesAndOutcomeRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CourtDatesAndOutcomesPage SelectView(string ViewText)
        {
            WaitForElementToBeClickable(viewSelector);
            Click(viewSelector);
            Click(viewSelectorOption(ViewText));

            return this;
        }

        public CourtDatesAndOutcomesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RightsAndRequestsForAnIMHAAndMHAAppealPage : CommonMethods
    {
        public RightsAndRequestsForAnIMHAAndMHAAppealPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personmhalegalstatusIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personmhalegalstatus&')]");
        readonly By CWNavItem_MHARightsAndRequestsFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Rights and Requests for an IMHA and MHA Appeal']");


        readonly By viewSelector = By.XPath("//*[@id='CWViewSelector']");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public RightsAndRequestsForAnIMHAAndMHAAppealPage WaitForRightsAndRequestsForAnIMHAAndMHAAppealPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personmhalegalstatusIFrame);
            SwitchToIframe(personmhalegalstatusIFrame);

            WaitForElement(CWNavItem_MHARightsAndRequestsFrame);
            SwitchToIframe(CWNavItem_MHARightsAndRequestsFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage SearchRightsAndRequestForAnIMHAAndMHAAppealRecord(string SearchQuery, string PersonID)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage SearchRightsAndRequestForAnIMHAAndMHAAppealRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage OpenRightsAndRequestForAnIMHAAndMHAAppealRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage SelectRightsAndRequestForAnIMHAAndMHAAppealRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage SelectView(string ViewTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            Click(viewSelector);
            SelectPicklistElementByText(viewSelector, ViewTextToSelect);

            return this;
        }
    }
}

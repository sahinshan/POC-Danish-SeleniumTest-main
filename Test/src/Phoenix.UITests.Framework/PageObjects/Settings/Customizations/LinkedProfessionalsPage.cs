using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class LinkedProfessionalsPage : CommonMethods
    {
        public LinkedProfessionalsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=communityclinicdiaryviewsetup&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By recordCheckListItem(string recordID) => By.XPath("//div[@id = 'CWCheckList']/ul/li/*[@id='" + recordID + "'][1]");



        public LinkedProfessionalsPage WaitForLinkedProfessionalsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(pageHeader);

            ValidateElementTextContainsText(pageHeader, "Linked Professionals");

            return this;
        }

        public LinkedProfessionalsPage InsertQuickSearchText(string Text)
        {
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public LinkedProfessionalsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LinkedProfessionalsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public LinkedProfessionalsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LinkedProfessionalsPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckListItem(RecordID));
            Click(recordCheckListItem(RecordID));

            return this;
        }

        public LinkedProfessionalsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public LinkedProfessionalsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public LinkedProfessionalsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}

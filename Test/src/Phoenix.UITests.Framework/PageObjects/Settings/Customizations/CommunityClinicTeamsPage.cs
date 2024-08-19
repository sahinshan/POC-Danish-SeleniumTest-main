using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class CommunityClinicTeamsPage : CommonMethods
    {
        public CommunityClinicTeamsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By recordCheckListItem(string recordID) => By.XPath("//div[@id = 'CWCheckList']/ul/li/*[@id='" + recordID + "'][1]");



        public CommunityClinicTeamsPage WaitForCommunityClinicTeamsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);

            ValidateElementTextContainsText(pageHeader, "Community/Clinic Teams");

            return this;
        }

        public CommunityClinicTeamsPage InsertQuickSearchText(string Text)
        {
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public CommunityClinicTeamsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CommunityClinicTeamsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public CommunityClinicTeamsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CommunityClinicTeamsPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckListItem(RecordID));
            Click(recordCheckListItem(RecordID));

            return this;
        }

        public CommunityClinicTeamsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public CommunityClinicTeamsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public CommunityClinicTeamsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}

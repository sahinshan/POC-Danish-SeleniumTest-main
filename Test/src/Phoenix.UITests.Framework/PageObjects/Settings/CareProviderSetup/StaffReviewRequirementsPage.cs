using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewRequirementsPage : CommonMethods
    {
        public StaffReviewRequirementsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecord_Button = By.XPath("//div/button[@title='Create new record']");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By name_Link = By.XPath("//a[@title='Name']");

        By headerCell_Link(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]/div");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");



        public StaffReviewRequirementsPage WaitForStaffReviewRequirementsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Staff Review Requirements");

            return this;
        }

        public StaffReviewRequirementsPage ValidateShouldnotdisplaycolumnName()
        {
            ValidateElementDoNotExist(name_Link);

            return this;
        }

        public StaffReviewRequirementsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecord_Button);
            Click(newRecord_Button);

            return this;
        }

        public StaffReviewRequirementsPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public StaffReviewRequirementsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public StaffReviewRequirementsPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public StaffReviewRequirementsPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));

            this.Click(recordRow(RecordID));

            return this;
        }

        public StaffReviewRequirementsPage ClickHeaderCell(int CellPosition)
        {
            ScrollToElement(headerCell_Link(CellPosition));
            Click(headerCell_Link(CellPosition));

            return this;
        }

        public StaffReviewRequirementsPage ValidateStaffReviewRequrimentDisplayed(string ExpectedText)
        {
            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }
    }
}

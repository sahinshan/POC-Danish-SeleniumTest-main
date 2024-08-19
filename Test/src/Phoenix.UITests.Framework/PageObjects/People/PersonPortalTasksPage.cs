using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPortalTasksPage : CommonMethods
    {

        public PersonPortalTasksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_PortalTasksFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='To Do List']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Title']");
        readonly By TargetUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Target User']");
        readonly By DueDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Due Date']");
        readonly By ActionHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Action']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Status']");
        

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By titleCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By targetUserCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By dueDateCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By actionCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By statusCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public PersonPortalTasksPage WaitForPersonPortalTasksPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_PortalTasksFrame);
            SwitchToIframe(CWNavItem_PortalTasksFrame);

            WaitForElement(pageHeader);

            WaitForElement(NameHeader);
            WaitForElement(TargetUserHeader);
            WaitForElement(ActionHeader);
            WaitForElement(StatusHeader);
            WaitForElement(DueDateHeader);

            return this;
        }

        

        public PersonPortalTasksPage ValidateTitleCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(titleCell(RecordID), ExpectedText);

            return this;
        }
        public PersonPortalTasksPage ValidateTargetUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(targetUserCell(RecordID), ExpectedText);

            return this;
        }
        public PersonPortalTasksPage ValidateActionCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(actionCell(RecordID), ExpectedText);

            return this;
        }
        public PersonPortalTasksPage ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(statusCell(RecordID), ExpectedText);

            return this;
        }
        public PersonPortalTasksPage ValidatedueDateCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(dueDateCell(RecordID), ExpectedText);

            return this;
        }



        public PersonPortalTasksPage ClickOnRecord(string RecordID)
        {
            WaitForElementToBeClickable(titleCell(RecordID));
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(titleCell(RecordID));

            return this;
        }
        public PersonPortalTasksPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public PersonPortalTasksPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public PersonPortalTasksPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public PersonPortalTasksPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public PersonPortalTasksPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public PersonPortalTasksPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonPortalTasksPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonPortalTasksPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public PersonPortalTasksPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

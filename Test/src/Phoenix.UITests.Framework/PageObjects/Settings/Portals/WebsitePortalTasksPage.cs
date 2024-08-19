using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebsitePortalTasksPage : CommonMethods
    {

        public WebsitePortalTasksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_PortalTasksFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='To Do List']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*[text()='Title']");
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



        By titleSearchCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By actionSearchCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By dueDateSearchCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By statusSearchCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");


        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebsitePortalTasksPage WaitForWebsitePortalTasksPageToLoad()
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
            WaitForElement(ActionHeader);
            WaitForElement(StatusHeader);
            WaitForElement(DueDateHeader);

            return this;
        }



        public WebsitePortalTasksPage ValidateTitleCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(titleCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateTargetUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(targetUserCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateActionCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(actionCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(statusCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidatedueDateCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(dueDateCell(RecordID), ExpectedText);

            return this;
        }


        public WebsitePortalTasksPage ValidateTitleSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(titleSearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateActionSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(actionSearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateDueDateSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(dueDateSearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ValidateStatusSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(statusSearchCell(RecordID), ExpectedText);

            return this;
        }




        public WebsitePortalTasksPage ClickOnRecord(string RecordID)
        {
            WaitForElementToBeClickable(titleCell(RecordID));
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(titleCell(RecordID));

            return this;
        }
        public WebsitePortalTasksPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebsitePortalTasksPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebsitePortalTasksPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebsitePortalTasksPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebsitePortalTasksPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebsitePortalTasksPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsitePortalTasksPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsitePortalTasksPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebsitePortalTasksPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

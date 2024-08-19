using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebsiteUserPasswordHistoryPage : CommonMethods
    {
        public WebsiteUserPasswordHistoryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuser&')]");
        readonly By CWNavItem_WebsiteUserPasswordHistoryFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website User Password History']");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*[text()='Created On']");

        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");

        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebsiteUserPasswordHistoryPage WaitForWebsiteUserPasswordHistoryPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteUserPasswordHistoryFrame);
            SwitchToIframe(CWNavItem_WebsiteUserPasswordHistoryFrame);

            WaitForElement(pageHeader);

            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);

            return this;
        }


        public WebsiteUserPasswordHistoryPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ClickOnWebSiteUserPasswordHistoryRecord(string RecordID)
        {
            WaitForElementToBeClickable(createdByCell(RecordID));

            Click(createdByCell(RecordID));

            return this;
        }
        public WebsiteUserPasswordHistoryPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(createdByCell(RecordID));

            return this;
        }
        public WebsiteUserPasswordHistoryPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(createdByCell(RecordID), 3);

            return this;
        }


        public WebsiteUserPasswordHistoryPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }

        public WebsiteUserPasswordHistoryPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }


        public WebsiteUserPasswordHistoryPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebsiteUserPasswordHistoryPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

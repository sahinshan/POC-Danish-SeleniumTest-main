using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteSitemapsPage : CommonMethods
    {
        public WebSiteSitemapsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteSitemapFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Sitemaps']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        

        #region Results Area

        readonly By nameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*[text()='Name']");
        readonly By WebsiteHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*[text()='Website']");
        readonly By TypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*[text()='Type']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/*[text()='Modified On']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By nameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By WebsiteCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By TypeCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By modifiedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");
        By modifiedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[8]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteSitemapsPage WaitForWebSiteSitemapsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteSitemapFrame);
            SwitchToIframe(CWNavItem_WebsiteSitemapFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(WebsiteHeader);
            WaitForElement(TypeHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }

        

        public WebSiteSitemapsPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateWebsiteCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateTypeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(TypeCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteSitemapsPage ClickOnWebSiteSitemapRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSiteSitemapsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteSitemapsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteSitemapsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteSitemapsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public WebSiteSitemapsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public WebSiteSitemapsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteSitemapsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSitemapsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSitemapsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteSitemapsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

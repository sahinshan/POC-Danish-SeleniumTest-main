using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePagesPage : CommonMethods
    {
        public WebSitePagesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsitePagesFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Pages']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By nameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Name']");
        readonly By isSecureHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Is Secure']");
        readonly By parentPageHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Parent Page']");
        readonly By SitemapOrBreadCrumbTextHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Sitemap or Bread Crumb Text']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Modified On']");

        By websiteRecordCheckbox(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[1]/input");
        By nameCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[2]");
        By createdByCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[8]");
        By createdOnCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[9]");
        By modifiedByCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[10]");
        By modifiedOnCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[11]");


        By nameCell_SearchResultArea(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[2]");
        By createdByCell_SearchResultArea(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[3]");
        By createdOnCell_SearchResultArea(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[4]");
        By modifiedByCell_SearchResultArea(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[5]");
        By modifiedOnCell_SearchResultArea(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[6]");


        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSitePagesPage WaitForWebSitePagesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsitePagesFrame);
            SwitchToIframe(CWNavItem_WebsitePagesFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(SitemapOrBreadCrumbTextHeader);
            WaitForElement(isSecureHeader);
            WaitForElement(parentPageHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }



        public WebSitePagesPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }


        public WebSitePagesPage ValidateNameCellText_SearchResultArea(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell_SearchResultArea(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateCreatedByCellText_SearchResultArea(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell_SearchResultArea(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateCreatedOnCellText_SearchResultArea(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell_SearchResultArea(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateModifiedByCellText_SearchResultArea(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell_SearchResultArea(RecordID), ExpectedText);

            return this;
        }
        public WebSitePagesPage ValidateModifiedOnCellText_SearchResultArea(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell_SearchResultArea(RecordID), ExpectedText);

            return this;
        }



        public WebSitePagesPage ClickOnWebSitePageRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSitePagesPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSitePagesPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSitePagesPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSitePagesPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSitePagesPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSitePagesPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePagesPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePagesPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSitePagesPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

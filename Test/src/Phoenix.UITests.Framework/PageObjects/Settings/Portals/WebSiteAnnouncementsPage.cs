using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteAnnouncementsPage : CommonMethods
    {
        public WebSiteAnnouncementsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteAnnouncementFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Announcements']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By nameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Name']");
        readonly By expiresOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Expires On']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Modified On']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By nameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By expiresOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By modifiedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By modifiedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteAnnouncementsPage WaitForWebSiteAnnouncementsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteAnnouncementFrame);
            SwitchToIframe(CWNavItem_WebsiteAnnouncementFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(expiresOnHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }

        

        public WebSiteAnnouncementsPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateExpiresOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(expiresOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteAnnouncementsPage ClickOnWebSiteAnnouncementRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(nameCell(RecordID));

            return this;
        }
        public WebSiteAnnouncementsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteAnnouncementsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteAnnouncementsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteAnnouncementsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteAnnouncementsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteAnnouncementsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteAnnouncementsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteAnnouncementsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteAnnouncementsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

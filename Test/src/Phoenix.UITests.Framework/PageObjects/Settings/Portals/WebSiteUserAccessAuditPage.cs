using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Settings - Portals - Website - Open Website Record - System User Access Audit
    /// </summary>
    public class WebSiteUserAccessAuditPage : CommonMethods
    {
        public WebSiteUserAccessAuditPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteUserAccessAuditFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website User Access Audit']");


        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By WebsiteUser_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Website User']");
        readonly By BrowserType_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Browser Type']");
        readonly By BrowserVersion_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Browser Version']");
        readonly By LoginDateTime_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Login Date & Time']");
        readonly By LogoutDateTime_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Logout Date & Time']");
        readonly By TokenExpireOn_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Token Expire On']");
        readonly By WebsiteSecurityProfile_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Website Security Profile']");
        readonly By IsMobileDevice_Header = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Is Mobile Device?']");
        

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]");
        By WebsiteUser_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By BrowserType_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By BrowserVersion_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By LoginDateTime_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By LogoutDateTime_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By TokenExpireOn_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");
        By WebsiteSecurityProfile_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[8]");
        By IsMobileDevice_Cell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[9]");


        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteUserAccessAuditPage WaitForWebSiteUserAccessAuditPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteUserAccessAuditFrame);
            SwitchToIframe(CWNavItem_WebsiteUserAccessAuditFrame);

            WaitForElement(pageHeader);

            WaitForElement(WebsiteUser_Header);
            WaitForElement(BrowserType_Header);
            WaitForElement(BrowserVersion_Header);
            WaitForElement(LoginDateTime_Header);
            WaitForElement(LogoutDateTime_Header);
            WaitForElement(TokenExpireOn_Header);
            WaitForElement(WebsiteSecurityProfile_Header);
            WaitForElement(IsMobileDevice_Header);
            
            return this;
        }



        public WebSiteUserAccessAuditPage ValidateWebsiteUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteUser_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateBrowserTypeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(BrowserType_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateBrowserVersionCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(BrowserVersion_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateLoginDateTimeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(LoginDateTime_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateLogoutDateTimeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(LogoutDateTime_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateTokenExpireOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(TokenExpireOn_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateWebsiteSecurityProfileCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteSecurityProfile_Cell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateIsMobileDeviceCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(IsMobileDevice_Cell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteUserAccessAuditPage ClickOnRecord(string RecordID)
        {
            WaitForElementToBeClickable(WebsiteUser_Cell(RecordID));

            Click(WebsiteUser_Cell(RecordID));

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }




        public WebSiteUserAccessAuditPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteUserAccessAuditPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }



        public WebSiteUserAccessAuditPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserAccessAuditPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserAccessAuditPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteUserAccessAuditPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUsersPage : CommonMethods
    {
        public WebSiteUsersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteUserFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Users']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By userNameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Username']");
        readonly By profiledHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Profile']");
        readonly By statusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Status']");
        readonly By securityProfileHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Security Profile']");
        readonly By lastLoginDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Last Login Date']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Modified On']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By userNameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By profileCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By statusCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By securityProfileCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By lastLoginDateCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[8]");
        By modifiedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[9]");
        By modifiedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[10]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteUsersPage WaitForWebSiteUsersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteUserFrame);
            SwitchToIframe(CWNavItem_WebsiteUserFrame);

            WaitForElement(pageHeader);

            WaitForElement(userNameHeader);
            WaitForElement(profiledHeader);
            WaitForElement(statusHeader);
            WaitForElement(securityProfileHeader);
            WaitForElement(lastLoginDateHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }


        public WebSiteUsersPage SelectView(string ViewText)
        {
            SelectPicklistElementByText(viewSelector, ViewText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteUsersPage ValidateUserNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(userNameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateProfileCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(profileCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(statusCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateSecurityProfileCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(securityProfileCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateLastLoginDateCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(lastLoginDateCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUsersPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteUsersPage ClickOnWebSiteUserRecord(string RecordID)
        {
            WaitForElementToBeClickable(userNameCell(RecordID));

            Click(userNameCell(RecordID));

            return this;
        }
        public WebSiteUsersPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteUsersPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteUsersPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteUsersPage ClickSearchButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteUsersPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteUsersPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUsersPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUsersPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteUsersPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

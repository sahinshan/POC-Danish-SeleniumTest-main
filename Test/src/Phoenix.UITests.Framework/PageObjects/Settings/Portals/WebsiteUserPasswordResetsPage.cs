using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebsiteUserPasswordResetsPage : CommonMethods
    {
        public WebsiteUserPasswordResetsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuser&')]");
        readonly By CWNavItem_WebsiteUserPasswordResetFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website User Password Resets']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By WebsiteUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Website User']");
        readonly By ResetPasswordLinkHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Reset Password Link']");
        readonly By ExpireOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Expire On']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created On']");
        

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By WebsiteUserCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By ResetPasswordLinkCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By ExpireOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebsiteUserPasswordResetsPage WaitForWebsiteUserPasswordResetsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteUserPasswordResetFrame);
            SwitchToIframe(CWNavItem_WebsiteUserPasswordResetFrame);

            WaitForElement(pageHeader);

            WaitForElement(WebsiteUserHeader);
            WaitForElement(ExpireOnHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(ResetPasswordLinkHeader);

            return this;
        }

        

        public WebsiteUserPasswordResetsPage ValidateWebsiteUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteUserCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateExpireOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(ExpireOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateResetPasswordLinkCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(ResetPasswordLinkCell(RecordID), ExpectedText);

            return this;
        }


        public WebsiteUserPasswordResetsPage ClickOnWebSiteUserPasswordResetRecord(string RecordID)
        {
            WaitForElementToBeClickable(WebsiteUserCell(RecordID));

            Click(WebsiteUserCell(RecordID));

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebsiteUserPasswordResetsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebsiteUserPasswordResetsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebsiteUserPasswordResetsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebsiteUserPasswordResetsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsiteUserPasswordResetsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebsiteUserPasswordResetsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebsiteUserPasswordResetsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

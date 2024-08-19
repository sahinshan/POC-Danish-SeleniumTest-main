using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUserPinsPage : CommonMethods
    {
        public WebSiteUserPinsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuser&')]");
        readonly By CWNavItem_WebsiteUserPinFrame = By.Id("CWUrlPanel_IFrame");
        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website User Pins']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        

        #region Results Area

        readonly By pinHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*[text()='PIN']");
        readonly By ExpireOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*[text()='Expire On']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*[text()='Modified On']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By pinCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By ExpireOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By modifiedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By modifiedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteUserPinsPage WaitForWebSiteUserPinsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteUserPinFrame);
            SwitchToIframe(CWNavItem_WebsiteUserPinFrame);

            WaitForElement(pageHeader);

            WaitForElement(pinHeader);
            WaitForElement(ExpireOnHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }

        

        public WebSiteUserPinsPage ValidatePINCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(pinCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ValidateExpireOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(ExpireOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteUserPinsPage ClickOnWebSiteUserPinRecord(string RecordID)
        {
            WaitForElementToBeClickable(pinCell(RecordID));

            Click(pinCell(RecordID));

            return this;
        }
        public WebSiteUserPinsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteUserPinsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteUserPinsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteUserPinsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteUserPinsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteUserPinsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserPinsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserPinsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteUserPinsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

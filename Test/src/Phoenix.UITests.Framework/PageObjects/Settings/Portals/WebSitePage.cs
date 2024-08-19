using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePage : CommonMethods
    {
        public WebSitePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Websites']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");



        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By nameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Name']");
        readonly By applicationHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Application']");
        readonly By userRecordTypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='User Record Type']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/span[text()='Modified On']");

        By websiteRecordCheckbox(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[1]/input");
        By nameCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[2]");
        By applicationCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[3]");
        By userRecordTypeCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[4]");
        By createdByCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[5]");
        By createdOnCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[6]");
        By modifiedByCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[7]");
        By modifiedOnCell(string WebSiteRecordID) => By.XPath("//*[@id='" + WebSiteRecordID + "']/td[8]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSitePage WaitForWebSiteToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(applicationHeader);
            WaitForElement(userRecordTypeHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            WaitForElementVisible(pageHeader);

            return this;
        }

        public WebSitePage WaitForWebSiteWithSearchResultsToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(applicationHeader);
            WaitForElement(userRecordTypeHeader);

            return this;
        }



        public WebSitePage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateApplicationCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(applicationCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateUserRecordTypeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(userRecordTypeCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSitePage ClickOnWebSiteRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSitePage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSitePage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSitePage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSitePage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSitePage ClicAddNewRecordButton()
        {
            WaitForElementToBeClickable(addNewRecordButton);
            Click(addNewRecordButton);

            return this;
        }


        public WebSitePage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSitePage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

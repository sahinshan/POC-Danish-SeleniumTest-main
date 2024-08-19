using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteFeedbackPage : CommonMethods
    {
        public WebSiteFeedbackPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteFeedbackFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Feedback']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Name']");
        readonly By EmailHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Email']");
        readonly By WebsiteFeedbackTypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Feedback Type']");
        readonly By MessageHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Feedback']");
        readonly By CreatedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created On']");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By nameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By EmailCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By WebsiteFeedbackTypeCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By MessageCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[10]");
        By CreatedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[9]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteFeedbackPage WaitForWebSiteFeedbackPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteFeedbackFrame);
            SwitchToIframe(CWNavItem_WebsiteFeedbackFrame);

            WaitForElement(pageHeader);

            WaitForElement(NameHeader);
            WaitForElement(EmailHeader);
            WaitForElement(WebsiteFeedbackTypeHeader);
            WaitForElement(MessageHeader);
            WaitForElement(CreatedOnHeader);

            return this;
        }



        public WebSiteFeedbackPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteFeedbackPage ValidateEmailCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(EmailCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteFeedbackPage ValidateWebsiteFeedbackTypeCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteFeedbackTypeCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteFeedbackPage ValidateMessageCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(MessageCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteFeedbackPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(CreatedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteFeedbackPage ClickOnWebSiteFeedbackRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSiteFeedbackPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckbox(RecordID));

            return this;
        }
        public WebSiteFeedbackPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteFeedbackPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteFeedbackPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteFeedbackPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteFeedbackPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteFeedbackPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteFeedbackPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteFeedbackPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

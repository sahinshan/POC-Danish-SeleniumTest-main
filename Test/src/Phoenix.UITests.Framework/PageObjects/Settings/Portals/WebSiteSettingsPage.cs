using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteSettingsPage : CommonMethods
    {
        public WebSiteSettingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteSettingFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Settings']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By nameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text()='Name']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text()='Modified On']");

        By websiteRecordCheckbox(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[1]/input");
        By nameCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[2]");
        By createdByCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[6]");
        By createdOnCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[7]");
        By modifiedByCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[8]");
        By modifiedOnCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[9]");




        By nameSearchCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[2]");
        By createdBySearchCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[3]");
        By createdOnSearchCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[4]");
        By modifiedBySearchCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[5]");
        By modifiedOnSearchCell(string WebSiteSettingID) => By.XPath("//*[@id='" + WebSiteSettingID + "']/td[6]");




        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteSettingsPage WaitForWebSiteSettingsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteSettingFrame);
            SwitchToIframe(CWNavItem_WebsiteSettingFrame);

            WaitForElement(pageHeader);

            WaitForElement(nameHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }



        public WebSiteSettingsPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }




        public WebSiteSettingsPage ValidateNameSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameSearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateCreatedBySearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdBySearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateCreatedOnSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnSearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateModifiedBySearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedBySearchCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ValidateModifiedOnSearchCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnSearchCell(RecordID), ExpectedText);

            return this;
        }



        public WebSiteSettingsPage ClickOnWebSiteSettingRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSiteSettingsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteSettingsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteSettingsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteSettingsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteSettingsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteSettingsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSettingsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSettingsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteSettingsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

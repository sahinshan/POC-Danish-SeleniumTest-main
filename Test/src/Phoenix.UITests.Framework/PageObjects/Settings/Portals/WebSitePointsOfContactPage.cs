using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePointsOfContactPage : CommonMethods
    {
        public WebSitePointsOfContactPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsitePointsOfContactFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Points of Contact']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By NameWebsiteHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*[text()='Name [Website]']");
        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*[text()='Name']");
        readonly By AddressHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*[text()='Address']");
        readonly By PhoneHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*[text()='Phone']");
        readonly By EmailHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*[text()='Email']");
        readonly By statusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*[text()='Status']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/*[text()='Responsible Team']");
        readonly By createdByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[9]/a/*[text()='Created By']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[10]/a/*[text()='Created On']");
        readonly By modifiedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]/a/*[text()='Modified By']");
        readonly By modifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[12]/a/*[text()='Modified On']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By nameWebsiteCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By nameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By AddressCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By PhoneCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By EmailCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By statusCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");
        By responsibleTeamCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[8]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[9]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[10]");
        By modifiedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[11]");
        By modifiedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[12]");



        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSitePointsOfContactPage WaitForWebSitePointsOfContactPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsitePointsOfContactFrame);
            SwitchToIframe(CWNavItem_WebsitePointsOfContactFrame);

            WaitForElement(pageHeader);

            WaitForElement(NameWebsiteHeader);
            WaitForElement(NameHeader);
            WaitForElement(AddressHeader);
            WaitForElement(PhoneHeader);
            WaitForElement(EmailHeader);
            WaitForElement(statusHeader);
            WaitForElement(ResponsibleTeamHeader);
            WaitForElement(createdByHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(modifiedbyHeader);
            WaitForElement(modifiedOnHeader);

            return this;
        }



        public WebSitePointsOfContactPage ValidateNameWebsiteCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameWebsiteCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateAddressCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AddressCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidatePhoneCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(PhoneCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateEmailCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(EmailCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(statusCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateResponsibleTeamCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(responsibleTeamCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateCreatedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateModifiedByCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(RecordID), ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ValidateModifiedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(RecordID), ExpectedText);

            return this;
        }



        public WebSitePointsOfContactPage ClickOnWebSitePointsOfContactRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSitePointsOfContactPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSitePointsOfContactPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSitePointsOfContactPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSitePointsOfContactPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSitePointsOfContactPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSitePointsOfContactPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePointsOfContactPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePointsOfContactPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSitePointsOfContactPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

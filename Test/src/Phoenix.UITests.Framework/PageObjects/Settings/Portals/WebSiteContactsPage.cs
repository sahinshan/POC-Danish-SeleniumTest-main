using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteContactsPage : CommonMethods
    {
        public WebSiteContactsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By websiteRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");
        readonly By CWNavItem_WebsiteContactFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Contacts']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");


        #region Results Area

        readonly By WebsiteHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Website']");
        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Subject']");
        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Name']");
        readonly By EmailHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Email']");
        readonly By PointOfContactHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Point of Contact']");
        readonly By createdOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Created On']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/a/*[text()='Responsible Team']");

        By websiteRecordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[1]/input");
        By WebsiteCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By SubjectCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By nameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By EmailCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By PointOfContactCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");
        By ResponsibleTeamCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[8]");


        By nameCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By pointOfContactCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By createdByCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By createdOnCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By modifiedByCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");
        By modifiedOnCell_SearchResultsPage(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[7]");


        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");


        public WebSiteContactsPage WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(websiteRecordIFrame);
            SwitchToIframe(websiteRecordIFrame);

            WaitForElement(CWNavItem_WebsiteContactFrame);
            SwitchToIframe(CWNavItem_WebsiteContactFrame);

            WaitForElement(pageHeader);

            WaitForElement(NameHeader);
            WaitForElement(PointOfContactHeader);
            WaitForElement(SubjectHeader);
            WaitForElement(WebsiteHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(EmailHeader);
            WaitForElement(ResponsibleTeamHeader);

            return this;
        }

        public WebSiteContactsPage WaitForWebsiteContactsPageToLoadFromSettingsLink()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(pageHeader);

            WaitForElement(NameHeader);
            WaitForElement(PointOfContactHeader);
            WaitForElement(SubjectHeader);
            WaitForElement(WebsiteHeader);
            WaitForElement(createdOnHeader);
            WaitForElement(EmailHeader);
            WaitForElement(ResponsibleTeamHeader);

            return this;
        }



        public WebSiteContactsPage ValidateNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidatePointOfContactCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(PointOfContactCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateSubjectCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateWebsiteCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(WebsiteCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateCreatedOnCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateEmailCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(EmailCell(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateResponsibleTeamCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordID), ExpectedText);

            return this;
        }




        public WebSiteContactsPage ValidateNameCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(nameCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidatePointOfContactCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(pointOfContactCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateCreatedByCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdByCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateCreatedOnCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateModifiedByCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }
        public WebSiteContactsPage ValidateModifiedOnCellText_SearchResultsPage(string RecordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell_SearchResultsPage(RecordID), ExpectedText);

            return this;
        }



        public WebSiteContactsPage ClickOnWebSiteContactRecord(string RecordID)
        {
            WaitForElementToBeClickable(nameCell(RecordID));

            Click(nameCell(RecordID));

            return this;
        }
        public WebSiteContactsPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(websiteRecordCheckbox(RecordID));

            return this;
        }
        public WebSiteContactsPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(websiteRecordCheckbox(RecordID), 3);

            return this;
        }


        public WebSiteContactsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(quickSearchTextBox, SearchQuery);

            return this;
        }
        public WebSiteContactsPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public WebSiteContactsPage ClicAddNewRecordButton()
        {
            Click(addNewRecordButton);

            return this;
        }


        public WebSiteContactsPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteContactsPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteContactsPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        public WebSiteContactsPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RecruitmentRequirementsPage : CommonMethods
    {
        public RecruitmentRequirementsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame"); 

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecord_Button = By.XPath("//div/button[@title='Create new record']");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        
        readonly By viewSelector = By.Id("CWViewSelector");

        readonly By recordId_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Record Id')]");
        readonly By RequirementName_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Requirement Name')]");
        readonly By RequiredItemType_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Required Item Type')]");
        readonly By AllRoles_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'All Roles?')]");
        readonly By NoRequiredForInduction_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'No. Required for Induction')]");
        readonly By StatusForInduction_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Status for Induction')]");
        readonly By NoRequiredForAcceptance_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'No. Required for Acceptance')]");
        readonly By StatusForAcceptance_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Status for Acceptance')]");
        readonly By StartDate_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'Start Date')]");
        readonly By EndDate_HeaderLink = By.XPath("//*[@id='CWGridHeaderRow']//a[contains(@title, 'End Date')]");
        
        By GridHeaderCell(string HeaderCellText) => By.XPath("//*[@id='CWGridHeaderRow']/th/*[contains(@title, '"+ HeaderCellText + "')]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");



        public RecruitmentRequirementsPage WaitForRecruitmentRequirementsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);
            
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecord_Button);
            ValidateElementText(pageHeader, "Recruitment Requirements");

            ScrollToElement(recordId_HeaderLink);
            WaitForElementVisible(recordId_HeaderLink);
            WaitForElementVisible(RequirementName_HeaderLink);
            WaitForElementVisible(RequiredItemType_HeaderLink);
            WaitForElementVisible(AllRoles_HeaderLink);
            WaitForElementVisible(NoRequiredForInduction_HeaderLink);
            WaitForElementVisible(StatusForInduction_HeaderLink);
            WaitForElementVisible(NoRequiredForAcceptance_HeaderLink);

            WaitForElement(StatusForAcceptance_HeaderLink);
            ScrollToElement(StatusForAcceptance_HeaderLink);
            WaitForElementVisible(StatusForAcceptance_HeaderLink);

            WaitForElement(StartDate_HeaderLink);
            ScrollToElement(StartDate_HeaderLink);
            WaitForElementVisible(StartDate_HeaderLink);

            WaitForElement(EndDate_HeaderLink);
            ScrollToElement(EndDate_HeaderLink);
            WaitForElementVisible(EndDate_HeaderLink);

            return this;
        }

        public RecruitmentRequirementsPage WaitForPageToLoadAfterSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecord_Button);
            ValidateElementText(pageHeader, "Recruitment Requirements");

            ScrollToElement(recordId_HeaderLink);
            WaitForElementVisible(recordId_HeaderLink);
            WaitForElementVisible(RequirementName_HeaderLink);
            WaitForElementVisible(RequiredItemType_HeaderLink);
            WaitForElementVisible(AllRoles_HeaderLink);
            WaitForElementVisible(NoRequiredForInduction_HeaderLink);
            WaitForElementVisible(StatusForInduction_HeaderLink);

            return this;
        }

        public RecruitmentRequirementsPage ClickRecordIdHeaderLink()
        {
            WaitForElement(recordId_HeaderLink);
            ScrollToElement(recordId_HeaderLink);
            Click(recordId_HeaderLink);

            return this;
        }

        public RecruitmentRequirementsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecord_Button);
            Click(newRecord_Button);

            return this;
        }

        public RecruitmentRequirementsPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public RecruitmentRequirementsPage ClickQuickSearchButton()
        {            
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);         

            return this;
        }

        public RecruitmentRequirementsPage ClickRefreshButton()
        {            
            WaitForElement(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RecruitmentRequirementsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));
            return this;
        }

        public RecruitmentRequirementsPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }

        public RecruitmentRequirementsPage ValidateRecordIsNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 7);

            return this;
        }

        public RecruitmentRequirementsPage SelectView(string ViewToSelect)
        {
            SelectPicklistElementByText(viewSelector, ViewToSelect);

            return this;
        }

        public RecruitmentRequirementsPage ValidateHeaderCellVisible(string HeaderCellText)
        {
            WaitForElementNotVisible(GridHeaderCell(HeaderCellText), 15);

            return this;
        }
    }
}

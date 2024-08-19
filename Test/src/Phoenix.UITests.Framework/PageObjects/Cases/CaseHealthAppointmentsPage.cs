using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseHealthAppointmentsPage : CommonMethods
    {
        public CaseHealthAppointmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]"); 
        readonly By CWNavItem_HealthAppointmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Health Appointments']");

        readonly By TimelineTab = By.Id("CWNavGroup_Timeline");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//*[@id='CWRefreshButton']");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By noRecordMessageHead = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordMessageBody = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By HealthLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Health']/a");
        readonly By RTTWaitTimeLeftSubMenuItem = By.Id("CWNavItem_RTTWaitTime");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");

        readonly By selectViewResult_DropDown = By.XPath("//select[@id='CWViewSelector']");


        public CaseHealthAppointmentsPage WaitForCaseHealthAppointmentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseIFrame);
            SwitchToIframe(caseIFrame);

            WaitForElement(CWNavItem_HealthAppointmentFrame);
            SwitchToIframe(CWNavItem_HealthAppointmentFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            return this;
        }

        public CaseHealthAppointmentsPage WaitForCaseHealthAppointmentsMenuSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseIFrame);
            SwitchToIframe(caseIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(TimelineTab);
            WaitForElementVisible(DetailsTab);

            return this;
        }

        public CaseHealthAppointmentsPage SearchCaseHealthAppointmentRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public CaseHealthAppointmentsPage SearchCaseHealthAppointmentRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CaseHealthAppointmentsPage OpenCaseHealthAppointmentRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public CaseHealthAppointmentsPage SelectCaseHealthAppointmentRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));            
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CaseHealthAppointmentsPage SelectView(string TextToSelect)
        {
            WaitForElement(viewSelector);
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(noRecordMessageHead);
                WaitForElementVisible(noRecordMessageBody);

            }
            else
            {
                WaitForElementNotVisible(noRecordMessageHead, 5);
                WaitForElementNotVisible(noRecordMessageBody, 5);
            }
            return this;
        }

        public CaseHealthAppointmentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CaseHealthAppointmentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public CaseHealthAppointmentsPage NavigateToRTTWaitTimePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(RTTWaitTimeLeftSubMenuItem);
            Click(RTTWaitTimeLeftSubMenuItem);

            return this;
        }

        public CaseHealthAppointmentsPage SelectViewResultDropDown(string ElementTextToBeSelect)
        {
            WaitForElement(selectViewResult_DropDown);
            SelectPicklistElementByText(selectViewResult_DropDown, ElementTextToBeSelect);

            return this;
        }
    }
}

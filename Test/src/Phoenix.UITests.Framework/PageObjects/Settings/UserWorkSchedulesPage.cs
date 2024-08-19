using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings
{
    public class UserWorkSchedulesPage : CommonMethods
    {
        public UserWorkSchedulesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By SystemUserIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By pageHeader = By.XPath("//div[@id='CWWrapper']/div/descendant::h1");
        readonly By userWorkSchedule_IFrame = By.Id("CWUrlPanel_IFrame");

        
        readonly By nnewRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By Title_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/*/*");
        readonly By EmploymentContract_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/*/*");
        readonly By AvailabilityType_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/*/*");
        readonly By AdHoc_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/*/*");        
        readonly By ReccurrencePattern_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]/*/*");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");


        public UserWorkSchedulesPage WaitForUserWorkSchedulesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);
            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);
            WaitForElement(userWorkSchedule_IFrame);
            SwitchToIframe(userWorkSchedule_IFrame);
            WaitForElement(pageHeader);
            WaitForElement(viewSelector);
            WaitForElementVisible(quickSearchTextbox);
            WaitForElementVisible(quickSearchButton);
            WaitForElementVisible(refreshButton);

            return this;
        }

        public UserWorkSchedulesPage WaitForResultsGridToLoad()
        {
            WaitForElement(Title_FieldHeader);
            WaitForElement(EmploymentContract_FieldHeader);
            WaitForElement(AvailabilityType_FieldHeader);
            WaitForElement(AdHoc_FieldHeader);
            WaitForElement(ReccurrencePattern_FieldHeader);            

            return this;
        }

        public UserWorkSchedulesPage ValidateUserWorkScheduleRecordPresent(string recordID)
        {
            WaitForElementToBeClickable(recordRow(recordID));
            MoveToElementInPage(recordRow(recordID));
            bool isRecordPresent = GetElementVisibility(recordRow(recordID));
            Assert.IsTrue(isRecordPresent);
            return this;
        }

        public UserWorkSchedulesPage SearchUserWorkSchedulesRecord(string searchString)
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(quickSearchTextbox);
            Click(quickSearchTextbox);
            MoveToElementInPage(quickSearchTextbox);
            SendKeys(quickSearchTextbox, searchString);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public UserWorkSchedulesPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            WaitForElementNotVisible("CWRefreshPanel", 20);
            return this;
        }

        public UserWorkSchedulesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(nnewRecordButton);
            Click(nnewRecordButton);

            return this;
        }

    }
}

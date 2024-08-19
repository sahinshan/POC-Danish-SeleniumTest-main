using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ConsultantEpisodesPage : CommonMethods
    {
        public ConsultantEpisodesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWNavItem_InpatientConsultantEpisodesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Consultant Episodes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");

        readonly By Timeline_Tab = By.Id("CWNavGroup_Timeline");
        readonly By Deatils_Tab = By.Id("CWNavGroup_EditForm");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By HealthLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Health']/a");
        readonly By RTTWaitTimeLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_RTTWaitTime']");

        public ConsultantEpisodesPage WaitForConsultantEpisodesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseFrame);
            SwitchToIframe(caseFrame);

            WaitForElement(CWNavItem_InpatientConsultantEpisodesFrame);
            SwitchToIframe(CWNavItem_InpatientConsultantEpisodesFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public ConsultantEpisodesPage WaitForConsultantEpisodesMenuSectionsToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseFrame);
            SwitchToIframe(caseFrame);

            WaitForElement(MenuButton);

            return this;
        }

        public ConsultantEpisodesPage SearchLeaveAWOLRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public ConsultantEpisodesPage SearchLeaveAWOLRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ConsultantEpisodesPage OpenConsultantEpisodeRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public ConsultantEpisodesPage SelectLeaveAWOLRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ConsultantEpisodesPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }


        public ConsultantEpisodesPage AddNewRecordButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NewRecordButton);
                ValidateElementEnabled(NewRecordButton);
            }
            else
            {
                WaitForElementNotVisible(NewRecordButton, 3);
            }

            return this;
        }

        public ConsultantEpisodesPage ValidateConsultantEpisodeRecord(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public ConsultantEpisodesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            MoveToElementInPage(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public ConsultantEpisodesPage NavigateToRTTWaitTimePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(RTTWaitTimeLeftSubMenuItem);
            Click(RTTWaitTimeLeftSubMenuItem);

            return this;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMHALegalStatusRecordPage : CommonMethods
    {
        public PersonMHALegalStatusRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personmhalegalstatusIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personmhalegalstatus&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");

        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");

        readonly By CWNavItem_CaseNotes = By.XPath("//*[@id='CWNavItem_CaseNotes']");
        readonly By CWNavItem_MHARightsAndRequests = By.XPath("//*[@id='CWNavItem_MHARightsAndRequests']");
        readonly By CWNavItem_MHARecordOfAppeal = By.XPath("//*[@id='CWNavItem_MHARecordOfAppeal']");
        readonly By CWNavItem_MHACourtDateOutcome = By.XPath("//*[@id='CWNavItem_MHACourtDateOutcome']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        #endregion


        public PersonMHALegalStatusRecordPage WaitForPersonMHALegalStatusRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personmhalegalstatusIFrame);
            SwitchToIframe(personmhalegalstatusIFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            return this;
        }

        public PersonMHALegalStatusRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public PersonMHALegalStatusRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public PersonMHALegalStatusRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonMHALegalStatusRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }



        public PersonMHALegalStatusRecordPage NavigateToPersonMHALegalStatusCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_CaseNotes);
            Click(CWNavItem_CaseNotes);

            return this;
        }

        public PersonMHALegalStatusRecordPage NavigateToRightsAndRequestsForAnIMHAAndMHAAppealArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_MHARightsAndRequests);
            Click(CWNavItem_MHARightsAndRequests);

            return this;
        }

        public PersonMHALegalStatusRecordPage NavigateToRecordsOfAppealArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_MHARecordOfAppeal);
            Click(CWNavItem_MHARecordOfAppeal);

            return this;
        }

        public PersonMHALegalStatusRecordPage NavigateToCourtDatesAndOutcomes()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_MHACourtDateOutcome);
            Click(CWNavItem_MHACourtDateOutcome);

            return this;
        }
    }
}

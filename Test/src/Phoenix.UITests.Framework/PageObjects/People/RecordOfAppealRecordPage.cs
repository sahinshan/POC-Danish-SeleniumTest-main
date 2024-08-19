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
    public class RecordOfAppealRecordPage : CommonMethods
    {
        public RecordOfAppealRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By mharecordofappealIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mharecordofappeal&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CWNavItem_MHARecordOfAppealCaseNote = By.XPath("//*[@id='CWNavItem_MHARecordOfAppealCaseNote']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        #endregion


        public RecordOfAppealRecordPage WaitForRecordOfAppealRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(mharecordofappealIFrame);
            SwitchToIframe(mharecordofappealIFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            return this;
        }

        public RecordOfAppealRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public RecordOfAppealRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public RecordOfAppealRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public RecordOfAppealRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }



        public RecordOfAppealRecordPage NavigateToRecordOfAppealCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_MHARecordOfAppealCaseNote);
            Click(CWNavItem_MHARecordOfAppealCaseNote);

            return this;
        }
    }
}

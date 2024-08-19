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
    public class PersonContactRecordPage : CommonMethods
    {
        public PersonContactRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By contactIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=contact&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CWNavItem_CaseNotes = By.XPath("//*[@id='CWNavItem_CaseNotes']");




        #region Field Labels

        readonly By regardingFieldLabel = By.XPath("//li[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");

        #endregion

        #region Fields

        readonly By regardingField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_regardingid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_regardingid']");

        #endregion


        public PersonContactRecordPage WaitForPersonContactRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(contactIFrame);
            SwitchToIframe(contactIFrame);

            WaitForElement(pageHeader);

            WaitForElement(regardingFieldLabel);

            return this;
        }

        public PersonContactRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public PersonContactRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public PersonContactRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonContactRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }



        public PersonContactRecordPage NavigateToContactCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_CaseNotes);
            Click(CWNavItem_CaseNotes);

            return this;
        }
    }
}

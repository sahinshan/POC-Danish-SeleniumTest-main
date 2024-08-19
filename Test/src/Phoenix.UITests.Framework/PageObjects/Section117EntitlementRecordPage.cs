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
    public class Section117EntitlementRecordPage : CommonMethods
    {
        public Section117EntitlementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By mhaaftercareentitlementIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mhaaftercareentitlement&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CWNavItem_MHAAftercareEntitlementCaseNote = By.XPath("//*[@id='CWNavItem_MHAAftercareEntitlementCaseNote']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        #endregion


        public Section117EntitlementRecordPage WaitForSection117EntitlementRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(mhaaftercareentitlementIFrame);
            SwitchToIframe(mhaaftercareentitlementIFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            return this;
        }

        public Section117EntitlementRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public Section117EntitlementRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public Section117EntitlementRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public Section117EntitlementRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }



        public Section117EntitlementRecordPage NavigateToSection117EntitlementCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_MHAAftercareEntitlementCaseNote);
            Click(CWNavItem_MHAAftercareEntitlementCaseNote);

            return this;
        }
    }
}

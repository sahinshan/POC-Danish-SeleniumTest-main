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
    public class AttendedEducationEstablishmentRecordPage : CommonMethods
    {
        public AttendedEducationEstablishmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personattendededucationestablishment&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");

        readonly By CWNavItem_PersonEducationYear = By.XPath("//*[@id='CWNavItem_PersonEducationYear']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        #endregion


        public AttendedEducationEstablishmentRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);
            WaitForElement(personField);
            WaitForElement(personLookupButton);

            return this;
        }

        public AttendedEducationEstablishmentRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public AttendedEducationEstablishmentRecordPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public AttendedEducationEstablishmentRecordPage TapSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }




        public AttendedEducationEstablishmentRecordPage NavigateToPersonEducationYear()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_PersonEducationYear);
            Click(CWNavItem_PersonEducationYear);

            return this;
        }
    }
}

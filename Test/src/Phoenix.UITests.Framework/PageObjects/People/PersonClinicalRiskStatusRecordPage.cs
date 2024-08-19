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
    public class PersonClinicalRiskStatusRecordPage : CommonMethods
    {
        public PersonClinicalRiskStatusRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personclinicalriskstatusIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personclinicalriskstatus&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By PersonClinicalRiskStatusCaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PersonClinicalRiskStatusCaseNote']");
        readonly By EmailsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By LettersLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By PhoneCallsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PhoneCall']");
        readonly By TasksLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Task']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        #endregion


        public PersonClinicalRiskStatusRecordPage WaitForPersonClinicalRiskStatusRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personclinicalriskstatusIFrame);
            SwitchToIframe(personclinicalriskstatusIFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            return this;
        }

        public PersonClinicalRiskStatusRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            
            WaitForElement(backButton);

            return this;
        }

        public PersonClinicalRiskStatusRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public PersonClinicalRiskStatusRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonClinicalRiskStatusRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }



        public PersonClinicalRiskStatusRecordPage NavigateToPersonClinicalRiskStatusCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(PersonClinicalRiskStatusCaseNotesLink_LeftMenu);
            Click(PersonClinicalRiskStatusCaseNotesLink_LeftMenu);

            return this;
        }
        public PersonClinicalRiskStatusRecordPage NavigateToPersonClinicalRiskStatusEmailsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(EmailsLink_LeftMenu);
            Click(EmailsLink_LeftMenu);

            return this;
        }
        public PersonClinicalRiskStatusRecordPage NavigateToPersonClinicalRiskStatusLettersArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(LettersLink_LeftMenu);
            Click(LettersLink_LeftMenu);

            return this;
        }
        public PersonClinicalRiskStatusRecordPage NavigateToPersonClinicalRiskStatusPhoneCallsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(PhoneCallsLink_LeftMenu);
            Click(PhoneCallsLink_LeftMenu);

            return this;
        }
        public PersonClinicalRiskStatusRecordPage NavigateToPersonClinicalRiskStatusTasksArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(TasksLink_LeftMenu);
            Click(TasksLink_LeftMenu);

            return this;
        }
    }
}

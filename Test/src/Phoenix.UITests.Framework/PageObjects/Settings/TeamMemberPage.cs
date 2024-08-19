
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class TeamMemberPage : CommonMethods
    {
        public TeamMemberPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=teammember&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By activateButton = By.Id("TI_ActivateButton");



        #region Fields Labels

        readonly By teamLabel = By.XPath("//*[@id='CWLabelHolder_teamid']/label[text()='Team']");
        readonly By responsibleTeamLabel = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label[text()='Member']");

        readonly By startDateLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By endDateLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");

        #endregion

        #region Fields

        readonly By teamLookupButton = By.XPath("//*[@id='CWLookupBtn_teamid']");
        readonly By memberLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");

        readonly By startDateField = By.XPath("//*[@id='CWField_startdate']");
        readonly By endDateField = By.XPath("//*[@id='CWField_enddate']");

        #endregion


        public TeamMemberPage WaitForTeamMemberPagePageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(pagehehader);

            WaitForElement(teamLabel);
            WaitForElement(responsibleTeamLabel);

            WaitForElement(startDateLabel);
            WaitForElement(endDateLabel);

            WaitForElement(teamLookupButton);
            WaitForElement(memberLookupButton);

            WaitForElement(startDateField);
            WaitForElement(endDateField);

            return this;
        }

        
        
        
        public TeamMemberPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public TeamMemberPage TapSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public TeamMemberPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }




        public TeamMemberPage ClickTeamLookupButton()
        {
            Click(teamLookupButton);

            return this;
        }

        public TeamMemberPage ClickMemberButton()
        {
            Click(memberLookupButton);

            return this;
        }

        public TeamMemberPage InsertStartDateValue(string StartDate)
        {
            SendKeys(startDateField, StartDate);

            return this;
        }

        public TeamMemberPage InsertEndDateValue(string EndDate)
        {
            SendKeys(endDateField, EndDate);

            return this;
        }




        public TeamMemberPage ValidateStartDateValue(string ExpectedValue)
        {
            ValidateElementValue(startDateField, ExpectedValue);

            return this;
        }

        public TeamMemberPage ValidateEndDateValue(string ExpectedValue)
        {
            ValidateElementValue(endDateField, ExpectedValue);

            return this;
        }


        public TeamMemberPage ValidateActivateButtonNotVisible()
        {
            WaitForElementNotVisible(activateButton, 7);

            return this;
        }

    }
}

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
    /// <summary>
    /// This class represents the Edit Assessment page for the document type "Automation - Person Form 1"
    /// 
    /// </summary>
    public class Testing_CDV6_17721EditAssessmentPage : CommonMethods
    {
        public Testing_CDV6_17721EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        #region Locators

        By notificationArea(string expectedMessage) => By.XPath("//*[@id='CWNotificationMessage_Assessment'][text()='" + expectedMessage + "']");

        

        #region Iframes

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        By iframe_CWDialog_(string AssessmentID) => By.Id("iframe_CWDialog_" + AssessmentID + "");
        readonly By iframe_CWAssessmentDialog = By.Id("iframe_CWAssessmentDialog");

        #endregion

        #region Top Menu


        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_CWAssessmentSaveAndCloseButton");
        readonly By printButton = By.Id("TI_CWAssessmentPrintButton");
        readonly By printHistoryButton = By.Id("TI_CWAssessmentPrintRecordButton");

        readonly By additionalToolbarItemsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        readonly By spellCheckButton = By.Id("TI_CWAssessmentSpellCheckButton");
        readonly By mandatoryQuestionsButton = By.Id("TI_CWDisplayMandatoryButton");
        readonly By expandAllButton = By.Id("TI_CWAssessmentExpandAll");
        readonly By collapseAllButton = By.Id("TI_CWAssessmentCollapseAll");
        readonly By changeLanguageButton = By.Id("TI_CWChangeLanguage");

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Testing CDV6-17721']");



        #endregion


        #region Section 1

        #region Questions

        
        By CDV6_17721_MR_RadioButtonOption(string MultiOptionAnswer) => By.XPath("//input[@value='" + MultiOptionAnswer + "']");

        #endregion

        

        #endregion


        #endregion


        #region Methods

        public Testing_CDV6_17721EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
        {

            #region Iframes

            driver.SwitchTo().DefaultContent();

            Wait.Until(d => d.FindElement(CWContentIFrame));
            driver.SwitchTo().Frame(driver.FindElement(CWContentIFrame));

            Wait.Until(d => d.FindElement(iframe_CWDialog_(AssessmentID)));
            driver.SwitchTo().Frame(driver.FindElement(iframe_CWDialog_(AssessmentID)));

            Wait.Until(d => d.FindElement(iframe_CWAssessmentDialog));
            driver.SwitchTo().Frame(driver.FindElement(iframe_CWAssessmentDialog));

            #endregion

            #region Top Menu

            Wait.Until(c => c.FindElement(backButton));
            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(saveAndCloseButton));
            Wait.Until(c => c.FindElement(printButton));
            Wait.Until(c => c.FindElement(printHistoryButton));
            Wait.Until(c => c.FindElement(additionalToolbarItemsButton));

            Wait.Until(c => c.FindElement(documentTitle));

            #endregion


            


            return this;
        }

        public Testing_CDV6_17721EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public Testing_CDV6_17721EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public Testing_CDV6_17721EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public Testing_CDV6_17721EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
        {
            Click(saveButton);

            if(WaitForRefreshPanelToClose)
                WaitForElementNotVisible("CWRefreshPanel", 7);

            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public CaseFormPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public Testing_CDV6_17721EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public Testing_CDV6_17721EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
        {

            Wait.Until(c => c.FindElement(spellCheckButton).Displayed);
            Wait.Until(c => c.FindElement(mandatoryQuestionsButton).Displayed);
            Wait.Until(c => c.FindElement(expandAllButton).Displayed);
            Wait.Until(c => c.FindElement(collapseAllButton).Displayed);
            Wait.Until(c => c.FindElement(changeLanguageButton).Displayed);


            return this;
        }

        public PrintAssessmentPopup TapPrintButton()
        {
            WaitForElementToBeClickable(printButton);
            Click(printButton);

            return new PrintAssessmentPopup(driver, Wait, appURL);
        }

        #endregion

        

        #region Section 1

        #region Questions

        

        public Testing_CDV6_17721EditAssessmentPage Click_CDV6_17721_MR_Checkbox(string MultiOptionAnswerId)
        {
            WaitForElementToBeClickable(CDV6_17721_MR_RadioButtonOption(MultiOptionAnswerId));

            Click(CDV6_17721_MR_RadioButtonOption(MultiOptionAnswerId));

            return this;
        }


        

        #endregion


        #endregion


        #endregion


    }
}

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
    public class AutomationProviderForm1EditAssessmentPage : CommonMethods
    {
        public AutomationProviderForm1EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Automation - Provider Form 1']");



        #endregion


        #region Section 1

        readonly By section1TitleLink = By.XPath("//a[@id='CWSH_QA-DS-330']");
        readonly By section1Title = By.XPath("//a[@id='CWSH_QA-DS-330']/h2[text()='Section 1']");
        readonly By section1Expanded = By.XPath("//a[@id='CWSH_QA-DS-330'][@class='section-heading level1 open']/h2[text()='Section 1']");
        readonly By section1MenuButton = By.XPath("//div[@id='CWS_QA-DS-330']/fieldset/div/span/a[@title='Click to open']");



        #region Question Labels

        readonly By WFShortAnswerQuestionLabel = By.XPath("//ul[@id='QA-DSQ-985']/li/label[text()='WF Short Answer']");

        #endregion

        #region Questions

        readonly By WFShortAnswerQuestion = By.XPath("//input[@id='QA-DQ-1307']");


        #endregion

        #region Menu

        readonly By section1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-330']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1PrintButton = By.XPath("//ul[@id='SM_QA-DS-330']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1PrintHistoryButton = By.XPath("//ul[@id='SM_QA-DS-330']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-330']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-330']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion


        #endregion


        #region Methods

        public AutomationProviderForm1EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
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


            #region Section 1

            Wait.Until(c => c.FindElement(section1Title));
            Wait.Until(c => c.FindElement(section1Expanded));
            Wait.Until(c => c.FindElement(section1MenuButton));

            Wait.Until(c => c.FindElement(WFShortAnswerQuestionLabel));

            #endregion


            return this;
        }

        public AutomationProviderForm1EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public AutomationProviderForm1EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public AutomationProviderForm1EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public AutomationProviderForm1EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
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

        public AutomationProviderForm1EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public AutomationProviderForm1EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
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

        public AutomationProviderForm1EditAssessmentPage InsertWFShortAnswerValue(string ValueToInsert)
        {
            SendKeys(WFShortAnswerQuestion, ValueToInsert);
            return this;
        }

        public AutomationProviderForm1EditAssessmentPage ValidateWFShortAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFShortAnswerQuestion, ExpectedValue);

            return this;
        }

        

        #endregion


        #endregion


        #endregion


    }
}

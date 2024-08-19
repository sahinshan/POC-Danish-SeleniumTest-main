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
    public class AutomationMASHEpisodeForm1EditAssessmentPage : CommonMethods
    {
        public AutomationMASHEpisodeForm1EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Automation - MASH Episode Form 1']");



        #endregion


        #region Section 1

        readonly By section1TitleLink = By.XPath("//a[@id='CWSH_QA-DS-331']");
        readonly By section1Title = By.XPath("//a[@id='CWSH_QA-DS-331']/h2[text()='Section 1']");
        readonly By section1Expanded = By.XPath("//a[@id='CWSH_QA-DS-331'][@class='section-heading level1 open']/h2[text()='Section 1']");
        readonly By section1MenuButton = By.XPath("//div[@id='CWS_QA-DS-331']/fieldset/div/span/a[@title='Click to open']");



        #region Question Labels

        readonly By WFShortAnswerQuestionLabel = By.XPath("//ul[@id='QA-DSQ-986']/li/label[text()='WF Short Answer']");
        readonly By MashEpisodeDateTimeCaseRecordedLabel = By.XPath("//ul[@id='QA-DSQ-992']/li/label[text()='Mash Episode DateTime Case Recorded']");

        #endregion

        #region Questions

        readonly By WFShortAnswerQuestion = By.XPath("//input[@id='QA-DQ-1308']");
        readonly By MashEpisodeDateTimeCaseRecorded_DateQuestion = By.XPath("//input[@id='QA-DQ-1314']");
        readonly By MashEpisodeDateTimeCaseRecorded_TimeQuestion = By.XPath("//input[@id='QA-DQ-1314_TIME']");


        #endregion

        #region Menu

        readonly By section1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-331']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1PrintButton = By.XPath("//ul[@id='SM_QA-DS-331']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1PrintHistoryButton = By.XPath("//ul[@id='SM_QA-DS-331']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-331']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-331']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion


        #endregion


        #region Methods

        public AutomationMASHEpisodeForm1EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
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

            WaitForElement(section1Title);
            WaitForElement(section1Expanded);
            WaitForElement(section1MenuButton);

            WaitForElement(WFShortAnswerQuestionLabel);
            WaitForElement(MashEpisodeDateTimeCaseRecordedLabel);

            #endregion


            return this;
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public AutomationMASHEpisodeForm1EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
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

        public AutomationMASHEpisodeForm1EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
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

        public AutomationMASHEpisodeForm1EditAssessmentPage InsertWFShortAnswerValue(string ValueToInsert)
        {
            SendKeys(WFShortAnswerQuestion, ValueToInsert);
            return this;
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage ValidateWFShortAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFShortAnswerQuestion, ExpectedValue);

            return this;
        }


        public AutomationMASHEpisodeForm1EditAssessmentPage InsertMashEpisodeDateTimeCaseRecordedValue(string Date, string Time)
        {
            SendKeys(MashEpisodeDateTimeCaseRecorded_DateQuestion, Date);
            SendKeys(MashEpisodeDateTimeCaseRecorded_TimeQuestion, Time);
            return this;
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage ValidateMashEpisodeDateTimeCaseRecordedValue(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(MashEpisodeDateTimeCaseRecorded_DateQuestion, ExpectedDate);
            ValidateElementValue(MashEpisodeDateTimeCaseRecorded_TimeQuestion, ExpectedTime);

            return this;
        }


        #endregion


        #endregion


        #endregion


    }
}

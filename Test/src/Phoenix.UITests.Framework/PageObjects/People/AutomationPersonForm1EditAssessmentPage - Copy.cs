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
    public class AutomationPersonForm1EditAssessmentPage : CommonMethods
    {
        public AutomationPersonForm1EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Automation - Person Form 1']");



        #endregion


        #region Section 1

        readonly By section1TitleLink = By.XPath("//a[@id='CWSH_QA-DS-57']");
        readonly By section1Title = By.XPath("//a[@id='CWSH_QA-DS-57']/h2[text()='Section 1']");
        readonly By section1Expanded = By.XPath("//a[@id='CWSH_QA-DS-57'][@class='section-heading level1 open']/h2[text()='Section 1']");
        readonly By section1MenuButton = By.XPath("//div[@id='CWS_QA-DS-57']/fieldset/div/span/a[@title='Click to open']");



        #region Question Labels

        readonly By WFMultipleChoiceQuestionLabel = By.XPath("//ul[@id='QA-DSQ-103']/li/label[text()='WF Multiple Choice']");
        readonly By WFMultipleChoiceOption1Label = By.XPath("//ul[@id='QA-DSQ-103']/li/ul/li/label[text()='Option 1']");
        readonly By WFMultipleChoiceOption2Label = By.XPath("//ul[@id='QA-DSQ-103']/li/ul/li/label[text()='Option 2']");
        readonly By WFMultipleChoiceOption3Label = By.XPath("//ul[@id='QA-DSQ-103']/li/ul/li/label[text()='Option 3']");

        readonly By WFDecimalQuestionLabel = By.XPath("//ul[@id='QA-DSQ-102']/li/label[text()='WF Decimal']");
        
        readonly By WFMultipleResponseQuestionLabel = By.XPath("//ul[@id='QA-DSQ-104']/li/label[text()='WF Multiple Response']");
        readonly By WFMultipleResponseOption1 = By.XPath("//ul[@id='QA-DSQ-104']/li/ul/li/label[text()='Day 1']");
        readonly By WFMultipleResponseOption2 = By.XPath("//ul[@id='QA-DSQ-104']/li/ul/li/label[text()='Day 2']");
        readonly By WFMultipleResponseOption3 = By.XPath("//ul[@id='QA-DSQ-104']/li/ul/li/label[text()='Day 3']");

        readonly By WFNumericQuestionLabel = By.XPath("//ul[@id='QA-DSQ-108']/li/label[text()='WF Numeric']");

        #endregion

        #region Questions

        
        readonly By WFMultipleChoiceRadioButton1 = By.XPath("//input[@value='E92F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton2 = By.XPath("//input[@value='F12F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton3 = By.XPath("//input[@value='4A306139-3F52-E911-A2C5-005056926FE4']");

        readonly By WFDecimalQuestion = By.XPath("//input[@id='QA-DQ-122']");

        readonly By WFMultipleResponseCheckbox1 = By.XPath("//input[@value='FF47D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox2 = By.XPath("//input[@value='0748D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox3 = By.XPath("//input[@value='0F48D74D-3F52-E911-A2C5-005056926FE4']");

        readonly By WFNumericQuestion = By.XPath("//input[@id='QA-DQ-128']");


        #endregion

        #region Menu

        readonly By section1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-57']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1PrintButton = By.XPath("//ul[@id='SM_QA-DS-57']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1PrintHistoryButton = By.XPath("//ul[@id='SM_QA-DS-57']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-57']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-57']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion


        #endregion


        #region Methods

        public AutomationPersonForm1EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
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

            Wait.Until(c => c.FindElement(WFMultipleChoiceQuestionLabel));
            Wait.Until(c => c.FindElement(WFMultipleChoiceOption1Label));
            Wait.Until(c => c.FindElement(WFMultipleChoiceOption2Label));
            Wait.Until(c => c.FindElement(WFMultipleChoiceOption3Label));

            Wait.Until(c => c.FindElement(WFDecimalQuestionLabel));

            Wait.Until(c => c.FindElement(WFMultipleResponseQuestionLabel));
            Wait.Until(c => c.FindElement(WFMultipleResponseOption1));
            Wait.Until(c => c.FindElement(WFMultipleResponseOption2));
            Wait.Until(c => c.FindElement(WFMultipleResponseOption3));

            Wait.Until(c => c.FindElement(WFNumericQuestionLabel));

            #endregion


            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public AutomationPersonForm1EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public AutomationPersonForm1EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public AutomationPersonForm1EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
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

        public AutomationPersonForm1EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public AutomationPersonForm1EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
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

        

        public AutomationPersonForm1EditAssessmentPage SelectWFMultipleChoice(int OptionToSelect)
        {
            switch (OptionToSelect)
            {
                case 1:
                    Click(WFMultipleChoiceRadioButton1);
                    break;
                case 2:
                    Click(WFMultipleChoiceRadioButton2);
                    break;
                case 3:
                    Click(WFMultipleChoiceRadioButton3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage InsertWFDecimalValue(string ValueToInsert)
        {
            SendKeys(WFDecimalQuestion, ValueToInsert);
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage SelectWFMultipleResponse(int OptionToSelect)
        {
            switch (OptionToSelect)
            {
                case 1:
                    Click(WFMultipleResponseCheckbox1);
                    break;
                case 2:
                    Click(WFMultipleResponseCheckbox2);
                    break;
                case 3:
                    Click(WFMultipleResponseCheckbox3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage InsertWFNumericValue(string ValueToInsert)
        {
            SendKeys(WFNumericQuestion, ValueToInsert);
            return this;
        }
       
        public AutomationPersonForm1EditAssessmentPage ValidateWFMultipleChoiceOptionSelected(int OptionSelected)
        {
            switch (OptionSelected)
            {
                case 1:
                    ValidateElementChecked(WFMultipleChoiceRadioButton1);
                    break;
                case 2:
                    ValidateElementChecked(WFMultipleChoiceRadioButton2);
                    break;
                case 3:
                    ValidateElementChecked(WFMultipleChoiceRadioButton3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateWFMultipleChoiceOptionNotSelected(int OptionSelected)
        {
            switch (OptionSelected)
            {
                case 1:
                    ValidateElementNotChecked(WFMultipleChoiceRadioButton1);
                    break;
                case 2:
                    ValidateElementNotChecked(WFMultipleChoiceRadioButton2);
                    break;
                case 3:
                    ValidateElementNotChecked(WFMultipleChoiceRadioButton3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateWFDecimalAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFDecimalQuestion, ExpectedValue);

            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateWFMultipleResponseOptionChecked(int OptionSelected)
        {
            switch (OptionSelected)
            {
                case 1:
                    ValidateElementChecked(WFMultipleResponseCheckbox1);
                    break;
                case 2:
                    ValidateElementChecked(WFMultipleResponseCheckbox2);
                    break;
                case 3:
                    ValidateElementChecked(WFMultipleResponseCheckbox3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateWFMultipleResponseOptionNotChecked(int OptionSelected)
        {
            switch (OptionSelected)
            {
                case 1:
                    ValidateElementNotChecked(WFMultipleResponseCheckbox1);
                    break;
                case 2:
                    ValidateElementNotChecked(WFMultipleResponseCheckbox2);
                    break;
                case 3:
                    ValidateElementNotChecked(WFMultipleResponseCheckbox3);
                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomationPersonForm1EditAssessmentPage ValidateWFNumericAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFNumericQuestion, ExpectedValue);

            return this;
        }

        

        #endregion


        #endregion


        #endregion


    }
}

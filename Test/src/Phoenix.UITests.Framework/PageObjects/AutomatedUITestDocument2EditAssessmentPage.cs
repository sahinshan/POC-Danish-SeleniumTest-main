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
    /// This class represents the Edit Assessment page for the document type "Automated UI Test Document 1"
    /// 
    /// </summary>
    public class AutomatedUITestDocument2EditAssessmentPage : CommonMethods
    {
        public AutomatedUITestDocument2EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Automated UI Test Document 2']");



        #endregion

        #region Person Toolbar

        readonly By personToolbarExpandButton = By.Id("CWToggleButton");

        By personToolbarFullNameAndID(string FirstName, string LastName, string Id) => By.Id("//div[@id='CWInfoLeft']/span/strong[text()='" + LastName.ToUpper() + "," + FirstName + " (Id: " + Id + ")']");

        readonly By personToolbarBornLabel = By.Id("//div[@id='CWInfoRight']/span[1]/strong[text()='Born: ']");

        readonly By personToolbarGender = By.Id("//div[@id='CWInfoRight']/span/strong[text()='Gender: ']");

        readonly By personToolbarNHSNo = By.Id("//div[@id='CWInfoRight']/span/strong[text()='NHS No: ']");

        readonly By personToolbarPreferredName = By.Id("//div[@id='CWBannerHolder']/ul/li/div/span/strong[text()='Preferred Name: ']");


        #endregion

        #region Left Menu

        readonly By leftMenuSection1Link = By.XPath("//a[@id='NL_QA-DS-132']");
        readonly By leftMenuSection2Link = By.XPath("//a[@id='NL_QA-DS-133']");
        readonly By leftMenuCollapseExpandButton = By.XPath("//a[@id='CWSplitter_Link']");

        #endregion

        #region Spelling Bar

        readonly By spellingBarHeader = By.XPath("//*[@id='CWSpellbar']/h2[text()='Spelling']");
        readonly By spellingBarCloseButton = By.XPath("//*[@id='CWSpellbar']/h2/a");

        readonly By spellingBarNotFoundInDictionaryLabel = By.XPath("//*[@id='CWSpellbar']/div[text()='Not Found in Dictionary']");
        By spellingBarHighlightedIncorrectWord(string IncorrectWord) => By.XPath("//*[@id='CWSpellbar']/div[2][text()='" + IncorrectWord + "']");

        readonly By spellingBarSuggestionsLabel = By.XPath("//*[@id='CWSpellbar']/div[text()='Suggestions']");
        readonly By spellingBarSuggestBox = By.XPath("//*[@id='CWSpellbar']/div[@class='suggestbox']");

        By spellingBarSuggestedWord(string SuggestedWord) => By.XPath("//*[@id='CWSpellbar']/div/div/a[text()='" + SuggestedWord + "']");
        By spellingBarSuggestedWordSelected(string SuggestedWord) => By.XPath("//*[@id='CWSpellbar']/div/div/a[@class='selected'][text()='" + SuggestedWord + "']");

        readonly By spellingBarChangeButton= By.XPath("//*[@id='CWSpellbar']/div/div/input[@value='Change']");
        readonly By spellingBarIgnoreButton= By.XPath("//*[@id='CWSpellbar']/div/div/input[@value='Ignore']");
        readonly By spellingBarChangeAllButton = By.XPath("//*[@id='CWSpellbar']/div/div/input[@value='Change All']");
        readonly By spellingBarIgnoreAllButton = By.XPath("//*[@id='CWSpellbar']/div/div/input[@value='Ignore All']");

        readonly By spellingBarLanguagePicklist = By.XPath("//*[@id='CWSpellbar']/div/select");


        #endregion

        #region Dinamic Dialog popup

        By dinamicDialogTitle(string ExpectedTitle) => By.XPath("//div[@id='CWDynamicDialog']/div/header/div/h1[text()='" + ExpectedTitle + "']");

        By dinamicDialogMessage(string ExpectedMessage) => By.XPath("//div[@id='CWDynamicDialog']/div/main/div[text()='" + ExpectedMessage + "']");

        readonly By dinamicDialogOKButton = By.XPath("//div[@id='CWDynamicDialog']/div/footer/input[@type='button'][@value='OK']");

        #endregion

        #region Section 1

        readonly By section1TitleLink = By.XPath("//a[@id='CWSH_QA-DS-132']");
        readonly By section1Title = By.XPath("//a[@id='CWSH_QA-DS-132']/h2[text()='Section 1']");
        readonly By section1Expanded = By.XPath("//a[@id='CWSH_QA-DS-132'][@class='section-heading level1 open']/h2[text()='Section 1']");
        readonly By section1MenuButton = By.XPath("//div[@id='CWS_QA-DS-132']/fieldset/div/span/a[@title='Click to open']");



        #region Question Labels

        readonly By WFMultipleChoiceQuestionLabel = By.XPath("//ul[@id='QA-DSQ-381']/li/label[text()='WF Multiple Choice']");
        readonly By WFMultipleChoiceOption1Label = By.XPath("//ul[@id='QA-DSQ-381']/li/ul/li/label[text()='Option 1']");
        readonly By WFMultipleChoiceOption2Label = By.XPath("//ul[@id='QA-DSQ-381']/li/ul/li/label[text()='Option 2']");
        readonly By WFMultipleChoiceOption3Label = By.XPath("//ul[@id='QA-DSQ-381']/li/ul/li/label[text()='Option 3']");

        readonly By WFDecimalQuestionLabel = By.XPath("//ul[@id='QA-DSQ-382']/li/label[text()='WF Decimal']");
        
        readonly By WFMultipleResponseQuestionLabel = By.XPath("//ul[@id='QA-DSQ-383']/li/label[text()='WF Multiple Response']");
        readonly By WFMultipleResponseOption1 = By.XPath("//ul[@id='QA-DSQ-383']/li/ul/li/label[text()='Day 1']");
        readonly By WFMultipleResponseOption2 = By.XPath("//ul[@id='QA-DSQ-383']/li/ul/li/label[text()='Day 2']");
        readonly By WFMultipleResponseOption3 = By.XPath("//ul[@id='QA-DSQ-383']/li/ul/li/label[text()='Day 3']");

        readonly By WFNumericQuestionLabel = By.XPath("//ul[@id='QA-DSQ-384']/li/label[text()='WF Numeric']");
        readonly By WFLookupQuestionLabel = By.XPath("//ul[@id='QA-DSQ-385']/li/label[text()='WF Lookup']");
        readonly By WFDateQuestionLabel = By.XPath("//ul[@id='QA-DSQ-386']/li/label[text()='WF Date']");
        readonly By WFShortAnswerQuestionLabel = By.XPath("//ul[@id='QA-DSQ-387']/li/label[text()='WF Short Answer']");
        readonly By WFParagraphQuestionLabel = By.XPath("//ul[@id='QA-DSQ-388']/li/label[text()='WF Paragraph']");
        readonly By WFPickListQuestionLabel = By.XPath("//ul[@id='QA-DSQ-389']/li/label[text()='WF PickList']");
        readonly By WFBooleanQuestionLabel = By.XPath("//ul[@id='QA-DSQ-390']/li/label[text()='WF Boolean']");
        readonly By WFTimeQuestionLabel = By.XPath("//ul[@id='QA-DSQ-391']/li/label[text()='WF Time']");

        readonly By TestHQQuestionLabel = By.XPath("//ul[@id='QA-DSQ-392']/li/ul/li/label");


        #endregion

        #region Questions

        
        readonly By WFMultipleChoiceRadioButton1 = By.XPath("//input[@value='E92F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton2 = By.XPath("//input[@value='F12F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton3 = By.XPath("//input[@value='4A306139-3F52-E911-A2C5-005056926FE4']");

        readonly By WFDecimalQuestion = By.XPath("//input[@id='QA-DQ-464']");

        readonly By WFMultipleResponseCheckbox1 = By.XPath("//input[@value='FF47D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox2 = By.XPath("//input[@value='0748D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox3 = By.XPath("//input[@value='0F48D74D-3F52-E911-A2C5-005056926FE4']");

        readonly By WFNumericQuestion = By.XPath("//input[@id='QA-DQ-466']");

        readonly By WFLookupAnswer = By.XPath("//a[@id='QA-DQ-467_LookupLink']");
        readonly By WFLookupClearbutton = By.XPath("//div[@id='QA-DQ-467']/div/div/button[@title='Clear QA-DQ-467']");
        readonly By WFLookupSearchButton = By.XPath("//div[@id='QA-DQ-467']/div/div/button[@title='Lookup QA-DQ-467']");

        readonly By WFDateQuestion = By.XPath("//input[@id='QA-DQ-468']");        
        readonly By WFShortAnswerQuestion = By.XPath("//input[@id='QA-DQ-469']");
        readonly By WFparagraphQuestion = By.XPath("//textarea[@id='QA-DQ-470']");
        readonly By WFPicklistQuestion = By.XPath("//select[@id='QA-DQ-471']");
        
        readonly By WFBooleanQuestionYesOption = By.XPath("//input[@id='QA-DQ-472']");
        readonly By WFBooleanQuestionNoOption = By.XPath("//input[@id='QA-DQ-472_F']");

        readonly By WFTimeQuestion = By.XPath("//input[@id='QA-DQ-473']");

        readonly By LocationRow1Question = By.XPath("//input[@id='QA-DQ-475']");
        readonly By TestDecRow1Question = By.XPath("//input[@id='QA-DQ-476']");
        readonly By LocationRow2Question = By.XPath("//input[@id='QA-DQ-477']");
        readonly By TestDecRow2Question = By.XPath("//input[@id='QA-DQ-478']");


        #endregion

        #region Menu

        readonly By section1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-132']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1PrintButton = By.XPath("//ul[@id='SM_QA-DS-132']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1PrintHistoryButton = By.XPath("//ul[@id='SM_QA-DS-132']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-132']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-132']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion


        #endregion


        #region Methods

        public AutomatedUITestDocument2EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
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

            #region Left Menu

            Wait.Until(c => c.FindElement(leftMenuSection1Link));
            Wait.Until(c => c.FindElement(leftMenuSection2Link));


            Wait.Until(c => c.FindElement(leftMenuCollapseExpandButton));

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
            Wait.Until(c => c.FindElement(WFLookupQuestionLabel));
            Wait.Until(c => c.FindElement(WFDateQuestionLabel));
            Wait.Until(c => c.FindElement(WFShortAnswerQuestionLabel));
            Wait.Until(c => c.FindElement(WFParagraphQuestionLabel));
            Wait.Until(c => c.FindElement(WFPickListQuestionLabel));
            Wait.Until(c => c.FindElement(WFBooleanQuestionLabel));
            Wait.Until(c => c.FindElement(WFTimeQuestionLabel));
            
            Wait.Until(c => c.FindElement(TestHQQuestionLabel));

            #endregion


            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public AutomatedUITestDocument2EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public AutomatedUITestDocument2EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
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

        public AutomatedUITestDocument2EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
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

        #region Left Menu

        public AutomatedUITestDocument2EditAssessmentPage TapCollapseExpandLeftMenuButton()
        {
            Click(leftMenuCollapseExpandButton);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage TapLeftMenuSection1()
        {
            Click(leftMenuSection1Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section1TitleLink);

            return this;
        }

      
        public AutomatedUITestDocument2EditAssessmentPage WaitForLeftMenuVisible()
        {
            #region Left Menu

            Wait.Until(c => c.FindElement(leftMenuSection1Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection2Link).Displayed);

            #endregion

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage WaitForLeftMenuHiden()
        {
            #region Left Menu

            Wait.Until(c => c.FindElement(leftMenuSection1Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection2Link).Displayed == false);

            #endregion

            return this;
        }



        #endregion

        #region Section 1

        #region Questions

        

        public AutomatedUITestDocument2EditAssessmentPage SelectWFMultipleChoice(int OptionToSelect)
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

        public AutomatedUITestDocument2EditAssessmentPage InsertWFDecimalValue(string ValueToInsert)
        {
            SendKeys(WFDecimalQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage SelectWFMultipleResponse(int OptionToSelect)
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

        public AutomatedUITestDocument2EditAssessmentPage InsertWFNumericValue(string ValueToInsert)
        {
            SendKeys(WFNumericQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage InsertWFDateValue(string ValueToInsert)
        {
            SendKeys(WFDateQuestion, ValueToInsert);
            return this;
        }

        public LookupPopup TapWFLookupLookupButton()
        {
            Click(WFLookupSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return new LookupPopup(driver, Wait, appURL);
        }




       
        public AutomatedUITestDocument2EditAssessmentPage ValidateWFMultipleChoiceOptionSelected(int OptionSelected)
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

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFMultipleChoiceOptionNotSelected(int OptionSelected)
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

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFDecimalAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFDecimalQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFMultipleResponseOptionChecked(int OptionSelected)
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

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFMultipleResponseOptionNotChecked(int OptionSelected)
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

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFNumericAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFNumericQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFLookupLookupValue(string ExpectedText)
        {
            if (string.IsNullOrEmpty(ExpectedText))
            {
                //if the string is empty it means that the test is trying to assert that the lookup is empty
                //in that case we need to assert that no link element exists
                ValidateElementDoNotExist(WFLookupAnswer);

                return this;
            }

            ValidateElementText(WFLookupAnswer, ExpectedText);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFDateAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFDateQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFShortAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFShortAnswerQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFParagraphAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFparagraphQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFPicklistSelectedValue(string ExpectedSelectedElement)
        {
            ValidatePicklistSelectedText(WFPicklistQuestion, ExpectedSelectedElement);
            
            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFBoolean(bool? ExpectedValue)
        {
            if (ExpectedValue.HasValue)
            {
                switch (ExpectedValue.Value)
                {
                    case true:
                        ValidateElementChecked(WFBooleanQuestionYesOption);

                        break;
                    case false:
                        ValidateElementChecked(WFBooleanQuestionNoOption);

                        break;
                    default:
                        break;
                }
            }
            else
            {
                ValidateElementNotChecked(WFBooleanQuestionYesOption);
                ValidateElementNotChecked(WFBooleanQuestionNoOption);
            }
            
            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateWFTimeAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFTimeQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateLocationRow1Answer(string ExpectedText)
        {
            ValidateElementValue(LocationRow1Question, ExpectedText);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateTestDecRow1Answer(string ExpectedText)
        {
            ValidateElementValue(TestDecRow1Question, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateLocationRow2Answer(string ExpectedText)
        {
            ValidateElementValue(LocationRow2Question, ExpectedText);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage ValidateTestDecRow2Answer(string ExpectedText)
        {
            ValidateElementValue(TestDecRow2Question, ExpectedText);
            return this;
        }

        #endregion


        #endregion

        #region Dinamic Dialog Popup

        public AutomatedUITestDocument2EditAssessmentPage WaitForDinamicDialogeToOpen(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementVisible(dinamicDialogTitle(ExpectedTitle));
            WaitForElementVisible(dinamicDialogMessage(ExpectedMessage));
            WaitForElementVisible(dinamicDialogOKButton);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage WaitForDinamicDialogeToClose(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementNotVisible(dinamicDialogTitle(ExpectedTitle), 3);
            WaitForElementNotVisible(dinamicDialogMessage(ExpectedMessage), 3);
            WaitForElementNotVisible(dinamicDialogOKButton, 3);

            return this;
        }

        public AutomatedUITestDocument2EditAssessmentPage DinamicDialogeTapOKButton()
        {
            Click(dinamicDialogOKButton);

            return this;
        }

        #endregion

        #endregion


    }
}

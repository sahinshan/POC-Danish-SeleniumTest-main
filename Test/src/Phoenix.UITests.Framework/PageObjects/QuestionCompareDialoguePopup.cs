using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// This class represents the Section information popup.
    /// this popup is displayed when a user open an assessment in edit mode, taps on a section (or sub section) menu button and tap on the "Section Information" link
    /// </summary>
    public class QuestionCompareDialoguePopup : CommonMethods
    {
        public QuestionCompareDialoguePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWAssessmentCompareDialog = By.Id("iframe_CWAssessmentCompareDialog");

        readonly By popupTitle = By.XPath("//*[@id='CWAssessmentCompareTitle']");



        #region Labels

        readonly By assessmentLabel = By.XPath("//label[@for='CWAssessmentCompareAssessment']");
        readonly By compareWithLabel = By.XPath("//label[@for='CWAssessmentCompareWith']");


        #endregion

        #region Fields

        readonly By assessmentPickList = By.XPath("//*[@id='CWAssessmentCompareAssessment']");
        readonly By compareWithPickList = By.XPath("//*[@id='CWAssessmentCompareWith']");
        
        #endregion

        #region Buttons

        readonly By closeButton = By.XPath("//*[@id='CWCompareCloseButton']");

        #endregion

        #region Compare First

        readonly By compareFirstTitle = By.XPath("//*[@id='CWAssessmentCompareFirstTitle']");

        #region WF Multiple Choice

        readonly By compareFirstWFMultipleChoiceQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Multiple Choice']");
        
        readonly By compareFirstWFMultipleChoiceOption1Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Option 1']");
        readonly By compareFirstWFMultipleChoiceOption2Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Option 2']");
        readonly By compareFirstWFMultipleChoiceOption3Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Option 3']");

        readonly By compareFirstWFMultipleChoiceOption1CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Option 1']");
        readonly By compareFirstWFMultipleChoiceOption2CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Option 2']");
        readonly By compareFirstWFMultipleChoiceOption3CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Option 3']");

        #endregion

        #region WF Decimal

        readonly By compareFirstWFDecimalQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Decimal']");
        readonly By compareFirstWFDecimalInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/input");

        #endregion

        #region WF Multiple Response

        readonly By compareFirstWFMultipleResponseQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Multiple Response']");
                                          
        readonly By compareFirstWFMultipleResponseDay1Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Day 1']");
        readonly By compareFirstWFMultipleResponseDay2Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Day 2']");
        readonly By compareFirstWFMultipleResponseDay3Label = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Day 3']");
                                          
        readonly By compareFirstWFMultipleResponseDay1CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Day 1']");
        readonly By compareFirstWFMultipleResponseDay2CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Day 2']");
        readonly By compareFirstWFMultipleResponseDay3CheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Day 3']");

        #endregion

        #region WF Numeric

        readonly By compareFirstWFNumericQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Numeric']");
        readonly By compareFirstWFNumericInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/input");

        #endregion

        #region WF Lookup

        readonly By compareFirstWFLookupQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Lookup']");
        By compareFirstWFLookupInput(string ExpectedText) => By.XPath("//div[@id='CWAssessmentCompareFirst']/ul/li/div/div/div/a[@id='QA-DQ-168_LookupLink'][text()='" + ExpectedText + "']");

        #endregion

        #region WF Date

        readonly By compareFirstWFDateQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Date']");
        readonly By compareFirstWFDateInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/input");

        #endregion

        #region WF Paragraph

        readonly By compareFirstWFParagraphQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Paragraph']");
        readonly By compareFirstWFParagraphInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/textarea");

        #endregion

        #region WF Picklist

        readonly By compareFirstWFPicklistQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF PickList']");
        readonly By compareFirstWFPicklistInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/select");

        #endregion

        #region WF Short Answer

        readonly By compareFirstWFShortAnswerQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Short Answer']");
        readonly By compareFirstWFShortAnswerInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/input");

        #endregion

        #region Test HQ

        readonly By compareFirstTestHQQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='Test HQ']");
        readonly By compareFirstTestHQColumn1Header = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Location']");
        readonly By compareFirstTestHQColumn2Header = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Test Dec']");
        readonly By compareFirstTestHQRow1Column1 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-177']");
        readonly By compareFirstTestHQRow1Column2 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-178']");
        readonly By compareFirstTestHQRow2Column1 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-179']");
        readonly By compareFirstTestHQRow2Column2 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-180']");

        #endregion

        #region WF Table With Unlimited Rows

        readonly By compareFirstWFTableWithUnlimitedRowsQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='WF Table With Unlimited Rows']");
        readonly By compareFirstWFTableWithUnlimitedRowsQuestionSubHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li[text()='WF Unlimited Rows Table Sub Heading']");
        readonly By compareFirstWFTableWithUnlimitedRowsColumn1Header = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Date became involved']");
        readonly By compareFirstWFTableWithUnlimitedRowsColumn2Header = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Reason for Assessment']");
        readonly By compareFirstWFTableWithUnlimitedRowsRow1Column1 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='1-QA-DQ-245']");
        readonly By compareFirstWFTableWithUnlimitedRowsRow1Column2 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/select[@id='1-QA-DQ-246']");
        readonly By compareFirstWFTableWithUnlimitedRowsRow2Column1 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='2-QA-DQ-245']");
        readonly By compareFirstWFTableWithUnlimitedRowsRow2Column2 = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/select[@id='2-QA-DQ-246']");

        #endregion

        #region Table PQ

        readonly By compareFirstTablePQQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='Table PQ']");

        readonly By compareFirstTablePQQuestion1Heading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='Question 1']");
        readonly By compareFirstTablePQQuestion1SubHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li[text()='Question 1 - Sub Heading']");
        readonly By compareFirstTablePQQuestion1ContributionNotesHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[@for='QA-DQ-253'][text()='Contribution Notes']");
        readonly By compareFirstTablePQQuestion1ContributionNotes = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/textarea[@id='QA-DQ-253']");
        readonly By compareFirstTablePQQuestion1RoleHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[@for='QA-DQ-255'][text()='Role']");
        readonly By compareFirstTablePQQuestion1Role = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/input[@id='QA-DQ-255']");

        readonly By compareFirstTablePQQuestion2Heading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='Question 2']");
        readonly By compareFirstTablePQQuestion2SubHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li[text()='Question 2 - Sub Heading']");
        readonly By compareFirstTablePQQuestion2ContributionNotesHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[@for='QA-DQ-254'][text()='Contribution Notes']");
        readonly By compareFirstTablePQQuestion2ContributionNotes = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/textarea[@id='QA-DQ-254']");
        readonly By compareFirstTablePQQuestion2RoleHeading = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[@for='QA-DQ-256'][text()='Role']");
        readonly By compareFirstTablePQQuestion2Role = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/input[@id='QA-DQ-256']");


        #endregion

        #region Test QPC

        readonly By compareFirstTestQPCQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/label[text()='Test QPC']");

        readonly By compareFirstTestQPCOutcomeHeader = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Outcome']");
        readonly By compareFirstTestQPCTypeOfInvolvementHeader = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Type of Involvement']");
        readonly By compareFirstTestQPCWFTimeHeader = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='WF Time']");
        readonly By compareFirstTestQPCWhoHeader = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Who']");

        readonly By compareFirstTestQPCOutcome = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-185']");
        readonly By compareFirstTestQPCTypeOfInvolvement = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-258']");
        readonly By compareFirstTestQPCWFTime = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-260']");
        readonly By compareFirstTestQPCWho = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-262']");

        #endregion

        #region WF Boolean

        readonly By compareFirstWFBooleanQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Boolean']");
                                  
        readonly By compareFirstWFBooleanYesLabel = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='Yes']");
        readonly By compareFirstWFBooleanNoLabel = By.XPath("//*[@id='CWAssessmentCompareFirst']//label[text()='No']");
                                  
        readonly By compareFirstWFBooleanYesCheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='Yes']");
        readonly By compareFirstWFBooleanNoCheckBox = By.XPath("//*[@id='CWAssessmentCompareFirst']//input[@title='No']");

        #endregion

        #region WF Time

        readonly By compareFirstWFTimeQuestionName = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/label[text()='WF Time']");
        readonly By compareFirstWFTimeInput = By.XPath("//*[@id='CWAssessmentCompareFirst']/ul/li/input");

        #endregion

        #endregion

        #region Compare Second

        readonly By compareSecondTitle = By.XPath("//*[@id='CWAssessmentCompareSecondTitle']");

        #region WF Multiple Choice

        readonly By compareSecondWFMultipleChoiceQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Multiple Choice']");

        readonly By compareSecondWFMultipleChoiceOption1Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Option 1']");
        readonly By compareSecondWFMultipleChoiceOption2Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Option 2']");
        readonly By compareSecondWFMultipleChoiceOption3Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Option 3']");

        readonly By compareSecondWFMultipleChoiceOption1CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Option 1']");
        readonly By compareSecondWFMultipleChoiceOption2CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Option 2']");
        readonly By compareSecondWFMultipleChoiceOption3CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Option 3']");

        #endregion

        #region WF Decimal

        readonly By compareSecondWFDecimalQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Decimal']");
        readonly By compareSecondWFDecimalInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/input");

        #endregion

        #region WF Multiple Response

        readonly By compareSecondWFMultipleResponseQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Multiple Response']");
                                                                                                        
        readonly By compareSecondWFMultipleResponseDay1Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Day 1']");
        readonly By compareSecondWFMultipleResponseDay2Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Day 2']");
        readonly By compareSecondWFMultipleResponseDay3Label = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Day 3']");
                           
        readonly By compareSecondWFMultipleResponseDay1CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Day 1']");
        readonly By compareSecondWFMultipleResponseDay2CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Day 2']");
        readonly By compareSecondWFMultipleResponseDay3CheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Day 3']");

        #endregion

        #region WF Numeric

        readonly By compareSecondWFNumericQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Numeric']");
        readonly By compareSecondWFNumericInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/input");

        #endregion

        #region WF Lookup

        readonly By compareSecondWFLookupQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Lookup']");
        By compareSecondWFLookupInput(string ExpectedText) => By.XPath("//div[@id='CWAssessmentCompareSecond']/ul/li/div/div/div/a[@id='QA-DQ-168_LookupLink'][text()='" + ExpectedText + "']");

        #endregion

        #region WF Date

        readonly By compareSecondWFDateQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Date']");
        readonly By compareSecondWFDateInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/input");

        #endregion

        #region WF Paragraph

        readonly By compareSecondWFParagraphQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Paragraph']");
        readonly By compareSecondWFParagraphInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/textarea");

        #endregion

        #region WF Picklist

        readonly By compareSecondWFPicklistQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF PickList']");
        readonly By compareSecondWFPicklistInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/select");

        #endregion

        #region WF Short Answer

        readonly By compareSecondWFShortAnswerQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Short Answer']");
        readonly By compareSecondWFShortAnswerInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/input");

        #endregion

        #region Test HQ

        readonly By compareSecondTestHQQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='Test HQ']");
        readonly By compareSecondTestHQColumn1Header = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Location']");
        readonly By compareSecondTestHQColumn2Header = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Test Dec']");
        readonly By compareSecondTestHQRow1Column1 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-177']");
        readonly By compareSecondTestHQRow1Column2 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-178']");
        readonly By compareSecondTestHQRow2Column1 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-179']");
        readonly By compareSecondTestHQRow2Column2 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-180']");

        #endregion

        #region WF Table With Unlimited Rows

        readonly By compareSecondWFTableWithUnlimitedRowsQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='WF Table With Unlimited Rows']");
        readonly By compareSecondWFTableWithUnlimitedRowsQuestionSubHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li[text()='WF Unlimited Rows Table Sub Heading']");
        readonly By compareSecondWFTableWithUnlimitedRowsColumn1Header = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Date became involved']");
        readonly By compareSecondWFTableWithUnlimitedRowsColumn2Header = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/thead/tr/th/span[text()='Reason for Assessment']");
        readonly By compareSecondWFTableWithUnlimitedRowsRow1Column1 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='1-QA-DQ-245']");
        readonly By compareSecondWFTableWithUnlimitedRowsRow1Column2 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/select[@id='1-QA-DQ-246']");
        readonly By compareSecondWFTableWithUnlimitedRowsRow2Column1 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='2-QA-DQ-245']");
        readonly By compareSecondWFTableWithUnlimitedRowsRow2Column2 = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/select[@id='2-QA-DQ-246']");

        #endregion

        #region Table PQ

        readonly By compareSecondTablePQQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='Table PQ']");
                           
        readonly By compareSecondTablePQQuestion1Heading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='Question 1']");
        readonly By compareSecondTablePQQuestion1SubHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li[text()='Question 1 - Sub Heading']");
        readonly By compareSecondTablePQQuestion1ContributionNotesHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[@for='QA-DQ-253'][text()='Contribution Notes']");
        readonly By compareSecondTablePQQuestion1ContributionNotes = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/textarea[@id='QA-DQ-253']");
        readonly By compareSecondTablePQQuestion1RoleHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[@for='QA-DQ-255'][text()='Role']");
        readonly By compareSecondTablePQQuestion1Role = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/input[@id='QA-DQ-255']");
                           
        readonly By compareSecondTablePQQuestion2Heading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='Question 2']");
        readonly By compareSecondTablePQQuestion2SubHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li[text()='Question 2 - Sub Heading']");
        readonly By compareSecondTablePQQuestion2ContributionNotesHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[@for='QA-DQ-254'][text()='Contribution Notes']");
        readonly By compareSecondTablePQQuestion2ContributionNotes = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/textarea[@id='QA-DQ-254']");
        readonly By compareSecondTablePQQuestion2RoleHeading = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[@for='QA-DQ-256'][text()='Role']");
        readonly By compareSecondTablePQQuestion2Role = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/input[@id='QA-DQ-256']");


        #endregion

        #region Test QPC

        readonly By compareSecondTestQPCQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/label[text()='Test QPC']");
                           
        readonly By compareSecondTestQPCOutcomeHeader = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Outcome']");
        readonly By compareSecondTestQPCTypeOfInvolvementHeader = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Type of Involvement']");
        readonly By compareSecondTestQPCWFTimeHeader = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='WF Time']");
        readonly By compareSecondTestQPCWhoHeader = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/label[text()='Who']");
                           
        readonly By compareSecondTestQPCOutcome = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-185']");
        readonly By compareSecondTestQPCTypeOfInvolvement = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-258']");
        readonly By compareSecondTestQPCWFTime = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-260']");
        readonly By compareSecondTestQPCWho = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/ul/li/div/table/tbody/tr/td/ul/li/input[@id='QA-DQ-262']");

        #endregion

        #region WF Boolean

        readonly By compareSecondWFBooleanQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Boolean']");
                           
        readonly By compareSecondWFBooleanYesLabel = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='Yes']");
        readonly By compareSecondWFBooleanNoLabel = By.XPath("//*[@id='CWAssessmentCompareSecond']//label[text()='No']");
                           
        readonly By compareSecondWFBooleanYesCheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='Yes']");
        readonly By compareSecondWFBooleanNoCheckBox = By.XPath("//*[@id='CWAssessmentCompareSecond']//input[@title='No']");

        #endregion

        #region WF Time

        readonly By compareSecondWFTimeQuestionName = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/label[text()='WF Time']");
        readonly By compareSecondWFTimeInput = By.XPath("//*[@id='CWAssessmentCompareSecond']/ul/li/input");

        #endregion

        #endregion



        public QuestionCompareDialoguePopup WaitForQuestionCompareDialoguePopupToLoad(string ExpectedPopupTitle)
        {
            WaitForElement(iframe_CWAssessmentCompareDialog);
            SwitchToIframe(iframe_CWAssessmentCompareDialog);

            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(popupTitle);
            ValidateElementText(popupTitle, ExpectedPopupTitle);

            WaitForElement(assessmentLabel);
            WaitForElement(compareWithLabel);

            WaitForElement(assessmentPickList);
            WaitForElement(compareWithPickList);
            
            WaitForElement(closeButton);
            
            return this;
        }


        public QuestionCompareDialoguePopup SelectAssessmentByValue(string ValueToSelect)
        {
            SelectPicklistElementByValue(assessmentPickList, ValueToSelect);

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public QuestionCompareDialoguePopup SelectCompareWithByValue(string ValueToSelect)
        {
            SelectPicklistElementByValue(compareWithPickList, ValueToSelect);

            System.Threading.Thread.Sleep(500);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateCompareFirstTitle(string ExpectedText)
        {
            ValidateElementText(compareFirstTitle, ExpectedText);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateCompareSecondTitle(string ExpectedText)
        {
            ValidateElementText(compareSecondTitle, ExpectedText);
            
            return this;
        }



        #region WF Multiple Choice


        public QuestionCompareDialoguePopup WaitForWFMultipleChoiceCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFMultipleChoiceQuestionName);

            WaitForElementVisible(compareFirstWFMultipleChoiceOption1Label);
            WaitForElementVisible(compareFirstWFMultipleChoiceOption2Label);
            WaitForElementVisible(compareFirstWFMultipleChoiceOption3Label);

            WaitForElementVisible(compareSecondWFMultipleChoiceQuestionName);
            
            WaitForElementVisible(compareSecondWFMultipleChoiceOption1Label);
            WaitForElementVisible(compareSecondWFMultipleChoiceOption2Label);
            WaitForElementVisible(compareSecondWFMultipleChoiceOption3Label);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareFirstOption1Checked(bool ExpectCheked)
        {
            if(ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleChoiceOption1CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleChoiceOption1CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareFirstOption2Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleChoiceOption2CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleChoiceOption2CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareFirstOption3Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleChoiceOption3CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleChoiceOption3CheckBox);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareSecondOption1Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleChoiceOption1CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleChoiceOption1CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareSecondOption2Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleChoiceOption2CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleChoiceOption2CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleChoiceCompareSecondOption3Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleChoiceOption3CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleChoiceOption3CheckBox);

            return this;
        }

        #endregion

        #region WF Decimal


        public QuestionCompareDialoguePopup WaitForWFDecimalCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFDecimalQuestionName);
            WaitForElementVisible(compareFirstWFDecimalInput);
            WaitForElementVisible(compareSecondWFDecimalQuestionName);
            WaitForElementVisible(compareSecondWFDecimalInput);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFDecimalCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFDecimalInput, ExpectedValue);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFDecimalCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFDecimalInput, ExpectedValue);

            return this;
        }

        #endregion

        #region WF Multiple Response
        
        public QuestionCompareDialoguePopup WaitForWFMultipleResponseCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFMultipleResponseQuestionName);

            WaitForElementVisible(compareFirstWFMultipleResponseDay1Label);
            WaitForElementVisible(compareFirstWFMultipleResponseDay2Label);
            WaitForElementVisible(compareFirstWFMultipleResponseDay3Label);

            WaitForElementVisible(compareSecondWFMultipleResponseQuestionName);

            WaitForElementVisible(compareSecondWFMultipleResponseDay1Label);
            WaitForElementVisible(compareSecondWFMultipleResponseDay2Label);
            WaitForElementVisible(compareSecondWFMultipleResponseDay3Label);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareFirstDay1Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleResponseDay1CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleResponseDay1CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareFirstDay2Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleResponseDay2CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleResponseDay2CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareFirstDay3Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFMultipleResponseDay3CheckBox);
            else
                ValidateElementNotChecked(compareFirstWFMultipleResponseDay3CheckBox);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareSecondDay1Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleResponseDay1CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleResponseDay1CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareSecondDay2Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleResponseDay2CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleResponseDay2CheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFMultipleResponseCompareSecondDay3Checked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFMultipleResponseDay3CheckBox);
            else
                ValidateElementNotChecked(compareSecondWFMultipleResponseDay3CheckBox);

            return this;
        }

        #endregion

        #region WF Numeric
        
        public QuestionCompareDialoguePopup WaitForWFNumericCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFNumericQuestionName);
            WaitForElementVisible(compareFirstWFNumericInput);
            WaitForElementVisible(compareSecondWFNumericQuestionName);
            WaitForElementVisible(compareSecondWFNumericInput);

            return this;
        }
        
        public QuestionCompareDialoguePopup ValidateWFNumericCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFNumericInput, ExpectedValue);

            return this;
        }
        
        public QuestionCompareDialoguePopup ValidateWFNumericCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFNumericInput, ExpectedValue);

            return this;
        }

        #endregion

        #region WF Lookup

        public QuestionCompareDialoguePopup WaitForWFLookupCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFLookupQuestionName);
            WaitForElementVisible(compareSecondWFLookupQuestionName);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFLookupCompareFirstInputValue(string ExpectedValue)
        {
            WaitForElement(compareFirstWFLookupInput(ExpectedValue));

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFLookupCompareSecondInputValue(string ExpectedValue)
        {
            WaitForElement(compareSecondWFLookupInput(ExpectedValue));

            return this;
        }

        #endregion

        #region WF Date

        public QuestionCompareDialoguePopup WaitForWFDateCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFDateQuestionName);
            WaitForElementVisible(compareFirstWFDateInput);
            WaitForElementVisible(compareSecondWFDateQuestionName);
            WaitForElementVisible(compareSecondWFDateInput);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFDateCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFDateInput, ExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFDateCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFDateInput, ExpectedValue);

            return this;
        }

        #endregion

        #region WF Paragraph

        public QuestionCompareDialoguePopup WaitForWFParagraphCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFParagraphQuestionName);
            WaitForElementVisible(compareFirstWFParagraphInput);
            ScrollToElement(compareSecondWFParagraphQuestionName);
            WaitForElementVisible(compareSecondWFParagraphQuestionName);
            ScrollToElement(compareSecondWFParagraphInput);
            WaitForElementVisible(compareSecondWFParagraphInput);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFParagraphCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFParagraphInput, ExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFParagraphCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFParagraphInput, ExpectedValue);

            return this;
        }

        #endregion

        #region WF Picklist

        public QuestionCompareDialoguePopup WaitForWFPicklistCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFPicklistQuestionName);
            WaitForElementVisible(compareFirstWFPicklistInput);
            WaitForElementVisible(compareSecondWFPicklistQuestionName);
            WaitForElementVisible(compareSecondWFPicklistInput);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFPicklistCompareFirstInputValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(compareFirstWFPicklistInput, ExpectedSelectedText);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFPicklistCompareSecondInputValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(compareSecondWFPicklistInput, ExpectedSelectedText);

            return this;
        }

        #endregion

        #region WF Short Answer

        public QuestionCompareDialoguePopup WaitForWFShortAnswerCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFShortAnswerQuestionName);
            WaitForElementVisible(compareFirstWFShortAnswerInput);
            WaitForElementVisible(compareSecondWFShortAnswerQuestionName);
            WaitForElementVisible(compareSecondWFShortAnswerInput);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFShortAnswerCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFShortAnswerInput, ExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFShortAnswerCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFShortAnswerInput, ExpectedValue);

            return this;
        }

        #endregion

        #region Test HQ

        public QuestionCompareDialoguePopup WaitForTestHQCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstTestHQQuestionName);
            WaitForElementVisible(compareFirstTestHQColumn1Header);
            WaitForElementVisible(compareFirstTestHQColumn2Header);
            WaitForElementVisible(compareFirstTestHQRow1Column1);
            WaitForElementVisible(compareFirstTestHQRow1Column2);
            WaitForElementVisible(compareFirstTestHQRow2Column1);
            WaitForElementVisible(compareFirstTestHQRow2Column2);

            WaitForElementVisible(compareSecondTestHQQuestionName);
            WaitForElementVisible(compareSecondTestHQColumn1Header);
            WaitForElementVisible(compareSecondTestHQColumn2Header);
            WaitForElementVisible(compareSecondTestHQRow1Column1);
            WaitForElementVisible(compareSecondTestHQRow1Column2);
            WaitForElementVisible(compareSecondTestHQRow2Column1);
            WaitForElementVisible(compareSecondTestHQRow2Column2);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateTestHQCompareFirstInputValue(string Row1Column1ExpectedValue, string Row1Column2ExpectedValue, string Row2Column1ExpectedValue, string Row2Column2ExpectedValue)
        {
            ValidateElementValue(compareFirstTestHQRow1Column1, Row1Column1ExpectedValue);
            ValidateElementValue(compareFirstTestHQRow1Column2, Row1Column2ExpectedValue);
            ValidateElementValue(compareFirstTestHQRow2Column1, Row2Column1ExpectedValue);
            ValidateElementValue(compareFirstTestHQRow2Column2, Row2Column2ExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateTestHQCompareSecondInputValue(string Row1Column1ExpectedValue, string Row1Column2ExpectedValue, string Row2Column1ExpectedValue, string Row2Column2ExpectedValue)
        {
            ValidateElementValue(compareSecondTestHQRow1Column1, Row1Column1ExpectedValue);
            ValidateElementValue(compareSecondTestHQRow1Column2, Row1Column2ExpectedValue);
            ValidateElementValue(compareSecondTestHQRow2Column1, Row2Column1ExpectedValue);
            ValidateElementValue(compareSecondTestHQRow2Column2, Row2Column2ExpectedValue);

            return this;
        }

        #endregion

        #region WF Table With Unlimited Rows

        public QuestionCompareDialoguePopup WaitForWFTableWithUnlimitedRowsCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsQuestionName);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsQuestionSubHeading);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsColumn1Header);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsColumn2Header);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsRow1Column1);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsRow1Column2);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsRow2Column1);
            WaitForElementVisible(compareFirstWFTableWithUnlimitedRowsRow2Column2);

            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsQuestionName);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsQuestionSubHeading);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsColumn1Header);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsColumn2Header);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsRow1Column1);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsRow1Column2);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsRow2Column1);
            WaitForElementVisible(compareSecondWFTableWithUnlimitedRowsRow2Column2);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFTableWithUnlimitedRowsCompareFirstInputValue(string Row1Column1ExpectedValue, string Row1Column2ExpectedValue, string Row2Column1ExpectedValue, string Row2Column2ExpectedValue)
        {
            ValidateElementValue(compareFirstWFTableWithUnlimitedRowsRow1Column1, Row1Column1ExpectedValue);
            ValidatePicklistSelectedText(compareFirstWFTableWithUnlimitedRowsRow1Column2, Row1Column2ExpectedValue);
            ValidateElementValue(compareFirstWFTableWithUnlimitedRowsRow2Column1, Row2Column1ExpectedValue);
            ValidatePicklistSelectedText(compareFirstWFTableWithUnlimitedRowsRow2Column2, Row2Column2ExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFTableWithUnlimitedRowsCompareSecondInputValue(string Row1Column1ExpectedValue, string Row1Column2ExpectedValue, string Row2Column1ExpectedValue, string Row2Column2ExpectedValue)
        {
            ValidateElementValue(compareSecondWFTableWithUnlimitedRowsRow1Column1, Row1Column1ExpectedValue);
            ValidatePicklistSelectedText(compareSecondWFTableWithUnlimitedRowsRow1Column2, Row1Column2ExpectedValue);
            ValidateElementValue(compareSecondWFTableWithUnlimitedRowsRow2Column1, Row2Column1ExpectedValue);
            ValidatePicklistSelectedText(compareSecondWFTableWithUnlimitedRowsRow2Column2, Row2Column2ExpectedValue);

            return this;
        }

        #endregion

        #region Table PQ

        public QuestionCompareDialoguePopup WaitForTablePQCompareInformationToLoad()
        {
            WaitForElement(compareFirstTablePQQuestionName);

            WaitForElement(compareFirstTablePQQuestion1Heading);
            WaitForElement(compareFirstTablePQQuestion1SubHeading);
            WaitForElement(compareFirstTablePQQuestion1ContributionNotesHeading);
            WaitForElement(compareFirstTablePQQuestion1ContributionNotes);
            WaitForElement(compareFirstTablePQQuestion1RoleHeading);
            WaitForElement(compareFirstTablePQQuestion1Role);

            WaitForElement(compareFirstTablePQQuestion2Heading);
            WaitForElement(compareFirstTablePQQuestion2SubHeading);
            WaitForElement(compareFirstTablePQQuestion2ContributionNotesHeading);
            WaitForElement(compareFirstTablePQQuestion2ContributionNotes);
            WaitForElement(compareFirstTablePQQuestion2RoleHeading);
            WaitForElement(compareFirstTablePQQuestion2Role);
            
            return this;
        }

        public QuestionCompareDialoguePopup ValidateTablePQCompareFirstInputValue(string Question1ContributionNotesExpectedText, string Question1RoleExpectedText, string Question2ContributionNotesExpectedText, string Question2RoleExpectedText)
        {
            ValidateElementValue(compareFirstTablePQQuestion1ContributionNotes, Question1ContributionNotesExpectedText);
            ValidateElementValue(compareFirstTablePQQuestion1Role, Question1RoleExpectedText);
            ValidateElementValue(compareFirstTablePQQuestion2ContributionNotes, Question2ContributionNotesExpectedText);
            ValidateElementValue(compareFirstTablePQQuestion2Role, Question2RoleExpectedText);


            return this;
        }

        public QuestionCompareDialoguePopup ValidateTablePQCompareSecondInputValue(string Question1ContributionNotesExpectedText, string Question1RoleExpectedText, string Question2ContributionNotesExpectedText, string Question2RoleExpectedText)
        {
            ValidateElementValue(compareSecondTablePQQuestion1ContributionNotes, Question1ContributionNotesExpectedText);
            ValidateElementValue(compareSecondTablePQQuestion1Role, Question1RoleExpectedText);
            ValidateElementValue(compareSecondTablePQQuestion2ContributionNotes, Question2ContributionNotesExpectedText);
            ValidateElementValue(compareSecondTablePQQuestion2Role, Question2RoleExpectedText);

            return this;
        }

        #endregion

        #region Test QPC

        public QuestionCompareDialoguePopup WaitForTestQPCCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstTestQPCQuestionName);

            WaitForElementVisible(compareFirstTestQPCOutcomeHeader);
            WaitForElementVisible(compareFirstTestQPCTypeOfInvolvementHeader);
            WaitForElementVisible(compareFirstTestQPCWFTimeHeader);
            WaitForElementVisible(compareFirstTestQPCWhoHeader);
            WaitForElementVisible(compareFirstTestQPCOutcome);
            WaitForElementVisible(compareFirstTestQPCTypeOfInvolvement);
            WaitForElementVisible(compareFirstTestQPCWFTime);
            WaitForElementVisible(compareFirstTestQPCWho);

            WaitForElementVisible(compareSecondTestQPCOutcomeHeader);
            WaitForElementVisible(compareSecondTestQPCTypeOfInvolvementHeader);
            WaitForElementVisible(compareSecondTestQPCWFTimeHeader);
            WaitForElementVisible(compareSecondTestQPCWhoHeader);
            WaitForElementVisible(compareSecondTestQPCOutcome);
            WaitForElementVisible(compareSecondTestQPCTypeOfInvolvement);
            WaitForElementVisible(compareSecondTestQPCWFTime);
            WaitForElementVisible(compareSecondTestQPCWho);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateTestQPCCompareFirstInputValue(string OutcomeExpectedValue, string TypeOfInvolvementExpectedValue, string WFTimeExpectedValue, string WhoExpectedValue)
        {
            ValidateElementValue(compareFirstTestQPCOutcome, OutcomeExpectedValue);
            ValidateElementValue(compareFirstTestQPCTypeOfInvolvement, TypeOfInvolvementExpectedValue);
            ValidateElementValue(compareFirstTestQPCWFTime, WFTimeExpectedValue);
            ValidateElementValue(compareFirstTestQPCWho, WhoExpectedValue);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateTestQPCCompareSecondInputValue(string OutcomeExpectedValue, string TypeOfInvolvementExpectedValue, string WFTimeExpectedValue, string WhoExpectedValue)
        {
            ValidateElementValue(compareSecondTestQPCOutcome, OutcomeExpectedValue);
            ValidateElementValue(compareSecondTestQPCTypeOfInvolvement, TypeOfInvolvementExpectedValue);
            ValidateElementValue(compareSecondTestQPCWFTime, WFTimeExpectedValue);
            ValidateElementValue(compareSecondTestQPCWho, WhoExpectedValue);

            return this;
        }

        #endregion

        #region WF Boolean


        public QuestionCompareDialoguePopup WaitForWFBooleanCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFBooleanQuestionName);

            WaitForElementVisible(compareFirstWFBooleanYesLabel);
            WaitForElementVisible(compareFirstWFBooleanNoLabel);

            WaitForElementVisible(compareSecondWFBooleanQuestionName);

            WaitForElementVisible(compareSecondWFBooleanYesLabel);
            WaitForElementVisible(compareSecondWFBooleanNoLabel);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFBooleanCompareFirstYesChecked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFBooleanYesCheckBox);
            else
                ValidateElementNotChecked(compareFirstWFBooleanYesCheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFBooleanCompareFirstNoChecked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareFirstWFBooleanNoCheckBox);
            else
                ValidateElementNotChecked(compareFirstWFBooleanNoCheckBox);

            return this;
        }
        


        public QuestionCompareDialoguePopup ValidateWFBooleanCompareSecondYesChecked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFBooleanYesCheckBox);
            else
                ValidateElementNotChecked(compareSecondWFBooleanYesCheckBox);

            return this;
        }

        public QuestionCompareDialoguePopup ValidateWFBooleanCompareSecondNoChecked(bool ExpectCheked)
        {
            if (ExpectCheked)
                ValidateElementChecked(compareSecondWFBooleanNoCheckBox);
            else
                ValidateElementNotChecked(compareSecondWFBooleanNoCheckBox);

            return this;
        }


        #endregion

        #region WF Time


        public QuestionCompareDialoguePopup WaitForWFTimeCompareInformationToLoad()
        {
            WaitForElementVisible(compareFirstWFTimeQuestionName);
            WaitForElementVisible(compareFirstWFTimeInput);
            WaitForElementVisible(compareSecondWFTimeQuestionName);
            WaitForElementVisible(compareSecondWFTimeInput);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFTimeCompareFirstInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareFirstWFTimeInput, ExpectedValue);

            return this;
        }


        public QuestionCompareDialoguePopup ValidateWFTimeCompareSecondInputValue(string ExpectedValue)
        {
            ValidateElementValue(compareSecondWFTimeInput, ExpectedValue);

            return this;
        }

        #endregion
    }
}

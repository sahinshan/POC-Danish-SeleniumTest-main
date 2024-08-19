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
    public class AutomatedUITestDocument1EditAssessmentPage : CommonMethods
    {
        public AutomatedUITestDocument1EditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Automated UI Test Document 1']");



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

        readonly By leftMenuSection1Link = By.XPath("//a[@id='NL_QA-DS-66']");
        readonly By leftMenuSection1_1Link = By.XPath("//a[@id='NL_QA-DS-68']");
        readonly By leftMenuSection1_2Link = By.XPath("//a[@id='NL_QA-DS-67']");

        readonly By leftMenuSection2Link = By.XPath("//a[@id='NL_QA-DS-69']");
        readonly By leftMenuSection2_1Link = By.XPath("//a[@id='NL_QA-DS-70']");

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

        By dinamicDialogTitle(string ExpectedTitle) => By.XPath("//div[@id='CWDynamicDialog']/header/div/h1[text()='" + ExpectedTitle + "']");

        By dinamicDialogMessage(string ExpectedMessage) => By.XPath("//div[@id='CWDynamicDialog']/main/div[text()='" + ExpectedMessage + "']");

        readonly By dinamicDialogOKButton = By.XPath("//div[@id='CWDynamicDialog']/footer/input[@type='button'][@value='OK']");

        #endregion

        #region Section 1

        readonly By section1TitleLink = By.XPath("//a[@id='CWSH_QA-DS-66']");
        readonly By section1Title = By.XPath("//a[@id='CWSH_QA-DS-66']/h2[text()='Section 1']");
        readonly By section1Expanded = By.XPath("//a[@id='CWSH_QA-DS-66'][@class='section-heading level1 open']/h2[text()='Section 1']");
        readonly By section1Collapsed = By.XPath("//a[@id='CWSH_QA-DS-66'][@class='section-heading level1 closed']/h2[text()='Section 1']");
        readonly By section1MenuButton = By.XPath("//div[@id='CWS_QA-DS-66']/fieldset/div/span/a[@title='Click to open']");
        readonly By section1SectionCompletedMessage= By.XPath("//*[@id='CWInnerSection_QA-DS-66']/div/p[text()='This section has been marked as completed.']");



        #region Question Labels

        By GuiPersonDOBQuestionLabelFindByText(string ExpectedText) => By.XPath("//ul[@id='QA-DSQ-198']/li/label[text()='" + ExpectedText + "']");
        readonly By GuiPersonDOBQuestionLabel = By.XPath("//ul[@id='QA-DSQ-198']/li/label[text()='Gui-PersonDOB']");
        readonly By IGACMSFieldWriteBackQuestionLabel = By.XPath("//ul[@id='QA-DSQ-199']/li/label[text()='IGA CMS Field (Write Back) Test 1 [CaseForm ReviewDate]']");

        readonly By IGACMSListEditableQuestionLabel = By.XPath("//ul[@id='QA-DSQ-200']/li/label[text()='IGA CMS List Editable Test 1']");
        readonly By IGACMSListEditableQuestionLabelHeader1 = By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/thead/tr/th/span[text()='Related Person']");
        readonly By IGACMSListEditableQuestionLabelHeader2 = By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/thead/tr/th/span[text()='Inside Household']");
        readonly By IGACMSListEditableQuestionLabelHeader3 = By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/thead/tr/th/span[text()='Relationship with Current Person']");
        readonly By IGACMSListEditableQuestionLabelHeader4 = By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/thead/tr/th/span[text()='Start Date']");
        readonly By IGACMSListEditableQuestionLabelHeader5 = By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/thead/tr/th/span[text()='End Date']");

        readonly By GuiClientRelationshipListQuestionLabel = By.XPath("//ul[@id='QA-DSQ-201']/li/label[text()='Gui-ClientRelationshipList']");
        readonly By GuiClientRelationshipListQuestionLabelHeader1 = By.XPath("//li[@id='CWList_QA-DQ-236']/table/thead/tr/th/span[text()='Related Person']");
        readonly By GuiClientRelationshipListQuestionLabelHeader2 = By.XPath("//li[@id='CWList_QA-DQ-236']/table/thead/tr/th/span[text()='Inside Household']");
        readonly By GuiClientRelationshipListQuestionLabelHeader3 = By.XPath("//li[@id='CWList_QA-DQ-236']/table/thead/tr/th/span[text()='Relationship with Current Person']");
        readonly By GuiClientRelationshipListQuestionLabelHeader4 = By.XPath("//li[@id='CWList_QA-DQ-236']/table/thead/tr/th/span[text()='Start Date']");
        readonly By GuiClientRelationshipListQuestionLabelHeader5 = By.XPath("//li[@id='CWList_QA-DQ-236']/table/thead/tr/th/span[text()='End Date']");

        readonly By CMSListWithRelatedDataQuestionLabel = By.XPath("//ul[@id='QA-DSQ-202']/li/label[text()='IGA CMS List with Related Data Test 1']");
        By CMSListWithRelatedDataQuestionLabelHeader1(string RelationshipID) => By.XPath("//li[@id='CWList_QA-DSQ-202']/ul/li/table/tbody/tr/td/ul/li[@cwfor='QA-DQ-238_" + RelationshipID + "']/label[text()='IGA Person Relationship Related Person']");
        By CMSListWithRelatedDataQuestionLabelHeader2(string RelationshipID) => By.XPath("//li[@id='CWList_QA-DSQ-202']/ul/li/table/tbody/tr/td/ul/li[@cwfor='QA-DQ-239_" + RelationshipID + "']/label[text()='IGA Person Relationship Relationship Type']");
        By CMSListWithRelatedDataQuestionLabelHeader3(string RelationshipID) => By.XPath("//li[@id='CWList_QA-DSQ-202']/ul/li/table/tbody/tr/td/ul/li[@cwfor='QA-DQ-240_" + RelationshipID + "']/label[text()='IGA Person Relationship Start Date']");
        By CMSListWithRelatedDataQuestionLabelHeader4(string RelationshipID) => By.XPath("//li[@id='CWList_QA-DSQ-202']/ul/li/table/tbody/tr/td/ul/li[@cwfor='QA-DQ-241_" + RelationshipID + "']/label[text()='IGA Person Relationship Inside Household']");

        readonly By ImageQuestionLabel = By.XPath("//ul[@id='QA-DSQ-146']/li/label[text()='Image']");

        readonly By WFMultipleChoiceQuestionLabel = By.XPath("//ul[@id='QA-DSQ-148']/li/label[text()='WF Multiple Choice']");
        readonly By WFMultipleChoiceOption1Label = By.XPath("//ul[@id='QA-DSQ-148']/li/ul/li/label[text()='Option 1']");
        readonly By WFMultipleChoiceOption2Label = By.XPath("//ul[@id='QA-DSQ-148']/li/ul/li/label[text()='Option 2']");
        readonly By WFMultipleChoiceOption3Label = By.XPath("//ul[@id='QA-DSQ-148']/li/ul/li/label[text()='Option 3']");

        readonly By WFDecimalQuestionLabel = By.XPath("//ul[@id='QA-DSQ-143']/li/label[text()='WF Decimal']");
        By WFDecimalQuestionErrorLabel(string ExpectedText) => By.XPath("//*[@id='QA-DSQ-143']/li/label[@class='FormError']/span[text()='"  + ExpectedText + "']");
        

        readonly By WFMultipleResponseQuestionLabel = By.XPath("//ul[@id='QA-DSQ-149']/li/label[text()='WF Multiple Response']");
        readonly By WFMultipleResponseOption1 = By.XPath("//ul[@id='QA-DSQ-149']/li/ul/li/label[text()='Day 1']");
        readonly By WFMultipleResponseOption2 = By.XPath("//ul[@id='QA-DSQ-149']/li/ul/li/label[text()='Day 2']");
        readonly By WFMultipleResponseOption3 = By.XPath("//ul[@id='QA-DSQ-149']/li/ul/li/label[text()='Day 3']");

        readonly By WFNumericQuestionLabel = By.XPath("//ul[@id='QA-DSQ-150']/li/label[text()='WF Numeric']");
        readonly By WFLookupQuestionLabel = By.XPath("//ul[@id='QA-DSQ-147']/li/label[text()='WF Lookup']");
        readonly By WFDateQuestionLabel = By.XPath("//ul[@id='QA-DSQ-142']/li/label[text()='WF Date']");

        #endregion

        #region Questions

        readonly By GuiPersonDOBQuestion = By.XPath("//a[contains(@id, 'QA-DQ-233')]");

        readonly By IGACMSFieldWriteBackQuestion = By.XPath("//input[@id='QA-DQ-234']");


        readonly By IGACMSListEditableNewButton = By.XPath("//div[@id='CWButtons_QA-DQ-235']/ul/li/a");
        By IGACMSListEditableAnswerRow(string PersonRelationshipID) => By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/tbody/tr[contains(@onclick,'" + PersonRelationshipID + "')]");
        By IGACMSListEditableAnswerColumn1(string PersonRelationshipID, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/tbody/tr[contains(@onclick,'" + PersonRelationshipID + "')]/td[1]/a/span/span[text()='" + ColumnText + "']");
        By IGACMSListEditableAnswerColumn2(string PersonRelationshipID, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/tbody/tr[contains(@onclick,'" + PersonRelationshipID + "')]/td[2]/span[text()='" + ColumnText + "']");
        By IGACMSListEditableAnswerColumn3(string PersonRelationshipID, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/tbody/tr[contains(@onclick,'" + PersonRelationshipID + "')]/td[3]/span[text()='" + ColumnText + "']");
        By IGACMSListEditableAnswerColumn4(string PersonRelationshipID, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-235']/div/table/tbody/tr[contains(@onclick,'" + PersonRelationshipID + "')]/td[4]/span[text()='" + ColumnText + "']");


        By GuiClientRelationshipListAnswerColumn1(int Row, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-236']/table/tbody/tr[" + Row.ToString() + "]/td[1]/span[text()='" + ColumnText + "']");
        By GuiClientRelationshipListAnswerColumn2(int Row, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-236']/table/tbody/tr[" + Row.ToString() + "]/td[2]/span[text()='" + ColumnText + "']");
        By GuiClientRelationshipListAnswerColumn3(int Row, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-236']/table/tbody/tr[" + Row.ToString() + "]/td[3]/span[text()='" + ColumnText + "']");
        By GuiClientRelationshipListAnswerColumn4(int Row, string ColumnText) => By.XPath("//li[@id='CWList_QA-DQ-236']/table/tbody/tr[" + Row.ToString() + "]/td[4]/span[text()='" + ColumnText + "']");


        By CMSListWithRelatedDataEditButton(string PersonRelationshipID) => By.XPath("//li[@id='CWList_QA-DSQ-202']/ul/li/span/a[contains(@onclick, '" + PersonRelationshipID + "')]");
        By CMSListWithRelatedDataRelatedPersonAnswer(string PersonRelationshipID, string ColumnText) => By.XPath("//label[@id='QA-DQ-238_" + PersonRelationshipID + "'][text()='" + ColumnText + "']");
        By CMSListWithRelatedDataRelationshipAnswer(string PersonRelationshipID, string ColumnText) => By.XPath("//label[@id='QA-DQ-239_" + PersonRelationshipID + "'][text()='" + ColumnText + "']");
        By CMSListWithRelatedDataStartDateAnswer(string PersonRelationshipID, string ColumnText) => By.XPath("//label[@id='QA-DQ-240_" + PersonRelationshipID + "'][text()='" + ColumnText + "']");
        By CMSListWithRelatedDataInsideHouseholdAnswer(string PersonRelationshipID, string ColumnText) => By.XPath("//label[@id='QA-DQ-241_" + PersonRelationshipID + "'][text()='" + ColumnText + "']");


        readonly By ImageQuestionEditButton = By.XPath("//a[@id='QA-DQ-167_Upload']");
        readonly By ImageQuestionSelectFileControl = By.XPath("//input[@id='QA-DQ-167_FileControl']");
        readonly By ImageQuestionUploadButton = By.XPath("//input[@id='QA-DQ-167_UploadButton']");
        readonly By ImageQuestionImageFile = By.XPath("//img[@id='QA-DQ-167_ImageControl']");



        readonly By WFMultipleChoiceRadioButton1 = By.XPath("//input[@value='E92F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton2 = By.XPath("//input[@value='F12F3C2D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleChoiceRadioButton3 = By.XPath("//input[@value='4A306139-3F52-E911-A2C5-005056926FE4']");


        readonly By WFDecimalQuestion = By.XPath("//input[@id='QA-DQ-164']");

        readonly By WFMultipleResponseCheckbox1 = By.XPath("//input[@value='FF47D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox2 = By.XPath("//input[@value='0748D74D-3F52-E911-A2C5-005056926FE4']");
        readonly By WFMultipleResponseCheckbox3 = By.XPath("//input[@value='0F48D74D-3F52-E911-A2C5-005056926FE4']");

        readonly By WFNumericQuestion = By.XPath("//input[@id='QA-DQ-171']");

        readonly By WFLookupAnswer = By.XPath("//a[@id='QA-DQ-168_LookupLink']");
        readonly By WFLookupClearbutton = By.XPath("//div[@id='QA-DQ-168']/div/div/button[@title='Clear QA-DQ-168']");
        readonly By WFLookupSearchButton = By.XPath("//div[@id='QA-DQ-168']/div/div/button[@title='Lookup QA-DQ-168']");

        readonly By WFDateQuestion = By.XPath("//input[@id='QA-DQ-163']");

        #endregion

        #region Questions Menu

        readonly By section1ImageQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-146']/li/span/a[@title='Click to open']");
        readonly By section1ImageQuestionQuestionInformationLink = By.XPath("//*[@id='QM_QA-DSQ-146']/li/a[contains(@onclick, 'CWD.SQI')]");
        readonly By section1ImageQuestionCommentsLink = By.XPath("//*[@id='QM_QA-DSQ-146']/li/a[contains(@onclick, 'CWD.SQC')]");
        

        readonly By section1WFMultipleChoiceQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-148']/li/span/a[@title='Click to open']");
        readonly By section1WFMultipleChoiceQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-148']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFMultipleChoiceQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-148']/li/a[contains(@onclick, 'CWD.SQCompare')]");
        

        readonly By section1WFDecimalQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-143']/li/span/a[@title='Click to open']");
        readonly By section1WFDecimalQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-143']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFDecimalQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-143']/li/a[contains(@onclick, 'CWD.SQCompare')]");


        readonly By section1WFMultipleResponseQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-149']/li/span/a[@title='Click to open']");
        readonly By section1WFMultipleResponseQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-149']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFMultipleResponseQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-149']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section1WFNumericQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-150']/li/span/a[@title='Click to open']");
        readonly By section1WFNumericQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-150']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFNumericQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-150']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section1WFLookupQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-147']/li/span/a[@title='Click to open']");
        readonly By section1WFLookupQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-147']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFLookupQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-147']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section1WFDateQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-142']/li/span/a[@title='Click to open']");
        readonly By section1WFDateQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-142']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1WFDateQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-142']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        #endregion

        #region Menu

        readonly By section1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-66']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1PrintButton = By.XPath("//ul[@id='SM_QA-DS-66']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1PrintHistoryButton = By.XPath("//ul[@id='SM_QA-DS-66']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-66']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-66']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion

        #region Section 1.1

        By section1_1ByText(string ExpectedText) => By.XPath("//a[@id='CWSH_QA-DS-68']/h3[text()='" + ExpectedText + "']");

        readonly By section1_1TitleLink = By.Id("CWSH_QA-DS-68");
        readonly By section1_1Title = By.XPath("//a[@id='CWSH_QA-DS-68']/h3[text()='Section 1.1']");
        readonly By section1_1Expanded = By.XPath("//a[@id='CWSH_QA-DS-68'][@class='subsection-heading level2 open']/h3[text()='Section 1.1']");
        readonly By section1_1Collapsed = By.XPath("//a[@id='CWSH_QA-DS-68'][@class='subsection-heading level2 closed']/h3[text()='Section 1.1']");
        readonly By section1_1MenuButton = By.XPath("//div[@id='CWS_QA-DS-68']/fieldset/div/span/a[@title='Click to open']");
        readonly By section1_1SectionCompletedMessage = By.XPath("//*[@id='CWInnerSection_QA-DS-68']/div/p[text()='This section has been marked as completed.']");

        #region Question Labels

        readonly By WFParagraphQuestionLabel = By.XPath("//ul[@id='QA-DSQ-151']/li/label[text()='WF Paragraph']");

        #endregion

        #region Questions


        readonly By Section1_1WFParagraphQuestionArea = By.XPath("//*[@id='QA-DSQ-151']");

        readonly By WFParagraphQuestion = By.XPath("//textarea[@id='QA-DQ-172']");

        #endregion

        #region Questions Menu

        readonly By section1_1WFParagraphQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-151']/li[1]/span/a[@title='Click to open']");
        readonly By section1_1WFParagraphQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-151']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1_1WFParagraphQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-151']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        #endregion

        #region Menu

        readonly By section1_1SectionInformationButton = By.XPath("//ul[@id='SM_QA-DS-68']/li/a[contains(@onclick, 'CWD.ShowSectionInfo')]");
        readonly By section1_1PrintButton = By.XPath("//ul[@id='SM_QA-DS-68']/li/a[contains(@onclick, 'CWD.PrintSection')]");
        readonly By section1_1PrinthistoryButton = By.XPath("//ul[@id='SM_QA-DS-68']/li/a[contains(@onclick, 'CWD.SectionPrintRecords')]");
        readonly By section1_1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-68']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");
        readonly By section1_1CompleteSectionButton = By.XPath("//ul[@id='SM_QA-DS-68']/li/a[contains(@onclick, 'CWD.CompleteSection')]");

        #endregion

        #endregion

        #region Section 1.2

        readonly By section1_2TitleLink = By.Id("CWSH_QA-DS-67");
        readonly By section1_2Title = By.XPath("//a[@id='CWSH_QA-DS-67']/h3[text()='Section 1.2']");
        readonly By section1_2Expanded = By.XPath("//a[@id='CWSH_QA-DS-67'][@class='subsection-heading level2 open']/h3[text()='Section 1.2']");
        readonly By section1_2Collapsed = By.XPath("//a[@id='CWSH_QA-DS-67'][@class='subsection-heading level2 closed']/h3[text()='Section 1.2']");
        readonly By section1_2MenuButton = By.XPath("//div[@id='CWS_QA-DS-67']/fieldset/div/span/a[@title='Click to open']");

        #region Question Labels

        readonly By WFPickListQuestionLabel = By.XPath("//ul[@id='QA-DSQ-152']/li/label[text()='WF PickList']");


        #endregion

        #region Questions

        readonly By WFPickListQuestion = By.XPath("//select[@id='QA-DQ-173']");

        #endregion

        #region Questions Menu

        readonly By section1_2WFPicklistQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-152']/li[1]/span/a[@title='Click to open']");
        readonly By section1_2WFPicklistQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-152']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section1_2WFPicklistQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-152']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        #endregion

        #endregion

        #region Section 2

        readonly By section2TitleLink = By.Id("CWSH_QA-DS-69");
        readonly By section2Title = By.XPath("//a[@id='CWSH_QA-DS-69']/h2[text()='Section 2']");
        readonly By section2Expanded = By.XPath("//a[@id='CWSH_QA-DS-69'][@class='section-heading level1 open']/h2[text()='Section 2']");
        readonly By section2Collapsed = By.XPath("//a[@id='CWSH_QA-DS-69'][@class='section-heading level1 closed']/h2[text()='Section 2']");
        readonly By section2MenuButton = By.XPath("//div[@id='CWS_QA-DS-69']/fieldset/div/span/a[@title='Click to open']");
        readonly By section2SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-69']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");


        #region Question Labels

        readonly By WFShortAnswerQuestionLabel = By.XPath("//ul[@id='QA-DSQ-153']/li/label[text()='WF Short Answer']");

        #endregion

        #region Questions

        readonly By WFShortAnswerQuestion = By.XPath("//input[@id='QA-DQ-174']");

        #endregion

        #region Questions Menu

        readonly By section2WFShortAnswerQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-153']/li[1]/span/a[@title='Click to open']");
        readonly By section2WFShortAnswerQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-153']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2WFShortAnswerQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-153']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        #endregion

        #endregion

        #region Section 2.1

        readonly By section2_1TitleLink = By.Id("CWSH_QA-DS-70");
        readonly By section2_1Title = By.XPath("//a[@id='CWSH_QA-DS-70']/h3");
        readonly By section2_1Expanded = By.XPath("//a[@id='CWSH_QA-DS-70'][@class='subsection-heading level2 open']/h3");
        readonly By section2_1Collapsed = By.XPath("//a[@id='CWSH_QA-DS-70'][@class='subsection-heading level2 closed']/h3");
        readonly By section2_1MenuButton = By.XPath("//div[@id='CWS_QA-DS-70']/fieldset/div/span/a[@title='Click to open']");
        readonly By section2_1SpellCheckButton = By.XPath("//ul[@id='SM_QA-DS-70']/li/a[contains(@onclick, 'CWD.SpellCheckSection')]");

        #region Question Labels

        readonly By AuthorisationQuestionLabel = By.XPath("//ul[@id='QA-DSQ-154']/li/label[text()='Authorisation']");

        readonly By TestHQQuestionLabel = By.XPath("//ul[@id='QA-DSQ-155']/li/ul/li/label[text()='Test HQ']");
        readonly By TestHQQuestionRow1Header = By.XPath("//ul[@id='QA-DSQ-155']/li/ul/li/div/table/thead/tr/th/span[text()='Location']");
        readonly By TestHQQuestionRow2Header = By.XPath("//ul[@id='QA-DSQ-155']/li/ul/li/div/table/thead/tr/th/span[text()='Test Dec']");

        readonly By TableWithUnlimtedRowsQuestionLabel = By.XPath("//*[@id='QA-DSQ-204']/li/ul/li/label[text()='WF Table With Unlimited Rows']");
        readonly By TableWithUnlimtedRowsQuestionSubHeading = By.XPath("//ul[@id='QA-DSQ-204']/li/ul/li[text()='WF Unlimited Rows Table Sub Heading']");
        readonly By TableWithUnlimtedRowsQuestionHeader1 = By.XPath("//ul[@id='QA-DSQ-204']/li/ul/li/div/table/thead/tr/th[1]/span[text()='Date became involved']");
        readonly By TableWithUnlimtedRowsQuestionHeader2 = By.XPath("//ul[@id='QA-DSQ-204']/li/ul/li/div/table/thead/tr/th[2]/span[text()='Reason for Assessment']");

        readonly By TablePQQuestionLabel = By.XPath("//ul[@id='QA-DSQ-157']/li/ul/li/label[text()='Table PQ']");
        readonly By TablePQQuistion1Heading = By.XPath("//ul[@id='QA-DSQ-157']/li/ul/li/label[text()='Question 1']");
        readonly By TablePQQuistion1SubHeading = By.XPath("//*[@id='QA-DSQ-157']/li/ul/li[text()='Question 1 - Sub Heading']");
        readonly By TablePQQuistion1ContributionNotesLabel = By.XPath("//*[@id='QA-DSQ-157']/li[3]/ul/li[3]/label[text()='Contribution Notes']");
        readonly By TablePQQuistion1RlesLabel = By.XPath("//*[@id='QA-DSQ-157']/li[3]/ul/li[5]/label[text()='Role']");
        readonly By TablePQQuistion2Heading = By.XPath("//ul[@id='QA-DSQ-157']/li/ul/li/label[text()='Question 2']");
        readonly By TablePQQuistion2SubHeading = By.XPath("//*[@id='QA-DSQ-157']/li/ul/li[text()='Question 2 - Sub Heading']");
        readonly By TablePQQuistion2ContributionNotesLabel = By.XPath("//*[@id='QA-DSQ-157']/li[4]/ul/li[3]/label[text()='Contribution Notes']");
        readonly By TablePQQuistion2RlesLabel = By.XPath("//*[@id='QA-DSQ-157']/li[4]/ul/li[5]/label[text()='Role']");

        readonly By TestQPCQuestionLabel = By.XPath("//ul[@id='QA-DSQ-158']/li/ul/li/label[text()='Test QPC']");
        readonly By TestQPCOutcomeLabel = By.XPath("//*[@id='QA-DSQ-158']/li/ul/li/div/table/tbody/tr[1]/td[1]/ul/li/label[text()='Outcome']");
        readonly By TestQPCTypeOfInvolvementLabel = By.XPath("//*[@id='QA-DSQ-158']/li/ul/li/div/table/tbody/tr[1]/td[2]/ul/li/label[text()='Type of Involvement']");
        readonly By TestQPCWFTimeLabel = By.XPath("//*[@id='QA-DSQ-158']/li/ul/li/div/table/tbody/tr[2]/td[1]/ul/li/label[text()='WF Time']");
        readonly By TestQPCWhoLabel = By.XPath("//*[@id='QA-DSQ-158']/li/ul/li/div/table/tbody/tr[2]/td[2]/ul/li/label[text()='Who']");


        readonly By WFBooleanQuestionLabel = By.XPath("//ul[@id='QA-DSQ-159']/li/label[text()='WF Boolean']");
        readonly By WFTextQuestionLabel = By.XPath("//ul[@id='QA-DSQ-160']/li/label[text()='WF Text']");
        readonly By WFTimeQuestionLabel = By.XPath("//ul[@id='QA-DSQ-161']/li/label[text()='WF Time']");
        readonly By WFHiperlink1QuestionLabel = By.XPath("//label[@id='QA-DQ-189']/a[text()='WF Hiperlink 1']");
        readonly By WFHiperlinkGroupQuestionLabel = By.XPath("//li[@id='QA-DQ-190']/a[text()='WF Hiperlink Group']");
        readonly By WFHiperlink2QuestionLabel = By.XPath("//ul[@id='LM_QA-DQ-190']/li/a[text()='WF Hiperlink 2']");
        readonly By WFHiperlink3QuestionLabel = By.XPath("//ul[@id='LM_QA-DQ-190']/li/a[text()='WF Hiperlink 3']");
        readonly By drawQuestionLabel = By.XPath("//*[@id='QA-DSQ-1011']/li[2]/label[text()='WF Drawing Canvas']");

        #endregion

        #region Questions

        readonly By AuthorisationEditSignatureEditButton = By.XPath("//a[@id='QA-DQ-175_EditSignature']");
        readonly By AuthorisationEditSignatureDeleteButton = By.XPath("//a[@id='QA-DQ-175_DeleteSignature']");

        readonly By TestHQQuestionRow1Column1 = By.XPath("//input[@id='QA-DQ-177']");
        readonly By TestHQQuestionRow1Column2 = By.XPath("//input[@id='QA-DQ-178']");
        readonly By TestHQQuestionRow2Column1 = By.XPath("//input[@id='QA-DQ-179']");
        readonly By TestHQQuestionRow2Column2 = By.XPath("//input[@id='QA-DQ-180']");

        readonly By TableWithUnlimtedRowsAddNewRowButton = By.XPath("//*[@id='QA-DSQ-204']/li[2]/ul/li/div/ul/li/a[contains(@onclick, 'CWD.AddUnlimitedRow')]");
        By TableWithUnlimtedRowsDateQuestion(int RowNumber) => By.XPath("//input[@id='" + RowNumber + "-QA-DQ-245']");
        By TableWithUnlimtedRowsPicklist(int RowNumber) => By.XPath("//*[@id='" + RowNumber + "-QA-DQ-246']");
        By TableWithUnlimtedRowsDeleteRowButton(int RowNumber) => By.XPath("//ul[@id='QA-DSQ-204']/li/ul/li/div/table/tbody/tr[" + RowNumber + "]/td/ul[contains(@onclick, 'DeleteUnlimitedRow')]");


        readonly By TablePAQuestion1ContributionNotes = By.XPath("//*[@id='QA-DQ-253']");
        readonly By TablePAQuestion1Role = By.XPath("//*[@id='QA-DQ-255']");
        readonly By TablePAQuestion2ContributionNotes = By.XPath("//*[@id='QA-DQ-254']");
        readonly By TablePAQuestion2Role = By.XPath("//*[@id='QA-DQ-256']");


        readonly By TestQPCOutcomeQuestion = By.XPath("//input[@id='QA-DQ-185']");
        readonly By TestQPCTypeOfInvolvementQuestion = By.XPath("//input[@id='QA-DQ-258']");
        readonly By TestQPCWFTimeQuestion = By.XPath("//input[@id='QA-DQ-260']");
        readonly By TestQPCWhoQuestion = By.XPath("//input[@id='QA-DQ-262']");

        readonly By WFBooleanYesOption = By.XPath("//input[@id='QA-DQ-186']");
        readonly By WFBooleanNoOption = By.XPath("//input[@id='QA-DQ-186_F']");

        readonly By WFTimeQuestion = By.XPath("//input[@id='QA-DQ-188']");
        
        readonly By drawQuestionEditDrawButton = By.XPath("//*[@id='QA-DQ-1338_EditDrawing']");
        readonly By drawQuestionDeleteDrawButton = By.XPath("//*[@id='QA-DQ-1338_DeleteDrawing']");


        #endregion

        #region Questions Menu

        readonly By section2_1WFTestHQQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-155']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFTestHQQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-155']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFTestHQQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-155']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section2_1WFTableWithUnlimitedRowsQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-204']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFTableWithUnlimitedRowsQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-204']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFTableWithUnlimitedRowsQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-204']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section2_1WFTablePQQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-157']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFTablePQQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-157']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFTablePQQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-157']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section2_1WFTestQPCQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-158']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFTestQPCQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-158']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFTestQPCQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-158']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section2_1WFBooleanQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-159']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFBooleanQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-159']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFBooleanQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-159']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        readonly By section2_1WFTimeQuestionMenuButton = By.XPath("//*[@id='QA-DSQ-161']/li[1]/span/a[@title='Click to open']");
        readonly By section2_1WFTimeQuestionAuditButton = By.XPath("//*[@id='QM_QA-DSQ-161']/li/a[contains(@onclick, 'CWD.SQA')]");
        readonly By section2_1WFTimeQuestionPreviousAnswersButton = By.XPath("//*[@id='QM_QA-DSQ-161']/li/a[contains(@onclick, 'CWD.SQCompare')]");

        #endregion

        #endregion

        #endregion


        #region Methods

        public AutomatedUITestDocument1EditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID)
        {

            #region Iframes

            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_(AssessmentID));
            SwitchToIframe(iframe_CWDialog_(AssessmentID));

            WaitForElement(iframe_CWAssessmentDialog);
            SwitchToIframe(iframe_CWAssessmentDialog);

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
            Wait.Until(c => c.FindElement(leftMenuSection1_1Link));
            Wait.Until(c => c.FindElement(leftMenuSection1_2Link));
            Wait.Until(c => c.FindElement(leftMenuSection2Link));

            Wait.Until(c => c.FindElement(leftMenuSection2_1Link));
            Wait.Until(c => c.FindElement(leftMenuSection2_1Link));

            Wait.Until(c => c.FindElement(leftMenuCollapseExpandButton));

            #endregion

            #region Section 1

            Wait.Until(c => c.FindElement(section1Title));
            Wait.Until(c => c.FindElement(section1Expanded));
            Wait.Until(c => c.FindElement(section1MenuButton));

            Wait.Until(c => c.FindElement(GuiPersonDOBQuestionLabel));
            Wait.Until(c => c.FindElement(IGACMSFieldWriteBackQuestionLabel));

            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabel));
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader1));
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader2));
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader3));
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader4));
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader5));

            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabel));
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader1));
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader2));
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader3));
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader4));
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader5));

            Wait.Until(c => c.FindElement(CMSListWithRelatedDataQuestionLabel));

            Wait.Until(c => c.FindElement(ImageQuestionLabel));

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

            #endregion

            #region Section 1.1

            Wait.Until(c => c.FindElement(section1_1Title));
            Wait.Until(c => c.FindElement(section1_1Expanded));
            Wait.Until(c => c.FindElement(section1_1MenuButton));

            Wait.Until(c => c.FindElement(WFParagraphQuestionLabel));

            #endregion

            #region Section 1.2

            Wait.Until(c => c.FindElement(section1_2Title));
            Wait.Until(c => c.FindElement(section1_2Expanded));
            Wait.Until(c => c.FindElement(section1_2MenuButton));

            Wait.Until(c => c.FindElement(WFPickListQuestionLabel));

            #endregion

            #region Section 2

            Wait.Until(c => c.FindElement(section2Title));
            Wait.Until(c => c.FindElement(section2Expanded));
            Wait.Until(c => c.FindElement(section2MenuButton));

            Wait.Until(c => c.FindElement(WFShortAnswerQuestionLabel));

            #endregion

            #region Section 2.1

            WaitForElement(section2_1Title);
            WaitForElement(section2_1Expanded);
            WaitForElement(section2_1MenuButton);

            WaitForElement(AuthorisationQuestionLabel);

            WaitForElement(TestHQQuestionLabel);
            WaitForElement(TestHQQuestionRow1Header);
            WaitForElement(TestHQQuestionRow2Header);

            WaitForElement(TableWithUnlimtedRowsQuestionLabel);
            WaitForElement(TableWithUnlimtedRowsQuestionSubHeading);
            WaitForElement(TableWithUnlimtedRowsQuestionHeader1);
            WaitForElement(TableWithUnlimtedRowsQuestionHeader2);

            WaitForElement(TablePQQuestionLabel);
            WaitForElement(TablePQQuistion1Heading);
            WaitForElement(TablePQQuistion1SubHeading);
            WaitForElement(TablePQQuistion1ContributionNotesLabel);
            WaitForElement(TablePQQuistion1RlesLabel);
            WaitForElement(TablePQQuistion2Heading);
            WaitForElement(TablePQQuistion2SubHeading);
            WaitForElement(TablePQQuistion2ContributionNotesLabel);
            WaitForElement(TablePQQuistion2RlesLabel);

            WaitForElement(TestQPCQuestionLabel);
            WaitForElement(TestQPCOutcomeLabel);
            WaitForElement(TestQPCTypeOfInvolvementLabel);
            WaitForElement(TestQPCWFTimeLabel);
            WaitForElement(TestQPCWhoLabel);

            WaitForElement(WFBooleanQuestionLabel);
            WaitForElement(WFTextQuestionLabel);
            WaitForElement(WFTimeQuestionLabel);
            WaitForElement(WFHiperlink1QuestionLabel);
            WaitForElement(WFHiperlinkGroupQuestionLabel);
            WaitForElement(WFHiperlink2QuestionLabel);
            WaitForElement(WFHiperlink3QuestionLabel);
            //WaitForElement(drawQuestionLabel);


            #endregion

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public AutomatedUITestDocument1EditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
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

            WaitForElementNotVisible("CWRefreshPanel", 17);

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public AutomatedUITestDocument1EditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
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

        public AutomatedUITestDocument1EditAssessmentPage TapCollapseExpandLeftMenuButton()
        {
            Click(leftMenuCollapseExpandButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapLeftMenuSection1()
        {
            Click(leftMenuSection1Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section1TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapLeftMenuSection1_1()
        {

            Click(leftMenuSection1_1Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section1_1TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapLeftMenuSection1_2()
        {
            Click(leftMenuSection1_2Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section1_2TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapLeftMenuSection2()
        {
            Click(leftMenuSection2Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section2TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapLeftMenuSection2_1()
        {
            Click(leftMenuSection2_1Link);

            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(section2_1TitleLink);

            return this;
        }



        public AutomatedUITestDocument1EditAssessmentPage WaitForLeftMenuVisible()
        {
            #region Left Menu

            Wait.Until(c => c.FindElement(leftMenuSection1Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection1_1Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection1_2Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection2Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection2_1Link).Displayed);
            Wait.Until(c => c.FindElement(leftMenuSection2_1Link).Displayed);

            #endregion

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForLeftMenuHidden()
        {
            #region Left Menu

            Wait.Until(c => c.FindElement(leftMenuSection1Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection1_1Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection1_2Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection2Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection2_1Link).Displayed == false);
            Wait.Until(c => c.FindElement(leftMenuSection2_1Link).Displayed == false);

            #endregion

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForLeftMenuSection1_1Hiden()
        {
            #region Left Menu
            
            Wait.Until(c => c.FindElement(leftMenuSection1_1Link).Displayed == false);

            #endregion

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateLeftMenuSection1_1Text(string ExpectedText)
        {
            ValidateElementText(leftMenuSection1_1Link, ExpectedText);
         
            return this;
        }

        #endregion

        #region Spelling Bar

        public AutomatedUITestDocument1EditAssessmentPage WaitForSpellingBarToBeDisplayed()
        {
            WaitForElementVisible(spellingBarHeader);
            WaitForElementVisible(spellingBarCloseButton);

            WaitForElementVisible(spellingBarNotFoundInDictionaryLabel);

            WaitForElementVisible(spellingBarSuggestionsLabel);

            WaitForElementVisible(spellingBarSuggestBox);

            WaitForElement(spellingBarChangeButton);
            WaitForElementVisible(spellingBarIgnoreButton);

            WaitForElementVisible(spellingBarLanguagePicklist);
            

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSpellingBarToBeClosed()
        {
            WaitForElementNotVisible(spellingBarHeader, 3);
            WaitForElementNotVisible(spellingBarCloseButton, 3);

            WaitForElementNotVisible(spellingBarNotFoundInDictionaryLabel, 3);

            WaitForElementNotVisible(spellingBarSuggestionsLabel, 3);

            WaitForElementNotVisible(spellingBarSuggestBox, 3);

            WaitForElementNotVisible(spellingBarChangeButton, 3);
            WaitForElementNotVisible(spellingBarIgnoreButton, 3);

            WaitForElementNotVisible(spellingBarLanguagePicklist, 3);


            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarTapCloseButton()
        {
            Click(spellingBarCloseButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarWaitForHighlightedIncorrectWord(string HighlightedIncorrectWord)
        {
            WaitForElementVisible(spellingBarHighlightedIncorrectWord(HighlightedIncorrectWord));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarWaitForSuggestedWord(string SuggestedWord)
        {
            WaitForElementVisible(spellingBarSuggestedWord(SuggestedWord));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarClickSuggestedWord(string SuggestedWord)
        {
            Click(spellingBarSuggestedWord(SuggestedWord));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarWaitForSuggestedWordSelected(string SuggestedWord)
        {
            WaitForElementVisible(spellingBarSuggestedWordSelected(SuggestedWord));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarClickChangeButton()
        {
            Click(spellingBarChangeButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarClickChangeAllButton()
        {
            Click(spellingBarChangeAllButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarClickIgnoreButton()
        {
            Click(spellingBarIgnoreButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarClickIgnoreAllButton()
        {
            Click(spellingBarIgnoreAllButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarSelectLanguageByValue(string LanguageValue)
        {
            SelectPicklistElementByValue(spellingBarLanguagePicklist, LanguageValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarValidateChangeAllButtonNotVisible()
        {
            WaitForElementNotVisible(spellingBarChangeAllButton, 3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SpellingBarValidateIgnoreAllButtonNotVisible()
        {
            WaitForElementNotVisible(spellingBarChangeAllButton, 3);

            return this;
        }

        #endregion

        #region Section 1

        public AutomatedUITestDocument1EditAssessmentPage TapSection1MenuButton()
        {
            Click(section1MenuButton);

            return this;
        }

        public SectionInformationDialoguePopup TapSection1SectionInformationButton()
        {
            WaitForElementToBeClickable(section1SectionInformationButton);
            Click(section1SectionInformationButton);

            return new SectionInformationDialoguePopup(driver, Wait, appURL);
        }

        public PrintAssessmentPopup TapSection1PrintButton()
        {
            WaitForElementToBeClickable(section1PrintButton);
            System.Threading.Thread.Sleep(500);
            Click(section1PrintButton);

            return new PrintAssessmentPopup(driver, Wait, appURL);
        }

        public PrintAssessmentPopup TapSection1PrintHistoryButton()
        {
            WaitForElementToBeClickable(section1PrintHistoryButton);
            Click(section1PrintHistoryButton);

            return new PrintAssessmentPopup(driver, Wait, appURL);
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection1SpellCheckButton()
        {
            WaitForElementToBeClickable(section1SpellCheckButton);
            Click(section1SpellCheckButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection1TitleArea()
        {
            Click(section1TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1Visible()
        {
            Wait.Until(c => c.FindElement(GuiPersonDOBQuestionLabel).Displayed);
            Wait.Until(c => c.FindElement(IGACMSFieldWriteBackQuestionLabel).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabel).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader1).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader1).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader2).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader3).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader4).Displayed);
            Wait.Until(c => c.FindElement(IGACMSListEditableQuestionLabelHeader5).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabel).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader1).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader2).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader3).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader4).Displayed);
            Wait.Until(c => c.FindElement(GuiClientRelationshipListQuestionLabelHeader5).Displayed);

            Wait.Until(c => c.FindElement(section1Expanded).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1Hidden()
        {
            Wait.Until(c => !c.FindElement(GuiPersonDOBQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSFieldWriteBackQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader1).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader1).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader2).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader3).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader4).Displayed);
            Wait.Until(c => !c.FindElement(IGACMSListEditableQuestionLabelHeader5).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabelHeader1).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabelHeader2).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabelHeader3).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabelHeader4).Displayed);
            Wait.Until(c => !c.FindElement(GuiClientRelationshipListQuestionLabelHeader5).Displayed);

            Wait.Until(c => c.FindElement(section1Collapsed).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1MenuItemsVisible()
        {
            Wait.Until(c => c.FindElement(section1SectionInformationButton).Displayed);
            Wait.Until(c => c.FindElement(section1PrintButton).Displayed);
            Wait.Until(c => c.FindElement(section1PrintHistoryButton).Displayed);
            Wait.Until(c => c.FindElement(section1SpellCheckButton).Displayed);
            Wait.Until(c => c.FindElement(section1CompleteSectionButton).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection1CompleteSectionButton()
        {
            WaitForElementToBeClickable(section1CompleteSectionButton);
            System.Threading.Thread.Sleep(500);
            Click(section1CompleteSectionButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1SectionCompletedMessageDisplayed()
        {
            WaitForElementVisible(section1SectionCompletedMessage);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1SectionCompletedMessageNotDisplayed()
        {
            WaitForElementNotVisible(section1SectionCompletedMessage, 3);

            return this;
        }



        #region Questions

        public AutomatedUITestDocument1EditAssessmentPage WaitForGuiPersonDOBQuestionNotVisible()
        {
            WaitForElementNotVisible(GuiPersonDOBQuestionLabel, 3);
            WaitForElementNotVisible(GuiPersonDOBQuestion, 3);
            
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateGuiPersonDOBQuestionLabelQuestionText(string ExpectedQuestionText)
        {
            WaitForElement(GuiPersonDOBQuestionLabelFindByText(ExpectedQuestionText));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateGuiPersonDOBQuestionText(string ExpectedText)
        {
            ValidateElementText(GuiPersonDOBQuestion, ExpectedText);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ClickGuiPersonDOBQuestion()
        {
            Click(GuiPersonDOBQuestion);

            return this;
        }

        //

        public AutomatedUITestDocument1EditAssessmentPage WaitForWFDecimalErrorLabelVisible(string ExpectedMessage)
        {
            WaitForElementVisible(WFDecimalQuestionErrorLabel(ExpectedMessage));

            return this;
        }



        public AutomatedUITestDocument1EditAssessmentPage InsertIGACMSFieldWriteBackTest1Value(string ValueToInsert)
        {
            SendKeys(IGACMSFieldWriteBackQuestion, ValueToInsert);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SelectWFMultipleChoice(int OptionToSelect)
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

        public AutomatedUITestDocument1EditAssessmentPage InsertWFDecimalValue(string ValueToInsert)
        {
            SendKeys(WFDecimalQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SelectWFMultipleResponse(int OptionToSelect)
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

        public AutomatedUITestDocument1EditAssessmentPage InsertWFNumericValue(string ValueToInsert)
        {
            SendKeys(WFNumericQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertWFDateValue(string ValueToInsert)
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

        public AutomatedUITestDocument1EditAssessmentPage TapImageQuestionEditButton()
        {
            Click(ImageQuestionEditButton);

            Wait.Until(c => c.FindElement(ImageQuestionSelectFileControl));
            Wait.Until(c => c.FindElement(ImageQuestionUploadButton));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ImageQuestionUploadFile(string FilePath)
        {
            SendKeys(ImageQuestionSelectFileControl, FilePath);

            WaitForElementToBeClickable(ImageQuestionUploadButton);
            MoveToElementInPage(ImageQuestionUploadButton);
            Click(ImageQuestionUploadButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForImageQuestionImage()
        {            
            WaitForElementVisible(ImageQuestionImageFile);
            MoveToElementInPage(ImageQuestionImageFile);
            string srcValue =  GetElementByAttributeValue(ImageQuestionImageFile, "src");
            Assert.IsNotNull(srcValue);
            Assert.IsTrue(srcValue.EndsWith(".jpg"));

            return this;
        }

        public PersonRelationshipPage TapIGACMSListEditableTestNewButton()
        {
            Click(IGACMSListEditableNewButton);

            return new PersonRelationshipPage(driver, Wait, appURL);
        }

        public PersonRelationshipPage TapIGACMSListEditableTestRow(string PersonRelationshipid)
        {
            Click(IGACMSListEditableAnswerRow(PersonRelationshipid));

            return new PersonRelationshipPage(driver, Wait, appURL);
        }

        public PersonRelationshipPage TapIGACMSListWithRelatedDataEditButton(string PersonRelationshipid)
        {
            Click(CMSListWithRelatedDataEditButton(PersonRelationshipid));

            return new PersonRelationshipPage(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSFieldWriteBackTest1Answer(string ExpectedValue)
        {
            ValidateElementValue(IGACMSFieldWriteBackQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSFieldWriteBackTest1Disabled()
        {
            ValidateElementDisabled(IGACMSFieldWriteBackQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSFieldWriteBackTest1Enabled()
        {
            ValidateElementEnabled(IGACMSFieldWriteBackQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleChoiceOptionSelected(int OptionSelected)
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

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleChoiceOptionNotSelected(int OptionSelected)
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

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleChoiceOptionDisabled()
        {
            ValidateElementDisabled(WFMultipleChoiceRadioButton1);
            ValidateElementDisabled(WFMultipleChoiceRadioButton2);
            ValidateElementDisabled(WFMultipleChoiceRadioButton3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleChoiceOptionEnabled()
        {
            ValidateElementEnabled(WFMultipleChoiceRadioButton1);
            ValidateElementEnabled(WFMultipleChoiceRadioButton2);
            ValidateElementEnabled(WFMultipleChoiceRadioButton3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDecimalAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFDecimalQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDecimalDisabled()
        {
            ValidateElementDisabled(WFDecimalQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDecimalEnabled()
        {
            ValidateElementEnabled(WFDecimalQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleResponseOptionChecked(int OptionSelected)
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

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleResponseOptionNotChecked(int OptionSelected)
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

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleResponseVisible()
        {
            WaitForElementVisible(WFMultipleResponseCheckbox1);
            WaitForElementVisible(WFMultipleResponseCheckbox2);
            WaitForElementVisible(WFMultipleResponseCheckbox3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleResponseDisabled()
        {
            ValidateElementDisabled(WFMultipleResponseCheckbox1);
            ValidateElementDisabled(WFMultipleResponseCheckbox2);
            ValidateElementDisabled(WFMultipleResponseCheckbox3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFMultipleResponseEnabled()
        {
            ValidateElementEnabled(WFMultipleResponseCheckbox1);
            ValidateElementEnabled(WFMultipleResponseCheckbox2);
            ValidateElementEnabled(WFMultipleResponseCheckbox3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFNumericAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFNumericQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFNumericDisabled()
        {
            ValidateElementDisabled(WFNumericQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFNumericEnabled()
        {
            ValidateElementEnabled(WFNumericQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDateAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFDateQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForWFDateQuestionVisible()
        {
            WaitForElementVisible(WFDateQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDateDisabled()
        {
            ValidateElementDisabled(WFDateQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFDateEnabled()
        {
            ValidateElementEnabled(WFDateQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSListEditableTest1Row(string PersonRelationshipID, string RelatedPerson, string InsideHousehold, string RelationshipWithCurrentPerson, string StartDate)
        {
            WaitForElement(IGACMSListEditableAnswerRow(PersonRelationshipID));
            WaitForElement(IGACMSListEditableAnswerColumn1(PersonRelationshipID, RelatedPerson));
            WaitForElement(IGACMSListEditableAnswerColumn2(PersonRelationshipID, InsideHousehold));
            WaitForElement(IGACMSListEditableAnswerColumn3(PersonRelationshipID, RelationshipWithCurrentPerson));
            WaitForElement(IGACMSListEditableAnswerColumn4(PersonRelationshipID, StartDate));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateGuiClientRelationshipListRow(int TableRow, string RelatedPerson, string InsideHousehold, string RelationshipWithCurrentPerson, string StartDate)
        {
            WaitForElement(GuiClientRelationshipListAnswerColumn1(TableRow, RelatedPerson));
            WaitForElement(GuiClientRelationshipListAnswerColumn2(TableRow, InsideHousehold));
            WaitForElement(GuiClientRelationshipListAnswerColumn3(TableRow, RelationshipWithCurrentPerson));
            WaitForElement(GuiClientRelationshipListAnswerColumn4(TableRow, StartDate));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSListWithRelatedDataTest1Headers(string PersonRelationshipID)
        {
            WaitForElement(CMSListWithRelatedDataQuestionLabelHeader1(PersonRelationshipID));
            WaitForElement(CMSListWithRelatedDataQuestionLabelHeader2(PersonRelationshipID));
            WaitForElement(CMSListWithRelatedDataQuestionLabelHeader3(PersonRelationshipID));
            WaitForElement(CMSListWithRelatedDataQuestionLabelHeader4(PersonRelationshipID));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateIGACMSListWithRelatedDataTest1(string PersonRelationshipID, string RelatedPerson, string InsideHousehold, string RelationshipWithCurrentPerson, string StartDate)
        {
            WaitForElement(CMSListWithRelatedDataRelatedPersonAnswer(PersonRelationshipID, RelatedPerson));
            WaitForElement(CMSListWithRelatedDataInsideHouseholdAnswer(PersonRelationshipID, InsideHousehold));
            WaitForElement(CMSListWithRelatedDataRelationshipAnswer(PersonRelationshipID, RelationshipWithCurrentPerson));
            WaitForElement(CMSListWithRelatedDataStartDateAnswer(PersonRelationshipID, StartDate));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFLookupLookupValue(string ExpectedText)
        {
            if(string.IsNullOrEmpty(ExpectedText))
            {
                //if the string is empty it means that the test is trying to assert that the lookup is empty
                //in that case we need to assert that no link element exists
                ValidateElementDoNotExist(WFLookupAnswer);

                return this;
            }

            ValidateElementText(WFLookupAnswer, ExpectedText);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFLookupDisabled()
        {
            WaitForElementRemoved(WFLookupSearchButton);
            WaitForElementRemoved(WFLookupClearbutton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFLookupEnabled()
        {
            ValidateElementEnabled(WFLookupSearchButton);
            ValidateElementEnabled(WFLookupClearbutton);

            return this;
        }

        #endregion

        #region Questions Menu

        public AutomatedUITestDocument1EditAssessmentPage ClickImageQuestionMenuButton()
        {
            Click(section1ImageQuestionMenuButton);

            return this;
        }

        public QuestionInformationDialoguePopup ClickImageQuestionQuestionInformationLink()
        {
            WaitForElementToBeClickable(section1ImageQuestionQuestionInformationLink);

            Click(section1ImageQuestionQuestionInformationLink);

            return new QuestionInformationDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCommentsDialoguePopup ClickImageQuestionCommentsLink()
        {
            WaitForElementToBeClickable(section1ImageQuestionCommentsLink);

            Click(section1ImageQuestionCommentsLink);

            return new QuestionCommentsDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFMultipleChoiceQuestionMenuButton()
        {
            Click(section1WFMultipleChoiceQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFMultipleChoiceQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFMultipleChoiceQuestionAuditButton);
            ScrollToElement(section1WFMultipleChoiceQuestionAuditButton);
            Click(section1WFMultipleChoiceQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFMultipleChoiceQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFMultipleChoiceQuestionPreviousAnswersButton);

            Click(section1WFMultipleChoiceQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFDecimalQuestionMenuButton()
        {
            Click(section1WFDecimalQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFDecimalQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFDecimalQuestionAuditButton);
            ScrollToElement(section1WFDecimalQuestionAuditButton);
            Click(section1WFDecimalQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFDecimalQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFDecimalQuestionPreviousAnswersButton);

            Click(section1WFDecimalQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFMultipleResponseQuestionMenuButton()
        {
            Click(section1WFMultipleResponseQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFMultipleResponseQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFMultipleResponseQuestionAuditButton);
            ScrollToElement(section1WFMultipleResponseQuestionAuditButton);
            Click(section1WFMultipleResponseQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFMultipleResponseQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFMultipleResponseQuestionPreviousAnswersButton);

            Click(section1WFMultipleResponseQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFNumericQuestionMenuButton()
        {
            Click(section1WFNumericQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFNumericQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFNumericQuestionAuditButton);
            ScrollToElement(section1WFNumericQuestionAuditButton);
            Click(section1WFNumericQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFNumericQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFNumericQuestionPreviousAnswersButton);

            Click(section1WFNumericQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFLookupQuestionMenuButton()
        {
            Click(section1WFLookupQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFLookupQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFLookupQuestionAuditButton);
            ScrollToElement(section1WFLookupQuestionAuditButton);
            Click(section1WFLookupQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFLookupQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFLookupQuestionPreviousAnswersButton);

            Click(section1WFLookupQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        public AutomatedUITestDocument1EditAssessmentPage ClickWFDateQuestionMenuButton()
        {
            Click(section1WFDateQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFDateQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1WFDateQuestionAuditButton);
            ScrollToElement(section1WFDateQuestionAuditButton);
            Click(section1WFDateQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFDateQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1WFDateQuestionPreviousAnswersButton);

            Click(section1WFDateQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }

        #endregion


        #endregion

        #region Section 1.1

        public AutomatedUITestDocument1EditAssessmentPage TapSection1_1TitleArea()
        {
            Click(section1_1TitleLink);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateSection1_1Text(string ExpectedSectionText)
        {
            WaitForElement(section1_1ByText(ExpectedSectionText));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1Visible()
        {
            Wait.Until(c => c.FindElement(section1_1Title).Displayed);
            Wait.Until(c => c.FindElement(section1_1Expanded).Displayed);
            Wait.Until(c => c.FindElement(section1_1MenuButton).Displayed);

            Wait.Until(c => c.FindElement(WFParagraphQuestionLabel).Displayed);
            Wait.Until(c => c.FindElement(WFParagraphQuestion).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1Hidden()
        {
            Wait.Until(c => !c.FindElement(WFParagraphQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(WFParagraphQuestion).Displayed);

            Wait.Until(c => c.FindElement(section1_1Collapsed).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1Hidden(bool Section1_1CollapseButtonVisible)
        {
            Wait.Until(c => !c.FindElement(WFParagraphQuestionLabel).Displayed);
            Wait.Until(c => !c.FindElement(WFParagraphQuestion).Displayed);

            if(Section1_1CollapseButtonVisible)
                Wait.Until(c => c.FindElement(section1_1Collapsed).Displayed);


            return this;
        }

        /// <summary>
        /// Wait for the Section 1.1 to be removed from the DOM
        /// </summary>
        /// <returns></returns>
        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1Removed()
        {
            WaitForElementRemoved(section1_1TitleLink);
            WaitForElementRemoved(section1_1Title);
            WaitForElementRemoved(section1_1Expanded);
            WaitForElementRemoved(section1_1Collapsed);
            WaitForElementRemoved(section1_1MenuButton);
            WaitForElementRemoved(section1_1SectionCompletedMessage);

            WaitForElementRemoved(WFParagraphQuestionLabel);
            WaitForElementRemoved(WFParagraphQuestion);

            WaitForElementRemoved(section1_1WFParagraphQuestionMenuButton);
            WaitForElementRemoved(section1_1WFParagraphQuestionAuditButton);
            WaitForElementRemoved(section1_1WFParagraphQuestionPreviousAnswersButton);

            return this;
        }   

        public AutomatedUITestDocument1EditAssessmentPage TapSection1_1MenuButton()
        {
            Click(section1_1MenuButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection1_1CompleteSectionButton()
        {
            WaitForElementToBeClickable(section1_1CompleteSectionButton);
            Click(section1_1CompleteSectionButton);

            return this;

        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1SectionCompletedMessageDisplayed()
        {
            WaitForElementVisible(section1_1SectionCompletedMessage);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_1SectionCompletedMessageNotDisplayed()
        {
            WaitForElementNotVisible(section1_1SectionCompletedMessage, 3);

            return this;
        }

        #region Questions

        public AutomatedUITestDocument1EditAssessmentPage InsertWFParagraph(string ValueToInsert)
        {
            SendKeys(WFParagraphQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFParagraphAnswer(string ExpectedValue)
        {
            ValidateElementValue(WFParagraphQuestion, ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFParagraphVisible()
        {
            WaitForElementVisible(WFParagraphQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFParagraphNotVisible()
        {
            WaitForElementNotVisible(WFParagraphQuestion, 3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFParagraphDisabled()
        {
            ValidateElementDisabled(WFParagraphQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFParagraphEnabled()
        {
            ValidateElementEnabled(WFParagraphQuestion);

            return this;
        }

        #endregion

        #region Questions Menu

        public AutomatedUITestDocument1EditAssessmentPage ClickWFParagraphQuestionMenuButton()
        {
            Click(section1_1WFParagraphQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFParagraphQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1_1WFParagraphQuestionAuditButton);
            ScrollToElement(section1_1WFParagraphQuestionAuditButton);
            Click(section1_1WFParagraphQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }
        
        public QuestionCompareDialoguePopup ClickWFParagraphQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1_1WFParagraphQuestionPreviousAnswersButton);

            Click(section1_1WFParagraphQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        #endregion



        #endregion

        #region Section 1.2

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection1_2Visible()
        {
            Wait.Until(c => c.FindElement(section1_2Title).Displayed);
            Wait.Until(c => c.FindElement(section1_2Expanded).Displayed);
            Wait.Until(c => c.FindElement(section1_2MenuButton).Displayed);

            Wait.Until(c => c.FindElement(WFPickListQuestionLabel).Displayed);

            return this;
        }


        #region Questions

        public AutomatedUITestDocument1EditAssessmentPage SelectWFPicklistByText(string ElementText)
        {
            SelectElement se = new SelectElement(GetWebElement(WFPickListQuestion));
            se.SelectByText(ElementText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFPicklistSelectedValue(string ExpectedSelectedElement)
        {
            SelectElement se = new SelectElement(GetWebElement(WFPickListQuestion));
            Assert.AreEqual(ExpectedSelectedElement, se.SelectedOption.Text);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForWFPicklistQuestionVisible()
        {
            WaitForElementVisible(WFPickListQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFPicklistDisabled()
        {
            ValidateElementDisabled(WFPickListQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFPicklistEnabled()
        {
            ValidateElementEnabled(WFPickListQuestion);

            return this;
        }

        #endregion

        #region Questions Menu

        public AutomatedUITestDocument1EditAssessmentPage ClickWFPicklistQuestionMenuButton()
        {
            Click(section1_2WFPicklistQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFPicklistQuestionAuditLink()
        {
            WaitForElementToBeClickable(section1_2WFPicklistQuestionAuditButton);
            ScrollToElement(section1_2WFPicklistQuestionAuditButton);
            Click(section1_2WFPicklistQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFPicklistQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section1_2WFPicklistQuestionPreviousAnswersButton);

            Click(section1_2WFPicklistQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }

        #endregion


        #endregion

        #region Section 2

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection2Visible()
        {
            Wait.Until(c => c.FindElement(section2Title).Displayed);
            Wait.Until(c => c.FindElement(section2Expanded).Displayed);
            Wait.Until(c => c.FindElement(section2MenuButton).Displayed);

            Wait.Until(c => c.FindElement(WFShortAnswerQuestionLabel).Displayed);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection2MenuButton()
        {
            Click(section2MenuButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection2SpellCheckButton()
        {
            WaitForElementToBeClickable(section2SpellCheckButton);
            Click(section2SpellCheckButton);

            return this;
        }

        #region Questions

        public AutomatedUITestDocument1EditAssessmentPage InsertWFShortAnswer(string ValueToInsert)
        {
            SendKeys(WFShortAnswerQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFShortAnswer(string ExpectedText)
        {
            ValidateElementValue(WFShortAnswerQuestion, ExpectedText);
            return this;
        }

        #endregion

        #region Questions Menu

        public AutomatedUITestDocument1EditAssessmentPage ClickWFShortAnswerQuestionMenuButton()
        {
            Click(section2WFShortAnswerQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFShortAnswerQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2WFShortAnswerQuestionAuditButton);
            ScrollToElement(section2WFShortAnswerQuestionAuditButton);
            Click(section2WFShortAnswerQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }
        
        public QuestionCompareDialoguePopup ClickWFShortAnswerQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2WFShortAnswerQuestionPreviousAnswersButton);

            Click(section2WFShortAnswerQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }

        #endregion


        #endregion

        #region Section 2.1

        public AutomatedUITestDocument1EditAssessmentPage WaitForSection2_1Visible()
        {
            Wait.Until(c => c.FindElement(section2_1MenuButton).Displayed);

            Wait.Until(c => c.FindElement(AuthorisationQuestionLabel).Displayed);


            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection2_1MenuButton()
        {
            Click(section2_1MenuButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapSection2_1SpellCheckButton()
        {
            WaitForElementToBeClickable(section2_1SpellCheckButton);
            Click(section2_1SpellCheckButton);

            return this;
        }

        #region Questions

        public AutomatedUITestDocument1EditAssessmentPage ClickSignatureEditButton()
        {
            Click(AuthorisationEditSignatureEditButton);

            return this;
        }
        public AutomatedUITestDocument1EditAssessmentPage ClickSignatureDeleteButton()
        {
            Click(AuthorisationEditSignatureDeleteButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateSignatureDeleteButtonVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(AuthorisationEditSignatureDeleteButton);
            else
                WaitForElementNotVisible(AuthorisationEditSignatureDeleteButton, 3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SelectWFBoolean(bool ValueToSelect)
        {
            switch (ValueToSelect)
            {
                case true:
                    Click(WFBooleanYesOption);

                    break;
                case false:
                    Click(WFBooleanNoOption);

                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertWFTime(string ValueToInsert)
        {
            SendKeys(WFTimeQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestHQRow1Column1Value(string ValueToInsert)
        {
            SendKeys(TestHQQuestionRow1Column1, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestHQRow1Column2Value(string ValueToInsert)
        {
            SendKeys(TestHQQuestionRow1Column2, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestHQRow2Column1Value(string ValueToInsert)
        {
            SendKeys(TestHQQuestionRow2Column1, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestHQRow2Column2Value(string ValueToInsert)
        {
            SendKeys(TestHQQuestionRow2Column2, ValueToInsert);
            return this;
        }


        public AutomatedUITestDocument1EditAssessmentPage TapWFTableWithUnlimitedRowsNewButton()
        {
            Click(TableWithUnlimtedRowsAddNewRowButton);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFTableWithUnlimitedRowsNewButtonDoNotExist()
        {
            ValidateElementDoNotExist(TableWithUnlimtedRowsAddNewRowButton);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFTableWithUnlimitedRowsNewButtonExists()
        {
            WaitForElement(TableWithUnlimtedRowsAddNewRowButton);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertDateBecameInvolvedValue(int TableRowNumber, string ValueToInsert)
        {
            WaitForElementToBeClickable(TableWithUnlimtedRowsDateQuestion(TableRowNumber));
            SendKeys(TableWithUnlimtedRowsDateQuestion(TableRowNumber), ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage SelectReasonForAssessmentValue(int TableRowNumber, string TextToSelect)
        {
            WaitForElementToBeClickable(TableWithUnlimtedRowsPicklist(TableRowNumber));
            SelectPicklistElementByText(TableWithUnlimtedRowsPicklist(TableRowNumber), TextToSelect);
            return this;
        }

        public AlertPopup TapWFTableWithUnlimitedRowsDeleteRowButton(int TableRowNumber)
        {
            Click(TableWithUnlimtedRowsDeleteRowButton(TableRowNumber));

            return new AlertPopup(driver, Wait, appURL);
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertQuestion1ContributionNotes(string ValueToInsert)
        {
            SendKeys(TablePAQuestion1ContributionNotes, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertQuestion1Role(string ValueToInsert)
        {
            SendKeys(TablePAQuestion1Role, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertQuestion2ContributionNotes(string ValueToInsert)
        {
            SendKeys(TablePAQuestion2ContributionNotes, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertQuestion2Role(string ValueToInsert)
        {
            SendKeys(TablePAQuestion2Role, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestQPCOutcomeAnswer(string ValueToInsert)
        {
            SendKeys(TestQPCOutcomeQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestQPCTypeOfInvolvementAnswer(string ValueToInsert)
        {
            SendKeys(TestQPCTypeOfInvolvementQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestQPCWFTimeAnswer(string ValueToInsert)
        {
            SendKeys(TestQPCWFTimeQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage InsertTestQPCWhoAnswer(string ValueToInsert)
        {
            SendKeys(TestQPCWhoQuestion, ValueToInsert);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapWFHiperlink1()
        {
            Click(WFHiperlink1QuestionLabel);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapWFHiperlinkGroup()
        {
            Click(WFHiperlinkGroupQuestionLabel);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapWFHiperlink2()
        {
            Click(WFHiperlink2QuestionLabel);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapWFHiperlink3()
        {
            Click(WFHiperlink3QuestionLabel);
            return this;
        }


        public AutomatedUITestDocument1EditAssessmentPage ValidateWFBoolean(bool ExpectedValue)
        {
            switch (ExpectedValue)
            {
                case true:
                    ValidateElementChecked(WFBooleanYesOption);

                    break;
                case false:
                    ValidateElementChecked(WFBooleanNoOption);

                    break;
                default:
                    break;
            }
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFBooleanNoOptionSelected()
        {
            ValidateElementNotChecked(WFBooleanYesOption);
            ValidateElementNotChecked(WFBooleanNoOption);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFBooleanVisible()
        {
            WaitForElementVisible(WFBooleanYesOption);
            WaitForElementVisible(WFBooleanNoOption);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFBooleanDisabled()
        {
            ValidateElementDisabled(WFBooleanYesOption);
            ValidateElementDisabled(WFBooleanNoOption);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFBooleanEnabled()
        {
            ValidateElementEnabled(WFBooleanYesOption);
            ValidateElementEnabled(WFBooleanNoOption);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFTimeQuestion(string ExpectedText)
        {
            ValidateElementValue(WFTimeQuestion, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFTimeDisabled()
        {
            ValidateElementDisabled(WFTimeQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateWFTimeEnabled()
        {
            ValidateElementEnabled(WFTimeQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow1Column1Question(string ExpectedText)
        {
            ValidateElementValue(TestHQQuestionRow1Column1, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow1Column2Question(string ExpectedText)
        {
            ValidateElementValue(TestHQQuestionRow1Column2, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow2Column1Question(string ExpectedText)
        {
            ValidateElementValue(TestHQQuestionRow2Column1, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow2Column2Question(string ExpectedText)
        {
            ValidateElementValue(TestHQQuestionRow2Column2, ExpectedText);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow1Column1Disabled()
        {
            ValidateElementDisabled(TestHQQuestionRow1Column1);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow1Column1Enabled()
        {
            ValidateElementEnabled(TestHQQuestionRow1Column1);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow2Column2Disabled()
        {
            ValidateElementDisabled(TestHQQuestionRow2Column2);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestHQRow2Column2Enabled()
        {
            ValidateElementEnabled(TestHQQuestionRow2Column2);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDateBecameInvolvedAnswer(int TableRowNumber, string ExpectedValue)
        {
            ValidateElementValue(TableWithUnlimtedRowsDateQuestion(TableRowNumber), ExpectedValue);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDateBecameInvolvedVisible(int TableRowNumber)
        {
            WaitForElementVisible(TableWithUnlimtedRowsDateQuestion(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDateBecameInvolvedDisabled(int TableRowNumber)
        {
            ValidateElementDisabled(TableWithUnlimtedRowsDateQuestion(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDateBecameInvolvedEnabled(int TableRowNumber)
        {
            ValidateElementEnabled(TableWithUnlimtedRowsDateQuestion(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateReasonForAssessmentAnswer(int TableRowNumber, string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(TableWithUnlimtedRowsPicklist(TableRowNumber), ExpectedSelectedText);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateReasonForAssessmentVisible(int TableRowNumber)
        {
            WaitForElementVisible(TableWithUnlimtedRowsPicklist(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateReasonForAssessmentDisabled(int TableRowNumber)
        {
            ValidateElementDisabled(TableWithUnlimtedRowsPicklist(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateReasonForAssessmentEnabled(int TableRowNumber)
        {
            ValidateElementEnabled(TableWithUnlimtedRowsPicklist(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDateBecameInvolvedRowDoNotExist(int TableRowNumber)
        {
            ValidateElementDoNotExist(TableWithUnlimtedRowsDateQuestion(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateReasonForAssessmentRowDoNotExist(int TableRowNumber)
        {
            ValidateElementDoNotExist(TableWithUnlimtedRowsPicklist(TableRowNumber));

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion1ContributionNotes(string ExpectedValue)
        {
            ValidateElementValue(TablePAQuestion1ContributionNotes, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion1ContributionDisabled()
        {
            ValidateElementDisabled(TablePAQuestion1ContributionNotes);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion1ContributionEnabled()
        {
            ValidateElementEnabled(TablePAQuestion1ContributionNotes);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion1Role(string ExpectedValue)
        {
            ValidateElementValue(TablePAQuestion1Role, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion2ContributionNotes(string ExpectedValue)
        {
            ValidateElementValue(TablePAQuestion2ContributionNotes, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion2Role(string ExpectedValue)
        {
            ValidateElementValue(TablePAQuestion2Role, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion2RoleDisabled()
        {
            ValidateElementDisabled(TablePAQuestion2Role);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateQuestion2RoleEnabled()
        {
            ValidateElementEnabled(TablePAQuestion2Role);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCOutcomeAnswer(string ExpectedValue)
        {
            ValidateElementValue(TestQPCOutcomeQuestion, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCTypeOfInvolvementAnswer(string ExpectedValue)
        {
            ValidateElementValue(TestQPCTypeOfInvolvementQuestion, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCTypeOfInvolvementDisabled()
        {
            ValidateElementDisabled(TestQPCTypeOfInvolvementQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCTypeOfInvolvementEnabled()
        {
            ValidateElementEnabled(TestQPCTypeOfInvolvementQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCWFTimeAnswer(string ExpectedValue)
        {
            ValidateElementValue(TestQPCWFTimeQuestion, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCWhoAnswer(string ExpectedValue)
        {
            ValidateElementValue(TestQPCWhoQuestion, ExpectedValue);
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCWhoDisabled()
        {
            ValidateElementDisabled(TestQPCWhoQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateTestQPCWhoEnabled()
        {
            ValidateElementEnabled(TestQPCWhoQuestion);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDrawQuestionEditButtonVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(drawQuestionEditDrawButton);
            }
            else
            {
                WaitForElementNotVisible(drawQuestionEditDrawButton, 7);
            }
            
            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ValidateDrawQuestionDeleteButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(drawQuestionDeleteDrawButton);
            }
            else
            {
                WaitForElementNotVisible(drawQuestionDeleteDrawButton, 7);
            }

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ClickDrawQuestionEditButton()
        {
            WaitForElementToBeClickable(drawQuestionEditDrawButton);
            Click(drawQuestionEditDrawButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage ClickDrawQuestionDeleteButton()
        {
            WaitForElementToBeClickable(drawQuestionDeleteDrawButton);
            Click(drawQuestionDeleteDrawButton);

            return this;
        }

        #endregion

        #region Questions Menu

        public AutomatedUITestDocument1EditAssessmentPage ClickTestHQQuestionMenuButton()
        {
            Click(section2_1WFTestHQQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickTestHQQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFTestHQQuestionAuditButton);
            ScrollToElement(section2_1WFTestHQQuestionAuditButton);
            Click(section2_1WFTestHQQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickTestHQQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFTestHQQuestionPreviousAnswersButton);

            Click(section2_1WFTestHQQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ClickWFTableWithUnlimitedRowsQuestionMenuButton()
        {
            Click(section2_1WFTableWithUnlimitedRowsQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFTableWithUnlimitedRowsQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFTableWithUnlimitedRowsQuestionAuditButton);
            ScrollToElement(section2_1WFTableWithUnlimitedRowsQuestionAuditButton);
            Click(section2_1WFTableWithUnlimitedRowsQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFTableWithUnlimitedRowsQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFTableWithUnlimitedRowsQuestionPreviousAnswersButton);

            Click(section2_1WFTableWithUnlimitedRowsQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ClickTablePQQuestionMenuButton()
        {
            Click(section2_1WFTablePQQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickTablePQQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFTablePQQuestionAuditButton);
            ScrollToElement(section2_1WFTablePQQuestionAuditButton);
            Click(section2_1WFTablePQQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickTablePQQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFTablePQQuestionPreviousAnswersButton);

            Click(section2_1WFTablePQQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ClickTestQPCQuestionMenuButton()
        {
            Click(section2_1WFTestQPCQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickTestQPCQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFTestQPCQuestionAuditButton);
            ScrollToElement(section2_1WFTestQPCQuestionAuditButton);
            Click(section2_1WFTestQPCQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickTestQPCQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFTestQPCQuestionPreviousAnswersButton);

            Click(section2_1WFTestQPCQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ClickWFBooleanQuestionMenuButton()
        {
            Click(section2_1WFBooleanQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFBooleanQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFBooleanQuestionAuditButton);
            ScrollToElement(section2_1WFBooleanQuestionAuditButton);
            Click(section2_1WFBooleanQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFBooleanQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFBooleanQuestionPreviousAnswersButton);

            Click(section2_1WFBooleanQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }



        public AutomatedUITestDocument1EditAssessmentPage ClickWFTimeQuestionMenuButton()
        {
            Click(section2_1WFTimeQuestionMenuButton);

            return this;
        }

        public QuestionAuditDialoguePopup ClickWFTimeQuestionAuditLink()
        {
            WaitForElementToBeClickable(section2_1WFTimeQuestionAuditButton);
            ScrollToElement(section2_1WFTimeQuestionAuditButton);
            Click(section2_1WFTimeQuestionAuditButton);

            return new QuestionAuditDialoguePopup(driver, Wait, appURL);
        }

        public QuestionCompareDialoguePopup ClickWFTimeQuestionPreviousAnswersLink()
        {
            WaitForElementToBeClickable(section2_1WFTimeQuestionPreviousAnswersButton);

            Click(section2_1WFTimeQuestionPreviousAnswersButton);

            return new QuestionCompareDialoguePopup(driver, Wait, appURL);
        }


        #endregion


        #endregion

        #region Dinamic Dialog Popup

        public AutomatedUITestDocument1EditAssessmentPage WaitForDinamicDialogeToOpen(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementVisible(dinamicDialogTitle(ExpectedTitle));
            WaitForElementVisible(dinamicDialogMessage(ExpectedMessage));
            WaitForElementVisible(dinamicDialogOKButton);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage WaitForDinamicDialogeToClose(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementNotVisible(dinamicDialogTitle(ExpectedTitle), 3);
            WaitForElementNotVisible(dinamicDialogMessage(ExpectedMessage), 3);
            WaitForElementNotVisible(dinamicDialogOKButton, 3);

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage DinamicDialogeTapOKButton()
        {
            Click(dinamicDialogOKButton);

            return this;
        }

        #endregion

        #endregion


    }
}

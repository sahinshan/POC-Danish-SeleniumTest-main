using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class AssessmentPage : CommonMethods
    {
        public AssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By pageTitle = By.XPath("//*[@id='CWPageHeader']/div/div/div/div/h1");
        readonly By breadcrumbElement = By.XPath("//*[@id='CWPageHeader']/mosaic-breadcrumb/ol/li[text()='Assessment']");
        readonly By pageTopMessage = By.XPath("//*[@id='CWAssessmentControl']/div/mosaic-alert/div[2]/p[text()='* denotes mandatory field']");

        

        readonly By alertMessage = By.XPath("//*[@id='CWAssessmentControl']/mosaic-alert/div/div");

        readonly By loaderIcon = By.XPath("//*[@id='CWLoader']");



        By SectionLink(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']");
        By SectionLinkText(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']/button/div[@class='mc-btn__div']");
        By SubSectionLink(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']");
        By SubSectionLinkText(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']/a");


        By SectionTitle(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "']/*/*/h2");
        By SubSectionTitle(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "']/h3");


        By QuestionTitle(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/label");
        By QuestionInstructions(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "_truncate']/p");
        By SubSectionQuestionTitle(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/label");



        By TableWithUnlimitedRows_TableTitle(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/h4");
        By TableWithUnlimitedRows_TableSubTitle(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/label");
        By TableWithUnlimitedRows_QuestionHeading(string DocumentQuestionIdentifier, int RowPosition) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "-" + RowPosition + "-question']/label");
        By TableWithUnlimitedRows_AddButton(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='unlimited-remove-button-" + DocumentSectionQuestionIdentifier + "']");
        By TableWithUnlimitedRows_RemoveButton(string DocumentSectionQuestionIdentifier, int RowPosition) => By.XPath("//*[@id='unlimited-remove-button-" + DocumentSectionQuestionIdentifier + "-" + RowPosition + "']");




        By TableWithHeaderQuestion_TableTitle(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/h4");
        By TableWithHeaderQuestion_QuestionHeading(string DocumentSectionQuestionIdentifier, string HeadingText) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/div/label[text()='" + HeadingText + "']");


        By TableWithPrimaryQuestion_TableTitle(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/h4");
        By TableWithPrimaryQuestion_RowHeading(string DocumentSectionQuestionIdentifier, string HeadingText) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/div/label[@class='subgroup-que-lbl'][text()='" + HeadingText + "']");
        By TableWithPrimaryQuestion_RowSubHeading(string DocumentSectionQuestionIdentifier, string SubHeadingText) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/div/label[@class='group-question-subheading'][text()='" + SubHeadingText + "']");
        By TableWithPrimaryQuestion_QuestionHeading(string DocumentSectionQuestionIdentifier, string HeadingText) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/div/div/label[text()='" + HeadingText + "']");



        By TableWithQuestionsPerCell_TableTitle(string DocumentSectionQuestionIdentifier) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/h4");
        By TableWithQuestionsPerCell_QuestionHeading(string DocumentSectionQuestionIdentifier, string HeadingText) => By.XPath("//*[@id='" + DocumentSectionQuestionIdentifier + "']/div/label[text()='" + HeadingText + "']");



        By RadioButtonQuestionOption(string SectionQuestionIdentifier, string Value) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/input[@value='" + Value + "']");
        By RadioButtonQuestionText(string SectionQuestionIdentifier, string Label) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/label[text()='" + Label + "']");

        By QuestionInput(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "-input']");
        By MosaicQuestionInput(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "']");
        By QuestionWarning(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "']/div[2]/span");


        By MultiResponseQuestionMosaicTopElement(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']");
        By MultiResponseQuestionPicklist(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div[@class='ss-multi-selected']/div[@class='ss-values']/span");
        By MultiResponseQuestionAddButton(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div[@class='ss-multi-selected']/div[@class='ss-add']/span");
        By MultiResponseQuestionOption(string SectionQuestionIdentifier, string MultiOptionText) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div/div/div[@class='ss-option'][text()='" + MultiOptionText + "']");
        By MultiResponseQuestionAddedOption(string SectionQuestionIdentifier, string SelectedMultiOptionText) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div/div/div/span[@class='ss-value-text'][text()='" + SelectedMultiOptionText + "']");
        By MultiResponseQuestionAddedOptionRemoveButton(string SectionQuestionIdentifier, string SelectedMultiOptionText) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div/div/div/span[@class='ss-value-text'][text()='" + SelectedMultiOptionText + "']/parent::div/span[@class='ss-value-delete']");



        By DateQuestionMosaicElement(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']");
        By DateQuestionRightSideIcon(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div[@class='mc-form__input-group']/div/span");
        By DateQuestionInput(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/input[@type='text']");
        By MosaicDateQuestionInput(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "']");
        By MosaicDateQuestionHiddenInput(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "']/div/input[@type='hidden']");



        By TimeQuestionInput(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/input[@type='text']");
        By MosaicTimeQuestionInput(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']");
        By MosaicTimeQuestionHiddenInput(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/input[@type='hidden']");


        By PicklistQuestionMosaicTopElement(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']");
        By PicklistQuestionPicklist(string SectionQuestionIdentifier) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div[@class='ss-single-selected']/span[@class='placeholder']");
        By PicklistQuestion_ClearButton(string DocumentQuestionIdentifier) => By.XPath("//*[@id='" + DocumentQuestionIdentifier + "']/div/div/span/mosaic-icon[@name='clear_symbol']");
        By PicklistQuestionOption(string SectionQuestionIdentifier, string OptionText) => By.XPath("//*[@id='" + SectionQuestionIdentifier + "']/div/div/div/div[@class='ss-option'][text()='" + OptionText + "']");



        By viewInPDFButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWPrintButton']");
        By saveButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWSaveButton']");
        By saveAndNextButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWNextButton']");
        By saveAndPreviousButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWPrevButton']/button");
        By submitButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWSubmitButton']");



        //By QuestionInstructions(string DocumentSectionQuestion) => By.XPath("//*[@id='" + DocumentSectionQuestion + "_truncate']/p");
        //By QuestionInstructionsReadMoreButton(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-CWSubmitButton']");




        #region Warning Dialog Area

        readonly By warningMessageBase = By.XPath("//*[@id='CWAssessmentAlertContent']");
        readonly By warningMessage = By.XPath("//*[@id='CWAssessmentAlertContent']/div");
        By warningSectionInformation(int SectionPosition) => By.XPath("//*[@id='CWAssessmentAlertContent']/ul/li[" + SectionPosition + "]");
        By warningQuestionInformation(int SectionPosition, int QuestionPosition) => By.XPath("//*[@id='CWAssessmentAlertContent']/ul/ul[" + SectionPosition + "]/li[" + QuestionPosition + "]/a");

        #endregion



        



        public AssessmentPage WaitForAssessmentPageToLoad(string AssessmentTitle)
        {
            WaitForBrowserWindowTitle("Assessment");

            WaitForElement(pageTitle);
            ValidateElementText(pageTitle, AssessmentTitle);

            WaitForElement(breadcrumbElement);

            return this;
        }


        public AssessmentPage ValidateSectionLinkTextVisibility(string SectionIdentifier, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SectionLinkText(SectionIdentifier));
            else
                WaitForElementNotVisible(SectionLinkText(SectionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateSectionLinkText(string SectionIdentifier, string ExpectedText)
        {
            ValidateElementText(SectionLinkText(SectionIdentifier), ExpectedText);

            return this;
        }


        public AssessmentPage ValidateSubSectionLinkVisibility(string SubSectionIdentifier, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SubSectionLink(SubSectionIdentifier));
            else
                WaitForElementNotVisible(SubSectionLink(SubSectionIdentifier), 7);

            return this;
        }

        public AssessmentPage ValidateSectionVisibility(string SectionIdentifier, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SectionTitle(SectionIdentifier));
            else
                WaitForElementNotVisible(SectionTitle(SectionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateSubSectionVisibility(string SectionIdentifier, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SubSectionTitle(SectionIdentifier));
            else
                WaitForElementNotVisible(SubSectionTitle(SectionIdentifier), 7);

            return this;
        }
        public AssessmentPage WaitForSectionToLoad(string SectionIdentifier)
        {
            WaitForElementVisible(SectionTitle(SectionIdentifier));

            return this;
        }
        public AssessmentPage WaitForSubSectionToLoad(string SectionIdentifier)
        {
            WaitForElementVisible(SubSectionTitle(SectionIdentifier));

            return this;
        }


        public AssessmentPage ValidateSectionTitleText(string SectionIdentifier, string ExpectedText)
        {
            ValidateElementText(SectionTitle(SectionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateSubSectionTitleText(string SectionIdentifier, string ExpectedText)
        {
            ValidateElementText(SubSectionTitle(SectionIdentifier), ExpectedText);

            return this;
        }


        public AssessmentPage ValidateQuestionTitleVisibility(string SectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(QuestionTitle(SectionQuestionIdentifier));
            else
                WaitForElementNotVisible(QuestionTitle(SectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateQuestionTitleText(string SectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(QuestionTitle(SectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateQuestionInstructionsVisibility(string SectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(QuestionInstructions(SectionQuestionIdentifier));
            else
                WaitForElementNotVisible(QuestionInstructions(SectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateQuestionInstructionsText(string SectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(QuestionInstructions(SectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateSubSectionQuestionTitleVisibility(string SectionQuestionIdentifier, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SubSectionQuestionTitle(SectionQuestionIdentifier));
            else
                WaitForElementNotVisible(SubSectionQuestionTitle(SectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateSubSectionQuestionTitleText(string SectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(SubSectionQuestionTitle(SectionQuestionIdentifier), ExpectedText);

            return this;
        }



        public AssessmentPage ValidateTableWithHeaderQuestion_TableTitle(string DocumentSectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(TableWithHeaderQuestion_TableTitle(DocumentSectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithHeaderQuestion_QuestionHeadingVisibility(string DocumentSectionQuestionIdentifier, string HeadingText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithHeaderQuestion_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText));
            else
                WaitForElementNotVisible(TableWithHeaderQuestion_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText), 7);

            return this;
        }



        public AssessmentPage ValidateTableWithPrimaryQuestion_TableTitle(string DocumentSectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(TableWithPrimaryQuestion_TableTitle(DocumentSectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithPrimaryQuestion_RowHeadingVisibility(string DocumentSectionQuestionIdentifier, string HeadingText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithPrimaryQuestion_RowHeading(DocumentSectionQuestionIdentifier, HeadingText));
            else
                WaitForElementNotVisible(TableWithPrimaryQuestion_RowHeading(DocumentSectionQuestionIdentifier, HeadingText), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithPrimaryQuestion_RowSubHeadingVisibility(string DocumentSectionQuestionIdentifier, string SubHeadingText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithPrimaryQuestion_RowSubHeading(DocumentSectionQuestionIdentifier, SubHeadingText));
            else
                WaitForElementNotVisible(TableWithPrimaryQuestion_RowSubHeading(DocumentSectionQuestionIdentifier, SubHeadingText), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithPrimaryQuestion_QuestionHeadingVisibility(string DocumentSectionQuestionIdentifier, string HeadingText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithPrimaryQuestion_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText));
            else
                WaitForElementNotVisible(TableWithPrimaryQuestion_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText), 7);

            return this;
        }



        public AssessmentPage ValidateTableWithQuestionsPerCell_TableTitle(string DocumentSectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(TableWithQuestionsPerCell_TableTitle(DocumentSectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithQuestionsPerCell_QuestionHeading(string DocumentSectionQuestionIdentifier, string HeadingText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithQuestionsPerCell_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText));
            else
                WaitForElementNotVisible(TableWithQuestionsPerCell_QuestionHeading(DocumentSectionQuestionIdentifier, HeadingText), 7);

            return this;
        }




        public AssessmentPage ValidateRadioButtonQuestionOptionDisabled(string SectionQuestionIdentifier, string Label, bool ExpectDisabled)
        {
            if(ExpectDisabled)
                ValidateElementDisabled(RadioButtonQuestionOption(SectionQuestionIdentifier, Label));
            else
                ValidateElementEnabled(RadioButtonQuestionOption(SectionQuestionIdentifier, Label));

            return this;
        }
        public AssessmentPage ValidateRadioButtonQuestionTextVisibility(string SectionQuestionIdentifier, string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonQuestionText(SectionQuestionIdentifier, Label));
            else
                WaitForElementNotVisible(RadioButtonQuestionText(SectionQuestionIdentifier, Label), 7);

            return this;
        }
        public AssessmentPage ValidateRadioButtonQuestionOptionChecked(string SectionQuestionIdentifier, string Value, bool ExpectOptionChecked)
        {
            if (ExpectOptionChecked)
                ValidateElementChecked(RadioButtonQuestionOption(SectionQuestionIdentifier, Value));
            else
                ValidateElementNotChecked(RadioButtonQuestionOption(SectionQuestionIdentifier, Value));

            return this;
        }
        public AssessmentPage ClickRadioButtonQuestionOption(string SectionQuestionIdentifier, string Value)
        {
            Click(RadioButtonQuestionOption(SectionQuestionIdentifier, Value));

            return this;
        }



        public AssessmentPage ValidateMosaicQuestionInputDisabled(string DocumentQuestionIdentifier, bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(MosaicQuestionInput(DocumentQuestionIdentifier));
            else
                ValidateElementEnabled(MosaicQuestionInput(DocumentQuestionIdentifier));

            return this;
        }
        public AssessmentPage ValidateMosaicQuestionInputValue(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(MosaicQuestionInput(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }
        public AssessmentPage ValidateMosaicQuestionInputVisibility(string DocumentQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(MosaicQuestionInput(DocumentQuestionIdentifier));
            else
                WaitForElementNotVisible(MosaicQuestionInput(DocumentQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage InsertQuestionInputValue(string DocumentQuestionIdentifier, string ValueToInsert)
        {
            SendKeys(QuestionInput(DocumentQuestionIdentifier), ValueToInsert);

            return this;
        }




        public AssessmentPage ValidateMultiResponseQuestionDisabled(string SectionQuestionIdentifier, bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(MultiResponseQuestionMosaicTopElement(SectionQuestionIdentifier));
            else
                ValidateElementEnabled(MultiResponseQuestionMosaicTopElement(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ValidateMultiResponseQuestionVisibility(string SectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(MultiResponseQuestionPicklist(SectionQuestionIdentifier));
                WaitForElementVisible(MultiResponseQuestionAddButton(SectionQuestionIdentifier));
            }
            else
            {
                WaitForElementNotVisible(MultiResponseQuestionPicklist(SectionQuestionIdentifier), 7);
                WaitForElementNotVisible(MultiResponseQuestionAddButton(SectionQuestionIdentifier), 7);
            }

            return this;
        }
        public AssessmentPage ClickMultiResponseQuestionPicklist(string SectionQuestionIdentifier)
        {
            Click(MultiResponseQuestionPicklist(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickMultiResponseQuestionAddButton(string SectionQuestionIdentifier)
        {
            Click(MultiResponseQuestionAddButton(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickMultiResponseQuestionOption(string SectionQuestionIdentifier, string MultiOptionText)
        {
            WaitForElementToBeClickable(MultiResponseQuestionOption(SectionQuestionIdentifier, MultiOptionText));
            Click(MultiResponseQuestionOption(SectionQuestionIdentifier, MultiOptionText));

            return this;
        }
        public AssessmentPage ValidateMultiResponseQuestionAddedOptionVisibility(string SectionQuestionIdentifier, string SelectedMultiOptionText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(MultiResponseQuestionAddedOption(SectionQuestionIdentifier, SelectedMultiOptionText));
            else
                WaitForElementNotVisible(MultiResponseQuestionAddedOption(SectionQuestionIdentifier, SelectedMultiOptionText), 7);

            return this;
        }
        public AssessmentPage ClickMultiResponseQuestionAddedOptionRemoveButton(string SectionQuestionIdentifier, string SelectedMultiOptionText)
        {
            WaitForElementToBeClickable(MultiResponseQuestionAddedOptionRemoveButton(SectionQuestionIdentifier, SelectedMultiOptionText));
            Click(MultiResponseQuestionAddedOptionRemoveButton(SectionQuestionIdentifier, SelectedMultiOptionText));

            return this;
        }








        public AssessmentPage ValidateTableWithUnlimitedRows_TableTitleText(string DocumentSectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(TableWithUnlimitedRows_TableTitle(DocumentSectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_TableTitleVisibility(string DocumentSectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithUnlimitedRows_TableTitle(DocumentSectionQuestionIdentifier));
            else
                WaitForElementNotVisible(TableWithUnlimitedRows_TableTitle(DocumentSectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_TableSubTitleText(string DocumentSectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(TableWithUnlimitedRows_TableSubTitle(DocumentSectionQuestionIdentifier), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_TableSubTitleVisibility(string DocumentSectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithUnlimitedRows_TableSubTitle(DocumentSectionQuestionIdentifier));
            else
                WaitForElementNotVisible(TableWithUnlimitedRows_TableSubTitle(DocumentSectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_QuestionHeadingText(string DocumentQuestionIdentifier, int RowPosition, string ExpectedText)
        {
            ValidateElementText(TableWithUnlimitedRows_QuestionHeading(DocumentQuestionIdentifier, RowPosition), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_QuestionHeadingVisibility(string DocumentQuestionIdentifier, int RowPosition, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithUnlimitedRows_QuestionHeading(DocumentQuestionIdentifier, RowPosition));
            else
                WaitForElementNotVisible(TableWithUnlimitedRows_QuestionHeading(DocumentQuestionIdentifier, RowPosition), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_AddButtonVisibility(string DocumentSectionQuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithUnlimitedRows_AddButton(DocumentSectionQuestionIdentifier));
            else
                WaitForElementNotVisible(TableWithUnlimitedRows_AddButton(DocumentSectionQuestionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateTableWithUnlimitedRows_RemoveButtonVisibility(string DocumentSectionQuestionIdentifier, int RowPosition, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TableWithUnlimitedRows_RemoveButton(DocumentSectionQuestionIdentifier, RowPosition));
            else
                WaitForElementNotVisible(TableWithUnlimitedRows_RemoveButton(DocumentSectionQuestionIdentifier, RowPosition), 7);

            return this;
        }
        public AssessmentPage ClickTableWithUnlimitedRows_AddButton(string DocumentSectionQuestionIdentifier)
        {
            WaitForElementToBeClickable(TableWithUnlimitedRows_AddButton(DocumentSectionQuestionIdentifier));
            System.Threading.Thread.Sleep(500);
            Click(TableWithUnlimitedRows_AddButton(DocumentSectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickTableWithUnlimitedRows_RemoveButton(string DocumentSectionQuestionIdentifier, int RowPosition)
        {
            WaitForElementToBeClickable(TableWithUnlimitedRows_RemoveButton(DocumentSectionQuestionIdentifier, RowPosition));
            System.Threading.Thread.Sleep(500);
            Click(TableWithUnlimitedRows_RemoveButton(DocumentSectionQuestionIdentifier, RowPosition));

            return this;
        }






        public AssessmentPage ValidateMosaicDateQuestionInputDisabled(string DocumentQuestionIdentifier, bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(MosaicDateQuestionInput(DocumentQuestionIdentifier));
            else
                ValidateElementEnabled(MosaicDateQuestionInput(DocumentQuestionIdentifier));

            return this;
        }
        public AssessmentPage ValidateMosaicDateQuestionInputValue(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(MosaicDateQuestionInput(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }
        public AssessmentPage ValidateMosaicDateQuestionHiddenInputValue(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(MosaicDateQuestionHiddenInput(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }
        public AssessmentPage InsertDateQuestion(string SectionQuestionIdentifier, string ValueToInsert)
        {
            if(string.IsNullOrEmpty(ValueToInsert))
            {
                this.SetElementAttribute(SectionQuestionIdentifier, "dirty", "true");
            }

            SendKeys(DateQuestionInput(SectionQuestionIdentifier), ValueToInsert);
            Click(DateQuestionRightSideIcon(SectionQuestionIdentifier));

            return this;
        }



        public AssessmentPage ValidateMosaicTimeQuestionInputDisabled(string DocumentQuestionIdentifier, bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(MosaicTimeQuestionInput(DocumentQuestionIdentifier));
            else
                ValidateElementEnabled(MosaicTimeQuestionInput(DocumentQuestionIdentifier));

            return this;
        }
        public AssessmentPage ValidateMosaicTimeQuestionInputValue(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(MosaicTimeQuestionInput(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }
        public AssessmentPage ValidateMosaicTimeQuestionHiddenInputValue(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(MosaicTimeQuestionHiddenInput(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }
        public AssessmentPage InsertTimeQuestion(string SectionQuestionIdentifier, string ValueToInsert)
        {
            SendKeys(TimeQuestionInput(SectionQuestionIdentifier), ValueToInsert);

            return this;
        }




        public AssessmentPage ValidatePicklistQuestionDisabled(string SectionQuestionIdentifier, bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(PicklistQuestionMosaicTopElement(SectionQuestionIdentifier));
            else
                ValidateElementEnabled(PicklistQuestionMosaicTopElement(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickPicklistQuestionPicklist(string SectionQuestionIdentifier)
        {
            WaitForElementToBeClickable(PicklistQuestionPicklist(SectionQuestionIdentifier));
            Click(PicklistQuestionPicklist(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickPicklistQuestion_ClearButton(string SectionQuestionIdentifier)
        {
            WaitForElementToBeClickable(PicklistQuestion_ClearButton(SectionQuestionIdentifier));
            Click(PicklistQuestion_ClearButton(SectionQuestionIdentifier));

            return this;
        }
        public AssessmentPage ClickPicklistQuestionOption(string SectionQuestionIdentifier, string OptionText)
        {
            WaitForElementToBeClickable(PicklistQuestionOption(SectionQuestionIdentifier, OptionText));
            Click(PicklistQuestionOption(SectionQuestionIdentifier, OptionText));

            return this;
        }
        public AssessmentPage ValidatePicklistQuestionSelectedText(string SectionQuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(PicklistQuestionPicklist(SectionQuestionIdentifier), ExpectedText);

            return this;
        }



        public AssessmentPage ValidateQuestionWarningVisibility(string QuestionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(QuestionWarning(QuestionIdentifier));
            else
                WaitForElementNotVisible(QuestionWarning(QuestionIdentifier), 7);

            return this;
        }

        public AssessmentPage ValidateQuestionWarningText(string QuestionIdentifier, string ExpectedText)
        {
            ValidateElementText(QuestionWarning(QuestionIdentifier), ExpectedText);

            return this;
        }



        public AssessmentPage ValidateAlertMessageVisibility(string ExpectedText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            { 
                WaitForElementVisible(alertMessage);
                ValidateElementText(alertMessage, ExpectedText);
            }
            else
            { 
                WaitForElementNotVisible(alertMessage, 7);
            }

            return this;
        }


        public AssessmentPage ValidateSaveButtonVisibility(string SectionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(saveButton(SectionIdentifier));
            else
                WaitForElementNotVisible(saveButton(SectionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateSaveAndNextButtonVisibility(string SectionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(saveAndNextButton(SectionIdentifier));
            else
                WaitForElementNotVisible(saveAndNextButton(SectionIdentifier), 7);

            return this;
        }
        public AssessmentPage ValidateViewInPDFButtonVisibility(string SectionIdentifier, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(viewInPDFButton(SectionIdentifier));
            else
                WaitForElementNotVisible(viewInPDFButton(SectionIdentifier), 7);

            return this;
        }



        public AssessmentPage ClickSectionLink(string SectionIdentifier)
        {
            WaitForElementToBeClickable(SectionLink(SectionIdentifier));
            MoveToElementInPage("CWS_" + SectionIdentifier + "-tab");
            Click(SectionLink(SectionIdentifier));

            return this;
        }
        public AssessmentPage ClickSaveAndPreviousButton(string SectionIdentifier)
        {
            Click(saveAndPreviousButton(SectionIdentifier));

            return this;
        }
        public AssessmentPage ClickSaveButton(string SectionIdentifier)
        {
            Click(saveButton(SectionIdentifier));

            WaitForElementNotVisible(loaderIcon, 15);

            return this;
        }
        public AssessmentPage ClickSaveAndNextButton(string SectionIdentifier, bool WaitForLoaderIconNotVisible = true)
        {
            WaitForElementToBeClickable(saveAndNextButton(SectionIdentifier));
            Click(saveAndNextButton(SectionIdentifier));

            if(WaitForLoaderIconNotVisible)
                WaitForElementNotVisible(loaderIcon, 15);

            return this;
        }
        public AssessmentPage ClickViewInPDFButton(string SectionIdentifier)
        {
            WaitForElementToBeClickable(viewInPDFButton(SectionIdentifier));
            System.Threading.Thread.Sleep(2000);
            Click(viewInPDFButton(SectionIdentifier));

            WaitForElementNotVisible(loaderIcon, 15);

            return this;
        }
        public AssessmentPage ClickSubmitButton(string SectionIdentifier)
        {
            System.Threading.Thread.Sleep(1000);
            WaitForElementToBeClickable(submitButton(SectionIdentifier));
            Click(submitButton(SectionIdentifier));

            WaitForElementNotVisible(loaderIcon, 15);

            return this;
        }
        public AssessmentPage ClickSubmitButtonWithoutWaitingForLoaderIconRemoved(string SectionIdentifier)
        {
            System.Threading.Thread.Sleep(1000);
            WaitForElementToBeClickable(submitButton(SectionIdentifier));
            Click(submitButton(SectionIdentifier));

            return this;
        }
        public AssessmentPage ValidateSubmitButtonVisibility(bool ExpectedVisible, string SectionIdentifier)
        {
            if (ExpectedVisible)
                WaitForElementVisible(submitButton(SectionIdentifier));
            else
                WaitForElementNotVisible(submitButton(SectionIdentifier), 7);

            return this;
        }




        public AssessmentPage ValidateWarningMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningMessage);
            else
                WaitForElementNotVisible(warningMessage, 7);

            return this;
        }
        public AssessmentPage ValidateWarningMessageBaseVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningMessageBase);
            else
                WaitForElementNotVisible(warningMessageBase, 7);

            return this;
        }
        public AssessmentPage ValidateWarningSectionInformationVisibility(int SectionPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningSectionInformation(SectionPosition));
            else
                WaitForElementNotVisible(warningSectionInformation(SectionPosition), 7);

            return this;
        }
        public AssessmentPage ValidateWarningQuestionInformationVisibility(int SectionPosition, int QuestionPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(warningQuestionInformation(SectionPosition, QuestionPosition));
            else
                WaitForElementNotVisible(warningQuestionInformation(SectionPosition, QuestionPosition), 7);

            return this;
        }
        public AssessmentPage ValidateWarningMessageText(string ExpectedText)
        {
            ValidateElementText(warningMessage, ExpectedText);

            return this;
        }
        public AssessmentPage ValidateWarningMessageBaseText(string ExpectedText)
        {
            ValidateElementText(warningMessageBase, ExpectedText);

            return this;
        }
        public AssessmentPage ValidateWarningSectionInformationText(int SectionPosition, string ExpectedText)
        {
            ValidateElementText(warningSectionInformation(SectionPosition), ExpectedText);

            return this;
        }
        public AssessmentPage ValidateWarningQuestionInformationText(int SectionPosition, int QuestionPosition, string ExpectedText)
        {
            ValidateElementText(warningQuestionInformation(SectionPosition, QuestionPosition), ExpectedText);

            return this;
        }
        public AssessmentPage ClickWarningQuestionInformation(int SectionPosition, int QuestionPosition)
        {
            WaitForElementToBeClickable(warningQuestionInformation(SectionPosition, QuestionPosition));
            Click(warningQuestionInformation(SectionPosition, QuestionPosition));
            System.Threading.Thread.Sleep(1000);
            return this;
        }

    }

}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewFormsRecordPage : CommonMethods
    {
        public StaffReviewFormsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By staffReviewForm_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreviewform&')]");
        readonly By assessmentDialog_IFrame = By.Id("iframe_CWAssessmentDialog");

        readonly By staffReviewId_LookUp = By.Id("CWLookupBtn_staffreviewid");
        readonly By documentIdLookUp = By.XPath("//div/button[@id='CWLookupBtn_documentid']");
        readonly By ownerid_LookUp = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleuserid_LookUp = By.Id("CWLookupBtn_responsibleuserid");

        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By dueDate_Field = By.Id("CWField_duedate");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");

        readonly By status_id = By.Id("CWField_assessmentstatusid");

        readonly By SaveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By editAssessmentButton = By.XPath("//*[@id='TI_EditAssessmentButton']");
        readonly By editAssessmentSaveAndClose_Button = By.Id("TI_CWAssessmentSaveAndCloseButton");
        readonly By validateForm_Mandatory = By.XPath("//span[text()='Please fill out this field.']/parent::label");
        By questionIdentifierTextarea_Field(string questionID) => By.XPath("//*[@queidentifierid ='" + questionID + "']");
        By questionIdentifierTextarea_Label(string questionID) => By.XPath("//*[@queidentifierid ='" + questionID + "']/parent::li/preceding-sibling::li/label");
        By questionIdentifierYes_RadioBtn(string text) => By.XPath("//label[text()='" + text + "']/parent::li/following-sibling::li//span/input[@title='Yes']");

        readonly By Type_Picklist = By.XPath("//select[@id='CW-DQ-31154']");
        readonly By Category_Picklist = By.XPath("//select[@id='CW-DQ-31155']");
        readonly By ReceivedBy_Picklist = By.XPath("//select[@id='CW-DQ-31156']");

        By ComplaintNameTextarea_Label => By.XPath("//*[@id='CW-DQ-31157']");
        By DetailsTextarea_Label => By.XPath("//*[@id='CW-DQ-31158']");
        By ImmediateActionTextarea_Label => By.XPath("//*[@id='CW-DQ-31159']");
        By InvestigationConductedByTextarea_Label => By.XPath("//*[@id='CW-DQ-31160']");

        By InvestigationFindingsByTextarea_Label => By.XPath("//*[@id='CW-DQ-31161']");

        By ActionSummaryTextarea_Label => By.XPath("//table[@class='table-question']//th[@cellid='CW-DQ-31163']");
        By ActionToBeTakenTextarea_Label => By.XPath("//table[@class='table-question']//th[@cellid='CW-DQ-31164']");
        By LeadNameTextarea_Label => By.XPath("//table[@class='table-question']//th[@cellid='CW-DQ-31165']");
        By TargetDateToBeCompletedTextarea_Label => By.XPath("//table[@class='table-question']//th[@cellid='CW-DQ-31166']");
        By ActualDateToBeCompletedTextarea_Label => By.XPath("//table[@class='table-question']//th[@cellid='CW-DQ-31167']");

        By Informalverbalwarningcheckbox => By.XPath("//input[@id='CW-DQ-31170']");
        By ExtensionOfProbationaryPeriodcheckbox => By.XPath("//input[@title='Extension of probationary period']");
        By Verbalwarningcheckbox => By.XPath("//input[@title='Verbal warning']");

        By Writtenwarningcheckbox => By.XPath("//input[@title='Written warning']");
        By FinalWrittenwarningcheckbox => By.XPath("//input[@title='Final written warning']");

        By Dismisalcheckbox => By.XPath("//input[@title='Dismisal']");
        By Upheldcheckbox => By.XPath("//input[@title='Upheld']");
        By Notsupportedcheckbox => By.XPath("//input[@title='Not supported']");
        By Othercheckbox => By.XPath("//input[@title='Other']");
        By PolicyRProcedureImplicationsTextArea => By.XPath("//*[@id='CW-DQ-31169']");


        public StaffReviewFormsRecordPage WaitForStaffReviewFormsRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(staffReviewForm_IFrame);
            SwitchToIframe(staffReviewForm_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public StaffReviewFormsRecordPage WaitForEditAssessmentPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(staffReviewForm_IFrame);
            SwitchToIframe(staffReviewForm_IFrame);

            WaitForElement(assessmentDialog_IFrame);
            SwitchToIframe(assessmentDialog_IFrame);

            WaitForElement(editAssessmentSaveAndClose_Button);

            WaitForElementNotVisible("CWRefreshPanel", 20);
            return this;
        }
        public StaffReviewFormsRecordPage ClickDocumentIdLookUp()
        {
            WaitForElementToBeClickable(documentIdLookUp);
            Click(documentIdLookUp);

            return this;
        }
        public StaffReviewFormsRecordPage InsertStartDate(string keys)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, keys);

            return this;
        }

        public StaffReviewFormsRecordPage InsertQuestionTextArea(string id, string keys)
        {
            WaitForElement(questionIdentifierTextarea_Field(id));
            SendKeys(questionIdentifierTextarea_Field(id), keys);

            return this;
        }

        public StaffReviewFormsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndClose_Button);
            Click(SaveAndClose_Button);

            return this;
        }
        public StaffReviewFormsRecordPage ClickEditAssessmentSaveAndCloseButton()
        {
            WaitForElement(editAssessmentSaveAndClose_Button);
            Click(editAssessmentSaveAndClose_Button);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateQuestionTextAreaLabel(string id, string ExpectedText)
        {
            WaitForElement(questionIdentifierTextarea_Label(id));
            ValidateElementText(questionIdentifierTextarea_Label(id), ExpectedText);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateDeleteRecordButton(string ExpectedText)
        {
            WaitForElement(DeleteRecord_Button);
            ValidateElementText(DeleteRecord_Button, ExpectedText);

            return this;
        }
        public StaffReviewFormsRecordPage ClickAdditionalItemsMenuButton()
        {
            WaitForElementToBeClickable(additionalItemsMenuButton);
            this.Click(additionalItemsMenuButton);

            return this;
        }
        public StaffReviewFormsRecordPage ClickDeleteRecordButton()
        {
            WaitForElement(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }
        public StaffReviewFormsRecordPage ClickEditAssessmentButton()
        {
            WaitForElement(editAssessmentButton);
            Click(editAssessmentButton);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateExistsFormRecordEditable()
        {
            WaitForElement(startDate_Field);
            ValidateElementEnabled(startDate_Field);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateAllFieldsDisableMode()
        {
            WaitForElement(staffReviewId_LookUp);
            ValidateElementDisabled(staffReviewId_LookUp);

            WaitForElement(startDate_Field);
            ValidateElementDisabled(startDate_Field);

            WaitForElement(documentIdLookUp);
            ValidateElementDisabled(documentIdLookUp);

            WaitForElement(ownerid_LookUp);
            ValidateElementDisabled(ownerid_LookUp);

            WaitForElement(responsibleuserid_LookUp);
            ValidateElementDisabled(responsibleuserid_LookUp);

            WaitForElement(dueDate_Field);
            ValidateElementDisabled(dueDate_Field);

            WaitForElement(reviewDate_Field);
            ValidateElementDisabled(reviewDate_Field);

            WaitForElement(status_id);
            ValidateElementDisabled(status_id);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateDeleteRecordButtonNotDisplay()
        {
            ValidateElementDoNotExist(DeleteRecord_Button);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateMandatoryErrorMsg(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(validateForm_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(validateForm_Mandatory, 3);
            }
            return this;
        }

        public StaffReviewFormsRecordPage ClickYesRadioButton(string text)
        {

            WaitForElement(questionIdentifierYes_RadioBtn(text));
            Click(questionIdentifierYes_RadioBtn(text));

            return this;
        }

        public StaffReviewFormsRecordPage ValidateTypeContainsElement(string ElementTextToFind)
        {

            WaitForElement(Type_Picklist);
            ValidatePicklistContainsElementByText(Type_Picklist, ElementTextToFind);

            return this;
        }

        public StaffReviewFormsRecordPage SelectCategoryVale(string optionToSelect)
        {

            WaitForElement(Category_Picklist);

            SelectPicklistElementByText(Category_Picklist, optionToSelect);

            return this;
        }

        public StaffReviewFormsRecordPage SelectReceivedByVale(string optionToSelect)
        {

            WaitForElement(ReceivedBy_Picklist);

            SelectPicklistElementByText(ReceivedBy_Picklist, optionToSelect);

            return this;
        }
        public StaffReviewFormsRecordPage SelectTypeVale(string optionToSelect)
        {

            WaitForElement(Type_Picklist);

            SelectPicklistElementByText(Type_Picklist, optionToSelect);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateCategoryContainsElement(string ElementTextToFind)
        {

            WaitForElementVisible(Category_Picklist);

            ValidatePicklistContainsElementByText(Category_Picklist, ElementTextToFind);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateReceivedByContainsElement(string ElementTextToFind)
        {

            WaitForElementVisible(ReceivedBy_Picklist);

            ValidatePicklistContainsElementByText(ReceivedBy_Picklist, ElementTextToFind);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateTextAreaLabel(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ComplaintNameTextarea_Label);
                WaitForElementVisible(DetailsTextarea_Label);
                WaitForElementVisible(ImmediateActionTextarea_Label);
                WaitForElementVisible(InvestigationConductedByTextarea_Label);
                WaitForElementVisible(InvestigationFindingsByTextarea_Label);
            }
            else
            {
                WaitForElementNotVisible(ComplaintNameTextarea_Label, 3);
                WaitForElementNotVisible(DetailsTextarea_Label, 3);
                WaitForElementNotVisible(ImmediateActionTextarea_Label, 3);
                WaitForElementNotVisible(InvestigationConductedByTextarea_Label, 3);
                WaitForElementNotVisible(InvestigationFindingsByTextarea_Label, 3);
            }

            return this;
        }
        public StaffReviewFormsRecordPage InsertDetailsNInvestigationTextAreaLabel(string complaintname, string details, string ImmediateAction, string InvestigationConductedBy, string InvestigationFindingsBy)
        {

            WaitForElement(ComplaintNameTextarea_Label);
            SendKeys(ComplaintNameTextarea_Label, complaintname);
            WaitForElement(DetailsTextarea_Label);
            SendKeys(DetailsTextarea_Label, details);
            WaitForElement(ImmediateActionTextarea_Label);
            SendKeys(ImmediateActionTextarea_Label, ImmediateAction);
            WaitForElement(InvestigationConductedByTextarea_Label);
            SendKeys(InvestigationConductedByTextarea_Label, InvestigationConductedBy);
            WaitForElement(InvestigationFindingsByTextarea_Label);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateActionsTextAreaLabel(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ActionSummaryTextarea_Label);
                WaitForElementVisible(ActionToBeTakenTextarea_Label);
                WaitForElementVisible(LeadNameTextarea_Label);
                WaitForElementVisible(TargetDateToBeCompletedTextarea_Label);
                WaitForElementVisible(ActualDateToBeCompletedTextarea_Label);
            }
            else
            {
                WaitForElementNotVisible(ActionSummaryTextarea_Label, 3);
                WaitForElementNotVisible(ActionToBeTakenTextarea_Label, 3);
                WaitForElementNotVisible(LeadNameTextarea_Label, 3);
                WaitForElementNotVisible(TargetDateToBeCompletedTextarea_Label, 3);
                WaitForElementNotVisible(ActualDateToBeCompletedTextarea_Label, 3);
            }

            return this;
        }
        public StaffReviewFormsRecordPage SelectOutcomeCheckboxVisible()
        {
            WaitForElementVisible(Informalverbalwarningcheckbox);
            Click(Informalverbalwarningcheckbox);
            return this;

        }
        public StaffReviewFormsRecordPage ValidateOutcomeCheckboxVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {

                WaitForElementVisible(Informalverbalwarningcheckbox);
                WaitForElementVisible(ExtensionOfProbationaryPeriodcheckbox);

                WaitForElementVisible(Verbalwarningcheckbox);

                WaitForElementVisible(Writtenwarningcheckbox);

                WaitForElementVisible(FinalWrittenwarningcheckbox);
                WaitForElementVisible(Dismisalcheckbox);
                WaitForElementVisible(Upheldcheckbox);
                WaitForElementVisible(Notsupportedcheckbox);
                WaitForElementVisible(Othercheckbox);

            }

            else
            {
                WaitForElementNotVisible(Informalverbalwarningcheckbox, 3);
                WaitForElementNotVisible(ExtensionOfProbationaryPeriodcheckbox, 3);
                WaitForElementNotVisible(Verbalwarningcheckbox, 3);
                WaitForElementNotVisible(Writtenwarningcheckbox, 3);
                WaitForElementNotVisible(FinalWrittenwarningcheckbox, 3);
                WaitForElementNotVisible(Dismisalcheckbox, 3);
                WaitForElementNotVisible(Upheldcheckbox, 3);
                WaitForElementNotVisible(Notsupportedcheckbox, 3);
                WaitForElementNotVisible(Othercheckbox, 3);
            }


            return this;
        }
        public StaffReviewFormsRecordPage ValidatePolicyRProcedureImplicationsTextArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {

                WaitForElementVisible(PolicyRProcedureImplicationsTextArea);

            }

            else
            {
                WaitForElementNotVisible(PolicyRProcedureImplicationsTextArea, 3);
            }
            return this;

        }
        public StaffReviewFormsRecordPage InsertPolicyRProcedureImplicationsTextArea(string PolicyRProcedureImplications)
        {

            WaitForElement(PolicyRProcedureImplicationsTextArea);

            SendKeys(PolicyRProcedureImplicationsTextArea, PolicyRProcedureImplications);
            return this;
        }


        public StaffReviewFormsRecordPage ValidateDetailsTextAreaLabel(string ExpectedText)
        {
            WaitForElement(DetailsTextarea_Label);
            ValidateElementText(DetailsTextarea_Label, ExpectedText);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateImmediateActionTextAreaLabel(string ExpectedText)
        {
            WaitForElement(ImmediateActionTextarea_Label);
            ValidateElementText(ImmediateActionTextarea_Label, ExpectedText);

            return this;
        }
        public StaffReviewFormsRecordPage ValidateIvestigationConductedByTextAreaLabel(string ExpectedText)
        {
            WaitForElement(InvestigationConductedByTextarea_Label);
            ValidateElementText(InvestigationConductedByTextarea_Label, ExpectedText);

            return this;
        }

        public StaffReviewFormsRecordPage ValidateIvestigationFindingsTextAreaLabel(string ExpectedText)
        {
            WaitForElement(InvestigationFindingsByTextarea_Label);
            ValidateElementText(InvestigationFindingsByTextarea_Label, ExpectedText);

            return this;
        }
    }
}

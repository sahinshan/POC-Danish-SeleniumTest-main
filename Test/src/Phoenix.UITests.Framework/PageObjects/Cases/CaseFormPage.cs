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
    /// This class represents a "Case Form" record when accessed from a Case record.
    /// The "Case Form" page is the page displayed when a User:
    /// --Open a Case Record
    /// --Navigate to the Case Forms sub section
    /// --Tap on the "New" button to create a new Case Form or Open an existing record
    /// </summary>
    public class CaseFormPage : CommonMethods
    {
        public CaseFormPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newCaseFormIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]");
        readonly By actionOutcomeIFrame = By.Id("CWIFrame_FormActionsGrid");


        #endregion

        #region Dinamic Dialog popup

        By dinamicDialogTitle(string ExpectedTitle) => By.XPath("//div[@id='CWDynamicDialog']/header/div/h1[text()='" + ExpectedTitle + "']");

        By dinamicDialogMessage(string ExpectedMessage) => By.XPath("//div[@id='CWDynamicDialog']/main/div[text()='" + ExpectedMessage + "']");

        readonly By dinamicDialogOKButton = By.XPath("//div[@id='CWDynamicDialog']/footer/input[@type='button'][@value='OK']");

        #endregion
        By PersonRow(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[2]");
        By PersonRowCheckBox(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[1]/input");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Form (Case): ']/span");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");
        readonly By viewAssessmentButton = By.Id("TI_ViewAssessmentButton");
        readonly By printAssessmentButton = By.Id("TI_PrintAssessmentButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By printHistoryButton = By.Id("TI_PrintAssessmentHistoryButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_CaseFormCaseNote']");
        readonly By AppointmentsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Appointment']");
        readonly By EmailsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By LettersLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By PhoneCallsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PhoneCall']");
        readonly By TasksLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Task']");
        readonly By AttachmentsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Attachments']");
        readonly By AssessmentFactors_LeftMenu = By.Id("CWNavItem_AssessmentFactor");
        readonly By Members_LeftMenu = By.Id("CWNavItem_CaseFormMember");

        readonly By generalSectionTitle = By.XPath("//div[@id='CWSection_General']/fieldset/div/span[text()='General']");
        readonly By additionalInformationSectionTitle = By.XPath("//div[@id='CWSection_AdditionalInformation']/fieldset/div/span[text()='Additional Information']");
        readonly By actionsOutcomesSectionTitle = By.XPath("//div[@id='CWSection_CaseFormOutcome']/fieldset/div/span[text()='Actions/Outcomes']");
        readonly By completionDetailsSectionTitle = By.XPath("//div[@id='CWSection_CompletionDetails']/fieldset/div/span[text()='Completion Details']");

        

        #region Field Labels

        readonly By caseFieldLabel = By.XPath("//li[@id='CWLabelHolder_caseid']/label[text()='Case']/Span[text()='*']");
        readonly By formTypeFieldLabel = By.XPath("//li[@id='CWLabelHolder_documentid']/label[text()='Form Type']/Span[text()='*']");
        readonly By statusFieldLabel = By.XPath("//li[@id='CWLabelHolder_assessmentstatusid']/label[text()='Status']/Span[text()='*']");
        readonly By startDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_startdate']/label[text()='Start Date']/Span[text()='*']");
        readonly By responsibleTeamFieldLabel = By.XPath("//li[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']/Span[text()='*']");
        readonly By responsibleUserFieldLabel = By.XPath("//li[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By dueDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_duedate']/label[text()='Due Date']");
        readonly By reviewDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_reviewdate']/label[text()='Review Date']");
        readonly By PrecedingFormFieldLabel = By.XPath("//li[@id='CWLabelHolder_precedingformid']/label[text()='Preceding Form']");

        readonly By separateAssessmentFieldLabel = By.XPath("//li[@id='CWLabelHolder_separateassessment']/label[text()='Separate Assessment']");
        readonly By carerdeclinedjointassessmentFieldLabel = By.XPath("//li[@id='CWLabelHolder_carerdeclinedjointassessment']/label[text()='Carer declined joint assessments?']");
        readonly By formdelayreasonidFieldLabel = By.XPath("//li[@id='CWLabelHolder_formdelayreasonid']/label[text()='Delay Reason']");
        readonly By targetstartdateFieldLabel = By.XPath("//li[@id='CWLabelHolder_targetstartdate']/label[text()='Target Start Date']");
        readonly By triggerdateFieldLabel = By.XPath("//li[@id='CWLabelHolder_triggerdate']/label[text()='Trigger Date']");
        readonly By jointcarerassessmentFieldLabel = By.XPath("//li[@id='CWLabelHolder_jointcarerassessment']/label[text()='Joint Carer Assessment']");
        readonly By jointcareridFieldLabel = By.XPath("//li[@id='CWLabelHolder_jointcarerid']/label[text()='Joint Carer']");
        readonly By newpersonFieldLabel = By.XPath("//li[@id='CWLabelHolder_newperson']/label[text()='New Person?']");
        readonly By terminateddateFieldLabel = By.XPath("//li[@id='CWLabelHolder_terminateddate']/label[text()='Terminated Date']");

        readonly By completedByFieldLabel = By.XPath("//*[@id='CWLabelHolder_completedbyid']/label");
        readonly By completedDateFieldLabel = By.XPath("//*[@id='CWLabelHolder_completiondate']/label");
        readonly By signedOffByFieldLabel = By.XPath("//*[@id='CWLabelHolder_signedoffbyid']/label");
        readonly By signedOffDateFieldLabel = By.XPath("//*[@id='CWLabelHolder_signoffdate']/label");


        #endregion

        #region Fields

        readonly By caseField = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/a[@id='CWField_caseid_Link']");
        readonly By caseClearButton = By.Id("CWClearLookup_caseid");
        readonly By caseLookupButton = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/button[@id='CWLookupBtn_caseid']");

        readonly By formTypeField = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/a[@id='CWField_documentid_Link']");
        readonly By formTypeClearButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWClearLookup_documentid']");
        readonly By formTypeLookupButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWLookupBtn_documentid']");

        readonly By statusPicklist = By.Id("CWField_assessmentstatusid");

        readonly By startDateField = By.XPath("//input[@id='CWField_startdate']");
        readonly By completiondate_Field = By.Id("CWField_completiondate");

        readonly By responsibleTeamField = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/a[@id='CWField_ownerid_Link']");
        readonly By responsibleTeamClearButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeamLookupButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUserField = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/a[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUserClearButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUserLookupButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWLookupBtn_responsibleuserid']");

        readonly By dueDateField = By.XPath("//input[@id='CWField_duedate']");

        readonly By reviewDateField = By.XPath("//input[@id='CWField_reviewdate']");

        readonly By PrecedingFormLinkField = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/a[@id='CWField_precedingformid_Link']");
        readonly By PrecedingFormClearButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWClearLookup_precedingformid']");
        readonly By PrecedingFormLookupButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWLookupBtn_precedingformid']");

        readonly By SeparateAssessmentYesOption = By.XPath("//input[@id='CWField_separateassessment_1']");
        readonly By SeparateAssessmentYesOptionChecked = By.XPath("//input[@id='CWField_separateassessment_1'][@checked='checked']");
        readonly By SeparateAssessmentNoOption = By.XPath("//input[@id='CWField_separateassessment_0']");
        readonly By SeparateAssessmentNoOptionChecked = By.XPath("//input[@id='CWField_separateassessment_0'][@checked='checked']");

        readonly By carerdeclinedjointassessmentYesOption = By.XPath("//input[@id='CWField_carerdeclinedjointassessment_1']");
        readonly By carerdeclinedjointassessmentYesOptionChecked = By.XPath("//input[@id='CWField_carerdeclinedjointassessment_1'][@checked='checked']");
        readonly By carerdeclinedjointassessmentNoOption = By.XPath("//input[@id='CWField_carerdeclinedjointassessment_0']");
        readonly By carerdeclinedjointassessmentNoOptionChecked = By.XPath("//input[@id='CWField_carerdeclinedjointassessment_0'][@checked='checked']");

        readonly By delayReasonField = By.XPath("//li[@id='CWControlHolder_formdelayreasonid']/div/div/a[@id='CWField_formdelayreasonid_Link']");
        readonly By delayReasonClearButton = By.XPath("//li[@id='CWControlHolder_formdelayreasonid']/div/div/button[@id='CWClearLookup_formdelayreasonid']");
        readonly By delayReasonLookupButton = By.XPath("//li[@id='CWControlHolder_formdelayreasonid']/div/div/button[@id='CWLookupBtn_formdelayreasonid']");

        readonly By targetStartDateField = By.XPath("//input[@id='CWField_targetstartdate']");
        readonly By targetStartDatePicker = By.XPath("//input[@id='CWField_targetstartdate_DatePicker']");

        readonly By triggerDateField = By.XPath("//input[@id='CWField_triggerdate']");
        readonly By triggerDatePicker = By.XPath("//input[@id='CWField_triggerdate_DatePicker']");

        readonly By jointCarerAssessmentYesOption = By.XPath("//input[@id='CWField_jointcarerassessment_1']");
        readonly By jointCarerAssessmentYesOptionChecked = By.XPath("//input[@id='CWField_jointcarerassessment_1'][@checked='checked']");
        readonly By jointCarerAssessmentNoOption = By.XPath("//input[@id='CWField_jointcarerassessment_0']");
        readonly By jointCarerAssessmentNoOptionChecked = By.XPath("//input[@id='CWField_jointcarerassessment_0'][@checked='checked']");

        readonly By jointCarerField = By.XPath("//li[@id='CWControlHolder_jointcarerid']/div/div/a[@id='CWField_jointcarerid_Link']");

        readonly By newPersonYesOption = By.XPath("//input[@id='CWField_newperson_1']");
        readonly By newPersonYesOptionChecked = By.XPath("//input[@id='CWField_newperson_1'][@checked='checked']");
        readonly By newPersonNoOption = By.XPath("//input[@id='CWField_newperson_0']");
        readonly By newPersonNoOptionChecked = By.XPath("//input[@id='CWField_newperson_0'][@checked='checked']");

        readonly By terminatedDateField = By.XPath("//input[@id='CWField_terminateddate']");
        readonly By terminatedDatePicker = By.XPath("//input[@id='CWField_terminateddate_DatePicker']");


        readonly By completedByField = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/a[@id='CWField_completedbyid_Link']");
        readonly By completedByClearButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWClearLookup_completedbyid']");
        readonly By completedByLookupButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWLookupBtn_completedbyid']");
        readonly By CompletionDetails_Section = By.Id("CWSection_CompletionDetails");

        readonly By completedDateField = By.XPath("//*[@id='CWField_completiondate']");

        readonly By signedOffByField = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/a[@id='CWField_signedoffbyid_Link']");
        readonly By signedOffByClearButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWClearLookup_signedoffbyid']");
        readonly By signedOffByLookupButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWLookupBtn_signedoffbyid']");

        readonly By signedOffDateField = By.XPath("//*[@id='CWField_signoffdate']");
        readonly By signedoffbyid_Field = By.Id("CWField_signedoffbyid_cwname");
        readonly By formcancellationreasonid_LookupButton = By.Id("CWLookupBtn_formcancellationreasonid");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        #endregion

        #region Notification and Error messages

        By WarningMainArea(string ExpactedText) => By.XPath("//div[@id='CWNotificationHolder_DataForm']/div[@id='CWNotificationMessage_DataForm'][text()='" + ExpactedText + "']");

        By caseIDErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_caseid']/label/span[text()='" + ExpactedText + "']");
        By FormTypeErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_documentid']/label/span[text()='" + ExpactedText + "']");
        By startDateErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_startdate']/label/span[text()='" + ExpactedText + "']");
        By responsibleTeamErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_ownerid']/label/span[text()='" + ExpactedText + "']");

        #endregion

        public CaseFormPage OpenCaseFormRecordHyperlink(string CaseFormRecordHyperlink)
        {
            driver.Navigate().GoToUrl(CaseFormRecordHyperlink);

            return this;
        }

        public CaseFormPage WaitForCaseFormPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newCaseFormIFrame);
            SwitchToIframe(newCaseFormIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);
            WaitForElement(generalSectionTitle);

            return this;
        }

        public CaseFormPage WaitForCaseFormRecordPageToLoadFromHyperlink(string ExpectedTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, ExpectedTitle);

            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(MenuButton);
            
            WaitForElement(caseFieldLabel);
            WaitForElement(formTypeFieldLabel);
            WaitForElement(statusFieldLabel);
            WaitForElement(startDateFieldLabel);

            return this;
        }

        public CaseFormPage WaitForActionOutcomePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newCaseFormIFrame);
            SwitchToIframe(newCaseFormIFrame);

            WaitForElement(actionOutcomeIFrame);
            SwitchToIframe(actionOutcomeIFrame);



            return this;
        }

        public CaseFormPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(editAssessmentButton);
            WaitForElement(printAssessmentButton);
            WaitForElement(additionalToolbarElementsButton);

            return this;
        }

        public CaseFormPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            
            WaitForElement(backButton);
            WaitForElement(viewAssessmentButton);
            WaitForElement(printAssessmentButton);
            WaitForElement(printHistoryButton);
            WaitForElement(shareButton);

            return this;
        }

        /// <summary>
        /// Wait for all toolbar buttons to be visible (after a user tapped on the Additional Toolbar Elements Button)
        /// </summary>
        /// <returns></returns>
        public CaseFormPage WaitForAllToolbarIconsToBeVisible()
        {
            Wait.Until(c => c.FindElement(backButton));
            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(saveAndCloseButton));
            Wait.Until(c => c.FindElement(editAssessmentButton));
            Wait.Until(c => c.FindElement(printAssessmentButton));
            Wait.Until(c => c.FindElement(additionalToolbarElementsButton));
            Wait.Until(c => c.FindElement(printHistoryButton));
            Wait.Until(c => c.FindElement(shareButton));
            Wait.Until(c => c.FindElement(assignButton));
            Wait.Until(c => c.FindElement(restrictAccessButton));
            Wait.Until(c => c.FindElement(deleteButton));

            return this;
        }

        public CaseFormPage WaitForCompletionDetailsAreaToBeVisible()
        {
            WaitForElementVisible(completionDetailsSectionTitle);

            WaitForElementVisible(completedByFieldLabel);
            WaitForElementVisible(completedDateFieldLabel);
            WaitForElementVisible(signedOffByFieldLabel);
            WaitForElementVisible(signedOffDateFieldLabel);

            WaitForElementVisible(completedByLookupButton);
            WaitForElementVisible(completedDateField);
            WaitForElementVisible(signedOffByLookupButton);
            WaitForElementVisible(signedOffDateField);
            
            return this;
        }

        #region Validation methods

        public CaseFormPage ValidateAllFieldLabelsVisible()
        {
            Wait.Until(c => c.FindElement(generalSectionTitle));

            Wait.Until(c => c.FindElement(caseFieldLabel));
            Wait.Until(c => c.FindElement(formTypeFieldLabel));
            Wait.Until(c => c.FindElement(statusFieldLabel));
            Wait.Until(c => c.FindElement(startDateFieldLabel));
            Wait.Until(c => c.FindElement(responsibleTeamFieldLabel));
            Wait.Until(c => c.FindElement(responsibleUserFieldLabel));
            Wait.Until(c => c.FindElement(dueDateFieldLabel));
            Wait.Until(c => c.FindElement(reviewDateFieldLabel));
            Wait.Until(c => c.FindElement(PrecedingFormFieldLabel));

            Wait.Until(c => c.FindElement(additionalInformationSectionTitle));

            Wait.Until(c => c.FindElement(separateAssessmentFieldLabel));
            Wait.Until(c => c.FindElement(carerdeclinedjointassessmentFieldLabel));
            Wait.Until(c => c.FindElement(formdelayreasonidFieldLabel));
            Wait.Until(c => c.FindElement(targetstartdateFieldLabel));
            Wait.Until(c => c.FindElement(triggerdateFieldLabel));
            Wait.Until(c => c.FindElement(jointcarerassessmentFieldLabel));
            Wait.Until(c => c.FindElement(jointcareridFieldLabel));
            Wait.Until(c => c.FindElement(newpersonFieldLabel));
            Wait.Until(c => c.FindElement(terminateddateFieldLabel));

            Wait.Until(c => c.FindElement(actionsOutcomesSectionTitle));

            return this;
        }

        public CaseFormPage OpenOtcomeRecord(string PersonId)
        {
            WaitForElement(PersonRow(PersonId));
            driver.FindElement(PersonRow(PersonId)).Click();

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage ValidateCaseFieldLinkText(string ExpectedText)
        {
            ValidateElementText(caseField, ExpectedText);

            return this;
        }

        public CaseFormPage ValidateCaseField(string ExpectedCaseTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string caseTitle = driver.FindElement(caseField).Text;
            bool clearButtonVisible = GetElementVisibility(caseClearButton);
            bool lookupButtonVisible = GetElementVisibility(caseLookupButton);

            Assert.AreEqual(ExpectedCaseTitle, caseTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }

        public CaseFormPage ValidateFormTypeField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string formTypeTitle = driver.FindElement(formTypeField).Text;
            bool clearButtonVisible = GetElementVisibility(formTypeClearButton);
            bool lookupButtonVisible = GetElementVisibility(formTypeLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, formTypeTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }

        public CaseFormPage ValidateStatusField(string ExpectedStatus)
        {
            Wait.Until(c => c.FindElement(statusPicklist));

            var selectElement = new SelectElement(driver.FindElement(statusPicklist));
            Assert.AreEqual(ExpectedStatus, selectElement.SelectedOption.Text);

            return this;
        }

        public CaseFormPage ValidateStartDateField(string ExpectedStartDate)
        {
            Wait.Until(c => c.FindElement(startDateField));

            string startDate = driver.FindElement(startDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedStartDate, startDate);

            return this;
        }

        public CaseFormPage ValidateSignOffDateField(string ExpectedSignOffDate)
        {
            Wait.Until(c => c.FindElement(signedOffDateField));

            string signOffDate = driver.FindElement(signedOffDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedSignOffDate, signOffDate);

            return this;
        }

        public CaseFormPage ValidateResponsibleTeamField(string ExpectedResponsibleTeamTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleTeamTitle = driver.FindElement(responsibleTeamField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleTeamClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleTeamLookupButton);

            Assert.AreEqual(ExpectedResponsibleTeamTitle, responsibleTeamTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }

       

        public CaseFormPage ValidateResponsibleUserField(string ExpectedResponsibleUserTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleUserTitle = driver.FindElement(responsibleUserField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleUserClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleUserLookupButton);

            Assert.AreEqual(ExpectedResponsibleUserTitle, responsibleUserTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }

        public CaseFormPage ValidatePrecedingFormFieldLinkText(string ExpectedText)
        {
            ValidateElementText(PrecedingFormLinkField, ExpectedText);

            return this;
        }

        public CaseFormPage ValidateDueDateField(string ExpectedDueDate)
        {
            Wait.Until(c => c.FindElement(dueDateField));

            string dueDate = driver.FindElement(dueDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedDueDate, dueDate);

            return this;
        }

        public CaseFormPage ValidateReviewDateField(string ExpectedReviewDate)
        {
            Wait.Until(c => c.FindElement(reviewDateField));

            string reviewDate = driver.FindElement(reviewDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedReviewDate, reviewDate);

            return this;
        }

        public CaseFormPage ValidateSeparateAssessmentField(bool SeparateAssessment)
        {
            if (SeparateAssessment)
            {
                Wait.Until(c => c.FindElement(SeparateAssessmentYesOptionChecked));
                Wait.Until(c => c.FindElement(SeparateAssessmentNoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(SeparateAssessmentYesOption));
                Wait.Until(c => c.FindElement(SeparateAssessmentNoOptionChecked));
            }

            return this;
        }

        public CaseFormPage ValidateCarerDeclinedJointAssessmentsField(bool CarerDeclinedJointAssessments)
        {
            if (CarerDeclinedJointAssessments)
            {
                Wait.Until(c => c.FindElement(carerdeclinedjointassessmentYesOptionChecked));
                Wait.Until(c => c.FindElement(carerdeclinedjointassessmentNoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(carerdeclinedjointassessmentYesOption));
                Wait.Until(c => c.FindElement(carerdeclinedjointassessmentNoOptionChecked));
            }

            return this;
        }

        public CaseFormPage ValidateDelayReasonField(string ExpectedDelayReasonTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string delayReasonTitle = driver.FindElement(delayReasonField).Text;
            bool clearButtonVisible = GetElementVisibility(delayReasonClearButton);
            bool lookupButtonVisible = GetElementVisibility(delayReasonLookupButton);

            Assert.AreEqual(ExpectedDelayReasonTitle, delayReasonTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }

        public CaseFormPage ValidateTargetStartDateField(string ExpectedTargetStartDate)
        {
            Wait.Until(c => c.FindElement(targetStartDateField));

            string fieldValue = driver.FindElement(targetStartDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedTargetStartDate, fieldValue);

            return this;
        }

        public CaseFormPage ValidateTriggerDateField(string ExpectedTriggerDate)
        {
            Wait.Until(c => c.FindElement(triggerDateField));

            string fieldValue = driver.FindElement(triggerDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedTriggerDate, fieldValue);

            return this;
        }

        public CaseFormPage ValidateJointCarerAssessmentField(bool JointCarerAssessment)
        {
            if (JointCarerAssessment)
            {
                Wait.Until(c => c.FindElement(jointCarerAssessmentYesOptionChecked));
                Wait.Until(c => c.FindElement(jointCarerAssessmentNoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(jointCarerAssessmentYesOption));
                Wait.Until(c => c.FindElement(jointCarerAssessmentNoOptionChecked));
            }

            return this;
        }

        public CaseFormPage ValidateJointCarerField(string ExpectedJointCarerTitle)
        {
            string jointCarerTitle = driver.FindElement(jointCarerField).Text;
            if (jointCarerTitle != ExpectedJointCarerTitle)
                throw new Exception("Joint Carer do not match.");

            return this;
        }

        public CaseFormPage ValidateNewPersonField(bool NewPerson)
        {
            if (NewPerson)
            {
                Wait.Until(c => c.FindElement(newPersonYesOptionChecked));
                Wait.Until(c => c.FindElement(newPersonNoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(newPersonYesOption));
                Wait.Until(c => c.FindElement(newPersonNoOptionChecked));
            }

            return this;
        }

        public CaseFormPage ValidateTerminatedDateField(string ExpectedTerminatedDate)
        {
            Wait.Until(c => c.FindElement(terminatedDateField));

            string fieldValue = driver.FindElement(terminatedDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedTerminatedDate, fieldValue);

            return this;
        }

        /// <summary>
        /// Validate that a specific warning message is displayed bellow the "Details" tab
        /// </summary>
        /// <param name="ExpectedMessage"></param>
        /// <returns></returns>
        public CaseFormPage ValidateTopAreaWarningMessage(string ExpectedMessage)
        {
            Wait.Until(c => c.FindElement(WarningMainArea(ExpectedMessage)));
            
            return this;
        }

        /// <summary>
        /// Validate that a specific error message is displayed under the Case field
        /// </summary>
        /// <param name="ExpectedMessage"></param>
        /// <returns></returns>
        public CaseFormPage ValidateCaseErrorMessage(string ExpectedMessage)
        {
            Wait.Until(c => c.FindElement(caseIDErrorArea(ExpectedMessage)));

            return this;
        }

        /// <summary>
        /// Validate that a specific error message is displayed under the Form Type field
        /// </summary>
        /// <param name="ExpectedMessage"></param>
        /// <returns></returns>
        public CaseFormPage ValidateFormTypeErrorMessage(string ExpectedMessage)
        {
            Wait.Until(c => c.FindElement(FormTypeErrorArea(ExpectedMessage)));

            return this;
        }

        /// <summary>
        /// Validate that a specific error message is displayed under the Start Date field
        /// </summary>
        /// <param name="ExpectedMessage"></param>
        /// <returns></returns>
        public CaseFormPage ValidateStartDateErrorMessage(string ExpectedMessage)
        {
            Wait.Until(c => c.FindElement(startDateErrorArea(ExpectedMessage)));

            return this;
        }

        /// <summary>
        /// Validate that a specific error message is displayed under the Start Date field
        /// </summary>
        /// <param name="ExpectedMessage"></param>
        /// <returns></returns>
        public CaseFormPage ValidateResponsibleTeamErrorMessage(string ExpectedMessage)
        {
            Wait.Until(c => c.FindElement(responsibleTeamErrorArea(ExpectedMessage)));

            return this;
        }

        #endregion

        public PrintAssessmentPopup TapPrintButton()
        {
            WaitForElementVisible(printAssessmentButton);
            WaitForElementToBeClickable(printAssessmentButton);
            driver.FindElement(printAssessmentButton).Click();

            return new PrintAssessmentPopup(driver, Wait, appURL);
        }


        public CaseFormPage TapClearCaseButton()
        {
            WaitForElementToBeClickable(caseClearButton);
            Click(caseClearButton);

            return this;
        }

        public CaseFormPage TapNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CaseFormPage TapFormTypeLookupButton()
        {
            WaitForElementToBeClickable(formTypeLookupButton);
            Click(formTypeLookupButton);
            
            return this;
        }

        public CaseFormPage TapClearResponsibleTeamButton()
        {
            WaitForElementToBeClickable(responsibleTeamClearButton);
            Click(responsibleTeamClearButton);

            return this;
        }

        public CaseFormPage TapResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUserLookupButton);
            Click(responsibleUserLookupButton);

            return this;
        }

        public CaseFormPage TapPrecedingFormLookupButton()
        {
            WaitForElementToBeClickable(PrecedingFormLookupButton);
            Click(PrecedingFormLookupButton);

            return this;
        }

        public CaseFormPage TapCancelledReasonButton()
        {
            driver.FindElement(formcancellationreasonid_LookupButton).Click();

            return this;
        }

        public CaseFormPage TapPrecedingFormClearButton()
        {
            driver.FindElement(PrecedingFormClearButton).Click();

            return this;
        }

        public CaseFormPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(statusPicklist, TextToSelect);

            return this;
        }

        public CaseFormPage InsertStartDate(string StartDate)
        {
            SendKeys(startDateField, StartDate);

            return this;
        }

        public CaseFormPage InsertCompletionDate(string completionDate)
        {
            WaitForElementVisible(completiondate_Field);
            SendKeys(completiondate_Field, completionDate);

            return this;
        }
        public CaseFormPage InsertDueDate(string DueDate)
        {
            SendKeys(dueDateField, DueDate);

            return this;
        }

        public CaseFormPage InsertReviewDate(string ReviewDate)
        {
            SendKeys(reviewDateField, ReviewDate);

            return this;
        }

        public CaseFormPage SelectSeparateAssessment(bool SeparateAssessment)
        {
            if (SeparateAssessment)
                driver.FindElement(SeparateAssessmentYesOption).Click();
            else
                driver.FindElement(SeparateAssessmentNoOption).Click();
            
            return this;
        }

        public CaseFormPage SelectCarerDeclinedJointAssessments(bool CarerDeclinedJointAssessments)
        {
            if (CarerDeclinedJointAssessments)
                driver.FindElement(carerdeclinedjointassessmentYesOption).Click();
            else
                driver.FindElement(carerdeclinedjointassessmentNoOption).Click();

            return this;
        }

        public LookupPopup TapDelayReasonLookupButton()
        {
            driver.FindElement(delayReasonLookupButton).Click();

            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage InsertTargetStartDate(string TargetStartDate)
        {
            SendKeys(targetStartDateField, TargetStartDate);

            return this;
        }

        public CaseFormPage InsertTriggerDate(string TriggerDate)
        {
            SendKeys(triggerDateField, TriggerDate);

            return this;
        }

        public CaseFormPage SelectJointCarerAssessment(bool JointCarerAssessment)
        {
            if (JointCarerAssessment)
                driver.FindElement(jointCarerAssessmentYesOption).Click();
            else
                driver.FindElement(jointCarerAssessmentNoOption).Click();

            return this;
        }

        public CaseFormPage SelectNewPerson(bool NewPerson)
        {
            if (NewPerson)
                driver.FindElement(newPersonYesOption).Click();
            else
                driver.FindElement(newPersonNoOption).Click();

            return this;
        }

        public CaseFormPage InsertTerminateDate(string TerminateDate)
        {
            SendKeys(terminatedDateField, TerminateDate);

            return this;
        }

        public CaseCasesFormPage TapBackButton()
        {
            Click(backButton);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }

        public CaseFormPage TapSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);            

            return this;
        }
        
        public CaseFormPage TapAdditionalToolbarElementsbutton()
        {
            driver.FindElement(additionalToolbarElementsButton).Click();

            return this;
        }

        public AssessmentPrintHistoryPopup TapPrintHistoryButton()
        {
            driver.FindElement(printHistoryButton).Click();

            return new AssessmentPrintHistoryPopup(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage TapRestrictAccessButtonButton()
        {
            driver.FindElement(restrictAccessButton).Click();

            return this;
        }

        public ShareRecordPopup TapShareButton()
        {
            driver.FindElement(shareButton).Click();

            return new ShareRecordPopup(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage TapDeleteButton()
        {
            driver.FindElement(deleteButton).Click();

            return this;
        }

        public AutomatedUITestDocument1EditAssessmentPage TapEditAssessmentButton()
        {
            WaitForElementToBeClickable(editAssessmentButton);
            Click(editAssessmentButton);

            return new AutomatedUITestDocument1EditAssessmentPage(driver, Wait, appURL);
        }

        public LookupPopup TapSignedOffByLookupButton()
        {
            Click(signedOffByLookupButton);

            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage InsertSignedOffDate(string ValueToInsert)
        {
            SendKeys(signedOffDateField, ValueToInsert);

            return this;
        }




        public CaseFormPage WaitForDinamicDialogeToOpen(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementVisible(dinamicDialogTitle(ExpectedTitle));
            WaitForElementVisible(dinamicDialogMessage(ExpectedMessage));
            WaitForElementVisible(dinamicDialogOKButton);
            
            return this;
        }

        public CaseFormPage WaitForDinamicDialogeToClose(string ExpectedTitle, string ExpectedMessage)
        {
            WaitForElementNotVisible(dinamicDialogTitle(ExpectedTitle), 3);
            WaitForElementNotVisible(dinamicDialogMessage(ExpectedMessage), 3);
            WaitForElementNotVisible(dinamicDialogOKButton, 3);

            return this;
        }

        public CaseFormPage DinamicDialogeTapOKButton()
        {
            Click(dinamicDialogOKButton);

            return this;
        }


        public CaseFormPage ValidateCompletedByFieldLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(completedByField);
            else
                WaitForElementNotVisible(completedByField, 7);

            return this;
        }
        public CaseFormPage ValidateCompletionDetailsSectionVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CompletionDetails_Section);
            else
                WaitForElementNotVisible(CompletionDetails_Section, 7);

            return this;
        }
        public CaseFormPage ValidateCompletedByFieldText(string ExpectText)
        {
            ValidateElementText(completedByField, ExpectText);

            return this;
        }


        public CaseFormPage ValidateCompletionDateValue(string ExpectValue)
        {
            ValidateElementValue(completedDateField, ExpectValue);

            return this;
        }

        public CaseFormPage ValidateCompletionDateText(string ExpectText)
        {
            ValidateElementText(completedDateField, ExpectText);

            return this;
        }

        public CaseFormPage NavigateToCaseFormCaseNotesArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CaseNotesLink_LeftMenu);
            Click(CaseNotesLink_LeftMenu);

            return this;
        }
        public CaseFormPage NavigateToCaseFormAppointmentsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(AppointmentsLink_LeftMenu);
            Click(AppointmentsLink_LeftMenu);

            return this;
        }

        public CaseFormPage NavigateToCaseFormAssessmentFactorsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(AssessmentFactors_LeftMenu);
            Click(AssessmentFactors_LeftMenu);

            return this;
        }

        public CaseFormPage NavigateToCaseFormMembersArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(Members_LeftMenu);
            Click(Members_LeftMenu);

            return this;
        }
        public CaseFormPage NavigateToCaseFormEmailsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(EmailsLink_LeftMenu);
            Click(EmailsLink_LeftMenu);

            return this;
        }
        public CaseFormPage NavigateToCaseFormLettersArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(LettersLink_LeftMenu);
            Click(LettersLink_LeftMenu);

            return this;
        }
        public CaseFormPage NavigateToCaseFormPhoneCallsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(PhoneCallsLink_LeftMenu);
            Click(PhoneCallsLink_LeftMenu);

            return this;
        }
        public CaseFormPage NavigateToCaseFormTasksArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(TasksLink_LeftMenu);
            Click(TasksLink_LeftMenu);

            return this;
        }

        public CaseFormPage NavigateToCaseFormAttachmentsArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(AttachmentsLink_LeftMenu);
            Click(AttachmentsLink_LeftMenu);

            return this;
        }

        
    }
}

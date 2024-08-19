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
    public class PersonFormInvolvementRecordPage : CommonMethods
    {
        public PersonFormInvolvementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newPersonFormIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]");
        readonly By newPersonFormInvolementIFrame = By.Id("CWUrlPanel_IFrame");

        #endregion



        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Form (Person): ']/span");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By generalSectionTitle = By.XPath("//div[@id='CWSection_General']/fieldset/div/span[text()='General']");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PersonFormCaseNote']");
        readonly By AppointmentsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Appointment']");
        readonly By EmailsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By LettersLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By PhoneCallsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PhoneCall']");
        readonly By TasksLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Task']");

        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By PersonFormInvolvementLeftSubMenuItem= By.XPath("//*[@id='CWNavItem_PersonFormInvolment']");

        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");
        readonly By formTypeFieldLabel = By.XPath("//li[@id='CWLabelHolder_documentid']/label[text()='Form Type']/Span[text()='*']");
        readonly By statusFieldLabel = By.XPath("//li[@id='CWLabelHolder_assessmentstatusid']/label[text()='Status']/Span[text()='*']");
        readonly By startDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_startdate']/label[text()='Start Date']/Span[text()='*']");
        readonly By PrecedingFormFieldLabel = By.XPath("//li[@id='CWLabelHolder_precedingformid']/label[text()='Preceding Form']");
        readonly By CreatedOnProviderPortalFieldLabel = By.XPath("//*[@id='CWLabelHolder_createdonproviderportal']/label[text()='Created On Provider Portal']");

        readonly By responsibleTeamFieldLabel = By.XPath("//li[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']/Span[text()='*']");
        readonly By responsibleUserFieldLabel = By.XPath("//li[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By dueDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_duedate']/label[text()='Due Date']");
        readonly By reviewDateFieldLabel = By.XPath("//li[@id='CWLabelHolder_reviewdate']/label[text()='Review Date']");
        readonly By SourceCaseFieldLabel = By.XPath("//li[@id='CWLabelHolder_caseid']/label[text()='Source Case']");
        readonly By PersonFormInvolvementHeader = By.XPath("//div[@id='CWToolbar']/child::div/child::div/following-sibling::h1[text()='Person Form Involvements']");
       
        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personClearButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWClearLookup_personid']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        readonly By formTypeField = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/a[@id='CWField_documentid_Link']");
        readonly By formTypeClearButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWClearLookup_documentid']");
        readonly By formTypeLookupButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWLookupBtn_documentid']");

        readonly By statusPicklist = By.XPath("//select[@id='CWField_assessmentstatusid']");

        readonly By startDateField = By.XPath("//input[@id='CWField_startdate']");

        readonly By PrecedingFormLinkField = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/a[@id='CWField_precedingformid_Link']");
        readonly By PrecedingFormClearButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWClearLookup_precedingformid']");
        readonly By PrecedingFormLookupButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWLookupBtn_precedingformid']");

        readonly By CreatedOnProviderPortal_YesRadioButton = By.XPath("//input[@id='CWField_createdonproviderportal_1']");
        readonly By CreatedOnProviderPortal_NoRadioButton = By.XPath("//input[@id='CWField_createdonproviderportal_0']");

        readonly By responsibleTeamField = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/a[@id='CWField_ownerid_Link']");
        readonly By responsibleTeamClearButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeamLookupButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUserField = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/a[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUserClearButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUserLookupButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWLookupBtn_responsibleuserid']");

        readonly By dueDateField = By.XPath("//input[@id='CWField_duedate']");

        readonly By reviewDateField = By.XPath("//input[@id='CWField_reviewdate']");

        readonly By SourceCaseField = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/a[@id='CWField_caseid_Link']");
        readonly By SourceCaseClearButton = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/button[@id='CWClearLookup_caseid']");
        readonly By SourceCaseLookupButton = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/button[@id='CWLookupBtn_caseid']");

        readonly By cancelledReasonField = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/a[@id='CWField_formcancellationreasonid_Link']");
        readonly By cancelledReasonClearButton = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/button[@id='CWClearLookup_formcancellationreasonid']");
        readonly By cancelledReasonLookupButton = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/button[@id='CWLookupBtn_formcancellationreasonid']");

        readonly By CompletedByField = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/a[@id='CWField_completedbyid_Link']");
        readonly By CompletedByClearButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWClearLookup_completedbyid']");
        readonly By CompletedByLookupButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWLookupBtn_completedbyid']");

        readonly By CompletionDateField = By.XPath("//input[@id='CWField_completiondate']");

        readonly By SignedOffByField = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/a[@id='CWField_signedoffbyid_Link']");
        readonly By SignedOffByClearButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWClearLookup_signedoffbyid']");
        readonly By SignedOffByLookupButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWLookupBtn_signedoffbyid']");

        readonly By SignedOffDateField = By.XPath("//input[@id='CWField_signoffdate']");

        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        #endregion

        #region Notification and Error messages

        By WarningMainArea(string ExpactedText) => By.XPath("//div[@id='CWNotificationHolder_DataForm']/div[@id='CWNotificationMessage_DataForm'][text()='" + ExpactedText + "']");
        By personIDErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_personid']/label/span[text()='" + ExpactedText + "']");
        By FormTypeErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_documentid']/label/span[text()='" + ExpactedText + "']");
        By startDateErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_startdate']/label/span[text()='" + ExpactedText + "']");
        By responsibleTeamErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_ownerid']/label/span[text()='" + ExpactedText + "']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        #endregion

        public PersonFormInvolvementRecordPage WaitForPersonFormInvolvementRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormIFrame);
            SwitchToIframe(newPersonFormIFrame);

            WaitForElement(newPersonFormInvolementIFrame);
            SwitchToIframe(newPersonFormInvolementIFrame);

            WaitForElement(PersonFormInvolvementHeader);

           

            return this;
        }

        public PersonFormInvolvementRecordPage WaitForPersonFormRecordPageToLoad(String ExpectedTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormIFrame);
            SwitchToIframe(newPersonFormIFrame);

            WaitForElement(generalSectionTitle);

            WaitForElement(personFieldLabel);
            WaitForElement(formTypeFieldLabel);
            WaitForElement(statusFieldLabel);
            WaitForElement(startDateFieldLabel);
            WaitForElement(PrecedingFormFieldLabel);
            WaitForElement(CreatedOnProviderPortalFieldLabel);
            WaitForElement(responsibleTeamFieldLabel);
            WaitForElement(responsibleUserFieldLabel);
            WaitForElement(dueDateFieldLabel);
            WaitForElement(reviewDateFieldLabel);
            WaitForElement(SourceCaseFieldLabel);

            ValidateElementTextContainsText(pageHeader, ExpectedTitle);

            return this;
        }

        public PersonFormInvolvementRecordPage WaitForPersonFormRecordPageToLoadDisabled(String ExpectedTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormIFrame);
            SwitchToIframe(newPersonFormIFrame);

            WaitForElement(generalSectionTitle);

            WaitForElement(personFieldLabel);
            WaitForElement(formTypeFieldLabel);
            WaitForElement(statusFieldLabel);
            WaitForElement(startDateFieldLabel);
            WaitForElement(PrecedingFormFieldLabel);
            WaitForElement(CreatedOnProviderPortalFieldLabel);
            WaitForElement(responsibleTeamFieldLabel);
            WaitForElement(responsibleUserFieldLabel);
            WaitForElement(dueDateFieldLabel);
            WaitForElement(reviewDateFieldLabel);
            WaitForElement(SourceCaseFieldLabel);

            ValidateElementTextContainsText(pageHeader, ExpectedTitle);

            WaitForElementNotVisible(saveButton, 7);
            WaitForElementNotVisible(saveAndCloseButton, 7);
            WaitForElementNotVisible(editAssessmentButton, 7);

            ValidateElementDisabled(personLookupButton);
            ValidateElementDisabled(formTypeLookupButton);
            ValidateElementDisabled(statusPicklist);
            ValidateElementDisabled(startDateField);
            ValidateElementDisabled(PrecedingFormLookupButton);
            ValidateElementDisabled(CreatedOnProviderPortal_YesRadioButton);
            ValidateElementDisabled(CreatedOnProviderPortal_NoRadioButton);
            ValidateElementDisabled(responsibleTeamLookupButton);
            ValidateElementDisabled(responsibleUserLookupButton);
            ValidateElementDisabled(dueDateField);
            ValidateElementDisabled(reviewDateField);
            ValidateElementDisabled(SourceCaseLookupButton);

            return this;
        }

        public PersonFormInvolvementRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(saveAndCloseButton));
            Wait.Until(c => c.FindElement(editAssessmentButton));

            return this;
        }


        /// <summary>
        /// Wait for all toolbar buttons to be visible (after a user tapped on the Additional Toolbar Elements Button)
        /// </summary>
        /// <returns></returns>
        public PersonFormInvolvementRecordPage WaitForAllToolbarIconsToBeVisible()
        {
            Wait.Until(c => c.FindElement(backButton));
            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(saveAndCloseButton));
            Wait.Until(c => c.FindElement(editAssessmentButton));
            Wait.Until(c => c.FindElement(deleteButton));

            return this;
        }

        #region Validation methods

        public PersonFormInvolvementRecordPage ValidateAllFieldLabelsVisible()
        {
            Wait.Until(c => c.FindElement(generalSectionTitle));

            Wait.Until(c => c.FindElement(personFieldLabel));
            Wait.Until(c => c.FindElement(formTypeFieldLabel));
            Wait.Until(c => c.FindElement(statusFieldLabel));
            Wait.Until(c => c.FindElement(startDateFieldLabel));
            Wait.Until(c => c.FindElement(responsibleTeamFieldLabel));
            Wait.Until(c => c.FindElement(responsibleUserFieldLabel));
            Wait.Until(c => c.FindElement(dueDateFieldLabel));
            Wait.Until(c => c.FindElement(reviewDateFieldLabel));
            Wait.Until(c => c.FindElement(PrecedingFormFieldLabel));

            return this;
        }
        public PersonFormInvolvementRecordPage ValidatePersonField(string ExpectedPersonTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string personTitle = driver.FindElement(personField).Text;
            bool clearButtonVisible = GetElementVisibility(personClearButton);
            bool lookupButtonVisible = GetElementVisibility(personLookupButton);

            Assert.AreEqual(ExpectedPersonTitle, personTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }
        public PersonFormInvolvementRecordPage ValidateFormTypeField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string formTypeTitle = driver.FindElement(formTypeField).Text;
            bool clearButtonVisible = GetElementVisibility(formTypeClearButton);
            bool lookupButtonVisible = GetElementVisibility(formTypeLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, formTypeTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateStatusField(string ExpectedStatus)
        {
            Wait.Until(c => c.FindElement(statusPicklist));

            var selectElement = new SelectElement(driver.FindElement(statusPicklist));
            Assert.AreEqual(ExpectedStatus, selectElement.SelectedOption.Text);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateStartDateField(string ExpectedStartDate)
        {
            Wait.Until(c => c.FindElement(startDateField));

            string startDate = driver.FindElement(startDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedStartDate, startDate);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateResponsibleTeamField(string ExpectedResponsibleTeamTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleTeamTitle = driver.FindElement(responsibleTeamField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleTeamClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleTeamLookupButton);

            Assert.AreEqual(ExpectedResponsibleTeamTitle, responsibleTeamTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }
        public PersonFormInvolvementRecordPage ValidateResponsibleUserField(string ExpectedResponsibleUserTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleUserTitle = driver.FindElement(responsibleUserField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleUserClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleUserLookupButton);

            Assert.AreEqual(ExpectedResponsibleUserTitle, responsibleUserTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidatePrecedingFormFieldLinkText(string ExpectedText)
        {
            ValidateElementText(PrecedingFormLinkField, ExpectedText);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateDueDateField(string ExpectedDueDate)
        {
            Wait.Until(c => c.FindElement(dueDateField));

            string dueDate = driver.FindElement(dueDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedDueDate, dueDate);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateReviewDateField(string ExpectedReviewDate)
        {
            Wait.Until(c => c.FindElement(reviewDateField));

            string reviewDate = driver.FindElement(reviewDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedReviewDate, reviewDate);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateCreatedOnProviderPortalOptionChecked(bool ExpectYesOpptionChecked)
        {
            if(ExpectYesOpptionChecked)
            {
                ValidateElementChecked(CreatedOnProviderPortal_YesRadioButton);
                ValidateElementNotChecked(CreatedOnProviderPortal_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CreatedOnProviderPortal_YesRadioButton);
                ValidateElementChecked(CreatedOnProviderPortal_NoRadioButton);
            }

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateFormTypeLinkField(string ExpectedFormTypeLinkText)
        {
            ValidateElementText(formTypeField, ExpectedFormTypeLinkText);
            
            return this;
        }
        public PersonFormInvolvementRecordPage ValidateCancelledReasonLinkField(string cancelledReasonLinkFieldText)
        {
            ValidateElementText(cancelledReasonField, cancelledReasonLinkFieldText);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateCompletedByLinkField(string CompletedByLinkFieldText)
        {
            ValidateElementText(CompletedByField, CompletedByLinkFieldText);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateSourceCaseField(string ExpectedText)
        {
            ValidateElementText(SourceCaseField, ExpectedText);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateSignedOffByLinkField(string ExpectedText)
        {
            ValidateElementText(SignedOffByField, ExpectedText);

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateSignedOffDateField(string ExpectedValue)
        {
            ValidateElementValue(SignedOffDateField, ExpectedValue);

            return this;
        }


        public PersonFormInvolvementRecordPage ValidateTopAreaWarningMessage(string ExpectedMessage)
        {
            WaitForElementVisible(WarningMainArea(ExpectedMessage));
            
            return this;
        }
        public PersonFormInvolvementRecordPage ValidatePersonErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(personIDErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateFormTypeErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(FormTypeErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateStartDateErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(startDateErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormInvolvementRecordPage ValidateResponsibleTeamErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(responsibleTeamErrorArea(ExpectedMessage));

            return this;
        }

        #endregion


        public PersonFormInvolvementRecordPage TapClearPersonButton()
        {
            Click(personClearButton);

            return this;
        }
        public LookupPopup TapFormTypeLookupButton()
        {
            Click(formTypeLookupButton);
            
            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }
        public PersonFormInvolvementRecordPage TapClearResponsibleTeamButton()
        {
            driver.FindElement(responsibleTeamClearButton).Click();

            return this;
        }
        public LookupPopup TapResponsibleUserLookupButton()
        {
            driver.FindElement(responsibleUserLookupButton).Click();

            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }
        public PersonFormInvolvementRecordPage TapPrecedingFormLookupButton()
        {
            driver.FindElement(PrecedingFormLookupButton).Click();

            return this;
        }
        public PersonFormInvolvementRecordPage TapCancelledReasonLookupButton()
        {
            Click(cancelledReasonLookupButton);

            return this;
        }
        public PersonFormInvolvementRecordPage TapCancelledReasonClearButton()
        {
            Click(cancelledReasonClearButton);

            return this;
        }
        public PersonFormInvolvementRecordPage TapPrecedingFormClearButton()
        {
            driver.FindElement(PrecedingFormClearButton).Click();

            return this;
        }
        public PersonFormInvolvementRecordPage ClickSourceCaseLookupButton()
        {
            Click(SourceCaseLookupButton);

            return this;
        }
        public PersonFormInvolvementRecordPage ClickSourceCaseRemoveButton()
        {
            Click(SourceCaseClearButton);

            return this;
        }
        public PersonFormInvolvementRecordPage ClickCompletedByLookupButton()
        {
            Click(CompletedByField);

            return this;
        }
        public PersonFormInvolvementRecordPage ClickCompletedByRemoveButton()
        {
            Click(CompletedByClearButton);

            return this;
        }
        public PersonFormInvolvementRecordPage TapSignedOffByLookupButton()
        {
            Click(SignedOffByLookupButton);

            return this;
        }
        public PersonFormInvolvementRecordPage TapSignedOffByClearButton()
        {
            Click(SignedOffByClearButton);

            return this;
        }



        public PersonFormInvolvementRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(statusPicklist, TextToSelect);

            return this;
        }



        public PersonFormInvolvementRecordPage InsertStartDate(string StartDate)
        {
            SendKeys(startDateField, StartDate);

            return this;
        }
        public PersonFormInvolvementRecordPage InsertDueDate(string DueDate)
        {
            SendKeys(dueDateField, DueDate);

            return this;
        }
        public PersonFormInvolvementRecordPage InsertReviewDate(string ReviewDate)
        {
            SendKeys(reviewDateField, ReviewDate);

            return this;
        }
        public PersonFormInvolvementRecordPage InsertSignedOffDate(string ValueToInsert)
        {
            SendKeys(SignedOffDateField, ValueToInsert);

            return this;
        }



        public PersonFormInvolvementRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonFormInvolvementRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonFormInvolvementRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonFormInvolvementRecordPage TapDeleteButton()
        {

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public AutomatedUITestDocument1EditAssessmentPage TapEditAssessmentButton()
        {
            driver.FindElement(editAssessmentButton).Click();

            return new AutomatedUITestDocument1EditAssessmentPage(driver, Wait, appURL);
        }



        public PersonFormInvolvementRecordPage NavigateToPersonFormCaseNotesArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CaseNotesLink_LeftMenu);
            Click(CaseNotesLink_LeftMenu);

            return this;
        }
        public PersonFormInvolvementRecordPage NavigateToPersonFormAppointmentsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(AppointmentsLink_LeftMenu);
            Click(AppointmentsLink_LeftMenu);

            return this;
        }
        public PersonFormInvolvementRecordPage NavigateToPersonFormEmailsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(EmailsLink_LeftMenu);
            Click(EmailsLink_LeftMenu);

            return this;
        }
        public PersonFormInvolvementRecordPage NavigateToPersonFormLettersArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(LettersLink_LeftMenu);
            Click(LettersLink_LeftMenu);

            return this;
        }
        public PersonFormInvolvementRecordPage NavigateToPersonFormPhoneCallsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(PhoneCallsLink_LeftMenu);
            Click(PhoneCallsLink_LeftMenu);

            return this;
        }
        public PersonFormInvolvementRecordPage NavigateToPersonFormTasksArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(TasksLink_LeftMenu);
            Click(TasksLink_LeftMenu);

            return this;
        }

        public PersonFormInvolvementRecordPage NavigateToPersonFormsPage()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(PersonFormInvolvementLeftSubMenuItem);
            Click(PersonFormInvolvementLeftSubMenuItem);

            return this;
        }

        public PersonFormInvolvementRecordPage OpenRecord(string RecordID)
        {
            WaitForElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public PersonFormInvolvementRecordPage ValidateRecordPresent(string RecordID)
        {
            WaitForElement(RecordRowCheckBox(RecordID));
            Click(RecordRowCheckBox(RecordID));

            return this;
        }

        public PersonFormInvolvementRecordPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }

        public PersonFormInvolvementRecordPage TapNewRecord()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }
        public PersonFormInvolvementRecordPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }

    }
}

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
    public class PersonFormRecordPage : CommonMethods
    {
        public PersonFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newPersonFormIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]");

        #endregion



        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Form (Person): ']/span");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

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
        readonly By RelatedStatus_DropDown = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By ReviewStatus_DropDownLabel = By.XPath("//li[@id='CWLabelHolder_reviewstatusid']");
        readonly By ReviewStatus_DropDown = By.XPath("//*[@id='CWField_reviewstatusid']");


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


        #endregion

        #region Notification and Error messages

        By WarningMainArea(string ExpactedText) => By.XPath("//div[@id='CWNotificationHolder_DataForm']/div[@id='CWNotificationMessage_DataForm'][text()='" + ExpactedText + "']");
        By personIDErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_personid']/label/span[text()='" + ExpactedText + "']");
        By FormTypeErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_documentid']/label/span[text()='" + ExpactedText + "']");
        By startDateErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_startdate']/label/span[text()='" + ExpactedText + "']");
        By responsibleTeamErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_ownerid']/label/span[text()='" + ExpactedText + "']");
        
        #endregion

        public PersonFormRecordPage WaitForPersonFormRecordPageToLoad(bool CreatedOnProviderPortalFieldLabelVisible = true)
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
            if (CreatedOnProviderPortalFieldLabelVisible)
                WaitForElement(CreatedOnProviderPortalFieldLabel);

            WaitForElement(responsibleTeamFieldLabel);
            WaitForElement(responsibleUserFieldLabel);
            WaitForElement(dueDateFieldLabel);
            WaitForElement(reviewDateFieldLabel);
            WaitForElement(SourceCaseFieldLabel);

            return this;
        }

        public PersonFormRecordPage WaitForPersonFormRecordPageToLoad(String ExpectedTitle, bool CreatedOnProviderPortalFieldLabelVisible = true)
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

            if (CreatedOnProviderPortalFieldLabelVisible)
                WaitForElement(CreatedOnProviderPortalFieldLabel);

            WaitForElement(responsibleTeamFieldLabel);
            WaitForElement(responsibleUserFieldLabel);
            WaitForElement(dueDateFieldLabel);
            WaitForElement(reviewDateFieldLabel);
            WaitForElement(SourceCaseFieldLabel);

            ValidateElementTextContainsText(pageHeader, ExpectedTitle);

            return this;
        }

        public PersonFormRecordPage WaitForPersonFormRecordPageToLoadDisabled(String ExpectedTitle, bool CreatedOnProviderPortalFieldLabelVisible = true)
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

            if (CreatedOnProviderPortalFieldLabelVisible)
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

            if (CreatedOnProviderPortalFieldLabelVisible)
            {
                ValidateElementDisabled(CreatedOnProviderPortal_YesRadioButton);
                ValidateElementDisabled(CreatedOnProviderPortal_NoRadioButton);
            }

            ValidateElementDisabled(responsibleTeamLookupButton);
            ValidateElementDisabled(responsibleUserLookupButton);
            ValidateElementDisabled(dueDateField);
            ValidateElementDisabled(reviewDateField);
            ValidateElementDisabled(SourceCaseLookupButton);

            return this;
        }

        public PersonFormRecordPage WaitForRecordToBeSaved()
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
        public PersonFormRecordPage WaitForAllToolbarIconsToBeVisible()
        {
            Wait.Until(c => c.FindElement(backButton));
            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(saveAndCloseButton));
            Wait.Until(c => c.FindElement(editAssessmentButton));
            Wait.Until(c => c.FindElement(deleteButton));

            return this;
        }

        #region Validation methods

        public PersonFormRecordPage ValidateAllFieldLabelsVisible()
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
        public PersonFormRecordPage ValidatePersonField(string ExpectedPersonTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string personTitle = driver.FindElement(personField).Text;
            bool clearButtonVisible = GetElementVisibility(personClearButton);
            bool lookupButtonVisible = GetElementVisibility(personLookupButton);

            Assert.AreEqual(ExpectedPersonTitle, personTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }
        public PersonFormRecordPage ValidateFormTypeField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string formTypeTitle = driver.FindElement(formTypeField).Text;
            bool clearButtonVisible = GetElementVisibility(formTypeClearButton);
            bool lookupButtonVisible = GetElementVisibility(formTypeLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, formTypeTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormRecordPage ValidateStatusField(string ExpectedStatus)
        {
            Wait.Until(c => c.FindElement(statusPicklist));

            var selectElement = new SelectElement(driver.FindElement(statusPicklist));
            Assert.AreEqual(ExpectedStatus, selectElement.SelectedOption.Text);

            return this;
        }
        public PersonFormRecordPage ValidateStartDateField(string ExpectedStartDate)
        {
            Wait.Until(c => c.FindElement(startDateField));

            string startDate = driver.FindElement(startDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedStartDate, startDate);

            return this;
        }
        public PersonFormRecordPage ValidateResponsibleTeamField(string ExpectedResponsibleTeamTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleTeamTitle = driver.FindElement(responsibleTeamField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleTeamClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleTeamLookupButton);

            Assert.AreEqual(ExpectedResponsibleTeamTitle, responsibleTeamTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }
        public PersonFormRecordPage ValidateResponsibleUserField(string ExpectedResponsibleUserTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string responsibleUserTitle = driver.FindElement(responsibleUserField).Text;
            bool clearButtonVisible = GetElementVisibility(responsibleUserClearButton);
            bool lookupButtonVisible = GetElementVisibility(responsibleUserLookupButton);

            Assert.AreEqual(ExpectedResponsibleUserTitle, responsibleUserTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormRecordPage ValidatePrecedingFormFieldLinkText(string ExpectedText)
        {
            ValidateElementText(PrecedingFormLinkField, ExpectedText);

            return this;
        }
        public PersonFormRecordPage ValidateDueDateField(string ExpectedDueDate)
        {
            Wait.Until(c => c.FindElement(dueDateField));

            string dueDate = driver.FindElement(dueDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedDueDate, dueDate);

            return this;
        }
        public PersonFormRecordPage ValidateReviewDateField(string ExpectedReviewDate)
        {
            Wait.Until(c => c.FindElement(reviewDateField));

            string reviewDate = driver.FindElement(reviewDateField).GetAttribute("value");
            Assert.AreEqual(ExpectedReviewDate, reviewDate);

            return this;
        }
        public PersonFormRecordPage ValidateCreatedOnProviderPortalOptionChecked(bool ExpectYesOpptionChecked)
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
        public PersonFormRecordPage ValidateFormTypeLinkField(string ExpectedFormTypeLinkText)
        {
            ValidateElementText(formTypeField, ExpectedFormTypeLinkText);
            
            return this;
        }
        public PersonFormRecordPage ValidateCancelledReasonLinkField(string cancelledReasonLinkFieldText)
        {
            ValidateElementText(cancelledReasonField, cancelledReasonLinkFieldText);

            return this;
        }
        public PersonFormRecordPage ValidateCompletedByLinkField(string CompletedByLinkFieldText)
        {
            ValidateElementText(CompletedByField, CompletedByLinkFieldText);

            return this;
        }
        public PersonFormRecordPage ValidateSourceCaseField(string ExpectedText)
        {
            ValidateElementText(SourceCaseField, ExpectedText);

            return this;
        }
        public PersonFormRecordPage ValidateSignedOffByLinkField(string ExpectedText)
        {
            ValidateElementText(SignedOffByField, ExpectedText);

            return this;
        }
        public PersonFormRecordPage ValidateSignedOffDateField(string ExpectedValue)
        {
            ValidateElementValue(SignedOffDateField, ExpectedValue);

            return this;
        }


        public PersonFormRecordPage ValidateTopAreaWarningMessage(string ExpectedMessage)
        {
            WaitForElementVisible(WarningMainArea(ExpectedMessage));
            
            return this;
        }
        public PersonFormRecordPage ValidatePersonErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(personIDErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormRecordPage ValidateFormTypeErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(FormTypeErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormRecordPage ValidateStartDateErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(startDateErrorArea(ExpectedMessage));

            return this;
        }
        public PersonFormRecordPage ValidateResponsibleTeamErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(responsibleTeamErrorArea(ExpectedMessage));

            return this;
        }

        #endregion


        public PersonFormRecordPage TapClearPersonButton()
        {
            Click(personClearButton);

            return this;
        }
        public LookupPopup TapFormTypeLookupButton()
        {
            Click(formTypeLookupButton);
            
            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }
        public PersonFormRecordPage TapClearResponsibleTeamButton()
        {
            driver.FindElement(responsibleTeamClearButton).Click();

            return this;
        }
        public LookupPopup TapResponsibleUserLookupButton()
        {
            driver.FindElement(responsibleUserLookupButton).Click();

            return new LookupPopup(this.driver, this.Wait, this.appURL);
        }
        public PersonFormRecordPage TapPrecedingFormLookupButton()
        {
            WaitForElementToBeClickable(PrecedingFormLookupButton);
            MoveToElementInPage(PrecedingFormLookupButton);
            Click(PrecedingFormLookupButton);

            return this;
        }
        public PersonFormRecordPage TapCancelledReasonLookupButton()
        {
            Click(cancelledReasonLookupButton);

            return this;
        }
        public PersonFormRecordPage TapCancelledReasonClearButton()
        {
            Click(cancelledReasonClearButton);

            return this;
        }
        public PersonFormRecordPage TapPrecedingFormClearButton()
        {
            driver.FindElement(PrecedingFormClearButton).Click();

            return this;
        }
        public PersonFormRecordPage ClickSourceCaseLookupButton()
        {
            Click(SourceCaseLookupButton);

            return this;
        }
        public PersonFormRecordPage ClickSourceCaseRemoveButton()
        {
            Click(SourceCaseClearButton);

            return this;
        }
        public PersonFormRecordPage ClickCompletedByLookupButton()
        {
            Click(CompletedByField);

            return this;
        }
        public PersonFormRecordPage ClickCompletedByRemoveButton()
        {
            Click(CompletedByClearButton);

            return this;
        }
        public PersonFormRecordPage TapSignedOffByLookupButton()
        {
            Click(SignedOffByLookupButton);

            return this;
        }
        public PersonFormRecordPage TapSignedOffByClearButton()
        {
            Click(SignedOffByClearButton);

            return this;
        }



        public PersonFormRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(statusPicklist, TextToSelect);

            return this;
        }



        public PersonFormRecordPage InsertStartDate(string StartDate)
        {
            SendKeys(startDateField, StartDate);

            return this;
        }
        public PersonFormRecordPage InsertDueDate(string DueDate)
        {
            SendKeys(dueDateField, DueDate);

            return this;
        }
        public PersonFormRecordPage InsertReviewDate(string ReviewDate)
        {
            SendKeys(reviewDateField, ReviewDate);

            return this;
        }
        public PersonFormRecordPage InsertSignedOffDate(string ValueToInsert)
        {
            SendKeys(SignedOffDateField, ValueToInsert);

            return this;
        }



        public PersonFormRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonFormRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonFormRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonFormRecordPage TapDeleteButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public AutomatedUITestDocument1EditAssessmentPage TapEditAssessmentButton()
        {
            driver.FindElement(editAssessmentButton).Click();

            return new AutomatedUITestDocument1EditAssessmentPage(driver, Wait, appURL);
        }



        public PersonFormRecordPage NavigateToPersonFormCaseNotesArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CaseNotesLink_LeftMenu);
            Click(CaseNotesLink_LeftMenu);

            return this;
        }
        public PersonFormRecordPage NavigateToPersonFormAppointmentsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(AppointmentsLink_LeftMenu);
            Click(AppointmentsLink_LeftMenu);

            return this;
        }
        public PersonFormRecordPage NavigateToPersonFormEmailsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(EmailsLink_LeftMenu);
            Click(EmailsLink_LeftMenu);

            return this;
        }
        public PersonFormRecordPage NavigateToPersonFormLettersArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(LettersLink_LeftMenu);
            Click(LettersLink_LeftMenu);

            return this;
        }
        public PersonFormRecordPage NavigateToPersonFormPhoneCallsArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(PhoneCallsLink_LeftMenu);
            Click(PhoneCallsLink_LeftMenu);

            return this;
        }
        public PersonFormRecordPage NavigateToPersonFormTasksArea()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(TasksLink_LeftMenu);
            Click(TasksLink_LeftMenu);

            return this;
        }

        public PersonFormRecordPage NavigateToPersonFormsInvolvementPage()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(PersonFormInvolvementLeftSubMenuItem);
            Click(PersonFormInvolvementLeftSubMenuItem);

            return this;
        }

        public PersonFormRecordPage ValidateRelatedStatusFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(RelatedStatus_DropDown);
                WaitForElementVisible(RelatedStatus_DropDown);
            }
            else
            {
                WaitForElementNotVisible(RelatedStatus_DropDown, 3);
            }

            return this;
        }

        public PersonFormRecordPage ValidateReviewStatusFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(ReviewStatus_DropDownLabel);
                WaitForElementVisible(ReviewStatus_DropDownLabel);
            }
            else
            {
                WaitForElementNotVisible(ReviewStatus_DropDownLabel, 3);
            }

            return this;
        }

        public PersonFormRecordPage ValidateReviewStatusFieldOption(string text)
        {
            WaitForElement(ReviewStatus_DropDown);
            ValidatePicklistContainsElementByText(ReviewStatus_DropDown, text);

            return this;
        }

    }
}

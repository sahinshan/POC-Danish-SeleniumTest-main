using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormCaseNoteRecordPage : CommonMethods
    {

        public CaseFormCaseNoteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseformcasenote&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By PersonTopBanner_SummaryItem_CWInfoLeft = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoLeft']");
        readonly By PersonTopBanner_SummaryItem_CWInfoRight = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoRight']");
        readonly By PersonTopBanner_SummaryItem_ExpandButton = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWButton']");


        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");



        #region Headers

        readonly By subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By Description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label[text()='Description']");

        readonly By CaseForm_FieldHeader = By.XPath("//*[@id='CWLabelHolder_caseformid']/label[text()='Case Form']");
        readonly By Reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By Priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By Date_FieldHeader = By.XPath("//*[@id='CWLabelHolder_casenotedate']/label[text()='Date']");
        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By ContainsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");
        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By Category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By SubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");

        readonly By SignificantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");

        readonly By IsCloned_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscloned']/label[text()='Is Cloned?']");
        readonly By ClonedFrom_FieldHeader = By.XPath("//*[@id='CWLabelHolder_clonefromid']/label[text()='Cloned From']");

        #endregion

        #region Fields

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By subject_ErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By Description_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By CaseForm_LinkField = By.XPath("//*[@id='CWField_caseformid_Link']");
        readonly By CaseForm_LookupButton = By.XPath("//*[@id='CWLookupBtn_caseformid']");
        readonly By Reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By Reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By Priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By Date_DateField = By.XPath("//*[@id='CWField_casenotedate']");
        readonly By Date_DateErrorLabel = By.XPath("//*[@id='CWControlHolder_casenotedate']/div/div[1]/label/span");
        readonly By Date_TimeField = By.XPath("//*[@id='CWField_casenotedate_Time']");
        readonly By Date_TimeErrorLabel = By.XPath("//*[@id='CWControlHolder_casenotedate']/div/div[2]/label/span");
        readonly By Status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By Status_ErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");
        readonly By ContainsInformationProvidedByAThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByAThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By Category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By Category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By SubCategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By SubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By Outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By Outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");

        readonly By SignificantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By SignificantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");

        readonly By IsCloned_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By IsCloned_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");
        readonly By ClonedFrom_LinkField = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By ClonedFrom_LookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");

        #endregion






        public CaseFormCaseNoteRecordPage WaitForCaseFormCaseNoteRecordPageToLoad(string EmailTitle)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(PersonTopBanner_SummaryItem_CWInfoLeft);
            WaitForElement(PersonTopBanner_SummaryItem_CWInfoRight);
            WaitForElement(PersonTopBanner_SummaryItem_ExpandButton);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(subject_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(CaseForm_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(Date_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(SignificantEvent_FieldHeader);
            //WaitForElement(IsCloned_FieldHeader);
            //WaitForElement(ClonedFrom_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Case Form Case Note:\r\n" + EmailTitle)
                throw new Exception("Page title do not equals: Case Form Case Note: " + EmailTitle);

            return this;
        }


        public CaseFormCaseNoteRecordPage ClickCloneButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public CaseFormCaseNoteRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateNotificationMessageText(string ExpectText)
        {
            ValidateElementText(NotificationMessage, ExpectText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateSubjectErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(subject_ErrorLabel);
            else
                WaitForElementNotVisible(subject_ErrorLabel, 3);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidatesubjectErrorLabelText(string ExpectText)
        {
            ValidateElementText(subject_ErrorLabel, ExpectText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Date_DateErrorLabel);
            else
                WaitForElementNotVisible(Date_DateErrorLabel, 3);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateDateErrorLabelText(string ExpectText)
        {
            ValidateElementText(Date_DateErrorLabel, ExpectText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Date_TimeErrorLabel);
            else
                WaitForElementNotVisible(Date_TimeErrorLabel, 3);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateTimeErrorLabelText(string ExpectText)
        {
            ValidateElementText(Date_TimeErrorLabel, ExpectText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateStatusErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Status_ErrorLabel);
            else
                WaitForElementNotVisible(Status_ErrorLabel, 3);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateStatusErrorLabelText(string ExpectText)
        {
            ValidateElementText(Status_ErrorLabel, ExpectText);

            return this;
        }



        public CaseFormCaseNoteRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateDescription(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(Description_Field, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateCaseFormFieldLinkText(string ExpectedText)
        {
            ValidateElementText(CaseForm_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateReasonFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Reason_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidatePriorityFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateDate(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(Date_DateField, ExpectedDate);
            ValidateElementValue(Date_TimeField, ExpectedTime);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateContainsInformationProvidedByAThirdPartyYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
            else
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateContainsInformationProvidedByAThirdPartyNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            else
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateResponsibleUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Category_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SubCategory_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateOutcomeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateClonedFromFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ClonedFrom_LinkField, ExpectedText);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateSignificantEventYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(SignificantEvent_YesRadioButton);
            else
                ValidateElementNotChecked(SignificantEvent_YesRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateSignificantEventNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(SignificantEvent_NoRadioButton);
            else
                ValidateElementNotChecked(SignificantEvent_NoRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateIsClonedOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(IsCloned_YesRadioButton);
            else
                ValidateElementNotChecked(IsCloned_YesRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ValidateIsClonedNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(IsCloned_NoRadioButton);
            else
                ValidateElementNotChecked(IsCloned_NoRadioButton);

            return this;
        }



        public CaseFormCaseNoteRecordPage InsertSubject(string Subject)
        {
            SendKeys(subject_Field, Subject);

            return this;
        }
        public CaseFormCaseNoteRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(Description_Field, Subject);

            return this;
        }
        public CaseFormCaseNoteRecordPage InsertDate(string Date, string Time)
        {
            WaitForElementToBeClickable(Date_DateField);
            SendKeys(Date_DateField, Date + Keys.Tab);
            WaitForElementToBeClickable(Date_TimeField);        
            SendKeys(Date_TimeField, Time);

            return this;
        }




        public CaseFormCaseNoteRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }


        public CaseFormCaseNoteRecordPage ClickCaseFormLookupButton()
        {
            Click(CaseForm_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickReasonLookupButton()
        {
            Click(Reason_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickPriorityLookupButton()
        {
            Click(Priority_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickCategoryLookupButton()
        {
            Click(Category_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSubCategoryLookupButton()
        {
            Click(SubCategory_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickOutcomeLookupButton()
        {
            Click(Outcome_LookupButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickClonedFromLookupButton()
        {
            Click(ClonedFrom_LookupButton);

            return this;
        }



        public CaseFormCaseNoteRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSignificantEvent_YesRadioButton()
        {
            Click(SignificantEvent_YesRadioButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSignificantEvent_NoRadioButton()
        {
            Click(SignificantEvent_NoRadioButton);

            return this;
        }



        public CaseFormCaseNoteRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public CaseFormCaseNoteRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public CaseFormCaseNoteRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }


    }
}

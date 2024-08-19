using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormTaskRecordPage : CommonMethods
    {

        public CaseFormTaskRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=task&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By CaseTopBanner_SummaryItem_CWInfoLeft = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoLeft']");
        readonly By CaseTopBanner_SummaryItem_CWInfoRight = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoRight']");
        readonly By CaseTopBanner_SummaryItem_ExpandButton = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWButton']");


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

        readonly By Regarding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");
        readonly By Reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By Priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By Due_FieldHeader = By.XPath("//*[@id='CWLabelHolder_duedate']/label[text()='Due']");
        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By ContainsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");
        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By Category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By SubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");
        readonly By IsCaseNote_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label[text()='Is Case Note?']");

        readonly By SignificantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");

        readonly By IsCloned_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscloned']/label[text()='Is Cloned?']");
        readonly By ClonedFrom_FieldHeader = By.XPath("//*[@id='CWLabelHolder_clonedfromid']/label[text()='Cloned From']");

        #endregion

        #region Fields

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By Description_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By Regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By Regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By Reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By Reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By Priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By Due_DueField = By.XPath("//*[@id='CWField_duedate']");
        readonly By Due_TimeField = By.XPath("//*[@id='CWField_duedate_Time']");
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
        readonly By IsCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By IsCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");

        readonly By SignificantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By SignificantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");

        readonly By IsCloned_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By IsCloned_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");
        readonly By ClonedFrom_LinkField = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By ClonedFrom_LookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");

        #endregion






        public CaseFormTaskRecordPage WaitForCaseFormTaskRecordPageToLoad(string RecordTitle)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(CaseTopBanner_SummaryItem_CWInfoLeft);
            WaitForElement(CaseTopBanner_SummaryItem_CWInfoRight);
            WaitForElement(CaseTopBanner_SummaryItem_ExpandButton);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(subject_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Regarding_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(Due_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(SignificantEvent_FieldHeader);
            WaitForElement(IsCloned_FieldHeader);
            WaitForElement(ClonedFrom_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Task:\r\n" + RecordTitle)
                throw new Exception("Page title do not equals: Task: " + RecordTitle);

            return this;
        }


        public CaseFormTaskRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }
        public CaseFormTaskRecordPage ValidateNotificationMessageText(string ExpectText)
        {
            ValidateElementText(NotificationMessage, ExpectText);

            return this;
        }



        public CaseFormTaskRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateDescription(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(Description_Field, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateRegardingFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Regarding_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateReasonFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Reason_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidatePriorityFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateDue(string ExpectedDue, string ExpectedTime)
        {
            ValidateElementValue(Due_DueField, ExpectedDue);
            ValidateElementValue(Due_TimeField, ExpectedTime);

            return this;
        }
        public CaseFormTaskRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateContainsInformationProvidedByAThirdPartyCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }

            return this;
        }
        public CaseFormTaskRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateResponsibleUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Category_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SubCategory_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateOutcomeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        public CaseFormTaskRecordPage ValidateIsCaseNoteCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(IsCaseNote_YesRadioButton);
                ValidateElementNotChecked(IsCaseNote_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(IsCaseNote_YesRadioButton);
                ValidateElementChecked(IsCaseNote_NoRadioButton);
            }

            return this;
        }

        public CaseFormTaskRecordPage ValidateSignificantEventCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(SignificantEvent_YesRadioButton);
                ValidateElementNotChecked(SignificantEvent_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SignificantEvent_YesRadioButton);
                ValidateElementChecked(SignificantEvent_NoRadioButton);
            }

            return this;
        }

        public CaseFormTaskRecordPage ValidateIsClonedCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(IsCloned_YesRadioButton);
                ValidateElementNotChecked(IsCloned_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(IsCloned_YesRadioButton);
                ValidateElementChecked(IsCloned_NoRadioButton);
            }

            return this;
        }
        public CaseFormTaskRecordPage ValidateClonedFromFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ClonedFrom_LinkField, ExpectedText);

            return this;
        }



        public CaseFormTaskRecordPage InsertSubject(string Subject)
        {
            SendKeys(subject_Field, Subject);

            return this;
        }
        public CaseFormTaskRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(Description_Field, Subject);

            return this;
        }
        public CaseFormTaskRecordPage InsertDue(string Due, string Time)
        {
            SendKeys(Due_DueField, Due);
            SendKeys(Due_TimeField, Time);

            return this;
        }




        public CaseFormTaskRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }


        public CaseFormTaskRecordPage ClickCaseFormLookupButton()
        {
            Click(Regarding_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickReasonLookupButton()
        {
            Click(Reason_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickPriorityLookupButton()
        {
            Click(Priority_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickCategoryLookupButton()
        {
            Click(Category_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickSubCategoryLookupButton()
        {
            Click(SubCategory_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickOutcomeLookupButton()
        {
            Click(Outcome_LookupButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickClonedFromLookupButton()
        {
            Click(ClonedFrom_LookupButton);

            return this;
        }



        public CaseFormTaskRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickSignificantEvent_YesRadioButton()
        {
            Click(SignificantEvent_YesRadioButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickSignificantEvent_NoRadioButton()
        {
            Click(SignificantEvent_NoRadioButton);

            return this;
        }



        public CaseFormTaskRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public CaseFormTaskRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public CaseFormTaskRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public CaseFormTaskRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }


    }
}

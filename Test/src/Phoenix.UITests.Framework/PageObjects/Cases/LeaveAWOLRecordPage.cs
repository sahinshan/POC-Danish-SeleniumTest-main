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
    public class LeaveAWOLRecordPage : CommonMethods
    {
        public LeaveAWOLRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By inpatientleaveawolIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientleaveawol&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By RelativeItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']");
        readonly By CWNavItem_InpatientLeaveAwolCaseNote = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolCaseNote']");



        readonly By appointment_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Appointment']");
        readonly By caseNotes_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolCaseNote']");
        readonly By emails_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By letters_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By tasks_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Task']");

        readonly By attachments_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolAttachment']");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");
        readonly By caseField = By.Id("CWField_caseid_Link");
        readonly By namedProfessionalField = By.Id("CWField_namedprofessionalid_cwname");
        readonly By namedProfessionalField_LookUpButton = By.Id("CWLookupBtn_namedprofessionalid");
        readonly By admissionDateField = By.Id("CWField_admissiondatetime");
        readonly By admissionDateTimeField = By.Id("CWField_admissiondatetime_Time");
        readonly By linkedMissingPersonRecordField = By.Id("CWField_linkedmissingpersonrecords_List");
        readonly By linkedMissingPersonRecord_LookUpButton = By.Id("CWLookupBtn_linkedmissingpersonrecords");
        readonly By leaveTypeAWOLField = By.Id("CWField_inpatientleavetypeid");
        readonly By leaveTypeAWOLField_LookUpButton = By.Id("CWLookupBtn_inpatientleavetypeid");
        readonly By agreedLeaveDateField = By.Id("CWField_agreedleavedatetime");
        readonly By agreedLeaveDateTimeField = By.Id("CWField_agreedleavedatetime_Time");
        readonly By agreedReturnDateField = By.Id("CWField_agreedreturndatetime");
        readonly By agreedReturnDateTimeField = By.Id("CWField_agreedreturndatetime_Time");
        readonly By whoAuthorisedLeaveIdField = By.Id("CWField_whoauthorisedleaveid_Link");
        readonly By whoAuthorisedLeaveIdField_LookUpButton = By.Id("CWLookupBtn_whoauthorisedleaveid");
        readonly By actualLeaveDateField = By.Id("CWField_actualleavedatetime");
        readonly By actualLeaveDateTimeField = By.Id("CWField_actualleavedatetime_Time");
        readonly By actualReturnDateField = By.Id("CWField_actualreturndatetime");
        readonly By actualReturnDateField_Disabled = By.XPath("//*[@id='CWField_actualreturndatetime_DatePicker' and  @disabled= 'disabled']");
        readonly By actualReturnDateTimeField = By.Id("CWField_actualreturndatetime_Time");
        readonly By endReason_LookUpButton = By.Id("CWLookupBtn_inpatientleaveendreasonid");
        readonly By endReason_LookUpButton_Disabled = By.Id("//*[@id='CWLookupBtn_inpatientleaveendreasonid' and  @disabled= 'disabled']");
        readonly By returnedToTheBed_YesOption = By.XPath("//*[@id='CWField_returnedtobed_1']");
        
        readonly By returnedToTheBed_NoOption = By.Id("CWField_returnedtobed_0");
        readonly By hospitalField_LookUpButton = By.Id("CWLookupBtn_providerid");
        readonly By wardField_LookUpButton = By.Id("CWLookupBtn_inpatientwardid");
        readonly By bayField_LookUpButton = By.Id("CWLookupBtn_inpatientbayid");
        readonly By bedField_LookUpButton = By.Id("CWLookupBtn_inpatientbedid");
        readonly By conditionsAttachedToLeave = By.Id("CWField_conditionsattachedtoleave");

        readonly By cancelLeave_YesOption_RadioButton = By.Id("CWField_iscancelled_1");
        readonly By cancellationDateField = By.Id("CWField_cancellationdatetime");
        readonly By cancellationTimeField = By.Id("CWField_cancellationdatetime_Time");
        readonly By cancellationReasonField = By.Id("CWField_inpatientleavecancellationreasonid_cwname");
        readonly By cancellationReason_LookUpButton = By.Id("CWLookupBtn_inpatientleavecancellationreasonid");
        readonly By cancelledBy_LookUpButton = By.Id("CWLookupBtn_cancelledbyid");

        readonly By personAwol_YesRadioButton = By.Id("CWField_personawol_1");
        readonly By policeInformed_Field = By.Id("CWField_policeinformedid");
        readonly By linkedMissingPersonRecordsField = By.Id("CWField_linkedmissingpersonrecords_List");
        readonly By classedAsAWOLDateField = By.Id("CWField_classedasawoldatetime");
        readonly By classedAsAWOLDateTimeField = By.Id("CWField_classedasawoldatetime_Time");



        #endregion


        public LeaveAWOLRecordPage WaitForLeaveAWOLRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(inpatientleaveawolIFrame);
            SwitchToIframe(inpatientleaveawolIFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public LeaveAWOLRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public LeaveAWOLRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public LeaveAWOLRecordPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

           
            return this;
        }

        public LeaveAWOLRecordPage TapSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;
        }



        public LeaveAWOLRecordPage NavigateToLeaveAwolCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CWNavItem_InpatientLeaveAwolCaseNote);
            Click(CWNavItem_InpatientLeaveAwolCaseNote);

            return this;
        }

        public LeaveAWOLRecordPage NavigateToAttachmentArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelativeItems_LeftMenu);
            Click(RelativeItems_LeftMenu);

            WaitForElementToBeClickable(attachments_MenuLeftSubMenu);
            Click(attachments_MenuLeftSubMenu);

            return this;
        }

        public LeaveAWOLRecordPage NavigateToActivities()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            return this;
        }

        public LeaveAWOLRecordPage NavigateToRelatedItems()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelativeItems_LeftMenu);
            Click(RelativeItems_LeftMenu);

            return this;
        }




        public LeaveAWOLRecordPage InsertAdmissionDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(admissionDateField);
            SendKeys(admissionDateField, DateToInsert);

            SendKeysWithoutClearing(admissionDateField, Keys.Tab);

            WaitForElementToBeClickable(admissionDateTimeField);
            SendKeys(admissionDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage InsertAgreedLeaveDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(agreedLeaveDateField);
            SendKeys(agreedLeaveDateField, DateToInsert);

            SendKeysWithoutClearing(agreedLeaveDateField, Keys.Tab);

            WaitForElement(agreedLeaveDateTimeField);
            SendKeys(agreedLeaveDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage InsertAgreedReturnLeaveDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(agreedReturnDateField);
            SendKeys(agreedReturnDateField, DateToInsert);

            SendKeysWithoutClearing(agreedReturnDateField, Keys.Tab);

            WaitForElement(agreedReturnDateTimeField);
            SendKeys(agreedReturnDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage InsertActualLeaveDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(actualLeaveDateField);
            SendKeys(actualLeaveDateField, DateToInsert);

            SendKeysWithoutClearing(actualLeaveDateField, Keys.Tab);

            WaitForElement(actualLeaveDateTimeField);
            SendKeys(actualLeaveDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage InsertActualReturnLeaveDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(actualReturnDateField);
            SendKeys(actualReturnDateField, DateToInsert);

            SendKeysWithoutClearing(actualReturnDateField, Keys.Tab);

            WaitForElement(actualReturnDateTimeField);
            SendKeys(actualReturnDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage InsertClassedAsAWOLDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(classedAsAWOLDateField);
            SendKeys(classedAsAWOLDateField, DateToInsert);

            SendKeysWithoutClearing(classedAsAWOLDateField, Keys.Tab);

            WaitForElement(classedAsAWOLDateTimeField);
            SendKeys(classedAsAWOLDateTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage ClickNamedProfessionalLookUpButton()
        {
            WaitForElement(namedProfessionalField_LookUpButton);
            Click(namedProfessionalField_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickHospitalLookUpButton()
        {
            WaitForElement(hospitalField_LookUpButton);
            Click(hospitalField_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickBedLookUpButton()
        {
            WaitForElement(bedField_LookUpButton);
            Click(bedField_LookUpButton);


            return this;
        }


        public LeaveAWOLRecordPage ClickWardLookUpButton()
        {
            WaitForElement(wardField_LookUpButton);
            Click(wardField_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickBayLookUpButton()
        {
            WaitForElement(bayField_LookUpButton);
            Click(bayField_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickWhoAuthorisedLeaveIdLookUpButton()
        {
            WaitForElement(whoAuthorisedLeaveIdField_LookUpButton);
            Click(whoAuthorisedLeaveIdField_LookUpButton);


            return this;
        }
        public LeaveAWOLRecordPage ClickLeaveCancel_YesRadioButton()
        {
            WaitForElement(cancelLeave_YesOption_RadioButton);
            Click(cancelLeave_YesOption_RadioButton);


            return this;
        }


        public LeaveAWOLRecordPage ClickPersonAWOL_YesRadioButton()
        {
            WaitForElement(personAwol_YesRadioButton);
            Click(personAwol_YesRadioButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickEndReasonLookUpButton()
        {
            WaitForElement(endReason_LookUpButton);
            Click(endReason_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage SelectPoliceInformed(string valueToSelect)
        {
            WaitForElement(policeInformed_Field);
            SelectPicklistElementByValue(policeInformed_Field, valueToSelect);

            return this;
        }

        public LeaveAWOLRecordPage ValidatePersonAWOLAdditionalFields()
        {
            WaitForElement(policeInformed_Field);
            ValidateElementEnabled(policeInformed_Field);

            WaitForElement(linkedMissingPersonRecordsField);
            ValidateElementEnabled(linkedMissingPersonRecordsField);

            WaitForElement(classedAsAWOLDateField);
            ValidateElementEnabled(classedAsAWOLDateField);

            WaitForElement(classedAsAWOLDateTimeField);
            ValidateElementEnabled(classedAsAWOLDateTimeField);


            return this;
        }

        public LeaveAWOLRecordPage ValidateActualReturnDateFieldDisabled()
        {
            WaitForElement(actualReturnDateField_Disabled);
            ValidateElementDisabled(actualReturnDateField_Disabled);

            return this;
        }

        public LeaveAWOLRecordPage ValidateEndReasonLookUpButtonDisabled()
        {
            WaitForElement(endReason_LookUpButton);
            ValidateElementDisabled(endReason_LookUpButton);

            return this;
        }

        public LeaveAWOLRecordPage ValidateReturnedtotheBedDisabled()
        {
            WaitForElement(returnedToTheBed_YesOption);
            ValidateElementDisabled(returnedToTheBed_YesOption);

            return this;
        }

        public LeaveAWOLRecordPage ClickReturnedToTheBed_NoOption()
        {
            WaitForElement(returnedToTheBed_NoOption);
            Click(returnedToTheBed_NoOption);

            return this;
        }

        public LeaveAWOLRecordPage InsertCancelledLeaveDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(cancellationDateField);
            SendKeys(cancellationDateField, DateToInsert);

            SendKeysWithoutClearing(cancellationDateField, Keys.Tab);

            WaitForElement(cancellationTimeField);
            SendKeys(cancellationTimeField, TimeToInsert);


            return this;
        }

        public LeaveAWOLRecordPage ClickCancellationReasonLookUpButton()
        {
            WaitForElement(cancellationReason_LookUpButton);
            Click(cancellationReason_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickCancellationByLookUpButton()
        {
            WaitForElement(cancelledBy_LookUpButton);
            Click(cancelledBy_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickLinkedMissingPersonRecordLookUpButton()
        {
            WaitForElement(linkedMissingPersonRecord_LookUpButton);
            Click(linkedMissingPersonRecord_LookUpButton);


            return this;
        }

        public LeaveAWOLRecordPage ClickLeaveTypeLookUpButton()
        {
            WaitForElement(leaveTypeAWOLField_LookUpButton);
            Click(leaveTypeAWOLField_LookUpButton);


            return this;
        }


        public LeaveAWOLRecordPage ValidateActivitiesSubMenuAvailable()
        {
            WaitForElement(appointment_MenuLeftSubMenu);
            ValidateElementEnabled(appointment_MenuLeftSubMenu);

            WaitForElement(caseNotes_MenuLeftSubMenu);
            ValidateElementEnabled(caseNotes_MenuLeftSubMenu);

            WaitForElement(emails_MenuLeftSubMenu);
            ValidateElementEnabled(emails_MenuLeftSubMenu);

            WaitForElement(letters_MenuLeftSubMenu);
            ValidateElementEnabled(letters_MenuLeftSubMenu);



            WaitForElement(tasks_MenuLeftSubMenu);
            ValidateElementEnabled(tasks_MenuLeftSubMenu);

            return this;
        }

        public LeaveAWOLRecordPage ValidateRelativeItemsSubMenuAvailable()
        {
            WaitForElement(attachments_MenuLeftSubMenu);
            ValidateElementEnabled(attachments_MenuLeftSubMenu);

            WaitForElement(audit_MenuLeftSubMenu);
            ValidateElementEnabled(audit_MenuLeftSubMenu);


            return this;
        }
    }
}
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewRecordPage : CommonMethods
    {
        public StaffReviewRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By StaffReviewsystemUser_IFrame = By.Id("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By StaffReviewsystemUser_IFrame2 = By.Id("CWNavItem_SystemUserStaffReviewFrame");
        readonly By StaffReviewFormGrid_IFrame = By.Id("CWNavItem_SystemUserStaffReviewFrame");
        readonly By RecordStaffReview_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreview&')]");

        readonly By RelatedItemMenu = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By attachment_linkFeild = By.XPath("//*[@id='CWNavItem_Attachments']");
        readonly By AttachmentMenuText = By.XPath("//*[@id='CWNavItem_Attachments']/span");

        readonly By Form_LinkField = By.XPath("//*[@id='CWNavItem_StaffReviewForm']");

        readonly By status_id = By.XPath("//select[@id='CWField_statusid']");
        readonly By outcome_id = By.XPath("//select[@id='CWField_outcomeid']");

        readonly By regardinguser_LookUp = By.XPath("//button[@id='CWLookupBtn_regardinguserid']");
        readonly By regardinguser_Field = By.Id("CWField_regardinguserid_cwname");
        readonly By regardinguser_FieldText = By.Id("CWField_regardinguserid_Link");

        readonly By roleId_LookUp = By.XPath("//button[@id='CWLookupBtn_roleid']");
        readonly By roleId_Link_Field= By.Id("CWField_roleid_Link");
        readonly By reviewTypeId_LookUp = By.XPath("//button[@id='CWLookupBtn_reviewtypeid']");
        readonly By reviewedById_LookUp = By.XPath("//button[@id='CWLookupBtn_reviewedbyid']");
        readonly By reviewedBy_Field = By.XPath("//input[@id='CWField_reviewedbyid_cwname']");
        readonly By provider_LookUp = By.XPath("//button[@id='CWLookupBtn_providerid']");

        readonly By startReviewTime_Field = By.XPath("//input[@id='CWField_reviewstarttime']");
        readonly By endReviewTime_Field = By.XPath("//input[@id='CWField_reviewendtime']");

        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        readonly By appointment_Alert = By.XPath("//div[@class='alert alert-danger']");

        readonly By dueDate_Field = By.XPath("//input[@id='CWField_duedate']");
        readonly By nextReviewDate_Field = By.XPath("//input[@id='CWField_nextreviewdate']");
        readonly By completedDate_Field = By.Id("CWField_completeddate");
        readonly By completedOption_Field = By.XPath("//select/option[text()='Completed']");
        readonly By inProgressOption_Field = By.XPath("//select/option[text()='In progress']");
        readonly By outstandingOption_Field = By.XPath("//select/option[text()='Outstanding']");
        readonly By ReviewTypeText_Field = By.Id("CWField_reviewtypeid_Link");
        readonly By ReviewType_Field = By.Id("CWField_reviewtypeid_cwname");
        readonly By Location_Field = By.XPath("//input[@id='CWField_location']");
        readonly By GeneralComment_Field = By.Id("CWField_generalcomment");

        readonly By stautsField_Label = By.XPath("//label[text()='Status']");
        readonly By NameField_Label = By.XPath("//label[text()='Name']");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By WarningMainArea(string ExpactedText) => By.XPath("//div[@id='CWNotificationHolder_DataForm']/div[@id='CWNotificationMessage_DataForm'][text()='" + ExpactedText + "']");

        public StaffReviewRecordPage WaitForStaffReviewRecordPageToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            WaitForElement(RecordStaffReview_IFrame);
            SwitchToIframe(RecordStaffReview_IFrame);
            WaitForElement(RelatedItemMenu);
            WaitForElement(attachment_linkFeild);

            return this; 
        }


        public StaffReviewRecordPage WaitForStaffReviewRecordPageToLoad1()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            WaitForElement(StaffReviewsystemUser_IFrame2);
            SwitchToIframe(StaffReviewsystemUser_IFrame2);
            WaitForElement(RecordStaffReview_IFrame);
            SwitchToIframe(RecordStaffReview_IFrame);

            return this;
        }
        public StaffReviewRecordPage WaitForStaffReviewNewRecordCreatePageToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            WaitForElement(RecordStaffReview_IFrame);
            SwitchToIframe(RecordStaffReview_IFrame);
            WaitForElement(regardinguser_LookUp);
            WaitForElement(roleId_LookUp);
            WaitForElement(reviewTypeId_LookUp);
            WaitForElement(completedDate_Field);

            return this;
        }
        public StaffReviewRecordPage WaitForStaffReviewFormsToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(RecordStaffReview_IFrame);
            SwitchToIframe(RecordStaffReview_IFrame);

            WaitForElement(StaffReviewFormGrid_IFrame);
            SwitchToIframe(StaffReviewFormGrid_IFrame);

            return this;
        }

        public StaffReviewRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public StaffReviewRecordPage ClickSubMenu()
        {
                WaitForElement(RelatedItemMenu);
                Click(RelatedItemMenu);
                return this;
         }
        public StaffReviewRecordPage ClickRegardinguserLookUp()
        {
            WaitForElement(regardinguser_LookUp);
            Click(regardinguser_LookUp);
            return this;
        }
        public StaffReviewRecordPage ClickRoleLookUp()
        {
            WaitForElement(roleId_LookUp);
            Click(roleId_LookUp);
            return this;
        }
        public StaffReviewRecordPage ClickReviewTypeIdLookUp()
        {
            WaitForElement(reviewTypeId_LookUp);
            Click(reviewTypeId_LookUp);
            return this;
        }
        public StaffReviewRecordPage ClickReviewedByIdLookUp()
        {
            WaitForElement(reviewedById_LookUp);
            Click(reviewedById_LookUp);
            return this;
        }

        public StaffReviewRecordPage SelectStatusOption(string selectText)
        {
            WaitForElement(status_id);
            System.Threading.Thread.Sleep(3000);
            SelectPicklistElementByText(status_id, selectText);
         
            return this;
        }
        public StaffReviewRecordPage InsertNextReviewDate(string StringToInsert)
        {
            WaitForElement(nextReviewDate_Field);
            SendKeys(nextReviewDate_Field, StringToInsert);
         
            return this;
        }

        public StaffReviewRecordPage InsertCompletedDate(string StringToInsert)
        {
            WaitForElement(completedDate_Field);
            SendKeys(completedDate_Field, StringToInsert);

            return this;
        }
        public StaffReviewRecordPage InsertReviewStartTime(string StringToInsert)
        {
            WaitForElement(startReviewTime_Field);
            SendKeys(startReviewTime_Field, StringToInsert);

            return this;
        }
        public StaffReviewRecordPage InsertReviewEndTime(string StringToInsert)
        {
            WaitForElement(endReviewTime_Field);
            SendKeys(endReviewTime_Field, StringToInsert);
           
            return this;
        }

        public StaffReviewRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public StaffReviewRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public StaffReviewRecordPage ValidateTopAreaWarningMessage(string ExpectedMessage)
        {
            WaitForElementVisible(WarningMainArea(ExpectedMessage));

            return this;
        }
        public StaffReviewRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }
        public StaffReviewRecordPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public StaffReviewRecordPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            return this;
        }
        public StaffReviewRecordPage SelectRecord(string recordID)
        {
            System.Threading.Thread.Sleep(600);
            WaitForElement(RecordRowCheckBox(recordID));
            this.Click(RecordRowCheckBox(recordID));
            return this;
        }
        public StaffReviewRecordPage ClickAttachmentLink()
        {
            WaitForElementToBeClickable(attachment_linkFeild);
            Click(attachment_linkFeild);
            return this;
        }

        public StaffReviewRecordPage ValidateAttachmentMenu(string verifytext)
        {
            WaitForElement(AttachmentMenuText);
            ValidateElementText(AttachmentMenuText, verifytext);
            return this;
        }

        public StaffReviewRecordPage ValidateNextReviewDate(string ExpectedText)
        {
            WaitForElement(nextReviewDate_Field);
            ValidateElementText(nextReviewDate_Field, ExpectedText);

            return this;
        }
        public StaffReviewRecordPage ValidateAppointmentAlertMessage(string ExpectedText)
        {
            WaitForElement(appointment_Alert);
            ValidateElementText(appointment_Alert, ExpectedText);

            return this;
        }
        public StaffReviewRecordPage ValidateCompletedDateRefilled(string ExpectedText)
        {
            WaitForElement(completedDate_Field);
            ValidateElementText(completedDate_Field, ExpectedText);

            return this;
        }
        public StaffReviewRecordPage ValidateCompletedEditable()
        {
            WaitForElement(completedDate_Field);
            ValidateElementNotDisabled(completedDate_Field);

            return this;
        }
        public StaffReviewRecordPage ValidateExsistingRecordEditable()
        {
            WaitForElement(reviewTypeId_LookUp);
            ValidateElementEnabled(reviewTypeId_LookUp);

            return this;
        }
        public StaffReviewRecordPage ValidateStatusField()
        {
            WaitForElement(stautsField_Label);
            ValidateElementNotDisabled(stautsField_Label);

            return this;
        }
        public StaffReviewRecordPage ValidateCompletedOption()
        {
            WaitForElement(completedOption_Field);
            ValidateElementNotDisabled(completedOption_Field);

            return this;
        }
        public StaffReviewRecordPage ValidateInProgressOption()
        {
            WaitForElement(inProgressOption_Field);
            ValidateElementNotDisabled(inProgressOption_Field);

            return this;
        }
        public StaffReviewRecordPage ValidateOutstandingOption()
        {
            WaitForElement(outstandingOption_Field);
            ValidateElementNotDisabled(outstandingOption_Field);

            return this;
        }
        public StaffReviewRecordPage ValidateNameFieldNotPresent()
        {
            Assert.IsFalse(GetElementVisibility(NameField_Label));

            return this;
        }
        public StaffReviewRecordPage ValidateDisplayedFormsLink(string ExpectedText)
        {
            WaitForElement(Form_LinkField);
            ValidateElementText(Form_LinkField, ExpectedText);

            return this;
        }
        public StaffReviewRecordPage ClickFormsLink()
        {
            WaitForElement(Form_LinkField);
            Click(Form_LinkField);

            return this;
        }


        public StaffReviewRecordPage ClickRegardingFieldLink()
        {
            WaitForElementToBeClickable(regardinguser_FieldText);
            MoveToElementInPage(regardinguser_FieldText);
            Click(regardinguser_FieldText);
            return this;
        }

        public StaffReviewRecordPage ValidateAllFieldsDisableMode()
        {
            System.Threading.Thread.Sleep(1000);

            WaitForElementVisible(regardinguser_LookUp);
            ValidateElementDisabled(regardinguser_LookUp);
            
            WaitForElement(roleId_LookUp);
            ValidateElementDisabled(roleId_LookUp);

            WaitForElement(reviewTypeId_LookUp);
            ValidateElementDisabled(reviewTypeId_LookUp);

            WaitForElement(reviewedById_LookUp);
            ValidateElementDisabled(reviewedById_LookUp);

            WaitForElement(status_id);
            ValidateElementDisabled(status_id);

            WaitForElement(nextReviewDate_Field);
            ValidateElementDisabled(nextReviewDate_Field);

            WaitForElement(startReviewTime_Field);
            ValidateElementDisabled(startReviewTime_Field);

            WaitForElement(endReviewTime_Field);
            ValidateElementDisabled(endReviewTime_Field);

            WaitForElement(dueDate_Field);
            ValidateElementDisabled(dueDate_Field);

            WaitForElement(outcome_id);
            ValidateElementDisabled(outcome_id);

            return this;
        }

        public StaffReviewRecordPage ValidateAllFieldsEnableMode()
        {
            WaitForElement(regardinguser_LookUp);
            ValidateElementDisabled(regardinguser_LookUp);

            WaitForElement(roleId_LookUp);
            ValidateElementEnabled(roleId_LookUp);

            WaitForElement(reviewTypeId_LookUp);
            ValidateElementEnabled(reviewTypeId_LookUp);

            WaitForElement(reviewedById_LookUp);
            ValidateElementEnabled(reviewedById_LookUp);

            WaitForElement(status_id);
            ValidateElementDisabled(status_id);

            WaitForElement(nextReviewDate_Field);
            ValidateElementEnabled(nextReviewDate_Field);

            WaitForElement(startReviewTime_Field);
            ValidateElementEnabled(startReviewTime_Field);

            WaitForElement(endReviewTime_Field);
            ValidateElementEnabled(endReviewTime_Field);

            WaitForElement(dueDate_Field);
            ValidateElementEnabled(dueDate_Field);

            WaitForElement(outcome_id);
            ValidateElementEnabled(outcome_id);

            return this;
        }

        public StaffReviewRecordPage ValidateReviewTypeFieldText(String ExpectText)
        {
            ScrollToElement(ReviewType_Field);
            string fieldText = GetElementValue(ReviewType_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;

        }

        public StaffReviewRecordPage ValidateOutcomeTypeFieldText(String ExpectText)
        {
            
                ValidatePicklistSelectedText(outcome_id, ExpectText);

                return this;
            
        }

        public StaffReviewRecordPage ValidateLocationFieldText(String ExpectText)
        {
            ScrollToElement(Location_Field);
            string fieldText = GetElementValue(Location_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }

        public StaffReviewRecordPage ValidateGeneralCommentFieldText(String ExpectText)
        {
            ScrollToElement(GeneralComment_Field);
            string fieldText = GetElementValue(GeneralComment_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }

        public StaffReviewRecordPage ValidateRegardingUserFieldText(String ExpectText)
        {
            MoveToElementInPage(regardinguser_Field);            
            string fieldText = GetElementValue(regardinguser_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }

        public StaffReviewRecordPage ValidateRegardingUserLinkFieldText(String ExpectText)
        {
            WaitForElementToBeClickable(regardinguser_FieldText);
            ScrollToElement(regardinguser_FieldText);
            string fieldText = GetElementByAttributeValue(regardinguser_FieldText, "title");
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }

        public StaffReviewRecordPage ValidateDueDateFieldText(String ExpectText)
        {
            ScrollToElement(dueDate_Field);
            string fieldText = GetElementValue(dueDate_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public StaffReviewRecordPage ValidateRoleIdFieldText(String ExpectText)
        {
            ScrollToElement(roleId_Link_Field);
            string fieldText = GetElementText(roleId_Link_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }


        public StaffReviewRecordPage ValidateReviewedByFieldText(String ExpectText)
        {
            ScrollToElement(reviewedBy_Field);
            string fieldText = GetElementValue(reviewedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }

        public StaffReviewRecordPage ClickProviderLookUp()
        {
            WaitForElementToBeClickable(provider_LookUp);
            Click(provider_LookUp);
            return this;
        }
		
        public StaffReviewRecordPage ValidatStaffReviewRecordPageTitle(string PageTitle)
        {
            WaitForElementVisible(pageHeader);
            MoveToElementInPage(pageHeader);			
            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }

    }
}

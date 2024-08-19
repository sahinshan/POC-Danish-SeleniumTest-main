using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewAttachmentsPage : CommonMethods
    {
        public StaffReviewAttachmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By StaffReview_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreview&')]");
        readonly By StaffReviewAttachments_Iframe = By.Id("CWUrlPanel_IFrame");
        readonly By BulkUploadFile_Iframe = By.Id("iframe_CWBulkUploadFileDialog");

        readonly By quickSearch_TextBox = By.XPath("//*[@id='CWQuickSearch']");

        readonly By recordsearch_Button = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By uploadMultiple_Button = By.XPath("//div[@id='CWToolbarButtons']/button[@id='TI_UploadMultipleButton']/span[text()='Upload Multiple Files']");
        readonly By BulkCreateButton = By.Id("TI_UploadMultipleButton");

        readonly By AttachmentPageText = By.XPath("//h1[text()='Attachments (For Staff Review)']");
        readonly By CreateNewRecordButtonText = By.XPath("//button[@id='TI_NewRecordButton']/span");
        readonly By titleText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[3]");
        readonly By documentTypeText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[5]");
        readonly By subDocumentTypeText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[6]");

        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By staffReviewRecordText(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        readonly By documenttype_LookUP = By.Id("CWLookupBtn_CWDocumentTypeId"); 
        readonly By documentsubtype_LookUP = By.Id("CWLookupBtn_CWDocumentSubTypeId");

        readonly By documentType_FieldErrorLable = By.XPath("//*[@id='CWDocumentTypeId_Container']/label[2]/span");
        readonly By documentSubType_FieldErrorLable = By.XPath("//*[@id='CWDocumentSubTypeId_Container']/label[2]/span");

        public StaffReviewAttachmentsPage WaitForStaffReviewAttachmentsPageToLoad()
        {
            SwitchToDefaultFrame();
            
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            
            WaitForElement(StaffReview_Iframe);
            SwitchToIframe(StaffReview_Iframe);
            
            WaitForElement(StaffReviewAttachments_Iframe);
            SwitchToIframe(StaffReviewAttachments_Iframe);

            return this;
        }
        public StaffReviewAttachmentsPage WaitForUploadMulitpleFilesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(StaffReview_Iframe);
            SwitchToIframe(StaffReview_Iframe);

            WaitForElement(StaffReviewAttachments_Iframe);
            SwitchToIframe(StaffReviewAttachments_Iframe);

            WaitForElement(BulkUploadFile_Iframe);
            SwitchToIframe(BulkUploadFile_Iframe);

            WaitForElement(documenttype_LookUP);
            WaitForElement(documentsubtype_LookUP);

            return this;
        }
        public StaffReviewAttachmentsPage ClickCreateNewRecord()
        {
            WaitForElement(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }
        public StaffReviewAttachmentsPage OpenRecord(string recordID)
        {
            WaitForElement(RecordRow(recordID));
            this.Click(RecordRow(recordID));
            return this;
        }
        public StaffReviewAttachmentsPage InsertQuickSearchText(string userrecord)
        {
            WaitForElement(quickSearch_TextBox);
            this.SendKeys(quickSearch_TextBox, userrecord);

            return this;
        }
        public StaffReviewAttachmentsPage ClickQuickSearchButton()
        {
            WaitForElement(recordsearch_Button);
            Click(recordsearch_Button);
            
            return this;
        }

        public StaffReviewAttachmentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public StaffReviewAttachmentsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }
        public StaffReviewAttachmentsPage ClickBulkCreateButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(BulkCreateButton);
            Click(BulkCreateButton);

                return this;
        }
        public StaffReviewAttachmentsPage SelectAttchmentRecord(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            this.Click(RecordRowCheckBox(recordID));
            return this;
        }
        public StaffReviewAttachmentsPage ValidateRecord(string recordId, string verifytext)
        {
            ValidateElementText(staffReviewRecordText(recordId), verifytext);
            
            return this;
        }
      
        public StaffReviewAttachmentsPage ValidateAttachmentDisplayed(string verifytext)
        {
            ValidateElementText(AttachmentPageText, verifytext);
            return this;
        }
        public StaffReviewAttachmentsPage ValidateNewRecordCreateButton(string verifytext)
        {
            ValidateElementText(CreateNewRecordButtonText, verifytext);
            return this;
        }
        public StaffReviewAttachmentsPage ValidateRecordNotExist(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            ValidateElementNotDisabled(RecordRowCheckBox(recordID));
            return this;
        }
        public StaffReviewAttachmentsPage ValidateElementDoNotExist(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            ValidateElementDoNotExist(RecordRowCheckBox(recordID));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateTitleCellText(string verifytext)
        {
            ValidateElementText(titleText, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateDocumentTypeCellText(string verifytext)
        {
            ValidateElementText(documentTypeText, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateSubDocumentTypeCellText(string verifytext)
        {
            ValidateElementText(subDocumentTypeText, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateUploadMultipleButton(string verifytext)
        {
            ValidateElementText(uploadMultiple_Button, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateDocumentTypeFieldErrorLable(string verifytext)
        {
            ValidateElementText(documentType_FieldErrorLable, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateDocumentSubTypeFieldErrorLable(string verifytext)
        {
            ValidateElementText(documentSubType_FieldErrorLable, (verifytext));

            return this;
        }
        public StaffReviewAttachmentsPage ValidateUploadMultipleButtonNotDisplay()
        {
            ValidateElementDoNotExist(uploadMultiple_Button);

            return this;
        }
        public StaffReviewAttachmentsPage ValidateDeleteButtonNotDisplay()
        {
            ValidateElementDoNotExist(DeleteRecord_Button);

            return this;
        }
    }
}


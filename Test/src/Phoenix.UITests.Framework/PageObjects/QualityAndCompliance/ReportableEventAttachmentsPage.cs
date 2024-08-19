using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventAttachmentsPage : CommonMethods
    {
        public ReportableEventAttachmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");
        readonly By ReportableEventAttachments_Iframe = By.Id("CWUrlPanel_IFrame");
        readonly By BulkUploadFile_Iframe = By.Id("iframe_CWBulkUploadFileDialog");

        readonly By quickSearch_TextBox = By.XPath("//*[@id='CWQuickSearch']");

        readonly By recordsearch_Button = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");
        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By uploadMultiple_Button = By.XPath("//div[@id='CWToolbarButtons']/button[@id='TI_UploadMultipleButton']/span[text()='Upload Multiple Files']");
        readonly By BulkCreateButton = By.Id("TI_UploadMultipleButton");

        readonly By AttachmentPageText = By.XPath("//h1[text()='Attachments (For Staff Review)']");
        readonly By CreateNewRecordButtonText = By.XPath("//button[@id='TI_NewRecordButton']/span");
        readonly By titleText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[2]");
        readonly By documentTypeText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[5]");
        readonly By subDocumentTypeText = By.XPath("//table[@id='CWGrid']/tbody/tr/td[6]");

        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By staffReviewRecordText(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        readonly By documenttype_LookUP = By.Id("CWLookupBtn_CWDocumentTypeId"); 
        readonly By documentsubtype_LookUP = By.Id("CWLookupBtn_CWDocumentSubTypeId");

        readonly By documentType_FieldErrorLable = By.XPath("//*[@id='CWDocumentTypeId_Container']/label[2]/span");
        readonly By documentSubType_FieldErrorLable = By.XPath("//*[@id='CWDocumentSubTypeId_Container']/label[2]/span");

        public ReportableEventAttachmentsPage WaitForReportableEventAttachmentsPageToLoad()
        {
            SwitchToDefaultFrame();
            
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            
            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);
            
            WaitForElement(ReportableEventAttachments_Iframe);
            SwitchToIframe(ReportableEventAttachments_Iframe);

            return this;
        }
        public ReportableEventAttachmentsPage WaitForUploadMulitpleFilesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);

            WaitForElement(ReportableEventAttachments_Iframe);
            SwitchToIframe(ReportableEventAttachments_Iframe);

            WaitForElement(BulkUploadFile_Iframe);
            SwitchToIframe(BulkUploadFile_Iframe);

            WaitForElement(documenttype_LookUP);
            WaitForElement(documentsubtype_LookUP);

            return this;
        }
        public ReportableEventAttachmentsPage ClickCreateNewRecord()
        {
            WaitForElement(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }
        public ReportableEventAttachmentsPage OpenRecord(string recordID)
        {
            WaitForElement(RecordRow(recordID));
            this.Click(RecordRow(recordID));
            return this;
        }
        public ReportableEventAttachmentsPage InsertQuickSearchText(string userrecord)
        {
            WaitForElement(quickSearch_TextBox);
            this.SendKeys(quickSearch_TextBox, userrecord);

            return this;
        }
        public ReportableEventAttachmentsPage ClickQuickSearchButton()
        {
            WaitForElement(recordsearch_Button);
            Click(recordsearch_Button);
            
            return this;
        }
        public ReportableEventAttachmentsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }
        public ReportableEventAttachmentsPage ClickBulkCreateButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(BulkCreateButton);
            Click(BulkCreateButton);

                return this;
        }
        public ReportableEventAttachmentsPage SelectAttchmentRecord(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            this.Click(RecordRowCheckBox(recordID));
            return this;
        }
        public ReportableEventAttachmentsPage ValidateRecord(string recordId, string verifytext)
        {
            ValidateElementText(staffReviewRecordText(recordId), verifytext);
            
            return this;
        }
      
        public ReportableEventAttachmentsPage ValidateAttachmentDisplayed(string verifytext)
        {
            ValidateElementText(AttachmentPageText, verifytext);
            return this;
        }
        public ReportableEventAttachmentsPage ValidateNewRecordCreateButton(string verifytext)
        {
            ValidateElementText(CreateNewRecordButtonText, verifytext);
            return this;
        }
        public ReportableEventAttachmentsPage ValidateRecordNotExist(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            ValidateElementNotDisabled(RecordRowCheckBox(recordID));
            return this;
        }
        public ReportableEventAttachmentsPage ValidateElementDoNotExist(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            ValidateElementDoNotExist(RecordRowCheckBox(recordID));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateTitleCellText(string verifytext)
        {
            ValidateElementText(titleText, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateDocumentTypeCellText(string verifytext)
        {
            ValidateElementText(documentTypeText, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateSubDocumentTypeCellText(string verifytext)
        {
            ValidateElementText(subDocumentTypeText, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateUploadMultipleButton(string verifytext)
        {
            ValidateElementText(uploadMultiple_Button, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateDocumentTypeFieldErrorLable(string verifytext)
        {
            ValidateElementText(documentType_FieldErrorLable, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateDocumentSubTypeFieldErrorLable(string verifytext)
        {
            ValidateElementText(documentSubType_FieldErrorLable, (verifytext));

            return this;
        }
        public ReportableEventAttachmentsPage ValidateUploadMultipleButtonNotDisplay()
        {
            ValidateElementDoNotExist(uploadMultiple_Button);

            return this;
        }
        public ReportableEventAttachmentsPage ValidateDeleteButtonNotDisplay()
        {
            ValidateElementDoNotExist(DeleteRecord_Button);

            return this;
        }
    }
}


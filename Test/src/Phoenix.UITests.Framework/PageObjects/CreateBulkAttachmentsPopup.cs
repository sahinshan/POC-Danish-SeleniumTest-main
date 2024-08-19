using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CreateBulkAttachmentsPopup : CommonMethods
    {
        public CreateBulkAttachmentsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWBulkUploadFileDialog");

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderText']");

        #region Field Titles
        
        readonly By DocumentType_FieldLabel = By.XPath("//*[@id='CWDocumentTypeId_Container']/*[text()='Document Type']");
        readonly By DocumentSubType_FieldLabel = By.XPath("//*[@id='CWDocumentSubTypeId_Container']/*[text()='Document Sub Type']");
        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWOwnerId_Container']/*[text()='Responsible Team']");

        readonly By FilesSectionTitle = By.XPath("//*[@id='CWSection_Attachments']/fieldset/div/span[text()='Files']");

        #endregion

        #region Fields

        readonly By DocumentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_CWDocumentTypeId']");
        readonly By DocumentTypeErrorLabel = By.XPath("//*[@for='CWDocumentTypeId'][@class='formerror']/span");
        readonly By DocumentSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_CWDocumentSubTypeId']");
        readonly By DocumentSubTypeErrorLabel = By.XPath("//*[@for='CWDocumentSubTypeId'][@class='formerror']/span");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_CWOwnerId']");

        readonly By FilesInput = By.XPath("//*[@class='btn btn-success fileinput-button']/input[@type='file']");
        readonly By StartUploadButton = By.XPath("//*[@id='CWUploadAll']");
        readonly By CancelUploadButton = By.XPath("//*[@id='CWClose']");

        By AttachedFileName(int FilePosition) => By.XPath("//*[@id='CWSection_Attachments']/fieldset/div/table/tbody/tr[" + FilePosition + "]/td/p[@class='name']");
        By AttachedFileSize(int FilePosition) => By.XPath("//*[@id='CWSection_Attachments']/fieldset/div/table/tbody/tr[" + FilePosition + "]/td/p[@class='size']");
        By AttachedFileCancelUploadButton(int FilePosition) => By.XPath("//*[@id='CWSection_Attachments']/*/*/*/*/tr[" + FilePosition + "]/*/button[@class='btn btn-warning cancel']");


        #endregion

        readonly By CloseButton = By.Id("CWClose");



        public CreateBulkAttachmentsPopup WaitForCreateBulkAttachmentsPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);
            ValidateElementText(popupHeader, "Upload Multiple Files");


            WaitForElement(DocumentType_FieldLabel);
            WaitForElement(DocumentSubType_FieldLabel);
            WaitForElement(ResponsibleTeam_FieldLabel);

            WaitForElement(FilesSectionTitle);

            WaitForElement(DocumentType_LookupButton);
            WaitForElement(DocumentSubType_LookupButton);
            WaitForElement(ResponsibleTeam_LookupButton);

            WaitForElement(FilesInput);
            WaitForElement(StartUploadButton);
            WaitForElement(CancelUploadButton);

            return this;
        }

        public CreateBulkAttachmentsPopup WaitForCreateBulkAttachmentsPopupToReload()
        {
            WaitForElement(popupHeader);
            ValidateElementText(popupHeader, "Upload Multiple Files");


            WaitForElement(DocumentType_FieldLabel);
            WaitForElement(DocumentSubType_FieldLabel);
            WaitForElement(ResponsibleTeam_FieldLabel);

            WaitForElement(DocumentType_LookupButton);
            WaitForElement(DocumentSubType_LookupButton);
            WaitForElement(ResponsibleTeam_LookupButton);

            WaitForElement(FilesInput);
            WaitForElement(StartUploadButton);
            WaitForElement(CancelUploadButton);

            return this;
        }


        public CreateBulkAttachmentsPopup ValidateDocumentTypeErrorLabelText(string ExpectText)
        {
            ValidateElementText(DocumentTypeErrorLabel, ExpectText);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateDocumentSubTypeErrorLabelText(string ExpectText)
        {
            ValidateElementText(DocumentSubTypeErrorLabel, ExpectText);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateAttachedFileNameText(int FilePosition, string ExpectText)
        {
            ValidateElementText(AttachedFileName(FilePosition), ExpectText);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateAttachedFileSizeText(int FilePosition, string ExpectText)
        {
            System.Threading.Thread.Sleep(3000);
            ValidateElementText(AttachedFileSize(FilePosition), ExpectText);

            return this;
        }


        public CreateBulkAttachmentsPopup ValidateDocumentTypeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DocumentTypeErrorLabel);
            else
                WaitForElementNotVisible(DocumentTypeErrorLabel, 7);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateDocumentSubTypeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DocumentSubTypeErrorLabel);
            else
                WaitForElementNotVisible(DocumentSubTypeErrorLabel, 7);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateAttachedFileNameVisibility(int FilePosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AttachedFileName(FilePosition));
            else
                WaitForElementNotVisible(AttachedFileName(FilePosition), 7);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateAttachedFileSizeVisibility(int FilePosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AttachedFileSize(FilePosition));
            else
                WaitForElementNotVisible(AttachedFileSize(FilePosition), 7);

            return this;
        }
        public CreateBulkAttachmentsPopup ValidateAttachedFileCancelUploadButtonVisibility(int FilePosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AttachedFileCancelUploadButton(FilePosition));
            else
                WaitForElementNotVisible(AttachedFileCancelUploadButton(FilePosition), 7);

            return this;
        }


        public CreateBulkAttachmentsPopup ClickDocumentTypeLookupButton()
        {
            Click(DocumentType_LookupButton);


            return this;
        }
        public CreateBulkAttachmentsPopup ClickDocumentSubTypeLookupButton()
        {
            Click(DocumentSubType_LookupButton);


            return this;
        }
        public CreateBulkAttachmentsPopup ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);


            return this;
        }
        public CreateBulkAttachmentsPopup ClickStartUploadButton()
        {
            Click(StartUploadButton);


            return this;
        }
        public CreateBulkAttachmentsPopup ClickCancelUploadButton()
        {
            Click(CancelUploadButton);


            return this;
        }

        public CreateBulkAttachmentsPopup ClickAttachedFileCancelUploadButton(int FilePosition)
        {
            Click(AttachedFileCancelUploadButton(FilePosition));


            return this;
        }
        public CreateBulkAttachmentsPopup ClickCloseButton()
        {
            Click(CloseButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }


        public CreateBulkAttachmentsPopup SelectFileToUpload(string FilePath)
        {
            MoveToElementInPage(FilesInput);
            SendKeysWithoutClearing(FilesInput, FilePath);

            return this;
        }

    }
}

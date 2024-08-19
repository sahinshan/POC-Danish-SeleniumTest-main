using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class AttachmentPage : CommonMethods
    {
        public AttachmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By filePicker = By.Id("CWFilePicker-input");
        readonly By filePickerTextArea = By.XPath("//*[@id='CWFilePicker']/div/label/div[1]");
        readonly By filePickerHelpText = By.XPath("//*[@id='CWFilePicker']/div[4]");
        readonly By resetButton = By.Id("CWAttachmentResetButton");
        readonly By submitButton = By.Id("CWAttachmentSubmitButton");

        readonly By filePickerErrorMessage = By.XPath("//*[@id='CWFilePicker']/div[3]/span");
        
        readonly By successMessage = By.XPath("//*[@id='CWUploadSuccess']/mosaic-alert/div/p");
        readonly By warningMessage = By.XPath("//*[@id='CWAttachmentForm']/form/mosaic-alert/div[2]/p");
        
        readonly By ToastMessage = By.XPath("//*[@class='toastify__message']");
        readonly By ToastMessageCloseButton = By.XPath("//*[@class='toast-close']");


        #region Existing Appointment

        readonly By AttachmentTitle = By.XPath("//*[@id='CWDownloadControls']/mosaic-card/mosaic-card-header/div[1]/div");
        readonly By AttachmentFileName = By.XPath("//*[@id='CWDownloadControls']/mosaic-card/mosaic-card-body/div/mosaic-alert/div[2]/p");
        readonly By AttachmentFileDownloadButton = By.XPath("//*[@id='CWAttachmentDownloadButton']");
        
        #endregion


        public AttachmentPage WaitForAttachmentPageToLoad()
        {
            WaitForBrowserWindowTitle("Attachment");

            WaitForElement(filePicker);
            WaitForElementVisible(filePickerHelpText);
            WaitForElementVisible(resetButton);
            WaitForElementVisible(submitButton);

            return this;
        }
        public AttachmentPage WaitForSuccessMessageToLoad()
        {
            WaitForBrowserWindowTitle("Attachment");

            WaitForElementNotVisible(filePicker, 7);
            WaitForElementNotVisible(filePickerHelpText, 7);
            WaitForElementNotVisible(resetButton, 7);
            WaitForElementNotVisible(submitButton, 7);

            WaitForElementVisible(successMessage);

            return this;
        }
        public AttachmentPage WaitForWarningMessageToLoad()
        {
            WaitForBrowserWindowTitle("Attachment");

            WaitForElementNotVisible(filePicker, 7);
            WaitForElementNotVisible(filePickerHelpText, 7);
            WaitForElementNotVisible(resetButton, 7);
            WaitForElementNotVisible(submitButton, 7);
            WaitForElementNotVisible(successMessage, 7);

            WaitForElementVisible(warningMessage);

            return this;
        }
        public AttachmentPage WaitForExistingAttachmentPageToLoad()
        {
            WaitForBrowserWindowTitle("Attachment");

            WaitForElementNotVisible(filePicker, 7);
            WaitForElementNotVisible(filePickerHelpText, 7);
            WaitForElementNotVisible(resetButton, 7);
            WaitForElementNotVisible(submitButton, 7);
            WaitForElementNotVisible(successMessage, 7);
            WaitForElementNotVisible(warningMessage, 7);

            WaitForElementVisible(AttachmentTitle);
            WaitForElementVisible(AttachmentFileName);
            WaitForElementVisible(AttachmentFileDownloadButton);

            return this;
        }


        public AttachmentPage PickFile(string FilePath)
        {
            SendKeys(filePicker, FilePath);

            return this;
        }



        public AttachmentPage ValidateFilePickerHelpText(string ExpectedText)
        {
            ValidateElementText(filePickerHelpText, ExpectedText);
            
            return this;
        }
        public AttachmentPage ValidateSuccessMessageText(string ExpectedText)
        {
            ValidateElementText(successMessage, ExpectedText);

            return this;
        }
        public AttachmentPage ValidateFilePickerErrorMessageText(string ExpectedText)
        {
            ValidateElementText(filePickerErrorMessage, ExpectedText);

            return this;
        }
        public AttachmentPage ValidateFilePickerTextAreaText(string ExpectedText)
        {
            ValidateElementText(filePickerTextArea, ExpectedText);

            return this;
        }
        public AttachmentPage ValidateToastMessageText(string ExpectedText)
        {
            ValidateElementText(ToastMessage, ExpectedText);

            return this;
        }
        public AttachmentPage ValidateAttachmentTitleText(string ExpectedText)
        {
            ValidateElementText(AttachmentTitle, ExpectedText);

            return this;
        }
        public AttachmentPage ValidateAttachmentFileNameText(string ExpectedText)
        {
            ValidateElementText(AttachmentFileName, ExpectedText);

            return this;
        }



        public AttachmentPage ClickResetButton()
        {
            Click(resetButton);

            return this;
        }
        public AttachmentPage ClickSubmitButton()
        {
            Click(submitButton);

            return this;
        }
        public AttachmentPage ClickDownloadButton()
        {
            Click(AttachmentFileDownloadButton);

            return this;
        }


        public AttachmentPage ValidateFilePickerHelpTextVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(filePickerHelpText);
            else
                WaitForElementNotVisible(filePickerHelpText, 7);

            return this;
        }
        public AttachmentPage ValidateSuccessMessageVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(successMessage);
            else
                WaitForElementNotVisible(successMessage, 7);

            return this;
        }
        public AttachmentPage ValidateFilePickerErrorMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(filePickerErrorMessage);
            else
                WaitForElementNotVisible(filePickerErrorMessage, 7);

            return this;
        }
        public AttachmentPage ValidateToastMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ToastMessage);
                WaitForElementVisible(ToastMessageCloseButton);
            }
            else
            {
                WaitForElementNotVisible(ToastMessage, 7);
                WaitForElementNotVisible(ToastMessageCloseButton, 7);
            }

            return this;
        }
        


    }
    
}

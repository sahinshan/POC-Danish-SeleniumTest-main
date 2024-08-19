using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonEmailAttachmentRecordPage : CommonMethods
    {

        public PersonEmailAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=emailattachment&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        

        readonly By email_FieldLabel = By.XPath("//*[@id='CWLabelHolder_emailid']/label[text()='Email']");
        readonly By file_FieldLabel = By.XPath("//*[@id='CWLabelHolder_fileid']/label[text()='File']");
        readonly By responsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");

        
        readonly By email_FieldLink = By.XPath("//*[@id='CWField_emailid_Link']");
        readonly By email_RemoveButton = By.Id("CWClearLookup_emailid");
        readonly By email_LookupButton = By.Id("CWLookupBtn_emailid");

        readonly By file_Field = By.Id("CWField_fileid");
        readonly By file_FieldLink = By.Id("CWField_fileid_FileLink"); 
        readonly By file_FieldErrorArea = By.XPath("//*[@id='CWField_fileid_FileSection']/label/span");
        readonly By browse_Button = By.Id("CWField_fileid");
        readonly By file_Button = By.Id("CWField_fileid_Upload");
        readonly By file_Upload = By.Id("CWField_fileid_UploadButton");


        readonly By responsibleTeam_FieldLink = By.Id("CWField_ownerid_Link");
        
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");



        public PersonEmailAttachmentRecordPage WaitForPersonEmailAttachmentRecordPageToLoad(string EmailTitle)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            if(string.IsNullOrEmpty(EmailTitle))
                ValidateElementText(pageHeader, "Email Attachment:");
            else
                ValidateElementText(pageHeader, "Email Attachment:\r\n" + EmailTitle);

            return this;
        }

        public PersonEmailAttachmentRecordPage WaitForPersonEmailAttachmentRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            WaitForElementVisible(pageHeader);
            
            return this;
        }



        public PersonEmailAttachmentRecordPage ValidateEmailFieldLabelVisible()
        {
            WaitForElement(email_FieldLabel);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateFileFieldLabelVisible()
        {
            WaitForElement(file_FieldLabel);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateResponsibleTeamFieldLabelVisible()
        {
            WaitForElement(responsibleTeam_FieldLabel);

            return this;
        }


        public PersonEmailAttachmentRecordPage ValidateEmailFieldLinkText(string ExpectedText)
        {
            ValidateElementText(email_FieldLink, ExpectedText);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateFileFieldLinkText(string ExpectedText)
        {
            ValidateElementText(file_FieldLink, ExpectedText);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_FieldLink, ExpectedText);

            return this;
        }


        public PersonEmailAttachmentRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            //WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickSaveAndCloseButtonWithoutWaitingForCWRefreshPanel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickFileFieldLink()
        {
            Click(file_FieldLink);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateNotificationMessage(string ExpectedMessage)
        {
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateFileFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(file_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickBrowseButton()
        {
            Click(browse_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickFileIcon()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(file_Button);
            Click(file_Button);

            return this;
        }

        public PersonEmailAttachmentRecordPage ClickFile1UploadDocument(string FilePath)
        {
            SendKeys(file_Field, FilePath);


            return this;
        }

        public PersonEmailAttachmentRecordPage ClickFileUpload()
        {

            WaitForElement(file_Upload);
            Click(file_Upload);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateLatestFileLink(bool ExpectedFileLink)
        {
            if (ExpectedFileLink)
            {
                WaitForElementVisible(file_FieldLink);
            }
            else
            {
                WaitForElementNotVisible(file_FieldLink, 5);
            }
            return this;
        }

        public PersonEmailAttachmentRecordPage ValidateLatestFileLinkText(String ExpectedFileLinkText)
        {
            ValidateElementText(file_FieldLink, ExpectedFileLinkText);

            return this;
        }

    }
}

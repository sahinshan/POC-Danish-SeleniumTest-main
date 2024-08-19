using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAttachmentRecordPage : CommonMethods
    {

        public PersonAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");


        #region Fields

        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By date_Field = By.XPath("//*[@id='CWField_date']");
        readonly By time_Field = By.XPath("//*[@id='CWField_date_Time']");
        readonly By documentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
        readonly By documentType_LinkField = By.XPath("//*[@id='CWField_documenttypeid_Link']");
        readonly By documentSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");
        readonly By documentSubType_LinkField = By.XPath("//*[@id='CWField_documentsubtypeid_Link']");
        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");

        #endregion

        public PersonAttachmentRecordPage WaitForPersonAttachmentRecordPageToLoad(string PageTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }



        public PersonAttachmentRecordPage InsertTitle(string TextToInsert)
        {
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        
        public PersonAttachmentRecordPage InsertDate(string DateToInsert, string TimeToInsert)
        {
            SendKeys(date_Field, DateToInsert + Keys.Tab);
            SendKeys(time_Field, TimeToInsert + Keys.Tab);

            return this;
        }

        public PersonAttachmentRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickFileUpdateButton()
        {
            Click(fileUpdate_Field);

            return this;
        }

        public PersonAttachmentRecordPage ClickFileUploadButton()
        {
            Click(fileUpload_Field);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickFileLinkField()
        {
            Click(fileLink_Field);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickDocumentTypeLookupButton()
        {
            Click(documentType_LookupButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickDocumentSubTypeLookupButton()
        {
            Click(documentSubType_LookupButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        
        public PersonAttachmentRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }



        public PersonAttachmentRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElement(person_LinkField);
            ValidateElementText(person_LinkField, ExpectedText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateTitleFieldValue(string ExpectedText)
        {
            WaitForElement(title_Field);
            ValidateElementValue(title_Field, ExpectedText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateDateFieldValue(string ExpectedDateText, string ExpectedTimeeText)
        {
            WaitForElement(date_Field);
            WaitForElement(time_Field);

            ValidateElementValue(date_Field, ExpectedDateText);
            ValidateElementValue(time_Field, ExpectedTimeeText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateDocumentTypeLinkText(string ExpectedText)
        {
            WaitForElement(documentType_LinkField);
            ValidateElementText(documentType_LinkField, ExpectedText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateDocumentSubTypeLinkText(string ExpectedText)
        {
            WaitForElement(documentSubType_LinkField);
            ValidateElementText(documentSubType_LinkField, ExpectedText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElement(responsibleTeam_LinkField);
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public PersonAttachmentRecordPage ValidateFileLinkText(string ExpectedText)
        {
            WaitForElement(fileLink_Field);
            ValidateElementText(fileLink_Field, ExpectedText);

            return this;
        }

    }
}

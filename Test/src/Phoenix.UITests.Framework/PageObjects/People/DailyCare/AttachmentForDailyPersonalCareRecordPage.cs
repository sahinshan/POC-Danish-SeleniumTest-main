using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AttachmentForDailyPersonalCareRecordPage : CommonMethods
    {

        public AttachmentForDailyPersonalCareRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpdailypersonalcareattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");


        #region Fields

        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By date_DateField = By.XPath("//*[@id='CWField_date']");
        readonly By date_TimeField = By.XPath("//*[@id='CWField_date_Time']");
        readonly By documentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
        readonly By documentSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");
        

        #endregion

        public AttachmentForDailyPersonalCareRecordPage WaitForPageToLoad()
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



        public AttachmentForDailyPersonalCareRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }




        public AttachmentForDailyPersonalCareRecordPage InsertTitle(string TextToInsert)
        {
            WaitForElementToBeClickable(title_Field);
            SendKeys(title_Field, TextToInsert);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ValidateTitleFieldValue(string ExpectedText)
        {
            WaitForElement(title_Field);
            ValidateElementValue(title_Field, ExpectedText);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage InsertDate(string DateToInsert)
        {
            WaitForElementToBeClickable(date_DateField);

            SendKeys(date_DateField, DateToInsert);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElement(responsibleTeam_LinkField);
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickDocumentTypeLookupButton()
        {
            WaitForElementToBeClickable(documentType_LookupButton);
            Click(documentType_LookupButton);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickDocumentSubTypeLookupButton()
        {
            WaitForElementToBeClickable(documentSubType_LookupButton);
            Click(documentSubType_LookupButton);

            return this;
        }



        public AttachmentForDailyPersonalCareRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }
        
        public AttachmentForDailyPersonalCareRecordPage ClickFileUpdateButton()
        {
            WaitForElementToBeClickable(fileUpdate_Field);
            Click(fileUpdate_Field);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ClickFileUploadButton()
        {
            WaitForElementToBeClickable(fileUpload_Field);
            Click(fileUpload_Field);

            return this;
        }
        
        public AttachmentForDailyPersonalCareRecordPage ClickFileLinkField()
        {
            WaitForElementToBeClickable(fileLink_Field);
            Click(fileLink_Field);

            return this;
        }

        public AttachmentForDailyPersonalCareRecordPage ValidateFileLinkText(string ExpectedText)
        {
            WaitForElement(fileLink_Field);
            ValidateElementText(fileLink_Field, ExpectedText);

            return this;
        }

    }
}

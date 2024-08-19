using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AttachmentForPersonalMoneyAccountRecordPage : CommonMethods
    {

        public AttachmentForPersonalMoneyAccountRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersonalmoneyaccountattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");


        #region Fields

        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");
        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By personalmoneyaccount_LinkField = By.XPath("//*[@id='CWField_personalmoneyaccountid_Link']");

        #endregion

        public AttachmentForPersonalMoneyAccountRecordPage WaitForAttachmentForPersonalMoneyAccountRecordPageToLoad()
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


        public AttachmentForPersonalMoneyAccountRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }




        public AttachmentForPersonalMoneyAccountRecordPage InsertTitle(string TextToInsert)
        {
            WaitForElementToBeClickable(title_Field);
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        
        public AttachmentForPersonalMoneyAccountRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }
        
        public AttachmentForPersonalMoneyAccountRecordPage ClickFileUpdateButton()
        {
            WaitForElementToBeClickable(fileUpdate_Field);
            Click(fileUpdate_Field);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ClickFileUploadButton()
        {
            WaitForElementToBeClickable(fileUpload_Field);
            Click(fileUpload_Field);

            return this;
        }
        
        public AttachmentForPersonalMoneyAccountRecordPage ClickFileLinkField()
        {
            WaitForElementToBeClickable(fileLink_Field);
            Click(fileLink_Field);

            return this;
        }
        
        


        public AttachmentForPersonalMoneyAccountRecordPage ValidatePersonalMoneyAccountLinkText(string ExpectedText)
        {
            WaitForElement(personalmoneyaccount_LinkField);
            ValidateElementText(personalmoneyaccount_LinkField, ExpectedText);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ValidateTitleFieldValue(string ExpectedText)
        {
            WaitForElement(title_Field);
            ValidateElementValue(title_Field, ExpectedText);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElement(responsibleTeam_LinkField);
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public AttachmentForPersonalMoneyAccountRecordPage ValidateFileLinkText(string ExpectedText)
        {
            WaitForElement(fileLink_Field);
            ValidateElementText(fileLink_Field, ExpectedText);

            return this;
        }

    }
}

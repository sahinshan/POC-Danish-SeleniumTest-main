using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserTrainingAttachmentRecordPage : CommonMethods
    {

        public SystemUserTrainingAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemusertrainingattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");


        #region Fields

        readonly By Name_Field = By.XPath("//*[@id='CWField_title']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");

        #endregion

        public SystemUserTrainingAttachmentRecordPage WaitForSystemUserTrainingAttachmentRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElementToBeClickable(saveButton);
            this.WaitForElementToBeClickable(saveAndCloseButton);

            return this;
        }



        public SystemUserTrainingAttachmentRecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public SystemUserTrainingAttachmentRecordPage UploadFile(string FilePath)
        {
            WaitForElement(file_Field);
            SendKeys(file_Field, FilePath);

            return this;
        }



        public SystemUserTrainingAttachmentRecordPage ClickFileUpdateButton()
        {
            Click(fileUpdate_Field);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickFileUploadButton()
        {
            Click(fileUpload_Field);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickFileLinkField()
        {
            Click(fileLink_Field);

            return this;
        }

        public SystemUserTrainingAttachmentRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        public SystemUserTrainingAttachmentRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormAttachmentRecordPage : CommonMethods
    {

        public CaseFormAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseformattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.XPath("//*[@id='TI_CloneButton']");
        

        #region Fields

        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By date_Field = By.XPath("//*[@id='CWField_date']");
        readonly By time_Field = By.XPath("//*[@id='CWField_date_Time']");
        readonly By documentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
        readonly By documentSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");

        #endregion

        public CaseFormAttachmentRecordPage WaitForCaseFormAttachmentRecordPageToLoad(string PageTitle)
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



        public CaseFormAttachmentRecordPage InsertTitle(string TextToInsert)
        {
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        public CaseFormAttachmentRecordPage InsertDate(string DateToInsert, string TimeToInsert)
        {
            SendKeys(date_Field, DateToInsert);
            SendKeys(time_Field, TimeToInsert);

            return this;
        }



        public CaseFormAttachmentRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }



        public CaseFormAttachmentRecordPage ClickFileUpdateButton()
        {
            Click(fileUpdate_Field);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickFileUploadButton()
        {
            Click(fileUpload_Field);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickFileLinkField()
        {
            Click(fileLink_Field);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickDocumentTypeLookupButton()
        {
            Click(documentType_LookupButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickDocumentSubTypeLookupButton()
        {
            Click(documentSubType_LookupButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickCloneButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        public CaseFormAttachmentRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

    }
}

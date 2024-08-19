using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class InpatientSeclusionAttachmentRecordPage : CommonMethods
    {

        public InpatientSeclusionAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientseclusionattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");


        #region Fields

        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        readonly By Date_DateField = By.XPath("//*[@id='CWField_date']");
        readonly By Date_TimeField = By.XPath("//*[@id='CWField_date_Time']");

        readonly By documentType_LinkField = By.XPath("//*[@id='CWField_documenttypeid_Link']");
        readonly By documentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
        readonly By documentType_RemoveButton = By.XPath("//*[@id='CWClearLookup_documenttypeid']");

        readonly By documentSubType_LinkField = By.XPath("//*[@id='CWField_documentsubtypeid_Link']");
        readonly By documentSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");
        readonly By documentSubType_RemoveButton = By.XPath("//*[@id='CWClearLookup_documentsubtypeid']");

        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");

        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileUpdate_Field = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By fileUpload_Field = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By fileLink_Field = By.XPath("//*[@id='CWField_fileid_FileLink']");


        readonly By IsCloned_YesRadioButton = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By IsCloned_NoRadioButton = By.XPath("//*[@id='CWField_iscloned_0']");

        readonly By ClonedFrom_LinkField = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By ClonedFrom_LookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");

        readonly By seclusion_LookUpButton = By.Id("CWLookupBtn_inpatientseclusionid");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        #endregion

        public InpatientSeclusionAttachmentRecordPage WaitForInpatientSeclusionAttachmentRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }



        public InpatientSeclusionAttachmentRecordPage InsertTitle(string TextToInsert)
        {
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage InsertDate(string DateToInsert, string TimeToInsert)
        {
            SendKeys(Date_DateField, DateToInsert);
            SendKeysWithoutClearing(Date_DateField, Keys.Tab);
            SendKeys(Date_TimeField, TimeToInsert);

            return this;
        }



        public InpatientSeclusionAttachmentRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }



        public InpatientSeclusionAttachmentRecordPage ClickFileUpdateButton()
        {
            Click(fileUpdate_Field);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickFileUploadButton()
        {
            Click(fileUpload_Field);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickFileLinkField()
        {
            Click(fileLink_Field);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickDocumentTypeLookupButton()
        {
            Click(documentType_LookupButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickDocumentTypeRemoveButton()
        {
            Click(documentType_RemoveButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickDocumentSubTypeLookupButton()
        {
            Click(documentSubType_LookupButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickDocumentSubTypeRemoveButton()
        {
            Click(documentSubType_RemoveButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickSeclusionLookupButton()
        {
            Click(seclusion_LookUpButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }        
        public InpatientSeclusionAttachmentRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }



        public InpatientSeclusionAttachmentRecordPage ValidateTitle(string ExpectedText)
        {
            ValidateElementValue(title_Field, ExpectedText);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ValidateDate(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(Date_DateField, ExpectedDate);
            ValidateElementValue(Date_TimeField, ExpectedTime);

            return this;
        }


       
        public InpatientSeclusionAttachmentRecordPage ValidateDocumentTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(documentType_LinkField, ExpectedText);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ValidateDocumentSubTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(documentSubType_LinkField, ExpectedText);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ValidateFileLinkFieldText(string ExpectedText)
        {
            ValidateElementText(fileLink_Field, ExpectedText);

            return this;
        }
        public InpatientSeclusionAttachmentRecordPage ValidateClonedFromLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ClonedFrom_LinkField, ExpectedText);

            return this;
        }



        public InpatientSeclusionAttachmentRecordPage ValidateIsClonedChecked(bool ExpectYes)
        {
            if(ExpectYes)
            {
                ValidateElementChecked(IsCloned_YesRadioButton);
                ValidateElementNotChecked(IsCloned_NoRadioButton);
            }else
            {
                ValidateElementNotChecked(IsCloned_YesRadioButton);
                ValidateElementChecked(IsCloned_NoRadioButton);
            }

            return this;
        }

        public InpatientSeclusionAttachmentRecordPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }


        public InpatientSeclusionAttachmentRecordPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

    }
}

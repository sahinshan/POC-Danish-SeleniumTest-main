using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Cases
{
    public class CaseAttachmentRecordPage : CommonMethods
    {

        public CaseAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseattachment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By ShareRecordButton = By.XPath("//*[@id='TI_ShareRecordButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By CloneButton = By.XPath("//*[@id='TI_CloneButton']");


        readonly By errorMessage = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By CaseidLink = By.XPath("//*[@id='CWField_caseid_Link']");
        readonly By CaseidLookupButton = By.XPath("//*[@id='CWLookupBtn_caseid']");
        readonly By Title = By.XPath("//*[@id='CWField_title']");
        readonly By Date = By.XPath("//*[@id='CWField_date']");
        readonly By DateDatePicker = By.XPath("//*[@id='CWField_date_DatePicker']");
        readonly By Date_Time = By.XPath("//*[@id='CWField_date_Time']");
        readonly By Date_Time_TimePicker = By.XPath("//*[@id='CWField_date_Time_TimePicker']");
        readonly By DocumenttypeidLink = By.XPath("//*[@id='CWField_documenttypeid_Link']");
        readonly By DocumenttypeidClearButton = By.XPath("//*[@id='CWClearLookup_documenttypeid']");
        readonly By DocumenttypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
        readonly By DocumentsubtypeidLink = By.XPath("//*[@id='CWField_documentsubtypeid_Link']");
        readonly By DocumentsubtypeidClearButton = By.XPath("//*[@id='CWClearLookup_documentsubtypeid']");
        readonly By DocumentsubtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By file_Field = By.XPath("//*[@id='CWField_fileid']");
        readonly By Fileid_FileLink = By.XPath("//*[@id='CWField_fileid_FileLink']");
        readonly By Fileid_Upload = By.XPath("//*[@id='CWField_fileid_Upload']");
        readonly By Fileid_UploadButton = By.XPath("//*[@id='CWField_fileid_UploadButton']");
        readonly By Iscloned_1 = By.XPath("//*[@id='CWField_iscloned_1']");
        readonly By Iscloned_0 = By.XPath("//*[@id='CWField_iscloned_0']");
        readonly By ClonedfromidLink = By.XPath("//*[@id='CWField_clonedfromid_Link']");
        readonly By ClonedfromidLookupButton = By.XPath("//*[@id='CWLookupBtn_clonedfromid']");


        public CaseAttachmentRecordPage WaitForCaseAttachmentRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementVisible(pageHeader);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementToBeClickable(BackButton);
            WaitForElementToBeClickable(SaveButton);
            WaitForElementToBeClickable(SaveAndCloseButton);

            return this;
        }

        public CaseAttachmentRecordPage WaitForCaseAttachmentRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementVisible(pageHeader);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementToBeClickable(BackButton);
            WaitForElementToBeClickable(SaveButton);
            WaitForElementToBeClickable(SaveAndCloseButton);

            return this;
        }


        public CaseAttachmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickShareRecordButton()
        {
            WaitForElementToBeClickable(ShareRecordButton);
            Click(ShareRecordButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(CloneButton);
            Click(CloneButton);

            return this;
        }


        public CaseAttachmentRecordPage ValidateErrorMessageVisible(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(errorMessage);
            else
                WaitForElementNotVisible(errorMessage, 7);

            return this;
        }

        public CaseAttachmentRecordPage ValidateErrorMessageText(string ExpectedText)
        {
            ValidateElementText(errorMessage, ExpectedText);

            return this;
        }



        public CaseAttachmentRecordPage ClickCaseLink()
        {
            WaitForElementToBeClickable(CaseidLink);
            Click(CaseidLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateCaseLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CaseidLink);
            ValidateElementText(CaseidLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickCaseLookupButton()
        {
            WaitForElementToBeClickable(CaseidLookupButton);
            Click(CaseidLookupButton);

            return this;
        }

        public CaseAttachmentRecordPage ValidateTitleText(string ExpectedText)
        {
            ValidateElementValue(Title, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage InsertTextOnTitle(string TextToInsert)
        {
            WaitForElementToBeClickable(Title);
            SendKeys(Title, TextToInsert);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDateText(string ExpectedText)
        {
            ValidateElementValue(Date, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage InsertTextOnDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Date);
            SendKeys(Date, TextToInsert + Keys.Tab);

            return this;
        }

        public CaseAttachmentRecordPage ClickDateDatePicker()
        {
            WaitForElementToBeClickable(DateDatePicker);
            Click(DateDatePicker);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDate_TimeFieldText(string ExpectedText)
        {
            ValidateElementValue(Date_Time, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage InsertTextOnDate_TimeField(string TextToInsert)
        {
            WaitForElementToBeClickable(Date_Time);
            SendKeys(Date_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public CaseAttachmentRecordPage ClickDate_TimeField_TimePicker()
        {
            WaitForElementToBeClickable(Date_Time_TimePicker);
            Click(Date_Time_TimePicker);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentTypeLink()
        {
            WaitForElementToBeClickable(DocumenttypeidLink);
            Click(DocumenttypeidLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DocumenttypeidLink);
            ValidateElementText(DocumenttypeidLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentTypeClearButton()
        {
            WaitForElementToBeClickable(DocumenttypeidClearButton);
            Click(DocumenttypeidClearButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentTypeLookupButton()
        {
            WaitForElementToBeClickable(DocumenttypeidLookupButton);
            Click(DocumenttypeidLookupButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentSubTypeLink()
        {
            WaitForElementToBeClickable(DocumentsubtypeidLink);
            Click(DocumentsubtypeidLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentSubTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DocumentsubtypeidLink);
            ValidateElementText(DocumentsubtypeidLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentSubTypeClearButton()
        {
            WaitForElementToBeClickable(DocumentsubtypeidClearButton);
            Click(DocumentsubtypeidClearButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickDocumentSubTypeLookupButton()
        {
            WaitForElementToBeClickable(DocumentsubtypeidLookupButton);
            Click(DocumentsubtypeidLookupButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickFile_FileLink()
        {
            WaitForElementToBeClickable(Fileid_FileLink);
            Click(Fileid_FileLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateFile_FileLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Fileid_FileLink);
            ValidateElementText(Fileid_FileLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickUploadNewFileButton()
        {
            WaitForElementToBeClickable(Fileid_Upload);
            Click(Fileid_Upload);

            return this;
        }

        public CaseAttachmentRecordPage ClickUploadButton()
        {
            WaitForElementToBeClickable(Fileid_UploadButton);
            Click(Fileid_UploadButton);

            return this;
        }

        public CaseAttachmentRecordPage ClickIscloned_YesRadioButton()
        {
            WaitForElementToBeClickable(Iscloned_1);
            Click(Iscloned_1);

            return this;
        }

        public CaseAttachmentRecordPage ValidateIscloned_YesRadioButtonChecked()
        {
            WaitForElement(Iscloned_1);
            ValidateElementChecked(Iscloned_1);

            return this;
        }

        public CaseAttachmentRecordPage ValidateIscloned_YesRadioButtonNotChecked()
        {
            WaitForElement(Iscloned_1);
            ValidateElementNotChecked(Iscloned_1);

            return this;
        }

        public CaseAttachmentRecordPage ClickIscloned_NoRadioButton()
        {
            WaitForElementToBeClickable(Iscloned_0);
            Click(Iscloned_0);

            return this;
        }

        public CaseAttachmentRecordPage ValidateIscloned_NoRadioButtonChecked()
        {
            WaitForElement(Iscloned_0);
            ValidateElementChecked(Iscloned_0);

            return this;
        }

        public CaseAttachmentRecordPage ValidateIscloned_NoRadioButtonNotChecked()
        {
            WaitForElement(Iscloned_0);
            ValidateElementNotChecked(Iscloned_0);

            return this;
        }

        public CaseAttachmentRecordPage ClickClonedFromLink()
        {
            WaitForElementToBeClickable(ClonedfromidLink);
            Click(ClonedfromidLink);

            return this;
        }

        public CaseAttachmentRecordPage ValidateClonedFromLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ClonedfromidLink);
            ValidateElementText(ClonedfromidLink, ExpectedText);

            return this;
        }

        public CaseAttachmentRecordPage ClickClonedFromLookupButton()
        {
            WaitForElementToBeClickable(ClonedfromidLookupButton);
            Click(ClonedfromidLookupButton);

            return this;
        }

        public CaseAttachmentRecordPage UploadFile(string FilePath)
        {
            SendKeys(file_Field, FilePath);

            return this;
        }

    }
}

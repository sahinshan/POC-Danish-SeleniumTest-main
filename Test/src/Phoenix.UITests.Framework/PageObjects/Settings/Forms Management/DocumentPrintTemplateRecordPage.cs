using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentPrintTemplateRecordPage : CommonMethods
    {
        public DocumentPrintTemplateRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentprinttemplate&')]"); 


        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By previewButton = By.Id("TI_PreviewInWordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        #endregion

        #region Navigation Area

        readonly By menuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");        

        #region Left Sub Menu

        readonly By auditLeftSubMenu = By.Id("CWNavItem_AuditHistory");

        #endregion

        #endregion

        #region Field Labels

        readonly By documentFieldLabel = By.XPath("//*[@id='CWLabelHolder_documentid']/label");
        readonly By nameFieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By fileFieldLabel = By.XPath("//*[@id='CWLabelHolder_fileid']/label");
        readonly By ValidForPrintHistoryOnCloseLabel = By.XPath("//*[@id='CWLabelHolder_validforsnapshot']/label");
        readonly By languageLabel = By.XPath("//*[@id='CWLabelHolder_languageid']/label");
        readonly By protectDocumentLabel = By.XPath("//*[@id='CWLabelHolder_protectdocument']/label");
        readonly By allowBlankOutputLabel = By.XPath("//*[@id='CWLabelHolder_allowblankoutput']/label");
        readonly By defaultForApplicationsLabel = By.XPath("//*[@id='CWLabelHolder_defaultforapplications']/label");

        #endregion

        #region Fields

        readonly By documentFieldLink = By.XPath("//*[@id='CWField_documentid_Link']");
        readonly By documentFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_documentid']");

        readonly By nameField = By.XPath("//*[@id='CWField_name']");
        readonly By fileField = By.XPath("//*[@id='CWField_fileid']");
        readonly By fileLinkField = By.XPath("//*[@id='CWField_fileid_FileLink']");
        readonly By ValidForPrintHistoryOnCloseYesRadioButton = By.XPath("//*[@id='CWField_validforsnapshot_1']");
        readonly By ValidForPrintHistoryOnCloseNoRadioButton = By.XPath("//*[@id='CWField_validforsnapshot_0']");

        readonly By languageFieldLink = By.XPath("//*[@id='CWField_languageid_Link']");
        readonly By languageFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_languageid']");

        readonly By protectDocumentYesRadioButton = By.XPath("//*[@id='CWField_protectdocument_1']");
        readonly By protectDocumentNoRadioButton = By.XPath("//*[@id='CWField_protectdocument_0']");

        readonly By allowBlankOutputYesRadioButton = By.XPath("//*[@id='CWField_allowblankoutput_1']");
        readonly By allowBlankOutputNoRadioButton = By.XPath("//*[@id='CWField_allowblankoutput_0']");

        By defaultForApplicationsAddedRecord(string recordId) => By.XPath("//*[@id='MS_defaultforapplications_" + recordId + "']");
        By defaultForApplicationsAddedRecordRemoveButton(string recordId) => By.XPath("//*[@id='MS_defaultforapplications_" + recordId + "']/a");
        readonly By defaultForApplicationsLookupButton = By.XPath("//*[@id='CWLookupBtn_defaultforapplications']");

        #endregion


        public DocumentPrintTemplateRecordPage WaitForDocumentPrintTemplateRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            
            WaitForElement(documentFieldLabel);
            WaitForElement(nameFieldLabel);
            WaitForElement(fileFieldLabel);
            WaitForElement(ValidForPrintHistoryOnCloseLabel);
            WaitForElement(languageLabel);
            WaitForElement(protectDocumentLabel);
            WaitForElement(allowBlankOutputLabel);
            WaitForElement(defaultForApplicationsLabel);

            return this;

        }
        public DocumentPrintTemplateRecordPage WaitForDocumentPrintTemplateRecordPageToReload()
        {
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(previewButton);
            WaitForElement(detailsTab);
            WaitForElement(menuButton);

            WaitForElement(documentFieldLabel);
            WaitForElement(nameFieldLabel);
            WaitForElement(fileFieldLabel);
            WaitForElement(ValidForPrintHistoryOnCloseLabel);
            WaitForElement(languageLabel);
            WaitForElement(protectDocumentLabel);
            WaitForElement(allowBlankOutputLabel);
            WaitForElement(defaultForApplicationsLabel);

            return this;

        }



        public DocumentPrintTemplateRecordPage ClickPreviewButton()
        {
            Click(previewButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickMenuButton()
        {
            WaitForElement(menuButton);
            Click(menuButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }







        public DocumentPrintTemplateRecordPage ValidateDocumentFieldLinkText(string ExpectedText)
        {
            ValidateElementText(documentFieldLink, ExpectedText);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateNameFieldValue(string ExpectedText)
        {
            ValidateElementValue(nameField, ExpectedText);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateFileLinkFieldText(string ExpectedText)
        {
            ValidateElementText(fileLinkField, ExpectedText);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateValidForPrintHistoryOnCloseYesRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(ValidForPrintHistoryOnCloseYesRadioButton);
            else
                ValidateElementNotChecked(ValidForPrintHistoryOnCloseYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateValidForPrintHistoryOnCloseNoRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(ValidForPrintHistoryOnCloseNoRadioButton);
            else
                ValidateElementNotChecked(ValidForPrintHistoryOnCloseNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateLanguageLinkFieldText(string ExpectedText)
        {
            ValidateElementText(languageFieldLink, ExpectedText);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateProtectDocumentYesRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(protectDocumentYesRadioButton);
            else
                ValidateElementNotChecked(protectDocumentYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateProtectDocumentNoRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(protectDocumentNoRadioButton);
            else
                ValidateElementNotChecked(protectDocumentNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateAllowBlankOutputYesRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(allowBlankOutputYesRadioButton);
            else
                ValidateElementNotChecked(allowBlankOutputYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateAllowBlankOutputNoRadioButtonChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(allowBlankOutputNoRadioButton);
            else
                ValidateElementNotChecked(allowBlankOutputNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ValidateDefaultForApplicationsAddedRecordText(string RecordID, string ExpectedText)
        {
            ValidateElementText(defaultForApplicationsAddedRecord(RecordID), ExpectedText);

            return this;
        }


        public DocumentPrintTemplateRecordPage ClickDocumentLookupButton()
        {
            Click(documentFieldLookupButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage InsertName(string TextToInsert)
        {
            SendKeys(nameField, TextToInsert);

            return this;
        }
        public DocumentPrintTemplateRecordPage SelectFile(string FilePath)
        {
            SendKeys(fileField, FilePath);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickValidForPrintHistoryOnCloseYesRadioButton()
        {
            Click(ValidForPrintHistoryOnCloseYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickValidForPrintHistoryOnCloseNoRadioButton()
        {
            Click(ValidForPrintHistoryOnCloseNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickLanguageLookupButton()
        {
            Click(languageFieldLookupButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickProtectDocumentYesRadioButton()
        {
            Click(protectDocumentYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickProtectDocumentNoRadioButton()
        {
            Click(protectDocumentNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickAllowBlankOutputYesRadioButton()
        {
            Click(allowBlankOutputYesRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickAllowBlankOutputNoRadioButton()
        {
            Click(allowBlankOutputNoRadioButton);

            return this;
        }
        public DocumentPrintTemplateRecordPage ClickDefaultForApplicationsLookupButton()
        {
            Click(defaultForApplicationsLookupButton);

            return this;
        }


        public DocumentPrintTemplateRecordPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }

    }
}

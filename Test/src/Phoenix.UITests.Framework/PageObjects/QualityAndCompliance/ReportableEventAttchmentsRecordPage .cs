using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventAttchmentsRecordPage   : CommonMethods
    {
        public ReportableEventAttchmentsRecordPage  (IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventattachment&')]");

        readonly By Title_field = By.Id("CWField_title");
        readonly By Title_FieldErrorLable = By.XPath("//*[@id='CWControlHolder_title']/label/span");

        readonly By Date_DateField = By.XPath("//input[@id='CWField_date']");
        readonly By Date_TimeField = By.XPath("//input[@id='CWField_date_Time']");
        readonly By Date_DateFieldErrorLable = By.XPath("//*[@id='CWControlHolder_date']//label[@for='CWField_date']/span");
        readonly By Date_TimeFieldErrorLable = By.XPath("//*[@id='CWControlHolder_date']//label[@for='CWField_date_Time']/span");
        readonly By responsibleTeam_Field = By.Id("CWField_ownerid");

        readonly By Documenttype_LinkField = By.Id("CWField_documenttypeid_Link");
        readonly By Documenttype_LookUP = By.Id("CWLookupBtn_documenttypeid");
        readonly By Documenttype_FieldErrorLable = By.XPath("//*[@id='CWControlHolder_documenttypeid']/label/span");

        readonly By Documentsubtype_LinkField = By.Id("CWField_documentsubtypeid_Link");
        readonly By Documentsubtype_LookUP = By.Id("CWLookupBtn_documentsubtypeid");
        readonly By Documentsubtype_FieldErrorLable = By.XPath("//*[@id='CWControlHolder_documentsubtypeid']/label/span");

        readonly By _file_UploadFileButton = By.XPath("//input[@id='CWField_fileid']");
        readonly By _file_Link = By.XPath("//a[@id='CWField_fileid_FileLink']");
        readonly By file_FieldErrorLable = By.XPath("//*[@id='CWField_fileid_FileSection']/label/span");

        readonly By SaveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");


        public ReportableEventAttchmentsRecordPage WaitForReportableEventAttchmentsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);

            WaitForElement(Title_field);
            WaitForElement(Date_DateField);

            WaitForElement(Documenttype_LookUP);
            WaitForElement(Documentsubtype_LookUP);

            return this;
        }


        public ReportableEventAttchmentsRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public ReportableEventAttchmentsRecordPage InsertTitle(String titlename)
        {
            WaitForElement(Title_field);
            SendKeys(Title_field, titlename);

            return this;
        }
        public ReportableEventAttchmentsRecordPage InsertDate(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(Date_DateField);

            SendKeys(Date_DateField, DateToInsert);
            SendKeysWithoutClearing(Date_DateField, Keys.Tab);
            SendKeys(Date_TimeField, TimeToInsert);
            return this;

        }
        public ReportableEventAttchmentsRecordPage ClickDocumentType_LookUp()
        {
            WaitForElement(Documenttype_LookUP);
            Click(Documenttype_LookUP);

            return this;
        }

        public ReportableEventAttchmentsRecordPage ClickSubDocumentType_LookUp()
        {
            WaitForElement(Documentsubtype_LookUP);
            Click(Documentsubtype_LookUP);

            return this;
        }
        public ReportableEventAttchmentsRecordPage FileUpload(string filepath)
        {
            WaitForElement(_file_UploadFileButton);
            SendKeys(_file_UploadFileButton, filepath);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndClose_Button);
            Click(SaveAndClose_Button);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ClickAdditionalItemsMenuButton()
        {
            this.Click(additionalItemsMenuButton);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateAttachmentRecordEditAble()
        {
            WaitForElement(Documenttype_LookUP);
            ValidateElementEnabled(Documenttype_LookUP);
            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateTitleFieldErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(Title_FieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDateFieldErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(Date_DateFieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateTimeFieldErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(Date_TimeFieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDocumentTypeErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(Documenttype_FieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDocumentSubTypeErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(Documentsubtype_FieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateFileErrormessage(string ExpectedTimeText)
        {
            ValidateElementText(file_FieldErrorLable, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateTitle(string ExpectedText)
        {
            ValidateElementValue(Title_field, ExpectedText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDate(string ExpectedDateText, string ExpectedTimeText)
        {
            ValidateElementValue(Date_DateField, ExpectedDateText);
            ValidateElementValue(Date_TimeField, ExpectedTimeText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDocumentTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Documenttype_LinkField, ExpectedText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDocumentSubTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Documentsubtype_LinkField, ExpectedText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateFileLinkText(string ExpectedText)
        {
            ValidateElementTextContainsText(_file_Link, ExpectedText);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateAllFieldsDisableMode()
        {
            WaitForElement(Title_field);
            ValidateElementDisabled(Title_field);

            WaitForElement(Date_DateField);
            ValidateElementDisabled(Date_DateField);

            WaitForElement(Documenttype_LookUP);
            ValidateElementDisabled(Documenttype_LookUP);

            WaitForElement(Documentsubtype_LookUP);
            ValidateElementDisabled(Documentsubtype_LookUP);

            WaitForElement(responsibleTeam_Field);
            ValidateElementDisabled(responsibleTeam_Field);

            return this;
        }
        public ReportableEventAttchmentsRecordPage ValidateDeleteButtonNotDisplay()
        {
            ValidateElementDoNotExist(DeleteRecordButton);

            return this;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class PersonFinancialDetailAttachmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personfinancialdetailattachment_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personfinancialdetailattachment_SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personfinancialdetailattachment_DeleteRecordButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personfinancialdetailattachment_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personfinancialdetailattachment_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string pageTitle) => e => e.Marked("MainStackLayout").Descendant().Marked(pageTitle);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Title_FieldTitle = e => e.Marked("Title");
        readonly Func<AppQuery, AppQuery> _DocumentType_FieldTitle = e => e.Marked("Document Type");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_FieldTitle = e => e.Marked("Document Sub Type");
        readonly Func<AppQuery, AppQuery> _Date_FieldTitle = e => e.Marked("Date");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _File_FieldTitle = e => e.Marked("File");
        

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Title_Field = e => e.Marked("Field_346fe236fc11eb11a2ce0050569231cf");

        readonly Func<AppQuery, AppQuery> _DocumentType_Field = e => e.Marked("Field_3fb9a10fff11eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _DocumentType_LookupEntry = e => e.Marked("3fb9a10fff11eb11a2ce0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _DocumentType_RemoveButton = e => e.Marked("3fb9a10fff11eb11a2ce0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _DocumentType_LookupButton = e => e.Marked("3fb9a10fff11eb11a2ce0050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _DocumentSubType_Field = e => e.Marked("Field_43b9a10fff11eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_LookupEntry = e => e.Marked("43b9a10fff11eb11a2ce0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_RemoveButton = e => e.Marked("43b9a10fff11eb11a2ce0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_LookupButton = e => e.Marked("43b9a10fff11eb11a2ce0050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Date_DateField = e => e.Marked("Field_cd07671eff11eb11a2ce0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _Date_TimeField = e => e.Marked("Field_cd07671eff11eb11a2ce0050569231cf_Time");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_d107671eff11eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupEntry = e => e.Marked("d107671eff11eb11a2ce0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveBUtton = e => e.Marked("d107671eff11eb11a2ce0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("d107671eff11eb11a2ce0050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _TakePicture_FileButton = e => e.Marked("imagePhoto");
        readonly Func<AppQuery, AppQuery> _Gallery_FileButton = e => e.Marked("imagePicture");
        readonly Func<AppQuery, AppQuery> _Download_FileButton = e => e.Marked("imageDownload");
        readonly Func<AppQuery, AppQuery> _Delete_FileButton = e => e.Marked("imageDelete");
        readonly Func<AppQuery, AppQuery> _AttachmentName_Field = e => e.Marked("AttachmentName");


        #endregion



        public PersonFinancialDetailAttachmentRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialDetailAttachmentRecordPage WaitForPersonFinancialDetailAttachmentRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_saveButton);
            WaitForElement(_saveAndCloseButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);

            WaitForElement(_Title_FieldTitle);
            WaitForElement(_Title_Field);

            return this;
        }



        public PersonFinancialDetailAttachmentRecordPage ValidateTitleFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Title_FieldTitle);
            ValidateElementVisibility(_Title_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDocumentTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_DocumentType_FieldTitle);
            ValidateElementVisibility(_DocumentType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDocumentSubTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_DocumentSubType_FieldTitle);
            ValidateElementVisibility(_DocumentSubType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Date_FieldTitle);
            ValidateElementVisibility(_Date_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ResponsibleTeam_FieldTitle);
            ValidateElementVisibility(_ResponsibleTeam_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateFileFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_File_FieldTitle);
            ValidateElementVisibility(_File_FieldTitle, ExpectElementVisible);
            return this;
        }



        public PersonFinancialDetailAttachmentRecordPage ValidateTakePictureFileButtonVisible(bool ExpectElementVisible)
        {
            ScrollToElement(_TakePicture_FileButton);
            ValidateElementVisibility(_TakePicture_FileButton, ExpectElementVisible);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateGalleryFileButtonVisible(bool ExpectElementVisible)
        {
            ScrollToElement(_Gallery_FileButton);
            ValidateElementVisibility(_Gallery_FileButton, ExpectElementVisible);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDownloadFileButtonVisible(bool ExpectElementVisible)
        {
            ScrollToElement(_Download_FileButton);
            ValidateElementVisibility(_Download_FileButton, ExpectElementVisible);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDeleteFileButtonVisible(bool ExpectElementVisible)
        {
            ScrollToElement(_Delete_FileButton);
            ValidateElementVisibility(_Delete_FileButton, ExpectElementVisible);

            return this;
        }




        public PersonFinancialDetailAttachmentRecordPage ValidateTitleFieldText(string ExpectedText)
        {
            ScrollToElement(_Title_Field);
            ValidateElementText(_Title_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDocumentTypeLookupEntryFieldText(string ExpectedText)
        {
            ScrollToElement(_DocumentType_LookupEntry);
            ValidateElementText(_DocumentType_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDocumentSybTypeLookupEntryFieldText(string ExpectedText)
        {
            ScrollToElement(_DocumentSubType_LookupEntry);
            ValidateElementText(_DocumentSubType_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateDateFieldText(string ExpectedDate, string ExpectedTime)
        {
            ScrollToElement(_Date_DateField);
            ValidateElementText(_Date_DateField, ExpectedDate);

            ScrollToElement(_Date_TimeField);
            ValidateElementText(_Date_TimeField, ExpectedTime);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateResponsibleTeamLookupEntrFieldText(string ExpectedText)
        {
            ScrollToElement(_ResponsibleTeam_LookupEntry);
            ValidateElementText(_ResponsibleTeam_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateResponsibleTeamFieldText(string ExpectedText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            ValidateElementText(_ResponsibleTeam_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage ValidateAttachmentNameFieldText(string ExpectedText)
        {
            ScrollToElement(_AttachmentName_Field);
            ValidateElementText(_AttachmentName_Field, ExpectedText);

            return this;
        }


        public PersonFinancialDetailAttachmentRecordPage InsertTitle(string TextToInsert)
        {
            ScrollToElement(_Title_Field);
            EnterText(_Title_Field, TextToInsert);

            return this;
        }

        public PersonFinancialDetailAttachmentRecordPage InsertDate(string Date, string Time)
        {
            ScrollToElement(_Title_Field);
            EnterText(_Date_DateField, Date);
            EnterText(_Date_TimeField, Time);

            return this;
        }


        public PersonFinancialDetailAttachmentRecordPage TapDocumentTypeLookupButton()
        {
            ScrollToElement(_DocumentType_LookupButton);
            Tap(_DocumentType_LookupButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapDocumentTypeRemoveButton()
        {
            ScrollToElement(_DocumentType_RemoveButton);
            Tap(_DocumentType_RemoveButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapDocumentSubTypeLookupButton()
        {
            ScrollToElement(_DocumentSubType_LookupButton);
            Tap(_DocumentSubType_LookupButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapDocumentSubTypeRemoveButton()
        {
            ScrollToElement(_DocumentSubType_RemoveButton);
            Tap(_DocumentSubType_RemoveButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapTakePictureFileButton()
        {
            ScrollToElement(_TakePicture_FileButton);
            Tap(_TakePicture_FileButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapGalleryFileButton()
        {
            ScrollToElement(_Gallery_FileButton);
            Tap(_Gallery_FileButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapDownloadFileButton()
        {
            ScrollToElement(_Download_FileButton);
            Tap(_Download_FileButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapDeleteFileButton()
        {
            ScrollToElement(_Delete_FileButton);
            Tap(_Delete_FileButton);

            return this;
        }






        
        

        public PersonFinancialDetailAttachmentRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public PersonFinancialDetailAttachmentRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }
        public PersonFinancialDetailAttachmentRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

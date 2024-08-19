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
    public class CaseAttachmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("caseattachment_SaveButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("caseattachment_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("caseattachment_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("caseattachment_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("caseattachment_SaveAndCloseButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Title_FieldTitle = e => e.Marked("Title");
        readonly Func<AppQuery, AppQuery> _DocumentType_FieldTitle = e => e.Marked("Document Type");
        readonly Func<AppQuery, AppQuery> _documentSubType_FieldTitle = e => e.Marked("Document Sub Type");
        readonly Func<AppQuery, AppQuery> _Date_FieldTitle = e => e.Marked("Date");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _file_FieldTitle = e => e.Marked("File");
        
        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Title_Field = e => e.Marked("Field_8cb4213512b8e8119979461ca8e2ff4b");

        readonly Func<AppQuery, AppQuery> _DocumentType_LookupField = e => e.Marked("97a233056c1ae91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _DocumentType_RemoveButton = e => e.Marked("97a233056c1ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _DocumentType_LookupButton = e => e.Marked("97a233056c1ae91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _DocumentSubType_LookupField = e => e.Marked("ee8a87126c1ae91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_RemoveButton = e => e.Marked("ee8a87126c1ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _DocumentSubType_LookupButton = e => e.Marked("ee8a87126c1ae91180dc0050560502cc_OpenLookup");


        readonly Func<AppQuery, AppQuery> _Date_DateField = e => e.Marked("Field_22e563226c1ae91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _Date_DatePicker = e => e.Marked("Field_22e563226c1ae91180dc0050560502cc_OpenPicker").Index(0);
        readonly Func<AppQuery, AppQuery> _Date_TimeField = e => e.Marked("Field_22e563226c1ae91180dc0050560502cc_Time");
        readonly Func<AppQuery, AppQuery> _Date_TimePicker = e => e.Marked("Field_22e563226c1ae91180dc0050560502cc_OpenPicker").Index(1);

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_ReadonlyField = e => e.Marked("Field_3578d01ba886e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupField = e => e.Marked("3578d01ba886e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveButton = e => e.Marked("3578d01ba886e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("3578d01ba886e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _file_TakePictureButton = e => e.Marked("imagePhoto");
        readonly Func<AppQuery, AppQuery> _file_UploadFileButton = e => e.Marked("imagePicture");
        readonly Func<AppQuery, AppQuery> _file_DownloadFileButton = e => e.Marked("imageDownload");
        readonly Func<AppQuery, AppQuery> _file_DeleteFileButton = e => e.Marked("imageDelete");
        readonly Func<AppQuery, AppQuery> _file_AttachmentName = e => e.Marked("AttachmentName");
        readonly Func<AppQuery, AppQuery> _file_AttachmentImage = e => e.Marked("entityImage");


        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public CaseAttachmentRecordPage(IApp app)
        {
            _app = app;
        }


        public CaseAttachmentRecordPage WaitForCaseAttachmentRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }



        public CaseAttachmentRecordPage ValidateTitleFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Title_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Title_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Title_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Title_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DocumentType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DocumentType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DocumentType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DocumentType_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentSubTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_documentSubType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_documentSubType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_documentSubType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_documentSubType_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Date_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Date_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Date_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Date_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateFileFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_file_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_file_FieldTitle));
            }
            else
            {
                TryScrollToElement(_file_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_file_FieldTitle));
            }

            return this;
        }




        public CaseAttachmentRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdBy_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdOn_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }

            return this;
        }






        public CaseAttachmentRecordPage ValidateTitleFieldText(string ExpectText)
        {
            ScrollToElement(_Title_Field);
            string fieldText = GetElementText(_Title_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentTypeFieldText(string ExpectText)
        {
            ScrollToElement(_DocumentType_LookupField);
            string fieldText = GetElementText(_DocumentType_LookupField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentSubTypeFieldText(string ExpectText)
        {
            ScrollToElement(_DocumentSubType_LookupField);
            string fieldText = GetElementText(_DocumentSubType_LookupField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateDateFieldText(string ExpectDateText, string ExpectTimeText)
        {
            ScrollToElement(_Date_DateField);

            string fieldText = GetElementText(_Date_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            fieldText = GetElementText(_Date_TimeField);
            Assert.AreEqual(ExpectTimeText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_LookupField);
            string fieldText = GetElementText(_ResponsibleTeam_LookupField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamReadonlyFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_ReadonlyField);
            string fieldText = GetElementText(_ResponsibleTeam_ReadonlyField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateAttachmentFileName(string ExpectText)
        {
            ScrollToElement(_file_AttachmentName);
            string fieldText = GetElementText(_file_AttachmentName);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        public CaseAttachmentRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseAttachmentRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public CaseAttachmentRecordPage InsertTitle(string ValueToInsert)
        {
            ScrollToElement(_Title_Field);
            this.EnterText(_Title_Field, ValueToInsert);

            return this;
        }

        public CaseAttachmentRecordPage TapDocumenTypeRemoveButton()
        {
            ScrollToElement(_DocumentType_RemoveButton);
            Tap(_DocumentType_RemoveButton);

            return this;
        }

        public CaseAttachmentRecordPage TapDocumentTypeLookupButton()
        {
            ScrollToElement(_DocumentType_LookupButton);
            Tap(_DocumentType_LookupButton);

            return this;
        }

        public CaseAttachmentRecordPage TapDocumentSubTypeRemoveButton()
        {
            ScrollToElement(_DocumentSubType_RemoveButton);
            Tap(_DocumentSubType_RemoveButton);

            return this;
        }

        public CaseAttachmentRecordPage TapDocumentSubTypeLookupButton()
        {
            ScrollToElement(_DocumentSubType_LookupButton);
            Tap(_DocumentSubType_LookupButton);
            
            return this;
        }

        public CaseAttachmentRecordPage InserDate(string DateValue, string TimeValue)
        {
            ScrollToElement(_Date_DateField);
            this.EnterText(_Date_DateField, DateValue);
            this.EnterText(_Date_TimeField, TimeValue);

            return this;
        }

        public CaseAttachmentRecordPage TapResponsibleTeamRemoveButton()
        {
            ScrollToElement(_ResponsibleTeam_RemoveButton);
            Tap(_ResponsibleTeam_RemoveButton);

            return this;
        }

        public CaseAttachmentRecordPage TapResponsibleTeamLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_LookupButton);
            Tap(_ResponsibleTeam_LookupButton);

            return this;
        }





        public CaseAttachmentRecordPage ValidateDocumentTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_DocumentType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_DocumentType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_DocumentType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_DocumentType_RemoveButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_DocumentType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_DocumentType_LookupButton));
            }
            else
            {
                TryScrollToElement(_DocumentType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_DocumentType_LookupButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentSubTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_DocumentSubType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_DocumentSubType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_DocumentSubType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_DocumentSubType_RemoveButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDocumentSubTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_DocumentSubType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_DocumentSubType_LookupButton));
            }
            else
            {
                TryScrollToElement(_DocumentSubType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_DocumentSubType_LookupButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamLRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ResponsibleTeam_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_RemoveButton));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_RemoveButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateResponsibleTeamLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ResponsibleTeam_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_LookupButton));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_LookupButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateTakePictureButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_file_TakePictureButton);
                Assert.IsTrue(CheckIfElementVisible(_file_TakePictureButton));
            }
            else
            {
                TryScrollToElement(_file_TakePictureButton);
                Assert.IsFalse(CheckIfElementVisible(_file_TakePictureButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateUploadFileButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_file_UploadFileButton);
                Assert.IsTrue(CheckIfElementVisible(_file_UploadFileButton));
            }
            else
            {
                TryScrollToElement(_file_UploadFileButton);
                Assert.IsFalse(CheckIfElementVisible(_file_UploadFileButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDownloadFileButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_file_DownloadFileButton);
                Assert.IsTrue(CheckIfElementVisible(_file_DownloadFileButton));
            }
            else
            {
                TryScrollToElement(_file_DownloadFileButton);
                Assert.IsFalse(CheckIfElementVisible(_file_DownloadFileButton));
            }

            return this;
        }

        public CaseAttachmentRecordPage ValidateDeleteAttachmentFileButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_file_DeleteFileButton);
                Assert.IsTrue(CheckIfElementVisible(_file_DeleteFileButton));
            }
            else
            {
                TryScrollToElement(_file_DeleteFileButton);
                Assert.IsFalse(CheckIfElementVisible(_file_DeleteFileButton));
            }

            return this;
        }







        public CaseAttachmentRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public CaseAttachmentRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public CaseAttachmentRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public CaseAttachmentRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public CaseAttachmentRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public CaseAttachmentRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

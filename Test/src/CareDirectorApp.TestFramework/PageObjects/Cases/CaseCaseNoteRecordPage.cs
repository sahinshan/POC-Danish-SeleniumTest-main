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
    public class CaseCaseNoteRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("casecasenote_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("casecasenote_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("casecasenote_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("casecasenote_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("casecasenote_SaveAndCloseButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _generalSection_FieldTitle = e => e.Marked("General");

        readonly Func<AppQuery, AppQuery> _subject_FieldTitle = e => e.Marked("Subject");
        readonly Func<AppQuery, AppQuery> _description_FieldTitle = e => e.Marked("Description");



        readonly Func<AppQuery, AppQuery> _detailsSection_FieldTitle = e => e.Marked("Details");

        readonly Func<AppQuery, AppQuery> _reason_FieldTitle = e => e.Marked("Reason");
        readonly Func<AppQuery, AppQuery> _priority_FieldTitle = e => e.Marked("Priority");
        readonly Func<AppQuery, AppQuery> _Date_FieldTitle = e => e.Marked("Date");
        readonly Func<AppQuery, AppQuery> _Outcome_FieldTitle = e => e.Marked("Outcome");
        readonly Func<AppQuery, AppQuery> _Status_FieldTitle = e => e.Marked("Status");
        
        readonly Func<AppQuery, AppQuery> _Category_FieldTitle = e => e.Marked("Category");
        readonly Func<AppQuery, AppQuery> _SubCategory_FieldTitle = e => e.Marked("Sub-Category");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _subject_Field = e => e.Marked("Field_a31c53f33010e91180dc0050560502cc");
        
        readonly Func<AppQuery, AppQuery> _descriptionWarningMessage_Field = e => e.Marked("The following text was created using a rich text editor and cannot be edited on this device. Click the expand icon to view text in a larger window.");
        Func<AppQuery, AppWebQuery> _descriptionRichTextEditorText_Field(string ExpectedText) => e => e.XPath("//*[text()='" + ExpectedText + "']");
        readonly Func<AppQuery, AppQuery> _description_Field = e => e.Marked("Field_a3da7941101be91180dc0050560502cc");


        readonly Func<AppQuery, AppQuery> _reason_Field = e => e.Marked("113c3b6f538ce911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _reason_RemoveButton = e => e.Marked("113c3b6f538ce911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _reason_LookupButton = e => e.Marked("113c3b6f538ce911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _priority_Field = e => e.Marked("63ba5673101be91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _priority_RemoveButton = e => e.Marked("63ba5673101be91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _priority_LookupButton = e => e.Marked("63ba5673101be91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Date_ReadOnlyField = e => e.Marked("Field_60098f5c101be91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Date_DateField = e => e.Marked("Field_60098f5c101be91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _Date_DatePicker = e => e.Marked("Field_60098f5c101be91180dc0050560502cc_OpenPicker").Index(0);
        readonly Func<AppQuery, AppQuery> _Date_TimeField = e => e.Marked("Field_60098f5c101be91180dc0050560502cc_Time");
        readonly Func<AppQuery, AppQuery> _Date_TimePicker = e => e.Marked("Field_60098f5c101be91180dc0050560502cc_OpenPicker").Index(1);

        readonly Func<AppQuery, AppQuery> _Outcome_Field = e => e.Marked("6dba5673101be91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Outcome_RemoveButton = e => e.Marked("6dba5673101be91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupButton = e => e.Marked("6dba5673101be91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Status_Field = e => e.All().Marked("Field_e2356979101be91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _Category_Field = e => e.Marked("027a2065101be91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Category_RemoveButton = e => e.Marked("027a2065101be91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Category_LookupButton = e => e.Marked("027a2065101be91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _SubCategory_Field = e => e.Marked("57496b6c101be91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _SubCategory_RemoveButton = e => e.Marked("57496b6c101be91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _SubCategory_LookupButton = e => e.Marked("57496b6c101be91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_dff24056101be91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("f11d724f101be91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_RemoveButton = e => e.Marked("f11d724f101be91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_LookupButton = e => e.Marked("f11d724f101be91180dc0050560502cc_OpenLookup");


        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public CaseCaseNoteRecordPage(IApp app)
        {
            _app = app;
        }


        public CaseCaseNoteRecordPage WaitForCaseCaseNoteRecordPageToLoad(string PageTitleText)
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



        public CaseCaseNoteRecordPage ValidateGeneralSectionTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_generalSection_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_generalSection_FieldTitle));
            }
            else
            {
                TryScrollToElement(_generalSection_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_generalSection_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubjectFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_subject_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_subject_FieldTitle));
            }
            else
            {
                TryScrollToElement(_subject_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_subject_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_description_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_description_FieldTitle));
            }
            else
            {
                TryScrollToElement(_description_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_description_FieldTitle));
            }

            return this;
        }



        public CaseCaseNoteRecordPage ValidateDetailsSectionTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_detailsSection_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_detailsSection_FieldTitle));
            }
            else
            {
                TryScrollToElement(_detailsSection_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_detailsSection_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_reason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_reason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_reason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_reason_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidatePriorityFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_priority_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_priority_FieldTitle));
            }
            else
            {
                TryScrollToElement(_priority_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_priority_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseCaseNoteRecordPage ValidateOutcomeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Outcome_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Outcome_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateStatusFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Status_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Status_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Status_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Status_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCategoryFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Category_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Category_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Category_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Category_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubCategoryFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SubCategory_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_SubCategory_FieldTitle));
            }
            else
            {
                TryScrollToElement(_SubCategory_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_FieldTitle));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseCaseNoteRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleUser_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleUser_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ResponsibleUser_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_FieldTitle));
            }

            return this;
        }








        public CaseCaseNoteRecordPage ValidateSubjectFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_subject_Field);
                Assert.IsTrue(CheckIfElementVisible(_subject_Field));
            }
            else
            {
                TryScrollToElement(_subject_Field);
                Assert.IsFalse(CheckIfElementVisible(_subject_Field));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDescriptionFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_description_Field);
                Assert.IsTrue(CheckIfElementVisible(_description_Field));
            }
            else
            {
                TryScrollToElement(_description_Field);
                Assert.IsFalse(CheckIfElementVisible(_description_Field));
            }

            return this;
        }


        public CaseCaseNoteRecordPage ValidateReasonFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_reason_Field);
                Assert.IsTrue(CheckIfElementVisible(_reason_Field));
                Assert.IsTrue(CheckIfElementVisible(_reason_LookupButton));
            }
            else
            {
                TryScrollToElement(_reason_Field);
                Assert.IsFalse(CheckIfElementVisible(_reason_Field));
                Assert.IsFalse(CheckIfElementVisible(_reason_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_reason_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidatePriorityFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_priority_Field);
                Assert.IsTrue(CheckIfElementVisible(_priority_Field));
            }
            else
            {
                TryScrollToElement(_priority_Field);
                Assert.IsFalse(CheckIfElementVisible(_priority_Field));
                Assert.IsFalse(CheckIfElementVisible(_priority_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_priority_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDateFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Date_DateField);
                Assert.IsTrue(CheckIfElementVisible(_Date_DateField));
                Assert.IsTrue(CheckIfElementVisible(_Date_TimeField));
            }
            else
            {
                TryScrollToElement(_Date_DateField);
                Assert.IsFalse(CheckIfElementVisible(_Date_DateField));
                Assert.IsFalse(CheckIfElementVisible(_Date_TimeField));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateOutcomeFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Outcome_Field);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_Field));
            }
            else
            {
                TryScrollToElement(_Outcome_Field);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_Field));
                Assert.IsFalse(CheckIfElementVisible(_Outcome_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_Outcome_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateStatusFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Status_Field);
                Assert.IsTrue(CheckIfElementVisible(_Status_Field));
            }
            else
            {
                TryScrollToElement(_Status_Field);
                Assert.IsFalse(CheckIfElementVisible(_Status_Field));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCategoryFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Category_Field);
                Assert.IsTrue(CheckIfElementVisible(_Category_Field));
            }
            else
            {
                TryScrollToElement(_Category_Field);
                Assert.IsFalse(CheckIfElementVisible(_Category_Field));
                Assert.IsFalse(CheckIfElementVisible(_Category_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_Category_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubCategoryFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SubCategory_Field);
                Assert.IsTrue(CheckIfElementVisible(_SubCategory_Field));
            }
            else
            {
                TryScrollToElement(_SubCategory_Field);
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_Field));
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleTeamFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleTeam_Field);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_Field));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_Field);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_Field));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleUserFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleUser_Field);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleUser_Field));
            }
            else
            {
                TryScrollToElement(_ResponsibleUser_Field);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_Field));
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_RemoveButton));
            }

            return this;
        }



        public CaseCaseNoteRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public CaseCaseNoteRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public CaseCaseNoteRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public CaseCaseNoteRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public CaseCaseNoteRecordPage ValidateDescriptionFieldEditable(bool ExpectEditable)
        {
            string className = GetElementClass(_description_Field);

            if (ExpectEditable)
                Assert.AreEqual(true, className.Contains("FormsEditText"));
            else
                Assert.AreEqual(true, className.Contains("FormsTextView"));

            return this;
        }
        public CaseCaseNoteRecordPage ValidateReasonFieldEditable(bool ExpectEditable)
        {
            string readonlyFieldClassName = GetElementClass(_Date_ReadOnlyField);

            if (ExpectEditable)
            {
                WaitForElement(_reason_FieldTitle);
                WaitForElement(_reason_Field);
                WaitForElement(_reason_LookupButton);
            }
            else
            {
                WaitForElement(_reason_FieldTitle);
                //WaitForElement(_reason_Field);
                //WaitForElementNotVisible(_reason_LookupButton);
                //WaitForElementNotVisible(_reason_RemoveButton);
            }

            return this;
        }
        public CaseCaseNoteRecordPage ValidateDateFieldEditable(bool ExpectEditable)
        {
            string readonlyFieldClassName = GetElementClass(_Date_ReadOnlyField);

            if (ExpectEditable)
            {
                WaitForElement(_Date_DateField);
                WaitForElement(_Date_DatePicker);
                WaitForElement(_Date_TimeField);
                WaitForElement(_Date_TimePicker);
                //WaitForElementNotVisible(_Date_ReadOnlyField);
            }
            else
            {
                //WaitForElementNotVisible(_Date_DateField);
                //WaitForElementNotVisible(_Date_DatePicker);
                //WaitForElementNotVisible(_Date_TimeField);
                //WaitForElementNotVisible(_Date_TimePicker);
                WaitForElement(_Date_ReadOnlyField);
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateStatusFieldEditable(bool ExpectEditable)
        {
            string className = GetElementClass(_Status_Field);

            if (ExpectEditable)
                Assert.AreEqual(true,  className.Contains("PickerEditText"));
            else
                Assert.AreEqual(true, className.Contains("FormsTextView"));

            return this;
        }






        public CaseCaseNoteRecordPage ValidateSubjectFieldText(string ExpectText)
        {
            ScrollToElement(_subject_Field);
            string fieldText = GetElementText(_subject_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_description_Field);
            string fieldText = GetElementText(_description_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDescriptionRichFieldText(string ExpectText)
        {            
            string fieldText = GetWebElementText(_descriptionRichTextEditorText_Field(ExpectText));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateReasonFieldText(string ExpectText)
        {
            ScrollToElement(_reason_Field);
            string fieldText = GetElementText(_reason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidatePriorityFieldText(string ExpectText)
        {
            ScrollToElement(_priority_Field);
            string fieldText = GetElementText(_priority_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateDateFieldText(string ExpectDateText, string ExpectTimeText)
        {
            ScrollToElement(_Date_DateField);

            string fieldText = GetElementText(_Date_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            fieldText = GetElementText(_Date_TimeField);
            Assert.AreEqual(ExpectTimeText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateOutcomeFieldText(string ExpectText)
        {
            ScrollToElement(_Outcome_Field);
            string fieldText = GetElementText(_Outcome_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateStatusFieldText(string ExpectText)
        {
            ScrollToElement(_Status_Field);
            string fieldText = GetElementText(_Status_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_Category_Field);
            string fieldText = GetElementText(_Category_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_SubCategory_Field);
            string fieldText = GetElementText(_SubCategory_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleUserFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            string fieldText = GetElementText(_ResponsibleUser_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public CaseCaseNoteRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseCaseNoteRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public CaseCaseNoteRecordPage InsertSubject(string ValueToInsert)
        {
            ScrollToElement(_subject_Field);
            this.EnterText(_subject_Field, ValueToInsert);

            return this;
        }

        public CaseCaseNoteRecordPage InsertDescription(string ValueToInsert)
        {
            ScrollToElement(_description_Field);
            this.EnterText(_description_Field, ValueToInsert);

            return this;
        }

        public CaseCaseNoteRecordPage TapReasonRemoveButton()
        {
            ScrollToElement(_reason_RemoveButton);
            Tap(_reason_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapReasonLookupButton()
        {
            ScrollToElement(_reason_LookupButton);
            Tap(_reason_LookupButton);
            
            return this;
        }

        public CaseCaseNoteRecordPage TapPriorityRemoveButton()
        {
            ScrollToElement(_priority_RemoveButton);
            Tap(_priority_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapPriorityLookupButton()
        {
            ScrollToElement(_priority_LookupButton);
            Tap(_priority_LookupButton);

            return this;
        }

        public CaseCaseNoteRecordPage InserDate(string DateValue, string TimeValue)
        {
            ScrollToElement(_Date_DateField);
            this.EnterText(_Date_DateField, DateValue);
            this.EnterText(_Date_TimeField, TimeValue);

            return this;
        }

        public CaseCaseNoteRecordPage TapOutcomeRemoveButton()
        {
            ScrollToElement(_Outcome_RemoveButton);
            Tap(_Outcome_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapOutcomeLookupButton()
        {
            ScrollToElement(_Outcome_LookupButton);
            Tap(_Outcome_LookupButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapStatusPicklist()
        {
            ScrollToElementWithWidthAndHeight(_Status_Field);
            TapOnElementWithWidthAndHeight(_Status_Field);

            return this;
        }

        public CaseCaseNoteRecordPage TapCategoryRemoveButton()
        {
            ScrollToElement(_Category_RemoveButton);
            Tap(_Category_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapCategoryLookupButton()
        {
            ScrollToElement(_Category_LookupButton);
            Tap(_Category_LookupButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapSubCategoryRemoveButton()
        {
            ScrollToElement(_SubCategory_RemoveButton);
            Tap(_SubCategory_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapSubCategoryLookupButton()
        {
            ScrollToElement(_SubCategory_LookupButton);
            Tap(_SubCategory_LookupButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapResponsibleUserRemoveButton()
        {
            ScrollToElement(_ResponsibleUser_RemoveButton);
            Tap(_ResponsibleUser_RemoveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapResponsibleUserLookupButton()
        {
            ScrollToElement(_ResponsibleUser_LookupButton);
            Tap(_ResponsibleUser_LookupButton);

            return this;
        }




        public CaseCaseNoteRecordPage ValidateReasonRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_reason_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_reason_RemoveButton));
            }
            else
            {
                TryScrollToElement(_reason_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_reason_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidatePriorityRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_priority_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_priority_RemoveButton));
            }
            else
            {
                TryScrollToElement(_priority_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_priority_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateOutcomeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_Outcome_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_RemoveButton));
            }
            else
            {
                TryScrollToElement(_Outcome_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCategoryRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_Category_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_Category_RemoveButton));
            }
            else
            {
                TryScrollToElement(_Category_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_Category_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubCategoryRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_SubCategory_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_SubCategory_RemoveButton));
            }
            else
            {
                TryScrollToElement(_SubCategory_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleUserRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ResponsibleUser_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleUser_RemoveButton));
            }
            else
            {
                TryScrollToElement(_ResponsibleUser_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_RemoveButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateReasonLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_reason_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_reason_LookupButton));
            }
            else
            {
                TryScrollToElement(_reason_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_reason_LookupButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidatePriorityLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_priority_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_priority_LookupButton));
            }
            else
            {
                TryScrollToElement(_priority_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_priority_LookupButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateOutcomeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_Outcome_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_LookupButton));
            }
            else
            {
                TryScrollToElement(_Outcome_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_LookupButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateCategoryLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_Category_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_Category_LookupButton));
            }
            else
            {
                TryScrollToElement(_Category_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_Category_LookupButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateSubCategoryLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_SubCategory_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_SubCategory_LookupButton));
            }
            else
            {
                TryScrollToElement(_SubCategory_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_SubCategory_LookupButton));
            }

            return this;
        }

        public CaseCaseNoteRecordPage ValidateResponsibleUserLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ResponsibleUser_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleUser_LookupButton));
            }
            else
            {
                TryScrollToElement(_ResponsibleUser_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleUser_LookupButton));
            }

            return this;
        }




        public CaseCaseNoteRecordPage TapOnBackButton()
        {
            Tap(_backButton);

            return this;
        }
        public CaseCaseNoteRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public CaseCaseNoteRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public CaseCaseNoteRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public CaseCaseNoteRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public CaseCaseNoteRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

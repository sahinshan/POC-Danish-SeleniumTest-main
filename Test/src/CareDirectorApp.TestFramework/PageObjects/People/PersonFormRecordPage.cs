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
    public class PersonFormRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personform_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personform_SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _editAssessmentButton = e => e.Marked("personform_EditAssessmentButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personform_DeleteRecordButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personform_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _additionalItemsButton = e => e.Marked("AdditionalImage");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _FormType_FieldTitle = e => e.Marked("Form Type");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _Status_FieldTitle = e => e.Marked("Status");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _DueDate_FieldTitle = e => e.Marked("Due Date");
        readonly Func<AppQuery, AppQuery> _ReviewDate_FieldTitle = e => e.Marked("Review Date");
        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _SourceCase_FieldTitle = e => e.Marked("Source Case");

        readonly Func<AppQuery, AppQuery> _CompletedBy_FieldTitle = e => e.Marked("Completed By");
        readonly Func<AppQuery, AppQuery> _CompletionDate_FieldTitle = e => e.Marked("Completion Date");
        readonly Func<AppQuery, AppQuery> _SignedOffBy_FieldTitle = e => e.Marked("Signed Off By");
        readonly Func<AppQuery, AppQuery> _SignedOffDate_FieldTitle = e => e.Marked("Signed Off Date");
         
        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _FormType_Field = e => e.Marked("Field_af3ee6636a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _FormType_RemoveButton = e => e.Marked("af3ee6636a1ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _FormType_LookupButton = e => e.Marked("af3ee6636a1ae91180dc0050560502cc_OpenLookup");
        
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("Field_8c1e666b6a1ae91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_8774dc726a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveButton = e => e.Marked("8774dc726a1ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("8774dc726a1ae91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Status_Field = e => e.Marked("Field_94309a836a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _StartDate_DateField = e => e.Marked("Field_fbe563916a1ae91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _StartDate_DatePicker = e => e.Marked("Field_fbe563916a1ae91180dc0050560502cc_OpenPicker");
        readonly Func<AppQuery, AppQuery> _DueDate_DateField = e => e.All().Marked("Field_7605959b6a1ae91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _DueDate_DatePicker = e => e.All().Marked("Field_7605959b6a1ae91180dc0050560502cc_OpenPicker");
        readonly Func<AppQuery, AppQuery> _ReviewDate_DateField = e => e.Marked("Field_627850a66a1ae91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _ReviewDate_DatePicker = e => e.Marked("Field_627850a66a1ae91180dc0050560502cc_OpenPicker");
        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_c7f5707fa74ce911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _SourceCase_LookupEntryField = e => e.Marked("a82334d22533eb11a2d30050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _SourceCase_RemoveButton = e => e.Marked("a82334d22533eb11a2d30050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _SourceCase_LookupButton = e => e.Marked("a82334d22533eb11a2d30050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _CompletedBy_Field = e => e.Marked("Field_8c653bd86a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _CompletionDate_Field = e => e.Marked("Field_a09480e36a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _SignedOffBy_Field = e => e.Marked("Field_65bf6deb6a1ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _SignedOffDate_Field = e => e.Marked("Field_132da4f16a1ae91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public PersonFormRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonFormRecordPage WaitForPersonFormRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            WaitForElement(_FormType_FieldTitle);

            return this;
        }



        public PersonFormRecordPage ValidateFormTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_FormType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_FormType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_FormType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_FormType_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateStatusFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StartDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_StartDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_StartDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_StartDate_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateDueDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DueDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DueDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DueDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DueDate_FieldTitle));
            }

            return this;
        }
        
        public PersonFormRecordPage ValidateReviewDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ReviewDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ReviewDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ReviewDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ReviewDate_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Person_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Person_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Person_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Person_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateSourceCaseFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SourceCase_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_SourceCase_FieldTitle));
            }
            else
            {
                TryScrollToElement(_SourceCase_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_SourceCase_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateCompletedByFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CompletedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CompletedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CompletedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CompletedBy_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateCompletionDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CompletionDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CompletionDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CompletionDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CompletionDate_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateSignedOffByFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SignedOffBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_SignedOffBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_SignedOffBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_SignedOffBy_FieldTitle));
            }

            return this;
        }

        public PersonFormRecordPage ValidateSignedOffDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SignedOffDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_SignedOffDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_SignedOffDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_SignedOffDate_FieldTitle));
            }

            return this;
        }



        public PersonFormRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public PersonFormRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public PersonFormRecordPage ValidateFormTypeFieldText(string ExpectText)
        {
            ScrollToElement(_FormType_Field);
            string fieldText = GetElementText(_FormType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateResponsibleUserFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            string fieldText = GetElementText(_ResponsibleUser_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateStatusFieldText(string ExpectText)
        {
            ScrollToElement(_Status_Field);
            string fieldText = GetElementText(_Status_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementText(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateDueDateFieldText(string ExpectText)
        {
            ScrollToElement(_DueDate_DateField);
            string fieldText = GetElementText(_DueDate_DateField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateReviewDateFieldText(string ExpectText)
        {
            ScrollToElement(_ReviewDate_DateField);
            string fieldText = GetElementText(_ReviewDate_DateField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidatePersonFieldText(string ExpectText)
        {
            ScrollToElement(_Person_Field);
            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public PersonFormRecordPage ValidateSourceCaseLookupEntryFieldText(string ExpectText)
        {
            ScrollToElement(_SourceCase_LookupEntryField);
            string fieldText = GetElementText(_SourceCase_LookupEntryField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateCompletedByFieldText(string ExpectText)
        {
            ScrollToElement(_CompletedBy_Field);
            string fieldText = GetElementText(_CompletedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public PersonFormRecordPage ValidateCompletionDateFieldText(string ExpectText)
        {
            ScrollToElement(_CompletionDate_Field);
            string fieldText = GetElementText(_CompletionDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public PersonFormRecordPage ValidateSignedOffByFieldText(string ExpectText)
        {
            ScrollToElement(_SignedOffBy_Field);
            string fieldText = GetElementText(_SignedOffBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public PersonFormRecordPage ValidateSignedOffDateFieldText(string ExpectText)
        {
            ScrollToElement(_SignedOffDate_Field);
            string fieldText = GetElementText(_SignedOffDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonFormRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonFormRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonFormRecordPage TapFormTypeRemoveButton()
        {
            ScrollToElement(_FormType_RemoveButton);
            Tap(_FormType_RemoveButton);

            return this;
        }

        public PersonFormRecordPage TapFormTypeLookupButton()
        {
            ScrollToElement(_FormType_LookupButton);
            Tap(_FormType_LookupButton);

            return this;
        }

        public PersonFormRecordPage TapResponsibleTeamRemoveButton()
        {
            ScrollToElement(_ResponsibleTeam_RemoveButton);
            Tap(_ResponsibleTeam_RemoveButton);

            return this;
        }

        public PersonFormRecordPage TapResponsibleTeamLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_LookupButton);
            Tap(_ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonFormRecordPage TapSourceCaseRemoveButton()
        {
            ScrollToElement(_SourceCase_RemoveButton);
            Tap(_SourceCase_RemoveButton);

            return this;
        }

        public PersonFormRecordPage TapSourceCaseLookupButton()
        {
            ScrollToElement(_SourceCase_LookupButton);
            Tap(_SourceCase_LookupButton);

            return this;
        }



        public PersonFormRecordPage InsertStartDate(string DateValue)
        {
            ScrollToElement(_StartDate_DateField);
            this.EnterText(_StartDate_DateField, DateValue);

            return this;
        }

        public PersonFormRecordPage InsertDueDate(string DateValue)
        {
            ScrollToElement(_DueDate_DateField);
            this.EnterText(_DueDate_DateField, DateValue);

            return this;
        }

        public PersonFormRecordPage InsertReviewDate(string DateValue)
        {
            ScrollToElement(_ReviewDate_DateField);
            this.EnterText(_ReviewDate_DateField, DateValue);

            return this;
        }








        public PersonFormRecordPage ValidateFormTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_FormType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_FormType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_FormType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_FormType_RemoveButton));
            }

            return this;
        }

        public PersonFormRecordPage ValidateResponsibleTeamRemoveButtonVisible(bool ExpectButtonVisible)
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

        public PersonFormRecordPage ValidateFormTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_FormType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_FormType_LookupButton));
            }
            else
            {
                TryScrollToElement(_FormType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_FormType_LookupButton));
            }

            return this;
        }

        public PersonFormRecordPage ValidateResponsibleTeamLookupButtonVisible(bool ExpectButtonVisible)
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


        





        public PersonFormRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonFormRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonFormRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }

        public PersonFormRecordPage TapEditAssessmentButton()
        {
            Tap(_editAssessmentButton);

            return this;
        }


        public PersonFormRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonFormRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public PersonFormRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

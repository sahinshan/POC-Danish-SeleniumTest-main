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
    public class PersonFinancialAssessmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("financialassessment_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("financialassessment_SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("financialassessment_DeleteRecordButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("financialassessment_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("financialassessment_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string pageTitle) => e => e.Marked("MainStackLayout").Descendant().Marked(pageTitle);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Id_FieldTitle = e => e.Marked("Id");
        readonly Func<AppQuery, AppQuery> _FinancialAssessmentStatus_FieldTitle = e => e.Marked("Financial Assessment Status");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");


        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");

        readonly Func<AppQuery, AppQuery> _ChargingRule_FieldTitle = e => e.Marked("Charging Rule");
        readonly Func<AppQuery, AppQuery> _IncomeSupportType_FieldTitle = e => e.Marked("Income Support Type");
        readonly Func<AppQuery, AppQuery> _FinancialAssessmentType_FieldTitle = e => e.Marked("Financial Assessment Type");
        readonly Func<AppQuery, AppQuery> _DaysPropertyDisregarded_FieldTitle = e => e.Marked("Days Property Disregarded");
        readonly Func<AppQuery, AppQuery> _IncomeSupportValue_FieldTitle = e => e.Marked("Income Support Value");

        readonly Func<AppQuery, AppQuery> _CommencementDate_FieldTitle = e => e.Marked("Commencement Date");
        readonly Func<AppQuery, AppQuery> _PermitChargeUpdatesViaFinancialAssessment_FieldTitle = e => e.Marked("Permit Charge Updates via Financial Assessment?");
        readonly Func<AppQuery, AppQuery> _PermitChargeUpdatesViaRecalculation_FieldTitle = e => e.Marked("Permit Charge Updates via Recalculation?");

        readonly Func<AppQuery, AppQuery> _NoteText_FieldTitle = e => e.Marked("Note Text");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Id_Field = e => e.Marked("Field_9bdc90ba0128eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _FinancialAssessmentStatus_Field = e => e.Marked("Field_9fdc90ba0128eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("Field_81d5b0d10128eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_9e3e46db0128eb11a2ce0050569231cf");

        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_014d8d370228eb11a2ce0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_fb667d590228eb11a2ce0050569231cf_Date");

        readonly Func<AppQuery, AppQuery> _ChargingRule_LookupEntry = e => e.Marked("e04d4e9e0228eb11a2ce0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ChargingRule_RemoveValueButton = e => e.Marked("e04d4e9e0228eb11a2ce0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ChargingRule_OpenLookupButton = e => e.Marked("e04d4e9e0228eb11a2ce0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _IncomeSupportType_LookupEntry = e => e.Marked("c6797db40228eb11a2ce0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _IncomeSupportType_RemoveValueButton = e => e.Marked("c6797db40228eb11a2ce0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _IncomeSupportType_OpenLookupButton = e => e.Marked("c6797db40228eb11a2ce0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _FinancialAssessmentType_Field = e => e.Marked("Field_a20703c50228eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _DaysPropertyDisregarded_Field = e => e.Marked("Field_b3436cd20228eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _IncomeSupportValue_EditableField = e => e.Marked("Field_ef8367da0228eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _IncomeSupportValue_ReadonlyField = e => e.Marked("Field_e33ac09c702eeb11a2d00050569231cf");

        readonly Func<AppQuery, AppQuery> _CommencementDate_Field = e => e.Marked("Field_adf5820f0428eb11a2ce0050569231cf");
        readonly Func<AppQuery, AppQuery> _PermitChargeUpdatesViaFinancialAssessment_Field = e => e.Marked("Field_7e440a160428eb11a2ce0050569231cf").Class("PickerEditText");
        readonly Func<AppQuery, AppQuery> _PermitChargeUpdatesViaRecalculation_Field = e => e.Marked("Field_da7dbc1c0428eb11a2ce0050569231cf").Class("PickerEditText");

        readonly Func<AppQuery, AppQuery> _NoteText_ViewField = e => e.Marked("Field_a6b277610428eb11a2ce0050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _NoteText_EditField = e => e.Marked("Field_a6b277610428eb11a2ce0050569231cf").Class("FormsEditText");

        #endregion



        public PersonFinancialAssessmentRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialAssessmentRecordPage WaitForPersonFinancialAssessmentRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_saveButton);
            WaitForElement(_saveAndCloseButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            //WaitForElement(_topBannerArea);

            //WaitForElement(_FinancialAssessmentStatus_FieldTitle);

            return this;
        }


        public PersonFinancialAssessmentRecordPage ValidateIdFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Id_FieldTitle);
            ValidateElementVisibility(_Id_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateFinancialAssessmentStatusFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_FinancialAssessmentStatus_FieldTitle);
            ValidateElementVisibility(_FinancialAssessmentStatus_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ResponsibleUser_FieldTitle);
            ValidateElementVisibility(_ResponsibleUser_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ResponsibleTeam_FieldTitle);
            ValidateElementVisibility(_ResponsibleTeam_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateStartDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_StartDate_FieldTitle);
            ValidateElementVisibility(_StartDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateEndDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_EndDate_FieldTitle);
            ValidateElementVisibility(_EndDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateChargingRuleFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ChargingRule_FieldTitle);
            ValidateElementVisibility(_ChargingRule_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_IncomeSupportType_FieldTitle);
            ValidateElementVisibility(_IncomeSupportType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateFinancialAssessmentTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_FinancialAssessmentType_FieldTitle);
            ValidateElementVisibility(_FinancialAssessmentType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateDaysPropertyDisregardedFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_DaysPropertyDisregarded_FieldTitle);
            ValidateElementVisibility(_DaysPropertyDisregarded_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportValueFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_IncomeSupportValue_FieldTitle);
            ValidateElementVisibility(_IncomeSupportValue_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateCommencementDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_CommencementDate_FieldTitle);
            ValidateElementVisibility(_CommencementDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidatePermitChargeUpdatesViaFinancialAssessmentFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_PermitChargeUpdatesViaFinancialAssessment_FieldTitle);
            ValidateElementVisibility(_PermitChargeUpdatesViaFinancialAssessment_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidatePermitChargeUpdatesViaRecalculationFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_PermitChargeUpdatesViaRecalculation_FieldTitle);
            ValidateElementVisibility(_PermitChargeUpdatesViaRecalculation_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateNoteTextFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_NoteText_FieldTitle);
            ValidateElementVisibility(_NoteText_FieldTitle, ExpectElementVisible);
            return this;
        }



        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportTypeLookupButtonVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_IncomeSupportType_OpenLookupButton);
            ValidateElementVisibility(_IncomeSupportType_OpenLookupButton, ExpectElementVisible);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportTypeRemoveButtonVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_IncomeSupportType_RemoveValueButton);
            ValidateElementVisibility(_IncomeSupportType_RemoveValueButton, ExpectElementVisible);

            return this;
        }





        public PersonFinancialAssessmentRecordPage ValidateIdFieldText(string ExpectedText)
        {
            ScrollToElement(_Id_Field);
            ValidateElementText(_Id_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateFinancialAssessmentStatusFieldText(string ExpectedText)
        {
            ScrollToElement(_FinancialAssessmentStatus_Field);
            ValidateElementText(_FinancialAssessmentStatus_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateResponsibleUserFieldText(string ExpectedText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            ValidateElementText(_ResponsibleUser_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateResponsibleTeamFieldText(string ExpectedText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            ValidateElementText(_ResponsibleTeam_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            ScrollToElement(_StartDate_Field);
            ValidateElementText(_StartDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            ScrollToElement(_EndDate_Field);
            ValidateElementText(_EndDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateChargingRuleFieldText(string ExpectedText)
        {
            ScrollToElement(_ChargingRule_LookupEntry);
            ValidateElementText(_ChargingRule_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportTypeFieldText(string ExpectedText)
        {
            ScrollToElement(_IncomeSupportType_LookupEntry);
            ValidateElementText(_IncomeSupportType_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateFinancialAssessmentTypeFieldText(string ExpectedText)
        {
            ScrollToElement(_FinancialAssessmentType_Field);
            ValidateElementText(_FinancialAssessmentType_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateDaysPropertyDisregardedFieldText(string ExpectedText)
        {
            ScrollToElement(_DaysPropertyDisregarded_Field);
            ValidateElementText(_DaysPropertyDisregarded_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportValueEditableFieldText(string ExpectedText)
        {
            ScrollToElement(_IncomeSupportValue_EditableField);
            ValidateElementText(_IncomeSupportValue_EditableField, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateIncomeSupportValueReadOnlyFieldText(string ExpectedText)
        {
            ScrollToElement(_IncomeSupportValue_ReadonlyField);
            ValidateElementText(_IncomeSupportValue_ReadonlyField, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateCommencementDateFieldText(string ExpectedText)
        {
            ScrollToElement(_CommencementDate_Field);
            ValidateElementText(_CommencementDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText(string ExpectedText)
        {
            ScrollToElement(_PermitChargeUpdatesViaFinancialAssessment_Field);
            ValidateElementText(_PermitChargeUpdatesViaFinancialAssessment_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidatePermitChargeUpdatesViaRecalculationFieldText(string ExpectedText)
        {
            ScrollToElement(_PermitChargeUpdatesViaRecalculation_Field);
            ValidateElementText(_PermitChargeUpdatesViaRecalculation_Field, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateNoteTextEditFieldText(string ExpectedText)
        {
            ScrollToElement(_NoteText_EditField);
            ValidateElementText(_NoteText_EditField, ExpectedText);

            return this;
        }
        public PersonFinancialAssessmentRecordPage ValidateNoteTextViewFieldText(string ExpectedText)
        {
            ScrollToElement(_NoteText_ViewField);
            ValidateElementText(_NoteText_ViewField, ExpectedText);

            return this;
        }



        public PersonFinancialAssessmentRecordPage InsertStartDate(string ValueToInsert)
        {
            ScrollToElement(_StartDate_Field);
            EnterText(_StartDate_Field, ValueToInsert);

            return this;
        }
        public PersonFinancialAssessmentRecordPage InsertEndDate(string ValueToInsert)
        {
            ScrollToElement(_EndDate_Field);
            EnterText(_EndDate_Field, ValueToInsert);

            return this;
        }
        public PersonFinancialAssessmentRecordPage InsertDaysPropertyDisregarded(string ValueToInsert)
        {
            ScrollToElement(_DaysPropertyDisregarded_Field);
            EnterText(_DaysPropertyDisregarded_Field, ValueToInsert);

            return this;
        }
        public PersonFinancialAssessmentRecordPage InsertIncomeSupportValue(string ValueToInsert)
        {
            ScrollToElement(_IncomeSupportValue_EditableField);
            EnterText(_IncomeSupportValue_EditableField, ValueToInsert);

            return this;
        }
        public PersonFinancialAssessmentRecordPage InsertNotes(string ValueToInsert)
        {
            ScrollToElement(_NoteText_EditField);
            EnterText(_NoteText_EditField, ValueToInsert);

            return this;
        }



        public PersonFinancialAssessmentRecordPage TapChargingRuleLookupButton()
        {
            ScrollToElement(_ChargingRule_OpenLookupButton);
            Tap(_ChargingRule_OpenLookupButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapChargingRuleRemoveButton()
        {
            ScrollToElement(_ChargingRule_RemoveValueButton);
            Tap(_ChargingRule_RemoveValueButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapIncomeSupportTypeLookupButton()
        {
            ScrollToElement(_IncomeSupportType_OpenLookupButton);
            Tap(_IncomeSupportType_OpenLookupButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapIncomeSupportTypeRemoveButton()
        {
            ScrollToElement(_IncomeSupportType_RemoveValueButton);
            Tap(_IncomeSupportType_RemoveValueButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapPermitChargeUpdatesViaFinancialAssessmentField()
        {
            ScrollToElement(_PermitChargeUpdatesViaFinancialAssessment_Field);
            Tap(_PermitChargeUpdatesViaFinancialAssessment_Field);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapPermitChargeUpdatesViaRecalculationField()
        {
            ScrollToElement(_PermitChargeUpdatesViaRecalculation_Field);
            Tap(_PermitChargeUpdatesViaRecalculation_Field);

            return this;
        }




        public PersonFinancialAssessmentRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }
        public PersonFinancialAssessmentRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }



    }
}

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
    public class HealthAppointmentCaseNoteRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("healthappointmentcasenote_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("healthappointmentcasenote_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("healthappointmentcasenote_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("healthappointmentcasenote_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("healthappointmentcasenote_SaveAndBackButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _subject_FieldTitle = e => e.Marked("Subject");
        readonly Func<AppQuery, AppQuery> _description_FieldTitle = e => e.Marked("Description");

        readonly Func<AppQuery, AppQuery> _reason_FieldTitle = e => e.Marked("Reason");
        readonly Func<AppQuery, AppQuery> _priority_FieldTitle = e => e.Marked("Priority");
        readonly Func<AppQuery, AppQuery> _Date_FieldTitle = e => e.Marked("Date");
        readonly Func<AppQuery, AppQuery> _Outcome_FieldTitle = e => e.Marked("Outcome");
        readonly Func<AppQuery, AppQuery> _Status_FieldTitle = e => e.Marked("Status");
        readonly Func<AppQuery, AppQuery> _CareIntervention_FieldTitle = e => e.Marked("Care Intervention");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");
        readonly Func<AppQuery, AppQuery> _Category_FieldTitle = e => e.Marked("Category");
        readonly Func<AppQuery, AppQuery> _SubCategory_FieldTitle = e => e.Marked("Sub-Category");
        readonly Func<AppQuery, AppQuery> _ContainsInformationProvidedByAThirdParty_FieldTitle = e => e.Marked("Contains Information Provided By A Third Party?");


        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _subject_Field = e => e.Marked("Field_f0c5894cf0e8e81180dc0050560502cc");
        
        readonly Func<AppQuery, AppQuery> _descriptionWarningMessage_Field = e => e.Marked("The following text was created using a rich text editor and cannot be edited on this device. Click the expand icon to view text in a larger window.");
        Func<AppQuery, AppWebQuery> _descriptionRichTextEditorText_Field(string ExpectedText) => e => e.XPath("//*[text()='" + ExpectedText + "']");
        readonly Func<AppQuery, AppQuery> _description_Field = e => e.Marked("Field_fd801e84b486e911a2c50050569231cf");


        readonly Func<AppQuery, AppQuery> _reason_Field = e => e.Marked("b9b50c98b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _reason_RemoveButton = e => e.Marked("b9b50c98b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _reason_LookupButton = e => e.Marked("b9b50c98b486e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _priority_Field = e => e.Marked("c3b50c98b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _priority_RemoveButton = e => e.Marked("c3b50c98b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _priority_LookupButton = e => e.Marked("c3b50c98b486e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Date_DateField = e => e.Marked("Field_22c1099eb486e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _Date_DatePicker = e => e.Marked("Field_22c1099eb486e911a2c50050569231cf_OpenPicker").Index(0);
        readonly Func<AppQuery, AppQuery> _Date_TimeField = e => e.Marked("Field_22c1099eb486e911a2c50050569231cf_Time");
        readonly Func<AppQuery, AppQuery> _Date_TimePicker = e => e.Marked("Field_22c1099eb486e911a2c50050569231cf_OpenPicker").Index(1);

        readonly Func<AppQuery, AppQuery> _Outcome_Field = e => e.Marked("291637a6b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Outcome_RemoveButton = e => e.Marked("291637a6b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupButton = e => e.Marked("291637a6b486e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Status_Field = e => e.All().Marked("Field_361637a6b486e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _CareIntervention_Field = e => e.Marked("eb04f2d4974dea11a2ca0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _CareIntervention_RemoveButton = e => e.Marked("eb04f2d4974dea11a2ca0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _CareIntervention_LookupButton = e => e.Marked("eb04f2d4974dea11a2ca0050569231cf_OpenLookup");





        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_b4acedc2b486e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("c0acedc2b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_RemoveButton = e => e.Marked("c0acedc2b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_LookupButton = e => e.Marked("c0acedc2b486e911a2c50050569231cf_OpenLookup"); 

        readonly Func<AppQuery, AppQuery> _Category_Field = e => e.Marked("904013c9b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Category_RemoveButton = e => e.Marked("904013c9b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Category_LookupButton = e => e.Marked("904013c9b486e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _SubCategory_Field = e => e.Marked("0cb501d2b486e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _SubCategory_RemoveButton = e => e.Marked("0cb501d2b486e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _SubCategory_LookupButton = e => e.Marked("0cb501d2b486e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _ContainsInformationProvidedByAThirdParty_Field = e => e.All().Marked("Field_10b501d2b486e911a2c50050569231cf");





        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public HealthAppointmentCaseNoteRecordPage(IApp app)
        {
            _app = app;
        }


        public HealthAppointmentCaseNoteRecordPage WaitForHealthAppointmentCaseNoteRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            //WaitForElement(_topBannerArea);
            
            return this;
        }



        public HealthAppointmentCaseNoteRecordPage ValidateSubjectFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
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


        public HealthAppointmentCaseNoteRecordPage ValidateReasonFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidatePriorityFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateOutcomeFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateStatusFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateCareInterventionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CareIntervention_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CareIntervention_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CareIntervention_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CareIntervention_FieldTitle));
            }

            return this;
        }


        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateCategoryFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateSubCategoryFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateContainsInformationProvidedByAThirdPartyFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ContainsInformationProvidedByAThirdParty_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ContainsInformationProvidedByAThirdParty_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ContainsInformationProvidedByAThirdParty_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ContainsInformationProvidedByAThirdParty_FieldTitle));
            }

            return this;
        }







        public HealthAppointmentCaseNoteRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public HealthAppointmentCaseNoteRecordPage ValidateSubjectFieldText(string ExpectText)
        {
            ScrollToElement(_subject_Field);
            string fieldText = GetElementText(_subject_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_description_Field);
            string fieldText = GetElementText(_description_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateDescriptionRichFieldText(string ExpectText)
        {            
            string fieldText = GetWebElementText(_descriptionRichTextEditorText_Field(ExpectText));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateReasonFieldText(string ExpectText)
        {
            ScrollToElement(_reason_Field);
            string fieldText = GetElementText(_reason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidatePriorityFieldText(string ExpectText)
        {
            ScrollToElement(_priority_Field);
            string fieldText = GetElementText(_priority_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateDateFieldText(string ExpectDateText, string ExpectTimeText)
        {
            ScrollToElement(_Date_DateField);

            string fieldText = GetElementText(_Date_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            fieldText = GetElementText(_Date_TimeField);
            Assert.AreEqual(ExpectTimeText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateOutcomeFieldText(string ExpectText)
        {
            ScrollToElement(_Outcome_Field);
            string fieldText = GetElementText(_Outcome_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateStatusFieldText(string ExpectText)
        {
            ScrollToElement(_Status_Field);
            string fieldText = GetElementText(_Status_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateCareInterventionFieldText(string ExpectText)
        {
            ScrollToElement(_CareIntervention_Field);
            string fieldText = GetElementText(_CareIntervention_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleUserFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            string fieldText = GetElementText(_ResponsibleUser_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_Category_Field);
            string fieldText = GetElementText(_Category_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateSubCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_SubCategory_Field);
            string fieldText = GetElementText(_SubCategory_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateContainsInformationProvidedByAThirdPartyFieldText(string ExpectText)
        {
            ScrollToElement(_ContainsInformationProvidedByAThirdParty_Field);
            string fieldText = GetElementText(_ContainsInformationProvidedByAThirdParty_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        public HealthAppointmentCaseNoteRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public HealthAppointmentCaseNoteRecordPage InsertSubject(string ValueToInsert)
        {
            ScrollToElement(_subject_Field);
            this.EnterText(_subject_Field, ValueToInsert);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage InsertDescription(string ValueToInsert)
        {
            ScrollToElement(_description_Field);
            this.EnterText(_description_Field, ValueToInsert);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapReasonRemoveButton()
        {
            ScrollToElement(_reason_RemoveButton);
            Tap(_reason_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapReasonLookupButton()
        {
            ScrollToElement(_reason_LookupButton);
            Tap(_reason_LookupButton);
            
            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapPriorityRemoveButton()
        {
            ScrollToElement(_priority_RemoveButton);
            Tap(_priority_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapPriorityLookupButton()
        {
            ScrollToElement(_priority_LookupButton);
            Tap(_priority_LookupButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage InserDate(string DateValue, string TimeValue)
        {
            ScrollToElement(_Date_DateField);
            this.EnterText(_Date_DateField, DateValue);
            this.EnterText(_Date_TimeField, TimeValue);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapOutcomeRemoveButton()
        {
            ScrollToElement(_Outcome_RemoveButton);
            Tap(_Outcome_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapOutcomeLookupButton()
        {
            ScrollToElement(_Outcome_LookupButton);
            Tap(_Outcome_LookupButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapStatusPicklist()
        {
            ScrollToElementWithWidthAndHeight(_Status_Field);
            TapOnElementWithWidthAndHeight(_Status_Field);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapCareInterventionRemoveButton()
        {
            ScrollToElement(_CareIntervention_RemoveButton);
            Tap(_CareIntervention_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapCareInterventionLookupButton()
        {
            ScrollToElement(_CareIntervention_LookupButton);
            Tap(_CareIntervention_LookupButton);

            return this;
        }




        public HealthAppointmentCaseNoteRecordPage TapResponsibleUserRemoveButton()
        {
            ScrollToElement(_ResponsibleUser_RemoveButton);
            Tap(_ResponsibleUser_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapResponsibleUserLookupButton()
        {
            ScrollToElement(_ResponsibleUser_LookupButton);
            Tap(_ResponsibleUser_LookupButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapCategoryRemoveButton()
        {
            ScrollToElement(_Category_RemoveButton);
            Tap(_Category_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapCategoryLookupButton()
        {
            ScrollToElement(_Category_LookupButton);
            Tap(_Category_LookupButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapSubCategoryRemoveButton()
        {
            ScrollToElement(_SubCategory_RemoveButton);
            Tap(_SubCategory_RemoveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapSubCategoryLookupButton()
        {
            ScrollToElement(_SubCategory_LookupButton);
            Tap(_SubCategory_LookupButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapContainsInformationProvidedByAThirdPartyField()
        {
            ScrollToElementWithWidthAndHeight(_ContainsInformationProvidedByAThirdParty_Field);
            TapOnElementWithWidthAndHeight(_ContainsInformationProvidedByAThirdParty_Field);

            return this;
        }






        public HealthAppointmentCaseNoteRecordPage ValidateReasonRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidatePriorityRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateOutcomeRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateCareInterventionRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_CareIntervention_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_CareIntervention_RemoveButton));
            }
            else
            {
                TryScrollToElement(_CareIntervention_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_CareIntervention_RemoveButton));
            }

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateCategoryRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateSubCategoryRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleUserRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateReasonLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidatePriorityLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateOutcomeLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateCareInterventionLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_CareIntervention_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_CareIntervention_LookupButton));
            }
            else
            {
                TryScrollToElement(_CareIntervention_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_CareIntervention_LookupButton));
            }

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage ValidateCategoryLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateSubCategoryLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentCaseNoteRecordPage ValidateResponsibleUserLookupButtonVisible(bool ExpectButtonVisible)
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





        public HealthAppointmentCaseNoteRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public HealthAppointmentCaseNoteRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public HealthAppointmentCaseNoteRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

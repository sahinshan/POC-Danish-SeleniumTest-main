﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class HealthAppointmentTaskRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("task_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("task_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("task_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("task_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("task_SaveAndCloseButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _subject_FieldTitle = e => e.Marked("Subject");
        readonly Func<AppQuery, AppQuery> _description_FieldTitle = e => e.Marked("Description");

        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _Due_FieldTitle = e => e.Marked("Due");
        readonly Func<AppQuery, AppQuery> _Status_FieldTitle = e => e.Marked("Status");
        readonly Func<AppQuery, AppQuery> _Outcome_FieldTitle = e => e.Marked("Outcome");

        readonly Func<AppQuery, AppQuery> _Category_FieldTitle = e => e.Marked("Category");
        readonly Func<AppQuery, AppQuery> _SubCategory_FieldTitle = e => e.Marked("Sub-Category");
        readonly Func<AppQuery, AppQuery> _priority_FieldTitle = e => e.Marked("Priority");
        readonly Func<AppQuery, AppQuery> _reason_FieldTitle = e => e.Marked("Reason");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _subject_Field = e => e.Marked("Field_adf58e0350a0e61180d30050560502cc");
        
        readonly Func<AppQuery, AppQuery> _descriptionWarningMessage_Field = e => e.Marked("The following text was created using a rich text editor and cannot be edited on this device. Click the expand icon to view text in a larger window.");
        Func<AppQuery, AppWebQuery> _descriptionRichTextEditorText_Field(string ExpectedText) => e => e.XPath("//*[text()='" + ExpectedText + "']");
        readonly Func<AppQuery, AppQuery> _description_Field = e => e.Marked("Field_b0f58e0350a0e61180d30050560502cc");



        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("Field_b11eeeb18619e91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_e9771a3a8719e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupEntryField = e => e.Marked("e9771a3a8719e91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Removebutton = e => e.Marked("e9771a3a8719e91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("e9771a3a8719e91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Due_DateField = e => e.Marked("Field_835a971750a0e61180d30050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _Due_DatePicker = e => e.Marked("Field_835a971750a0e61180d30050560502cc_OpenPicker").Index(0);
        readonly Func<AppQuery, AppQuery> _Due_TimeField = e => e.Marked("Field_835a971750a0e61180d30050560502cc_Time");
        readonly Func<AppQuery, AppQuery> _Due_TimePicker = e => e.Marked("Field_835a971750a0e61180d30050560502cc_OpenPicker").Index(1);

        readonly Func<AppQuery, AppQuery> _Status_Field = e => e.All().Marked("Field_1e705d7a0aace61180d30050560502cc");

        readonly Func<AppQuery, AppQuery> _Outcome_Field = e => e.Marked("76297c850aace61180d30050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Outcome_RemoveButton = e => e.Marked("76297c850aace61180d30050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupButton = e => e.Marked("76297c850aace61180d30050560502cc_OpenLookup");


        readonly Func<AppQuery, AppQuery> _Category_Field = e => e.Marked("865a971750a0e61180d30050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Category_RemoveButton = e => e.Marked("865a971750a0e61180d30050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Category_LookupButton = e => e.Marked("865a971750a0e61180d30050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _SubCategory_Field = e => e.Marked("2935281e50a0e61180d30050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _SubCategory_RemoveButton = e => e.Marked("2935281e50a0e61180d30050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _SubCategory_LookupButton = e => e.Marked("2935281e50a0e61180d30050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _priority_Field = e => e.Marked("d8e4ab5ee476e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _priority_RemoveButton = e => e.Marked("d8e4ab5ee476e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _priority_LookupButton = e => e.Marked("d8e4ab5ee476e911a2c50050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _reason_Field = e => e.Marked("564f6b2b50a0e61180d30050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _reason_RemoveButton = e => e.Marked("564f6b2b50a0e61180d30050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _reason_LookupButton = e => e.Marked("564f6b2b50a0e61180d30050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public HealthAppointmentTaskRecordPage(IApp app)
        {
            _app = app;
        }


        public HealthAppointmentTaskRecordPage WaitForHealthAppointmentTaskRecordPageToLoad(string PageTitleText)
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



        public HealthAppointmentTaskRecordPage ValidateSubjectFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
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



        public HealthAppointmentTaskRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateDueFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Due_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Due_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Due_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Due_FieldTitle));
            }

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateStatusFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateOutcomeFieldTitleVisible(bool ExpectFieldVisible)
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



        public HealthAppointmentTaskRecordPage ValidateCategoryFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateSubCategoryFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidatePriorityFieldTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateReasonFieldTitleVisible(bool ExpectFieldVisible)
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

        


        public HealthAppointmentTaskRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public HealthAppointmentTaskRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public HealthAppointmentTaskRecordPage ValidateSubjectFieldText(string ExpectText)
        {
            ScrollToElement(_subject_Field);
            string fieldText = GetElementText(_subject_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_description_Field);
            string fieldText = GetElementText(_description_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateDescriptionRichFieldText(string ExpectText)
        {
            string fieldText = GetWebElementText(_descriptionRichTextEditorText_Field(ExpectText));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public HealthAppointmentTaskRecordPage ValidateResponsibleUserFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            string fieldText = GetElementText(_ResponsibleUser_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateResponsibleTeamLookupFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_LookupEntryField);
            string fieldText = GetElementText(_ResponsibleTeam_LookupEntryField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateDueFieldText(string ExpectDateText, string ExpectTimeText)
        {
            ScrollToElement(_Due_DateField);

            string fieldText = GetElementText(_Due_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            fieldText = GetElementText(_Due_TimeField);
            Assert.AreEqual(ExpectTimeText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateStatusFieldText(string ExpectText)
        {
            ScrollToElement(_Status_Field);
            string fieldText = GetElementText(_Status_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        
        public HealthAppointmentTaskRecordPage ValidateOutcomeFieldText(string ExpectText)
        {
            ScrollToElement(_Outcome_Field);
            string fieldText = GetElementText(_Outcome_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public HealthAppointmentTaskRecordPage ValidateCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_Category_Field);
            string fieldText = GetElementText(_Category_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateSubCategoryFieldText(string ExpectText)
        {
            ScrollToElement(_SubCategory_Field);
            string fieldText = GetElementText(_SubCategory_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidatePriorityFieldText(string ExpectText)
        {
            ScrollToElement(_priority_Field);
            string fieldText = GetElementText(_priority_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateReasonFieldText(string ExpectText)
        {
            ScrollToElement(_reason_Field);
            string fieldText = GetElementText(_reason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public HealthAppointmentTaskRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public HealthAppointmentTaskRecordPage InsertSubject(string ValueToInsert)
        {
            ScrollToElement(_subject_Field);
            this.EnterText(_subject_Field, ValueToInsert);

            return this;
        }

        public HealthAppointmentTaskRecordPage InsertDescription(string ValueToInsert)
        {
            ScrollToElement(_description_Field);
            this.EnterText(_description_Field, ValueToInsert);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapResponsibleTeamRemoveButton()
        {
            ScrollToElement(_ResponsibleTeam_Removebutton);
            Tap(_ResponsibleTeam_Removebutton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapResponsibleTeamLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_LookupButton);
            Tap(_ResponsibleTeam_LookupButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapReasonRemoveButton()
        {
            ScrollToElement(_reason_RemoveButton);
            Tap(_reason_RemoveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapReasonLookupButton()
        {
            ScrollToElement(_reason_LookupButton);
            Tap(_reason_LookupButton);
            
            return this;
        }

        public HealthAppointmentTaskRecordPage TapPriorityRemoveButton()
        {
            ScrollToElement(_priority_RemoveButton);
            Tap(_priority_RemoveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapPriorityLookupButton()
        {
            ScrollToElement(_priority_LookupButton);
            Tap(_priority_LookupButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage InsertDueDate(string DateValue, string TimeValue)
        {
            ScrollToElement(_Due_DateField);
            this.EnterText(_Due_DateField, DateValue);
            this.EnterText(_Due_TimeField, TimeValue);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapOutcomeRemoveButton()
        {
            ScrollToElement(_Outcome_RemoveButton);
            Tap(_Outcome_RemoveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapOutcomeLookupButton()
        {
            ScrollToElement(_Outcome_LookupButton);
            Tap(_Outcome_LookupButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapStatusPicklist()
        {
            ScrollToElementWithWidthAndHeight(_Status_Field);
            TapOnElementWithWidthAndHeight(_Status_Field);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapCategoryRemoveButton()
        {
            ScrollToElement(_Category_RemoveButton);
            Tap(_Category_RemoveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapCategoryLookupButton()
        {
            ScrollToElement(_Category_LookupButton);
            Tap(_Category_LookupButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapSubCategoryRemoveButton()
        {
            ScrollToElement(_SubCategory_RemoveButton);
            Tap(_SubCategory_RemoveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapSubCategoryLookupButton()
        {
            ScrollToElement(_SubCategory_LookupButton);
            Tap(_SubCategory_LookupButton);

            return this;
        }





        public HealthAppointmentTaskRecordPage ValidateResponsibleTeamRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ResponsibleTeam_Removebutton);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_Removebutton));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_Removebutton);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_Removebutton));
            }

            return this;
        }

        public HealthAppointmentTaskRecordPage ValidateReasonRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidatePriorityRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateOutcomeRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateCategoryRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateSubCategoryRemoveButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateResponsibleTeamLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateReasonLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidatePriorityLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateOutcomeLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateCategoryLookupButtonVisible(bool ExpectButtonVisible)
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

        public HealthAppointmentTaskRecordPage ValidateSubCategoryLookupButtonVisible(bool ExpectButtonVisible)
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

        





        public HealthAppointmentTaskRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public HealthAppointmentTaskRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public HealthAppointmentTaskRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}

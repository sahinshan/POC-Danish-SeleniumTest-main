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
    public class PersonAppointmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("appointment_SpeechStart");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("appointment_SpeechStop");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("appointment_Save");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("appointment_SaveAndBack");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _subject_FieldTitle = e => e.Marked("Subject");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _StartTime_FieldTitle = e => e.Marked("Start Time");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _EndTime_FieldTitle = e => e.Marked("End Time");
        readonly Func<AppQuery, AppQuery> _AppointmentType_FieldTitle = e => e.Marked("Appointment Type");
        readonly Func<AppQuery, AppQuery> _Case_FieldTitle = e => e.Marked("Case");
        readonly Func<AppQuery, AppQuery> _Status_FieldTitle = e => e.Marked("Status");
        readonly Func<AppQuery, AppQuery> _Outcome_FieldTitle = e => e.Marked("Outcome");

        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _Location_FieldTitle = e => e.Marked("Location");
        readonly Func<AppQuery, AppQuery> _Required_FieldTitle = e => e.Marked("Required");
        readonly Func<AppQuery, AppQuery> _Optional_FieldTitle = e => e.Marked("Optional");
        readonly Func<AppQuery, AppQuery> _MeetingNotes_FieldTitle = e => e.Marked("Meeting Notes");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _subject_Field = e => e.Marked("Field_26d03b08eca7e61180d30050560502cc");
        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_de70f29b361ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _StartTime_Field = e => e.Marked("Field_2fd67ba5361ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_f7f8fead361ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _EndTime_Field = e => e.Marked("Field_8b0d12b6361ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _AppointmentType_Field = e => e.Marked("Field_3d9bea31361ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Case_Field = e => e.Marked("Field_71d4a1977178e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _Status_Field = e => e.Marked("Field_8ce72ac91ec2e61180d40050560502cc");
        readonly Func<AppQuery, AppQuery> _Outcome_Field = e => e.Marked("90bbc9c21ec2e61180d40050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Outcome_RemoveButton = e => e.Marked("90bbc9c21ec2e61180d40050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupButton = e => e.Marked("90bbc9c21ec2e61180d40050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_a39fa4b47178e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _Location_Field = e => e.Marked("Field_83deab471cc2e61180d40050560502cc");
        Func<AppQuery, AppQuery> _RequiredEntryValue_Field(string recordID) => e => e.Marked(recordID + "_LookupEntry");
        Func<AppQuery, AppQuery> _OptionalEntryValue_Field(string recordID) => e => e.Marked(recordID + "_LookupEntry");

        readonly Func<AppQuery, AppQuery> _MeetingNotesWarningMessage_Field = e => e.Marked("The following text was created using a rich text editor and cannot be edited on this device. Click the expand icon to view text in a larger window.");
        Func<AppQuery, AppWebQuery> _MeetingNotesRichTextEditorText_Field(string ExpectedText) => e => e.XPath("//*[text()='" + ExpectedText + "']");

        readonly Func<AppQuery, AppQuery> __MeetingNotes_Field = e => e.Marked("Field_b295ecbb1ec2e61180d40050560502cc");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public PersonAppointmentRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonAppointmentRecordPage WaitForPersonAppointmentRecordPageToLoad(string PageTitleText)
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



        public PersonAppointmentRecordPage ValidateSubjectFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateStartTimeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StartTime_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_StartTime_FieldTitle));
            }
            else
            {
                TryScrollToElement(_StartTime_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_StartTime_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EndDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EndDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EndDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EndDate_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateEndTimeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EndTime_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EndTime_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EndTime_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EndTime_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateAppointmentTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AppointmentType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AppointmentType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AppointmentType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AppointmentType_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateCaseFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Case_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Case_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Case_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Case_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateStatusFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateOutcomeFieldTitleVisible(bool ExpectFieldVisible)
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



        public PersonAppointmentRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateLocationFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Location_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Location_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Location_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Location_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateRequiredFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Required_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Required_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Required_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Required_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateOptionalFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Optional_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Optional_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Optional_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Optional_FieldTitle));
            }

            return this;
        }

        public PersonAppointmentRecordPage ValidateMeetingNotesFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_MeetingNotes_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MeetingNotes_FieldTitle));
            }
            else
            {
                TryScrollToElement(_MeetingNotes_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MeetingNotes_FieldTitle));
            }

            return this;
        }




        public PersonAppointmentRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public PersonAppointmentRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public PersonAppointmentRecordPage ValidateSubjectFieldText(string ExpectText)
        {
            ScrollToElement(_subject_Field);
            string fieldText = GetElementText(_subject_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateStartTimeRichFieldText(string ExpectText)
        {
            ScrollToElement(_StartTime_Field);
            string fieldText = GetElementText(_StartTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateEndTimeFieldText(string ExpectText)
        {
            ScrollToElement(_EndTime_Field);
            string fieldText = GetElementText(_EndTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateAppointmentTypeFieldText(string ExpectText)
        {
            ScrollToElement(_AppointmentType_Field);
            string fieldText = GetElementText(_AppointmentType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateCaseFieldText(string ExpectText)
        {
            ScrollToElement(_Case_Field);
            string fieldText = GetElementText(_Case_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateStatusFieldText(string ExpectText)
        {
            ScrollToElement(_Status_Field);
            string fieldText = GetElementText(_Status_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateOutcomeFieldText(string ExpectText)
        {
            ScrollToElement(_Outcome_Field);
            string fieldText = GetElementText(_Outcome_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonAppointmentRecordPage ValidatePersonFieldText(string ExpectText)
        {
            ScrollToElement(_Person_Field);
            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateLocationFieldText(string ExpectText)
        {
            ScrollToElement(_Location_Field);
            string fieldText = GetElementText(_Location_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateRequiredFieldEntryText(string recordID, string ExpectText)
        {
            ScrollToElement(_RequiredEntryValue_Field(recordID));
            string fieldText = GetElementText(_RequiredEntryValue_Field(recordID));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateOptionalFieldEntryText(string recordID, string ExpectText)
        {
            ScrollToElement(_OptionalEntryValue_Field(recordID));
            string fieldText = GetElementText(_OptionalEntryValue_Field(recordID));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateMeetingNotesRichTeaxtFieldText(string ExpectText)
        {
            string fieldText = GetWebElementText(_MeetingNotesRichTextEditorText_Field(ExpectText));
            Assert.AreEqual(ExpectText, fieldText);


            return this;
        }

        public PersonAppointmentRecordPage ValidateMeetingNotesFieldText(string ExpectText)
        {
            ScrollToElement(__MeetingNotes_Field);
            string fieldText = GetElementText(__MeetingNotes_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonAppointmentRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAppointmentRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        public PersonAppointmentRecordPage TapOnPersonField()
        {

            ScrollToElement(_Person_Field);
            Tap(_Person_Field);

            return this;
        }

        public PersonAppointmentRecordPage TapStatusField()
        {
            ScrollToElement(_Status_Field);
            Tap(_Status_Field);

            return this;
        }

        public PersonAppointmentRecordPage TapOnOutcomeField()
        {

            ScrollToElement(_Outcome_Field);
            Tap(_Outcome_Field);

            return this;
        }

        public PersonAppointmentRecordPage TapOutcomeRemoveButtonField()
        {
            ScrollToElement(_Outcome_RemoveButton);
            Tap(_Outcome_RemoveButton);

            return this;
        }

        public PersonAppointmentRecordPage TapOutcomeLookupButtonField()
        {
            ScrollToElement(_Outcome_LookupButton);
            Tap(_Outcome_LookupButton);

            return this;
        }

        public PersonAppointmentRecordPage TapRequiredEntryField(string recordID)
        {
            ScrollToElement(_RequiredEntryValue_Field(recordID));
            Tap(_RequiredEntryValue_Field(recordID));

            return this;
        }

        public PersonAppointmentRecordPage TapOptionalEntryField(string recordID)
        {
            ScrollToElement(_OptionalEntryValue_Field(recordID));
            Tap(_OptionalEntryValue_Field(recordID));

            return this;
        }

        public PersonAppointmentRecordPage InsertMeetingNotes(string TextToInsert)
        {
            ScrollToElement(__MeetingNotes_Field);
            EnterText(__MeetingNotes_Field, TextToInsert);

            return this;
        }


        public PersonAppointmentRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonAppointmentRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonAppointmentRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonAppointmentRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

    }
}

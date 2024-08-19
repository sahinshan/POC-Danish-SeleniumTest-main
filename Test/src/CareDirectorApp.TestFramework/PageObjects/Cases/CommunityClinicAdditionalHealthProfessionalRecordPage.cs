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
    public class CommunityClinicAdditionalHealthProfessionalRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("healthappointmentadditionalprofessional_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("healthappointmentadditionalprofessional_TextToSpeechStopButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("healthappointmentadditionalprofessional_Save");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("healthappointmentadditionalprofessional_SaveAndClose");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _CommunityClinicAppointment_FieldTitle = e => e.Marked("Community/Clinic Appointment");
        readonly Func<AppQuery, AppQuery> _Case_FieldTitle = e => e.Marked("Case");
        readonly Func<AppQuery, AppQuery> _ProfessionalRemainingForFullDuration_FieldTitle = e => e.Marked("Professional remaining for full duration?");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _AddTravelTimeToAppointment_FieldTitle = e => e.Marked("Add Travel Time to Appointment?");
        readonly Func<AppQuery, AppQuery> _TravelTimeInMinutes_FieldTitle = e => e.Marked("Travel Time (in minutes)");

        readonly Func<AppQuery, AppQuery> _Professional_FieldTitle = e => e.Marked("Professional");
        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _StartTime_FieldTitle = e => e.Marked("Start Time");
        readonly Func<AppQuery, AppQuery> _EndTime_FieldTitle = e => e.Marked("End Time");
        readonly Func<AppQuery, AppQuery> _ReturnToBaseAfterAppointment_FieldTitle = e => e.Marked("Return to Base After Appointment?");
        readonly Func<AppQuery, AppQuery> _TravelTimeBackToBaseInMinutes_FieldTitle = e => e.Marked("Travel Time Back to Base (in minutes)");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _CommunityClinicAppointment_Field = e => e.Marked("Field_fdb4ac4cdf82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _Case_Field = e => e.Marked("Field_6f58797bdf82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ProfessionalRemainingForFullDuration_Field = e => e.Marked("Field_c7b0a381df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_ae7fc487df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartDate_EditField = e => e.Marked("Field_ae7fc487df82e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_b8d26a8edf82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _EndDate_EditField = e => e.Marked("Field_b8d26a8edf82e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _AddTravelTimeToAppointment_Field = e => e.Marked("Field_a2cb5bb7df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeInMinutes_Field = e => e.Marked("Field_a18b8bc7df82e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _Professional_Field = e => e.Marked("Field_dad6fb71df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _Professional_LookupEntry = e => e.Marked("dad6fb71df82e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Professional_RemoveValue = e => e.Marked("dad6fb71df82e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Professional_OpenLookup = e => e.Marked("dad6fb71df82e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_9bef7d9804c2e61180d40050560502cc");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_bf21289edf82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupEntry = e => e.Marked("bf21289edf82e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveValue = e => e.Marked("bf21289edf82e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_OpenLookup = e => e.Marked("bf21289edf82e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _StartTime_Field = e => e.Marked("Field_64516aa4df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartTime_EditField = e => e.Marked("Field_64516aa4df82e911a2c50050569231cf_Time");
        readonly Func<AppQuery, AppQuery> _EndTime_Field = e => e.Marked("Field_68516aa4df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _EndTime_EditField = e => e.Marked("Field_68516aa4df82e911a2c50050569231cf_Time");
        readonly Func<AppQuery, AppQuery> _ReturnToBaseAfterAppointment_Field = e => e.Marked("Field_7f525dc0df82e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeBackToBaseInMinutes_Field = e => e.Marked("Field_34785de8df82e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public CommunityClinicAdditionalHealthProfessionalRecordPage(IApp app)
        {
            _app = app;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad(string PageTitleText)
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

        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            //WaitForElement(_pageTitle(PageTitleText));

            //WaitForElement(_topBannerArea);

            return this;
        }



        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCommunityClinicAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CommunityClinicAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CommunityClinicAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CommunityClinicAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CommunityClinicAppointment_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCaseFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalRemainingForFullDurationFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ProfessionalRemainingForFullDuration_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ProfessionalRemainingForFullDuration_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ProfessionalRemainingForFullDuration_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ProfessionalRemainingForFullDuration_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateAddTravelTimeToAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AddTravelTimeToAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AddTravelTimeToAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AddTravelTimeToAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AddTravelTimeToAppointment_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateTravelTimeInMinutesFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TravelTimeInMinutes_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_TravelTimeInMinutes_FieldTitle));
            }
            else
            {
                TryScrollToElement(_TravelTimeInMinutes_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_TravelTimeInMinutes_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Professional_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Professional_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Professional_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Professional_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartTimeFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndTimeFieldTitleVisible(bool ExpectFieldVisible)
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
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateReturnToBaseAfterAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ReturnToBaseAfterAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ReturnToBaseAfterAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ReturnToBaseAfterAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ReturnToBaseAfterAppointment_FieldTitle));
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TravelTimeBackToBaseInMinutes_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_TravelTimeBackToBaseInMinutes_FieldTitle));
            }
            else
            {
                TryScrollToElement(_TravelTimeBackToBaseInMinutes_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_TravelTimeBackToBaseInMinutes_FieldTitle));
            }

            return this;
        }




        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCommunityClinicAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_CommunityClinicAppointment_Field);
            string fieldText = GetElementText(_CommunityClinicAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCaseFieldText(string ExpectText)
        {
            ScrollToElement(_Case_Field);
            string fieldText = GetElementText(_Case_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalRemainingForFullDurationFieldText(string ExpectText)
        {
            ScrollToElement(_ProfessionalRemainingForFullDuration_Field);
            string fieldText = GetElementText(_ProfessionalRemainingForFullDuration_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateAddTravelTimeToAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_AddTravelTimeToAppointment_Field);
            string fieldText = GetElementText(_AddTravelTimeToAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateTravelTimeInMinutesFieldText(string ExpectText)
        {
            ScrollToElement(_TravelTimeInMinutes_Field);
            string fieldText = GetElementText(_TravelTimeInMinutes_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalFieldText(string ExpectText)
        {
            ScrollToElement(_Professional_Field);
            string fieldText = GetElementText(_Professional_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidatePersonFieldText(string ExpectText)
        {
            ScrollToElement(_Person_Field);
            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartTimeFieldText(string ExpectText)
        {
            ScrollToElement(_StartTime_Field);
            string fieldText = GetElementText(_StartTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndTimeFieldText(string ExpectText)
        {
            ScrollToElement(_EndTime_Field);
            string fieldText = GetElementText(_EndTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateReturnToBaseAfterAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_ReturnToBaseAfterAppointment_Field);
            string fieldText = GetElementText(_ReturnToBaseAfterAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateTravelTimeBackToBaseInMinutesFieldText(string ExpectText)
        {
            ScrollToElement(_TravelTimeBackToBaseInMinutes_Field);
            string fieldText = GetElementText(_TravelTimeBackToBaseInMinutes_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertStartDate(string ValueToInsert)
        {
            ScrollToElement(_StartDate_EditField);
            this.EnterText(_StartDate_EditField, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertEndDate(string ValueToInsert)
        {
            ScrollToElement(_EndDate_EditField);
            this.EnterText(_EndDate_EditField, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertStartTime(string ValueToInsert)
        {
            ScrollToElement(_StartTime_EditField);
            this.EnterText(_StartTime_EditField, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertEndTime(string ValueToInsert)
        {
            ScrollToElement(_EndTime_EditField);
            this.EnterText(_EndTime_EditField, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapProfessionalOpenLookupButton()
        {
            ScrollToElement(_Professional_OpenLookup);
            Tap(_Professional_OpenLookup);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapResponsibleTeamOpenLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_OpenLookup);
            Tap(_ResponsibleTeam_OpenLookup);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapAddTravelTimeToAppointmentField()
        {
            ScrollToElement(_AddTravelTimeToAppointment_Field);
            Tap(_AddTravelTimeToAppointment_Field);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapReturnToBaseAfterAppointmentField()
        {
            ScrollToElement(_ReturnToBaseAfterAppointment_Field);
            Tap(_ReturnToBaseAfterAppointment_Field);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertTravelTimeInMinutes(string ValueToInsert)
        {
            ScrollToElement(_TravelTimeInMinutes_Field);
            this.EnterText(_TravelTimeInMinutes_Field, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertTravelTimeBackToBaseInMinutes(string ValueToInsert)
        {
            ScrollToElement(_TravelTimeBackToBaseInMinutes_Field);
            this.EnterText(_TravelTimeBackToBaseInMinutes_Field, ValueToInsert);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapProfessionalRemainingForFullDurationField()
        {
            ScrollToElement(_ProfessionalRemainingForFullDuration_Field);
            Tap(_ProfessionalRemainingForFullDuration_Field);

            return this;
        }
        





        public CommunityClinicAdditionalHealthProfessionalRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

    }
}

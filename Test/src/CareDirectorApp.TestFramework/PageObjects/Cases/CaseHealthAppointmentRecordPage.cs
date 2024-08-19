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
    public class CaseHealthAppointmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("healthappointment_StartSpeech");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("healthappointment_StopSpeech");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("healthappointment_Save");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("healthappointment_SaveAndBack");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");

        readonly Func<AppQuery, AppQuery> _RelatedItemsButton = e => e.Marked("RelatedItemsButton");
        
        readonly Func<AppQuery, AppQuery> _RelatedItems_LeftMenuSubArea = e => e.Marked("RelatedItems_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _Activities_LeftMenuSubArea = e => e.Marked("Activities_CategoryLabel");
        
        readonly Func<AppQuery, AppQuery> _AdditionalHealthProfessionals_Link = e => e.Marked("RelatedItems_Item_AdditionalHealthProfessionals");
        readonly Func<AppQuery, AppQuery> _Tasks_Link = e => e.Marked("Activities_Item_Tasks");
        readonly Func<AppQuery, AppQuery> _CaseNotes_Link = e => e.Marked("Activities_Item_CaseNotes");
        

        #region Fields titles

        readonly Func<AppQuery, AppQuery> _AppointmentReason_FieldTitle = e => e.Marked("Appointment Reason");
        readonly Func<AppQuery, AppQuery> _CommunityClinicTeam_FieldTitle = e => e.Marked("Community/Clinic Team");
        readonly Func<AppQuery, AppQuery> _RelatedCase_FieldTitle = e => e.Marked("Related Case");
        readonly Func<AppQuery, AppQuery> _ContactType_FieldTitle = e => e.Marked("Contact Type");
        readonly Func<AppQuery, AppQuery> _TranslatorRequired_FieldTitle = e => e.Marked("Translator Required");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_FieldTitle = e => e.Marked("Responsible User");
        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _PreferredLanguage_FieldTitle = e => e.Marked("Preferred Language");

        readonly Func<AppQuery, AppQuery> _AppointmentInformation_FieldTitle = e => e.Marked("Appointment Information (such as family and friends who attended with client)");
        readonly Func<AppQuery, AppQuery> _AllowConcurrentAppointment_FieldTitle = e => e.Marked("Allow Concurrent Appointment");
        readonly Func<AppQuery, AppQuery> _BypassRestrictions_FieldTitle = e => e.Marked("Bypass Restrictions");
        readonly Func<AppQuery, AppQuery> _OutOfHours_FieldTitle = e => e.Marked("Out of Hours");

        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _StartTime_FieldTitle = e => e.Marked("Start Time");
        readonly Func<AppQuery, AppQuery> _EndTime_FieldTitle = e => e.Marked("End Time");

        readonly Func<AppQuery, AppQuery> _LocationType_FieldTitle = e => e.Marked("Location Type");
        readonly Func<AppQuery, AppQuery> _AdditionalProfessionalRequired_FieldTitle = e => e.Marked("Additional Professional Required?");
        readonly Func<AppQuery, AppQuery> _LeadProfessional_FieldTitle = e => e.Marked("Lead Professional");
        readonly Func<AppQuery, AppQuery> _ClientsUsualPlaceOfResidence_FieldTitle = e => e.Marked("Clients Usual Place of residence");
        
        readonly Func<AppQuery, AppQuery> _CancelAppointment_FieldTitle = e => e.Marked("Cancel Appointment");
        readonly Func<AppQuery, AppQuery> _Outcome_FieldTitle = e => e.Marked("Outcome");
        readonly Func<AppQuery, AppQuery> _WhoLedTheAppointment_FieldTitle = e => e.Marked("Who Led the Appointment");

        readonly Func<AppQuery, AppQuery> _Reason_FieldTitle = e => e.Marked("Reason");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointment_FieldTitle = e => e.Marked("Who Cancelled The Appointment");
        readonly Func<AppQuery, AppQuery> _DateUnavailableFrom_FieldTitle = e => e.Marked("Date Unavailable From");
        readonly Func<AppQuery, AppQuery> _AbsenceReason_FieldTitle = e => e.Marked("Absence Reason");
        readonly Func<AppQuery, AppQuery> _DNACNAWNB_FieldTitle = e => e.Marked("DNA/CNA/WNB");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointmentFreeText_FieldTitle = e => e.Marked("Who Cancelled the Appointment? (Free text)");
        readonly Func<AppQuery, AppQuery> _DateAvailableFrom_FieldTitle = e => e.Marked("Date Available From");
        readonly Func<AppQuery, AppQuery> _CNANotificationDate_FieldTitle = e => e.Marked("CNA Notification Date");

        readonly Func<AppQuery, AppQuery> _AddTravelTimeToAppointment_FieldTitle = e => e.Marked("Add Travel Time to Appointment?");
        readonly Func<AppQuery, AppQuery> _ReturnToBaseAfterAppointment_FieldTitle = e => e.Marked("Return to Base After Appointment?");
        readonly Func<AppQuery, AppQuery> _TravelTimeInMinutes_FieldTitle = e => e.Marked("Travel Time (in Minutes)");
        readonly Func<AppQuery, AppQuery> _TravelTimeBackToBaseInMinutes_FieldTitle = e => e.Marked("Travel Time Back to Base (in Minutes)");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _AppointmentReason_Field = e => e.Marked("Field_da5b6579371ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _AppointmentReason_RemoveButton = e => e.Marked("da5b6579371ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _AppointmentReason_LookupButton = e => e.Marked("da5b6579371ae91180dc0050560502cc_OpenLookup");
        readonly Func<AppQuery, AppQuery> _CommunityClinicTeam_Field = e => e.Marked("e8509eb62181e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _CommunityClinicTeam_RemoveButton = e => e.Marked("e8509eb62181e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _CommunityClinicTeam_LookupButotn = e => e.Marked("e8509eb62181e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _RelatedCase_Field = e => e.Marked("Field_dfe031d8371ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _ContactType_Field = e => e.Marked("Field_79a7d8e52181e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ContactType_LookupEntry = e => e.Marked("79a7d8e52181e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ContactType_RemoveButton = e => e.Marked("79a7d8e52181e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ContactType_LookupButton = e => e.Marked("79a7d8e52181e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _TranslatorRequired_Field = e => e.Marked("Field_d163ecbce206ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_198d88762181e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_ReadonlyField = e => e.Marked("Field_198d88762181e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveButton = e => e.Marked("198d88762181e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("198d88762181e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_Field = e => e.Marked("Field_533c57982181e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_LookupEntry = e => e.Marked("533c57982181e911a2c50050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_RemoveButton = e => e.Marked("533c57982181e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleUser_LookupButton = e => e.Marked("533c57982181e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_79c000b1f8c1e61180d40050560502cc");
        readonly Func<AppQuery, AppQuery> _Person_ReadonlyField = e => e.Marked("Field_79c000b1f8c1e61180d40050560502cc").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _PreferredLanguage_Field = e => e.Marked("Field_aa990ff3e206ea11a2c90050569231cf");

        readonly Func<AppQuery, AppQuery> _AppointmentInformation_Field = e => e.Marked("Field_c20844392281e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_be98a84c2381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartDate_EditableField = e => e.Marked("Field_be98a84c2381e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_fb064e542381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _EndDate_EditableField = e => e.Marked("Field_fb064e542381e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _StartTime_Field = e => e.Marked("Field_e048685c2381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartTime_EditableField = e => e.Marked("Field_e048685c2381e911a2c50050569231cf_Time");
        readonly Func<AppQuery, AppQuery> _EndTime_Field = e => e.Marked("Field_77c723662381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _EndTime_EditableField = e => e.Marked("Field_77c723662381e911a2c50050569231cf_Time");

        readonly Func<AppQuery, AppQuery> _LocationType_Field = e => e.Marked("Field_775b147a2381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _LocationType_RemoveButton = e => e.Marked("775b147a2381e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _LocationType_LookupButton = e => e.Marked("775b147a2381e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _AdditionalProfessionalRequired_ScrollableField = e => e.All().Marked("Field_cc514f062481e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _AdditionalProfessionalRequired_Field = e => e.Marked("Field_cc514f062481e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _LeadProfessional_Field = e => e.Marked("Field_3a0abcf72381e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _LeadProfessional_RemoveButton = e => e.Marked("3a0abcf72381e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _LeadProfessional_LookupButton = e => e.Marked("3a0abcf72381e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _ClientsUsualPlaceOfResidence_Field = e => e.Marked("Field_84d6f9122481e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _CancelAppointment_Field = e => e.Marked("Field_ae1d3adff9c1e61180d40050560502cc");
        readonly Func<AppQuery, AppQuery> _Outcome_Field = e => e.Marked("Field_c5d944cb381ae91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupEntry = e => e.Marked("c5d944cb381ae91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Outcome_RemoveButton = e => e.Marked("c5d944cb381ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Outcome_LookupButton = e => e.Marked("c5d944cb381ae91180dc0050560502cc_OpenLookup");
        readonly Func<AppQuery, AppQuery> _WhoLedTheAppointment_Field = e => e.Marked("Field_571ecaf6f9c1e61180d40050560502cc");
        readonly Func<AppQuery, AppQuery> _WhoLedTheAppointment_LookupEntry = e => e.Marked("571ecaf6f9c1e61180d40050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _WhoLedTheAppointment_RemoveButton = e => e.Marked("571ecaf6f9c1e61180d40050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _WhoLedTheAppointment_LookupButton = e => e.Marked("571ecaf6f9c1e61180d40050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Reason_Field = e => e.Marked("Field_5a196cb52581e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointment_Field = e => e.Marked("Field_748bb4bc2581e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointment_RemoveButton = e => e.Marked("748bb4bc2581e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointment_LookupButton = e => e.Marked("748bb4bc2581e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _DateUnavailableFrom_Field = e => e.Marked("Field_d753f3ee2581e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _AbsenceReason_Field = e => e.Marked("Field_06fa43d22581e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _AbsenceReason_RemoveButton = e => e.Marked("06fa43d22581e911a2c50050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _AbsenceReason_LookupButton = e => e.Marked("06fa43d22581e911a2c50050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _DNACNAWNB_Field = e => e.Marked("Field_ed5d1cd92581e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _WhoCancelledTheAppointmentFreeText_Field = e => e.Marked("Field_7d9a19c42581e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _DateAvailableFrom_Field = e => e.Marked("Field_90a523f92581e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _CNANotificationDate_Field = e => e.Marked("Field_94a523f92581e911a2c50050569231cf_Date");

        readonly Func<AppQuery, AppQuery> _AddTravelTimeToAppointment_ScrollableField = e => e.All().Marked("Field_0b331782e009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _AddTravelTimeToAppointment_Field = e => e.Marked("Field_0b331782e009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _ReturnToBaseAfterAppointment_ScrollableField = e => e.All().Marked("Field_02f7978ae009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _ReturnToBaseAfterAppointment_Field = e => e.Marked("Field_02f7978ae009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeInMinutes_ScrollableField = e => e.All().Marked("Field_47d16ca5e009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeInMinutes_Field = e => e.Marked("Field_47d16ca5e009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeBackToBaseInMinutes_ScrollableField = e => e.All().Marked("Field_c2b3aeaee009ea11a2c90050569231cf");
        readonly Func<AppQuery, AppQuery> _TravelTimeBackToBaseInMinutes_Field = e => e.Marked("Field_c2b3aeaee009ea11a2c90050569231cf");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public CaseHealthAppointmentRecordPage(IApp app)
        {
            _app = app;
        }


        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPageToLoad(string PageTitleText)
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





        public CaseHealthAppointmentRecordPage ValidateAppointmentReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AppointmentReason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AppointmentReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AppointmentReason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AppointmentReason_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCommunityClinicTeamFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CommunityClinicTeam_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CommunityClinicTeam_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CommunityClinicTeam_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CommunityClinicTeam_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateRelatedCaseFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_RelatedCase_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_RelatedCase_FieldTitle));
            }
            else
            {
                TryScrollToElement(_RelatedCase_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_RelatedCase_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateContactTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ContactType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ContactType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ContactType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ContactType_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateTranslatorRequiredFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TranslatorRequired_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_TranslatorRequired_FieldTitle));
            }
            else
            {
                TryScrollToElement(_TranslatorRequired_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_TranslatorRequired_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateResponsibleUserFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidatePreferredLanguageFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PreferredLanguage_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PreferredLanguage_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PreferredLanguage_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PreferredLanguage_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAppointmentInformationFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AppointmentInformation_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AppointmentInformation_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AppointmentInformation_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AppointmentInformation_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAllowConcurrentAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AllowConcurrentAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AllowConcurrentAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AllowConcurrentAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AllowConcurrentAppointment_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateBypassRestrictionsFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_BypassRestrictions_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_BypassRestrictions_FieldTitle));
            }
            else
            {
                TryScrollToElement(_BypassRestrictions_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_BypassRestrictions_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateOutOfHoursFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_OutOfHours_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_OutOfHours_FieldTitle));
            }
            else
            {
                TryScrollToElement(_OutOfHours_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_OutOfHours_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateStartTimeFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateEndTimeFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateLocationTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_LocationType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_LocationType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_LocationType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_LocationType_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAdditionalProfessionalRequiredFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AdditionalProfessionalRequired_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AdditionalProfessionalRequired_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AdditionalProfessionalRequired_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AdditionalProfessionalRequired_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLeadProfessionalFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_LeadProfessional_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_LeadProfessional_FieldTitle));
            }
            else
            {
                TryScrollToElement(_LeadProfessional_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_LeadProfessional_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateClientsUsualPlaceOfResidenceFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ClientsUsualPlaceOfResidence_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ClientsUsualPlaceOfResidence_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ClientsUsualPlaceOfResidence_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ClientsUsualPlaceOfResidence_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCancelAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CancelAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CancelAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CancelAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CancelAppointment_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateOutcomeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Outcome_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Outcome_Field);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoLedTheAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WhoLedTheAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_WhoLedTheAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_WhoLedTheAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_WhoLedTheAppointment_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Reason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Reason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Reason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Reason_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WhoCancelledTheAppointment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_WhoCancelledTheAppointment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_WhoCancelledTheAppointment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_WhoCancelledTheAppointment_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDateUnavailableFromFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateUnavailableFrom_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DateUnavailableFrom_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DateUnavailableFrom_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DateUnavailableFrom_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAbsenceReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AbsenceReason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AbsenceReason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDNACNAWNBFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DNACNAWNB_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DNACNAWNB_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DNACNAWNB_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DNACNAWNB_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointmentFreeTextFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WhoCancelledTheAppointmentFreeText_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_WhoCancelledTheAppointmentFreeText_FieldTitle));
            }
            else
            {
                TryScrollToElement(_WhoCancelledTheAppointmentFreeText_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_WhoCancelledTheAppointmentFreeText_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDateAvailableFromFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateAvailableFrom_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DateAvailableFrom_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DateAvailableFrom_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DateAvailableFrom_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCNANotificationDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CNANotificationDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CNANotificationDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CNANotificationDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CNANotificationDate_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAddTravelTimeToAppointmentFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateReturnToBaseAfterAppointmentFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateTravelTimeInMinutesFieldTitleVisible(bool ExpectFieldVisible)
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
        public CaseHealthAppointmentRecordPage ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(bool ExpectFieldVisible)
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







        public CaseHealthAppointmentRecordPage ValidateDateUnavailableFromFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateUnavailableFrom_Field);
                Assert.IsTrue(CheckIfElementVisible(_DateUnavailableFrom_Field));
            }
            else
            {
                TryScrollToElement(_DateUnavailableFrom_Field);
                Assert.IsFalse(CheckIfElementVisible(_DateUnavailableFrom_Field));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDateAvailableFromFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateAvailableFrom_Field);
                Assert.IsTrue(CheckIfElementVisible(_DateAvailableFrom_Field));
            }
            else
            {
                TryScrollToElement(_DateAvailableFrom_Field);
                Assert.IsFalse(CheckIfElementVisible(_DateAvailableFrom_Field));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCNANotificationDateFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CNANotificationDate_Field);
                Assert.IsTrue(CheckIfElementVisible(_CNANotificationDate_Field));
            }
            else
            {
                TryScrollToElement(_CNANotificationDate_Field);
                Assert.IsFalse(CheckIfElementVisible(_CNANotificationDate_Field));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAbsenceReasonElementsVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AbsenceReason_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_LookupButton));
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_RemoveButton));
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AbsenceReason_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_LookupButton));
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_RemoveButton));
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_FieldTitle));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDNACNAWNBElementsVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DNACNAWNB_Field);
                Assert.IsTrue(CheckIfElementVisible(_DNACNAWNB_Field));
                Assert.IsTrue(CheckIfElementVisible(_DNACNAWNB_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DNACNAWNB_Field);
                Assert.IsFalse(CheckIfElementVisible(_DNACNAWNB_Field));
                Assert.IsFalse(CheckIfElementVisible(_DNACNAWNB_FieldTitle));
            }

            return this;
        }





        public CaseHealthAppointmentRecordPage ValidateAppointmentReasonFieldText(string ExpectText)
        {
            ScrollToElement(_AppointmentReason_Field);
            string fieldText = GetElementText(_AppointmentReason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCommunityClinicTeamFieldText(string ExpectText)
        {
            ScrollToElement(_CommunityClinicTeam_Field);
            string fieldText = GetElementText(_CommunityClinicTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateRelatedCaseFieldText(string ExpectText)
        {
            ScrollToElement(_RelatedCase_Field);
            string fieldText = GetElementText(_RelatedCase_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateContactTypeFieldText(string ExpectText)
        {
            ScrollToElement(_ContactType_Field);
            string fieldText = GetElementText(_ContactType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateContactTypeLookupEntryText(string ExpectText)
        {
            ScrollToElement(_ContactType_LookupEntry);
            string fieldText = GetElementText(_ContactType_LookupEntry);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateTranslatorRequiredFieldText(string ExpectText)
        {
            ScrollToElement(_TranslatorRequired_Field);
            string fieldText = GetElementText(_TranslatorRequired_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleReadonlyTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_ReadonlyField);
            string fieldText = GetElementText(_ResponsibleTeam_ReadonlyField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleUserFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_Field);
            string fieldText = GetElementText(_ResponsibleUser_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleUserLookupEntryText(string ExpectText)
        {
            ScrollToElement(_ResponsibleUser_LookupEntry);
            string fieldText = GetElementText(_ResponsibleUser_LookupEntry);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidatePersonFieldText(string ExpectText)
        {
            ScrollToElement(_Person_Field);
            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidatePersonReadonlyFieldText(string ExpectText)
        {
            ScrollToElement(_Person_ReadonlyField);
            string fieldText = GetElementText(_Person_ReadonlyField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidatePreferredLanguageFieldText(string ExpectText)
        {
            ScrollToElement(_PreferredLanguage_Field);
            string fieldText = GetElementText(_PreferredLanguage_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAppointmentInformationFieldText(string ExpectText)
        {
            ScrollToElement(_AppointmentInformation_Field);
            string fieldText = GetElementText(_AppointmentInformation_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateStartTimeFieldText(string ExpectText)
        {
            ScrollToElement(_StartTime_Field);
            string fieldText = GetElementText(_StartTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateEndTimeFieldText(string ExpectText)
        {
            ScrollToElement(_EndTime_Field);
            string fieldText = GetElementText(_EndTime_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLocationTypeFieldText(string ExpectText)
        {
            ScrollToElement(_LocationType_Field);
            string fieldText = GetElementText(_LocationType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAdditionalProfessionalRequiredFieldText(string ExpectText)
        {
            ScrollToElement(_AdditionalProfessionalRequired_ScrollableField);
            string fieldText = GetElementText(_AdditionalProfessionalRequired_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLeadProfessionalFieldText(string ExpectText)
        {
            ScrollToElement(_LeadProfessional_Field);
            string fieldText = GetElementText(_LeadProfessional_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateClientsUsualPlaceOfResidenceFieldText(string ExpectText)
        {
            ScrollToElement(_ClientsUsualPlaceOfResidence_Field);
            string fieldText = GetElementText(_ClientsUsualPlaceOfResidence_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCancelAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_createdOn_Field);
            ScrollToElement(_CancelAppointment_Field);
            string fieldText = GetElementText(_CancelAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateOutcomeFieldText(string ExpectText)
        {
            ScrollToElement(_Outcome_Field);
            string fieldText = GetElementText(_Outcome_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateOutcomeLookupEntryText(string ExpectText)
        {
            ScrollToElement(_Outcome_LookupEntry);
            string fieldText = GetElementText(_Outcome_LookupEntry);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoLedTheAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_WhoLedTheAppointment_Field);
            string fieldText = GetElementText(_WhoLedTheAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoLedTheAppointmentLookupEntryText(string ExpectText)
        {
            ScrollToElement(_WhoLedTheAppointment_LookupEntry);
            string fieldText = GetElementText(_WhoLedTheAppointment_LookupEntry);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateReasonFieldText(string ExpectText)
        {
            ScrollToElement(_Reason_Field);
            string fieldText = GetElementText(_Reason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_WhoCancelledTheAppointment_Field);
            string fieldText = GetElementText(_WhoCancelledTheAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDateUnavailableFromFieldText(string ExpectText)
        {
            ScrollToElement(_DateUnavailableFrom_Field);
            string fieldText = GetElementText(_DateUnavailableFrom_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAbsenceReasonFieldText(string ExpectText)
        {
            ScrollToElement(_AbsenceReason_Field);
            string fieldText = GetElementText(_AbsenceReason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDNACNAWNBFieldText(string ExpectText)
        {
            ScrollToElement(_DNACNAWNB_Field);
            string fieldText = GetElementText(_DNACNAWNB_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointmentFreeTextFieldText(string ExpectText)
        {
            ScrollToElement(_WhoCancelledTheAppointmentFreeText_Field);
            string fieldText = GetElementText(_WhoCancelledTheAppointmentFreeText_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateDateAvailableFromFieldText(string ExpectText)
        {
            ScrollToElement(_DateAvailableFrom_Field);
            string fieldText = GetElementText(_DateAvailableFrom_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCNANotificationDateFieldText(string ExpectText)
        {
            ScrollToElement(_CNANotificationDate_Field);
            string fieldText = GetElementText(_CNANotificationDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAddTravelTimeToAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_AddTravelTimeToAppointment_Field);
            string fieldText = GetElementText(_AddTravelTimeToAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateReturnToBaseAfterAppointmentFieldText(string ExpectText)
        {
            ScrollToElement(_ReturnToBaseAfterAppointment_Field);
            string fieldText = GetElementText(_ReturnToBaseAfterAppointment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateTravelTimeInMinutesFieldText(string ExpectText)
        {
            ScrollToElement(_TravelTimeInMinutes_Field);
            string fieldText = GetElementText(_TravelTimeInMinutes_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateTravelTimeBackToBaseInMinutesFieldText(string ExpectText)
        {
            ScrollToElement(_TravelTimeBackToBaseInMinutes_Field);
            string fieldText = GetElementText(_TravelTimeBackToBaseInMinutes_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public CaseHealthAppointmentRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public CaseHealthAppointmentRecordPage InsertAppointmentInformation(string ValueToInsert)
        {
            ScrollToElement(_AppointmentInformation_Field);
            this.EnterText(_AppointmentInformation_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertStartDate(string ValueToInsert)
        {
            ScrollToElement(_StartDate_EditableField);
            this.EnterText(_StartDate_EditableField, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertEndDate(string ValueToInsert)
        {
            ScrollToElement(_EndDate_EditableField);
            this.EnterText(_EndDate_EditableField, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertStartTime(string ValueToInsert)
        {
            ScrollToElement(_StartTime_EditableField);
            this.EnterText(_StartTime_EditableField, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertEndTime(string ValueToInsert)
        {
            ScrollToElement(_EndTime_EditableField);
            this.EnterText(_EndTime_EditableField, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertTravelTimeInMinutes(string ValueToInsert)
        {
            ScrollToElement(_TravelTimeInMinutes_ScrollableField);
            this.EnterText(_TravelTimeInMinutes_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertTravelTimeBackToBaseInMinutes(string ValueToInsert)
        {
            ScrollToElement(_TravelTimeBackToBaseInMinutes_ScrollableField);
            this.EnterText(_TravelTimeBackToBaseInMinutes_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertDateUnavailableFrom(string ValueToInsert)
        {
            ScrollToElement(_DateUnavailableFrom_Field);
            this.EnterText(_DateUnavailableFrom_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertWhoCancelledTheAppointmentFreeText(string ValueToInsert)
        {
            ScrollToElement(_WhoCancelledTheAppointmentFreeText_Field);
            this.EnterText(_WhoCancelledTheAppointmentFreeText_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertDateAvailableFrom(string ValueToInsert)
        {
            ScrollToElement(_DateAvailableFrom_Field);
            this.EnterText(_DateAvailableFrom_Field, ValueToInsert);

            return this;
        }
        public CaseHealthAppointmentRecordPage InsertCNANotificationDate(string ValueToInsert)
        {
            ScrollToElement(_CNANotificationDate_Field);
            this.EnterText(_CNANotificationDate_Field, ValueToInsert);

            return this;
        }



        public CaseHealthAppointmentRecordPage TapAppointmentReasonRemoveButton()
        {
            ScrollToElement(_AppointmentReason_RemoveButton);
            Tap(_AppointmentReason_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapAppointmentReasonLookupButton()
        {
            ScrollToElement(_AppointmentReason_LookupButton);
            Tap(_AppointmentReason_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapCommunityClinicTeamRemoveButton()
        {
            ScrollToElement(_CommunityClinicTeam_RemoveButton);
            Tap(_CommunityClinicTeam_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapCommunityClinicTeamLookupButotn()
        {
            ScrollToElement(_CommunityClinicTeam_LookupButotn);
            Tap(_CommunityClinicTeam_LookupButotn);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapContactTypeRemoveButton()
        {
            ScrollToElement(_ContactType_RemoveButton);
            Tap(_ContactType_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapContactTypeLookupButton()
        {
            ScrollToElement(_ContactType_LookupButton);
            Tap(_ContactType_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapResponsibleTeamRemoveButton()
        {
            ScrollToElement(_ResponsibleTeam_RemoveButton);
            Tap(_ResponsibleTeam_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapResponsibleTeamLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_LookupButton);
            Tap(_ResponsibleTeam_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapResponsibleUserRemoveButton()
        {
            ScrollToElement(_ResponsibleUser_RemoveButton);
            Tap(_ResponsibleUser_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapResponsibleUserLookupButton()
        {
            ScrollToElement(_ResponsibleUser_LookupButton);
            Tap(_ResponsibleUser_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapLocationTypeRemoveButton()
        {
            ScrollToElement(_LocationType_RemoveButton);
            Tap(_LocationType_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapLocationTypeLookupButton()
        {
            ScrollToElement(_LocationType_LookupButton);
            Tap(_LocationType_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapLeadProfessionalRemoveButton()
        {
            ScrollToElement(_LeadProfessional_RemoveButton);
            Tap(_LeadProfessional_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapLeadProfessionalLookupButton()
        {
            ScrollToElement(_LeadProfessional_LookupButton);
            Tap(_LeadProfessional_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapOutcomeRemoveButton()
        {
            ScrollToElement(_Outcome_RemoveButton);
            Tap(_Outcome_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapOutcomeLookupButton()
        {
            ScrollToElement(_Outcome_LookupButton);
            Tap(_Outcome_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapWhoLedTheAppointmentRemoveButton()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_WhoLedTheAppointment_RemoveButton);
            Tap(_WhoLedTheAppointment_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapWhoLedTheAppointmentLookupButton()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_WhoLedTheAppointment_LookupButton);
            Tap(_WhoLedTheAppointment_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapWhoCancelledTheAppointmentRemoveButton()
        {
            ScrollToElement(_WhoCancelledTheAppointment_LookupButton);
            Tap(_WhoCancelledTheAppointment_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapWhoCancelledTheAppointment_LookupButton()
        {
            ScrollToElement(_WhoCancelledTheAppointment_LookupButton);
            Tap(_WhoCancelledTheAppointment_LookupButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapAbsenceReasonRemoveButton()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_AbsenceReason_RemoveButton);
            Tap(_AbsenceReason_RemoveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapAbsenceReasonLookupButton()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_AbsenceReason_LookupButton);
            Tap(_AbsenceReason_LookupButton);

            return this;
        }



        public CaseHealthAppointmentRecordPage TapAdditionalProfessionalRequiredField()
        {
            ScrollToElement(_AdditionalProfessionalRequired_ScrollableField);
            Tap(_AdditionalProfessionalRequired_Field);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapCancelAppointmentField()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_CancelAppointment_Field);
            Tap(_CancelAppointment_Field);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapReasonField()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_Reason_Field);
            Tap(_Reason_Field);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapDNACNAWNBField()
        {
            ScrollToElement(_createdBy_Field);//this scroll is just to make the app scroll to the bottom. For some reason the bottom bar interferes with the scroll and the elements keep getting hidden behind it
            ScrollToElement(_DNACNAWNB_Field);
            Tap(_DNACNAWNB_Field);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapAddTravelTimeToAppointmentField()
        {
            ScrollToElement(_AddTravelTimeToAppointment_ScrollableField);
            Tap(_AddTravelTimeToAppointment_Field);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapReturnToBaseAfterAppointmentField()
        {
            ScrollToElement(_ReturnToBaseAfterAppointment_ScrollableField);
            Tap(_ReturnToBaseAfterAppointment_Field);

            return this;
        }



        public CaseHealthAppointmentRecordPage ValidateAppointmentReasonRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AppointmentReason_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_AppointmentReason_RemoveButton));
            }
            else
            {
                TryScrollToElement(_AppointmentReason_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_AppointmentReason_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAppointmentReasonLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AppointmentReason_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_AppointmentReason_LookupButton));
            }
            else
            {
                TryScrollToElement(_AppointmentReason_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_AppointmentReason_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCommunityClinicTeamRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_CommunityClinicTeam_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_CommunityClinicTeam_RemoveButton));
            }
            else
            {
                TryScrollToElement(_CommunityClinicTeam_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_CommunityClinicTeam_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateCommunityClinicTeamLookupVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_CommunityClinicTeam_LookupButotn);
                Assert.IsTrue(CheckIfElementVisible(_CommunityClinicTeam_LookupButotn));
            }
            else
            {
                TryScrollToElement(_CommunityClinicTeam_LookupButotn);
                Assert.IsFalse(CheckIfElementVisible(_CommunityClinicTeam_LookupButotn));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateContactTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ContactType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_ContactType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_ContactType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_ContactType_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateContactTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_ContactType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_ContactType_LookupButton));
            }
            else
            {
                TryScrollToElement(_ContactType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_ContactType_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateResponsibleTeamRemoveButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateResponsibleTeamLookupButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateResponsibleUserRemoveButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateResponsibleUserLookupButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateLocationTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_LocationType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_LocationType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_LocationType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_LocationType_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLocationTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_LocationType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_LocationType_LookupButton));
            }
            else
            {
                TryScrollToElement(_LocationType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_LocationType_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLeadProfessionalRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_LeadProfessional_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_LeadProfessional_RemoveButton));
            }
            else
            {
                TryScrollToElement(_LeadProfessional_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_LeadProfessional_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateLeadProfessionalLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_LeadProfessional_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_LeadProfessional_LookupButton));
            }
            else
            {
                TryScrollToElement(_LeadProfessional_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_LeadProfessional_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateOutcomeRemoveButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateOutcomeLookupButtonVisible(bool ExpectButtonVisible)
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
        public CaseHealthAppointmentRecordPage ValidateWhoLedTheAppointmentRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_WhoLedTheAppointment_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_WhoLedTheAppointment_RemoveButton));
            }
            else
            {
                TryScrollToElement(_WhoLedTheAppointment_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_WhoLedTheAppointment_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoLedTheAppointmentLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_WhoLedTheAppointment_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_WhoLedTheAppointment_LookupButton));
            }
            else
            {
                TryScrollToElement(_WhoLedTheAppointment_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_WhoLedTheAppointment_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointmentRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_WhoCancelledTheAppointment_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_WhoCancelledTheAppointment_RemoveButton));
            }
            else
            {
                TryScrollToElement(_WhoCancelledTheAppointment_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_WhoCancelledTheAppointment_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateWhoCancelledTheAppointment_LookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_WhoCancelledTheAppointment_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_WhoCancelledTheAppointment_LookupButton));
            }
            else
            {
                TryScrollToElement(_WhoCancelledTheAppointment_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_WhoCancelledTheAppointment_LookupButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAbsenceReasonRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AbsenceReason_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_RemoveButton));
            }
            else
            {
                TryScrollToElement(_AbsenceReason_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_RemoveButton));
            }

            return this;
        }
        public CaseHealthAppointmentRecordPage ValidateAbsenceReasonLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AbsenceReason_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_AbsenceReason_LookupButton));
            }
            else
            {
                TryScrollToElement(_AbsenceReason_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_AbsenceReason_LookupButton));
            }

            return this;
        }





        public CaseHealthAppointmentRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }



        public CaseHealthAppointmentRecordPage TapRelatedItemsButton()
        {
            WaitForElement(_RelatedItemsButton);
            Tap(_RelatedItemsButton);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapRelatedItems_LeftMenuSubArea()
        {
            WaitForElement(_RelatedItems_LeftMenuSubArea);
            Tap(_RelatedItems_LeftMenuSubArea);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapActivities_LeftMenuSubArea()
        {
            WaitForElement(_Activities_LeftMenuSubArea);
            Tap(_Activities_LeftMenuSubArea);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapAdditionalHealthProfessionalsLink()
        {
            WaitForElement(_AdditionalHealthProfessionals_Link);
            Tap(_AdditionalHealthProfessionals_Link);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapTasksIcon()
        {
            WaitForElement(_Tasks_Link);
            Tap(_Tasks_Link);

            return this;
        }
        public CaseHealthAppointmentRecordPage TapCaseNotesLink()
        {
            WaitForElement(_CaseNotes_Link);
            Tap(_CaseNotes_Link);

            return this;
        }

    }
}

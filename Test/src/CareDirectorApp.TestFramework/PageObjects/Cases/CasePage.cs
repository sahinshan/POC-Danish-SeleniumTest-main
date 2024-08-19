
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
    public class CasePage : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string RecordName) => e => e.Text("CASE: " + RecordName);

        #endregion

        #region Top Banner

        readonly Func<AppQuery, AppQuery> _personNameAndId_TopBanner = e => e.Marked("text_heading");
        readonly Func<AppQuery, AppQuery> _bornLabel_TopBanner = e => e.Marked("label_Born:");
        readonly Func<AppQuery, AppQuery> _bornText_TopBanner = e => e.Marked("text_Born:");
        readonly Func<AppQuery, AppQuery> _genderLabel_TopBanner = e => e.Marked("label_Gender:");
        readonly Func<AppQuery, AppQuery> _genderText_TopBanner = e => e.Marked("text_Gender:");
        readonly Func<AppQuery, AppQuery> _nhsLabel_TopBanner = e => e.Marked("label_NHS No:");
        readonly Func<AppQuery, AppQuery> _nhsText_TopBanner = e => e.Marked("text_NHS No:");
        readonly Func<AppQuery, AppQuery> _toogleIcon_TopBanner = e => e.Marked("toggleIcon");
        readonly Func<AppQuery, AppQuery> _preferredNameLabel_TopBanner = e => e.Marked("label_Preferred Name:");
        readonly Func<AppQuery, AppQuery> _preferredNameText_TopBanner = e => e.Marked("text_Preferred Name:");

        readonly Func<AppQuery, AppQuery> _primaryAddressLabel_TopBanner = e => e.Marked("label_Address (Home)");
        readonly Func<AppQuery, AppQuery> _primaryAddressText_TopBanner = e => e.Marked("text_Address (Home)");
        readonly Func<AppQuery, AppQuery> _phoneAndEmailLabel_TopBanner = e => e.Marked("label_Phone and Email");
        readonly Func<AppQuery, AppQuery> _homeLabel_TopBanner = e => e.Marked("label_Home:");
        readonly Func<AppQuery, AppQuery> _homeText_TopBanner = e => e.Marked("text_Home:");
        readonly Func<AppQuery, AppQuery> _workLabel_TopBanner = e => e.Marked("label_Work:");
        readonly Func<AppQuery, AppQuery> _workText_TopBanner = e => e.Marked("text_Work:");
        readonly Func<AppQuery, AppQuery> _mobileLabel_TopBanner = e => e.Marked("label_Mobile:");
        readonly Func<AppQuery, AppQuery> _mobileText_TopBanner = e => e.Marked("text_Mobile:");
        readonly Func<AppQuery, AppQuery> _emailLabel_TopBanner = e => e.Marked("label_Email:");
        readonly Func<AppQuery, AppQuery> _emailText_TopBanner = e => e.Marked("text_Email:");

        #endregion

        #region Fields
        readonly Func<AppQuery, AppQuery> _CaseNoLabel = e => e.Text("Case No");
        readonly Func<AppQuery, AppQuery> _PersonLabel = e => e.Text("Person");
        readonly Func<AppQuery, AppQuery> _PersonAgeLabel = e => e.Text("Person Age");
        readonly Func<AppQuery, AppQuery> _InitialContactLabel = e => e.Text("Initial Contact");
        readonly Func<AppQuery, AppQuery> _OtherRelatedContactLabel = e => e.Text("Other Related Contact");
        readonly Func<AppQuery, AppQuery> _DateTimeContactReceivedLabel = e => e.Text("Date/Time Contact Received");
        readonly Func<AppQuery, AppQuery> _ContactReceivedByLabel = e => e.Text("Contact Received By");
        readonly Func<AppQuery, AppQuery> _DateTimeRequestReceivedLabel = e => e.Text("Date/Time Request Received");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeamLabel = e => e.Text("Responsible Team");
        readonly Func<AppQuery, AppQuery> _ResponsibleUserLabel = e => e.Text("Responsible User");
        readonly Func<AppQuery, AppQuery> _ContactReasonLabel = e => e.Text("Contact Reason");
        readonly Func<AppQuery, AppQuery> _SecondaryCaseReasonLabel = e => e.Text("Secondary Case Reason");
        readonly Func<AppQuery, AppQuery> _PresentingPriorityLabel = e => e.Text("Presenting Priority");
        readonly Func<AppQuery, AppQuery> _PresentingNeedLabel = e => e.Text("Presenting Need");
        readonly Func<AppQuery, AppQuery> _AdditionalInformationLabel = e => e.Text("Additional Information");

        readonly Func<AppQuery, AppQuery> _ContactSourceLabel = e => e.Text("Contact Source");
        readonly Func<AppQuery, AppQuery> _AdministrativeCategoryLabel = e => e.Text("Administrative Category");
        readonly Func<AppQuery, AppQuery> _CaseTransferredFromLabel = e => e.Text("Case Transferred from");
        readonly Func<AppQuery, AppQuery> _ContactMadeByLabel = e => e.Text("Contact Made By");
        readonly Func<AppQuery, AppQuery> _ContactMadeByFreeTextLabel = e => e.Text("Contact Made By (Free text)");

        readonly Func<AppQuery, AppQuery> _IsThePersonAwareOfTheContactLabel = e => e.Text("Is the Person aware of the Contact?");
        readonly Func<AppQuery, AppQuery> _IsNOKCarerAwareOfThisContactLabel = e => e.Text("Street");

        readonly Func<AppQuery, AppQuery> _CommunityClinicTeamRequiredLabel = e => e.Text("Community/Clinic Team Required");
        readonly Func<AppQuery, AppQuery> _CaseAcceptedLabel = e => e.Text("Case Accepted");
        readonly Func<AppQuery, AppQuery> _CasePriorityLabel = e => e.Text("Case Priority");

        readonly Func<AppQuery, AppQuery> _PathwayKeySourceLabel = e => e.Text("Pathway Key Source");

        readonly Func<AppQuery, AppQuery> _ServiceTypeRequestedLabel = e => e.Text("Service Type Requested");
        readonly Func<AppQuery, AppQuery> _CNACountLabel = e => e.Text("CNA Count");
        readonly Func<AppQuery, AppQuery> _CaseStatusLabel = e => e.Text("Case Status");
        readonly Func<AppQuery, AppQuery> _DNACountLabel = e => e.Text("DNA Count");

        readonly Func<AppQuery, AppQuery> _DischargePersonLabel = e => e.Text("Discharge Person");

        #region SocialCareCase

        readonly Func<AppQuery, AppQuery> _SocialCareCase_CaseNoField = e => e.Marked("Field_378ecdbe7d19e91180dc0050560502cc").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_PersonField = e => e.Marked("Field_bd106f94d24fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_PersonAgeField = e => e.Marked("Field_f891e4f7d24fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CaseDateTimeField = e => e.Marked("Field_ed5d91cc7d19e91180dc0050560502cc").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_InitialContactField = e => e.Marked("Field_0c8216aad24fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_DateTimeContactReceivedField = e => e.Marked("Field_ea74ccd7d24fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ContactReceivedByField = e => e.Marked("Field_53685705d34fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ContactReasonField = e => e.Marked("Field_4412930dd34fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_PresentingPriorityField = e => e.Marked("Field_06c2aa20d34fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CINCodeField = e => e.Marked("Field_58104928d34fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_AdditionalInformationField = e => e.Marked("Field_8672bc2fd34fe911a2c40050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _SocialCareCase_ContactMadeByField = e => e.Marked("Field_fe6cc79af88ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ContactMadeByFreeTextField = e => e.Marked("Field_33e075b8f88ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CaseOriginField = e => e.Marked("Field_2bb758c6f88ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ContactSourceField = e => e.Marked("Field_2ec6edd2f88ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_IsThePersonAwareOfTheContactField = e => e.Marked("Field_8eec1c11f98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_DoesPersonAgreeSupportThisContactField = e => e.Marked("Field_8a17e91af98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_IsParentCarerAwareOfThisContactField = e => e.Marked("Field_83e04591f98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_DoesParentCarerAgreeSupportThisContactField = e => e.Marked("Field_c880499df98ce911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_IsNOKCarerAwareOfThisContactField = e => e.Marked("Field_5ab1b5acf98ce911a2c50050569231cf").Class("FormsTextView");
        
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CaseStatusField = e => e.Marked("Field_36755bc9f98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CasePriorityField = e => e.Marked("Field_f22442d0f98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ResponsibleTeamField = e => e.Marked("Field_671276e6f98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ResponsibleUserField = e => e.Marked("Field_0607d3def98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ReviewDateField = e => e.Marked("Field_c7c27bedf98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_CloseDateField = e => e.Marked("Field_980388faf98ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ClosureReasonField = e => e.Marked("Field_88053f01fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ClosureAcceptedByField = e => e.Marked("Field_d98b7b08fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ArchieDateField = e => e.Marked("Field_a5aa3110fa8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _SocialCareCase_ReReferralField = e => e.Marked("Field_4cfa0336fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ReferringAgencyCaseIDField = e => e.Marked("Field_a2b88a42fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_ResponseMadeToContactField = e => e.Marked("Field_a891c44cfa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_NonMigratedWorkerNameField = e => e.Marked("Field_10e6f154fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_DateAndTimeOfContactWithTraineStaffField = e => e.Marked("Field_d17999b2fa8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _SocialCareCase_PoliceNotifiedField = e => e.Marked("Field_3f7affc8fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_PoliceNotifiedDateField = e => e.Marked("Field_b01430d0fa8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _SocialCareCase_PoliceNotesField = e => e.Marked("Field_105a93d9fa8ce911a2c50050569231cf").Class("FormsTextView");

        #endregion

        #region CommunityHealtCase

        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CaseNoField = e => e.Marked("Field_1debf1ce3b51e911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_PersonField = e => e.Marked("Field_0d8566ea3b51e911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_PersonAgeField = e => e.Marked("Field_d30c358c008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_InitialContactField = e => e.Marked("Field_87d53409008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_OtherRelatedContactField = e => e.Marked("Field_3e043c11008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_DateTimeContactReceivedField = e => e.Marked("Field_14ca651d008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ContactReceivedByField = e => e.Marked("Field_15639439008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_DateTimeRequestReceivedField = e => e.Marked("Field_f5616d42008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ResponsibleTeamField = e => e.Marked("Field_a086d4f73b51e911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ResponsibleUserField = e => e.Marked("Field_c81584013c51e911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ContactReasonField = e => e.Marked("Field_8e65b26d008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_SecondaryCaseReasonField = e => e.Marked("Field_d346ce4b008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_PresentingPriorityField = e => e.Marked("Field_f7a7e452008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_PresentingNeedField = e => e.Marked("Field_0fddda5d008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_AdditionalInformationField = e => e.Marked("Field_187d527b008de911a2c50050569231cf").Class("FormsTextView");


        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ContactSourceField = e => e.Marked("Field_1b112bb2008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_AdministrativeCategoryField = e => e.Marked("Field_d1681aba008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CaseTransferredFromField = e => e.Marked("Field_69985cc0008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ContactMadeByField = e => e.Marked("Field_c700d8c6008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ContactMadeByFreeTextField = e => e.Marked("Field_247b66cd008de911a2c50050569231cf").Class("FormsTextView");


        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_IsThePersonAwareOfTheContactField = e => e.Marked("Field_70be9ae8008de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_IsNOKCarerAwareOfThisContactField = e => e.Marked("Field_5fdc64f7008de911a2c50050569231cf").Class("FormsTextView");


        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CommunityClinicTeamRequiredField = e => e.Marked("Field_2c5095fa018de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CaseAcceptedField = e => e.Marked("Field_0d5b9200028de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CasePriorityField = e => e.Marked("Field_ff4d4fa9028de911a2c50050569231cf").Class("FormsTextView");


        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_PathwayKeySourceField = e => e.Marked("Field_493cb9d0028de911a2c50050569231cf").Class("FormsTextView");


        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_ServiceTypeRequestedField = e => e.Marked("Field_72a998db038de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CNACountField = e => e.Marked("Field_384fd1e1038de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_CaseStatusField = e => e.Marked("Field_ecb48ae8038de911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_DNACountField = e => e.Marked("Field_0b0db5ee038de911a2c50050569231cf").Class("FormsTextView");

        
        readonly Func<AppQuery, AppQuery> _CommunityHealtCase_DischargePersonField = e => e.Marked("Field_fed77ed53c51e911a2c40050569231cf").Class("FormsTextView");


        #endregion

        #region InpatientCase

        readonly Func<AppQuery, AppQuery> _InpatientCase_CaseNoField = e => e.Marked("Field_8a7ec4ced84fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PersonField = e => e.Marked("Field_d409e2ddd84fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_InitialContactField = e => e.Marked("Field_0805f9e6d84fe911a2c40050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_OtherRelatedContactField = e => e.Marked("Field_8586c42dfb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DateTimeContactReceivedField = e => e.Marked("Field_6aec2336fb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ContactReceivedByField = e => e.Marked("Field_c5af3c3dfb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ResponsibleUserField = e => e.Marked("Field_b8767751fb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ResponsibleTeamField = e => e.Marked("Field_be7cdd57fb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ReasonForAdmissionField = e => e.Marked("Field_017f5b7efb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_SecondaryAdmissionReasonField = e => e.Marked("Field_55674c97fb8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_InpatientStatusField = e => e.Marked("Field_4bbb989ffb8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_ContactSourceField = e => e.Marked("Field_36fec2a9fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ContactMadeByField = e => e.Marked("Field_4b6b87b1fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ContactMadeByFreeTextField = e => e.Marked("Field_6e2206bffc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_AdministrativeCategoryField = e => e.Marked("Field_509213c8fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_WhoWasNotifiedField = e => e.Marked("Field_0decf2f1fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_AdmissionSourceField = e => e.Marked("Field_e5fe61dbfc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PatientClassificationField = e => e.Marked("Field_da41ace8fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField = e => e.Marked("Field_09ecf2f1fc8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_IntendedManagementField = e => e.Marked("Field_5ca448d2fc8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_OutlineNeedForAdmissionField = e => e.Marked("Field_0143c225fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_CriteriaForDischargeField = e => e.Marked("Field_a468703efd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_AdmissionMethodField = e => e.Marked("Field_c48b8e47fd8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_ServiceTypeRequestedField = e => e.Marked("Field_abb2a763fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_CurrentConsultatField = e => e.Marked("Field_61fee86afd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DoLSConcernField = e => e.Marked("Field_4e016573fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_NamedProfessionalField = e => e.Marked("Field_ead3ed7afd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_AdmissionDateTimeField = e => e.Marked("Field_89ff5183fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_LegalStatusOnAdmissionField = e => e.Marked("Field_eeff0b8cfd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DaysAsInpatientField = e => e.Marked("Field_62371e95fd8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_DecisionToAdmitAgreedDateTimeField = e => e.Marked("Field_471c4daefd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_IntendedAdmissionDateField = e => e.Marked("Field_4d6c31b5fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ReasonForChangeIntendedAdmissionField = e => e.Marked("Field_89baf9bdfd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DateIntendedAdmissionChangedField = e => e.Marked("Field_ce5d35c6fd8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_HospitalField = e => e.Marked("Field_cd21dadbfd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_WardField = e => e.Marked("Field_0f4252ebfd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_BayRoomField = e => e.Marked("Field_e052a6f5fd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_BedField = e => e.Marked("Field_6464aefcfd8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ResponsibleWardField = e => e.Marked("Field_8d494a04fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_TransferOfCareField = e => e.Marked("Field_48671f0cfe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ActualDateTimeOfTransferField = e => e.Marked("Field_420d5812fe8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_PathwayKeySourceField = e => e.Marked("Field_cc7c0326fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PathwayKeyField = e => e.Marked("Field_9cc7ce30fe8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_EstimatedDateOfDischargeField = e => e.Marked("Field_21026e47fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PlannedDateOfDischargeField = e => e.Marked("Field_d7b4bf51fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PlannedDischargeDestinationField = e => e.Marked("Field_25262759fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_FitForDischargeDateField = e => e.Marked("Field_b34b0762fe8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_ActualDischargeDateTimeField = e => e.Marked("Field_34359b7afe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DischargeMethodField = e => e.Marked("Field_d664f793fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_ReasonNotAcceptedField = e => e.Marked("Field_bab2849efe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DischargeInformationField = e => e.Marked("Field_5e511da7fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_Section117AftercareEntitlementField = e => e.Marked("Field_32422cb1fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_CarerNoKNotifiedField = e => e.Marked("Field_305056bafe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_WishesContactFromAnIMHAField = e => e.Marked("Field_64f672c2fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DischargeCloseDateField = e => e.Marked("Field_03d8acd4fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_DischargedToHomAddressField = e => e.Marked("Field_61c756dffe8ce911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _InpatientCase_PropertyNameield = e => e.Marked("Field_9d0415eefe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PropertyNoField = e => e.Marked("Field_466dfbf7fe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_StreetField = e => e.Marked("Field_eb1072fefe8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_VlgField = e => e.Marked("Field_85fc7205ff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PropertyTypeField = e => e.Marked("Field_98a6130dff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_TownField = e => e.Marked("Field_483c7a1dff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_CountryField = e => e.Marked("Field_955ed139ff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_UPRNField = e => e.Marked("Field_701d509aff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_BoroughField = e => e.Marked("Field_741d509aff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_CountyField = e => e.Marked("Field_c5bc23a1ff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_TempAddressWardField = e => e.Marked("Field_90c045abff8ce911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _InpatientCase_PostCodeField = e => e.Marked("Field_30f087b1ff8ce911a2c50050569231cf").Class("FormsTextView");


        #endregion





        #endregion



        #region Footer
        readonly Func<AppQuery, AppQuery> _createdByFooterLabel = e => e.Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdOnFooterLabel = e => e.Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedByFooterLabel = e => e.Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedOnFooterLabel = e => e.Marked("FooterLabel_modifiedon");
        #endregion

        #region Related Items

        readonly Func<AppQuery, AppQuery> _relatedItemsButton = e => e.Marked("RelatedItemsButton");

        readonly Func<AppQuery, AppQuery> _relatedItemsContentView = e => e.Marked("RelatedItemsContentView");
        readonly Func<AppQuery, AppQuery> _activitiesLabel = e => e.Marked("Activities_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _relatedItemsLabel = e => e.Marked("RelatedItems_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _healthLabel = e => e.Marked("Health_CategoryLabel");

        readonly Func<AppQuery, AppQuery> _tasksRelatedItem = e => e.Marked("Activities_Item_Task");
        readonly Func<AppQuery, AppQuery> _caseNotesRelatedItem = e => e.Marked("Activities_Item_CaseCaseNote");
        
        readonly Func<AppQuery, AppQuery> _caseAttachmentsRelatedItem = e => e.Marked("RelatedItems_Item_Attachments");
        readonly Func<AppQuery, AppQuery> _caseFormsRelatedItem = e => e.Marked("RelatedItems_Item_CaseForm");
        readonly Func<AppQuery, AppQuery> _caseInvolvementsRelatedItem = e => e.Marked("RelatedItems_Item_CaseInvolvement");

        readonly Func<AppQuery, AppQuery> _healthAppointmentsRelatedItem = e => e.Marked("Health_Item_HealthAppointment");

        #endregion


        public CasePage(IApp app)
        {
            _app = app;
        }



        #region Related Items

        public CasePage WaitForRelatedItemsSubMenuToOpen()
        {
            this._app.WaitForElement(_relatedItemsContentView);
            this._app.WaitForElement(_activitiesLabel);
            this._app.WaitForElement(_relatedItemsLabel);
            this._app.WaitForElement(_relatedItemsLabel);


            return this;
        }

        public CasePage TapRelatedItemsButton()
        {
            this._app.Tap(_relatedItemsButton);

            return this;
        }

        public CasePage TapActivitiesArea_RelatedItems()
        {
            this._app.Tap(_activitiesLabel);

            return this;
        }

        public CasePage TapRelatedItemsArea_RelatedItems()
        {
            this._app.Tap(_relatedItemsLabel);

            return this;
        }

        public CasePage TapHealthArea_RelatedItems()
        {
            this._app.Tap(_healthLabel);

            return this;
        }


        public TasksPage TapTasksIcon_RelatedItems()
        {
            this._app.Tap(_tasksRelatedItem);

            return new TasksPage(this._app);
        }

        public CasePage TapCaseNotesIcon_RelatedItems()
        {
            this._app.Tap(_caseNotesRelatedItem);

            return this;
        }

        public CasePage TapAttachmentsIcon_RelatedItems()
        {
            Tap(_caseAttachmentsRelatedItem);

            return this;
        }

        public CasePage TapCaseForms_RelatedItems()
        {
            Tap(_caseFormsRelatedItem);

            return this;
        }

        public CasePage TapCaseInvolvements_RelatedItems()
        {
            Tap(_caseInvolvementsRelatedItem);

            return this;
        }

        public CasePage TapHealthAppointmentsIcon_RelatedItems()
        {
            Tap(_healthAppointmentsRelatedItem);

            return this;
        }


        public CasePage ValidateActivitiesAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_tasksRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseNotesRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_caseNotesRelatedItem element was not visible");

            return this;
        }

        public CasePage ValidateActivitiesAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_tasksRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseNotesRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_caseNotesRelatedItem element was not visible");

            return this;
        }

        public CasePage ValidateRelatedItemsAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_caseAttachmentsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_caseAttachmentsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseFormsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_caseFormsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseInvolvementsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_caseInvolvementsRelatedItem element was not visible");


            return this;
        }

        public CasePage ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_caseAttachmentsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_caseAttachmentsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseFormsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_caseFormsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseInvolvementsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_caseInvolvementsRelatedItem element was not visible");


            return this;
        }

        public CasePage ValidateHealthAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_healthAppointmentsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_healthAppointmentsRelatedItem element was not visible");


            return this;
        }

        public CasePage ValidateHealthAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_healthAppointmentsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_healthAppointmentsRelatedItem element was not visible");


            return this;
        }



        #region Mobile View Mode



        #endregion




        #endregion


        public CasePage WaitForCasePageToLoad(string PersonName)
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_peoplePageIconButton);
            _app.WaitForElement(_pageTitle(PersonName));

            _app.WaitForElement(_relatedItemsButton);

            return this;
        }
        public CasePage ExpandTopBanner()
        {
            this._app.Tap(_toogleIcon_TopBanner);

            return this;
        }
        public CasePage CollapseTopBanner()
        {
            this._app.Tap(_toogleIcon_TopBanner);

            return this;
        }

        public TeamPage SocialCareCase_TapResponsibleTeamField()
        {
            ScrollToElement(_SocialCareCase_ResponsibleTeamField);

            this._app.Tap(_SocialCareCase_ResponsibleTeamField);

            return new TeamPage(this._app);
        }

        public TeamPage CommunityHealtCase_TapResponsibleTeamField()
        {
            ScrollToElement(_CommunityHealtCase_ResponsibleTeamField);

            this._app.Tap(_CommunityHealtCase_ResponsibleTeamField);

            return new TeamPage(this._app);
        }

        public TeamPage InpatientCase_TapResponsibleTeamField()
        {
            ScrollToElement(_InpatientCase_ResponsibleTeamField);

            this._app.Tap(_InpatientCase_ResponsibleTeamField);

            return new TeamPage(this._app);
        }


        public CasePage ValidateMainTopBannerLabelsVisible()
        {
            WaitForElement(_bornLabel_TopBanner);
            WaitForElement(_genderLabel_TopBanner);
            WaitForElement(_nhsLabel_TopBanner);
            WaitForElement(_preferredNameLabel_TopBanner);

            return this;
        }
        public CasePage ValidateSecondayTopBannerLabelsNotVisible()
        {
            WaitForElementNotVisible(_primaryAddressLabel_TopBanner);
            WaitForElementNotVisible(_phoneAndEmailLabel_TopBanner);
            WaitForElementNotVisible(_homeLabel_TopBanner);
            WaitForElementNotVisible(_workLabel_TopBanner);
            WaitForElementNotVisible(_mobileLabel_TopBanner);
            WaitForElementNotVisible(_emailLabel_TopBanner);

            return this;
        }
        public CasePage ValidateSecondayTopBannerLabelsVisible()
        {
            WaitForElement(_primaryAddressLabel_TopBanner);
            WaitForElement(_phoneAndEmailLabel_TopBanner);
            WaitForElement(_homeLabel_TopBanner);
            WaitForElement(_workLabel_TopBanner);
            WaitForElement(_mobileLabel_TopBanner);
            WaitForElement(_emailLabel_TopBanner);

            return this;
        }

        public CasePage ValidatePersonNameAndId_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_personNameAndId_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateBornText_TopBanner(DateTime DateOfBirth)
        {
            string expectedFieldText = GetFullBirthDateText(DateOfBirth);
            string fieldText = this._app.Query(_bornText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(expectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateGenderText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_genderText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateNHSNoText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_nhsText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidatePreferredNameText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_preferredNameText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateAddressText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_primaryAddressText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateHomePhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_homeText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateWorkPhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_workText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateMobilePhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_mobileText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public CasePage ValidateEmailText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_emailText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }


        #region CommunityHealthCase

        public CasePage ValidateCommunityHealtCase_CaseNoField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CaseNoField);
            string fieldText = this._app.Query(_CommunityHealtCase_CaseNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_PersonField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_PersonField);
            string fieldText = this._app.Query(_CommunityHealtCase_PersonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_PersonAgeField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_PersonAgeField);
            string fieldText = this._app.Query(_CommunityHealtCase_PersonAgeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_InitialContactField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_InitialContactField);
            string fieldText = this._app.Query(_CommunityHealtCase_InitialContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_OtherRelatedContactField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_OtherRelatedContactField);
            string fieldText = this._app.Query(_CommunityHealtCase_OtherRelatedContactField ).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_DateTimeContactReceivedField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_DateTimeContactReceivedField);
            string fieldText = this._app.Query(_CommunityHealtCase_DateTimeContactReceivedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ContactReceivedByField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ContactReceivedByField);
            string fieldText = this._app.Query(_CommunityHealtCase_ContactReceivedByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_DateTimeRequestReceivedField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_DateTimeRequestReceivedField);
            string fieldText = this._app.Query(_CommunityHealtCase_DateTimeRequestReceivedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ResponsibleTeamField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ResponsibleTeamField);
            string fieldText = this._app.Query(_CommunityHealtCase_ResponsibleTeamField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ResponsibleUserField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ResponsibleUserField);
            string fieldText = this._app.Query(_CommunityHealtCase_ResponsibleUserField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ContactReasonField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ContactReasonField);
            string fieldText = this._app.Query(_CommunityHealtCase_ContactReasonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_SecondaryCaseReasonField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_SecondaryCaseReasonField);
            string fieldText = this._app.Query(_CommunityHealtCase_SecondaryCaseReasonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_PresentingPriorityField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_PresentingPriorityField);
            string fieldText = this._app.Query(_CommunityHealtCase_PresentingPriorityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_PresentingNeedField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_PresentingNeedField);
            string fieldText = this._app.Query(_CommunityHealtCase_PresentingNeedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_AdditionalInformationField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_AdditionalInformationField);
            string fieldText = this._app.Query(_CommunityHealtCase_AdditionalInformationField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_ContactSourceField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ContactSourceField);
            string fieldText = this._app.Query(_CommunityHealtCase_ContactSourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_AdministrativeCategoryField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_AdministrativeCategoryField);
            string fieldText = this._app.Query(_CommunityHealtCase_AdministrativeCategoryField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_CaseTransferredFromField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CaseTransferredFromField);
            string fieldText = this._app.Query(_CommunityHealtCase_CaseTransferredFromField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ContactMadeByField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ContactMadeByField);
            string fieldText = this._app.Query(_CommunityHealtCase_ContactMadeByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_ContactMadeByFreeTextField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ContactMadeByFreeTextField);
            string fieldText = this._app.Query(_CommunityHealtCase_ContactMadeByFreeTextField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_IsThePersonAwareOfTheContactField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_IsThePersonAwareOfTheContactField);
            string fieldText = this._app.Query(_CommunityHealtCase_IsThePersonAwareOfTheContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_IsNOKCarerAwareOfThisContactField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_IsNOKCarerAwareOfThisContactField);
            string fieldText = this._app.Query(_CommunityHealtCase_IsNOKCarerAwareOfThisContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_CommunityClinicTeamRequiredField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CommunityClinicTeamRequiredField);
            string fieldText = this._app.Query(_CommunityHealtCase_CommunityClinicTeamRequiredField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_CaseAcceptedField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CaseAcceptedField);
            string fieldText = this._app.Query(_CommunityHealtCase_CaseAcceptedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_CasePriorityField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CasePriorityField);
            string fieldText = this._app.Query(_CommunityHealtCase_CasePriorityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_PathwayKeySourceField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_PathwayKeySourceField);
            string fieldText = this._app.Query(_CommunityHealtCase_PathwayKeySourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_ServiceTypeRequestedField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_ServiceTypeRequestedField);
            string fieldText = this._app.Query(_CommunityHealtCase_ServiceTypeRequestedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_CNACountField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CNACountField);
            string fieldText = this._app.Query(_CommunityHealtCase_CNACountField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_CaseStatusField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_CaseStatusField);
            string fieldText = this._app.Query(_CommunityHealtCase_CaseStatusField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateCommunityHealtCase_DNACountField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_DNACountField);
            System.Threading.Thread.Sleep(300);
            string fieldText = this._app.Query(_CommunityHealtCase_DNACountField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        public CasePage ValidateCommunityHealtCase_DischargePersonField(string ExpectedFieldText)
        {
            ScrollToElement(_CommunityHealtCase_DischargePersonField);
            string fieldText = this._app.Query(_CommunityHealtCase_DischargePersonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        #endregion

        #region SocialCareCase

        public CasePage ValidateSocialCareCase_CaseNoField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CaseNoField);
            string fieldText = this._app.Query(_SocialCareCase_CaseNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_PersonField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PersonField);
            string fieldText = this._app.Query(_SocialCareCase_PersonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        
        public CasePage ValidateSocialCareCase_PersonAgeField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PersonAgeField);
            string fieldText = this._app.Query(_SocialCareCase_PersonAgeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_CaseDateTimeField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CaseDateTimeField);

            string fieldText = this._app.Query(_SocialCareCase_CaseDateTimeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateSocialCareCase_InitialContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_InitialContactField);
            string fieldText = this._app.Query(_SocialCareCase_InitialContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_DateTimeContactReceivedField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_DateTimeContactReceivedField);
            string fieldText = this._app.Query(_SocialCareCase_DateTimeContactReceivedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ContactReceivedByField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ContactReceivedByField);
            string fieldText = this._app.Query(_SocialCareCase_ContactReceivedByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ContactReasonField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ContactReasonField);
            string fieldText = this._app.Query(_SocialCareCase_ContactReasonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_PresentingPriorityField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PresentingPriorityField);
            string fieldText = this._app.Query(_SocialCareCase_PresentingPriorityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_CINCodeField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CINCodeField);

            string fieldText = this._app.Query(_SocialCareCase_CINCodeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateSocialCareCase_AdditionalInformationField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_AdditionalInformationField);
            string fieldText = this._app.Query(_SocialCareCase_AdditionalInformationField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }




        public CasePage ValidateSocialCareCase_ContactMadeByField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ContactMadeByField);
            string fieldText = this._app.Query(_SocialCareCase_ContactMadeByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ContactMadeByFreeTextField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ContactMadeByFreeTextField);
            string fieldText = this._app.Query(_SocialCareCase_ContactMadeByFreeTextField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_CaseOriginField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CaseOriginField);

            string fieldText = this._app.Query(_SocialCareCase_CaseOriginField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateSocialCareCase_ContactSourceField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ContactSourceField);
            string fieldText = this._app.Query(_SocialCareCase_ContactSourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }



        public CasePage ValidateSocialCareCase_IsThePersonAwareOfTheContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_IsThePersonAwareOfTheContactField);
            string fieldText = this._app.Query(_SocialCareCase_IsThePersonAwareOfTheContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_DoesPersonAgreeSupportThisContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_DoesPersonAgreeSupportThisContactField);

            string fieldText = this._app.Query(_SocialCareCase_DoesPersonAgreeSupportThisContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateSocialCareCase_IsParentCarerAwareOfThisContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_IsParentCarerAwareOfThisContactField);

            string fieldText = this._app.Query(_SocialCareCase_IsParentCarerAwareOfThisContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateSocialCareCase_DoesParentCarerAgreeSupportThisContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_DoesParentCarerAgreeSupportThisContactField);
            string fieldText = this._app.Query(_SocialCareCase_DoesParentCarerAgreeSupportThisContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_IsNOKCarerAwareOfThisContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_IsNOKCarerAwareOfThisContactField);
            string fieldText = this._app.Query(_SocialCareCase_IsNOKCarerAwareOfThisContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateSocialCareCase_CaseStatusField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CaseStatusField);
            string fieldText = this._app.Query(_SocialCareCase_CaseStatusField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_CasePriorityField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CasePriorityField);
            string fieldText = this._app.Query(_SocialCareCase_CasePriorityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ResponsibleTeamField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ResponsibleTeamField);
            string fieldText = this._app.Query(_SocialCareCase_ResponsibleTeamField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ResponsibleUserField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ResponsibleUserField);
            string fieldText = this._app.Query(_SocialCareCase_ResponsibleUserField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ReviewDateField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ReviewDateField);
            string fieldText = this._app.Query(_SocialCareCase_ReviewDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_CloseDateField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_CloseDateField);
            string fieldText = this._app.Query(_SocialCareCase_CloseDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ClosureReasonField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ClosureReasonField);
            string fieldText = this._app.Query(_SocialCareCase_ClosureReasonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ClosureAcceptedByField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ClosureAcceptedByField);
            string fieldText = this._app.Query(_SocialCareCase_ClosureAcceptedByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ArchieDateField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ArchieDateField);
            string fieldText = this._app.Query(_SocialCareCase_ArchieDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }




        public CasePage ValidateSocialCareCase_ReReferralField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ReReferralField);
            string fieldText = this._app.Query(_SocialCareCase_ReReferralField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ReferringAgencyCaseIDField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ReferringAgencyCaseIDField);
            string fieldText = this._app.Query(_SocialCareCase_ReferringAgencyCaseIDField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_ResponseMadeToContactField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_ResponseMadeToContactField);
            string fieldText = this._app.Query(_SocialCareCase_ResponseMadeToContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_NonMigratedWorkerNameField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_NonMigratedWorkerNameField);
            string fieldText = this._app.Query(_SocialCareCase_NonMigratedWorkerNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_DateAndTimeOfContactWithTraineStaffField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_DateAndTimeOfContactWithTraineStaffField);
            string fieldText = this._app.Query(_SocialCareCase_DateAndTimeOfContactWithTraineStaffField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }



        public CasePage ValidateSocialCareCase_PoliceNotifiedField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PoliceNotifiedField);
            string fieldText = this._app.Query(_SocialCareCase_PoliceNotifiedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_PoliceNotifiedDateField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PoliceNotifiedDateField);
            string fieldText = this._app.Query(_SocialCareCase_PoliceNotifiedDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        public CasePage ValidateSocialCareCase_PoliceNotesField(string ExpectedFieldText)
        {
            ScrollToElement(_SocialCareCase_PoliceNotesField);
            string fieldText = this._app.Query(_SocialCareCase_PoliceNotesField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }


        #endregion

        #region InpatientCase

        public CasePage ValidateInpatientCase_CaseNoField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CaseNoField);
            string fieldText = this._app.Query(_InpatientCase_CaseNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PersonField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PersonField);
            string fieldText = this._app.Query(_InpatientCase_PersonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_InitialContactField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_InitialContactField);
            string fieldText = this._app.Query(_InpatientCase_InitialContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_OtherRelatedContactField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_OtherRelatedContactField);
            string fieldText = this._app.Query(_InpatientCase_OtherRelatedContactField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DateTimeContactReceivedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DateTimeContactReceivedField);
            string fieldText = this._app.Query(_InpatientCase_DateTimeContactReceivedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ContactReceivedByField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ContactReceivedByField);
            string fieldText = this._app.Query(_InpatientCase_ContactReceivedByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ResponsibleUserField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ResponsibleUserField);
            string fieldText = this._app.Query(_InpatientCase_ResponsibleUserField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ResponsibleTeamField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ResponsibleTeamField);
            string fieldText = this._app.Query(_InpatientCase_ResponsibleTeamField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ReasonForAdmissionField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ReasonForAdmissionField);
            string fieldText = this._app.Query(_InpatientCase_ReasonForAdmissionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_SecondaryAdmissionReasonField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_SecondaryAdmissionReasonField);
            string fieldText = this._app.Query(_InpatientCase_SecondaryAdmissionReasonField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_InpatientStatusField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_InpatientStatusField);
            string fieldText = this._app.Query(_InpatientCase_InpatientStatusField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_ContactSourceField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ContactSourceField);
            string fieldText = this._app.Query(_InpatientCase_ContactSourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ContactMadeByField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ContactMadeByField);
            string fieldText = this._app.Query(_InpatientCase_ContactMadeByField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ContactMadeByFreeTextField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ContactMadeByFreeTextField);
            string fieldText = this._app.Query(_InpatientCase_ContactMadeByFreeTextField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_AdministrativeCategoryField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_AdministrativeCategoryField);
            string fieldText = this._app.Query(_InpatientCase_AdministrativeCategoryField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_WhoWasNotifiedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_WhoWasNotifiedField);
            ValidateElementText(_InpatientCase_WhoWasNotifiedField, ExpectedFieldText);
            
            return this;
        }
        public CasePage ValidateInpatientCase_AdmissionSourceField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_AdmissionSourceField);
            System.Threading.Thread.Sleep(300);
            string fieldText = this._app.Query(_InpatientCase_AdmissionSourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PatientClassificationField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PatientClassificationField);
            string fieldText = this._app.Query(_InpatientCase_PatientClassificationField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField);
            string fieldText = this._app.Query(_InpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_IntendedManagementField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_IntendedManagementField);
            string fieldText = this._app.Query(_InpatientCase_IntendedManagementField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_OutlineNeedForAdmissionField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_OutlineNeedForAdmissionField);
            string fieldText = this._app.Query(_InpatientCase_OutlineNeedForAdmissionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_CriteriaForDischargeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CriteriaForDischargeField);
            string fieldText = this._app.Query(_InpatientCase_CriteriaForDischargeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_AdmissionMethodField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_AdmissionMethodField);
            string fieldText = this._app.Query(_InpatientCase_AdmissionMethodField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_ServiceTypeRequestedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ServiceTypeRequestedField);
            string fieldText = this._app.Query(_InpatientCase_ServiceTypeRequestedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_CurrentConsultatField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CurrentConsultatField);
            string fieldText = this._app.Query(_InpatientCase_CurrentConsultatField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DoLSConcernField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DoLSConcernField);
            string fieldText = this._app.Query(_InpatientCase_DoLSConcernField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_NamedProfessionalField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_NamedProfessionalField);
            string fieldText = this._app.Query(_InpatientCase_NamedProfessionalField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_AdmissionDateTimeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_AdmissionDateTimeField);
            string fieldText = this._app.Query(_InpatientCase_AdmissionDateTimeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_LegalStatusOnAdmissionField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_LegalStatusOnAdmissionField);
            string fieldText = this._app.Query(_InpatientCase_LegalStatusOnAdmissionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DaysAsInpatientField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DaysAsInpatientField);
            string fieldText = this._app.Query(_InpatientCase_DaysAsInpatientField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_DecisionToAdmitAgreedDateTimeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DecisionToAdmitAgreedDateTimeField);
            string fieldText = this._app.Query(_InpatientCase_DecisionToAdmitAgreedDateTimeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_IntendedAdmissionDateField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_IntendedAdmissionDateField);
            string fieldText = this._app.Query(_InpatientCase_IntendedAdmissionDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ReasonForChangeIntendedAdmissionField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ReasonForChangeIntendedAdmissionField);
            string fieldText = this._app.Query(_InpatientCase_ReasonForChangeIntendedAdmissionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DateIntendedAdmissionChangedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DateIntendedAdmissionChangedField);
            string fieldText = this._app.Query(_InpatientCase_DateIntendedAdmissionChangedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_HospitalField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_HospitalField);
            string fieldText = this._app.Query(_InpatientCase_HospitalField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_WardField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_WardField);
            string fieldText = this._app.Query(_InpatientCase_WardField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_BayRoomField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_BayRoomField);
            string fieldText = this._app.Query(_InpatientCase_BayRoomField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_BedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_BedField);
            string fieldText = this._app.Query(_InpatientCase_BedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ResponsibleWardField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ResponsibleWardField);
            string fieldText = this._app.Query(_InpatientCase_ResponsibleWardField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_TransferOfCareField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_TransferOfCareField);
            string fieldText = this._app.Query(_InpatientCase_TransferOfCareField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ActualDateTimeOfTransferField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ActualDateTimeOfTransferField);
            string fieldText = this._app.Query(_InpatientCase_ActualDateTimeOfTransferField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_PathwayKeySourceField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PathwayKeySourceField);
            string fieldText = this._app.Query(_InpatientCase_PathwayKeySourceField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PathwayKeyField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PathwayKeyField);
            string fieldText = this._app.Query(_InpatientCase_PathwayKeyField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_EstimatedDateOfDischargeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_EstimatedDateOfDischargeField);
            string fieldText = this._app.Query(_InpatientCase_EstimatedDateOfDischargeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PlannedDateOfDischargeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PlannedDateOfDischargeField);
            string fieldText = this._app.Query(_InpatientCase_PlannedDateOfDischargeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PlannedDischargeDestinationField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PlannedDischargeDestinationField);
            string fieldText = this._app.Query(_InpatientCase_PlannedDischargeDestinationField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_FitForDischargeDateField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_FitForDischargeDateField);
            string fieldText = this._app.Query(_InpatientCase_FitForDischargeDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }





        public CasePage ValidateInpatientCase_ActualDischargeDateTimeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ActualDischargeDateTimeField);
            string fieldText = this._app.Query(_InpatientCase_ActualDischargeDateTimeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DischargeMethodField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DischargeMethodField);
            string fieldText = this._app.Query(_InpatientCase_DischargeMethodField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_ReasonNotAcceptedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_ReasonNotAcceptedField);
            string fieldText = this._app.Query(_InpatientCase_ReasonNotAcceptedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DischargeInformationField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DischargeInformationField);
            string fieldText = this._app.Query(_InpatientCase_DischargeInformationField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_Section117AftercareEntitlementField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_Section117AftercareEntitlementField);
            string fieldText = this._app.Query(_InpatientCase_Section117AftercareEntitlementField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_CarerNoKNotifiedField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CarerNoKNotifiedField);
            string fieldText = this._app.Query(_InpatientCase_CarerNoKNotifiedField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_WishesContactFromAnIMHAField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_WishesContactFromAnIMHAField);
            string fieldText = this._app.Query(_InpatientCase_WishesContactFromAnIMHAField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DischargeCloseDateField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DischargeCloseDateField);
            string fieldText = this._app.Query(_InpatientCase_DischargeCloseDateField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_DischargedToHomAddressField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_DischargedToHomAddressField);
            string fieldText = this._app.Query(_InpatientCase_DischargedToHomAddressField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }




        public CasePage ValidateInpatientCase_PropertyNameield(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PropertyNameield);
            string fieldText = this._app.Query(_InpatientCase_PropertyNameield).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PropertyNoField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PropertyNoField);
            string fieldText = this._app.Query(_InpatientCase_PropertyNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_StreetField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_StreetField);
            string fieldText = this._app.Query(_InpatientCase_StreetField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_VlgField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_VlgField);
            string fieldText = this._app.Query(_InpatientCase_VlgField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PropertyTypeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PropertyTypeField);
            string fieldText = this._app.Query(_InpatientCase_PropertyTypeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_TownField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_TownField);
            string fieldText = this._app.Query(_InpatientCase_TownField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_CountryField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CountryField);
            string fieldText = this._app.Query(_InpatientCase_CountryField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_UPRNField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_UPRNField);
            string fieldText = this._app.Query(_InpatientCase_UPRNField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_BoroughField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_BoroughField);
            string fieldText = this._app.Query(_InpatientCase_BoroughField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_CountyField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_CountyField);
            string fieldText = this._app.Query(_InpatientCase_CountyField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_TempAddressWardField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_TempAddressWardField);
            string fieldText = this._app.Query(_InpatientCase_TempAddressWardField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }
        public CasePage ValidateInpatientCase_PostCodeField(string ExpectedFieldText)
        {
            ScrollToElement(_InpatientCase_PostCodeField);
            string fieldText = this._app.Query(_InpatientCase_PostCodeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);
            return this;
        }

        #endregion




        public CasePage ValidateCreatedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdOnFooterLabel);

            string fieldText = this._app.Query(_createdOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateCreatedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdByFooterLabel);

            string fieldText = this._app.Query(_createdByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateModifiedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedOnFooterLabel);

            string fieldText = this._app.Query(_modifiedOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public CasePage ValidateModifiedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedByFooterLabel);

            string fieldText = this._app.Query(_modifiedByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        private string GetFullBirthDateText(DateTime DateOfBirth)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            DateTime currentDate = DateTime.Now.Date;

            TimeSpan span = currentDate - DateOfBirth;

            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int years = (zeroTime + span).Year - 1;
            int month = (zeroTime + span).Month - 1;


            if ((zeroTime + span).Year >= 18)
            {
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                return string.Format("{0} ({1} Years)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years);
            }
            else
            {
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                if (month == 1)
                {
                    return string.Format("{0} ({1} Years, {2} Month)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years, month);
                }
                else
                {
                    return string.Format("{0} ({1} Years, {2} Months)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years, month);
                }
            }



        }
    }
}

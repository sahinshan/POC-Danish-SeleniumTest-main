using CareDirector.Sdk.ServiceResponse;
using Phoenix.DBHelper.Models;

namespace Phoenix.DBHelper
{
    public class DatabaseHelper : BaseClass
    {
        public AuthenticateResponse authenticationResponse;

        #region Internal Variables

        internal Models.CPSystemUserShiftPayment _cpSystemUserShiftPayment;
        internal Models.CareProviderPayrollBatch _careProviderPayrollBatch;
        internal Models.CPPersonPainManagement _cpPersonPainManagement;
        internal Models.TypeOfEmotionalSupport _typeOfEmotionalSupport;
        internal Models.CPPersonEmotional _cpPersonEmotional;
        internal Models.PersonDiaryEventType _personDiaryEventType;
        internal Models.CPPersonDiaryEvent _cpPersonDiaryEvent;
        internal Models.PersonActivities _personActivities;
        internal Models.CPPersonActivities _cpPersonActivities;
        internal Models.UrineColour _urineColour;
        internal Models.CPDailyPersonalCareAttachment _cpDailyPersonalCareAttachment;
        internal Models.PersonalCareBodyArea _personalCareBodyArea;
        internal Models.PersonalCareOralCare _personalCareOralCare;
        internal Models.PersonalCareClothes _personalCareClothes;
        internal Models.PersonalCareOther _personalCareOther;
        internal Models.PersonalCareWash _personalCareWash;
        internal Models.CareProviderCarePlanSkinCondition _careProviderCarePlanSkinCondition;
        internal Models.CPPersonPersonalCareDailyRecord _cpPersonPersonalCareDailyRecord;
        internal Models.CareProviderMasterPayArrangement _careProviderMasterPayArrangement;
        internal Models.CareProviderVATCodeSetup _careProviderVATCodeSetup;
        internal Models.CareProviderFinanceTransactionTrigger _careProviderFinanceTransactionTrigger;
        internal Models.CPExpressBookingResult _cpExpressBookingResult;
        internal Models.CareProviderContractServiceBandedRates _careProviderContractServiceBandedRates;
        internal Models.CareProviderBandRateSchedule _careProviderBandRateSchedule;
        internal Models.CareProviderBandRateType _careProviderBandRateType;
        internal Models.CPTimebandSet _cpTimebandSet;
        internal Models.CPTimeband _cpTimeband;
        internal Models.CareProviderFinanceInvoiceBatchSetup _careProviderFinanceInvoiceBatchSetup;
        internal Models.cpPersonalMoneyAccountDetailAttachment _cpPersonalMoneyAccountDetailAttachment;
        internal Models.CareProviderPersonalMoneyAccountAttachment _careProviderPersonalMoneyAccountAttachment;
        internal Models.CareProviderPersonalMoneyAccountEntryType _careProviderPersonalMoneyAccountEntryType;
        internal Models.CareProviderPersonalMoneyAccountDetail _careProviderPersonalMoneyAccountDetail;
        internal Models.CareProviderPersonalMoneyAccount _careProviderPersonalMoneyAccount;
        internal Models.CareProviderPersonalMoneyAccountType _careProviderPersonalMoneyAccountType;
        internal Models.MailMerge _mailMerge;
        internal Models.CareProviderContractServiceRatePeriod _careProviderContractServiceRatePeriod;
        internal Models.CPSuspendDebtorInvoicesReason _cpSuspendDebtorInvoicesReason;
        internal Models.CareProviderFinanceCode _careProviderFinanceCode;
        internal Models.CareProviderFinanceCodeLocation _careProviderFinanceCodeLocation;
        internal Models.ComplianceManagement _complianceManagement;
        internal Models.SocialWorkerChangeReason _socialWorkerChangeReason;
        internal Models.CareProviderPersonContractServiceEndReason _careProviderPersonContractServiceEndReason;
        internal Models.CPPersonAbsenceReason _cpPersonAbsenceReason;
        internal Models.CPRegularCareTaskSchedule _cpRegularCareTaskSchedule;
        internal Models.CPRegularCareTaskDiary _cpRegularCareTaskDiary;
        internal Models.CareProviderPersonContractService _careProviderPersonContractService;
        internal Models.CareProviderPersonContractEndReason _careProviderPersonContractEndReason;
        internal Models.ScheduleBookingToPeople _scheduleBookingToPeople;
        internal Models.MASHStatus _mashStatus;
        internal Models.DuplicateDetectionRule _duplicateDetectionRule;
        internal Models.LACReview _lacReview;
        internal Models.LACReviewType _lacReviewType;
        internal Models.PersonalMoneyAccountTransactionType _personalMoneyAccountTransactionType;
        internal Models.ContractEndReason _contractEndReason;
        internal Models.CaseRejectedReason _caseRejectedReason;
        internal Models.RTTEvent _rttEvent;
        internal Models.RTTWaitTime _rttWaitTime;
        internal Models.JobRoleType _jobRoleType;
        internal Models.RTTPathwaySetup _rttPathwaySetup;
        internal Models.RTTTreatmentStatus _rttTreatmentStatus;
        internal Models.ProviderComplaintNature _providerComplaintNature;
        internal Models.ProviderComplaintOutcome _providerComplaintOutcome;
        internal Models.ProviderComplaintStage _providerComplaintStage;
        internal Models.ProviderComplaintFeedBackType _providerComplaintFeedBackType;
        internal Models.ProviderComplaintFeedback _providerComplaintFeedback;
        internal Models.PersonAttendedEducationEstablishment _personAttendedEducationEstablishment;
        internal Models.CaseReopenReason _caseReopenReason;
        internal Models.RTTTreatmentFunctionCode _rttTreatmentFunctionCode;
        internal Models.CPFeeType _cpFeeType;
        internal Models.CPFeeSetup _cpFeeSetup;
        internal Models.CPFee _cpFee;
        internal Models.CPAllowance _cpAllowance;
        internal Models.Decision _decision;
        internal Models.CarerBatchGrouping _carerBatchGrouping;
        internal Models.CPCarerRateUnit _cpCarerRateUnit;
        internal Models.CPAllowanceSetup _cpAllowanceSetup;
        internal Models.CPAllowanceType _cpAllowanceType;
        internal Models.ApprovedCareType _approvedCareType;
        internal Models.CarerApprovalDecision _carerApprovalDecision;
        internal Models.InpatientBedType _inpatientBedType;
        internal Models.InpatientWardSpecialty _inpatientWardSpecialty;
        internal Models.AppointmentType _appointmentType;
        internal Models.InpatientDischargeDestination _inpatientDischargeDestination;
        internal Models.CaseClosureReason _caseClosureReason;
        internal Models.LACLegalStatusEndReason _lacLegalStatusEndReason;
        internal Models.LACPlacementProvider _lacPlacementProvider;
        internal Models.LACLocationCode _lacLocationCode;
        internal Models.LACPlacement _lacPlacement;
        internal Models.LACLegalStatus _lacLegalStatus;
        internal Models.LACLegalStatusReason _lacLegalStatusReason;
        internal Models.ProfessionType _professionType;
        internal Models.CaseActionType _caseActionType;
        internal Models.ReferralPriority _referralPriority;
        internal Models.ContactOutcome _contactOutcome;
        internal Models.DocumentPickList _documentPickList;
        internal Models.DocumentPickListValue _documentPickListValue;
        internal Models.EmploymentStatus _employmentStatus;
        internal Models.PersonEmployment _personEmployment;
        internal Models.EmploymentWeeklyHoursWorked _employmentWeeklyHoursWorked;
        internal Models.DataRestriction _dataRestriction;
        internal Models.ActivityCategory _activityCategory;
        internal Models.ActivitySubCategory _activitySubCategory;
        internal Models.ActivityOutcome _activityOutcome;
        internal Models.ActivityPriority _activityPriority;
        internal Models.ActivityReason _activityReason;
        internal Models.RecruitmentRequirementStaffRole _recruitmentRequirementStaffRole;
        internal Models.RecruitmentRequirement _recruitmentRequirement;
        internal Models.StaffRecruitmentItem _staffRecruitmentItem;
        internal Models.PaymentTypeCode _paymentTypeCode;
        internal Models.VATCode _vatCode;
        internal Models.ProviderBatchGrouping _providerBatchGrouping;
        internal Models.ServiceFinanceSettings _serviceFinanceSettings;
        internal Models.ServiceProvidedRateSchedule _serviceProvidedRateSchedule;
        internal Models.ServiceProvidedRatePeriod _serviceProvidedRatePeriod;
        internal Models.TableQuestionCell _tableQuestionCell;
        internal Models.MultiOptionAnswer _multiOptionAnswer;
        internal Models.UserTransportationSchedule _userTransportationSchedule;
        internal Models.ContactAttachment _contactAttachment;
        internal Models.ContactStatus _contactStatus;
        internal Models.ContactPresentingPriority _contactPresentingPriority;
        internal Models.ContactType _contactType;
        internal Models.Contact _contact;
        internal Models.HomeScreen _homeScreen;
        internal Models.UserDataView _userDataView;
        internal Models.UserFavouriteRecord _userFavouriteRecord;
        internal Models.RecurrencePattern _recurrencePattern;
        internal Models.HealthAppointmentLocationType _healthAppointmentLocationType;
        internal Models.BreakGlass _breakGlass;
        internal Models.SDEMap _sdeMap;
        internal Models.DocumentAnswerAudit _documentAnswerAudit;
        internal Models.AssessmentSectionQuestionComment _assessmentSectionQuestionComment;
        internal Models.AssessmentPrintRecord _assessmentPrintRecord;
        internal Models.CaseFormOutcome _caseFormOutcome;
        internal Models.Document _document;
        internal Models.AttachDocumentType _attachDocumentType;
        internal Models.AttachDocumentSubType _attachDocumentSubType;
        internal Models.PersonCaseNote _personCaseNote;
        internal Models.PersonPhysicalObservationCaseNote _personPhysicalObservationCaseNote;
        internal Models.FinancialAssessmentCaseNote _financialAssessmentCaseNote;
        internal Models.PhoneCall _phoneCall;
        internal Models.Email _email;
        internal Models.SMS _sms;
        internal Models.EmailTo _emailTo;
        internal Models.EmailCc _emailCc;
        internal Models.Appointment _appointment;
        internal Models.TeamRestrictedDataAccess _teamRestrictedDataAccess;
        internal Models.PersonSignificantEvent _personSignificantEvent;
        internal Models.CaseAttachment _caseAttachment;
        internal Models.CaseAction _caseAction;
        internal Models.PersonForm _personForm;
        internal Models.InvolvementRole _involvementRole;
        internal Models.PersonFormInvolvement _personFormInvolvement;
        internal Models.Person _person;
        internal Models.PersonAddress _personAddress;
        internal Models.PersonMHALegalStatus _personMHALegalStatus;
        internal Models.MHACourtDateOutcome _mhaCourtDateOutcome;
        internal Models.PersonRelationship _personRelationship;
        internal Models.Professional _professional;
        internal Models.ServiceProvision _serviceProvision;
        internal Models.ServiceProvisionCostPerWeek _serviceProvisionCostPerWeek;
        internal Models.FinancialAssessment _financialAssessment;
        internal Models.FinancialAssessmentCharge _financialAssessmentCharge;
        internal Models.FinancialAssessmentChargeDetail _financialAssessmentChargeDetail;
        internal Models.FinancialAssessmentChargeTotal _financialAssessmentChargeTotal;
        internal Models.ServiceProvisionStatus _serviceProvisionStatus;
        internal Models.FACalculationTrigger _FACalculationTrigger;
        internal Models.ScheduleSetup _scheduleSetup;
        internal Models.FAContribution _faContribution;
        internal Models.FAContributionException _faContributionException;
        internal Models.ChargingRuleSetup _chargingRuleSetup;
        internal Models.IncomeSupportSetup _incomeSupportSetup;
        internal Models.NonResidentialPolicyRateSetup _nonResidentialPolicyRateSetup;
        internal Models.ChargeforServicesSetup _chargeforServicesSetup;
        internal Models.FinancialDetailRateSetup _financialDetailRateSetup;
        internal Models.PersonFinancialDetail _personFinancialDetail;
        internal Models.FinancialDetailDisregard _financialDetailDisregard;
        internal Models.FinancialDetail _financialDetail;
        internal Models.FinancialDetailDisregardChargingRuleType _financialDetailDisregardChargingRuleType;
        internal Models.FinanceTransaction _financeTransaction;
        internal Models.FinanceInvoice _financeInvoice;
        internal Models.FinanceInvoiceBatch _financeInvoiceBatch;
        internal Models.FinanceExtract _financeExtract;
        internal Models.ScheduledJob _scheduledJob;
        internal Models.FinanceExtractSetup _financeExtractSetup;
        internal Models.TeamMember _teamMember;
        internal Models.SystemSetting _systemSetting;
        internal Models.Letter _letter;
        internal Models.PersonAlertAndHazard _personAlertAndHazard;
        internal Models.PersonAlertAndHazardReview _personAlertAndHazardReview;
        internal Models.Ethnicity _ethnicity;
        internal Models.MaritalStatus _maritalStatus;
        internal Models.Language _language;
        internal Models.ProductLanguage _productLanguage;
        internal Models.AddressPropertyType _addressPropertyType;
        internal Models.Team _team;
        internal Models.PersonAllergy _personAllergy;
        internal Models.Case _Case;
        internal Models.CaseInvolvement _CaseInvolvement;
        internal Models.CaseStatusHistory _CaseStatusHistory;
        internal Models.CaseForm _caseForm;
        internal Models.HealthAppointment _healthAppointment;
        internal Models.Task _task;
        internal Models.HealthAppointmentAdditionalProfessional _healthAppointmentAdditionalProfessional;
        internal Models.HealthAppointmentCaseNote _healthAppointmentCaseNote;
        internal Models.AdultSafeguarding _AdultSafeguarding;
        internal Models.Allegation _allegation;
        internal Models.AllegationInvestigator _allegationInvestigator;
        internal Models.ChildProtection _ChildProtection;
        internal Models.LACEpisode _LACEpisode;
        internal Models.PersonLACLegalStatus _PersonLACLegalStatus;
        internal Models.MergedRecord _MergedRecord;
        internal Models.EmailAttachment _EmailAttachment;
        internal Models.CPIS _cpis;
        internal Models.UserSecurityProfile _userSecurityProfile;
        internal Models.TeamSecurityProfile _teamSecurityProfile;
        internal Models.SecurityProfile _securityProfile;
        internal Models.Audit _audit;
        internal Models.AuditChangeSet _auditChangeSet;
        internal Models.Audit_2019 _audit_2019;
        internal Models.AuditChangeSet_2019 _auditChangeSet_2019;
        internal Models.DocumentAnswer _documentAnswer;
        internal Models.DocumentAnswerChecklist _documentAnswerChecklist;
        internal Models.AssessmentSection _assessmentSection;
        internal Models.Automated_UI_Test_Document_1 _automated_UI_Test_Document_1;
        internal Models.FileDestruction _fileDestruction;
        internal Models.Workflow _workflow;
        internal Models.WorkflowJob _workflowJob;
        internal Models.UserRestrictedDataAccess _userRestrictedDataAccess;
        internal Models.SystemDashboard _systemDashboard;
        internal Models.BusinessObjectDashboard _businessObjectDashboard;
        internal Models.UserDashboard _userDashboard;
        internal Models.FAQ _faq;
        internal Models.FAQCategory _faqCategory;
        internal Models.Application _application;
        internal Models.ApplicationComponent _applicationComponent;
        internal Models.Website _website;
        internal Models.WebsitePage _websitePage;
        internal Models.WebsiteSetting _websiteSetting;
        internal Models.WebsiteAnnouncement _websiteAnnouncement;
        internal Models.WebsiteUserPin _websiteUserPin;
        internal Models.WebsiteUserPasswordReset _websiteUserPasswordReset;
        internal Models.WebsiteUserEmailVerification _websiteUserEmailVerification;
        internal Models.WebsiteUserPasswordHistory _websiteUserPasswordHistory;
        internal Models.WebsiteUser _websiteUser;
        internal Models.WebsiteSitemap _websiteSitemap;
        internal Models.WebsitePointOfContact _websitePointOfContact;
        internal Models.WebsiteFeedback _websiteFeedback;
        internal Models.WebsiteContact _websiteContact;
        internal Models.LocalizedString _localizedString;
        internal Models.LocalizedStringValue _localizedStringValue;
        internal Models.UserChart _userChart;
        internal Models.DocumentApplicationAccess _documentApplicationAccess;
        internal Models.DocumentSectionApplicationAccess _documentSectionApplicationAccess;
        internal Models.DocumentSection _documentSection;
        internal Models.DocumentSectionQuestionApplicationAccess _documentSectionQuestionApplicationAccess;
        internal Models.DocumentSectionQuestion _documentSectionQuestion;
        internal Models.PersonAttachment _personAttachment;
        internal Models.DocumentFile _documentFile;
        internal Models.PortalTask _portalTask;
        internal Models.Nationality _nationality;
        internal Models.PersonFormCaseNote _personFormCaseNote;
        internal Models.CaseFormCaseNote _caseFormCaseNote;
        internal Models.CaseCaseNote _caseCaseNote;
        internal Models.EDMSRepository _edmsRepository;
        internal Models.DocumentQuestionIdentifier _documentQuestionIdentifier;
        internal Models.DocumentPrintTemplate _documentPrintTemplate;
        internal Models.DocumentPrintTemplateLinkedApplication _documentPrintTemplateLinkedApplication;
        internal Models.DuplicateRecord _duplicateRecord;
        internal Models.SubordinateDuplicate _subordinateDuplicate;
        internal Models.BrokerageEpisode _brokerageEpisode;
        internal Models.WebsiteMessage _websiteMessage;
        internal Models.PersonConsentToTreatment _personConsentToTreatment;
        internal Models.WebsiteHandler _websiteHandler;
        internal Models.WebsiteLocalizedString _websiteLocalizedString;
        internal Models.WebsiteLocalizedStringValue _websiteLocalizedStringValue;
        internal Models.WebsitePageFile _websitePageFile;
        internal Models.WebsiteRecordType _websiteRecordType;
        internal Models.WebsiteResourceFile _websiteResourceFile;
        internal Models.WebsiteSecurityProfile _websiteSecurityProfile;
        internal Models.WebsiteSplashScreen _websiteSplashScreen;
        internal Models.WebsiteSplashScreenItem _websiteSplashScreenItem;
        internal Models.WebsiteOnDemandWorkflow _websiteOnDemandWorkflow;
        internal Models.CaseFormAttachment _caseFormAttachment;
        internal Models.DuplicateDetectionCondition _duplicateDetectionCondition;
        internal Models.ProviderForm _providerForm;
        internal Models.Provider _provider;
        internal Models.StaffMembership _staffMembership;
        internal Models.MASHEpisodeForm _mashEpisodeForm;
        internal Models.MashEpisode _mashEpisode;
        internal Models.ImageFile _imageFile;
        internal Models.AppointmentRequiredAttendee _appointmentRequiredAttendee;
        internal Models.AppointmentOptionalAttendee _appointmentOptionalAttendee;
        internal Models.PersonPrimarySupportReason _personPrimarySupportReason;
        internal Models.PersonChronology _personChronology;
        internal Models.SignificantEventSubCategoryPersonChronology _significantEventSubCategoryPersonChronology;
        internal Models.SignificantEventCategoryPersonChronology _significantEventCategoryPersonChronology;
        internal Models.PersonChronologySnapshot _personChronologySnapshot;
        internal Models.AdoptionLink _adoptionLink;
        internal Models.PersonClinicalRiskFactor _personClinicalRiskFactor;
        internal Models.PersonClinicalRiskFactorHistory _personClinicalRiskFactorHistory;
        internal Models.PersonHealthProfessional _personHealthProfessional;
        internal Models.PersonHealthDetail _personHealthDetail;
        internal Models.PersonFormOutcome _personFormOutcome;
        internal Models.HealthIssueType _healthIssueType;
        internal Models.PersonGestationPeriod _personGestationPeriod;
        internal Models.PersonDisabilityImpairments _personDisabilityImpairments;
        internal Models.RecurringAppointment _recurringAppointment;
        internal Models.RecurringAppointmentRequiredAttendee _recurringAppointmentRequiredAttendee;
        internal Models.RecurringAppointmentOptionalAttendee _recurringAppointmentOptionalAttendee;
        internal Models.PersonDNAR _personDNAR;
        internal Models.PersonPhysicalObservation _personPhysicalObservation;
        internal Models.SystemUser _systemUser;
        internal Models.UserWorkSchedule _userWorkSchedule;
        internal Models.UserDairy _userDairy;
        internal Models.AuthorisationLevel _authorisationLevel;
        internal Models.PersonImmunisation _personImmunisation;
        internal Models.ProviderAddress _providerAddress;
        internal Models.InpatientWard _inpatientWard;
        internal Models.InpatientBay _inpatientBay;
        internal Models.InpatientBed _inpatientBed;
        internal Models.InpatientBedStatusHistory _inpatientBedStatusHistory;
        internal Models.InpatientConsultantEpisode _inpatientConsultantEpisode;
        internal Models.PersonLanguage _personLanguage;
        internal Models.PersonNameHistory _personNameHistory;
        internal Models.PersonAllergicReaction _personAllergicReaction;
        internal Models.BusinessModule _businessModule;
        internal Models.BrokerageOffer _brokerageOffer;
        internal Models.AwaitingCommunicationFromBrokerageOffer _awaitingCommunicationFromBrokerageOffer;
        internal Models.BrokerageOfferCommunication _brokerageOfferCommunication;
        internal Models.BrokerageEpisodeEscalation _brokerageEpisodeEscalation;
        internal Models.CaseFormMember _caseFormMember;
        internal Models.CaseFormAssessmentFactor _caseFormAssessmentFactor;
        internal Models.BrokerageEpisodePausePeriod _brokerageEpisodePausePeriod;
        internal Models.BrokerageEpisodeAttachment _brokerageEpisodeAttachment;
        internal Models.BrokerageOfferAttachment _brokerageOfferAttachment;
        internal Models.ServiceProvisionStartReason _serviceProvisionStartReason;
        internal Models.ServiceProvisionEndReason _serviceProvisionEndReason;
        internal Models.ServiceDelivery _serviceDelivery;
        internal Models.SystemUserLanguage _systemUserLanguage;
        internal Models.SystemUserAddress _systemUserAddress;
        internal Models.UserApplication _userApplication;
        internal Models.SystemUserAlias _systemUserAlias;
        internal Models.BusinessUnit _businessUnit;
        internal Models.CarePhysicalLocation _carePhysicalLocation;
        internal Models.CareEquipment _careEquipment;
        internal Models.CPPersonDayNightCheckObservations _cPPersonDayNightCheckObservations;
        internal Models.MobilityDistanceUnit _mobilityDistanceUnit;
        internal Models.CareAssistanceNeeded _careAssistanceNeeded;
        internal Models.CareWellbeing _careWellbeing;
        internal Models.SpecialistMattress _specialistmattress;
        internal Models.CPPersonMobility _cPPersonMobility;
        internal Models.CPPersonTurning _cPPersonTurning;
        internal Models.CareProviderPersonHandoverDetail _careProviderPersonHandoverDetail;
        internal Models.ContactReason _contactReason;
        internal Models.ContactSource _contactSource;
        internal Models.DocumentCategory _documentCategory;
        internal Models.DocumentType _documentType;
        internal Models.QuestionCatalogue _questionCatalogue;
        internal Models.BusinessObject _businessObject;
        internal Models.CaseStatus _caseStatus;
        internal Models.DataForm _dataForm;
        internal Models.AuthenticationProvider _authenticationProvider;
        internal Models.SystemUserAliasType _systemUserAliasType;
        internal Models.DemographicsTitle _demographicsTitle;
        internal Models.TransportType _transportType;
        internal Models.StaffReview _staffReview;
        internal Models.StaffReviewAttachment _staffReviewAttachment;
        internal Models.SystemUserEmploymentContract _systemUserEmploymentContract;
        internal Models.SystemUserEmploymentContractCPBookingType _systemUserEmploymentContractCPBookingType;
        internal Models.CareProviderContractScheme _careProviderContractScheme;
        internal Models.SystemUserEmploymentContractTeam _systemUserEmploymentContractTeam;
        internal Models.CPBookingSchedule _cpBookingSchedule;
        internal Models.CPExpressBookingCriteria _cpExpressBookingCriteria;
        internal Models.CPExpressBookingProcessed _cpExpressBookingProcessed;
        internal Models.CPBookingScheduleStaff _cpBookingScheduleStaff;
        internal Models.AddressGazetteer _addressGazetteer;
        internal Models.AddressBorough _addressBorough;
        internal Models.AddressWard _addressWard;
        internal Models.SystemUserEmergencyContacts _systemUserEmergencyContacts;
        internal Models.CareProviderStaffRoleType _careProviderStaffRoleType;
        internal Models.EmploymentContractType _employmentContractType;
        internal Models.StaffReviewRequirement _staffReviewRequirement;
        internal Models.StaffReviewSetup _staffReviewSetup;
        internal Models.ServiceElement1 _serviceElement1;
        internal Models.ServiceElement2 _serviceElement2;
        internal Models.ServiceElement3 _serviceElement3;
        internal Models.FinanceClientCategory _financeClientCategory;
        internal Models.GLCodeLocation _glCodeLocation;
        internal Models.GLCode _glCode;
        internal Models.RateUnit _rateUnit;
        internal Models.ServiceProvided _serviceProvided;
        internal Models.CurrentRanking _currentRanking;
        internal Models.PlacementRoomType _placementRoomType;
        internal Models.FinancialAssessmentStatus _financialAssessmentStatus;
        internal Models.ChargingRuleType _chargingRuleType;
        internal Models.IncomeSupportType _incomeSupportType;
        internal Models.IncomeSupportTypeChargingRuleTypes _incomeSupportTypeChargingRuleTypes;
        internal Models.FinanceScheduleType _financeScheduleType;
        internal Models.FinancialAssessmentType _financialAssessmentType;
        internal Models.ContributionType _contributionType;
        internal Models.RecoveryMethod _recoveryMethod;
        internal Models.DebtorBatchGrouping _debtorBatchGrouping;
        internal Models.CommunityAndClinicTeam _communityAndClinicTeam;
        internal Models.CommunityClinicDiaryViewSetup _communityClinicDiaryViewSetup;
        internal Models.ContactAdministrativeCategory _contactAdministrativeCategory;
        internal Models.CaseServiceTypeRequested _caseServiceTypeRequested;
        internal Models.HealthAppointmentContactType _healthAppointmentContactType;
        internal Models.HealthAppointmentReason _healthAppointmentReason;
        internal Models.LanguageFluency _languageFluency;
        internal Models.SystemUserSuspension _systemUserSuspension;
        internal Models.InpatientAdmissionSource _inpatientAdmissionSource;
        internal Models.InpatientAdmissionMethod _inpatientAdmissionMethod;
        internal Models.SecondaryCaseReason _secondaryCaseReason;
        internal Models.Diagnosis _diagnosis;
        internal Models.PersonDiagnosis _personDiagnosis;
        internal Models.PersonDiagnosisEndReason _personDiagnosisEndReason;
        internal Models.CarePlanType _carePlanType;
        internal Models.PersonCarePlan _personCarePlan;
        internal Models.PersonCarePlanNeed _personCarePlanNeed;
        internal Models.PersonCarePlanForm _personCarePlanForm;
        internal Models.CarePlanNeedDomain _CarePlanNeedDomain;
        internal Models.CarePlanGoalType _CarePlanGoalType;
        internal Models.CarePlanInterventionType _CarePlanInterventionType;
        internal Models.PersonCarePlanGoal _personcareplangoal;
        internal Models.PersonCarePlanIntervention _personcareplanintervention;
        internal Models.InpatientLeaveAwol _inpatientLeaveAwol;
        internal Models.InpatientLeaveCancellationReason _inpatientLeaveCancellationReason;
        internal Models.InpatientLeaveType _inpatientLeaveType;
        internal Models.PersonAbsenceType _personAbsenceType;
        internal Models.OpenEndedAbsence _openendedabsence;
        internal Models.MissingPerson _missingPerson;
        internal Models.InpatientLeaveEndReason _inpatientLeaveEndReason;
        internal Models.InpatientLeaveAwolAttachment _inpatientLeaveAwolAttachment;
        internal Models.InpatientBedOccupancyHistory _inpatientBedOccupancyHistory;
        internal Models.InpatientSeclusion _inpatientSeclusion;
        internal Models.InpatientSeclusionReason _inpatientSeclusionReason;
        internal Models.inpatientSeclusionReview _inpatientSeclusionReview;
        internal Models.InpatientSeclusionAttachment _inpatientSeclusionAttachment;
        internal Models.StaffReviewForm _StaffReviewForm;
        internal Models.OrganisationalRiskType _organisationalRiskType;
        internal Models.OrganisationalRisk _organisationalRisk;
        internal Models.OrganisationalRiskActionPlan _organisationalRiskActionPlan;
        internal Models.CPBookingType _cPBookingType;
        internal Models.CPSchedulingSetup _cPSchedulingSetup;
        internal Models.CPPersonAbsenceReason _cPPersonAbsenceReason;
        internal Models.CPPersonAbsence _cpPersonAbsence;
        internal Models.ProviderAllowableBookingTypes _providerAllowableBookingTypes;
        internal Models.OrganisationalRiskCategory _organisationalRiskCategory;
        internal Models.BusinessObjectField _businessObjectField;
        internal Models.AboutMeSetup _aboutMeSetup;
        internal Models.PersonAboutMe _personAboutMe;
        internal Models.CareproviderReportableEvent _careproviderReportableEvent;
        internal Models.CareproviderReportableEventRole _careproviderReportableEventRole;
        internal Models.CareproviderReportableEventCategory _careproviderReportableEventCategory;
        internal Models.CareproviderReportableEventSubCategory _careproviderReportableEventSubCategory;
        internal Models.CareproviderReportableEventType _careproviderReportableEventType;
        internal Models.CareproviderReportableEventSeverity _careproviderReportableEventSeverity;
        internal Models.CareproviderReportableEventInjurySeverity _careproviderReportableEventInjurySeverity;
        internal Models.CareproviderReportableEventStatus _careproviderReportableEventStatus;
        internal Models.CareProviderReportableEventAttachment _careProviderReportableEventAttachment;
        internal Models.CareproviderReportableEventAction _careproviderReportableEventAction;
        internal Models.CPReportableEventBehaviourActionType _CPReportableEventBehaviourActionType;
        internal Models.CareproviderReportableEventBehaviourType _careproviderReportableEventBehaviourType;
        internal Models.CareproviderReportableEventBehaviour _careproviderReportableEventBehaviour;
        internal Models.CareproviderReportableEventImpact _careproviderReportableEventImpact;
        internal Models.ChildProtectionCategoryOfAbuse _childProtectionCategoryOfAbuse;
        internal Models.ChildProtectionStatusType _childProtectionStatusType;
        internal Models.ChildProtectionEndReasonType _childProtectionEndReasonType;
        internal Models.PersonRelationshipType _personRelationshipType;

        internal Models.ProviderRoom _providerRoom;
        internal Models.ClinicRoom _clinicRoom;
        internal Models.CommunityClinicLinkedProfessional _communityClinicLinkedProfessional;
        internal Models.ClinicSlot _clinicSlot;
        internal Models.HealthAppointmentAttendeeAdvocateType _healthAppointmentAttendeeAdvocateType;
        internal Models.HealthAppointmentOutcomeType _healthAppointmentOutcomeType;
        internal Models.CommunityClinicCareIntervention _communityClinicCareIntervention;
        internal Models.HealthAppointmentAttendingAdvocate _healthAppointmentAttendingAdvocate;
        internal Models.ClinicBookedSlot _clinicBookedSlot;
        internal Models.Applicant _applicant;
        internal Models.RecruitmentRoleApplicant _recruitmentRoleApplicant;
        internal Models.AvailabilityTypes _availabilityTypes;
        internal Models.RateType _rateType;
        internal Models.ServiceMapping _serviceMapping;
        internal Models.CareType _careType;
        internal Models.ServiceElement1ValidRateUnits _serviceElement1ValidRateUnits;
        internal Models.ServiceGLCoding _serviceGLCoding;
        internal Models.SystemUserSuspensionContract _systemUserSuspensionContract;
        internal Models.SystemUserSuspensionReason _systemUserSuspensionReason;
        internal Models.ApplicationSource _applicationSource;
        internal Models.RejectedReason _rejectedReason;
        internal Models.Compliance _compliance;
        internal Models.StaffTrainingItem _staffTrainingItem;
        internal Models.TrainingRequirementSetup _trainingRequirementSetup;
        internal Models.TrainingRequirement _trainingRequirement;
        internal Models.SystemUserTraining _systemUserTraining;
        internal Models.ApplicantLanguage _applicantLanguage;
        internal Models.ApplicantAlias _applicantAlias;
        internal Models.ComplianceItemAttachment _complianceItemAttachment;
        internal Models.RequirementLastChasedOutcome _requirementLastChasedOutcome;
        internal Models.SignificantEventCategory _significantEventCategory;
        internal Models.OptionSet _optionSet;
        internal Models.OptionsetValue _optionsetValue;
        internal Models.ExtractName _extractName;
        internal Models.InvoiceBy _invoiceBy;
        internal Models.InvoiceFrequency _invoiceFrequency;
        internal Models.FinanceInvoiceBatchSetup _financeInvoiceBatchSetup;
        internal Models.FormCancellationReason _formCancellationReason;
        internal Models.PersonTargetGroup _personTargetGroup;
        internal Models.ServicePermission _servicePermission;
        internal Models.PersonalBudgetType _personalBudgetType;
        internal Models.CareProviderRateUnit _careProviderRateUnit;
        internal Models.CareProviderService _careProviderService;
        internal Models.CareProviderBatchGrouping _careProviderBatchGrouping;
        internal Models.CareProviderCarePeriodSetUp _careProviderCarePeriodSetUp;
        internal Models.CareProviderVatCode _careProviderVatCode;
        internal Models.CareProviderPersonContract _careProviderPersonContract;
        internal Models.CareProviderContractService _careProviderContractService;
        internal Models.CPBookingDiary _cPBookingDiary;
        internal Models.CPBookingRegularCareTask _cPBookingRegularCareTask;
        internal Models.CPBookingRegularCareTaskStaff _cPBookingRegularCareTaskStaff;
        internal Models.CPBookingDiaryStaff _cPBookingDiaryStaff;
        internal Models.DiaryBookingToPeople _diaryBookingToPeople;
        internal Models.BrokerageRequestSource _brokerageRequestSource;
        internal Models.BrokerageEpisodePriority _brokerageEpisodePriority;
        internal Models.BrokerageEpisodePauseReason _brokerageEpisodePauseReason;
        internal Models.BrokerageEpisodeTrackingStatus _brokerageEpisodeTrackingStatus;
        internal Models.BrokerageTargetSetup _brokerageTargetSetup;
        internal Models.BrokerageTargetTrackingStatusSetup _brokerageTargetTrackingStatusSetup;
        internal Models.BrokerageOfferRejectionReason _brokerageOfferRejectionReason;
        internal Models.GLCodeMapping _glCodeMapping;
        internal Models.BrokerageCarePackageType _brokerageCarePackageType;
        internal Models.BrokerageOfferCancellationReason _brokerageOfferCancellationReason;
        internal Models.BrokerageOfferAwaitingCommunicationFrom _brokerageOfferAwaitingCommunicationFrom;
        internal Models.BrokerageOfferCommunicationOutcome _brokerageOfferCommunicationOutcome;
        internal Models.ContactMethod _contactMethod;
        internal Models.BrokerageExistingCarePackage _brokerageExistingCarePackage;
        internal Models.BankHoliday _bankHoliday;
        internal Models.BrokerageEpisodeRejectionReason _brokerageEpisodeRejectionReason;
        internal Models.BrokerageEpisodeCancellationReason _brokerageEpisodeCancellationReason;
        internal Models.CaseFormOutcomeType _caseFormOutcomeType;
        internal Models.AssessmentFactorType _assessmentFactorType;
        internal Models.SignificantEventSubCategory _significantEventSubCategory;
        internal Models.InpatientLeaveAwolCaseNote _inpatientLeaveAwolCaseNote;
        internal Models.CasePriority _casePriority;
        internal Models.FosteringExperience _fosteringExperience;
        internal Models.ChildInNeedCode _childInNeedCode;
        internal Models.ErrorManagementReason _errorManagementReason;
        internal Models.FormDelayReason _formDelayReason;
        internal Models.PersonFormAttachment _personFormAttachment;
        internal Models.DocumentOutcomeType _documentOutcomeType;
        internal Models.ServiceProvisionRatePeriod _serviceProvisionRatePeriod;
        internal Models.ServiceProvisionRateSchedule _serviceProvisionRateSchedule;
        internal Models.DocumentBusinessObjectMapping _documentBusinessObjectMapping;
        internal Models.CommunityClinicRestriction _communityClinicRestriction;
        internal Models.ContactCaseNote _contactCaseNote;
        internal Models.MHASectionSetup _mhaSectionSetup;
        internal Models.MHACourtDateOutcomeCaseNote _mhaCourtDateOutcomeCaseNote;
        internal Models.PersonMHALegalStatusCaseNote _personMHALegalStatusCaseNote;
        internal Models.ClinicalRiskFactorType _clinicalRiskFactorType;
        internal Models.ClinicalRiskFactorSubType _clinicalRiskFactorSubType;
        internal Models.ClinicalRiskfactorEndReason _clinicalRiskfactorEndReason;
        internal Models.ClinicalRiskLevel _clinicalRiskLevel;
        internal Models.GestationPeriodEndReason _gestationPeriodEndReason;
        internal Models.PersonCarePlanCaseNote _personCarePlanCaseNote;
        internal Models.ClinicalRiskStatus _clinicalRiskStatus;
        internal Models.PersonClinicalRiskStatus _personClinicalRiskStatus;
        internal Models.PersonClinicalRiskStatusCaseNote _personClinicalRiskStatusCaseNote;
        internal Models.PersonHeightAndWeight _personHeightAndWeight;
        internal Models.PersonHeightAndWeightCaseNote _personHeightAndWeightCaseNote;
        internal Models.DisabilityType _disabilityType;
        internal Models.ImpairmentType _impairmentType;
        internal Models.ImmunisationType _immunisationType;
        internal Models.ServicePackage _servicePackage;
        internal Models.ServicePackageType _servicePackageType;
        internal Models.ServiceUprate _serviceUprate;
        internal Models.ServiceUprateDetail _serviceUprateDetail;
        internal Models.DebtorHeaderText _debtorHeaderText;
        internal Models.DebtorTransactionText _debtorTransactionText;
        internal Models.DebtorRecoveryText _debtorRecoveryText;
        internal Models.AlertAndHazardType _alertAndHazardType;
        internal Models.AlertAndHazardEndReason _alertAndHazardEndReason;
        internal Models.AlertAndHazardReviewOutcome _alertAndHazardReviewOutcome;
        internal Models.AdultSafeguardingCategoryOfAbuse _adultSafeguardingCategoryOfAbuse;
        internal Models.AdultSafeguardingStatus _adultSafeguardingStatus;
        internal Models.AllegationCategory _allegationCategory;
        internal Models.PrimarySupportReasonType _primarySupportReasonType;
        internal Models.PersonFormOutcomeType _personFormOutcomeType;
        internal Models.PrivateFosteringArrangement _privateFosteringArrangement;
        internal Models.PrivateFosteringArrangementCaseNote _privateFosteringArrangementCaseNote;
        internal Models.MHARightsAndRequests _mhaRightsAndRequests;
        internal Models.MHARightsAndRequestsCaseNote _mhaRightsAndRequestsCaseNote;
        internal Models.MHAAftercareEntitlement _mhaAftercareEntitlement;
        internal Models.MHAAftercareEntitlementCaseNote _mhaAftercareEntitlementCaseNote;
        internal Models.FinanceGeneralSettings _financeGeneralSettings;
        internal Models.FinanceInvoiceStatus _financeInvoiceStatus;
        internal Models.FinanceTransactionTrigger _financeTransactionTrigger;
        internal Models.RegularCareTask _regularCareTask;
        internal Models.CareTask _careTask;
        internal Models.ProtectiveMarkingScheme _protectiveMarkingScheme;
        internal Models.PersonSignificantEventChronology _personSignificantEventChronology;
        internal Models.PersonMhaAppeal _personMhaAppeal;
        internal Models.MhaRecordOfAppeal _mhaRecordOfAppeal;
        internal Models.MhaRecordOfAppealCaseNote _mhaRecordOfAppealCaseNote;
        internal Models.AccommodationType _accommodationType;
        internal Models.LowerSuperOutputArea _lowerSuperOutputArea;
        internal Models.ModeOfCommunication _modeOfCommunication;
        internal Models.PersonDocumentFormat _personDocumentFormat;
        internal Models.ImmigrationStatus _immigrationStatus;
        internal Models.Religion _religion;
        internal Models.Country _country;
        internal Models.LeavingCareEligibility _leavingCareEligibility;
        internal Models.UPNUnknownReason _upnUnknownReason;
        internal Models.SexualOrientation _sexualOrientation;
        internal Models.PersonCarePlanReview _personCarePlanReview;
        internal Models.CarePlanReviewOutcome _carePlanReviewOutcome;
        internal Models.InpatientConsultantEpisodeEndReason _inpatientConsultantEpisodeEndReason;
        internal Models.CareProviderStaffRoleTypeGroup _careProviderStaffRoleTypeGroup;
        internal Models.CPBankHolidayChargingCalendar _cpBankHolidayChargingCalendar;
        internal Models.CPBankHolidayDate _cpBankHolidayDate;
        internal Models.CareProviderBankHolidayType _careProviderBankHolidayType;
        internal Models.Pronouns _pronouns;
        internal Models.CareProviderServiceMapping _careProviderServiceMapping;
        internal Models.CareProviderServiceDetail _careProviderServiceDetail;
        internal Models.CareProviderExtractName _careProviderExtractName;
        internal Models.CareProviderExtractType _careProviderExtractType;
        internal Models.CareProviderFinanceExtractBatchSetup _careProviderFinanceExtractBatchSetup;
        internal Models.CareProviderFinanceExtractBatch _careProviderFinanceExtractBatch;
        internal Models.CareProviderFinanceCodeMapping _careProviderFinanceCodeMapping;
        internal Models.CareProviderPersonContractServiceRatePeriod _careProviderPersonContractServiceRatePeriod;
        internal Models.CareProviderFinanceInvoiceBatch _careProviderFinanceInvoiceBatch;
        internal Models.CareProviderFinanceInvoice _careProviderFinanceInvoice;
        internal Models.CareProviderFinanceTransaction _careProviderFinanceTransaction;
        internal Models.CareProviderPersonContractServiceChargePerWk _careProviderPersonContractServiceChargePerWk;
        internal Models.CPBands _cPBands;
        internal Models.CPBookingScheduleDeletionReason _cpBookingScheduleDeletionReason;
        internal Models.CPBookingTypeClashAction _cpBookingTypeClashAction;
        internal Models.CPPersonDailyRecord _cPPersonDailyRecord;
        internal Models.CPBookingDiaryDeletionReason _cpBookingDiaryDeletionReason;
        internal Models.CareProviderChargeApportionment _careProviderChargeApportionment;
        internal Models.CareProviderChargeApportionmentDetail _careProviderChargeApportionmentDetail;
        internal Models.AllergyType _allergyType;
        internal Models.PaymentMethod _paymentMethod;
        internal Models.CareProviderFinanceInvoicePayment _careProviderFinanceInvoicePayment;
        internal Models.CareProviderAccountingPeriod _careProviderAccountingPeriod;
        internal Models.CarePlanAgreedBy _carePlanAgreedBy;
        internal Models.CareProviderFinanceInvoicePaymentReportType _careProviderFinanceInvoicePaymentReportType;
        internal Models.CPKeyworkerNotesAttachment _cpKeyworkerNotesAttachment;
        internal Models.CPPersonDayNightCheck _cpPersonDayNightCheck;
        internal Models.CPPersonToileting _cpPersonToileting;
        internal Models.CarePhysicalLocationDailyCare _carePhysicalLocationDailyCare;
        internal Models.CPPersonKeyworkerNote _cpPersonKeyworkerNote;
        internal Models.CpMobilityAttachment _cpMobilityAttachment;
        internal Models.CpPersonCarePreferences _cpPersonCarePreferences;
        internal Models.CpPersonBehaviourIncident _cpPersonBehaviourIncident;
        internal Models.IncidentTrigger _incidentTrigger;
        internal Models.PersonConversations _personConversations;
        internal Models.FoodAndFluidMealType _foodAndFluidMealType;
        internal Models.FoodAmountOffered _foodAmountOffered;
        internal Models.FoodAmountEaten _foodAmountEaten;
        internal Models.TypeOfFluid _typeOfFluid;
        internal Models.FluidAmountOffered _fluidAmountOffered;
        internal Models.NonOralFluidDelivery _nonOralFluidDelivery;
        internal Models.CpPersonFoodAndFluid _cpPersonFoodAndFluid;
        internal Models.CPPersonPersonalSafetyandEnvironment _cpPersonalSafetyandEnvironment;
        internal Models.MotionSensorType _motionSensorType;
        internal Models.ProviderHolidayYear _providerHolidayYear;

        #endregion

        #region Public Properties

        
        public Models.CPSystemUserShiftPayment cpSystemUserShiftPayment
        {
            get
            {
                if (_cpSystemUserShiftPayment == null)
                    _cpSystemUserShiftPayment = new Models.CPSystemUserShiftPayment(authenticationResponse);
                return _cpSystemUserShiftPayment;
            }
        }


        public Models.CareProviderPayrollBatch careProviderPayrollBatch
        {
            get
            {
                if (_careProviderPayrollBatch == null)
                    _careProviderPayrollBatch = new Models.CareProviderPayrollBatch(authenticationResponse);
                return _careProviderPayrollBatch;
            }
        }


        public Models.ProviderHolidayYear providerHolidayYear
        {
            get
            {
                if (_providerHolidayYear == null)
                    _providerHolidayYear = new Models.ProviderHolidayYear(authenticationResponse);
                return _providerHolidayYear;
            }
        }


        public Models.CPPersonPersonalSafetyandEnvironment cpPersonalSafetyandEnvironment
        {
            get
            {
                if (_cpPersonalSafetyandEnvironment == null)
                    _cpPersonalSafetyandEnvironment = new Models.CPPersonPersonalSafetyandEnvironment(authenticationResponse);
                return _cpPersonalSafetyandEnvironment;
            }
        }


        public Models.CPPersonPainManagement cpPersonPainManagement
        {
            get
            {
                if (_cpPersonPainManagement == null)
                    _cpPersonPainManagement = new Models.CPPersonPainManagement(authenticationResponse);
                return _cpPersonPainManagement;
            }
        }


        public Models.TypeOfEmotionalSupport typeOfEmotionalSupport
        {
            get
            {
                if (_typeOfEmotionalSupport == null)
                    _typeOfEmotionalSupport = new Models.TypeOfEmotionalSupport(authenticationResponse);
                return _typeOfEmotionalSupport;
            }
        }


        public Models.CPPersonEmotional cpPersonEmotional
        {
            get
            {
                if (_cpPersonEmotional == null)
                    _cpPersonEmotional = new Models.CPPersonEmotional(authenticationResponse);
                return _cpPersonEmotional;
            }
        }


        public Models.PersonDiaryEventType personDiaryEventType
        {
            get
            {
                if (_personDiaryEventType == null)
                    _personDiaryEventType = new Models.PersonDiaryEventType(authenticationResponse);
                return _personDiaryEventType;
            }
        }


        public Models.CPPersonDiaryEvent cpPersonDiaryEvent
        {
            get
            {
                if (_cpPersonDiaryEvent == null)
                    _cpPersonDiaryEvent = new Models.CPPersonDiaryEvent(authenticationResponse);
                return _cpPersonDiaryEvent;
            }
        }


        public Models.PersonActivities personActivities
        {
            get
            {
                if (_personActivities == null)
                    _personActivities = new Models.PersonActivities(authenticationResponse);
                return _personActivities;
            }
        }


        public Models.CPPersonActivities cpPersonActivities
        {
            get
            {
                if (_cpPersonActivities == null)
                    _cpPersonActivities = new Models.CPPersonActivities(authenticationResponse);
                return _cpPersonActivities;
            }
        }


        public Models.UrineColour urineColour
        {
            get
            {
                if (_urineColour == null)
                    _urineColour = new Models.UrineColour(authenticationResponse);
                return _urineColour;
            }
        }


        public Models.CPDailyPersonalCareAttachment cpDailyPersonalCareAttachment
        {
            get
            {
                if (_cpDailyPersonalCareAttachment == null)
                    _cpDailyPersonalCareAttachment = new Models.CPDailyPersonalCareAttachment(authenticationResponse);
                return _cpDailyPersonalCareAttachment;
            }
        }


        public Models.PersonalCareBodyArea personalCareBodyArea
        {
            get
            {
                if (_personalCareBodyArea == null)
                    _personalCareBodyArea = new Models.PersonalCareBodyArea(authenticationResponse);
                return _personalCareBodyArea;
            }
        }


        public Models.PersonalCareOralCare personalCareOralCare
        {
            get
            {
                if (_personalCareOralCare == null)
                    _personalCareOralCare = new Models.PersonalCareOralCare(authenticationResponse);
                return _personalCareOralCare;
            }
        }


        public Models.PersonalCareClothes personalCareClothes
        {
            get
            {
                if (_personalCareClothes == null)
                    _personalCareClothes = new Models.PersonalCareClothes(authenticationResponse);
                return _personalCareClothes;
            }
        }


        public Models.PersonalCareOther personalCareOther
        {
            get
            {
                if (_personalCareOther == null)
                    _personalCareOther = new Models.PersonalCareOther(authenticationResponse);
                return _personalCareOther;
            }
        }


        public Models.PersonalCareWash personalCareWash
        {
            get
            {
                if (_personalCareWash == null)
                    _personalCareWash = new Models.PersonalCareWash(authenticationResponse);
                return _personalCareWash;
            }
        }


        public Models.CareProviderCarePlanSkinCondition careProviderCarePlanSkinCondition
        {
            get
            {
                if (_careProviderCarePlanSkinCondition == null)
                    _careProviderCarePlanSkinCondition = new Models.CareProviderCarePlanSkinCondition(authenticationResponse);
                return _careProviderCarePlanSkinCondition;
            }
        }

        public Models.CarePhysicalLocationDailyCare carePhysicalLocationDailyCare
        {
            get
            {
                if (_carePhysicalLocationDailyCare == null)
                    _carePhysicalLocationDailyCare = new Models.CarePhysicalLocationDailyCare(authenticationResponse);
                return _carePhysicalLocationDailyCare;
            }
        }


        public Models.CPPersonPersonalCareDailyRecord cpPersonPersonalCareDailyRecord
        {
            get
            {
                if (_cpPersonPersonalCareDailyRecord == null)
                    _cpPersonPersonalCareDailyRecord = new Models.CPPersonPersonalCareDailyRecord(authenticationResponse);
                return _cpPersonPersonalCareDailyRecord;
            }
        }


        public Models.CareProviderMasterPayArrangement careProviderMasterPayArrangement
        {
            get
            {
                if (_careProviderMasterPayArrangement == null)
                    _careProviderMasterPayArrangement = new Models.CareProviderMasterPayArrangement(authenticationResponse);
                return _careProviderMasterPayArrangement;
            }
        }


        public Models.CareProviderVATCodeSetup careProviderVATCodeSetup
        {
            get
            {
                if (_careProviderVATCodeSetup == null)
                    _careProviderVATCodeSetup = new Models.CareProviderVATCodeSetup(authenticationResponse);
                return _careProviderVATCodeSetup;
            }
        }


        public Models.CareProviderFinanceTransactionTrigger careProviderFinanceTransactionTrigger
        {
            get
            {
                if (_careProviderFinanceTransactionTrigger == null)
                    _careProviderFinanceTransactionTrigger = new Models.CareProviderFinanceTransactionTrigger(authenticationResponse);
                return _careProviderFinanceTransactionTrigger;
            }
        }


        public Models.CPPersonDailyRecord cPPersonDailyRecord
        {
            get
            {
                if (_cPPersonDailyRecord == null)
                    _cPPersonDailyRecord = new Models.CPPersonDailyRecord(authenticationResponse);
                return _cPPersonDailyRecord;
            }
        }


        public Models.CPExpressBookingResult cpExpressBookingResult
        {
            get
            {
                if (_cpExpressBookingResult == null)
                    _cpExpressBookingResult = new Models.CPExpressBookingResult(authenticationResponse);
                return _cpExpressBookingResult;
            }
        }


        public Models.CareProviderContractServiceBandedRates careProviderContractServiceBandedRates
        {
            get
            {
                if (_careProviderContractServiceBandedRates == null)
                    _careProviderContractServiceBandedRates = new Models.CareProviderContractServiceBandedRates(authenticationResponse);
                return _careProviderContractServiceBandedRates;
            }
        }


        public Models.CareProviderBandRateSchedule careProviderBandRateSchedule
        {
            get
            {
                if (_careProviderBandRateSchedule == null)
                    _careProviderBandRateSchedule = new Models.CareProviderBandRateSchedule(authenticationResponse);
                return _careProviderBandRateSchedule;
            }
        }


        public Models.CareProviderBandRateType careProviderBandRateType
        {
            get
            {
                if (_careProviderBandRateType == null)
                    _careProviderBandRateType = new Models.CareProviderBandRateType(authenticationResponse);
                return _careProviderBandRateType;
            }
        }


        public Models.CPTimebandSet cpTimebandSet
        {
            get
            {
                if (_cpTimebandSet == null)
                    _cpTimebandSet = new Models.CPTimebandSet(authenticationResponse);
                return _cpTimebandSet;
            }
        }


        public Models.CPTimeband cpTimeband
        {
            get
            {
                if (_cpTimeband == null)
                    _cpTimeband = new Models.CPTimeband(authenticationResponse);
                return _cpTimeband;
            }
        }


        public Models.CareProviderFinanceInvoiceBatchSetup careProviderFinanceInvoiceBatchSetup
        {
            get
            {
                if (_careProviderFinanceInvoiceBatchSetup == null)
                    _careProviderFinanceInvoiceBatchSetup = new Models.CareProviderFinanceInvoiceBatchSetup(authenticationResponse);
                return _careProviderFinanceInvoiceBatchSetup;
            }
        }


        public Models.cpPersonalMoneyAccountDetailAttachment cpPersonalMoneyAccountDetailAttachment
        {
            get
            {
                if (_cpPersonalMoneyAccountDetailAttachment == null)
                    _cpPersonalMoneyAccountDetailAttachment = new Models.cpPersonalMoneyAccountDetailAttachment(authenticationResponse);
                return _cpPersonalMoneyAccountDetailAttachment;
            }
        }


        public Models.CareProviderPersonalMoneyAccountAttachment careProviderPersonalMoneyAccountAttachment
        {
            get
            {
                if (_careProviderPersonalMoneyAccountAttachment == null)
                    _careProviderPersonalMoneyAccountAttachment = new Models.CareProviderPersonalMoneyAccountAttachment(authenticationResponse);
                return _careProviderPersonalMoneyAccountAttachment;
            }
        }

        public Models.CareProviderPersonalMoneyAccountEntryType careProviderPersonalMoneyAccountEntryType
        {
            get
            {
                if (_careProviderPersonalMoneyAccountEntryType == null)
                    _careProviderPersonalMoneyAccountEntryType = new Models.CareProviderPersonalMoneyAccountEntryType(authenticationResponse);
                return _careProviderPersonalMoneyAccountEntryType;
            }
        }


        public Models.CareProviderPersonalMoneyAccountDetail careProviderPersonalMoneyAccountDetail
        {
            get
            {
                if (_careProviderPersonalMoneyAccountDetail == null)
                    _careProviderPersonalMoneyAccountDetail = new Models.CareProviderPersonalMoneyAccountDetail(authenticationResponse);
                return _careProviderPersonalMoneyAccountDetail;
            }
        }


        public Models.CareProviderPersonalMoneyAccountType careProviderPersonalMoneyAccountType
        {
            get
            {
                if (_careProviderPersonalMoneyAccountType == null)
                    _careProviderPersonalMoneyAccountType = new Models.CareProviderPersonalMoneyAccountType(authenticationResponse);
                return _careProviderPersonalMoneyAccountType;
            }
        }


        public Models.CareProviderPersonalMoneyAccount careProviderPersonalMoneyAccount
        {
            get
            {
                if (_careProviderPersonalMoneyAccount == null)
                    _careProviderPersonalMoneyAccount = new Models.CareProviderPersonalMoneyAccount(authenticationResponse);
                return _careProviderPersonalMoneyAccount;
            }
        }


        public Models.MailMerge mailMerge
        {
            get
            {
                if (_mailMerge == null)
                    _mailMerge = new Models.MailMerge(authenticationResponse);
                return _mailMerge;
            }
        }


        public Models.CareProviderContractServiceRatePeriod careProviderContractServiceRatePeriod
        {
            get
            {
                if (_careProviderContractServiceRatePeriod == null)
                    _careProviderContractServiceRatePeriod = new Models.CareProviderContractServiceRatePeriod(authenticationResponse);
                return _careProviderContractServiceRatePeriod;
            }
        }


        public Models.CPSuspendDebtorInvoicesReason cpSuspendDebtorInvoicesReason
        {
            get
            {
                if (_cpSuspendDebtorInvoicesReason == null)
                    _cpSuspendDebtorInvoicesReason = new Models.CPSuspendDebtorInvoicesReason(authenticationResponse);
                return _cpSuspendDebtorInvoicesReason;
            }
        }


        public Models.CareProviderFinanceCode careProviderFinanceCode
        {
            get
            {
                if (_careProviderFinanceCode == null)
                    _careProviderFinanceCode = new Models.CareProviderFinanceCode(authenticationResponse);
                return _careProviderFinanceCode;
            }
        }


        public Models.CareProviderFinanceCodeLocation careProviderFinanceCodeLocation
        {
            get
            {
                if (_careProviderFinanceCodeLocation == null)
                    _careProviderFinanceCodeLocation = new Models.CareProviderFinanceCodeLocation(authenticationResponse);
                return _careProviderFinanceCodeLocation;
            }
        }


        public Models.ComplianceManagement complianceManagement
        {
            get
            {
                if (_complianceManagement == null)
                    _complianceManagement = new Models.ComplianceManagement(authenticationResponse);
                return _complianceManagement;
            }
        }


        public Models.SocialWorkerChangeReason socialWorkerChangeReason
        {
            get
            {
                if (_socialWorkerChangeReason == null)
                    _socialWorkerChangeReason = new Models.SocialWorkerChangeReason(authenticationResponse);
                return _socialWorkerChangeReason;
            }
        }


        public Models.CareProviderPersonContractServiceEndReason careProviderPersonContractServiceEndReason
        {
            get
            {
                if (_careProviderPersonContractServiceEndReason == null)
                    _careProviderPersonContractServiceEndReason = new Models.CareProviderPersonContractServiceEndReason(authenticationResponse);
                return _careProviderPersonContractServiceEndReason;
            }
        }


        public Models.CPPersonAbsenceReason cpPersonAbsenceReason
        {
            get
            {
                if (_cpPersonAbsenceReason == null)
                    _cpPersonAbsenceReason = new Models.CPPersonAbsenceReason(authenticationResponse);
                return _cpPersonAbsenceReason;
            }
        }

        public Models.CPRegularCareTaskSchedule cpRegularCareTaskSchedule
        {
            get
            {
                if (_cpRegularCareTaskSchedule == null)
                    _cpRegularCareTaskSchedule = new Models.CPRegularCareTaskSchedule(authenticationResponse);
                return _cpRegularCareTaskSchedule;
            }
        }

        public Models.CPRegularCareTaskDiary cpRegularCareTaskDiary
        {
            get
            {
                if (_cpRegularCareTaskDiary == null)
                    _cpRegularCareTaskDiary = new Models.CPRegularCareTaskDiary(authenticationResponse);
                return _cpRegularCareTaskDiary;
            }
        }



        public Models.CPPersonAbsence cpPersonAbsence
        {
            get
            {
                if (_cpPersonAbsence == null)
                    _cpPersonAbsence = new Models.CPPersonAbsence(authenticationResponse);
                return _cpPersonAbsence;
            }
        }


        public Models.CareProviderPersonContractService careProviderPersonContractService
        {
            get
            {
                if (_careProviderPersonContractService == null)
                    _careProviderPersonContractService = new Models.CareProviderPersonContractService(authenticationResponse);
                return _careProviderPersonContractService;
            }
        }


        public Models.CareProviderPersonContractEndReason careProviderPersonContractEndReason
        {
            get
            {
                if (_careProviderPersonContractEndReason == null)
                    _careProviderPersonContractEndReason = new Models.CareProviderPersonContractEndReason(authenticationResponse);
                return _careProviderPersonContractEndReason;
            }
        }


        public Models.ScheduleBookingToPeople scheduleBookingToPeople
        {
            get
            {
                if (_scheduleBookingToPeople == null)
                    _scheduleBookingToPeople = new Models.ScheduleBookingToPeople(authenticationResponse);
                return _scheduleBookingToPeople;
            }
        }


        public Models.MASHStatus mashStatus
        {
            get
            {
                if (_mashStatus == null)
                    _mashStatus = new Models.MASHStatus(authenticationResponse);
                return _mashStatus;
            }
        }


        public Models.RegularCareTask regularCareTask
        {
            get
            {
                if (_regularCareTask == null)
                    _regularCareTask = new Models.RegularCareTask(authenticationResponse);
                return _regularCareTask;
            }
        }



        public Models.DuplicateDetectionRule duplicateDetectionRule
        {
            get
            {
                if (_duplicateDetectionRule == null)
                    _duplicateDetectionRule = new Models.DuplicateDetectionRule(authenticationResponse);
                return _duplicateDetectionRule;
            }
        }


        public Models.LACReviewType lacReviewType
        {
            get
            {
                if (_lacReviewType == null)
                    _lacReviewType = new Models.LACReviewType(authenticationResponse);
                return _lacReviewType;
            }
        }


        public Models.LACReview lacReview
        {
            get
            {
                if (_lacReview == null)
                    _lacReview = new Models.LACReview(authenticationResponse);
                return _lacReview;
            }
        }



        public Models.PersonalMoneyAccountTransactionType personalMoneyAccountTransactionType
        {
            get
            {
                if (_personalMoneyAccountTransactionType == null)
                    _personalMoneyAccountTransactionType = new Models.PersonalMoneyAccountTransactionType(authenticationResponse);
                return _personalMoneyAccountTransactionType;
            }
        }


        public Models.ContractEndReason contractEndReason
        {
            get
            {
                if (_contractEndReason == null)
                    _contractEndReason = new Models.ContractEndReason(authenticationResponse);
                return _contractEndReason;
            }
        }


        public Models.CaseRejectedReason caseRejectedReason
        {
            get
            {
                if (_caseRejectedReason == null)
                    _caseRejectedReason = new Models.CaseRejectedReason(authenticationResponse);
                return _caseRejectedReason;
            }
        }

        public Models.RTTEvent rttEvent
        {
            get
            {
                if (_rttEvent == null)
                    _rttEvent = new Models.RTTEvent(authenticationResponse);
                return _rttEvent;
            }
        }


        public Models.RTTWaitTime rttWaitTime
        {
            get
            {
                if (_rttWaitTime == null)
                    _rttWaitTime = new Models.RTTWaitTime(authenticationResponse);
                return _rttWaitTime;
            }
        }


        public Models.JobRoleType jobRoleType
        {
            get
            {
                if (_jobRoleType == null)
                    _jobRoleType = new Models.JobRoleType(authenticationResponse);
                return _jobRoleType;
            }
        }


        public Models.RTTPathwaySetup rttPathwaySetup
        {
            get
            {
                if (_rttPathwaySetup == null)
                    _rttPathwaySetup = new Models.RTTPathwaySetup(authenticationResponse);
                return _rttPathwaySetup;
            }
        }


        public Models.RTTTreatmentStatus rttTreatmentStatus
        {
            get
            {
                if (_rttTreatmentStatus == null)
                    _rttTreatmentStatus = new Models.RTTTreatmentStatus(authenticationResponse);
                return _rttTreatmentStatus;
            }
        }


        public Models.ProviderComplaintNature providerComplaintNature
        {
            get
            {
                if (_providerComplaintNature == null)
                    _providerComplaintNature = new Models.ProviderComplaintNature(authenticationResponse);
                return _providerComplaintNature;
            }
        }


        public Models.ProviderComplaintOutcome providerComplaintOutcome
        {
            get
            {
                if (_providerComplaintOutcome == null)
                    _providerComplaintOutcome = new Models.ProviderComplaintOutcome(authenticationResponse);
                return _providerComplaintOutcome;
            }
        }


        public Models.ProviderComplaintStage providerComplaintStage
        {
            get
            {
                if (_providerComplaintStage == null)
                    _providerComplaintStage = new Models.ProviderComplaintStage(authenticationResponse);
                return _providerComplaintStage;
            }
        }


        public Models.ProviderComplaintFeedBackType providerComplaintFeedBackType
        {
            get
            {
                if (_providerComplaintFeedBackType == null)
                    _providerComplaintFeedBackType = new Models.ProviderComplaintFeedBackType(authenticationResponse);
                return _providerComplaintFeedBackType;
            }
        }


        public Models.ProviderComplaintFeedback providerComplaintFeedback
        {
            get
            {
                if (_providerComplaintFeedback == null)
                    _providerComplaintFeedback = new Models.ProviderComplaintFeedback(authenticationResponse);
                return _providerComplaintFeedback;
            }
        }


        public Models.PersonAttendedEducationEstablishment personAttendedEducationEstablishment
        {
            get
            {
                if (_personAttendedEducationEstablishment == null)
                    _personAttendedEducationEstablishment = new Models.PersonAttendedEducationEstablishment(authenticationResponse);
                return _personAttendedEducationEstablishment;
            }
        }


        public Models.CaseReopenReason caseReopenReason
        {
            get
            {
                if (_caseReopenReason == null)
                    _caseReopenReason = new Models.CaseReopenReason(authenticationResponse);
                return _caseReopenReason;
            }
        }


        public Models.RTTTreatmentFunctionCode rttTreatmentFunctionCode
        {
            get
            {
                if (_rttTreatmentFunctionCode == null)
                    _rttTreatmentFunctionCode = new Models.RTTTreatmentFunctionCode(authenticationResponse);
                return _rttTreatmentFunctionCode;
            }
        }


        public Models.CPFeeType cpFeeType
        {
            get
            {
                if (_cpFeeType == null)
                    _cpFeeType = new Models.CPFeeType(authenticationResponse);
                return _cpFeeType;
            }
        }


        public Models.CPFeeSetup cpFeeSetup
        {
            get
            {
                if (_cpFeeSetup == null)
                    _cpFeeSetup = new Models.CPFeeSetup(authenticationResponse);
                return _cpFeeSetup;
            }
        }


        public Models.CPFee cpFee
        {
            get
            {
                if (_cpFee == null)
                    _cpFee = new Models.CPFee(authenticationResponse);
                return _cpFee;
            }
        }


        public Models.CPAllowance cpAllowance
        {
            get
            {
                if (_cpAllowance == null)
                    _cpAllowance = new Models.CPAllowance(authenticationResponse);
                return _cpAllowance;
            }
        }


        public Models.Decision decision
        {
            get
            {
                if (_decision == null)
                    _decision = new Models.Decision(authenticationResponse);
                return _decision;
            }
        }


        public Models.CarerBatchGrouping carerBatchGrouping
        {
            get
            {
                if (_carerBatchGrouping == null)
                    _carerBatchGrouping = new Models.CarerBatchGrouping(authenticationResponse);
                return _carerBatchGrouping;
            }
        }


        public Models.CPCarerRateUnit cpCarerRateUnit
        {
            get
            {
                if (_cpCarerRateUnit == null)
                    _cpCarerRateUnit = new Models.CPCarerRateUnit(authenticationResponse);
                return _cpCarerRateUnit;
            }
        }

        public Models.CPAllowanceSetup cpAllowanceSetup
        {
            get
            {
                if (_cpAllowanceSetup == null)
                    _cpAllowanceSetup = new Models.CPAllowanceSetup(authenticationResponse);
                return _cpAllowanceSetup;
            }
        }


        public Models.CPAllowanceType cpAllowanceType
        {
            get
            {
                if (_cpAllowanceType == null)
                    _cpAllowanceType = new Models.CPAllowanceType(authenticationResponse);
                return _cpAllowanceType;
            }
        }


        public Models.ApprovedCareType approvedCareType
        {
            get
            {
                if (_approvedCareType == null)
                    _approvedCareType = new Models.ApprovedCareType(authenticationResponse);
                return _approvedCareType;
            }
        }


        public Models.CarerApprovalDecision carerApprovalDecision
        {
            get
            {
                if (_carerApprovalDecision == null)
                    _carerApprovalDecision = new Models.CarerApprovalDecision(authenticationResponse);
                return _carerApprovalDecision;
            }
        }


        public Models.DiaryBookingToPeople diaryBookingToPeople
        {
            get
            {
                if (_diaryBookingToPeople == null)
                    _diaryBookingToPeople = new Models.DiaryBookingToPeople(authenticationResponse);
                return _diaryBookingToPeople;
            }
        }


        public Models.InpatientBedType inpatientBedType
        {
            get
            {
                if (_inpatientBedType == null)
                    _inpatientBedType = new Models.InpatientBedType(authenticationResponse);
                return _inpatientBedType;
            }
        }


        public Models.InpatientWardSpecialty inpatientWardSpecialty
        {
            get
            {
                if (_inpatientWardSpecialty == null)
                    _inpatientWardSpecialty = new Models.InpatientWardSpecialty(authenticationResponse);
                return _inpatientWardSpecialty;
            }
        }


        public Models.AppointmentType appointmentType
        {
            get
            {
                if (_appointmentType == null)
                    _appointmentType = new Models.AppointmentType(authenticationResponse);
                return _appointmentType;
            }
        }


        public Models.CareProviderPersonContract careProviderPersonContract
        {
            get
            {
                if (_careProviderPersonContract == null)
                    _careProviderPersonContract = new Models.CareProviderPersonContract(authenticationResponse);
                return _careProviderPersonContract;
            }
        }


        public Models.CPBookingDiaryStaff cPBookingDiaryStaff
        {
            get
            {
                if (_cPBookingDiaryStaff == null)
                    _cPBookingDiaryStaff = new Models.CPBookingDiaryStaff(authenticationResponse);
                return _cPBookingDiaryStaff;
            }
        }

        public Models.CPBookingRegularCareTask cPBookingRegularCareTask
        {
            get
            {
                if (_cPBookingRegularCareTask == null)
                    _cPBookingRegularCareTask = new Models.CPBookingRegularCareTask(authenticationResponse);
                return _cPBookingRegularCareTask;
            }
        }

        public Models.CPBookingRegularCareTaskStaff cPBookingRegularCareTaskStaff
        {
            get
            {
                if (_cPBookingRegularCareTaskStaff == null)
                    _cPBookingRegularCareTaskStaff = new Models.CPBookingRegularCareTaskStaff(authenticationResponse);
                return _cPBookingRegularCareTaskStaff;
            }
        }

        public Models.CareProviderContractService careProviderContractService
        {
            get
            {
                if (_careProviderContractService == null)
                    _careProviderContractService = new Models.CareProviderContractService(authenticationResponse);
                return _careProviderContractService;
            }
        }

        public Models.CareProviderRateUnit careProviderRateUnit
        {
            get
            {
                if (_careProviderRateUnit == null)
                    _careProviderRateUnit = new Models.CareProviderRateUnit(authenticationResponse);
                return _careProviderRateUnit;
            }
        }

        public Models.CPBookingDiary cPBookingDiary
        {
            get
            {
                if (_cPBookingDiary == null)
                    _cPBookingDiary = new Models.CPBookingDiary(authenticationResponse);
                return _cPBookingDiary;
            }
        }

        public Models.CareProviderBatchGrouping careProviderBatchGrouping
        {
            get
            {
                if (_careProviderBatchGrouping == null)
                    _careProviderBatchGrouping = new Models.CareProviderBatchGrouping(authenticationResponse);
                return _careProviderBatchGrouping;
            }
        }

        public Models.CareProviderCarePeriodSetUp careProviderCarePeriodSetUp
        {
            get
            {
                if (_careProviderCarePeriodSetUp == null)
                    _careProviderCarePeriodSetUp = new Models.CareProviderCarePeriodSetUp(authenticationResponse);
                return _careProviderCarePeriodSetUp;
            }
        }

        public Models.CareProviderVatCode careProviderVatCode
        {
            get
            {
                if (_careProviderVatCode == null)
                    _careProviderVatCode = new Models.CareProviderVatCode(authenticationResponse);
                return _careProviderVatCode;
            }
        }

        public Models.CareProviderService careProviderService
        {
            get
            {
                if (_careProviderService == null)
                    _careProviderService = new Models.CareProviderService(authenticationResponse);
                return _careProviderService;
            }
        }

        public Models.InpatientDischargeDestination inpatientDischargeDestination
        {
            get
            {
                if (_inpatientDischargeDestination == null)
                    _inpatientDischargeDestination = new Models.InpatientDischargeDestination(authenticationResponse);
                return _inpatientDischargeDestination;
            }
        }

        public Models.CaseClosureReason caseClosureReason
        {
            get
            {
                if (_caseClosureReason == null)
                    _caseClosureReason = new Models.CaseClosureReason(authenticationResponse);
                return _caseClosureReason;
            }
        }

        public Models.LACLegalStatusEndReason lacLegalStatusEndReason
        {
            get
            {
                if (_lacLegalStatusEndReason == null)
                    _lacLegalStatusEndReason = new Models.LACLegalStatusEndReason(authenticationResponse);
                return _lacLegalStatusEndReason;
            }
        }

        public Models.LACPlacementProvider lacPlacementProvider
        {
            get
            {
                if (_lacPlacementProvider == null)
                    _lacPlacementProvider = new Models.LACPlacementProvider(authenticationResponse);
                return _lacPlacementProvider;
            }
        }


        public Models.LACLocationCode lacLocationCode
        {
            get
            {
                if (_lacLocationCode == null)
                    _lacLocationCode = new Models.LACLocationCode(authenticationResponse);
                return _lacLocationCode;
            }
        }


        public Models.LACPlacement lacPlacement
        {
            get
            {
                if (_lacPlacement == null)
                    _lacPlacement = new Models.LACPlacement(authenticationResponse);
                return _lacPlacement;
            }
        }


        public Models.LACLegalStatus lacLegalStatus
        {
            get
            {
                if (_lacLegalStatus == null)
                    _lacLegalStatus = new Models.LACLegalStatus(authenticationResponse);
                return _lacLegalStatus;
            }
        }


        public Models.LACLegalStatusReason lacLegalStatusReason
        {
            get
            {
                if (_lacLegalStatusReason == null)
                    _lacLegalStatusReason = new Models.LACLegalStatusReason(authenticationResponse);
                return _lacLegalStatusReason;
            }
        }


        public Models.ProfessionType professionType
        {
            get
            {
                if (_professionType == null)
                    _professionType = new Models.ProfessionType(authenticationResponse);
                return _professionType;
            }
        }

        public Models.CaseActionType caseActionType
        {
            get
            {
                if (_caseActionType == null)
                    _caseActionType = new Models.CaseActionType(authenticationResponse);
                return _caseActionType;
            }
        }

        public Models.ReferralPriority referralPriority
        {
            get
            {
                if (_referralPriority == null)
                    _referralPriority = new Models.ReferralPriority(authenticationResponse);
                return _referralPriority;
            }
        }

        public Models.ContactOutcome contactOutcome
        {
            get
            {
                if (_contactOutcome == null)
                    _contactOutcome = new Models.ContactOutcome(authenticationResponse);
                return _contactOutcome;
            }
        }

        public Models.SignificantEventCategory significantEventCategory
        {
            get
            {
                if (_significantEventCategory == null)
                    _significantEventCategory = new Models.SignificantEventCategory(authenticationResponse);
                return _significantEventCategory;
            }
        }

        public Models.DocumentPickList documentPickList
        {
            get
            {
                if (_documentPickList == null)
                    _documentPickList = new Models.DocumentPickList(authenticationResponse);
                return _documentPickList;
            }
        }

        public Models.DocumentPickListValue documentPickListValue
        {
            get
            {
                if (_documentPickListValue == null)
                    _documentPickListValue = new Models.DocumentPickListValue(authenticationResponse);
                return _documentPickListValue;
            }
        }

        public Models.EmploymentStatus employmentStatus
        {
            get
            {
                if (_employmentStatus == null)
                    _employmentStatus = new Models.EmploymentStatus(authenticationResponse);
                return _employmentStatus;
            }
        }

        public Models.EmploymentWeeklyHoursWorked employmentWeeklyHoursWorked
        {
            get
            {
                if (_employmentWeeklyHoursWorked == null)
                    _employmentWeeklyHoursWorked = new Models.EmploymentWeeklyHoursWorked(authenticationResponse);
                return _employmentWeeklyHoursWorked;
            }
        }

        public Models.PersonEmployment personEmployment
        {
            get
            {
                if (_personEmployment == null)
                    _personEmployment = new Models.PersonEmployment(authenticationResponse);
                return _personEmployment;
            }
        }
        public Models.DataRestriction dataRestriction
        {
            get
            {
                if (_dataRestriction == null)
                    _dataRestriction = new Models.DataRestriction(authenticationResponse);
                return _dataRestriction;
            }
        }

        public Models.ActivityCategory activityCategory
        {
            get
            {
                if (_activityCategory == null)
                    _activityCategory = new Models.ActivityCategory(authenticationResponse);
                return _activityCategory;
            }
        }

        public Models.ActivitySubCategory activitySubCategory
        {
            get
            {
                if (_activitySubCategory == null)
                    _activitySubCategory = new Models.ActivitySubCategory(authenticationResponse);
                return _activitySubCategory;
            }
        }

        public Models.ActivityOutcome activityOutcome
        {
            get
            {
                if (_activityOutcome == null)
                    _activityOutcome = new Models.ActivityOutcome(authenticationResponse);
                return _activityOutcome;
            }
        }

        public Models.ActivityPriority activityPriority
        {
            get
            {
                if (_activityPriority == null)
                    _activityPriority = new Models.ActivityPriority(authenticationResponse);
                return _activityPriority;
            }
        }

        public Models.ActivityReason activityReason
        {
            get
            {
                if (_activityReason == null)
                    _activityReason = new Models.ActivityReason(authenticationResponse);
                return _activityReason;
            }
        }

        public Models.RecruitmentRequirementStaffRole recruitmentRequirementStaffRole
        {
            get
            {
                if (_recruitmentRequirementStaffRole == null)
                    _recruitmentRequirementStaffRole = new Models.RecruitmentRequirementStaffRole(authenticationResponse);
                return _recruitmentRequirementStaffRole;
            }
        }

        public Models.RecruitmentRequirement recruitmentRequirement
        {
            get
            {
                if (_recruitmentRequirement == null)
                    _recruitmentRequirement = new Models.RecruitmentRequirement(authenticationResponse);
                return _recruitmentRequirement;
            }
        }

        public Models.StaffRecruitmentItem staffRecruitmentItem
        {
            get
            {
                if (_staffRecruitmentItem == null)
                    _staffRecruitmentItem = new Models.StaffRecruitmentItem(authenticationResponse);
                return _staffRecruitmentItem;
            }
        }

        public Models.PaymentTypeCode paymentTypeCode
        {
            get
            {
                if (_paymentTypeCode == null)
                    _paymentTypeCode = new Models.PaymentTypeCode(authenticationResponse);
                return _paymentTypeCode;
            }
        }

        public Models.VATCode vatCode
        {
            get
            {
                if (_vatCode == null)
                    _vatCode = new Models.VATCode(authenticationResponse);
                return _vatCode;
            }
        }

        public Models.ProviderBatchGrouping providerBatchGrouping
        {
            get
            {
                if (_providerBatchGrouping == null)
                    _providerBatchGrouping = new Models.ProviderBatchGrouping(authenticationResponse);
                return _providerBatchGrouping;
            }
        }

        public Models.StaffMembership staffMembership
        {
            get
            {
                if (_staffMembership == null)
                    _staffMembership = new Models.StaffMembership(authenticationResponse);
                return _staffMembership;
            }
        }


        public Models.ServiceFinanceSettings serviceFinanceSettings
        {
            get
            {
                if (_serviceFinanceSettings == null)
                    _serviceFinanceSettings = new Models.ServiceFinanceSettings(authenticationResponse);
                return _serviceFinanceSettings;
            }
        }

        public Models.ServiceProvidedRateSchedule serviceProvidedRateSchedule
        {
            get
            {
                if (_serviceProvidedRateSchedule == null)
                    _serviceProvidedRateSchedule = new Models.ServiceProvidedRateSchedule(authenticationResponse);
                return _serviceProvidedRateSchedule;
            }
        }

        public Models.ServiceProvidedRatePeriod serviceProvidedRatePeriod
        {
            get
            {
                if (_serviceProvidedRatePeriod == null)
                    _serviceProvidedRatePeriod = new Models.ServiceProvidedRatePeriod(authenticationResponse);
                return _serviceProvidedRatePeriod;
            }
        }

        public Models.TableQuestionCell tableQuestionCell
        {
            get
            {
                if (_tableQuestionCell == null)
                    _tableQuestionCell = new Models.TableQuestionCell(authenticationResponse);
                return _tableQuestionCell;
            }
        }

        public Models.MultiOptionAnswer multiOptionAnswer
        {
            get
            {
                if (_multiOptionAnswer == null)
                    _multiOptionAnswer = new Models.MultiOptionAnswer(authenticationResponse);
                return _multiOptionAnswer;
            }
        }

        public Models.UserTransportationSchedule userTransportationSchedule
        {
            get
            {
                if (_userTransportationSchedule == null)
                    _userTransportationSchedule = new Models.UserTransportationSchedule(authenticationResponse);
                return _userTransportationSchedule;
            }
        }

        public Models.ContactAttachment contactAttachment
        {
            get
            {
                if (_contactAttachment == null)
                    _contactAttachment = new Models.ContactAttachment(authenticationResponse);
                return _contactAttachment;
            }
        }

        public Models.ContactStatus contactStatus
        {
            get
            {
                if (_contactStatus == null)
                    _contactStatus = new Models.ContactStatus(authenticationResponse);
                return _contactStatus;
            }
        }

        public Models.ContactPresentingPriority contactPresentingPriority
        {
            get
            {
                if (_contactPresentingPriority == null)
                    _contactPresentingPriority = new Models.ContactPresentingPriority(authenticationResponse);
                return _contactPresentingPriority;
            }
        }

        public Models.ContactType contactType
        {
            get
            {
                if (_contactType == null)
                    _contactType = new Models.ContactType(authenticationResponse);
                return _contactType;
            }
        }

        public Models.Contact contact
        {
            get
            {
                if (_contact == null)
                    _contact = new Models.Contact(authenticationResponse);
                return _contact;
            }
        }

        public Models.HomeScreen homeScreen
        {
            get
            {
                if (_homeScreen == null)
                    _homeScreen = new Models.HomeScreen(authenticationResponse);
                return _homeScreen;
            }
        }


        public Models.PersonCarePlan personCarePlan
        {
            get
            {
                if (_personCarePlan == null)
                    _personCarePlan = new Models.PersonCarePlan(authenticationResponse);
                return _personCarePlan;
            }
        }


        public Models.PersonCarePlanNeed personCarePlanNeed
        {
            get
            {
                if (_personCarePlanNeed == null)
                    _personCarePlanNeed = new Models.PersonCarePlanNeed(authenticationResponse);
                return _personCarePlanNeed;
            }
        }
        public Models.PersonCarePlanForm personCarePlanForm
        {
            get
            {
                if (_personCarePlanForm == null)
                    _personCarePlanForm = new Models.PersonCarePlanForm(authenticationResponse);
                return _personCarePlanForm;
            }
        }

        public Models.CarePlanNeedDomain personCarePlanNeedDomain
        {
            get
            {
                if (_CarePlanNeedDomain == null)
                    _CarePlanNeedDomain = new Models.CarePlanNeedDomain(authenticationResponse);
                return _CarePlanNeedDomain;
            }
        }

        public Models.CarePlanGoalType personCarePlanGoalType
        {
            get
            {
                if (_CarePlanGoalType == null)
                    _CarePlanGoalType = new Models.CarePlanGoalType(authenticationResponse);
                return _CarePlanGoalType;
            }
        }

        public Models.CarePlanInterventionType personCarePlanInterventionType
        {
            get
            {
                if (_CarePlanInterventionType == null)
                    _CarePlanInterventionType = new Models.CarePlanInterventionType(authenticationResponse);
                return _CarePlanInterventionType;
            }
        }

        public Models.PersonCarePlanIntervention personCarePlanIntervention
        {
            get
            {
                if (_personcareplanintervention == null)
                    _personcareplanintervention = new Models.PersonCarePlanIntervention(authenticationResponse);
                return _personcareplanintervention;
            }
        }


        public Models.PersonCarePlanGoal personCarePlanGoal
        {
            get
            {
                if (_personcareplangoal == null)
                    _personcareplangoal = new Models.PersonCarePlanGoal(authenticationResponse);
                return _personcareplangoal;
            }
        }
        public Models.UserDataView userDataView
        {
            get
            {
                if (_userDataView == null)
                    _userDataView = new Models.UserDataView(authenticationResponse);
                return _userDataView;
            }
        }

        public Models.UserFavouriteRecord userFavouriteRecord
        {
            get
            {
                if (_userFavouriteRecord == null)
                    _userFavouriteRecord = new Models.UserFavouriteRecord(authenticationResponse);
                return _userFavouriteRecord;
            }
        }

        public Models.RecurrencePattern recurrencePattern
        {
            get
            {
                if (_recurrencePattern == null)
                    _recurrencePattern = new Models.RecurrencePattern(authenticationResponse);
                return _recurrencePattern;
            }
        }

        public Models.HealthAppointmentLocationType healthAppointmentLocationType
        {
            get
            {
                if (_healthAppointmentLocationType == null)
                    _healthAppointmentLocationType = new Models.HealthAppointmentLocationType(authenticationResponse);
                return _healthAppointmentLocationType;
            }
        }

        public Models.BreakGlass breakGlass
        {
            get
            {
                if (_breakGlass == null)
                    _breakGlass = new Models.BreakGlass(authenticationResponse);
                return _breakGlass;
            }
        }

        public Models.SDEMap sdeMap
        {
            get
            {
                if (_sdeMap == null)
                    _sdeMap = new Models.SDEMap(authenticationResponse);
                return _sdeMap;
            }
        }

        public Models.DocumentAnswerAudit documentAnswerAudit
        {
            get
            {
                if (_documentAnswerAudit == null)
                    _documentAnswerAudit = new Models.DocumentAnswerAudit(authenticationResponse);
                return _documentAnswerAudit;
            }
        }

        public Models.AssessmentSectionQuestionComment assessmentSectionQuestionComment
        {
            get
            {
                if (_assessmentSectionQuestionComment == null)
                    _assessmentSectionQuestionComment = new Models.AssessmentSectionQuestionComment(authenticationResponse);
                return _assessmentSectionQuestionComment;
            }
        }

        public Models.AssessmentPrintRecord assessmentPrintRecord
        {
            get
            {
                if (_assessmentPrintRecord == null)
                    _assessmentPrintRecord = new Models.AssessmentPrintRecord(authenticationResponse);
                return _assessmentPrintRecord;
            }
        }

        public Models.CaseFormOutcome caseFormOutcome
        {
            get
            {
                if (_caseFormOutcome == null)
                    _caseFormOutcome = new Models.CaseFormOutcome(authenticationResponse);
                return _caseFormOutcome;
            }
        }

        public Models.Document document
        {
            get
            {
                if (_document == null)
                    _document = new Models.Document(authenticationResponse);
                return _document;
            }
        }

        public Models.AttachDocumentType attachDocumentType
        {
            get
            {
                if (_attachDocumentType == null)
                    _attachDocumentType = new Models.AttachDocumentType(authenticationResponse);
                return _attachDocumentType;
            }
        }
        public Models.AttachDocumentSubType attachDocumentSubType
        {
            get
            {
                if (_attachDocumentSubType == null)
                    _attachDocumentSubType = new Models.AttachDocumentSubType(authenticationResponse);
                return _attachDocumentSubType;
            }
        }

        public Models.PersonCaseNote personCaseNote
        {
            get
            {
                if (_personCaseNote == null)
                    _personCaseNote = new Models.PersonCaseNote(authenticationResponse);
                return _personCaseNote;
            }
        }

        public Models.PersonPhysicalObservationCaseNote personPhysicalObservationCaseNote
        {
            get
            {
                if (_personPhysicalObservationCaseNote == null)
                    _personPhysicalObservationCaseNote = new Models.PersonPhysicalObservationCaseNote(authenticationResponse);
                return _personPhysicalObservationCaseNote;
            }
        }

        public Models.FinancialAssessmentCaseNote financialAssessmentCaseNote
        {
            get
            {
                if (_financialAssessmentCaseNote == null)
                    _financialAssessmentCaseNote = new Models.FinancialAssessmentCaseNote(authenticationResponse);
                return _financialAssessmentCaseNote;
            }
        }

        public Models.PhoneCall phoneCall
        {
            get
            {
                if (_phoneCall == null)
                    _phoneCall = new Models.PhoneCall(authenticationResponse);
                return _phoneCall;
            }
        }

        public Models.Email email
        {
            get
            {
                if (_email == null)
                    _email = new Models.Email(authenticationResponse);
                return _email;
            }
        }

        public Models.SMS sms
        {
            get
            {
                if (_sms == null)
                    _sms = new Models.SMS(authenticationResponse);
                return _sms;
            }
        }

        public Models.EmailTo emailTo
        {
            get
            {
                if (_emailTo == null)
                    _emailTo = new Models.EmailTo(authenticationResponse);
                return _emailTo;
            }
        }

        public Models.EmailCc emailCc
        {
            get
            {
                if (_emailCc == null)
                    _emailCc = new Models.EmailCc(authenticationResponse);
                return _emailCc;
            }
        }

        public Models.Appointment appointment
        {
            get
            {
                if (_appointment == null)
                    _appointment = new Models.Appointment(authenticationResponse);
                return _appointment;
            }
        }

        public Models.TeamRestrictedDataAccess teamRestrictedDataAccess
        {
            get
            {
                if (_teamRestrictedDataAccess == null)
                    _teamRestrictedDataAccess = new Models.TeamRestrictedDataAccess(authenticationResponse);
                return _teamRestrictedDataAccess;
            }
        }

        public Models.PersonSignificantEvent personSignificantEvent
        {
            get
            {
                if (_personSignificantEvent == null)
                    _personSignificantEvent = new Models.PersonSignificantEvent(authenticationResponse);
                return _personSignificantEvent;
            }
        }

        public Models.CaseAttachment caseAttachment
        {
            get
            {
                if (_caseAttachment == null)
                    _caseAttachment = new Models.CaseAttachment(authenticationResponse);
                return _caseAttachment;
            }
        }

        public Models.CaseAction caseAction
        {
            get
            {
                if (_caseAction == null)
                    _caseAction = new Models.CaseAction(authenticationResponse);
                return _caseAction;
            }
        }

        public Models.PersonForm personForm
        {
            get
            {
                if (_personForm == null)
                    _personForm = new Models.PersonForm(authenticationResponse);
                return _personForm;
            }
        }

        public Models.PersonFormInvolvement personFormInvolvement
        {
            get
            {
                if (_personFormInvolvement == null)
                    _personFormInvolvement = new Models.PersonFormInvolvement(authenticationResponse);
                return _personFormInvolvement;
            }
        }
        public Models.InvolvementRole InvolvementRole
        {
            get
            {
                if (_involvementRole == null)
                    _involvementRole = new Models.InvolvementRole(authenticationResponse);
                return _involvementRole;
            }
        }
        public Models.Person person
        {
            get
            {
                if (_person == null)
                    _person = new Models.Person(authenticationResponse);
                return _person;
            }
        }

        public Models.PersonAddress personAddress
        {
            get
            {
                if (_personAddress == null)
                    _personAddress = new Models.PersonAddress(authenticationResponse);
                return _personAddress;
            }
        }

        public Models.PersonMHALegalStatus personMHALegalStatus
        {
            get
            {
                if (_personMHALegalStatus == null)
                    _personMHALegalStatus = new Models.PersonMHALegalStatus(authenticationResponse);
                return _personMHALegalStatus;
            }
        }

        public Models.MHACourtDateOutcome mhaCourtDateOutcome
        {
            get
            {
                if (_mhaCourtDateOutcome == null)
                    _mhaCourtDateOutcome = new Models.MHACourtDateOutcome(authenticationResponse);
                return _mhaCourtDateOutcome;
            }
        }

        public Models.PersonRelationship personRelationship
        {
            get
            {
                if (_personRelationship == null)
                    _personRelationship = new Models.PersonRelationship(authenticationResponse);
                return _personRelationship;
            }
        }

        public Models.Professional professional
        {
            get
            {
                if (_professional == null)
                    _professional = new Models.Professional(authenticationResponse);
                return _professional;
            }
        }

        public Models.ServiceProvision serviceProvision
        {
            get
            {
                if (_serviceProvision == null)
                    _serviceProvision = new Models.ServiceProvision(authenticationResponse);
                return _serviceProvision;
            }
        }

        public Models.ServiceProvisionCostPerWeek serviceProvisionCostPerWeek
        {
            get
            {
                if (_serviceProvisionCostPerWeek == null)
                    _serviceProvisionCostPerWeek = new Models.ServiceProvisionCostPerWeek(authenticationResponse);
                return _serviceProvisionCostPerWeek;
            }
        }

        public Models.FinancialAssessment financialAssessment
        {
            get
            {
                if (_financialAssessment == null)
                    _financialAssessment = new Models.FinancialAssessment(authenticationResponse);
                return _financialAssessment;
            }
        }

        public Models.FinancialAssessmentCharge financialAssessmentCharge
        {
            get
            {
                if (_financialAssessmentCharge == null)
                    _financialAssessmentCharge = new Models.FinancialAssessmentCharge(authenticationResponse);
                return _financialAssessmentCharge;
            }
        }

        public Models.FinancialAssessmentChargeDetail financialAssessmentChargeDetail
        {
            get
            {
                if (_financialAssessmentChargeDetail == null)
                    _financialAssessmentChargeDetail = new Models.FinancialAssessmentChargeDetail(authenticationResponse);
                return _financialAssessmentChargeDetail;
            }
        }

        public Models.FinancialAssessmentChargeTotal financialAssessmentChargeTotal
        {
            get
            {
                if (_financialAssessmentChargeTotal == null)
                    _financialAssessmentChargeTotal = new Models.FinancialAssessmentChargeTotal(authenticationResponse);
                return _financialAssessmentChargeTotal;
            }
        }

        public Models.ServiceProvisionStatus serviceProvisionStatus
        {
            get
            {
                if (_serviceProvisionStatus == null)
                    _serviceProvisionStatus = new Models.ServiceProvisionStatus(authenticationResponse);
                return _serviceProvisionStatus;
            }
        }

        public Models.FACalculationTrigger FACalculationTrigger
        {
            get
            {
                if (_FACalculationTrigger == null)
                    _FACalculationTrigger = new Models.FACalculationTrigger(authenticationResponse);
                return _FACalculationTrigger;
            }
        }

        public Models.ScheduleSetup scheduleSetup
        {
            get
            {
                if (_scheduleSetup == null)
                    _scheduleSetup = new Models.ScheduleSetup(authenticationResponse);
                return _scheduleSetup;
            }
        }

        public Models.FAContribution faContribution
        {
            get
            {
                if (_faContribution == null)
                    _faContribution = new Models.FAContribution(authenticationResponse);
                return _faContribution;
            }
        }

        public Models.FAContributionException faContributionException
        {
            get
            {
                if (_faContributionException == null)
                    _faContributionException = new Models.FAContributionException(authenticationResponse);
                return _faContributionException;
            }
        }

        public Models.ChargingRuleSetup chargingRuleSetup
        {
            get
            {
                if (_chargingRuleSetup == null)
                    _chargingRuleSetup = new Models.ChargingRuleSetup(authenticationResponse);
                return _chargingRuleSetup;
            }
        }

        public Models.IncomeSupportSetup incomeSupportSetup
        {
            get
            {
                if (_incomeSupportSetup == null)
                    _incomeSupportSetup = new Models.IncomeSupportSetup(authenticationResponse);
                return _incomeSupportSetup;
            }
        }

        public Models.NonResidentialPolicyRateSetup nonResidentialPolicyRateSetup
        {
            get
            {
                if (_nonResidentialPolicyRateSetup == null)
                    _nonResidentialPolicyRateSetup = new Models.NonResidentialPolicyRateSetup(authenticationResponse);
                return _nonResidentialPolicyRateSetup;
            }
        }

        public Models.ChargeforServicesSetup chargeforServicesSetup
        {
            get
            {
                if (_chargeforServicesSetup == null)
                    _chargeforServicesSetup = new Models.ChargeforServicesSetup(authenticationResponse);
                return _chargeforServicesSetup;
            }
        }

        public Models.FinancialDetailRateSetup financialDetailRateSetup
        {
            get
            {
                if (_financialDetailRateSetup == null)
                    _financialDetailRateSetup = new Models.FinancialDetailRateSetup(authenticationResponse);
                return _financialDetailRateSetup;
            }
        }

        public Models.PersonFinancialDetail personFinancialDetail
        {
            get
            {
                if (_personFinancialDetail == null)
                    _personFinancialDetail = new Models.PersonFinancialDetail(authenticationResponse);
                return _personFinancialDetail;
            }
        }

        public Models.FinancialDetailDisregard financialDetailDisregard
        {
            get
            {
                if (_financialDetailDisregard == null)
                    _financialDetailDisregard = new Models.FinancialDetailDisregard(authenticationResponse);
                return _financialDetailDisregard;
            }
        }

        public Models.FinancialDetail financialDetail
        {
            get
            {
                if (_financialDetail == null)
                    _financialDetail = new Models.FinancialDetail(authenticationResponse);
                return _financialDetail;
            }
        }

        public Models.FinancialDetailDisregardChargingRuleType financialDetailDisregardChargingRuleType
        {
            get
            {
                if (_financialDetailDisregardChargingRuleType == null)
                    _financialDetailDisregardChargingRuleType = new Models.FinancialDetailDisregardChargingRuleType(authenticationResponse);
                return _financialDetailDisregardChargingRuleType;
            }
        }

        public Models.FinanceTransaction financeTransaction
        {
            get
            {
                if (_financeTransaction == null)
                    _financeTransaction = new Models.FinanceTransaction(authenticationResponse);
                return _financeTransaction;
            }
        }

        public Models.FinanceInvoice financeInvoice
        {
            get
            {
                if (_financeInvoice == null)
                    _financeInvoice = new Models.FinanceInvoice(authenticationResponse);
                return _financeInvoice;
            }
        }

        public Models.FinanceInvoiceBatch financeInvoiceBatch
        {
            get
            {
                if (_financeInvoiceBatch == null)
                    _financeInvoiceBatch = new Models.FinanceInvoiceBatch(authenticationResponse);
                return _financeInvoiceBatch;
            }
        }

        public Models.FinanceExtract financeExtract
        {
            get
            {
                if (_financeExtract == null)
                    _financeExtract = new Models.FinanceExtract(authenticationResponse);
                return _financeExtract;
            }
        }

        public Models.ScheduledJob scheduledJob
        {
            get
            {
                if (_scheduledJob == null)
                    _scheduledJob = new Models.ScheduledJob(authenticationResponse);
                return _scheduledJob;
            }
        }

        public Models.FinanceExtractSetup financeExtractSetup
        {
            get
            {
                if (_financeExtractSetup == null)
                    _financeExtractSetup = new Models.FinanceExtractSetup(authenticationResponse);
                return _financeExtractSetup;
            }
        }

        public Models.TeamMember teamMember
        {
            get
            {
                if (_teamMember == null)
                    _teamMember = new Models.TeamMember(authenticationResponse);
                return _teamMember;
            }
        }

        public Models.SystemSetting systemSetting
        {
            get
            {
                if (_systemSetting == null)
                    _systemSetting = new Models.SystemSetting(authenticationResponse);
                return _systemSetting;
            }
        }

        public Models.Letter letter
        {
            get
            {
                if (_letter == null)
                    _letter = new Models.Letter(authenticationResponse);
                return _letter;
            }
        }

        public Models.PersonAlertAndHazard personAlertAndHazard
        {
            get
            {
                if (_personAlertAndHazard == null)
                    _personAlertAndHazard = new Models.PersonAlertAndHazard(authenticationResponse);
                return _personAlertAndHazard;
            }
        }

        public Models.PersonAlertAndHazardReview personAlertAndHazardReview
        {
            get
            {
                if (_personAlertAndHazardReview == null)
                    _personAlertAndHazardReview = new Models.PersonAlertAndHazardReview(authenticationResponse);
                return _personAlertAndHazardReview;
            }
        }

        public Models.Ethnicity ethnicity
        {
            get
            {
                if (_ethnicity == null)
                    _ethnicity = new Models.Ethnicity(authenticationResponse);
                return _ethnicity;
            }
        }

        public Models.MaritalStatus maritalStatus
        {
            get
            {
                if (_maritalStatus == null)
                    _maritalStatus = new Models.MaritalStatus(authenticationResponse);
                return _maritalStatus;
            }
        }

        public Models.Language language
        {
            get
            {
                if (_language == null)
                    _language = new Models.Language(authenticationResponse);
                return _language;
            }
        }

        public Models.ProductLanguage productLanguage
        {
            get
            {
                if (_productLanguage == null)
                    _productLanguage = new Models.ProductLanguage(authenticationResponse);
                return _productLanguage;
            }
        }

        public Models.AddressPropertyType addressPropertyType
        {
            get
            {
                if (_addressPropertyType == null)
                    _addressPropertyType = new Models.AddressPropertyType(authenticationResponse);
                return _addressPropertyType;
            }
        }

        public Models.Team team
        {
            get
            {
                if (_team == null)
                    _team = new Models.Team(authenticationResponse);
                return _team;
            }
        }

        public Models.PersonAllergy personAllergy
        {
            get
            {
                if (_personAllergy == null)
                    _personAllergy = new Models.PersonAllergy(authenticationResponse);
                return _personAllergy;
            }
        }

        public Models.Case Case
        {
            get
            {
                if (_Case == null)
                    _Case = new Models.Case(authenticationResponse);
                return _Case;
            }
        }

        public Models.CaseInvolvement CaseInvolvement
        {
            get
            {
                if (_CaseInvolvement == null)
                    _CaseInvolvement = new Models.CaseInvolvement(authenticationResponse);
                return _CaseInvolvement;
            }
        }

        public Models.CaseStatusHistory CaseStatusHistory
        {
            get
            {
                if (_CaseStatusHistory == null)
                    _CaseStatusHistory = new Models.CaseStatusHistory(authenticationResponse);
                return _CaseStatusHistory;
            }
        }

        public Models.CaseForm caseForm
        {
            get
            {
                if (_caseForm == null)
                    _caseForm = new Models.CaseForm(authenticationResponse);
                return _caseForm;
            }
        }

        public Models.HealthAppointment healthAppointment
        {
            get
            {
                if (_healthAppointment == null)
                    _healthAppointment = new Models.HealthAppointment(authenticationResponse);
                return _healthAppointment;
            }
        }

        public Models.Task task
        {
            get
            {
                if (_task == null)
                    _task = new Models.Task(authenticationResponse);
                return _task;
            }
        }

        public Models.HealthAppointmentAdditionalProfessional healthAppointmentAdditionalProfessional
        {
            get
            {
                if (_healthAppointmentAdditionalProfessional == null)
                    _healthAppointmentAdditionalProfessional = new Models.HealthAppointmentAdditionalProfessional(authenticationResponse);
                return _healthAppointmentAdditionalProfessional;
            }
        }

        public Models.HealthAppointmentCaseNote healthAppointmentCaseNote
        {
            get
            {
                if (_healthAppointmentCaseNote == null)
                    _healthAppointmentCaseNote = new Models.HealthAppointmentCaseNote(authenticationResponse);
                return _healthAppointmentCaseNote;
            }
        }

        public Models.AdultSafeguarding AdultSafeguarding
        {
            get
            {
                if (_AdultSafeguarding == null)
                    _AdultSafeguarding = new Models.AdultSafeguarding(authenticationResponse);
                return _AdultSafeguarding;
            }
        }

        public Models.Allegation allegation
        {
            get
            {
                if (_allegation == null)
                    _allegation = new Models.Allegation(authenticationResponse);
                return _allegation;
            }
        }

        public Models.AllegationInvestigator allegationInvestigator
        {
            get
            {
                if (_allegationInvestigator == null)
                    _allegationInvestigator = new Models.AllegationInvestigator(authenticationResponse);
                return _allegationInvestigator;
            }
        }

        public Models.ChildProtection ChildProtection
        {
            get
            {
                if (_ChildProtection == null)
                    _ChildProtection = new Models.ChildProtection(authenticationResponse);
                return _ChildProtection;
            }
        }

        public Models.LACEpisode LACEpisode
        {
            get
            {
                if (_LACEpisode == null)
                    _LACEpisode = new Models.LACEpisode(authenticationResponse);
                return _LACEpisode;
            }
        }

        public Models.PersonLACLegalStatus PersonLACLegalStatus
        {
            get
            {
                if (_PersonLACLegalStatus == null)
                    _PersonLACLegalStatus = new Models.PersonLACLegalStatus(authenticationResponse);
                return _PersonLACLegalStatus;
            }
        }

        public Models.MergedRecord MergedRecord
        {
            get
            {
                if (_MergedRecord == null)
                    _MergedRecord = new Models.MergedRecord(authenticationResponse);
                return _MergedRecord;
            }
        }

        public Models.EmailAttachment EmailAttachment
        {
            get
            {
                if (_EmailAttachment == null)
                    _EmailAttachment = new Models.EmailAttachment(authenticationResponse);
                return _EmailAttachment;
            }
        }

        public Models.CPIS cpis
        {
            get
            {
                if (_cpis == null)
                    _cpis = new Models.CPIS(authenticationResponse);
                return _cpis;
            }
        }

        public Models.UserSecurityProfile userSecurityProfile
        {
            get
            {
                if (_userSecurityProfile == null)
                    _userSecurityProfile = new Models.UserSecurityProfile(authenticationResponse);
                return _userSecurityProfile;
            }
        }

        public Models.TeamSecurityProfile teamSecurityProfile
        {
            get
            {
                if (_teamSecurityProfile == null)
                    _teamSecurityProfile = new Models.TeamSecurityProfile(authenticationResponse);
                return _teamSecurityProfile;
            }
        }

        public Models.SecurityProfile securityProfile
        {
            get
            {
                if (_securityProfile == null)
                    _securityProfile = new Models.SecurityProfile(authenticationResponse);
                return _securityProfile;
            }
        }

        public Models.Audit audit
        {
            get
            {
                if (_audit == null)
                    _audit = new Models.Audit(authenticationResponse);
                return _audit;
            }
        }

        public Models.AuditChangeSet auditChangeSet
        {
            get
            {
                if (_auditChangeSet == null)
                    _auditChangeSet = new Models.AuditChangeSet(authenticationResponse);
                return _auditChangeSet;
            }
        }

        public Models.Audit_2019 audit_2019
        {
            get
            {
                if (_audit_2019 == null)
                    _audit_2019 = new Models.Audit_2019(authenticationResponse);
                return _audit_2019;
            }
        }

        public Models.AuditChangeSet_2019 auditChangeSet_2019
        {
            get
            {
                if (_auditChangeSet_2019 == null)
                    _auditChangeSet_2019 = new Models.AuditChangeSet_2019(authenticationResponse);
                return _auditChangeSet_2019;
            }
        }

        public Models.DocumentAnswer documentAnswer
        {
            get
            {
                if (_documentAnswer == null)
                    _documentAnswer = new Models.DocumentAnswer(authenticationResponse);
                return _documentAnswer;
            }
        }

        public Models.DocumentAnswerChecklist documentAnswerChecklist
        {
            get
            {
                if (_documentAnswerChecklist == null)
                    _documentAnswerChecklist = new Models.DocumentAnswerChecklist(authenticationResponse);
                return _documentAnswerChecklist;
            }
        }

        public Models.AssessmentSection assessmentSection
        {
            get
            {
                if (_assessmentSection == null)
                    _assessmentSection = new Models.AssessmentSection(authenticationResponse);
                return _assessmentSection;
            }
        }

        public Models.Automated_UI_Test_Document_1 automated_UI_Test_Document_1
        {
            get
            {
                if (_automated_UI_Test_Document_1 == null)
                    _automated_UI_Test_Document_1 = new Models.Automated_UI_Test_Document_1(authenticationResponse);
                return _automated_UI_Test_Document_1;
            }
        }

        public Models.FileDestruction fileDestruction
        {
            get
            {
                if (_fileDestruction == null)
                    _fileDestruction = new Models.FileDestruction(authenticationResponse);
                return _fileDestruction;
            }
        }

        public Models.Workflow workflow
        {
            get
            {
                if (_workflow == null)
                    _workflow = new Models.Workflow(authenticationResponse);
                return _workflow;
            }
        }

        public Models.WorkflowJob workflowJob
        {
            get
            {
                if (_workflowJob == null)
                    _workflowJob = new Models.WorkflowJob(authenticationResponse);
                return _workflowJob;
            }
        }

        public Models.UserRestrictedDataAccess userRestrictedDataAccess
        {
            get
            {
                if (_userRestrictedDataAccess == null)
                    _userRestrictedDataAccess = new Models.UserRestrictedDataAccess(authenticationResponse);
                return _userRestrictedDataAccess;
            }
        }

        public Models.SystemDashboard systemDashboard
        {
            get
            {
                if (_systemDashboard == null)
                    _systemDashboard = new Models.SystemDashboard(authenticationResponse);
                return _systemDashboard;
            }
        }

        public Models.BusinessObjectDashboard businessObjectDashboard
        {
            get
            {
                if (_businessObjectDashboard == null)
                    _businessObjectDashboard = new Models.BusinessObjectDashboard(authenticationResponse);
                return _businessObjectDashboard;
            }
        }

        public Models.UserDashboard userDashboard
        {
            get
            {
                if (_userDashboard == null)
                    _userDashboard = new Models.UserDashboard(authenticationResponse);
                return _userDashboard;
            }
        }

        public Models.FAQ faq
        {
            get
            {
                if (_faq == null)
                    _faq = new Models.FAQ(authenticationResponse);
                return _faq;
            }
        }

        public Models.FAQCategory faqCategory
        {
            get
            {
                if (_faqCategory == null)
                    _faqCategory = new Models.FAQCategory(authenticationResponse);
                return _faqCategory;
            }
        }

        public Models.Application application
        {
            get
            {
                if (_application == null)
                    _application = new Models.Application(authenticationResponse);
                return _application;
            }
        }

        public Models.ApplicationComponent applicationComponent
        {
            get
            {
                if (_applicationComponent == null)
                    _applicationComponent = new Models.ApplicationComponent(authenticationResponse);
                return _applicationComponent;
            }
        }

        public Models.Website website
        {
            get
            {
                if (_website == null)
                    _website = new Models.Website(authenticationResponse);
                return _website;
            }
        }

        public Models.WebsitePage websitePage
        {
            get
            {
                if (_websitePage == null)
                    _websitePage = new Models.WebsitePage(authenticationResponse);
                return _websitePage;
            }
        }

        public Models.WebsiteSetting websiteSetting
        {
            get
            {
                if (_websiteSetting == null)
                    _websiteSetting = new Models.WebsiteSetting(authenticationResponse);
                return _websiteSetting;
            }
        }

        public Models.WebsiteAnnouncement websiteAnnouncement
        {
            get
            {
                if (_websiteAnnouncement == null)
                    _websiteAnnouncement = new Models.WebsiteAnnouncement(authenticationResponse);
                return _websiteAnnouncement;
            }
        }

        public Models.WebsiteUserPin websiteUserPin
        {
            get
            {
                if (_websiteUserPin == null)
                    _websiteUserPin = new Models.WebsiteUserPin(authenticationResponse);
                return _websiteUserPin;
            }
        }

        public Models.WebsiteUserPasswordReset websiteUserPasswordReset
        {
            get
            {
                if (_websiteUserPasswordReset == null)
                    _websiteUserPasswordReset = new Models.WebsiteUserPasswordReset(authenticationResponse);
                return _websiteUserPasswordReset;
            }
        }

        public Models.WebsiteUserEmailVerification websiteUserEmailVerification
        {
            get
            {
                if (_websiteUserEmailVerification == null)
                    _websiteUserEmailVerification = new Models.WebsiteUserEmailVerification(authenticationResponse);
                return _websiteUserEmailVerification;
            }
        }

        public Models.WebsiteUserPasswordHistory websiteUserPasswordHistory
        {
            get
            {
                if (_websiteUserPasswordHistory == null)
                    _websiteUserPasswordHistory = new Models.WebsiteUserPasswordHistory(authenticationResponse);
                return _websiteUserPasswordHistory;
            }
        }

        public Models.WebsiteUser websiteUser
        {
            get
            {
                if (_websiteUser == null)
                    _websiteUser = new Models.WebsiteUser(authenticationResponse);
                return _websiteUser;
            }
        }

        public Models.WebsiteSitemap websiteSitemap
        {
            get
            {
                if (_websiteSitemap == null)
                    _websiteSitemap = new Models.WebsiteSitemap(authenticationResponse);
                return _websiteSitemap;
            }
        }

        public Models.WebsitePointOfContact websitePointOfContact
        {
            get
            {
                if (_websitePointOfContact == null)
                    _websitePointOfContact = new Models.WebsitePointOfContact(authenticationResponse);
                return _websitePointOfContact;
            }
        }

        public Models.WebsiteFeedback websiteFeedback
        {
            get
            {
                if (_websiteFeedback == null)
                    _websiteFeedback = new Models.WebsiteFeedback(authenticationResponse);
                return _websiteFeedback;
            }
        }

        public Models.WebsiteContact websiteContact
        {
            get
            {
                if (_websiteContact == null)
                    _websiteContact = new Models.WebsiteContact(authenticationResponse);
                return _websiteContact;
            }
        }

        public Models.LocalizedString localizedString
        {
            get
            {
                if (_localizedString == null)
                    _localizedString = new Models.LocalizedString(authenticationResponse);
                return _localizedString;
            }
        }

        public Models.LocalizedStringValue localizedStringValue
        {
            get
            {
                if (_localizedStringValue == null)
                    _localizedStringValue = new Models.LocalizedStringValue(authenticationResponse);
                return _localizedStringValue;
            }
        }

        public Models.UserChart userChart
        {
            get
            {
                if (_userChart == null)
                    _userChart = new Models.UserChart(authenticationResponse);
                return _userChart;
            }
        }

        public Models.DocumentApplicationAccess documentApplicationAccess
        {
            get
            {
                if (_documentApplicationAccess == null)
                    _documentApplicationAccess = new Models.DocumentApplicationAccess(authenticationResponse);
                return _documentApplicationAccess;
            }
        }

        public Models.DocumentSectionApplicationAccess documentSectionApplicationAccess
        {
            get
            {
                if (_documentSectionApplicationAccess == null)
                    _documentSectionApplicationAccess = new Models.DocumentSectionApplicationAccess(authenticationResponse);
                return _documentSectionApplicationAccess;
            }
        }

        public Models.DocumentSection documentSection
        {
            get
            {
                if (_documentSection == null)
                    _documentSection = new Models.DocumentSection(authenticationResponse);
                return _documentSection;
            }
        }

        public Models.DocumentSectionQuestionApplicationAccess documentSectionQuestionApplicationAccess
        {
            get
            {
                if (_documentSectionQuestionApplicationAccess == null)
                    _documentSectionQuestionApplicationAccess = new Models.DocumentSectionQuestionApplicationAccess(authenticationResponse);
                return _documentSectionQuestionApplicationAccess;
            }
        }

        public Models.DocumentSectionQuestion documentSectionQuestion
        {
            get
            {
                if (_documentSectionQuestion == null)
                    _documentSectionQuestion = new Models.DocumentSectionQuestion(authenticationResponse);
                return _documentSectionQuestion;
            }
        }

        public Models.PersonAttachment personAttachment
        {
            get
            {
                if (_personAttachment == null)
                    _personAttachment = new Models.PersonAttachment(authenticationResponse);
                return _personAttachment;
            }
        }

        public Models.DocumentFile documentFile
        {
            get
            {
                if (_documentFile == null)
                    _documentFile = new Models.DocumentFile(authenticationResponse);
                return _documentFile;
            }
        }

        public Models.PortalTask portalTask
        {
            get
            {
                if (_portalTask == null)
                    _portalTask = new Models.PortalTask(authenticationResponse);
                return _portalTask;
            }
        }

        public Models.Nationality nationality
        {
            get
            {
                if (_nationality == null)
                    _nationality = new Models.Nationality(authenticationResponse);
                return _nationality;
            }
        }

        public Models.PersonFormCaseNote personFormCaseNote
        {
            get
            {
                if (_personFormCaseNote == null)
                    _personFormCaseNote = new Models.PersonFormCaseNote(authenticationResponse);
                return _personFormCaseNote;
            }
        }

        public Models.CaseFormCaseNote caseFormCaseNote
        {
            get
            {
                if (_caseFormCaseNote == null)
                    _caseFormCaseNote = new Models.CaseFormCaseNote(authenticationResponse);
                return _caseFormCaseNote;
            }
        }

        public Models.CaseCaseNote caseCaseNote
        {
            get
            {
                if (_caseCaseNote == null)
                    _caseCaseNote = new Models.CaseCaseNote(authenticationResponse);
                return _caseCaseNote;
            }
        }

        public Models.EDMSRepository edmsRepository
        {
            get
            {
                if (_edmsRepository == null)
                    _edmsRepository = new Models.EDMSRepository(authenticationResponse);
                return _edmsRepository;
            }
        }

        public Models.DocumentQuestionIdentifier documentQuestionIdentifier
        {
            get
            {
                if (_documentQuestionIdentifier == null)
                    _documentQuestionIdentifier = new Models.DocumentQuestionIdentifier(authenticationResponse);
                return _documentQuestionIdentifier;
            }
        }

        public Models.DocumentPrintTemplate documentPrintTemplate
        {
            get
            {
                if (_documentPrintTemplate == null)
                    _documentPrintTemplate = new Models.DocumentPrintTemplate(authenticationResponse);
                return _documentPrintTemplate;
            }
        }

        public Models.DocumentPrintTemplateLinkedApplication documentPrintTemplateLinkedApplication
        {
            get
            {
                if (_documentPrintTemplateLinkedApplication == null)
                    _documentPrintTemplateLinkedApplication = new Models.DocumentPrintTemplateLinkedApplication(authenticationResponse);
                return _documentPrintTemplateLinkedApplication;
            }
        }

        public Models.DuplicateRecord duplicateRecord
        {
            get
            {
                if (_duplicateRecord == null)
                    _duplicateRecord = new Models.DuplicateRecord(authenticationResponse);
                return _duplicateRecord;
            }
        }

        public Models.SubordinateDuplicate subordinateDuplicate
        {
            get
            {
                if (_subordinateDuplicate == null)
                    _subordinateDuplicate = new Models.SubordinateDuplicate(authenticationResponse);
                return _subordinateDuplicate;
            }
        }

        public Models.BrokerageEpisode brokerageEpisode
        {
            get
            {
                if (_brokerageEpisode == null)
                    _brokerageEpisode = new Models.BrokerageEpisode(authenticationResponse);
                return _brokerageEpisode;
            }
        }

        public Models.WebsiteMessage websiteMessage
        {
            get
            {
                if (_websiteMessage == null)
                    _websiteMessage = new Models.WebsiteMessage(authenticationResponse);
                return _websiteMessage;
            }
        }

        public Models.PersonConsentToTreatment personConsentToTreatment
        {
            get
            {
                if (_personConsentToTreatment == null)
                    _personConsentToTreatment = new Models.PersonConsentToTreatment(authenticationResponse);
                return _personConsentToTreatment;
            }
        }

        public Models.WebsiteHandler websiteHandler
        {
            get
            {
                if (_websiteHandler == null)
                    _websiteHandler = new Models.WebsiteHandler(authenticationResponse);
                return _websiteHandler;
            }
        }

        public Models.WebsiteLocalizedString websiteLocalizedString
        {
            get
            {
                if (_websiteLocalizedString == null)
                    _websiteLocalizedString = new Models.WebsiteLocalizedString(authenticationResponse);
                return _websiteLocalizedString;
            }
        }

        public Models.WebsiteLocalizedStringValue websiteLocalizedStringValue
        {
            get
            {
                if (_websiteLocalizedStringValue == null)
                    _websiteLocalizedStringValue = new Models.WebsiteLocalizedStringValue(authenticationResponse);
                return _websiteLocalizedStringValue;
            }
        }

        public Models.WebsitePageFile websitePageFile
        {
            get
            {
                if (_websitePageFile == null)
                    _websitePageFile = new Models.WebsitePageFile(authenticationResponse);
                return _websitePageFile;
            }
        }

        public Models.WebsiteRecordType websiteRecordType
        {
            get
            {
                if (_websiteRecordType == null)
                    _websiteRecordType = new Models.WebsiteRecordType(authenticationResponse);
                return _websiteRecordType;
            }
        }

        public Models.WebsiteResourceFile websiteResourceFile
        {
            get
            {
                if (_websiteResourceFile == null)
                    _websiteResourceFile = new Models.WebsiteResourceFile(authenticationResponse);
                return _websiteResourceFile;
            }
        }

        public Models.WebsiteSecurityProfile websiteSecurityProfile
        {
            get
            {
                if (_websiteSecurityProfile == null)
                    _websiteSecurityProfile = new Models.WebsiteSecurityProfile(authenticationResponse);
                return _websiteSecurityProfile;
            }
        }

        public Models.WebsiteSplashScreen websiteSplashScreen
        {
            get
            {
                if (_websiteSplashScreen == null)
                    _websiteSplashScreen = new Models.WebsiteSplashScreen(authenticationResponse);
                return _websiteSplashScreen;
            }
        }

        public Models.WebsiteSplashScreenItem websiteSplashScreenItem
        {
            get
            {
                if (_websiteSplashScreenItem == null)
                    _websiteSplashScreenItem = new Models.WebsiteSplashScreenItem(authenticationResponse);
                return _websiteSplashScreenItem;
            }
        }

        public Models.WebsiteOnDemandWorkflow websiteOnDemandWorkflow
        {
            get
            {
                if (_websiteOnDemandWorkflow == null)
                    _websiteOnDemandWorkflow = new Models.WebsiteOnDemandWorkflow(authenticationResponse);
                return _websiteOnDemandWorkflow;
            }
        }

        public Models.CaseFormAttachment caseFormAttachment
        {
            get
            {
                if (_caseFormAttachment == null)
                    _caseFormAttachment = new Models.CaseFormAttachment(authenticationResponse);
                return _caseFormAttachment;
            }
        }

        public Models.DuplicateDetectionCondition duplicateDetectionCondition
        {
            get
            {
                if (_duplicateDetectionCondition == null)
                    _duplicateDetectionCondition = new Models.DuplicateDetectionCondition(authenticationResponse);
                return _duplicateDetectionCondition;
            }
        }

        public Models.ProviderForm providerForm
        {
            get
            {
                if (_providerForm == null)
                    _providerForm = new Models.ProviderForm(authenticationResponse);
                return _providerForm;
            }
        }

        public Models.Provider provider
        {
            get
            {
                if (_provider == null)
                    _provider = new Models.Provider(authenticationResponse);
                return _provider;
            }
        }

        public Models.MASHEpisodeForm mashEpisodeForm
        {
            get
            {
                if (_mashEpisodeForm == null)
                    _mashEpisodeForm = new Models.MASHEpisodeForm(authenticationResponse);
                return _mashEpisodeForm;
            }
        }

        public Models.MashEpisode mashEpisode
        {
            get
            {
                if (_mashEpisode == null)
                    _mashEpisode = new Models.MashEpisode(authenticationResponse);
                return _mashEpisode;
            }
        }

        public Models.ImageFile imageFile
        {
            get
            {
                if (_imageFile == null)
                    _imageFile = new Models.ImageFile(authenticationResponse);
                return _imageFile;
            }
        }

        public Models.AppointmentRequiredAttendee appointmentRequiredAttendee
        {
            get
            {
                if (_appointmentRequiredAttendee == null)
                    _appointmentRequiredAttendee = new Models.AppointmentRequiredAttendee(authenticationResponse);
                return _appointmentRequiredAttendee;
            }
        }

        public Models.AppointmentOptionalAttendee appointmentOptionalAttendee
        {
            get
            {
                if (_appointmentOptionalAttendee == null)
                    _appointmentOptionalAttendee = new Models.AppointmentOptionalAttendee(authenticationResponse);
                return _appointmentOptionalAttendee;
            }
        }

        public Models.PersonPrimarySupportReason personPrimarySupportReason
        {
            get
            {
                if (_personPrimarySupportReason == null)
                    _personPrimarySupportReason = new Models.PersonPrimarySupportReason(authenticationResponse);
                return _personPrimarySupportReason;
            }
        }

        public Models.PersonChronology personChronology
        {
            get
            {
                if (_personChronology == null)
                    _personChronology = new Models.PersonChronology(authenticationResponse);
                return _personChronology;
            }
        }

        public Models.SignificantEventSubCategoryPersonChronology significantEventSubCategoryPersonChronology
        {
            get
            {
                if (_significantEventSubCategoryPersonChronology == null)
                    _significantEventSubCategoryPersonChronology = new Models.SignificantEventSubCategoryPersonChronology(authenticationResponse);
                return _significantEventSubCategoryPersonChronology;
            }
        }

        public Models.SignificantEventCategoryPersonChronology significantEventCategoryPersonChronology
        {
            get
            {
                if (_significantEventCategoryPersonChronology == null)
                    _significantEventCategoryPersonChronology = new Models.SignificantEventCategoryPersonChronology(authenticationResponse);
                return _significantEventCategoryPersonChronology;
            }
        }

        public Models.PersonChronologySnapshot personChronologySnapshot
        {
            get
            {
                if (_personChronologySnapshot == null)
                    _personChronologySnapshot = new Models.PersonChronologySnapshot(authenticationResponse);
                return _personChronologySnapshot;
            }
        }

        public Models.AdoptionLink adoptionLink
        {
            get
            {
                if (_adoptionLink == null)
                    _adoptionLink = new Models.AdoptionLink(authenticationResponse);
                return _adoptionLink;
            }
        }

        public Models.PersonClinicalRiskFactor personClinicalRiskFactor
        {
            get
            {
                if (_personClinicalRiskFactor == null)
                    _personClinicalRiskFactor = new Models.PersonClinicalRiskFactor(authenticationResponse);
                return _personClinicalRiskFactor;
            }
        }

        public Models.PersonClinicalRiskFactorHistory personClinicalRiskFactorHistory
        {
            get
            {
                if (_personClinicalRiskFactorHistory == null)
                    _personClinicalRiskFactorHistory = new Models.PersonClinicalRiskFactorHistory(authenticationResponse);
                return _personClinicalRiskFactorHistory;
            }
        }

        public Models.PersonHealthProfessional personHealthProfessional
        {
            get
            {
                if (_personHealthProfessional == null)
                    _personHealthProfessional = new Models.PersonHealthProfessional(authenticationResponse);
                return _personHealthProfessional;
            }
        }

        public Models.PersonHealthDetail personHealthDetail
        {
            get
            {
                if (_personHealthDetail == null)
                    _personHealthDetail = new Models.PersonHealthDetail(authenticationResponse);
                return _personHealthDetail;
            }
        }

        public Models.PersonFormOutcome personFormOutcome
        {
            get
            {
                if (_personFormOutcome == null)
                    _personFormOutcome = new Models.PersonFormOutcome(authenticationResponse);
                return _personFormOutcome;
            }
        }

        public Models.HealthIssueType healthIssueType
        {
            get
            {
                if (_healthIssueType == null)
                    _healthIssueType = new Models.HealthIssueType(authenticationResponse);
                return _healthIssueType;
            }
        }

        public Models.PersonGestationPeriod personGestationPeriod
        {
            get
            {
                if (_personGestationPeriod == null)
                    _personGestationPeriod = new Models.PersonGestationPeriod(authenticationResponse);
                return _personGestationPeriod;
            }
        }

        public Models.PersonDisabilityImpairments personDisabilityImpairments
        {
            get
            {
                if (_personDisabilityImpairments == null)
                    _personDisabilityImpairments = new Models.PersonDisabilityImpairments(authenticationResponse);
                return _personDisabilityImpairments;
            }
        }

        public Models.RecurringAppointment recurringAppointment
        {
            get
            {
                if (_recurringAppointment == null)
                    _recurringAppointment = new Models.RecurringAppointment(authenticationResponse);

                return _recurringAppointment;
            }
        }

        public Models.RecurringAppointmentRequiredAttendee recurringAppointmentRequiredAttendee
        {
            get
            {
                if (_recurringAppointmentRequiredAttendee == null)
                    _recurringAppointmentRequiredAttendee = new Models.RecurringAppointmentRequiredAttendee(authenticationResponse);

                return _recurringAppointmentRequiredAttendee;
            }
        }

        public Models.RecurringAppointmentOptionalAttendee recurringAppointmentOptionalAttendee
        {
            get
            {
                if (_recurringAppointmentOptionalAttendee == null)
                    _recurringAppointmentOptionalAttendee = new Models.RecurringAppointmentOptionalAttendee(authenticationResponse);

                return _recurringAppointmentOptionalAttendee;
            }
        }

        public Models.PersonDNAR personDNAR
        {
            get
            {
                if (_personDNAR == null)
                    _personDNAR = new Models.PersonDNAR(authenticationResponse);

                return _personDNAR;
            }
        }
        public Models.PersonPhysicalObservation personPhysicalObservation
        {
            get
            {
                if (_personPhysicalObservation == null)
                    _personPhysicalObservation = new Models.PersonPhysicalObservation(authenticationResponse);

                return _personPhysicalObservation;
            }
        }
        public Models.SystemUser systemUser
        {
            get
            {
                if (_systemUser == null)
                    _systemUser = new Models.SystemUser(authenticationResponse);

                return _systemUser;
            }
        }

        public Models.UserWorkSchedule userWorkSchedule
        {
            get
            {
                if (_userWorkSchedule == null)
                    _userWorkSchedule = new Models.UserWorkSchedule(authenticationResponse);

                return _userWorkSchedule;
            }
        }

        public Models.UserDairy userDairy
        {
            get
            {
                if (_userDairy == null)
                    _userDairy = new Models.UserDairy(authenticationResponse);

                return _userDairy;
            }
        }

        public Models.AuthorisationLevel authorisationLevel
        {
            get
            {
                if (_authorisationLevel == null)
                    _authorisationLevel = new Models.AuthorisationLevel(authenticationResponse);

                return _authorisationLevel;
            }
        }

        public Models.PersonImmunisation personImmunisation
        {
            get
            {
                if (_personImmunisation == null)
                    _personImmunisation = new Models.PersonImmunisation(authenticationResponse);

                return _personImmunisation;
            }
        }


        public Models.ProviderAddress providerAddress
        {
            get
            {
                if (_providerAddress == null)
                    _providerAddress = new Models.ProviderAddress(authenticationResponse);

                return _providerAddress;
            }
        }

        public Models.InpatientWard inpatientWard
        {
            get
            {
                if (_inpatientWard == null)
                    _inpatientWard = new Models.InpatientWard(authenticationResponse);

                return _inpatientWard;
            }
        }

        public Models.InpatientBay inpatientBay
        {
            get
            {
                if (_inpatientBay == null)
                    _inpatientBay = new Models.InpatientBay(authenticationResponse);

                return _inpatientBay;
            }
        }

        public Models.InpatientBed inpatientBed
        {
            get
            {
                if (_inpatientBed == null)
                    _inpatientBed = new Models.InpatientBed(authenticationResponse);

                return _inpatientBed;
            }
        }

        public Models.InpatientBedStatusHistory inpatientBedStatusHistory
        {
            get
            {
                if (_inpatientBedStatusHistory == null)
                    _inpatientBedStatusHistory = new Models.InpatientBedStatusHistory(authenticationResponse);

                return _inpatientBedStatusHistory;
            }
        }

        public Models.InpatientConsultantEpisode inpatientConsultantEpisode
        {
            get
            {
                if (_inpatientConsultantEpisode == null)
                    _inpatientConsultantEpisode = new Models.InpatientConsultantEpisode(authenticationResponse);

                return _inpatientConsultantEpisode;
            }
        }

        public Models.PersonLanguage personLanguage
        {
            get
            {
                if (_personLanguage == null)
                    _personLanguage = new Models.PersonLanguage(authenticationResponse);

                return _personLanguage;
            }
        }

        public Models.PersonNameHistory personNameHistory
        {
            get
            {
                if (_personNameHistory == null)
                    _personNameHistory = new Models.PersonNameHistory(authenticationResponse);

                return _personNameHistory;
            }
        }

        public Models.PersonAllergicReaction personAllergicReaction
        {
            get
            {
                if (_personAllergicReaction == null)
                    _personAllergicReaction = new Models.PersonAllergicReaction(authenticationResponse);
                return _personAllergicReaction;
            }
        }

        public Models.BusinessModule businessModule
        {
            get
            {
                if (_businessModule == null)
                    _businessModule = new Models.BusinessModule(authenticationResponse);
                return _businessModule;
            }
        }


        public Models.BrokerageOffer brokerageOffer
        {
            get
            {
                if (_brokerageOffer == null)
                    _brokerageOffer = new Models.BrokerageOffer(authenticationResponse);
                return _brokerageOffer;
            }
        }

        public Models.AwaitingCommunicationFromBrokerageOffer awaitingCommunicationFromBrokerageOffer
        {
            get
            {
                if (_awaitingCommunicationFromBrokerageOffer == null)
                    _awaitingCommunicationFromBrokerageOffer = new Models.AwaitingCommunicationFromBrokerageOffer(authenticationResponse);
                return _awaitingCommunicationFromBrokerageOffer;
            }
        }

        public Models.BrokerageOfferCommunication brokerageOfferCommunication
        {
            get
            {
                if (_brokerageOfferCommunication == null)
                    _brokerageOfferCommunication = new Models.BrokerageOfferCommunication(authenticationResponse);
                return _brokerageOfferCommunication;
            }
        }

        public Models.BrokerageEpisodeEscalation brokerageEpisodeEscalation
        {
            get
            {
                if (_brokerageEpisodeEscalation == null)
                    _brokerageEpisodeEscalation = new Models.BrokerageEpisodeEscalation(authenticationResponse);
                return _brokerageEpisodeEscalation;
            }
        }

        public Models.CaseFormMember caseFormMember
        {
            get
            {
                if (_caseFormMember == null)
                    _caseFormMember = new Models.CaseFormMember(authenticationResponse);
                return _caseFormMember;
            }
        }

        public Models.CaseFormAssessmentFactor caseFormAssessmentFactor
        {
            get
            {
                if (_caseFormAssessmentFactor == null)
                    _caseFormAssessmentFactor = new Models.CaseFormAssessmentFactor(authenticationResponse);
                return _caseFormAssessmentFactor;
            }
        }

        public Models.BrokerageEpisodePausePeriod brokerageEpisodePausePeriod
        {
            get
            {
                if (_brokerageEpisodePausePeriod == null)
                    _brokerageEpisodePausePeriod = new Models.BrokerageEpisodePausePeriod(authenticationResponse);
                return _brokerageEpisodePausePeriod;
            }
        }

        public Models.BrokerageEpisodeAttachment brokerageEpisodeAttachment
        {
            get
            {
                if (_brokerageEpisodeAttachment == null)
                    _brokerageEpisodeAttachment = new Models.BrokerageEpisodeAttachment(authenticationResponse);
                return _brokerageEpisodeAttachment;

            }
        }

        public Models.BrokerageOfferAttachment brokerageOfferAttachment
        {
            get
            {
                if (_brokerageOfferAttachment == null)
                    _brokerageOfferAttachment = new Models.BrokerageOfferAttachment(authenticationResponse);
                return _brokerageOfferAttachment;
            }
        }

        public Models.ServiceProvisionStartReason serviceProvisionStartReason
        {
            get
            {
                if (_serviceProvisionStartReason == null)
                    _serviceProvisionStartReason = new Models.ServiceProvisionStartReason(authenticationResponse);
                return _serviceProvisionStartReason;
            }
        }

        public Models.ServiceProvisionEndReason serviceProvisionEndReason
        {
            get
            {
                if (_serviceProvisionEndReason == null)
                    _serviceProvisionEndReason = new Models.ServiceProvisionEndReason(authenticationResponse);
                return _serviceProvisionEndReason;
            }
        }

        public Models.ServiceDelivery serviceDelivery
        {
            get
            {
                if (_serviceDelivery == null)
                    _serviceDelivery = new Models.ServiceDelivery(authenticationResponse);
                return _serviceDelivery;
            }
        }

        public Models.SystemUserLanguage systemUserLanguage
        {
            get
            {
                if (_systemUserLanguage == null)
                    _systemUserLanguage = new Models.SystemUserLanguage(authenticationResponse);
                return _systemUserLanguage;
            }
        }

        public Models.SystemUserAddress systemUserAddress
        {
            get
            {
                if (_systemUserAddress == null)
                    _systemUserAddress = new Models.SystemUserAddress(authenticationResponse);
                return _systemUserAddress;
            }
        }

        public Models.UserApplication userApplication
        {
            get
            {
                if (_userApplication == null)
                    _userApplication = new Models.UserApplication(authenticationResponse);
                return _userApplication;
            }
        }

        public Models.SystemUserAlias systemUserAlias
        {
            get
            {
                if (_systemUserAlias == null)
                    _systemUserAlias = new Models.SystemUserAlias(authenticationResponse);
                return _systemUserAlias;

            }

        }

        public Models.BusinessUnit businessUnit
        {
            get
            {
                if (_businessUnit == null)
                    _businessUnit = new Models.BusinessUnit(authenticationResponse);
                return _businessUnit;
            }
        }

        public Models.CarePhysicalLocation carePhysicalLocation
        {
            get
            {
                if (_carePhysicalLocation == null)
                    _carePhysicalLocation = new Models.CarePhysicalLocation(authenticationResponse);
                return _carePhysicalLocation;
            }
        }

        public Models.CareEquipment careEquipment
        {
            get
            {
                if (_careEquipment == null)
                    _careEquipment = new Models.CareEquipment(authenticationResponse);
                return _careEquipment;
            }
        }

        public Models.CarePlanAgreedBy carePlanAgreedBy
        {
            get
            {
                if (_carePlanAgreedBy == null)
                    _carePlanAgreedBy = new Models.CarePlanAgreedBy(authenticationResponse);
                return _carePlanAgreedBy;
            }
        }

        public Models.CPPersonDayNightCheckObservations cPPersonDayNightCheckObservations
        {
            get
            {
                if (_cPPersonDayNightCheckObservations == null)
                    _cPPersonDayNightCheckObservations = new Models.CPPersonDayNightCheckObservations(authenticationResponse);
                return _cPPersonDayNightCheckObservations;
            }
        }

        public Models.MobilityDistanceUnit mobilityDistanceUnit
        {
            get
            {
                if (_mobilityDistanceUnit == null)
                    _mobilityDistanceUnit = new Models.MobilityDistanceUnit(authenticationResponse);
                return _mobilityDistanceUnit;
            }
        }


        public Models.CareAssistanceNeeded careAssistanceNeeded
        {
            get
            {
                if (_careAssistanceNeeded == null)
                    _careAssistanceNeeded = new Models.CareAssistanceNeeded(authenticationResponse);
                return _careAssistanceNeeded;
            }
        }

        public Models.CareWellbeing careWellbeing
        {
            get
            {
                if (_careWellbeing == null)
                    _careWellbeing = new Models.CareWellbeing(authenticationResponse);
                return _careWellbeing;
            }
        }

        public Models.SpecialistMattress specialistmattress
        {
            get
            {
                if (_specialistmattress == null)
                    _specialistmattress = new Models.SpecialistMattress(authenticationResponse);
                return _specialistmattress;
            }
        }

        public Models.CPPersonMobility cPPersonMobility
        {
            get
            {
                if (_cPPersonMobility == null)
                    _cPPersonMobility = new Models.CPPersonMobility(authenticationResponse);
                return _cPPersonMobility;
            }
        }

        public Models.CPPersonTurning cPPersonTurning
        {
            get
            {
                if (_cPPersonTurning == null)
                    _cPPersonTurning = new Models.CPPersonTurning(authenticationResponse);
                return _cPPersonTurning;
            }
        }

        public Models.CareProviderPersonHandoverDetail careProviderPersonHandoverDetail
        {
            get
            {
                if (_careProviderPersonHandoverDetail == null)
                    _careProviderPersonHandoverDetail = new Models.CareProviderPersonHandoverDetail(authenticationResponse);
                return _careProviderPersonHandoverDetail;
            }
        }

        public Models.ContactReason contactReason
        {
            get
            {
                if (_contactReason == null)
                    _contactReason = new Models.ContactReason(authenticationResponse);
                return _contactReason;
            }
        }

        public Models.ContactSource contactSource
        {
            get
            {
                if (_contactSource == null)
                    _contactSource = new Models.ContactSource(authenticationResponse);
                return _contactSource;
            }
        }

        public Models.DocumentCategory documentCategory
        {
            get
            {
                if (_documentCategory == null)
                    _documentCategory = new Models.DocumentCategory(authenticationResponse);
                return _documentCategory;
            }
        }

        public Models.DocumentType documentType
        {
            get
            {
                if (_documentType == null)
                    _documentType = new Models.DocumentType(authenticationResponse);
                return _documentType;
            }
        }

        public Models.QuestionCatalogue questionCatalogue
        {
            get
            {
                if (_questionCatalogue == null)
                    _questionCatalogue = new Models.QuestionCatalogue(authenticationResponse);
                return _questionCatalogue;
            }
        }

        public Models.BusinessObject businessObject
        {
            get
            {
                if (_businessObject == null)
                    _businessObject = new Models.BusinessObject(authenticationResponse);
                return _businessObject;
            }
        }

        public Models.CaseStatus caseStatus
        {
            get
            {
                if (_caseStatus == null)
                    _caseStatus = new Models.CaseStatus(authenticationResponse);
                return _caseStatus;
            }
        }

        public Models.DataForm dataForm
        {
            get
            {
                if (_dataForm == null)
                    _dataForm = new Models.DataForm(authenticationResponse);
                return _dataForm;
            }
        }


        public Models.AuthenticationProvider authenticationProvider
        {
            get
            {
                if (_authenticationProvider == null)
                    _authenticationProvider = new Models.AuthenticationProvider(authenticationResponse);
                return _authenticationProvider;

            }
        }


        public Models.SystemUserAliasType systemUserAliasType
        {
            get
            {
                if (_systemUserAliasType == null)
                    _systemUserAliasType = new Models.SystemUserAliasType(authenticationResponse);
                return _systemUserAliasType;

            }
        }

        public Models.DemographicsTitle demographicsTitle
        {
            get
            {
                if (_demographicsTitle == null)
                    _demographicsTitle = new Models.DemographicsTitle(authenticationResponse);
                return _demographicsTitle;

            }
        }

        public Models.TransportType transportType
        {
            get
            {
                if (_transportType == null)
                    _transportType = new Models.TransportType(authenticationResponse);
                return _transportType;

            }
        }

        public Models.StaffReview staffReview
        {
            get
            {
                if (_staffReview == null)
                    _staffReview = new Models.StaffReview(authenticationResponse);
                return _staffReview;

            }
        }

        public Models.StaffReviewAttachment staffReviewAttachment
        {
            get
            {
                if (_staffReviewAttachment == null)
                    _staffReviewAttachment = new Models.StaffReviewAttachment(authenticationResponse);
                return _staffReviewAttachment;

            }
        }

        public Models.SystemUserEmploymentContract systemUserEmploymentContract
        {
            get
            {
                if (_systemUserEmploymentContract == null)
                    _systemUserEmploymentContract = new Models.SystemUserEmploymentContract(authenticationResponse);
                return _systemUserEmploymentContract;

            }
        }

        public Models.SystemUserEmploymentContractCPBookingType systemUserEmploymentContractCPBookingType
        {
            get
            {
                if (_systemUserEmploymentContractCPBookingType == null)
                    _systemUserEmploymentContractCPBookingType = new Models.SystemUserEmploymentContractCPBookingType(authenticationResponse);
                return _systemUserEmploymentContractCPBookingType;

            }
        }

        public Models.CareProviderContractScheme careProviderContractScheme
        {
            get
            {
                if (_careProviderContractScheme == null)
                    _careProviderContractScheme = new Models.CareProviderContractScheme(authenticationResponse);
                return _careProviderContractScheme;

            }
        }

        public Models.SystemUserEmploymentContractTeam systemUserEmploymentContractTeam
        {
            get
            {
                if (_systemUserEmploymentContractTeam == null)
                    _systemUserEmploymentContractTeam = new Models.SystemUserEmploymentContractTeam(authenticationResponse);
                return _systemUserEmploymentContractTeam;

            }
        }

        public Models.CPBookingSchedule cpBookingSchedule
        {
            get
            {
                if (_cpBookingSchedule == null)
                    _cpBookingSchedule = new Models.CPBookingSchedule(authenticationResponse);
                return _cpBookingSchedule;

            }
        }

        public Models.CPExpressBookingCriteria cpExpressBookingCriteria
        {
            get
            {
                if (_cpExpressBookingCriteria == null)
                    _cpExpressBookingCriteria = new Models.CPExpressBookingCriteria(authenticationResponse);
                return _cpExpressBookingCriteria;

            }
        }

        public Models.CPExpressBookingProcessed cpExpressBookingProcessed
        {
            get
            {
                if (_cpExpressBookingProcessed == null)
                    _cpExpressBookingProcessed = new Models.CPExpressBookingProcessed(authenticationResponse);
                return _cpExpressBookingProcessed;

            }
        }

        public Models.CPBookingScheduleStaff cpBookingScheduleStaff
        {
            get
            {
                if (_cpBookingScheduleStaff == null)
                    _cpBookingScheduleStaff = new Models.CPBookingScheduleStaff(authenticationResponse);
                return _cpBookingScheduleStaff;

            }
        }

        public Models.SystemUserEmergencyContacts systemUserEmergencyContacts
        {
            get
            {
                if (_systemUserEmergencyContacts == null)
                    _systemUserEmergencyContacts = new Models.SystemUserEmergencyContacts(authenticationResponse);
                return _systemUserEmergencyContacts;

            }
        }

        public Models.CarePlanType carePlanType
        {
            get
            {
                if (_carePlanType == null)
                    _carePlanType = new Models.CarePlanType(authenticationResponse);
                return _carePlanType;
            }
        }


        public Models.AddressGazetteer addressGazetteer
        {
            get
            {
                if (_addressGazetteer == null)
                    _addressGazetteer = new Models.AddressGazetteer(authenticationResponse);
                return _addressGazetteer;

            }
        }

        public Models.AddressBorough addressBorough
        {
            get
            {
                if (_addressBorough == null)
                    _addressBorough = new Models.AddressBorough(authenticationResponse);
                return _addressBorough;

            }
        }

        public Models.AddressWard addressWard
        {
            get
            {
                if (_addressWard == null)
                    _addressWard = new Models.AddressWard(authenticationResponse);
                return _addressWard;

            }
        }

        public Models.CareProviderStaffRoleType careProviderStaffRoleType
        {
            get
            {
                if (_careProviderStaffRoleType == null)
                    _careProviderStaffRoleType = new Models.CareProviderStaffRoleType(authenticationResponse);
                return _careProviderStaffRoleType;

            }
        }
        public Models.Applicant applicant
        {
            get
            {
                if (_applicant == null)
                    _applicant = new Models.Applicant(authenticationResponse);
                return _applicant;

            }
        }
        public Models.RecruitmentRoleApplicant recruitmentRoleApplicant
        {
            get
            {
                if (_recruitmentRoleApplicant == null)
                    _recruitmentRoleApplicant = new Models.RecruitmentRoleApplicant(authenticationResponse);
                return _recruitmentRoleApplicant;

            }
        }
        public Models.EmploymentContractType employmentContractType
        {
            get
            {
                if (_employmentContractType == null)
                    _employmentContractType = new Models.EmploymentContractType(authenticationResponse);
                return _employmentContractType;

            }
        }

        public Models.StaffReviewRequirement staffReviewRequirement
        {
            get
            {
                if (_staffReviewRequirement == null)
                    _staffReviewRequirement = new Models.StaffReviewRequirement(authenticationResponse);
                return _staffReviewRequirement;

            }
        }

        public Models.StaffReviewSetup staffReviewSetup
        {
            get
            {
                if (_staffReviewSetup == null)
                    _staffReviewSetup = new Models.StaffReviewSetup(authenticationResponse);
                return _staffReviewSetup;

            }
        }
        public Models.StaffReviewForm staffReviewForm
        {
            get
            {
                if (_StaffReviewForm == null)
                    _StaffReviewForm = new Models.StaffReviewForm(authenticationResponse);
                return _StaffReviewForm;

            }
        }

        public Models.CareProviderReportableEventAttachment careProviderReportableEventAttachment
        {
            get
            {
                if (_careProviderReportableEventAttachment == null)
                    _careProviderReportableEventAttachment = new Models.CareProviderReportableEventAttachment(authenticationResponse);
                return _careProviderReportableEventAttachment;
            }
        }
        public Models.CareproviderReportableEventAction careProviderReportableEventAction
        {
            get
            {
                if (_careproviderReportableEventAction == null)
                    _careproviderReportableEventAction = new Models.CareproviderReportableEventAction(authenticationResponse);
                return _careproviderReportableEventAction;
            }
        }

        public Models.CPReportableEventBehaviourActionType CPReportableEventBehaviourActionType
        {
            get
            {
                if (_CPReportableEventBehaviourActionType == null)
                    _CPReportableEventBehaviourActionType = new Models.CPReportableEventBehaviourActionType(authenticationResponse);
                return _CPReportableEventBehaviourActionType;
            }
        }

        public Models.CareproviderReportableEventBehaviourType CareproviderReportableEventBehaviourType
        {
            get
            {
                if (_careproviderReportableEventBehaviourType == null)
                    _careproviderReportableEventBehaviourType = new Models.CareproviderReportableEventBehaviourType(authenticationResponse);
                return _careproviderReportableEventBehaviourType;
            }
        }

        public Models.CareproviderReportableEventBehaviour CareproviderReportableEventBehaviour
        {
            get
            {
                if (_careproviderReportableEventBehaviour == null)
                    _careproviderReportableEventBehaviour = new Models.CareproviderReportableEventBehaviour(authenticationResponse);
                return _careproviderReportableEventBehaviour;
            }
        }



        public Models.CareproviderReportableEventImpact careProviderReportableEventImpact
        {
            get
            {
                if (_careproviderReportableEventImpact == null)
                    _careproviderReportableEventImpact = new Models.CareproviderReportableEventImpact(authenticationResponse);
                return _careproviderReportableEventImpact;
            }
        }

        public Models.CareproviderReportableEventRole careProviderReportableEventRole
        {
            get
            {
                if (_careproviderReportableEventRole == null)
                    _careproviderReportableEventRole = new Models.CareproviderReportableEventRole(authenticationResponse);
                return _careproviderReportableEventRole;
            }
        }

        public Models.CareproviderReportableEventCategory careProviderReportableEventCategory
        {
            get
            {
                if (_careproviderReportableEventCategory == null)
                    _careproviderReportableEventCategory = new Models.CareproviderReportableEventCategory(authenticationResponse);
                return _careproviderReportableEventCategory;
            }
        }

        public Models.CareproviderReportableEventSubCategory careProviderReportableEventSubCategory
        {
            get
            {
                if (_careproviderReportableEventSubCategory == null)
                    _careproviderReportableEventSubCategory = new Models.CareproviderReportableEventSubCategory(authenticationResponse);
                return _careproviderReportableEventSubCategory;
            }
        }
        public Models.OrganisationalRiskActionPlan organisationalRiskActionPlan
        {
            get
            {
                if (_organisationalRiskActionPlan == null)
                    _organisationalRiskActionPlan = new Models.OrganisationalRiskActionPlan(authenticationResponse);
                return _organisationalRiskActionPlan;
            }
        }
        public Models.CareproviderReportableEvent careproviderReportableEvent
        {
            get
            {
                if (_careproviderReportableEvent == null)
                    _careproviderReportableEvent = new Models.CareproviderReportableEvent(authenticationResponse);
                return _careproviderReportableEvent;
            }
        }

        public Models.CareproviderReportableEventType careproviderReportableEventType
        {
            get
            {
                if (_careproviderReportableEventType == null)
                    _careproviderReportableEventType = new Models.CareproviderReportableEventType(authenticationResponse);
                return _careproviderReportableEventType;
            }
        }
        public Models.CareproviderReportableEventSeverity careproviderReportableEventSeverity
        {
            get
            {
                if (_careproviderReportableEventSeverity == null)
                    _careproviderReportableEventSeverity = new Models.CareproviderReportableEventSeverity(authenticationResponse);
                return _careproviderReportableEventSeverity;
            }
        }

        public Models.CareproviderReportableEventRole careproviderReportableEventRole
        {
            get
            {
                if (_careproviderReportableEventRole == null)
                    _careproviderReportableEventRole = new Models.CareproviderReportableEventRole(authenticationResponse);
                return _careproviderReportableEventRole;
            }
        }

        public Models.CareproviderReportableEventInjurySeverity careproviderReportableEventInjurySeverity
        {
            get
            {
                if (_careproviderReportableEventInjurySeverity == null)
                    _careproviderReportableEventInjurySeverity = new Models.CareproviderReportableEventInjurySeverity(authenticationResponse);
                return _careproviderReportableEventInjurySeverity;
            }
        }
        public Models.CareproviderReportableEventStatus careproviderReportableEventStatus
        {
            get
            {
                if (_careproviderReportableEventStatus == null)
                    _careproviderReportableEventStatus = new Models.CareproviderReportableEventStatus(authenticationResponse);
                return _careproviderReportableEventStatus;
            }
        }

        public Models.ServiceElement1 serviceElement1
        {
            get
            {
                if (_serviceElement1 == null)
                    _serviceElement1 = new Models.ServiceElement1(authenticationResponse);
                return _serviceElement1;

            }
        }

        public Models.ServiceElement2 serviceElement2
        {
            get
            {
                if (_serviceElement2 == null)
                    _serviceElement2 = new Models.ServiceElement2(authenticationResponse);
                return _serviceElement2;

            }
        }

        public Models.ServiceElement3 serviceElement3
        {
            get
            {
                if (_serviceElement3 == null)
                    _serviceElement3 = new Models.ServiceElement3(authenticationResponse);
                return _serviceElement3;

            }
        }

        public Models.FinanceClientCategory financeClientCategory
        {
            get
            {
                if (_financeClientCategory == null)
                    _financeClientCategory = new Models.FinanceClientCategory(authenticationResponse);
                return _financeClientCategory;

            }
        }

        public Models.GLCodeLocation glCodeLocation
        {
            get
            {
                if (_glCodeLocation == null)
                    _glCodeLocation = new Models.GLCodeLocation(authenticationResponse);
                return _glCodeLocation;

            }
        }

        public Models.GLCode glCode
        {
            get
            {
                if (_glCode == null)
                    _glCode = new Models.GLCode(authenticationResponse);
                return _glCode;

            }
        }

        public Models.RateUnit rateUnit
        {
            get
            {
                if (_rateUnit == null)
                    _rateUnit = new Models.RateUnit(authenticationResponse);
                return _rateUnit;

            }
        }

        public Models.ServiceProvided serviceProvided
        {
            get
            {
                if (_serviceProvided == null)
                    _serviceProvided = new Models.ServiceProvided(authenticationResponse);
                return _serviceProvided;

            }
        }

        public Models.CurrentRanking currentRanking
        {
            get
            {
                if (_currentRanking == null)
                    _currentRanking = new Models.CurrentRanking(authenticationResponse);
                return _currentRanking;

            }
        }

        public Models.PlacementRoomType placementRoomType
        {
            get
            {
                if (_placementRoomType == null)
                    _placementRoomType = new Models.PlacementRoomType(authenticationResponse);
                return _placementRoomType;

            }
        }

        public Models.FinancialAssessmentStatus financialAssessmentStatus
        {
            get
            {
                if (_financialAssessmentStatus == null)
                    _financialAssessmentStatus = new Models.FinancialAssessmentStatus(authenticationResponse);
                return _financialAssessmentStatus;

            }
        }

        public Models.ChargingRuleType chargingRuleType
        {
            get
            {
                if (_chargingRuleType == null)
                    _chargingRuleType = new Models.ChargingRuleType(authenticationResponse);
                return _chargingRuleType;
            }

        }

        public Models.LanguageFluency languageFluency
        {
            get
            {
                if (_languageFluency == null)
                    _languageFluency = new Models.LanguageFluency(authenticationResponse);
                return _languageFluency;

            }
        }


        public Models.IncomeSupportType incomeSupportType
        {
            get
            {
                if (_incomeSupportType == null)
                    _incomeSupportType = new Models.IncomeSupportType(authenticationResponse);
                return _incomeSupportType;
            }
        }

        public Models.SystemUserSuspension systemUserSuspension
        {
            get
            {
                if (_systemUserSuspension == null)
                    _systemUserSuspension = new Models.SystemUserSuspension(authenticationResponse);
                return _systemUserSuspension;

            }
        }

        public Models.IncomeSupportTypeChargingRuleTypes incomeSupportTypeChargingRuleTypes
        {
            get
            {
                if (_incomeSupportTypeChargingRuleTypes == null)
                    _incomeSupportTypeChargingRuleTypes = new Models.IncomeSupportTypeChargingRuleTypes(authenticationResponse);
                return _incomeSupportTypeChargingRuleTypes;
            }
        }

        public Models.InpatientAdmissionSource inpatientAdmissionSource
        {
            get
            {
                if (_inpatientAdmissionSource == null)
                    _inpatientAdmissionSource = new Models.InpatientAdmissionSource(authenticationResponse);
                return _inpatientAdmissionSource;

            }
        }

        public Models.FinanceScheduleType financeScheduleType
        {
            get
            {
                if (_financeScheduleType == null)
                    _financeScheduleType = new Models.FinanceScheduleType(authenticationResponse);
                return _financeScheduleType;

            }
        }


        public Models.InpatientAdmissionMethod inpatientAdmissionMethod
        {
            get
            {
                if (_inpatientAdmissionMethod == null)
                    _inpatientAdmissionMethod = new Models.InpatientAdmissionMethod(authenticationResponse);
                return _inpatientAdmissionMethod;

            }
        }

        public Models.FinancialAssessmentType financialAssessmentType
        {
            get
            {
                if (_financialAssessmentType == null)
                    _financialAssessmentType = new Models.FinancialAssessmentType(authenticationResponse);
                return _financialAssessmentType;

            }
        }

        public Models.ContributionType contributionType
        {
            get
            {
                if (_contributionType == null)
                    _contributionType = new Models.ContributionType(authenticationResponse);
                return _contributionType;

            }
        }

        public Models.RecoveryMethod recoveryMethod
        {
            get
            {
                if (_recoveryMethod == null)
                    _recoveryMethod = new Models.RecoveryMethod(authenticationResponse);
                return _recoveryMethod;

            }
        }

        public Models.DebtorBatchGrouping debtorBatchGrouping
        {
            get
            {
                if (_debtorBatchGrouping == null)
                    _debtorBatchGrouping = new Models.DebtorBatchGrouping(authenticationResponse);
                return _debtorBatchGrouping;

            }
        }

        public Models.CommunityAndClinicTeam communityAndClinicTeam
        {
            get
            {
                if (_communityAndClinicTeam == null)
                    _communityAndClinicTeam = new Models.CommunityAndClinicTeam(authenticationResponse);
                return _communityAndClinicTeam;

            }
        }

        public Models.CommunityClinicDiaryViewSetup communityClinicDiaryViewSetup
        {
            get
            {
                if (_communityClinicDiaryViewSetup == null)
                    _communityClinicDiaryViewSetup = new Models.CommunityClinicDiaryViewSetup(authenticationResponse);
                return _communityClinicDiaryViewSetup;

            }
        }

        public Models.ContactAdministrativeCategory contactAdministrativeCategory
        {
            get
            {
                if (_contactAdministrativeCategory == null)
                    _contactAdministrativeCategory = new Models.ContactAdministrativeCategory(authenticationResponse);
                return _contactAdministrativeCategory;

            }
        }

        public Models.CaseServiceTypeRequested caseServiceTypeRequested
        {
            get
            {
                if (_caseServiceTypeRequested == null)
                    _caseServiceTypeRequested = new Models.CaseServiceTypeRequested(authenticationResponse);
                return _caseServiceTypeRequested;

            }
        }

        public Models.HealthAppointmentContactType healthAppointmentContactType
        {
            get
            {
                if (_healthAppointmentContactType == null)
                    _healthAppointmentContactType = new Models.HealthAppointmentContactType(authenticationResponse);
                return _healthAppointmentContactType;
            }
        }

        public Models.HealthAppointmentReason healthAppointmentReason
        {
            get
            {
                if (_healthAppointmentReason == null)
                    _healthAppointmentReason = new Models.HealthAppointmentReason(authenticationResponse);
                return _healthAppointmentReason;
            }
        }

        public Models.SecondaryCaseReason secondaryCaseReason
        {
            get
            {
                if (_secondaryCaseReason == null)
                    _secondaryCaseReason = new Models.SecondaryCaseReason(authenticationResponse);
                return _secondaryCaseReason;
            }
        }


        public Models.Diagnosis diagnosis
        {
            get
            {
                if (_diagnosis == null)
                    _diagnosis = new Models.Diagnosis(authenticationResponse);
                return _diagnosis;
            }
        }

        public Models.PersonDiagnosis personDiagnosis
        {
            get
            {
                if (_personDiagnosis == null)
                    _personDiagnosis = new Models.PersonDiagnosis(authenticationResponse);
                return _personDiagnosis;
            }
        }

        public Models.PersonDiagnosisEndReason personDiagnosisEndReason
        {
            get
            {
                if (_personDiagnosisEndReason == null)
                    _personDiagnosisEndReason = new Models.PersonDiagnosisEndReason(authenticationResponse);
                return _personDiagnosisEndReason;
            }
        }


        public Models.InpatientLeaveAwol inpatientLeaveAwol
        {
            get
            {
                if (_inpatientLeaveAwol == null)
                    _inpatientLeaveAwol = new Models.InpatientLeaveAwol(authenticationResponse);
                return _inpatientLeaveAwol;
            }
        }

        public Models.InpatientLeaveCancellationReason inpatientLeaveCancellationReason
        {
            get
            {
                if (_inpatientLeaveCancellationReason == null)
                    _inpatientLeaveCancellationReason = new Models.InpatientLeaveCancellationReason(authenticationResponse);
                return _inpatientLeaveCancellationReason;
            }
        }


        public Models.InpatientLeaveType inpatientLeaveType
        {
            get
            {
                if (_inpatientLeaveType == null)
                    _inpatientLeaveType = new Models.InpatientLeaveType(authenticationResponse);
                return _inpatientLeaveType;
            }
        }

        public Models.PersonAbsenceType personAbsenceType
        {
            get
            {
                if (_personAbsenceType == null)
                    _personAbsenceType = new Models.PersonAbsenceType(authenticationResponse);
                return _personAbsenceType;
            }
        }

        public Models.OpenEndedAbsence OpenEndedAbsence
        {
            get
            {
                if (_openendedabsence == null)
                    _openendedabsence = new Models.OpenEndedAbsence(authenticationResponse);
                return _openendedabsence;
            }
        }

        public Models.MissingPerson missingPerson
        {
            get
            {
                if (_missingPerson == null)
                    _missingPerson = new Models.MissingPerson(authenticationResponse);
                return _missingPerson;
            }
        }

        public Models.InpatientLeaveEndReason inpatientLeaveEndReason
        {
            get
            {
                if (_inpatientLeaveEndReason == null)
                    _inpatientLeaveEndReason = new Models.InpatientLeaveEndReason(authenticationResponse);
                return _inpatientLeaveEndReason;
            }
        }


        public Models.InpatientLeaveAwolAttachment inpatientLeaveAwolAttachment
        {
            get
            {
                if (_inpatientLeaveAwolAttachment == null)
                    _inpatientLeaveAwolAttachment = new Models.InpatientLeaveAwolAttachment(authenticationResponse);
                return _inpatientLeaveAwolAttachment;
            }
        }

        public Models.InpatientBedOccupancyHistory inpatientBedOccupancyHistory
        {
            get
            {
                if (_inpatientBedOccupancyHistory == null)
                    _inpatientBedOccupancyHistory = new Models.InpatientBedOccupancyHistory(authenticationResponse);
                return _inpatientBedOccupancyHistory;
            }
        }

        public Models.InpatientSeclusion inpatientSeclusion
        {
            get
            {
                if (_inpatientSeclusion == null)
                    _inpatientSeclusion = new Models.InpatientSeclusion(authenticationResponse);
                return _inpatientSeclusion;
            }
        }

        public Models.InpatientSeclusionReason inpatientSeclusionReason
        {
            get
            {
                if (_inpatientSeclusionReason == null)
                    _inpatientSeclusionReason = new Models.InpatientSeclusionReason(authenticationResponse);
                return _inpatientSeclusionReason;
            }
        }

        public Models.inpatientSeclusionReview inpatientSeclusionReview
        {
            get
            {
                if (_inpatientSeclusionReview == null)
                    _inpatientSeclusionReview = new Models.inpatientSeclusionReview(authenticationResponse);
                return _inpatientSeclusionReview;
            }
        }


        public Models.InpatientSeclusionAttachment inpatientSeclusionAttachment
        {
            get
            {
                if (_inpatientSeclusionAttachment == null)
                    _inpatientSeclusionAttachment = new Models.InpatientSeclusionAttachment(authenticationResponse);
                return _inpatientSeclusionAttachment;
            }
        }

        public Models.OrganisationalRiskType organisationalRiskType
        {
            get
            {
                if (_organisationalRiskType == null)
                    _organisationalRiskType = new Models.OrganisationalRiskType(authenticationResponse);
                return _organisationalRiskType;
            }
        }

        public Models.OrganisationalRisk organisationalRisk
        {
            get
            {
                if (_organisationalRisk == null)
                    _organisationalRisk = new Models.OrganisationalRisk(authenticationResponse);
                return _organisationalRisk;
            }
        }

        public Models.CPBookingType cpBookingType
        {
            get
            {
                if (_cPBookingType == null)
                    _cPBookingType = new Models.CPBookingType(authenticationResponse);
                return _cPBookingType;
            }
        }

        public Models.CPPersonAbsenceReason cPPersonAbsenceReason
        {
            get
            {
                if (_cPPersonAbsenceReason == null)
                    _cPPersonAbsenceReason = new Models.CPPersonAbsenceReason(authenticationResponse);
                return _cPPersonAbsenceReason;
            }
        }

        public Models.CPSchedulingSetup cPSchedulingSetup
        {
            get
            {
                if (_cPSchedulingSetup == null)
                    _cPSchedulingSetup = new Models.CPSchedulingSetup(authenticationResponse);
                return _cPSchedulingSetup;
            }
        }

        public Models.ProviderAllowableBookingTypes providerAllowableBookingTypes
        {
            get
            {
                if (_providerAllowableBookingTypes == null)
                    _providerAllowableBookingTypes = new Models.ProviderAllowableBookingTypes(authenticationResponse);
                return _providerAllowableBookingTypes;
            }
        }


        public Models.OrganisationalRiskCategory organisationalRiskCategory
        {
            get
            {
                if (_organisationalRiskCategory == null)
                    _organisationalRiskCategory = new Models.OrganisationalRiskCategory(authenticationResponse);
                return _organisationalRiskCategory;
            }
        }

        public Models.BusinessObjectField businessObjectField
        {
            get
            {
                if (_businessObjectField == null)
                    _businessObjectField = new Models.BusinessObjectField(authenticationResponse);
                return _businessObjectField;
            }
        }

        public Models.AboutMeSetup aboutMeSetup
        {
            get
            {
                if (_aboutMeSetup == null)
                    _aboutMeSetup = new Models.AboutMeSetup(authenticationResponse);
                return _aboutMeSetup;
            }
        }

        public Models.PersonAboutMe personAboutMe
        {
            get
            {
                if (_personAboutMe == null)
                    _personAboutMe = new Models.PersonAboutMe(authenticationResponse);
                return _personAboutMe;
            }
        }

        public Models.ProviderRoom providerRoom
        {
            get
            {
                if (_providerRoom == null)
                    _providerRoom = new Models.ProviderRoom(authenticationResponse);
                return _providerRoom;
            }
        }

        public Models.ClinicRoom clinicRoom
        {
            get
            {
                if (_clinicRoom == null)
                    _clinicRoom = new Models.ClinicRoom(authenticationResponse);
                return _clinicRoom;
            }
        }

        public Models.CommunityClinicLinkedProfessional communityClinicLinkedProfessional
        {
            get
            {
                if (_communityClinicLinkedProfessional == null)
                    _communityClinicLinkedProfessional = new Models.CommunityClinicLinkedProfessional(authenticationResponse);
                return _communityClinicLinkedProfessional;
            }
        }

        public Models.ClinicSlot clinicSlot
        {
            get
            {
                if (_clinicSlot == null)
                    _clinicSlot = new Models.ClinicSlot(authenticationResponse);
                return _clinicSlot;
            }
        }

        public Models.HealthAppointmentAttendeeAdvocateType healthAppointmentAttendeeAdvocateType
        {
            get
            {
                if (_healthAppointmentAttendeeAdvocateType == null)
                    _healthAppointmentAttendeeAdvocateType = new Models.HealthAppointmentAttendeeAdvocateType(authenticationResponse);
                return _healthAppointmentAttendeeAdvocateType;
            }
        }

        public Models.HealthAppointmentOutcomeType healthAppointmentOutcomeType
        {
            get
            {
                if (_healthAppointmentOutcomeType == null)
                    _healthAppointmentOutcomeType = new Models.HealthAppointmentOutcomeType(authenticationResponse);
                return _healthAppointmentOutcomeType;
            }
        }

        public Models.CommunityClinicCareIntervention communityClinicCareIntervention
        {
            get
            {
                if (_communityClinicCareIntervention == null)
                    _communityClinicCareIntervention = new Models.CommunityClinicCareIntervention(authenticationResponse);
                return _communityClinicCareIntervention;
            }
        }

        public Models.HealthAppointmentAttendingAdvocate healthAppointmentAttendingAdvocate
        {
            get
            {
                if (_healthAppointmentAttendingAdvocate == null)
                    _healthAppointmentAttendingAdvocate = new Models.HealthAppointmentAttendingAdvocate(authenticationResponse);
                return _healthAppointmentAttendingAdvocate;
            }
        }


        public Models.ClinicBookedSlot clinicBookedSlot
        {
            get
            {
                if (_clinicBookedSlot == null)
                    _clinicBookedSlot = new Models.ClinicBookedSlot(authenticationResponse);
                return _clinicBookedSlot;
            }
        }

        public Models.AvailabilityTypes availabilityTypes
        {
            get
            {
                if (_availabilityTypes == null)
                    _availabilityTypes = new Models.AvailabilityTypes(authenticationResponse);
                return _availabilityTypes;
            }
        }


        public Models.RateType rateType
        {
            get
            {
                if (_rateType == null)
                    _rateType = new Models.RateType(authenticationResponse);
                return _rateType;

            }
        }

        public Models.ServiceMapping serviceMapping
        {
            get
            {
                if (_serviceMapping == null)
                    _serviceMapping = new Models.ServiceMapping(authenticationResponse);
                return _serviceMapping;

            }
        }

        public Models.CareType careType
        {
            get
            {
                if (_careType == null)
                    _careType = new Models.CareType(authenticationResponse);
                return _careType;

            }
        }

        public Models.CareTask careTask
        {
            get
            {
                if (_careTask == null)
                    _careTask = new Models.CareTask(authenticationResponse);
                return _careTask;

            }
        }


        public Models.ServiceElement1ValidRateUnits serviceElement1ValidRateUnits
        {
            get
            {
                if (_serviceElement1ValidRateUnits == null)
                    _serviceElement1ValidRateUnits = new Models.ServiceElement1ValidRateUnits(authenticationResponse);
                return _serviceElement1ValidRateUnits;

            }
        }

        public Models.ServiceGLCoding serviceGLCoding
        {
            get
            {
                if (_serviceGLCoding == null)
                    _serviceGLCoding = new Models.ServiceGLCoding(authenticationResponse);
                return _serviceGLCoding;

            }
        }


        public Models.ChildProtectionCategoryOfAbuse childProtectionCategoryOfAbuse
        {
            get
            {
                if (_childProtectionCategoryOfAbuse == null)
                    _childProtectionCategoryOfAbuse = new Models.ChildProtectionCategoryOfAbuse(authenticationResponse);
                return _childProtectionCategoryOfAbuse;
            }
        }

        public Models.ChildProtectionStatusType childProtectionStatusType
        {
            get
            {
                if (_childProtectionStatusType == null)
                    _childProtectionStatusType = new Models.ChildProtectionStatusType(authenticationResponse);
                return _childProtectionStatusType;
            }
        }

        public Models.ChildProtectionEndReasonType childProtectionEndReasonType
        {
            get
            {
                if (_childProtectionEndReasonType == null)
                    _childProtectionEndReasonType = new Models.ChildProtectionEndReasonType(authenticationResponse);
                return _childProtectionEndReasonType;
            }
        }

        public Models.PersonRelationshipType personRelationshipType
        {
            get
            {
                if (_personRelationshipType == null)
                    _personRelationshipType = new Models.PersonRelationshipType(authenticationResponse);
                return _personRelationshipType;
            }
        }

        public Models.SystemUserSuspensionContract systemUserSuspensionContract
        {
            get
            {
                if (_systemUserSuspensionContract == null)
                    _systemUserSuspensionContract = new Models.SystemUserSuspensionContract(authenticationResponse);
                return _systemUserSuspensionContract;

            }
        }

        public Models.SystemUserSuspensionReason systemUserSuspensionReason
        {
            get
            {
                if (_systemUserSuspensionReason == null)
                    _systemUserSuspensionReason = new Models.SystemUserSuspensionReason(authenticationResponse);
                return _systemUserSuspensionReason;

            }
        }

        public Models.ApplicationSource applicationSource
        {
            get
            {
                if (_applicationSource == null)
                    _applicationSource = new Models.ApplicationSource(authenticationResponse);
                return _applicationSource;
            }
        }

        public Models.Compliance compliance
        {
            get
            {
                if (_compliance == null)
                    _compliance = new Models.Compliance(authenticationResponse);
                return _compliance;
            }
        }

        public Models.StaffTrainingItem staffTrainingItem
        {
            get
            {
                if (_staffTrainingItem == null)
                    _staffTrainingItem = new Models.StaffTrainingItem(authenticationResponse);
                return _staffTrainingItem;
            }
        }

        public Models.TrainingRequirementSetup trainingRequirementSetup
        {
            get
            {
                if (_trainingRequirementSetup == null)
                    _trainingRequirementSetup = new Models.TrainingRequirementSetup(authenticationResponse);
                return _trainingRequirementSetup;
            }
        }

        public Models.TrainingRequirement trainingRequirement
        {
            get
            {
                if (_trainingRequirement == null)
                    _trainingRequirement = new Models.TrainingRequirement(authenticationResponse);
                return _trainingRequirement;
            }
        }

        public Models.SystemUserTraining systemUserTraining
        {
            get
            {
                if (_systemUserTraining == null)
                    _systemUserTraining = new Models.SystemUserTraining(authenticationResponse);
                return _systemUserTraining;
            }
        }

        public Models.RejectedReason rejectedReason
        {
            get
            {
                if (_rejectedReason == null)
                    _rejectedReason = new Models.RejectedReason(authenticationResponse);
                return _rejectedReason;
            }
        }

        public Models.ApplicantLanguage applicantLanguage
        {
            get
            {
                if (_applicantLanguage == null)
                    _applicantLanguage = new Models.ApplicantLanguage(authenticationResponse);
                return _applicantLanguage;
            }
        }

        public Models.ApplicantAlias applicantAlias
        {
            get
            {
                if (_applicantAlias == null)
                    _applicantAlias = new Models.ApplicantAlias(authenticationResponse);
                return _applicantAlias;
            }
        }

        public Models.RequirementLastChasedOutcome requirementLastChasedOutcome
        {
            get
            {
                if (_requirementLastChasedOutcome == null)
                    _requirementLastChasedOutcome = new Models.RequirementLastChasedOutcome(authenticationResponse);
                return _requirementLastChasedOutcome;
            }
        }

        public Models.ComplianceItemAttachment complianceItemAttachment
        {
            get
            {
                if (_complianceItemAttachment == null)
                    _complianceItemAttachment = new Models.ComplianceItemAttachment(authenticationResponse);
                return _complianceItemAttachment;
            }
        }

        public Models.OptionSet optionSet
        {
            get
            {
                if (_optionSet == null)
                    _optionSet = new Models.OptionSet(authenticationResponse);
                return _optionSet;
            }
        }

        public Models.OptionsetValue optionsetValue
        {
            get
            {
                if (_optionsetValue == null)
                    _optionsetValue = new Models.OptionsetValue(authenticationResponse);
                return _optionsetValue;
            }
        }

        public Models.ExtractName extractName
        {
            get
            {
                if (_extractName == null)
                    _extractName = new Models.ExtractName(authenticationResponse);
                return _extractName;
            }
        }

        public Models.InvoiceBy invoiceBy
        {
            get
            {
                if (_invoiceBy == null)
                    _invoiceBy = new Models.InvoiceBy(authenticationResponse);
                return _invoiceBy;
            }
        }

        public Models.InvoiceFrequency invoiceFrequency
        {
            get
            {
                if (_invoiceFrequency == null)
                    _invoiceFrequency = new Models.InvoiceFrequency(authenticationResponse);
                return _invoiceFrequency;
            }
        }

        public Models.FinanceInvoiceBatchSetup financeInvoiceBatchSetup
        {
            get
            {
                if (_financeInvoiceBatchSetup == null)
                    _financeInvoiceBatchSetup = new Models.FinanceInvoiceBatchSetup(authenticationResponse);
                return _financeInvoiceBatchSetup;
            }
        }

        public Models.FormCancellationReason formCancellationReason
        {
            get
            {
                if (_formCancellationReason == null)
                    _formCancellationReason = new Models.FormCancellationReason(authenticationResponse);
                return _formCancellationReason;
            }
        }

        public Models.PersonTargetGroup personTargetGroup
        {
            get
            {
                if (_personTargetGroup == null)
                    _personTargetGroup = new Models.PersonTargetGroup(authenticationResponse);
                return _personTargetGroup;
            }
        }

        public Models.ServicePermission servicePermission
        {
            get
            {
                if (_servicePermission == null)
                    _servicePermission = new Models.ServicePermission(authenticationResponse);
                return _servicePermission;

            }
        }

        public Models.PersonalBudgetType personalBudgetType
        {
            get
            {
                if (_personalBudgetType == null)
                    _personalBudgetType = new Models.PersonalBudgetType(authenticationResponse);
                return _personalBudgetType;

            }
        }

        public Models.BrokerageRequestSource brokerageRequestSource
        {
            get
            {
                if (_brokerageRequestSource == null)
                    _brokerageRequestSource = new Models.BrokerageRequestSource(authenticationResponse);
                return _brokerageRequestSource;

            }
        }

        public Models.BrokerageEpisodePriority brokerageEpisodePriority
        {
            get
            {
                if (_brokerageEpisodePriority == null)
                    _brokerageEpisodePriority = new Models.BrokerageEpisodePriority(authenticationResponse);
                return _brokerageEpisodePriority;

            }
        }

        public Models.BrokerageEpisodePauseReason brokerageEpisodePauseReason
        {
            get
            {
                if (_brokerageEpisodePauseReason == null)
                    _brokerageEpisodePauseReason = new Models.BrokerageEpisodePauseReason(authenticationResponse);
                return _brokerageEpisodePauseReason;

            }
        }

        public Models.BrokerageEpisodeTrackingStatus brokerageEpisodeTrackingStatus
        {
            get
            {
                if (_brokerageEpisodeTrackingStatus == null)
                    _brokerageEpisodeTrackingStatus = new Models.BrokerageEpisodeTrackingStatus(authenticationResponse);
                return _brokerageEpisodeTrackingStatus;

            }
        }

        public Models.BrokerageTargetSetup brokerageTargetSetup
        {
            get
            {
                if (_brokerageTargetSetup == null)
                    _brokerageTargetSetup = new Models.BrokerageTargetSetup(authenticationResponse);
                return _brokerageTargetSetup;

            }
        }

        public Models.BrokerageTargetTrackingStatusSetup brokerageTargetTrackingStatusSetup
        {
            get
            {
                if (_brokerageTargetTrackingStatusSetup == null)
                    _brokerageTargetTrackingStatusSetup = new Models.BrokerageTargetTrackingStatusSetup(authenticationResponse);
                return _brokerageTargetTrackingStatusSetup;

            }
        }

        public Models.BrokerageOfferRejectionReason brokerageOfferRejectionReason
        {
            get
            {
                if (_brokerageOfferRejectionReason == null)
                    _brokerageOfferRejectionReason = new Models.BrokerageOfferRejectionReason(authenticationResponse);
                return _brokerageOfferRejectionReason;

            }
        }

        public Models.GLCodeMapping glCodeMapping
        {
            get
            {
                if (_glCodeMapping == null)
                    _glCodeMapping = new Models.GLCodeMapping(authenticationResponse);
                return _glCodeMapping;

            }
        }

        public Models.BrokerageCarePackageType brokerageCarePackageType
        {
            get
            {
                if (_brokerageCarePackageType == null)
                    _brokerageCarePackageType = new Models.BrokerageCarePackageType(authenticationResponse);
                return _brokerageCarePackageType;

            }
        }

        public Models.BrokerageOfferCancellationReason brokerageOfferCancellationReason
        {
            get
            {
                if (_brokerageOfferCancellationReason == null)
                    _brokerageOfferCancellationReason = new Models.BrokerageOfferCancellationReason(authenticationResponse);
                return _brokerageOfferCancellationReason;

            }
        }

        public Models.BrokerageOfferAwaitingCommunicationFrom brokerageOfferAwaitingCommunicationFrom
        {
            get
            {
                if (_brokerageOfferAwaitingCommunicationFrom == null)
                    _brokerageOfferAwaitingCommunicationFrom = new Models.BrokerageOfferAwaitingCommunicationFrom(authenticationResponse);
                return _brokerageOfferAwaitingCommunicationFrom;

            }
        }

        public Models.BrokerageOfferCommunicationOutcome brokerageOfferCommunicationOutcome
        {
            get
            {
                if (_brokerageOfferCommunicationOutcome == null)
                    _brokerageOfferCommunicationOutcome = new Models.BrokerageOfferCommunicationOutcome(authenticationResponse);
                return _brokerageOfferCommunicationOutcome;

            }
        }

        public Models.BrokerageExistingCarePackage brokerageExistingCarePackage
        {
            get
            {
                if (_brokerageExistingCarePackage == null)
                    _brokerageExistingCarePackage = new Models.BrokerageExistingCarePackage(authenticationResponse);
                return _brokerageExistingCarePackage;

            }
        }

        public Models.ContactMethod contactMethod
        {
            get
            {
                if (_contactMethod == null)
                    _contactMethod = new Models.ContactMethod(authenticationResponse);
                return _contactMethod;

            }
        }

        public Models.BankHoliday bankHoliday
        {
            get
            {
                if (_bankHoliday == null)
                    _bankHoliday = new Models.BankHoliday(authenticationResponse);
                return _bankHoliday;

            }
        }

        public Models.BrokerageEpisodeRejectionReason brokerageEpisodeRejectionReason
        {
            get
            {
                if (_brokerageEpisodeRejectionReason == null)
                    _brokerageEpisodeRejectionReason = new Models.BrokerageEpisodeRejectionReason(authenticationResponse);
                return _brokerageEpisodeRejectionReason;

            }
        }

        public Models.BrokerageEpisodeCancellationReason brokerageEpisodeCancellationReason
        {
            get
            {
                if (_brokerageEpisodeCancellationReason == null)
                    _brokerageEpisodeCancellationReason = new Models.BrokerageEpisodeCancellationReason(authenticationResponse);
                return _brokerageEpisodeCancellationReason;

            }
        }

        public Models.CaseFormOutcomeType caseFormOutcomeType
        {
            get
            {
                if (_caseFormOutcomeType == null)
                    _caseFormOutcomeType = new Models.CaseFormOutcomeType(authenticationResponse);
                return _caseFormOutcomeType;

            }
        }

        public Models.AssessmentFactorType assessmentFactorType
        {
            get
            {
                if (_assessmentFactorType == null)
                    _assessmentFactorType = new Models.AssessmentFactorType(authenticationResponse);
                return _assessmentFactorType;

            }
        }

        public Models.SignificantEventSubCategory significantEventSubCategory
        {
            get
            {
                if (_significantEventSubCategory == null)
                    _significantEventSubCategory = new Models.SignificantEventSubCategory(authenticationResponse);
                return _significantEventSubCategory;
            }
        }

        public Models.InpatientLeaveAwolCaseNote inpatientLeaveAwolCaseNote
        {
            get
            {
                if (_inpatientLeaveAwolCaseNote == null)
                    _inpatientLeaveAwolCaseNote = new Models.InpatientLeaveAwolCaseNote(authenticationResponse);
                return _inpatientLeaveAwolCaseNote;
            }
        }

        public Models.CasePriority casePriority
        {
            get
            {
                if (_casePriority == null)
                    _casePriority = new Models.CasePriority(authenticationResponse);
                return _casePriority;
            }
        }

        public Models.FosteringExperience fosteringExperience
        {
            get
            {
                if (_fosteringExperience == null)
                    _fosteringExperience = new Models.FosteringExperience(authenticationResponse);
                return _fosteringExperience;
            }
        }

        public Models.ChildInNeedCode childInNeedCode
        {
            get
            {
                if (_childInNeedCode == null)
                    _childInNeedCode = new Models.ChildInNeedCode(authenticationResponse);
                return _childInNeedCode;
            }
        }

        public Models.ErrorManagementReason errorManagementReason
        {
            get
            {
                if (_errorManagementReason == null)
                    _errorManagementReason = new Models.ErrorManagementReason(authenticationResponse);
                return _errorManagementReason;
            }
        }

        public Models.FormDelayReason formDelayReason
        {
            get
            {
                if (_formDelayReason == null)
                    _formDelayReason = new Models.FormDelayReason(authenticationResponse);
                return _formDelayReason;
            }
        }

        public Models.PersonFormAttachment personFormAttachment
        {
            get
            {
                if (_personFormAttachment == null)
                    _personFormAttachment = new Models.PersonFormAttachment(authenticationResponse);
                return _personFormAttachment;
            }
        }

        public Models.DocumentOutcomeType documentOutcomeType
        {
            get
            {
                if (_documentOutcomeType == null)
                    _documentOutcomeType = new Models.DocumentOutcomeType(authenticationResponse);
                return _documentOutcomeType;

            }
        }

        public Models.ServiceProvisionRatePeriod serviceProvisionRatePeriod
        {
            get
            {
                if (_serviceProvisionRatePeriod == null)
                    _serviceProvisionRatePeriod = new Models.ServiceProvisionRatePeriod(authenticationResponse);
                return _serviceProvisionRatePeriod;

            }
        }

        public Models.ServiceProvisionRateSchedule serviceProvisionRateSchedule
        {
            get
            {
                if (_serviceProvisionRateSchedule == null)
                    _serviceProvisionRateSchedule = new Models.ServiceProvisionRateSchedule(authenticationResponse);
                return _serviceProvisionRateSchedule;

            }
        }

        public Models.DocumentBusinessObjectMapping documentBusinessObjectMapping
        {
            get
            {
                if (_documentBusinessObjectMapping == null)
                    _documentBusinessObjectMapping = new Models.DocumentBusinessObjectMapping(authenticationResponse);
                return _documentBusinessObjectMapping;

            }
        }

        public Models.CommunityClinicRestriction communityClinicRestriction
        {
            get
            {
                if (_communityClinicRestriction == null)
                    _communityClinicRestriction = new Models.CommunityClinicRestriction(authenticationResponse);
                return _communityClinicRestriction;

            }
        }


        public Models.ClinicalRiskFactorType clinicalRiskFactorType
        {
            get
            {
                if (_clinicalRiskFactorType == null)
                    _clinicalRiskFactorType = new Models.ClinicalRiskFactorType(authenticationResponse);
                return _clinicalRiskFactorType;

            }
        }

        public Models.ContactCaseNote contactCaseNote
        {
            get
            {
                if (_contactCaseNote == null)
                    _contactCaseNote = new Models.ContactCaseNote(authenticationResponse);
                return _contactCaseNote;

            }
        }

        public Models.ClinicalRiskFactorSubType clinicalRiskFactorSubType
        {
            get
            {
                if (_clinicalRiskFactorSubType == null)
                    _clinicalRiskFactorSubType = new Models.ClinicalRiskFactorSubType(authenticationResponse);
                return _clinicalRiskFactorSubType;

            }
        }

        public Models.ClinicalRiskfactorEndReason clinicalRiskfactorEndReason
        {
            get
            {
                if (_clinicalRiskfactorEndReason == null)
                    _clinicalRiskfactorEndReason = new Models.ClinicalRiskfactorEndReason(authenticationResponse);
                return _clinicalRiskfactorEndReason;

            }
        }

        public Models.ClinicalRiskLevel clinicalRiskLevel
        {
            get
            {
                if (_clinicalRiskLevel == null)
                    _clinicalRiskLevel = new Models.ClinicalRiskLevel(authenticationResponse);
                return _clinicalRiskLevel;

            }
        }

        public Models.MHASectionSetup mhaSectionSetup
        {
            get
            {
                if (_mhaSectionSetup == null)
                    _mhaSectionSetup = new Models.MHASectionSetup(authenticationResponse);
                return _mhaSectionSetup;
            }
        }

        public Models.MHACourtDateOutcomeCaseNote mhaCourtDateOutcomeCaseNote
        {
            get
            {
                if (_mhaCourtDateOutcomeCaseNote == null)
                    _mhaCourtDateOutcomeCaseNote = new Models.MHACourtDateOutcomeCaseNote(authenticationResponse);
                return _mhaCourtDateOutcomeCaseNote;
            }
        }

        public Models.PersonMHALegalStatusCaseNote personMHALegalStatusCaseNote
        {
            get
            {
                if (_personMHALegalStatusCaseNote == null)
                    _personMHALegalStatusCaseNote = new Models.PersonMHALegalStatusCaseNote(authenticationResponse);
                return _personMHALegalStatusCaseNote;
            }
        }

        public Models.GestationPeriodEndReason gestationPeriodEndReason
        {
            get
            {
                if (_gestationPeriodEndReason == null)
                    _gestationPeriodEndReason = new Models.GestationPeriodEndReason(authenticationResponse);
                return _gestationPeriodEndReason;
            }
        }

        public Models.PersonCarePlanCaseNote personCarePlanCaseNote
        {
            get
            {
                if (_personCarePlanCaseNote == null)
                    _personCarePlanCaseNote = new Models.PersonCarePlanCaseNote(authenticationResponse);
                return _personCarePlanCaseNote;
            }
        }

        public Models.ClinicalRiskStatus clinicalRiskStatus
        {
            get
            {
                if (_clinicalRiskStatus == null)
                    _clinicalRiskStatus = new Models.ClinicalRiskStatus(authenticationResponse);
                return _clinicalRiskStatus;
            }
        }

        public Models.PersonClinicalRiskStatus personClinicalRiskStatus
        {
            get
            {
                if (_personClinicalRiskStatus == null)
                    _personClinicalRiskStatus = new Models.PersonClinicalRiskStatus(authenticationResponse);
                return _personClinicalRiskStatus;
            }
        }

        public Models.PersonClinicalRiskStatusCaseNote personClinicalRiskStatusCaseNote
        {
            get
            {
                if (_personClinicalRiskStatusCaseNote == null)
                    _personClinicalRiskStatusCaseNote = new Models.PersonClinicalRiskStatusCaseNote(authenticationResponse);
                return _personClinicalRiskStatusCaseNote;
            }
        }

        public Models.PersonHeightAndWeight personHeightAndWeight
        {
            get
            {
                if (_personHeightAndWeight == null)
                    _personHeightAndWeight = new Models.PersonHeightAndWeight(authenticationResponse);
                return _personHeightAndWeight;
            }
        }

        public Models.PersonHeightAndWeightCaseNote personHeightAndWeightCaseNote
        {
            get
            {
                if (_personHeightAndWeightCaseNote == null)
                    _personHeightAndWeightCaseNote = new Models.PersonHeightAndWeightCaseNote(authenticationResponse);
                return _personHeightAndWeightCaseNote;
            }
        }

        public Models.DisabilityType disabilityType
        {
            get
            {
                if (_disabilityType == null)
                    _disabilityType = new Models.DisabilityType(authenticationResponse);
                return _disabilityType;
            }
        }

        public Models.ImpairmentType impairmentType
        {
            get
            {
                if (_impairmentType == null)
                    _impairmentType = new Models.ImpairmentType(authenticationResponse);
                return _impairmentType;
            }
        }

        public Models.ImmunisationType immunisationType
        {
            get
            {
                if (_immunisationType == null)
                    _immunisationType = new Models.ImmunisationType(authenticationResponse);
                return _immunisationType;
            }
        }

        public Models.ServicePackage servicePackage
        {
            get
            {
                if (_servicePackage == null)
                    _servicePackage = new Models.ServicePackage(authenticationResponse);
                return _servicePackage;
            }
        }

        public Models.ServicePackageType servicePackageType
        {
            get
            {
                if (_servicePackageType == null)
                    _servicePackageType = new Models.ServicePackageType(authenticationResponse);
                return _servicePackageType;
            }
        }

        public Models.ServiceUprate serviceUprate
        {
            get
            {
                if (_serviceUprate == null)
                    _serviceUprate = new Models.ServiceUprate(authenticationResponse);
                return _serviceUprate;
            }
        }

        public Models.ServiceUprateDetail serviceUprateDetail
        {
            get
            {
                if (_serviceUprateDetail == null)
                    _serviceUprateDetail = new Models.ServiceUprateDetail(authenticationResponse);
                return _serviceUprateDetail;
            }
        }

        public Models.DebtorHeaderText debtorHeaderText
        {
            get
            {
                if (_debtorHeaderText == null)
                    _debtorHeaderText = new Models.DebtorHeaderText(authenticationResponse);
                return _debtorHeaderText;
            }
        }

        public Models.DebtorTransactionText debtorTransactionText
        {
            get
            {
                if (_debtorTransactionText == null)
                    _debtorTransactionText = new Models.DebtorTransactionText(authenticationResponse);
                return _debtorTransactionText;
            }
        }

        public Models.DebtorRecoveryText debtorRecoveryText
        {
            get
            {
                if (_debtorRecoveryText == null)
                    _debtorRecoveryText = new Models.DebtorRecoveryText(authenticationResponse);
                return _debtorRecoveryText;
            }
        }

        public Models.AlertAndHazardType alertAndHazardType
        {
            get
            {
                if (_alertAndHazardType == null)
                    _alertAndHazardType = new Models.AlertAndHazardType(authenticationResponse);
                return _alertAndHazardType;
            }
        }

        public Models.AlertAndHazardEndReason alertAndHazardEndReason
        {
            get
            {
                if (_alertAndHazardEndReason == null)
                    _alertAndHazardEndReason = new Models.AlertAndHazardEndReason(authenticationResponse);
                return _alertAndHazardEndReason;
            }
        }

        public Models.AlertAndHazardReviewOutcome alertAndHazardReviewOutcome
        {
            get
            {
                if (_alertAndHazardReviewOutcome == null)
                    _alertAndHazardReviewOutcome = new Models.AlertAndHazardReviewOutcome(authenticationResponse);
                return _alertAndHazardReviewOutcome;
            }
        }

        public Models.AdultSafeguardingCategoryOfAbuse adultSafeguardingCategoryOfAbuse
        {
            get
            {
                if (_adultSafeguardingCategoryOfAbuse == null)
                    _adultSafeguardingCategoryOfAbuse = new Models.AdultSafeguardingCategoryOfAbuse(authenticationResponse);
                return _adultSafeguardingCategoryOfAbuse;
            }
        }

        public Models.AdultSafeguardingStatus adultSafeguardingStatus
        {
            get
            {
                if (_adultSafeguardingStatus == null)
                    _adultSafeguardingStatus = new Models.AdultSafeguardingStatus(authenticationResponse);
                return _adultSafeguardingStatus;
            }
        }

        public Models.AllegationCategory allegationCategory
        {
            get
            {
                if (_allegationCategory == null)
                    _allegationCategory = new Models.AllegationCategory(authenticationResponse);
                return _allegationCategory;
            }
        }

        public Models.PrimarySupportReasonType primarySupportReasonType
        {
            get
            {
                if (_primarySupportReasonType == null)
                    _primarySupportReasonType = new Models.PrimarySupportReasonType(authenticationResponse);
                return _primarySupportReasonType;
            }
        }

        public Models.PersonFormOutcomeType personFormOutcomeType
        {
            get
            {
                if (_personFormOutcomeType == null)
                    _personFormOutcomeType = new Models.PersonFormOutcomeType(authenticationResponse);
                return _personFormOutcomeType;

            }
        }

        public Models.PrivateFosteringArrangement privateFosteringArrangement
        {
            get
            {
                if (_privateFosteringArrangement == null)
                    _privateFosteringArrangement = new Models.PrivateFosteringArrangement(authenticationResponse);
                return _privateFosteringArrangement;
            }
        }

        public Models.PrivateFosteringArrangementCaseNote privateFosteringArrangementCaseNote
        {
            get
            {
                if (_privateFosteringArrangementCaseNote == null)
                    _privateFosteringArrangementCaseNote = new Models.PrivateFosteringArrangementCaseNote(authenticationResponse);
                return _privateFosteringArrangementCaseNote;
            }
        }

        public Models.MHARightsAndRequests mhaRightsAndRequests
        {
            get
            {
                if (_mhaRightsAndRequests == null)
                    _mhaRightsAndRequests = new Models.MHARightsAndRequests(authenticationResponse);
                return _mhaRightsAndRequests;
            }
        }

        public Models.MHARightsAndRequestsCaseNote mhaRightsAndRequestsCaseNote
        {
            get
            {
                if (_mhaRightsAndRequestsCaseNote == null)
                    _mhaRightsAndRequestsCaseNote = new Models.MHARightsAndRequestsCaseNote(authenticationResponse);
                return _mhaRightsAndRequestsCaseNote;
            }
        }

        public Models.MHAAftercareEntitlement mhaAftercareEntitlement
        {
            get
            {
                if (_mhaAftercareEntitlement == null)
                    _mhaAftercareEntitlement = new Models.MHAAftercareEntitlement(authenticationResponse);
                return _mhaAftercareEntitlement;
            }
        }

        public Models.MHAAftercareEntitlementCaseNote mhaAftercareEntitlementCaseNote
        {
            get
            {
                if (_mhaAftercareEntitlementCaseNote == null)
                    _mhaAftercareEntitlementCaseNote = new Models.MHAAftercareEntitlementCaseNote(authenticationResponse);
                return _mhaAftercareEntitlementCaseNote;
            }
        }

        public Models.FinanceGeneralSettings financeGeneralSettings
        {
            get
            {
                if (_financeGeneralSettings == null)
                    _financeGeneralSettings = new Models.FinanceGeneralSettings(authenticationResponse);
                return _financeGeneralSettings;
            }
        }

        public Models.FinanceInvoiceStatus financeInvoiceStatus
        {
            get
            {
                if (_financeInvoiceStatus == null)
                    _financeInvoiceStatus = new Models.FinanceInvoiceStatus(authenticationResponse);
                return _financeInvoiceStatus;
            }
        }

        public Models.FinanceTransactionTrigger financeTransactionTrigger
        {
            get
            {
                if (_financeTransactionTrigger == null)
                    _financeTransactionTrigger = new Models.FinanceTransactionTrigger(authenticationResponse);
                return _financeTransactionTrigger;
            }
        }

        public Models.ProtectiveMarkingScheme protectiveMarkingScheme
        {
            get
            {
                if (_protectiveMarkingScheme == null)
                    _protectiveMarkingScheme = new Models.ProtectiveMarkingScheme(authenticationResponse);
                return _protectiveMarkingScheme;
            }
        }

        public Models.PersonSignificantEventChronology personSignificantEventChronology
        {
            get
            {
                if (_personSignificantEventChronology == null)
                    _personSignificantEventChronology = new Models.PersonSignificantEventChronology(authenticationResponse);
                return _personSignificantEventChronology;
            }
        }

        public Models.PersonMhaAppeal personMhaAppeal
        {
            get
            {
                if (_personMhaAppeal == null)
                    _personMhaAppeal = new Models.PersonMhaAppeal(authenticationResponse);
                return _personMhaAppeal;
            }
        }

        public Models.MhaRecordOfAppeal mhaRecordOfAppeal
        {
            get
            {
                if (_mhaRecordOfAppeal == null)
                    _mhaRecordOfAppeal = new Models.MhaRecordOfAppeal(authenticationResponse);
                return _mhaRecordOfAppeal;
            }
        }

        public Models.MhaRecordOfAppealCaseNote mhaRecordOfAppealCaseNote
        {
            get
            {
                if (_mhaRecordOfAppealCaseNote == null)
                    _mhaRecordOfAppealCaseNote = new Models.MhaRecordOfAppealCaseNote(authenticationResponse);
                return _mhaRecordOfAppealCaseNote;
            }
        }

        public Models.AccommodationType accommodationType
        {
            get
            {
                if (_accommodationType == null)
                    _accommodationType = new Models.AccommodationType(authenticationResponse);
                return _accommodationType;
            }
        }

        public Models.LowerSuperOutputArea lowerSuperOutputArea
        {
            get
            {
                if (_lowerSuperOutputArea == null)
                    _lowerSuperOutputArea = new Models.LowerSuperOutputArea(authenticationResponse);
                return _lowerSuperOutputArea;
            }
        }

        public Models.ModeOfCommunication modeOfCommunication
        {
            get
            {
                if (_modeOfCommunication == null)
                    _modeOfCommunication = new Models.ModeOfCommunication(authenticationResponse);
                return _modeOfCommunication;
            }
        }

        public Models.PersonDocumentFormat personDocumentFormat
        {
            get
            {
                if (_personDocumentFormat == null)
                    _personDocumentFormat = new Models.PersonDocumentFormat(authenticationResponse);
                return _personDocumentFormat;
            }
        }

        public Models.ImmigrationStatus immigrationStatus
        {
            get
            {
                if (_immigrationStatus == null)
                    _immigrationStatus = new Models.ImmigrationStatus(authenticationResponse);
                return _immigrationStatus;
            }
        }

        public Models.Religion religion
        {
            get
            {
                if (_religion == null)
                    _religion = new Models.Religion(authenticationResponse);
                return _religion;
            }
        }

        public Models.Country country
        {
            get
            {
                if (_country == null)
                    _country = new Models.Country(authenticationResponse);
                return _country;
            }
        }

        public Models.LeavingCareEligibility leavingCareEligibility
        {
            get
            {
                if (_leavingCareEligibility == null)
                    _leavingCareEligibility = new Models.LeavingCareEligibility(authenticationResponse);
                return _leavingCareEligibility;
            }
        }

        public Models.UPNUnknownReason upnUnknownReason
        {
            get
            {
                if (_upnUnknownReason == null)
                    _upnUnknownReason = new Models.UPNUnknownReason(authenticationResponse);
                return _upnUnknownReason;
            }
        }

        public Models.SexualOrientation sexualOrientation
        {
            get
            {
                if (_sexualOrientation == null)
                    _sexualOrientation = new Models.SexualOrientation(authenticationResponse);
                return _sexualOrientation;
            }
        }

        public Models.PersonCarePlanReview personCarePlanReview
        {
            get
            {
                if (_personCarePlanReview == null)
                    _personCarePlanReview = new Models.PersonCarePlanReview(authenticationResponse);
                return _personCarePlanReview;
            }
        }

        public Models.CarePlanReviewOutcome carePlanReviewOutcome
        {
            get
            {
                if (_carePlanReviewOutcome == null)
                    _carePlanReviewOutcome = new Models.CarePlanReviewOutcome(authenticationResponse);
                return _carePlanReviewOutcome;
            }
        }

        public Models.InpatientConsultantEpisodeEndReason inpatientConsultantEpisodeEndReason
        {
            get
            {
                if (_inpatientConsultantEpisodeEndReason == null)
                    _inpatientConsultantEpisodeEndReason = new Models.InpatientConsultantEpisodeEndReason(authenticationResponse);
                return _inpatientConsultantEpisodeEndReason;
            }
        }

        public Models.CareProviderStaffRoleTypeGroup careProviderStaffRoleTypeGroup
        {
            get
            {
                if (_careProviderStaffRoleTypeGroup == null)
                    _careProviderStaffRoleTypeGroup = new Models.CareProviderStaffRoleTypeGroup(authenticationResponse);
                return _careProviderStaffRoleTypeGroup;
            }
        }

        public Models.CPBankHolidayChargingCalendar cpBankHolidayChargingCalendar
        {
            get
            {
                if (_cpBankHolidayChargingCalendar == null)
                    _cpBankHolidayChargingCalendar = new Models.CPBankHolidayChargingCalendar(authenticationResponse);
                return _cpBankHolidayChargingCalendar;
            }
        }

        public Models.CPBankHolidayDate cpBankHolidayDate
        {
            get
            {
                if (_cpBankHolidayDate == null)
                    _cpBankHolidayDate = new Models.CPBankHolidayDate(authenticationResponse);
                return _cpBankHolidayDate;
            }
        }

        public Models.CareProviderBankHolidayType careProviderBankHolidayType
        {
            get
            {
                if (_careProviderBankHolidayType == null)
                    _careProviderBankHolidayType = new Models.CareProviderBankHolidayType(authenticationResponse);
                return _careProviderBankHolidayType;
            }
        }

        public Models.Pronouns pronouns
        {
            get
            {
                if (_pronouns == null)
                    _pronouns = new Models.Pronouns(authenticationResponse);
                return _pronouns;
            }
        }

        public Models.CareProviderServiceMapping careProviderServiceMapping
        {
            get
            {
                if (_careProviderServiceMapping == null)
                    _careProviderServiceMapping = new Models.CareProviderServiceMapping(authenticationResponse);
                return _careProviderServiceMapping;
            }
        }

        public Models.CareProviderServiceDetail careProviderServiceDetail
        {
            get
            {
                if (_careProviderServiceDetail == null)
                    _careProviderServiceDetail = new Models.CareProviderServiceDetail(authenticationResponse);
                return _careProviderServiceDetail;
            }
        }

        public Models.CareProviderExtractName careProviderExtractName
        {
            get
            {
                if (_careProviderExtractName == null)
                    _careProviderExtractName = new Models.CareProviderExtractName(authenticationResponse);
                return _careProviderExtractName;
            }
        }

        public Models.CareProviderExtractType careProviderExtractType
        {
            get
            {
                if (_careProviderExtractType == null)
                    _careProviderExtractType = new Models.CareProviderExtractType(authenticationResponse);
                return _careProviderExtractType;
            }
        }

        public Models.CareProviderFinanceExtractBatchSetup careProviderFinanceExtractBatchSetup
        {
            get
            {
                if (_careProviderFinanceExtractBatchSetup == null)
                    _careProviderFinanceExtractBatchSetup = new Models.CareProviderFinanceExtractBatchSetup(authenticationResponse);
                return _careProviderFinanceExtractBatchSetup;
            }
        }

        public Models.CareProviderFinanceExtractBatch careProviderFinanceExtractBatch
        {
            get
            {
                if (_careProviderFinanceExtractBatch == null)
                    _careProviderFinanceExtractBatch = new Models.CareProviderFinanceExtractBatch(authenticationResponse);
                return _careProviderFinanceExtractBatch;
            }
        }

        public Models.CareProviderFinanceCodeMapping careProviderFinanceCodeMapping
        {
            get
            {
                if (_careProviderFinanceCodeMapping == null)
                    _careProviderFinanceCodeMapping = new Models.CareProviderFinanceCodeMapping(authenticationResponse);
                return _careProviderFinanceCodeMapping;
            }
        }

        public Models.CareProviderPersonContractServiceRatePeriod careProviderPersonContractServiceRatePeriod
        {
            get
            {
                if (_careProviderPersonContractServiceRatePeriod == null)
                    _careProviderPersonContractServiceRatePeriod = new Models.CareProviderPersonContractServiceRatePeriod(authenticationResponse);
                return _careProviderPersonContractServiceRatePeriod;
            }
        }

        public Models.CareProviderFinanceInvoiceBatch careProviderFinanceInvoiceBatch
        {
            get
            {
                if (_careProviderFinanceInvoiceBatch == null)
                    _careProviderFinanceInvoiceBatch = new Models.CareProviderFinanceInvoiceBatch(authenticationResponse);
                return _careProviderFinanceInvoiceBatch;
            }
        }

        public Models.CareProviderFinanceInvoice careProviderFinanceInvoice
        {
            get
            {
                if (_careProviderFinanceInvoice == null)
                    _careProviderFinanceInvoice = new Models.CareProviderFinanceInvoice(authenticationResponse);
                return _careProviderFinanceInvoice;
            }
        }

        public Models.CareProviderFinanceTransaction careProviderFinanceTransaction
        {
            get
            {
                if (_careProviderFinanceTransaction == null)
                    _careProviderFinanceTransaction = new Models.CareProviderFinanceTransaction(authenticationResponse);
                return _careProviderFinanceTransaction;
            }
        }

        public Models.CareProviderPersonContractServiceChargePerWk careProviderPersonContractServiceChargePerWk
        {
            get
            {
                if (_careProviderPersonContractServiceChargePerWk == null)
                    _careProviderPersonContractServiceChargePerWk = new Models.CareProviderPersonContractServiceChargePerWk(authenticationResponse);
                return _careProviderPersonContractServiceChargePerWk;
            }
        }

        public Models.CPBands cPBands
        {
            get
            {
                if (_cPBands == null)
                    _cPBands = new Models.CPBands(authenticationResponse);
                return _cPBands;
            }
        }

        public Models.CPBookingScheduleDeletionReason cpBookingScheduleDeletionReason
        {
            get
            {
                if (_cpBookingScheduleDeletionReason == null)
                    _cpBookingScheduleDeletionReason = new Models.CPBookingScheduleDeletionReason(authenticationResponse);
                return _cpBookingScheduleDeletionReason;
            }
        }

        public Models.CPBookingTypeClashAction cpBookingTypeClashAction
        {
            get
            {
                if (_cpBookingTypeClashAction == null)
                    _cpBookingTypeClashAction = new Models.CPBookingTypeClashAction(authenticationResponse);
                return _cpBookingTypeClashAction;
            }
        }

        public Models.CPBookingDiaryDeletionReason cpBookingDiaryDeletionReason
        {
            get
            {
                if (_cpBookingDiaryDeletionReason == null)
                    _cpBookingDiaryDeletionReason = new Models.CPBookingDiaryDeletionReason(authenticationResponse);
                return _cpBookingDiaryDeletionReason;
            }
        }

        public Models.CareProviderChargeApportionment careProviderChargeApportionment
        {
            get
            {
                if (_careProviderChargeApportionment == null)
                    _careProviderChargeApportionment = new Models.CareProviderChargeApportionment(authenticationResponse);
                return _careProviderChargeApportionment;
            }
        }

        public Models.CareProviderChargeApportionmentDetail careProviderChargeApportionmentDetail
        {
            get
            {
                if (_careProviderChargeApportionmentDetail == null)
                    _careProviderChargeApportionmentDetail = new Models.CareProviderChargeApportionmentDetail(authenticationResponse);
                return _careProviderChargeApportionmentDetail;
            }
        }

        public Models.AllergyType allergyType
        {
            get
            {
                if (_allergyType == null)
                    _allergyType = new Models.AllergyType(authenticationResponse);
                return _allergyType;
            }
        }

        public Models.PaymentMethod paymentMethod
        {
            get
            {
                if (_paymentMethod == null)
                    _paymentMethod = new Models.PaymentMethod(authenticationResponse);
                return _paymentMethod;
            }
        }

        public Models.CareProviderFinanceInvoicePayment careProviderFinanceInvoicePayment
        {
            get
            {
                if (_careProviderFinanceInvoicePayment == null)
                    _careProviderFinanceInvoicePayment = new Models.CareProviderFinanceInvoicePayment(authenticationResponse);
                return _careProviderFinanceInvoicePayment;
            }
        }

        public Models.CareProviderAccountingPeriod careProviderAccountingPeriod
        {
            get
            {
                if (_careProviderAccountingPeriod == null)
                    _careProviderAccountingPeriod = new Models.CareProviderAccountingPeriod(authenticationResponse);
                return _careProviderAccountingPeriod;
            }
        }

        public Models.CareProviderFinanceInvoicePaymentReportType careProviderFinanceInvoicePaymentReportType
        {
            get
            {
                if (_careProviderFinanceInvoicePaymentReportType == null)
                    _careProviderFinanceInvoicePaymentReportType = new Models.CareProviderFinanceInvoicePaymentReportType(authenticationResponse);
                return _careProviderFinanceInvoicePaymentReportType;
            }
        }

        public Models.CPKeyworkerNotesAttachment cpKeyworkerNotesAttachment
        {
            get
            {
                if (_cpKeyworkerNotesAttachment == null)
                    _cpKeyworkerNotesAttachment = new Models.CPKeyworkerNotesAttachment(authenticationResponse);
                return _cpKeyworkerNotesAttachment;
            }
        }

        public Models.CPPersonDayNightCheck cpPersonDayNightCheck
        {
            get
            {
                if (_cpPersonDayNightCheck == null)
                    _cpPersonDayNightCheck = new Models.CPPersonDayNightCheck(authenticationResponse);
                return _cpPersonDayNightCheck;
            }
        }

        public Models.CPPersonToileting cpPersonToileting
        {
            get
            {
                if (_cpPersonToileting == null)
                    _cpPersonToileting = new Models.CPPersonToileting(authenticationResponse);
                return _cpPersonToileting;
            }
        }

        public Models.CPPersonKeyworkerNote cpPersonKeyworkerNote
        {
            get
            {
                if (_cpPersonKeyworkerNote == null)
                    _cpPersonKeyworkerNote = new Models.CPPersonKeyworkerNote(authenticationResponse);
                return _cpPersonKeyworkerNote;
            }
        }

        public Models.CpMobilityAttachment cpMobilityAttachment
        {
            get
            {
                if (_cpMobilityAttachment == null)
                    _cpMobilityAttachment = new Models.CpMobilityAttachment(authenticationResponse);
                return _cpMobilityAttachment;
            }
        }

        public Models.CpPersonCarePreferences cpPersonCarePreferences
        {
            get
            {
                if (_cpPersonCarePreferences == null)
                    _cpPersonCarePreferences = new Models.CpPersonCarePreferences(authenticationResponse);
                return (_cpPersonCarePreferences);
            }
        }

        public Models.CpPersonBehaviourIncident cpPersonBehaviourIncident
        {
            get
            {
                if (_cpPersonBehaviourIncident == null)
                    _cpPersonBehaviourIncident = new Models.CpPersonBehaviourIncident(authenticationResponse);
                return _cpPersonBehaviourIncident;
            }
        }

        public Models.IncidentTrigger incidentTrigger
        {
            get
            {
                if (_incidentTrigger == null)
                    _incidentTrigger = new Models.IncidentTrigger(authenticationResponse);
                return _incidentTrigger;
            }
        }

        public Models.PersonConversations personConversations
        {
            get
            {
                if (_personConversations == null)
                    _personConversations = new Models.PersonConversations(authenticationResponse);
                return _personConversations;
            }
        }

        public Models.FoodAndFluidMealType foodAndFluidMealType
        {
            get
            {
                if (_foodAndFluidMealType == null)
                    _foodAndFluidMealType = new Models.FoodAndFluidMealType(authenticationResponse);
                return _foodAndFluidMealType;
            }
        }

        public Models.FoodAmountOffered foodAmountOffered
        {
            get
            {
                if (_foodAmountOffered == null)
                    _foodAmountOffered = new Models.FoodAmountOffered(authenticationResponse);
                return _foodAmountOffered;
            }
        }

        public Models.FoodAmountEaten foodAmountEaten
        {
            get
            {
                if (_foodAmountEaten == null)
                    _foodAmountEaten = new Models.FoodAmountEaten(authenticationResponse);
                return _foodAmountEaten;
            }
        }

        public Models.TypeOfFluid typeOfFluid
        {
            get
            {
                if (_typeOfFluid == null)
                    _typeOfFluid = new Models.TypeOfFluid(authenticationResponse);
                return _typeOfFluid;
            }
        }

        public Models.FluidAmountOffered fluidAmountOffered
        {
            get
            {
                if (_fluidAmountOffered == null)
                    _fluidAmountOffered = new Models.FluidAmountOffered(authenticationResponse);
                return _fluidAmountOffered;
            }
        }

        public Models.NonOralFluidDelivery nonOralFluidDelivery
        {
            get
            {
                if (_nonOralFluidDelivery == null)
                    _nonOralFluidDelivery = new Models.NonOralFluidDelivery(authenticationResponse);
                return _nonOralFluidDelivery;
            }
        }

        public Models.CpPersonFoodAndFluid cpPersonFoodAndFluid
        {
            get
            {
                if (_cpPersonFoodAndFluid == null)
                    _cpPersonFoodAndFluid = new Models.CpPersonFoodAndFluid(authenticationResponse);
                return _cpPersonFoodAndFluid;
            }
        }

        public Models.MotionSensorType motionSensorType
        {
            get
            {
                if (_motionSensorType == null)
                    _motionSensorType = new Models.MotionSensorType(authenticationResponse);
                return _motionSensorType;
            }
        }


        #endregion


        public DatabaseHelper()
        {
            int count = 0;
            while (count < 600) //try for 10 minutes to authenticate
            {
                try
                {
                    authenticationResponse = AuthenticateUser();
                    break;
                }
                catch (System.ServiceModel.ServerTooBusyException ex)
                {
                    count = count + 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public DatabaseHelper(string TenantName = null)
        {
            int count = 0;
            while (count < 600) //try for 10 minutes to authenticate
            {
                try
                {
                    authenticationResponse = AuthenticateUser(TenantName);
                    break;
                }
                catch (System.ServiceModel.ServerTooBusyException ex)
                {
                    count = count + 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public DatabaseHelper(string username, string password, string TenantName = null)
        {
            int count = 0;
            while (count < 600) //try for 10 minutes to authenticate
            {
                try
                {
                    authenticationResponse = AuthenticateUser(username, password, TenantName);
                    break;
                }
                catch (System.ServiceModel.ServerTooBusyException ex)
                {
                    count = count + 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}

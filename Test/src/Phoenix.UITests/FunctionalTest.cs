using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Extensions;
using Phoenix.UITests.Framework.PageObjects;
using Phoenix.UITests.Framework.PageObjects.Cases;
using Phoenix.UITests.Framework.PageObjects.Cases.Health;
using Phoenix.UITests.Framework.PageObjects.ExpressBook;
using Phoenix.UITests.Framework.PageObjects.Finance;
using Phoenix.UITests.Framework.PageObjects.People;
using Phoenix.UITests.Framework.PageObjects.Providers;
using Phoenix.UITests.Framework.PageObjects.QualityAndCompliance;
using Phoenix.UITests.Framework.PageObjects.Recruitment;
using Phoenix.UITests.Framework.PageObjects.Recruitment.ApplicantPage;
using Phoenix.UITests.Framework.PageObjects.Settings;
using Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin;
using Phoenix.UITests.Framework.PageObjects.Settings.Customizations;
using Phoenix.UITests.Framework.PageObjects.Settings.Security;
using Phoenix.UITests.Framework.PageObjects.StaffReview;
using System;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Phoenix.UITests
{
    public abstract class FunctionalTest
    {
        public IWebDriver driver;
        public int defaultTimeoutSeconds;
        public OpenQA.Selenium.Support.UI.WebDriverWait wait;
        public string appURL;
        public string browser;
        public string updateTestResult;

        public Framework.WebAppAPI.WebAPIHelper WebAPIHelper;
        public DBHelper.DatabaseHelper dbHelper;
        public string DownloadsDirectory;
        public Framework.FileSystem.FileIOHelper fileIOHelper;
        public Framework.OfficeTools.MSWordHelper msWordHelper;
        public Phoenix.UITests.Framework.PDFTools.PdfHelper pdfHelper;
        public Framework.Image.ImageHelper imageHelper;
        public Framework.CommonMethods.CommonMethods commonMethodsHelper;
        public Phoenix.UITests.CommonMethodsDB commonMethodsDB;

        internal AtlassianServiceAPI.Models.Zapi zapi;
        internal AtlassianServiceAPI.Models.JiraApi jiraAPI;
        internal AtlassianServicesAPI.AtlassianService atlassianService;
        internal string versionName;


        #region Private variables

        private BookingPaymentRecordPage _bookingPaymentRecordPage;
        private BookingPaymentsPage _bookingPaymentsPage;
        private PayrollBatchRecordPage _payrollBatchRecordPage;
        private PayrollBatchesPage _payrollBatchesPage;
        private ChangeBookingStaffStatusDialogPopup _changeBookingStaffStatusDialogPopup;
        private PersonalSafetyAndEnvironmentRecordPage _personalSafetyAndEnvironmentRecordPage;
        private PersonalSafetyAndEnvironmentPage _personalSafetyAndEnvironmentPage;
        private PainManagementRecordPage _painManagementRecordPage;
        private PainManagementPage _painManagementPage;
        private AttachmentsForFoodAndFluid _attachmentsForFoodAndFluid;
        private FoodAndFluidRecordPage _foodAndFluidRecordPage;
        private FoodAndFluidPage _foodAndFluidPage;
        private EmotionalSupportRecordPage _emotionalSupportRecordPage;
        private EmotionalSupportPage _emotionalSupportPage;
        private DistressedBehaviourRecordPage _distressedBehaviourRecordPage;
        private DistressedBehavioursPage _distressedBehavioursPage;
        private PersonDiaryEventsPage _personDiaryEventsPage;
        private PersonDiaryEventRecordPage _personDiaryEventRecordPage;
        private PersonActivityRecordPage _personActivityRecordPage;
        private PersonActivitiesPage _personActivitiesPage;
        private PersonalisedCareAndSupportPlanRecordPage _personalisedCareAndSupportPlanRecordPage;
        private SelectPersonPhysicalObservationTypePopup _selectPersonPhysicalObservationTypePopup;
        private HeightAndWeightRecordPage _heightAndWeightRecordPage;
        private HeightAndWeightPage _heightAndWeightPage;
        private AttachmentForDailyPersonalCareRecordPage _attachmentForDailyPersonalCareRecordPage;
        private AttachmentsForDailyPersonalCare _attachmentsForDailyPersonalCare;
        private ContinenceCareRecordPage _continenceCareRecordPage;
        private ContinenceCarePage _continenceCarePage;
        private PersonDailyRecord_RecordPage _personDailyRecord_RecordPage;
        private PersonDailyRecordsPage _personDailyRecordsPage;
        private PersonDailyPersonalCareRecordPage _personDailyPersonalCareRecordPage;
        private PersonRepositioningRecordPage _personRepositioningRecordPage;
        private PersonRepositioningPage _personRepositioningPage;
        private PersonWelfareChecksPage _personWelfareChecksPage;
        private PersonWelfareCheckRecordPage _personWelfareCheckRecordPage;
        private PersonDailyPersonalCarePage _personDailyPersonalCarePage;
        private ClonePersonContractServiceRatePeriodPopup _clonePersonContractServiceRatePeriodPopup;
        private SpecifyCareWorkerPopup _specifyCareWorkerPopup;
        private CopyBookingToDialogPopup _copyBookingToDialogPopup;
        private WallchartWarningDialogPopup _wallchartWarningDialogPopup;
        private BandRateScheduleRecordPage _bandRateScheduleRecordPage;
        private BandRateSchedulesPage _bandRateSchedulesPage;
        private BandRateTypeRecordPage _bandRateTypeRecordPage;
        private BandRateTypesPage _bandRateTypesPage;
        private CPMasterPayArrangementRecordPage _cpMasterPayArrangementRecordPage;
        private CPMasterPayArrangementsPage _cpMasterPayArrangementsPage;
        private FinanceCodeUpdaterPopup _financeCodeUpdaterPopup;
        private ProcessScheduledBookingsForWeekCommencingPopup _processScheduledBookingsForWeekCommencingPopup;
        private ExpressBookingCriteriaPage _expressBookingCriteriaPage;
        private ExpressBookingCriteriaRecordPage _expressBookingCriteriaRecordPage;
        private SelectMultiplePeoplePopup _selectMultiplePeoplePopup;
        private EmployeeDiaryPage _employeeDiaryPage;
        private CreateDiaryBookingPopup _createDiaryBookingPopup;
        private ContractServiceBandedRatesRecordPage _contractServiceBandedRatesRecordPage;
        private ContractServiceBandedRatesPage _contractServiceBandedRatesPage;
        private CloningContractServiceRatePeriodPopup _cloningContractServiceRatePeriodPopup;
        private ContractServiceRatePeriodRecordPage _contractServiceRatePeriodRecordPage;
        private ContractServiceRatePeriodsPage _contractServiceRatePeriodsPage;
        private ContractServiceRecordPage _contractServiceRecordPage;
        private ContractServicesPage _contractServicesPage;
        private AttachmentForPersonalMoneyAccountDetailRecordPage _attachmentForPersonalMoneyAccountDetailRecordPage;
        private AttachmentsForPersonalMoneyAccountDetailPage _attachmentsForPersonalMoneyAccountDetailPage;
        private AttachmentForPersonalMoneyAccountRecordPage _attachmentForPersonalMoneyAccountRecordPage;
        private AttachmentsForPersonalMoneyAccountPage _attachmentsForPersonalMoneyAccountPage;
        private PersonalMoneyAccountDetailRecordPage _personalMoneyAccountDetailRecordPage;
        private PersonalMoneyAccountDetailsPage _personalMoneyAccountDetailsPage;
        private PersonalMoneyAccountsPage _personalMoneyAccountsPage;
        private PersonalMoneyAccountRecordPage _personalMoneyAccountRecordPage;
        private ContractServicesWithRatesPage _contractServicesWithRatesPage;
        private RecruitmentDocumentsManagementPage _recruitmentDocumentsManagementPage;
        private SystemUserRecruitmentDocumentsPage _systemUserRecruitmentDocumentsPage;
        private SystemUserRecruitmentDashboardPage _systemUserRecruitmentDashboardPage;
        private ApplicantDashboardPage _applicantDashboardPage;
        private AvailabilityErrorDialogPopup _availabilityErrorDialogPopup;
        private ScheduleAvailabilityPopup _scheduleAvailabilityPopup;
        private AvailabilitySaveChangesDialogPopup _availabilitySaveChangesDialogPopup;
        private EndContractActionPopup _endContractActionPopup;
        private PersonSpecificTrainingRecordPage _personSpecificTrainingRecordPage;
        private PersonSpecificTrainingsPage _personSpecificTrainings;
        private ReasonForChangeOfSocialWorkerDialogPopup _reasonForChangeOfSocialWorkerDialogPopup;
        private CaseInvolvementRecordPage _caseInvolvementRecordPage;
        private CaseInvolvementsPage _caseInvolvementsPage;
        private SocialWorkerChangeReasonRecordPage _socialWorkerChangeReasonRecordPage;
        private SocialWorkerChangeReasonsPage _socialWorkerChangeReasonsPage;
        private Phoenix.UITests.Framework.PageObjects.People.PersonAbsencesPage _PersonAbsencesPage;
        private MainMenuPinnedRecordsArea _mainMenuPinnedRecordsArea;
        private Phoenix.UITests.Framework.PageObjects.People.PersonContractsPage _personContractsPage;
        private Phoenix.UITests.Framework.PageObjects.People.PersonContractRecordPage _personContractRecordPage;
        private CaseAttachmentsPage _caseAttachmentsPage;
        private CaseAttachmentRecordPage _caseAttachmentRecordPage;
        private EndContractWithSuspensionDatePopup _endContractWithSuspensionDatePopup;
        private StaffReviewRequirementsPopup _staffReviewRequirementsPopup;
        private AppointmentDialogPopup _appointmentDialogPopup;
        private DiaryViewSetupRestrictionsPage _diaryViewSetupRestrictionsPage;
        private DiaryViewSetupRestrictionRecordPage _diaryViewSetupRestrictionRecordPage;
        private LinkedProfessionalsPage _linkedProfessionalsPage;
        private LinkedProfessionalRecordPage _linkedProfessionalRecordPage;
        private DiaryViewSetupPage _diaryViewSetupPage;
        private DiaryViewSetupRecordPage _diaryViewSetupRecordPage;
        private HealthSetupPage _healthSetupPage;
        private CommunityClinicTeamsPage _communityClinicTeamsPage;
        private CommunityClinicTeamRecordPage _communityClinicTeamRecordPage;
        private PersonAddressRecordPage _personAddressRecordPage;
        private CloneServiceDeliveryPopup _cloneServiceDeliveryPopup;
        private ServiceDeliveriesPage _serviceDeliveriesPage;
        private ServiceDeliveryRecordPage _serviceDeliveryRecordPage;
        private RTTWaitTimesPage _rttWaitTimesPage;
        private RTTWaitTimeRecordPage _rttWaitTimeRecordPage;
        private RTTEventsPage _rttEventsPage;
        private RTTEventRecordPage _rttEventRecordPage;
        private ComplaintsAndFeedbackPage _complaintsAndFeedbackPage;
        private ComplaintAndFeedbackRecordPage _complaintAndFeedbackRecordPage;
        private AttendedEducationEstablishmentsPage _attendedEducationEstablishmentsPage;
        private AttendedEducationEstablishmentRecordPage _attendedEducationEstablishmentRecordPage;
        private EducationYearsPage _educationYearsPage;
        private EducationYearRecordPage _educationYearRecordPage;
        private ReactivateCasePopup _reactivateCasePopup;
        private TrainingRequirementsSetupPage _trainingRequirementsSetupPage;
        private TrainingRequirementSetupRecordPage _trainingRequirementSetupRecordPage;
        private TransportTypesPage _transportTypesPage;
        private TransportTypeRecordPage _transportTypeRecordPage;
        private ServiceProvisionCostPerWeekPage _serviceProvisionCostPerWeekPage;
        private ServiceProvisionCostPerWeekRecordPage _serviceProvisionCostPerWeekRecordPage;
        private FeesPage _feesPage;
        private FeeRecordPage _feeRecordPage;
        private ServiceProvisionAllowancesPage _serviceProvisionAllowancesPage;
        private ServiceProvisionAllowanceRecordPage _serviceProvisionAllowanceRecordPage;
        private FinancialAssessmentContributionsPage _financialAssessmentContributionsPage;
        private FinancialAssessmentContributionRecordPage _financialAssessmentContributionRecordPage;
        private StaffRecruitmentItemsPage _staffRecruitmentItemsPage;
        private StaffRecruitmentItemRecordPage _staffRecruitmentItemRecordPage;
        private BusinessModulesChecklistPage _businessModulesChecklistPage;
        private SystemUserTrainingAttachmentsPage _systemUserTrainingAttachmentsPage;
        private SystemUserTrainingAttachmentRecordPage _systemUserTrainingAttachmentRecordPage;
        private SystemUserTrainingPage _systemUserTrainingPage;
        private SystemUserTrainingRecordPage _systemUserTrainingRecordPage;
        private SystemUserAbsencesPage _systemUserAbsencesPage;
        private SystemUserAbsencesRecordPage _systemUserAbsencesRecordPage;
        private ServiceMappingRecordPage _serviceMappingRecordPage;
        private ServiceMappingsPage _serviceMappingsPage;
        private RecruitmentRequirementsPage _recruitmentRequirementsPage;
        private RecruitmentRequirementRecordPage _recruitmentRequirementRecordPage;
        private CopyCarePlanPopupPage _copyCarePlanPopupPage;
        private NewCareNeedPopupPage _newCareNeedPopupPage;
        private Testing_CDV6_17721EditAssessmentPage _testing_CDV6_17721EditAssessmentPage;
        private AvailabilityDinamicDialogPopup _availabilityDinamicDialogPopup;
        private AmendDiaryBookingPopUp _amendDiaryBookingPopUp;
        private ApplicantScheduleTransportSubPage _applicantScheduleTransportSubPage;
        private ApplicantSelectScheduledTransportPopup _applicantSelectScheduledTransportPopup;
        private CalendarPickerPopup _calendarPickerPopup;
        private LoginPage _loginPage;
        private HomePage _homePage;
        private HomePage_DashboardsArea _homePage_DashboardsArea;
        private MainMenu _mainMenu;
        private ProfessionalsPage _professionalsPage;
        private ProfessionalRecordPage _professionalRecordPage;
        private DocumentsPage _documentsPage;
        private DocumentPage _documentPage;
        private DocumentApplicationAccessPage _documentApplicationAccessPage;
        private DocumentApplicationAccessRecordPage _documentApplicationAccessRecordPage;
        private DocumentSectionsSubPage _documentSectionsSubPage;
        private DocumentSectionRecordPage _documentSectionRecordPage;
        private DocumentSectionApplicationAccessPage _documentSectionApplicationAccessPage;
        private DocumentSectionApplicationAccessRecordPage _documentSectionApplicationAccessRecordPage;
        private DocumentSectionQuestionsSubPage _documentSectionQuestionsSubPage;
        private DocumentSectionQuestionRecordPage _documentSectionQuestionRecordPage;
        private DocumentSectionQuestionApplicationAccessPage _documentSectionQuestionApplicationAccessPage;
        private DocumentSectionQuestionApplicationAccessRecordPage _documentSectionQuestionApplicationAccessRecordPage;
        private AutomatedUiTestDocument4PreviewPage _automatedUiTestDocument4PreviewPage;
        private MattTestScreeningToolTestSDEMapCopyAnswersPage _mattTestScreeningToolTestSDEMapCopyAnswersPage;
        private AutomatedUITestDocument2SDEMapCopyAnswersPage _automatedUITestDocument2SDEMapCopyAnswersPage;
        private AutomatedUITestDocument2EditAssessmentPage _automatedUITestDocument2EditAssessmentPage;
        private ManageSDEMapsPopup _manageSDEMapsPopup;
        private CasesPage _casesPage;
        private PeoplePage _peoplePage;
        private ProviderDiaryPage _providerDiaryPage;
        private ProviderSchedulePage _providerSchedulePage;
        private PeopleDiaryPage _peopleDiaryPage;
        private DiaryBookingsPage _diaryBookingsPage;
        private DiaryBookingsRecordPage _diaryBookingsRecordPage;
        private BookingDiaryStaffPage _bookingDiaryStaffPage;
        private PersonRecordPage _personRecordPage;
        private PersonHealthAbsencesPage _personHealthAbsencesPage;
        private PersonAbsencesRecordPage _personAbsencesRecordPage;
        private PersonHealthAbsencesRecordPage _personHealthAbsencesRecordPage;
        private PersonRecordEditPage _personRecordEditPage;
        private PersonRelationshipPage _personRelationshipPage;
        private PersonFinancialAssessmentsPage _personFinancialAssessmentsPage;
        private PersonServiceProvisionsPage _personServiceProvisionsPage;
        private Person_DiaryRecordPage _personDiaryRecordPage;
        private ServiceProvisionRecordPage _serviceProvisionRecordPage;
        private FinancialAssessmentRecordPage _financialAssessmentRecordPage;
        private FinancialAssessmentChargesSubPage _financialAssessmentChargesSubPage;
        private FinancialAssessmentChargePage _financialAssessmentChargePage;
        private CaseFormPage _caseFormPage;
        private LookupPopup _lookupPopup;
        private LookupMultiSelectPopup _lookupMultiSelectPopup;
        private RegularCareLookupPopUp _regularCareLookupPopUp;
        private LookupViewPopup _lookupViewPopup;
        private CaseCasesFormPage _caseCasesFormPage;
        private CaseRecordPage _caseRecordPage;
        private PrintAssessmentPopup _printAssessmentPopup;
        private AssessmentPrintHistoryPopup _assessmentPrintHistoryPopup;
        private AlertPopup _alertPopup;
        private EditPrintAuditRecordPopup _editPrintAuditRecordPopup;
        private ShareRecordPopup _shareRecordPopup;
        private ShareRecordResultsPopup _shareRecordResultsPopup;
        private AutomatedUITestDocument1EditAssessmentPage _automatedUITestDocument1EditAssessmentPage;
        private EditSignaturePopup _editSignaturePopup;
        private SectionInformationDialoguePopup _sectionInformationDialoguePopup;
        private QuestionInformationDialoguePopup _questionInformationDialoguePopup;
        private QuestionCommentsDialoguePopup _questionCommentsDialoguePopup;
        private QuestionAuditDialoguePopup _questionAuditDialoguePopup;
        private QuestionCompareDialoguePopup _questionCompareDialoguePopup;
        private MailMergePopup _mailMergePopup;
        private FinanceAdminPage _financeAdminPage;
        private SchedulesSetupPage _schedulesSetupPage;
        private ScheduleSetupRecordPage _scheduleSetupRecordPage;
        private ChargingRulesSetupPage _chargingRulesSetupPage;
        private ChargingRuleSetupRecordPage _chargingRuleSetupRecordPage;
        private IncomeSupportSetupPage _incomeSupportSetupPage;
        private IncomeSupportSetupRecordPage _incomeSupportSetupRecordPage;
        private NonResidentialPolicyRateSetupPage _nonResidentialPolicyRateSetupPage;
        private NonResidentialPolicyRateSetupRecordPage _nonResidentialPolicyRateSetupRecordPage;
        private ChargesForServicesSetupPage _chargesForServicesSetupPage;
        private ChargeForServicesSetupRecordPage _chargeForServicesSetupRecordPage;
        private FinancialDetailRatesSetupPage _financialDetailRatesSetupPage;
        private FinancialDetailRateSetupRecordPage _financialDetailRateSetupRecordPage;
        private PersonFinancialDetailsSubPage _personFinancialDetailsSubPage;
        private PersonFinancialDetailRecordPage _personFinancialDetailRecordPage;
        private FinancialDetailDisregardsPage _financialDetailDisregardsPage;
        private FinancialDetailDisregardRecordPage _financialDetailDisregardRecordPage;
        private PersonContributionsSubPage _personContributionsSubPage;
        private PersonContributionRecordPage _personContributionRecordPage;
        private PersonContributionExceptionsPage _personContributionExceptionsPage;
        private PersonContributionExceptionRecordPage _personContributionExceptionRecordPage;
        private FinancialDetailRecordPage _financialDetailRecordPage;
        private SystemUsersPage _systemUsersPage;
        private SystemUserRecordPage _systemUserRecordPage;
        private SystemUserTeamMemberSubPage _systemUserTeamMemberSubPage;
        private TeamMemberPage _teamMemberPage;
        private DynamicDialogPopup _dynamicDialogPopup;
        private SpellCheckPopup _spellCheckPopup;
        private ConfirmDynamicDialogPopup _confirmDynamicDialogPopup;
        private CustomizationsPage _customizationsPage;
        private BusinessObjectsPage _businessObjectsPage;
        private BusinessObjectRecordPage _businessObjectRecordPage;
        private BusinessObjectFieldsSubPage _businessObjectFieldsSubPage;
        private BusinessObjectFieldRecordPage _businessObjectFieldRecordPage;
        private BulkEditDialogPopup _bulkEditDialogPopup;
        private PersonCaseNotesPage _personCaseNotesPage;
        private PersonCaseNoteRecordPage _personCaseNoteRecordPage;
        private PersonDocumentViewSubPage _personDocumentViewSubPage;
        private DataManagementPage _dataManagementPage;
        private MergedRecordsPage _mergedRecordsPage;
        private MergedRecordRecordPage _mergedRecordRecordPage;
        private PersonTimelineSubPage _personTimelineSubPage;
        private PersonAllActivitiesSubPage _personAllActivitiesSubPage;
        private PersonCarePlansSubPage _personCarePlansSubPage;
        private CareDiaryPage _CareDiaryPage;
        private CareDiaryRecordPage _CareDiaryRecordPage;
        private PersonCarePlansSubPage_AssessmentsTab _PersonCarePlansSubPage_AssessmentsTab;
        private PersonCarePlansSubPage_CarePlansTab _personCarePlansSubPage_CarePlansTab;
        private PersonCarePlansSubPage_RegularCareTasksTab _personCarePlansSubPage_regularCareTasksTab;
        private PersonChronologiesPage _personChronologiesPage;
        private PersonChronologyRecordPage _personChronologyRecordPage;
        private PersonChronologyRecordPrintPopup _personChronologyRecordPrintPopup;
        private PersonPhysicalObservationCaseNotesPage _personPhysicalObservationCaseNotesPage;
        private PersonFinancialAssessmentCaseNoteRecordPage _personFinacialAssessmentCaseNoteRecordPage;
        private PersonFinancialAssessmentCaseNotesPage _personFinacialAssessmentCaseNotesPage;
        private PersonPhysicalObservationCaseNoteRecordPage _personPhysicalObservationCaseNoteRecordPage;
        private PersonPhysicalObservationsGenerateChartPopup _personPhysicalObservationsGenerateChartPopup;
        private PersonEmailsPage _personEmailsPage;
        private PersonEmailRecordPage _personEmailRecordPage;
        private PersonEmailAttachmentsPage _personEmailAttachmentsPage;
        private PersonEmailAttachmentRecordPage _personEmailAttachmentRecordPage;
        private PersonCPISPage _personCPISPage;
        private PersonCPISRecordPage _personCPISRecordPage;
        private AuditListPage _auditListPage;
        private AuditChangeSetDialogPopup _auditChangeSetDialogPopup;
        private FileDestructionGDPRPage _fileDestructionGDPRPage;
        private FileDestructionGDPRRecordPage _fileDestructionGDPRRecordPage;
        private SystemDashboardsPage _systemDashboardsPage;
        private SystemDashboardRecordPage _systemDashboardRecordPage;
        private SummaryDashboardsPage _summaryDashboardsPage;
        private SummaryDashboardRecordPage _summaryDashboardRecordPage;
        private PersonRecordPage_SummaryArea _personRecordPage_SummaryArea;
        private UserDashboardsPage _userDashboardsPage;
        private UserDashboardRecordPage _userDashboardRecordPage;
        private DashboardsPage _dashboardsPage;
        private FAQsPage _faqsPage;
        private FAQRecordPage _faqRecordPage;
        private FAQApplicationComponentsPage _faqApplicationComponentsPage;
        private FAQApplicationComponentRecordPage _faqApplicationComponentRecordPage;
        private FAQSearchPage _faqSearchPage;
        private WebSitePage _webSitePage;
        private WebSiteRecordPage _webSiteRecordPage;
        private WebSitePagesPage _webSitePagesPage;
        private WebSitePageRecordPage _webSitePageRecordPage;
        private WebSiteSettingsPage _webSiteSettingsPage;
        private WebSiteSettingRecordPage _webSiteSettingRecordPage;
        private ChangePasswordPopup _changePasswordPopup;
        private ChangeEncryptedValuePopup _changeEncryptedValuePopup;
        private WebSiteAnnouncementsPage _webSiteAnnouncementsPage;
        private WebSiteAnnouncementRecordPage _webSiteAnnouncementRecordPage;
        private WebSiteUsersPage _webSiteUsersPage;
        private GenericWebSiteUsersPage _genericWebSiteUsersPage;
        private WebSiteUserRecordPage _webSiteUserRecordPage;
        private WebSiteUserPinsPage _webSiteUserPinsPage;
        private WebSiteUserPinRecordPage _webSiteUserPinRecordPage;
        private WebsiteUserPasswordResetsPage _websiteUserPasswordResetsPage;
        private WebSiteUserPasswordResetRecordPage _webSiteUserPasswordResetRecordPage;
        private WebsiteUserPasswordHistoryPage _websiteUserPasswordHistoryPage;
        private WebSiteUserPasswordHistoryRecordPage _websiteUserPasswordHistoryRecordPage;
        private WebSiteUserAccessAuditPage _webSiteUserAccessAuditPage;
        private WebsiteUserAccessAudiRecordPage _websiteUserAccessAudiRecordPage;
        private WebSiteUserAccessAuditTab _webSiteUserAccessAuditTab;
        private WebSitePageRecordDesignerSubPage _webSitePageRecordDesignerSubPage;
        private WebsitePageWidgetSettingsPopup _websitePageWidgetSettingsPopup;
        private WebSiteSitemapsPage _webSiteSitemapsPage;
        private WebSiteSitemapRecordPage _webSiteSitemapRecordPage;
        private WebSiteSitemapRecordDesignerPage _webSiteSitemapRecordDesignerPage;
        private WebSitePointsOfContactPage _webSitePointsOfContactPage;
        private WebSitePointOfContactRecordPage _webSitePointOfContactRecordPage;
        private WebSiteFeedbackPage _webSiteFeedbackPage;
        private WebSiteFeedbackRecordPage _webSiteFeedbackRecordPage;
        private WebSiteContactsPage _webSiteContactsPage;
        private WebSiteContactRecordPage _webSiteContactRecordPage;
        private PersonFormsPage _personFormsPage;
        private PersonFormRecordPage _personFormRecordPage;
        private PersonFormInvolvementRecordPage _personFormInvolvementRecordPage;
        private UserChartsPage _userChartsPage;
        private UserChartPopup _userChartPopup;
        private QueryResultsPage _queryResultsPage;
        private WebsitePortalTasksPage _websitePortalTasksPage;
        private WebsitePortalTaskRecordPage _websitePortalTaskRecordPage;
        private PersonPortalTasksPage _PersonPortalTasksPage;
        private PersonPortalTaskRecordPage _PersonPortalTaskRecordPage;
        private ReferenceDataPage _referenceDataPage;
        private AdvanceSearchPage _advanceSearchPage;
        private PersonFormCaseNotesPage _personFormCaseNotesPage;
        private PersonFormCaseNoteRecordPage _personFormCaseNoteRecordPage;
        private PersonFormAppointmentsPage _personFormAppointmentsPage;
        private PersonFormAppointmentRecordPage _personFormAppointmentRecordPage;
        private PersonFormEmailsPage _personFormEmailsPage;
        private PersonFormEmailRecordPage _personFormEmailRecordPage;
        private PersonFormLettersPage _personFormLettersPage;
        private PersonFormLetterRecordPage _personFormLetterRecordPage;
        private PersonFormInvolvementRecordSubPage _personFormInvolvementRecordSubPage;
        private PersonPhoneCallsPage _personPhoneCallsPage;
        private PersonFormPhoneCallsPage _personFormPhoneCallsPage;
        private PersonPhoneCallRecordPage _personPhoneCallRecordPage;
        private PersonFormPhoneCallRecordPage _personFormPhoneCallRecordPage;
        private PersonFormTasksPage _personFormTasksPage;
        private PersonFormTaskRecordPage _personFormTaskRecordPage;
        private CaseFormCaseNotesPage _caseFormCaseNotesPage;
        private CaseFormCaseNoteRecordPage _caseFormCaseNoteRecordPage;
        private CloneActivityPopup _cloneActivityPopup;
        private TeamSelectingPage _teamSelectingPage;
        private ChangeDefaultTeamPopup _changeDefaultTeamPopup;
        private CopyToClipboardDynamicDialogPopup _copyToClipboardDynamicDialogPopup;
        private SecurityProfilesPage _securityProfilesPage;
        private SecurityProfileRecordPage _securityProfileRecordPage;
        private CaseFormAppointmentsPage _caseFormAppointmentsPage;
        private CaseFormAppointmentRecordPage _caseFormAppointmentRecordPage;
        private CaseFormEmailsPage _caseFormEmailsPage;
        private CaseFormEmailRecordPage _caseFormEmailRecordPage;
        private CaseFormLettersPage _caseFormLettersPage;
        private CaseFormLetterRecordPage _caseFormLetterRecordPage;
        private CaseFormPhoneCallsPage _caseFormPhoneCallsPage;
        private CaseFormPhoneCallRecordPage _caseFormPhoneCallRecordPage;
        private CaseFormTasksPage _caseFormTasksPage;
        private CaseFormTaskRecordPage _caseFormTaskRecordPage;
        private SystemManagementPage _systemManagementPage;
        private EDMSRepositoriesPage _edmsRepositoriesPage;
        private ScheduleJobsPage _scheduleJobsPage;
        private ScheduleJobsRecordPage _scheduleJobsRecordPage;
        private EDMSRepositoryRecordPage _edmsRepositoryRecordPage;
        private PersonAttachmentsPage _personAttachmentsPage;
        private PersonAttachmentRecordPage _personAttachmentRecordPage;
        private CreateBulkAttachmentsPopup _createBulkAttachmentsPopup;
        private DocumentPrintTemplatesPage _documentPrintTemplatesPage;
        private DocumentPrintTemplateRecordPage _documentPrintTemplateRecordPage;
        private WorkflowsPage _workflowsPage;
        private WorkflowRecordPage _workflowRecordPage;
        private DuplicateRecordsPage _duplicateRecordsPage;
        private DuplicateRecordRecordPage _duplicateRecordRecordPage;
        private DuplicateRecordMergeDialog _duplicateRecordMergeDialog;
        private WorkflowConditionsPopup _workflowConditionsPopup;
        private WorkflowActionPopup _workflowActionPopup;
        private ProviderRecordPage _providerRecordPage;
        private ProvidersPage _providersPage;
        private ProviderWebsiteMessageRecordPage _providerWebsiteMessageRecordPage;
        private ProviderWebsiteMessagesPage _providerWebsiteMessagesPage;
        private WorkflowJobsPage _workflowJobsPage;
        private WorkflowJobRecordPage _workflowJobRecordPage;
        private WidgetSettingsPopup _widgetSettingsPopup;
        private CaseFormActionsOutcomesPageFrame _caseFormActionsOutcomesPageFrame;
        private FormActionOutcomePage _formActionOutcomePage;
        private CloneWebsitePopup _cloneWebsitePopup;
        private PersonFormActionOutcomePage _personFormActionOutcomePage;
        private PersonFormActionsOutcomesPageFrame _personFormActionsOutcomesPageFrame;
        private SDEMapListPopup _sdeMapListPopup;
        private AddressPropertyTypesPage _addressPropertyTypesPage;
        private ReportableEventInjuritySeveritiesPage _reportableEventInjuritySeveritiesPage;
        private ReportableEventRolesPage _reportableEventRolesPage;
        private ReportableEventSubCategoriesPage _reportableEventSubCategoriesPage;
        private ReportableEventInjuritySeveritiesRecordPage _reportableEventInjuritySeveritiesRecordPage;
        private ReportableEventRolesRecordPage _reportableEventRolesRecordPage;
        private ReportableEventSubCategoriesRecordPage _reportableEventSubCategoriesRecordPage;
        private WorkflowSendEmailPropertiesPage _workflowSendEmailPropertiesPage;
        private CaseFormAttachmentsPage _caseFormAttachmentsPage;
        private CaseFormAttachmentRecordPage _caseFormAttachmentRecordPage;
        private PotentialDuplicatesPopup _potentialDuplicatesPopup;
        private PersonSearchPage _personSearchPage;
        private AutomationPersonForm1EditAssessmentPage _automationPersonForm1EditAssessmentPage;
        private ProviderFormsPage _providerFormsPage;
        private ProviderFormRecordPage _providerFormRecordPage;
        private AutomationProviderForm1EditAssessmentPage _automationProviderForm1EditAssessmentPage;
        private PersonMASHEpisodesPage _personMASHEpisodesPage;
        private PersonMASHEpisodeRecordPage _personMASHEpisodeRecordPage;
        private PersonMASHEpisodeFormsPage _personMASHEpisodeFormsPage;
        private PersonMASHEpisodeFormRecordPage _personMASHEpisodeFormRecordPage;
        private AutomationMASHEpisodeForm1EditAssessmentPage _automationMASHEpisodeForm1EditAssessmentPage;
        private PersonLettersPage _personLettersPage;
        private PersonLetterRecordPage _personLetterRecordPage;
        private PersonTasksPage _personTasksPage;
        private PersonTaskRecordPage _personTaskRecordPage;
        private PersonClinicalRiskStatusesPage _personClinicalRiskStatusesPage;
        private PersonClinicalRiskStatusRecordPage _personClinicalRiskStatusRecordPage;
        private PersonClinicalRiskStatusCaseNotesPage _personClinicalRiskStatusCaseNotesPage;
        private PersonClinicalRiskStatusCaseNoteRecordPage _personClinicalRiskStatusCaseNoteRecordPage;
        private PersonCarePlanRecordPage _personCarePlanRecordPage;
        private PersonCarePlanNeedRecordPage _personCarePlanNeedRecordPage;
        private PersonCarePlanFormInitialAssessmentRecordPage _personCarePlanFormInitialAssessmentRecordPage;
        private PersonCarePlanFormRecordPage _personCarePlanFormRecordPage;
        private PersonCarePlanNeedGoalsROutcomePage _personCarePlanNeedGoalsROutcomePage;
        private PersonCarePlanNeedGoalsROutcomeRecordPage _personCarePlanNeedGoalsROutcomeRecordPage;
        private PersonCarePlanInterventionRecordPage _personCarePlanInterventionsRecordPage;
        private PersonCarePlanInterventionRecordAddPage _personCarePlanInterventionsRecordAddPage;
        private PersonCarePlanCaseNotesPage _personCarePlanCaseNotesPage;
        private PersonCarePlanCaseNoteRecordPage _personCarePlanCaseNoteRecordPage;
        private CaseHealthAppointmentsPage _caseHealthAppointmentsPage;
        private CaseHealthAppointmentRecordPage _caseHealthAppointmentRecordPage;
        private HealthAppointmentCaseNotesPage _healthAppointmentCaseNotesPage;
        private HealthAppointmentCaseNoteRecordPage _healthAppointmentCaseNoteRecordPage;
        private PersonHeightAndWeightCaseNoteRecordPage _personHeightAndWeightCaseNoteRecordPage;
        private PersonHeightAndWeightCaseNotesPage _personHeightAndWeightCaseNotesPage;
        private PersonHeightWeighObservationRecordPage _personHeightWeighObservationRecordPage;
        private PersonHeightWeightObservationsPage _personHeightWeightObservationsPage;
        private PersonMHALegalStatusesPage _personMHALegalStatusesPage;
        private PersonMHALegalStatusRecordPage _personMHALegalStatusRecordPage;
        private RightsAndRequestsForAnIMHAAndMHAAppealPage _rightsAndRequestsForAnIMHAAndMHAAppealPage;
        private RightsAndRequestForAnIMHAAndMHAAppealRecordPage _rightsAndRequestForAnIMHAAndMHAAppealRecordPage;
        private RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage _rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage;
        private RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage _rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage;
        private RecordsOfAppealPage _recordsOfAppealPage;
        private RecordOfAppealRecordPage _recordOfAppealRecordPage;
        private RecordOfAppealCaseNotesPage _recordOfAppealCaseNotesPage;
        private RecordOfAppealCaseNoteRecordPage _recordOfAppealCaseNoteRecordPage;
        private PersonMHALegalStatusCaseNotesPage _personMHALegalStatusCaseNotesPage;
        private PersonPhysicalObservationsPage _personPhysicalObservationsPage;
        private PersonPhysicalObservationsRecordPage _personPhysicalObservationsRecordPage;
        private PersonMHALegalStatusCaseNoteRecordPage _personMHALegalStatusCaseNoteRecordPage;
        private CaseCaseNotesPage _caseCaseNotesPage;
        private CaseCaseNoteRecordPage _caseCaseNoteRecordPage;
        private CourtDatesAndOutcomesPage _courtDatesAndOutcomesPage;
        private CourtDatesAndOutcomeRecordPage _courtDatesAndOutcomeRecordPage;
        private CourtDatesAndOutcomesCaseNotesPage _courtDatesAndOutcomesCaseNotesPage;
        private CourtDatesAndOutcomesCaseNoteRecordPage _courtDatesAndOutcomesCaseNoteRecordPage;
        private Section117EntitlementsPage _Section117EntitlementsPage;
        private Section117EntitlementRecordPage _Section117EntitlementRecordPage;
        private Section117EntitlementCaseNotesPage _Section117EntitlementCaseNotesPage;
        private Sectio117EntitlementCaseNoteRecordPage _Sectio117EntitlementCaseNoteRecordPage;
        private LeavesAWOLPage _leavesAWOLPage;
        private LeaveAWOLRecordPage _leaveAWOLRecordPage;
        private InpatientLeaveAwolCaseNotesPage _inpatientLeaveAwolCaseNotesPage;
        private InpatientLeaveAwolCaseNoteRecordPage _inpatientLeaveAwolCaseNoteRecordPage;
        private PersonContactsPage _personContactsPage;
        private PersonContactRecordPage _personContactRecordPage;
        private PersonContactCaseNotesPage _personContactCaseNotesPage;
        private PersonContactCaseNoteRecordPage _personContactCaseNoteRecordPage;
        private CaseTasksPage _caseTasksPage;
        private CaseTaskRecordPage _caseTaskRecordPage;
        private CloneAttachmentsPopup _cloneAttachmentsPopup;
        private DrawingCanvasPopup _drawingCanvasPopup;
        private PersonAppointmentsPage _personAppointmentsPage;
        private PersonAppointmentRecordPage _personAppointmentRecordPage;
        private ExportDataPopup _exportDataPopup;
        private PersonPrimarySupportReasonPage _personPrimarySupportReasonPage;
        private PersonPrimarySupportReasonRecordPage _personPrimarySupportReasonRecordPage;
        private AssignRecordPopup _assignRecordPopup;
        private PersonAlertandHazardsPage _personAlertAndHazardsPage;
        private PersonAlertandHazardsRecordPage _personAlertAndHazardsRecordPage;
        private RestrictPersonAlertAndHazardPopup _restrictPersonAlertAndHazardPopup;
        private PersonAlertAndHazardReviewRecordPage _personAlertAndHazardReviewRecordPage;
        private ModulesObjectPage _modulesObjectPage;
        private ModulesObjectRecordPage _modulesObjectRecordPage;
        private PersonSignificantEvent _personSignificantEvent;
        private PersonPostAdoptionLinksPage _personPostAdoptionLinksPage;
        private PersonPostAdoptionLinkRecordPage _personPostAdoptionLinkRecordPage;
        private PersonPreAdoptionLinksPage _personPreAdoptionLinksPage;
        private PersonPreAdoptionLinkRecordPage _personPreAdoptionLinkRecordPage;
        private PersonAlertAndHazardReviewPage _personAlertAndHazardReviewPage;
        private PersonClinicalRiskFactorsPage _personClinicalRiskFactorsPage;
        private PersonClinicalRiskFactorRecordPage _personClinicalRiskFactorRecordPage;
        private PersonClinicalRiskFactorHistoryPage _personClinicalRiskFactorHistoryPage;
        private PersonClinicalRiskFactorHistoryRecordPage _personClinicalRiskFactorHistoryRecordPage;
        private PersonHealthProfessionalsRecordPage _personHealthProfessionalsRecordPage;
        private PersonHealthProfessionalsPage _personHealthProfessionalsPage;
        private PersonHealthDetailsPage _personHealthDetailPage;
        private PersonHealthDetailRecordPage _personHealthDetailsRecordPage;
        private HealthIssueTypesPage _healthIssueTypesPage;
        private HealthIssueTypesRecordPage _healthIssueTypesRecordPage;
        private PersonGestationPeriodPage _personGestationPeriodPage;
        private PersonGestationPeriodRecordPage _personGestationPeriodRecordPage;
        private PersonHealthDisabilityImpairmentsPage _personHealthDisabilityImpairmentsPage;
        private PersonHealthDisabilityImpairmentsRecordPage _personHealthDisabilityImpairmentsRecordPage;
        private PersonRecurringAppointmentsPage _personRecurringAppointmentsPage;
        private PersonRecurringAppointmentRecordPage _personRecurringAppointmentRecordPage;
        private PersonRecordsOfDNARPage _personRecordsOfDNARPage;
        private PersonRecordsOfDNARRecordPage _personRecordsOfDNARRecordPage;
        private PersonRelationshipRecordPage _personRelationshipRecordPage;
        private AllegedAbuserPage _allegedAbuserPage;
        private AllegedAbsuserRecordPage _allegedAbsuserRecordPage;
        private AllegedVictimPage _allegedVictimPage;
        private AllegedVictimRecordPage _allegedVictimRecordPage;
        private AllegationInvestigatorPage _allegationInvestigatorPage;
        private AllegationInvestigatorRecordPage _allegationInvestigatorRecordPage;
        private PersonHealthImmunisationPage _personHealthImmunisationPage;
        private PersonHealthImmunisationRecordPage _personHealthImmunisationRecordPage;

        private HospitalWardsPage _hospitalWardsPage;
        private HospitalWardsRecordPage _hospitalWardsRecordPage;
        private ProvidersRecordPage _providersRecordPage;
        private BayOrRoomspage _bayOrRoomspage;
        private BayOrRoomsRecordpage _bayOrRoomsRecordpage;
        private BedPage _bedPage;
        private BedRecordPage _bedRecordPage;
        private PersonCasesPage _personCasesPage;
        private PersonCasesRecordPage _personCasesRecordPage;
        private CorrectErrorsPopUp _correctErrorsPopUp;
        private PersonRecordNewPage _personRecordNewPage;
        private AddressActionPopUp _addressActionPopUp;
        private PersonAllergiesPage _personAllergiesPage;
        private PersonAllergyRecordPage _personAllergyRecordPage;
        private SnomedLookupPopup _snomedLookupPopup;
        private PersonAllergyReactionsInnerGridArea _personAllergyReactionsInnerGridArea;
        private PersonAllergicReactionRecordPage _personAllergicReactionRecordPage;
        private CaseBrokerageEpisodesPage _caseBrokerageEpisodesPage;
        private CaseBrokerageEpisodeRecordPage _caseBrokerageEpisodeRecordPage;
        private CaseBrokerageEpisodeOffersPage _caseBrokerageEpisodeOffersPage;
        private CaseBrokerageEpisodeOfferRecordPage _caseBrokerageEpisodeOfferRecordPage;
        private CaseBrokerageOfferCommunicationsSubArea _caseBrokerageOfferCommunicationsSubArea;
        private CaseBrokerageOfferCommunicationRecordPage _caseBrokerageOfferCommunicationRecordPage;
        private BrokerageEpisodeEscalationsPage _brokerageEpisodeEscalationsPage;
        private BrokerageEpisodeEscalationRecordPage _brokerageEpisodeEscalationRecordPage;
        private BrokerageEpisodeEscalationsSubArea _brokerageEpisodeEscalationsSubArea;
        private SelectCaseTypePopUp _selectCaseTypePopUp;
        private CaseFormAssessmentFactorPage _caseFormAssessmentFactorPage;
        private CaseFormAssessmentFactorRecordPage _caseFormAssessmentFactorRecordPage;
        private CaseFormMembersPage _caseFormMembersPage;
        private CaseFormMembersRecordPage _caseFormMembersRecordPage;
        private CaseBrokerageEpisodePausePeriodPage _caseBrokerageEpisodePausePeriodPage;
        private CaseBrokerageEpisodePausePeriodsRecordPage _caseBrokerageEpisodePausePeriodsRecordPage;
        private CaseBrokerageEpisodeAttachmentsPage _caseBrokerageEpisodeAttachmentsPage;
        private CaseBrokerageEpisodeAttachmentRecordPage _caseBrokerageEpisodeAttachmentRecordPage;
        private PersonCarePlansSubPage_RegularCareTasksTab_Record _personCarePlansSubPage_RegularCareTasksTab_Record;
        private PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage _personCarePlansSubPage_RegularCareTask_CareScheludesRecordPage;
        private CaseBrokerageOfferAttachmentsPage _caseBrokerageOfferAttachmentsPage;
        private CaseBrokerageOfferAttachmentRecordPage _caseBrokerageOfferAttachmentRecordPage;
        private CaseBrokerageEpisodeServiceProvisionsPage _caseBrokerageEpisodeServiceProvisionsPage;
        private ServiceProvisionStartReasonPage _serviceProvisionStartReasonPage;
        private ServiceProvisionStartReasonRecordPage _serviceProvisionStartReasonRecordPage;
        private LookupFormPage _lookupFormPage;
        private ServiceElement1RecordPage _serviceElement1RecordPage;
        private BrokerageOfferCancellationReasonsPage _brokerageOfferCancellationReasonsPage;
        private BrokerageOfferCancellationReasonsRecordPage _brokerageOfferCancellationReasonsRecordPage;
        private OptionSetFormPopUp _optionSetFormPopUp;
        private SystemUserAddressPage _systemUserAddressPage;
        private SystemUserAddressRecordPage _systemUserAddressRecordPage;
        private SystemUserLanguagesPage _systemUserLanguagesPage;
        private SystemUserLanguageRecordPage _systemUserLanguageRecordPage;
        private PersonAddressesPage _personAddressesPage;
        private CacheMonitorPage _cacheMonitorPage;
        private SmokeTest_CaseFormEditAssessmentPage _smokeTest_CaseFormEditAssessmentPage;
        private SystemUserAliasesPage _systemUserAliasesPage;
        private SystemUserAliasesRecordPage _systemUserAliasesRecordPage;
        private SystemUserEmergencyContactsPage _systemUserEmergencyContactsPage;
        private SystemUserEmergencyContactsRecordPage _systemUserEmergencyContactsRecordPage;
        private SystemUserStaffReviewPage _systemUserStaffReviewPage;
        private StaffReviewStaffSupervisionFormRecordPage _StaffReviewStaffSupervisionFormRecordPage;
        private StaffReviewStaffSpotFormRecordPage _StaffReviewStaffSpotFormRecordPage;
        private StaffReviewRecordPage _staffReviewRecordPage;
        private StaffReviewAttachmentsPage _staffReviewAttachmentsPage;
        private StaffReviewAttchmentsRecordPage _staffReviewAttchmentsRecordPage;
        private SystemUserSuspensionsPage _systemUserSuspensionsPage;
        private SystemUserSuspensionsRecordPage _systemUserSuspensionsRecordPage;
        private MyStaffReviewPage _staffReviewPage;
        private SystemUserUserWorkScheduleRecordPage _SystemUserUserWorkScheduleRecordPage;
        private SystemUserEmploymentContractsPage _systemUserEmploymentContractsPage;
        private SystemUserEmploymentContractsRecordPage _systemUserEmploymentContractsRecordPage;
        private SystemSettingsPage _systemSettingsPage;
        private SystemSettingRecordPage _systemSettingRecordPage;
        private PersonHealthDiagnosesPage _personHealthDiagnosesPage;
        private PersonHealthDiagnosisRecordPage _personHealthDiagnosisRecordPage;
        private ConsultantEpisodesPage _consultantEpisodesPage;
        private ConsultantEpisodesRecordPage _consultantEpisodesRecordPage;
        private InpatientLeaveAwolAttachmentsPage _inpatientLeaveAwolAttachmentsPage;
        private InpatientLeaveAwolAttachmentsRecordPage _inpatientLeaveAwolAttachmentsRecordPage;
        private SeclusionsPage _seclusionsPage;
        private SeclusionsRecordPage _seclusionsRecordPage;
        private SeclusionReviewsPage _seclusionReviewsPage;
        private SeclusionReviewsRecordPage _seclusionReviewsRecordPage;
        private InpatientSeclusionAttachmentPage _inpatientSeclusionAttachmentPage;
        private InpatientSeclusionAttachmentRecordPage _inpatientSeclusionAttachmentRecordPage;

        private OrganisationalRiskManagementPage _organisationalRiskManagementPage;
        private OrganisationalRiskManagementRecordPage _organisationalRiskManagementRecordPage;

        private StaffReviewRequirementsPage _staffReviewRequirementsPage;
        private StaffReviewRequirementsRecordPage _staffReviewRequirementsRecordPage;


        private ActionPlansPage _ActionPlansPage;
        private ActionPlansRecordPage _ActionPlansRecordPage;
        private StaffReviewFormsPage _staffReviewFormsPage;
        private StaffReviewFormsRecordPage _staffReviewFormsRecordPage;
        private StaffReviewFormQuestionsRecordPage _staffReviewFormQuestionsRecordPage;
        private StaffEvaluationFormQuestionsRecordPage _staffEvaluationFormQuestionsRecordPage;
        private PersonBodyMapsPage _personBodyMapsPage;
        private PersonBodyMapRecordPage _personBodyMapRecordPage;
        private PersonMobilityPage _personMobilityPage;
        private PersonMobilityRecordPage _personMobilityRecordPage;
        private LookUpRecordsPopUp _lookUpRecordsPopUp;
        private BookingTypesPopUp _bookingTypesPopUp;
        private OpenEndedAbsencesPopUp _openEndedAbsencesPopUp;
        private OrganisationalRiskCategoryPage _organisationalRiskCategoryPage;
        private OrganisationalRiskCategoryRecordPage _organisationalRiskCategoryRecordPage;
        private PersonRecordPage_AboutMeArea _personRecordPage_AboutMeArea;
        private Person_AboutMePage _personAboutMePage;
        private Person_AboutMeRecordPage _personAboutMeRecordPage;
        private AboutMeSetupPage _aboutMeSetupPage;
        private AboutMeSetupRecordPage _aboutMeSetupRecordPage;
        private CarePlanTypesPage _carePlanTypesPage;
        private CarePlanTypesRecordPage _carePlanTypesRecordPage;
        private ReportableEventPage _reportableEventPage;
        private ReportableEventBehaviourPage _reportableEventBehaviourPage;
        private ReportableEventRecordPage _reportableEventRecordPage;
        private ReportableEventBehaviourRecordPages _reportableEventBehaviourRecordPages;
        private ReportableEventImpactsPage _reportableEventImpactsPage;
        private ReportableEventImpactRecordPages _reportableEventImpactRecordPages;
        private ReportableEventImpactPersonBodymapRecordPages _reportableEventImpactPersonBodymapRecordPages;
        private ReportableEventAttachmentsPage _reportableEventAttachmentsPage;
        private ReportableEventAttchmentsRecordPage _reportableEventAttchmentsRecordPage;
        private SelectHealthAppointmentTypePopUp _selectHealthAppointmentTypePopUp;
        private ReportableEventActionsPage _reportableEventActionsPage;
        private ReportableEventActionsRecordPage _reportableEventActionsRecordPage;
        private ApplicantPage _applicantPage;
        private ApplicantRecordPage _applicantRecordPage;
        private ApplicantSheduleAvailability _applicantSheduleAvailability;
        private ApplicantViewDiaryOrManageAdhoc _applicantViewDiaryOrManageAdhoc;
        private CommunityClinicAdditionalHealthProfessionalRecordPage _communityClinicAdditionalHealthProfessionalRecordPage;
        private HealthDiaryViewPage _healthDiaryViewPage;
        private SystemUser_DiaryPage _systemUser_DiaryPage;
        private SystemUserAvailabilitySubPage _systemUserAvailabilitySubPage;
        private CreateScheduledAvailabilityPopup _createScheduledAvailabilityPopup;
        private CreateScheduleBookingPopup _createScheduleBookingPopup;
        private SelectMultiplePeoplePopUp _selectMultiplePeoplePopUp;
        private SelectStaffPopup _selectStaffPopup;
        private ServiceProvisionsPage _serviceProvisionsPage;
        private ServiceElement1Page _serviceElement1Page;
        private ServiceGLCodingsPage _serviceGLCodingsPage;
        private ServiceGLCodingsRecordPage _serviceGLCodingsRecordPage;
        private ServicesProvidedPage _servicesProvidedPage;
        private ServiceProvidedRecordPage _serviceProvidedRecordPage;
        private SystemUserViewDiaryManageAdHocPage _systemUserViewDiaryManageAdHocPage;
        private SystemUserAvailabilityScheduleTransportPage _systemUserAvailabilityScheduleTransportPage;
        private UserWorkSchedulesPage _userWorkSchedulesPage;
        private UserTransportationSchedule _userTransportationSchedulePage;
        private TeamsPage _teamsPage;
        private CoreUsersPage _coreUsersPage;
        private ProviderUsersPage _providerUsersPage;
        private RosteredUsersPage _rosteredUsersPage;
        private RoleApplicationRecordPage _roleApplicationRecordPage;
        private RoleApplicationsPage _roleApplicationsPage;
        private RecruitmentDocumentsPage _recruitmentDocumentsPage;
        private RecruitmentDocumentsRecordPage _recruitmentDocumentsRecordPage;
        private ApplicantLanguageRecordPage _applicantLanguageRecordPage;
        private ApplicantAliasRecordPage _applicantAliasRecordPage;
        private RecruitmentDocumentAttachmentPage _recruitmentDocumentAttachmentPage;
        private RecruitmentDocumentAttachmentRecordPage _recruitmentDocumentAttachmentRecordPage;
        private RecruitmentDocumentManagementRecordPage _recruitmentDocumentManagementRecordPage;
        private OptionSetsPage _optionSetsPage;
        private OptionSetsRecordPage _optionSetsRecordPage;
        private OptionsetValuesPage _optionsetValuesPage;
        private OptionsetValueRecordPage _optionsetValueRecordPage;
        private TrainingCourseSetupPage _trainingCourseSetupPage;
        private TrainingCourseRecordPage _trainingCourseRecordPage;
        private ServiceElement2Page _serviceElement2Page;
        private ServiceElement2RecordPage _serviceElement2RecordPage;
        private ServicePermissionsPage _servicePermissionsPage;
        private ServicePermissionRecordPage _servicePermissionRecordPage;
        private ApprovedCareTypesPage _approvedCareTypesPage;
        private ApprovedCareTypeRecordPage _approvedCareTypeRecordPage;
        private GLCodeMappingsPage _glCodeMappingsPage;
        private GLCodeMappingsRecordPage _glCodeMappingsRecordPage;
        private AvailabilityTypesPage _availabilityTypesPage;
        private AvailabilityTypeRecordPage _availabilityTypeRecordPage;
        private LocalizedStringsPage _localizedStringsPage;
        private LocalizedStringsRecordPage _localizedStringsRecordPage;
        private LocalizedStringValuesPage _localizedStringValuesPage;
        private LocalizedStringValuesRecordPage _localizedStringValuesRecordPage;
        private TrainingRequirementSetupPage _trainingRequirementSetupPage;
        private ServiceProvidedRatePeriodsPage _serviceProvidedRatePeriodsPage;
        private ServiceProvidedRatePeriodRecordPage _serviceProvidedRatePeriodRecordPage;
        private ServiceProvidedRateSchedulesPage _serviceProvidedRateSchedulesPage;
        private ServiceProvidedRateScheduleRecordPage _serviceProvidedRateScheduleRecordPage;
        private ServiceFinanceSettingsPage _serviceFinanceSettingsPage;
        private ServiceFinanceSettingRecordPage _serviceFinanceSettingRecordPage;
        private ServiceProvisionRatePeriodsPage _serviceProvisionRatePeriodsPage;
        private ServiceProvisionRatePeriodRecordPage _serviceProvisionRatePeriodRecordPage;
        private ServiceProvisionRateSchedulePage _serviceProvisionRateSchedulePage;
        private ServiceProvisionRateScheduleRecordPage _serviceProvisionRateScheduleRecordPage;
        private RecurringHealthAppointmentsPage _recurringHealthAppointmentsPage;
        private RecurringHealthAppointmentRecordPage _recurringHealthAppointmentRecordPage;
        private ProviderCarerSearchPopup _providerCarerSearchPopup;
        private CalendarDatePicker _calendarDatePicker;
        private FinanceInvoiceBatchSetupPage _financeInvoiceBatchSetupPage;
        private FinanceInvoiceBatchSetupRecordPage _financeInvoiceBatchSetupRecordPage;
        private FinanceInvoiceBatchesPage _financeInvoiceBatchesPage;
        private ServiceUpratesPage _serviceUpratesPage;
        private ServiceUprateRecordPage _serviceUprateRecordPage;
        private FinanceInvoiceBatchRecordPage _financeInvoiceBatchRecordPage;
        private FinanceTransactionsPage _financeTransactionsPage;
        private FinanceInvoicesPage _financeInvoicesPage;
        private FinanceInvoiceRecordPage _financeInvoiceRecordPage;
        private AdditionalTransactionsFinanceTransactionsPage _additionalTransactionsFinanceTransactionsPage;
        private FinanceTransactionRecordPage _financeTransactionRecordPage;
        private FinanceTransactionTriggersPage _financeTransactionTriggersPage;
        private ContactReasonsPage _contactReasonsPage;
        private ContactReasonRecordPage _contactReasonRecordPage;
        private FinanceExtractsPage _financeExtractsPage;
        private FinanceExtractRecordPage _financeExtractRecordPage;
        private TotalsPage _totalsPage;
        private RTTPathwaysSetupPage _rttPathwaysSetupPage;
        private RTTPathwaysSetupRecordPage _rttPathwaysSetupRecordPage;
        private RTTTreatmentFunctionsPage _rttTreatmentFunctionsPage;
        private RTTTreatmentFunctionRecordPage _rttTreatmentFunctionRecordPage;
        private CareProviderStaffRoleTypesPage _careProviderStaffRoleTypesPage;
        private CareProviderStaffRoleTypeRecordPage _careProviderStaffRoleTypeRecordPage;
        private CareProviderStaffRoleTypeGroupsPage _careProviderStaffRoleTypeGroupsPage;
        private CareProviderStaffRoleTypeGroupRecordPage _careProviderStaffRoleTypeGroupRecordPage;
        private RateUnitsPage _rateUnitsPage;
        private RateUnitRecordPage _rateUnitRecordPage;
        private BankHolidayChargingCalendarsPage _bankHolidayChargingCalendarsPage;
        private BankHolidayChargingCalendarRecordPage _bankHolidayChargingCalendarRecordPage;
        private BankHolidayDatesPage _bankHolidayDatesPage;
        private BankHolidayDateRecordPage _bankHolidayDateRecordPage;
        private NoBookingOpenEndedAbsencesPopUp _noBookingOpenEndedAbsencesPopUp;
        private ReviewExistingBookingsPopUp _reviewExistingBookingsPopUp;
        private PronounsPage _pronounsPage;
        private PronounsRecordPage _pronounsRecordPage;
        private DataFormsPage _dataFormsPage;
        private DataFormRecordPage _dataFormRecordPage;
        private DataFormEditPage _dataFormEditPage;
        private DataFormFieldPage _dataFormFieldPage;
        private AllRoleApplicationsPage _allRoleApplicationsPage;
        private ContractSchemesPage _contractSchemesPage;
        private ContractSchemeRecordPage _contractSchemeRecordPage;
        private CPFinanceAdminPage _cpFinanceAdminPage;
        private ServicesPage _servicesPage;
        private ServiceRecordPage _serviceRecordPage;
        private CPServiceMappingsPage _cpServiceMappingsPage;
        private FinanceCodeLocationsPage _financeCodeLocationsPage;
        private FinanceCodeMappingsPage _financeCodeMappingsPage;
        private FinanceCodeMappingRecordPage _financeCodeMappingRecordPage;
        private ServiceDetailsPage _serviceDetailsPage;
        private ServiceDetailsRecordPage _serviceDetailsRecordPage;
        private CPServiceMappingRecordPage _cpServiceMappingRecordPage;
        private PersonContractServicesPage _personContractServicesPage;
        private PersonContractServiceRecordPage _personContractServiceRecordPage;
        private CPFinanceExtractBatchSetupsPage _cpFinanceExtractBatchSetupsPage;
        private CPFinanceExtractBatchSetupRecordPage _cpFinanceExtractBatchSetupRecordPage;
        private CPFinanceExtractBatchesPage _cpFinanceExtractBatchesPage;
        private CPFinanceExtractBatchRecordPage _cpFinanceExtractBatchRecordPage;
        private CalendarTimePicker _calendarTimePicker;
        private PersonContractServiceRatePeriodsPage _personContractServiceRatePeriodsPage;
        private PersonContractServiceRatePeriodRecordPage _personContractServiceRatePeriodRecordPage;
        private CPFinanceInvoicesPage _cpFinanceInvoicesPage;
        private CPFinanceTransactionsPage _cpFinanceTransactionsPage;
        private CPFinanceInvoiceBatchesPage _cpFinanceInvoiceBatchesPage;
        private CPFinanceInvoiceBatchRecordPage _cpFinanceInvoiceBatchRecordPage;
        private CreateInvoiceFilePopup _createInvoiceFilePopup;
        private PersonContractServiceChargesPerWeekPage _personContractServiceChargesPerWeekPage;
        private PersonContractServiceChargesPerWeekRecordPage _personContractServiceChargesPerWeekRecordPage;
        private CareProviderSchedulingSetupPage _careProviderSchedulingSetupPage;
        private CareProviderSchedulingSetupRecordPage _careProviderSchedulingSetupRecordPage;
        private CareProviderBookingScheduleRecordPage _careProviderBookingScheduleRecordPage;
        private BookingScheduleStaffRecordPage _bookingScheduleStaffRecordPage;
        private BookingScheduleDeletionReasonsPage _bookingScheduleDeletionReasonsPage;
        private BookingScheduleDeletionReasonsRecordPage _bookingScheduleDeletionReasonsRecordPage;
        private PublicHolidaysPage _publicHolidaysPage;
        private PublicHolidayRecordPage _publicHolidayRecordPage;
        private ExpressBookingResultsPage _expressBookingResultsPage;
        private ExpressBookingResultRecordPage _expressBookingResultRecordPage;
        private BookingTypesPage _bookingTypesPage;
        private BookingTypeRecordPage _bookingTypeRecordPage;
        private BookingTypeClashActionsPage _bookingTypeClashActionsPage;
        private BookingTypeClashActionRecordPage _bookingTypeClashActionRecordPage;
        private BookingDiaryStaffRecordPage _bookingDiaryStaffRecordPage;
        private DrawerDialogPopup _drawerDialogPopup;
        private BookingDiaryDeletionReasonsPage _bookingDiaryDeletionReasonsPage;
        private BookingDiaryDeletionReasonsRecordPage _bookingDiaryDeletionReasonsRecordPage;
        private EmployeeSchedulePage _employeeSchedulePage;
        private PeopleSchedulePage _peopleSchedulePage;
        private ChargeApportionmentsPage _chargeApportionmentsPage;
        private ChargeApportionmentRecordPage _chargeApportionmentRecordPage;
        private CPFinanceInvoiceBatchSetupsPage _cpFinanceInvoiceBatchSetupsPage;
        private CPFinanceInvoiceBatchSetupRecordPage _cpFinanceInvoiceBatchSetupRecordPage;
        private ChargeApportionmentDetailsPage _chargeApportionmentDetailsPage;
        private ChargeApportionmentDetailRecordPage _chargeApportionmentDetailRecordPage;
        private CPFinanceTransactionRecordPage _cpFinanceTransactionRecordPage;
        private CPFinanceTransactionTriggersPage _cpFinanceTransactionTriggersPage;
        private CPFinanceTransactionTriggerRecordPage _cpFinanceTransactionTriggerRecordPage;
        private CPFinanceInvoicePaymentsPage _cpFinanceInvoicePaymentsPage;
        private CPFinanceInvoicePaymentRecordPage _cpFinanceInvoicePaymentRecordPage;
        private CPContractServicesPage _cpContractServicesPage;
        private CPContractServiceRecordPage _cpContractServiceRecordPage;
        private CloningContractServicePopup _cloningContractServicePopup;
        private PaymentAllocatePage _paymentAllocatePage;
        private UpliftRatesforMasterPayArrangementsPopup _upliftRatesforMasterPayArrangementsPopup;
        private PersonKeyworkerNotesPage _personKeyworkerNotesPage;
        private PersonKeyworkerNoteRecordPage _personKeyworkerNoteRecordPage;
        private KeyworkerNotesAttachmentsPage _keyworkerNotesAttachmentsPage;
        private KeyworkerNotesAttachmentRecordPage _keyworkerNotesAttachmentRecordPage;
        private MobilityAttachmentRecordPage _mobilityAttachmentRecordPage;
        private MobilityAttachmentsPage _mobilityAttachmentsPage;
        private IncidentTriggersPage _incidentTriggersPage;
        private CareTasksPage _careTasksPage;
        private ConversationsPage _conversationsPage;
        private ConversationRecordPage _conversationRecordPage;
        private HandoverCommentsPage _handoverCommentsPage;
        private HandoverCommentRecordPage _handoverCommentRecordPage;
        private TypeOfSensorPage _typeOfSensorPage;

        #endregion

        #region Public Properties
        

        public BookingPaymentRecordPage bookingPaymentRecordPage
        {
            get
            {
                if (_bookingPaymentRecordPage == null)
                    _bookingPaymentRecordPage = new BookingPaymentRecordPage(driver, wait, appURL);
                return _bookingPaymentRecordPage;
            }
        }


        public BookingPaymentsPage bookingPaymentsPage
        {
            get
            {
                if (_bookingPaymentsPage == null)
                    _bookingPaymentsPage = new BookingPaymentsPage(driver, wait, appURL);
                return _bookingPaymentsPage;
            }
        }


        public PayrollBatchRecordPage payrollBatchRecordPage
        {
            get
            {
                if (_payrollBatchRecordPage == null)
                    _payrollBatchRecordPage = new PayrollBatchRecordPage(driver, wait, appURL);
                return _payrollBatchRecordPage;
            }
        }


        public PayrollBatchesPage payrollBatchesPage
        {
            get
            {
                if (_payrollBatchesPage == null)
                    _payrollBatchesPage = new PayrollBatchesPage(driver, wait, appURL);
                return _payrollBatchesPage;
            }
        }


        public ChangeBookingStaffStatusDialogPopup changeBookingStaffStatusDialogPopup
        {
            get
            {
                if (_changeBookingStaffStatusDialogPopup == null)
                    _changeBookingStaffStatusDialogPopup = new ChangeBookingStaffStatusDialogPopup(driver, wait, appURL);
                return _changeBookingStaffStatusDialogPopup;
            }
        }


        public PersonalSafetyAndEnvironmentRecordPage personalSafetyAndEnvironmentRecordPage
        {
            get
            {
                if (_personalSafetyAndEnvironmentRecordPage == null)
                    _personalSafetyAndEnvironmentRecordPage = new PersonalSafetyAndEnvironmentRecordPage(driver, wait, appURL);
                return _personalSafetyAndEnvironmentRecordPage;
            }
        }

        public PersonalSafetyAndEnvironmentPage personalSafetyAndEnvironmentPage
        {
            get
            {
                if (_personalSafetyAndEnvironmentPage == null)
                    _personalSafetyAndEnvironmentPage = new PersonalSafetyAndEnvironmentPage(driver, wait, appURL);
                return _personalSafetyAndEnvironmentPage;
            }
        }

        public PainManagementRecordPage painManagementRecordPage
        {
            get
            {
                if (_painManagementRecordPage == null)
                    _painManagementRecordPage = new PainManagementRecordPage(driver, wait, appURL);
                return _painManagementRecordPage;
            }
        }


        public PainManagementPage painManagementPage
        {
            get
            {
                if (_painManagementPage == null)
                    _painManagementPage = new PainManagementPage(driver, wait, appURL);
                return _painManagementPage;
            }
        }


        public AttachmentsForFoodAndFluid attachmentsForFoodAndFluid
        {
            get
            {
                if (_attachmentsForFoodAndFluid == null)
                    _attachmentsForFoodAndFluid = new AttachmentsForFoodAndFluid(driver, wait, appURL);
                return _attachmentsForFoodAndFluid;
            }
        }


        public FoodAndFluidRecordPage foodAndFluidRecordPage
        {
            get
            {
                if (_foodAndFluidRecordPage == null)
                    _foodAndFluidRecordPage = new FoodAndFluidRecordPage(driver, wait, appURL);
                return _foodAndFluidRecordPage;
            }
        }


        public FoodAndFluidPage foodAndFluidPage
        {
            get
            {
                if (_foodAndFluidPage == null)
                    _foodAndFluidPage = new FoodAndFluidPage(driver, wait, appURL);
                return _foodAndFluidPage;
            }
        }


        public EmotionalSupportRecordPage emotionalSupportRecordPage
        {
            get
            {
                if (_emotionalSupportRecordPage == null)
                    _emotionalSupportRecordPage = new EmotionalSupportRecordPage(driver, wait, appURL);
                return _emotionalSupportRecordPage;
            }
        }


        public EmotionalSupportPage emotionalSupportPage
        {
            get
            {
                if (_emotionalSupportPage == null)
                    _emotionalSupportPage = new EmotionalSupportPage(driver, wait, appURL);
                return _emotionalSupportPage;
            }
        }


        public DistressedBehavioursPage distressedBehavioursPage
        {
            get
            {
                if (_distressedBehavioursPage == null)
                    _distressedBehavioursPage = new DistressedBehavioursPage(driver, wait, appURL);
                return _distressedBehavioursPage;
            }
        }

        public DistressedBehaviourRecordPage distressedBehaviourRecordPage
        {
            get
            {
                if (_distressedBehaviourRecordPage == null)
                    _distressedBehaviourRecordPage = new DistressedBehaviourRecordPage(driver, wait, appURL);
                return _distressedBehaviourRecordPage;
            }
        }


        public PersonDiaryEventsPage personDiaryEventsPage
        {
            get
            {
                if (_personDiaryEventsPage == null)
                    _personDiaryEventsPage = new PersonDiaryEventsPage(driver, wait, appURL);
                return _personDiaryEventsPage;
            }
        }


        public PersonDiaryEventRecordPage personDiaryEventRecordPage
        {
            get
            {
                if (_personDiaryEventRecordPage == null)
                    _personDiaryEventRecordPage = new PersonDiaryEventRecordPage(driver, wait, appURL);
                return _personDiaryEventRecordPage;
            }
        }


        public PersonActivityRecordPage personActivityRecordPage
        {
            get
            {
                if (_personActivityRecordPage == null)
                    _personActivityRecordPage = new PersonActivityRecordPage(driver, wait, appURL);
                return _personActivityRecordPage;
            }
        }

        public PersonActivitiesPage personActivitiesPage
        {
            get
            {
                if (_personActivitiesPage == null)
                    _personActivitiesPage = new PersonActivitiesPage(driver, wait, appURL);
                return _personActivitiesPage;
            }
        }


        public PersonalisedCareAndSupportPlanRecordPage personalisedCareAndSupportPlanRecordPage
        {
            get
            {
                if (_personalisedCareAndSupportPlanRecordPage == null)
                    _personalisedCareAndSupportPlanRecordPage = new PersonalisedCareAndSupportPlanRecordPage(driver, wait, appURL);
                return _personalisedCareAndSupportPlanRecordPage;
            }
        }


        public SelectPersonPhysicalObservationTypePopup selectPersonPhysicalObservationTypePopup
        {
            get
            {
                if (_selectPersonPhysicalObservationTypePopup == null)
                    _selectPersonPhysicalObservationTypePopup = new SelectPersonPhysicalObservationTypePopup(driver, wait, appURL);
                return _selectPersonPhysicalObservationTypePopup;
            }
        }


        public HeightAndWeightRecordPage heightAndWeightRecordPage
        {
            get
            {
                if (_heightAndWeightRecordPage == null)
                    _heightAndWeightRecordPage = new HeightAndWeightRecordPage(driver, wait, appURL);
                return _heightAndWeightRecordPage;
            }
        }


        public HeightAndWeightPage heightAndWeightPage
        {
            get
            {
                if (_heightAndWeightPage == null)
                    _heightAndWeightPage = new HeightAndWeightPage(driver, wait, appURL);
                return _heightAndWeightPage;
            }
        }


        public AttachmentForDailyPersonalCareRecordPage attachmentForDailyPersonalCareRecordPage
        {
            get
            {
                if (_attachmentForDailyPersonalCareRecordPage == null)
                    _attachmentForDailyPersonalCareRecordPage = new AttachmentForDailyPersonalCareRecordPage(driver, wait, appURL);
                return _attachmentForDailyPersonalCareRecordPage;
            }
        }


        public AttachmentsForDailyPersonalCare attachmentsForDailyPersonalCare
        {
            get
            {
                if (_attachmentsForDailyPersonalCare == null)
                    _attachmentsForDailyPersonalCare = new AttachmentsForDailyPersonalCare(driver, wait, appURL);
                return _attachmentsForDailyPersonalCare;
            }
        }


        public ContinenceCareRecordPage continenceCareRecordPage
        {
            get
            {
                if (_continenceCareRecordPage == null)
                    _continenceCareRecordPage = new ContinenceCareRecordPage(driver, wait, appURL);
                return _continenceCareRecordPage;
            }
        }


        public ContinenceCarePage continenceCarePage
        {
            get
            {
                if (_continenceCarePage == null)
                    _continenceCarePage = new ContinenceCarePage(driver, wait, appURL);
                return _continenceCarePage;
            }
        }


        public PersonDailyRecord_RecordPage personDailyRecord_RecordPage
        {
            get
            {
                if (_personDailyRecord_RecordPage == null)
                    _personDailyRecord_RecordPage = new PersonDailyRecord_RecordPage(driver, wait, appURL);
                return _personDailyRecord_RecordPage;
            }
        }


        public PersonDailyRecordsPage personDailyRecordsPage
        {
            get
            {
                if (_personDailyRecordsPage == null)
                    _personDailyRecordsPage = new PersonDailyRecordsPage(driver, wait, appURL);
                return _personDailyRecordsPage;
            }
        }


        public PersonDailyPersonalCareRecordPage personDailyPersonalCareRecordPage
        {
            get
            {
                if (_personDailyPersonalCareRecordPage == null)
                    _personDailyPersonalCareRecordPage = new PersonDailyPersonalCareRecordPage(driver, wait, appURL);
                return _personDailyPersonalCareRecordPage;
            }
        }


        public PersonRepositioningRecordPage personRepositioningRecordPage
        {
            get
            {
                if (_personRepositioningRecordPage == null)
                    _personRepositioningRecordPage = new PersonRepositioningRecordPage(driver, wait, appURL);
                return _personRepositioningRecordPage;
            }
        }


        public PersonRepositioningPage personRepositioningPage
        {
            get
            {
                if (_personRepositioningPage == null)
                    _personRepositioningPage = new PersonRepositioningPage(driver, wait, appURL);
                return _personRepositioningPage;
            }
        }


        public PersonWelfareCheckRecordPage personWelfareCheckRecordPage
        {
            get
            {
                if (_personWelfareCheckRecordPage == null)
                    _personWelfareCheckRecordPage = new PersonWelfareCheckRecordPage(driver, wait, appURL);
                return _personWelfareCheckRecordPage;
            }
        }


        public PersonWelfareChecksPage personWelfareChecksPage
        {
            get
            {
                if (_personWelfareChecksPage == null)
                    _personWelfareChecksPage = new PersonWelfareChecksPage(driver, wait, appURL);
                return _personWelfareChecksPage;
            }
        }


        public PersonDailyPersonalCarePage personDailyPersonalCarePage
        {
            get
            {
                if (_personDailyPersonalCarePage == null)
                    _personDailyPersonalCarePage = new PersonDailyPersonalCarePage(driver, wait, appURL);
                return _personDailyPersonalCarePage;
            }
        }


        public ClonePersonContractServiceRatePeriodPopup clonePersonContractServiceRatePeriodPopup
        {
            get
            {
                if (_clonePersonContractServiceRatePeriodPopup == null)
                    _clonePersonContractServiceRatePeriodPopup = new ClonePersonContractServiceRatePeriodPopup(driver, wait, appURL);
                return _clonePersonContractServiceRatePeriodPopup;
            }
        }


        public SpecifyCareWorkerPopup specifyCareWorkerPopup
        {
            get
            {
                if (_specifyCareWorkerPopup == null)
                    _specifyCareWorkerPopup = new SpecifyCareWorkerPopup(driver, wait, appURL);
                return _specifyCareWorkerPopup;
            }
        }


        public CopyBookingToDialogPopup copyBookingToDialogPopup
        {
            get
            {
                if (_copyBookingToDialogPopup == null)
                    _copyBookingToDialogPopup = new CopyBookingToDialogPopup(driver, wait, appURL);
                return _copyBookingToDialogPopup;
            }
        }


        public WallchartWarningDialogPopup wallchartWarningDialogPopup
        {
            get
            {
                if (_wallchartWarningDialogPopup == null)
                    _wallchartWarningDialogPopup = new WallchartWarningDialogPopup(driver, wait, appURL);
                return _wallchartWarningDialogPopup;
            }
        }


        public BandRateScheduleRecordPage bandRateScheduleRecordPage
        {
            get
            {
                if (_bandRateScheduleRecordPage == null)
                    _bandRateScheduleRecordPage = new BandRateScheduleRecordPage(driver, wait, appURL);
                return _bandRateScheduleRecordPage;
            }
        }


        public BandRateSchedulesPage bandRateSchedulesPage
        {
            get
            {
                if (_bandRateSchedulesPage == null)
                    _bandRateSchedulesPage = new BandRateSchedulesPage(driver, wait, appURL);
                return _bandRateSchedulesPage;
            }
        }


        public BandRateTypeRecordPage bandRateTypeRecordPage
        {
            get
            {
                if (_bandRateTypeRecordPage == null)
                    _bandRateTypeRecordPage = new BandRateTypeRecordPage(driver, wait, appURL);
                return _bandRateTypeRecordPage;
            }
        }

        public BandRateTypesPage bandRateTypesPage
        {
            get
            {
                if (_bandRateTypesPage == null)
                    _bandRateTypesPage = new BandRateTypesPage(driver, wait, appURL);
                return _bandRateTypesPage;
            }
        }

        public CloningContractServicePopup cloningContractServicePopup
        {
            get
            {
                if (_cloningContractServicePopup == null)
                    _cloningContractServicePopup = new CloningContractServicePopup(driver, wait, appURL);
                return _cloningContractServicePopup;
            }
        }

        public CPContractServiceRecordPage cpContractServiceRecordPage
        {
            get
            {
                if (_cpContractServiceRecordPage == null)
                    _cpContractServiceRecordPage = new CPContractServiceRecordPage(driver, wait, appURL);
                return _cpContractServiceRecordPage;
            }
        }

        public CPContractServicesPage cpContractServicesPage
        {
            get
            {
                if (_cpContractServicesPage == null)
                    _cpContractServicesPage = new CPContractServicesPage(driver, wait, appURL);
                return _cpContractServicesPage;
            }
        }

        public CPMasterPayArrangementRecordPage cpMasterPayArrangementRecordPage
        {
            get
            {
                if (_cpMasterPayArrangementRecordPage == null)
                    _cpMasterPayArrangementRecordPage = new CPMasterPayArrangementRecordPage(driver, wait, appURL);
                return _cpMasterPayArrangementRecordPage;
            }
        }


        public CPMasterPayArrangementsPage cpMasterPayArrangementsPage
        {
            get
            {
                if (_cpMasterPayArrangementsPage == null)
                    _cpMasterPayArrangementsPage = new CPMasterPayArrangementsPage(driver, wait, appURL);
                return _cpMasterPayArrangementsPage;
            }
        }


        public FinanceCodeUpdaterPopup financeCodeUpdaterPopup
        {
            get
            {
                if (_financeCodeUpdaterPopup == null)
                    _financeCodeUpdaterPopup = new FinanceCodeUpdaterPopup(driver, wait, appURL);
                return _financeCodeUpdaterPopup;
            }
        }


        public ProcessScheduledBookingsForWeekCommencingPopup processScheduledBookingsForWeekCommencingPopup
        {
            get
            {
                if (_processScheduledBookingsForWeekCommencingPopup == null)
                    _processScheduledBookingsForWeekCommencingPopup = new ProcessScheduledBookingsForWeekCommencingPopup(driver, wait, appURL);
                return _processScheduledBookingsForWeekCommencingPopup;
            }
        }


        public ExpressBookingCriteriaPage expressBookingCriteriaPage
        {
            get
            {
                if (_expressBookingCriteriaPage == null)
                    _expressBookingCriteriaPage = new ExpressBookingCriteriaPage(driver, wait, appURL);
                return _expressBookingCriteriaPage;
            }
        }


        public ExpressBookingCriteriaRecordPage expressBookingCriteriaRecordPage
        {
            get
            {
                if (_expressBookingCriteriaRecordPage == null)
                    _expressBookingCriteriaRecordPage = new ExpressBookingCriteriaRecordPage(driver, wait, appURL);
                return _expressBookingCriteriaRecordPage;
            }
        }


        public SelectMultiplePeoplePopup selectMultiplePeoplePopup
        {
            get
            {
                if (_selectMultiplePeoplePopup == null)
                    _selectMultiplePeoplePopup = new SelectMultiplePeoplePopup(driver, wait, appURL);
                return _selectMultiplePeoplePopup;
            }
        }


        public EmployeeDiaryPage employeeDiaryPage
        {
            get
            {
                if (_employeeDiaryPage == null)
                    _employeeDiaryPage = new EmployeeDiaryPage(driver, wait, appURL);
                return _employeeDiaryPage;
            }
        }


        public CreateDiaryBookingPopup createDiaryBookingPopup
        {
            get
            {
                if (_createDiaryBookingPopup == null)
                    _createDiaryBookingPopup = new CreateDiaryBookingPopup(driver, wait, appURL);
                return _createDiaryBookingPopup;
            }
        }


        public ContractServiceBandedRatesRecordPage contractServiceBandedRatesRecordPage
        {
            get
            {
                if (_contractServiceBandedRatesRecordPage == null)
                    _contractServiceBandedRatesRecordPage = new ContractServiceBandedRatesRecordPage(driver, wait, appURL);
                return _contractServiceBandedRatesRecordPage;
            }
        }


        public ContractServiceBandedRatesPage contractServiceBandedRatesPage
        {
            get
            {
                if (_contractServiceBandedRatesPage == null)
                    _contractServiceBandedRatesPage = new ContractServiceBandedRatesPage(driver, wait, appURL);
                return _contractServiceBandedRatesPage;
            }
        }


        public CloningContractServiceRatePeriodPopup cloningContractServiceRatePeriodPopup
        {
            get
            {
                if (_cloningContractServiceRatePeriodPopup == null)
                    _cloningContractServiceRatePeriodPopup = new CloningContractServiceRatePeriodPopup(driver, wait, appURL);
                return _cloningContractServiceRatePeriodPopup;
            }
        }


        public ContractServiceRatePeriodRecordPage contractServiceRatePeriodRecordPage
        {
            get
            {
                if (_contractServiceRatePeriodRecordPage == null)
                    _contractServiceRatePeriodRecordPage = new ContractServiceRatePeriodRecordPage(driver, wait, appURL);
                return _contractServiceRatePeriodRecordPage;
            }
        }


        public ContractServiceRatePeriodsPage contractServiceRatePeriodsPage
        {
            get
            {
                if (_contractServiceRatePeriodsPage == null)
                    _contractServiceRatePeriodsPage = new ContractServiceRatePeriodsPage(driver, wait, appURL);
                return _contractServiceRatePeriodsPage;
            }
        }


        public ContractServiceRecordPage contractServiceRecordPage
        {
            get
            {
                if (_contractServiceRecordPage == null)
                    _contractServiceRecordPage = new ContractServiceRecordPage(driver, wait, appURL);
                return _contractServiceRecordPage;
            }
        }


        public ContractServicesPage contractServicesPage
        {
            get
            {
                if (_contractServicesPage == null)
                    _contractServicesPage = new ContractServicesPage(driver, wait, appURL);
                return _contractServicesPage;
            }
        }


        public AttachmentForPersonalMoneyAccountDetailRecordPage attachmentForPersonalMoneyAccountDetailRecordPage
        {
            get
            {
                if (_attachmentForPersonalMoneyAccountDetailRecordPage == null)
                    _attachmentForPersonalMoneyAccountDetailRecordPage = new AttachmentForPersonalMoneyAccountDetailRecordPage(driver, wait, appURL);
                return _attachmentForPersonalMoneyAccountDetailRecordPage;
            }
        }


        public AttachmentsForPersonalMoneyAccountDetailPage attachmentsForPersonalMoneyAccountDetailPage
        {
            get
            {
                if (_attachmentsForPersonalMoneyAccountDetailPage == null)
                    _attachmentsForPersonalMoneyAccountDetailPage = new AttachmentsForPersonalMoneyAccountDetailPage(driver, wait, appURL);
                return _attachmentsForPersonalMoneyAccountDetailPage;
            }
        }


        public AttachmentForPersonalMoneyAccountRecordPage attachmentForPersonalMoneyAccountRecordPage
        {
            get
            {
                if (_attachmentForPersonalMoneyAccountRecordPage == null)
                    _attachmentForPersonalMoneyAccountRecordPage = new AttachmentForPersonalMoneyAccountRecordPage(driver, wait, appURL);
                return _attachmentForPersonalMoneyAccountRecordPage;
            }
        }


        public AttachmentsForPersonalMoneyAccountPage attachmentsForPersonalMoneyAccountPage
        {
            get
            {
                if (_attachmentsForPersonalMoneyAccountPage == null)
                    _attachmentsForPersonalMoneyAccountPage = new AttachmentsForPersonalMoneyAccountPage(driver, wait, appURL);
                return _attachmentsForPersonalMoneyAccountPage;
            }
        }


        public PersonalMoneyAccountDetailRecordPage personalMoneyAccountDetailRecordPage
        {
            get
            {
                if (_personalMoneyAccountDetailRecordPage == null)
                    _personalMoneyAccountDetailRecordPage = new PersonalMoneyAccountDetailRecordPage(driver, wait, appURL);
                return _personalMoneyAccountDetailRecordPage;

            }
        }


        public PersonalMoneyAccountDetailsPage personalMoneyAccountDetailsPage
        {
            get
            {
                if (_personalMoneyAccountDetailsPage == null)
                    _personalMoneyAccountDetailsPage = new PersonalMoneyAccountDetailsPage(driver, wait, appURL);
                return _personalMoneyAccountDetailsPage;

            }
        }


        public PersonalMoneyAccountsPage personalMoneyAccountsPage
        {
            get
            {
                if (_personalMoneyAccountsPage == null)
                    _personalMoneyAccountsPage = new PersonalMoneyAccountsPage(driver, wait, appURL);
                return _personalMoneyAccountsPage;

            }
        }


        public PersonalMoneyAccountRecordPage personalMoneyAccountRecordPage
        {
            get
            {
                if (_personalMoneyAccountRecordPage == null)
                    _personalMoneyAccountRecordPage = new PersonalMoneyAccountRecordPage(driver, wait, appURL);
                return _personalMoneyAccountRecordPage;

            }
        }


        public ContractServicesWithRatesPage contractServicesWithRatesPage
        {
            get
            {
                if (_contractServicesWithRatesPage == null)
                    _contractServicesWithRatesPage = new ContractServicesWithRatesPage(driver, wait, appURL);
                return _contractServicesWithRatesPage;

            }
        }


        public RecruitmentDocumentsManagementPage recruitmentDocumentsManagementPage
        {
            get
            {
                if (_recruitmentDocumentsManagementPage == null)
                    _recruitmentDocumentsManagementPage = new RecruitmentDocumentsManagementPage(driver, wait, appURL);
                return _recruitmentDocumentsManagementPage;

            }
        }


        public SystemUserRecruitmentDocumentsPage systemUserRecruitmentDocumentsPage
        {
            get
            {
                if (_systemUserRecruitmentDocumentsPage == null)
                    _systemUserRecruitmentDocumentsPage = new SystemUserRecruitmentDocumentsPage(driver, wait, appURL);
                return _systemUserRecruitmentDocumentsPage;

            }
        }


        public SystemUserRecruitmentDashboardPage systemUserRecruitmentDashboardPage
        {
            get
            {
                if (_systemUserRecruitmentDashboardPage == null)
                    _systemUserRecruitmentDashboardPage = new SystemUserRecruitmentDashboardPage(driver, wait, appURL);
                return _systemUserRecruitmentDashboardPage;

            }
        }


        public ApplicantDashboardPage applicantDashboardPage
        {
            get
            {
                if (_applicantDashboardPage == null)
                    _applicantDashboardPage = new ApplicantDashboardPage(driver, wait, appURL);
                return _applicantDashboardPage;


            }
        }


        public AvailabilityErrorDialogPopup availabilityErrorDialogPopup
        {
            get
            {
                if (_availabilityErrorDialogPopup == null)
                    _availabilityErrorDialogPopup = new AvailabilityErrorDialogPopup(driver, wait, appURL);
                return _availabilityErrorDialogPopup;

            }
        }


        public ScheduleAvailabilityPopup scheduleAvailabilityPopup
        {
            get
            {
                if (_scheduleAvailabilityPopup == null)
                    _scheduleAvailabilityPopup = new ScheduleAvailabilityPopup(driver, wait, appURL);
                return _scheduleAvailabilityPopup;

            }
        }


        public AvailabilitySaveChangesDialogPopup availabilitySaveChangesDialogPopup
        {
            get
            {
                if (_availabilitySaveChangesDialogPopup == null)
                    _availabilitySaveChangesDialogPopup = new AvailabilitySaveChangesDialogPopup(driver, wait, appURL);
                return _availabilitySaveChangesDialogPopup;

            }
        }


        public EndContractActionPopup endContractActionPopup
        {
            get
            {
                if (_endContractActionPopup == null)
                    _endContractActionPopup = new EndContractActionPopup(driver, wait, appURL);
                return _endContractActionPopup;

            }
        }


        public PersonSpecificTrainingRecordPage personSpecificTrainingRecordPage
        {
            get
            {
                if (_personSpecificTrainingRecordPage == null)
                    _personSpecificTrainingRecordPage = new PersonSpecificTrainingRecordPage(driver, wait, appURL);
                return _personSpecificTrainingRecordPage;

            }
        }


        public PersonSpecificTrainingsPage personSpecificTrainingsPage
        {
            get
            {
                if (_personSpecificTrainings == null)
                    _personSpecificTrainings = new PersonSpecificTrainingsPage(driver, wait, appURL);
                return _personSpecificTrainings;

            }
        }


        public ReasonForChangeOfSocialWorkerDialogPopup reasonForChangeOfSocialWorkerDialogPopup
        {
            get
            {
                if (_reasonForChangeOfSocialWorkerDialogPopup == null)
                    _reasonForChangeOfSocialWorkerDialogPopup = new ReasonForChangeOfSocialWorkerDialogPopup(driver, wait, appURL);
                return _reasonForChangeOfSocialWorkerDialogPopup;

            }
        }


        public CaseInvolvementRecordPage caseInvolvementRecordPage
        {
            get
            {
                if (_caseInvolvementRecordPage == null)
                    _caseInvolvementRecordPage = new CaseInvolvementRecordPage(driver, wait, appURL);
                return _caseInvolvementRecordPage;

            }
        }


        public CaseInvolvementsPage caseInvolvementsPage
        {
            get
            {
                if (_caseInvolvementsPage == null)
                    _caseInvolvementsPage = new CaseInvolvementsPage(driver, wait, appURL);
                return _caseInvolvementsPage;

            }
        }


        public SocialWorkerChangeReasonRecordPage socialWorkerChangeReasonRecordPage
        {
            get
            {
                if (_socialWorkerChangeReasonRecordPage == null)
                    _socialWorkerChangeReasonRecordPage = new SocialWorkerChangeReasonRecordPage(driver, wait, appURL);
                return _socialWorkerChangeReasonRecordPage;

            }
        }


        public SocialWorkerChangeReasonsPage socialWorkerChangeReasonsPage
        {
            get
            {
                if (_socialWorkerChangeReasonsPage == null)
                    _socialWorkerChangeReasonsPage = new SocialWorkerChangeReasonsPage(driver, wait, appURL);
                return _socialWorkerChangeReasonsPage;

            }
        }


        public Phoenix.UITests.Framework.PageObjects.People.PersonAbsencesPage personAbsencesPage
        {
            get
            {
                if (_PersonAbsencesPage == null)
                    _PersonAbsencesPage = new Phoenix.UITests.Framework.PageObjects.People.PersonAbsencesPage(driver, wait, appURL);
                return _PersonAbsencesPage;

            }
        }


        public MainMenuPinnedRecordsArea mainMenuPinnedRecordsArea
        {
            get
            {
                if (_mainMenuPinnedRecordsArea == null)
                    _mainMenuPinnedRecordsArea = new MainMenuPinnedRecordsArea(driver, wait, appURL);
                return _mainMenuPinnedRecordsArea;

            }
        }


        public Phoenix.UITests.Framework.PageObjects.People.PersonContractsPage personContractsPage
        {
            get
            {
                if (_personContractsPage == null)
                    _personContractsPage = new Phoenix.UITests.Framework.PageObjects.People.PersonContractsPage(driver, wait, appURL);
                return _personContractsPage;

            }
        }


        public Phoenix.UITests.Framework.PageObjects.People.PersonContractRecordPage personContractRecordPage
        {
            get
            {
                if (_personContractRecordPage == null)
                    _personContractRecordPage = new Phoenix.UITests.Framework.PageObjects.People.PersonContractRecordPage(driver, wait, appURL);
                return _personContractRecordPage;

            }
        }


        public CaseAttachmentRecordPage caseAttachmentRecordPage
        {
            get
            {
                if (_caseAttachmentRecordPage == null)
                    _caseAttachmentRecordPage = new CaseAttachmentRecordPage(driver, wait, appURL);
                return _caseAttachmentRecordPage;

            }
        }


        public CaseAttachmentsPage caseAttachmentsPage
        {
            get
            {
                if (_caseAttachmentsPage == null)
                    _caseAttachmentsPage = new CaseAttachmentsPage(driver, wait, appURL);
                return _caseAttachmentsPage;

            }
        }


        public EndContractWithSuspensionDatePopup endContractWithSuspensionDatePopup
        {
            get
            {
                if (_endContractWithSuspensionDatePopup == null)
                    _endContractWithSuspensionDatePopup = new EndContractWithSuspensionDatePopup(driver, wait, appURL);
                return _endContractWithSuspensionDatePopup;

            }
        }


        public StaffReviewRequirementsPopup staffReviewRequirementsPopup
        {
            get
            {
                if (_staffReviewRequirementsPopup == null)
                    _staffReviewRequirementsPopup = new StaffReviewRequirementsPopup(driver, wait, appURL);
                return _staffReviewRequirementsPopup;

            }
        }


        public AppointmentDialogPopup appointmentDialogPopup
        {
            get
            {
                if (_appointmentDialogPopup == null)
                    _appointmentDialogPopup = new AppointmentDialogPopup(driver, wait, appURL);
                return _appointmentDialogPopup;

            }
        }

        public DiaryViewSetupRestrictionsPage diaryViewSetupRestrictionsPage
        {
            get
            {
                if (_diaryViewSetupRestrictionsPage == null)
                    _diaryViewSetupRestrictionsPage = new DiaryViewSetupRestrictionsPage(driver, wait, appURL);
                return _diaryViewSetupRestrictionsPage;

            }
        }


        public DiaryViewSetupRestrictionRecordPage diaryViewSetupRestrictionRecordPage
        {
            get
            {
                if (_diaryViewSetupRestrictionRecordPage == null)
                    _diaryViewSetupRestrictionRecordPage = new DiaryViewSetupRestrictionRecordPage(driver, wait, appURL);
                return _diaryViewSetupRestrictionRecordPage;

            }
        }


        public LinkedProfessionalsPage linkedProfessionalsPage
        {
            get
            {
                if (_linkedProfessionalsPage == null)
                    _linkedProfessionalsPage = new LinkedProfessionalsPage(driver, wait, appURL);
                return _linkedProfessionalsPage;

            }
        }


        public LinkedProfessionalRecordPage linkedProfessionalRecordPage
        {
            get
            {
                if (_linkedProfessionalRecordPage == null)
                    _linkedProfessionalRecordPage = new LinkedProfessionalRecordPage(driver, wait, appURL);
                return _linkedProfessionalRecordPage;

            }
        }


        public DiaryViewSetupPage diaryViewSetupPage
        {
            get
            {
                if (_diaryViewSetupPage == null)
                    _diaryViewSetupPage = new DiaryViewSetupPage(driver, wait, appURL);
                return _diaryViewSetupPage;

            }
        }


        public DiaryViewSetupRecordPage diaryViewSetupRecordPage
        {
            get
            {
                if (_diaryViewSetupRecordPage == null)
                    _diaryViewSetupRecordPage = new DiaryViewSetupRecordPage(driver, wait, appURL);
                return _diaryViewSetupRecordPage;

            }
        }


        public HealthSetupPage healthSetupPage
        {
            get
            {
                if (_healthSetupPage == null)
                    _healthSetupPage = new HealthSetupPage(driver, wait, appURL);
                return _healthSetupPage;

            }
        }


        public CommunityClinicTeamsPage communityClinicTeamsPage
        {
            get
            {
                if (_communityClinicTeamsPage == null)
                    _communityClinicTeamsPage = new CommunityClinicTeamsPage(driver, wait, appURL);
                return _communityClinicTeamsPage;

            }
        }


        public CommunityClinicTeamRecordPage communityClinicTeamRecordPage
        {
            get
            {
                if (_communityClinicTeamRecordPage == null)
                    _communityClinicTeamRecordPage = new CommunityClinicTeamRecordPage(driver, wait, appURL);
                return _communityClinicTeamRecordPage;

            }
        }


        public PersonAddressRecordPage personAddressRecordPage
        {
            get
            {
                if (_personAddressRecordPage == null)
                    _personAddressRecordPage = new PersonAddressRecordPage(driver, wait, appURL);
                return _personAddressRecordPage;

            }
        }


        public CloneServiceDeliveryPopup cloneServiceDeliveryPopup
        {
            get
            {
                if (_cloneServiceDeliveryPopup == null)
                    _cloneServiceDeliveryPopup = new CloneServiceDeliveryPopup(driver, wait, appURL);
                return _cloneServiceDeliveryPopup;

            }
        }


        public ServiceDeliveriesPage serviceDeliveriesPage
        {
            get
            {
                if (_serviceDeliveriesPage == null)
                    _serviceDeliveriesPage = new ServiceDeliveriesPage(driver, wait, appURL);
                return _serviceDeliveriesPage;

            }
        }


        public ServiceDeliveryRecordPage serviceDeliveryRecordPage
        {
            get
            {
                if (_serviceDeliveryRecordPage == null)
                    _serviceDeliveryRecordPage = new ServiceDeliveryRecordPage(driver, wait, appURL);
                return _serviceDeliveryRecordPage;

            }
        }


        public RTTWaitTimesPage rttWaitTimesPage
        {
            get
            {
                if (_rttWaitTimesPage == null)
                    _rttWaitTimesPage = new RTTWaitTimesPage(driver, wait, appURL);
                return _rttWaitTimesPage;

            }
        }


        public RTTWaitTimeRecordPage rttWaitTimeRecordPage
        {
            get
            {
                if (_rttWaitTimeRecordPage == null)
                    _rttWaitTimeRecordPage = new RTTWaitTimeRecordPage(driver, wait, appURL);
                return _rttWaitTimeRecordPage;

            }
        }


        public RTTEventsPage rttEventsPage
        {
            get
            {
                if (_rttEventsPage == null)
                    _rttEventsPage = new RTTEventsPage(driver, wait, appURL);
                return _rttEventsPage;

            }
        }


        public RTTEventRecordPage rttEventRecordPage
        {
            get
            {
                if (_rttEventRecordPage == null)
                    _rttEventRecordPage = new RTTEventRecordPage(driver, wait, appURL);
                return _rttEventRecordPage;

            }
        }


        public ComplaintsAndFeedbackPage complaintsAndFeedbackPage
        {
            get
            {
                if (_complaintsAndFeedbackPage == null)
                    _complaintsAndFeedbackPage = new ComplaintsAndFeedbackPage(driver, wait, appURL);
                return _complaintsAndFeedbackPage;

            }
        }


        public ComplaintAndFeedbackRecordPage complaintAndFeedbackRecordPage
        {
            get
            {
                if (_complaintAndFeedbackRecordPage == null)
                    _complaintAndFeedbackRecordPage = new ComplaintAndFeedbackRecordPage(driver, wait, appURL);
                return _complaintAndFeedbackRecordPage;

            }
        }


        public AttendedEducationEstablishmentsPage attendedEducationEstablishmentsPage
        {
            get
            {
                if (_attendedEducationEstablishmentsPage == null)
                    _attendedEducationEstablishmentsPage = new AttendedEducationEstablishmentsPage(driver, wait, appURL);
                return _attendedEducationEstablishmentsPage;

            }
        }


        public AttendedEducationEstablishmentRecordPage attendedEducationEstablishmentRecordPage
        {
            get
            {
                if (_attendedEducationEstablishmentRecordPage == null)
                    _attendedEducationEstablishmentRecordPage = new AttendedEducationEstablishmentRecordPage(driver, wait, appURL);
                return _attendedEducationEstablishmentRecordPage;

            }
        }


        public EducationYearsPage educationYearsPage
        {
            get
            {
                if (_educationYearsPage == null)
                    _educationYearsPage = new EducationYearsPage(driver, wait, appURL);
                return _educationYearsPage;

            }
        }


        public EducationYearRecordPage educationYearRecordPage
        {
            get
            {
                if (_educationYearRecordPage == null)
                    _educationYearRecordPage = new EducationYearRecordPage(driver, wait, appURL);
                return _educationYearRecordPage;

            }
        }


        public ReactivateCasePopup reactivateCasePopup
        {
            get
            {
                if (_reactivateCasePopup == null)
                    _reactivateCasePopup = new ReactivateCasePopup(driver, wait, appURL);
                return _reactivateCasePopup;

            }
        }


        public TrainingRequirementsSetupPage trainingRequirementsSetupPage
        {
            get
            {
                if (_trainingRequirementsSetupPage == null)
                    _trainingRequirementsSetupPage = new TrainingRequirementsSetupPage(driver, wait, appURL);
                return _trainingRequirementsSetupPage;

            }
        }


        public TrainingRequirementSetupRecordPage trainingRequirementSetupRecordPage
        {
            get
            {
                if (_trainingRequirementSetupRecordPage == null)
                    _trainingRequirementSetupRecordPage = new TrainingRequirementSetupRecordPage(driver, wait, appURL);
                return _trainingRequirementSetupRecordPage;

            }
        }


        public TransportTypesPage transportTypesPage
        {
            get
            {
                if (_transportTypesPage == null)
                    _transportTypesPage = new TransportTypesPage(driver, wait, appURL);
                return _transportTypesPage;

            }
        }


        public TransportTypeRecordPage transportTypeRecordPage
        {
            get
            {
                if (_transportTypeRecordPage == null)
                    _transportTypeRecordPage = new TransportTypeRecordPage(driver, wait, appURL);
                return _transportTypeRecordPage;

            }
        }


        public ServiceProvisionCostPerWeekPage serviceProvisionCostPerWeekPage
        {
            get
            {
                if (_serviceProvisionCostPerWeekPage == null)
                    _serviceProvisionCostPerWeekPage = new ServiceProvisionCostPerWeekPage(driver, wait, appURL);
                return _serviceProvisionCostPerWeekPage;

            }
        }

        public ServiceProvisionCostPerWeekRecordPage serviceProvisionCostPerWeekRecordPage
        {
            get
            {
                if (_serviceProvisionCostPerWeekRecordPage == null)
                    _serviceProvisionCostPerWeekRecordPage = new ServiceProvisionCostPerWeekRecordPage(driver, wait, appURL);
                return _serviceProvisionCostPerWeekRecordPage;

            }
        }


        public FeesPage feesPage
        {
            get
            {
                if (_feesPage == null)
                    _feesPage = new FeesPage(driver, wait, appURL);
                return _feesPage;

            }
        }


        public FeeRecordPage feeRecordPage
        {
            get
            {
                if (_feeRecordPage == null)
                    _feeRecordPage = new FeeRecordPage(driver, wait, appURL);
                return _feeRecordPage;

            }
        }


        public ServiceProvisionAllowancesPage serviceProvisionAllowancesPage
        {
            get
            {
                if (_serviceProvisionAllowancesPage == null)
                    _serviceProvisionAllowancesPage = new ServiceProvisionAllowancesPage(driver, wait, appURL);
                return _serviceProvisionAllowancesPage;

            }
        }


        public ServiceProvisionAllowanceRecordPage serviceProvisionAllowanceRecordPage
        {
            get
            {
                if (_serviceProvisionAllowanceRecordPage == null)
                    _serviceProvisionAllowanceRecordPage = new ServiceProvisionAllowanceRecordPage(driver, wait, appURL);
                return _serviceProvisionAllowanceRecordPage;

            }
        }


        public FinancialAssessmentContributionsPage financialAssessmentContributionsPage
        {
            get
            {
                if (_financialAssessmentContributionsPage == null)
                    _financialAssessmentContributionsPage = new FinancialAssessmentContributionsPage(driver, wait, appURL);
                return _financialAssessmentContributionsPage;

            }
        }


        public FinancialAssessmentContributionRecordPage financialAssessmentContributionRecordPage
        {
            get
            {
                if (_financialAssessmentContributionRecordPage == null)
                    _financialAssessmentContributionRecordPage = new FinancialAssessmentContributionRecordPage(driver, wait, appURL);
                return _financialAssessmentContributionRecordPage;

            }
        }


        public StaffRecruitmentItemsPage staffRecruitmentItemsPage
        {
            get
            {
                if (_staffRecruitmentItemsPage == null)
                    _staffRecruitmentItemsPage = new StaffRecruitmentItemsPage(driver, wait, appURL);
                return _staffRecruitmentItemsPage;

            }
        }


        public StaffRecruitmentItemRecordPage staffRecruitmentItemRecordPage
        {
            get
            {
                if (_staffRecruitmentItemRecordPage == null)
                    _staffRecruitmentItemRecordPage = new StaffRecruitmentItemRecordPage(driver, wait, appURL);
                return _staffRecruitmentItemRecordPage;

            }
        }


        public BusinessModulesChecklistPage businessModulesChecklistPage
        {
            get
            {
                if (_businessModulesChecklistPage == null)
                    _businessModulesChecklistPage = new BusinessModulesChecklistPage(driver, wait, appURL);
                return _businessModulesChecklistPage;

            }
        }

        public SystemUserTrainingAttachmentRecordPage systemUserTrainingAttachmentRecordPage
        {
            get
            {
                if (_systemUserTrainingAttachmentRecordPage == null)
                    _systemUserTrainingAttachmentRecordPage = new SystemUserTrainingAttachmentRecordPage(driver, wait, appURL);
                return _systemUserTrainingAttachmentRecordPage;

            }
        }

        public SystemUserTrainingAttachmentsPage systemUserTrainingAttachmentsPage
        {
            get
            {
                if (_systemUserTrainingAttachmentsPage == null)
                    _systemUserTrainingAttachmentsPage = new SystemUserTrainingAttachmentsPage(driver, wait, appURL);
                return _systemUserTrainingAttachmentsPage;

            }
        }

        public SystemUserTrainingRecordPage systemUserTrainingRecordPage
        {
            get
            {
                if (_systemUserTrainingRecordPage == null)
                    _systemUserTrainingRecordPage = new SystemUserTrainingRecordPage(driver, wait, appURL);
                return _systemUserTrainingRecordPage;

            }
        }

        public SystemUserTrainingPage systemUserTrainingPage
        {
            get
            {
                if (_systemUserTrainingPage == null)
                    _systemUserTrainingPage = new SystemUserTrainingPage(driver, wait, appURL);
                return _systemUserTrainingPage;

            }
        }

        public SystemUserAbsencesPage systemUserAbsencesPage
        {
            get
            {
                if (_systemUserAbsencesPage == null)
                    _systemUserAbsencesPage = new SystemUserAbsencesPage(driver, wait, appURL);
                return _systemUserAbsencesPage;

            }
        }


        public SystemUserAbsencesRecordPage systemUserAbsencesRecordPage
        {
            get
            {
                if (_systemUserAbsencesRecordPage == null)
                    _systemUserAbsencesRecordPage = new SystemUserAbsencesRecordPage(driver, wait, appURL);
                return _systemUserAbsencesRecordPage;

            }
        }

        public ServiceMappingRecordPage serviceMappingRecordPage
        {
            get
            {
                if (_serviceMappingRecordPage == null)
                    _serviceMappingRecordPage = new ServiceMappingRecordPage(driver, wait, appURL);
                return _serviceMappingRecordPage;

            }
        }

        public ServiceMappingsPage serviceMappingsPage
        {
            get
            {
                if (_serviceMappingsPage == null)
                    _serviceMappingsPage = new ServiceMappingsPage(driver, wait, appURL);
                return _serviceMappingsPage;

            }
        }

        public RecruitmentRequirementsPage recruitmentRequirementsPage
        {
            get
            {
                if (_recruitmentRequirementsPage == null)
                    _recruitmentRequirementsPage = new RecruitmentRequirementsPage(driver, wait, appURL);
                return _recruitmentRequirementsPage;

            }
        }

        public StaffReviewStaffSupervisionFormRecordPage staffReviewStaffSupervisionFormRecordPage
        {
            get
            {
                if (_StaffReviewStaffSupervisionFormRecordPage == null)
                    _StaffReviewStaffSupervisionFormRecordPage = new StaffReviewStaffSupervisionFormRecordPage(driver, wait, appURL);
                return _StaffReviewStaffSupervisionFormRecordPage;

            }
        }

        public StaffReviewStaffSpotFormRecordPage staffReviewStaffSpotFormRecordPage
        {
            get
            {
                if (_StaffReviewStaffSpotFormRecordPage == null)
                    _StaffReviewStaffSpotFormRecordPage = new StaffReviewStaffSpotFormRecordPage(driver, wait, appURL);
                return _StaffReviewStaffSpotFormRecordPage;

            }
        }

        public RecruitmentRequirementRecordPage recruitmentRequirementRecordPage
        {
            get
            {
                if (_recruitmentRequirementRecordPage == null)
                    _recruitmentRequirementRecordPage = new RecruitmentRequirementRecordPage(driver, wait, appURL);
                return _recruitmentRequirementRecordPage;

            }
        }

        public CopyCarePlanPopupPage copyCarePlanPopupPage
        {
            get
            {
                if (_copyCarePlanPopupPage == null)
                    _copyCarePlanPopupPage = new CopyCarePlanPopupPage(driver, wait, appURL);
                return _copyCarePlanPopupPage;

            }
        }

        public NewCareNeedPopupPage newCareNeedPopupPage
        {
            get
            {
                if (_newCareNeedPopupPage == null)
                    _newCareNeedPopupPage = new NewCareNeedPopupPage(driver, wait, appURL);
                return _newCareNeedPopupPage;

            }
        }

        public Testing_CDV6_17721EditAssessmentPage testing_CDV6_17721EditAssessmentPage
        {
            get
            {
                if (_testing_CDV6_17721EditAssessmentPage == null)
                    _testing_CDV6_17721EditAssessmentPage = new Testing_CDV6_17721EditAssessmentPage(driver, wait, appURL);
                return _testing_CDV6_17721EditAssessmentPage;

            }
        }

        public AvailabilityDinamicDialogPopup availabilityDinamicDialogPopup
        {
            get
            {
                if (_availabilityDinamicDialogPopup == null)
                    _availabilityDinamicDialogPopup = new AvailabilityDinamicDialogPopup(driver, wait, appURL);
                return _availabilityDinamicDialogPopup;

            }
        }

        public AmendDiaryBookingPopUp amendDiaryBookingPopUp
        {
            get
            {
                if (_amendDiaryBookingPopUp == null)
                    _amendDiaryBookingPopUp = new AmendDiaryBookingPopUp(driver, wait, appURL);
                return _amendDiaryBookingPopUp;

            }
        }

        public ApplicantScheduleTransportSubPage applicantScheduleTransportSubPage
        {
            get
            {
                if (_applicantScheduleTransportSubPage == null)
                    _applicantScheduleTransportSubPage = new ApplicantScheduleTransportSubPage(driver, wait, appURL);
                return _applicantScheduleTransportSubPage;

            }
        }

        public ApplicantSelectScheduledTransportPopup applicantSelectScheduledTransportPopup
        {
            get
            {
                if (_applicantSelectScheduledTransportPopup == null)
                    _applicantSelectScheduledTransportPopup = new ApplicantSelectScheduledTransportPopup(driver, wait, appURL);
                return _applicantSelectScheduledTransportPopup;

            }
        }

        public CreateScheduledAvailabilityPopup createScheduledAvailabilityPopup
        {
            get
            {
                if (_createScheduledAvailabilityPopup == null)
                    _createScheduledAvailabilityPopup = new CreateScheduledAvailabilityPopup(driver, wait, appURL);
                return _createScheduledAvailabilityPopup;

            }

        }

        public CreateScheduleBookingPopup createScheduleBookingPopup
        {
            get
            {
                if (_createScheduleBookingPopup == null)
                    _createScheduleBookingPopup = new CreateScheduleBookingPopup(driver, wait, appURL);
                return _createScheduleBookingPopup;

            }

        }

        public SelectMultiplePeoplePopUp selectMultiplePeoplePopUp
        {
            get
            {
                if (_selectMultiplePeoplePopUp == null)
                    _selectMultiplePeoplePopUp = new SelectMultiplePeoplePopUp(driver, wait, appURL);
                return _selectMultiplePeoplePopUp;

            }

        }

        public SelectStaffPopup selectStaffPopup
        {
            get
            {
                if (_selectStaffPopup == null)
                    _selectStaffPopup = new SelectStaffPopup(driver, wait, appURL);
                return _selectStaffPopup;

            }

        }



        public CalendarPickerPopup calendarPickerPopup
        {
            get
            {
                if (_calendarPickerPopup == null)
                    _calendarPickerPopup = new CalendarPickerPopup(driver, wait, appURL);
                return _calendarPickerPopup;

            }
        }

        public LoginPage loginPage
        {
            get
            {
                if (_loginPage == null)
                    _loginPage = new LoginPage(driver, wait, appURL);
                return _loginPage;

            }
        }

        public HomePage homePage
        {
            get
            {
                if (_homePage == null)
                    _homePage = new HomePage(driver, wait, appURL);
                return _homePage;

            }
        }

        public HomePage_DashboardsArea homePage_DashboardsArea
        {
            get
            {
                if (_homePage_DashboardsArea == null)
                    _homePage_DashboardsArea = new HomePage_DashboardsArea(driver, wait, appURL);
                return _homePage_DashboardsArea;

            }
        }

        public MainMenu mainMenu
        {
            get
            {
                if (_mainMenu == null)
                    _mainMenu = new MainMenu(driver, wait, appURL);
                return _mainMenu;

            }
        }

        public ProfessionalsPage professionalsPage
        {
            get
            {
                if (_professionalsPage == null)
                    _professionalsPage = new ProfessionalsPage(driver, wait, appURL);
                return _professionalsPage;

            }
        }

        public ProfessionalRecordPage professionalRecordPage
        {
            get
            {
                if (_professionalRecordPage == null)
                    _professionalRecordPage = new ProfessionalRecordPage(driver, wait, appURL);
                return _professionalRecordPage;

            }
        }

        public DocumentsPage documentsPage
        {
            get
            {
                if (_documentsPage == null)
                    _documentsPage = new DocumentsPage(driver, wait, appURL);
                return _documentsPage;

            }
        }

        public DocumentPage documentPage
        {
            get
            {
                if (_documentPage == null)
                    _documentPage = new DocumentPage(driver, wait, appURL);
                return _documentPage;

            }
        }

        public DocumentApplicationAccessPage documentApplicationAccessPage
        {
            get
            {
                if (_documentApplicationAccessPage == null)
                    _documentApplicationAccessPage = new DocumentApplicationAccessPage(driver, wait, appURL);
                return _documentApplicationAccessPage;

            }
        }

        public DocumentApplicationAccessRecordPage documentApplicationAccessRecordPage
        {
            get
            {
                if (_documentApplicationAccessRecordPage == null)
                    _documentApplicationAccessRecordPage = new DocumentApplicationAccessRecordPage(driver, wait, appURL);
                return _documentApplicationAccessRecordPage;

            }
        }

        public DocumentSectionsSubPage documentSectionsSubPage
        {
            get
            {
                if (_documentSectionsSubPage == null)
                    _documentSectionsSubPage = new DocumentSectionsSubPage(driver, wait, appURL);
                return _documentSectionsSubPage;

            }
        }

        public DocumentSectionRecordPage documentSectionRecordPage
        {
            get
            {
                if (_documentSectionRecordPage == null)
                    _documentSectionRecordPage = new DocumentSectionRecordPage(driver, wait, appURL);
                return _documentSectionRecordPage;

            }
        }

        public DocumentSectionApplicationAccessPage documentSectionApplicationAccessPage
        {
            get
            {
                if (_documentSectionApplicationAccessPage == null)
                    _documentSectionApplicationAccessPage = new DocumentSectionApplicationAccessPage(driver, wait, appURL);
                return _documentSectionApplicationAccessPage;

            }
        }

        public DocumentSectionApplicationAccessRecordPage documentSectionApplicationAccessRecordPage
        {
            get
            {
                if (_documentSectionApplicationAccessRecordPage == null)
                    _documentSectionApplicationAccessRecordPage = new DocumentSectionApplicationAccessRecordPage(driver, wait, appURL);
                return _documentSectionApplicationAccessRecordPage;

            }
        }

        public DocumentSectionQuestionsSubPage documentSectionQuestionsSubPage
        {
            get
            {
                if (_documentSectionQuestionsSubPage == null)
                    _documentSectionQuestionsSubPage = new DocumentSectionQuestionsSubPage(driver, wait, appURL);
                return _documentSectionQuestionsSubPage;

            }
        }
        public SystemUserStaffReviewPage systemUserStaffReviewPage
        {
            get
            {
                if (_systemUserStaffReviewPage == null)
                    _systemUserStaffReviewPage = new SystemUserStaffReviewPage(driver, wait, appURL);
                return _systemUserStaffReviewPage;

            }
        }
        public StaffReviewRecordPage staffReviewRecordPage
        {
            get
            {
                if (_staffReviewRecordPage == null)
                    _staffReviewRecordPage = new StaffReviewRecordPage(driver, wait, appURL);
                return _staffReviewRecordPage;

            }

        }

        public StaffReviewAttchmentsRecordPage staffReviewAttchmentsRecordPage
        {
            get
            {
                if (_staffReviewAttchmentsRecordPage == null)
                    _staffReviewAttchmentsRecordPage = new StaffReviewAttchmentsRecordPage(driver, wait, appURL);
                return _staffReviewAttchmentsRecordPage;

            }
        }
        public StaffReviewAttachmentsPage staffReviewAttachmentsPage
        {
            get
            {
                if (_staffReviewAttachmentsPage == null)
                    _staffReviewAttachmentsPage = new StaffReviewAttachmentsPage(driver, wait, appURL);
                return _staffReviewAttachmentsPage;

            }
        }
        public MyStaffReviewPage staffReviewPage
        {
            get
            {
                if (_staffReviewPage == null)
                    _staffReviewPage = new MyStaffReviewPage(driver, wait, appURL);
                return _staffReviewPage;

            }
        }
        public ApplicantPage applicantPage
        {
            get
            {
                if (_applicantPage == null)
                    _applicantPage = new ApplicantPage(driver, wait, appURL);
                return _applicantPage;

            }
        }

        public ApplicantRecordPage applicantRecordPage
        {
            get
            {
                if (_applicantRecordPage == null)
                    _applicantRecordPage = new ApplicantRecordPage(driver, wait, appURL);
                return _applicantRecordPage;

            }
        }
        public ApplicantSheduleAvailability applicantSheduleAvailability
        {
            get
            {
                if (_applicantSheduleAvailability == null)
                    _applicantSheduleAvailability = new ApplicantSheduleAvailability(driver, wait, appURL);
                return _applicantSheduleAvailability;

            }
        }

        public ApplicantViewDiaryOrManageAdhoc applicantViewDiaryOrManageAdhoc
        {
            get
            {
                if (_applicantViewDiaryOrManageAdhoc == null)
                    _applicantViewDiaryOrManageAdhoc = new ApplicantViewDiaryOrManageAdhoc(driver, wait, appURL);
                return _applicantViewDiaryOrManageAdhoc;

            }
        }
        public CarePlanTypesPage carePlanTypesPage
        {
            get
            {
                if (_carePlanTypesPage == null)
                    _carePlanTypesPage = new CarePlanTypesPage(driver, wait, appURL);
                return _carePlanTypesPage;

            }
        }
        public CarePlanTypesRecordPage carePlanTypesRecordPage
        {
            get
            {
                if (_carePlanTypesRecordPage == null)
                    _carePlanTypesRecordPage = new CarePlanTypesRecordPage(driver, wait, appURL);
                return _carePlanTypesRecordPage;

            }
        }
        public SystemUserUserWorkScheduleRecordPage SystemUserUserWorkScheduleRecordPage
        {
            get
            {
                if (_SystemUserUserWorkScheduleRecordPage == null)
                    _SystemUserUserWorkScheduleRecordPage = new SystemUserUserWorkScheduleRecordPage(driver, wait, appURL);
                return _SystemUserUserWorkScheduleRecordPage;

            }
        }
        public StaffReviewRequirementsPage staffReviewRequirementsPage
        {
            get
            {
                if (_staffReviewRequirementsPage == null)
                    _staffReviewRequirementsPage = new StaffReviewRequirementsPage(driver, wait, appURL);
                return _staffReviewRequirementsPage;

            }
        }
        public StaffReviewRequirementsRecordPage staffReviewRequirementsRecordPage
        {
            get
            {
                if (_staffReviewRequirementsRecordPage == null)
                    _staffReviewRequirementsRecordPage = new StaffReviewRequirementsRecordPage(driver, wait, appURL);
                return _staffReviewRequirementsRecordPage;

            }
        }
        public StaffReviewFormsPage staffReviewFormsPage
        {
            get
            {
                if (_staffReviewFormsPage == null)
                    _staffReviewFormsPage = new StaffReviewFormsPage(driver, wait, appURL);
                return _staffReviewFormsPage;

            }
        }
        public StaffReviewFormsRecordPage staffReviewFormsRecordPage
        {
            get
            {
                if (_staffReviewFormsRecordPage == null)
                    _staffReviewFormsRecordPage = new StaffReviewFormsRecordPage(driver, wait, appURL);
                return _staffReviewFormsRecordPage;

            }
        }

        public StaffReviewFormQuestionsRecordPage staffReviewFormQuestionsRecordPage
        {
            get
            {
                if (_staffReviewFormQuestionsRecordPage == null)
                    _staffReviewFormQuestionsRecordPage = new StaffReviewFormQuestionsRecordPage(driver, wait, appURL);
                return _staffReviewFormQuestionsRecordPage;

            }
        }

        public StaffEvaluationFormQuestionsRecordPage staffEvaluationFormQuestionsRecordPage
        {
            get
            {
                if (_staffEvaluationFormQuestionsRecordPage == null)
                    _staffEvaluationFormQuestionsRecordPage = new StaffEvaluationFormQuestionsRecordPage(driver, wait, appURL);
                return _staffEvaluationFormQuestionsRecordPage;

            }
        }
        public DocumentSectionQuestionRecordPage documentSectionQuestionRecordPage
        {
            get
            {
                if (_documentSectionQuestionRecordPage == null)
                    _documentSectionQuestionRecordPage = new DocumentSectionQuestionRecordPage(driver, wait, appURL);
                return _documentSectionQuestionRecordPage;

            }
        }

        public DocumentSectionQuestionApplicationAccessPage documentSectionQuestionApplicationAccessPage
        {
            get
            {
                if (_documentSectionQuestionApplicationAccessPage == null)
                    _documentSectionQuestionApplicationAccessPage = new DocumentSectionQuestionApplicationAccessPage(driver, wait, appURL);
                return _documentSectionQuestionApplicationAccessPage;

            }
        }

        public DocumentSectionQuestionApplicationAccessRecordPage documentSectionQuestionApplicationAccessRecordPage
        {
            get
            {
                if (_documentSectionQuestionApplicationAccessRecordPage == null)
                    _documentSectionQuestionApplicationAccessRecordPage = new DocumentSectionQuestionApplicationAccessRecordPage(driver, wait, appURL);
                return _documentSectionQuestionApplicationAccessRecordPage;

            }
        }

        public AutomatedUiTestDocument4PreviewPage automatedUiTestDocument4PreviewPage
        {
            get
            {
                if (_automatedUiTestDocument4PreviewPage == null)
                    _automatedUiTestDocument4PreviewPage = new AutomatedUiTestDocument4PreviewPage(driver, wait, appURL);
                return _automatedUiTestDocument4PreviewPage;

            }
        }

        public MattTestScreeningToolTestSDEMapCopyAnswersPage mattTestScreeningToolTestSDEMapCopyAnswersPage
        {
            get
            {
                if (_mattTestScreeningToolTestSDEMapCopyAnswersPage == null)
                    _mattTestScreeningToolTestSDEMapCopyAnswersPage = new MattTestScreeningToolTestSDEMapCopyAnswersPage(driver, wait, appURL);
                return _mattTestScreeningToolTestSDEMapCopyAnswersPage;

            }
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage automatedUITestDocument2SDEMapCopyAnswersPage
        {
            get
            {
                if (_automatedUITestDocument2SDEMapCopyAnswersPage == null)
                    _automatedUITestDocument2SDEMapCopyAnswersPage = new AutomatedUITestDocument2SDEMapCopyAnswersPage(driver, wait, appURL);
                return _automatedUITestDocument2SDEMapCopyAnswersPage;

            }
        }

        public AutomatedUITestDocument2EditAssessmentPage automatedUITestDocument2EditAssessmentPage
        {
            get
            {
                if (_automatedUITestDocument2EditAssessmentPage == null)
                    _automatedUITestDocument2EditAssessmentPage = new AutomatedUITestDocument2EditAssessmentPage(driver, wait, appURL);
                return _automatedUITestDocument2EditAssessmentPage;

            }
        }

        public ManageSDEMapsPopup manageSDEMapsPopup
        {
            get
            {
                if (_manageSDEMapsPopup == null)
                    _manageSDEMapsPopup = new ManageSDEMapsPopup(driver, wait, appURL);
                return _manageSDEMapsPopup;

            }
        }

        public CasesPage casesPage
        {
            get
            {
                if (_casesPage == null)
                    _casesPage = new CasesPage(driver, wait, appURL);
                return _casesPage;

            }
        }

        public PeoplePage peoplePage
        {
            get
            {
                if (_peoplePage == null)
                    _peoplePage = new PeoplePage(driver, wait, appURL);
                return _peoplePage;

            }
        }

        public ProviderDiaryPage providerDiaryPage
        {
            get
            {
                if (_providerDiaryPage == null)
                    _providerDiaryPage = new ProviderDiaryPage(driver, wait, appURL);
                return _providerDiaryPage;

            }
        }

        public ProviderSchedulePage providerSchedulePage
        {
            get
            {
                if (_providerSchedulePage == null)
                    _providerSchedulePage = new ProviderSchedulePage(driver, wait, appURL);
                return _providerSchedulePage;

            }
        }

        public PeopleDiaryPage peopleDiaryPage
        {
            get
            {
                if (_peopleDiaryPage == null)
                    _peopleDiaryPage = new PeopleDiaryPage(driver, wait, appURL);
                return _peopleDiaryPage;

            }
        }

        public DiaryBookingsPage diaryBookingsPage
        {
            get
            {
                if (_diaryBookingsPage == null)
                    _diaryBookingsPage = new DiaryBookingsPage(driver, wait, appURL);
                return _diaryBookingsPage;

            }
        }

        public DiaryBookingsRecordPage diaryBookingsRecordPage
        {
            get
            {
                if (_diaryBookingsRecordPage == null)
                    _diaryBookingsRecordPage = new DiaryBookingsRecordPage(driver, wait, appURL);
                return _diaryBookingsRecordPage;

            }
        }

        public BookingDiaryStaffPage bookingDiaryStaffPage
        {
            get
            {
                if (_bookingDiaryStaffPage == null)
                    _bookingDiaryStaffPage = new BookingDiaryStaffPage(driver, wait, appURL);
                return _bookingDiaryStaffPage;

            }
        }

        public PersonRecordPage personRecordPage
        {
            get
            {
                if (_personRecordPage == null)
                    _personRecordPage = new PersonRecordPage(driver, wait, appURL);
                return _personRecordPage;

            }
        }

        public PersonHealthAbsencesPage personHealthAbsencesPage
        {
            get
            {
                if (_personHealthAbsencesPage == null)
                    _personHealthAbsencesPage = new PersonHealthAbsencesPage(driver, wait, appURL);
                return _personHealthAbsencesPage;

            }
        }

        public PersonHealthAbsencesRecordPage personHealthAbsencesRecordPage
        {
            get
            {
                if (_personHealthAbsencesRecordPage == null)
                    _personHealthAbsencesRecordPage = new PersonHealthAbsencesRecordPage(driver, wait, appURL);
                return _personHealthAbsencesRecordPage;

            }
        }

        public PersonRecordEditPage personRecordEditPage
        {
            get
            {
                if (_personRecordEditPage == null)
                    _personRecordEditPage = new PersonRecordEditPage(driver, wait, appURL);
                return _personRecordEditPage;

            }
        }

        public PersonRelationshipPage personRelationshipPage
        {
            get
            {
                if (_personRelationshipPage == null)
                    _personRelationshipPage = new PersonRelationshipPage(driver, wait, appURL);
                return _personRelationshipPage;

            }
        }

        public PersonFinancialAssessmentsPage personFinancialAssessmentsPage
        {
            get
            {
                if (_personFinancialAssessmentsPage == null)
                    _personFinancialAssessmentsPage = new PersonFinancialAssessmentsPage(driver, wait, appURL);
                return _personFinancialAssessmentsPage;

            }
        }

        public PersonServiceProvisionsPage personServiceProvisionsPage
        {
            get
            {
                if (_personServiceProvisionsPage == null)
                    _personServiceProvisionsPage = new PersonServiceProvisionsPage(driver, wait, appURL);
                return _personServiceProvisionsPage;

            }
        }

        public ServiceProvisionRecordPage serviceProvisionRecordPage
        {
            get
            {
                if (_serviceProvisionRecordPage == null)
                    _serviceProvisionRecordPage = new ServiceProvisionRecordPage(driver, wait, appURL);
                return _serviceProvisionRecordPage;

            }
        }

        public FinancialAssessmentRecordPage financialAssessmentRecordPage
        {
            get
            {
                if (_financialAssessmentRecordPage == null)
                    _financialAssessmentRecordPage = new FinancialAssessmentRecordPage(driver, wait, appURL);
                return _financialAssessmentRecordPage;

            }
        }

        public FinancialAssessmentChargesSubPage financialAssessmentChargesSubPage
        {
            get
            {
                if (_financialAssessmentChargesSubPage == null)
                    _financialAssessmentChargesSubPage = new FinancialAssessmentChargesSubPage(driver, wait, appURL);
                return _financialAssessmentChargesSubPage;

            }
        }

        public FinancialAssessmentChargePage financialAssessmentChargePage
        {
            get
            {
                if (_financialAssessmentChargePage == null)
                    _financialAssessmentChargePage = new FinancialAssessmentChargePage(driver, wait, appURL);
                return _financialAssessmentChargePage;

            }
        }

        public CaseFormPage caseFormPage
        {
            get
            {
                if (_caseFormPage == null)
                    _caseFormPage = new CaseFormPage(driver, wait, appURL);
                return _caseFormPage;

            }
        }

        public LookupPopup lookupPopup
        {
            get
            {
                if (_lookupPopup == null)
                    _lookupPopup = new LookupPopup(driver, wait, appURL);
                return _lookupPopup;

            }
        }

        public LookupMultiSelectPopup lookupMultiSelectPopup
        {
            get
            {
                if (_lookupMultiSelectPopup == null)
                    _lookupMultiSelectPopup = new LookupMultiSelectPopup(driver, wait, appURL);
                return _lookupMultiSelectPopup;

            }
        }

        public RegularCareLookupPopUp regularCareLookupPopUp
        {
            get
            {
                if (_regularCareLookupPopUp == null)
                    _regularCareLookupPopUp = new RegularCareLookupPopUp(driver, wait, appURL);
                return _regularCareLookupPopUp;

            }
        }

        public LookupViewPopup lookupViewPopup
        {
            get
            {
                if (_lookupViewPopup == null)
                    _lookupViewPopup = new LookupViewPopup(driver, wait, appURL);
                return _lookupViewPopup;

            }
        }

        public CaseCasesFormPage caseCasesFormPage
        {
            get
            {
                if (_caseCasesFormPage == null)
                    _caseCasesFormPage = new CaseCasesFormPage(driver, wait, appURL);
                return _caseCasesFormPage;

            }
        }

        public CaseRecordPage caseRecordPage
        {
            get
            {
                if (_caseRecordPage == null)
                    _caseRecordPage = new CaseRecordPage(driver, wait, appURL);
                return _caseRecordPage;

            }
        }

        public PrintAssessmentPopup printAssessmentPopup
        {
            get
            {
                if (_printAssessmentPopup == null)
                    _printAssessmentPopup = new PrintAssessmentPopup(driver, wait, appURL);
                return _printAssessmentPopup;

            }
        }

        public AssessmentPrintHistoryPopup assessmentPrintHistoryPopup
        {
            get
            {
                if (_assessmentPrintHistoryPopup == null)
                    _assessmentPrintHistoryPopup = new AssessmentPrintHistoryPopup(driver, wait, appURL);
                return _assessmentPrintHistoryPopup;

            }
        }

        public AlertPopup alertPopup
        {
            get
            {
                if (_alertPopup == null)
                    _alertPopup = new AlertPopup(driver, wait, appURL);
                return _alertPopup;

            }
        }

        public EditPrintAuditRecordPopup editPrintAuditRecordPopup
        {
            get
            {
                if (_editPrintAuditRecordPopup == null)
                    _editPrintAuditRecordPopup = new EditPrintAuditRecordPopup(driver, wait, appURL);
                return _editPrintAuditRecordPopup;

            }
        }

        public ShareRecordPopup shareRecordPopup
        {
            get
            {
                if (_shareRecordPopup == null)
                    _shareRecordPopup = new ShareRecordPopup(driver, wait, appURL);
                return _shareRecordPopup;

            }
        }

        public ShareRecordResultsPopup shareRecordResultsPopup
        {
            get
            {
                if (_shareRecordResultsPopup == null)
                    _shareRecordResultsPopup = new ShareRecordResultsPopup(driver, wait, appURL);
                return _shareRecordResultsPopup;

            }
        }

        public AutomatedUITestDocument1EditAssessmentPage automatedUITestDocument1EditAssessmentPage
        {
            get
            {
                if (_automatedUITestDocument1EditAssessmentPage == null)
                    _automatedUITestDocument1EditAssessmentPage = new AutomatedUITestDocument1EditAssessmentPage(driver, wait, appURL);
                return _automatedUITestDocument1EditAssessmentPage;

            }
        }

        public EditSignaturePopup editSignaturePopup
        {
            get
            {
                if (_editSignaturePopup == null)
                    _editSignaturePopup = new EditSignaturePopup(driver, wait, appURL);
                return _editSignaturePopup;

            }
        }

        public SectionInformationDialoguePopup sectionInformationDialoguePopup
        {
            get
            {
                if (_sectionInformationDialoguePopup == null)
                    _sectionInformationDialoguePopup = new SectionInformationDialoguePopup(driver, wait, appURL);
                return _sectionInformationDialoguePopup;

            }
        }

        public QuestionInformationDialoguePopup questionInformationDialoguePopup
        {
            get
            {
                if (_questionInformationDialoguePopup == null)
                    _questionInformationDialoguePopup = new QuestionInformationDialoguePopup(driver, wait, appURL);
                return _questionInformationDialoguePopup;

            }
        }

        public QuestionCommentsDialoguePopup questionCommentsDialoguePopup
        {
            get
            {
                if (_questionCommentsDialoguePopup == null)
                    _questionCommentsDialoguePopup = new QuestionCommentsDialoguePopup(driver, wait, appURL);
                return _questionCommentsDialoguePopup;

            }
        }

        public QuestionAuditDialoguePopup questionAuditDialoguePopup
        {
            get
            {
                if (_questionAuditDialoguePopup == null)
                    _questionAuditDialoguePopup = new QuestionAuditDialoguePopup(driver, wait, appURL);
                return _questionAuditDialoguePopup;

            }
        }

        public QuestionCompareDialoguePopup questionCompareDialoguePopup
        {
            get
            {
                if (_questionCompareDialoguePopup == null)
                    _questionCompareDialoguePopup = new QuestionCompareDialoguePopup(driver, wait, appURL);
                return _questionCompareDialoguePopup;

            }
        }

        public MailMergePopup mailMergePopup
        {
            get
            {
                if (_mailMergePopup == null)
                    _mailMergePopup = new MailMergePopup(driver, wait, appURL);
                return _mailMergePopup;

            }
        }

        public FinanceAdminPage financeAdminPage
        {
            get
            {
                if (_financeAdminPage == null)
                    _financeAdminPage = new FinanceAdminPage(driver, wait, appURL);
                return _financeAdminPage;

            }
        }

        public SchedulesSetupPage schedulesSetupPage
        {
            get
            {
                if (_schedulesSetupPage == null)
                    _schedulesSetupPage = new SchedulesSetupPage(driver, wait, appURL);
                return _schedulesSetupPage;

            }
        }

        public ScheduleSetupRecordPage scheduleSetupRecordPage
        {
            get
            {
                if (_scheduleSetupRecordPage == null)
                    _scheduleSetupRecordPage = new ScheduleSetupRecordPage(driver, wait, appURL);
                return _scheduleSetupRecordPage;

            }
        }

        public ChargingRulesSetupPage chargingRulesSetupPage
        {
            get
            {
                if (_chargingRulesSetupPage == null)
                    _chargingRulesSetupPage = new ChargingRulesSetupPage(driver, wait, appURL);
                return _chargingRulesSetupPage;

            }
        }

        public ChargingRuleSetupRecordPage chargingRuleSetupRecordPage
        {
            get
            {
                if (_chargingRuleSetupRecordPage == null)
                    _chargingRuleSetupRecordPage = new ChargingRuleSetupRecordPage(driver, wait, appURL);
                return _chargingRuleSetupRecordPage;

            }
        }

        public IncomeSupportSetupPage incomeSupportSetupPage
        {
            get
            {
                if (_incomeSupportSetupPage == null)
                    _incomeSupportSetupPage = new IncomeSupportSetupPage(driver, wait, appURL);
                return _incomeSupportSetupPage;

            }
        }

        public IncomeSupportSetupRecordPage incomeSupportSetupRecordPage
        {
            get
            {
                if (_incomeSupportSetupRecordPage == null)
                    _incomeSupportSetupRecordPage = new IncomeSupportSetupRecordPage(driver, wait, appURL);
                return _incomeSupportSetupRecordPage;

            }
        }

        public NonResidentialPolicyRateSetupPage nonResidentialPolicyRateSetupPage
        {
            get
            {
                if (_nonResidentialPolicyRateSetupPage == null)
                    _nonResidentialPolicyRateSetupPage = new NonResidentialPolicyRateSetupPage(driver, wait, appURL);
                return _nonResidentialPolicyRateSetupPage;

            }
        }

        public NonResidentialPolicyRateSetupRecordPage nonResidentialPolicyRateSetupRecordPage
        {
            get
            {
                if (_nonResidentialPolicyRateSetupRecordPage == null)
                    _nonResidentialPolicyRateSetupRecordPage = new NonResidentialPolicyRateSetupRecordPage(driver, wait, appURL);
                return _nonResidentialPolicyRateSetupRecordPage;

            }
        }

        public ChargesForServicesSetupPage chargesForServicesSetupPage
        {
            get
            {
                if (_chargesForServicesSetupPage == null)
                    _chargesForServicesSetupPage = new ChargesForServicesSetupPage(driver, wait, appURL);
                return _chargesForServicesSetupPage;

            }
        }

        public ChargeForServicesSetupRecordPage chargeForServicesSetupRecordPage
        {
            get
            {
                if (_chargeForServicesSetupRecordPage == null)
                    _chargeForServicesSetupRecordPage = new ChargeForServicesSetupRecordPage(driver, wait, appURL);
                return _chargeForServicesSetupRecordPage;

            }
        }

        public FinancialDetailRatesSetupPage financialDetailRatesSetupPage
        {
            get
            {
                if (_financialDetailRatesSetupPage == null)
                    _financialDetailRatesSetupPage = new FinancialDetailRatesSetupPage(driver, wait, appURL);
                return _financialDetailRatesSetupPage;

            }
        }

        public FinancialDetailRateSetupRecordPage financialDetailRateSetupRecordPage
        {
            get
            {
                if (_financialDetailRateSetupRecordPage == null)
                    _financialDetailRateSetupRecordPage = new FinancialDetailRateSetupRecordPage(driver, wait, appURL);
                return _financialDetailRateSetupRecordPage;

            }
        }

        public PersonFinancialDetailsSubPage personFinancialDetailsSubPage
        {
            get
            {
                if (_personFinancialDetailsSubPage == null)
                    _personFinancialDetailsSubPage = new PersonFinancialDetailsSubPage(driver, wait, appURL);
                return _personFinancialDetailsSubPage;

            }
        }

        public PersonFinancialDetailRecordPage personFinancialDetailRecordPage
        {
            get
            {
                if (_personFinancialDetailRecordPage == null)
                    _personFinancialDetailRecordPage = new PersonFinancialDetailRecordPage(driver, wait, appURL);
                return _personFinancialDetailRecordPage;

            }
        }

        public FinancialDetailDisregardsPage financialDetailDisregardsPage
        {
            get
            {
                if (_financialDetailDisregardsPage == null)
                    _financialDetailDisregardsPage = new FinancialDetailDisregardsPage(driver, wait, appURL);
                return _financialDetailDisregardsPage;

            }
        }

        public FinancialDetailDisregardRecordPage financialDetailDisregardRecordPage
        {
            get
            {
                if (_financialDetailDisregardRecordPage == null)
                    _financialDetailDisregardRecordPage = new FinancialDetailDisregardRecordPage(driver, wait, appURL);
                return _financialDetailDisregardRecordPage;

            }
        }

        public PersonContributionsSubPage personContributionsSubPage
        {
            get
            {
                if (_personContributionsSubPage == null)
                    _personContributionsSubPage = new PersonContributionsSubPage(driver, wait, appURL);
                return _personContributionsSubPage;

            }
        }

        public PersonContributionRecordPage personContributionRecordPage
        {
            get
            {
                if (_personContributionRecordPage == null)
                    _personContributionRecordPage = new PersonContributionRecordPage(driver, wait, appURL);
                return _personContributionRecordPage;

            }
        }

        public PersonContributionExceptionsPage personContributionExceptionsPage
        {
            get
            {
                if (_personContributionExceptionsPage == null)
                    _personContributionExceptionsPage = new PersonContributionExceptionsPage(driver, wait, appURL);
                return _personContributionExceptionsPage;

            }
        }

        public PersonContributionExceptionRecordPage personContributionExceptionRecordPage
        {
            get
            {
                if (_personContributionExceptionRecordPage == null)
                    _personContributionExceptionRecordPage = new PersonContributionExceptionRecordPage(driver, wait, appURL);
                return _personContributionExceptionRecordPage;

            }
        }

        public FinancialDetailRecordPage financialDetailRecordPage
        {
            get
            {
                if (_financialDetailRecordPage == null)
                    _financialDetailRecordPage = new FinancialDetailRecordPage(driver, wait, appURL);
                return _financialDetailRecordPage;

            }
        }

        public SystemUsersPage systemUsersPage
        {
            get
            {
                if (_systemUsersPage == null)
                    _systemUsersPage = new SystemUsersPage(driver, wait, appURL);
                return _systemUsersPage;

            }
        }

        public SystemUserRecordPage systemUserRecordPage
        {
            get
            {
                if (_systemUserRecordPage == null)
                    _systemUserRecordPage = new SystemUserRecordPage(driver, wait, appURL);
                return _systemUserRecordPage;

            }
        }

        public SystemUserTeamMemberSubPage systemUserTeamMemberSubPage
        {
            get
            {
                if (_systemUserTeamMemberSubPage == null)
                    _systemUserTeamMemberSubPage = new SystemUserTeamMemberSubPage(driver, wait, appURL);
                return _systemUserTeamMemberSubPage;

            }
        }

        public SystemUserAvailabilitySubPage systemUserAvailabilitySubPage
        {
            get
            {
                if (_systemUserAvailabilitySubPage == null)
                    _systemUserAvailabilitySubPage = new SystemUserAvailabilitySubPage(driver, wait, appURL);
                return _systemUserAvailabilitySubPage;

            }
        }

        public TeamMemberPage teamMemberPage
        {
            get
            {
                if (_teamMemberPage == null)
                    _teamMemberPage = new TeamMemberPage(driver, wait, appURL);
                return _teamMemberPage;

            }
        }

        public DynamicDialogPopup dynamicDialogPopup
        {
            get
            {
                if (_dynamicDialogPopup == null)
                    _dynamicDialogPopup = new DynamicDialogPopup(driver, wait, appURL);
                return _dynamicDialogPopup;

            }
        }

        public SpellCheckPopup spellCheckPopup
        {
            get
            {
                if (_spellCheckPopup == null)
                    _spellCheckPopup = new SpellCheckPopup(driver, wait, appURL);
                return _spellCheckPopup;

            }
        }

        public ConfirmDynamicDialogPopup confirmDynamicDialogPopup
        {
            get
            {
                if (_confirmDynamicDialogPopup == null)
                    _confirmDynamicDialogPopup = new ConfirmDynamicDialogPopup(driver, wait, appURL);
                return _confirmDynamicDialogPopup;

            }
        }

        public CustomizationsPage customizationsPage
        {
            get
            {
                if (_customizationsPage == null)
                    _customizationsPage = new CustomizationsPage(driver, wait, appURL);
                return _customizationsPage;

            }
        }

        public BusinessObjectsPage businessObjectsPage
        {
            get
            {
                if (_businessObjectsPage == null)
                    _businessObjectsPage = new BusinessObjectsPage(driver, wait, appURL);
                return _businessObjectsPage;

            }
        }

        public BusinessObjectRecordPage businessObjectRecordPage
        {
            get
            {
                if (_businessObjectRecordPage == null)
                    _businessObjectRecordPage = new BusinessObjectRecordPage(driver, wait, appURL);
                return _businessObjectRecordPage;

            }
        }

        public BusinessObjectFieldsSubPage businessObjectFieldsSubPage
        {
            get
            {
                if (_businessObjectFieldsSubPage == null)
                    _businessObjectFieldsSubPage = new BusinessObjectFieldsSubPage(driver, wait, appURL);
                return _businessObjectFieldsSubPage;

            }
        }

        public BusinessObjectFieldRecordPage businessObjectFieldRecordPage
        {
            get
            {
                if (_businessObjectFieldRecordPage == null)
                    _businessObjectFieldRecordPage = new BusinessObjectFieldRecordPage(driver, wait, appURL);
                return _businessObjectFieldRecordPage;

            }
        }

        public BulkEditDialogPopup bulkEditDialogPopup
        {
            get
            {
                if (_bulkEditDialogPopup == null)
                    _bulkEditDialogPopup = new BulkEditDialogPopup(driver, wait, appURL);
                return _bulkEditDialogPopup;

            }
        }

        public PersonCaseNotesPage personCaseNotesPage
        {
            get
            {
                if (_personCaseNotesPage == null)
                    _personCaseNotesPage = new PersonCaseNotesPage(driver, wait, appURL);
                return _personCaseNotesPage;

            }
        }

        public PersonCaseNoteRecordPage personCaseNoteRecordPage
        {
            get
            {
                if (_personCaseNoteRecordPage == null)
                    _personCaseNoteRecordPage = new PersonCaseNoteRecordPage(driver, wait, appURL);
                return _personCaseNoteRecordPage;

            }
        }

        public PersonDocumentViewSubPage personDocumentViewSubPage
        {
            get
            {
                if (_personDocumentViewSubPage == null)
                    _personDocumentViewSubPage = new PersonDocumentViewSubPage(driver, wait, appURL);
                return _personDocumentViewSubPage;

            }
        }

        public DataManagementPage dataManagementPage
        {
            get
            {
                if (_dataManagementPage == null)
                    _dataManagementPage = new DataManagementPage(driver, wait, appURL);
                return _dataManagementPage;

            }
        }

        public MergedRecordsPage mergedRecordsPage
        {
            get
            {
                if (_mergedRecordsPage == null)
                    _mergedRecordsPage = new MergedRecordsPage(driver, wait, appURL);
                return _mergedRecordsPage;

            }
        }

        public MergedRecordRecordPage mergedRecordRecordPage
        {
            get
            {
                if (_mergedRecordRecordPage == null)
                    _mergedRecordRecordPage = new MergedRecordRecordPage(driver, wait, appURL);
                return _mergedRecordRecordPage;

            }
        }

        public PersonTimelineSubPage personTimelineSubPage
        {
            get
            {
                if (_personTimelineSubPage == null)
                    _personTimelineSubPage = new PersonTimelineSubPage(driver, wait, appURL);
                return _personTimelineSubPage;

            }
        }

        public PersonAllActivitiesSubPage personAllActivitiesSubPage
        {
            get
            {
                if (_personAllActivitiesSubPage == null)
                    _personAllActivitiesSubPage = new PersonAllActivitiesSubPage(driver, wait, appURL);
                return _personAllActivitiesSubPage;

            }
        }

        public PersonCarePlansSubPage personCarePlansSubPage
        {
            get
            {
                if (_personCarePlansSubPage == null)
                    _personCarePlansSubPage = new PersonCarePlansSubPage(driver, wait, appURL);
                return _personCarePlansSubPage;

            }
        }


        public CareDiaryPage CareDiaryPage
        {
            get
            {
                if (_CareDiaryPage == null)
                    _CareDiaryPage = new CareDiaryPage(driver, wait, appURL);
                return _CareDiaryPage;

            }
        }

        public CareDiaryRecordPage CareDiaryRecordPage
        {
            get
            {
                if (_CareDiaryRecordPage == null)
                    _CareDiaryRecordPage = new CareDiaryRecordPage(driver, wait, appURL);
                return _CareDiaryRecordPage;

            }
        }
        public PersonCarePlansSubPage_AssessmentsTab PersonCarePlansSubPage_AssessmentsTab
        {
            get
            {
                if (_PersonCarePlansSubPage_AssessmentsTab == null)
                    _PersonCarePlansSubPage_AssessmentsTab = new PersonCarePlansSubPage_AssessmentsTab(driver, wait, appURL);
                return _PersonCarePlansSubPage_AssessmentsTab;

            }
        }

        public PersonCarePlansSubPage_CarePlansTab personCarePlansSubPage_CarePlansTab
        {
            get
            {
                if (_personCarePlansSubPage_CarePlansTab == null)
                    _personCarePlansSubPage_CarePlansTab = new PersonCarePlansSubPage_CarePlansTab(driver, wait, appURL);
                return _personCarePlansSubPage_CarePlansTab;

            }
        }
        public PersonCarePlansSubPage_RegularCareTasksTab personCarePlansSubPage_regularCareTasksTab
        {
            get
            {
                if (_personCarePlansSubPage_regularCareTasksTab == null)
                    _personCarePlansSubPage_regularCareTasksTab = new PersonCarePlansSubPage_RegularCareTasksTab(driver, wait, appURL);
                return _personCarePlansSubPage_regularCareTasksTab;

            }
        }
        public PersonChronologiesPage personChronologiesPage
        {
            get
            {
                if (_personChronologiesPage == null)
                    _personChronologiesPage = new PersonChronologiesPage(driver, wait, appURL);
                return _personChronologiesPage;

            }
        }

        public PersonChronologyRecordPage personChronologyRecordPage
        {
            get
            {
                if (_personChronologyRecordPage == null)
                    _personChronologyRecordPage = new PersonChronologyRecordPage(driver, wait, appURL);
                return _personChronologyRecordPage;

            }
        }

        public PersonChronologyRecordPrintPopup personChronologyRecordPrintPopup
        {
            get
            {
                if (_personChronologyRecordPrintPopup == null)
                    _personChronologyRecordPrintPopup = new PersonChronologyRecordPrintPopup(driver, wait, appURL);
                return _personChronologyRecordPrintPopup;

            }
        }

        public PersonPhysicalObservationsPage personPhysicalObservationsPage
        {
            get
            {
                if (_personPhysicalObservationsPage == null)
                    _personPhysicalObservationsPage = new PersonPhysicalObservationsPage(driver, wait, appURL);
                return _personPhysicalObservationsPage;

            }
        }

        public PersonPhysicalObservationsRecordPage personPhysicalObservationsRecordPage
        {
            get
            {
                if (_personPhysicalObservationsRecordPage == null)
                    _personPhysicalObservationsRecordPage = new PersonPhysicalObservationsRecordPage(driver, wait, appURL);
                return _personPhysicalObservationsRecordPage;

            }
        }

        public PersonPhysicalObservationsGenerateChartPopup personPhysicalObservationsGenerateChartPopup
        {
            get
            {
                if (_personPhysicalObservationsGenerateChartPopup == null)
                    _personPhysicalObservationsGenerateChartPopup = new PersonPhysicalObservationsGenerateChartPopup(driver, wait, appURL);
                return _personPhysicalObservationsGenerateChartPopup;

            }
        }

        public PersonEmailsPage personEmailsPage
        {
            get
            {
                if (_personEmailsPage == null)
                    _personEmailsPage = new PersonEmailsPage(driver, wait, appURL);
                return _personEmailsPage;

            }
        }

        public PersonEmailRecordPage personEmailRecordPage
        {
            get
            {
                if (_personEmailRecordPage == null)
                    _personEmailRecordPage = new PersonEmailRecordPage(driver, wait, appURL);
                return _personEmailRecordPage;

            }
        }

        public PersonEmailAttachmentsPage personEmailAttachmentsPage
        {
            get
            {
                if (_personEmailAttachmentsPage == null)
                    _personEmailAttachmentsPage = new PersonEmailAttachmentsPage(driver, wait, appURL);
                return _personEmailAttachmentsPage;

            }
        }

        public PersonEmailAttachmentRecordPage personEmailAttachmentRecordPage
        {
            get
            {
                if (_personEmailAttachmentRecordPage == null)
                    _personEmailAttachmentRecordPage = new PersonEmailAttachmentRecordPage(driver, wait, appURL);
                return _personEmailAttachmentRecordPage;

            }
        }

        public PersonCPISPage personCPISPage
        {
            get
            {
                if (_personCPISPage == null)
                    _personCPISPage = new PersonCPISPage(driver, wait, appURL);
                return _personCPISPage;

            }
        }

        public PersonCPISRecordPage personCPISRecordPage
        {
            get
            {
                if (_personCPISRecordPage == null)
                    _personCPISRecordPage = new PersonCPISRecordPage(driver, wait, appURL);
                return _personCPISRecordPage;

            }
        }

        public AuditListPage auditListPage
        {
            get
            {
                if (_auditListPage == null)
                    _auditListPage = new AuditListPage(driver, wait, appURL);
                return _auditListPage;

            }
        }

        public AuditChangeSetDialogPopup auditChangeSetDialogPopup
        {
            get
            {
                if (_auditChangeSetDialogPopup == null)
                    _auditChangeSetDialogPopup = new AuditChangeSetDialogPopup(driver, wait, appURL);
                return _auditChangeSetDialogPopup;

            }
        }

        public FileDestructionGDPRPage fileDestructionGDPRPage
        {
            get
            {
                if (_fileDestructionGDPRPage == null)
                    _fileDestructionGDPRPage = new FileDestructionGDPRPage(driver, wait, appURL);
                return _fileDestructionGDPRPage;

            }
        }

        public FileDestructionGDPRRecordPage fileDestructionGDPRRecordPage
        {
            get
            {
                if (_fileDestructionGDPRRecordPage == null)
                    _fileDestructionGDPRRecordPage = new FileDestructionGDPRRecordPage(driver, wait, appURL);
                return _fileDestructionGDPRRecordPage;

            }
        }

        public SystemDashboardsPage systemDashboardsPage
        {
            get
            {
                if (_systemDashboardsPage == null)
                    _systemDashboardsPage = new SystemDashboardsPage(driver, wait, appURL);
                return _systemDashboardsPage;

            }
        }

        public SystemDashboardRecordPage systemDashboardRecordPage
        {
            get
            {
                if (_systemDashboardRecordPage == null)
                    _systemDashboardRecordPage = new SystemDashboardRecordPage(driver, wait, appURL);
                return _systemDashboardRecordPage;

            }
        }

        public SummaryDashboardsPage summaryDashboardsPage
        {
            get
            {
                if (_summaryDashboardsPage == null)
                    _summaryDashboardsPage = new SummaryDashboardsPage(driver, wait, appURL);
                return _summaryDashboardsPage;

            }
        }

        public SummaryDashboardRecordPage summaryDashboardRecordPage
        {
            get
            {
                if (_summaryDashboardRecordPage == null)
                    _summaryDashboardRecordPage = new SummaryDashboardRecordPage(driver, wait, appURL);
                return _summaryDashboardRecordPage;

            }
        }

        public PersonRecordPage_SummaryArea personRecordPage_SummaryArea
        {
            get
            {
                if (_personRecordPage_SummaryArea == null)
                    _personRecordPage_SummaryArea = new PersonRecordPage_SummaryArea(driver, wait, appURL);
                return _personRecordPage_SummaryArea;

            }
        }

        public UserDashboardsPage userDashboardsPage
        {
            get
            {
                if (_userDashboardsPage == null)
                    _userDashboardsPage = new UserDashboardsPage(driver, wait, appURL);
                return _userDashboardsPage;

            }
        }

        public UserDashboardRecordPage userDashboardRecordPage
        {
            get
            {
                if (_userDashboardRecordPage == null)
                    _userDashboardRecordPage = new UserDashboardRecordPage(driver, wait, appURL);
                return _userDashboardRecordPage;

            }
        }

        public DashboardsPage dashboardsPage
        {
            get
            {
                if (_dashboardsPage == null)
                    _dashboardsPage = new DashboardsPage(driver, wait, appURL);
                return _dashboardsPage;

            }
        }

        public FAQsPage faqsPage
        {
            get
            {
                if (_faqsPage == null)
                    _faqsPage = new FAQsPage(driver, wait, appURL);
                return _faqsPage;

            }
        }

        public FAQRecordPage faqRecordPage
        {
            get
            {
                if (_faqRecordPage == null)
                    _faqRecordPage = new FAQRecordPage(driver, wait, appURL);
                return _faqRecordPage;

            }
        }

        public FAQApplicationComponentsPage faqApplicationComponentsPage
        {
            get
            {
                if (_faqApplicationComponentsPage == null)
                    _faqApplicationComponentsPage = new FAQApplicationComponentsPage(driver, wait, appURL);
                return _faqApplicationComponentsPage;

            }
        }

        public FAQApplicationComponentRecordPage faqApplicationComponentRecordPage
        {
            get
            {
                if (_faqApplicationComponentRecordPage == null)
                    _faqApplicationComponentRecordPage = new FAQApplicationComponentRecordPage(driver, wait, appURL);
                return _faqApplicationComponentRecordPage;

            }
        }

        public FAQSearchPage faqSearchPage
        {
            get
            {
                if (_faqSearchPage == null)
                    _faqSearchPage = new FAQSearchPage(driver, wait, appURL);
                return _faqSearchPage;

            }
        }

        public WebSitePage webSitePage
        {
            get
            {
                if (_webSitePage == null)
                    _webSitePage = new WebSitePage(driver, wait, appURL);
                return _webSitePage;

            }
        }

        public WebSiteRecordPage webSiteRecordPage
        {
            get
            {
                if (_webSiteRecordPage == null)
                    _webSiteRecordPage = new WebSiteRecordPage(driver, wait, appURL);
                return _webSiteRecordPage;

            }
        }

        public WebSitePagesPage webSitePagesPage
        {
            get
            {
                if (_webSitePagesPage == null)
                    _webSitePagesPage = new WebSitePagesPage(driver, wait, appURL);
                return _webSitePagesPage;

            }
        }

        public WebSitePageRecordPage webSitePageRecordPage
        {
            get
            {
                if (_webSitePageRecordPage == null)
                    _webSitePageRecordPage = new WebSitePageRecordPage(driver, wait, appURL);
                return _webSitePageRecordPage;

            }
        }

        public WebSiteSettingsPage webSiteSettingsPage
        {
            get
            {
                if (_webSiteSettingsPage == null)
                    _webSiteSettingsPage = new WebSiteSettingsPage(driver, wait, appURL);
                return _webSiteSettingsPage;

            }
        }

        public WebSiteSettingRecordPage webSiteSettingRecordPage
        {
            get
            {
                if (_webSiteSettingRecordPage == null)
                    _webSiteSettingRecordPage = new WebSiteSettingRecordPage(driver, wait, appURL);
                return _webSiteSettingRecordPage;

            }
        }

        public ChangePasswordPopup changePasswordPopup
        {
            get
            {
                if (_changePasswordPopup == null)
                    _changePasswordPopup = new ChangePasswordPopup(driver, wait, appURL);
                return _changePasswordPopup;

            }
        }

        public ChangeEncryptedValuePopup changeEncryptedValuePopup
        {
            get
            {
                if (_changeEncryptedValuePopup == null)
                    _changeEncryptedValuePopup = new ChangeEncryptedValuePopup(driver, wait, appURL);
                return _changeEncryptedValuePopup;

            }
        }

        public WebSiteAnnouncementsPage webSiteAnnouncementsPage
        {
            get
            {
                if (_webSiteAnnouncementsPage == null)
                    _webSiteAnnouncementsPage = new WebSiteAnnouncementsPage(driver, wait, appURL);
                return _webSiteAnnouncementsPage;

            }
        }

        public WebSiteAnnouncementRecordPage webSiteAnnouncementRecordPage
        {
            get
            {
                if (_webSiteAnnouncementRecordPage == null)
                    _webSiteAnnouncementRecordPage = new WebSiteAnnouncementRecordPage(driver, wait, appURL);
                return _webSiteAnnouncementRecordPage;

            }
        }

        public WebSiteUsersPage webSiteUsersPage
        {
            get
            {
                if (_webSiteUsersPage == null)
                    _webSiteUsersPage = new WebSiteUsersPage(driver, wait, appURL);
                return _webSiteUsersPage;

            }
        }

        public GenericWebSiteUsersPage genericWebSiteUsersPage
        {
            get
            {
                if (_genericWebSiteUsersPage == null)
                    _genericWebSiteUsersPage = new GenericWebSiteUsersPage(driver, wait, appURL);
                return _genericWebSiteUsersPage;

            }
        }

        public WebSiteUserRecordPage webSiteUserRecordPage
        {
            get
            {
                if (_webSiteUserRecordPage == null)
                    _webSiteUserRecordPage = new WebSiteUserRecordPage(driver, wait, appURL);
                return _webSiteUserRecordPage;

            }
        }

        public WebSiteUserPinsPage webSiteUserPinsPage
        {
            get
            {
                if (_webSiteUserPinsPage == null)
                    _webSiteUserPinsPage = new WebSiteUserPinsPage(driver, wait, appURL);
                return _webSiteUserPinsPage;

            }
        }

        public WebSiteUserPinRecordPage webSiteUserPinRecordPage
        {
            get
            {
                if (_webSiteUserPinRecordPage == null)
                    _webSiteUserPinRecordPage = new WebSiteUserPinRecordPage(driver, wait, appURL);
                return _webSiteUserPinRecordPage;

            }
        }

        public WebsiteUserPasswordResetsPage websiteUserPasswordResetsPage
        {
            get
            {
                if (_websiteUserPasswordResetsPage == null)
                    _websiteUserPasswordResetsPage = new WebsiteUserPasswordResetsPage(driver, wait, appURL);
                return _websiteUserPasswordResetsPage;

            }
        }

        public WebSiteUserPasswordResetRecordPage webSiteUserPasswordResetRecordPage
        {
            get
            {
                if (_webSiteUserPasswordResetRecordPage == null)
                    _webSiteUserPasswordResetRecordPage = new WebSiteUserPasswordResetRecordPage(driver, wait, appURL);
                return _webSiteUserPasswordResetRecordPage;

            }
        }

        public WebsiteUserPasswordHistoryPage websiteUserPasswordHistoryPage
        {
            get
            {
                if (_websiteUserPasswordHistoryPage == null)
                    _websiteUserPasswordHistoryPage = new WebsiteUserPasswordHistoryPage(driver, wait, appURL);
                return _websiteUserPasswordHistoryPage;

            }
        }

        public WebSiteUserPasswordHistoryRecordPage websiteUserPasswordHistoryRecordPage
        {
            get
            {
                if (_websiteUserPasswordHistoryRecordPage == null)
                    _websiteUserPasswordHistoryRecordPage = new WebSiteUserPasswordHistoryRecordPage(driver, wait, appURL);
                return _websiteUserPasswordHistoryRecordPage;

            }
        }

        public WebSiteUserAccessAuditPage webSiteUserAccessAuditPage
        {
            get
            {
                if (_webSiteUserAccessAuditPage == null)
                    _webSiteUserAccessAuditPage = new WebSiteUserAccessAuditPage(driver, wait, appURL);
                return _webSiteUserAccessAuditPage;

            }
        }

        public WebsiteUserAccessAudiRecordPage websiteUserAccessAudiRecordPage
        {
            get
            {
                if (_websiteUserAccessAudiRecordPage == null)
                    _websiteUserAccessAudiRecordPage = new WebsiteUserAccessAudiRecordPage(driver, wait, appURL);
                return _websiteUserAccessAudiRecordPage;

            }
        }

        public WebSiteUserAccessAuditTab webSiteUserAccessAuditTab
        {
            get
            {
                if (_webSiteUserAccessAuditTab == null)
                    _webSiteUserAccessAuditTab = new WebSiteUserAccessAuditTab(driver, wait, appURL);
                return _webSiteUserAccessAuditTab;

            }
        }

        public WebSitePageRecordDesignerSubPage webSitePageRecordDesignerSubPage
        {
            get
            {
                if (_webSitePageRecordDesignerSubPage == null)
                    _webSitePageRecordDesignerSubPage = new WebSitePageRecordDesignerSubPage(driver, wait, appURL);
                return _webSitePageRecordDesignerSubPage;

            }
        }

        public WebsitePageWidgetSettingsPopup websitePageWidgetSettingsPopup
        {
            get
            {
                if (_websitePageWidgetSettingsPopup == null)
                    _websitePageWidgetSettingsPopup = new WebsitePageWidgetSettingsPopup(driver, wait, appURL);
                return _websitePageWidgetSettingsPopup;

            }
        }

        public WebSiteSitemapsPage webSiteSitemapsPage
        {
            get
            {
                if (_webSiteSitemapsPage == null)
                    _webSiteSitemapsPage = new WebSiteSitemapsPage(driver, wait, appURL);
                return _webSiteSitemapsPage;

            }
        }

        public WebSiteSitemapRecordPage webSiteSitemapRecordPage
        {
            get
            {
                if (_webSiteSitemapRecordPage == null)
                    _webSiteSitemapRecordPage = new WebSiteSitemapRecordPage(driver, wait, appURL);
                return _webSiteSitemapRecordPage;

            }
        }

        public WebSiteSitemapRecordDesignerPage webSiteSitemapRecordDesignerPage
        {
            get
            {
                if (_webSiteSitemapRecordDesignerPage == null)
                    _webSiteSitemapRecordDesignerPage = new WebSiteSitemapRecordDesignerPage(driver, wait, appURL);
                return _webSiteSitemapRecordDesignerPage;

            }
        }

        public WebSitePointsOfContactPage webSitePointsOfContactPage
        {
            get
            {
                if (_webSitePointsOfContactPage == null)
                    _webSitePointsOfContactPage = new WebSitePointsOfContactPage(driver, wait, appURL);
                return _webSitePointsOfContactPage;

            }
        }

        public WebSitePointOfContactRecordPage webSitePointOfContactRecordPage
        {
            get
            {
                if (_webSitePointOfContactRecordPage == null)
                    _webSitePointOfContactRecordPage = new WebSitePointOfContactRecordPage(driver, wait, appURL);
                return _webSitePointOfContactRecordPage;

            }
        }

        public WebSiteFeedbackPage webSiteFeedbackPage
        {
            get
            {
                if (_webSiteFeedbackPage == null)
                    _webSiteFeedbackPage = new WebSiteFeedbackPage(driver, wait, appURL);
                return _webSiteFeedbackPage;

            }
        }

        public WebSiteFeedbackRecordPage webSiteFeedbackRecordPage
        {
            get
            {
                if (_webSiteFeedbackRecordPage == null)
                    _webSiteFeedbackRecordPage = new WebSiteFeedbackRecordPage(driver, wait, appURL);
                return _webSiteFeedbackRecordPage;

            }
        }

        public WebSiteContactsPage webSiteContactsPage
        {
            get
            {
                if (_webSiteContactsPage == null)
                    _webSiteContactsPage = new WebSiteContactsPage(driver, wait, appURL);
                return _webSiteContactsPage;

            }
        }

        public WebSiteContactRecordPage webSiteContactRecordPage
        {
            get
            {
                if (_webSiteContactRecordPage == null)
                    _webSiteContactRecordPage = new WebSiteContactRecordPage(driver, wait, appURL);
                return _webSiteContactRecordPage;

            }
        }

        public PersonFormsPage personFormsPage
        {
            get
            {
                if (_personFormsPage == null)
                    _personFormsPage = new PersonFormsPage(driver, wait, appURL);
                return _personFormsPage;

            }
        }

        public PersonFormRecordPage personFormRecordPage
        {
            get
            {
                if (_personFormRecordPage == null)
                    _personFormRecordPage = new PersonFormRecordPage(driver, wait, appURL);
                return _personFormRecordPage;

            }
        }

        public PersonFormInvolvementRecordPage personFormInvolvementRecordPage
        {
            get
            {
                if (_personFormInvolvementRecordPage == null)
                    _personFormInvolvementRecordPage = new PersonFormInvolvementRecordPage(driver, wait, appURL);
                return _personFormInvolvementRecordPage;

            }
        }

        public UserChartsPage userChartsPage
        {
            get
            {
                if (_userChartsPage == null)
                    _userChartsPage = new UserChartsPage(driver, wait, appURL);
                return _userChartsPage;

            }
        }

        public UserChartPopup userChartPopup
        {
            get
            {
                if (_userChartPopup == null)
                    _userChartPopup = new UserChartPopup(driver, wait, appURL);
                return _userChartPopup;

            }
        }

        public QueryResultsPage queryResultsPage
        {
            get
            {
                if (_queryResultsPage == null)
                    _queryResultsPage = new QueryResultsPage(driver, wait, appURL);
                return _queryResultsPage;

            }
        }

        public WebsitePortalTasksPage websitePortalTasksPage
        {
            get
            {
                if (_websitePortalTasksPage == null)
                    _websitePortalTasksPage = new WebsitePortalTasksPage(driver, wait, appURL);
                return _websitePortalTasksPage;

            }
        }

        public WebsitePortalTaskRecordPage websitePortalTaskRecordPage
        {
            get
            {
                if (_websitePortalTaskRecordPage == null)
                    _websitePortalTaskRecordPage = new WebsitePortalTaskRecordPage(driver, wait, appURL);
                return _websitePortalTaskRecordPage;

            }
        }

        public PersonPortalTasksPage PersonPortalTasksPage
        {
            get
            {
                if (_PersonPortalTasksPage == null)
                    _PersonPortalTasksPage = new PersonPortalTasksPage(driver, wait, appURL);
                return _PersonPortalTasksPage;

            }
        }

        public PersonPortalTaskRecordPage PersonPortalTaskRecordPage
        {
            get
            {
                if (_PersonPortalTaskRecordPage == null)
                    _PersonPortalTaskRecordPage = new PersonPortalTaskRecordPage(driver, wait, appURL);
                return _PersonPortalTaskRecordPage;

            }
        }

        public ReferenceDataPage referenceDataPage
        {
            get
            {
                if (_referenceDataPage == null)
                    _referenceDataPage = new ReferenceDataPage(driver, wait, appURL);
                return _referenceDataPage;

            }
        }

        public AdvanceSearchPage advanceSearchPage
        {
            get
            {
                if (_advanceSearchPage == null)
                    _advanceSearchPage = new AdvanceSearchPage(driver, wait, appURL);
                return _advanceSearchPage;

            }
        }

        public PersonFormCaseNotesPage personFormCaseNotesPage
        {
            get
            {
                if (_personFormCaseNotesPage == null)
                    _personFormCaseNotesPage = new PersonFormCaseNotesPage(driver, wait, appURL);
                return _personFormCaseNotesPage;

            }
        }

        public PersonFormCaseNoteRecordPage personFormCaseNoteRecordPage
        {
            get
            {
                if (_personFormCaseNoteRecordPage == null)
                    _personFormCaseNoteRecordPage = new PersonFormCaseNoteRecordPage(driver, wait, appURL);
                return _personFormCaseNoteRecordPage;

            }
        }

        public PersonFormAppointmentsPage personFormAppointmentsPage
        {
            get
            {
                if (_personFormAppointmentsPage == null)
                    _personFormAppointmentsPage = new PersonFormAppointmentsPage(driver, wait, appURL);
                return _personFormAppointmentsPage;

            }
        }

        public PersonFormAppointmentRecordPage personFormAppointmentRecordPage
        {
            get
            {
                if (_personFormAppointmentRecordPage == null)
                    _personFormAppointmentRecordPage = new PersonFormAppointmentRecordPage(driver, wait, appURL);
                return _personFormAppointmentRecordPage;

            }
        }

        public PersonFormEmailsPage personFormEmailsPage
        {
            get
            {
                if (_personFormEmailsPage == null)
                    _personFormEmailsPage = new PersonFormEmailsPage(driver, wait, appURL);
                return _personFormEmailsPage;

            }
        }

        public PersonFormEmailRecordPage personFormEmailRecordPage
        {
            get
            {
                if (_personFormEmailRecordPage == null)
                    _personFormEmailRecordPage = new PersonFormEmailRecordPage(driver, wait, appURL);
                return _personFormEmailRecordPage;

            }
        }

        public PersonFormLettersPage personFormLettersPage
        {
            get
            {
                if (_personFormLettersPage == null)
                    _personFormLettersPage = new PersonFormLettersPage(driver, wait, appURL);
                return _personFormLettersPage;

            }
        }

        public PersonFormLetterRecordPage personFormLetterRecordPage
        {
            get
            {
                if (_personFormLetterRecordPage == null)
                    _personFormLetterRecordPage = new PersonFormLetterRecordPage(driver, wait, appURL);
                return _personFormLetterRecordPage;

            }
        }

        public PersonFormInvolvementRecordSubPage personFormInvolvementRecordSubPage
        {
            get
            {
                if (_personFormInvolvementRecordSubPage == null)
                    _personFormInvolvementRecordSubPage = new PersonFormInvolvementRecordSubPage(driver, wait, appURL);
                return _personFormInvolvementRecordSubPage;

            }
        }

        public PersonPhoneCallsPage personPhoneCallsPage
        {
            get
            {
                if (_personPhoneCallsPage == null)
                    _personPhoneCallsPage = new PersonPhoneCallsPage(driver, wait, appURL);
                return _personPhoneCallsPage;

            }
        }

        public PersonFormPhoneCallsPage personFormPhoneCallsPage
        {
            get
            {
                if (_personFormPhoneCallsPage == null)
                    _personFormPhoneCallsPage = new PersonFormPhoneCallsPage(driver, wait, appURL);
                return _personFormPhoneCallsPage;

            }
        }

        public PersonPhoneCallRecordPage personPhoneCallRecordPage
        {
            get
            {
                if (_personPhoneCallRecordPage == null)
                    _personPhoneCallRecordPage = new PersonPhoneCallRecordPage(driver, wait, appURL);
                return _personPhoneCallRecordPage;

            }
        }

        public PersonFormPhoneCallRecordPage personFormPhoneCallRecordPage
        {
            get
            {
                if (_personFormPhoneCallRecordPage == null)
                    _personFormPhoneCallRecordPage = new PersonFormPhoneCallRecordPage(driver, wait, appURL);
                return _personFormPhoneCallRecordPage;

            }
        }

        public PersonFormTasksPage personFormTasksPage
        {
            get
            {
                if (_personFormTasksPage == null)
                    _personFormTasksPage = new PersonFormTasksPage(driver, wait, appURL);
                return _personFormTasksPage;

            }
        }

        public PersonFormTaskRecordPage personFormTaskRecordPage
        {
            get
            {
                if (_personFormTaskRecordPage == null)
                    _personFormTaskRecordPage = new PersonFormTaskRecordPage(driver, wait, appURL);
                return _personFormTaskRecordPage;

            }
        }

        public CaseFormCaseNotesPage caseFormCaseNotesPage
        {
            get
            {
                if (_caseFormCaseNotesPage == null)
                    _caseFormCaseNotesPage = new CaseFormCaseNotesPage(driver, wait, appURL);
                return _caseFormCaseNotesPage;

            }
        }

        public CaseFormCaseNoteRecordPage caseFormCaseNoteRecordPage
        {
            get
            {
                if (_caseFormCaseNoteRecordPage == null)
                    _caseFormCaseNoteRecordPage = new CaseFormCaseNoteRecordPage(driver, wait, appURL);
                return _caseFormCaseNoteRecordPage;

            }
        }

        public CloneActivityPopup cloneActivityPopup
        {
            get
            {
                if (_cloneActivityPopup == null)
                    _cloneActivityPopup = new CloneActivityPopup(driver, wait, appURL);
                return _cloneActivityPopup;

            }
        }

        public TeamSelectingPage teamSelectingPage
        {
            get
            {
                if (_teamSelectingPage == null)
                    _teamSelectingPage = new TeamSelectingPage(driver, wait, appURL);
                return _teamSelectingPage;

            }
        }

        public ChangeDefaultTeamPopup changeDefaultTeamPopup
        {
            get
            {
                if (_changeDefaultTeamPopup == null)
                    _changeDefaultTeamPopup = new ChangeDefaultTeamPopup(driver, wait, appURL);
                return _changeDefaultTeamPopup;

            }
        }

        public CopyToClipboardDynamicDialogPopup copyToClipboardDynamicDialogPopup
        {
            get
            {
                if (_copyToClipboardDynamicDialogPopup == null)
                    _copyToClipboardDynamicDialogPopup = new CopyToClipboardDynamicDialogPopup(driver, wait, appURL);
                return _copyToClipboardDynamicDialogPopup;

            }
        }

        public SecurityProfilesPage securityProfilesPage
        {
            get
            {
                if (_securityProfilesPage == null)
                    _securityProfilesPage = new SecurityProfilesPage(driver, wait, appURL);
                return _securityProfilesPage;

            }
        }

        public SecurityProfileRecordPage securityProfileRecordPage
        {
            get
            {
                if (_securityProfileRecordPage == null)
                    _securityProfileRecordPage = new SecurityProfileRecordPage(driver, wait, appURL);
                return _securityProfileRecordPage;

            }
        }

        public CaseFormAppointmentsPage caseFormAppointmentsPage
        {
            get
            {
                if (_caseFormAppointmentsPage == null)
                    _caseFormAppointmentsPage = new CaseFormAppointmentsPage(driver, wait, appURL);
                return _caseFormAppointmentsPage;

            }
        }

        public CaseFormAppointmentRecordPage caseFormAppointmentRecordPage
        {
            get
            {
                if (_caseFormAppointmentRecordPage == null)
                    _caseFormAppointmentRecordPage = new CaseFormAppointmentRecordPage(driver, wait, appURL);
                return _caseFormAppointmentRecordPage;

            }
        }

        public CaseFormEmailsPage caseFormEmailsPage
        {
            get
            {
                if (_caseFormEmailsPage == null)
                    _caseFormEmailsPage = new CaseFormEmailsPage(driver, wait, appURL);
                return _caseFormEmailsPage;

            }
        }

        public CaseFormEmailRecordPage caseFormEmailRecordPage
        {
            get
            {
                if (_caseFormEmailRecordPage == null)
                    _caseFormEmailRecordPage = new CaseFormEmailRecordPage(driver, wait, appURL);
                return _caseFormEmailRecordPage;

            }
        }

        public CaseFormLettersPage caseFormLettersPage
        {
            get
            {
                if (_caseFormLettersPage == null)
                    _caseFormLettersPage = new CaseFormLettersPage(driver, wait, appURL);
                return _caseFormLettersPage;

            }
        }

        public CaseFormLetterRecordPage caseFormLetterRecordPage
        {
            get
            {
                if (_caseFormLetterRecordPage == null)
                    _caseFormLetterRecordPage = new CaseFormLetterRecordPage(driver, wait, appURL);
                return _caseFormLetterRecordPage;

            }
        }

        public CaseFormPhoneCallsPage caseFormPhoneCallsPage
        {
            get
            {
                if (_caseFormPhoneCallsPage == null)
                    _caseFormPhoneCallsPage = new CaseFormPhoneCallsPage(driver, wait, appURL);
                return _caseFormPhoneCallsPage;

            }
        }

        public CaseFormPhoneCallRecordPage caseFormPhoneCallRecordPage
        {
            get
            {
                if (_caseFormPhoneCallRecordPage == null)
                    _caseFormPhoneCallRecordPage = new CaseFormPhoneCallRecordPage(driver, wait, appURL);
                return _caseFormPhoneCallRecordPage;

            }
        }

        public CaseFormTasksPage caseFormTasksPage
        {
            get
            {
                if (_caseFormTasksPage == null)
                    _caseFormTasksPage = new CaseFormTasksPage(driver, wait, appURL);
                return _caseFormTasksPage;

            }
        }

        public CaseFormTaskRecordPage caseFormTaskRecordPage
        {
            get
            {
                if (_caseFormTaskRecordPage == null)
                    _caseFormTaskRecordPage = new CaseFormTaskRecordPage(driver, wait, appURL);
                return _caseFormTaskRecordPage;

            }
        }

        public SystemManagementPage systemManagementPage
        {
            get
            {
                if (_systemManagementPage == null)
                    _systemManagementPage = new SystemManagementPage(driver, wait, appURL);
                return _systemManagementPage;

            }
        }

        public EDMSRepositoriesPage edmsRepositoriesPage
        {
            get
            {
                if (_edmsRepositoriesPage == null)
                    _edmsRepositoriesPage = new EDMSRepositoriesPage(driver, wait, appURL);
                return _edmsRepositoriesPage;

            }
        }

        public ScheduleJobsPage scheduleJobsPage
        {
            get
            {
                if (_scheduleJobsPage == null)
                    _scheduleJobsPage = new ScheduleJobsPage(driver, wait, appURL);
                return _scheduleJobsPage;

            }
        }

        public ScheduleJobsRecordPage scheduleJobsRecordPage
        {
            get
            {
                if (_scheduleJobsRecordPage == null)
                    _scheduleJobsRecordPage = new ScheduleJobsRecordPage(driver, wait, appURL);
                return _scheduleJobsRecordPage;

            }
        }

        public EDMSRepositoryRecordPage edmsRepositoryRecordPage
        {
            get
            {
                if (_edmsRepositoryRecordPage == null)
                    _edmsRepositoryRecordPage = new EDMSRepositoryRecordPage(driver, wait, appURL);
                return _edmsRepositoryRecordPage;

            }
        }

        public PersonAttachmentsPage personAttachmentsPage
        {
            get
            {
                if (_personAttachmentsPage == null)
                    _personAttachmentsPage = new PersonAttachmentsPage(driver, wait, appURL);
                return _personAttachmentsPage;

            }
        }

        public PersonAttachmentRecordPage personAttachmentRecordPage
        {
            get
            {
                if (_personAttachmentRecordPage == null)
                    _personAttachmentRecordPage = new PersonAttachmentRecordPage(driver, wait, appURL);
                return _personAttachmentRecordPage;

            }
        }

        public CreateBulkAttachmentsPopup createBulkAttachmentsPopup
        {
            get
            {
                if (_createBulkAttachmentsPopup == null)
                    _createBulkAttachmentsPopup = new CreateBulkAttachmentsPopup(driver, wait, appURL);
                return _createBulkAttachmentsPopup;

            }
        }

        public DocumentPrintTemplatesPage documentPrintTemplatesPage
        {
            get
            {
                if (_documentPrintTemplatesPage == null)
                    _documentPrintTemplatesPage = new DocumentPrintTemplatesPage(driver, wait, appURL);
                return _documentPrintTemplatesPage;

            }
        }

        public DocumentPrintTemplateRecordPage documentPrintTemplateRecordPage
        {
            get
            {
                if (_documentPrintTemplateRecordPage == null)
                    _documentPrintTemplateRecordPage = new DocumentPrintTemplateRecordPage(driver, wait, appURL);
                return _documentPrintTemplateRecordPage;

            }
        }

        public WorkflowsPage workflowsPage
        {
            get
            {
                if (_workflowsPage == null)
                    _workflowsPage = new WorkflowsPage(driver, wait, appURL);
                return _workflowsPage;

            }
        }

        public WorkflowRecordPage workflowRecordPage
        {
            get
            {
                if (_workflowRecordPage == null)
                    _workflowRecordPage = new WorkflowRecordPage(driver, wait, appURL);
                return _workflowRecordPage;

            }
        }

        public DuplicateRecordsPage duplicateRecordsPage
        {
            get
            {
                if (_duplicateRecordsPage == null)
                    _duplicateRecordsPage = new DuplicateRecordsPage(driver, wait, appURL);
                return _duplicateRecordsPage;

            }
        }

        public DuplicateRecordRecordPage duplicateRecordRecordPage
        {
            get
            {
                if (_duplicateRecordRecordPage == null)
                    _duplicateRecordRecordPage = new DuplicateRecordRecordPage(driver, wait, appURL);
                return _duplicateRecordRecordPage;

            }
        }

        public DuplicateRecordMergeDialog duplicateRecordMergeDialog
        {
            get
            {
                if (_duplicateRecordMergeDialog == null)
                    _duplicateRecordMergeDialog = new DuplicateRecordMergeDialog(driver, wait, appURL);
                return _duplicateRecordMergeDialog;

            }
        }

        public WorkflowConditionsPopup workflowConditionsPopup
        {
            get
            {
                if (_workflowConditionsPopup == null)
                    _workflowConditionsPopup = new WorkflowConditionsPopup(driver, wait, appURL);
                return _workflowConditionsPopup;

            }
        }

        public WorkflowActionPopup workflowActionPopup
        {
            get
            {
                if (_workflowActionPopup == null)
                    _workflowActionPopup = new WorkflowActionPopup(driver, wait, appURL);
                return _workflowActionPopup;

            }
        }

        public ProviderRecordPage providerRecordPage
        {
            get
            {
                if (_providerRecordPage == null)
                    _providerRecordPage = new ProviderRecordPage(driver, wait, appURL);
                return _providerRecordPage;

            }
        }

        public ProvidersPage providersPage
        {
            get
            {
                if (_providersPage == null)
                    _providersPage = new ProvidersPage(driver, wait, appURL);
                return _providersPage;

            }
        }



        public ProviderWebsiteMessageRecordPage providerWebsiteMessageRecordPage
        {
            get
            {
                if (_providerWebsiteMessageRecordPage == null)
                    _providerWebsiteMessageRecordPage = new ProviderWebsiteMessageRecordPage(driver, wait, appURL);
                return _providerWebsiteMessageRecordPage;

            }
        }

        public ProviderWebsiteMessagesPage providerWebsiteMessagesPage
        {
            get
            {
                if (_providerWebsiteMessagesPage == null)
                    _providerWebsiteMessagesPage = new ProviderWebsiteMessagesPage(driver, wait, appURL);
                return _providerWebsiteMessagesPage;

            }
        }

        public WorkflowJobsPage workflowJobsPage
        {
            get
            {
                if (_workflowJobsPage == null)
                    _workflowJobsPage = new WorkflowJobsPage(driver, wait, appURL);
                return _workflowJobsPage;

            }
        }

        public WorkflowJobRecordPage workflowJobRecordPage
        {
            get
            {
                if (_workflowJobRecordPage == null)
                    _workflowJobRecordPage = new WorkflowJobRecordPage(driver, wait, appURL);
                return _workflowJobRecordPage;

            }
        }

        public WidgetSettingsPopup widgetSettingsPopup
        {
            get
            {
                if (_widgetSettingsPopup == null)
                    _widgetSettingsPopup = new WidgetSettingsPopup(driver, wait, appURL);
                return _widgetSettingsPopup;

            }
        }

        public CaseFormActionsOutcomesPageFrame caseFormActionsOutcomesPageFrame
        {
            get
            {
                if (_caseFormActionsOutcomesPageFrame == null)
                    _caseFormActionsOutcomesPageFrame = new CaseFormActionsOutcomesPageFrame(driver, wait, appURL);
                return _caseFormActionsOutcomesPageFrame;

            }
        }

        public FormActionOutcomePage formActionOutcomePage
        {
            get
            {
                if (_formActionOutcomePage == null)
                    _formActionOutcomePage = new FormActionOutcomePage(driver, wait, appURL);
                return _formActionOutcomePage;

            }
        }

        public CloneWebsitePopup cloneWebsitePopup
        {
            get
            {
                if (_cloneWebsitePopup == null)
                    _cloneWebsitePopup = new CloneWebsitePopup(driver, wait, appURL);
                return _cloneWebsitePopup;

            }
        }

        public PersonFormActionOutcomePage personFormActionOutcomePage
        {
            get
            {
                if (_personFormActionOutcomePage == null)
                    _personFormActionOutcomePage = new PersonFormActionOutcomePage(driver, wait, appURL);
                return _personFormActionOutcomePage;

            }
        }

        public PersonFormActionsOutcomesPageFrame personFormActionsOutcomesPageFrame
        {
            get
            {
                if (_personFormActionsOutcomesPageFrame == null)
                    _personFormActionsOutcomesPageFrame = new PersonFormActionsOutcomesPageFrame(driver, wait, appURL);
                return _personFormActionsOutcomesPageFrame;

            }
        }

        public SDEMapListPopup sdeMapListPopup
        {
            get
            {
                if (_sdeMapListPopup == null)
                    _sdeMapListPopup = new SDEMapListPopup(driver, wait, appURL);
                return _sdeMapListPopup;

            }
        }

        public AddressPropertyTypesPage addressPropertyTypesPage
        {
            get
            {
                if (_addressPropertyTypesPage == null)
                    _addressPropertyTypesPage = new AddressPropertyTypesPage(driver, wait, appURL);
                return _addressPropertyTypesPage;

            }
        }

        public ReportableEventInjuritySeveritiesPage reportableEventInjuritySeveritiesPage
        {
            get
            {
                if (_reportableEventInjuritySeveritiesPage == null)
                    _reportableEventInjuritySeveritiesPage = new ReportableEventInjuritySeveritiesPage(driver, wait, appURL);
                return _reportableEventInjuritySeveritiesPage;

            }
        }

        public ReportableEventRolesPage reportableEventRolesPage
        {
            get
            {
                if (_reportableEventRolesPage == null)
                    _reportableEventRolesPage = new ReportableEventRolesPage(driver, wait, appURL);
                return _reportableEventRolesPage;

            }
        }

        public ReportableEventSubCategoriesPage reportableEventSubcategoriesPage
        {
            get
            {
                if (_reportableEventSubCategoriesPage == null)
                    _reportableEventSubCategoriesPage = new ReportableEventSubCategoriesPage(driver, wait, appURL);
                return _reportableEventSubCategoriesPage;

            }
        }

        public ReportableEventInjuritySeveritiesRecordPage reportableEventInjuritySeveritiesRecordPage
        {
            get
            {
                if (_reportableEventInjuritySeveritiesRecordPage == null)
                    _reportableEventInjuritySeveritiesRecordPage = new ReportableEventInjuritySeveritiesRecordPage(driver, wait, appURL);
                return _reportableEventInjuritySeveritiesRecordPage;

            }
        }

        public ReportableEventRolesRecordPage reportableEventRolesRecordPage
        {
            get
            {
                if (_reportableEventRolesRecordPage == null)
                    _reportableEventRolesRecordPage = new ReportableEventRolesRecordPage(driver, wait, appURL);
                return _reportableEventRolesRecordPage;

            }
        }

        public ReportableEventSubCategoriesRecordPage reportableEventSubcategoriesRecordPage
        {
            get
            {
                if (_reportableEventSubCategoriesRecordPage == null)
                    _reportableEventSubCategoriesRecordPage = new ReportableEventSubCategoriesRecordPage(driver, wait, appURL);
                return _reportableEventSubCategoriesRecordPage;

            }
        }


        public WorkflowSendEmailPropertiesPage workflowSendEmailPropertiesPage
        {
            get
            {
                if (_workflowSendEmailPropertiesPage == null)
                    _workflowSendEmailPropertiesPage = new WorkflowSendEmailPropertiesPage(driver, wait, appURL);
                return _workflowSendEmailPropertiesPage;

            }
        }

        public CaseFormAttachmentsPage caseFormAttachmentsPage
        {
            get
            {
                if (_caseFormAttachmentsPage == null)
                    _caseFormAttachmentsPage = new CaseFormAttachmentsPage(driver, wait, appURL);
                return _caseFormAttachmentsPage;

            }
        }

        public CaseFormAttachmentRecordPage caseFormAttachmentRecordPage
        {
            get
            {
                if (_caseFormAttachmentRecordPage == null)
                    _caseFormAttachmentRecordPage = new CaseFormAttachmentRecordPage(driver, wait, appURL);
                return _caseFormAttachmentRecordPage;

            }
        }

        public PotentialDuplicatesPopup potentialDuplicatesPopup
        {
            get
            {
                if (_potentialDuplicatesPopup == null)
                    _potentialDuplicatesPopup = new PotentialDuplicatesPopup(driver, wait, appURL);
                return _potentialDuplicatesPopup;

            }
        }

        public PersonSearchPage personSearchPage
        {
            get
            {
                if (_personSearchPage == null)
                    _personSearchPage = new PersonSearchPage(driver, wait, appURL);
                return _personSearchPage;

            }
        }

        public AutomationPersonForm1EditAssessmentPage automationPersonForm1EditAssessmentPage
        {
            get
            {
                if (_automationPersonForm1EditAssessmentPage == null)
                    _automationPersonForm1EditAssessmentPage = new AutomationPersonForm1EditAssessmentPage(driver, wait, appURL);
                return _automationPersonForm1EditAssessmentPage;

            }
        }

        public ProviderFormsPage providerFormsPage
        {
            get
            {
                if (_providerFormsPage == null)
                    _providerFormsPage = new ProviderFormsPage(driver, wait, appURL);
                return _providerFormsPage;

            }
        }

        public ProviderFormRecordPage providerFormRecordPage
        {
            get
            {
                if (_providerFormRecordPage == null)
                    _providerFormRecordPage = new ProviderFormRecordPage(driver, wait, appURL);
                return _providerFormRecordPage;

            }
        }

        public AutomationProviderForm1EditAssessmentPage automationProviderForm1EditAssessmentPage
        {
            get
            {
                if (_automationProviderForm1EditAssessmentPage == null)
                    _automationProviderForm1EditAssessmentPage = new AutomationProviderForm1EditAssessmentPage(driver, wait, appURL);
                return _automationProviderForm1EditAssessmentPage;

            }
        }

        public PersonMASHEpisodesPage personMASHEpisodesPage
        {
            get
            {
                if (_personMASHEpisodesPage == null)
                    _personMASHEpisodesPage = new PersonMASHEpisodesPage(driver, wait, appURL);
                return _personMASHEpisodesPage;

            }
        }

        public PersonMASHEpisodeRecordPage personMASHEpisodeRecordPage
        {
            get
            {
                if (_personMASHEpisodeRecordPage == null)
                    _personMASHEpisodeRecordPage = new PersonMASHEpisodeRecordPage(driver, wait, appURL);
                return _personMASHEpisodeRecordPage;

            }
        }

        public PersonMASHEpisodeFormsPage personMASHEpisodeFormsPage
        {
            get
            {
                if (_personMASHEpisodeFormsPage == null)
                    _personMASHEpisodeFormsPage = new PersonMASHEpisodeFormsPage(driver, wait, appURL);
                return _personMASHEpisodeFormsPage;

            }
        }

        public PersonMASHEpisodeFormRecordPage personMASHEpisodeFormRecordPage
        {
            get
            {
                if (_personMASHEpisodeFormRecordPage == null)
                    _personMASHEpisodeFormRecordPage = new PersonMASHEpisodeFormRecordPage(driver, wait, appURL);
                return _personMASHEpisodeFormRecordPage;

            }
        }

        public AutomationMASHEpisodeForm1EditAssessmentPage automationMASHEpisodeForm1EditAssessmentPage
        {
            get
            {
                if (_automationMASHEpisodeForm1EditAssessmentPage == null)
                    _automationMASHEpisodeForm1EditAssessmentPage = new AutomationMASHEpisodeForm1EditAssessmentPage(driver, wait, appURL);
                return _automationMASHEpisodeForm1EditAssessmentPage;

            }
        }

        public PersonLettersPage personLettersPage
        {
            get
            {
                if (_personLettersPage == null)
                    _personLettersPage = new PersonLettersPage(driver, wait, appURL);
                return _personLettersPage;

            }
        }

        public PersonLetterRecordPage personLetterRecordPage
        {
            get
            {
                if (_personLetterRecordPage == null)
                    _personLetterRecordPage = new PersonLetterRecordPage(driver, wait, appURL);
                return _personLetterRecordPage;

            }
        }

        public PersonTasksPage personTasksPage
        {
            get
            {
                if (_personTasksPage == null)
                    _personTasksPage = new PersonTasksPage(driver, wait, appURL);
                return _personTasksPage;

            }
        }

        public PersonTaskRecordPage personTaskRecordPage
        {
            get
            {
                if (_personTaskRecordPage == null)
                    _personTaskRecordPage = new PersonTaskRecordPage(driver, wait, appURL);
                return _personTaskRecordPage;

            }
        }

        public PersonClinicalRiskStatusesPage personClinicalRiskStatusesPage
        {
            get
            {
                if (_personClinicalRiskStatusesPage == null)
                    _personClinicalRiskStatusesPage = new PersonClinicalRiskStatusesPage(driver, wait, appURL);
                return _personClinicalRiskStatusesPage;

            }
        }

        public PersonClinicalRiskStatusRecordPage personClinicalRiskStatusRecordPage
        {
            get
            {
                if (_personClinicalRiskStatusRecordPage == null)
                    _personClinicalRiskStatusRecordPage = new PersonClinicalRiskStatusRecordPage(driver, wait, appURL);
                return _personClinicalRiskStatusRecordPage;

            }
        }

        public PersonClinicalRiskStatusCaseNotesPage personClinicalRiskStatusCaseNotesPage
        {
            get
            {
                if (_personClinicalRiskStatusCaseNotesPage == null)
                    _personClinicalRiskStatusCaseNotesPage = new PersonClinicalRiskStatusCaseNotesPage(driver, wait, appURL);
                return _personClinicalRiskStatusCaseNotesPage;

            }
        }

        public PersonClinicalRiskStatusCaseNoteRecordPage personClinicalRiskStatusCaseNoteRecordPage
        {
            get
            {
                if (_personClinicalRiskStatusCaseNoteRecordPage == null)
                    _personClinicalRiskStatusCaseNoteRecordPage = new PersonClinicalRiskStatusCaseNoteRecordPage(driver, wait, appURL);
                return _personClinicalRiskStatusCaseNoteRecordPage;

            }
        }

        public PersonCarePlanRecordPage personCarePlanRecordPage
        {
            get
            {
                if (_personCarePlanRecordPage == null)
                    _personCarePlanRecordPage = new PersonCarePlanRecordPage(driver, wait, appURL);
                return _personCarePlanRecordPage;

            }
        }

        public PersonCarePlanNeedRecordPage personCarePlanNeedRecordPage
        {
            get
            {
                if (_personCarePlanNeedRecordPage == null)
                    _personCarePlanNeedRecordPage = new PersonCarePlanNeedRecordPage(driver, wait, appURL);
                return _personCarePlanNeedRecordPage;

            }
        }

        public PersonCarePlanFormInitialAssessmentRecordPage personCarePlanFormInitialAssessmentRecordPage
        {
            get
            {
                if (_personCarePlanFormInitialAssessmentRecordPage == null)
                    _personCarePlanFormInitialAssessmentRecordPage = new PersonCarePlanFormInitialAssessmentRecordPage(driver, wait, appURL);
                return _personCarePlanFormInitialAssessmentRecordPage;

            }
        }

        public PersonCarePlanFormRecordPage personCarePlanFormRecordPage
        {
            get
            {
                if (_personCarePlanFormRecordPage == null)
                    _personCarePlanFormRecordPage = new PersonCarePlanFormRecordPage(driver, wait, appURL);
                return _personCarePlanFormRecordPage;

            }
        }

        public PersonCarePlanNeedGoalsROutcomePage personCarePlanNeedGoalsROutcomePage
        {
            get
            {
                if (_personCarePlanNeedGoalsROutcomePage == null)
                    _personCarePlanNeedGoalsROutcomePage = new PersonCarePlanNeedGoalsROutcomePage(driver, wait, appURL);
                return _personCarePlanNeedGoalsROutcomePage;

            }
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage personCarePlanNeedGoalsROutcomeRecordPage
        {
            get
            {
                if (_personCarePlanNeedGoalsROutcomeRecordPage == null)
                    _personCarePlanNeedGoalsROutcomeRecordPage = new PersonCarePlanNeedGoalsROutcomeRecordPage(driver, wait, appURL);
                return _personCarePlanNeedGoalsROutcomeRecordPage;

            }
        }

        public PersonCarePlanInterventionRecordPage personCarePlanInterventionsRecordPage
        {
            get
            {
                if (_personCarePlanInterventionsRecordPage == null)
                    _personCarePlanInterventionsRecordPage = new PersonCarePlanInterventionRecordPage(driver, wait, appURL);
                return _personCarePlanInterventionsRecordPage;

            }
        }

        public PersonCarePlanInterventionRecordAddPage personCarePlanInterventionsRecordAddPage
        {
            get
            {
                if (_personCarePlanInterventionsRecordAddPage == null)
                    _personCarePlanInterventionsRecordAddPage = new PersonCarePlanInterventionRecordAddPage(driver, wait, appURL);
                return _personCarePlanInterventionsRecordAddPage;

            }
        }
        public PersonCarePlanCaseNotesPage personCarePlanCaseNotesPage
        {
            get
            {
                if (_personCarePlanCaseNotesPage == null)
                    _personCarePlanCaseNotesPage = new PersonCarePlanCaseNotesPage(driver, wait, appURL);
                return _personCarePlanCaseNotesPage;

            }
        }

        public PersonCarePlanCaseNoteRecordPage personCarePlanCaseNoteRecordPage
        {
            get
            {
                if (_personCarePlanCaseNoteRecordPage == null)
                    _personCarePlanCaseNoteRecordPage = new PersonCarePlanCaseNoteRecordPage(driver, wait, appURL);
                return _personCarePlanCaseNoteRecordPage;

            }
        }

        public CaseHealthAppointmentsPage caseHealthAppointmentsPage
        {
            get
            {
                if (_caseHealthAppointmentsPage == null)
                    _caseHealthAppointmentsPage = new CaseHealthAppointmentsPage(driver, wait, appURL);
                return _caseHealthAppointmentsPage;

            }
        }

        public CaseHealthAppointmentRecordPage caseHealthAppointmentRecordPage
        {
            get
            {
                if (_caseHealthAppointmentRecordPage == null)
                    _caseHealthAppointmentRecordPage = new CaseHealthAppointmentRecordPage(driver, wait, appURL);
                return _caseHealthAppointmentRecordPage;

            }
        }

        public HealthAppointmentCaseNotesPage healthAppointmentCaseNotesPage
        {
            get
            {
                if (_healthAppointmentCaseNotesPage == null)
                    _healthAppointmentCaseNotesPage = new HealthAppointmentCaseNotesPage(driver, wait, appURL);
                return _healthAppointmentCaseNotesPage;

            }
        }

        public HealthAppointmentCaseNoteRecordPage healthAppointmentCaseNoteRecordPage
        {
            get
            {
                if (_healthAppointmentCaseNoteRecordPage == null)
                    _healthAppointmentCaseNoteRecordPage = new HealthAppointmentCaseNoteRecordPage(driver, wait, appURL);
                return _healthAppointmentCaseNoteRecordPage;

            }
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage communityClinicAdditionalHealthProfessionalRecordPage
        {
            get
            {
                if (_communityClinicAdditionalHealthProfessionalRecordPage == null)
                    _communityClinicAdditionalHealthProfessionalRecordPage = new CommunityClinicAdditionalHealthProfessionalRecordPage(driver, wait, appURL);
                return _communityClinicAdditionalHealthProfessionalRecordPage;

            }
        }

        public PersonHeightAndWeightCaseNoteRecordPage personHeightAndWeightCaseNoteRecordPage
        {
            get
            {
                if (_personHeightAndWeightCaseNoteRecordPage == null)
                    _personHeightAndWeightCaseNoteRecordPage = new PersonHeightAndWeightCaseNoteRecordPage(driver, wait, appURL);
                return _personHeightAndWeightCaseNoteRecordPage;

            }
        }

        public PersonHeightAndWeightCaseNotesPage personHeightAndWeightCaseNotesPage
        {
            get
            {
                if (_personHeightAndWeightCaseNotesPage == null)
                    _personHeightAndWeightCaseNotesPage = new PersonHeightAndWeightCaseNotesPage(driver, wait, appURL);
                return _personHeightAndWeightCaseNotesPage;

            }
        }

        public PersonHeightWeighObservationRecordPage personHeightWeighObservationRecordPage
        {
            get
            {
                if (_personHeightWeighObservationRecordPage == null)
                    _personHeightWeighObservationRecordPage = new PersonHeightWeighObservationRecordPage(driver, wait, appURL);
                return _personHeightWeighObservationRecordPage;

            }
        }

        public PersonHeightWeightObservationsPage personHeightWeightObservationsPage
        {
            get
            {
                if (_personHeightWeightObservationsPage == null)
                    _personHeightWeightObservationsPage = new PersonHeightWeightObservationsPage(driver, wait, appURL);
                return _personHeightWeightObservationsPage;

            }
        }

        public PersonMHALegalStatusesPage personMHALegalStatusesPage
        {
            get
            {
                if (_personMHALegalStatusesPage == null)
                    _personMHALegalStatusesPage = new PersonMHALegalStatusesPage(driver, wait, appURL);
                return _personMHALegalStatusesPage;

            }
        }

        public PersonMHALegalStatusRecordPage personMHALegalStatusRecordPage
        {
            get
            {
                if (_personMHALegalStatusRecordPage == null)
                    _personMHALegalStatusRecordPage = new PersonMHALegalStatusRecordPage(driver, wait, appURL);
                return _personMHALegalStatusRecordPage;

            }
        }

        public RightsAndRequestsForAnIMHAAndMHAAppealPage rightsAndRequestsForAnIMHAAndMHAAppealPage
        {
            get
            {
                if (_rightsAndRequestsForAnIMHAAndMHAAppealPage == null)
                    _rightsAndRequestsForAnIMHAAndMHAAppealPage = new RightsAndRequestsForAnIMHAAndMHAAppealPage(driver, wait, appURL);
                return _rightsAndRequestsForAnIMHAAndMHAAppealPage;

            }
        }

        public RightsAndRequestForAnIMHAAndMHAAppealRecordPage rightsAndRequestForAnIMHAAndMHAAppealRecordPage
        {
            get
            {
                if (_rightsAndRequestForAnIMHAAndMHAAppealRecordPage == null)
                    _rightsAndRequestForAnIMHAAndMHAAppealRecordPage = new RightsAndRequestForAnIMHAAndMHAAppealRecordPage(driver, wait, appURL);
                return _rightsAndRequestForAnIMHAAndMHAAppealRecordPage;

            }
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage
        {
            get
            {
                if (_rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage == null)
                    _rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage = new RightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage(driver, wait, appURL);
                return _rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage;

            }
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage
        {
            get
            {
                if (_rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage == null)
                    _rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage = new RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage(driver, wait, appURL);
                return _rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage;

            }
        }

        public RecordsOfAppealPage recordsOfAppealPage
        {
            get
            {
                if (_recordsOfAppealPage == null)
                    _recordsOfAppealPage = new RecordsOfAppealPage(driver, wait, appURL);
                return _recordsOfAppealPage;

            }
        }

        public RecordOfAppealRecordPage recordOfAppealRecordPage
        {
            get
            {
                if (_recordOfAppealRecordPage == null)
                    _recordOfAppealRecordPage = new RecordOfAppealRecordPage(driver, wait, appURL);
                return _recordOfAppealRecordPage;

            }
        }

        public RecordOfAppealCaseNotesPage recordOfAppealCaseNotesPage
        {
            get
            {
                if (_recordOfAppealCaseNotesPage == null)
                    _recordOfAppealCaseNotesPage = new RecordOfAppealCaseNotesPage(driver, wait, appURL);
                return _recordOfAppealCaseNotesPage;

            }
        }

        public RecordOfAppealCaseNoteRecordPage recordOfAppealCaseNoteRecordPage
        {
            get
            {
                if (_recordOfAppealCaseNoteRecordPage == null)
                    _recordOfAppealCaseNoteRecordPage = new RecordOfAppealCaseNoteRecordPage(driver, wait, appURL);
                return _recordOfAppealCaseNoteRecordPage;

            }
        }

        public PersonMHALegalStatusCaseNotesPage personMHALegalStatusCaseNotesPage
        {
            get
            {
                if (_personMHALegalStatusCaseNotesPage == null)
                    _personMHALegalStatusCaseNotesPage = new PersonMHALegalStatusCaseNotesPage(driver, wait, appURL);
                return _personMHALegalStatusCaseNotesPage;

            }
        }

        public PersonPhysicalObservationCaseNotesPage personPhysicalObservationCaseNotesPage
        {
            get
            {
                if (_personPhysicalObservationCaseNotesPage == null)
                    _personPhysicalObservationCaseNotesPage = new PersonPhysicalObservationCaseNotesPage(driver, wait, appURL);
                return _personPhysicalObservationCaseNotesPage;

            }
        }

        public PersonFinancialAssessmentCaseNoteRecordPage personFinancialAssessmentCaseNoteRecordPage
        {
            get
            {
                if (_personFinacialAssessmentCaseNoteRecordPage == null)
                    _personFinacialAssessmentCaseNoteRecordPage = new PersonFinancialAssessmentCaseNoteRecordPage(driver, wait, appURL);
                return _personFinacialAssessmentCaseNoteRecordPage;

            }
        }

        public PersonFinancialAssessmentCaseNotesPage personFinacialAssessmentCaseNotesPage
        {
            get
            {
                if (_personFinacialAssessmentCaseNotesPage == null)
                    _personFinacialAssessmentCaseNotesPage = new PersonFinancialAssessmentCaseNotesPage(driver, wait, appURL);
                return _personFinacialAssessmentCaseNotesPage;

            }
        }

        public PersonMHALegalStatusCaseNoteRecordPage personMHALegalStatusCaseNoteRecordPage
        {
            get
            {
                if (_personMHALegalStatusCaseNoteRecordPage == null)
                    _personMHALegalStatusCaseNoteRecordPage = new PersonMHALegalStatusCaseNoteRecordPage(driver, wait, appURL);
                return _personMHALegalStatusCaseNoteRecordPage;

            }
        }

        public PersonPhysicalObservationCaseNoteRecordPage personPhysicalObservationCaseNoteRecordPage
        {
            get
            {
                if (_personPhysicalObservationCaseNoteRecordPage == null)
                    _personPhysicalObservationCaseNoteRecordPage = new PersonPhysicalObservationCaseNoteRecordPage(driver, wait, appURL);
                return _personPhysicalObservationCaseNoteRecordPage;

            }
        }

        public CaseCaseNotesPage caseCaseNotesPage
        {
            get
            {
                if (_caseCaseNotesPage == null)
                    _caseCaseNotesPage = new CaseCaseNotesPage(driver, wait, appURL);
                return _caseCaseNotesPage;

            }
        }

        public CaseCaseNoteRecordPage caseCaseNoteRecordPage
        {
            get
            {
                if (_caseCaseNoteRecordPage == null)
                    _caseCaseNoteRecordPage = new CaseCaseNoteRecordPage(driver, wait, appURL);
                return _caseCaseNoteRecordPage;

            }
        }

        public CourtDatesAndOutcomesPage courtDatesAndOutcomesPage
        {
            get
            {
                if (_courtDatesAndOutcomesPage == null)
                    _courtDatesAndOutcomesPage = new CourtDatesAndOutcomesPage(driver, wait, appURL);
                return _courtDatesAndOutcomesPage;

            }
        }

        public CourtDatesAndOutcomeRecordPage courtDatesAndOutcomeRecordPage
        {
            get
            {
                if (_courtDatesAndOutcomeRecordPage == null)
                    _courtDatesAndOutcomeRecordPage = new CourtDatesAndOutcomeRecordPage(driver, wait, appURL);
                return _courtDatesAndOutcomeRecordPage;

            }
        }

        public CourtDatesAndOutcomesCaseNotesPage courtDatesAndOutcomesCaseNotesPage
        {
            get
            {
                if (_courtDatesAndOutcomesCaseNotesPage == null)
                    _courtDatesAndOutcomesCaseNotesPage = new CourtDatesAndOutcomesCaseNotesPage(driver, wait, appURL);
                return _courtDatesAndOutcomesCaseNotesPage;

            }
        }

        public CourtDatesAndOutcomesCaseNoteRecordPage courtDatesAndOutcomesCaseNoteRecordPage
        {
            get
            {
                if (_courtDatesAndOutcomesCaseNoteRecordPage == null)
                    _courtDatesAndOutcomesCaseNoteRecordPage = new CourtDatesAndOutcomesCaseNoteRecordPage(driver, wait, appURL);
                return _courtDatesAndOutcomesCaseNoteRecordPage;

            }
        }

        public Section117EntitlementsPage Section117EntitlementsPage
        {
            get
            {
                if (_Section117EntitlementsPage == null)
                    _Section117EntitlementsPage = new Section117EntitlementsPage(driver, wait, appURL);
                return _Section117EntitlementsPage;

            }
        }

        public Section117EntitlementRecordPage Section117EntitlementRecordPage
        {
            get
            {
                if (_Section117EntitlementRecordPage == null)
                    _Section117EntitlementRecordPage = new Section117EntitlementRecordPage(driver, wait, appURL);
                return _Section117EntitlementRecordPage;

            }
        }

        public Section117EntitlementCaseNotesPage Section117EntitlementCaseNotesPage
        {
            get
            {
                if (_Section117EntitlementCaseNotesPage == null)
                    _Section117EntitlementCaseNotesPage = new Section117EntitlementCaseNotesPage(driver, wait, appURL);
                return _Section117EntitlementCaseNotesPage;

            }
        }

        public Sectio117EntitlementCaseNoteRecordPage Sectio117EntitlementCaseNoteRecordPage
        {
            get
            {
                if (_Sectio117EntitlementCaseNoteRecordPage == null)
                    _Sectio117EntitlementCaseNoteRecordPage = new Sectio117EntitlementCaseNoteRecordPage(driver, wait, appURL);
                return _Sectio117EntitlementCaseNoteRecordPage;

            }
        }

        public LeavesAWOLPage leavesAWOLPage
        {
            get
            {
                if (_leavesAWOLPage == null)
                    _leavesAWOLPage = new LeavesAWOLPage(driver, wait, appURL);
                return _leavesAWOLPage;

            }
        }

        public LeaveAWOLRecordPage leaveAWOLRecordPage
        {
            get
            {
                if (_leaveAWOLRecordPage == null)
                    _leaveAWOLRecordPage = new LeaveAWOLRecordPage(driver, wait, appURL);
                return _leaveAWOLRecordPage;

            }
        }

        public InpatientLeaveAwolCaseNotesPage inpatientLeaveAwolCaseNotesPage
        {
            get
            {
                if (_inpatientLeaveAwolCaseNotesPage == null)
                    _inpatientLeaveAwolCaseNotesPage = new InpatientLeaveAwolCaseNotesPage(driver, wait, appURL);
                return _inpatientLeaveAwolCaseNotesPage;

            }
        }

        public InpatientLeaveAwolCaseNoteRecordPage inpatientLeaveAwolCaseNoteRecordPage
        {
            get
            {
                if (_inpatientLeaveAwolCaseNoteRecordPage == null)
                    _inpatientLeaveAwolCaseNoteRecordPage = new InpatientLeaveAwolCaseNoteRecordPage(driver, wait, appURL);
                return _inpatientLeaveAwolCaseNoteRecordPage;

            }
        }

        public PersonContactsPage personContactsPage
        {
            get
            {
                if (_personContactsPage == null)
                    _personContactsPage = new PersonContactsPage(driver, wait, appURL);
                return _personContactsPage;

            }
        }

        public PersonContactRecordPage personContactRecordPage
        {
            get
            {
                if (_personContactRecordPage == null)
                    _personContactRecordPage = new PersonContactRecordPage(driver, wait, appURL);
                return _personContactRecordPage;

            }
        }

        public PersonContactCaseNotesPage personContactCaseNotesPage
        {
            get
            {
                if (_personContactCaseNotesPage == null)
                    _personContactCaseNotesPage = new PersonContactCaseNotesPage(driver, wait, appURL);
                return _personContactCaseNotesPage;

            }
        }

        public PersonContactCaseNoteRecordPage personContactCaseNoteRecordPage
        {
            get
            {
                if (_personContactCaseNoteRecordPage == null)
                    _personContactCaseNoteRecordPage = new PersonContactCaseNoteRecordPage(driver, wait, appURL);
                return _personContactCaseNoteRecordPage;

            }
        }

        public CaseTasksPage caseTasksPage
        {
            get
            {
                if (_caseTasksPage == null)
                    _caseTasksPage = new CaseTasksPage(driver, wait, appURL);
                return _caseTasksPage;

            }
        }

        public CaseTaskRecordPage caseTaskRecordPage
        {
            get
            {
                if (_caseTaskRecordPage == null)
                    _caseTaskRecordPage = new CaseTaskRecordPage(driver, wait, appURL);
                return _caseTaskRecordPage;

            }
        }

        public CloneAttachmentsPopup cloneAttachmentsPopup
        {
            get
            {
                if (_cloneAttachmentsPopup == null)
                    _cloneAttachmentsPopup = new CloneAttachmentsPopup(driver, wait, appURL);
                return _cloneAttachmentsPopup;

            }
        }

        public DrawingCanvasPopup drawingCanvasPopup
        {
            get
            {
                if (_drawingCanvasPopup == null)
                    _drawingCanvasPopup = new DrawingCanvasPopup(driver, wait, appURL);
                return _drawingCanvasPopup;

            }
        }

        public PersonAppointmentsPage personAppointmentsPage
        {
            get
            {
                if (_personAppointmentsPage == null)
                    _personAppointmentsPage = new PersonAppointmentsPage(driver, wait, appURL);
                return _personAppointmentsPage;

            }
        }

        public PersonAppointmentRecordPage personAppointmentRecordPage
        {
            get
            {
                if (_personAppointmentRecordPage == null)
                    _personAppointmentRecordPage = new PersonAppointmentRecordPage(driver, wait, appURL);
                return _personAppointmentRecordPage;

            }
        }

        public ExportDataPopup exportDataPopup
        {
            get
            {
                if (_exportDataPopup == null)
                    _exportDataPopup = new ExportDataPopup(driver, wait, appURL);
                return _exportDataPopup;

            }
        }

        public PersonPrimarySupportReasonPage personPrimarySupportReasonPage
        {
            get
            {
                if (_personPrimarySupportReasonPage == null)
                    _personPrimarySupportReasonPage = new PersonPrimarySupportReasonPage(driver, wait, appURL);
                return _personPrimarySupportReasonPage;

            }
        }

        public PersonPrimarySupportReasonRecordPage personPrimarySupportReasonRecordPage
        {
            get
            {
                if (_personPrimarySupportReasonRecordPage == null)
                    _personPrimarySupportReasonRecordPage = new PersonPrimarySupportReasonRecordPage(driver, wait, appURL);
                return _personPrimarySupportReasonRecordPage;

            }
        }

        public AssignRecordPopup assignRecordPopup
        {
            get
            {
                if (_assignRecordPopup == null)
                    _assignRecordPopup = new AssignRecordPopup(driver, wait, appURL);
                return _assignRecordPopup;

            }
        }

        public PersonAlertandHazardsPage personAlertAndHazardsPage
        {
            get
            {
                if (_personAlertAndHazardsPage == null)
                    _personAlertAndHazardsPage = new PersonAlertandHazardsPage(driver, wait, appURL);
                return _personAlertAndHazardsPage;

            }
        }

        public PersonAlertandHazardsRecordPage personAlertAndHazardsRecordPage
        {
            get
            {
                if (_personAlertAndHazardsRecordPage == null)
                    _personAlertAndHazardsRecordPage = new PersonAlertandHazardsRecordPage(driver, wait, appURL);
                return _personAlertAndHazardsRecordPage;

            }
        }

        public RestrictPersonAlertAndHazardPopup restrictPersonAlertAndHazardPopup
        {
            get
            {
                if (_restrictPersonAlertAndHazardPopup == null)
                    _restrictPersonAlertAndHazardPopup = new RestrictPersonAlertAndHazardPopup(driver, wait, appURL);
                return _restrictPersonAlertAndHazardPopup;

            }
        }

        public PersonAlertAndHazardReviewRecordPage personAlertAndHazardReviewRecordPage
        {
            get
            {
                if (_personAlertAndHazardReviewRecordPage == null)
                    _personAlertAndHazardReviewRecordPage = new PersonAlertAndHazardReviewRecordPage(driver, wait, appURL);
                return _personAlertAndHazardReviewRecordPage;

            }
        }

        public ModulesObjectPage modulesObjectPage
        {
            get
            {
                if (_modulesObjectPage == null)
                    _modulesObjectPage = new ModulesObjectPage(driver, wait, appURL);
                return _modulesObjectPage;

            }
        }

        public ModulesObjectRecordPage modulesObjectRecordPage
        {
            get
            {
                if (_modulesObjectRecordPage == null)
                    _modulesObjectRecordPage = new ModulesObjectRecordPage(driver, wait, appURL);
                return _modulesObjectRecordPage;

            }
        }

        public PersonSignificantEvent personSignificantEvent
        {
            get
            {
                if (_personSignificantEvent == null)
                    _personSignificantEvent = new PersonSignificantEvent(driver, wait, appURL);
                return _personSignificantEvent;

            }
        }

        public PersonPostAdoptionLinksPage personPostAdoptionLinksPage
        {
            get
            {
                if (_personPostAdoptionLinksPage == null)
                    _personPostAdoptionLinksPage = new PersonPostAdoptionLinksPage(driver, wait, appURL);
                return _personPostAdoptionLinksPage;

            }
        }

        public PersonPostAdoptionLinkRecordPage personPostAdoptionLinkRecordPage
        {
            get
            {
                if (_personPostAdoptionLinkRecordPage == null)
                    _personPostAdoptionLinkRecordPage = new PersonPostAdoptionLinkRecordPage(driver, wait, appURL);
                return _personPostAdoptionLinkRecordPage;

            }
        }

        public PersonPreAdoptionLinksPage personPreAdoptionLinksPage
        {
            get
            {
                if (_personPreAdoptionLinksPage == null)
                    _personPreAdoptionLinksPage = new PersonPreAdoptionLinksPage(driver, wait, appURL);
                return _personPreAdoptionLinksPage;

            }
        }

        public PersonPreAdoptionLinkRecordPage personPreAdoptionLinkRecordPage
        {
            get
            {
                if (_personPreAdoptionLinkRecordPage == null)
                    _personPreAdoptionLinkRecordPage = new PersonPreAdoptionLinkRecordPage(driver, wait, appURL);
                return _personPreAdoptionLinkRecordPage;

            }
        }

        public PersonAlertAndHazardReviewPage personAlertAndHazardReviewPage
        {
            get
            {
                if (_personAlertAndHazardReviewPage == null)
                    _personAlertAndHazardReviewPage = new PersonAlertAndHazardReviewPage(driver, wait, appURL);
                return _personAlertAndHazardReviewPage;

            }
        }

        public PersonClinicalRiskFactorsPage personClinicalRiskFactorsPage
        {
            get
            {
                if (_personClinicalRiskFactorsPage == null)
                    _personClinicalRiskFactorsPage = new PersonClinicalRiskFactorsPage(driver, wait, appURL);
                return _personClinicalRiskFactorsPage;

            }
        }

        public PersonClinicalRiskFactorRecordPage personClinicalRiskFactorRecordPage
        {
            get
            {
                if (_personClinicalRiskFactorRecordPage == null)
                    _personClinicalRiskFactorRecordPage = new PersonClinicalRiskFactorRecordPage(driver, wait, appURL);
                return _personClinicalRiskFactorRecordPage;

            }
        }

        public PersonClinicalRiskFactorHistoryPage personClinicalRiskFactorHistoryPage
        {
            get
            {
                if (_personClinicalRiskFactorHistoryPage == null)
                    _personClinicalRiskFactorHistoryPage = new PersonClinicalRiskFactorHistoryPage(driver, wait, appURL);
                return _personClinicalRiskFactorHistoryPage;

            }
        }

        public PersonClinicalRiskFactorHistoryRecordPage personClinicalRiskFactorHistoryRecordPage
        {
            get
            {
                if (_personClinicalRiskFactorHistoryRecordPage == null)
                    _personClinicalRiskFactorHistoryRecordPage = new PersonClinicalRiskFactorHistoryRecordPage(driver, wait, appURL);
                return _personClinicalRiskFactorHistoryRecordPage;

            }
        }

        public PersonHealthProfessionalsRecordPage personHealthProfessionalsRecordPage
        {
            get
            {
                if (_personHealthProfessionalsRecordPage == null)
                    _personHealthProfessionalsRecordPage = new PersonHealthProfessionalsRecordPage(driver, wait, appURL);
                return _personHealthProfessionalsRecordPage;

            }
        }

        public PersonHealthProfessionalsPage personHealthProfessionalsPage
        {
            get
            {
                if (_personHealthProfessionalsPage == null)
                    _personHealthProfessionalsPage = new PersonHealthProfessionalsPage(driver, wait, appURL);
                return _personHealthProfessionalsPage;

            }
        }

        public PersonHealthDetailsPage personHealthDetailPage
        {
            get
            {
                if (_personHealthDetailPage == null)
                    _personHealthDetailPage = new PersonHealthDetailsPage(driver, wait, appURL);
                return _personHealthDetailPage;

            }
        }

        public PersonHealthDetailRecordPage personHealthDetailsRecordPage
        {
            get
            {
                if (_personHealthDetailsRecordPage == null)
                    _personHealthDetailsRecordPage = new PersonHealthDetailRecordPage(driver, wait, appURL);
                return _personHealthDetailsRecordPage;

            }
        }

        public HealthIssueTypesPage healthIssueTypesPage
        {
            get
            {
                if (_healthIssueTypesPage == null)
                    _healthIssueTypesPage = new HealthIssueTypesPage(driver, wait, appURL);
                return _healthIssueTypesPage;

            }
        }

        public HealthIssueTypesRecordPage healthIssueTypesRecordPage
        {
            get
            {
                if (_healthIssueTypesRecordPage == null)
                    _healthIssueTypesRecordPage = new HealthIssueTypesRecordPage(driver, wait, appURL);
                return _healthIssueTypesRecordPage;

            }
        }

        public PersonGestationPeriodPage personGestationPeriodPage
        {
            get
            {
                if (_personGestationPeriodPage == null)
                    _personGestationPeriodPage = new PersonGestationPeriodPage(driver, wait, appURL);
                return _personGestationPeriodPage;

            }
        }

        public PersonGestationPeriodRecordPage personGestationPeriodRecordPage
        {
            get
            {
                if (_personGestationPeriodRecordPage == null)
                    _personGestationPeriodRecordPage = new PersonGestationPeriodRecordPage(driver, wait, appURL);
                return _personGestationPeriodRecordPage;

            }
        }

        public PersonHealthDisabilityImpairmentsPage personHealthDisabilityImpairmentsPage
        {
            get
            {
                if (_personHealthDisabilityImpairmentsPage == null)
                    _personHealthDisabilityImpairmentsPage = new PersonHealthDisabilityImpairmentsPage(driver, wait, appURL);
                return _personHealthDisabilityImpairmentsPage;

            }
        }

        public PersonHealthDisabilityImpairmentsRecordPage personHealthDisabilityImpairmentsRecordPage
        {
            get
            {
                if (_personHealthDisabilityImpairmentsRecordPage == null)
                    _personHealthDisabilityImpairmentsRecordPage = new PersonHealthDisabilityImpairmentsRecordPage(driver, wait, appURL);
                return _personHealthDisabilityImpairmentsRecordPage;

            }
        }

        public PersonRecurringAppointmentsPage personRecurringAppointmentsPage
        {
            get
            {
                if (_personRecurringAppointmentsPage == null)
                    _personRecurringAppointmentsPage = new PersonRecurringAppointmentsPage(driver, wait, appURL);
                return _personRecurringAppointmentsPage;
            }
        }

        public PersonRecurringAppointmentRecordPage personRecurringAppointmentRecordPage
        {
            get
            {
                if (_personRecurringAppointmentRecordPage == null)
                    _personRecurringAppointmentRecordPage = new PersonRecurringAppointmentRecordPage(driver, wait, appURL);
                return _personRecurringAppointmentRecordPage;
            }
        }

        public PersonRecordsOfDNARPage personRecordsOfDNARPage
        {
            get
            {
                if (_personRecordsOfDNARPage == null)
                    _personRecordsOfDNARPage = new PersonRecordsOfDNARPage(driver, wait, appURL);
                return _personRecordsOfDNARPage;
            }
        }

        public PersonRecordsOfDNARRecordPage personRecordsOfDNARRecordPage
        {
            get
            {
                if (_personRecordsOfDNARRecordPage == null)
                    _personRecordsOfDNARRecordPage = new PersonRecordsOfDNARRecordPage(driver, wait, appURL);
                return _personRecordsOfDNARRecordPage;
            }
        }

        public PersonRelationshipRecordPage personRelationshipRecordPage
        {
            get
            {
                if (_personRelationshipRecordPage == null)
                    _personRelationshipRecordPage = new PersonRelationshipRecordPage(driver, wait, appURL);
                return _personRelationshipRecordPage;
            }
        }

        public AllegedAbuserPage allegedAbuserPage
        {
            get
            {
                if (_allegedAbuserPage == null)
                    _allegedAbuserPage = new AllegedAbuserPage(driver, wait, appURL);
                return _allegedAbuserPage;
            }
        }

        public AllegedAbsuserRecordPage allegedAbsuserRecordPage
        {
            get
            {
                if (_allegedAbsuserRecordPage == null)
                    _allegedAbsuserRecordPage = new AllegedAbsuserRecordPage(driver, wait, appURL);
                return _allegedAbsuserRecordPage;
            }
        }
        public AllegedVictimPage allegedVictimPage
        {
            get
            {
                if (_allegedVictimPage == null)
                    _allegedVictimPage = new AllegedVictimPage(driver, wait, appURL);
                return _allegedVictimPage;
            }
        }

        public PersonHealthImmunisationPage personHealthImmunisationPage
        {
            get
            {
                if (_personHealthImmunisationPage == null)
                    _personHealthImmunisationPage = new PersonHealthImmunisationPage(driver, wait, appURL);
                return _personHealthImmunisationPage;
            }
        }

        public PersonHealthImmunisationRecordPage personHealthImmunisationRecordPage
        {
            get
            {
                if (_personHealthImmunisationRecordPage == null)
                    _personHealthImmunisationRecordPage = new PersonHealthImmunisationRecordPage(driver, wait, appURL);
                return _personHealthImmunisationRecordPage;
            }
        }

        public AllegedVictimRecordPage allegedVictimRecordPage
        {
            get
            {
                if (_allegedVictimRecordPage == null)
                    _allegedVictimRecordPage = new AllegedVictimRecordPage(driver, wait, appURL);
                return _allegedVictimRecordPage;
            }
        }

        public AllegationInvestigatorPage allegationInvestigatorPage
        {
            get
            {
                if (_allegationInvestigatorPage == null)
                    _allegationInvestigatorPage = new AllegationInvestigatorPage(driver, wait, appURL);
                return _allegationInvestigatorPage;
            }
        }

        public AllegationInvestigatorRecordPage allegationInvestigatorRecordPage
        {
            get
            {
                if (_allegationInvestigatorRecordPage == null)
                    _allegationInvestigatorRecordPage = new AllegationInvestigatorRecordPage(driver, wait, appURL);
                return _allegationInvestigatorRecordPage;
            }
        }


        public HospitalWardsPage hospitalWardsPage
        {
            get
            {
                if (_hospitalWardsPage == null)
                    _hospitalWardsPage = new HospitalWardsPage(driver, wait, appURL);
                return _hospitalWardsPage;
            }
        }

        public HospitalWardsRecordPage hospitalWardsRecordPage
        {
            get
            {
                if (_hospitalWardsRecordPage == null)
                    _hospitalWardsRecordPage = new HospitalWardsRecordPage(driver, wait, appURL);
                return _hospitalWardsRecordPage;
            }
        }

        public ProvidersRecordPage providersRecordPage
        {
            get
            {
                if (_providersRecordPage == null)
                    _providersRecordPage = new ProvidersRecordPage(driver, wait, appURL);
                return _providersRecordPage;
            }
        }

        public BayOrRoomsRecordpage bayOrRoomsRecordpage
        {
            get
            {
                if (_bayOrRoomsRecordpage == null)
                    _bayOrRoomsRecordpage = new BayOrRoomsRecordpage(driver, wait, appURL);
                return _bayOrRoomsRecordpage;
            }
        }

        public BayOrRoomspage bayOrRoomspage
        {
            get
            {
                if (_bayOrRoomspage == null)
                    _bayOrRoomspage = new BayOrRoomspage(driver, wait, appURL);
                return _bayOrRoomspage;
            }
        }

        public BedPage bedPage
        {
            get
            {
                if (_bedPage == null)
                    _bedPage = new BedPage(driver, wait, appURL);
                return _bedPage;
            }
        }

        public BedRecordPage bedRecordPage
        {
            get
            {
                if (_bedRecordPage == null)
                    _bedRecordPage = new BedRecordPage(driver, wait, appURL);
                return _bedRecordPage;
            }
        }

        public PersonCasesPage personCasesPage
        {
            get
            {
                if (_personCasesPage == null)
                    _personCasesPage = new PersonCasesPage(driver, wait, appURL);
                return _personCasesPage;
            }
        }

        public PersonCasesRecordPage personCasesRecordPage
        {
            get
            {
                if (_personCasesRecordPage == null)
                    _personCasesRecordPage = new PersonCasesRecordPage(driver, wait, appURL);
                return _personCasesRecordPage;
            }
        }



        public PersonRecordNewPage personRecordNewPage
        {
            get
            {
                if (_personRecordNewPage == null)
                    _personRecordNewPage = new PersonRecordNewPage(driver, wait, appURL);
                return _personRecordNewPage;
            }
        }

        public AddressActionPopUp addressActionPopUp
        {
            get
            {
                if (_addressActionPopUp == null)
                    _addressActionPopUp = new AddressActionPopUp(driver, wait, appURL);
                return _addressActionPopUp;
            }
        }

        public PersonAllergiesPage personAllergiesPage
        {
            get
            {
                if (_personAllergiesPage == null)
                    _personAllergiesPage = new PersonAllergiesPage(driver, wait, appURL);
                return _personAllergiesPage;
            }
        }

        public PersonAllergyRecordPage personAllergyRecordPage
        {
            get
            {
                if (_personAllergyRecordPage == null)
                    _personAllergyRecordPage = new PersonAllergyRecordPage(driver, wait, appURL);
                return _personAllergyRecordPage;
            }
        }

        public SnomedLookupPopup snomedLookupPopup
        {
            get
            {
                if (_snomedLookupPopup == null)
                    _snomedLookupPopup = new SnomedLookupPopup(driver, wait, appURL);
                return _snomedLookupPopup;

            }
        }

        public PersonAllergyReactionsInnerGridArea personAllergyReactionsInnerGridArea
        {
            get
            {
                if (_personAllergyReactionsInnerGridArea == null)
                    _personAllergyReactionsInnerGridArea = new PersonAllergyReactionsInnerGridArea(driver, wait, appURL);
                return _personAllergyReactionsInnerGridArea;

            }
        }

        public PersonAllergicReactionRecordPage personAllergicReactionRecordPage
        {
            get
            {
                if (_personAllergicReactionRecordPage == null)
                    _personAllergicReactionRecordPage = new PersonAllergicReactionRecordPage(driver, wait, appURL);
                return _personAllergicReactionRecordPage;
            }
        }


        public PersonAbsencesRecordPage personAbsencesRecordPage
        {
            get
            {
                if (_personAbsencesRecordPage == null)
                    _personAbsencesRecordPage = new PersonAbsencesRecordPage(driver, wait, appURL);
                return _personAbsencesRecordPage;
            }
        }


        public CaseBrokerageEpisodesPage caseBrokerageEpisodesPage
        {
            get
            {
                if (_caseBrokerageEpisodesPage == null)
                    _caseBrokerageEpisodesPage = new CaseBrokerageEpisodesPage(driver, wait, appURL);
                return _caseBrokerageEpisodesPage;
            }
        }

        public CaseBrokerageEpisodeRecordPage caseBrokerageEpisodeRecordPage
        {
            get
            {
                if (_caseBrokerageEpisodeRecordPage == null)
                    _caseBrokerageEpisodeRecordPage = new CaseBrokerageEpisodeRecordPage(driver, wait, appURL);
                return _caseBrokerageEpisodeRecordPage;
            }
        }

        public CaseBrokerageEpisodeOffersPage caseBrokerageEpisodeOffersPage
        {
            get
            {
                if (_caseBrokerageEpisodeOffersPage == null)
                    _caseBrokerageEpisodeOffersPage = new CaseBrokerageEpisodeOffersPage(driver, wait, appURL);
                return _caseBrokerageEpisodeOffersPage;
            }
        }

        public CaseBrokerageEpisodeOfferRecordPage caseBrokerageEpisodeOfferRecordPage
        {
            get
            {
                if (_caseBrokerageEpisodeOfferRecordPage == null)
                    _caseBrokerageEpisodeOfferRecordPage = new CaseBrokerageEpisodeOfferRecordPage(driver, wait, appURL);
                return _caseBrokerageEpisodeOfferRecordPage;
            }
        }

        public CaseBrokerageOfferCommunicationsSubArea caseBrokerageOfferCommunicationsSubArea
        {
            get
            {
                if (_caseBrokerageOfferCommunicationsSubArea == null)
                    _caseBrokerageOfferCommunicationsSubArea = new CaseBrokerageOfferCommunicationsSubArea(driver, wait, appURL);
                return _caseBrokerageOfferCommunicationsSubArea;
            }
        }

        public CaseBrokerageOfferCommunicationRecordPage caseBrokerageOfferCommunicationRecordPage
        {
            get
            {
                if (_caseBrokerageOfferCommunicationRecordPage == null)
                    _caseBrokerageOfferCommunicationRecordPage = new CaseBrokerageOfferCommunicationRecordPage(driver, wait, appURL);
                return _caseBrokerageOfferCommunicationRecordPage;
            }
        }

        public BrokerageEpisodeEscalationsPage brokerageEpisodeEscalationsPage
        {
            get
            {
                if (_brokerageEpisodeEscalationsPage == null)
                    _brokerageEpisodeEscalationsPage = new BrokerageEpisodeEscalationsPage(driver, wait, appURL);
                return _brokerageEpisodeEscalationsPage;
            }
        }

        public BrokerageEpisodeEscalationRecordPage brokerageEpisodeEscalationRecordPage
        {
            get
            {
                if (_brokerageEpisodeEscalationRecordPage == null)
                    _brokerageEpisodeEscalationRecordPage = new BrokerageEpisodeEscalationRecordPage(driver, wait, appURL);
                return _brokerageEpisodeEscalationRecordPage;
            }
        }

        public BrokerageEpisodeEscalationsSubArea brokerageEpisodeEscalationsSubArea
        {
            get
            {
                if (_brokerageEpisodeEscalationsSubArea == null)
                    _brokerageEpisodeEscalationsSubArea = new BrokerageEpisodeEscalationsSubArea(driver, wait, appURL);
                return _brokerageEpisodeEscalationsSubArea;
            }
        }


        public SelectCaseTypePopUp selectCaseTypePopUp
        {
            get
            {
                if (_selectCaseTypePopUp == null)
                    _selectCaseTypePopUp = new SelectCaseTypePopUp(driver, wait, appURL);
                return _selectCaseTypePopUp;

            }
        }


        public CaseFormAssessmentFactorPage caseFormAssessmentFactorPage
        {
            get
            {
                if (_caseFormAssessmentFactorPage == null)
                    _caseFormAssessmentFactorPage = new CaseFormAssessmentFactorPage(driver, wait, appURL);
                return _caseFormAssessmentFactorPage;
            }
        }


        public CorrectErrorsPopUp correctErrorsPopUp
        {
            get
            {
                if (_correctErrorsPopUp == null)
                    _correctErrorsPopUp = new CorrectErrorsPopUp(driver, wait, appURL);
                return _correctErrorsPopUp;


            }
        }


        public CaseFormAssessmentFactorRecordPage caseFormAssessmentFactorRecordPage
        {
            get
            {
                if (_caseFormAssessmentFactorRecordPage == null)
                    _caseFormAssessmentFactorRecordPage = new CaseFormAssessmentFactorRecordPage(driver, wait, appURL);
                return _caseFormAssessmentFactorRecordPage;


            }
        }

        public CaseFormMembersPage caseFormMembersPage
        {
            get
            {
                if (_caseFormMembersPage == null)
                    _caseFormMembersPage = new CaseFormMembersPage(driver, wait, appURL);
                return _caseFormMembersPage;

            }
        }

        public CaseFormMembersRecordPage caseFormMembersRecordPage
        {
            get
            {
                if (_caseFormMembersRecordPage == null)
                    _caseFormMembersRecordPage = new CaseFormMembersRecordPage(driver, wait, appURL);
                return _caseFormMembersRecordPage;

            }
        }
        public CaseBrokerageEpisodeServiceProvisionsPage caseBrokerageEpisodeServiceProvisionsPage
        {
            get
            {
                if (_caseBrokerageEpisodeServiceProvisionsPage == null)
                    _caseBrokerageEpisodeServiceProvisionsPage = new CaseBrokerageEpisodeServiceProvisionsPage(driver, wait, appURL);
                return _caseBrokerageEpisodeServiceProvisionsPage;
            }
        }


        public CaseBrokerageEpisodeAttachmentsPage caseBrokerageEpisodeAttachmentsPage
        {
            get
            {
                if (_caseBrokerageEpisodeAttachmentsPage == null)
                    _caseBrokerageEpisodeAttachmentsPage = new CaseBrokerageEpisodeAttachmentsPage(driver, wait, appURL);
                return _caseBrokerageEpisodeAttachmentsPage;


            }
        }


        public CaseBrokerageEpisodePausePeriodsRecordPage caseBrokerageEpisodePausePeriodsRecordPage
        {
            get
            {
                if (_caseBrokerageEpisodePausePeriodsRecordPage == null)
                    _caseBrokerageEpisodePausePeriodsRecordPage = new CaseBrokerageEpisodePausePeriodsRecordPage(driver, wait, appURL);
                return _caseBrokerageEpisodePausePeriodsRecordPage;
            }
        }

        public CaseBrokerageEpisodeAttachmentRecordPage caseBrokerageEpisodeAttachmentRecordPage
        {
            get
            {
                if (_caseBrokerageEpisodeAttachmentRecordPage == null)
                    _caseBrokerageEpisodeAttachmentRecordPage = new CaseBrokerageEpisodeAttachmentRecordPage(driver, wait, appURL);
                return _caseBrokerageEpisodeAttachmentRecordPage;

            }
        }


        public PersonCarePlansSubPage_RegularCareTasksTab_Record personCarePlansSubPage_RegularCareTasksTab_Record
        {
            get
            {
                if (_personCarePlansSubPage_RegularCareTasksTab_Record == null)
                    _personCarePlansSubPage_RegularCareTasksTab_Record = new PersonCarePlansSubPage_RegularCareTasksTab_Record(driver, wait, appURL);
                return _personCarePlansSubPage_RegularCareTasksTab_Record;

            }
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage
        {
            get
            {
                if (_personCarePlansSubPage_RegularCareTask_CareScheludesRecordPage == null)
                    _personCarePlansSubPage_RegularCareTask_CareScheludesRecordPage = new PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage(driver, wait, appURL);
                return _personCarePlansSubPage_RegularCareTask_CareScheludesRecordPage;

            }
        }
        public CaseBrokerageOfferAttachmentsPage caseBrokerageOfferAttachmentsPage
        {
            get
            {
                if (_caseBrokerageOfferAttachmentsPage == null)
                    _caseBrokerageOfferAttachmentsPage = new CaseBrokerageOfferAttachmentsPage(driver, wait, appURL);
                return _caseBrokerageOfferAttachmentsPage;

            }
        }

        public CaseBrokerageOfferAttachmentRecordPage caseBrokerageOfferAttachmentRecordPage
        {
            get
            {
                if (_caseBrokerageOfferAttachmentRecordPage == null)
                    _caseBrokerageOfferAttachmentRecordPage = new CaseBrokerageOfferAttachmentRecordPage(driver, wait, appURL);
                return _caseBrokerageOfferAttachmentRecordPage;


            }
        }



        public CaseBrokerageEpisodePausePeriodPage caseBrokerageEisodePausePeriodPage
        {
            get
            {
                if (_caseBrokerageEpisodePausePeriodPage == null)
                    _caseBrokerageEpisodePausePeriodPage = new CaseBrokerageEpisodePausePeriodPage(driver, wait, appURL);
                return _caseBrokerageEpisodePausePeriodPage;

            }
        }


        public ServiceProvisionStartReasonPage serviceProvisionStartReasonPage
        {
            get
            {
                if (_serviceProvisionStartReasonPage == null)
                    _serviceProvisionStartReasonPage = new ServiceProvisionStartReasonPage(driver, wait, appURL);
                return _serviceProvisionStartReasonPage;

            }
        }

        public ServiceProvisionStartReasonRecordPage serviceProvisionStartReasonRecordPage
        {
            get
            {
                if (_serviceProvisionStartReasonRecordPage == null)
                    _serviceProvisionStartReasonRecordPage = new ServiceProvisionStartReasonRecordPage(driver, wait, appURL);
                return _serviceProvisionStartReasonRecordPage;

            }
        }

        public LookupFormPage lookupFormPage
        {
            get
            {
                if (_lookupFormPage == null)
                    _lookupFormPage = new LookupFormPage(driver, wait, appURL);
                return _lookupFormPage;

            }
        }

        public ServiceElement1RecordPage serviceElement1RecordPage
        {
            get
            {
                if (_serviceElement1RecordPage == null)
                    _serviceElement1RecordPage = new ServiceElement1RecordPage(driver, wait, appURL);
                return _serviceElement1RecordPage;

            }
        }

        public BrokerageOfferCancellationReasonsPage brokerageOfferCancellationReasons
        {
            get
            {
                if (_brokerageOfferCancellationReasonsPage == null)
                    _brokerageOfferCancellationReasonsPage = new BrokerageOfferCancellationReasonsPage(driver, wait, appURL);
                return _brokerageOfferCancellationReasonsPage;

            }
        }




        public BrokerageOfferCancellationReasonsRecordPage brokerageOfferCancellationReasonsRecordPage
        {
            get
            {
                if (_brokerageOfferCancellationReasonsRecordPage == null)
                    _brokerageOfferCancellationReasonsRecordPage = new BrokerageOfferCancellationReasonsRecordPage(driver, wait, appURL);
                return _brokerageOfferCancellationReasonsRecordPage;

            }
        }

        public OptionSetFormPopUp optionSetFormPopUp
        {
            get
            {
                if (_optionSetFormPopUp == null)
                    _optionSetFormPopUp = new OptionSetFormPopUp(driver, wait, appURL);
                return _optionSetFormPopUp;

            }
        }

        public SystemUserLanguagesPage systemUserLanguagesPage
        {
            get
            {
                if (_systemUserLanguagesPage == null)
                    _systemUserLanguagesPage = new SystemUserLanguagesPage(driver, wait, appURL);
                return _systemUserLanguagesPage;

            }
        }
        public SystemUserLanguageRecordPage systemUserLanguageRecordPage
        {
            get
            {
                if (_systemUserLanguageRecordPage == null)
                    _systemUserLanguageRecordPage = new SystemUserLanguageRecordPage(driver, wait, appURL);
                return _systemUserLanguageRecordPage;

            }
        }

        public SystemUserAddressPage systemUserAddressPage
        {
            get
            {
                if (_systemUserAddressPage == null)
                    _systemUserAddressPage = new SystemUserAddressPage(driver, wait, appURL);
                return _systemUserAddressPage;

            }
        }

        public SystemUserAddressRecordPage systemUserAddressRecordPage
        {
            get
            {
                if (_systemUserAddressRecordPage == null)
                    _systemUserAddressRecordPage = new SystemUserAddressRecordPage(driver, wait, appURL);
                return _systemUserAddressRecordPage;

            }
        }

        public PersonAddressesPage personAddressesPage
        {
            get
            {
                if (_personAddressesPage == null)
                    _personAddressesPage = new PersonAddressesPage(driver, wait, appURL);
                return _personAddressesPage;


            }
        }

        public CacheMonitorPage cacheMonitorPage
        {
            get
            {
                if (_cacheMonitorPage == null)
                    _cacheMonitorPage = new CacheMonitorPage(driver, wait, appURL);
                return _cacheMonitorPage;
            }
        }

        public SystemUserAliasesPage systemUserAliasesPage
        {
            get
            {
                if (_systemUserAliasesPage == null)
                    _systemUserAliasesPage = new SystemUserAliasesPage(driver, wait, appURL);
                return _systemUserAliasesPage;
            }
        }


        public SmokeTest_CaseFormEditAssessmentPage smokeTest_CaseFormEditAssessmentPage
        {
            get
            {
                if (_smokeTest_CaseFormEditAssessmentPage == null)
                    _smokeTest_CaseFormEditAssessmentPage = new SmokeTest_CaseFormEditAssessmentPage(driver, wait, appURL);
                return _smokeTest_CaseFormEditAssessmentPage;
            }
        }

        public SystemUserAliasesRecordPage systemUserAliasesRecordPage
        {
            get
            {
                if (_systemUserAliasesRecordPage == null)
                    _systemUserAliasesRecordPage = new SystemUserAliasesRecordPage(driver, wait, appURL);
                return _systemUserAliasesRecordPage;

            }
        }

        public SystemUserEmergencyContactsPage systemUserEmergencyContactsPage
        {
            get
            {
                if (_systemUserEmergencyContactsPage == null)
                    _systemUserEmergencyContactsPage = new SystemUserEmergencyContactsPage(driver, wait, appURL);
                return _systemUserEmergencyContactsPage;

            }
        }

        public SystemUserEmergencyContactsRecordPage systemUserEmergencyContactsRecordPage
        {
            get
            {
                if (_systemUserEmergencyContactsRecordPage == null)
                    _systemUserEmergencyContactsRecordPage = new SystemUserEmergencyContactsRecordPage(driver, wait, appURL);
                return _systemUserEmergencyContactsRecordPage;

            }
        }

        public SystemUserSuspensionsPage systemUserSuspensionsPage
        {
            get
            {
                if (_systemUserSuspensionsPage == null)
                    _systemUserSuspensionsPage = new SystemUserSuspensionsPage(driver, wait, appURL);
                return _systemUserSuspensionsPage;

            }
        }

        public SystemUserSuspensionsRecordPage systemUserSuspensionsRecordPage
        {
            get
            {
                if (_systemUserSuspensionsRecordPage == null)
                    _systemUserSuspensionsRecordPage = new SystemUserSuspensionsRecordPage(driver, wait, appURL);
                return _systemUserSuspensionsRecordPage;

            }
        }

        public SystemUserEmploymentContractsPage systemUserEmploymentContractsPage
        {
            get
            {
                if (_systemUserEmploymentContractsPage == null)
                    _systemUserEmploymentContractsPage = new SystemUserEmploymentContractsPage(driver, wait, appURL);
                return _systemUserEmploymentContractsPage;

            }
        }

        public SystemUserEmploymentContractsRecordPage systemUserEmploymentContractsRecordPage
        {
            get
            {
                if (_systemUserEmploymentContractsRecordPage == null)
                    _systemUserEmploymentContractsRecordPage = new SystemUserEmploymentContractsRecordPage(driver, wait, appURL);
                return _systemUserEmploymentContractsRecordPage;

            }
        }

        public SystemSettingsPage systemSettingsPage
        {
            get
            {
                if (_systemSettingsPage == null)
                    _systemSettingsPage = new SystemSettingsPage(driver, wait, appURL);
                return _systemSettingsPage;

            }
        }

        public SystemSettingRecordPage systemSettingRecordPage
        {
            get
            {
                if (_systemSettingRecordPage == null)
                    _systemSettingRecordPage = new SystemSettingRecordPage(driver, wait, appURL);
                return _systemSettingRecordPage;

            }
        }

        public PersonHealthDiagnosesPage personHealthDiagnosesPage
        {
            get
            {
                if (_personHealthDiagnosesPage == null)
                    _personHealthDiagnosesPage = new PersonHealthDiagnosesPage(driver, wait, appURL);
                return _personHealthDiagnosesPage;

            }
        }

        public PersonHealthDiagnosisRecordPage PersonHealthDiagnosisRecordPage
        {
            get
            {
                if (_personHealthDiagnosisRecordPage == null)
                    _personHealthDiagnosisRecordPage = new PersonHealthDiagnosisRecordPage(driver, wait, appURL);
                return _personHealthDiagnosisRecordPage;

            }
        }

        public ConsultantEpisodesPage consultantEpisodesPage
        {
            get
            {
                if (_consultantEpisodesPage == null)
                    _consultantEpisodesPage = new ConsultantEpisodesPage(driver, wait, appURL);
                return _consultantEpisodesPage;

            }
        }

        public ConsultantEpisodesRecordPage consultantEpisodesRecordPage
        {
            get
            {
                if (_consultantEpisodesRecordPage == null)
                    _consultantEpisodesRecordPage = new ConsultantEpisodesRecordPage(driver, wait, appURL);
                return _consultantEpisodesRecordPage;

            }
        }

        public InpatientLeaveAwolAttachmentsPage inpatientLeaveAwolAttachmentsPage
        {
            get
            {
                if (_inpatientLeaveAwolAttachmentsPage == null)
                    _inpatientLeaveAwolAttachmentsPage = new InpatientLeaveAwolAttachmentsPage(driver, wait, appURL);
                return _inpatientLeaveAwolAttachmentsPage;
            }
        }

        public InpatientLeaveAwolAttachmentsRecordPage inpatientLeaveAwolAttachmentsRecordPage
        {
            get
            {
                if (_inpatientLeaveAwolAttachmentsRecordPage == null)
                    _inpatientLeaveAwolAttachmentsRecordPage = new InpatientLeaveAwolAttachmentsRecordPage(driver, wait, appURL);
                return _inpatientLeaveAwolAttachmentsRecordPage;

            }
        }


        public SeclusionsPage seclusionsPage
        {
            get
            {
                if (_seclusionsPage == null)
                    _seclusionsPage = new SeclusionsPage(driver, wait, appURL);
                return _seclusionsPage;

            }
        }

        public SeclusionsRecordPage seclusionsRecordPage
        {
            get
            {
                if (_seclusionsRecordPage == null)
                    _seclusionsRecordPage = new SeclusionsRecordPage(driver, wait, appURL);
                return _seclusionsRecordPage;

            }
        }

        public SeclusionReviewsPage seclusionReviewsPage
        {
            get
            {
                if (_seclusionReviewsPage == null)
                    _seclusionReviewsPage = new SeclusionReviewsPage(driver, wait, appURL);
                return _seclusionReviewsPage;

            }
        }

        public SeclusionReviewsRecordPage seclusionReviewsRecordPage
        {
            get
            {
                if (_seclusionReviewsRecordPage == null)
                    _seclusionReviewsRecordPage = new SeclusionReviewsRecordPage(driver, wait, appURL);
                return _seclusionReviewsRecordPage;

            }
        }

        public InpatientSeclusionAttachmentPage inpatientSeclusionAttachmentPage
        {
            get
            {
                if (_inpatientSeclusionAttachmentPage == null)
                    _inpatientSeclusionAttachmentPage = new InpatientSeclusionAttachmentPage(driver, wait, appURL);
                return _inpatientSeclusionAttachmentPage;

            }
        }

        public InpatientSeclusionAttachmentRecordPage inpatientSeclusionAttachmentRecordPage
        {
            get
            {
                if (_inpatientSeclusionAttachmentRecordPage == null)
                    _inpatientSeclusionAttachmentRecordPage = new InpatientSeclusionAttachmentRecordPage(driver, wait, appURL);
                return _inpatientSeclusionAttachmentRecordPage;

            }
        }

        public OrganisationalRiskManagementPage organisationalRiskManagementPage
        {
            get
            {
                if (_organisationalRiskManagementPage == null)
                    _organisationalRiskManagementPage = new OrganisationalRiskManagementPage(driver, wait, appURL);
                return _organisationalRiskManagementPage;

            }
        }



        public OrganisationalRiskManagementRecordPage organisationalRiskManagementRecordPage
        {
            get
            {
                if (_organisationalRiskManagementRecordPage == null)
                    _organisationalRiskManagementRecordPage = new OrganisationalRiskManagementRecordPage(driver, wait, appURL);
                return _organisationalRiskManagementRecordPage;

            }
        }

        public ActionPlansPage actionPlansPage
        {
            get
            {
                if (_ActionPlansPage == null)
                    _ActionPlansPage = new ActionPlansPage(driver, wait, appURL);
                return _ActionPlansPage;
            }
        }

        public ActionPlansRecordPage actionPlansRecordPage

        {
            get
            {
                if (_ActionPlansRecordPage == null)
                    _ActionPlansRecordPage = new ActionPlansRecordPage(driver, wait, appURL);
                return _ActionPlansRecordPage;

            }
        }
        public PersonBodyMapsPage personBodyMaps
        {
            get
            {
                if (_personBodyMapsPage == null)
                    _personBodyMapsPage = new PersonBodyMapsPage(driver, wait, appURL);
                return _personBodyMapsPage;

            }
        }
        public PersonBodyMapRecordPage personBodyRecordPage
        {
            get
            {
                if (_personBodyMapRecordPage == null)
                    _personBodyMapRecordPage = new PersonBodyMapRecordPage(driver, wait, appURL);
                return _personBodyMapRecordPage;

            }
        }

        public PersonMobilityPage personMobilityPage
        {
            get
            {
                if (_personMobilityPage == null)
                    _personMobilityPage = new PersonMobilityPage(driver, wait, appURL);
                return _personMobilityPage;

            }
        }

        public PersonMobilityRecordPage personMobilityRecordPage
        {
            get
            {
                if (_personMobilityRecordPage == null)
                    _personMobilityRecordPage = new PersonMobilityRecordPage(driver, wait, appURL);
                return _personMobilityRecordPage;

            }
        }

        public LookUpRecordsPopUp lookUpRecordsPopUp
        {
            get
            {
                if (_lookUpRecordsPopUp == null)
                    _lookUpRecordsPopUp = new LookUpRecordsPopUp(driver, wait, appURL);
                return _lookUpRecordsPopUp;

            }
        }


        public BookingTypesPopUp bookingTypesPopUp
        {
            get
            {
                if (_bookingTypesPopUp == null)
                    _bookingTypesPopUp = new BookingTypesPopUp(driver, wait, appURL);
                return _bookingTypesPopUp;

            }
        }

        public OpenEndedAbsencesPopUp openEndedAbsencesPopUp
        {
            get
            {
                if (_openEndedAbsencesPopUp == null)
                    _openEndedAbsencesPopUp = new OpenEndedAbsencesPopUp(driver, wait, appURL);
                return _openEndedAbsencesPopUp;

            }
        }

        public NoBookingOpenEndedAbsencesPopUp noBookingOpenEndedAbsencesPopUp
        {
            get
            {
                if (_noBookingOpenEndedAbsencesPopUp == null)
                    _noBookingOpenEndedAbsencesPopUp = new NoBookingOpenEndedAbsencesPopUp(driver, wait, appURL);
                return _noBookingOpenEndedAbsencesPopUp;

            }
        }

        public ReviewExistingBookingsPopUp reviewExistingBookingsPopUp
        {
            get
            {
                if (_reviewExistingBookingsPopUp == null)
                    _reviewExistingBookingsPopUp = new ReviewExistingBookingsPopUp(driver, wait, appURL);
                return _reviewExistingBookingsPopUp;

            }
        }
        public OrganisationalRiskCategoryPage organisationalRiskCategoriesPage
        {
            get
            {
                if (_organisationalRiskCategoryPage == null)
                    _organisationalRiskCategoryPage = new OrganisationalRiskCategoryPage(driver, wait, appURL);
                return _organisationalRiskCategoryPage;
            }
        }

        public OrganisationalRiskCategoryRecordPage organisationalRiskCategoriesRecordPage
        {
            get
            {
                if (_organisationalRiskCategoryRecordPage == null)
                    _organisationalRiskCategoryRecordPage = new OrganisationalRiskCategoryRecordPage(driver, wait, appURL);
                return _organisationalRiskCategoryRecordPage;
            }
        }



        public PersonRecordPage_AboutMeArea personRecordPage_AboutMeArea
        {
            get
            {
                if (_personRecordPage_AboutMeArea == null)
                    _personRecordPage_AboutMeArea = new PersonRecordPage_AboutMeArea(driver, wait, appURL);
                return _personRecordPage_AboutMeArea;
            }
        }

        public Person_AboutMePage personAboutMePage
        {
            get
            {
                if (_personAboutMePage == null)
                    _personAboutMePage = new Person_AboutMePage(driver, wait, appURL);
                return _personAboutMePage;
            }
        }

        public Person_AboutMeRecordPage personAboutMeRecordPage
        {
            get
            {
                if (_personAboutMeRecordPage == null)
                    _personAboutMeRecordPage = new Person_AboutMeRecordPage(driver, wait, appURL);
                return _personAboutMeRecordPage;
            }
        }
        public Person_DiaryRecordPage personDiaryRecordPage
        {
            get
            {
                if (_personDiaryRecordPage == null)
                    _personDiaryRecordPage = new Person_DiaryRecordPage(driver, wait, appURL);
                return _personDiaryRecordPage;
            }
        }

        public ReportableEventPage reportableEventPage
        {
            get
            {
                if (_reportableEventPage == null)
                    _reportableEventPage = new ReportableEventPage(driver, wait, appURL);
                return _reportableEventPage;

            }
        }

        public ReportableEventBehaviourPage reportableEventBehaviourPage
        {
            get
            {
                if (_reportableEventBehaviourPage == null)
                    _reportableEventBehaviourPage = new ReportableEventBehaviourPage(driver, wait, appURL);
                return _reportableEventBehaviourPage;

            }
        }
        public ReportableEventBehaviourRecordPages reportableEventBehaviourRecordPages
        {
            get
            {
                if (_reportableEventBehaviourRecordPages == null)
                    _reportableEventBehaviourRecordPages = new ReportableEventBehaviourRecordPages(driver, wait, appURL);
                return _reportableEventBehaviourRecordPages;

            }

        }
        public ReportableEventRecordPage reportableEventRecordPage
        {
            get
            {
                if (_reportableEventRecordPage == null)
                    _reportableEventRecordPage = new ReportableEventRecordPage(driver, wait, appURL);
                return _reportableEventRecordPage;

            }

        }
        public ReportableEventImpactsPage reportableEventImpactsPage
        {
            get
            {
                if (_reportableEventImpactsPage == null)
                    _reportableEventImpactsPage = new ReportableEventImpactsPage(driver, wait, appURL);
                return _reportableEventImpactsPage;

            }
        }

        public ReportableEventImpactRecordPages reportableEventImpactRecordPages
        {
            get
            {
                if (_reportableEventImpactRecordPages == null)
                    _reportableEventImpactRecordPages = new ReportableEventImpactRecordPages(driver, wait, appURL);
                return _reportableEventImpactRecordPages;

            }
        }

        public ReportableEventImpactPersonBodymapRecordPages reportableEventImpactPersonBodymapRecordPages
        {
            get
            {
                if (_reportableEventImpactPersonBodymapRecordPages == null)
                    _reportableEventImpactPersonBodymapRecordPages = new ReportableEventImpactPersonBodymapRecordPages(driver, wait, appURL);
                return _reportableEventImpactPersonBodymapRecordPages;

            }
        }

        public ReportableEventAttachmentsPage reportableEventAttachmentsPage
        {
            get
            {
                if (_reportableEventAttachmentsPage == null)
                    _reportableEventAttachmentsPage = new ReportableEventAttachmentsPage(driver, wait, appURL);
                return _reportableEventAttachmentsPage;

            }
        }

        public ReportableEventAttchmentsRecordPage reportableEventAttchmentsRecordPage
        {
            get
            {
                if (_reportableEventAttchmentsRecordPage == null)
                    _reportableEventAttchmentsRecordPage = new ReportableEventAttchmentsRecordPage(driver, wait, appURL);
                return _reportableEventAttchmentsRecordPage;

            }
        }
        public ReportableEventActionsPage reportableEventActionsPage
        {
            get
            {
                if (_reportableEventActionsPage == null)
                    _reportableEventActionsPage = new ReportableEventActionsPage(driver, wait, appURL);
                return _reportableEventActionsPage;

            }
        }
        public ReportableEventActionsRecordPage reportableEventActionsRecordPage
        {
            get
            {
                if (_reportableEventActionsRecordPage == null)
                    _reportableEventActionsRecordPage = new ReportableEventActionsRecordPage(driver, wait, appURL);
                return _reportableEventActionsRecordPage;

            }
        }


        public AboutMeSetupPage aboutMeSetupPage
        {
            get
            {
                if (_aboutMeSetupPage == null)
                    _aboutMeSetupPage = new AboutMeSetupPage(driver, wait, appURL);
                return _aboutMeSetupPage;
            }
        }

        public AboutMeSetupRecordPage aboutMeSetupRecordPage
        {
            get
            {
                if (_aboutMeSetupRecordPage == null)
                    _aboutMeSetupRecordPage = new AboutMeSetupRecordPage(driver, wait, appURL);
                return _aboutMeSetupRecordPage;
            }
        }

        public SelectHealthAppointmentTypePopUp selectHealthAppointmentTypePopUp
        {
            get
            {
                if (_selectHealthAppointmentTypePopUp == null)
                    _selectHealthAppointmentTypePopUp = new SelectHealthAppointmentTypePopUp(driver, wait, appURL);
                return _selectHealthAppointmentTypePopUp;
            }
        }

        public HealthDiaryViewPage healthDiaryViewPage
        {
            get
            {
                if (_healthDiaryViewPage == null)
                    _healthDiaryViewPage = new HealthDiaryViewPage(driver, wait, appURL);
                return _healthDiaryViewPage;

            }
        }

        public SystemUser_DiaryPage systemUser_DiaryPage
        {
            get
            {
                if (_systemUser_DiaryPage == null)
                    _systemUser_DiaryPage = new SystemUser_DiaryPage(driver, wait, appURL);
                return _systemUser_DiaryPage;

            }
        }

        public ServiceProvisionsPage serviceProvisionsPage
        {
            get
            {
                if (_serviceProvisionsPage == null)
                    _serviceProvisionsPage = new ServiceProvisionsPage(driver, wait, appURL);
                return _serviceProvisionsPage;

            }
        }

        public ServiceElement1Page serviceElement1Page
        {
            get
            {
                if (_serviceElement1Page == null)
                    _serviceElement1Page = new ServiceElement1Page(driver, wait, appURL);
                return _serviceElement1Page;

            }
        }

        public ServiceGLCodingsPage serviceGLCodingsPage
        {
            get
            {
                if (_serviceGLCodingsPage == null)
                    _serviceGLCodingsPage = new ServiceGLCodingsPage(driver, wait, appURL);
                return _serviceGLCodingsPage;

            }
        }


        public ServiceGLCodingsRecordPage serviceGLCodingsRecordPage
        {
            get
            {
                if (_serviceGLCodingsRecordPage == null)
                    _serviceGLCodingsRecordPage = new ServiceGLCodingsRecordPage(driver, wait, appURL);
                return _serviceGLCodingsRecordPage;

            }
        }


        public ServicesProvidedPage servicesProvidedPage
        {
            get
            {
                if (_servicesProvidedPage == null)
                    _servicesProvidedPage = new ServicesProvidedPage(driver, wait, appURL);
                return _servicesProvidedPage;

            }
        }

        public ServiceProvidedRecordPage serviceProvidedRecordPage
        {
            get
            {
                if (_serviceProvidedRecordPage == null)
                    _serviceProvidedRecordPage = new ServiceProvidedRecordPage(driver, wait, appURL);
                return _serviceProvidedRecordPage;

            }
        }

        public SystemUserViewDiaryManageAdHocPage systemUserViewDiaryManageAdHocPage
        {
            get
            {
                if (_systemUserViewDiaryManageAdHocPage == null)
                    _systemUserViewDiaryManageAdHocPage = new SystemUserViewDiaryManageAdHocPage(driver, wait, appURL);
                return _systemUserViewDiaryManageAdHocPage;

            }
        }

        public SystemUserAvailabilityScheduleTransportPage systemUserAvailabilityScheduleTransportPage
        {
            get
            {
                if (_systemUserAvailabilityScheduleTransportPage == null)
                    _systemUserAvailabilityScheduleTransportPage = new SystemUserAvailabilityScheduleTransportPage(driver, wait, appURL);
                return _systemUserAvailabilityScheduleTransportPage;

            }
        }

        public UserWorkSchedulesPage userWorkSchedulesPage
        {
            get
            {
                if (_userWorkSchedulesPage == null)
                    _userWorkSchedulesPage = new UserWorkSchedulesPage(driver, wait, appURL);
                return _userWorkSchedulesPage;

            }
        }

        public UserTransportationSchedule userTransportationSchedulePage
        {
            get
            {
                if (_userTransportationSchedulePage == null)
                    _userTransportationSchedulePage = new UserTransportationSchedule(driver, wait, appURL);
                return _userTransportationSchedulePage;

            }
        }

        public TeamsPage teamsPage
        {
            get
            {
                if (_teamsPage == null)
                    _teamsPage = new TeamsPage(driver, wait, appURL);
                return _teamsPage;

            }
        }

        public RosteredUsersPage rosteredUsersPage
        {
            get
            {
                if (_rosteredUsersPage == null)
                    _rosteredUsersPage = new RosteredUsersPage(driver, wait, appURL);
                return _rosteredUsersPage;

            }
        }


        public ProviderUsersPage providerUsersPage
        {
            get
            {
                if (_providerUsersPage == null)
                    _providerUsersPage = new ProviderUsersPage(driver, wait, appURL);
                return _providerUsersPage;

            }
        }

        public CoreUsersPage coreUsersPage
        {
            get
            {
                if (_coreUsersPage == null)
                    _coreUsersPage = new CoreUsersPage(driver, wait, appURL);
                return _coreUsersPage;

            }
        }

        public RoleApplicationsPage roleApplicationsPage
        {
            get
            {
                if (_roleApplicationsPage == null)
                    _roleApplicationsPage = new RoleApplicationsPage(driver, wait, appURL);
                return _roleApplicationsPage;

            }
        }

        public RoleApplicationRecordPage roleApplicationRecordPage
        {
            get
            {
                if (_roleApplicationRecordPage == null)
                    _roleApplicationRecordPage = new RoleApplicationRecordPage(driver, wait, appURL);
                return _roleApplicationRecordPage;

            }
        }

        public RecruitmentDocumentsPage recruitmentDocumentsPage
        {
            get
            {
                if (_recruitmentDocumentsPage == null)
                    _recruitmentDocumentsPage = new RecruitmentDocumentsPage(driver, wait, appURL);
                return _recruitmentDocumentsPage;

            }
        }

        public RecruitmentDocumentsRecordPage recruitmentDocumentsRecordPage
        {
            get
            {
                if (_recruitmentDocumentsRecordPage == null)
                    _recruitmentDocumentsRecordPage = new RecruitmentDocumentsRecordPage(driver, wait, appURL);
                return _recruitmentDocumentsRecordPage;
            }
        }

        public ApplicantLanguageRecordPage applicantLanguageRecordPage
        {
            get
            {
                if (_applicantLanguageRecordPage == null)
                    _applicantLanguageRecordPage = new ApplicantLanguageRecordPage(driver, wait, appURL);
                return _applicantLanguageRecordPage;

            }
        }

        public ApplicantAliasRecordPage applicantAliasRecordPage
        {
            get
            {
                if (_applicantAliasRecordPage == null)
                    _applicantAliasRecordPage = new ApplicantAliasRecordPage(driver, wait, appURL);
                return _applicantAliasRecordPage;

            }
        }

        public RecruitmentDocumentAttachmentPage recruitmentDocumentAttachmentPage
        {
            get
            {
                if (_recruitmentDocumentAttachmentPage == null)
                    _recruitmentDocumentAttachmentPage = new RecruitmentDocumentAttachmentPage(driver, wait, appURL);
                return _recruitmentDocumentAttachmentPage;

            }
        }

        public RecruitmentDocumentAttachmentRecordPage recruitmentDocumentAttachmentRecordPage
        {
            get
            {
                if (_recruitmentDocumentAttachmentRecordPage == null)
                    _recruitmentDocumentAttachmentRecordPage = new RecruitmentDocumentAttachmentRecordPage(driver, wait, appURL);
                return _recruitmentDocumentAttachmentRecordPage;
            }
        }

        public RecruitmentDocumentManagementRecordPage recruitmentDocumentManagementRecordPage
        {
            get
            {
                if (_recruitmentDocumentManagementRecordPage == null)
                    _recruitmentDocumentManagementRecordPage = new RecruitmentDocumentManagementRecordPage(driver, wait, appURL);
                return _recruitmentDocumentManagementRecordPage;
            }
        }

        public OptionSetsPage optionSetsPage
        {
            get
            {
                if (_optionSetsPage == null)
                    _optionSetsPage = new OptionSetsPage(driver, wait, appURL);
                return _optionSetsPage;
            }
        }

        public OptionSetsRecordPage optionSetsRecordPage
        {
            get
            {
                if (_optionSetsRecordPage == null)
                    _optionSetsRecordPage = new OptionSetsRecordPage(driver, wait, appURL);
                return _optionSetsRecordPage;
            }
        }

        public OptionsetValuesPage optionsetValuesPage
        {
            get
            {
                if (_optionsetValuesPage == null)
                    _optionsetValuesPage = new OptionsetValuesPage(driver, wait, appURL);
                return _optionsetValuesPage;
            }
        }

        public OptionsetValueRecordPage optionsetValueRecordPage
        {
            get
            {
                if (_optionsetValueRecordPage == null)
                    _optionsetValueRecordPage = new OptionsetValueRecordPage(driver, wait, appURL);
                return _optionsetValueRecordPage;
            }
        }

        public TrainingCourseSetupPage trainingCourseSetupPage
        {
            get
            {
                if (_trainingCourseSetupPage == null)
                    _trainingCourseSetupPage = new TrainingCourseSetupPage(driver, wait, appURL);
                return _trainingCourseSetupPage;
            }
        }

        public TrainingCourseRecordPage trainingCourseRecordPage
        {
            get
            {
                if (_trainingCourseRecordPage == null)
                    _trainingCourseRecordPage = new TrainingCourseRecordPage(driver, wait, appURL);
                return _trainingCourseRecordPage;
            }
        }

        public ServiceElement2Page serviceElement2Page
        {
            get
            {
                if (_serviceElement2Page == null)
                    _serviceElement2Page = new ServiceElement2Page(driver, wait, appURL);
                return _serviceElement2Page;

            }
        }

        public ServiceElement2RecordPage serviceElement2RecordPage
        {
            get
            {
                if (_serviceElement2RecordPage == null)
                    _serviceElement2RecordPage = new ServiceElement2RecordPage(driver, wait, appURL);
                return _serviceElement2RecordPage;

            }
        }

        public ServicePermissionsPage servicePermissionsPage
        {
            get
            {
                if (_servicePermissionsPage == null)
                    _servicePermissionsPage = new ServicePermissionsPage(driver, wait, appURL);
                return _servicePermissionsPage;

            }
        }

        public ServicePermissionRecordPage servicePermissionRecordPage
        {
            get
            {
                if (_servicePermissionRecordPage == null)
                    _servicePermissionRecordPage = new ServicePermissionRecordPage(driver, wait, appURL);
                return _servicePermissionRecordPage;

            }
        }

        public ApprovedCareTypesPage approvedCareTypesPage
        {
            get
            {
                if (_approvedCareTypesPage == null)
                    _approvedCareTypesPage = new ApprovedCareTypesPage(driver, wait, appURL);
                return _approvedCareTypesPage;

            }
        }

        public ApprovedCareTypeRecordPage approvedCareTypeRecordPage
        {
            get
            {
                if (_approvedCareTypeRecordPage == null)
                    _approvedCareTypeRecordPage = new ApprovedCareTypeRecordPage(driver, wait, appURL);
                return _approvedCareTypeRecordPage;

            }
        }

        public GLCodeMappingsPage glCodeMappingsPage
        {
            get
            {
                if (_glCodeMappingsPage == null)
                    _glCodeMappingsPage = new GLCodeMappingsPage(driver, wait, appURL);
                return _glCodeMappingsPage;

            }
        }

        public GLCodeMappingsRecordPage glCodeMappingsRecordPage
        {
            get
            {
                if (_glCodeMappingsRecordPage == null)
                    _glCodeMappingsRecordPage = new GLCodeMappingsRecordPage(driver, wait, appURL);
                return _glCodeMappingsRecordPage;

            }
        }

        public AvailabilityTypesPage availabilityTypesPage
        {
            get
            {
                if (_availabilityTypesPage == null)
                    _availabilityTypesPage = new AvailabilityTypesPage(driver, wait, appURL);
                return _availabilityTypesPage;

            }
        }

        public AvailabilityTypeRecordPage availabilityTypeRecordPage
        {
            get
            {
                if (_availabilityTypeRecordPage == null)
                    _availabilityTypeRecordPage = new AvailabilityTypeRecordPage(driver, wait, appURL);
                return _availabilityTypeRecordPage;

            }
        }

        public LocalizedStringsPage localizedStringsPage
        {
            get
            {
                if (_localizedStringsPage == null)
                    _localizedStringsPage = new LocalizedStringsPage(driver, wait, appURL);
                return _localizedStringsPage;

            }
        }

        public LocalizedStringsRecordPage localizedStringsRecordPage
        {
            get
            {
                if (_localizedStringsRecordPage == null)
                    _localizedStringsRecordPage = new LocalizedStringsRecordPage(driver, wait, appURL);
                return _localizedStringsRecordPage;

            }
        }

        public LocalizedStringValuesPage localizedStringValuesPage
        {
            get
            {
                if (_localizedStringValuesPage == null)
                    _localizedStringValuesPage = new LocalizedStringValuesPage(driver, wait, appURL);
                return _localizedStringValuesPage;

            }
        }

        public LocalizedStringValuesRecordPage localizedStringValuesRecordPage
        {
            get
            {
                if (_localizedStringValuesRecordPage == null)
                    _localizedStringValuesRecordPage = new LocalizedStringValuesRecordPage(driver, wait, appURL);
                return _localizedStringValuesRecordPage;

            }
        }

        public TrainingRequirementSetupPage trainingRequirementSetupPage
        {
            get
            {
                if (_trainingRequirementSetupPage == null)
                    _trainingRequirementSetupPage = new TrainingRequirementSetupPage(driver, wait, appURL);
                return _trainingRequirementSetupPage;

            }
        }

        public ServiceProvidedRatePeriodsPage serviceProvidedRatePeriodsPage
        {
            get
            {
                if (_serviceProvidedRatePeriodsPage == null)
                    _serviceProvidedRatePeriodsPage = new ServiceProvidedRatePeriodsPage(driver, wait, appURL);
                return _serviceProvidedRatePeriodsPage;

            }
        }

        public ServiceProvidedRatePeriodRecordPage serviceProvidedRatePeriodRecordPage
        {
            get
            {
                if (_serviceProvidedRatePeriodRecordPage == null)
                    _serviceProvidedRatePeriodRecordPage = new ServiceProvidedRatePeriodRecordPage(driver, wait, appURL);
                return _serviceProvidedRatePeriodRecordPage;

            }
        }

        public ServiceProvidedRateSchedulesPage serviceProvidedRateSchedulesPage
        {
            get
            {
                if (_serviceProvidedRateSchedulesPage == null)
                    _serviceProvidedRateSchedulesPage = new ServiceProvidedRateSchedulesPage(driver, wait, appURL);
                return _serviceProvidedRateSchedulesPage;
            }
        }

        public ServiceFinanceSettingsPage serviceFinanceSettingsPage
        {
            get
            {
                if (_serviceFinanceSettingsPage == null)
                    _serviceFinanceSettingsPage = new ServiceFinanceSettingsPage(driver, wait, appURL);
                return _serviceFinanceSettingsPage;

            }
        }

        public ServiceProvidedRateScheduleRecordPage serviceProvidedRateScheduleRecordPage
        {
            get
            {
                if (_serviceProvidedRateScheduleRecordPage == null)
                    _serviceProvidedRateScheduleRecordPage = new ServiceProvidedRateScheduleRecordPage(driver, wait, appURL);
                return _serviceProvidedRateScheduleRecordPage;
            }
        }

        public ServiceFinanceSettingRecordPage serviceFinanceSettingRecordPage
        {
            get
            {
                if (_serviceFinanceSettingRecordPage == null)
                    _serviceFinanceSettingRecordPage = new ServiceFinanceSettingRecordPage(driver, wait, appURL);
                return _serviceFinanceSettingRecordPage;
            }
        }

        public ServiceProvisionRatePeriodsPage serviceProvisionRatePeriodsPage
        {
            get
            {
                if (_serviceProvisionRatePeriodsPage == null)
                    _serviceProvisionRatePeriodsPage = new ServiceProvisionRatePeriodsPage(driver, wait, appURL);
                return _serviceProvisionRatePeriodsPage;
            }
        }

        public ServiceProvisionRatePeriodRecordPage serviceProvisionRatePeriodRecordPage
        {
            get
            {
                if (_serviceProvisionRatePeriodRecordPage == null)
                    _serviceProvisionRatePeriodRecordPage = new ServiceProvisionRatePeriodRecordPage(driver, wait, appURL);
                return _serviceProvisionRatePeriodRecordPage;
            }
        }

        public ServiceProvisionRateSchedulePage serviceProvisionRateSchedulePage
        {
            get
            {
                if (_serviceProvisionRateSchedulePage == null)
                    _serviceProvisionRateSchedulePage = new ServiceProvisionRateSchedulePage(driver, wait, appURL);
                return _serviceProvisionRateSchedulePage;
            }
        }

        public ServiceProvisionRateScheduleRecordPage serviceProvisionRateScheduleRecordPage
        {
            get
            {
                if (_serviceProvisionRateScheduleRecordPage == null)
                    _serviceProvisionRateScheduleRecordPage = new ServiceProvisionRateScheduleRecordPage(driver, wait, appURL);
                return _serviceProvisionRateScheduleRecordPage;
            }
        }

        public RecurringHealthAppointmentsPage recurringHealthAppointmentsPage
        {
            get
            {
                if (_recurringHealthAppointmentsPage == null)
                    _recurringHealthAppointmentsPage = new RecurringHealthAppointmentsPage(driver, wait, appURL);
                return _recurringHealthAppointmentsPage;
            }
        }

        public RecurringHealthAppointmentRecordPage recurringHealthAppointmentRecordPage
        {
            get
            {
                if (_recurringHealthAppointmentRecordPage == null)
                    _recurringHealthAppointmentRecordPage = new RecurringHealthAppointmentRecordPage(driver, wait, appURL);
                return _recurringHealthAppointmentRecordPage;
            }
        }

        public ProviderCarerSearchPopup providerCarerSearchPopup
        {
            get
            {
                if (_providerCarerSearchPopup == null)
                    _providerCarerSearchPopup = new ProviderCarerSearchPopup(driver, wait, appURL);
                return _providerCarerSearchPopup;
            }
        }

        public CalendarDatePicker calendarDatePicker
        {
            get
            {
                if (_calendarDatePicker == null)
                    _calendarDatePicker = new CalendarDatePicker(driver, wait, appURL);
                return _calendarDatePicker;
            }
        }



        public FinanceInvoiceBatchSetupPage financeInvoiceBatchSetupPage
        {
            get
            {
                if (_financeInvoiceBatchSetupPage == null)
                    _financeInvoiceBatchSetupPage = new FinanceInvoiceBatchSetupPage(driver, wait, appURL);
                return _financeInvoiceBatchSetupPage;
            }
        }

        public FinanceInvoiceBatchSetupRecordPage financeInvoiceBatchSetupRecordPage
        {
            get
            {
                if (_financeInvoiceBatchSetupRecordPage == null)
                    _financeInvoiceBatchSetupRecordPage = new FinanceInvoiceBatchSetupRecordPage(driver, wait, appURL);
                return _financeInvoiceBatchSetupRecordPage;
            }
        }

        public FinanceInvoiceBatchesPage financeInvoiceBatchesPage
        {
            get
            {
                if (_financeInvoiceBatchesPage == null)
                    _financeInvoiceBatchesPage = new FinanceInvoiceBatchesPage(driver, wait, appURL);
                return _financeInvoiceBatchesPage;
            }
        }

        public ServiceUprateRecordPage serviceUprateRecordPage
        {
            get
            {
                if (_serviceUprateRecordPage == null)
                    _serviceUprateRecordPage = new ServiceUprateRecordPage(driver, wait, appURL);
                return _serviceUprateRecordPage;
            }
        }

        public ServiceUpratesPage serviceUpratesPage
        {
            get
            {
                if (_serviceUpratesPage == null)
                    _serviceUpratesPage = new ServiceUpratesPage(driver, wait, appURL);
                return _serviceUpratesPage;
            }
        }

        public FinanceInvoiceBatchRecordPage financeInvoiceBatchRecordPage
        {
            get
            {
                if (_financeInvoiceBatchRecordPage == null)
                    _financeInvoiceBatchRecordPage = new FinanceInvoiceBatchRecordPage(driver, wait, appURL);
                return _financeInvoiceBatchRecordPage;
            }
        }

        public FinanceTransactionsPage financeTransactionsPage
        {
            get
            {
                if (_financeTransactionsPage == null)
                    _financeTransactionsPage = new FinanceTransactionsPage(driver, wait, appURL);
                return _financeTransactionsPage;
            }
        }

        public FinanceInvoicesPage financeInvoicesPage
        {
            get
            {
                if (_financeInvoicesPage == null)
                    _financeInvoicesPage = new FinanceInvoicesPage(driver, wait, appURL);
                return _financeInvoicesPage;
            }
        }

        public FinanceInvoiceRecordPage financeInvoiceRecordPage
        {
            get
            {
                if (_financeInvoiceRecordPage == null)
                    _financeInvoiceRecordPage = new FinanceInvoiceRecordPage(driver, wait, appURL);
                return _financeInvoiceRecordPage;
            }
        }

        public AdditionalTransactionsFinanceTransactionsPage additionalTransactionsFinanceTransactionsPage
        {
            get
            {
                if (_additionalTransactionsFinanceTransactionsPage == null)
                    _additionalTransactionsFinanceTransactionsPage = new AdditionalTransactionsFinanceTransactionsPage(driver, wait, appURL);
                return _additionalTransactionsFinanceTransactionsPage;
            }
        }

        public FinanceTransactionRecordPage financeTransactionRecordPage
        {
            get
            {
                if (_financeTransactionRecordPage == null)
                    _financeTransactionRecordPage = new FinanceTransactionRecordPage(driver, wait, appURL);
                return _financeTransactionRecordPage;
            }
        }

        public FinanceTransactionTriggersPage financeTransactionTriggersPage
        {
            get
            {
                if (_financeTransactionTriggersPage == null)
                    _financeTransactionTriggersPage = new FinanceTransactionTriggersPage(driver, wait, appURL);
                return _financeTransactionTriggersPage;
            }
        }

        public ContactReasonsPage contactReasonsPage
        {
            get
            {
                if (_contactReasonsPage == null)
                    _contactReasonsPage = new ContactReasonsPage(driver, wait, appURL);
                return _contactReasonsPage;
            }
        }

        public ContactReasonRecordPage contactReasonRecordPage
        {
            get
            {
                if (_contactReasonRecordPage == null)
                    _contactReasonRecordPage = new ContactReasonRecordPage(driver, wait, appURL);
                return _contactReasonRecordPage;
            }
        }

        public FinanceExtractsPage financeExtractsPage
        {
            get
            {
                if (_financeExtractsPage == null)
                    _financeExtractsPage = new FinanceExtractsPage(driver, wait, appURL);
                return _financeExtractsPage;
            }
        }

        public FinanceExtractRecordPage financeExtractRecordPage
        {
            get
            {
                if (_financeExtractRecordPage == null)
                    _financeExtractRecordPage = new FinanceExtractRecordPage(driver, wait, appURL);
                return _financeExtractRecordPage;
            }
        }

        public TotalsPage totalsPage
        {
            get
            {
                if (_totalsPage == null)
                    _totalsPage = new TotalsPage(driver, wait, appURL);
                return _totalsPage;
            }
        }

        public RTTPathwaysSetupPage rttPathwaysSetupPage
        {
            get
            {
                if (_rttPathwaysSetupPage == null)
                    _rttPathwaysSetupPage = new RTTPathwaysSetupPage(driver, wait, appURL);
                return _rttPathwaysSetupPage;
            }
        }

        public RTTPathwaysSetupRecordPage rttPathwaysSetupRecordPage
        {
            get
            {
                if (_rttPathwaysSetupRecordPage == null)
                    _rttPathwaysSetupRecordPage = new RTTPathwaysSetupRecordPage(driver, wait, appURL);
                return _rttPathwaysSetupRecordPage;
            }
        }

        public RTTTreatmentFunctionsPage rttTreatmentFunctionsPage
        {
            get
            {
                if (_rttTreatmentFunctionsPage == null)
                    _rttTreatmentFunctionsPage = new RTTTreatmentFunctionsPage(driver, wait, appURL);
                return _rttTreatmentFunctionsPage;
            }
        }

        public RTTTreatmentFunctionRecordPage rttTreatmentFunctionRecordPage
        {
            get
            {
                if (_rttTreatmentFunctionRecordPage == null)
                    _rttTreatmentFunctionRecordPage = new RTTTreatmentFunctionRecordPage(driver, wait, appURL);
                return _rttTreatmentFunctionRecordPage;
            }
        }

        public CareProviderStaffRoleTypesPage careProviderStaffRoleTypesPage
        {
            get
            {
                if (_careProviderStaffRoleTypesPage == null)
                    _careProviderStaffRoleTypesPage = new CareProviderStaffRoleTypesPage(driver, wait, appURL);
                return _careProviderStaffRoleTypesPage;
            }
        }

        public CareProviderStaffRoleTypeRecordPage careProviderStaffRoleTypeRecordPage
        {
            get
            {
                if (_careProviderStaffRoleTypeRecordPage == null)
                    _careProviderStaffRoleTypeRecordPage = new CareProviderStaffRoleTypeRecordPage(driver, wait, appURL);
                return _careProviderStaffRoleTypeRecordPage;
            }
        }

        public CareProviderStaffRoleTypeGroupsPage careProviderStaffRoleTypeGroupsPage
        {
            get
            {
                if (_careProviderStaffRoleTypeGroupsPage == null)
                    _careProviderStaffRoleTypeGroupsPage = new CareProviderStaffRoleTypeGroupsPage(driver, wait, appURL);
                return _careProviderStaffRoleTypeGroupsPage;
            }
        }

        public CareProviderStaffRoleTypeGroupRecordPage careProviderStaffRoleTypeGroupRecordPage
        {
            get
            {
                if (_careProviderStaffRoleTypeGroupRecordPage == null)
                    _careProviderStaffRoleTypeGroupRecordPage = new CareProviderStaffRoleTypeGroupRecordPage(driver, wait, appURL);
                return _careProviderStaffRoleTypeGroupRecordPage;
            }
        }


        public RateUnitsPage rateUnitsPage
        {
            get
            {
                if (_rateUnitsPage == null)
                    _rateUnitsPage = new RateUnitsPage(driver, wait, appURL);
                return _rateUnitsPage;
            }
        }

        public RateUnitRecordPage rateUnitRecordPage
        {
            get
            {
                if (_rateUnitRecordPage == null)
                    _rateUnitRecordPage = new RateUnitRecordPage(driver, wait, appURL);
                return _rateUnitRecordPage;
            }
        }

        public BankHolidayChargingCalendarsPage bankHolidayChargingCalendarsPage
        {
            get
            {
                if (_bankHolidayChargingCalendarsPage == null)
                    _bankHolidayChargingCalendarsPage = new BankHolidayChargingCalendarsPage(driver, wait, appURL);
                return _bankHolidayChargingCalendarsPage;
            }
        }

        public BankHolidayChargingCalendarRecordPage bankHolidayChargingCalendarRecordPage
        {
            get
            {
                if (_bankHolidayChargingCalendarRecordPage == null)
                    _bankHolidayChargingCalendarRecordPage = new BankHolidayChargingCalendarRecordPage(driver, wait, appURL);
                return _bankHolidayChargingCalendarRecordPage;
            }
        }

        public BankHolidayDatesPage bankHolidayDatesPage
        {
            get
            {
                if (_bankHolidayDatesPage == null)
                    _bankHolidayDatesPage = new BankHolidayDatesPage(driver, wait, appURL);
                return _bankHolidayDatesPage;
            }
        }

        public BankHolidayDateRecordPage bankHolidayDateRecordPage
        {
            get
            {
                if (_bankHolidayDateRecordPage == null)
                    _bankHolidayDateRecordPage = new BankHolidayDateRecordPage(driver, wait, appURL);
                return _bankHolidayDateRecordPage;
            }
        }

        public PronounsPage pronounsPage
        {
            get
            {
                if (_pronounsPage == null)
                    _pronounsPage = new PronounsPage(driver, wait, appURL);
                return _pronounsPage;
            }
        }

        public PronounsRecordPage pronounsRecordPage
        {
            get
            {
                if (_pronounsRecordPage == null)
                    _pronounsRecordPage = new PronounsRecordPage(driver, wait, appURL);
                return _pronounsRecordPage;
            }
        }

        public DataFormsPage dataFormsPage
        {
            get
            {
                if (_dataFormsPage == null)
                    _dataFormsPage = new DataFormsPage(driver, wait, appURL);
                return _dataFormsPage;
            }
        }

        public DataFormRecordPage dataFormRecordPage
        {
            get
            {
                if (_dataFormRecordPage == null)
                    _dataFormRecordPage = new DataFormRecordPage(driver, wait, appURL);
                return _dataFormRecordPage;
            }
        }

        public DataFormEditPage dataFormEditPage
        {
            get
            {
                if (_dataFormEditPage == null)
                    _dataFormEditPage = new DataFormEditPage(driver, wait, appURL);
                return _dataFormEditPage;
            }
        }

        public DataFormFieldPage dataFormFieldPage
        {
            get
            {
                if (_dataFormFieldPage == null)
                    _dataFormFieldPage = new DataFormFieldPage(driver, wait, appURL);
                return _dataFormFieldPage;
            }
        }

        public AllRoleApplicationsPage allRoleApplicationsPage
        {
            get
            {
                if (_allRoleApplicationsPage == null)
                    _allRoleApplicationsPage = new AllRoleApplicationsPage(driver, wait, appURL);
                return _allRoleApplicationsPage;
            }
        }

        public ContractSchemesPage contractSchemesPage
        {
            get
            {
                if (_contractSchemesPage == null)
                    _contractSchemesPage = new ContractSchemesPage(driver, wait, appURL);
                return _contractSchemesPage;
            }
        }

        public ContractSchemeRecordPage contractSchemeRecordPage
        {
            get
            {
                if (_contractSchemeRecordPage == null)
                    _contractSchemeRecordPage = new ContractSchemeRecordPage(driver, wait, appURL);
                return _contractSchemeRecordPage;

            }
        }

        public CPFinanceAdminPage cpFinanceAdminPage
        {
            get
            {
                if (_cpFinanceAdminPage == null)
                    _cpFinanceAdminPage = new CPFinanceAdminPage(driver, wait, appURL);
                return _cpFinanceAdminPage;
            }
        }

        public ServicesPage servicesPage
        {
            get
            {
                if (_servicesPage == null)
                    _servicesPage = new ServicesPage(driver, wait, appURL);
                return _servicesPage;
            }
        }

        public ServiceRecordPage serviceRecordPage
        {
            get
            {
                if (_serviceRecordPage == null)
                    _serviceRecordPage = new ServiceRecordPage(driver, wait, appURL);
                return _serviceRecordPage;
            }
        }

        public CPServiceMappingsPage cpServiceMappingsPage
        {
            get
            {
                if (_cpServiceMappingsPage == null)
                    _cpServiceMappingsPage = new CPServiceMappingsPage(driver, wait, appURL);
                return _cpServiceMappingsPage;
            }
        }

        public FinanceCodeLocationsPage financeCodeLocationsPage
        {
            get
            {
                if (_financeCodeLocationsPage == null)
                    _financeCodeLocationsPage = new FinanceCodeLocationsPage(driver, wait, appURL);
                return _financeCodeLocationsPage;
            }
        }

        public FinanceCodeMappingsPage financeCodeMappingsPage
        {
            get
            {
                if (_financeCodeMappingsPage == null)
                    _financeCodeMappingsPage = new FinanceCodeMappingsPage(driver, wait, appURL);
                return _financeCodeMappingsPage;
            }
        }

        public FinanceCodeMappingRecordPage financeCodeMappingRecordPage
        {
            get
            {
                if (_financeCodeMappingRecordPage == null)
                    _financeCodeMappingRecordPage = new FinanceCodeMappingRecordPage(driver, wait, appURL);
                return _financeCodeMappingRecordPage;
            }
        }

        public ServiceDetailsPage serviceDetailsPage
        {
            get
            {
                if (_serviceDetailsPage == null)
                    _serviceDetailsPage = new ServiceDetailsPage(driver, wait, appURL);
                return _serviceDetailsPage;
            }
        }

        public ServiceDetailsRecordPage serviceDetailsRecordPage
        {
            get
            {
                if (_serviceDetailsRecordPage == null)
                    _serviceDetailsRecordPage = new ServiceDetailsRecordPage(driver, wait, appURL);
                return _serviceDetailsRecordPage;
            }
        }

        public CPServiceMappingRecordPage cpServiceMappingRecordPage
        {
            get
            {
                if (_cpServiceMappingRecordPage == null)
                    _cpServiceMappingRecordPage = new CPServiceMappingRecordPage(driver, wait, appURL);
                return _cpServiceMappingRecordPage;
            }
        }

        public PersonContractServicesPage personContractServicesPage
        {
            get
            {
                if (_personContractServicesPage == null)
                    _personContractServicesPage = new PersonContractServicesPage(driver, wait, appURL);
                return _personContractServicesPage;
            }
        }

        public PersonContractServiceRecordPage personContractServiceRecordPage
        {
            get
            {
                if (_personContractServiceRecordPage == null)
                    _personContractServiceRecordPage = new PersonContractServiceRecordPage(driver, wait, appURL);
                return _personContractServiceRecordPage;
            }
        }

        public CPFinanceExtractBatchSetupsPage cpFinanceExtractBatchSetupsPage
        {
            get
            {
                if (_cpFinanceExtractBatchSetupsPage == null)
                    _cpFinanceExtractBatchSetupsPage = new CPFinanceExtractBatchSetupsPage(driver, wait, appURL);
                return _cpFinanceExtractBatchSetupsPage;
            }
        }

        public CPFinanceExtractBatchSetupRecordPage cpFinanceExtractBatchSetupRecordPage
        {
            get
            {
                if (_cpFinanceExtractBatchSetupRecordPage == null)
                    _cpFinanceExtractBatchSetupRecordPage = new CPFinanceExtractBatchSetupRecordPage(driver, wait, appURL);
                return _cpFinanceExtractBatchSetupRecordPage;
            }
        }

        public CPFinanceExtractBatchesPage cpFinanceExtractBatchesPage
        {
            get
            {
                if (_cpFinanceExtractBatchesPage == null)
                    _cpFinanceExtractBatchesPage = new CPFinanceExtractBatchesPage(driver, wait, appURL);
                return _cpFinanceExtractBatchesPage;
            }
        }

        public CPFinanceExtractBatchRecordPage cpFinanceExtractBatchRecordPage
        {
            get
            {
                if (_cpFinanceExtractBatchRecordPage == null)
                    _cpFinanceExtractBatchRecordPage = new CPFinanceExtractBatchRecordPage(driver, wait, appURL);
                return _cpFinanceExtractBatchRecordPage;
            }
        }

        public CalendarTimePicker calendarTimePicker
        {
            get
            {
                if (_calendarTimePicker == null)
                    _calendarTimePicker = new CalendarTimePicker(driver, wait, appURL);
                return _calendarTimePicker;
            }
        }

        public PersonContractServiceRatePeriodsPage personContractServiceRatePeriodsPage
        {
            get
            {
                if (_personContractServiceRatePeriodsPage == null)
                    _personContractServiceRatePeriodsPage = new PersonContractServiceRatePeriodsPage(driver, wait, appURL);
                return _personContractServiceRatePeriodsPage;
            }
        }

        public PersonContractServiceRatePeriodRecordPage personContractServiceRatePeriodRecordPage
        {
            get
            {
                if (_personContractServiceRatePeriodRecordPage == null)
                    _personContractServiceRatePeriodRecordPage = new PersonContractServiceRatePeriodRecordPage(driver, wait, appURL);
                return _personContractServiceRatePeriodRecordPage;
            }
        }

        public CPFinanceInvoicesPage cpFinanceInvoicesPage
        {
            get
            {
                if (_cpFinanceInvoicesPage == null)
                    _cpFinanceInvoicesPage = new CPFinanceInvoicesPage(driver, wait, appURL);
                return _cpFinanceInvoicesPage;
            }
        }

        public CPFinanceTransactionsPage cpFinanceTransactionsPage
        {
            get
            {
                if (_cpFinanceTransactionsPage == null)
                    _cpFinanceTransactionsPage = new CPFinanceTransactionsPage(driver, wait, appURL);
                return _cpFinanceTransactionsPage;
            }
        }

        public CPFinanceInvoiceBatchesPage cpFinanceInvoiceBatchesPage
        {
            get
            {
                if (_cpFinanceInvoiceBatchesPage == null)
                    _cpFinanceInvoiceBatchesPage = new CPFinanceInvoiceBatchesPage(driver, wait, appURL);
                return _cpFinanceInvoiceBatchesPage;
            }
        }

        public CPFinanceInvoiceBatchRecordPage cpFinanceInvoiceBatchRecordPage
        {
            get
            {
                if (_cpFinanceInvoiceBatchRecordPage == null)
                    _cpFinanceInvoiceBatchRecordPage = new CPFinanceInvoiceBatchRecordPage(driver, wait, appURL);
                return _cpFinanceInvoiceBatchRecordPage;
            }
        }

        public CreateInvoiceFilePopup createInvoiceFilePopup
        {
            get
            {
                if (_createInvoiceFilePopup == null)
                    _createInvoiceFilePopup = new CreateInvoiceFilePopup(driver, wait, appURL);
                return _createInvoiceFilePopup;
            }
        }

        public PersonContractServiceChargesPerWeekPage personContractServiceChargesPerWeekPage
        {
            get
            {
                if (_personContractServiceChargesPerWeekPage == null)
                    _personContractServiceChargesPerWeekPage = new PersonContractServiceChargesPerWeekPage(driver, wait, appURL);
                return _personContractServiceChargesPerWeekPage;
            }
        }

        public PersonContractServiceChargesPerWeekRecordPage personContractServiceChargesPerWeekRecordPage
        {
            get
            {
                if (_personContractServiceChargesPerWeekRecordPage == null)
                    _personContractServiceChargesPerWeekRecordPage = new PersonContractServiceChargesPerWeekRecordPage(driver, wait, appURL);
                return _personContractServiceChargesPerWeekRecordPage;
            }
        }

        public CareProviderSchedulingSetupPage careProviderSchedulingSetupPage
        {
            get
            {
                if (_careProviderSchedulingSetupPage == null)
                    _careProviderSchedulingSetupPage = new CareProviderSchedulingSetupPage(driver, wait, appURL);
                return _careProviderSchedulingSetupPage;
            }
        }

        public CareProviderSchedulingSetupRecordPage careProviderSchedulingSetupRecordPage
        {
            get
            {
                if (_careProviderSchedulingSetupRecordPage == null)
                    _careProviderSchedulingSetupRecordPage = new CareProviderSchedulingSetupRecordPage(driver, wait, appURL);
                return _careProviderSchedulingSetupRecordPage;
            }
        }

        public CareProviderBookingScheduleRecordPage careProviderBookingScheduleRecordPage
        {
            get
            {
                if (_careProviderBookingScheduleRecordPage == null)
                    _careProviderBookingScheduleRecordPage = new CareProviderBookingScheduleRecordPage(driver, wait, appURL);
                return _careProviderBookingScheduleRecordPage;
            }
        }

        public BookingScheduleStaffRecordPage bookingScheduleStaffRecordPage
        {
            get
            {
                if (_bookingScheduleStaffRecordPage == null)
                    _bookingScheduleStaffRecordPage = new BookingScheduleStaffRecordPage(driver, wait, appURL);
                return _bookingScheduleStaffRecordPage;
            }
        }

        public BookingScheduleDeletionReasonsPage bookingScheduleDeletionReasonsPage
        {
            get
            {
                if (_bookingScheduleDeletionReasonsPage == null)
                    _bookingScheduleDeletionReasonsPage = new BookingScheduleDeletionReasonsPage(driver, wait, appURL);
                return _bookingScheduleDeletionReasonsPage;
            }
        }

        public BookingScheduleDeletionReasonsRecordPage bookingScheduleDeletionReasonsRecordPage
        {
            get
            {
                if (_bookingScheduleDeletionReasonsRecordPage == null)
                    _bookingScheduleDeletionReasonsRecordPage = new BookingScheduleDeletionReasonsRecordPage(driver, wait, appURL);
                return _bookingScheduleDeletionReasonsRecordPage;

            }
        }

        public PublicHolidaysPage publicHolidaysPage
        {
            get
            {
                if (_publicHolidaysPage == null)
                    _publicHolidaysPage = new PublicHolidaysPage(driver, wait, appURL);
                return _publicHolidaysPage;
            }
        }

        public PublicHolidayRecordPage publicHolidayRecordPage
        {
            get
            {
                if (_publicHolidayRecordPage == null)
                    _publicHolidayRecordPage = new PublicHolidayRecordPage(driver, wait, appURL);
                return _publicHolidayRecordPage;
            }
        }

        public ExpressBookingResultsPage expressBookingResultsPage
        {
            get
            {
                if (_expressBookingResultsPage == null)
                    _expressBookingResultsPage = new ExpressBookingResultsPage(driver, wait, appURL);
                return _expressBookingResultsPage;
            }
        }

        public ExpressBookingResultRecordPage expressBookingResultRecordPage
        {
            get
            {
                if (_expressBookingResultRecordPage == null)
                    _expressBookingResultRecordPage = new ExpressBookingResultRecordPage(driver, wait, appURL);
                return _expressBookingResultRecordPage;
            }
        }

        public BookingTypesPage bookingTypesPage
        {
            get
            {
                if (_bookingTypesPage == null)
                    _bookingTypesPage = new BookingTypesPage(driver, wait, appURL);
                return _bookingTypesPage;
            }
        }

        public BookingTypeRecordPage bookingTypeRecordPage
        {
            get
            {
                if (_bookingTypeRecordPage == null)
                    _bookingTypeRecordPage = new BookingTypeRecordPage(driver, wait, appURL);
                return _bookingTypeRecordPage;
            }
        }

        public BookingTypeClashActionsPage bookingTypeClashActionsPage
        {
            get
            {
                if (_bookingTypeClashActionsPage == null)
                    _bookingTypeClashActionsPage = new BookingTypeClashActionsPage(driver, wait, appURL);
                return _bookingTypeClashActionsPage;
            }
        }

        public BookingTypeClashActionRecordPage bookingTypeClashActionRecordPage
        {
            get
            {
                if (_bookingTypeClashActionRecordPage == null)
                    _bookingTypeClashActionRecordPage = new BookingTypeClashActionRecordPage(driver, wait, appURL);
                return _bookingTypeClashActionRecordPage;
            }
        }

        public BookingDiaryStaffRecordPage bookingDiaryStaffRecordPage
        {
            get
            {
                if (_bookingDiaryStaffRecordPage == null)
                    _bookingDiaryStaffRecordPage = new BookingDiaryStaffRecordPage(driver, wait, appURL);
                return _bookingDiaryStaffRecordPage;
            }
        }

        public DrawerDialogPopup drawerDialogPopup
        {
            get
            {
                if (_drawerDialogPopup == null)
                    _drawerDialogPopup = new DrawerDialogPopup(driver, wait, appURL);
                return _drawerDialogPopup;
            }
        }

        public BookingDiaryDeletionReasonsPage bookingDiaryDeletionReasonsPage
        {
            get
            {
                if (_bookingDiaryDeletionReasonsPage == null)
                    _bookingDiaryDeletionReasonsPage = new BookingDiaryDeletionReasonsPage(driver, wait, appURL);
                return _bookingDiaryDeletionReasonsPage;
            }
        }

        public BookingDiaryDeletionReasonsRecordPage bookingDiaryDeletionReasonsRecordPage
        {
            get
            {
                if (_bookingDiaryDeletionReasonsRecordPage == null)
                    _bookingDiaryDeletionReasonsRecordPage = new BookingDiaryDeletionReasonsRecordPage(driver, wait, appURL);
                return _bookingDiaryDeletionReasonsRecordPage;
            }
        }

        public EmployeeSchedulePage employeeSchedulePage
        {
            get
            {
                if (_employeeSchedulePage == null)
                    _employeeSchedulePage = new EmployeeSchedulePage(driver, wait, appURL);
                return _employeeSchedulePage;
            }
        }

        public PeopleSchedulePage peopleSchedulePage
        {
            get
            {
                if (_peopleSchedulePage == null)
                    _peopleSchedulePage = new PeopleSchedulePage(driver, wait, appURL);
                return _peopleSchedulePage;
            }
        }

        public ChargeApportionmentsPage chargeApportionmentsPage
        {
            get
            {
                if (_chargeApportionmentsPage == null)
                    _chargeApportionmentsPage = new ChargeApportionmentsPage(driver, wait, appURL);
                return _chargeApportionmentsPage;
            }
        }

        public ChargeApportionmentRecordPage chargeApportionmentRecordPage
        {
            get
            {
                if (_chargeApportionmentRecordPage == null)
                    _chargeApportionmentRecordPage = new ChargeApportionmentRecordPage(driver, wait, appURL);
                return _chargeApportionmentRecordPage;
            }
        }

        public CPFinanceInvoiceBatchSetupsPage cpFinanceInvoiceBatchSetupsPage
        {
            get
            {
                if (_cpFinanceInvoiceBatchSetupsPage == null)
                    _cpFinanceInvoiceBatchSetupsPage = new CPFinanceInvoiceBatchSetupsPage(driver, wait, appURL);
                return _cpFinanceInvoiceBatchSetupsPage;
            }
        }

        public CPFinanceInvoiceBatchSetupRecordPage cpFinanceInvoiceBatchSetupRecordPage
        {
            get
            {
                if (_cpFinanceInvoiceBatchSetupRecordPage == null)
                    _cpFinanceInvoiceBatchSetupRecordPage = new CPFinanceInvoiceBatchSetupRecordPage(driver, wait, appURL);
                return _cpFinanceInvoiceBatchSetupRecordPage;
            }
        }

        public ChargeApportionmentDetailsPage chargeApportionmentDetailsPage
        {
            get
            {
                if (_chargeApportionmentDetailsPage == null)
                    _chargeApportionmentDetailsPage = new ChargeApportionmentDetailsPage(driver, wait, appURL);
                return _chargeApportionmentDetailsPage;
            }
        }

        public ChargeApportionmentDetailRecordPage chargeApportionmentDetailRecordPage
        {
            get
            {
                if (_chargeApportionmentDetailRecordPage == null)
                    _chargeApportionmentDetailRecordPage = new ChargeApportionmentDetailRecordPage(driver, wait, appURL);
                return _chargeApportionmentDetailRecordPage;
            }
        }

        public CPFinanceTransactionRecordPage cpFinanceTransactionRecordPage
        {
            get
            {
                if (_cpFinanceTransactionRecordPage == null)
                    _cpFinanceTransactionRecordPage = new CPFinanceTransactionRecordPage(driver, wait, appURL);
                return _cpFinanceTransactionRecordPage;
            }
        }

        public CPFinanceTransactionTriggersPage cpFinanceTransactionTriggersPage
        {
            get
            {
                if (_cpFinanceTransactionTriggersPage == null)
                    _cpFinanceTransactionTriggersPage = new CPFinanceTransactionTriggersPage(driver, wait, appURL);
                return _cpFinanceTransactionTriggersPage;
            }
        }

        public CPFinanceTransactionTriggerRecordPage cpFinanceTransactionTriggerRecordPage
        {
            get
            {
                if (_cpFinanceTransactionTriggerRecordPage == null)
                    _cpFinanceTransactionTriggerRecordPage = new CPFinanceTransactionTriggerRecordPage(driver, wait, appURL);
                return _cpFinanceTransactionTriggerRecordPage;
            }
        }

        public CPFinanceInvoicePaymentsPage cpFinanceInvoicePaymentsPage
        {
            get
            {
                if (_cpFinanceInvoicePaymentsPage == null)
                    _cpFinanceInvoicePaymentsPage = new CPFinanceInvoicePaymentsPage(driver, wait, appURL);
                return _cpFinanceInvoicePaymentsPage;
            }
        }

        public CPFinanceInvoicePaymentRecordPage cpFinanceInvoicePaymentRecordPage
        {
            get
            {
                if (_cpFinanceInvoicePaymentRecordPage == null)
                    _cpFinanceInvoicePaymentRecordPage = new CPFinanceInvoicePaymentRecordPage(driver, wait, appURL);
                return _cpFinanceInvoicePaymentRecordPage;
            }
        }

        public PaymentAllocatePage paymentAllocatePage
        {
            get
            {
                if (_paymentAllocatePage == null)
                    _paymentAllocatePage = new PaymentAllocatePage(driver, wait, appURL);
                return _paymentAllocatePage;
            }
        }

        public UpliftRatesforMasterPayArrangementsPopup upliftRatesforMasterPayArrangementsPopup
        {
            get
            {
                if (_upliftRatesforMasterPayArrangementsPopup == null)
                    _upliftRatesforMasterPayArrangementsPopup = new UpliftRatesforMasterPayArrangementsPopup(driver, wait, appURL);
                return _upliftRatesforMasterPayArrangementsPopup;
            }
        }

        public PersonKeyworkerNotesPage personKeyworkerNotesPage
        {
            get
            {
                if (_personKeyworkerNotesPage == null)
                    _personKeyworkerNotesPage = new PersonKeyworkerNotesPage(driver, wait, appURL);
                return _personKeyworkerNotesPage;
            }
        }

        public PersonKeyworkerNoteRecordPage personKeyworkerNoteRecordPage
        {
            get
            {
                if (_personKeyworkerNoteRecordPage == null)
                    _personKeyworkerNoteRecordPage = new PersonKeyworkerNoteRecordPage(driver, wait, appURL);
                return _personKeyworkerNoteRecordPage;
            }
        }

        public KeyworkerNotesAttachmentsPage keyworkerNotesAttachmentsPage
        {
            get
            {
                if (_keyworkerNotesAttachmentsPage == null)
                    _keyworkerNotesAttachmentsPage = new KeyworkerNotesAttachmentsPage(driver, wait, appURL);
                return _keyworkerNotesAttachmentsPage;
            }
        }

        public KeyworkerNotesAttachmentRecordPage keyworkerNotesAttachmentRecordPage
        {
            get
            {
                if (_keyworkerNotesAttachmentRecordPage == null)
                    _keyworkerNotesAttachmentRecordPage = new KeyworkerNotesAttachmentRecordPage(driver, wait, appURL);
                return _keyworkerNotesAttachmentRecordPage;
            }
        }

        public MobilityAttachmentsPage mobilityAttachmentsPage
        {
            get
            {
                if (_mobilityAttachmentsPage == null)
                    _mobilityAttachmentsPage = new MobilityAttachmentsPage(driver, wait, appURL);
                return _mobilityAttachmentsPage;
            }
        }

        public MobilityAttachmentRecordPage mobilityAttachmentRecordPage
        {
            get
            {
                if (_mobilityAttachmentRecordPage == null)
                    _mobilityAttachmentRecordPage = new MobilityAttachmentRecordPage(driver, wait, appURL);
                return _mobilityAttachmentRecordPage;
            }
        }

        public IncidentTriggersPage incidentTriggersPage
        {
            get
            {
                if (_incidentTriggersPage == null)
                    _incidentTriggersPage = new IncidentTriggersPage(driver, wait, appURL);
                return _incidentTriggersPage;
            }
        }

        public CareTasksPage careTasksPage
        {
            get
            {
                if (_careTasksPage == null)
                    _careTasksPage = new CareTasksPage(driver, wait, appURL);
                return _careTasksPage;
            }
        }

        public ConversationsPage conversationsPage
        {
            get
            {
                if (_conversationsPage == null)
                    _conversationsPage = new ConversationsPage(driver, wait, appURL);
                return _conversationsPage;
            }
        }

        public ConversationRecordPage conversationRecordPage
        {
            get
            {
                if (_conversationRecordPage == null)
                    _conversationRecordPage = new ConversationRecordPage(driver, wait, appURL);
                return _conversationRecordPage;
            }
        }

        public HandoverCommentsPage handoverCommentsPage
        {
            get
            {
                if (_handoverCommentsPage == null)
                    _handoverCommentsPage = new HandoverCommentsPage(driver, wait, appURL);
                return _handoverCommentsPage;
            }
        }

        public HandoverCommentRecordPage handoverCommentRecordPage
        {
            get
            {
                if (_handoverCommentRecordPage == null)
                    _handoverCommentRecordPage = new HandoverCommentRecordPage(driver, wait, appURL);
                return _handoverCommentRecordPage;
            }
        }

        public TypeOfSensorPage typeOfSensorPage
        {
            get
            {
                if (_typeOfSensorPage == null)
                    _typeOfSensorPage = new TypeOfSensorPage(driver, wait, appURL);
                return _typeOfSensorPage;
            }
        }


        #endregion


        private TestContext testContextInstance;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestInitialize()]
        public void SetupTest()
        {
            this.ShutDownAllProcesses();

            try
            {
                fileIOHelper = new Framework.FileSystem.FileIOHelper();
                msWordHelper = new Framework.OfficeTools.MSWordHelper();
                pdfHelper = new Framework.PDFTools.PdfHelper();
                imageHelper = new Framework.Image.ImageHelper();
                commonMethodsHelper = new Framework.CommonMethods.CommonMethods();

                DownloadsDirectory = TestContext.TestRunDirectory + "\\Downloads";
                fileIOHelper.CreateDirectoryAndRemoveFiles(DownloadsDirectory);

                this.browser = ConfigurationManager.AppSettings["browser"];
                this.SetDriver(browser, DownloadsDirectory);

                WebAPIHelper = new Framework.WebAppAPI.WebAPIHelper();
                dbHelper = new Phoenix.DBHelper.DatabaseHelper();
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                appURL = ConfigurationManager.AppSettings["appURL"];
                defaultTimeoutSeconds = int.Parse(ConfigurationManager.AppSettings["DefaultTimeoutSeconds"]);
                wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 0, defaultTimeoutSeconds));
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        [TestCleanup()]
        public virtual void MyTestCleanup()
        {
            bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

            TakeScreeshots(testPassed);

            SendTestResultsToZephyr(testPassed);

            driver.Quit();

            ShutDownAllProcesses();
        }

        private void SetCorrectConfigurationBaseOnJiraTest(string JiraIssueID)
        {
            zapi = new AtlassianServiceAPI.Models.Zapi()
            {
                AccessKey = ConfigurationManager.AppSettings["AccessKey"],
                SecretKey = ConfigurationManager.AppSettings["SecretKey"],
                User = ConfigurationManager.AppSettings["User"],
            };

            if (JiraIssueID.StartsWith("ACC"))
            {
                jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["ACCProjectKey"]
                };

                versionName = ConfigurationManager.AppSettings["ACCCurrentVersionName"];
            }
            else if (JiraIssueID.StartsWith("CDV6"))
            {
                jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["CDV6ProjectKey"]
                };

                versionName = ConfigurationManager.AppSettings["CDV6CurrentVersionName"];
            }

            atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

        }

        public void ShutDownAllProcesses()
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
            {
                try { process.Kill(); } catch { }
            }
        }

        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description,JiraIssueID");

            Type t = this.GetType();

            foreach (var method in t.GetMethods())
            {
                TestMethodAttribute testMethod = null;
                DescriptionAttribute descAttr = null;
                TestPropertyAttribute propertyAttr = null;

                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is TestMethodAttribute)
                        testMethod = attribute as TestMethodAttribute;

                    if (attribute is DescriptionAttribute)
                        descAttr = attribute as DescriptionAttribute;

                    if (attribute is TestPropertyAttribute && (attribute as TestPropertyAttribute).Name == "JiraIssueID")
                        propertyAttr = attribute as TestPropertyAttribute;
                }

                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty((propertyAttr as TestPropertyAttribute).Value))
                {
                    sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";") + "," + (propertyAttr as TestPropertyAttribute).Value);
                    continue;
                }

                if (testMethod != null)
                {
                    sb.AppendLine(method.Name + "," + t.FullName + "." + method.Name);
                    continue;
                }

            }

            Console.WriteLine(sb.ToString());
        }

        public void GetAllDailyRunTestIds()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description,JiraIssueID");

            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var method in t.GetMethods())
                {
                    TestPropertyAttribute propertyAttr = null;
                    TestCategoryAttribute categoryAttr = null;

                    foreach (var attribute in method.GetCustomAttributes(false))
                    {
                        if (attribute is TestPropertyAttribute && (attribute as TestPropertyAttribute).Name == "JiraIssueID")
                            propertyAttr = attribute as TestPropertyAttribute;

                        if (attribute is TestCategoryAttribute && (attribute as TestCategoryAttribute).TestCategories != null && (attribute as TestCategoryAttribute).TestCategories.Contains("Daily_Runs"))
                            categoryAttr = (attribute as TestCategoryAttribute);
                    }

                    if (categoryAttr != null)
                        sb.AppendLine((propertyAttr as TestPropertyAttribute).Value);
                }
            }

            Console.WriteLine(sb.ToString());
        }

        public void Sleep(int millisecondsTimeout)
        {
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }

        private void SetDriver(string browser, string DownloadsDir)
        {
            var _runTestsInHeadlessMode = ConfigurationManager.AppSettings["RunTestsInHeadlessMode"];
            var _zoomOutBrowser = ConfigurationManager.AppSettings["ZoomOutBrowser"];
            var _disableGpu = ConfigurationManager.AppSettings["DisableGpu"];
            var _noSandbox = ConfigurationManager.AppSettings["NoSandbox"];
            var _disableDevShmUsage = ConfigurationManager.AppSettings["DisableDevShmUsage"];

            switch (browser)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();

                    var _specifyChromeBinaryLocation = ConfigurationManager.AppSettings["SpecifyChromeBinaryLocation"];
                    if (_specifyChromeBinaryLocation.Equals("true"))
                    {
                        options.BinaryLocation = ConfigurationManager.AppSettings["ChromeBinaryLocation"];
                    }

                    options.AddUserProfilePreference("download.default_directory", DownloadsDir);
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

                    if (_runTestsInHeadlessMode.Equals("true"))
                    {
                        options.AddArguments("--headless");
                        options.AddArguments("window-size=1920,1080");
                    }

                    if (_zoomOutBrowser.Equals("true"))
                    {
                        options.AddArguments("force-device-scale-factor=0.85");
                        options.AddArguments("high-dpi-support=0.85");
                    }

                    if (_disableGpu.Equals("true"))
                    {
                        options.AddArguments("--disable-gpu");
                    }

                    if (_noSandbox.Equals("true"))
                    {
                        options.AddArguments("--no-sandbox");
                    }

                    if (_disableDevShmUsage.Equals("true"))
                    {
                        options.AddArguments("--disable-dev-shm-usage");
                    }

                    driver = new ChromeDriver(options);
                    driver.Manage().Cookies.DeleteAllCookies();

                    break;
                case "Firefox":
                    FirefoxProfile profile = new FirefoxProfile();
                    profile.SetPreference("browser.download.folderList", 2);
                    profile.SetPreference("browser.download.dir", DownloadsDir);
                    profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/csv,application/java-archive, application/x-msexcel,application/excel,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/x-excel,application/vnd.ms-excel,image/png,image/jpeg,text/html,text/plain,application/msword,application/xml,application/vnd.microsoft.portable-executable");
                    profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf;text/plain;application/text;text/xml;application/xml;application/zip");
                    profile.SetPreference("pdfjs.disabled", true);  // disable the built-in PDF viewer


                    FirefoxOptions option = new FirefoxOptions();
                    option.Profile = profile;

                    if (_runTestsInHeadlessMode.Equals("true"))
                    {
                        option.AddArgument("--headless");
                        option.AddArgument("--window-size=1920,1080");
                    }

                    option.AddArgument("--start-maximized");

                    driver = new FirefoxDriver(option);
                    driver.Manage().Cookies.DeleteAllCookies();

                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();

        }

        private void SendTestResultsToZephyr(bool testPassed)
        {
            updateTestResult = ConfigurationManager.AppSettings["UpdateStatusInZephyr"];

            if (updateTestResult.Equals("true"))
            {
                string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

                //if we have a jira id for the test then we will update its status in jira
                try
                {
                    if (jiraIssueID != null)
                    {
                        SetCorrectConfigurationBaseOnJiraTest(jiraIssueID);

                        if (testPassed)
                            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_UI", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                        else
                            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_UI", "....", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);
                    }
                }
                catch { }
            }
        }

        private void TakeScreeshots(bool testPassed)
        {
            try
            {
                var takeScreenShots = ConfigurationManager.AppSettings["TakeScreenshotOnTestFailure"];
                if (!takeScreenShots.Equals("true"))
                    return;

                if (!testPassed)
                {
                    var screenshotFolderPath = ConfigurationManager.AppSettings["ScreenshotFolderPath"];
                    fileIOHelper.CreateDirectory(screenshotFolderPath);

                    var fileName = this.TestContext.TestName + DateTime.Now.ToString("-yyyyMMdd-HHmmss");
                    var screenShot = driver.TakeScreenshot();
                    screenShot.SaveAsFile(screenshotFolderPath + fileName + ".jpeg", ScreenshotImageFormat.Jpeg);
                }
            }
            catch { }
        }
    }
}

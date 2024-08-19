using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Xamarin.UITest;
using CareDirectorApp.TestFramework.PageObjects;
using CareDirectorApp.TestFramework;
using NUnit.Framework;

namespace CareDirectorApp.UITests
{
    public abstract class TestBase
    {

        #region Variables

        public string _apkFileLocation;
        public string _deviceSerial;
        public IApp _app = null;

        private string _serviceEndpointURL;

        public bool IgnoreBaseClassTestCasesInitialization;
        public bool IgnoreTestFixtureSetUp;
        public bool IgnoreSetUp;
        public bool SaveLogToFile;
        public string LogPath;

        #endregion

        #region Public Properties

        private PlatformServicesHelper _platformServicesHelper;
        public PlatformServicesHelper PlatformServicesHelper
        {
            get
            {
                if (_platformServicesHelper == null)
                    _platformServicesHelper = new PlatformServicesHelper();
                return _platformServicesHelper;
            }
            set
            {
                _platformServicesHelper = value;
            }
        }


        private LoginPage _loginPage;
        public LoginPage loginPage
        {
            get
            {
                if (_loginPage == null)
                    _loginPage = new LoginPage(this._app);
                return _loginPage;
            }
        }


        private MainMenu _mainMenu;
        public MainMenu mainMenu
        {
            get
            {
                if (_mainMenu == null)
                    _mainMenu = new MainMenu(this._app);
                return _mainMenu;
            }
        }


        private ServiceEndpointsPage _serviceEndpointsPage;
        public ServiceEndpointsPage serviceEndpointsPage
        {
            get
            {
                if (_serviceEndpointsPage == null)
                    _serviceEndpointsPage = new ServiceEndpointsPage(this._app);
                return _serviceEndpointsPage;
            }
        }


        private ErrorPopup _errorPopup;
        public ErrorPopup errorPopup
        {
            get
            {
                if (_errorPopup == null)
                    _errorPopup = new ErrorPopup(this._app);
                return _errorPopup;
            }
        }


        private PinPage _pinPage;
        public PinPage pinPage
        {
            get
            {
                if (_pinPage == null)
                    _pinPage = new PinPage(this._app);
                return _pinPage;
            }
        }


        private WarningPopup _warningPopup;
        public WarningPopup warningPopup
        {
            get
            {
                if (_warningPopup == null)
                    _warningPopup = new WarningPopup(this._app);

                return _warningPopup;
            }
        }


        private HomePage _homePage;
        public HomePage homePage
        {
            get
            {
                if (_homePage == null)
                    _homePage = new HomePage(this._app);

                return _homePage;
            }
        }


        private SettingsPage _settingsPage;
        public SettingsPage settingsPage
        {
            get
            {
                if (_settingsPage == null)
                    _settingsPage = new SettingsPage(this._app);

                return _settingsPage;
            }
        }


        
        private AppointmentsPage _appointmentsPage;
        public AppointmentsPage appointmentsPage
        {
            get
            {
                if (_appointmentsPage == null)
                    _appointmentsPage = new AppointmentsPage(this._app);

                return _appointmentsPage;
            }
        }



        private CasesPage _casesPage;
        public CasesPage casesPage
        {
            get
            {
                if (_casesPage == null)
                    _casesPage = new CasesPage(this._app);

                return _casesPage;
            }
        }

        private CasePage _casePage;
        public CasePage casePage
        {
            get
            {
                if (_casePage == null)
                    _casePage = new CasePage(this._app);

                return _casePage;
            }
        }


        private CaseTasksPage _caseTasksPage;
        public CaseTasksPage caseTasksPage
        {
            get
            {
                if (_caseTasksPage == null)
                    _caseTasksPage = new CaseTasksPage(this._app);

                return _caseTasksPage;
            }
        }


        private CaseTaskRecordPage _caseTaskRecordPage;
        public CaseTaskRecordPage caseTaskRecordPage
        {
            get
            {
                if (_caseTaskRecordPage == null)
                    _caseTaskRecordPage = new CaseTaskRecordPage(this._app);

                return _caseTaskRecordPage;
            }
        }


        private CaseCaseNotesPage _caseCaseNotesPage;
        public CaseCaseNotesPage caseCaseNotesPage
        {
            get
            {
                if (_caseCaseNotesPage == null)
                    _caseCaseNotesPage = new CaseCaseNotesPage(this._app);

                return _caseCaseNotesPage;
            }
        }


        private CaseCaseNoteRecordPage _caseCaseNoteRecordPage;
        public CaseCaseNoteRecordPage caseCaseNoteRecordPage
        {
            get
            {
                if (_caseCaseNoteRecordPage == null)
                    _caseCaseNoteRecordPage = new CaseCaseNoteRecordPage(this._app);

                return _caseCaseNoteRecordPage;
            }
        }


        private CaseAttachmentsPage _caseAttachmentsPage;
        public CaseAttachmentsPage caseAttachmentsPage
        {
            get
            {
                if (_caseAttachmentsPage == null)
                    _caseAttachmentsPage = new CaseAttachmentsPage(this._app);

                return _caseAttachmentsPage;
            }
        }


        private CaseAttachmentRecordPage _caseAttachmentRecordPage;
        public CaseAttachmentRecordPage caseAttachmentRecordPage
        {
            get
            {
                if (_caseAttachmentRecordPage == null)
                    _caseAttachmentRecordPage = new CaseAttachmentRecordPage(this._app);

                return _caseAttachmentRecordPage;
            }
        }



        private CaseFormsPage _caseFormsPage;
        public CaseFormsPage caseFormsPage
        {
            get
            {
                if (_caseFormsPage == null)
                    _caseFormsPage = new CaseFormsPage(this._app);

                return _caseFormsPage;
            }
        }

        private CaseFormRecordPage _caseFormRecordPage;
        public CaseFormRecordPage caseFormRecordPage
        {
            get
            {
                if (_caseFormRecordPage == null)
                    _caseFormRecordPage = new CaseFormRecordPage(this._app);

                return _caseFormRecordPage;
            }
        }


        private CaseInvolvementsPage _caseInvolvementsPage;
        public CaseInvolvementsPage caseInvolvementsPage
        {
            get
            {
                if (_caseInvolvementsPage == null)
                    _caseInvolvementsPage = new CaseInvolvementsPage(this._app);

                return _caseInvolvementsPage;
            }
        }

        private CaseInvolvementRecordPage _caseInvolvementRecordPage;
        public CaseInvolvementRecordPage caseInvolvementRecordPage
        {
            get
            {
                if (_caseInvolvementRecordPage == null)
                    _caseInvolvementRecordPage = new CaseInvolvementRecordPage(this._app);

                return _caseInvolvementRecordPage;
            }
        }



        private CaseHealthAppointmentsPage _caseHealthAppointmentsPage;
        public CaseHealthAppointmentsPage caseHealthAppointmentsPage
        {
            get
            {
                if (_caseHealthAppointmentsPage == null)
                    _caseHealthAppointmentsPage = new CaseHealthAppointmentsPage(this._app);

                return _caseHealthAppointmentsPage;
            }
        }

        private CaseHealthAppointmentRecordPage _caseHealthAppointmentRecordPage;
        public CaseHealthAppointmentRecordPage caseHealthAppointmentRecordPage
        {
            get
            {
                if (_caseHealthAppointmentRecordPage == null)
                    _caseHealthAppointmentRecordPage = new CaseHealthAppointmentRecordPage(this._app);

                return _caseHealthAppointmentRecordPage;
            }
        }




        private CommunityClinicAdditionalHealthProfessionals _communityClinicAdditionalHealthProfessionals;
        public CommunityClinicAdditionalHealthProfessionals communityClinicAdditionalHealthProfessionals
        {
            get
            {
                if (_communityClinicAdditionalHealthProfessionals == null)
                    _communityClinicAdditionalHealthProfessionals = new CommunityClinicAdditionalHealthProfessionals(this._app);

                return _communityClinicAdditionalHealthProfessionals;
            }
        }


        private CommunityClinicAdditionalHealthProfessionalRecordPage _communityClinicAdditionalHealthProfessionalRecordPage;
        public CommunityClinicAdditionalHealthProfessionalRecordPage communityClinicAdditionalHealthProfessionalRecordPage
        {
            get
            {
                if (_communityClinicAdditionalHealthProfessionalRecordPage == null)
                    _communityClinicAdditionalHealthProfessionalRecordPage = new CommunityClinicAdditionalHealthProfessionalRecordPage(this._app);

                return _communityClinicAdditionalHealthProfessionalRecordPage;
            }
        }





        private HealthAppointmentCaseNotesPage _healthAppointmentCaseNotesPage;
        public HealthAppointmentCaseNotesPage healthAppointmentCaseNotesPage
        {
            get
            {
                if (_healthAppointmentCaseNotesPage == null)
                    _healthAppointmentCaseNotesPage = new HealthAppointmentCaseNotesPage(this._app);

                return _healthAppointmentCaseNotesPage;
            }
        }


        private HealthAppointmentCaseNoteRecordPage _healthAppointmentCaseNoteRecordPage;
        public HealthAppointmentCaseNoteRecordPage healthAppointmentCaseNoteRecordPage
        {
            get
            {
                if (_healthAppointmentCaseNoteRecordPage == null)
                    _healthAppointmentCaseNoteRecordPage = new HealthAppointmentCaseNoteRecordPage(this._app);

                return _healthAppointmentCaseNoteRecordPage;
            }
        }




        private MobileCaseFormEditAssessmentPage _mobileCaseFormEditAssessmentPage;
        public MobileCaseFormEditAssessmentPage mobileCaseFormEditAssessmentPage
        {
            get
            {
                if (_mobileCaseFormEditAssessmentPage == null)
                    _mobileCaseFormEditAssessmentPage = new MobileCaseFormEditAssessmentPage(this._app);

                return _mobileCaseFormEditAssessmentPage;
            }
        }



        private SystemUserPage _systemUserPage;
        public SystemUserPage SystemUserPage
        {
            get
            {
                if (_systemUserPage == null)
                    _systemUserPage = new SystemUserPage(this._app);

                return _systemUserPage;
            }
        }

        private ActivityOutcome _activityOutcome;
        public ActivityOutcome activityOutcome
        {
            get
            {
                if (_activityOutcome == null)
                    _activityOutcome = new ActivityOutcome(this._app);

                return _activityOutcome;
            }
        }


        private ProviderPage _providerPage;
        public ProviderPage providerPage
        {
            get
            {
                if (_providerPage == null)
                    _providerPage = new ProviderPage(this._app);

                return _providerPage;
            }
        }


        private ProfessionalPage _professionalPage;
        public ProfessionalPage professionalPage
        {
            get
            {
                if (_professionalPage == null)
                    _professionalPage = new ProfessionalPage(this._app);

                return _professionalPage;
            }
        }




        private PeoplePage _peoplePage;
        public PeoplePage peoplePage
        {
            get
            {
                if (_peoplePage == null)
                    _peoplePage = new PeoplePage(this._app);

                return _peoplePage;
            }
        }


        private PersonPage _personPage;
        public PersonPage personPage
        {
            get
            {
                if (_personPage == null)
                    _personPage = new PersonPage(this._app);

                return _personPage;
            }
        }


        private TeamPage _teamPage;
        public TeamPage teamPage
        {
            get
            {
                if (_teamPage == null)
                    _teamPage = new TeamPage(this._app);

                return _teamPage;
            }
        }


        private LookupPopup _lookupPopup;
        public LookupPopup lookupPopup
        {
            get
            {
                if (_lookupPopup == null)
                    _lookupPopup = new LookupPopup(this._app);

                return _lookupPopup;
            }
        }

        
        private PersonFormsPage _personFormsPage;
        public PersonFormsPage personFormsPage
        {
            get
            {
                if (_personFormsPage == null)
                    _personFormsPage = new PersonFormsPage(this._app);

                return _personFormsPage;
            }
        }


        private PersonFormRecordPage _personFormRecordPage;
        public PersonFormRecordPage personFormRecordPage
        {
            get
            {
                if (_personFormRecordPage == null)
                    _personFormRecordPage = new PersonFormRecordPage(this._app);

                return _personFormRecordPage;
            }
        }


        private MobilePersonFormEditAssessmentPage _mobilePersonFormEditAssessmentPage;
        public MobilePersonFormEditAssessmentPage mobilePersonFormEditAssessmentPage
        {
            get
            {
                if (_mobilePersonFormEditAssessmentPage == null)
                    _mobilePersonFormEditAssessmentPage = new MobilePersonFormEditAssessmentPage(this._app);

                return _mobilePersonFormEditAssessmentPage;
            }
        }


        private MobilePersonFormTableQuestionsEditAssessmentPage _mobilePersonFormTableQuestionsEditAssessmentPage;
        public MobilePersonFormTableQuestionsEditAssessmentPage mobilePersonFormTableQuestionsEditAssessmentPage
        {
            get
            {
                if (_mobilePersonFormTableQuestionsEditAssessmentPage == null)
                    _mobilePersonFormTableQuestionsEditAssessmentPage = new MobilePersonFormTableQuestionsEditAssessmentPage(this._app);

                return _mobilePersonFormTableQuestionsEditAssessmentPage;
            }
        }


        private MobilePersonFormDocumentRulesEditAssessmentPage _mobilePersonFormDocumentRulesEditAssessmentPage;
        public MobilePersonFormDocumentRulesEditAssessmentPage mobilePersonFormDocumentRulesEditAssessmentPage
        {
            get
            {
                if (_mobilePersonFormDocumentRulesEditAssessmentPage == null)
                    _mobilePersonFormDocumentRulesEditAssessmentPage = new MobilePersonFormDocumentRulesEditAssessmentPage(this._app);

                return _mobilePersonFormDocumentRulesEditAssessmentPage;
            }
        }


        private PersonDisabilityImpairmentsPage _personDisabilityImpairmentsPage;
        public PersonDisabilityImpairmentsPage personDisabilityImpairmentsPage
        {
            get
            {
                if (_personDisabilityImpairmentsPage == null)
                    _personDisabilityImpairmentsPage = new PersonDisabilityImpairmentsPage(this._app);

                return _personDisabilityImpairmentsPage;
            }
        }

        private PersonDNARRecordPage _personDNARRecordPage;
        public PersonDNARRecordPage personDNARRecordPage
        {
            get
            {
                if (_personDNARRecordPage == null)
                    _personDNARRecordPage = new PersonDNARRecordPage(this._app);

                return _personDNARRecordPage;
            }
        }


        private PersonDisabilityImpairmentRecordPage _personDisabilityImpairmentRecordPage;
        public PersonDisabilityImpairmentRecordPage personDisabilityImpairmentRecordPage
        {
            get
            {
                if (_personDisabilityImpairmentRecordPage == null)
                    _personDisabilityImpairmentRecordPage = new PersonDisabilityImpairmentRecordPage(this._app);

                return _personDisabilityImpairmentRecordPage;
            }
        }


        private PersonAllergyPage _personAllergyPage;
        public PersonAllergyPage personAllergyPage
        {
            get
            {
                if (_personAllergyPage == null)
                    _personAllergyPage = new PersonAllergyPage(this._app);

                return _personAllergyPage;
            }
        }


        private PersonAllergyRecordPage _personAllergyRecordPage;
        public PersonAllergyRecordPage personAllergyRecordPage
        {
            get
            {
                if (_personAllergyRecordPage == null)
                    _personAllergyRecordPage = new PersonAllergyRecordPage(this._app);

                return _personAllergyRecordPage;
            }
        }
        private PersonDNARActiveNInactiveRecordPage _personDNARActiveNInactiveRecordPage;
        public PersonDNARActiveNInactiveRecordPage personDNARActiveNInactiveRecordPage
        {
            get
            {
                if (_personDNARActiveNInactiveRecordPage == null)
                    _personDNARActiveNInactiveRecordPage = new PersonDNARActiveNInactiveRecordPage(this._app);

                return _personDNARActiveNInactiveRecordPage;
            }
        }
        private PersonAllergicReactionsPage _personAllergicReactionsPage;
        public PersonAllergicReactionsPage personAllergicReactionsPage
        {
            get
            {
                if (_personAllergicReactionsPage == null)
                    _personAllergicReactionsPage = new PersonAllergicReactionsPage(this._app);

                return _personAllergicReactionsPage;
            }
        }


        private PersonAllergicReactionRecordPage _personAllergicReactionRecordPage;
        public PersonAllergicReactionRecordPage personAllergicReactionRecordPage
        {
            get
            {
                if (_personAllergicReactionRecordPage == null)
                    _personAllergicReactionRecordPage = new PersonAllergicReactionRecordPage(this._app);

                return _personAllergicReactionRecordPage;
            }
        }


        private PersonRelationshipsPage _personRelationshipsPage;
        public PersonRelationshipsPage personRelationshipsPage
        {
            get
            {
                if (_personRelationshipsPage == null)
                    _personRelationshipsPage = new PersonRelationshipsPage(this._app);

                return _personRelationshipsPage;
            }
        }

        private PersonRelationshipRecordPage _personRelationshipRecordPage;
        public PersonRelationshipRecordPage personRelationshipRecordPage
        {
            get
            {
                if (_personRelationshipRecordPage == null)
                    _personRelationshipRecordPage = new PersonRelationshipRecordPage(this._app);

                return _personRelationshipRecordPage;
            }
        }


        private PersonBodyMapsPage _personBodyMapsPage;
        public PersonBodyMapsPage personBodyMapsPage
        {
            get
            {
                if (_personBodyMapsPage == null)
                    _personBodyMapsPage = new PersonBodyMapsPage(this._app);

                return _personBodyMapsPage;
            }
        }


        private PersonBodyMapRecordPage _personBodyMapRecordPage;
        public PersonBodyMapRecordPage personBodyMapRecordPage
        {
            get
            {
                if (_personBodyMapRecordPage == null)
                    _personBodyMapRecordPage = new PersonBodyMapRecordPage(this._app);

                return _personBodyMapRecordPage;
            }
        }


        private PersonBodyMapReviewPopup _personBodyMapReviewPopup;
        public PersonBodyMapReviewPopup personBodyMapReviewPopup
        {
            get
            {
                if (_personBodyMapReviewPopup == null)
                    _personBodyMapReviewPopup = new PersonBodyMapReviewPopup(this._app);

                return _personBodyMapReviewPopup;
            }
        }


        private BodyAreaLookupPopup _bodyAreaLookupPopup;
        public BodyAreaLookupPopup bodyAreaLookupPopup
        {
            get
            {
                if (_bodyAreaLookupPopup == null)
                    _bodyAreaLookupPopup = new BodyAreaLookupPopup(this._app);

                return _bodyAreaLookupPopup;
            }
        }


        private PersonBodyInjuryDescriptionPopup _personBodyInjuryDescriptionPopup;
        public PersonBodyInjuryDescriptionPopup personBodyInjuryDescriptionPopup
        {
            get
            {
                if (_personBodyInjuryDescriptionPopup == null)
                    _personBodyInjuryDescriptionPopup = new PersonBodyInjuryDescriptionPopup(this._app);

                return _personBodyInjuryDescriptionPopup;
            }
        }


        private PickList _pickList;
        public PickList pickList
        {
            get
            {
                if (_pickList == null)
                    _pickList = new PickList(this._app);

                return _pickList;
            }
        }


        private PersonCaseNotesPage _personCaseNotesPage;
        public PersonCaseNotesPage personCaseNotesPage
        {
            get
            {
                if (_personCaseNotesPage == null)
                    _personCaseNotesPage = new PersonCaseNotesPage(this._app);

                return _personCaseNotesPage;
            }
        }


        private PersonCaseNoteRecordPage _personCaseNoteRecordPage;
        public PersonCaseNoteRecordPage personCaseNoteRecordPage
        {
            get
            {
                if (_personCaseNoteRecordPage == null)
                    _personCaseNoteRecordPage = new PersonCaseNoteRecordPage(this._app);

                return _personCaseNoteRecordPage;
            }
        }


        private PersonTasksPage _personTasksPage;
        public PersonTasksPage personTasksPage
        {
            get
            {
                if (_personTasksPage == null)
                    _personTasksPage = new PersonTasksPage(this._app);

                return _personTasksPage;
            }
        }



        private PersonTaskRecordPage _personTaskRecordPage;
        public PersonTaskRecordPage personTaskRecordPage
        {
            get
            {
                if (_personTaskRecordPage == null)
                    _personTaskRecordPage = new PersonTaskRecordPage(this._app);

                return _personTaskRecordPage;
            }
        }



        private PersonAlertsAndHazardsPage _personAlertsAndHazardsPage;
        public PersonAlertsAndHazardsPage personAlertsAndHazardsPage
        {
            get
            {
                if (_personAlertsAndHazardsPage == null)
                    _personAlertsAndHazardsPage = new PersonAlertsAndHazardsPage(this._app);

                return _personAlertsAndHazardsPage;
            }
        }



        private PersonAlertsAndHazardRecordPage _personAlertsAndHazardRecordPage;
        public PersonAlertsAndHazardRecordPage personAlertsAndHazardRecordPage
        {
            get
            {
                if (_personAlertsAndHazardRecordPage == null)
                    _personAlertsAndHazardRecordPage = new PersonAlertsAndHazardRecordPage(this._app);

                return _personAlertsAndHazardRecordPage;
            }
        }


        private PersonAppointmentsPage _personAppointmentsPage;
        public PersonAppointmentsPage personAppointmentsPage
        {
            get
            {
                if (_personAppointmentsPage == null)
                    _personAppointmentsPage = new PersonAppointmentsPage(this._app);

                return _personAppointmentsPage;
            }
        }

        private PersonAppointmentRecordPage _personAppointmentRecordPage;
        public PersonAppointmentRecordPage personAppointmentRecordPage
        {
            get
            {
                if (_personAppointmentRecordPage == null)
                    _personAppointmentRecordPage = new PersonAppointmentRecordPage(this._app);

                return _personAppointmentRecordPage;
            }
        }

        private PersonFinancialDetailsPage _personFinancialDetailsPage;
        public PersonFinancialDetailsPage personFinancialDetailsPage
        {
            get
            {
                if (_personFinancialDetailsPage == null)
                    _personFinancialDetailsPage = new PersonFinancialDetailsPage(this._app);

                return _personFinancialDetailsPage;
            }
        }

        private PersonFinancialDetailRecordPage _personFinancialDetailRecordPage;
        public PersonFinancialDetailRecordPage personFinancialDetailRecordPage
        {
            get
            {
                if (_personFinancialDetailRecordPage == null)
                    _personFinancialDetailRecordPage = new PersonFinancialDetailRecordPage(this._app);

                return _personFinancialDetailRecordPage;
            }
        }



        private PersonFinancialDetailAttachmentsPage _personFinancialDetailAttachmentsPage;
        public PersonFinancialDetailAttachmentsPage personFinancialDetailAttachmentsPage
        {
            get
            {
                if (_personFinancialDetailAttachmentsPage == null)
                    _personFinancialDetailAttachmentsPage = new PersonFinancialDetailAttachmentsPage(this._app);

                return _personFinancialDetailAttachmentsPage;
            }
        }

        private PersonFinancialDetailAttachmentRecordPage _personFinancialDetailAttachmentRecordPage;
        public PersonFinancialDetailAttachmentRecordPage personFinancialDetailAttachmentRecordPage
        {
            get
            {
                if (_personFinancialDetailAttachmentRecordPage == null)
                    _personFinancialDetailAttachmentRecordPage = new PersonFinancialDetailAttachmentRecordPage(this._app);

                return _personFinancialDetailAttachmentRecordPage;
            }
        }


        private PersonFinancialAssessmentsPage _personFinancialAssessmentsPage;
        public PersonFinancialAssessmentsPage personFinancialAssessmentsPage
        {
            get
            {
                if (_personFinancialAssessmentsPage == null)
                    _personFinancialAssessmentsPage = new PersonFinancialAssessmentsPage(this._app);

                return _personFinancialAssessmentsPage;
            }
        }

        private PersonFinancialAssessmentRecordPage _personFinancialAssessmentRecordPage;
        public PersonFinancialAssessmentRecordPage personFinancialAssessmentRecordPage
        {
            get
            {
                if (_personFinancialAssessmentRecordPage == null)
                    _personFinancialAssessmentRecordPage = new PersonFinancialAssessmentRecordPage(this._app);

                return _personFinancialAssessmentRecordPage;
            }
        }


        #endregion

        #region Constructor

        public TestBase()
        {
            IgnoreBaseClassTestCasesInitialization = Boolean.Parse(ConfigurationManager.AppSettings["IgnoreBaseClassTestCasesInitialization"] ?? "false");
            IgnoreTestFixtureSetUp = Boolean.Parse(ConfigurationManager.AppSettings["IgnoreTestFixtureSetUp"] ?? "false");
            IgnoreSetUp = Boolean.Parse(ConfigurationManager.AppSettings["IgnoreSetUp"] ?? "false");
            SaveLogToFile = Boolean.Parse(ConfigurationManager.AppSettings["SaveLogToFile"] ?? "false");
            LogPath = ConfigurationManager.AppSettings["LogPath"];

            //read the appconfig information to determine the location of the apk file and the device to run the tests on
            _apkFileLocation = ConfigurationManager.AppSettings["ApkFileLocation"];
            _deviceSerial = ConfigurationManager.AppSettings["DeviceSerial"];

            //get the default service endpoint url
            _serviceEndpointURL = ConfigurationManager.AppSettings["ServiceEndpointURL"];
        }


        #endregion

        #region Public Methods

        public void SetDefaultEndpointURL()
        {
            SetDefaultEndpointURL(_serviceEndpointURL);
        }

        public void SetDefaultEndpointURL(string ServiceEndpointURL)
        {
            //the login page is the landing page, so we need for it to load
            loginPage
                .WaitForBasicLoginPageToLoad();

            //navigate to the service endpoints to set the default endpoint
            mainMenu
                .WaitForMainMenuButtonToLoad()
                .NavigateToServiceEndpointsLink();

            //set the URL for the default endpoint
            serviceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .TapOnServiceEndpoint("Name: CareDirector")
                .WaitForServiceEndpointEditPageToLoad()
                .InsertEndpointURLIfEmpty(ServiceEndpointURL)
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .tapBackButton()
                .WaitForBasicLoginPageToLoad();
        }

        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TestName,Description");

            Type t = this.GetType();

            foreach (var method in t.GetMethods())
            {
                NUnit.Framework.TestAttribute testMethod = null;
                NUnit.Framework.DescriptionAttribute descAttr = null;
                PropertyAttribute propertyAttr = null;

                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is NUnit.Framework.TestAttribute)
                        testMethod = attribute as NUnit.Framework.TestAttribute;

                    if (attribute is NUnit.Framework.DescriptionAttribute)
                        descAttr = attribute as NUnit.Framework.DescriptionAttribute;

                    if (attribute is PropertyAttribute && (attribute as PropertyAttribute).Properties.Contains("JiraIssueID"))
                        propertyAttr = attribute as PropertyAttribute;
                }

                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty((string)propertyAttr.Properties["JiraIssueID"]))
                {
                    sb.AppendLine((string)propertyAttr.Properties["JiraIssueID"]);
                    continue;
                }
                if (testMethod != null)
                {
                    sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";"));
                }

            }

            if (SaveLogToFile)
                System.IO.File.WriteAllText(LogPath + this.GetType().Name + ".csv", sb.ToString());
            else
                Console.WriteLine(sb.ToString());
        }


        [TearDown]
        public void TestTearDownMethod()
        {
            if (IgnoreSetUp || IgnoreTestFixtureSetUp || IgnoreBaseClassTestCasesInitialization)
                return;

            string jiraIssueID = (string)TestContext.CurrentContext.Test.Properties["JiraIssueID"];

            //if we have a JIRA id for the test then we will update its status in JIRA
            if (!string.IsNullOrEmpty(jiraIssueID))
            {
                bool testPassed = TestContext.CurrentContext.Result.Status == TestStatus.Passed;

                var zapi = new AtlassianServiceAPI.Models.Zapi()
                {
                    AccessKey = ConfigurationManager.AppSettings["AccessKey"],
                    SecretKey = ConfigurationManager.AppSettings["SecretKey"],
                    User = ConfigurationManager.AppSettings["User"],
                };

                var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
                {
                    Authentication = ConfigurationManager.AppSettings["Authentication"],
                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
                    ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
                };

                AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

                string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

                if (testPassed)
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_Mobile", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                else
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automation_Regression_Testing_Mobile", "TEST FAILED", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);
            }
        }



        #endregion
    }
}

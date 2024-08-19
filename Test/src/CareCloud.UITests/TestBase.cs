using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using CareCloudTestFramework.PageObjects;
using CareCloudTestFramework;


namespace CareCloud.UITests
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

        private Phoenix.DBHelper.DatabaseHelper _dbHelper;
        public Phoenix.DBHelper.DatabaseHelper dbHelper
        {
            get
            {
                if (_dbHelper == null)
                    _dbHelper = new Phoenix.DBHelper.DatabaseHelper();
                return _dbHelper;
            }
            set
            {
                _dbHelper = value;
            }
        }

        private CareCloudTestFramework.PageObjects.LoginPage _loginPage;
        public CareCloudTestFramework.PageObjects.LoginPage loginPage
        {
            get
            {
                if (_loginPage == null)
                    _loginPage = new CareCloudTestFramework.PageObjects.LoginPage(this._app);
                return _loginPage;
            }
        }

        private CareCloudTestFramework.PageObjects.MainMenu _mainMenu;
        public CareCloudTestFramework.PageObjects.MainMenu MainMenu
        {
            get
            {
                if (_mainMenu == null)
                    _mainMenu = new CareCloudTestFramework.PageObjects.MainMenu(this._app);
                return _mainMenu;
            }
        }

        private CareCloudTestFramework.PageObjects.MyBookingsPage _mybookingPage;
        public CareCloudTestFramework.PageObjects.MyBookingsPage MyBookingsPage
        {
            get
            {
                if (_mybookingPage == null)
                    _mybookingPage = new CareCloudTestFramework.PageObjects.MyBookingsPage(this._app);
                return _mybookingPage;
            }
        }

        private CareCloudTestFramework.PageObjects.BookingDetailsPage _bookingDetailsPage;
        public CareCloudTestFramework.PageObjects.BookingDetailsPage bookingDetailsPage
        {
            get
            {
                if (_bookingDetailsPage == null)
                    _bookingDetailsPage = new CareCloudTestFramework.PageObjects.BookingDetailsPage(this._app);
                return _bookingDetailsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.VisitDetailsPage _visitDetailsPage;
        public CareCloudTestFramework.PageObjects.VisitDetailsPage visitDetailsPage
        {
            get
            {
                if (_visitDetailsPage == null)
                    _visitDetailsPage = new CareCloudTestFramework.PageObjects.VisitDetailsPage(this._app);
                return _visitDetailsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.PersonDailyRecordPage _personDailyRecordPage;
        public CareCloudTestFramework.PageObjects.PersonDailyRecordPage personDailyRecordPage
        {
            get
            {
                if (_personDailyRecordPage == null)
                    _personDailyRecordPage = new CareCloudTestFramework.PageObjects.PersonDailyRecordPage(this._app);
                return _personDailyRecordPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ActivitiesLookUp _activitiesLookUp;
        public CareCloudTestFramework.PageObjects.ActivitiesLookUp ActivitiesLookUp
        {
            get
            {
                if (_activitiesLookUp == null)
                    _activitiesLookUp = new CareCloudTestFramework.PageObjects.ActivitiesLookUp(this._app);
                return _activitiesLookUp;
            }
        }

        private CareCloudTestFramework.PageObjects.DailyCareItemLookup _dailyCareItemLookup;
        public CareCloudTestFramework.PageObjects.DailyCareItemLookup dailyCareItemLookup
        {
            get
            {
                if (_dailyCareItemLookup == null)
                    _dailyCareItemLookup = new CareCloudTestFramework.PageObjects.DailyCareItemLookup(this._app);
                return _dailyCareItemLookup;
            }
        }


        private CareCloudTestFramework.PageObjects.ServiceEndpointsPage _serviceEndpointsPage;
        public CareCloudTestFramework.PageObjects.ServiceEndpointsPage ServiceEndpointsPage
        {
            get
            {
                if (_serviceEndpointsPage == null)
                    _serviceEndpointsPage = new CareCloudTestFramework.PageObjects.ServiceEndpointsPage(this._app);
                return _serviceEndpointsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ServiceEndPointEditPage _serviceEndpointEditPage;
        public CareCloudTestFramework.PageObjects.ServiceEndPointEditPage ServiceEndpointEditPage
        {
            get
            {
                if (_serviceEndpointEditPage == null)
                    _serviceEndpointEditPage = new CareCloudTestFramework.PageObjects.ServiceEndPointEditPage(this._app);
                return _serviceEndpointEditPage;
            }
        }

        private CareCloudTestFramework.PageObjects.WarningPopUp _warningPopUp;
        public CareCloudTestFramework.PageObjects.WarningPopUp WarningPopUp
        {
            get
            {
                if (_warningPopUp == null)
                    _warningPopUp = new CareCloudTestFramework.PageObjects.WarningPopUp(this._app);
                return _warningPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.HomePage _homePage;
        public CareCloudTestFramework.PageObjects.HomePage HomePage
        {
            get
            {
                if (_homePage == null)
                    _homePage = new CareCloudTestFramework.PageObjects.HomePage(this._app);
                return _homePage;
            }
        }

        private CareCloudTestFramework.PageObjects.PinPage _pinPage;
        public CareCloudTestFramework.PageObjects.PinPage PinPage
        {
            get
            {
                if (_pinPage == null)
                    _pinPage = new CareCloudTestFramework.PageObjects.PinPage(this._app);
                return _pinPage;
            }
        }


        private CareCloudTestFramework.PageObjects.ForgotPinPage _forgotPinPage;
        public CareCloudTestFramework.PageObjects.ForgotPinPage ForgotPinPage
        {
            get
            {
                if (_forgotPinPage == null)
                    _forgotPinPage = new CareCloudTestFramework.PageObjects.ForgotPinPage(this._app);
                return _forgotPinPage;
            }
        }

        private CareCloudTestFramework.PageObjects.LookupPopUp _lookupPopUp;
        public CareCloudTestFramework.PageObjects.LookupPopUp LookupPopUp
        {
            get
            {
                if (_lookupPopUp == null)
                    _lookupPopUp = new CareCloudTestFramework.PageObjects.LookupPopUp(this._app);
                return _lookupPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.RegularCareTaskLookup _regularCareTaskLookup;
        public CareCloudTestFramework.PageObjects.RegularCareTaskLookup RegularCareTaskLookup
        {
            get
            {
                if (_regularCareTaskLookup == null)
                    _regularCareTaskLookup = new CareCloudTestFramework.PageObjects.RegularCareTaskLookup(this._app);
                return _regularCareTaskLookup;
             }
        }

        private CareCloudTestFramework.PageObjects.AdditionalCareTaskLookup _additionalCareTaskLookup;
        public CareCloudTestFramework.PageObjects.AdditionalCareTaskLookup AdditionalCareTaskLookup
        {
            get
            {
                if (_additionalCareTaskLookup == null)
                    _additionalCareTaskLookup = new CareCloudTestFramework.PageObjects.AdditionalCareTaskLookup(this._app);
                return _additionalCareTaskLookup;
            }
        }

        private CareCloudTestFramework.PageObjects.VisitCompletedPage _visitCompletedPage;
        public CareCloudTestFramework.PageObjects.VisitCompletedPage VisitCompletedPage
        {
            get
            {
                if (_visitCompletedPage == null)
                    _visitCompletedPage = new CareCloudTestFramework.PageObjects.VisitCompletedPage(this._app);
                return _visitCompletedPage;
            }
        }


        private CareCloudTestFramework.PageObjects.ErrorPopUp _errorPopUp;
        public CareCloudTestFramework.PageObjects.ErrorPopUp ErrorPopUp
        {
            get
            {
                if (_errorPopUp == null)
                    _errorPopUp = new CareCloudTestFramework.PageObjects.ErrorPopUp(this._app);
                return _errorPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.SettingsPage _settingsPage;
        public CareCloudTestFramework.PageObjects.SettingsPage SettingsPage
        {
            get
            {
                if (_settingsPage == null)
                    _settingsPage = new CareCloudTestFramework.PageObjects.SettingsPage(this._app);
                return _settingsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ProviderSelectionPage _providerSelectionPage;
        public CareCloudTestFramework.PageObjects.ProviderSelectionPage providerSelectionPage
        {
            get
            {
                if (_providerSelectionPage == null)
                    _providerSelectionPage = new CareCloudTestFramework.PageObjects.ProviderSelectionPage(this._app);
                return _providerSelectionPage;
            }
        }

        private CareCloudTestFramework.PageObjects.HandoversDetailsPage _handoversDetailsPage;
        public CareCloudTestFramework.PageObjects.HandoversDetailsPage handoversDetailsPage
        {
            get
            {
                if (_handoversDetailsPage == null)
                    _handoversDetailsPage = new CareCloudTestFramework.PageObjects.HandoversDetailsPage(this._app);
                return _handoversDetailsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ResedentHandoverNotesPage _resedentHandoverNotesPage;
        public CareCloudTestFramework.PageObjects.ResedentHandoverNotesPage resedentHandoverNotesPage
        {
            get

            {
                if (_resedentHandoverNotesPage == null)
                    _resedentHandoverNotesPage = new CareCloudTestFramework.PageObjects.ResedentHandoverNotesPage(this._app);
                return _resedentHandoverNotesPage;
            }
        }


        private CareCloudTestFramework.PageObjects.SeeAllNotesPage _seeAllNotesPage;
        public CareCloudTestFramework.PageObjects.SeeAllNotesPage seeAllNotesPage
        {
            get
            {
                if (_seeAllNotesPage == null)
                    _seeAllNotesPage = new CareCloudTestFramework.PageObjects.SeeAllNotesPage(this._app);
                return _seeAllNotesPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ResedentDetailsPage _resedentDetailsPage;
        public CareCloudTestFramework.PageObjects.ResedentDetailsPage resedentDetailsPage
        {
            get
            {
                if (_resedentDetailsPage == null)
                    _resedentDetailsPage = new CareCloudTestFramework.PageObjects.ResedentDetailsPage(this._app);
                return _resedentDetailsPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ContinenceCareRecordPage _continenceCareRecordPage;
        public CareCloudTestFramework.PageObjects.ContinenceCareRecordPage continenceCareRecordPage
        {
            get
            {
                if (_continenceCareRecordPage == null)
                    _continenceCareRecordPage = new CareCloudTestFramework.PageObjects.ContinenceCareRecordPage(this._app);
                return _continenceCareRecordPage;
            }
        }


        private CareCloudTestFramework.PageObjects.AwayFromHomePage _awayFromHomePage;
        public CareCloudTestFramework.PageObjects.AwayFromHomePage awayFromHomePage
        {
            get
            {
                if (_awayFromHomePage == null)
                    _awayFromHomePage = new CareCloudTestFramework.PageObjects.AwayFromHomePage(this._app);
                return _awayFromHomePage;
            }
        }
        private CareCloudTestFramework.PageObjects.CarePlanPage _carePlanPage;
        public CareCloudTestFramework.PageObjects.CarePlanPage carePlanPage
        {
            get
            {
                if (_carePlanPage == null)
                    _carePlanPage = new CareCloudTestFramework.PageObjects.CarePlanPage(this._app);
                return _carePlanPage;
            }
        }

        private CareCloudTestFramework.PageObjects.MobilityRecordPage _mobilityRecordPage;
        public CareCloudTestFramework.PageObjects.MobilityRecordPage mobilityRecordPage
        {
            get
            {
                if (_mobilityRecordPage == null)
                    _mobilityRecordPage = new CareCloudTestFramework.PageObjects.MobilityRecordPage(this._app);
                return _mobilityRecordPage;
            }
        }

        private CareCloudTestFramework.PageObjects.SelectSkinConditionPopUp _selectSkinConditionPopUp;
        public CareCloudTestFramework.PageObjects.SelectSkinConditionPopUp selectSkinConditionPopUp
        {
            get
            {
                if (_selectSkinConditionPopUp == null)
                    _selectSkinConditionPopUp = new CareCloudTestFramework.PageObjects.SelectSkinConditionPopUp(this._app);
                return _selectSkinConditionPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.RepositioningRecordPage _repositioningRecordPage;
        public CareCloudTestFramework.PageObjects.RepositioningRecordPage repositioningRecordPage
        {
            get
            {
                if (_repositioningRecordPage == null)
                    _repositioningRecordPage = new CareCloudTestFramework.PageObjects.RepositioningRecordPage(this._app);
                return _repositioningRecordPage;
            }
        }

        private CareCloudTestFramework.PageObjects.TimePickerPopUp _timePickerPopUp;
        public CareCloudTestFramework.PageObjects.TimePickerPopUp timePickerPopUp
        {
            get
            {
                if (_timePickerPopUp == null)
                    _timePickerPopUp = new CareCloudTestFramework.PageObjects.TimePickerPopUp(this._app);
                return _timePickerPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.DatePickerPopUp _datePickerPopUp;
        public CareCloudTestFramework.PageObjects.DatePickerPopUp datePickerPopUp
        {
            get
            {
                if (_datePickerPopUp == null)
                    _datePickerPopUp = new CareCloudTestFramework.PageObjects.DatePickerPopUp(this._app);
                return _datePickerPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.WelfareCheckRecordPage _welfareCheckRecordPage;
        public CareCloudTestFramework.PageObjects.WelfareCheckRecordPage welfareCheckRecordPage
        {
            get
            {
                if (_welfareCheckRecordPage == null)
                    _welfareCheckRecordPage = new CareCloudTestFramework.PageObjects.WelfareCheckRecordPage(this._app);
                return _welfareCheckRecordPage;
            }
        }

        private CareCloudTestFramework.PageObjects.KeyWorkerNotesRecordPage _keyWorkerNotesRecordPage;
        public CareCloudTestFramework.PageObjects.KeyWorkerNotesRecordPage keyWorkerNotesRecordPage
        {
            get
            {
                if (_keyWorkerNotesRecordPage == null)
                    _keyWorkerNotesRecordPage = new CareCloudTestFramework.PageObjects.KeyWorkerNotesRecordPage(this._app);
                return _keyWorkerNotesRecordPage;
            }
        }

        private CareCloudTestFramework.PageObjects.ObservationsPopUp _observationsPopUp;
        public CareCloudTestFramework.PageObjects.ObservationsPopUp observationsPopUp
        {
            get
            {
                if (_observationsPopUp == null)
                    _observationsPopUp = new CareCloudTestFramework.PageObjects.ObservationsPopUp(this._app);
                return _observationsPopUp;
            }
        }

        private CareCloudTestFramework.PageObjects.KeyRisksPage _keyRisksPage;
        public CareCloudTestFramework.PageObjects.KeyRisksPage keyRisksPage
        {
            get
            {
                if (_keyRisksPage == null)
                    _keyRisksPage = new CareCloudTestFramework.PageObjects.KeyRisksPage(this._app);
                return _keyRisksPage;
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

        public void SetDefaultEndpointURL()
        {
            SetDefaultEndpointURL(_serviceEndpointURL);
        }

        public void SetDefaultEndpointURL(string ServiceEndpointURL)
        {
            //the login page is the landing page, so we need for it to load
           // loginPage
           //     .WaitForBasicLoginPageToLoad();

            //navigate to the service endpoints to set the default endpoint
            MainMenu
                .WaitForMainMenuButtonToLoad()
                .NavigateToServiceEndpointsLink();

            //set the URL for the default endpoint
            ServiceEndpointsPage
                .WaitForServiceEndpointsPageToLoad()
                .TapOnServiceEndpoint("Name: Care Cloud");
            
            ServiceEndpointEditPage
                .WaitForServiceEndPointEditPageToLoad()
                .InsertEndpointURLIfEmpty(ServiceEndpointURL)
                .TapServiceEndpointSaveAndCloseButton()
                .WaitForServiceEndpointsPageToLoad()
                .tapBackButton()
                .WaitForLoginPageToLoad();
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

                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is NUnit.Framework.TestAttribute)
                        testMethod = attribute as NUnit.Framework.TestAttribute;

                    if (attribute is NUnit.Framework.DescriptionAttribute)
                        descAttr = attribute as NUnit.Framework.DescriptionAttribute;
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
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Mobile", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
                else
                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Mobile", "TEST FAILED", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);
            }
        }
    }
}

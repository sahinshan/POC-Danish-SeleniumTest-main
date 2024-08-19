using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Health
{

    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Person_HealthDetail_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private string _defaultUsername;
        private Guid _defaultUserId;
        private string _defaultUserFullname;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _personId;
        private int _personNumber;
        private string _personFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Person_HealthDetail_User_1";

                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Health Detail", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "Health";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _personFullName = (string)dbHelper.person.GetPersonById(_personId, "fullname")["fullname"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12506

        [TestProperty("JiraIssueID", "CDV6-12525")]
        [Description("To verify new Health Details record is created- For Health issue type is Psychosis - Verify the following fields are displayed below Psychosis Information section and " +
            "Null is displayed by default for all the fields:-Prodromal Psychosis Date-Emergent Psychosis Date-Manifest Psychosis Date-First Prescription Date-Psychosis First Treatment Start Date ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases01()
        {
            var startDate = DateTime.Now.Date.AddMonths(-5);

            #region Health Issue Type

            var _healthIssueTypeName = "Psychosis";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2), 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
               .WaitForPersonHealthDetailRecordPageToLoad("New")
               .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ValidateProdromePsychosisDateFieldVisible(true)
               .ValidatePsychosisFirstTreatmentStartDateFieldVisible(true)
               .ValidateManifestPsychosisDateFieldVisible(true)
               .ValidateFirstPrescriptionDateFieldVisible(true)
               .ValidateEmergentPsychosisDateFieldVisible(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12526")]
        [Description("Verify system does not allow user to create Health Details record when 'Start date' after 'Current Date' in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases02()
        {
            var startDate = DateTime.Now.Date.AddMonths(5);

            #region Health Issue Type

            var _healthIssueTypeName = "Psychosis";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2), 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Start Date cannot be in the future");

        }

        [TestProperty("JiraIssueID", "CDV6-12527")]
        [Description("Verify system allows user to create Health Details record when 'Start date' as 'Current Date' in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases03()
        {
            var startDate = DateTime.Now.Date;

            #region Health Issue Type

            var _healthIssueTypeName = "Health Issue_123";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1500);

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad();

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12528")]
        [Description("Verify system does not allow user to create Health Details record when 'End Date' after 'Current Date' in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases04()
        {
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(25);

            #region Health Issue Type

            var _healthIssueTypeName = "Health Issue_123";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date cannot be in the future");

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12529")]

        [Description("Verify system does not allow user to create Health Details record when 'End Date' before 'Start Date' in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases05()
        {
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(-5);

            #region Health Issue Type

            var _healthIssueTypeName = "Health Issue_123";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date cannot be earlier than the Start Date");

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12530")]
        [Description("Verify that Diagnosed Date validation message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases06()
        {
            var startDate = DateTime.Now.Date;
            var diagnosedDate = DateTime.Now.Date.AddDays(10);

            #region Health Issue Type

            var _healthIssueTypeName = "Health Issue_123";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectDiagnosed("1")
                .InsertDiagnosedDate(diagnosedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Diagnosed Date cannot be in the future");

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12531")]
        [Description("Verify system does not allow user to create Health Details record when 'Diagnosed Date' after 'Current Date' in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases07()
        {
            var startDate = DateTime.Now.Date.AddMonths(-5);
            var diagnosedDate = DateTime.Now.Date.AddDays(-45);
            var endDate = DateTime.Now.Date.AddMonths(-4);

            #region Health Issue Type

            var _healthIssueTypeName = "Health Issue_123";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDiagnosedDate(diagnosedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Diagnosed date cannot be later than the end date");

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12532")]
        [Description("Verify system allows user to create Health Details record when 'End Date' is blank in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases08()
        {
            var startDate = DateTime.Now.Date.AddMonths(-5);
            var diagnosedDate = DateTime.Now.Date;

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDiagnosedDate(diagnosedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad();

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }
        [TestProperty("JiraIssueID", "CDV6-12533")]
        [Description("Verify system allows user to create Health Details record when 'End Date' is blank in New Health Details screen.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases09()
        {
            var startDate = DateTime.Now.AddMonths(-5);
            var diagnosedDate = DateTime.Now.Date.AddDays(-5);

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            #region Person Health Detail

            Guid HealthDetailRecord = dbHelper.personHealthDetail.CreatePersonHealthDetailRecord(_personId, _teamId, _healthIssueTypeId, 2, startDate, diagnosedDate, "HealthDetails");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .OpenPersonHealthDetailRecord(HealthDetailRecord.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("Health Detail for " + _personFullName + " created by " + _defaultUserFullname + " on ")
                .ValidateEndDate("")
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad();

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12534")]
        [Description("Verify that system does not allow to create Health Details record with Same Health Issue and Same Start and End Dates")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases10()
        {
            var startDate = DateTime.Now.AddMonths(-12);
            var endDate = DateTime.Now.Date.AddMonths(-2);
            var startDate2 = DateTime.Now.AddMonths(-12);
            var endDate2 = DateTime.Now.Date.AddMonths(-2);

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate2.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate2.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("An ongoing Person Health Detail record of this type already exists for this person. Click OK.");

        }

        [TestProperty("JiraIssueID", "CDV6-12535")]
        [Description("Enter the Start Date and End Date with overlapping periods - Click on Save" +
         "Verify that system does not allow to create multiple Health Details records with Same Health Issue")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases11()
        {
            var startDate = DateTime.Now.AddMonths(-12);
            var endDate = DateTime.Now.Date.AddMonths(-2);
            var diagnosedDate = DateTime.Now.Date.AddMonths(-3);
            var startDate2 = DateTime.Now.AddMonths(-10);
            var endDate2 = DateTime.Now.Date.AddMonths(-1);

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDiagnosedDate(diagnosedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .SelectViewSelector("Inactive Health Details Related View")
                .ValidateNoRecordMessageVisibile(false)
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_healthIssueTypeName)
                .TapSearchButton()
                .SelectResultElement(_healthIssueTypeId.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .InsertStartDate(startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNotes("Health Details")
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("An ongoing Person Health Detail record of this type already exists for this person. Click OK.").TapCloseButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12536")]
        [Description("Verify that system  allow user to create multiple Health Details records with different Health Issue")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases12()
        {
            var startDate = DateTime.Now.AddMonths(-12);
            var endDate = DateTime.Now.Date.AddMonths(-2);
            var diagnosedDate = DateTime.Now.Date.AddMonths(-3);
            var endDate2 = DateTime.Now.Date.AddMonths(-1);

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            var _healthIssueTypeName2 = "Health Issue_123";
            var _healthIssueTypeId2 = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName2, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            #region Health Detail  Records

            Guid HealthDetailRecord1 = dbHelper.personHealthDetail.CreatePersonHealthDetailRecord(_personId, _teamId, _healthIssueTypeId, 2, startDate, diagnosedDate, "HealthDetails");
            Guid HealthDetailRecord2 = dbHelper.personHealthDetail.CreatePersonHealthDetailRecord(_personId, _teamId, _healthIssueTypeId2, 2, startDate, diagnosedDate, "HealthDetails");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();


            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .OpenPersonHealthDetailRecord(HealthDetailRecord1.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("Health Detail for " + _personFullName + " created by " + _defaultUserFullname + " on ")
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .OpenPersonHealthDetailRecord(HealthDetailRecord2.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("Health Detail for " + _personFullName + " created by " + _defaultUserFullname + " on ")
                .InsertEndDate(endDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .SelectViewSelector("Inactive Health Details Related View")
                .ValidateNoRecordMessageVisibile(false);

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad();

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(2, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12537")]
        [Description("Verify that user is able to delete the Health Details record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases13()
        {
            var startDate = DateTime.Now.Date;
            var diagnosedDate = DateTime.Now.Date.AddDays(-20);

            #region Health Issue Type

            var _healthIssueTypeName = "Asthma";
            var _healthIssueTypeId = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, _healthIssueTypeName, 0, 0, new DateTime(2021, 1, 2));

            #endregion

            #region Health Detail  Record

            Guid HealthDetailRecord = dbHelper.personHealthDetail.CreatePersonHealthDetailRecord(_personId, _teamId, _healthIssueTypeId, 2, startDate, diagnosedDate, "HealthDetails");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .OpenPersonHealthDetailRecord(HealthDetailRecord.ToString());

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("Health Detail for " + _personFullName + " created by " + _defaultUserFullname + " on ")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad();

            var records = dbHelper.personHealthDetail.GetPersonHealthDetailIdByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12571")]
        [Description("Verify that user is able to create a new Health Issue Type.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases14()
        {
            var startDate = DateTime.Now.Date;

            #region Health Issue Type

            var HealthIssueTypeName = "CDV6_3444_HealthDetailType_DO_NOT_USE";
            Guid HealthIssueType = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, HealthIssueTypeName, 0, 0, startDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Health Issue Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Person Health")
                .ClickReferenceDataElement("Health Issue Types");

            healthIssueTypesPage
                .WaitForHealthIssueTypesPageToLoad()
                .SearchHealthIssueTypesRecord(HealthIssueTypeName);

            var records = dbHelper.healthIssueType.GetHealthIssueTypeByID(HealthIssueType);
            Assert.AreEqual(1, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12574")]
        [Description("Verify that newly created Health Issue type is displayed in Health Issue Types lookup.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases15()
        {
            var startDate = DateTime.Now.Date;

            #region Health Issue Type

            var HealthIssueTypeName = "CDV6_3444_HealthDetailType_DO_NOT_USE";
            Guid HealthIssueType = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, HealthIssueTypeName, 0, 0, startDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHealthDetailsPage();

            personHealthDetailPage
                .WaitForPersonHealthDetailsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickNewRecordButton();

            personHealthDetailsRecordPage
                .WaitForPersonHealthDetailRecordPageToLoad("New")
                .ClickHealthIssueLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(HealthIssueTypeName)
                .TapSearchButton()
                .ValidateResultElementPresent(HealthIssueType.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12589")]
        [Description("Verify that user is not able to delete the Health Issue Type for which Health Details record is been created.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases16()
        {
            var startDate = DateTime.Now.Date;
            var diagnosedDate = DateTime.Now.Date.AddDays(-20);

            #region Health Issue Type

            var HealthIssueTypeName = "CDV6_3444_HealthDetailType_DO_NOT_USE";
            Guid HealthIssueType = commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, HealthIssueTypeName, 0, 0, startDate);

            #endregion

            #region Health Detail  Record

            dbHelper.personHealthDetail.CreatePersonHealthDetailRecord(_personId, _teamId, HealthIssueType, 2, startDate, diagnosedDate, "HealthDetails");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Health Issue Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Person Health")
                .ClickReferenceDataElement("Health Issue Types");

            healthIssueTypesPage
                .WaitForHealthIssueTypesPageToLoad()
                .SearchHealthIssueTypesRecord(HealthIssueTypeName)
                .SelectHealthIssueTypeCheckBox()
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("0 item(s) deleted. 1 item(s) not deleted.").TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12590")]
        [Description("Verify that user is able to delete the created Health Issue Type")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_HealthDetail_UITestCases17()
        {
            var startDate = DateTime.Now.Date;
            var HealthIssueTypeName = "CDV6_3444_HealthDetailType_DO_NOT_USE";

            #region Remove  Health Issue type  for the person

            foreach (var healthIsuueTypeID in dbHelper.healthIssueType.GetByHealthIssueTypeName(HealthIssueTypeName))
            {
                foreach (var personHealthIssueId in dbHelper.personHealthDetail.GetByHealthIssueTypeId(healthIsuueTypeID))
                {
                    dbHelper.personHealthDetail.DeletePersonHealthDetail(personHealthIssueId);
                }
                dbHelper.healthIssueType.DeleteHealthIssueType(healthIsuueTypeID);
            }

            #endregion

            #region Health Detail  Record

            commonMethodsDB.CreateHealthIssueTypeRecord(_teamId, HealthIssueTypeName, 0, 0, startDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Health Issue Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Person Health")
                .ClickReferenceDataElement("Health Issue Types");

            healthIssueTypesPage
                .WaitForHealthIssueTypesPageToLoad()
                .SearchHealthIssueTypesRecord(HealthIssueTypeName)
                .SelectHealthIssueTypeCheckBox()
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }
}



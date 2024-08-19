using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonActivities_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion


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

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PA BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PA T1", null, _businessUnitId, "907678", "PersonActivitiesT1@careworkstempmail.com", "Person Activities T1", "020 123456");

                #endregion

                #region Create System User Record

                _systemUserName = "PAUser1";
                _systemUserFullName = "Person Activities User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Activities", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8940

        [TestProperty("JiraIssueID", "ACC-9065")]
        [Description("Step 4 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUserId, _systemUserFullName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9066")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateConsentGivenErrorLabelVisibility(true)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateConsentGivenErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9067")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .SelectConsentGiven("No")
                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateNonConsentDetailErrorLabelVisibility(true)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateNonConsentDetailErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9068")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")

                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateReasonForAbsenceErrorLabelVisibility(true)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateReasonForAbsenceErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .InsertTextOnReasonForAbsence("Reason Number 1\r\nReason Number 2")
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Absent")
                .ValidateReasonForAbsenceText("Reason Number 1\r\nReason Number 2");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9069")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod05()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)
                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")

                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateReasonConsentDeclinedErrorLabelVisibility(true)
                .ValidateEncouragementGivenErrorLabelVisibility(true)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateReasonConsentDeclinedErrorLabelText("Please fill out this field.")
                .ValidateEncouragementGivenErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .InsertTextOnReasonConsentDeclined("Reason Number 1\r\nReason Number 2")
                .InsertTextOnEncouragementGiven("Encouragement Number 1\r\nEncouragement Number 2")
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Reason Number 1\r\nReason Number 2")
                .ValidateEncouragementGivenText("Encouragement Number 1\r\nEncouragement Number 2")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9070")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod06()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);
            var deferredToDate = DateTime.Now.AddDays(7);

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")

                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(true)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")
                .ValidateDeferredToTimeOrShiftErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .ClickSaveAndCloseButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(true)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateDeferredToTimeErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .InsertDeferredToTime("09:15")
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("09:15");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9071")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod07()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Team

            var _defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true)[0];

            #endregion

            #region Care Periods

            var carePeriodsId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_defaultTeamId, "7 AM", new TimeSpan(7, 0, 0));

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);
            var deferredToDate = DateTime.Now.AddDays(7);

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")

                .ClickSaveButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(true)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")
                .ValidateDeferredToTimeOrShiftErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Shift")
                .ClickSaveAndCloseButton();

            personActivityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(true)

                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateDeferredToShiftErrorLabelText("Please fill out this field.");

            personActivityRecordPage
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodsId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9072")]
        [Description("Step 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod08()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);
            var deferredToDate = DateTime.Now.AddDays(-3);

            personActivityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .InsertDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("\"Deferred To?\" cannot be before today.").TapCloseButton();

            deferredToDate = DateTime.Now.AddDays(3);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateTopPageNotificationVisibility(false)
                .ValidateDeferredToDateErrorLabelVisibility(false)
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)
                .ValidateDeferredToTimeErrorLabelVisibility(false)
                .ValidateDeferredToShiftErrorLabelVisibility(false)
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("08:45");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-9073")]
        [Description("Step 6 to 7 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod09()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Activities

            var personActivityId_AnimalTherapy = dbHelper.personActivities.GetByName("Animal Therapy")[0];
            var personActivityId_Other = dbHelper.personActivities.GetByName("Other")[0];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("Yes")

                .ValidateActivitiesOtherVisibility(false)

                .ClickActivitiesParticipatedInLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Animal Therapy").TapSearchButton().SelectResultElement(personActivityId_AnimalTherapy);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateActivitiesOtherVisibility(false)
                .ClickActivitiesParticipatedInLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Other").TapSearchButton().SelectResultElement(personActivityId_Other);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateActivitiesOtherVisibility(true)

                .InsertTextOnActivitiesOther("Swimming")

                .InsertTextOnDetailsOfActivity("Detail 1\r\nDetail 2")

                .SelectEnjoymentOfActivity("Unknown")
                .SelectEnjoymentOfActivity("Disliked")
                .SelectEnjoymentOfActivity("Neither")
                .SelectEnjoymentOfActivity("Enjoyed");

            #endregion
            
            #region Step 7

            personActivityRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Living Room").TapSearchButton().SelectResultElement(carePhysicalLocationId_LivingRoom);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Other").TapSearchButton().SelectResultElement(carePhysicalLocationId_Other);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(true)
                .InsertTextOnLocationIfOther("Garden")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(false)
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .InsertTextOnActionTaken("Small conversations")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Kai was slightly depressed\r\nThe small conversation helped him")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .SelectAssistanceAmount("Some");

            personActivityRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("Yes");

            personActivityRecordPage
                .ValidateActivitiesParticipatedIn_SelectedElementLinkText(personActivityId_AnimalTherapy, "Animal Therapy")
                .ValidateActivitiesParticipatedIn_SelectedElementLinkText(personActivityId_Other, "Other")
                .ValidateActivitiesOtherText("Swimming")
                .ValidateDetailsOfActivityText("Detail 1\r\nDetail 2")
                .ValidateEnjoymentOfActivitySelectedText("Enjoyed");

            personActivityRecordPage
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_Other, "Other")
                .ValidateLocationIfOtherText("Garden")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Small conversations")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateAdditionalNotesText("Kai was slightly depressed\r\nThe small conversation helped him")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some");


            personActivityRecordPage
                .ValidateCareNoteText("Kai participated in the following activity(s): Animal Therapy and Swimming.\r\nThe activity involved: Detail 1 Detail 2.\r\nKai's overall enjoyment of the activity was: Enjoyed.\r\nKai was in the Living Room and Garden.\r\nKai came across as OK.\r\nThe action taken was: Small conversations.\r\nKai required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nKai was assisted by 1 colleague(s).\r\nOverall I spent 30 minutes with Kai.\r\nWe would like to note that: Kai was slightly depressed The small conversation helped him.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9074")]
        [Description("Step 8 to 9 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod10()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            var dailycarerecordid = 6; //Continence Care
            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C");

            #endregion

            #region Step 8 & 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Kai.") //the only care preference record saved is for Continence Care records
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Preferences

            dailycarerecordid = 8; //Activities
            var carePreferenceID = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference 1\r\nPreference 2\r\nPreference 3");

            #endregion

            personActivitiesPage
                .ClickNewRecordButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Preference 1\r\nPreference 2\r\nPreference 3");

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Absent reason ...")
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            #region Care Preferences

            //this update will not change the stored value for the activity record
            dbHelper.cpPersonCarePreferences.UpdateCarePreferences(carePreferenceID, "Preference 4\r\nPreference 5\r\nPreference 6");

            #endregion

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Preference 1\r\nPreference 2\r\nPreference 3");

            

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-9075")]
        [Description("Step 10 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod11()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Absent reason ...")
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .ValidateRecordCellText(personActivityId, 2, dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00")
                .ValidateRecordCellText(personActivityId, 3, "No")
                .ValidateRecordCellText(personActivityId, 4, "Absent")
                .ValidateRecordCellText(personActivityId, 5, "");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-9076")]
        [Description("Step 12 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod12()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Activities

            var personActivityId_HomeSocialEvent = dbHelper.personActivities.GetByName("Home Social Event")[0];
            var personActivityId_NewspaperMagazine = dbHelper.personActivities.GetByName("Newspaper/Magazine")[0];
            var personActivityId_BirthdayCelebration = dbHelper.personActivities.GetByName("Birthday Celebration")[0];
            var personActivityId_MusicalEntertainment = dbHelper.personActivities.GetByName("Musical Entertainment")[0];
            var personActivityId_Games = dbHelper.personActivities.GetByName("Games")[0];
            var personActivityId_Lifeskills = dbHelper.personActivities.GetByName("Life skills (Gardening, Cooking/Baking, Laundry)")[0];
            var personActivityId_CinemaFilm = dbHelper.personActivities.GetByName("Cinema/Film")[0];
            var personActivityId_Exercise = dbHelper.personActivities.GetByName("Exercise")[0];
            var personActivityId_TV = dbHelper.personActivities.GetByName("TV")[0];
            var personActivityId_IndependentActivity = dbHelper.personActivities.GetByName("Independent Activity")[0];
            var personActivityId_SpiritualEvent = dbHelper.personActivities.GetByName("Spiritual Event")[0];
            var personActivityId_AnimalTherapy = dbHelper.personActivities.GetByName("Animal Therapy")[0];
            var personActivityId_PersonalCareEvent = dbHelper.personActivities.GetByName("Personal Care Event")[0];
            var personActivityId_Other = dbHelper.personActivities.GetByName("Other")[0];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_DiningRoom = dbHelper.carePhysicalLocation.GetByName("Dining Room")[0];
            var carePhysicalLocationId_Garden = dbHelper.carePhysicalLocation.GetByName("Garden")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("Yes")

                .ValidateActivitiesOtherVisibility(false)

                .ClickActivitiesParticipatedInLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Home Social Event").TapSearchButton().AddElementToList(personActivityId_HomeSocialEvent)
                .TypeSearchQuery("Newspaper/Magazine").TapSearchButton().AddElementToList(personActivityId_NewspaperMagazine)
                .TypeSearchQuery("Birthday Celebration").TapSearchButton().AddElementToList(personActivityId_BirthdayCelebration)
                .TypeSearchQuery("Musical Entertainment").TapSearchButton().AddElementToList(personActivityId_MusicalEntertainment)
                .TypeSearchQuery("Games").TapSearchButton().AddElementToList(personActivityId_Games)
                .TypeSearchQuery("Life skills (Gardening, Cooking/Baking, Laundry)").TapSearchButton().AddElementToList(personActivityId_Lifeskills)
                .TypeSearchQuery("Cinema/Film").TapSearchButton().AddElementToList(personActivityId_CinemaFilm)
                .TypeSearchQuery("Exercise").TapSearchButton().AddElementToList(personActivityId_Exercise)
                .TypeSearchQuery("TV").TapSearchButton().AddElementToList(personActivityId_TV)
                .TypeSearchQuery("Independent Activity").TapSearchButton().AddElementToList(personActivityId_IndependentActivity)
                .TypeSearchQuery("Spiritual Event").TapSearchButton().AddElementToList(personActivityId_SpiritualEvent)
                .TypeSearchQuery("Animal Therapy").TapSearchButton().AddElementToList(personActivityId_AnimalTherapy)
                .TypeSearchQuery("Personal Care Event").TapSearchButton().AddElementToList(personActivityId_PersonalCareEvent)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(personActivityId_Other)
                .TapOKButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TypeSearchQuery("Dining Room").TapSearchButton().AddElementToList(carePhysicalLocationId_DiningRoom)
                .TypeSearchQuery("Garden").TapSearchButton().AddElementToList(carePhysicalLocationId_Garden)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(carePhysicalLocationId_Other)
                .TapOKButton();

            personActivityRecordPage
                .WaitForPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9077")]
        [Description("Additional step where Care Provided Without Consent is set to Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Activities")]
        public void PersonActivities_ACC2699_UITestMethod13()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Kai";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Activities

            var personActivityId_AnimalTherapy = dbHelper.personActivities.GetByName("Animal Therapy")[0];
            var personActivityId_Other = dbHelper.personActivities.GetByName("Other")[0];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion



            #region Care provided without consent

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToActivitiesPage();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-2);

            personActivityRecordPage
                .WaitForPageToLoad()

                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Reason Number 1\r\nReason Number 2")
                .InsertTextOnEncouragementGiven("Encouragement Number 1\r\nEncouragement Number 2")
                .ClickCareProvidedWithoutConsent_YesRadioButton();

            personActivityRecordPage
                .ClickActivitiesParticipatedInLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Animal Therapy").TapSearchButton().AddElementToList(personActivityId_AnimalTherapy)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(personActivityId_Other)
                .TapOKButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActivitiesOther("Swimming")
                .InsertTextOnDetailsOfActivity("Detail 1\r\nDetail 2")
                .SelectEnjoymentOfActivity("Enjoyed")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(carePhysicalLocationId_Other)
                .TapOKButton();

            personActivityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Garden")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .InsertTextOnActionTaken("Small conversations")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Kai was slightly depressed\r\nThe small conversation helped him")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .SelectAssistanceAmount("Some");

            personActivityRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personActivitiesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var personActivities = dbHelper.cpPersonActivities.GetByPersonId(personId);
            Assert.AreEqual(1, personActivities.Count);
            var personActivityId = personActivities[0];

            personActivitiesPage
                .OpenRecord(personActivityId);

            personActivityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("No preferences recorded, please check with Kai.")
                .ValidateResponsibleTeamLinkText("PA T1")
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Reason Number 1\r\nReason Number 2")
                .ValidateEncouragementGivenText("Encouragement Number 1\r\nEncouragement Number 2")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked();

            personActivityRecordPage
                .ValidateActivitiesParticipatedIn_SelectedElementLinkText(personActivityId_AnimalTherapy, "Animal Therapy")
                .ValidateActivitiesParticipatedIn_SelectedElementLinkText(personActivityId_Other, "Other")
                .ValidateActivitiesOtherText("Swimming")
                .ValidateDetailsOfActivityText("Detail 1\r\nDetail 2")
                .ValidateEnjoymentOfActivitySelectedText("Enjoyed");

            personActivityRecordPage
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_Other, "Other")
                .ValidateLocationIfOtherText("Garden")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Small conversations")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateAdditionalNotesText("Kai was slightly depressed\r\nThe small conversation helped him")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some");

            personActivityRecordPage
                .ValidateCareNoteText("Kai participated in the following activity(s): Animal Therapy and Swimming.\r\nThe activity involved: Detail 1 Detail 2.\r\nKai's overall enjoyment of the activity was: Enjoyed.\r\nKai was in the Living Room and Garden.\r\nKai came across as OK.\r\nThe action taken was: Small conversations.\r\nKai required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nKai was assisted by 1 colleague(s).\r\nOverall I spent 30 minutes with Kai.\r\nWe would like to note that: Kai was slightly depressed The small conversation helped him.");

            #endregion

        }

        #endregion

    }

}
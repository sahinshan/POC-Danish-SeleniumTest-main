using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonPainManagement_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _defaultTeam;
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PPM BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PPM T1", null, _businessUnitId, "907678", "PersonPainManagementT1@careworkstempmail.com", "Person Pain Management T1", "020 123456");
                _defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

                #endregion

                #region Security Profiles

                var securityProfilesList = new List<Guid>();

                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (View)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)").First());

                #endregion

                #region Create System User Record

                _systemUserName = "ppmrostereduser1";
                _systemUserFullName = "Person Pain Management Rostered User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Pain Management", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

                #endregion

                #region Team Member

                commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-9260

        [TestProperty("JiraIssueID", "ACC-9298")]
        [Description("Save record with empty mandatory fields (Consent Given not inserted)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Save record with empty mandatory fields

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            painManagementRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)

                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(true)
                .ValidateConsentGivenErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9299")]
        [Description("Save record with empty mandatory fields (Consent Given = No)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Save record with empty mandatory fields

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            painManagementRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ValidateNonConsentDetailErrorLabelVisibility(false)
                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .SelectConsentGiven("No")
                .ClickSaveButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(true)
                .ValidateNonConsentDetailErrorLabelText("Please fill out this field.")

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(false)

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            painManagementRecordPage
                .SelectNonConsentDetail("Absent")
                .ClickSaveAndCloseButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(true)
                .ValidateReasonForAbsenceErrorLabelText("Please fill out this field.")

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(false)

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            painManagementRecordPage
                .SelectNonConsentDetail("Declined")
                .ClickSaveAndCloseButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(true)
                .ValidateReasonConsentDeclinedErrorLabelText("Please fill out this field.")

                .ValidateEncouragementGivenErrorLabelVisibility(true)
                .ValidateEncouragementGivenErrorLabelText("Please fill out this field.")

                .ValidateDeferredToDateErrorLabelVisibility(false)

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            painManagementRecordPage
                .SelectNonConsentDetail("Deferred")
                .ClickSaveAndCloseButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(true)
                .ValidateDeferredToTimeOrShiftErrorLabelText("Please fill out this field.")

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            painManagementRecordPage
                .SelectDeferredToTimeOrShift("Time")
                .ClickSaveAndCloseButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(true)
                .ValidateDeferredToTimeErrorLabelText("Please fill out this field.")

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            painManagementRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickSaveAndCloseButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(true)
                .ValidateDeferredToShiftErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9300")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Absent)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Went to the hospital")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, allRecords.Count);
            var cpPersonPainManagementSupportId = allRecords[0];

            painManagementPage
                .OpenRecord(cpPersonPainManagementSupportId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Absent")
                .ValidateReasonForAbsenceText("Went to the hospital")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9301")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Declined)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of pain management")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, allRecords.Count);
            var cpPersonPainManagementSupportId = allRecords[0];

            painManagementPage
                .OpenRecord(cpPersonPainManagementSupportId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of pain management")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9302")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Deferred)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod05()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Periods

            var carePeriodId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_defaultTeam, "7 AM", new TimeSpan(7, 0, 0));

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);
            var deferredToDate = DateTime.Now.AddDays(2);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .InsertDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, allRecords.Count);
            var cpPersonPainManagementSupportId = allRecords[0];

            painManagementPage
                .OpenRecord(cpPersonPainManagementSupportId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("08:45");

            painManagementRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(cpPersonPainManagementSupportId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9303")]
        [Description("Save record with empty mandatory fields (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod06()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion


            #region Save record with empty mandatory fields

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            painManagementRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)

                /*Pain Review*/
                .ValidateIsThePersonInPainErrorLabelVisibility(false)
                .ValidateWhereIsThePainErrorLabelVisibility(false)
                .ValidateSeverityOfPainErrorLabelVisibility(false)

                /*Abbey Pain Scale*/
                .ValidateVocalisationErrorLabelVisibility(false)
                .ValidateFacialExpressionErrorLabelVisibility(false)
                .ValidateChangeInBodyLanguageErrorLabelVisibility(false)
                .ValidateBehaviouralChangeErrorLabelVisibility(false)
                .ValidatePhysiologicalChangeErrorLabelVisibility(false)
                .ValidatePhysicalChangesErrorLabelVisibility(false)
                .ValidateReviewDetailsErrorLabelVisibility(false)

                /*Additional Information*/
                .ValidateLocationErrorLabelVisibility(false)
                .ValidateAssistanceNeededErrorLabelVisibility(false)
                .ValidateWellbeingErrorLabelVisibility(false)
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .SelectConsentGiven("Yes")
                .ClickSaveButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                /*Pain Review*/
                .ValidateIsThePersonInPainErrorLabelVisibility(true)
                .ValidateIsThePersonInPainErrorLabelText("Please fill out this field.")
                .ValidateWhereIsThePainErrorLabelVisibility(false)
                .ValidateSeverityOfPainErrorLabelVisibility(false)

                /*Abbey Pain Scale*/
                .ValidateVocalisationErrorLabelVisibility(false)
                .ValidateFacialExpressionErrorLabelVisibility(false)
                .ValidateChangeInBodyLanguageErrorLabelVisibility(false)
                .ValidateBehaviouralChangeErrorLabelVisibility(false)
                .ValidatePhysiologicalChangeErrorLabelVisibility(false)
                .ValidatePhysicalChangesErrorLabelVisibility(false)
                .ValidateReviewDetailsErrorLabelVisibility(false)

                /*Additional Information*/
                .ValidateLocationErrorLabelVisibility(true)
                .ValidateLocationErrorLabelText("Please fill out this field.")
                .ValidateAssistanceNeededErrorLabelVisibility(true)
                .ValidateAssistanceNeededErrorLabelText("Please fill out this field.")
                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelText("Please fill out this field.")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .ClickIsThePersonInPain_YesRadioButton()
                .ClickSaveButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                /*Pain Review*/
                .ValidateIsThePersonInPainErrorLabelVisibility(false)
                .ValidateWhereIsThePainErrorLabelVisibility(true)
                .ValidateWhereIsThePainErrorLabelText("Please fill out this field.")
                .ValidateSeverityOfPainErrorLabelVisibility(true)
                .ValidateSeverityOfPainErrorLabelText("Please fill out this field.")

                /*Abbey Pain Scale*/
                .ValidateVocalisationErrorLabelVisibility(true)
                .ValidateVocalisationErrorLabelText("Please fill out this field.")
                .ValidateFacialExpressionErrorLabelVisibility(true)
                .ValidateFacialExpressionErrorLabelText("Please fill out this field.")
                .ValidateChangeInBodyLanguageErrorLabelVisibility(true)
                .ValidateChangeInBodyLanguageErrorLabelText("Please fill out this field.")
                .ValidateBehaviouralChangeErrorLabelVisibility(true)
                .ValidateBehaviouralChangeErrorLabelText("Please fill out this field.")
                .ValidatePhysiologicalChangeErrorLabelVisibility(true)
                .ValidatePhysiologicalChangeErrorLabelText("Please fill out this field.")
                .ValidatePhysicalChangesErrorLabelVisibility(true)
                .ValidatePhysicalChangesErrorLabelText("Please fill out this field.")
                .ValidateReviewDetailsErrorLabelVisibility(false)

                /*Additional Information*/
                .ValidateLocationErrorLabelVisibility(true)
                .ValidateLocationErrorLabelText("Please fill out this field.")
                .ValidateAssistanceNeededErrorLabelVisibility(true)
                .ValidateAssistanceNeededErrorLabelText("Please fill out this field.")
                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelText("Please fill out this field.")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .ClickSaveButton();

            painManagementRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                /*Pain Review*/
                .ValidateIsThePersonInPainErrorLabelVisibility(false)
                .ValidateWhereIsThePainErrorLabelVisibility(true)
                .ValidateWhereIsThePainErrorLabelText("Please fill out this field.")
                .ValidateSeverityOfPainErrorLabelVisibility(true)
                .ValidateSeverityOfPainErrorLabelText("Please fill out this field.")

                /*Abbey Pain Scale*/
                .ValidateVocalisationErrorLabelVisibility(true)
                .ValidateVocalisationErrorLabelText("Please fill out this field.")
                .ValidateFacialExpressionErrorLabelVisibility(true)
                .ValidateFacialExpressionErrorLabelText("Please fill out this field.")
                .ValidateChangeInBodyLanguageErrorLabelVisibility(true)
                .ValidateChangeInBodyLanguageErrorLabelText("Please fill out this field.")
                .ValidateBehaviouralChangeErrorLabelVisibility(true)
                .ValidateBehaviouralChangeErrorLabelText("Please fill out this field.")
                .ValidatePhysiologicalChangeErrorLabelVisibility(true)
                .ValidatePhysiologicalChangeErrorLabelText("Please fill out this field.")
                .ValidatePhysicalChangesErrorLabelVisibility(true)
                .ValidatePhysicalChangesErrorLabelText("Please fill out this field.")
                .ValidateReviewDetailsErrorLabelVisibility(true)
                .ValidateReviewDetailsErrorLabelText("Please fill out this field.")

                /*Additional Information*/
                .ValidateLocationErrorLabelVisibility(true)
                .ValidateLocationErrorLabelText("Please fill out this field.")
                .ValidateAssistanceNeededErrorLabelVisibility(true)
                .ValidateAssistanceNeededErrorLabelText("Please fill out this field.")
                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelText("Please fill out this field.")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .ClickSaveButton();



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9304")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod07()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            painManagementRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickIsThePersonInPain_NoRadioButton()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            painManagementRecordPage
                .WaitForPageToLoad();

            painManagementRecordPage
                .ValidateCareNoteText("Elowen was not in pain.\r\nElowen was in the Living Room.\r\nElowen came across as Happy.\r\nElowen did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var painManagementRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, painManagementRecords.Count);
            var painManagementId = painManagementRecords[0];

            painManagementPage
                .OpenRecord(painManagementId);

            painManagementRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("Yes")

                //Pain Review
                .ValidateIsThePersonInPain_NoRadioButtonChecked()
                .ValidateWhereIsThePainVisibility(false)
                .ValidateSeverityOfPainVisibility(false)

                //Abbey Pain Scale
                .ValidateVocalisationVisibility(false)
                .ValidateFacialExpressionVisibility(false)
                .ValidateChangeInBodyLanguageVisibility(false)
                .ValidateBehaviouralChangeVisibility(false)
                .ValidatePhysiologicalChangeVisibility(false)
                .ValidatePhysicalChangesVisibility(false)
                .ValidateTotalPainScoreVisibility(false)
                .ValidateTotalPainScoreDescriptionVisibility(false)
                .ValidateReviewDetailsVisibility(false)

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateTotalTimeSpentWithPersonText("")
                .ValidateAdditionalNotesText("")
                .ValidateAssistanceNeededLinkText("Independent")

                //Care Note
                .ValidateCareNoteText("Elowen was not in pain.\r\nElowen was in the Living Room.\r\nElowen came across as Happy.\r\nElowen did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).")

                //Handover
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9305")]
        [Description("Set data in all mandatory and optional fields and save the record (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod08()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion


            #region Set data in all mandatory and optional fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            /*General*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30");

            /*Pain Review*/
            painManagementRecordPage
                .ClickIsThePersonInPain_YesRadioButton()
                .InsertTextOnWhereIsThePain("Entire Body")
                .SelectSeverityOfPain("Moderate");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Mild")
                .SelectFacialExpression("Moderate")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Mild")
                .SelectPhysiologicalChange("Moderate")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("12")
                .ValidateSelectedTotalPainScoreDescription("Moderate Pain")
                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .InsertTextOnReviewDetails("Medication is required for the pain");

            /*Additional Information*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(carePhysicalLocationId_Other)
                .TapOKButton();

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Garden")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Talked with him")
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("At the end he was happier")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            painManagementRecordPage
                .SelectAssistanceAmount("Some")
                .WaitForPageToLoad();

            painManagementRecordPage
                .ValidateCareNoteText("Elowen had Moderate pain in their Entire Body.\r\nElowen's responses to the Abbey Pain Scale were as follows:\r\nVocalisation: Mild.\r\nFacial Expression: Moderate.\r\nChange in Body Language: Severe.\r\nBehavioural Change: Mild.\r\nPhysiological Change: Moderate.\r\nPhysical Change: Severe.\r\nThe total pain score for Elowen was 12 - Moderate Pain.\r\nElowen was in the Living Room and Garden.\r\nElowen came across as OK.\r\nThe action taken was: Talked with him.\r\nElowen required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Elowen.\r\nWe would like to note that: At the end he was happier.")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var painManagementRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, painManagementRecords.Count);
            var painManagementId = painManagementRecords[0];

            painManagementPage
                .OpenRecord(painManagementId);

            painManagementRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("Yes")

                //Pain Review
                .ValidateIsThePersonInPain_YesRadioButtonChecked()
                .ValidateWhereIsThePainVisibility(true)
                .ValidateSeverityOfPainVisibility(true)
                .ValidateWhereIsThePainText("Entire Body")
                .ValidateSeverityOfPainSelectedText("Moderate")

                //Abbey Pain Scale
                .ValidateVocalisationVisibility(true)
                .ValidateFacialExpressionVisibility(true)
                .ValidateChangeInBodyLanguageVisibility(true)
                .ValidateBehaviouralChangeVisibility(true)
                .ValidatePhysiologicalChangeVisibility(true)
                .ValidatePhysicalChangesVisibility(true)
                .ValidateTotalPainScoreVisibility(true)
                .ValidateTotalPainScoreDescriptionVisibility(true)
                .ValidateReviewDetailsVisibility(true)
                .ValidateVocalisationSelectedText("Mild")
                .ValidateFacialExpressionSelectedText("Moderate")
                .ValidateChangeInBodyLanguageSelectedText("Severe")
                .ValidateBehaviouralChangeSelectedText("Mild")
                .ValidatePhysiologicalChangeSelectedText("Moderate")
                .ValidatePhysicalChangesSelectedText("Severe")
                .ValidateTotalPainScoreText("12")
                .ValidateSelectedTotalPainScoreDescription("Moderate Pain")
                .ValidateReviewRequiredBySeniorColleague_YesRadioButtonChecked()
                .ValidateReviewDetailsText("Medication is required for the pain")

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_Other, "Other")
                .ValidateLocationIfOtherText("Garden")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Talked with him")
                .ValidateTotalTimeSpentWithPersonText("45")
                .ValidateAdditionalNotesText("At the end he was happier")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")

                //Care Note
                .ValidateCareNoteText("Elowen had Moderate pain in their Entire Body.\r\nElowen's responses to the Abbey Pain Scale were as follows:\r\nVocalisation: Mild.\r\nFacial Expression: Moderate.\r\nChange in Body Language: Severe.\r\nBehavioural Change: Mild.\r\nPhysiological Change: Moderate.\r\nPhysical Change: Severe.\r\nThe total pain score for Elowen was 12 - Moderate Pain.\r\nElowen was in the Living Room and Garden.\r\nElowen came across as OK.\r\nThe action taken was: Talked with him.\r\nElowen required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Elowen.\r\nWe would like to note that: At the end he was happier.")

                //Handover
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9306")]
        [Description("Create, Update and Delete a record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod09()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Unhappy = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_PhysicalAssistance = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion


            #region Create, Update and Delete a record

            #region Create


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            /*General*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30");

            /*Pain Review*/
            painManagementRecordPage
                .ClickIsThePersonInPain_YesRadioButton()
                .InsertTextOnWhereIsThePain("Entire Body")
                .SelectSeverityOfPain("Moderate");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Mild")
                .SelectFacialExpression("Moderate")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Mild")
                .SelectPhysiologicalChange("Moderate")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("12")
                .ValidateSelectedTotalPainScoreDescription("Moderate Pain")
                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .InsertTextOnReviewDetails("Medication is required for the pain");

            /*Additional Information*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(carePhysicalLocationId_Other)
                .TapOKButton();

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Garden")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Talked with him")
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("At the end he was happier")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            painManagementRecordPage
                .SelectAssistanceAmount("Some")
                .WaitForPageToLoad();

            painManagementRecordPage
                .ValidateCareNoteText("Elowen had Moderate pain in their Entire Body.\r\nElowen's responses to the Abbey Pain Scale were as follows:\r\nVocalisation: Mild.\r\nFacial Expression: Moderate.\r\nChange in Body Language: Severe.\r\nBehavioural Change: Mild.\r\nPhysiological Change: Moderate.\r\nPhysical Change: Severe.\r\nThe total pain score for Elowen was 12 - Moderate Pain.\r\nElowen was in the Living Room and Garden.\r\nElowen came across as OK.\r\nThe action taken was: Talked with him.\r\nElowen required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Elowen.\r\nWe would like to note that: At the end he was happier.")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var painManagementRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, painManagementRecords.Count);
            var painManagementId = painManagementRecords[0];

            #endregion

            #region Update

            painManagementPage
                .OpenRecord(painManagementId);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnWhereIsThePain("Legs only")
                .SelectSeverityOfPain("Severe");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Moderate")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Absent")
                .SelectPhysiologicalChange("Moderate")
                .SelectPhysicalChanges("Absent")
                .ValidateTotalPainScoreText("7")
                .ValidateSelectedTotalPainScoreDescription("Mild Pain")
                .ClickReviewRequiredBySeniorColleague_NoRadioButton();

            /*Additional Information*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickLocation_SelectedElementRemoveButton(carePhysicalLocationId_LivingRoom.ToString())
                .InsertTextOnLocationIfOther("Kitchen")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Unhappy", careWellbeingId_Unhappy);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Quick chat with him")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("We was a bit better at the end")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId_PhysicalAssistance);

            painManagementRecordPage
                .SelectAssistanceAmount("A Lot")
                .WaitForPageToLoad();

            painManagementRecordPage
                .ValidateCareNoteText("Elowen had Severe pain in their Legs only.\r\nElowen's responses to the Abbey Pain Scale were as follows:\r\nVocalisation: Absent.\r\nFacial Expression: Moderate.\r\nChange in Body Language: Severe.\r\nBehavioural Change: Absent.\r\nPhysiological Change: Moderate.\r\nPhysical Change: Absent.\r\nThe total pain score for Elowen was 7 - Mild Pain.\r\nElowen was in the Kitchen.\r\nElowen came across as Unhappy.\r\nThe action taken was: Quick chat with him.\r\nElowen required assistance: Physical Assistance. Amount given: A Lot.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).\r\nOverall I spent 30 minutes with Elowen.\r\nWe would like to note that: We was a bit better at the end.")
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(painManagementId);

            painManagementRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.")
                .ValidateConsentGivenSelectedText("Yes")

                //Pain Review
                .ValidateIsThePersonInPain_YesRadioButtonChecked()
                .ValidateWhereIsThePainVisibility(true)
                .ValidateSeverityOfPainVisibility(true)
                .ValidateWhereIsThePainText("Legs only")
                .ValidateSeverityOfPainSelectedText("Severe")

                //Abbey Pain Scale
                .ValidateVocalisationVisibility(true)
                .ValidateFacialExpressionVisibility(true)
                .ValidateChangeInBodyLanguageVisibility(true)
                .ValidateBehaviouralChangeVisibility(true)
                .ValidatePhysiologicalChangeVisibility(true)
                .ValidatePhysicalChangesVisibility(true)
                .ValidateTotalPainScoreVisibility(true)
                .ValidateTotalPainScoreDescriptionVisibility(true)
                .ValidateReviewDetailsVisibility(false)
                .ValidateVocalisationSelectedText("Absent")
                .ValidateFacialExpressionSelectedText("Moderate")
                .ValidateChangeInBodyLanguageSelectedText("Severe")
                .ValidateBehaviouralChangeSelectedText("Absent")
                .ValidatePhysiologicalChangeSelectedText("Moderate")
                .ValidatePhysicalChangesSelectedText("Absent")
                .ValidateTotalPainScoreText("7")
                .ValidateSelectedTotalPainScoreDescription("Mild Pain")
                .ValidateReviewRequiredBySeniorColleague_NoRadioButtonChecked()

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_Other, "Other")
                .ValidateLocationIfOtherText("Kitchen")
                .ValidateWellbeingLinkText("Unhappy")
                .ValidateActionTakenText("Quick chat with him")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateAdditionalNotesText("We was a bit better at the end")
                .ValidateAssistanceNeededLinkText("Physical Assistance")
                .ValidateAssistanceAmountSelectedText("A Lot")

                //Care Note
                .ValidateCareNoteText("Elowen had Severe pain in their Legs only.\r\nElowen's responses to the Abbey Pain Scale were as follows:\r\nVocalisation: Absent.\r\nFacial Expression: Moderate.\r\nChange in Body Language: Severe.\r\nBehavioural Change: Absent.\r\nPhysiological Change: Moderate.\r\nPhysical Change: Absent.\r\nThe total pain score for Elowen was 7 - Mild Pain.\r\nElowen was in the Kitchen.\r\nElowen came across as Unhappy.\r\nThe action taken was: Quick chat with him.\r\nElowen required assistance: Physical Assistance. Amount given: A Lot.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nElowen was assisted by 1 colleague(s).\r\nOverall I spent 30 minutes with Elowen.\r\nWe would like to note that: We was a bit better at the end.")

                //Handover
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

            #region Delete

            painManagementRecordPage
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            painManagementRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(0, painManagementRecords.Count);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9307")]
        [Description("Validate that Care Preferences with Care Record set to 'Pain Management' are loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod10()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Care Preferences

            var dailycarerecordid = 11; //Pain Management
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C");

            #endregion


            #region Validate that Care Preferences with Care Record set to 'Pain Management' are loaded

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Preference A\r\nPreference B\r\nPreference C")
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickIsThePersonInPain_NoRadioButton()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            painManagementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("")
                .InsertTextOnAdditionalNotes("")
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickSaveAndCloseButton();

            painManagementPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            //get the record id
            var painManagementRecords = dbHelper.cpPersonPainManagement.GetByPersonId(personId);
            Assert.AreEqual(1, painManagementRecords.Count);
            var painManagementId = painManagementRecords[0];


            //update the care preference
            dbHelper.cpPersonCarePreferences.UpdateCarePreferences(carePreferenceId, "Preference D\r\nPreference E\r\nPreference F");

            painManagementPage
                .OpenRecord(painManagementId);

            painManagementRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("Preference A\r\nPreference B\r\nPreference C")
                .ValidateConsentGivenSelectedText("Yes")

                //Details
                .ValidateIsThePersonInPain_NoRadioButtonChecked()

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateTotalTimeSpentWithPersonText("")
                .ValidateAdditionalNotesText("")
                .ValidateAssistanceNeededLinkText("Independent")

                //Handover
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9308")]
        [Description("Validate that Care Preferences with Care Record NOT set to 'Pain Management' are NOT loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod11()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            var dailycarerecordid = 8; //Activities
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C");

            #endregion


            #region Validate that Care Preferences with Care Record NOT set to 'Pain Management' are NOT loaded

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9309")]
        [Description("Validate that INACTIVE Care Preferences with Care Record set to 'Pain Management' are NOT loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod12()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Care Preferences

            var dailycarerecordid = 11; //Pain Management
            var inactive = true;
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C", inactive);

            #endregion


            #region Validate that INACTIVE Care Preferences with Care Record set to 'Pain Management' are NOT loaded

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            painManagementRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Elowen.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9310")]
        [Description("Validate the calculation of the different pain levels")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Pain Management")]
        public void PersonPainManagement_ACC2059_UITestMethod13()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Elowen";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var carePhysicalLocationId_LivingRoom = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var carePhysicalLocationId_Other = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeingId_Happy = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeingId_Ok = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId_Independent = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeededId_AskedForHelp = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion


            #region Validate calculation of Severe pain

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPainManagementPage();

            painManagementPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            /*General*/
            painManagementRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30");

            /*Pain Review*/
            painManagementRecordPage
                .ClickIsThePersonInPain_YesRadioButton()
                .InsertTextOnWhereIsThePain("Entire Body")
                .SelectSeverityOfPain("Moderate");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Moderate")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Severe")
                .SelectPhysiologicalChange("Severe")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("14")
                .ValidateSelectedTotalPainScoreDescription("Severe Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Severe")
                .SelectFacialExpression("Severe")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Severe")
                .SelectPhysiologicalChange("Severe")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("18")
                .ValidateSelectedTotalPainScoreDescription("Severe Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Absent")
                .SelectChangeInBodyLanguage("Absent")
                .SelectBehaviouralChange("Moderate")
                .SelectPhysiologicalChange("Severe")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("8")
                .ValidateSelectedTotalPainScoreDescription("Moderate Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Mild")
                .SelectChangeInBodyLanguage("Severe")
                .SelectBehaviouralChange("Severe")
                .SelectPhysiologicalChange("Severe")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("13")
                .ValidateSelectedTotalPainScoreDescription("Moderate Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Absent")
                .SelectChangeInBodyLanguage("Absent")
                .SelectBehaviouralChange("Absent")
                .SelectPhysiologicalChange("Absent")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("3")
                .ValidateSelectedTotalPainScoreDescription("Mild Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Absent")
                .SelectChangeInBodyLanguage("Absent")
                .SelectBehaviouralChange("Mild")
                .SelectPhysiologicalChange("Severe")
                .SelectPhysicalChanges("Severe")
                .ValidateTotalPainScoreText("7")
                .ValidateSelectedTotalPainScoreDescription("Mild Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Absent")
                .SelectFacialExpression("Absent")
                .SelectChangeInBodyLanguage("Absent")
                .SelectBehaviouralChange("Absent")
                .SelectPhysiologicalChange("Absent")
                .SelectPhysicalChanges("Absent")
                .ValidateTotalPainScoreText("0")
                .ValidateSelectedTotalPainScoreDescription("No Pain");

            /*Abbey Pain Scale*/
            painManagementRecordPage
                .SelectVocalisation("Mild")
                .SelectFacialExpression("Mild")
                .SelectChangeInBodyLanguage("Absent")
                .SelectBehaviouralChange("Absent")
                .SelectPhysiologicalChange("Absent")
                .SelectPhysicalChanges("Absent")
                .ValidateTotalPainScoreText("2")
                .ValidateSelectedTotalPainScoreDescription("No Pain");



            #endregion

        }

        #endregion

    }

}
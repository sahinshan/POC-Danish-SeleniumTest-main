using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonEmotionalSupport_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PES BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PES T1", null, _businessUnitId, "907678", "PersonEmotionalSupportT1@careworkstempmail.com", "Person Emotional Support T1", "020 123456");
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

                _systemUserName = "pesrostereduser1";
                _systemUserFullName = "Person Emotional Support Rostered User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Emotional Support", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-9036

        [TestProperty("JiraIssueID", "ACC-9175")]
        [Description("Save record with empty mandatory fields (Consent Given not inserted)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            emotionalSupportRecordPage
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

        [TestProperty("JiraIssueID", "ACC-9176")]
        [Description("Save record with empty mandatory fields (Consent Given = No)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ValidateNonConsentDetailErrorLabelVisibility(false)
                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .SelectConsentGiven("No")
                .ClickSaveButton();

            emotionalSupportRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(true)
                .ValidateConsentGivenErrorLabelText("Please fill out this field.")

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(false)

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            emotionalSupportRecordPage
                .SelectNonConsentDetail("Absent")
                .ClickSaveAndCloseButton();

            emotionalSupportRecordPage
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

            emotionalSupportRecordPage
                .SelectNonConsentDetail("Declined")
                .ClickSaveAndCloseButton();

            emotionalSupportRecordPage
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

            emotionalSupportRecordPage
                .SelectNonConsentDetail("Deferred")
                .ClickSaveAndCloseButton();

            emotionalSupportRecordPage
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

            emotionalSupportRecordPage
                .SelectDeferredToTimeOrShift("Time")
                .ClickSaveAndCloseButton();

            emotionalSupportRecordPage
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

            emotionalSupportRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickSaveAndCloseButton();

            emotionalSupportRecordPage
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

        [TestProperty("JiraIssueID", "ACC-9177")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Absent)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Went to the hospital")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonEmotionalSupport = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonEmotionalSupport.Count);
            var cpPersonEmotionalSupportId = allPersonEmotionalSupport[0];

            emotionalSupportPage
                .OpenRecord(cpPersonEmotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Absent")
                .ValidateReasonForAbsenceText("Went to the hospital")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9178")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Declined)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of emotional support")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonEmotionalSupport = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonEmotionalSupport.Count);
            var cpPersonEmotionalSupportId = allPersonEmotionalSupport[0];

            emotionalSupportPage
                .OpenRecord(cpPersonEmotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of emotional support")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9179")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Deferred)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod05()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);
            var deferredToDate = DateTime.Now.AddDays(2);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .InsertDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .InsertDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonEmotionalSupport = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonEmotionalSupport.Count);
            var cpPersonEmotionalSupportId = allPersonEmotionalSupport[0];

            emotionalSupportPage
                .OpenRecord(cpPersonEmotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("08:45");

            emotionalSupportRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(cpPersonEmotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateCreatedOnText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9180")]
        [Description("Save record with empty mandatory fields (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod06()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateTypeEmotionalSupportRequiredErrorLabelVisibility(false)
                .ValidateEmotionalSupportIfOtherErrorLabelVisibility(false)
                .ValidateDescribeSupportGivenErrorLabelVisibility(false)
                .ValidateWhatWasTheOutcomeErrorLabelVisibility(false)

                .SelectConsentGiven("Yes")
                .ClickSaveButton();

            emotionalSupportRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                //Details
                .ValidateTypeEmotionalSupportRequiredErrorLabelVisibility(true)
                .ValidateTypeEmotionalSupportRequiredErrorLabelText("Please fill out this field.")

                .ValidateEmotionalSupportIfOtherErrorLabelVisibility(false)

                .ValidateDescribeSupportGivenErrorLabelVisibility(true)
                .ValidateDescribeSupportGivenErrorLabelText("Please fill out this field.")

                .ValidateWhatWasTheOutcomeErrorLabelVisibility(true)
                .ValidateWhatWasTheOutcomeErrorLabelText("Please fill out this field.")

                //Additional Information
                .ValidateLocationErrorLabelVisibility(true)
                .ValidateLocationErrorLabelText("Please fill out this field.")

                .ValidateLocationIfOtherErrorLabelVisibility(false)

                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelText("Please fill out this field.")

                .ValidateActionTakenErrorLabelVisibility(false)

                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .ValidateAssistanceNeededErrorLabelVisibility(true)
                .ValidateAssistanceNeededErrorLabelText("Please fill out this field.")

                .ValidateAssistanceAmountErrorLabelVisibility(false);

            emotionalSupportRecordPage
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Other").TapSearchButton().SelectResultElement(typeOfEmotionalSupportId_Other);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Other").TapSearchButton().SelectResultElement(carePhysicalLocationId_Other);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                //Details
                .ValidateTypeEmotionalSupportRequiredErrorLabelVisibility(false)

                .ValidateEmotionalSupportIfOtherErrorLabelVisibility(true)
                .ValidateEmotionalSupportIfOtherErrorLabelText("Please fill out this field.")

                .ValidateDescribeSupportGivenErrorLabelVisibility(true)
                .ValidateDescribeSupportGivenErrorLabelText("Please fill out this field.")

                .ValidateWhatWasTheOutcomeErrorLabelVisibility(true)
                .ValidateWhatWasTheOutcomeErrorLabelText("Please fill out this field.")

                //Additional Information
                .ValidateLocationErrorLabelVisibility(false)

                .ValidateLocationIfOtherErrorLabelVisibility(true)
                .ValidateLocationIfOtherErrorLabelText("Please fill out this field.")

                .ValidateWellbeingErrorLabelVisibility(false)

                .ValidateActionTakenErrorLabelVisibility(true)
                .ValidateActionTakenErrorLabelText("Please fill out this field.")

                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)

                .ValidateAssistanceNeededErrorLabelVisibility(false)

                .ValidateAssistanceAmountErrorLabelVisibility(true)
                .ValidateAssistanceAmountErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9181")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod07()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Companionship").TapSearchButton().AddElementToList(typeOfEmotionalSupportId_Companionship)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDescribeSupportGiven("Talked with him")
                .InsertTextOnWhatWasTheOutcome("He was happier");

            emotionalSupportRecordPage
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var emotionalSupportRecords = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, emotionalSupportRecords.Count);
            var emotionalSupportId = emotionalSupportRecords[0];

            emotionalSupportPage
                .OpenRecord(emotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("Yes")

                //Details
                .ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(typeOfEmotionalSupportId_Companionship, "Companionship")
                .ValidateDescribeSupportGivenText("Talked with him")
                .ValidateWhatWasTheOutcomeText("He was happier")

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateTotalTimeSpentWithPersonText("")
                .ValidateAdditionalNotesText("")
                .ValidateAssistanceNeededLinkText("Independent")

                //Care Note
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).")

                //Handover
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9182")]
        [Description("Set data in all mandatory and optional fields and save the record (Consent Given = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod08()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Companionship").TapSearchButton().AddElementToList(typeOfEmotionalSupportId_Companionship)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDescribeSupportGiven("Talked with him")
                .InsertTextOnWhatWasTheOutcome("He was happier")
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("In the beginning he seemed slightly depressed")
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton();

            emotionalSupportRecordPage
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var emotionalSupportRecords = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, emotionalSupportRecords.Count);
            var emotionalSupportId = emotionalSupportRecords[0];

            emotionalSupportPage
                .OpenRecord(emotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("Yes")

                //Details
                .ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(typeOfEmotionalSupportId_Companionship, "Companionship")
                .ValidateDescribeSupportGivenText("Talked with him")
                .ValidateWhatWasTheOutcomeText("He was happier")

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateTotalTimeSpentWithPersonText("45")
                .ValidateAdditionalNotesText("In the beginning he seemed slightly depressed")
                .ValidateAssistanceNeededLinkText("Independent")

                //Care Note
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")

                //Handover
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9183")]
        [Description("Create, Update and Delete a record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod09()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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


            #region Create, Update and Delete a record

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Companionship").TapSearchButton().AddElementToList(typeOfEmotionalSupportId_Companionship)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDescribeSupportGiven("Talked with him")
                .InsertTextOnWhatWasTheOutcome("He was happier")
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("In the beginning he seemed slightly depressed")
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton();

            emotionalSupportRecordPage
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var emotionalSupportRecords = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, emotionalSupportRecords.Count);
            var emotionalSupportId = emotionalSupportRecords[0];

            emotionalSupportPage
                .OpenRecord(emotionalSupportId);


            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(typeOfEmotionalSupportId_Other)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEmotionalSupportIfOther("Art Therapy\r\nMeditation")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(carePhysicalLocationId_Other)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Kitchen")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId_Ok);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Had a long talk with him")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeededId_AskedForHelp);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("55")
                .SelectAssistanceAmount("Some")
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship and Art Therapy Meditation.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room and Kitchen.\r\nEsmond came across as OK.\r\nThe action taken was: Had a long talk with him.\r\nEsmond required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:45:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 55 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(emotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.")
                .ValidateConsentGivenSelectedText("Yes")

                //Details
                .ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(typeOfEmotionalSupportId_Companionship, "Companionship")
                .ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(typeOfEmotionalSupportId_Other, "Other")
                .ValidateEmotionalSupportIfOtherText("Art Therapy\r\nMeditation")
                .ValidateDescribeSupportGivenText("Talked with him")
                .ValidateWhatWasTheOutcomeText("He was happier")

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_Other, "Other")
                .ValidateLocationIfOtherText("Kitchen")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Had a long talk with him")
                .ValidateTotalTimeSpentWithPersonText("55")
                .ValidateAdditionalNotesText("In the beginning he seemed slightly depressed")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")

                //Care Note
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship and Art Therapy Meditation.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room and Kitchen.\r\nEsmond came across as OK.\r\nThe action taken was: Had a long talk with him.\r\nEsmond required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:45:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 55 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")

                //Handover
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();

            emotionalSupportRecordPage
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            emotionalSupportRecords = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(0, emotionalSupportRecords.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9184")]
        [Description("Validate that Care Preferences with Care Record set to 'Emotional Support' are loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod10()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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

            var dailycarerecordid = 10; //Emotional Support
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C");

            #endregion


            #region Validate that Care Preferences with Care Record set to 'Emotional Support' are loaded

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Preference A\r\nPreference B\r\nPreference C")
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:30")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Companionship").TapSearchButton().AddElementToList(typeOfEmotionalSupportId_Companionship)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(carePhysicalLocationId_LivingRoom)
                .TapOKButton();

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeingId_Happy);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId_Independent);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDescribeSupportGiven("Talked with him")
                .InsertTextOnWhatWasTheOutcome("He was happier")
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("In the beginning he seemed slightly depressed")
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton();

            emotionalSupportRecordPage
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")
                .ClickSaveAndCloseButton();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            //get the record id
            var emotionalSupportRecords = dbHelper.cpPersonEmotional.GetByPersonId(personId);
            Assert.AreEqual(1, emotionalSupportRecords.Count);
            var emotionalSupportId = emotionalSupportRecords[0];


            //update the care preference
            dbHelper.cpPersonCarePreferences.UpdateCarePreferences(carePreferenceId, "Preference D\r\nPreference E\r\nPreference F");

            emotionalSupportPage
                .OpenRecord(emotionalSupportId);

            emotionalSupportRecordPage
                .WaitForPageToLoad()

                //General
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateTimeOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:30")
                .ValidatePreferencesText("Preference A\r\nPreference B\r\nPreference C")
                .ValidateConsentGivenSelectedText("Yes")

                //Details
                .ValidateTypeEmotionalSupportRequired_SelectedElementLinkText(typeOfEmotionalSupportId_Companionship, "Companionship")
                .ValidateDescribeSupportGivenText("Talked with him")
                .ValidateWhatWasTheOutcomeText("He was happier")

                //Additional Information
                .ValidateLocation_SelectedElementLinkText(carePhysicalLocationId_LivingRoom, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateTotalTimeSpentWithPersonText("45")
                .ValidateAdditionalNotesText("In the beginning he seemed slightly depressed")
                .ValidateAssistanceNeededLinkText("Independent")

                //Care Note
                .ValidateCareNoteText("Esmond required emotional support of type: Companionship.\r\nThe support given was: Talked with him.\r\nThe outcome was: He was happier.\r\nEsmond was in the Living Room.\r\nEsmond came across as Happy.\r\nEsmond did not require any assistance.\r\nThis care was given at " + dateTimeOccurred.ToString("dd/MM/yyyy") + " 08:30:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall I spent 45 minutes with Esmond.\r\nWe would like to note that: In the beginning he seemed slightly depressed.")

                //Handover
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9185")]
        [Description("Validate that Care Preferences with Care Record NOT set to 'Emotional Support' are NOT loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod11()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            var dailycarerecordid = 8; //Activities
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C");

            #endregion


            #region Validate that Care Preferences with Care Record NOT set to 'Emotional Support' are NOT loaded

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9186")]
        [Description("Validate that INACTIVE Care Preferences with Care Record set to 'Emotional Support' are NOT loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod12()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];

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

            var dailycarerecordid = 10; //Emotional Support
            var inactive = true;
            var carePreferenceId = dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference A\r\nPreference B\r\nPreference C", inactive);

            #endregion


            #region Validate that INACTIVE Care Preferences with Care Record set to 'Emotional Support' are NOT loaded

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Esmond.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9187")]
        [Description("Validate shipped Types Of Emotional Support")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Emotional Support")]
        public void PersonEmotionalSupport_ACC2174_UITestMethod13()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Esmond";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Types Of Emotional Support

            var typeOfEmotionalSupportId_Reassurance = dbHelper.typeOfEmotionalSupport.GetByName("Reassurance")[0];
            var typeOfEmotionalSupportId_Companionship = dbHelper.typeOfEmotionalSupport.GetByName("Companionship")[0];
            var typeOfEmotionalSupportId_MeaningfulEngagement = dbHelper.typeOfEmotionalSupport.GetByName("Meaningful Engagement")[0];
            var typeOfEmotionalSupportId_Dolltherapy = dbHelper.typeOfEmotionalSupport.GetByName("Doll therapy")[0];
            var typeOfEmotionalSupportId_Pettherapy = dbHelper.typeOfEmotionalSupport.GetByName("Pet therapy")[0];
            var typeOfEmotionalSupportId_Namaste = dbHelper.typeOfEmotionalSupport.GetByName("Namaste")[0];
            var typeOfEmotionalSupportId_Gardenvisit = dbHelper.typeOfEmotionalSupport.GetByName("Garden visit")[0];
            var typeOfEmotionalSupportId_Spiritual = dbHelper.typeOfEmotionalSupport.GetByName("Spiritual")[0];
            var typeOfEmotionalSupportId_Music = dbHelper.typeOfEmotionalSupport.GetByName("Music")[0];
            var typeOfEmotionalSupportId_Family = dbHelper.typeOfEmotionalSupport.GetByName("Family")[0];
            var typeOfEmotionalSupportId_Other = dbHelper.typeOfEmotionalSupport.GetByName("Other")[0];


            #endregion


            #region Validate shipped Types Of Emotional Support

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
                .NavigateToEmotionalSupportPage();

            emotionalSupportPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateTimeOccurred = DateTime.Now.AddDays(-1);

            emotionalSupportRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ClickTypeEmotionalSupportRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Reassurance").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Reassurance)
                .TypeSearchQuery("Companionship").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Companionship)
                .TypeSearchQuery("Meaningful Engagement").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_MeaningfulEngagement)
                .TypeSearchQuery("Doll therapy").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Dolltherapy)
                .TypeSearchQuery("Pet therapy").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Pettherapy)
                .TypeSearchQuery("Namaste").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Namaste)
                .TypeSearchQuery("Garden visit").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Gardenvisit)
                .TypeSearchQuery("Spiritual").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Spiritual)
                .TypeSearchQuery("Music").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Music)
                .TypeSearchQuery("Family").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Family)
                .TypeSearchQuery("Other").TapSearchButton().ValidateResultElementPresent(typeOfEmotionalSupportId_Other);

            #endregion

        }

        #endregion

    }

}
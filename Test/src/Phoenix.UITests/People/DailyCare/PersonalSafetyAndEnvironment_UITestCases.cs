using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonalSafetyAndEnvironment_UITestCases : FunctionalTest
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
        private List<Guid> securityProfilesList;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PSE BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PSE T1", null, _businessUnitId, "907678", "PersonalSafetyAndEnvironmentT1@careworkstempmail.com", "Personal Safety and Environment T1", "020 123456");
                _defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

                #endregion

                #region Security Profiles

                securityProfilesList = new List<Guid>();

                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (View)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First());


                #endregion

                #region Create System User Record

                _systemUserName = "pserostereduser1";
                _systemUserFullName = "Personal Safety and Environment Rostered User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Personal Safety and Environment", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-9323

        [TestProperty("JiraIssueID", "ACC-9352")]
        [Description("Step(s) 1 to 5 and 13 from the original test - ACC-2718")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1 - 5

            //Step1
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
                .NavigateToPersonalSafetyandEnvironmentPage();

            //Step 2-3, 13
            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateResponsibleTeamLookupButtonVisibility(true)
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 9, "PSE " + _currentDateSuffix); //Personal Safety and Environment = 9

            #endregion

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now;

            //Step 4
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateResponsibleTeamLookupButtonVisibility(true)
                .ValidateResponsibleTeamLinkText("PSE T1")
                .ValidatePersonLookupButtonVisibility(true)
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("PSE " + _currentDateSuffix) //Step 13
                .ValidateDateAndTimeOccurred_DateText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(dateandTimeOccurred.ToString("HH:mm"))
                .ValidateConsentGivenSelectedText("");

            personalSafetyAndEnvironmentRecordPage
                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(true)
                .ValidateConsentGivenErrorLabelText("Please fill out this field.");

            //Step 5
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("No")
                .ValidateNonConsentDetailVisibility(true)
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateNonConsentDetailErrorLabelVisibility(true)
                .ValidateNonConsentDetailErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9353")]
        [Description("Step 6 from the original test - ACC-2718 Consent Given = No & Non-consent Detail = Absent")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 6 - Non-consent detail = Absent

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .ValidateReasonForAbsenceVisibility(false)
                .SelectNonConsentDetail("Absent")
                .ValidateReasonForAbsenceVisibility(true)
                .ValidateReasonForAbsenceMaxLength("2000")
                .ClickSaveButton()

                .WaitForPageToLoad()
                .ValidateReasonForAbsenceErrorLabelVisibility(true)
                .ValidateReasonForAbsenceErrorLabelText("Please fill out this field.")

                .InsertTextOnReasonForAbsence("Went to the hospital")
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Absent")
                .ValidateReasonForAbsenceText("Went to the hospital");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9354")]
        [Description("Step(s) 7, 8 from the original test - ACC-2718 Consent Given = No & Non-consent Detail = Declined")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 7, 8

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .ValidateReasonConsentDeclinedVisibility(false)
                .ValidateEncouragementGivenVisibility(false)
                .SelectNonConsentDetail("Declined")
                .ValidateReasonConsentDeclinedVisibility(true)
                .ValidateReasonConsentDeclinedMaxLength("2000")
                .ValidateEncouragementGivenVisibility(true)
                .ValidateEncouragementGivenMaxLength("2000")
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()

                .ValidateReasonConsentDeclinedErrorLabelVisibility(true)
                .ValidateReasonConsentDeclinedErrorLabelText("Please fill out this field.")
                .ValidateEncouragementGivenErrorLabelVisibility(true)
                .ValidateEncouragementGivenErrorLabelText("Please fill out this field.");

            personalSafetyAndEnvironmentRecordPage
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of personal safety")
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of personal safety")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9355")]
        [Description("Step(s) 10 - 12 from the original test - ACC-2718 Consent Given = No & Non-consent Detail = Deferred")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Periods

            var carePeriodId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_defaultTeam, "7 AM", new TimeSpan(7, 0, 0));

            #endregion

            #region Step 10 - 12

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);
            var deferredToDate = DateTime.Now.AddDays(2);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("No")
                .ValidateDeferredToDateVisibility(false)
                .ValidateDeferredToTimeOrShiftVisibility(false)
                .SelectNonConsentDetail("Deferred")
                .ValidateDeferredToDateVisibility(true)
                .ValidateDeferredToTimeOrShiftVisibility(true)
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabelText("Please fill out this field.")
                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(true)
                .ValidateDeferredToTimeOrShiftErrorLabelText("Please fill out this field.");

            personalSafetyAndEnvironmentRecordPage
                .InsertDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeVisibility(true)
                .ValidateDeferredToShiftLookupButtonVisibility(false)
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateDeferredToTimeErrorLabelVisibility(true)
                .ValidateDeferredToTimeErrorLabelText("Please fill out this field.")
                .InsertDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("08:45");

            personalSafetyAndEnvironmentRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ValidateDeferredToTimeVisibility(false)
                .ValidateDeferredToShiftLookupButtonVisibility(true)
                .ClickSaveButton()

                .ValidateDeferredToShiftErrorLabelVisibility(true)
                .ValidateDeferredToShiftErrorLabelText("Please fill out this field.")

                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9356")]
        [Description("Step(s) 14 - 18 from the original test - ACC-2718 Call Bell and Motion Sensor fields are not selected.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod05()
        {
            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 14 - 18

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("Yes")
                .ValidateBellSwitchedOnRadioButtonsVisibility(true)
                .ValidateBellWorkingRadioButtonsVisibility(true)
                .ValidateBellCorrectPositionRadioButtonsVisibility(true)
                .ValidateMandatoryFieldIsVisible("Is the call bell switched on?", false)
                .ValidateMandatoryFieldIsVisible("Is the call bell working?", false)
                .ValidateMandatoryFieldIsVisible("Is the call bell in the correct position?", false);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateMotionsensortypeidLookupButtonVisibility(true)
                .ValidateMotionSensorSwitchedOnRadioButtonsVisibility(true)
                .ValidateMotionSensorWorkingRadioButtonsVisibility(true)
                .ValidateMotionSensorCorrectPositionRadioButtonsVisibility(true)
                .ValidateMandatoryFieldIsVisible("Type of sensor?", false)
                .ValidateMandatoryFieldIsVisible("Is the motion sensor switched on?", false)
                .ValidateMandatoryFieldIsVisible("Is the motion sensor working?", false)
                .ValidateMandatoryFieldIsVisible("Is the motion sensor in the correct position?", false);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(livingroom_LocationId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Happy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("30")

                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes")

                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateAdditionalNotesText("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9357")]
        [Description("Step(s) 14 - 18 from the original test - ACC-2718 Call Bell and Motion Sensor fields selected.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod06()
        {
            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Camera")[0];
            var MotionSensorTypeId2 = dbHelper.motionSensorType.GetByName("Acoustic")[0];
            var MotionSensorTypeId3 = dbHelper.motionSensorType.GetByName("Laser")[0];
            var MotionSensorTypeId4 = dbHelper.motionSensorType.GetByName("Passive")[0];
            var MotionSensorTypeId5 = dbHelper.motionSensorType.GetByName("Pressure mat")[0];
            var MotionSensorTypeId6 = dbHelper.motionSensorType.GetByName("Smart")[0];

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 14 - 18

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("Yes")
                .ClickBellswitchedon_YesRadioButton()
                .ClickBellworking_YesRadioButton()
                .ClickBellcorrectposition_YesRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensortypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(MotionSensorTypeId1)
                .ValidateResultElementPresent(MotionSensorTypeId2)
                .ValidateResultElementPresent(MotionSensorTypeId3)
                .ValidateResultElementPresent(MotionSensorTypeId4)
                .ValidateResultElementPresent(MotionSensorTypeId5)
                .ValidateResultElementPresent(MotionSensorTypeId6)
                .SelectResultElement(MotionSensorTypeId1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensorswitchedon_YesRadioButton()
                .ClickMotionsensorworking_YesRadioButton()
                .ClickIsTheMotionSensorInTheCorrectPosition_YesRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(livingroom_LocationId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Happy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("30")

                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateBellworking_YesRadioButtonChecked()
                .ValidateBellworking_NoRadioButtonNotChecked()
                .ValidateBellswitchedon_YesRadioButtonChecked()
                .ValidateBellswitchedon_NoRadioButtonNotChecked()
                .ValidateBellcorrectposition_YesRadioButtonChecked()
                .ValidateBellcorrectposition_NoRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateMotionsensortypeidLinkText("Camera")
                .ValidateMotionsensorswitchedon_YesRadioButtonChecked()
                .ValidateMotionsensorswitchedon_NoRadioButtonNotChecked()
                .ValidateMotionsensorworking_YesRadioButtonChecked()
                .ValidateMotionsensorworking_NoRadioButtonNotChecked()
                .ValidateMotionsensorcorrectposition_YesRadioButtonChecked()
                .ValidateMotionsensorcorrectposition_NoRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateWellbeingLinkText("Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateAdditionalNotesText("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9358")]
        [Description("Step(s) 22 - 23 from the original test - ACC-2718 Location fields selected.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod07()
        {

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 22-23

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("Yes")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .ValidateResultElementPresent(livingroom_LocationId)
                .ValidateResultElementPresent(other_LocationId)
                .AddElementToList(livingroom_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(other_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(true)
                .ClickSaveButton()

                .ValidateLocationIfOtherErrorLabelVisibility(true)
                .ValidateLocationIfOtherErrorLabelText("Please fill out this field.")
                .InsertTextOnLocationIfOther("Other Location . . .")
                .ClickSaveButton()
                .ValidateLocationIfOtherErrorLabelVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9359")]
        [Description("Step(s) 24 - 25 from the original test - ACC-2718 Wellbeing field.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod08()
        {

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("Very Happy")[0];
            var careWellbeing3Id = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeing4Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeing5Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 24 - 25

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("Yes")
                .ClickSaveButton()
                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelText("Please fill out this field.")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(careWellbeing1Id)
                .ValidateResultElementPresent(careWellbeing2Id)
                .ValidateResultElementPresent(careWellbeing3Id)
                .ValidateResultElementPresent(careWellbeing4Id)
                .ValidateResultElementPresent(careWellbeing5Id)
                .SelectResultElement(careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateWellbeingErrorLabelVisibility(false)
                .ValidateActionTakenVisibility(false)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing2Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(false)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing3Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing4Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing5Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .ClickSaveButton()
                .ValidateWellbeingErrorLabelVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9361")]
        [Description("Step(s) 26 - 33 from the original test - ACC-2718.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod09()
        {
            #region Create System User Record

            var _systemUser2Name = "pserostereduser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Person Safety and Environment", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];
            var careAssistanceNeeded2Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeeded3Id = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 26 - 33

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("Yes")
                .ClickSaveButton()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(careAssistanceNeeded1Id)
                .ValidateResultElementPresent(careAssistanceNeeded2Id)
                .ValidateResultElementPresent(careAssistanceNeeded3Id)
                .SelectResultElement(careAssistanceNeeded2Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded3Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .ClickSaveButton()
                .ValidateAssistanceNeededErrorLabelVisibility(false)
                .ValidateAssistanceAmountErrorLabelVisibility(true)
                .ValidateAssistanceAmountErrorLabelText("Please fill out this field.")
                .SelectAssistanceAmount("Some")
                .ValidateAssistanceAmountErrorLabelVisibility(false);

            //Step 27
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false);

            personalSafetyAndEnvironmentRecordPage
                .InsertTextOnTotalTimeSpentWithPerson("aa")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(true)
                .ValidateTotalTimeSpentWithPersonErrorLabelText("Please enter a value between 1 and 1440.")
                .InsertTextOnTotalTimeSpentWithPerson("10")
                .ClickSaveButton()
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false);

            //Step 28
            personalSafetyAndEnvironmentRecordPage
                .ValidateMandatoryFieldIsVisible("Additional Notes", false)
                .InsertTextOnAdditionalNotes("Additional notes");

            //Step 29
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Staff Required", true)
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUserId, _systemUserFullName)
                .ClickStaffRequired_SelectedElementRemoveButton(_systemUserId.ToString())
                .ClickSaveButton()
                .ValidateStaffRequiredErrorLabelVisibility(true)
                .ValidateStaffRequiredErrorLabelText("Please fill out this field.")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUserName)
                .TapSearchButton()
                .AddElementToList(_systemUserId)
                .TypeSearchQuery(_systemUser2Name)
                .TapSearchButton()
                .AddElementToList(_systemUser2Id)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUserId, _systemUserFullName)
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUser2Id, "Person Safety and Environment Rostered User 2");

            //Step 30
            personalSafetyAndEnvironmentRecordPage
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(_carePlanNeedDomainId);

            //Step 31 
            //Linked Care Need field is deprecated. This step is not valid anymore.

            //Step 32
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked();

            //Step 33
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonNotChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9362")]
        [Description("Step(s) 34 - 36 from the original test - ACC-2718. Verify Handover details section.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod10()
        {
            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Camera")[0];

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Create System User Record

            var _systemUser2Name = "pserostereduser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Person Safety and Environment", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 34 - 36

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:00")
                .SelectConsentGiven("Yes")
                .ClickBellswitchedon_YesRadioButton()
                .ClickBellworking_YesRadioButton()
                .ClickBellcorrectposition_YesRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensortypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(MotionSensorTypeId1)
                .SelectResultElement(MotionSensorTypeId1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensorswitchedon_YesRadioButton()
                .ClickMotionsensorworking_YesRadioButton()
                .ClickIsTheMotionSensorInTheCorrectPosition_YesRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Other Location . . .")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser2Name)
                .TapSearchButton()
                .AddElementToList(_systemUser2Id)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(_carePlanNeedDomainId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            //Step 35 to verify the date and time occurred field
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:00")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateBellworking_YesRadioButtonChecked()
                .ValidateBellworking_NoRadioButtonNotChecked()
                .ValidateBellswitchedon_YesRadioButtonChecked()
                .ValidateBellswitchedon_NoRadioButtonNotChecked()
                .ValidateBellcorrectposition_YesRadioButtonChecked()
                .ValidateBellcorrectposition_NoRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateMotionsensortypeidLinkText("Camera")
                .ValidateMotionsensorswitchedon_YesRadioButtonChecked()
                .ValidateMotionsensorswitchedon_NoRadioButtonNotChecked()
                .ValidateMotionsensorworking_YesRadioButtonChecked()
                .ValidateMotionsensorworking_NoRadioButtonNotChecked()
                .ValidateMotionsensorcorrectposition_YesRadioButtonChecked()
                .ValidateMotionsensorcorrectposition_NoRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherText("Other Location . . .")
                .ValidateWellbeingLinkText("Very Unhappy")
                .ValidateActionTakenText("Action taken")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateStaffRequired_SelectedElementLinkText(_systemUser2Id, "Person Safety and Environment Rostered User 2")
                .ValidateAdditionalNotesText("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Arjun's call bell was checked:\r\n" +
                "The call bell was switched on\r\n" +
                "The call bell was working\r\n" +
                "The call bell was in the correct position\r\n" +
                "Arjun's motion sensor was checked:\r\n" +
                "Motion sensor type in use: Camera\r\n" +
                "The motion sensor was switched on\r\n" +
                "The motion sensor was working\r\n" +
                "The motion sensor was in the correct position\r\n" +
                "Arjun was in the Living Room and Other Location....\r\n" +
                "Arjun came across as Very Unhappy.\r\n" +
                "The action taken was: Action taken.\r\n" +
                "Arjun required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + dateOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Arjun was assisted by 2 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Arjun.\r\n" +
                "We would like to note that: Additional notes.");

            personalSafetyAndEnvironmentRecordPage
                .ValidateLinkedAdlCategories_SelectedElementLinkText(_carePlanNeedDomainId, "Acute");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonNotChecked();

            //Step 34 - Verify Handover Details/Handover Comments section
            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonNotChecked();

            handoverCommentsPage
                .WaitForHandoverCommentsToLoad("cppersonpersonalsafetyandenvironment")
                .ValidateHeaderCellText("Handover Comments")
                .ValidateHeaderCellText("Record")
                .ValidateHeaderCellText("Handover Acknowledged?")
                .ValidateHeaderCellText("Acknowledged By");

            //Step 36
            //This step is not valid.

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9363")]
        [Description("Step(s) 9 from the original test - ACC-2718. Care provided without consent = Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod11()
        {
            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Pressure mat")[0];

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Create System User Record

            var _systemUser2Name = "pserostereduser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Person Safety and Environment", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 9, "PSE " + _currentDateSuffix); //Personal Safety and Environment = 9

            #endregion

            #region Step 9

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:00")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of personal safety")
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickBellswitchedon_NoRadioButton()
                .ClickBellworking_NoRadioButton()
                .ClickBellcorrectposition_NoRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensortypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(MotionSensorTypeId1)
                .SelectResultElement(MotionSensorTypeId1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensorswitchedon_NoRadioButton()
                .ClickMotionsensorworking_NoRadioButton()
                .ClickIsTheMotionSensorInTheCorrectPosition_NoRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Other Location . . .")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser2Name)
                .TapSearchButton()
                .AddElementToList(_systemUser2Id)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(_carePlanNeedDomainId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:00")
                .ValidatePreferencesText("PSE " + _currentDateSuffix)
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of personal safety")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
                .ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateBellworking_NoRadioButtonChecked()
                .ValidateBellworking_YesRadioButtonNotChecked()
                .ValidateBellswitchedon_NoRadioButtonChecked()
                .ValidateBellswitchedon_YesRadioButtonNotChecked()
                .ValidateBellcorrectposition_NoRadioButtonChecked()
                .ValidateBellcorrectposition_YesRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateMotionsensortypeidLinkText("Pressure mat")
                .ValidateMotionsensorswitchedon_NoRadioButtonChecked()
                .ValidateMotionsensorswitchedon_YesRadioButtonNotChecked()
                .ValidateMotionsensorworking_NoRadioButtonChecked()
                .ValidateMotionsensorworking_YesRadioButtonNotChecked()
                .ValidateMotionsensorcorrectposition_NoRadioButtonChecked()
                .ValidateMotionsensorcorrectposition_YesRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherText("Other Location . . .")
                .ValidateWellbeingLinkText("Very Unhappy")
                .ValidateActionTakenText("Action taken")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateStaffRequired_SelectedElementLinkText(_systemUser2Id, "Person Safety and Environment Rostered User 2")
                .ValidateAdditionalNotesText("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Arjun's call bell was checked:\r\n" +
                "The call bell was not switched on\r\n" +
                "The call bell was not working\r\n" +
                "The call bell was not in the correct position\r\n" +
                "Arjun's motion sensor was checked:\r\n" +
                "Motion sensor type in use: Pressure mat\r\n" +
                "The motion sensor was not switched on\r\n" +
                "The motion sensor was not working\r\n" +
                "The motion sensor was not in the correct position\r\n" +
                "Arjun was in the Living Room and Other Location....\r\n" +
                "Arjun came across as Very Unhappy.\r\n" +
                "The action taken was: Action taken.\r\n" +
                "Arjun required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + dateOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Arjun was assisted by 2 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Arjun.\r\n" +
                "We would like to note that: Additional notes.");

            personalSafetyAndEnvironmentRecordPage
                .ValidateLinkedAdlCategories_SelectedElementLinkText(_carePlanNeedDomainId, "Acute");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonNotChecked();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonNotChecked();

            handoverCommentsPage
                .WaitForHandoverCommentsToLoad("cppersonpersonalsafetyandenvironment");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9391

        [TestProperty("JiraIssueID", "ACC-2741")]
        [Description("Steps from the original test - ACC-2741")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("BusinessObject", "Personal Safety and Environment")]
        [TestProperty("Screen1", "Business Objects")]
        [TestProperty("Screen2", "Reference Data")]
        public void PersonalSafetyAndEnvironment_UITestMethod12()
        {
            #region Create System User Record

            var _systemUser2Name = "pseadminuser3";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Person Safety and Environment", "Admin User 3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Reference Data

            var CPPersonPersonalSafetyandEnvironmentBoId = dbHelper.businessObject.GetBusinessObjectByName("CPPersonPersonalSafetyandEnvironment")[0];

            #endregion

            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Camera")[0];
            var MotionSensorTypeId2 = dbHelper.motionSensorType.GetByName("Acoustic")[0];
            var MotionSensorTypeId3 = dbHelper.motionSensorType.GetByName("Laser")[0];
            var MotionSensorTypeId4 = dbHelper.motionSensorType.GetByName("Passive")[0];
            var MotionSensorTypeId5 = dbHelper.motionSensorType.GetByName("Pressure mat")[0];
            var MotionSensorTypeId6 = dbHelper.motionSensorType.GetByName("Smart")[0];

            #endregion

            #region Personal Safety and Environment Option Set

            var OptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object").FirstOrDefault();
            var OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(OptionSetId, "Personal Safety and Environment").FirstOrDefault();

            #endregion

            #region Care Tasks

            var PSECareTaskId = dbHelper.careTask.GetByName("Personal Safety and Environment").First();

            #endregion

            #region Step 1a - Access BO 

            loginPage
                .GoToLoginPage()
                .Login(_systemUser2Name, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("CPPersonPersonalSafetyandEnvironment")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .ValidateRecordIsPresent(CPPersonPersonalSafetyandEnvironmentBoId.ToString(), true);

            businessObjectsPage
                .OpenRecord(CPPersonPersonalSafetyandEnvironmentBoId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateNameFieldValue("cppersonpersonalsafetyandenvironment")
                .ValidateSingularNameFieldValue("Personal Safety and Environment")
                .ValidatePluralNameFieldValue("Personal Safety and Environment")
                .ValidateTypeFieldValue("Business Data")
                .ValidateOwnershipFieldVisible("Team Based")
                .ValidateBusinessModuleFieldValue("Care Provider Care Plan")
                .ValidateDescriptionFieldValue("Care records relating to Personal Safety and Environment");

            #endregion

            #region Step 2

            //Navigate to Settings - Configuration - Customizations from Main Menu
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickOptionSetsButton();

            optionSetsPage
                .WaitForOptionSetsPageToLoad()
                .InsertQuickSearchText("Daily Care Record Business Object")
                .ClickQuickSearchButton()
                .WaitForOptionSetsPageToLoad()
                .ValidateRecordIsPresent(OptionSetId.ToString(), true)
                .OpenRecord(OptionSetId.ToString());

            optionSetsRecordPage
                .WaitForOptionSetsRecordPageToLoad()
                .NavigateToOptionSetValuesPage();

            optionsetValuesPage
                .WaitForOptionsetValuesPageToLoad()
                .InsertQuickSearchText("Personal Safety and Environment")
                .ClickQuickSearchButton()
                .WaitForOptionsetValuesPageToLoad()
                .ValidateOptionSetRecordIsAvailable(OptionSetValueId.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

            #region Step 3  - Access Reference Data using Rostered User with access to Reference Data

            loginPage
               .GoToLoginPage()
               .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, true, true)
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Type of Sensor?")
                .TapSearchButton()
                .ValidateReferenceDataMainHeaderVisibility("Care Provider Care Plan", true)
                .ClickReferenceDataMainHeader("Care Provider Care Plan")
                .ValidateReferenceDataElementVisibility("Type of Sensor?", true);

            referenceDataPage
                .ClickReferenceDataElement("Type of Sensor?");

            typeOfSensorPage
                .WaitForTypeOfSensorPageToLoad()
                .ValidateRecordPresentOrNot(MotionSensorTypeId1.ToString(), true)
                .ValidateRecordPresentOrNot(MotionSensorTypeId2.ToString(), true)
                .ValidateRecordPresentOrNot(MotionSensorTypeId3.ToString(), true)
                .ValidateRecordPresentOrNot(MotionSensorTypeId4.ToString(), true)
                .ValidateRecordPresentOrNot(MotionSensorTypeId5.ToString(), true)
                .ValidateRecordPresentOrNot(MotionSensorTypeId6.ToString(), true);

            #endregion

            #region Step 3

            /*
             * In current UI, Include for Residential Care field is now Available for Care Scheduling
             */

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, true, true)
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Care Task")
                .TapSearchButton()
                .ValidateReferenceDataMainHeaderVisibility("Care Provider Care Plan", true)
                .ClickReferenceDataMainHeader("Care Provider Care Plan")
                .ClickReferenceDataElement("Care Tasks");

            careTasksPage
                .WaitForCareTasksPageToLoad()
                .InsertSearchQuery("Personal Safety and Environment")
                .TapSearchButton()
                .WaitForCareTasksPageToLoad()
                .ValidateRecordPresentOrNot(PSECareTaskId.ToString(), true)
                .ValidateRecordCellContent(PSECareTaskId.ToString(), 3, "Yes")
                .ValidateRecordCellContent(PSECareTaskId.ToString(), 5, "Personal Safety and Environment");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2744")]
        [Description("Step(s) from the original test - ACC-2744.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod13()
        {
            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Pressure mat")[0];

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ValidateHeaderCellText(2, "Date and Time Occurred")
                .ValidateHeaderCellText(3, "Consent Given?")
                .ValidateHeaderCellText(4, "Non-consent Detail")
                .ValidateHeaderCellText(5, "Wellbeing")
                .ValidateHeaderCellText(6, "Assistance Needed?")
                .ValidateHeaderCellText(7, "Include in next Handover?")
                .ValidateHeaderCellText(8, "Flag record for handover")
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:00")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of personal safety")
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickBellswitchedon_NoRadioButton()
                .ClickBellworking_NoRadioButton()
                .ClickBellcorrectposition_NoRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensortypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(MotionSensorTypeId1)
                .SelectResultElement(MotionSensorTypeId1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensorswitchedon_NoRadioButton()
                .ClickMotionsensorworking_NoRadioButton()
                .ClickIsTheMotionSensorInTheCorrectPosition_NoRadioButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Other Location . . .")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            personalSafetyAndEnvironmentPage
                .SearchRecord(dateOccurred.ToString("dd'/'MM'/'yyyy")+" 07:00:00")        
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            personalSafetyAndEnvironmentPage
                .SearchRecord("No")
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            personalSafetyAndEnvironmentPage
                .SearchRecord("Declined")
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            personalSafetyAndEnvironmentPage
                .SearchRecord("Very Unhappy")
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            personalSafetyAndEnvironmentPage
                .SearchRecord("Asked For Help")
                .ValidateRecordPresent(cpPersonalSafetyId, true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9395

        [TestProperty("JiraIssueID", "ACC-2753")]
        [Description("Step(s) from the original test - ACC-2753.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod14()
        {
            #region Motion Sensor Type

            var MotionSensorTypeId1 = dbHelper.motionSensorType.GetByName("Laser")[0];

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("Very Happy")[0];


            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];
            var careAssistanceNeeded2Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1,2

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:00")
                .SelectConsentGiven("Yes")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickSaveAndCloseButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:00")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateWellbeingLinkText("Very Unhappy")
                .ValidateActionTakenText("Action taken")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonText("30");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAdditionalNotesText("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Arjun was in the Living Room.\r\n" +
                "Arjun came across as Very Unhappy.\r\n" +
                "The action taken was: Action taken.\r\n" +
                "Arjun required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + dateOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Arjun was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Arjun.\r\n" +
                "We would like to note that: Additional notes.");

            dateOccurred = dateOccurred.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:30")
                .ClickBellswitchedon_YesRadioButton()
                .ClickBellworking_YesRadioButton()
                .ClickBellcorrectposition_YesRadioButton()
                .ClickMotionsensortypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(MotionSensorTypeId1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickMotionsensorswitchedon_YesRadioButton()
                .ClickMotionsensorworking_YesRadioButton()
                .ClickIsTheMotionSensorInTheCorrectPosition_NoRadioButton()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(other_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Other Location . . .")
                .ClickLocation_SelectedElementRemoveButton(livingroom_LocationId.ToString())
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Happy", careWellbeing2Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", careAssistanceNeeded2Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("45")
                .InsertTextOnAdditionalNotes("Additional notes updated")
                .ClickSaveButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:30")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateWellbeingLinkText("Very Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonText("45");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAdditionalNotesText("Additional notes updated");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Arjun's call bell was checked:\r\n" +
                "The call bell was switched on\r\n" +
                "The call bell was working\r\n" +
                "The call bell was in the correct position\r\n" +
                "Arjun's motion sensor was checked:\r\n" +
                "Motion sensor type in use: Laser\r\n" +
                "The motion sensor was switched on\r\n" +
                "The motion sensor was working\r\n" +
                "The motion sensor was not in the correct position\r\n" +
                "Arjun was in the Other Location....\r\n" +
                "Arjun came across as Very Happy.\r\n" +
                "Arjun did not require any assistance.\r\n" +
                "This care was given at " + dateOccurred.ToString("dd'/'MM'/'yyyy") + " 07:30:00.\r\n" +
                "Arjun was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 45 minutes with Arjun.\r\n" +
                "We would like to note that: Additional notes updated.");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(cpPersonalSafetyId, false)
                .ValidateNoRecordMessageVisibile(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9396")]
        [Description("Step 34 from the original test - ACC-2718. " +
            "Open new record page > Fill mandatory fields > " +
            "Click 'Flag record for handover' = Yes radio button and verify auto-save record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Personal Safety and Environment")]
        public void PersonalSafetyAndEnvironment_UITestMethod15()
        {
            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Very Happy")[0];


            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id  = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Arjun";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1,2

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
                .NavigateToPersonalSafetyandEnvironmentPage();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("07:00")
                .SelectConsentGiven("Yes")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .TapOKButton();

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Happy", careWellbeing1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton()
                .WaitForPageToLoad();

            var allPersonalSafety = dbHelper.cpPersonalSafetyandEnvironment.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonalSafety.Count);
            var cpPersonalSafetyId = allPersonalSafety[0];

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ClickBackButton();

            personalSafetyAndEnvironmentPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(cpPersonalSafetyId, true)
                .OpenRecord(cpPersonalSafetyId);

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:00")
                .ValidatePreferencesText("No preferences recorded, please check with Arjun.")
                .ValidateConsentGivenSelectedText("Yes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateWellbeingLinkText("Very Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonText("30");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateAdditionalNotesText("Additional notes");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Arjun was in the Living Room.\r\n" +
                "Arjun came across as Very Happy.\r\n" +
                "Arjun did not require any assistance.\r\n" +
                "This care was given at " + dateOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Arjun was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Arjun.\r\n" +
                "We would like to note that: Additional notes.");

            personalSafetyAndEnvironmentRecordPage
                .WaitForPageToLoad()
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();

            #endregion

        }


        #endregion

    }
}
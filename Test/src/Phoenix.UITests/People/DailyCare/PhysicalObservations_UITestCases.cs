using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PhysicalObservations_UITestCases : FunctionalTest
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
        private Guid _defaultTeamId;

        private Guid Other_LocationId;
        private Guid VeryUnhappy_WellbeingId;
        private Guid Other_EquipmentId;
        private Guid careAssistanceNeededId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("WC BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("WC T1", null, _businessUnitId, "907678", "HeightAndWeight_UITestCasesT1@careworkstempmail.com", "Welfare Checks T1", "020 123456");

                #endregion

                #region Create SystemUser Record

                _systemUserName = "physicalobsuser1";
                _systemUserFullName = "PhysicalObservation User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PhysicalObservation", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #region Team membership

                _defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true)[0];
                commonMethodsDB.CreateTeamMember(_defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

                #endregion

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8632

        [TestProperty("JiraIssueID", "ACC-8736")]
        [Description("Step(s) 1 to 7 from the original test - ACC-3932")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC3932_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Andre";
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
                .WaitForMainMenuToLoad();

            #endregion

            #region Step 2

            mainMenu
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad();

            #endregion

            #region Step 3

            personPhysicalObservationsPage
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad();

            #endregion

            #region Step 4

            selectPersonPhysicalObservationTypePopup
                .VerifyPersonPhysicalObservationTypeOptions("Physical Observation - NEWS");

            #endregion

            #region Step 5

            selectPersonPhysicalObservationTypePopup
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            #endregion

            #region Step 6

            personPhysicalObservationsRecordPage
                .ValidatePreferencesText("No preferences recorded, please check with Andre.")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 13, "Physical Observation " + _currentDateSuffix); //13

            #endregion

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidatePreferencesText("Physical Observation " + _currentDateSuffix);

            #endregion

            #region Step 7

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateFieldIsVisible("Non-consent Detail", false)
                .SelectConsentGiven("No")
                .ValidateFieldIsVisible("Non-consent Detail", true)
                .ValidateNonConsentDetailOptions("Absent", true)
                .ValidateNonConsentDetailOptions("Declined", true)
                .ValidateNonConsentDetailOptions("Deferred", true)
                .SelectNonConsentDetail("Absent")
                .SelectNonConsentDetail("Declined")
                .SelectNonConsentDetail("Deferred");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8633

        [TestProperty("JiraIssueID", "ACC-8737")]
        [Description("Step(s) 8 to 14 from the original test - ACC-3932")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC3932_UITestMethod02()
        {
            #region Care Physical Locations 

            Other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Well Being

            VeryUnhappy_WellbeingId = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Equipment

            Other_EquipmentId = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion

            #region Care Assistances Needed

            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Andre";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 8

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .ValidateReasonForAbsenceFieldVisible(true)
                .InsertTextInReasonForAbsence("Not available");

            #endregion

            #region Step 9

            personPhysicalObservationsRecordPage
                .SelectNonConsentDetail("Declined")
                .ValidateReasonForAbsenceFieldVisible(false)
                .ValidateReasonConsentDeclinedFieldVisible(true)
                .InsertTextInReasonConsentDeclined("Not interested");

            #endregion

            #region Step 10

            personPhysicalObservationsRecordPage
                .ValidateEncouragementGivenFieldVisible(true)
                .InsertTextInEncouragementGiven("Encouragement");

            #endregion

            #region Step 11

            personPhysicalObservationsRecordPage
                .ValidateEncouragementGivenFieldTooltip("What strategies were used to encourage the resident?");

            #endregion

            #region Step 12

            personPhysicalObservationsRecordPage
                .ValidateCareProvidedWithoutConsentOptionsVisible(true)
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
                .ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickCareProvidedWithoutConsent_NoRadioButton();

            #endregion

            #region Step 13

            personPhysicalObservationsRecordPage
                .SelectNonConsentDetail("Deferred")
                .ValidateReasonForAbsenceFieldVisible(false)
                .ValidateReasonConsentDeclinedFieldVisible(false)
                .ValidateEncouragementGivenFieldVisible(false)
                .ValidateCareProvidedWithoutConsentOptionsVisible(false)
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true)
                .ValidateMandatoryFieldIsVisible("Deferred To Date?");

            personPhysicalObservationsRecordPage
                .InsertTextInDeferredToDate(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftFieldVisible(true)
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeFieldVisible(true)
                .SetDeferredToTime("10:00 AM");

            #endregion

            #region Step 14

            personPhysicalObservationsRecordPage
                .SelectConsentGiven("Yes");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateMandatoryFieldIsVisible("Location")
                .ValidateFieldIsVisible("Location If Other?", false)
                .ValidateLocationIfOtherTextareaFieldVisible(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_LocationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateLocationIfOtherTextareaFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Location If Other?", true);

            personPhysicalObservationsRecordPage
                .ValidateMandatoryFieldIsVisible("Equipment")
                .ValidateFieldIsVisible("Equipment If Other", false)
                .ValidateEquipmentIfOtherTextareaFieldVisible(false)
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_EquipmentId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateEquipmentIfOtherTextareaFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Equipment If Other", true);

            personPhysicalObservationsRecordPage
                .ValidateMandatoryFieldIsVisible("Wellbeing")
                .ValidateFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", false)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", VeryUnhappy_WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateMandatoryFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", true);

            personPhysicalObservationsRecordPage
                .ValidateMandatoryFieldIsVisible("Assistance Needed?")
                .ValidateFieldIsVisible("Assistance Amount?", false)
                .ValidateAssistanceAmountPicklistVisible(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .ValidateMandatoryFieldIsVisible("Assistance Amount?", true);

            personPhysicalObservationsRecordPage
                .ValidateTotalTimeSpentWithPersonMinutesFieldVisible(true)
                .ValidateStaffRequiredLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Staff Required", true)
                .ValidateAdditionalNotesFieldVisible(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8635

        [TestProperty("JiraIssueID", "ACC-8738")]
        [Description("Step(s) 15 to 18 from the original test - ACC-3932")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC3932_UITestMethod03()
        {
            #region Care Physical Locations 

            Other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Well Being

            VeryUnhappy_WellbeingId = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Equipment

            Other_EquipmentId = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion

            #region Care Assistances Needed

            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Care plan need domain

            var CarePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Administration of Medicine").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Andre";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 15

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("Yes");

            personPhysicalObservationsRecordPage
                .ValidateFieldIsVisible("Linked Activities Of Daily Living (ADL) / Domain of Need")
                .ValidateLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButtonVisible(true);

            #endregion

            #region Step 16

            personPhysicalObservationsRecordPage
                .ValidateIncludeInNextHandoverOptionsVisible(true)
                .ValidateFlagRecordForHandoverOptionsVisible(true);

            #endregion

            #region Step 17

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_LocationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInLocationIfOtherTextareaField("Other Location " + _currentDateSuffix)
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_EquipmentId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInEquipmentIfOtherTextareaField("Other Equipment " + _currentDateSuffix)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", VeryUnhappy_WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("Action Taken " + _currentDateSuffix)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton();

            personPhysicalObservationsRecordPage
                .SelectTalkingInSentencesValue("Yes")
                .SelectBreathingBetween11to25Value("Yes")
                .SelectAbleToStandUnaidedValue("Yes")
                .SelectAlertAndResponsiveValue("Yes")
                .SelectDoesNotHavePhysicalHealthProblemValue("Yes")
                .ClickSaveButton()
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            personPhysicalObservationsRecordPage
                .ValidatePreferencesText("No preferences recorded, please check with Andre.")
                .ValidateDateAndTimeOccurred_DateText(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("10:00")
                .ValidateConsentGivenSelectedText("Yes")
                .ValidateCarenoteText("Andre was visually assessed as follows:\r\n" +
                "A: Talking in sentences, not just moans and groans. Breathing is quiet and regular.\r\n" +
                "B: Breathing is between 11-25 per minute. It requires no extra effort and is not difficult or laboured.\r\n" +
                "C: Able to stand unaided. Usual Level of mobility. Coherent even if pre-existing confusion or disorientation. Usual skin tone.\r\n" +
                "D: Alert, responsive and active. Spontaneous speech.\r\n" +
                "E: Everything seems stable. Neither staff nor patient are concerned about physical health. Is not known or suspected to have physical health problems such as rapid tranquillisation, asthma, diabetes or alcohol/substance use.\r\n" +
                "Andre was in the Other Location " + _currentDateSuffix + ".\r\n" +
                "Andre used the following equipment: Other Equipment " + _currentDateSuffix + ".\r\n" +
                "Andre came across as Very Unhappy.\r\n" +
                "The action taken was: Action Taken " + _currentDateSuffix + ".\r\n" +
                "Andre required assistance: Physical Assistance. Amount given: Fair.\r\n" +
                "This care was given at " + DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy") + " 10:00:00.\r\n" +
                "Andre was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Andre.\r\n" +
                "We would like to note that: Additional notes " + _currentDateSuffix + ".");

            #endregion

            #region Step 18

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickBackButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ValidateHeaderCellIsDisplayed("Respiratory Distress", false)
                .ValidateHeaderCellIsDisplayed("Score Pain", false)
                .SelectView("Visual Assessments in the Last 2 Years")
                .ValidateHeaderCellIsDisplayed("Respiratory Distress", false)
                .ValidateHeaderCellIsDisplayed("Score Pain", false)
                .SelectView("Related (for Bed Management)")
                .ValidateHeaderCellIsDisplayed("Respiratory Distress", false)
                .ValidateHeaderCellIsDisplayed("Score Pain", false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8802

        [TestProperty("JiraIssueID", "ACC-4884")]
        [Description("Step(s) 1 to 5 from the original test - ACC-4884")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC4844_UITestMethod01()
        {
            #region Care Physical Locations 

            var LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Well Being

            var WellbeingId = dbHelper.careWellbeing.GetByName("Unhappy")[0];

            #endregion

            #region Equipment

            var EquipmentId = dbHelper.careEquipment.GetByName("Pulse Oximeter")[0];

            #endregion

            #region Care Assistances Needed

            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Care plan need domain

            var CarePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Administration of Medicine").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jack";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            #endregion

            #region Step 3

            personPhysicalObservationsRecordPage
                .InsertTextOnDateAndTimeOccurred_DateField("01/06/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("Yes")
                .ValidateTotalTimeSpentWithPersonMinutesFieldRange("1,1440");

            #endregion

            #region Step 4

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .SelectResultElement(LocationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Pulse Oximeter")
                .TapSearchButton()
                .SelectResultElement(EquipmentId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Unhappy", WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("Action Taken " + _currentDateSuffix)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveButton()
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickBackButton();

            var physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(1, physicalObservationRecords.Count);
            var physicalObservationId1 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .SelectView("Related (for Bed Management)")
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ValidateRecordPresent(physicalObservationId1, true);

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/06/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("Yes")
                .ValidateTotalTimeSpentWithPersonMinutesFieldRange("1,1440")
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .SelectResultElement(LocationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Pulse Oximeter")
                .TapSearchButton()
                .SelectResultElement(EquipmentId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Unhappy", WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("Action Taken " + _currentDateSuffix)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveButton()
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickBackButton();

            physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(2, physicalObservationRecords.Count);
            var physicalObservationId2 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .SelectView("Related (for Bed Management)")
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ValidateRecordPresent(physicalObservationId1, true)
                .ValidateRecordPresent(physicalObservationId2, true);

            #endregion

            #region Step 5

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes")
                .ValidateMandatoryFieldIsVisible("Location")
                .ValidateLocationLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Equipment")
                .ValidateEquipmentLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Wellbeing")
                .ValidateWellbeingLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Assistance Needed?")
                .ValidateAssistanceNeededLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Staff Required")
                .ValidateStaffRequiredLookupButtonVisible(true)
                .ValidateMandatoryFieldIsVisible("Total Time Spent With Person (Minutes)")
                .ValidateTotalTimeSpentWithPersonMinutesFieldVisible(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8828

        [TestProperty("JiraIssueID", "ACC-5132")]
        [Description("Step(s) 1 to 5 from the original test - ACC-5132")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC5132_UITestMethod01()
        {
            #region Care Physical Locations 

            var LocationId1 = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var LocationId2 = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            Other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Well Being

            VeryUnhappy_WellbeingId = dbHelper.careWellbeing.GetByName("Unhappy")[0];

            #endregion

            #region Equipment

            var EquipmentId1 = dbHelper.careEquipment.GetByName("Pulse Oximeter")[0];
            var EquipmentId2 = dbHelper.careEquipment.GetByName("Temporal Thermometer")[0];
            Other_EquipmentId = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion

            #region Care Assistances Needed

            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Care plan need domain

            var CarePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Administration of Medicine").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jack";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad();

            #endregion

            #region Step 2

            mainMenu
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad();

            #endregion

            #region Step 3

            personPhysicalObservationsPage
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - NEWS")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            #endregion

            #region Step 4

            personPhysicalObservationsRecordPage
                .InsertTextOnDateAndTimeOccurred_DateField("01/06/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("Yes")
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateCarenoteText("This care was given at 01/06/2024 10:00:00.\r\n" +
                "Jack was assisted by 1 colleague(s).");

            #endregion

            #region Step 5

            personPhysicalObservationsRecordPage
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnTemperature("60")
                .SelectAreaTemperatureTaken("Temporal (Temple)")
                .InsertTextOnTemperatureActionsTakenRequired("Temperature action taken " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnBpsystolic("120")
                .InsertTextOnBpdiastolic("80")
                .SelectPositionWhenReadingTaken("Sitting")
                .ClickRequireSecondaryBpReading_YesOption()
                .InsertTextOnSecondaryBpSystolic("130")
                .InsertTextOnSecondaryBpDiastolic("80")
                .SelectSecondaryBpPositionWhenReadingTaken("Lying")
                .InsertTextOnBloodPressureReadingActionsTakenRequired("BP Secondary Reading action taken " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnPulse("95")
                .SelectIsPulseRegularOrIrregular("Regular")
                .SelectPersonPhysicalObservationWhenPulseTaken("Whilst resting")
                .InsertTextOnPulseActionsTakenRequired("Pulse action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnRespiration("50")
                .InsertTextOnRespirationActionsTakenRequired("Respiration action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnOxygenSaturation("90")
                .InsertTextOnPeakFlow("100")
                .InsertTextOnOxygenSaturationActionsTakenRequired("Oxygen Saturation action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnBloodSugarLevel("1")
                .SelectBloodSugarWhenWasReadingTaken("After meal - within 2 hours")
                .InsertTextOnBloodGlucoseActionsTakenRequired("Blood Sugar action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .SelectRightPupilSize("6 mm")
                .SelectLeftPupilSize("6 mm")
                .SelectRightPupilReactionType("Reaction")
                .SelectLeftPupilReactionType("Reaction")
                .InsertTextOnNeurologicalObservationsActionsTakenRequired("Neurological Observations action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .InsertTextOnTotalScoresActionsTakenRequired("Total Scores action " + _currentDateSuffix)
                .ClickReviewRequiredBySeniorColleague_YesOption()
                .InsertTextOnReviewDetails("Review details information");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(LocationId1)
                .AddElementToList(LocationId2)
                .TapOKButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Pulse Oximeter")
                .TapSearchButton()
                .AddElementToList(EquipmentId1)
                .TypeSearchQuery("Temporal Thermometer")
                .TapSearchButton()
                .AddElementToList(EquipmentId2)
                .TapOKButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Unhappy", VeryUnhappy_WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("Action Taken " + _currentDateSuffix)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveButton()
                .WaitForPersonPhysicalObservationsRecordPageToLoad();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateTemperatureText("60.0")
                .ValidateAreaTemperatureTakenSelectedText("Temporal (Temple)")
                .ValidateTemperatureActionsTakenRequiredText("Temperature action taken " + _currentDateSuffix)
                .ValidateTemperatureEarlyWarningScoreText("2");

            personPhysicalObservationsRecordPage
                .ValidateBpSystolicText("120")
                .ValidateBpDiastolicText("80")
                .ValidatePositionWhenReadingTakenSelectedText("Sitting")
                .ValidateBpEarlyWarningScoreText("0")
                .ValidateRequireSecondaryBpReading_YesOptionChecked()
                .ValidateSecondaryBpSystolicText("130")
                .ValidateSecondaryBpDiastolicText("80")
                .ValidateSecondaryBpPositionWhenReadingTakenSelectedText("Lying")
                .ValidateBloodPressureReadingActionsTakenRequiredText("BP Secondary Reading action taken " + _currentDateSuffix)
                .ValidateSecondaryBpEarlyWarningScoreText("0");

            personPhysicalObservationsRecordPage
                .ValidateRespirationText("50")
                .ValidateRespiratoryDistressEarlyWarningScoreText("3")
                .ValidateRespirationActionsTakenRequiredText("Respiration action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .ValidateOxygenSaturationText("90.00")
                .ValidatePeakFlowText("100")
                .ValidateOxygenSaturationEarlyWarningScoreText("3")
                .ValidateOxygenSaturationActionsTakenRequiredText("Oxygen Saturation action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .ValidateBloodSugarlevelText("1.00")
                .ValidateBloodSugarWhenWasReadingTakenSelectedText("After meal - within 2 hours")
                .ValidateBloodGlucoseActionsTakenRequiredText("Blood Sugar action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .ValidateRightPupilSizeSelectedText("6 mm")
                .ValidateLeftPupilSizeSelectedText("6 mm")
                .ValidateRightPupilReactionTypeSelectedText("Reaction")
                .ValidateLeftPupilReactionTypeSelectedText("Reaction")
                .ValidateNeurologicalObservationsActionsTakenRequiredText("Neurological Observations action " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .ValidateTotalscoreText("9")
                .ValidateTotalScoresActionsTakenRequiredText("Total Scores action " + _currentDateSuffix)
                .ValidateReviewRequiredBySeniorColleague_YesOptionChecked()
                .ValidateReviewRequiredBySeniorColleague_NoOptionNotChecked()
                .ValidateReviewDetailsText("Review details information");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateCarenoteText("Jack's respiration rate was 50 breaths per minute.\r\n" +
                "The following actions were noted regarding Jack's respiration: Respiration action " + _currentDateSuffix + ".\r\n" +
                "Jack's oxygen saturation was 90%.\r\n" +
                "The following actions were noted regarding Jack's O2 Saturation: Oxygen Saturation action " + _currentDateSuffix + ".\r\n" +
                "A BP reading of 120/80mmHg was taken while Jack was Sitting.\r\n" +
                "A secondary BP reading of 130/80 was taken while Jack was Lying.\r\n" +
                "The following actions were noted regarding Jack's BP: BP Secondary Reading action taken " + _currentDateSuffix + ".\r\n" +
                "Jack's pulse was 95 beats per minute and was Regular. This reading was taken Whilst resting.\r\n" +
                "The following actions were noted regarding Jack's pulse: Pulse action " + _currentDateSuffix + ".\r\n" +
                "Jack's temperature was 60 degrees celsius.\r\n" +
                "Their temperature was taken in the following area: Temporal (Temple).\r\n" +
                "The following actions were noted regarding Jack's temperature: Temperature action taken " + _currentDateSuffix + ".\r\n" +
                "Jack's blood glucose level was taken After meal - within 2 hours and was 1 mmols/Litre.\r\n" +
                "Jack's left pupil reaction was and the size was measured as 6 mm.\r\n" +
                "The following actions were noted regarding Jack's blood glucose level: Blood Sugar action " + _currentDateSuffix + ".\r\n" +
                "Jack's right pupil reaction was and the size was measured as 6 mm.\r\n" +
                "The total early warning score for this physical observation is: 9.\r\n" +
                "The following actions were noted regarding Jack's neurological observations: Neurological Observations action " + _currentDateSuffix + ".\r\n" +
                "The following overall actions were noted for this physical observation: Total Scores action " + _currentDateSuffix + ".\r\n" +
                "Jack was in the Living Room and Bedroom.\r\n" +
                "Jack used the following equipment: Pulse Oximeter and Temporal Thermometer.\r\n" +
                "Jack came across as Unhappy.\r\nThe action taken was: Action Taken " + _currentDateSuffix + ".\r\n" +
                "Jack required assistance: Physical Assistance. Amount given: Fair.\r\nThis care was given at 01/06/2024 10:00:00.\r\n" +
                "Jack was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Jack.\r\n" +
                "We would like to note that: Additional notes " + _currentDateSuffix + ".")
                .ClickBackButton();

            var physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(1, physicalObservationRecords.Count);
            var physicalObservationId1 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .SelectView("Related (for Bed Management)")
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ValidateRecordPresent(physicalObservationId1, true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8850

        [TestProperty("JiraIssueID", "ACC-5165")]
        [Description("Step(s) 1 to 15 from the original test - ACC-5165")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC5165_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jack";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1 to 5

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .ValidateSelectedNonConsentDetail("Absent")
                .SelectNonConsentDetail("Declined")
                .ValidateSelectedNonConsentDetail("Declined")
                .SelectNonConsentDetail("Deferred")
                .ValidateSelectedNonConsentDetail("Deferred");

            #endregion

            #region Step 6, 14

            personPhysicalObservationsRecordPage
                .InsertTextOnDateAndTimeOccurred_DateField("01/04/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("09:00");

            personPhysicalObservationsRecordPage
                .ValidateReasonForAbsenceFieldVisible(false)
                .ValidateReasonConsentDeclinedFieldVisible(false)
                .ValidateEncouragementGivenFieldVisible(false)
                .ValidateCareProvidedWithoutConsentOptionsVisible(false)
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true)
                .ValidateDeferredToTimeOrShiftFieldVisible(true)
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeFieldVisible(true)
                .ValidateDeferredToTime_TimePickerVisible(true)
                .ValidateDeferredToShiftLookupButtonVisible(false)
                .SelectDeferredToTimeOrShift("Shift")
                .ValidateDeferredToTimeFieldVisible(false)
                .ValidateDeferredToTime_TimePickerVisible(false)
                .ValidateDeferredToShiftLookupButtonVisible(true);

            personPhysicalObservationsRecordPage
                .ClickDeferredToDate_DatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-1));

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectDeferredToTimeOrShift("Time")
                .ClickDeferredToTime_TimePicker();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour("15")
                .SelectMinute("00");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("\"Deferred To?\" cannot be before today.")
                .TapCloseButton();

            personPhysicalObservationsRecordPage
                .SetDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonPhysicalObservationsPageToLoad();

            var physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(1, physicalObservationRecords.Count);
            var physicalObservationId1 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .SelectView("Related (for Bed Management)")
                .WaitForPersonPhysicalObservationsPageToLoad()
                .OpenRecord(physicalObservationId1);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateConsentGivenSelectedText("No")
                .ValidateDateAndTimeOccurred_DateText("01/04/2024")
                .ValidateDateAndTimeOccurred_TimeText("09:00")
                .ValidatePreferencesText("No preferences recorded, please check with Jack.")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Time")
                .ValidateDeferredToTime("15:00");

            var CareProviderCarePeriodSetupId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_teamId, "Morning", new TimeSpan(6, 0, 0));

            #region Deferred to Shift

            personPhysicalObservationsRecordPage
                .ClickBackButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .InsertTextOnDateAndTimeOccurred_DateField("01/05/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .SetDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Morning", CareProviderCarePeriodSetupId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonPhysicalObservationsPageToLoad();

            physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(2, physicalObservationRecords.Count);
            var physicalObservationId2 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .SelectView("Related (for Bed Management)")
                .WaitForPersonPhysicalObservationsPageToLoad()
                .OpenRecord(physicalObservationId2);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateConsentGivenSelectedText("No")
                .ValidateDateAndTimeOccurred_DateText("01/05/2024")
                .ValidateDateAndTimeOccurred_TimeText("10:00")
                .ValidatePreferencesText("No preferences recorded, please check with Jack.")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("Morning");

            #endregion

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8857

        [TestProperty("JiraIssueID", "ACC-5166")]
        [Description("Step(s) 1 to 12 from the original test - ACC-5166")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen", "Person Physical Observation")]
        public void PhysicalObservations_ACC5166_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Jack";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Skin Condition

            var skinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Dry Skin")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var livingRoom_LocationId1 = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            Other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];


            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];
            VeryUnhappy_WellbeingId = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Equipment 

            var equipmentId1 = dbHelper.careEquipment.GetByName("BP Machine")[0];
            var equipmentId2 = dbHelper.careEquipment.GetByName("Oral Thermometer")[0];
            Other_EquipmentId = dbHelper.careEquipment.GetByName("Other")[0];


            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 13, "Physical Observation " + _currentDateSuffix); //13

            #endregion

            #region Care plan need domain

            var CarePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Administration of Medicine").FirstOrDefault();

            #endregion

            #region Step 1 to 4

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - NEWS")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/05/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("10:00")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextInReasonConsentDeclined("Reason for Declined ...")
                .InsertTextInEncouragementGiven("Encouragement given ...");

            #endregion

            #region Step 5

            personPhysicalObservationsRecordPage
                .ValidatePreferencesText("Physical Observation " + _currentDateSuffix);

            #endregion

            #region Step 6

            personPhysicalObservationsRecordPage
                .ClickCareProvidedWithoutConsent_YesRadioButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnTemperature("60")
                .SelectAreaTemperatureTaken("Oral (Mouth)")
                .InsertTextOnTemperatureActionsTakenRequired("Temperature action taken")

                .InsertTextOnBpsystolic("120")
                .InsertTextOnBpdiastolic("80")
                .SelectPositionWhenReadingTaken("Lying")
                .ClickRequireSecondaryBpReading_NoOption()
                .InsertTextOnBloodPressureReadingActionsTakenRequired("BP action taken")

                .InsertTextOnPulse("95")
                .SelectIsPulseRegularOrIrregular("Regular")
                .SelectPersonPhysicalObservationWhenPulseTaken("After intense exercise")
                .InsertTextOnPulseActionsTakenRequired("Pulse action")

                .InsertTextOnRespiration("25")
                .InsertTextOnRespirationActionsTakenRequired("Respiration action")

                .InsertTextOnOxygenSaturation("90")
                .InsertTextOnPeakFlow("100")
                .InsertTextOnOxygenSaturationActionsTakenRequired("Oxygen Saturation action")

                .InsertTextOnBloodSugarLevel("49")
                .SelectBloodSugarWhenWasReadingTaken("More than 2 hours after meal")
                .InsertTextOnBloodGlucoseActionsTakenRequired("Blood Sugar action")

                .SelectRightPupilSize("5 mm")
                .SelectLeftPupilSize("5 mm")
                .SelectRightPupilReactionType("No Reaction")
                .SelectLeftPupilReactionType("No Reaction")
                .InsertTextOnNeurologicalObservationsActionsTakenRequired("Neurological Observations action")

                .InsertTextOnTotalScoresActionsTakenRequired("Total Scores action")
                .ClickReviewRequiredBySeniorColleague_YesOption()
                .InsertTextOnReviewDetails("Review details information");

            personPhysicalObservationsRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
            .WaitForLookupMultiSelectPopupToLoad()
            .TypeSearchQuery("Bedroom")
            .TapSearchButton()
            .AddElementToList(bedroom_CarePhysicalLocationId)
            .TypeSearchQuery("Living room")
            .TapSearchButton()
            .AddElementToList(livingRoom_LocationId1)
            .TypeSearchQuery("Other")
            .TapSearchButton()
            .AddElementToList(Other_LocationId)
            .TapOKButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInLocationIfOtherTextareaField("Other location ...")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("BP Machine")
                .TapSearchButton()
                .AddElementToList(equipmentId1)
                .TypeSearchQuery("Oral Thermometer")
                .TapSearchButton()
                .AddElementToList(equipmentId2)
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .AddElementToList(Other_EquipmentId)
                .TapOKButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInEquipmentIfOtherTextareaField("Other equipment ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("action take info ...")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("extra notes ...");

            personPhysicalObservationsRecordPage
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndCloseButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton();

            var physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(1, physicalObservationRecords.Count);
            var physicalObservationId = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .SelectView("Related (for Bed Management)")
                .OpenRecord(physicalObservationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidatePreferencesText("Physical Observation " + _currentDateSuffix)
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclined("Reason for Declined ...")
                .ValidateEncouragementGiven("Encouragement given ...")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
                .ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
                .ValidateDateAndTimeOccurred_DateText("01/05/2024")
                .ValidateDateAndTimeOccurred_TimeText("10:00");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateTemperatureText("60.0")
                .ValidateAreaTemperatureTakenSelectedText("Oral (Mouth)")
                .ValidateTemperatureActionsTakenRequiredText("Temperature action taken")
                .ValidateTemperatureEarlyWarningScoreText("2");

            personPhysicalObservationsRecordPage
                .ValidateBpSystolicText("120")
                .ValidateBpDiastolicText("80")
                .ValidatePositionWhenReadingTakenSelectedText("Lying")
                .ValidateBpEarlyWarningScoreText("0")
                .ValidateRequireSecondaryBpReading_NoOptionChecked()
                .ValidateBloodPressureReadingActionsTakenRequiredText("BP action taken");

            personPhysicalObservationsRecordPage
                .ValidatePulseText("95")
                .ValidateIsPulseRegularOrIrregularSelectedText("Regular")
                .ValidateWhenPulseTakenSelectedText("After intense exercise")
                .ValidatePulseEarlyWarningScoreText("1")
                .ValidatePulseActionsTakenRequiredText("Pulse action");

            personPhysicalObservationsRecordPage
                .ValidateRespirationText("25")
                .ValidateRespiratoryDistressEarlyWarningScoreText("3")
                .ValidateRespirationActionsTakenRequiredText("Respiration action");

            personPhysicalObservationsRecordPage
                .ValidateOxygenSaturationText("90.00")
                .ValidatePeakFlowText("100")
                .ValidateOxygenSaturationEarlyWarningScoreText("3")
                .ValidateOxygenSaturationActionsTakenRequiredText("Oxygen Saturation action");

            personPhysicalObservationsRecordPage
                .ValidateBloodSugarlevelText("49.00")
                .ValidateBloodSugarWhenWasReadingTakenSelectedText("More than 2 hours after meal")
                .ValidateBloodGlucoseActionsTakenRequiredText("Blood Sugar action");

            personPhysicalObservationsRecordPage
                .ValidateRightPupilSizeSelectedText("5 mm")
                .ValidateLeftPupilSizeSelectedText("5 mm")
                .ValidateRightPupilReactionTypeSelectedText("No Reaction")
                .ValidateLeftPupilReactionTypeSelectedText("No Reaction")
                .ValidateNeurologicalObservationsActionsTakenRequiredText("Neurological Observations action");

            personPhysicalObservationsRecordPage
                .ValidateTotalscoreText("9")
                .ValidateTotalScoresActionsTakenRequiredText("Total Scores action")
                .ValidateReviewRequiredBySeniorColleague_YesOptionChecked()
                .ValidateReviewRequiredBySeniorColleague_NoOptionNotChecked()
                .ValidateReviewDetailsText("Review details information");

            personPhysicalObservationsRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_CarePhysicalLocationId, "Bedroom")
                .ValidateLocation_SelectedElementLinkText(livingRoom_LocationId1, "Living Room")
                .ValidateLocation_SelectedElementLinkText(Other_LocationId, "Other")
                .ValidateLocationIfOtherTextareaFieldText("Other location ...");

            personPhysicalObservationsRecordPage
                .ValidateEquipment_SelectedElementLinkText(equipmentId1, "BP Machine")
                .ValidateEquipment_SelectedElementLinkText(equipmentId2, "Oral Thermometer")
                .ValidateEquipment_SelectedElementLinkText(Other_EquipmentId, "Other")
                .ValidateEquipmentIfOtherTextareaFieldText("Other equipment ...");

            personPhysicalObservationsRecordPage
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenHasPainReliefBeenOfferedTextareaFieldText("action take info ...");

            personPhysicalObservationsRecordPage
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateStaffRequiredSelectedOptionText(_systemUserId.ToString(), "PhysicalObservation User 1")
                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateAdditionalNotesText("extra notes ...");

            personPhysicalObservationsRecordPage
                .ValidateCarenoteText("Jack's respiration rate was 25 breaths per minute.\r\n" +
                "The following actions were noted regarding Jack's respiration: Respiration action.\r\n" +
                "Jack's oxygen saturation was 90%.\r\n" +
                "The following actions were noted regarding Jack's O2 Saturation: Oxygen Saturation action.\r\n" +
                "A BP reading of 120/80mmHg was taken while Jack was Lying.\r\n" +
                "The following actions were noted regarding Jack's BP: BP action taken.\r\n" +
                "Jack's pulse was 95 beats per minute and was Regular. This reading was taken After intense exercise.\r\n" +
                "The following actions were noted regarding Jack's pulse: Pulse action.\r\n" +
                "Jack's temperature was 60 degrees celsius.\r\n" +
                "Their temperature was taken in the following area: Oral (Mouth).\r\n" +
                "The following actions were noted regarding Jack's temperature: Temperature action taken.\r\n" +
                "Jack's blood glucose level was taken More than 2 hours after meal and was 49 mmols/Litre.\r\n" +
                "Jack's left pupil reaction was and the size was measured as 5 mm.\r\n" +
                "The following actions were noted regarding Jack's blood glucose level: Blood Sugar action.\r\n" +
                "Jack's right pupil reaction was and the size was measured as 5 mm.\r\n" +
                "The total early warning score for this physical observation is: 9.\r\n" +
                "The following actions were noted regarding Jack's neurological observations: Neurological Observations action.\r\n" +
                "The following overall actions were noted for this physical observation: Total Scores action.\r\n" +
                "Jack was in the Bedroom, Living Room and Other location....\r\n" +
                "Jack used the following equipment: BP Machine, Oral Thermometer and Other equipment....\r\n" +
                "Jack came across as OK.\r\n" +
                "The action taken was: action take info....\r\n" +
                "Jack did not require any assistance.\r\n" +
                "This care was given at 01/05/2024 10:00:00.\r\n" +
                "Jack was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Jack.\r\n" +
                "We would like to note that: extra notes....");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_NoRadioButtonChecked();

            personPhysicalObservationsRecordPage
                .ValidateLinkedADL_DomainOfNeed_SelectedOption(CarePlanNeedDomainId.ToString(), "Administration of Medicine")
                .ClickBackButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickNewRecordButton();

            selectPersonPhysicalObservationTypePopup
                .WaitForPopupToLoad()
                .SelectPersonPhysicalObservationType("Physical Observation - Visual Assessment")
                .ClickNextButton();

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/06/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("11:00")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextInReasonConsentDeclined("Reason for Declined 2...")
                .InsertTextInEncouragementGiven("Encouragement given 2...");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidatePreferencesText("Physical Observation " + _currentDateSuffix);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickLinkedActivitiesOfDailyLiving_DomainOfNeedLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Administration of Medicine")
                .TapSearchButton()
                .SelectResultElement(CarePlanNeedDomainId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectTalkingInSentencesValue("No")
                .SelectBreathingBetween11to25Value("No")
                .SelectAbleToStandUnaidedValue("No")
                .SelectAlertAndResponsiveValue("No")
                .SelectDoesNotHavePhysicalHealthProblemValue("No");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .AddElementToList(Other_LocationId)
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .AddElementToList(livingRoom_LocationId1)
                .TypeSearchQuery("Bedroom")
                .TapSearchButton()
                .SelectResultElement(bedroom_CarePhysicalLocationId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInLocationIfOtherTextareaField("Other Location")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .AddElementToList(Other_EquipmentId)
                .TypeSearchQuery("BP Machine")
                .TapSearchButton()
                .AddElementToList(equipmentId1)
                .TypeSearchQuery("Oral Thermometer")
                .TapSearchButton()
                .SelectResultElement(equipmentId2);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInEquipmentIfOtherTextareaField("Other Equipment")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", VeryUnhappy_WellbeingId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .InsertTextInActionTakenHasPainReliefBeenOfferedTextareaField("Action Taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", careAssistanceNeededId);

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes");

            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton();

            personPhysicalObservationsRecordPage
                .ClickSaveAndCloseButton();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .ClickRefreshButton();

            physicalObservationRecords = dbHelper.personPhysicalObservation.GetByPersonId(personId);
            Assert.AreEqual(2, physicalObservationRecords.Count);
            var physicalObservationId2 = physicalObservationRecords[0];

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .SelectView("Related (for Bed Management)")
                .OpenRecord(physicalObservationId2);


            personPhysicalObservationsRecordPage
                .WaitForPersonPhysicalObservationsRecordPageToLoad()
                .ValidatePreferencesText("Physical Observation " + _currentDateSuffix)
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclined("Reason for Declined 2...")
                .ValidateEncouragementGiven("Encouragement given 2...")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
                .ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
                .ValidateDateAndTimeOccurred_DateText("01/06/2024")
                .ValidateDateAndTimeOccurred_TimeText("11:00")
                .ValidateCarenoteText("Jack was visually assessed as follows:\r\n" +
                "A: Breathing is noisy. There appears to be an obstruction in the mouth or throat.\r\n" +
                "B: Breathing is below 10 per minute or above 25 per minute. Breathing appears to be difficult, laboured and/or noisy.\r\n" +
                "C: Unable to stand unaided. Mottled or cyanosed skin. Blue grey tinge to mucus membranes inside the mouth.\r\n" +
                "D: Unexpectedly sleepy/drowsy, change in responsiveness – only responding to voice or physical stimuli or unresponsive. Unexpected or new confusion, coherence and/or disorientation.\r\n" +
                "E: New rashes, wounds actively bleeding. Staff or patient concerned. New pain discomfort. Is known or suspected to have physical health problems such as rapid tranquillisation, asthma, diabetes or alcohol/substance use.\r\n" +
                "Jack was in the Other Location, Living Room and Bedroom.\r\n" +
                "Jack used the following equipment: Other Equipment, BP Machine and Oral Thermometer.\r\n" +
                "Jack came across as Very Unhappy.\r\n" +
                "The action taken was: Action Taken.\r\n" +
                "Jack required assistance: Physical Assistance. Amount given: Fair.\r\n" +
                "This care was given at 01/06/2024 11:00:00.\r\n" +
                "Jack was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Jack.\r\n" +
                "We would like to note that: Additional notes.");

            #endregion

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class HeightAndWeight_UITestCases : FunctionalTest
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

        private Guid careprovidercareperiodsetupId;
        private Guid _specialistMattressId;
        private Guid rash_SkinConditionId;
        private Guid blisters_SkinConditionId;
        private Guid Livingroom_CarePhysicalLocationId;
        private Guid Other_LocationId;
        private Guid VeryHappy_WellbeingId;
        private Guid VeryUnhappy_WellbeingId;
        private Guid NoEquipmentId;
        private Guid Other_EquipmentId;
        private Guid careAssistanceNeededId;
        private Guid physicalAssistance;

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

                _systemUserName = "hnwuser1";
                _systemUserFullName = "HeightAndWeight User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "HeightAndWeight", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-8601

        [TestProperty("JiraIssueID", "ACC-8624")]
        [Description("Step(s) 1 to 7 from the original test - ACC-4066")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_UITestCases_ACC4066_UITestMethod01()
        {
            #region Care Period

            careprovidercareperiodsetupId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_teamId, "9 AM", new TimeSpan(9, 0, 0));

            #endregion

            #region Type Of Pressure Relieving Equipment In Use (Specialist Mattress Id)

            _specialistMattressId = dbHelper.specialistmattress.GetByName("Not applicable")[0];

            #endregion

            #region Describe Skin Condition

            rash_SkinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Rash")[0];
            blisters_SkinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Blister(s)")[0];

            #endregion

            #region Care Physical Locations 

            Livingroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            Other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Well Being

            VeryHappy_WellbeingId = dbHelper.careWellbeing.GetByName("Very Happy")[0];
            VeryUnhappy_WellbeingId = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion

            #region Equipment

            NoEquipmentId = dbHelper.careEquipment.GetByName("No equipment")[0];
            Other_EquipmentId = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion

            #region Care Assistances Needed

            careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            physicalAssistance = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 3

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateFieldIsVisible("Responsible User", false)
                .ValidateFieldIsVisible("Case Required", false)
                .ValidateFieldIsVisible("Commence a Care Plan", false);

            #endregion

            #region Step 4

            heightAndWeightRecordPage
                .SelectConsentGiven("Yes")
                .ValidateSectionName("Height", 2)
                .ValidateSectionName("Weight", 3);

            heightAndWeightRecordPage
                .ValidateHeightInMetresFieldIsVisible(true)
                .ValidateHeightInFeetFieldIsVisible(true)
                .ValidateHeightInInchesFieldIsVisible(true);

            #endregion

            #region Step 5

            heightAndWeightRecordPage
                .ValidateFieldIsVisible("Other Staff Who Assisted", false)
                .ValidateFieldIsVisible("Staff Required", true)
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName + "\r\n");

            #endregion

            #region Step 6

            var DateOccurred = DateTime.Now.AddDays(-2);
            string TimeOccurred = "12:00";

            heightAndWeightRecordPage
                .InsertTextOnDateAndTimeOccurred_DateField(DateOccurred.ToString("dd'/'MM'/'yyyy"));

            System.Threading.Thread.Sleep(1000);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickDateAndTimeOccurred_TimePicker();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour("12")
                .SelectMinute("00");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_TimeField(TimeOccurred)
                .VerifyPreferencesTextAreaFieldIsDisplayed(true)
                .VerifyPreferencesTextAreaFieldIsDisabled(true);

            heightAndWeightRecordPage
                .SelectConsentGiven("No");

            //Non consent detail = Absent
            heightAndWeightRecordPage
                .ValidateNonConsentDetailFieldVisible(true)
                .SelectNonConsentDetail("Absent");

            heightAndWeightRecordPage
                .ValidateReasonForAbsenceFieldVisible(true)
                .SetReasonForAbsence("Not available");

            //Non consent detail = Declined
            heightAndWeightRecordPage
                .SelectNonConsentDetail("Declined")
                .ValidateReasonForAbsenceFieldVisible(false);

            heightAndWeightRecordPage
                .ValidateReasonConsentDeclinedFieldVisible(true)
                .ValidateEncouragementGivenFieldVisible(true)
                .ValidateCareProvidedWithoutConsentOptionsVisible(true)
                .InsertTextInReasonConsentDeclined("Not interested")
                .InsertTextInEncouragementGiven("Encouragement not given")
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickCareProvidedWithoutConsent_NoRadioButton();

            //Non consent detail = Deferred
            heightAndWeightRecordPage
                .SelectNonConsentDetail("Deferred")
                .ValidateReasonForAbsenceFieldVisible(false)
                .ValidateReasonConsentDeclinedFieldVisible(false)
                .ValidateEncouragementGivenFieldVisible(false)
                .ValidateCareProvidedWithoutConsentOptionsVisible(false);

            heightAndWeightRecordPage
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true)
                .SetDeferredToDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftFieldVisible(true)
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeFieldVisible(true)
                .ValidateDeferredToShiftLookupButtonVisible(false)
                .SetDeferredToTime("12:00")
                .SelectDeferredToTimeOrShift("Shift")
                .ValidateDeferredToTimeFieldVisible(false)
                .ValidateDeferredToShiftLookupButtonVisible(true)
                .ClickDeferredToShiftLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("9 AM", careprovidercareperiodsetupId);

            heightAndWeightRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 7

            heightAndWeightRecordPage
                .SelectConsentGiven("Yes")
                .ValidateMandatoryFieldIsVisible("Location")
                .ValidateFieldIsVisible("Location If Other?", false)
                .ValidateLocationIfOtherTextareaFieldVisible(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_LocationId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherTextareaFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Location If Other?", true);

            heightAndWeightRecordPage
                .ValidateMandatoryFieldIsVisible("Equipment")
                .ValidateFieldIsVisible("Equipment If Other", false)
                .ValidateEquipmentIfOtherTextareaFieldVisible(false)
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(Other_EquipmentId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateEquipmentIfOtherTextareaFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Equipment If Other", true);

            heightAndWeightRecordPage
                .ValidateMandatoryFieldIsVisible("Wellbeing")
                .ValidateFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", false)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Unhappy", VeryUnhappy_WellbeingId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", true);

            heightAndWeightRecordPage
                .ValidateMandatoryFieldIsVisible("Assistance Needed?")
                .ValidateFieldIsVisible("Assistance Amount?", false)
                .ValidateAssistanceAmountPicklistVisible(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", physicalAssistance);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .ValidateMandatoryFieldIsVisible("Assistance Amount?", true);

            heightAndWeightRecordPage
                .ValidateTotalTimeSpentWithPersonMinutesFieldVisible(true)
                .ValidateAdditionalNotesFieldVisible(true);


            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8601

        [TestProperty("JiraIssueID", "ACC-8625")]
        [Description("Step(s) 8 to 12 from the original test - ACC-4066")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_UITestCases_ACC4066_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Andre";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 15, "Home Care " + _currentDateSuffix); //Height & Weight = 15

            #endregion

            #region Height & Weight Option Set

            var OptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object").FirstOrDefault();
            var OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(OptionSetId, "Height & Weight").FirstOrDefault();

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad();

            mainMenu
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Home Care " + _currentDateSuffix);

            #endregion

            #region Step 10

            heightAndWeightRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ValidateHeaderCellIsDisplayed("Head Circumference", false)
                .ValidateHeaderCellText(2, "Date and Time Occurred")
                .ValidateHeaderCellText(3, "Consent Given?")
                .ValidateHeaderCellText(4, "Non-consent Detail");

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            //Alert is not displayed in chromedriver when Customizations section is opened from new Height and Weight record
            //alertPopup
            //    .WaitForAlertPopupToLoad()
            //    .TapOKButton();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickOptionSetsButton();

            optionSetsPage
                .WaitForOptionSetsPageToLoad()
                .InsertQuickSearchText("Daily Care Record Business Object")
                .ClickQuickSearchButton()
                .WaitForOptionSetsPageToLoad()
                .OpenRecord(OptionSetId.ToString());

            optionSetsRecordPage
                .WaitForOptionSetsRecordPageToLoad()
                .NavigateToOptionSetValuesPage();

            optionsetValuesPage
                .WaitForOptionsetValuesPageToLoad()
                .InsertQuickSearchText("Height & Weight")
                .ClickQuickSearchButton()
                .WaitForOptionsetValuesPageToLoad()
                .ValidateOptionSetRecordIsAvailable(OptionSetValueId.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8644")]
        [Description("Step(s) 4, 9 from the original test - ACC-4066")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_UITestCases_ACC4066_UITestMethod03()
        {
            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId


            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();

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

            #region Reference Data for Height and Weight

            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var dateTimeOcurred = todayDate.ToUniversalTime().AddHours(-1);

            var _carePhysicalLocation = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationId, "name")["name"];
            var _locationIds = new Dictionary<Guid, String>();
            _locationIds.Add(_carePhysicalLocationId, _carePhysicalLocation.ToString());

            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUserName);

            #endregion

            #region Step 9 and 4

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            /*
             * Verify that a message should be displayed in preferences field If Care Preference record does not exist for the BO,

             * For all fields in “Height” section
             * “Estimated Value?”, “Height in Metres”, “Height in Feet”, “and Inches (Height)”
             * populate with data from previous Height and Weight Observation record (latest created record)
             */
            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Andre.")
                .SelectConsentGiven("Yes")
                .ValidateHeightmetresText("")
                .ValidateHeightfeetText("")
                .ValidateHeightinchesText("")
                .ValidateEstimatedheight_NoRadioButtonChecked()
                .ValidateEstimatedheight_YesRadioButtonNotChecked();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateWeightInKilogramsText("")
                .ValidateWeightpoundsText("")
                .ValidateWeightouncesText("")
                .ValidateEstimatedweight_NoRadioButtonChecked()
                .ValidateEstimatedweight_YesRadioButtonNotChecked()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Height and Weight

            var _personHeightAndWeightId = dbHelper.personHeightAndWeight.CreatePersonHeightAndWeight(_teamId, personId, "No preferences recorded, please check with Andre.",
                dateTimeOcurred, 1, false, 75m, 11, 11, 6, false, 1.85m, 6, 0, 5m, null, null, null, null, null, "", "", null, null, null, null, null, "", _locationIds, equipmentids, _careWellBeingdId,
                _careAssistanceNeededId, systemuserinfo, 30);

            #endregion

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .ValidateRecordPresent(_personHeightAndWeightId, true)
                .ClickNewRecordButton();

            /*
             * Verify that a message should be displayed in preferences field If Care Preference record does not exist for the BO,

             * For all fields in “Height” section
             * “Estimated Value?”, “Height in Metres”, “Height in Feet”, “and Inches (Height)”
             * populate with data from previous Height and Weight Observation record (latest created record)
             */
            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ValidatePreferencesText("No preferences recorded, please check with Andre.")
                .ValidateHeightmetresText("1.85")
                .ValidateHeightfeetText("6")
                .ValidateHeightinchesText("0")
                .ValidateEstimatedheight_NoRadioButtonChecked()
                .ValidateEstimatedheight_YesRadioButtonNotChecked();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateWeightInKilogramsText("")
                .ValidateWeightpoundsText("")
                .ValidateWeightouncesText("")
                .ValidateEstimatedweight_NoRadioButtonChecked()
                .ValidateEstimatedweight_YesRadioButtonNotChecked();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8880

        [TestProperty("JiraIssueID", "ACC-6121")]
        [Description("Step(s) 1 to 3 from the original test - ACC-6121")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_UITestCases_ACC6121_UITestMethod01()
        {
            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId


            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "James";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 15, "Home Care " + _currentDateSuffix); //Height & Weight = 15

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Home Care " + _currentDateSuffix);

            #endregion

            #region Step 3

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/07/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("00:30")
                .SelectConsentGiven("Yes")
                .InsertTextOnHeightmetres("1.85")
                .InsertTextOnWeightInKilograms("75")
                .InsertTextOnWeightlosskilos("5");

            heightAndWeightRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .SelectResultElement(_carePhysicalLocationId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("No equipment")
                .TapSearchButton()
                .SelectResultElement(_careEquipmentId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Happy", _careWellBeingdId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", _careAssistanceNeededId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Test")
                .ClickFlagrecordforhandover_NoRadioButton()
                .ClickIsincludeinnexthandover_NoRadioButton()
                .ClickSaveAndCloseButton();

            var heightAndWeightRecords = dbHelper.personHeightAndWeight.GetByPersonId(personId);
            Assert.AreEqual(1, heightAndWeightRecords.Count);
            var heightAndWeightId1 = heightAndWeightRecords[0];

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(heightAndWeightId1, true);

            heightAndWeightPage
                .OpenRecord(heightAndWeightId1);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("Home Care " + _currentDateSuffix)
                .ValidateDateAndTimeOccurred_DateText("01/07/2024")
                .ValidateDateAndTimeOccurred_TimeText("00:30")
                .ValidateConsentGivenSelectedText("Yes")
                .ValidateEstimatedheight_NoRadioButtonChecked()
                .ValidateEstimatedweight_YesRadioButtonNotChecked()
                .ValidateHeightmetresText("1.85")
                .ValidateHeightfeetText("6")
                .ValidateHeightinchesText("0")
                .ValidateEstimatedweight_NoRadioButtonChecked()
                .ValidateEstimatedweight_YesRadioButtonNotChecked()
                .ValidateWeightInKilogramsText("75.00")
                .ValidateWeightstonesText("11")
                .ValidateWeightpoundsText("11")
                .ValidateWeightouncesText("6")
                .ValidateHasamputation_NoRadioButtonChecked()
                .ValidateHasamputation_YesRadioButtonNotChecked()
                .ValidateWeightLossInKilogramsText("5.00")
                .ValidateWeightlossstonesText("0")
                .ValidateWeightlosspoundsText("11")
                .ValidateWeightLossPercentText("6.67")
                .ValidateAgeattimetakenText("24")
                .ValidateEstimatedbmi_NoRadioButtonChecked()
                .ValidateEstimatedbmi_YesRadioButtonNotChecked()
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("1")
                .ValidateAcutediseaseeffect_NoRadioButtonChecked()
                .ValidateAcutediseaseeffect_YesRadioButtonNotChecked()
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("1")
                .ValidateRiskText("Medium Risk")
                .ValidateSuggestedscreeningidSelectedText("")
                .ValidateMonitorfoodandfluid_YesRadioButtonChecked()
                .ValidateMonitorfoodandfluid_NoRadioButtonNotChecked()
                .ValidateNextscreeningdateText("")
                .ValidateNextscreeningdate_TimeText("")
                .ValidateAdditionalcommentsText("")
                .ValidateLocation_SelectedElementLinkText(_carePhysicalLocationId, "Living Room")
                .ValidateEquipment_SelectedElementLinkText(_careEquipmentId, "No equipment")
                .ValidateWellbeingLinkText("Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateAdditionalNotesText("Test")
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName)
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches).\r\n" +
                "James's weight was measured as 75 kg (11 st, 11 lb, 6 oz).\r\n" +
                "James's unplanned weight loss was measured as 5 kg (0 st, 11 lb). This is a percentage loss of 6.67%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal.\r\n" +
                "James has a MUST Total Score of 1 - Medium Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .InsertTextOnWeightlosskilos("25")
                .ValidateWeightlossstonesText("3")
                .ValidateWeightlosspoundsText("13")
                .ValidateWeightLossPercentText("33.33")
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("2")
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("2")
                .ValidateRiskText("High Risk")
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches).\r\n" +
                "James's weight was measured as 75 kg (11 st, 11 lb, 6 oz).\r\n" +
                "James's unplanned weight loss was measured as 25 kg (3 st, 13 lb). This is a percentage loss of 33.33%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal.\r\n" +
                "James has a MUST Total Score of 2 - High Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ValidateWeightLossInKilogramsText("25.00")
                .ValidateWeightlossstonesText("3")
                .ValidateWeightlosspoundsText("13")
                .ValidateWeightLossPercentText("33.33")
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("2")
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("2")
                .ValidateRiskText("High Risk")
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches).\r\n" +
                "James's weight was measured as 75 kg (11 st, 11 lb, 6 oz).\r\n" +
                "James's unplanned weight loss was measured as 25 kg (3 st, 13 lb). This is a percentage loss of 33.33%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal.\r\n" +
                "James has a MUST Total Score of 2 - High Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .InsertTextOnWeightlosskilos("7.5")
                .ValidateWeightlossstonesText("1")
                .ValidateWeightlosspoundsText("2")
                .ValidateWeightLossPercentText("10")
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("1")
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("1")
                .ValidateRiskText("Medium Risk")
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches).\r\n" +
                "James's weight was measured as 75 kg (11 st, 11 lb, 6 oz).\r\n" +
                "James's unplanned weight loss was measured as 7.5 kg (1 st, 2 lb). This is a percentage loss of 10%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal.\r\n" +
                "James has a MUST Total Score of 1 - Medium Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ValidateWeightLossInKilogramsText("7.50")
                .ValidateWeightlossstonesText("1")
                .ValidateWeightlosspoundsText("2")
                .ValidateWeightLossPercentText("10.00")
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("1")
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("1")
                .ValidateRiskText("Medium Risk")
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches).\r\n" +
                "James's weight was measured as 75 kg (11 st, 11 lb, 6 oz).\r\n" +
                "James's unplanned weight loss was measured as 7.5 kg (1 st, 2 lb). This is a percentage loss of 10%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal.\r\n" +
                "James has a MUST Total Score of 1 - Medium Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .ClickEstimatedheight_YesRadioButton()
                .ClickEstimatedweight_YesRadioButton()
                .InsertTextOnWeightlosskilos("10")
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ValidateWeightLossInKilogramsText("10.00")
                .ValidateWeightlossstonesText("1")
                .ValidateWeightlosspoundsText("8")
                .ValidateWeightLossPercentText("13.33")
                .ValidateBmiresultText("Normal")
                .ValidateBmiscoreText("21.91")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("2")
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("2")
                .ValidateRiskText("High Risk")
                .ValidateCarenoteText("James's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "James's weight was measured as 75.00 kg (11 st, 11 lb, 6 oz). This was an estimated value.\r\n" +
                "James's unplanned weight loss was measured as 10 kg (1 st, 8 lb). This is a percentage loss of 13.33%.\r\n" +
                "James's calculated BMI score is 21.91, which is categorised as Normal. This is an estimated result.\r\n" +
                "James has a MUST Total Score of 2 - High Risk.\r\n" +
                "James was in the Living Room.\r\n" +
                "James used the following equipment: No equipment.\r\n" +
                "James came across as Happy.\r\n" +
                "James did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "James was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with James.\r\nWe would like to note that: Test.");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9402

        [TestProperty("JiraIssueID", "ACC-8350")]
        [Description("Step(s) 1 to 9 from the original test - ACC-8350")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_ACC8350_UITestMethod01()
        {
            #region Create SystemUser Record

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
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First());

            #endregion


            var _rosteredUsername = "hnwrostereduser1";
            var _systemUserFullName2 = "HeightAndWeight RosteredUser1";
            var _rosteredUserId = commonMethodsDB.CreateSystemUserRecord(_rosteredUsername, "HeightAndWeight", "RosteredUser1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #region Team membership

            _defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true)[0];
            commonMethodsDB.CreateTeamMember(_defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

            #endregion

            #endregion

            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room").FirstOrDefault();
            var _otherCarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Other").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();
            var _otherCareEquipmentId = dbHelper.careEquipment.GetByName("Other").FirstOrDefault();
            var _sittingScaleId = dbHelper.careEquipment.GetByName("Sitting scales").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId


            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();
            var _careAssistanceNeededId2 = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();
            var _careWellBeingdId2 = dbHelper.careWellbeing.GetByName("OK").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Noah";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_rosteredUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true);

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/07/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("00:30")
                .SelectConsentGiven("Yes")
                .ClickSectionMenuButton_Weight()
                .ClickSectionMenuButton_Height();

            #endregion

            #region Step 3

            heightAndWeightRecordPage
                .ClickEstimatedheight_YesRadioButton()
                .ClickEstimatedweight_YesRadioButton()
                .InsertTextOnHeightmetres("1.85");

            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("10")
                .ValidateWeightInKilogramsText("")
                .ValidateWeightInKilogramsFieldDisabled(true)
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked();

            #endregion

            #region Step 4

            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("")
                .ValidateWeightInKilogramsText("")
                .ValidateWeightInKilogramsFieldDisabled(false)
                .ValidateEstimatedbmi_YesRadioButtonNotChecked()
                .ValidateEstimatedbmi_NoRadioButtonChecked();

            #endregion

            #region Step 5

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnWeightInKilograms("75")
                .ValidateMuacmeasurementcmText("")
                .ValidateMuacmeasurementcmFieldDisabled(true)
                .ValidateEstimatedbmi_NoRadioButtonChecked()
                .ValidateEstimatedbmi_YesRadioButtonNotChecked();

            heightAndWeightRecordPage
                .InsertTextOnWeightInKilograms("")
                .ValidateMuacmeasurementcmText("")
                .ValidateMuacmeasurementcmFieldDisabled(false)
                .ValidateEstimatedbmi_NoRadioButtonChecked()
                .ValidateEstimatedbmi_YesRadioButtonNotChecked();

            #endregion

            #region Step 6, 7, 8

            //If MUAC is greater than 32.0 cm, BMI = 31
            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("32.1")
                .ValidateBmiscoreText("31")
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked();

            //If MUAC is less than 23.5 cm, BMI = 19
            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("23.4")
                .ValidateBmiscoreText("19");

            //If MUAC is equal to or greater than 23.5 AND less than or equal to 32.0 then BMI = 25
            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("27.5")
                .ValidateBmiscoreText("25");

            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("32")
                .ValidateBmiscoreText("25");

            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("23.5")
                .ValidateBmiscoreText("25")
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked();

            #endregion

            #region Step 9

            heightAndWeightRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .SelectResultElement(_carePhysicalLocationId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("No equipment")
                .TapSearchButton()
                .SelectResultElement(_careEquipmentId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Happy", _careWellBeingdId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", _careAssistanceNeededId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Test")
                .ClickSaveAndCloseButton();

            var heightAndWeightRecords = dbHelper.personHeightAndWeight.GetByPersonId(personId);
            Assert.AreEqual(1, heightAndWeightRecords.Count);
            var heightAndWeightId1 = heightAndWeightRecords[0];

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(heightAndWeightId1, true);

            heightAndWeightPage
                .OpenRecord(heightAndWeightId1);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Noah.")
                .ValidateDateAndTimeOccurred_DateText("01/07/2024")
                .ValidateDateAndTimeOccurred_TimeText("00:30")
                .ValidateConsentGivenSelectedText("Yes")
                .ValidateEstimatedheight_YesRadioButtonChecked()
                .ValidateEstimatedweight_NoRadioButtonNotChecked()
                .ValidateHeightmetresText("1.85")
                .ValidateHeightfeetText("6")
                .ValidateHeightinchesText("0")
                .ValidateEstimatedweight_YesRadioButtonChecked()
                .ValidateEstimatedweight_NoRadioButtonNotChecked()
                .ValidateMuacmeasurementcmText("23.5")
                .ValidateWeightInKilogramsText("")
                .ValidateWeightstonesText("")
                .ValidateWeightpoundsText("")
                .ValidateWeightouncesText("")
                .ValidateHasamputation_NoRadioButtonChecked()
                .ValidateHasamputation_YesRadioButtonNotChecked()
                .ValidateAgeattimetakenText("24")
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked()
                .ValidateBmiresultText("Overweight")
                .ValidateBmiscoreText("25")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("0")
                .ValidateAcutediseaseeffect_NoRadioButtonChecked()
                .ValidateAcutediseaseeffect_YesRadioButtonNotChecked()
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("0")
                .ValidateRiskText("Low Risk")
                .ValidateSuggestedscreeningidSelectedText("")
                .ValidateMonitorfoodandfluid_NoRadioButtonChecked()
                .ValidateMonitorfoodandfluid_YesRadioButtonNotChecked()
                .ValidateNextscreeningdateText("")
                .ValidateNextscreeningdate_TimeText("")
                .ValidateAdditionalcommentsText("")
                .ValidateLocation_SelectedElementLinkText(_carePhysicalLocationId, "Living Room")
                .ValidateEquipment_SelectedElementLinkText(_careEquipmentId, "No equipment")
                .ValidateWellbeingLinkText("Happy")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateAdditionalNotesText("Test")
                .ValidateStaffRequiredSelectedOptionText(_rosteredUserId, _systemUserFullName2)
                .ValidateCarenoteText("Noah's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "Noah's calculated BMI score is 25, which is categorised as Overweight. This is an estimated result.\r\n" +
                "Noah has a MUST Total Score of 0 - Low Risk.\r\n" +
                "Noah was in the Living Room.\r\n" +
                "Noah used the following equipment: No equipment.\r\n" +
                "Noah came across as Happy.\r\n" +
                "Noah did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "Noah was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Noah.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .InsertTextOnMuacmeasurementcm("33")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(_otherCarePhysicalLocationId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOtherTextareaField("Test")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .AddElementToList(_otherCareEquipmentId)
                .TypeSearchQuery("Sitting scales")
                .TapSearchButton()
                .AddElementToList(_sittingScaleId)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEquipmentIfOtherTextareaField("Test")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("OK", _careWellBeingdId2);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTakenTextareaField("Test")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Physical Assistance", _careAssistanceNeededId2);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmountFromPicklist("Fair")
                .ClickSaveButton()
                .WaitForPageToLoad();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesText("No preferences recorded, please check with Noah.")
                .ValidateDateAndTimeOccurred_DateText("01/07/2024")
                .ValidateDateAndTimeOccurred_TimeText("00:30")
                .ValidateConsentGivenSelectedText("Yes")
                .ValidateEstimatedheight_YesRadioButtonChecked()
                .ValidateEstimatedweight_NoRadioButtonNotChecked()
                .ValidateHeightmetresText("1.85")
                .ValidateHeightfeetText("6")
                .ValidateHeightinchesText("0")
                .ValidateEstimatedweight_YesRadioButtonChecked()
                .ValidateEstimatedweight_NoRadioButtonNotChecked()
                .ValidateMuacmeasurementcmText("33.0")
                .ValidateWeightInKilogramsText("")
                .ValidateWeightstonesText("")
                .ValidateWeightpoundsText("")
                .ValidateWeightouncesText("")
                .ValidateHasamputation_NoRadioButtonChecked()
                .ValidateHasamputation_YesRadioButtonNotChecked()
                .ValidateAgeattimetakenText("24")
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked()
                .ValidateBmiresultText("Obese")
                .ValidateBmiscoreText("31")
                .ValidateBmimustscoreText("0")
                .ValidateWeightlossmustscoreText("0")
                .ValidateAcutediseaseeffect_NoRadioButtonChecked()
                .ValidateAcutediseaseeffect_YesRadioButtonNotChecked()
                .ValidateAcutediseasemustscoreText("0")
                .ValidateMusttotalscoreText("0")
                .ValidateRiskText("Low Risk")
                .ValidateSuggestedscreeningidSelectedText("")
                .ValidateMonitorfoodandfluid_NoRadioButtonChecked()
                .ValidateMonitorfoodandfluid_YesRadioButtonNotChecked()
                .ValidateNextscreeningdateText("")
                .ValidateNextscreeningdate_TimeText("")
                .ValidateAdditionalcommentsText("")
                .ValidateLocation_SelectedElementLinkText(_carePhysicalLocationId, "Living Room")
                .ValidateLocation_SelectedElementLinkText(_otherCarePhysicalLocationId, "Other")
                .ValidateLocationIfOtherTextareaField("Test")
                .ValidateEquipment_SelectedElementLinkText(_careEquipmentId, "No equipment")
                .ValidateEquipment_SelectedElementLinkText(_otherCareEquipmentId, "Other")
                .ValidateEquipment_SelectedElementLinkText(_sittingScaleId, "Sitting scales")
                .ValidateEquipmentIfOtherTextareaField("Test")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenTextareaField("Test")
                .ValidateAssistanceNeededLinkText("Physical Assistance")
                .ValidateAssistanceAmountPicklistSelectedValue("Fair")
                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateAdditionalNotesText("Test")
                .ValidateStaffRequiredSelectedOptionText(_rosteredUserId, _systemUserFullName2)
                
                .ValidateCarenoteText("Noah's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "Noah's calculated BMI score is 31, which is categorised as Obese. This is an estimated result.\r\n" +
                "Noah has a MUST Total Score of 0 - Low Risk.\r\n" +
                "Noah was in the Living Room and Test.\r\n" +
                "Noah used the following equipment: No equipment, Test and Sitting scales.\r\n" +
                "Noah came across as OK.\r\n" +
                "The action taken was: Test.\r\n" +
                "Noah required assistance: Physical Assistance. Amount given: Fair.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "Noah was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Noah.\r\n" +
                "We would like to note that: Test.");

            #endregion
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9404

        [TestProperty("JiraIssueID", "ACC-5266")]
        [Description("Step(s) 1 to 5, 15 from the original test - ACC-5266")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_ACC5266_UITestMethod01()
        {

            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room").FirstOrDefault();
            var _otherLocationId = dbHelper.carePhysicalLocation.GetByName("Other").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();
            var _otherEquipmentId = dbHelper.careEquipment.GetByName("Other").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId

            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Asked For Help").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Unhappy").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Pete";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 15, "Home Care " + _currentDateSuffix); //Height & Weight = 15

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad();

            #endregion

            #region Step 3 - 4

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/07/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("00:30")
                .SelectConsentGiven("Yes")
                .ValidateCarenoteIsVisible(true);

            #endregion

            #region Step 5

            heightAndWeightRecordPage
                .ClickEstimatedheight_YesRadioButton()
                .InsertTextOnHeightmetres("1.85")
                .ClickEstimatedweight_YesRadioButton()
                .InsertTextOnWeightInKilograms("75")
                .InsertTextOnWeightlosskilos("5");

            heightAndWeightRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePhysicalLocationId)
                .AddElementToList(_otherLocationId)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOtherTextareaField("Other location")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_careEquipmentId)
                .AddElementToList(_otherEquipmentId)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEquipmentIfOtherTextareaField("Other equipment")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careWellBeingdId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTakenTextareaField("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careAssistanceNeededId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmountFromPicklist("A Lot")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Test")
                .ClickSaveAndCloseButton();

            var heightAndWeightRecords = dbHelper.personHeightAndWeight.GetByPersonId(personId);
            Assert.AreEqual(1, heightAndWeightRecords.Count);
            var heightAndWeightId1 = heightAndWeightRecords[0];

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(heightAndWeightId1, true);

            heightAndWeightPage
                .OpenRecord(heightAndWeightId1);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateCarenoteText("Pete's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "Pete's weight was measured as 75 kg (11 st, 11 lb, 6 oz). This was an estimated value.\r\n" +
                "Pete's unplanned weight loss was measured as 5 kg (0 st, 11 lb). This is a percentage loss of 6.67%.\r\n" +
                "Pete's calculated BMI score is 21.91, which is categorised as Normal. This is an estimated result.\r\n" +
                "Pete has a MUST Total Score of 1 - Medium Risk.\r\n" +
                "Pete was in the Living Room and Other location.\r\n" +
                "Pete used the following equipment: No equipment and Other equipment.\r\n" +
                "Pete came across as Unhappy.\r\n" +
                "The action taken was: Action taken.\r\n" +
                "Pete required assistance: Asked For Help. Amount given: A Lot.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "Pete was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Pete.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .ClickBackButton();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            string todayTime = todayDate.ToString("HH:mm:00");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .WaitForPageToLoad()
                .ValidateEstimatedheight_YesRadioButtonChecked()
                .ValidateEstimatedheight_NoRadioButtonNotChecked()
                .ValidateHeightmetresText("1.85")
                .ValidateHeightfeetText("6")
                .ValidateHeightinchesText("0")
                .ValidateCarenoteText("Pete's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "This care was given at " + todayDate.ToString("dd'/'MM'/'yyyy") + " " + todayTime + ".\r\n" +
                "Pete was assisted by 1 colleague(s).");

            #endregion

            #region Step 6 - 14

            //Step 6 to Step 14 are not valid for web UI. They are for mobile app.

            #endregion

            #region Step 15

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextInReasonConsentDeclined("Pete refused to have his height and weight measured.")
                .InsertTextInEncouragementGiven("Pete was encouraged to have his height and weight measured.")
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .ClickEstimatedheight_YesRadioButton()
                .InsertTextOnHeightmetres("1.85")
                .ClickEstimatedweight_YesRadioButton()
                .InsertTextOnWeightInKilograms("70")
                .InsertTextOnWeightlosskilos("5");

            heightAndWeightRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePhysicalLocationId)
                .AddElementToList(_otherLocationId)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOtherTextareaField("Other location")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_careEquipmentId)
                .AddElementToList(_otherEquipmentId)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEquipmentIfOtherTextareaField("Other equipment")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careWellBeingdId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTakenTextareaField("Action taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careAssistanceNeededId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmountFromPicklist("A Lot")
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Test");

            heightAndWeightRecordPage                
                .InsertTextOnDateAndTimeOccurred_DateField("02/07/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("08:00");

            heightAndWeightRecordPage
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ClickBackButton();

            heightAndWeightRecords = dbHelper.personHeightAndWeight.GetByPersonId(personId);
            Assert.AreEqual(2, heightAndWeightRecords.Count);
            var heightAndWeightId2 = heightAndWeightRecords[0];

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(heightAndWeightId2, true);

            heightAndWeightPage
                .OpenRecord(heightAndWeightId2);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclined("Pete refused to have his height and weight measured.")
                .ValidateEncouragementGiven("Pete was encouraged to have his height and weight measured.")
                .ValidateCarenoteText("Pete's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "Pete's weight was measured as 70 kg (11 st, 0 lb, 5 oz). This was an estimated value.\r\n" +
                "Pete's unplanned weight loss was measured as 5 kg (0 st, 11 lb). This is a percentage loss of 7.14%.\r\n" +
                "Pete's calculated BMI score is 20.45, which is categorised as Normal. This is an estimated result.\r\n" +
                "Pete has a MUST Total Score of 1 - Medium Risk.\r\n" +
                "Pete was in the Living Room and Other location.\r\n" +
                "Pete used the following equipment: No equipment and Other equipment.\r\n" +
                "Pete came across as Unhappy.\r\n" +
                "The action taken was: Action taken.\r\n" +
                "Pete required assistance: Asked For Help. Amount given: A Lot.\r\n" +
                "This care was given at 02/07/2024 08:00:00.\r\n" +
                "Pete was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Pete.\r\n" +
                "We would like to note that: Test.");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-9446")]
        [Description("Step(s) from test for story ACC-5928. Verify Previous MUST Score section." +
            "Does The Person Have An Amputation?, Amputation Level, Muac measurements.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Physical Observation and Monitoring")]
        [TestProperty("Screen 1", "Height & Weight")]
        public void HeightAndWeight_ACC9446_UITestMethod01()
        {

            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId

            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();

            #endregion

            #region Amputation Level

            var AmputationLevelOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Amputation Level").FirstOrDefault();
            var OptionSetValueId1 = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(AmputationLevelOptionSetId, "Right Foot").FirstOrDefault(); //Right Foot = Numeric Code 2
            var OptionSetValueId2 = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(AmputationLevelOptionSetId, "Below Right Knee").FirstOrDefault(); //Below Right Knee = Numeric Code 4

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Tom";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Steps

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
                .NavigateToHeightWeightObservationsPage();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_DateField("01/07/2024")
                .InsertTextOnDateAndTimeOccurred_TimeField("00:30")
                .SelectConsentGiven("Yes")
                .ClickEstimatedheight_YesRadioButton()
                .InsertTextOnHeightmetres("1.85")
                .ClickEstimatedweight_YesRadioButton()
                .ValidateAmputationlevelLookupButtonVisible(false)
                .ClickHasamputation_YesRadioButton()
                .ValidateAmputationlevelLookupButtonVisible(true)
                .ClickAmputationlevelLookupButton();

            lookupMultiSelectPopup
                .WaitForOptionSetLookupMultiSelectPopupToLoad()
                .AddElementToList(OptionSetValueId1)
                .AddElementToList(OptionSetValueId2)
                .TapOKButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnMuacmeasurementcm("50")
                .InsertTextOnWeightlosskilos("5")
                .ValidateAcutediseaseeffect_NoRadioButtonChecked() //Acute Disease Effect (ADE) Field is No by default
                .ValidateAcutediseaseeffect_YesRadioButtonNotChecked()
                .ClickAcutediseaseeffect_YesRadioButton();

            heightAndWeightRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(_carePhysicalLocationId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .SelectResultElement(_careEquipmentId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careWellBeingdId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careAssistanceNeededId);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Test")
                .ClickSaveAndCloseButton();

            var heightAndWeightRecords = dbHelper.personHeightAndWeight.GetByPersonId(personId);
            Assert.AreEqual(1, heightAndWeightRecords.Count);
            var heightAndWeightId1 = heightAndWeightRecords[0];

            heightAndWeightPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(heightAndWeightId1, true);

            heightAndWeightPage
                .OpenRecord(heightAndWeightId1);

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateMusttotalscorepreviousText("")
                .ValidateRiskpreviousText("")
                .ValidateHasamputation_YesRadioButtonChecked()
                .ValidateHasamputation_NoRadioButtonNotChecked()
                .ValidateAmputationLevelSelectedElementLinkText(2, "Right Foot\r\n")
                .ValidateAmputationLevelSelectedElementLinkText(4, "Below Right Knee\r\n")
                .ValidateMuacmeasurementcmText("50.0")
                .ValidateBmiresultText("Obese")
                .ValidateBmiscoreText("31")
                .ValidateMusttotalscoreText("2")
                .ValidateRiskText("High Risk")
                .ValidateCarenoteText("Tom's height was measured as 1.85 m (6 ft, 0 inches). This was an estimated value.\r\n" +
                "Tom's unplanned weight loss was measured as 5 kg (0 st, 11 lb). This is a percentage loss of %.\r\n" +
                "Tom's calculated BMI score is 31, which is categorised as Obese. This is an estimated result.\r\n" +
                "Tom has a MUST Total Score of 2 - High Risk.\r\n" +
                "Tom was in the Living Room.\r\n" +
                "Tom used the following equipment: No equipment.\r\n" +
                "Tom came across as Happy.\r\n" +
                "Tom did not require any assistance.\r\n" +
                "This care was given at 01/07/2024 00:30:00.\r\n" +
                "Tom was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Tom.\r\n" +
                "We would like to note that: Test.");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ClickBackButton();

            heightAndWeightPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateMusttotalscorepreviousText("2")
                .ValidateRiskpreviousText("High Risk");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateHasamputation_YesRadioButtonChecked()
                .ValidateHasamputation_NoRadioButtonNotChecked()
                .ValidateAmputationLevelSelectedElementLinkText(2, "Right Foot\r\n")
                .ValidateAmputationLevelSelectedElementLinkText(4, "Below Right Knee\r\n");

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateEstimatedbmi_YesRadioButtonChecked()
                .ValidateEstimatedbmi_NoRadioButtonNotChecked();

            heightAndWeightRecordPage
                .WaitForPageToLoad()
                .ValidateMuacmeasurementcmText("");

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


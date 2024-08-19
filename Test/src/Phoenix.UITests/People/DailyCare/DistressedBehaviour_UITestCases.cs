using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class DistressedBehaviour_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid defaultTeamId;
        private List<Guid> securityProfilesList;
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DistressedBehaviourBU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DB T1", null, _businessUnitId, "907678", "DistressedBehaviourT1@careworkstempmail.com", "DistressedBehaviour T1", "020 123456");

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
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First());

                #endregion

                #region Create System User Record

                _systemUserName = "BehaviourUser1";
                _systemUserFullName = "Behaviour User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Behaviour", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

                #endregion

                #region Default Team

                //get the default team for the tenant
                defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

                #endregion

                #region Team Member

                //link the user with the default team so that we can access all reference data in the system
                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-9042

        [TestProperty("JiraIssueID", "ACC-9099")]
        [Description("Step(s) 1 to 9 from the original test - ACC-2612")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod01()
        {            

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Ben";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Period

            var careprovidercareperiodsetupId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_teamId, "9 AM", new TimeSpan(9, 0, 0));

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ValidateHeaderCellIsDisplayed("Consent Given?")
                .ValidateHeaderCellIsDisplayed("Non-consent Detail")
                .ValidateHeaderCellIsDisplayed("Reason for Absence?", false)
                .ValidateHeaderCellIsDisplayed("Reason Consent Declined?", false)
                .ValidateHeaderCellIsDisplayed("Encouragement Given?", false)
                .ValidateHeaderCellIsDisplayed("Care provided without consent?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Date?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Time?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Shift?", false)
                .ValidateHeaderCellIsDisplayed("Preferences", false)
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes")
                .SelectConsentGiven("No");

            #endregion

            #region Step 2

            distressedBehaviourRecordPage
                .ValidateNonConsentDetailFieldVisible(true)
                .ValidateSelectedNonConsentDetail("")
                .SelectNonConsentDetail("Absent")
                .SelectNonConsentDetail("Declined")
                .SelectNonConsentDetail("Deferred");

            #endregion

            #region Step 3

            distressedBehaviourRecordPage
                .SelectNonConsentDetail("Absent")
                .ValidateReasonForAbsenceFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Reason for Absence?")
                .ValidateReasonForAbsenceFieldMaxLength("2000")
                .SetReasonForAbsence("Visit to the doctor");

            #endregion

            #region Step 4

            distressedBehaviourRecordPage
                .SelectNonConsentDetail("Declined")
                .ValidateReasonConsentDeclinedFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Reason Consent Declined?")
                .ValidateReasonConsentDeclinedFieldMaxLength("2000");

            #endregion

            #region Step 5

            distressedBehaviourRecordPage
                .ValidateEncouragementGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Encouragement Given?")
                .ValidateEncouragementGivenFieldMaxLength("2000");

            #endregion

            #region Step 6

            distressedBehaviourRecordPage
                .ValidateCareProvidedWithoutConsentOptionsVisible(true)
                .ValidateMandatoryFieldIsVisible("Care provided without consent?")
                .InsertTextInReasonConsentDeclined("Not in a good mental state")
                .InsertTextInEncouragementGiven("Mental support")
                .ClickCareProvidedWithoutConsent_NoRadioButton();

            #endregion

            #region Step 7

            var deferredToDate = DateTime.Now.AddDays(2);

            distressedBehaviourRecordPage
                .SelectNonConsentDetail("Deferred")
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true)
                .ValidateMandatoryFieldIsVisible("Deferred To Date?")
                .SetDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 8

            distressedBehaviourRecordPage
                .ValidateDeferredToTimeOrShiftFieldVisible(true)                
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeFieldVisible(true)
                .VerifyDeferredToTime_TimePickerIsDisplayed(true)
                .ValidateMandatoryFieldIsVisible("Deferred To Time?")

                .SetDeferredToTime("09:00");

            #endregion

            #region Step 9

            distressedBehaviourRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ValidateMandatoryFieldIsVisible("Deferred To Shift?")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("9 AM", careprovidercareperiodsetupId);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var careProvidedWithoutConsentRecords = dbHelper.cpPersonBehaviourIncident.GetByPersonId(personId);
            Assert.AreEqual(1, careProvidedWithoutConsentRecords.Count);
            var personDistressedBehaviourRecordId = careProvidedWithoutConsentRecords[0];

            distressedBehavioursPage
                .OpenRecord(personDistressedBehaviourRecordId);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(personFullName)
                .ValidateResponsibleTeamLinkText("DB T1")
                .ValidatePreferencesFieldIsDisabled(true)
                .ValidatePreferencesText("No preferences recorded, please check with Ben.")
                .ValidateOccurredText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateOccurred_TimeText("07:00")
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("9 AM");


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9100")]
        [Description("Step(s) 10 to 15 from the original test - ACC-2612")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod02()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Ben";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 7, "Person Behaviour or Distressed Behaviour " + _currentDateSuffix); //Distressed Behaviour = 7

            #endregion

            #region Incident Triggers - IncidentTrigger

            var TriggerId1 = dbHelper.incidentTrigger.GetByName("Frightened")[0];
            var TriggerId2 = dbHelper.incidentTrigger.GetByName("Noise")[0];
            var TriggerId3 = dbHelper.incidentTrigger.GetByName("Lack of stimulation")[0];
            var TriggerId4 = dbHelper.incidentTrigger.GetByName("Pain")[0];
            var TriggerId5 = dbHelper.incidentTrigger.GetByName("Tired")[0];
            var TriggerId6 = dbHelper.incidentTrigger.GetByName("Personal care")[0];
            var TriggerId7 = dbHelper.incidentTrigger.GetByName("Hungry")[0];
            var TriggerId8 = dbHelper.incidentTrigger.GetByName("Embarrassment")[0];
            var TriggerId9 = dbHelper.incidentTrigger.GetByName("Thirsty")[0];
            var TriggerId10 = dbHelper.incidentTrigger.GetByName("Communication needs not met")[0];
            var TriggerId11 = dbHelper.incidentTrigger.GetByName("Other")[0];

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes");

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidatePreferencesFieldIsDisabled(true)
                .ValidatePreferencesText("Person Behaviour or Distressed Behaviour " + _currentDateSuffix);

            #endregion

            #region Step 11

            distressedBehaviourRecordPage
                .InsertTextOnAntecedent("Antecedent9042")
                .InsertTextOnBehaviour("Behaviour9042")
                .InsertTextOnConsequence("Consequence9042")
                .ValidateWerethereanytriggers_NoOptionChecked()
                .ValidateWerethereanytriggers_YesOptionNotChecked();

            #endregion

            #region Step 12

            distressedBehaviourRecordPage
                .ValidateWhatWereTheTriggersLookupButtonIsVisible(false)
                .ClickWerethereanytriggers_YesOption()
                .ValidateWhatWereTheTriggersLookupButtonIsVisible(true)
                .ValidateMandatoryFieldIsVisible("What were the triggers?")
                .ClickWhatwerethetriggersLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(TriggerId1)
                .AddElementToList(TriggerId2)
                .AddElementToList(TriggerId3)
                .AddElementToList(TriggerId4)
                .AddElementToList(TriggerId5)
                .AddElementToList(TriggerId6)
                .AddElementToList(TriggerId7)
                .AddElementToList(TriggerId8)
                .AddElementToList(TriggerId9)
                .AddElementToList(TriggerId10)
                .AddElementToList(TriggerId11)
                .TapOKButton();

            #endregion

            #region Step 13

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateTriggersIfOtherTextareaVisible(true)
                .ValidateMandatoryFieldIsVisible("Triggers If Other?")
                .ValidateTriggersIfOtherTextareaFieldMaxLength("2000")
                .InsertTextOnTriggersIfOtherTextarea("Other");

            #endregion

            #region Step 14

            distressedBehaviourRecordPage
                .ValidateReviewRequiredBySeniorColleagueRadioButtonsVisible(true)
                .ValidateReviewrequiredbyseniorcolleague_NoOptionChecked()
                .ValidateReviewrequiredbyseniorcolleague_YesOptionNotChecked()
                .ValidateMandatoryFieldIsVisible("Review required by Senior Colleague?", false);

            #endregion

            #region Step 15

            distressedBehaviourRecordPage
                .ValidateReviewDetailsIsVisible(false)
                .ClickReviewrequiredbyseniorcolleague_YesOption()
                .ValidateReviewDetailsIsVisible(true)
                .ValidateMandatoryFieldIsVisible("Review details")
                .ValidateReviewDetailsFieldMaxLength("2000")
                .InsertReviewDetails("Review details here ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9101")]
        [Description("Step(s) 16 to 19 from the original test - ACC-2612")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod03()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Ben";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Option Set

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object")[0];

            #endregion

            #region Option Set Value

            var continenceCareOptionSetValueId1 = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Continence Care")[0];
            var continenceCareNumericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(continenceCareOptionSetValueId1, "numericcode")["numericcode"]);
            var continenceCareText = (string)(dbHelper.optionsetValue.GetOptionsetValueByID(continenceCareOptionSetValueId1, "text")["text"]);
            Dictionary<int, string> continenceSetValueIds1 = new Dictionary<int, string> { };
            continenceSetValueIds1.Add(continenceCareNumericCode, continenceCareText);

            var distressedBehaviourOptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Distressed Behaviour")[0];
            var distressedBehaviourNumericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "numericcode")["numericcode"]);
            var distressedBehaviourText = (string)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "text")["text"]);
            Dictionary<int, string> behaviourSetValueIds2 = new Dictionary<int, string> { };
            behaviourSetValueIds2.Add(distressedBehaviourNumericCode, distressedBehaviourText);

            #endregion

            #region Care Physical Locations 

            var bathroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Bathroom")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            var notValid_LocationId = commonMethodsDB.CreateCarePhysicalLocation("Mall", new DateTime(2024, 1, 1), _teamId, continenceSetValueIds1);
            var valid_LocationId = commonMethodsDB.CreateCarePhysicalLocation("Park", new DateTime(2024, 2, 1), _teamId, behaviourSetValueIds2);

            if(!dbHelper.carePhysicalLocationDailyCare.GetByCarePhysicalLocationIdAndOptionSetValueId(notValid_LocationId, continenceCareNumericCode).Any())
                dbHelper.carePhysicalLocationDailyCare.CreateCarePhysicalLocationDailyCare(notValid_LocationId, continenceCareNumericCode);
            if (!dbHelper.carePhysicalLocationDailyCare.GetByCarePhysicalLocationIdAndOptionSetValueId(valid_LocationId, distressedBehaviourNumericCode).Any())
                dbHelper.carePhysicalLocationDailyCare.CreateCarePhysicalLocationDailyCare(valid_LocationId, distressedBehaviourNumericCode);


            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeing3Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];
            var careWellbeing4Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes");

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Location")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Mall")
                .TapSearchButton()
                .ValidateResultElementNotPresent(notValid_LocationId)
                .TypeSearchQuery("Park")
                .TapSearchButton()
                .ValidateResultElementPresent(valid_LocationId)
                .TypeSearchQuery("")
                .TapSearchButton()
                .AddElementToList(bathroom_LocationId)
                .AddElementToList(valid_LocationId);

            #endregion

            #region Step 17

            lookupMultiSelectPopup
                .AddElementToList(other_LocationId)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisible(true)
                .ValidateLocationIfOtherTextareaFieldMaxLength("2000")
                .InsertTextOnLocationIfOtherTextareaField("other location ...");

            #endregion

            #region Step 18

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateWellbeingLookupButtonIsVisible(true)
                .ValidateMandatoryFieldIsVisible("Wellbeing")
                .ClickWellbeingLookupButton();

            #endregion

            #region Step 19

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeing4Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenIsVisible(false)
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Unhappy", careWellbeing1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", false)
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 1 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing2Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", false)
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 2 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Very Unhappy", careWellbeing3Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateMandatoryFieldIsVisible("Action Taken? (Has Pain Relief been offered?)", false)
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 3 ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9102")]
        [Description("Step(s) 20 to 23 from the original test - ACC-2612")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod04()
        {

            #region Create System User Record

            var _systemUser2Name = "BehaviourRosteredUser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Behaviour", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Ben";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];
            var careAssistanceNeeded2Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeeded3Id = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("Some")
                .SelectAssistanceAmountFromPicklist("Fair")
                .SelectAssistanceAmountFromPicklist("A Lot")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded2Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Physical Assistance", careAssistanceNeeded3Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("Fair")
                .SelectAssistanceAmountFromPicklist("A Lot")
                .SelectAssistanceAmountFromPicklist("Some");

            #endregion

            #region Step 21

            distressedBehaviourRecordPage
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName);

            distressedBehaviourRecordPage
                .ValidateMandatoryFieldIsVisible("Staff Required")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("BehaviourRosteredUser2").TapSearchButton().AddElementToList(_systemUser2Id)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName)
                .ValidateStaffRequiredSelectedOptionText(_systemUser2Id, "Behaviour Rostered User 2");

            #endregion

            #region Step 22

            distressedBehaviourRecordPage
                .ValidateTotalTimeSpentWithPersonMinutesFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Total Time Spent With Person (Minutes)")
                .InsertTextOnTotalTimesSpentWithPersonMinutes("a")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(true)
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorText("Please enter a value between 1 and 1440.");

            distressedBehaviourRecordPage
                .InsertTextOnTotalTimesSpentWithPersonMinutes("2500")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(true)
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorText("Please enter a value between 1 and 1440.")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(true);

            distressedBehaviourRecordPage
                .InsertTextOnTotalTimesSpentWithPersonMinutes("25")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(false);

            #endregion

            #region Step 23

            distressedBehaviourRecordPage
                .ValidateAdditionalNotesFieldVisible(true)
                .InsertTextOnAdditionalnotes("additional notes ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9103")]
        [Description("Additional step for - ACC-2612" +
            "When Consent Given = yes, fill values in all fields and verify record is saved.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod05()
        {

            #region Create System User Record

            var _systemUser2Name = "BehaviourRosteredUser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Behaviour", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Create System User Record

            var _systemUser3Name = "BehaviourRosteredUser3";
            var _systemUser3Id = commonMethodsDB.CreateSystemUserRecord(_systemUser3Name, "Behaviour", "Rostered User 3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser3Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Ben";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 7, "Person Behaviour or Distressed Behaviour " + _currentDateSuffix); //Distressed Behaviour = 7

            #endregion

            #region Incident Triggers - IncidentTrigger

            var TriggerId1 = dbHelper.incidentTrigger.GetByName("Frightened")[0];
            var TriggerId2 = dbHelper.incidentTrigger.GetByName("Noise")[0];
            var TriggerId3 = dbHelper.incidentTrigger.GetByName("Lack of stimulation")[0];
            var TriggerId4 = dbHelper.incidentTrigger.GetByName("Pain")[0];
            var TriggerId5 = dbHelper.incidentTrigger.GetByName("Tired")[0];
            var TriggerId6 = dbHelper.incidentTrigger.GetByName("Personal care")[0];
            var TriggerId7 = dbHelper.incidentTrigger.GetByName("Hungry")[0];
            var TriggerId8 = dbHelper.incidentTrigger.GetByName("Embarrassment")[0];
            var TriggerId9 = dbHelper.incidentTrigger.GetByName("Thirsty")[0];
            var TriggerId10 = dbHelper.incidentTrigger.GetByName("Communication needs not met")[0];
            var TriggerId11 = dbHelper.incidentTrigger.GetByName("Other")[0];

            #endregion

            #region Option Set

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object")[0];

            #endregion

            #region Option Set Value

            var distressedBehaviourOptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Distressed Behaviour")[0];
            var distressedBehaviourNumericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "numericcode")["numericcode"]);
            var distressedBehaviourText = (string)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "text")["text"]);
            Dictionary<int, string> behaviourSetValueIds = new Dictionary<int, string> { };
            behaviourSetValueIds.Add(distressedBehaviourNumericCode, distressedBehaviourText);

            #endregion

            #region Care Physical Locations 

            var bathroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Bathroom")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            var valid_LocationId = commonMethodsDB.CreateCarePhysicalLocation("Park", new DateTime(2024, 2, 1), _teamId, behaviourSetValueIds);

            if (!dbHelper.carePhysicalLocationDailyCare.GetByCarePhysicalLocationIdAndOptionSetValueId(valid_LocationId, distressedBehaviourNumericCode).Any())
                dbHelper.carePhysicalLocationDailyCare.CreateCarePhysicalLocationDailyCare(valid_LocationId, distressedBehaviourNumericCode);


            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login(_systemUser2Name, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("Yes")
                .InsertTextOnAntecedent("Antecedent9042")
                .InsertTextOnBehaviour("Behaviour9042")
                .InsertTextOnConsequence("Consequence9042")
                .ClickWerethereanytriggers_YesOption()
                .ClickWhatwerethetriggersLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(TriggerId1)
                .AddElementToList(TriggerId2)
                .AddElementToList(TriggerId3)
                .AddElementToList(TriggerId4)
                .AddElementToList(TriggerId5)
                .AddElementToList(TriggerId6)
                .AddElementToList(TriggerId7)
                .AddElementToList(TriggerId8)
                .AddElementToList(TriggerId9)
                .AddElementToList(TriggerId10)
                .AddElementToList(TriggerId11)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTriggersIfOtherTextarea("Other")
                .ClickReviewrequiredbyseniorcolleague_YesOption()
                .InsertReviewDetails("Review details here ...")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bathroom_LocationId)
                .AddElementToList(valid_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOtherTextareaField("other location ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Unhappy", careWellbeing1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("action taken 1 ...")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("A Lot")
                .ValidateMandatoryFieldIsVisible("Staff Required")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser3Name).TapSearchButton().AddElementToList(_systemUser3Id)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimesSpentWithPersonMinutes("25")
                .InsertTextOnAdditionalnotes("additional notes ...")
                .ClickSaveAndCloseButton();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var careProvidedWithConsentRecords = dbHelper.cpPersonBehaviourIncident.GetByPersonId(personId);
            Assert.AreEqual(1, careProvidedWithConsentRecords.Count);
            var personDistressedBehaviourRecordId = careProvidedWithConsentRecords[0];

            distressedBehavioursPage
                .ValidateHeaderCellIsDisplayed("Date and Time Occurred")
                .ValidateHeaderCellIsDisplayed("Consent Given?")
                .ValidateHeaderCellIsDisplayed("Non-consent Detail")
                .ValidateHeaderCellIsDisplayed("Reason for Absence?", false)
                .ValidateHeaderCellIsDisplayed("Reason Consent Declined?", false)
                .ValidateHeaderCellIsDisplayed("Encouragement Given?", false)
                .ValidateHeaderCellIsDisplayed("Care provided without consent?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Date?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Time?", false)
                .ValidateHeaderCellIsDisplayed("Deferred To Shift?", false)
                .ValidateHeaderCellIsDisplayed("Preferences", false)
                .ValidateHeaderCellIsDisplayed("Antecedent")
                .ValidateHeaderCellIsDisplayed("Behaviour")
                .ValidateHeaderCellIsDisplayed("Consequence")
                .ValidateHeaderCellIsDisplayed("Were there any triggers?", false)
                .ValidateHeaderCellIsDisplayed("What were the triggers?", false)
                .ValidateHeaderCellIsDisplayed("Triggers If Other?", false)
                .ValidateHeaderCellIsDisplayed("Review required by Senior Colleague?", false)
                .ValidateHeaderCellIsDisplayed("Review details", false)
                .ValidateHeaderCellIsDisplayed("Location", false)
                .ValidateHeaderCellIsDisplayed("Location If Other?", false)
                .ValidateHeaderCellIsDisplayed("Wellbeing", false)
                .ValidateHeaderCellIsDisplayed("Action Taken? (Has Pain Relief been offered?)", false)
                .ValidateHeaderCellIsDisplayed("Assistance Needed?", false)
                .ValidateHeaderCellIsDisplayed("Assistance Amount?", false)
                .ValidateHeaderCellIsDisplayed("Staff Required", false)
                .ValidateHeaderCellIsDisplayed("Total Time Spent With Person (Minutes)", false)
                .ValidateHeaderCellIsDisplayed("Additional notes", false)
                .OpenRecord(personDistressedBehaviourRecordId); 

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(personFullName)
                .ValidateResponsibleTeamLinkText("DB T1")
                .ValidatePreferencesText("Person Behaviour or Distressed Behaviour " + _currentDateSuffix)
                .ValidateOccurredText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateOccurred_TimeText("07:00")
                .ValidateConsentGivenSelectedText("Yes");

            distressedBehaviourRecordPage
                .ValidateAntecedentText("Antecedent9042")
                .ValidateBehaviourText("Behaviour9042")
                .ValidateConsequenceText("Consequence9042")
                .ValidateWerethereanytriggers_YesOptionChecked()
                .ValidateWerethereanytriggers_NoOptionNotChecked()
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId1, "Frightened")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId2, "Noise")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId3, "Lack of stimulation")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId4, "Pain")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId5, "Tired")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId6, "Personal care")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId7, "Hungry")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId8, "Embarrassment")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId9, "Thirsty")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId10, "Communication needs not met")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId11, "Other")
                .ValidateTriggersIfOtherTextareaText("Other")
                .ValidateReviewrequiredbyseniorcolleague_YesOptionChecked()
                .ValidateReviewrequiredbyseniorcolleague_NoOptionNotChecked()
                .ValidateReviewdetailsText("Review details here ...");

            distressedBehaviourRecordPage
                .ValidateLocation_SelectedElementLinkText(valid_LocationId, "Park")
                .ValidateLocation_SelectedElementLinkText(bathroom_LocationId, "Bathroom")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherTextareaFieldText("other location ...")

                .ValidateWellbeingLinkText("Unhappy")
                .VerifyActionTaken_HasPainReliefBeenOfferedText("action taken 1 ...")

                .ValidateAssistanceneededLinkText("Asked For Help")
                .ValidateAssistanceAmountPicklistSelectedText("A Lot")
                .ValidateStaffRequiredSelectedOptionText(_systemUser3Id, "Behaviour Rostered User 3")
                .ValidateStaffRequiredSelectedOptionText(_systemUser2Id, "Behaviour Rostered User 2")

                .ValidateTotalTimeSpentWithPersonMinutesText("25")
                .ValidateAdditionalnotesText("additional notes ...");

            distressedBehaviourRecordPage
                .ValidateCarenoteText("The antecedent to the behaviour was: Antecedent9042.\r\n" +
                "The behaviour was: Behaviour9042.\r\n" +
                "The consequence of the behaviour was: Consequence9042.\r\n" +
                "The triggers were: Frightened, Noise, Lack of stimulation, Pain, Tired, Personal care, Hungry, Embarrassment, Thirsty, Communication needs not met and Other.\r\n" +
                "Ben was in the Bathroom, Park and other location....\r\n" +
                "Ben came across as Unhappy.\r\n" +
                "The action taken was: action taken 1....\r\n" +
                "Ben required assistance: Asked For Help. Amount given: A Lot.\r\n" +
                "This care was given at " + dateandTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Ben was assisted by 2 colleague(s).\r\n" +
                "Overall, I spent 25 minutes with Ben.\r\n" +
                "We would like to note that: additional notes....");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9112

        [TestProperty("JiraIssueID", "ACC-2700")]
        [Description("Step 1 from the original test - ACC-2700")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2700_UITestMethod01()
        {

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ValidateHeaderCellText(2, "Date and Time Occurred")
                .ValidateHeaderCellText(3, "Consent Given?")
                .ValidateHeaderCellText(4, "Non-consent Detail");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2702")]
        [Description("Step 1 from the original test - ACC-2702")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Business Objects")]
        [TestProperty("Screen2", "Reference Data")]
        public void DailyCare_ACC2702_UITestMethod01()
        {
            #region Create System User Record

            var _systemUser2Name = "BehaviourRosteredUser4";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Behaviour", "Rostered User 4", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Reference Data

            var IncidentTriggerBoId = dbHelper.businessObject.GetBusinessObjectByName("IncidentTrigger")[0];

            #endregion

            #region Incident Triggers - IncidentTrigger

            var TriggerId1 = dbHelper.incidentTrigger.GetByName("Frightened")[0];
            var TriggerId2 = dbHelper.incidentTrigger.GetByName("Noise")[0];
            var TriggerId3 = dbHelper.incidentTrigger.GetByName("Lack of stimulation")[0];
            var TriggerId4 = dbHelper.incidentTrigger.GetByName("Pain")[0];
            var TriggerId5 = dbHelper.incidentTrigger.GetByName("Tired")[0];
            var TriggerId6 = dbHelper.incidentTrigger.GetByName("Personal care")[0];
            var TriggerId7 = dbHelper.incidentTrigger.GetByName("Hungry")[0];
            var TriggerId8 = dbHelper.incidentTrigger.GetByName("Embarrassment")[0];
            var TriggerId9 = dbHelper.incidentTrigger.GetByName("Thirsty")[0];
            var TriggerId10 = dbHelper.incidentTrigger.GetByName("Communication needs not met")[0];
            var TriggerId11 = dbHelper.incidentTrigger.GetByName("Other")[0];

            #endregion

            #region Behaviour Option Set

            var OptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object").FirstOrDefault();
            var OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(OptionSetId, "Distressed Behaviour").FirstOrDefault();

            #endregion

            #region Care Tasks

            var BehaviourCareTaskId = dbHelper.careTask.GetByName("Distressed Behaviour").First();

            #endregion

            #region Step 1a - Access BO 

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("IncidentTrigger")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .ValidateRecordIsPresent(IncidentTriggerBoId.ToString(), true);

            businessObjectsPage
                .OpenRecord(IncidentTriggerBoId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateOwnershipFieldVisible("Team Based")
                .ValidateBusinessModuleFieldValue("Care Provider Care Plan");

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
                .InsertQuickSearchText("Distressed Behaviour") //Behaviour Incidents = Distressed Behaviour
                .ClickQuickSearchButton()
                .WaitForOptionsetValuesPageToLoad()
                .ValidateOptionSetRecordIsAvailable(OptionSetValueId.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

            #region Step 1b  - Access Reference Data using Rostered User with access to Reference Data

            loginPage
               .GoToLoginPage()
               .Login(_systemUser2Name, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Incident")
                .TapSearchButton()
                .ValidateReferenceDataMainHeaderVisibility("Care Provider Care Plan", true)
                .ClickReferenceDataMainHeader("Care Provider Care Plan")
                .ValidateReferenceDataElementVisibility("Incident Triggers", true);

            referenceDataPage
                .ClickReferenceDataElement("Incident Triggers");

            incidentTriggersPage
                .WaitForIncidentTriggersPageToLoad()
                .ValidateRecordPresentOrNot(TriggerId1.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId2.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId3.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId4.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId5.ToString(), true)              
                .ValidateRecordPresentOrNot(TriggerId6.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId7.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId8.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId9.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId10.ToString(), true)
                .ValidateRecordPresentOrNot(TriggerId11.ToString(), true);

            #endregion

            #region Step 3

            /*
             * In current UI, Include for Residential Care field is now Available for Care Scheduling
             */

            mainMenu
                .WaitForMainMenuToLoad()
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
                .InsertSearchQuery("Distressed Behaviour")
                .TapSearchButton()
                .WaitForCareTasksPageToLoad()
                .ValidateRecordPresentOrNot(BehaviourCareTaskId.ToString(), true)
                .ValidateRecordCellContent(BehaviourCareTaskId.ToString(), 3, "Yes")
                .ValidateRecordCellContent(BehaviourCareTaskId.ToString(), 5, "Distressed Behaviour");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-2791")]
        [Description("Step 1 from the original test - ACC-2791")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]        
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2791_UITestMethod01()
        {
            #region Create System User Record

            var _systemUser2Name = "BehaviourRosteredUser4";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Behaviour", "Rostered User 4", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUser2Name, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ValidateSectionName("General", 1)
                .ValidateSectionName("Incident Details", 2)
                .InsertTextOnAntecedent("Antecedent9112")
                .InsertTextOnBehaviour("Behaviour9112")
                .InsertTextOnConsequence("Consequence9112");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9137")]
        [Description("Additional step for - ACC-2612" +
            "When Consent Given = No, Non consent reason = declined, Care provided without consent? = Yes, " +
            "fill values in all fields and verify record is saved.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Distressed Behaviour")]
        public void DailyCare_ACC2612_UITestMethod06()
        {

            #region Create System User Record

            var _systemUser2Name = "BehaviourRosteredUser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Behaviour", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Create System User Record

            var _systemUser3Name = "BehaviourRosteredUser3";
            var _systemUser3Id = commonMethodsDB.CreateSystemUserRecord(_systemUser3Name, "Behaviour", "Rostered User 3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser2Id, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUser3Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 7, "Person Behaviour or Distressed Behaviour " + _currentDateSuffix); //Distressed Behaviour = 7

            #endregion

            #region Incident Triggers - IncidentTrigger

            var TriggerId1 = dbHelper.incidentTrigger.GetByName("Communication needs not met")[0];
            var TriggerId2 = dbHelper.incidentTrigger.GetByName("Other")[0];

            #endregion

            #region Option Set

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object")[0];

            #endregion

            #region Option Set Value

            var distressedBehaviourOptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Distressed Behaviour")[0];
            var distressedBehaviourNumericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "numericcode")["numericcode"]);
            var distressedBehaviourText = (string)(dbHelper.optionsetValue.GetOptionsetValueByID(distressedBehaviourOptionSetValueId, "text")["text"]);
            Dictionary<int, string> behaviourSetValueIds = new Dictionary<int, string> { };
            behaviourSetValueIds.Add(distressedBehaviourNumericCode, distressedBehaviourText);

            #endregion

            #region Care Physical Locations 

            var bedroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            var valid_LocationId = commonMethodsDB.CreateCarePhysicalLocation("Park", new DateTime(2024, 2, 1), _teamId, behaviourSetValueIds);

            if (!dbHelper.carePhysicalLocationDailyCare.GetByCarePhysicalLocationIdAndOptionSetValueId(valid_LocationId, distressedBehaviourNumericCode).Any())
                dbHelper.carePhysicalLocationDailyCare.CreateCarePhysicalLocationDailyCare(valid_LocationId, distressedBehaviourNumericCode);


            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login(_systemUser2Name, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDistressedBehaviourPage();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .ValidateConsentGivenFieldVisible(true)
                .ValidateMandatoryFieldIsVisible("Consent Given?")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextInReasonConsentDeclined("Reason for decline ...")
                .InsertTextInEncouragementGiven("Encouragement given ...")
                .ClickCareProvidedWithoutConsent_YesRadioButton()
                .InsertTextOnAntecedent("Antecedent9112")
                .InsertTextOnBehaviour("Behaviour9112")
                .InsertTextOnConsequence("Consequence9112")
                .ClickWerethereanytriggers_YesOption()
                .ClickWhatwerethetriggersLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(TriggerId1)
                .AddElementToList(TriggerId2)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTriggersIfOtherTextarea("Other")
                .ClickReviewrequiredbyseniorcolleague_YesOption()
                .InsertReviewDetails("Review details here ...")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bedroom_LocationId)
                .AddElementToList(valid_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOtherTextareaField("other location ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("action taken 1 ...")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Physical Assistance", careAssistanceNeeded1Id);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("Fair")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser3Name).TapSearchButton().AddElementToList(_systemUser3Id)
                .TapOKButton();

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTotalTimesSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalnotes("additional notes ...")
                .ClickSaveAndCloseButton();

            distressedBehavioursPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var careProvidedWithoutConsentRecords = dbHelper.cpPersonBehaviourIncident.GetByPersonId(personId);
            Assert.AreEqual(1, careProvidedWithoutConsentRecords.Count);
            var personDistressedBehaviourRecordId = careProvidedWithoutConsentRecords[0];

            distressedBehavioursPage
                .OpenRecord(personDistressedBehaviourRecordId);

            distressedBehaviourRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(personFullName)
                .ValidateResponsibleTeamLinkText("DB T1")
                .ValidatePreferencesText("Person Behaviour or Distressed Behaviour " + _currentDateSuffix)
                .ValidateOccurredText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateOccurred_TimeText("07:00")
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclined("Reason for decline ...")
                .ValidateEncouragementGiven("Encouragement given ...")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
                .ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked();

            distressedBehaviourRecordPage
                .ValidateAntecedentText("Antecedent9112")
                .ValidateBehaviourText("Behaviour9112")
                .ValidateConsequenceText("Consequence9112")
                .ValidateWerethereanytriggers_YesOptionChecked()
                .ValidateWerethereanytriggers_NoOptionNotChecked()
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId1, "Communication needs not met")
                .ValidateWhatWereTheTriggers_SelectedElementLinkText(TriggerId2, "Other")
                .ValidateTriggersIfOtherTextareaText("Other")
                .ValidateReviewrequiredbyseniorcolleague_YesOptionChecked()
                .ValidateReviewrequiredbyseniorcolleague_NoOptionNotChecked()
                .ValidateReviewdetailsText("Review details here ...");

            distressedBehaviourRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_LocationId, "Bedroom")
                .ValidateLocation_SelectedElementLinkText(valid_LocationId, "Park")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherTextareaFieldText("other location ...")

                .ValidateWellbeingLinkText("OK")
                .VerifyActionTaken_HasPainReliefBeenOfferedText("action taken 1 ...")

                .ValidateAssistanceneededLinkText("Physical Assistance")
                .ValidateAssistanceAmountPicklistSelectedText("Fair")
                .ValidateStaffRequiredSelectedOptionText(_systemUser3Id, "Behaviour Rostered User 3")
                .ValidateStaffRequiredSelectedOptionText(_systemUser2Id, "Behaviour Rostered User 2")

                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateAdditionalnotesText("additional notes ...");

            distressedBehaviourRecordPage
                .ValidateCarenoteText("The antecedent to the behaviour was: Antecedent9112.\r\n" +
                "The behaviour was: Behaviour9112.\r\n" +
                "The consequence of the behaviour was: Consequence9112.\r\n" +
                "The triggers were: Communication needs not met and Other.\r\n" +
                "Erik was in the Bedroom, Park and other location....\r\n" +
                "Erik came across as OK.\r\n" +
                "The action taken was: action taken 1....\r\n" +
                "Erik required assistance: Physical Assistance. Amount given: Fair.\r\n" +
                "This care was given at " + dateandTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Erik was assisted by 2 colleague(s).\r\n" +
                "Overall, I spent 30 minutes with Erik.\r\n" +
                "We would like to note that: additional notes....");

            #endregion

        }

        #endregion

    }

}














using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonMobility : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PM BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PM T1", null, _businessUnitId, "907678", "PersonMobilityT1@careworkstempmail.com", "Person Mobility T1", "020 123456");
                _defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true).First();

                #endregion

                #region Create SystemUser Record

                _systemUserName = "pmuser1";
                _systemUserFullName = "Person Mobility User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Mobility", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8185

        [TestProperty("JiraIssueID", "ACC-8324")]
        [Description("Step(s) 1 to 2 from the original test - ACC-2723")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC2446_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Boris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Period

            var careprovidercareperiodsetupId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_teamId, "9 AM", new TimeSpan(9, 0, 0));

            #endregion

            #region Step 1 & 2

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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var deferredToDate = DateTime.Now.AddDays(2);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .SetDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .SetDeferredToTime("10:30")
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton();

            var mobilityRecords = dbHelper.cPPersonMobility.GetByPersonId(personId);
            Assert.AreEqual(1, mobilityRecords.Count);
            var personMobilityId = mobilityRecords[0];

            personMobilityPage
                .OpenRecord(personMobilityId);

            personMobilityRecordPage
                .WaitForPageToLoad()

                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Time")
                .ValidateDeferredToTime("10:30")

                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("9 AM", careprovidercareperiodsetupId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(personMobilityId);

            personMobilityRecordPage
                .WaitForPageToLoad()

                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("9 AM")

                .ClickDeferredToShiftClearButton()
                .ValidateDeferredToShiftErrorLabel("Please fill out this field.")

                .SelectDeferredToTimeOrShift("")
                .ValidateDeferredToTimeOrShiftErrorLabel("Please fill out this field.")

                .SetDeferredToDate("")
                .ValidateDeferredToDateErrorLabel("Please fill out this field.")

                .SetDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .SetDeferredToTime("")
                .ValidateDeferredToTimeErrorLabel("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8325")]
        [Description("Step(s) 3 to 6 from the original test - ACC-2723")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC2446_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Boris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Option Set

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object")[0];

            #endregion

            #region Option Set Value

            var foodAndFluidOptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Food and Fluid")[0];
            var foodAndFluidNumericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(foodAndFluidOptionSetValueId, "numericcode")["numericcode"]);


            #endregion

            #region Care Physical Location

            List<int> optionSetValueIds = new List<int> { foodAndFluidNumericCode };
            var carePhysicalLocation1Id = commonMethodsDB.CreateCarePhysicalLocation("CPL ACC-2723", new DateTime(2020, 1, 1), _teamId, optionSetValueIds); //Care Physical Location only valid for Food and Fluid records 

            var carePhysicalLocation2Id = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var carePhysicalLocation3Id = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Step 3

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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var deferredToDate = DateTime.Now.AddDays(2);

            personMobilityRecordPage
                .WaitForPageToLoad()

                .SelectConsentGiven("Yes")
                .ClickSaveAndCloseButton()
                .ValidateMobilisedFromErrorLabel("Please fill out this field.");

            personMobilityRecordPage
                .ClickMobilisedFromLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPL ACC-2723").TapSearchButton().ValidateResultElementNotPresent(carePhysicalLocation1Id) //Care Physical Location only valid for Food and Fluid records 
                .SearchAndSelectRecord("Bedroom", carePhysicalLocation2Id); //this one is valid for Mobility records

            personMobilityRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 4

            personMobilityRecordPage
                .ValidateMobilisedFromOtherFieldVisible(false)
                .ClickMobilisedFromLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Other", carePhysicalLocation3Id);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidateMobilisedFromOtherFieldVisible(true)
                .ClickSaveAndCloseButton() //on saving should trigger the error label to be visible
                .ValidateMobilisedFromOtherErrorLabel("Please fill out this field.")
                .InsertMobilisedFromOther("Hall");

            #endregion

            #region Step 5

            personMobilityRecordPage
                .ValidateMobilisedToErrorLabel("Please fill out this field.")
                .ClickMobilisedToLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPL ACC-2723").TapSearchButton().ValidateResultElementNotPresent(carePhysicalLocation1Id) //Care Physical Location only valid for Food and Fluid records 
                .SearchAndSelectRecord("Bedroom", carePhysicalLocation2Id); //this one is valid for Mobility records

            personMobilityRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 6

            personMobilityRecordPage
                .ValidateMobilisedToOtherFieldVisible(false)
                .ClickMobilisedToLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Other", carePhysicalLocation3Id);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidateMobilisedToOtherFieldVisible(true)
                .ClickSaveAndCloseButton()
                .ValidateMobilisedToOtherErrorLabel("Please fill out this field.")
                .InsertMobilisedToOther("Other Hall");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-8326")]
        [Description("Step(s) 7 to 8 from the original test - ACC-2723")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC2446_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Boris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 7

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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var deferredToDate = DateTime.Now.AddDays(2);

            personMobilityRecordPage
                .WaitForPageToLoad()

                .SelectConsentGiven("Yes")
                .ClickSaveAndCloseButton()
                .ValidateApproximateDiatanceErrorLabel("Please fill out this field.")
                .SetApproximateDistance("a")
                .ValidateApproximateDiatanceErrorLabel("Please enter a value between 0 and 7.922816251426434e+28.")
                .SetApproximateDistance("-1")
                .ValidateApproximateDiatanceErrorLabel("Please enter a value between 0 and 7.922816251426434e+28.")
                .SetApproximateDistance("0")
                .ValidateApproximateDiatanceErrorLabel("")
                .SetApproximateDistance("1000")
                .ValidateApproximateDiatanceErrorLabel("");

            #endregion

            #region Step 8

            personMobilityRecordPage

                .ValidateApproximateDiatanceUnitLinkFieldText("Metres") //Metres seems to be selected by default
                .ValidateApproximateDiatanceUnitErrorLabel("")

                .ClickApproximateDistanceUnitClearButton()
                .ValidateApproximateDiatanceUnitErrorLabel("Please fill out this field.");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9327

        [TestProperty("JiraIssueID", "ACC-9175")]
        [Description("Save record with empty mandatory fields (Consent Given not inserted)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod01()
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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personMobilityRecordPage
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
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod02()
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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personMobilityRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ValidateNonConsentDetailErrorLabelVisibility(false)
                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .SelectConsentGiven("No")
                .ClickSaveButton();

            personMobilityRecordPage
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

            personMobilityRecordPage
                .SelectNonConsentDetail("Absent")
                .ClickSaveAndCloseButton();

            personMobilityRecordPage
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

            personMobilityRecordPage
                .SelectNonConsentDetail("Declined")
                .ClickSaveAndCloseButton();

            personMobilityRecordPage
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

            personMobilityRecordPage
                .SelectNonConsentDetail("Deferred")
                .ClickSaveAndCloseButton();

            personMobilityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabel("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(true)
                .ValidateDeferredToTimeOrShiftErrorLabel("Please fill out this field.")

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            personMobilityRecordPage
                .SelectDeferredToTimeOrShift("Time")
                .ClickSaveAndCloseButton();

            personMobilityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabel("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(true)
                .ValidateDeferredToTimeErrorLabel("Please fill out this field.")

                .ValidateDeferredToShiftErrorLabelVisibility(false);

            personMobilityRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickSaveAndCloseButton();

            personMobilityRecordPage
                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(false)

                .ValidateNonConsentDetailErrorLabelVisibility(false)

                .ValidateReasonForAbsenceErrorLabelVisibility(false)

                .ValidateReasonConsentDeclinedErrorLabelVisibility(false)

                .ValidateEncouragementGivenErrorLabelVisibility(false)

                .ValidateDeferredToDateErrorLabelVisibility(true)
                .ValidateDeferredToDateErrorLabel("Please fill out this field.")

                .ValidateDeferredToTimeOrShiftErrorLabelVisibility(false)

                .ValidateDeferredToTimeErrorLabelVisibility(false)

                .ValidateDeferredToShiftErrorLabelVisibility(true)
                .ValidateDeferredToShiftErrorLabel("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9177")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Absent)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod03()
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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Went to the hospital")
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonMobility = dbHelper.cPPersonMobility.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonMobility.Count);
            var cPPersonMobilitySupportId = allPersonMobility[0];

            personMobilityPage
                .OpenRecord(cPPersonMobilitySupportId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Esmond.")
                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Absent")
                .ValidateReasonForAbsenceText("Went to the hospital")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9178")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Declined)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod04()
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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of Person Mobility")
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonMobility = dbHelper.cPPersonMobility.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonMobility.Count);
            var cPPersonMobilitySupportId = allPersonMobility[0];

            personMobilityPage
                .OpenRecord(cPPersonMobilitySupportId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Esmond.")
                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of Person Mobility")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9179")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Deferred)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod05()
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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);
            var deferredToDate = DateTime.Now.AddDays(2);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .SetDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .SetDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonMobility = dbHelper.cPPersonMobility.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonMobility.Count);
            var cPPersonMobilitySupportId = allPersonMobility[0];

            personMobilityPage
                .OpenRecord(cPPersonMobilitySupportId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Esmond.")
                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Time")
                .ValidateDeferredToTime("08:45");

            personMobilityRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(cPPersonMobilitySupportId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Esmond.")
                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9178")]
        [Description("Set data in all fields and save the record (Consent Given = No & Non-consent Detail = Declined & Care provided without consent = Yes)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Mobility")]
        public void PersonMobility_ACC1770_UITestMethod06()
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

            #region Care Physical Location

            var carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region CareEquipment

            var _careEquipment1Id = dbHelper.careEquipment.GetByName("Walking Frame")[0];
            var _careEquipment2Id = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion

            #region CareAssistance Needed

            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("OK")[0];

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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            /*General section*/
            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(dateOccurred.ToString("dd/MM/yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of Person Mobility")
                .ClickCareProvidedWithoutConsent_YesRadioButton();

            /*Details section*/
            personMobilityRecordPage
                .ClickMobilisedFromLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Other", carePhysicalLocationId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickMobilisedToLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Other", carePhysicalLocationId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertMobilisedFromOther("Garden")
                .InsertMobilisedToOther("Kitchen")
                .SetApproximateDistance("25");

            /*Additional Information section*/
            personMobilityRecordPage
                .ClickEquipmentLookUpBtn();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Walking Frame").TapSearchButton().AddElementToList(_careEquipment1Id)
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(_careEquipment2Id)
                .TapOKButton();

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookUpBtn();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", _careWellBeingdId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookUpBtn();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", _careAssistanceNeededId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEquipmentIfOther("Special cane")
                .InsertTextOnActionTaken("Gave some pain pills")
                .SetTotalTimeSpentWithClient("25")
                .InsertTextInAdditionalNotesTextArea("He did not wanted to be mobilised")
                .SelectAssistanceAmount("Some");

            /*Care Note section*/
            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidateTextInCareNoteTextArea("Esmond moved from the Garden to the Kitchen, approximately 25 Metres.\r\nEsmond used the following equipment: Walking Frame and Special cane.\r\nEsmond came across as OK.\r\nThe action taken was: Gave some pain pills.\r\nEsmond required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateOccurred.ToString("dd/MM/yyyy") + " 08:45:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall, I spent 25 minutes with Esmond.\r\nWe would like to note that: He did not wanted to be mobilised.")
                .ClickSaveAndCloseButton();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonMobility = dbHelper.cPPersonMobility.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonMobility.Count);
            var cPPersonMobilitySupportId = allPersonMobility[0];

            personMobilityPage
                .OpenRecord(cPPersonMobilitySupportId);

            /*General section*/
            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurredText(dateOccurred.ToString("dd/MM/yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Esmond.")
                .ValidateSelectedConsentGiven("No")
                .ValidateSelectedNonConsentDetail("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of Person Mobility")
                .ValidateCareProvidedWithoutConsent_YesRadioButtonChecked();

            /*Details section*/
            personMobilityRecordPage
                .ValidateMobilisedFromLinkText("Other")
                .ValidateMobilisedFromOther("Garden")
                .ValidateMobilisedToLinkText("Other")
                .ValidateMobilisedToOther("Kitchen")
                .ValidateApproximateDistance("25.00")
                .ValidateApproximateDiatanceUnitLinkFieldText("Metres");

            /*Additional Information section*/
            personMobilityRecordPage
                .ValidateEquipment_SelectedElementLinkText(_careEquipment1Id, "Walking Frame")
                .ValidateEquipment_SelectedElementLinkText(_careEquipment2Id, "Other")
                .ValidateEquipmentIfOtherText("Special cane")
                .ValidateWellbeingLinkText("OK")
                .InsertTextOnActionTaken("Gave some pain pills")
                .SetTotalTimeSpentWithClient("25")
                .InsertTextInAdditionalNotesTextArea("He did not wanted to be mobilised")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateSelectedAssistanceAmount("Some");

            /*Care Note section*/
            personMobilityRecordPage
                .ValidateTextInCareNoteTextArea("Esmond moved from the Garden to the Kitchen, approximately 25 Metres.\r\nEsmond used the following equipment: Walking Frame and Special cane.\r\nEsmond came across as OK.\r\nThe action taken was: Gave some pain pills.\r\nEsmond required assistance: Asked For Help. Amount given: Some.\r\nThis care was given at " + dateOccurred.ToString("dd/MM/yyyy") + " 08:45:00.\r\nEsmond was assisted by 1 colleague(s).\r\nOverall, I spent 25 minutes with Esmond.\r\nWe would like to note that: He did not wanted to be mobilised.");

            #endregion

        }

        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }

}














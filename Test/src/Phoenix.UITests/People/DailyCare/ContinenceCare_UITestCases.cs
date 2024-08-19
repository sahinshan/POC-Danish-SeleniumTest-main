using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class ContinenceCare : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DPC BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DPC T1", null, _businessUnitId, "907678", "ContinenceCareT1@careworkstempmail.com", "ContinenceCare T1", "020 123456");

                #endregion

                #region Create System User Record

                _systemUserName = "DPCUser1";
                _systemUserFullName = "Daily Personal Care User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8184

        [TestProperty("JiraIssueID", "ACC-3853")]
        [Description("Step(s) 1 to 9 from the original test - ACC-3853")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC3853_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Moris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Urine Colour

            var urineColourId = dbHelper.urineColour.GetByName("Amber")[0];

            #endregion

            #region Skin Condition

            var skinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Dry Skin")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Stoma")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Moris.")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            #region Care Preferences

            var dailycarerecordid = 6; //Continence Care
            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference 1\r\nPreference 2\r\nPreference 3");

            #endregion

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("Preference 1\r\nPreference 2\r\nPreference 3");

            #endregion

            #region Step 5 to 9

            continenceCareRecordPage
                .SelectConsentGivenPicklistValueByText("Yes")
                .SetDateOccurred("01/06/2024")
                .SetTimeOccurred("09:00");

            continenceCareRecordPage
                .ClickDoesThePersonNeedCatheterCare_YesRadioButton()
                .ClickIsTheCatheterPatentAndDraining_YesRadioButton()
                .ClickHasTheCatheterBagBeenEmptied_YesRadioButton()
                .ClickPassedUrine_YesRadioButton()
                .InsertTextOnUrineOutputAmount("12")
                .ClickDescribeUrineColourLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Amber", urineColourId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickIsTheCatheterPositionedSecured_YesRadioButton()
                .ClickHaveYouCleanedTheCatheterArea_YesRadioButton()
                .ClickIsThereAnyMalodour_YesRadioButton();

            continenceCareRecordPage
                .ClickBowelsOpened_YesRadioButton()
                .SelectStoolType("Type 4 - Like a sausage or snake, smooth and soft")
                .SelectStoolAmount("Medium")
                .ClickMucusPresent_YesRadioButton()
                .ClickBloodPresent_YesRadioButton();

            continenceCareRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
                .InsertTextOnWhereOnTheBody("Neck")
                .ClickDescribeSkinConditionLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Dry Skin").TapSearchButton().SelectResultElement(skinConditionId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SelectHasContinencePadBeenChanged("Yes")
                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .InsertTextOnReviewDetails("details here ...");

            continenceCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(bedroom_CarePhysicalLocationId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("action take info ...")
                .InsertTotalTimeSpentWithClientMinutes("30")
                .InsertTextOnAdditionalNotes("extra notes ...")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Stoma").TapSearchButton().SelectResultElement(equipment1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            continenceCareRecordPage
            .WaitForPageToLoad()
                .VerifyConsentGivenPicklistSelectedValue("Yes")
                .VerifyDateAndTimeOccurredDateFieldText("01/06/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("09:00");

            continenceCareRecordPage
                .ValidateDoesThePersonNeedCatheterCare_YesRadioButtonChecked()
                .ValidateIsTheCatheterPatentAndDraining_YesRadioButtonChecked()
                .ValidateHasTheCatheterBagBeenEmptied_YesRadioButtonChecked()
                .ValidatePassedUrine_YesRadioButtonChecked()
                .ValidateUrineOutputAmountText("12")
                .ValidateDescribeUrineColourLinkText("Amber")
                .ValidateIsTheCatheterPositionedSecured_YesRadioButtonChecked()
                .ValidateHaveYouCleanedTheCatheterArea_YesRadioButtonChecked()
                .ValidateIsThereAnyMalodour_YesRadioButtonChecked();

            continenceCareRecordPage
                .ValidateBowelsOpened_YesRadioButtonChecked()
                .ValidateStoolTypeSelectedText("Type 4 - Like a sausage or snake, smooth and soft")
                .ValidateStoolAmountSelectedText("Medium")
                .ValidateMucusPresent_YesRadioButtonChecked()
                .ValidateBloodPresent_YesRadioButtonChecked();

            continenceCareRecordPage
                .ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonChecked()
                .ValidateWhereOnTheBodyText("Neck")
                .ValidateDescribeSkinCondition_SelectedElementLinkText(skinConditionId, "Dry Skin")
                .ValidateHasContinencePadBeenChangedSelectedText("Yes")
                .ValidateReviewRequiredBySeniorColleague_YesRadioButtonChecked()
                .ValidateReviewDetailsText("details here ...");

            continenceCareRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_CarePhysicalLocationId, "Bedroom")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("action take info ...")
                .VerifyTotalTimeSpentWithClientMinutesFieldText("30")
                .ValidateAdditionalNotesText("extra notes ...")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Stoma")
                .ValidateAssistanceNeededLinkText("Independent");

            continenceCareRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8750

        [TestProperty("JiraIssueID", "ACC-3854")]
        [Description("Step(s) 1 to 15 from the original test - ACC-3854")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC3854_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Moris";
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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Absent")
                .VerifyNonConsentDetailSelectedValue("Absent")
                .SelectNonConsentDetailValueByText("Declined")
                .VerifyNonConsentDetailSelectedValue("Declined")
                .SelectNonConsentDetailValueByText("Deferred")
                .VerifyNonConsentDetailSelectedValue("Deferred");

            #endregion

            #region Step 6, 14

            continenceCareRecordPage
                .SetDateOccurred("01/04/2024")
                .SetTimeOccurred("09:00");

            continenceCareRecordPage
                .ValidateReasonForAbsenceTextareaFieldVisible(false)
                .ValidateReasonConsentDeclinedTextareaFieldFieldVisible(false)
                .ValidateEncouragementGivenFieldVisible(false)
                .ValidateCareProvidedWithoutConsentOptionsVisible(false)
                .ValidateDeferredToDateFieldVisible(true)
                .VerifyDeferredToDate_DatePickerIsDisplayed(true)
                .ValidateDeferredToTimeOrShiftFieldVisible(true)
                .SelectDeferredToTimeOrShift("Time")
                .ValidateDeferredToTimeFieldVisible(true)
                .VerifyDeferredToTime_TimePickerIsDisplayed(true)
                .ValidateDeferredToShiftLookupButtonVisible(false)
                .SelectDeferredToTimeOrShift("Shift")
                .ValidateDeferredToTimeFieldVisible(false)
                .VerifyDeferredToTime_TimePickerIsDisplayed(false)
                .ValidateDeferredToShiftLookupButtonVisible(true);

            continenceCareRecordPage
                .ClickDeferredToDate_DatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1));

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SelectDeferredToTimeOrShift("Time")
                .ClickDeferredToTime_TimePicker();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour("15")
                .SelectMinute("00");

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            continenceCareRecordPage
            .WaitForPageToLoad()
                .VerifyConsentGivenPicklistSelectedValue("No")
                .VerifyDateAndTimeOccurredDateFieldText("01/04/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("09:00")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Moris.")
                .VerifyNonConsentDetailSelectedValue("Deferred")
                .ValidateDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Time")
                .ValidateDeferredToTime("15:00");

            var CareProviderCarePeriodSetupId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_teamId, "Morning", new TimeSpan(6, 0, 0));

            #region Deferred to Shift

            continenceCareRecordPage
                .ClickBackButton();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred("01/05/2024")
                .SetTimeOccurred("10:00")
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Deferred")
                .SetDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Morning", CareProviderCarePeriodSetupId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(2, continenceRecords.Count);
            var continenceId2 = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId2);

            continenceCareRecordPage
            .WaitForPageToLoad()
                .VerifyConsentGivenPicklistSelectedValue("No")
                .VerifyDateAndTimeOccurredDateFieldText("01/05/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("10:00")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Moris.")
                .VerifyNonConsentDetailSelectedValue("Deferred")
                .ValidateDeferredToDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("Morning");

            #endregion

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8754

        [TestProperty("JiraIssueID", "ACC-8760")]
        [Description("Step(s) 1 to 9 from the original test - ACC-3855")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC3855_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Moris";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Urine Colour

            var urineColourId = dbHelper.urineColour.GetByName("Amber")[0];

            #endregion

            #region Skin Condition

            var skinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Dry Skin")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Stoma")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Moris.");

            #endregion

            #region Step 6 to 9

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred("01/05/2024")
                .SetTimeOccurred("10:00")
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Declined")
                .InsertTextInReasonConsentDeclinedTextareaField("Reason for Declined ...")
                .InsertTextInEncouragementGivenTextareaField("Encouragement given ...")
                .SelectCareProvidedWithoutConsent_YesOption()

                .ClickDoesThePersonNeedCatheterCare_NoRadioButton()
                .ValidateIsTheCatheterPositionedSecuredOptionsVisible(false)
                .ValidateIsTheCatheterPatentAndDrainingOptionsVisible(false)
                .ValidateHasTheCatheterBagBeenEmptiedOptionsVisible(false)
                .ValidateHaveYouCleanedTheCatheterAreaOptionsVisible(false)
                .ValidateIsThereAnyMalodourOptionsVisible(false)

                .ClickDoesThePersonNeedCatheterCare_YesRadioButton()
                .ValidateIsTheCatheterPositionedSecuredOptionsVisible(true)
                .ValidateIsTheCatheterPatentAndDrainingOptionsVisible(true)
                .ValidateHasTheCatheterBagBeenEmptiedOptionsVisible(true)
                .ValidateHaveYouCleanedTheCatheterAreaOptionsVisible(true)
                .ValidateIsThereAnyMalodourOptionsVisible(true)

                .ClickIsTheCatheterPositionedSecured_YesRadioButton()
                .ClickIsTheCatheterPatentAndDraining_YesRadioButton()
                .ClickHaveYouCleanedTheCatheterArea_YesRadioButton()
                .ClickHasTheCatheterBagBeenEmptied_YesRadioButton()
                .ClickIsThereAnyMalodour_NoRadioButton()


                .ClickPassedUrine_NoRadioButton()
                .ValidateUrineOutputAmountFieldVisible(false)
                .ValidateDescribeUrineColourLookupButtonVisible(false)

                .ClickPassedUrine_YesRadioButton()
                .ValidateUrineOutputAmountFieldVisible(true)
                .ValidateDescribeUrineColourLookupButtonVisible(true)

                .InsertTextOnUrineOutputAmount("50")
                .ClickDescribeUrineColourLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Amber", urineColourId);

            continenceCareRecordPage
                .WaitForPageToLoad()

                .ClickBowelsOpened_NoRadioButton()
                .ValidateLastTimeBowelsOpenedTextareaFieldVisible(true)
                .ValidateStoolActionTakenTextareaFieldVisible(true)
                .ValidateStoolTypeFieldVisible(false)
                .ValidateStoolAmountFieldVisible(false)
                .ValidateMucusPresentOptionsVisible(false)
                .ValidateBloodPresentOptionsVisible(false)

                .ClickBowelsOpened_YesRadioButton()
                .ValidateLastTimeBowelsOpenedTextareaFieldVisible(false)
                .ValidateStoolActionTakenTextareaFieldVisible(false)
                .ValidateStoolTypeFieldVisible(true)
                .ValidateStoolAmountFieldVisible(true)
                .ValidateMucusPresentOptionsVisible(true)
                .ValidateBloodPresentOptionsVisible(true)

                .SelectStoolType("Type 1 - Separate hard lumps, like nuts (hard to pass)")
                .SelectStoolAmount("Small")
                .ClickMucusPresent_NoRadioButton()
                .ClickBloodPresent_NoRadioButton();

            continenceCareRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
                .InsertTextOnWhereOnTheBody("Neck")
                .ClickDescribeSkinConditionLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Dry Skin").TapSearchButton().SelectResultElement(skinConditionId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SelectHasContinencePadBeenChanged("Yes")
                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .InsertTextOnReviewDetails("details here ...");

            continenceCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(bedroom_CarePhysicalLocationId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("action take info ...")
                .InsertTotalTimeSpentWithClientMinutes("30")
                .InsertTextOnAdditionalNotes("extra notes ...")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Stoma").TapSearchButton().SelectResultElement(equipment1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyConsentGivenPicklistSelectedValue("No")
                .VerifyNonConsentDetailSelectedValue("Declined")
                .VerifyReasonConsentDeclinedTextareaFieldText("Reason for Declined ...")
                .VerifyEncouragementGivenTextareaFieldText("Encouragement given ...")
                .VerifyCareProvidedWithoutConsent_YesOptionSelected(true)
                .VerifyCareProvidedWithoutConsent_NoOptionSelected(false)

                .VerifyDateAndTimeOccurredDateFieldText("01/05/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("10:00");

            continenceCareRecordPage
                .ValidateDoesThePersonNeedCatheterCare_YesRadioButtonChecked()
                .ValidateIsTheCatheterPatentAndDraining_YesRadioButtonChecked()
                .ValidateHasTheCatheterBagBeenEmptied_YesRadioButtonChecked()
                .ValidatePassedUrine_YesRadioButtonChecked()
                .ValidateUrineOutputAmountText("50")
                .ValidateDescribeUrineColourLinkText("Amber")
                .ValidateIsTheCatheterPositionedSecured_YesRadioButtonChecked()
                .ValidateHaveYouCleanedTheCatheterArea_YesRadioButtonChecked()
                .ValidateIsThereAnyMalodour_NoRadioButtonChecked();

            continenceCareRecordPage
                .ValidateBowelsOpened_YesRadioButtonChecked()
                .ValidateStoolTypeSelectedText("Type 1 - Separate hard lumps, like nuts (hard to pass)")
                .ValidateStoolAmountSelectedText("Small")
                .ValidateMucusPresent_NoRadioButtonChecked()
                .ValidateBloodPresent_NoRadioButtonChecked();

            continenceCareRecordPage
                .ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonChecked()
                .ValidateWhereOnTheBodyText("Neck")
                .ValidateDescribeSkinCondition_SelectedElementLinkText(skinConditionId, "Dry Skin")
                .ValidateHasContinencePadBeenChangedSelectedText("Yes")
                .ValidateReviewRequiredBySeniorColleague_YesRadioButtonChecked()
                .ValidateReviewDetailsText("details here ...");

            continenceCareRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_CarePhysicalLocationId, "Bedroom")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("action take info ...")
                .VerifyTotalTimeSpentWithClientMinutesFieldText("30")
                .ValidateAdditionalNotesText("extra notes ...")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Stoma")
                .ValidateAssistanceNeededLinkText("Independent");

            continenceCareRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8790

        [TestProperty("JiraIssueID", "ACC-3895")]
        [Description("Step(s) 1 to 15 from the original test - ACC-3895")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC3895_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Moris";
            var lastName = _currentDateSuffix;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Urine Colour

            var urineColourId = dbHelper.urineColour.GetByName("Amber")[0];

            #endregion

            #region Skin Condition

            var skinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Dry Skin")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Stoma")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred("02/06/2024")
                .SetTimeOccurred("08:00")
                .SelectConsentGivenPicklistValueByText("Yes")


                .ValidateFieldIsVisible("Care Note")
                .ValidateCareNoteFieldVisible(true)

                .ClickDoesThePersonNeedCatheterCare_YesRadioButton()
                .ClickIsTheCatheterPositionedSecured_YesRadioButton()
                .ClickIsTheCatheterPatentAndDraining_YesRadioButton()
                .ClickHaveYouCleanedTheCatheterArea_YesRadioButton()
                .ClickHasTheCatheterBagBeenEmptied_YesRadioButton()
                .ClickIsThereAnyMalodour_NoRadioButton()

                .ClickPassedUrine_YesRadioButton()
                .ValidateUrineOutputAmountFieldVisible(true)
                .InsertTextOnUrineOutputAmount("50")
                .ClickDescribeUrineColourLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Amber", urineColourId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickBowelsOpened_YesRadioButton()
                .SelectStoolType("Type 1 - Separate hard lumps, like nuts (hard to pass)")
                .SelectStoolAmount("Small")
                .ClickMucusPresent_NoRadioButton()
                .ClickBloodPresent_NoRadioButton();

            continenceCareRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
                .InsertTextOnWhereOnTheBody("Neck")
                .ClickDescribeSkinConditionLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Dry Skin").TapSearchButton().SelectResultElement(skinConditionId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SelectHasContinencePadBeenChanged("Yes")
                .ClickReviewRequiredBySeniorColleague_YesRadioButton()
                .InsertTextOnReviewDetails("details here ...");

            continenceCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(bedroom_CarePhysicalLocationId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("action take info ...")
                .InsertTotalTimeSpentWithClientMinutes("30")
                .InsertTextOnAdditionalNotes("extra notes ...")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Stoma").TapSearchButton().SelectResultElement(equipment1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyConsentGivenPicklistSelectedValue("Yes")
                .VerifyDateAndTimeOccurredDateFieldText("02/06/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("08:00");

            continenceCareRecordPage
                .ValidateCareNoteText("Moris's catheter care was completed as required.\r\n" +
                "Moris passed urine that was Amber.\r\n" +
                "Moris passed stool that was Type 1 - Separate hard lumps, like nuts (hard to pass).\r\n" +
                "The following new skin concerns were noted on Moris's Neck: Dry Skin.\r\n" +
                "Moris's continence pad was changed.\r\n" +
                "Moris was in the Bedroom.\r\n" +
                "Moris used the following equipment: Stoma.\r\n" +
                "Moris came across as OK.\r\n" +
                "The action taken was: action take info....\r\n" +
                "Moris did not require any assistance.\r\n" +
                "This care was given at 02/06/2024 08:00:00.\r\n" +
                "Moris was assisted by 1 colleague(s).\r\n" +
                "Overall, I spent 30 minutes with Moris.\r\n" +
                "We would like to note that: extra notes....");

            continenceCareRecordPage
                .SelectHasContinencePadBeenChanged("No");

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Moris's catheter care was completed as required.\r\n" +
                "Moris passed urine that was Amber.\r\n" +
                "Moris passed stool that was Type 1 - Separate hard lumps, like nuts (hard to pass).\r\n" +
                "The following new skin concerns were noted on Moris's Neck: Dry Skin.\r\n" +
                "Moris's continence pad was not changed.\r\n" +
                "Moris was in the Bedroom.\r\n" +
                "Moris used the following equipment: Stoma.\r\n" +
                "Moris came across as OK.\r\n" +
                "The action taken was: action take info....\r\n" +
                "Moris did not require any assistance.\r\n" +
                "This care was given at 02/06/2024 08:00:00.\r\n" +
                "Moris was assisted by 1 colleague(s).\r\n" +
                "Overall, I spent 30 minutes with Moris.\r\n" +
                "We would like to note that: extra notes....");

            continenceCareRecordPage
                .ClickMucusPresent_YesRadioButton()
                .ClickBloodPresent_YesRadioButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Moris's catheter care was completed as required.\r\n" +
                "Moris passed urine that was Amber.\r\n" +
                "Moris passed stool that was Type 1 - Separate hard lumps, like nuts (hard to pass).\r\n" +
                "Mucus was present in their stool.\r\n" +
                "Blood was present in their stool.\r\n" +
                "The following new skin concerns were noted on Moris's Neck: Dry Skin.\r\n" +
                "Moris's continence pad was not changed.\r\n" +
                "Moris was in the Bedroom.\r\n" +
                "Moris used the following equipment: Stoma.\r\n" +
                "Moris came across as OK.\r\n" +
                "The action taken was: action take info....\r\n" +
                "Moris did not require any assistance.\r\n" +
                "This care was given at 02/06/2024 08:00:00.\r\n" +
                "Moris was assisted by 1 colleague(s).\r\n" +
                "Overall, I spent 30 minutes with Moris.\r\nWe would like to note that: extra notes....");

            continenceCareRecordPage
                .ClickBowelsOpened_NoRadioButton()
                .InsertTextInLastTimeBowelsOpenedTextareaField("last time text..")
                .InsertTextInStoolActionTakenTextareaField("action taken text..")
                .WaitForPageToLoad()
                .ValidateCareNoteText("Moris's catheter care was completed as required.\r\n" +
                "Moris passed urine that was Amber.\r\n" +
                "The following new skin concerns were noted on Moris's Neck: Dry Skin.\r\n" +
                "Moris's continence pad was not changed.\r\n" +
                "Moris was in the Bedroom.\r\n" +
                "Moris used the following equipment: Stoma.\r\n" +
                "Moris came across as OK.\r\n" +
                "The action taken was: action take info....\r\n" +
                "Moris did not require any assistance.\r\n" +
                "This care was given at 02/06/2024 08:00:00.\r\n" +
                "Moris was assisted by 1 colleague(s).\r\n" +
                "Overall, I spent 30 minutes with Moris.\r\nWe would like to note that: extra notes....");


            #endregion

            #region Step 5 - 15

            //Steps 5 - 15 - These steps are not valid for web app and will be ignored for web ui automation.

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8845

        [TestProperty("JiraIssueID", "ACC-8919")]
        [Description("Test steps to validate the story ACC-1817. This test will try to validate that the Preferences field is not updated if any Preference record is created after the Continence Care record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC1817_UITestMethod01()
        {
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

            _systemUserName = "ccrostereduser1";
            _systemUserFullName = "Continence Care Rostered User 1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Continence Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Team

            var defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Zachary";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Stoma")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            /** General **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Zachary.")
                .SelectConsentGivenPicklistValueByText("Yes")
                .SetDateOccurred("01/06/2024")
                .SetTimeOccurred("09:00");

            /** Urine & Catheter Care **/
            continenceCareRecordPage
                .ClickDoesThePersonNeedCatheterCare_NoRadioButton()
                .ClickPassedUrine_NoRadioButton();

            /** Stool **/
            continenceCareRecordPage
                .ClickBowelsOpened_NoRadioButton()
                .InsertTextInLastTimeBowelsOpenedTextareaField("4 days ago")
                .InsertTextInStoolActionTakenTextareaField("Gave laxative medication");

            /** Other **/
            continenceCareRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonSkin_NoRadioButton()
                .SelectHasContinencePadBeenChanged("No")
                .ClickReviewRequiredBySeniorColleague_NoRadioButton();

            /** Additional Information **/
            continenceCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(bedroom_CarePhysicalLocationId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action take info ...")
                .InsertTotalTimeSpentWithClientMinutes("30")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Stoma").TapSearchButton().SelectResultElement(equipment1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            /** Care Note **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("There were no new concerns with Zachary's skin.\r\nZachary's continence pad was not changed.\r\nZachary was in the Bedroom.\r\nZachary used the following equipment: Stoma.\r\nZachary came across as OK.\r\nThe action taken was: Action take info....\r\nZachary did not require any assistance.\r\nThis care was given at 01/06/2024 09:00:00.\r\nZachary was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Zachary.");

            /** Handover **/
            continenceCareRecordPage
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            /** Care Preferences **/
            var dailycarerecordid = 6; //Continence Care
            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, dailycarerecordid, "Preference 1\r\nPreference 2\r\nPreference 3");


            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            /** General **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Zachary.")
                .VerifyConsentGivenPicklistSelectedValue("Yes")
                .VerifyDateAndTimeOccurredDateFieldText("01/06/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("09:00");

            /** Urine & Catheter Care **/
            continenceCareRecordPage
                .ValidateDoesThePersonNeedCatheterCare_NoRadioButtonChecked()
                .ValidatePassedUrine_NoRadioButtonChecked();

            /** Stool **/
            continenceCareRecordPage
                .ValidateBowelsOpened_NoRadioButtonChecked()
                .VerifyLastTimeBowelsOpenedTextareaFieldText("4 days ago")
                .VerifyStoolActionTakenTextareaFieldText("Gave laxative medication");

            /** Other **/
            continenceCareRecordPage
                .ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonChecked()
                .ValidateHasContinencePadBeenChangedSelectedText("No")
                .ValidateReviewRequiredBySeniorColleague_NoRadioButtonChecked();

            /** Additional Information **/
            continenceCareRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_CarePhysicalLocationId, "Bedroom")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Action take info ...")
                .VerifyTotalTimeSpentWithClientMinutesFieldText("30")
                .ValidateAdditionalNotesText("")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Stoma")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName);

            /** Care Note **/
            continenceCareRecordPage
                .ValidateCareNoteText("There were no new concerns with Zachary's skin.\r\nZachary's continence pad was not changed.\r\nZachary was in the Bedroom.\r\nZachary used the following equipment: Stoma.\r\nZachary came across as OK.\r\nThe action taken was: Action take info....\r\nZachary did not require any assistance.\r\nThis care was given at 01/06/2024 09:00:00.\r\nZachary was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Zachary.");

            /** Handover **/
            continenceCareRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8920")]
        [Description("Test steps to validate the story ACC-1817. This test will try to validate the users can provide care even if no consent was given: Consent Given? = No; Non-consent Detail = Declined; Care provided without consent? = Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Continence Care")]
        public void ContinenceCare_ACC1817_UITestMethod02()
        {
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

            _systemUserName = "ccrostereduser1";
            _systemUserFullName = "Continence Care Rostered User 1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Continence Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Team

            var defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Zachary";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Stoma")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

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
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            /** General **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Zachary.")
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Declined")
                .InsertTextInReasonConsentDeclinedTextareaField("Refused to be cleaned")
                .InsertTextInEncouragementGivenTextareaField("Explained the the cleaning was for sanitary reasons")
                .SelectCareProvidedWithoutConsent_YesOption()
                .SetDateOccurred("01/06/2024")
                .SetTimeOccurred("09:00");

            /** Urine & Catheter Care **/
            continenceCareRecordPage
                .ClickDoesThePersonNeedCatheterCare_NoRadioButton()
                .ClickPassedUrine_NoRadioButton();

            /** Stool **/
            continenceCareRecordPage
                .ClickBowelsOpened_NoRadioButton()
                .InsertTextInLastTimeBowelsOpenedTextareaField("4 days ago")
                .InsertTextInStoolActionTakenTextareaField("Gave laxative medication");

            /** Other **/
            continenceCareRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonSkin_NoRadioButton()
                .SelectHasContinencePadBeenChanged("No")
                .ClickReviewRequiredBySeniorColleague_NoRadioButton();

            /** Additional Information **/
            continenceCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(bedroom_CarePhysicalLocationId);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action take info ...")
                .InsertTotalTimeSpentWithClientMinutes("30")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Stoma").TapSearchButton().SelectResultElement(equipment1Id);

            continenceCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            /** Care Note **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("There were no new concerns with Zachary's skin.\r\nZachary's continence pad was not changed.\r\nZachary was in the Bedroom.\r\nZachary used the following equipment: Stoma.\r\nZachary came across as OK.\r\nThe action taken was: Action take info....\r\nZachary did not require any assistance.\r\nThis care was given at 01/06/2024 09:00:00.\r\nZachary was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Zachary.");

            /** Handover **/
            continenceCareRecordPage
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndClose();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var continenceRecords = dbHelper.cpPersonToileting.GetByPersonId(personId);
            Assert.AreEqual(1, continenceRecords.Count);
            var continenceId = continenceRecords[0];

            continenceCarePage
                .OpenRecord(continenceId);

            /** General **/
            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Zachary.")
                .VerifyConsentGivenPicklistSelectedValue("No")
                .VerifyNonConsentDetailSelectedValue("Declined")
                .VerifyReasonConsentDeclinedTextareaFieldText("Refused to be cleaned")
                .VerifyEncouragementGivenTextareaFieldText("Explained the the cleaning was for sanitary reasons")
                .VerifyCareProvidedWithoutConsent_YesOptionSelected(true)
                .VerifyDateAndTimeOccurredDateFieldText("01/06/2024")
                .VerifyDateAndTimeOccurredTimeFieldText("09:00");

            /** Urine & Catheter Care **/
            continenceCareRecordPage
                .ValidateDoesThePersonNeedCatheterCare_NoRadioButtonChecked()
                .ValidatePassedUrine_NoRadioButtonChecked();

            /** Stool **/
            continenceCareRecordPage
                .ValidateBowelsOpened_NoRadioButtonChecked()
                .VerifyLastTimeBowelsOpenedTextareaFieldText("4 days ago")
                .VerifyStoolActionTakenTextareaFieldText("Gave laxative medication");

            /** Other **/
            continenceCareRecordPage
                .ValidateAreThereAnyNewConcernsWithThePersonSkin_NoRadioButtonChecked()
                .ValidateHasContinencePadBeenChangedSelectedText("No")
                .ValidateReviewRequiredBySeniorColleague_NoRadioButtonChecked();

            /** Additional Information **/
            continenceCareRecordPage
                .ValidateLocation_SelectedElementLinkText(bedroom_CarePhysicalLocationId, "Bedroom")
                .ValidateWellbeingLinkText("OK")
                .ValidateActionTakenText("Action take info ...")
                .VerifyTotalTimeSpentWithClientMinutesFieldText("30")
                .ValidateAdditionalNotesText("")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Stoma")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName);

            /** Care Note **/
            continenceCareRecordPage
                .ValidateCareNoteText("There were no new concerns with Zachary's skin.\r\nZachary's continence pad was not changed.\r\nZachary was in the Bedroom.\r\nZachary used the following equipment: Stoma.\r\nZachary came across as OK.\r\nThe action taken was: Action take info....\r\nZachary did not require any assistance.\r\nThis care was given at 01/06/2024 09:00:00.\r\nZachary was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Zachary.");

            /** Handover **/
            continenceCareRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        #endregion

    }

}














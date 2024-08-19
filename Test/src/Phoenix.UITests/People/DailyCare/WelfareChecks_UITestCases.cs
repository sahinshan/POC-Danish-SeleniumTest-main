using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class WelfareChecks : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("WC BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("WC T1", null, _businessUnitId, "907678", "WelfareChecksT1@careworkstempmail.com", "Welfare Checks T1", "020 123456");

                #endregion

                #region Create SystemUser Record

                _systemUserName = "wcuser1";
                _systemUserFullName = "Welfare Check User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Welfare Check", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8367

        [TestProperty("JiraIssueID", "ACC-3351")]
        [Description("Step(s) 1 to 5 from the original test - ACC-3351")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void WelfareChecks_ACC3351_UITestMethod01()
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

            var dayNightCheckObservations_OptionSetId = dbHelper.optionSet.GetOptionSetIdByName("Day/Night Check Observations")[0];

            #endregion

            #region Option Set Values

            var resting_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(dayNightCheckObservations_OptionSetId, "Resting")[0];
            var watchingTV_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(dayNightCheckObservations_OptionSetId, "Watching TV")[0];
            var other_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(dayNightCheckObservations_OptionSetId, "Other")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var other_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Physical Locations 

            var ok_careWellbeingId = dbHelper.careWellbeing.GetByName("OK")[0];

            #endregion

            #region Create System User Record

            var systemUserName2 = "wcuser2";
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(systemUserName2, "Welfare Check", "User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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
                .NavigateToWelfareChecksPage();

            personWelfareChecksPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();


            var dateAndTimeOccurred = DateTime.Now.AddDays(-2);

            personWelfareCheckRecordPage
                .WaitForPageToLoad()

                .SetDateOccurred(dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred("09:00")

                .ValidateCareNoteText("This care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .SelectWereTheyAsleepOrAwake("Asleep")
                .ValidateCareNoteText("Boris was Asleep.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
               .SelectWereTheyAsleepOrAwake("Awake")
               .ClickObservationsLookupButton();

            lookupMultiSelectPopup
                .WaitForOptionSetLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Resting").TapSearchButton().AddElementToList(resting_OptionSetValueId)
                .TypeSearchQuery("Watching TV").TapSearchButton().AddElementToList(watchingTV_OptionSetValueId)
                .TapOKButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting and Watching TV.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
               .ClickObservationsLookupButton();

            lookupMultiSelectPopup
                .WaitForOptionSetLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(other_OptionSetValueId)
                .TapOKButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Other.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTextOnOtherObservationText("Playing Cards")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTextOnDetailsOfConversation("Life and family talk")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .ClickIsTheResidentInASafePlace_YesRadioButton()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was in a safe place.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .ClickIsTheResidentInASafePlace_NoRadioButton()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTextOnActionTaken("Moved to a safe place")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
               .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Bedroom").TapSearchButton().AddElementToList(bedroom_CarePhysicalLocationId)
                .TapOKButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
               .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Other").TapSearchButton().AddElementToList(other_CarePhysicalLocationId)
                .TapOKButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Other.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTextOnLocationIfOther("Garden")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", ok_careWellbeingId);

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTextOnActionTakenHasPainReliefBeenOffered("Walk around the garden")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThe action taken was: Walk around the garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 1 colleague(s).");

            personWelfareCheckRecordPage
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(systemUserName2).TapSearchButton().AddElementToList(systemUser2Id)
                .TapOKButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThe action taken was: Walk around the garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 2 colleague(s).");

            personWelfareCheckRecordPage
                .InsertTotalTimeSpentWithPersonMinutes("35")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThe action taken was: Walk around the garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 2 colleague(s).\r\nOverall, I spent 35 minutes with Boris.");

            personWelfareCheckRecordPage
                .InsertTextOnAdditionalNotes("Boris was in a happy mood")
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThe action taken was: Walk around the garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 2 colleague(s).\r\nOverall, I spent 35 minutes with Boris.\r\nWe would like to note that: Boris was in a happy mood.");

            personWelfareCheckRecordPage
                .ClickSave()
                .WaitForPageToLoad()
                .ValidateCareNoteText("Boris was Awake.\r\nBoris was Resting, Watching TV and Playing Cards.\r\nOur conversation included: Life and family talk.\r\nBoris was not in a safe place.\r\nThe action taken was: Moved to a safe place.\r\nBoris was in the Bedroom and Garden.\r\nBoris came across as OK.\r\nThe action taken was: Walk around the garden.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 09:00:00.\r\nBoris was assisted by 2 colleague(s).\r\nOverall, I spent 35 minutes with Boris.\r\nWe would like to note that: Boris was in a happy mood.");


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














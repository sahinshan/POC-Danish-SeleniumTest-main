using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper.Models;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class PersonDiaryEvents_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PA PDE");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PDE T1", null, _businessUnitId, "907678", "PersonDiaryEventT1@careworkstempmail.com", "Person Diary Event T1", "020 123456");
                var defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

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

                _systemUserName = "pderostereduser1";
                _systemUserFullName = "Person Diary Event Rostered User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Diary Event", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

                #endregion

                #region Team Member

                commonMethodsDB.CreateTeamMember(defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

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

        [TestProperty("JiraIssueID", "ACC-9090")]
        [Description("Save record with empty mandatory fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()

                .ValidateTopPageNotificationVisibility(false)
                .ValidateDiaryEventTypeErrorLabelVisibility(false)
                .ValidateDiaryEventTypeOtherErrorLabelVisibility(false)
                .ValidateStartDateTimeErrorLabelVisibility(false)

                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateDiaryEventTypeErrorLabelVisibility(true)
                .ValidateDiaryEventTypeErrorLabelText("Please fill out this field.")
                .ValidateDiaryEventTypeOtherErrorLabelVisibility(false)
                .ValidateStartDateTimeErrorLabelVisibility(true)
                .ValidateStartDateTimeErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9091")]
        [Description("Set data in all mandatory fields and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Diary Event Type

            var personDiaryEventTypeId = dbHelper.personDiaryEventType.GetByName("Social")[0];

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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ClickDiaryEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Social", personDiaryEventTypeId);

            var startDate = DateTime.Now.AddDays(-2);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDateTime(startDate.ToString("dd/MM/yyyy"))
                .InsertTextOnStartDateTime_Time("08:45")
                .ClickSaveAndCloseButton();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonDiaryEvents = dbHelper.cpPersonDiaryEvent.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonDiaryEvents.Count);
            var cpPersonDiaryEventId = allPersonDiaryEvents[0];

            personDiaryEventsPage
                .OpenRecord(cpPersonDiaryEventId);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDiaryEventTypeLinkText("Social")
                .ValidateStartDateTimeText(startDate.ToString("dd/MM/yyyy"))
                .ValidateStartDateTime_TimeText("08:45")
                .ValidateResponsibleTeamLinkText("PDE T1")
                .ValidateDateCreatedText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateEnddatetimeText("")
                .ValidateEndDateTime_TimeText("")
                .ValidateCareNoteText("")
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9092")]
        [Description("Set data in all fields and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Diary Event Type

            var personDiaryEventTypeId = dbHelper.personDiaryEventType.GetByName("Social")[0];

            #endregion


            #region Set data in all fields and save the record

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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ClickDiaryEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Social", personDiaryEventTypeId);

            var startDate = DateTime.Now.AddDays(-2);
            var endDate = DateTime.Now.AddDays(2);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDateTime(startDate.ToString("dd/MM/yyyy"))
                .InsertTextOnStartDateTime_Time("08:45")
                .InsertTextOnEndDateTime(endDate.ToString("dd/MM/yyyy"))
                .InsertTextOnEndDateTime_Time("19:30")
                .InsertTextOnCareNote("Note 1\r\nNote 2")
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickSaveAndCloseButton();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonDiaryEvents = dbHelper.cpPersonDiaryEvent.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonDiaryEvents.Count);
            var cpPersonDiaryEventId = allPersonDiaryEvents[0];

            personDiaryEventsPage
                .OpenRecord(cpPersonDiaryEventId);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDiaryEventTypeLinkText("Social")
                .ValidateStartDateTimeText(startDate.ToString("dd/MM/yyyy"))
                .ValidateStartDateTime_TimeText("08:45")
                .ValidateResponsibleTeamLinkText("PDE T1")
                .ValidateDateCreatedText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateEnddatetimeText(endDate.ToString("dd/MM/yyyy"))
                .ValidateEndDateTime_TimeText("19:30")
                .ValidateCareNoteText("Note 1\r\nNote 2")
                .ValidateIncludeInNextHandover_YesRadioButtonChecked()
                .ValidateFlagRecordForHandover_YesRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9093")]
        [Description("Validate Diary Event Type Other field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Diary Event Type

            var personDiaryEventTypeId = dbHelper.personDiaryEventType.GetByName("Other")[0];

            #endregion


            #region Validate Diary Event Type Other field

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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                
                .ValidateDiaryEventTypeOtherFieldVisibility(false)

                .ClickDiaryEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Other", personDiaryEventTypeId);

            var startTime = DateTime.Now.AddDays(-2);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                
                .ValidateDiaryEventTypeOtherFieldVisibility(true)
                .InsertTextOnDiaryEventTypeOther("Walking with friends")

                .InsertTextOnStartDateTime(startTime.ToString("dd/MM/yyyy"))
                .InsertTextOnStartDateTime_Time("08:45")
                .ClickSaveAndCloseButton();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonDiaryEvents = dbHelper.cpPersonDiaryEvent.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonDiaryEvents.Count);
            var cpPersonDiaryEventId = allPersonDiaryEvents[0];

            personDiaryEventsPage
                .OpenRecord(cpPersonDiaryEventId);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDiaryEventTypeLinkText("Other")
                .ValidateDiaryEventTypeOtherFieldVisibility(true)
                .ValidateDiaryEventTypeOtherText("Walking with friends")
                .ValidateStartDateTimeText(startTime.ToString("dd/MM/yyyy"))
                .ValidateStartDateTime_TimeText("08:45")
                .ValidateResponsibleTeamLinkText("PDE T1")
                .ValidateDateCreatedText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateEnddatetimeText("")
                .ValidateEndDateTime_TimeText("")
                .ValidateCareNoteText("")
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9094")]
        [Description("Try to save a record with an end date smaller than the start date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod05()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Diary Event Type

            var personDiaryEventTypeId = dbHelper.personDiaryEventType.GetByName("Social")[0];

            #endregion


            #region Try to save a record with an end date smaller than the start date

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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ClickDiaryEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Social", personDiaryEventTypeId);

            var startDate = DateTime.Now.AddDays(-2);
            var endDate = DateTime.Now.AddDays(-4);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDateTime(startDate.ToString("dd/MM/yyyy"))
                .InsertTextOnStartDateTime_Time("08:45")
                .InsertTextOnEndDateTime(endDate.ToString("dd/MM/yyyy"))
                .InsertTextOnEndDateTime_Time("19:05")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("End date and time must be greater than start date and time.").TapCloseButton();

            startDate = DateTime.Now.AddDays(-4);
            endDate = DateTime.Now.AddDays(-2);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDateTime(startDate.ToString("dd/MM/yyyy"))
                .InsertTextOnStartDateTime_Time("08:45")
                .InsertTextOnEndDateTime(endDate.ToString("dd/MM/yyyy"))
                .InsertTextOnEndDateTime_Time("19:05")
                .ClickSaveAndCloseButton();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonDiaryEvents = dbHelper.cpPersonDiaryEvent.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonDiaryEvents.Count);
            var cpPersonDiaryEventId = allPersonDiaryEvents[0];

            personDiaryEventsPage
                .OpenRecord(cpPersonDiaryEventId);

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDiaryEventTypeLinkText("Social")
                .ValidateStartDateTimeText(startDate.ToString("dd/MM/yyyy"))
                .ValidateStartDateTime_TimeText("08:45")
                .ValidateResponsibleTeamLinkText("PDE T1")
                .ValidateDateCreatedText(DateTime.Now.ToString("dd/MM/yyyy"))
                .ValidateEnddatetimeText(endDate.ToString("dd/MM/yyyy"))
                .ValidateEndDateTime_TimeText("19:05")
                .ValidateCareNoteText("")
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9095")]
        [Description("Try to save a record with an end date smaller than the start date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Diary Event")]
        public void PersonDiaryEvents_ACC4090_UITestMethod06()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Eldric";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Person Diary Event Type

            var personDiaryEventType1Id = dbHelper.personDiaryEventType.GetByName("Social")[0];
            var personDiaryEventType2Id = dbHelper.personDiaryEventType.GetByName("GP")[0];
            var personDiaryEventType3Id = dbHelper.personDiaryEventType.GetByName("Hairdresser")[0];
            var personDiaryEventType4Id = dbHelper.personDiaryEventType.GetByName("District Nurse")[0];
            var personDiaryEventType5Id = dbHelper.personDiaryEventType.GetByName("Other Healthcare Professional")[0];
            var personDiaryEventType6Id = dbHelper.personDiaryEventType.GetByName("SALT")[0];
            var personDiaryEventType7Id = dbHelper.personDiaryEventType.GetByName("MHT")[0];
            var personDiaryEventType8Id = dbHelper.personDiaryEventType.GetByName("Other")[0];

            #endregion


            #region Try to save a record with an end date smaller than the start date

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
                .NavigateToDiaryEventsPage();

            personDiaryEventsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDiaryEventRecordPage
                .WaitForPageToLoad()
                .ClickDiaryEventTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Social").TapSearchButton().ValidateResultElementPresent(personDiaryEventType1Id)
                .TypeSearchQuery("GP").TapSearchButton().ValidateResultElementPresent(personDiaryEventType2Id)
                .TypeSearchQuery("Hairdresser").TapSearchButton().ValidateResultElementPresent(personDiaryEventType3Id)
                .TypeSearchQuery("District Nurse").TapSearchButton().ValidateResultElementPresent(personDiaryEventType4Id)
                .TypeSearchQuery("Other Healthcare Professional").TapSearchButton().ValidateResultElementPresent(personDiaryEventType5Id)
                .TypeSearchQuery("SALT").TapSearchButton().ValidateResultElementPresent(personDiaryEventType6Id)
                .TypeSearchQuery("MHT").TapSearchButton().ValidateResultElementPresent(personDiaryEventType7Id)
                .TypeSearchQuery("Other").TapSearchButton().ValidateResultElementPresent(personDiaryEventType8Id);

            #endregion

        }


        #endregion

    }

}
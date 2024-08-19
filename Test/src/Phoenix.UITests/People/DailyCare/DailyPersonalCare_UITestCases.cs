using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class DailyPersonalCare : FunctionalTest
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

                _teamId = commonMethodsDB.CreateTeam("DPC T1", null, _businessUnitId, "907678", "DailyPersonalCareT1@careworkstempmail.com", "DailyPersonalCare T1", "020 123456");

                #endregion

                #region Create System User Record

                _systemUserName = "DPCUser1";
                _systemUserFullName = "Daily Personal Care User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

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

        [TestProperty("JiraIssueID", "ACC-2446")]
        [Description("Step(s) 1 from the original test - ACC-2446")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2446_UITestMethod01()
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

            #region Person Daily Personal Care

            var dateTimeOccurred = new DateTime(2024, 1, 1, 10, 30, 0);
            var consetGive = 2; //No
            var carenonconsentid = 1; //Absent
            var cpPersonPersonalCareDailyRecord1Id = dbHelper.cpPersonPersonalCareDailyRecord.CreateCPPersonPersonalCareDailyRecord(personId, _teamId, "no data...", dateTimeOccurred, consetGive, carenonconsentid, "Went to hospital.");

            #endregion

            #region Step 1

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()

                .ValidateHeaderCellText(2, "Date and Time Occurred")
                .ValidateHeaderCellText(3, "Consent Given?")
                .ValidateHeaderCellText(4, "Non-consent Detail")
                .ValidateHeaderCellText(5, "Are there any new concerns with the person’s skin?")
                .ValidateHeaderCellText(6, "Created By")
                .ValidateHeaderCellText(7, "Created On")
                .ValidateHeaderCellText(8, "Modified By")
                .ValidateHeaderCellText(9, "Modified On")

                .ValidateRecordCellText(cpPersonPersonalCareDailyRecord1Id, 2, "01/01/2024 10:30:00")
                .ValidateRecordCellText(cpPersonPersonalCareDailyRecord1Id, 3, "No")
                .ValidateRecordCellText(cpPersonPersonalCareDailyRecord1Id, 4, "Absent")
                .ValidateRecordCellText(cpPersonPersonalCareDailyRecord1Id, 5, "No");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8386

        [TestProperty("JiraIssueID", "ACC-8509")]
        [Description("Step(s) 1 to 5 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod01()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .SelectConsentGiven("No");

            #endregion

            #region Step 2

            personDailyPersonalCareRecordPage
                .ValidateSelectedNonConsentDetail("")
                .SelectNonConsentDetail("Absent")
                .SelectNonConsentDetail("Declined")
                .SelectNonConsentDetail("Deferred");

            #endregion

            #region Step 3

            personDailyPersonalCareRecordPage
                .SelectNonConsentDetail("Absent")
                .SetReasonForAbsence("Visit to the doctor");

            #endregion

            #region Step 4

            personDailyPersonalCareRecordPage
                .SelectNonConsentDetail("Declined")
                .SetReasonConsentDeclined("Not in a good mental state")
                .SetEncouragementGiven("Mental support")
                .ClickCareProvidedWithoutConsent_NoRadioButton();

            #endregion

            #region Step 5

            var deferredToDate = DateTime.Now.AddDays(2);

            personDailyPersonalCareRecordPage
                .SelectNonConsentDetail("Deferred")
                .SetDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .SetDeferredToTime("09:00");

            personDailyPersonalCareRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("9 AM", careprovidercareperiodsetupId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var careProvidedWithoutConsentRecords = dbHelper.cpPersonPersonalCareDailyRecord.GetByPersonId(personId);
            Assert.AreEqual(1, careProvidedWithoutConsentRecords.Count);
            var personPersonalCareDailyRecordId = careProvidedWithoutConsentRecords[0];

            personDailyPersonalCarePage
                .OpenRecord(personPersonalCareDailyRecordId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(personFullName)
                .ValidateResponsibleTeamLinkText("DPC T1")
                .VerifyPreferencesTextAreaFieldText("No preferences recorded, please check with Clark.")
                .VerifyDateAndTimeOccurredDateFieldText(dateandTimeOccurred.ToString("dd/MM/yyyy"))
                .VerifyDateAndTimeOccurredTimeFieldText("07:00")
                .ValidateConsentGivenSelectedText("No")
                .ValidateSelectedNonConsentDetail("Deferred")
                .ValidateDeferredToDate(deferredToDate.ToString("dd/MM/yyyy"))
                .ValidateSelectedDeferredToTimeOrShift("Shift")
                .ValidateDeferredToShiftLinkText("9 AM");


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8510")]
        [Description("Step(s) 6 to 12 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod02()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Care Wash

            var wash1Id = dbHelper.personalCareWash.GetByName("Bath")[0];
            var wash2Id = dbHelper.personalCareWash.GetByName("Shower")[0];
            var wash3Id = dbHelper.personalCareWash.GetByName("Wash")[0];

            #endregion

            #region Personal Care Body Areas

            var personalCareBodyArea1Id = dbHelper.personalCareBodyArea.GetByName("Face")[0];
            var personalCareBodyArea2Id = dbHelper.personalCareBodyArea.GetByName("Feet")[0];
            var personalCareBodyArea3Id = dbHelper.personalCareBodyArea.GetByName("Hair")[0];
            var personalCareBodyArea4Id = dbHelper.personalCareBodyArea.GetByName("Hands")[0];
            var personalCareBodyArea5Id = dbHelper.personalCareBodyArea.GetByName("Lower Body")[0];
            var personalCareBodyArea6Id = dbHelper.personalCareBodyArea.GetByName("Upper Body")[0];

            #endregion

            #region Personal Care Oral Care

            var oralCare1Id = dbHelper.personalCareOralCare.GetByName("Brush Teeth")[0];
            var oralCare2Id = dbHelper.personalCareOralCare.GetByName("Clean Dentures")[0];
            var oralCare3Id = dbHelper.personalCareOralCare.GetByName("Floss")[0];
            var oralCare4Id = dbHelper.personalCareOralCare.GetByName("Mouth Care")[0];
            var oralCare5Id = dbHelper.personalCareOralCare.GetByName("Put Dentures In")[0];
            var oralCare6Id = dbHelper.personalCareOralCare.GetByName("Remove Dentures")[0];

            #endregion

            #region Personal Care Clothes

            var clothes1Id = dbHelper.personalCareClothes.GetByName("Changed")[0];
            var clothes2Id = dbHelper.personalCareClothes.GetByName("Dressed")[0];
            var clothes3Id = dbHelper.personalCareClothes.GetByName("Undressed")[0];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var other_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Shower Chair")[0];
            var equipment2Id = dbHelper.careEquipment.GetByName("Hand Rail")[0];
            var equipment3Id = dbHelper.careEquipment.GetByName("Wheelchair")[0];
            var equipment4Id = dbHelper.careEquipment.GetByName("Walking Stick")[0];
            var equipment5Id = dbHelper.careEquipment.GetByName("Walking Frame")[0];
            var equipment6Id = dbHelper.careEquipment.GetByName("Hoist / Stand Aid")[0];
            var equipment7Id = dbHelper.careEquipment.GetByName("Other")[0];

            #endregion


            #region Step 6

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .ClickWashLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(wash1Id)
                .AddElementToList(wash2Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateBodyAreasLookupButtonVisibility(false) //at this point the Body Areas shoud not yet be visible
                .ClickWashLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(wash3Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateBodyAreasLookupButtonVisibility(true) //After selectng "Wash" this field shoud be visible
                .WaitForPageToLoad()
                .ClickBodyAreasLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(personalCareBodyArea1Id)
                .AddElementToList(personalCareBodyArea2Id)
                .AddElementToList(personalCareBodyArea3Id)
                .AddElementToList(personalCareBodyArea4Id)
                .AddElementToList(personalCareBodyArea5Id)
                .AddElementToList(personalCareBodyArea6Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 7

            personDailyPersonalCareRecordPage
                .ClickOralCareLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(oralCare1Id)
                .AddElementToList(oralCare2Id)
                .AddElementToList(oralCare3Id)
                .AddElementToList(oralCare4Id)
                .AddElementToList(oralCare5Id)
                .AddElementToList(oralCare6Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 8

            personDailyPersonalCareRecordPage
                .ClickClothesLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(clothes1Id)
                .AddElementToList(clothes2Id)
                .AddElementToList(clothes3Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 9 & 10

            personDailyPersonalCareRecordPage
                .ClickReviewrequiredbyseniorcolleague_NoRadioButton()
                .ValidateReviewDetailsVisibility(false)
                .ClickReviewrequiredbyseniorcolleague_YesRadioButton()
                .ValidateReviewDetailsVisibility(true)
                .InsertReviewDetails("Review details here ...");

            #endregion

            #region Step 11

            personDailyPersonalCareRecordPage
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bedroom_CarePhysicalLocationId)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(other_CarePhysicalLocationId)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(true)
                .InsertTextOnLocationIfOther("other location ...");

            #endregion

            #region Step 12

            personDailyPersonalCareRecordPage
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment1Id)
                .AddElementToList(equipment2Id)
                .AddElementToList(equipment3Id)
                .AddElementToList(equipment4Id)
                .AddElementToList(equipment5Id)
                .AddElementToList(equipment6Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateEquipmentIfOtherVisibility(false)
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment7Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateEquipmentIfOtherVisibility(true)
                .InsertTextOnEquipmentIfOther("Special equipment ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8511")]
        [Description("Step(s) 13 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod03()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeing3Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];

            #endregion


            #region Step 13

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Unhappy", careWellbeing1Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .InsertTextOnActionTaken("action taken 1 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeing2Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .InsertTextOnActionTaken("action taken 2 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Very Unhappy", careWellbeing3Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .InsertTextOnActionTaken("action taken 3 ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8512")]
        [Description("Step(s) 14 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod04()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
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


            #region Step 14

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Asked For Help", careAssistanceNeeded1Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .SelectAssistanceAmount("Some")
                .SelectAssistanceAmount("Fair")
                .SelectAssistanceAmount("A Lot")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded2Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Physical Assistance", careAssistanceNeeded3Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)
                .SelectAssistanceAmount("Some");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-8513")]
        [Description("Step(s) 15 to 17 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod05()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            var _systemUser2Name = "DPCRosteredUser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Daily Personal Care", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);


            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Step 14

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("DPCRosteredUser2").TapSearchButton().AddElementToList(_systemUser2Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 16

            personDailyPersonalCareRecordPage
                .InsertTotalTimeSpentWithPersonMinutes("a")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(true)
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorText("Please enter a value between 1 and 1440.");

            personDailyPersonalCareRecordPage
                .InsertTotalTimeSpentWithPersonMinutes("25")
                .VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(false);


            #endregion

            #region Step 17

            personDailyPersonalCareRecordPage
                .InsertTextOnAdditionalNotes("Note 1\r\nNote 2");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8514")]
        [Description("Step(s) 18 from the original test - ACC-2437")]
        [TestMethod]
        [DeploymentItem("Files\\DocToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod06()
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

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("No equipment")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Attach Document Type

            var _documenttypeid = commonMethodsDB.CreateAttachDocumentType(_teamId, "Supporting Information", new DateTime(2020, 1, 1));

            #endregion

            #region Attach Document Sub Type

            var _documentsubtypeid = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Supporting documentation", new DateTime(2020, 1, 1), _documenttypeid);

            #endregion

            #region Step 18

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .ClickAreThereAnyNewConcernsWithThePersonSkin_NoRadioButton()
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bedroom_CarePhysicalLocationId)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Happy", careWellbeing1Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .InsertTotalTimeSpentWithPersonMinutes("20")
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment1Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeeded1Id);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton()
                .ClickSaveAndCloseButton();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var dailyPersonalCareRecords = dbHelper.cpPersonPersonalCareDailyRecord.GetByPersonId(personId);
            Assert.AreEqual(1, dailyPersonalCareRecords.Count);
            var dailyPersonalCareId = dailyPersonalCareRecords[0];

            personDailyPersonalCarePage
                .OpenRecord(dailyPersonalCareId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            attachmentsForDailyPersonalCare
                .WaitForPageToLoadInsidePersonDailyPersonalCare()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpdailypersonalcareattachment")
                .ClickOnExpandIcon();

            attachmentForDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .InsertTitle("Attachment 1")
                .InsertDate(dateandTimeOccurred.ToString("dd/MM/yyyy"))
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Supporting Information", _documenttypeid);

            attachmentForDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Supporting documentation", _documentsubtypeid);

            attachmentForDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .UploadFile(TestContext.DeploymentDirectory + "\\DocToUpload.txt")
                .ClickSaveAndCloseButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad();

            attachmentsForDailyPersonalCare
                .WaitForPageToLoadInsidePersonDailyPersonalCare();

            var attachments = dbHelper.cpDailyPersonalCareAttachment.GetByPersonDailyPersonalCareId(dailyPersonalCareId);
            Assert.AreEqual(1, attachments.Count);
            var attachmentId = attachments[0];

            attachmentsForDailyPersonalCare
                .ValidateRecordPresent(attachmentId, true);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-8515")]
        [Description("Step(s) 19 to 20 from the original test - ACC-2437")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC2437_UITestMethod07()
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
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First());

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Physical Locations 

            var bedroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("No equipment")[0];

            #endregion

            #region Personal Care Other

            var personalCareOther1Id = dbHelper.personalCareOther.GetByName("Brush Hair")[0];
            var personalCareOther2Id = dbHelper.personalCareOther.GetByName("Make Up")[0];
            var personalCareOther3Id = dbHelper.personalCareOther.GetByName("Shave")[0];
            var personalCareOther4Id = dbHelper.personalCareOther.GetByName("Fingernails")[0];
            var personalCareOther5Id = dbHelper.personalCareOther.GetByName("Toenails")[0];
            var personalCareOther6Id = dbHelper.personalCareOther.GetByName("Foot Care")[0];
            var personalCareOther7Id = dbHelper.personalCareOther.GetByName("Other")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Attach Document Type

            var _documenttypeid = commonMethodsDB.CreateAttachDocumentType(_teamId, "Supporting Information", new DateTime(2020, 1, 1));

            #endregion

            #region Attach Document Sub Type

            var _documentsubtypeid = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Supporting documentation", new DateTime(2020, 1, 1), _documenttypeid);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Daily Personal Care")
                .SelectFilterInsideOptGroup("1", "Person")
                .SelectFilterInsideOptGroup("1", "Date and Time Occurred")
                .SelectFilterInsideOptGroup("1", "Wellbeing")
                .SelectFilterInsideOptGroup("1", "Assistance Needed?")
                .SelectFilterInsideOptGroup("1", "Total Time Spent With Person (Minutes)");

            #endregion

            #region Step 20

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .ClickOtherLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(personalCareOther1Id)
                .AddElementToList(personalCareOther2Id)
                .AddElementToList(personalCareOther3Id)
                .AddElementToList(personalCareOther4Id)
                .AddElementToList(personalCareOther5Id)
                .AddElementToList(personalCareOther6Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateOtherTextVisibility(false)
                .ClickOtherLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(personalCareOther7Id)
                .TapOKButton();

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ValidateOtherTextVisibility(true)
                .InsertOtherText("Other type of care ....");


            #endregion


        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8627

        [TestProperty("JiraIssueID", "ACC-3445")]
        [Description("Step 8 from the original test (Only step valid for automation)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Daily Personal Care")]
        public void DailyPersonalCare_ACC3445_UITestMethod01()
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

            #endregion

            #region Create System User Record

            _systemUserName = "DPCRosteredUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Daily Personal Care", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            #endregion

            #region Default Team

            //get the default team for the tenant
            var defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Team Member

            //link the user with the default team so that we can access all reference data in the system
            commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Clark";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Care Washes

            var personalCareWashId = dbHelper.personalCareWash.GetByName("Bath").First();

            #endregion

            #region Personal Care Clothes

            var personalCareClothesId = dbHelper.personalCareClothes.GetByName("Changed").First();

            #endregion

            #region Personal Care Oral Cares

            var personalCareOralCareId = dbHelper.personalCareOralCare.GetByName("Brush Teeth").First();

            #endregion

            #region Personal Care Other

            var personalCareOtherId = dbHelper.personalCareOther.GetByName("Brush Hair").First();

            #endregion

            #region Personal Care Other

            var skinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Dry Skin").First();

            #endregion

            #region Care Physical Locations

            var carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Bathroom").First();

            #endregion

            #region Care Equipment

            var careEquipmentId = dbHelper.careEquipment.GetByName("Shower Chair").First();

            #endregion

            #region Care Wellbeing

            var careWellbeingId = dbHelper.careWellbeing.GetByName("OK").First();

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").First();

            #endregion


            #region Step 8

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
                .NavigateToDailyPersonalCarePage();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd/MM/yyyy"), "07:00")
                .SelectConsentGiven("Yes")
                .ClickWashLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bath").TapSearchButton().SelectResultElement(personalCareWashId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickClothesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Changed").TapSearchButton().SelectResultElement(personalCareClothesId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickOralCareLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Brush Teeth").TapSearchButton().SelectResultElement(personalCareOralCareId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickOtherLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Brush Hair").TapSearchButton().SelectResultElement(personalCareOtherId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()

                .ClickAreTheirGlassesClean_YesRadioButton()
                .ClickAreTheirGlassesBeingWorn_NoRadioButton()
                .ClickAreTheirHearingAidsSwitchedOn_YesRadioButton()
                .ClickAreTheirHearingAidsWorking_NoRadioButton()
                .ClickAreTheirHearingAidsInTheCorrectPosition_YesRadioButton()

                .ClickAreThereAnyNewConcernsWithThePersonSkin_YesRadioButton()
                .InsertWhereOnTheBody("Neck ...")
                .ClickDescribeSkinConditionLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Dry Skin").TapSearchButton().SelectResultElement(skinConditionId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickReviewrequiredbyseniorcolleague_YesRadioButton()
                .InsertReviewDetails("Review details ...")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Bathroom").TapSearchButton().SelectResultElement(carePhysicalLocationId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Shower Chair").TapSearchButton().SelectResultElement(careEquipmentId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("OK", careWellbeingId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Independent", careAssistanceNeededId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action taken ...")
                .InsertTotalTimeSpentWithPersonMinutes("30")
                .InsertTextOnAdditionalNotes("Additional notes ...");

            personDailyPersonalCareRecordPage
                .ValidateCareNoteText("Clark had a Bath.\r\nClark completed the following oral care: Brush Teeth.\r\nClark Changed.\r\nClark also did the following: Brush Hair.\r\nClark's glasses were checked:\r\nThey were not wearing their glasses\r\nTheir glasses were clean\r\nClark's hearing aids were checked:\r\nTheir hearing aids were switched on\r\nTheir hearing aids were not working\r\nTheir hearing aids were in the correct position\r\nThe following new skin concerns were noted on Clark's Neck...: Dry Skin.\r\nClark was in the Bathroom.\r\nClark used the following equipment: Shower Chair.\r\nClark came across as OK.\r\nThe action taken was: Action taken....\r\nClark did not require any assistance.\r\nThis care was given at " + dateandTimeOccurred.ToString("dd/MM/yyyy") + " 07:00:00.\r\nClark was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Clark.\r\nWe would like to note that: Additional notes....");

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            personDailyPersonalCarePage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var careProvidedWithoutConsentRecords = dbHelper.cpPersonPersonalCareDailyRecord.GetByPersonId(personId);
            Assert.AreEqual(1, careProvidedWithoutConsentRecords.Count);
            var personPersonalCareDailyRecordId = careProvidedWithoutConsentRecords[0];

            personDailyPersonalCarePage
                .OpenRecord(personPersonalCareDailyRecordId);

            personDailyPersonalCareRecordPage
                .WaitForPageToLoad()

                .ValidateConsentGivenSelectedText("Yes")
                .VerifyDateAndTimeOccurredDateFieldText(dateandTimeOccurred.ToString("dd/MM/yyyy"))
                .VerifyDateAndTimeOccurredTimeFieldText("07:00");

            personDailyPersonalCareRecordPage
                .ValidateWashOptionLinkFieldText(personalCareWashId, "Bath")
                .ValidateOralCareOptionLinkFieldText(personalCareOralCareId, "Brush Teeth")
                .ValidateClothesOptionLinkFieldText(personalCareClothesId, "Changed")
                .ValidateOtherOptionLinkFieldText(personalCareOtherId, "Brush Hair")

                .ValidateAreTheirGlassesClean_YesRadioButtonChecked()
                .ValidateAreTheirGlassesBeingWorn_NoRadioButtonChecked()
                .ValidateAreTheirHearingAidsSwitchedOn_YesRadioButtonChecked()
                .ValidateAreTheirHearingAidsWorking_NoRadioButtonChecked()
                .ValidateAreTheirHearingAidsInTheCorrectPosition_YesRadioButtonChecked()

                .ValidateAreThereAnyNewConcernsWithThePersonSkin_YesRadioButtonChecked()
                .ValidateWhereOnTheBody("Neck ...")
                .ValidateDescribeSkinConditionOptionLinkFieldText(skinConditionId, "Dry Skin")
                .ValidateReviewrequiredbyseniorcolleague_YesRadioButtonChecked()
                .ValidateReviewDetails("Review details ...");

            personDailyPersonalCareRecordPage
                .ValidateLocationOptionLinkFieldText(carePhysicalLocationId, "Bathroom")
                .ValidateEquipmentOptionLinkFieldText(careEquipmentId, "Shower Chair")
                .ValidateWellbeingLinkText("OK")
                .ValidateAssistanceNeededLinkText("Independent")
                .ValidateActionTakenText("Action taken ...")
                .VerifyTotalTimeSpentWithPersonMinutesFieldText("30")
                .ValidateAdditionalNotesText("Additional notes ...");

            personDailyPersonalCareRecordPage
                .ValidateCareNoteText("Clark had a Bath.\r\nClark completed the following oral care: Brush Teeth.\r\nClark Changed.\r\nClark also did the following: Brush Hair.\r\nClark's glasses were checked:\r\nThey were not wearing their glasses\r\nTheir glasses were clean\r\nClark's hearing aids were checked:\r\nTheir hearing aids were switched on\r\nTheir hearing aids were not working\r\nTheir hearing aids were in the correct position\r\nThe following new skin concerns were noted on Clark's Neck...: Dry Skin.\r\nClark was in the Bathroom.\r\nClark used the following equipment: Shower Chair.\r\nClark came across as OK.\r\nThe action taken was: Action taken....\r\nClark did not require any assistance.\r\nThis care was given at " + dateandTimeOccurred.ToString("dd/MM/yyyy") + " 07:00:00.\r\nClark was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Clark.\r\nWe would like to note that: Additional notes....");


            #endregion

        }

        #endregion
    }

}














using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.HealthSetup
{
    [TestClass]
    public class DiaryViewSetup_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;


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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DiaryViewSetup BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DiaryViewSetup T1", null, _businessUnitId, "907678", "DiaryViewSetupT1@careworkstempmail.com", "DiaryViewSetup T3", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User DiaryViewSetupUser1

                _systemUsername = "DiaryViewSetupUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DiaryViewSetup", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1605

        [TestProperty("JiraIssueID", "CDV6-25436")]
        [Description("Step(s) 1 and 2 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickCommunityClinicTeamsButton();

            communityClinicTeamsPage
                .WaitForCommunityClinicTeamsPageToLoad()
                .OpenRecord(_communityAndClinicTeamId);

            communityClinicTeamRecordPage
                .WaitForCommunityClinicTeamRecordPage()
                .NavigateToDiaryViewSetup();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .ClickNewRecordButton();

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .InsertTextOnTitle("DVS Home Visit")
                .InsertTextOnStartDate(DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"))
                .ClickHomeVisit_YesRadioButton()
                .ClickAvailableOnBankHolidays_YesRadioButton()
                .InsertTextOnStartTime("01:00")
                .InsertTextOnEndTime("23:00")
                .ClickGroupBookingAllowed_YesRadioButton()
                .InsertTextOnNumberOfGroupsPerDay("10")
                .InsertTextOnNumberOfIndividualsPerGroup("15")
                .ClickSaveAndCloseButton();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .ClickRefreshButton();

            var _diaryViewSetups = dbHelper.communityClinicDiaryViewSetup.GetByCommunityClinicTeam(_communityAndClinicTeamId);
            Assert.AreEqual(1, _diaryViewSetups.Count);
            var newDiaryViewSetupId = _diaryViewSetups[0];


            diaryViewSetupPage
                .OpenRecord(newDiaryViewSetupId);

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .ValidateTitleText("DVS Home Visit")
                .ValidateStartDateText(DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"))
                .ValidateHomeVisit_YesRadioButtonChecked()
                .ValidateAvailableOnBankHolidays_YesRadioButtonChecked()
                .ValidateStartTimeText("01:00")
                .ValidateEndTimeText("23:00")
                .ValidateGroupBookingAllowed_YesRadioButtonChecked()
                .ValidateNumberOfGroupsPerDayText("10")
                .ValidateNumberOfIndividualsPerGroupText("15")

                .ValidateHomeVisit_YesRadioButtonDisabled(true)
                .ValidateHomeVisit_NoRadioButtonDisabled(true);

            #endregion

            #region Step 2

            diaryViewSetupRecordPage
                .InsertTextOnEndDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("'End Date' cannot be before 'Start Date'").TapOKButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-25437")]
        [Description("Step(s) 3 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Diary View Setup

            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "DVS Home Visit", currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 100, 10, 15);

            #endregion

            #region System User

            var _linkedProfessionalSystemUserName = "ACC_1605_User_" + currentDateTimeString;
            var _linkedProfessionalName = "ACC_1605 User_" + currentDateTimeString;
            var _linkedProfessionalSystemUserId = commonMethodsDB.CreateSystemUserRecord(_linkedProfessionalSystemUserName, "ACC_1605", "User_" + currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Recurrence Pattern

            var _recurrencePatternId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 days", 1, 1);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", _linkedProfessionalSystemUserId, _teamId, _recurrencePatternId, currentDate, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion


            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickCommunityClinicTeamsButton();

            communityClinicTeamsPage
                .WaitForCommunityClinicTeamsPageToLoad()
                .OpenRecord(_communityAndClinicTeamId);

            communityClinicTeamRecordPage
                .WaitForCommunityClinicTeamRecordPage()
                .NavigateToDiaryViewSetup();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .OpenRecord(_diaryViewSetupId);

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .NavigateToLinkedProfessional();

            linkedProfessionalsPage
                .WaitForLinkedProfessionalsPageToLoad()
                .ClickNewRecordButton();

            linkedProfessionalRecordPage
                .WaitForLinkedProfessionalRecordPageToLoad()
                .ClickProfessionalLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_linkedProfessionalSystemUserName).TapSearchButton().SelectResultElement(_linkedProfessionalSystemUserId);

            linkedProfessionalRecordPage
                .WaitForLinkedProfessionalRecordPageToLoad()
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Occurs every 1 days").TapSearchButton().SelectResultElement(_recurrencePatternId);

            linkedProfessionalRecordPage
                .WaitForLinkedProfessionalRecordPageToLoad()
                .InsertTextOnNumberOfIndividualsPerDay("500")
                .ClickSaveAndCloseButton();

            linkedProfessionalsPage
                .WaitForLinkedProfessionalsPageToLoad()
                .ClickRefreshButton();

            var linkedProfessionals = dbHelper.communityClinicLinkedProfessional.GetLinkedProfessionalByID(_diaryViewSetupId);
            Assert.AreEqual(1, linkedProfessionals.Count);
            var linkedProfessionalId = linkedProfessionals[0];

            linkedProfessionalsPage
                .OpenRecord(linkedProfessionalId);

            linkedProfessionalRecordPage
                .WaitForLinkedProfessionalRecordPageToLoad()
                .ValidateDiaryViewSetupLinkText("DVS Home Visit")
                .ValidateProfessionalLinkText(_linkedProfessionalName)
                .ValidateStartDateText(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText("")
                .ValidateResponsibleTeamLinkText("DiaryViewSetup T1")
                .ValidateRecurrencePatternLinkText("Occurs every 1 days")
                .ValidateStartTimeText("01:00")
                .ValidateEndTimeText("23:00")
                .ValidateNumberOfIndividualsPerDayText("500");

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25438")]
        [Description("Step(s) 4 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Diary View Setup

            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "DVS Home Visit", currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 100, 10, 15);

            #endregion

            #region System User

            var _linkedProfessionalSystemUserName = "ACC_1605_User_" + currentDateTimeString;
            var _linkedProfessionalName = "ACC_1605 User_" + currentDateTimeString;
            var _linkedProfessionalSystemUserId = commonMethodsDB.CreateSystemUserRecord(_linkedProfessionalSystemUserName, "ACC_1605", "User_" + currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Recurrence Pattern

            var _recurrencePatternId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 days", 1, 1);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", _linkedProfessionalSystemUserId, _teamId, _recurrencePatternId, currentDate, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Linked Health Professional

            var _linkedHealthProfessionalId = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _diaryViewSetupId, _linkedProfessionalSystemUserId, currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), _recurrencePatternId, _linkedProfessionalName);

            #endregion


            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickCommunityClinicTeamsButton();

            communityClinicTeamsPage
                .WaitForCommunityClinicTeamsPageToLoad()
                .OpenRecord(_communityAndClinicTeamId);

            communityClinicTeamRecordPage
                .WaitForCommunityClinicTeamRecordPage()
                .NavigateToDiaryViewSetup();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .OpenRecord(_diaryViewSetupId);

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .NavigateToRestrictions();

            diaryViewSetupRestrictionsPage
                .WaitForDiaryViewSetupRestrictionsPageToLoad()
                .ClickNewRecordButton();

            diaryViewSetupRestrictionRecordPage
                .WaitForDiaryViewSetupRestrictionRecordPageToLoad()
                .SelectRestrictionType("Health Professional Unavailable")
                .InsertTextOnStartDate(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnEndDate(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickHealthProfessionalLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_linkedProfessionalSystemUserName).TapSearchButton().SelectResultElement(_linkedProfessionalSystemUserId);

            diaryViewSetupRestrictionRecordPage
                .WaitForDiaryViewSetupRestrictionRecordPageToLoad()
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Occurs every 1 days").TapSearchButton().SelectResultElement(_recurrencePatternId);

            diaryViewSetupRestrictionRecordPage
                .WaitForDiaryViewSetupRestrictionRecordPageToLoad()
                .InsertTextOnDetails("extra details here ...")
                .ClickSaveAndCloseButton();

            diaryViewSetupRestrictionsPage
                .WaitForDiaryViewSetupRestrictionsPageToLoad()
                .ClickRefreshButton();

            var _restrictions = dbHelper.communityClinicRestriction.GetByDiaryViewSetupId(_diaryViewSetupId);
            Assert.AreEqual(1, _restrictions.Count);
            var _restrictionId = _restrictions[0];

            diaryViewSetupRestrictionsPage
                .OpenRecord(_restrictionId);

            diaryViewSetupRestrictionRecordPage
                .WaitForDiaryViewSetupRestrictionRecordPageToLoad()
                .ValidateDiaryViewSetupLinkText("DVS Home Visit")
                .ValidateRestrictionTypeSelectedText("Health Professional Unavailable")
                .ValidateStartDateText(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateText(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateHealthProfessionalLinkText(_linkedProfessionalName)
                .ValidateResponsibleTeamLinkText("DiaryViewSetup T1")
                .ValidateRecurrencePatternLinkText("Occurs every 1 days")
                .ValidateStartTimeText("01:00")
                .ValidateEndTimeText("23:00")
                .ValidateDetailsText("extra details here ...");

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25439")]
        [Description("Step(s) 5 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickCommunityClinicTeamsButton();

            communityClinicTeamsPage
                .WaitForCommunityClinicTeamsPageToLoad()
                .OpenRecord(_communityAndClinicTeamId);

            communityClinicTeamRecordPage
                .WaitForCommunityClinicTeamRecordPage()
                .NavigateToDiaryViewSetup();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .ClickNewRecordButton();

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .InsertTextOnTitle("DVS Home Visit")
                .InsertTextOnStartDate("01/01/2023")
                .ClickHomeVisit_YesRadioButton()
                .ClickAvailableOnBankHolidays_YesRadioButton()
                .InsertTextOnStartTime("01:00")
                .InsertTextOnEndTime("23:00")
                .ClickGroupBookingAllowed_YesRadioButton()
                .InsertTextOnNumberOfGroupsPerDay("10")
                .InsertTextOnNumberOfIndividualsPerGroup("15")
                .ClickSaveAndCloseButton();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .ClickRefreshButton();

            var _diaryViewSetups = dbHelper.communityClinicDiaryViewSetup.GetByCommunityClinicTeam(_communityAndClinicTeamId);
            Assert.AreEqual(1, _diaryViewSetups.Count);
            var newDiaryViewSetupId = _diaryViewSetups[0];


            diaryViewSetupPage
                .OpenRecord(newDiaryViewSetupId);

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .InsertTextOnEndDate("02/01/2023")
                .ClickSaveAndCloseButton();

            diaryViewSetupPage
                .WaitForDiaryViewSetupPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(newDiaryViewSetupId);

            diaryViewSetupRecordPage
                .WaitForDiaryViewSetupRecordPageToLoad()
                .NavigateToAudit();

            auditListPage
                .WaitForAuditListPageToLoad("communityclinicdiaryviewsetup")
                .ValidateCellText(1, 2, "Update")
                .ValidateCellText(2, 2, "Create");

            #endregion


        }

        [TestProperty("JiraIssueID", "CDV6-25440")]
        [Description("Step(s) 6 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod005()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider (Hospital)

            var _providerId = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 3);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Diary View Setup

            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "DVS Home Visit", currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 100, 10, 15);

            #endregion

            #region System User

            var _linkedProfessionalSystemUserName = "ACC_1605_User_" + currentDateTimeString;
            var _linkedProfessionalName = "ACC_1605 User_" + currentDateTimeString;
            var _linkedProfessionalSystemUserId = commonMethodsDB.CreateSystemUserRecord(_linkedProfessionalSystemUserName, "ACC_1605", "User_" + currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Recurrence Pattern

            var _recurrencePatternId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 days", 1, 1);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", _linkedProfessionalSystemUserId, _teamId, _recurrencePatternId, currentDate, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Linked Health Professional

            var _linkedHealthProfessionalId = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _diaryViewSetupId, _linkedProfessionalSystemUserId, currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), _recurrencePatternId, _linkedProfessionalName);

            #endregion

            #region Person

            var _firstName = "Raphael";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;

            var _personID = dbHelper.person.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Waiting List").First();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default", new DateTime(2020, 1, 1), 140000000, 2, true);

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_teamId, "NHS Patient", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_teamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Data Form Community Health Case

            var _dataFormId = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Case Service Type Requested

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(
                _teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                _caseServiceTypeRequestedid, _dataFormId, _contactSourceId,
                DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");

            var _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(new Guid("05786b1b-a6ae-ed11-83ea-0a7be9a500fe"), _teamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var _communityClinicAppointmentContactTypesId = dbHelper.healthAppointmentContactType.GetByName("Face To Face").First();

            #endregion

            #region Community/Clinic Appointment Contact Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home").First();

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber)
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp.WaitForSelectHealthAppointmentTypePopUpToLoad().SelectViewByText("Appointments").TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Face To Face").TapSearchButton().SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickHomeVisitYesRadioButton()
                .InsertStartDate(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("07:05")
                .InsertEndDate(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"))
                .InsertEndTime("07:10")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Clients or patients home").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Provider ACC-1605 " + currentDateTimeString).TapSearchButton().SelectResultElement(_providerId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_linkedProfessionalSystemUserId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            var healthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);
            Assert.AreEqual(1, healthAppointments.Count);
            var healthAppointmentId = healthAppointments[0];

            caseHealthAppointmentsPage
                .OpenCaseHealthAppointmentRecord(healthAppointmentId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPage()
                .ValidateCommunityClinicTeamLinkFieldText("CCT ACC 1605 " + currentDateTimeString)
                .ValidateAppointmentReasonLinkFieldText("Assessment")
                .ValidateResponsibleTeamLinkFieldText("DiaryViewSetup T1")
                .ValidateResponsibleUserLinkFieldText("DiaryViewSetup User1")
                .ValidateContactTypeLinkFieldText("Face To Face")
                .ValidateHomeVisitSelectedOption(true)
                .ValidateStartDateFieldText(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldText(DateTime.Now.AddDays(1).Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTimeFieldText("07:05")
                .ValidateEndTimeFieldText("07:10")
                .ValidateLocationTypeLinkFieldText("Clients or patients home")
                .ValidateLocationLinkFieldText("Provider ACC-1605 " + currentDateTimeString)
                .ValidateLeadProfessionalLinkFieldText(_linkedProfessionalName);

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25441")]
        [Description("Step(s) 7 from the original jira test CDV6-2603")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void DiaryViewSetup_UITestMethod006()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider (Hospital)

            var _providerId = commonMethodsDB.CreateProvider("Provider ACC-1605 " + currentDateTimeString, _teamId, 3);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId, _teamId, "CCT ACC 1605 " + currentDateTimeString, "Testing ACC-1605");

            #endregion

            #region Diary View Setup

            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "DVS Home Visit", currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), 100, 10, 15);

            #endregion

            #region System User

            var _linkedProfessionalSystemUserName = "ACC_1605_User_" + currentDateTimeString;
            var _linkedProfessionalName = "ACC_1605 User_" + currentDateTimeString;
            var _linkedProfessionalSystemUserId = commonMethodsDB.CreateSystemUserRecord(_linkedProfessionalSystemUserName, "ACC_1605", "User_" + currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Recurrence Pattern

            var _recurrencePatternId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 days", 1, 1);

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", _linkedProfessionalSystemUserId, _teamId, _recurrencePatternId, currentDate, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Linked Health Professional

            var _linkedHealthProfessionalId = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_teamId, _diaryViewSetupId, _linkedProfessionalSystemUserId, currentDate, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), _recurrencePatternId, _linkedProfessionalName);

            #endregion

            #region Person

            var _firstName = "Raphael";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;

            var _personID = dbHelper.person.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Waiting List").First();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default", new DateTime(2020, 1, 1), 140000000, 2, true);

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_teamId, "NHS Patient", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_teamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Data Form Community Health Case

            var _dataFormId = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Case Service Type Requested

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(
                _teamId, _personID, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                _caseServiceTypeRequestedid, _dataFormId, _contactSourceId,
                DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");

            var _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(new Guid("05786b1b-a6ae-ed11-83ea-0a7be9a500fe"), _teamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var _communityClinicAppointmentContactTypesId = dbHelper.healthAppointmentContactType.GetByName("Face To Face").First();

            #endregion

            #region Community/Clinic Appointment Contact Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home").First();

            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthDiarySection();

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad()
                .WaitForCalendarToLoad()
                .InsertDate(commonMethodsHelper.GetCurrentDate())
                .ClickCommunityClinicTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CCT ACC 1605 " + currentDateTimeString).TapSearchButton().SelectResultElement(_communityAndClinicTeamId.ToString());

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad()
                .ClickHomeVisitYesNoOption(true)
                .WaitForCalendarToLoad()
                .ClickHealthDiaryDateCell("07:00", DateTime.Now.ToString("yyyy-MM-dd"));

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .ClickRelatedCaseLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_caseNumber).TapSearchButton().SelectResultElement(_caseId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .ClickContactTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Face To Face").TapSearchButton().SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Clients or patients home").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Provider ACC-1605 " + currentDateTimeString).TapSearchButton().SelectResultElement(_providerId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary()
                .TapSaveAndCloseButton();

            healthDiaryViewPage
                .WaitForHealthDiaryViewPageToLoad();

            var healthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_caseId);
            Assert.AreEqual(1, healthAppointments.Count);
            var healthAppointmentId = healthAppointments[0];

            healthDiaryViewPage
                .ValidateHealthAppointmentDisplayed("07:00", DateTime.Now.ToString("yyyy-MM-dd"), healthAppointmentId.ToString())
                .ClickHealthAppointment("07:00", DateTime.Now.ToString("yyyy-MM-dd"), healthAppointmentId.ToString());

            appointmentDialogPopup.WaitForAppointmentDialogPopupToLoad().ClickOpenButton();

            caseHealthAppointmentRecordPage
                .WaitForHealthAppointmentRecordPageToLoadFromHealthDiary(false)
                .ValidateCommunityClinicTeamLinkFieldText("CCT ACC 1605 " + currentDateTimeString)
                .ValidateAppointmentReasonLinkFieldText("Assessment")
                .ValidateResponsibleTeamLinkFieldText("DiaryViewSetup T1")
                .ValidateResponsibleUserLinkFieldText("DiaryViewSetup User1")
                .ValidateContactTypeLinkFieldText("Face To Face")
                .ValidateHomeVisitSelectedOption(true)
                .ValidateStartDateFieldText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDateFieldText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTimeFieldText("07:00")
                .ValidateEndTimeFieldText("07:05")
                .ValidateLocationTypeLinkFieldText("Clients or patients home")
                .ValidateLocationLinkFieldText("Provider ACC-1605 " + currentDateTimeString)
                .ValidateLeadProfessionalLinkFieldText(_linkedProfessionalName);

            #endregion


        }

        #endregion

    }
}


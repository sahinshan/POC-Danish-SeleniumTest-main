using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_HealthAppointments_UITestCases2 : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _recurrencePatternId;
        private Guid _personID;
        private string _firstName;
        private string _lastName;
        private Guid _caseStatusId;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _communityClinicAppointmentContactTypesId;
        private Guid _communityClinicLocationTypesId;
        private Guid _healthAppointmentReasonId;
        public Guid adminUserID1;
        private string _systemUsername;
        private string _systemUserFullName;
        private Guid _systemUserId;
        private DateTime sourceDate;
        private DateTime significantEventDate;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                significantEventDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion  Business Unit

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion Team

                #region System User "HealthAppointmentUser"

                _systemUsername = "HealthAppointmentUser_" + _currentDateSuffix;
                _systemUserFullName = "HealthAppointmentUser " + _currentDateSuffix;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "HealthAppointmentUser", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Appointment_Ethnicity", new DateTime(2020, 1, 1));

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

                #endregion

                #region Health Appointment Reason

                _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Follow Up Appointment", new DateTime(2020, 1, 1), "3", null);

                #endregion

                #region Contact Administrative Category

                _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

                #endregion

                #region Case Service Type Requested

                _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form Community Health Case

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                #endregion

                #region Community/Clinic Appointment Contact Types

                _communityClinicAppointmentContactTypesId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Community_Clinic Appointment Contact Types_Appointment", new DateTime(2020, 1, 1), "3");

                #endregion

                #region Community/Clinic Appointment Location Types

                _communityClinicLocationTypesId = commonMethodsDB.CreateHealthAppointmentLocationType(_careDirectorQA_TeamId, "Health Clinic managed by Voluntary or Private Agents", new DateTime(2020, 1, 1));

                #endregion

                #region Recurrence pattern

                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

                #endregion

                #region Person 1

                _firstName = "Jhon";
                _lastName = DateTime.Now.ToString("yyyyMMdd.HHmmss");
                _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

                #endregion

                #region Data Form Id (Appointments)

                var dataformId = dbHelper.dataForm.GetByName("Appointments").FirstOrDefault();

                #endregion

            }
            catch (Exception ex)
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw ex;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1086

        [TestProperty("JiraIssueID", "ACC-1195")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23585 : Step 1 to Step 7")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointment_UITestMethod001()
        {
            #region Provider (Hospital)

            var _providerId_Hospital = commonMethodsDB.CreateProvider("Hospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);
            var _providerRoomA = dbHelper.providerRoom.CreateProviderRoom("RoomA", _careDirectorQA_TeamId, _providerId_Hospital);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "CCT_" + _currentDateSuffix, "_currentDateSuffix");
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(_careDirectorQA_TeamId, communityClinicTeam, "DVS_" + _currentDateSuffix, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500, _providerId_Hospital);


            #endregion

            #region Clinic Room
            var clinicRoomId = dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _diaryViewSetupId, _providerRoomA,
                _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0));

            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            var _linkedProfessional2 = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            dbHelper.communityClinicRestriction.CreateCommunityClinicRestriction(_diaryViewSetupId, _careDirectorQA_TeamId, 1, _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(23, 55, 0), clinicRoomId, newSystemUserId, "Restrictions");
            #endregion

            #region Community Case record
            var caseDate = new DateTime(2023, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            string communityCaseNumber1 = (string)dbHelper.Case.GetCaseByID(_communityCaseId1, "casenumber")["casenumber"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(communityCaseNumber1, _communityCaseId1.ToString())
                .OpenCaseRecord(_communityCaseId1.ToString(), communityCaseNumber1);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow Up Appointment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:30")
                .InsertEndDate(caseDate.AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("11:30")
                .ClickContactTypeLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Community_Clinic Appointment Contact Types_Appointment")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationTypesLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername2)
                .TapSearchButton()
                .SelectResultElement(newSystemUserId.ToString());


            caseHealthAppointmentRecordPage
                    .WaitForCaseHealthAppointmentRecordPageToLoad()
                    .ClickLocationLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Hospital" + _currentDateSuffix)
                    .TapSearchButton()
                    .SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RoomA")
                .TapSearchButton()
                .SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("07:00")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("08:55")
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("08:55")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("09:10")
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:00")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("09:10")
                .ClickBypassRestrictionsYesOption()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            var caseHealthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_communityCaseId1);
            Assert.AreEqual(1, caseHealthAppointments.Count);

            caseHealthAppointmentsPage
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow Up Appointment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:05")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("09:15")
                .ClickBypassRestrictionsYesOption()
                .ClickContactTypeLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Community_Clinic Appointment Contact Types_Appointment")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationTypesLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername2)
                .TapSearchButton()
                .SelectResultElement(newSystemUserId.ToString());


            caseHealthAppointmentRecordPage
                    .WaitForCaseHealthAppointmentRecordPageToLoad()
                    .ClickLocationLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Hospital" + _currentDateSuffix)
                    .TapSearchButton()
                    .SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RoomA")
                .TapSearchButton()
                .SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Room 'RoomA' has a conflicting appointment at this time. (From 01/02/2023 09:00:00 to 01/02/2023 09:10:00)\r\n" +
                _firstName + " " + _lastName + " has a conflicting appointment at this time. (From 01/02/2023 09:00:00 to 01/02/2023 09:10:00)")
                .TapCloseButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("10:00")
                .InsertEndDate(caseDate.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("11:00")
                .ClickBypassRestrictionsYesOption()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            caseHealthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_communityCaseId1);
            Assert.AreEqual(2, caseHealthAppointments.Count);

        }

        [TestProperty("JiraIssueID", "ACC-1200")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23585 : Step 8")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointment_UITestMethod002()
        {
            #region Provider (Hospital)

            var _providerId_Hospital = commonMethodsDB.CreateProvider("Hospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);
            var _providerRoomA = dbHelper.providerRoom.CreateProviderRoom("RoomA", _careDirectorQA_TeamId, _providerId_Hospital);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "CCT_" + _currentDateSuffix, _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(_careDirectorQA_TeamId, communityClinicTeam, "DVS_" + _currentDateSuffix, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500, _providerId_Hospital);


            #endregion

            #region Clinic Room
            var clinicRoomId = dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _diaryViewSetupId, _providerRoomA,
                _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(21, 30, 0));

            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            dbHelper.communityClinicRestriction.CreateCommunityClinicRestriction(_diaryViewSetupId, _careDirectorQA_TeamId, 1, _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(23, 55, 0), clinicRoomId, newSystemUserId, "Restrictions");
            #endregion

            #region Community Case record
            var caseDate = new DateTime(2023, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            string communityCaseNumber1 = (string)dbHelper.Case.GetCaseByID(_communityCaseId1, "casenumber")["casenumber"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(communityCaseNumber1, _communityCaseId1.ToString())
                .OpenCaseRecord(_communityCaseId1.ToString(), communityCaseNumber1);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow Up Appointment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickContactTypeLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Community_Clinic Appointment Contact Types_Appointment")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationTypesLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents")
                    .TapSearchButton()
                    .SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_systemUsername2)
                .TapSearchButton()
                .SelectResultElement(newSystemUserId.ToString());

            caseHealthAppointmentRecordPage
                    .WaitForCaseHealthAppointmentRecordPageToLoad()
                    .ClickLocationLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery("Hospital" + _currentDateSuffix)
                    .TapSearchButton()
                    .SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RoomA")
                .TapSearchButton()
                .SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.AddDays(1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:05")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("09:35")
                .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be prior to Start Date.")
                .TapOKButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:35")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("09:05")
                .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Start Time cannot be after End Time.")
                .TapOKButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertStartTime("09:30")
                .InsertEndDate(caseDate.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndTime("21:35")
                .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The difference between End Date/Time and Start Date/Time cannot be greater than 12 hours")
                .TapOKButton();
        }

        [TestProperty("JiraIssueID", "ACC-1204")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23585 : Step 9")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointment_UITestMethod003()
        {
            #region Provider (Hospital)

            var _providerId_Hospital = commonMethodsDB.CreateProvider("Hospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);
            var _providerRoomA = dbHelper.providerRoom.CreateProviderRoom("RoomA", _careDirectorQA_TeamId, _providerId_Hospital);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "CCT_" + _currentDateSuffix, _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(_careDirectorQA_TeamId, communityClinicTeam, "DVS_" + _currentDateSuffix, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500, _providerId_Hospital);


            #endregion

            #region Clinic Room
            var clinicRoomId = dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _diaryViewSetupId, _providerRoomA, _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0));

            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            //dbHelper.communityClinicRestriction.CreateCommunityClinicRestriction(_diaryViewSetupId, _careDirectorQA_TeamId, 1, _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(23, 55, 0), clinicRoomId, newSystemUserId, "Restrictions");

            #endregion

            #region Community Case record

            var caseDate = new DateTime(2023, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            string communityCaseNumber1 = (string)dbHelper.Case.GetCaseByID(_communityCaseId1, "casenumber")["casenumber"];
            string communityCaseTitle = (string)dbHelper.Case.GetCaseById(_communityCaseId1, "title")["title"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(communityCaseNumber1, _communityCaseId1.ToString())
                .OpenCaseRecord(_communityCaseId1.ToString(), communityCaseNumber1);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToRecurringHealthAppointmentsPage();

            recurringHealthAppointmentsPage
                .WaitForRecurringHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickHealthAppointmentReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow Up Appointment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickHealthAppointmentContactTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Community_Clinic Appointment Contact Types_Appointment")
                .TapSearchButton()
                .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartTime("09:00")
                .InsertEndTime("09:10")
                .ClickRecurrencePatternLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Occurs every 1 days")
                .TapSearchButton()
                .SelectResultElement(_recurrencePatternId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartRange(caseDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .SelectEndRange("By Occurrence")
                .InsertNumberOfOccurrences("1")
                .ClickLocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hospital" + _currentDateSuffix)
                .TapSearchButton()
                .SelectResultElement(_providerId_Hospital.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickProviderRoomLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RoomA")
                .TapSearchButton()
                .SelectResultElement(_providerRoomA.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartRange(caseDate.ToString("dd'/'MM'/'yyyy"))
                .InsertStartTime("07:00")
                .InsertEndTime("08:55")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartTime("08:55")
                .InsertEndTime("09:10")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Health Appointments cannot occur before the parent Case has started. Please review the Start Date and Time for this Health Appointment.")
                .TapCloseButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartTime("09:00")
                .InsertEndTime("09:10")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateHealthAppointmentReasonLinkText("Follow Up Appointment")
                .ValidateCaseLinkText(communityCaseTitle)
                .ValidateContactTypeLinkText("Community_Clinic Appointment Contact Types_Appointment")
                .ValidateStartTime("09:00")
                .ValidateEndTimeText("09:10")
                .ValidateStartDateText(caseDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndRangeSelectedText("By Occurrence")
                .ValidateFirstAppointmentDateText(caseDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateLastAppointmentDateText(caseDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateOccurrencesText("1")
                .ValidateRecurrencePatternLinkText("Occurs every 1 days")
                .ValidateLocationLinkText("Hospital" + _currentDateSuffix)
                .ValidateRoomLinkText("RoomA")
                .ValidateAmbulancerequired_NoOptionChecked()
                .ClickBackButton();

            recurringHealthAppointmentsPage
                .WaitForRecurringHealthAppointmentsPageToLoad();

            var caseHealthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_communityCaseId1);
            Assert.AreEqual(1, caseHealthAppointments.Count);

            recurringHealthAppointmentsPage
                .ClickNewRecordButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickHealthAppointmentReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow Up Appointment")
                .TapSearchButton()
                .SelectResultElement(_healthAppointmentReasonId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickHealthAppointmentContactTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Community_Clinic Appointment Contact Types_Appointment")
                .TapSearchButton()
                .SelectResultElement(_communityClinicAppointmentContactTypesId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartTime("09:05")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Occurs every 1 days")
                .TapSearchButton()
                .SelectResultElement(_recurrencePatternId.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartRange(caseDate.ToString("dd'/'MM'/'yyyy"))
                .SelectEndRange("By Occurrence")
                .InsertNumberOfOccurrences("1")
                .ClickLocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hospital" + _currentDateSuffix)
                .TapSearchButton()
                .SelectResultElement(_providerId_Hospital.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickProviderRoomLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RoomA")
                .TapSearchButton()
                .SelectResultElement(_providerRoomA.ToString());

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("One of the recurring appointments could not be created. Date: 01/02/2023. Room 'RoomA' has a conflicting appointment at this time. (From 01/02/2023 09:00:00 to 01/02/2023 09:10:00)\r\n" +
                _firstName + " " + _lastName + " has a conflicting appointment at this time. (From 01/02/2023 09:00:00 to 01/02/2023 09:10:00)")
                .TapCloseButton();

            recurringHealthAppointmentRecordPage
                .WaitForRecurringHealthAppointmentRecordPageToLoad()
                .InsertStartTime("09:15")
                .InsertEndTime("10:00")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStartTime("09:15")
                .ValidateEndTimeText("10:00")
                .ClickBackButton();

            recurringHealthAppointmentsPage
                .WaitForRecurringHealthAppointmentsPageToLoad();

            caseHealthAppointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(_communityCaseId1);
            Assert.AreEqual(2, caseHealthAppointments.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2073

        [TestProperty("JiraIssueID", "CDV6-2476")]
        [Description("Automation for the test CDV6-2476 : Step(s) 1 to 7 ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointment_NoHomeVisit_UITestMethod001()
        {
            #region Provider (Hospital)

            var _providerId_Hospital = commonMethodsDB.CreateProvider("Hospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);
            var _providerRoomA = dbHelper.providerRoom.CreateProviderRoom("RoomA", _careDirectorQA_TeamId, _providerId_Hospital);

            #endregion

            #region Community Clinic Team

            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "CCT_" + _currentDateSuffix, "_currentDateSuffix");
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(_careDirectorQA_TeamId, communityClinicTeam, "DVS_NHV_" + _currentDateSuffix, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500, _providerId_Hospital);

            #endregion

            #region Clinic Room
            var clinicRoomId = dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _diaryViewSetupId, _providerRoomA,
                _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0));

            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Create New User WorkSchedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            var _linkedProfessional2 = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);

            #endregion

            #region Community Case record

            var caseDate = new DateTime(2023, 5, 26, 10, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            string communityCaseNumber1 = (string)dbHelper.Case.GetCaseByID(_communityCaseId1, "casenumber")["casenumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(communityCaseNumber1, _communityCaseId1.ToString())
                .OpenCaseRecord(_communityCaseId1.ToString(), communityCaseNumber1);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .TapSaveAndCloseButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 2

            caseHealthAppointmentRecordPage
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate("25/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("25/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 3

            caseHealthAppointmentsPage
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate("26/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("26/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 4

            caseHealthAppointmentsPage
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .InsertStartDate("27/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("27/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickRoomLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 5

            caseHealthAppointmentsPage
                .ValidateNoRecordMessageVisibile(true);

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("Health Appointments");

            advanceSearchPage
               .SelectFilter("1", "Group Booking")
               .SelectOperator("1", "Equals")
               .SelectPicklistRuleValue("1", "Yes");

            advanceSearchPage
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Location")
                .SelectOperator("2", "Equals")
                .ClickRuleValueLookupButton("2");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            var groupBookingAppointments = dbHelper.healthAppointment.GetGroupBookingsByProviderID(_providerId_Hospital);
            Assert.AreEqual(3, groupBookingAppointments.Count);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(groupBookingAppointments[0].ToString())
                .ValidateSearchResultRecordPresent(groupBookingAppointments[1].ToString())
                .ValidateSearchResultRecordPresent(groupBookingAppointments[2].ToString());

            #endregion

            #region Step 7

            advanceSearchPage
                .ClickNewRecordButton_ResultsPage();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoadFromAdvancedSearch()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .ClickGroupBookingYesRadioButton()
                .ClickCommunityClinicTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CCT_" + _currentDateSuffix).TapSearchButton().SelectResultElement(communityClinicTeam.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .InsertStartDate("28/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("28/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .ClickRoomLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(_providerRoomA.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
                .TapSaveAndCloseButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .WaitForResultsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            groupBookingAppointments = dbHelper.healthAppointment.GetGroupBookingsByProviderID(_providerId_Hospital);
            Assert.AreEqual(4, groupBookingAppointments.Count);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(groupBookingAppointments[0].ToString())
                .ValidateSearchResultRecordPresent(groupBookingAppointments[1].ToString())
                .ValidateSearchResultRecordPresent(groupBookingAppointments[2].ToString())
                .ValidateSearchResultRecordPresent(groupBookingAppointments[3].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25564")]
        [Description("Automation for the test CDV6-2476 : Step(s) 8 ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void HealthAppointment_HomeVisit_UITestMethod001()
        {
            #region Provider (Hospital)

            var _providerId_Hospital = commonMethodsDB.CreateProvider("Hospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);
            var _providerRoomA = dbHelper.providerRoom.CreateProviderRoom("RoomA", _careDirectorQA_TeamId, _providerId_Hospital);

            #endregion

            #region Community Clinic Team

            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "CCT_" + _currentDateSuffix, "_currentDateSuffix");

            #endregion

            #region Diary View Setup

            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "DVS_HV_" + _currentDateSuffix, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Clinic Room
            var clinicRoomId = dbHelper.clinicRoom.CreateClinicRoom(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _diaryViewSetupId, _providerRoomA, _recurrencePatternId, new DateTime(2023, 1, 1), new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0));

            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Create New User WorkSchedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2023, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            var _linkedProfessional2 = dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);

            #endregion

            #region Community Case record

            var caseDate = new DateTime(2023, 5, 26, 10, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            string communityCaseNumber1 = (string)dbHelper.Case.GetCaseByID(_communityCaseId1, "casenumber")["casenumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(communityCaseNumber1, _communityCaseId1.ToString())
                .OpenCaseRecord(_communityCaseId1.ToString(), communityCaseNumber1);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToHealthAppointmentsPage();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .TapSaveAndCloseButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 2

            caseHealthAppointmentRecordPage
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickHomeVisitYesRadioButton()
                .InsertStartDate("25/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("25/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("HealthAppointmentUser2_" + _currentDateSuffix).TapSearchButton().SelectResultElement(newSystemUserId);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 3

            caseHealthAppointmentsPage
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickHomeVisitYesRadioButton()
                .InsertStartDate("26/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("26/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("HealthAppointmentUser2_" + _currentDateSuffix).TapSearchButton().SelectResultElement(newSystemUserId);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 4

            caseHealthAppointmentsPage
                .ClickNewRecordButton();

            selectHealthAppointmentTypePopUp
                .WaitForSelectHealthAppointmentTypePopUpToLoad()
                .SelectViewByText("Appointments")
                .TapNextButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickGroupBookingYesRadioButton()
                .ClickhealthAppointmentReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow Up Appointment").TapSearchButton().SelectResultElement(_healthAppointmentReasonId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickHomeVisitYesRadioButton()
                .InsertStartDate("27/05/2023")
                .InsertStartTime("10:00")
                .InsertEndDate("27/05/2023")
                .InsertEndTime("11:00")
                .ClickLocationTypesLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Health Clinic managed by Voluntary or Private Agents").TapSearchButton().SelectResultElement(_communityClinicLocationTypesId.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLocationLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Hospital" + _currentDateSuffix).TapSearchButton().SelectResultElement(_providerId_Hospital.ToString());

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .ClickLeadProfessionalLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("HealthAppointmentUser2_" + _currentDateSuffix).TapSearchButton().SelectResultElement(newSystemUserId);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad()
                .TapSaveAndCloseButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .ClickRefreshButton();

            #endregion

            var groupBookingAppointments = dbHelper.healthAppointment.GetGroupBookingsByProviderID(_providerId_Hospital);
            Assert.AreEqual(3, groupBookingAppointments.Count);

        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

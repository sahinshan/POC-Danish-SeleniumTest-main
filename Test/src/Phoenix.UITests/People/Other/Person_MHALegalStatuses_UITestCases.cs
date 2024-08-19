using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_MHALegalStatuses_UITestCases : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUsername;
        private String _defaultUserFullname;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;

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

                string decodedUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(decodedUsername)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PersonMHALegalStatusUser1
                _systemUsername = "PersonMHALegalStatusUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PersonMHALegalStatus", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private void CreateReferenceDataForRecordData()
        {
            #region Activity Categories                

            _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Significant Event Category
            _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category", new DateTime(2020, 1, 1), _teamId, null, null, null);

            #endregion

            #region Significant Event Sub Category

            if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").Any())
            {
                dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_teamId, "Sub Category 1_2", _significantEventCategoryId, new DateTime(2020, 1, 1), null, null);
            }
            _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").FirstOrDefault();

            #endregion


        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-25004")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Rights and Request for an IMHA and MHA Appeal Case Note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void RightsAndRequestsForAnIMHADndMHAAppealCaseNote_Cloning_UITestMethod01()
        {
            #region Person

            var _firstName = "Selma";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Reference Data for fields

            CreateReferenceDataForRecordData();

            #endregion

            #region Case
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            #region MHA Section

            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _teamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_teamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid RightsAndRequestsForAnIMHAAndMHAAppealID = dbHelper.mhaRightsAndRequests.CreateMHARightsAndRequests(_teamId,
                personID, caseID, new DateTime(2020, 9, 1), personMhaLegalStatusId, _systemUserId);
            Guid RightsAndRequestsForAnIMHAAndMHAAppealCaseNoteID = dbHelper.mhaRightsAndRequestsCaseNote.CreateMHARightsAndRequestsCaseNote(_teamId, _systemUserId,
                personID, "Rights and Request for an IMHA and MHA Appeal Case Note 001", "Rights and Request for an IMHA and MHA Appeal Case Note Description",
                RightsAndRequestsForAnIMHAAndMHAAppealID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), true);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonMHALegalStatusesPage();

            personMHALegalStatusesPage
                .WaitForPersonMHALegalStatusesPageToLoad()
                .OpenPersonMHALegalStatusRecord(personMhaLegalStatusId.ToString());

            personMHALegalStatusRecordPage
                .WaitForPersonMHALegalStatusRecordPageToLoad()
                .NavigateToRightsAndRequestsForAnIMHAAndMHAAppealArea();

            rightsAndRequestsForAnIMHAAndMHAAppealPage
                .WaitForRightsAndRequestsForAnIMHAAndMHAAppealPageToLoad()
                .SelectView("Active MHA Rights and Requests for Appeal")
                .OpenRightsAndRequestForAnIMHAAndMHAAppealRecord(RightsAndRequestsForAnIMHAAndMHAAppealID.ToString());

            rightsAndRequestForAnIMHAAndMHAAppealRecordPage
                .WaitForRightsAndRequestForAnIMHAAndMHAAppealRecordPageToLoad()
                .NavigateToRightsAndRequestForAnIMHAAndMHAAppealCaseNotesArea();

            rightsAndRequestForAnIMHAAndMHAAppealCaseNotesPage
                .WaitForRightsAndRequestForAnIMHAAndMHAAppealCaseNotesPageToLoad()
                .OpenRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecord(RightsAndRequestsForAnIMHAAndMHAAppealCaseNoteID.ToString());

            rightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage
                .WaitForRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Rights and Request for an IMHA and MHA Appeal Case Note 001", fields["subject"]);
            Assert.AreEqual("Rights and Request for an IMHA and MHA Appeal Case Note Description", fields["notes"]);
            Assert.AreEqual(personID.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(RightsAndRequestsForAnIMHAAndMHAAppealCaseNoteID.ToString(), fields["clonedfromid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-20388")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Record of Appeal Case Note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void RecordOfAppealCaseNotes_Cloning_UITestMethod01()
        {
            #region Person

            var _firstName = "Selma";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Reference Data for fields

            CreateReferenceDataForRecordData();

            #endregion

            #region Case
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            #region MHA Section

            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _teamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_teamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid personMhaAppealID = dbHelper.personMhaAppeal.CreatePersonMhaAppeal(_teamId, personID, 2, new DateTime(2020, 9, 1), null);
            Guid recordOfAppealID = dbHelper.mhaRecordOfAppeal.CreateMhaRecordOfAppeal(_teamId, personID, personMhaLegalStatusId, personMhaAppealID, 2, new DateTime(2020, 9, 1), new DateTime(2020, 9, 2));
            Guid recordOfAppealCaseNoteID = dbHelper.mhaRecordOfAppealCaseNote.CreateMhaRecordOfAppealCaseNote(_teamId, _systemUserId, "Record of Appeal Case Note 001", "Record of Appeal Case Note Description",
                recordOfAppealID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), true);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonMHALegalStatusesPage();

            personMHALegalStatusesPage
                .WaitForPersonMHALegalStatusesPageToLoad()
                .OpenPersonMHALegalStatusRecord(personMhaLegalStatusId.ToString());

            personMHALegalStatusRecordPage
                .WaitForPersonMHALegalStatusRecordPageToLoad()
                .NavigateToRecordsOfAppealArea();

            recordsOfAppealPage
                .WaitForRecordsOfAppealPageToLoad()
                .OpenRecordOfAppealRecord(recordOfAppealID.ToString());

            recordOfAppealRecordPage
                .WaitForRecordOfAppealRecordPageToLoad()
                .NavigateToRecordOfAppealCaseNotesArea();

            recordOfAppealCaseNotesPage
                .WaitForRecordOfAppealCaseNotesPageToLoad()
                .OpenRecordOfAppealCaseNoteRecord(recordOfAppealCaseNoteID.ToString());

            recordOfAppealCaseNoteRecordPage
                .WaitForRecordOfAppealCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open
            Assert.AreEqual("Record of Appeal Case Note 001", fields["subject"]);
            Assert.AreEqual("Record of Appeal Case Note Description", fields["notes"]);
            Assert.AreEqual(personID.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(recordOfAppealCaseNoteID.ToString(), fields["clonedfromid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-20389")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Person MHA Legal Status Case Note record(with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonMHALegalStatusCaseNotes_Cloning_UITestMethod01()
        {
            #region Person

            var _firstName = "Selma";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Reference Data for fields

            CreateReferenceDataForRecordData();

            #endregion

            #region Case
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            #region MHA Section

            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _teamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_teamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid personMhaLegalStatusCaseNoteId = dbHelper.personMHALegalStatusCaseNote.CreatePersonMHALegalStatusCaseNote(_teamId, _systemUserId, "Person MHA Legal Status Case Note 001", "Person MHA Legal Status Case Note Description",
                personMhaLegalStatusId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), true);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonMHALegalStatusesPage();

            personMHALegalStatusesPage
                .WaitForPersonMHALegalStatusesPageToLoad()
                .OpenPersonMHALegalStatusRecord(personMhaLegalStatusId.ToString());

            personMHALegalStatusRecordPage
                .WaitForPersonMHALegalStatusRecordPageToLoad()
                .NavigateToPersonMHALegalStatusCaseNotesArea();

            personMHALegalStatusCaseNotesPage
                .WaitForPersonMHALegalStatusCaseNotesPageToLoad()
                .OpenPersonMHALegalStatusCaseNoteRecord(personMhaLegalStatusCaseNoteId.ToString());

            personMHALegalStatusCaseNoteRecordPage
                .WaitForPersonMHALegalStatusCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Person MHA Legal Status Case Note 001", fields["subject"]);
            Assert.AreEqual("Person MHA Legal Status Case Note Description", fields["notes"]);
            Assert.AreEqual(personID.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personMhaLegalStatusCaseNoteId.ToString(), fields["clonedfromid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-20390")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Court Date and Outcome Case Note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CourtDateandOutcomeCaseNotes_Cloning_UITestMethod01()
        {
            #region Person

            var _firstName = "Selma";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Reference Data for fields

            CreateReferenceDataForRecordData();

            #endregion

            #region Case
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            #region MHA Section

            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _teamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_teamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeId = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_teamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeCaseNoteId = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_teamId, _systemUserId,
                "Court Dates and Outcomes Case Notes 001", "Court Dates and Outcomes Case Notes Description", mhaCourtDateAndOutcomeId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId,
                true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), true);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonMHALegalStatusesPage();

            personMHALegalStatusesPage
                .WaitForPersonMHALegalStatusesPageToLoad()
                .OpenPersonMHALegalStatusRecord(personMhaLegalStatusId.ToString());

            personMHALegalStatusRecordPage
                .WaitForPersonMHALegalStatusRecordPageToLoad()
                .NavigateToCourtDatesAndOutcomes();

            courtDatesAndOutcomesPage
                .WaitForCourtDatesAndOutcomesPageToLoad()
                .SelectView("Active Court Dates and Outcomes View")
                .OpenCourtDatesAndOutcomeRecord(mhaCourtDateAndOutcomeId.ToString());

            courtDatesAndOutcomeRecordPage
                .WaitForCourtDatesAndOutcomeRecordPageToLoad()
                .NavigateToCourtDateOutcomeCaseNotesArea();

            courtDatesAndOutcomesCaseNotesPage
                .WaitForCourtDatesAndOutcomesCaseNotesPageToLoad()
                .OpenCourtDateAndOutcomeCaseNoteRecord(mhaCourtDateAndOutcomeCaseNoteId.ToString());

            courtDatesAndOutcomesCaseNoteRecordPage
                .WaitForCourtDatesAndOutcomesCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Court Dates and Outcomes Case Notes 001", fields["subject"]);
            Assert.AreEqual("Court Dates and Outcomes Case Notes Description", fields["notes"]);
            Assert.AreEqual(personID.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(mhaCourtDateAndOutcomeCaseNoteId.ToString(), fields["clonedfromid"].ToString());

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

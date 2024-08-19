using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{

    [TestClass]
    public class Person_Contacts_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _personId;
        private int _personNumber;
        private string _personFullName;
        private Guid _activityPriorityId;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityOutcomeId;
        private Guid _activityReasonTypeId;
        private Guid _eventCategoryId;
        private Guid _eventSubCategoryId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {

            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                var _adminUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

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

                #region Default System User

                _systemUserName = "PersonContactsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Contacts", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateString;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _personFullName = firstName + " " + lastName;

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority("Normal", new DateTime(2022, 1, 1), _teamId);

                #endregion

                #region Activity Category

                _activityCategoryId = commonMethodsDB.CreateActivityCategory("Advice", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Sub Category

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory("Home Support", new DateTime(2021, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome("More information needed", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Reason

                _activityReasonTypeId = commonMethodsDB.CreateActivityReason("Assessment", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Significant Event Category

                _eventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category 1", new DateTime(2020, 1, 1), _teamId, "", null, null, true);

                #endregion

                #region Significant Event Category

                _eventSubCategoryId = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, "Sub Cat 1_2", _eventCategoryId, new DateTime(2022, 1, 1), null, null);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11192")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Person Contact Case Note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonContactCaseNotes_Cloning_UITestMethod01()
        {
            DateTime casenotedate = new DateTime(2021, 7, 5, 8, 25, 0, DateTimeKind.Utc);
            var statusid = 1; //Open
            DateTime significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);

            #region Responsible System User

            var _responsibleUserId = commonMethodsDB.CreateSystemUserRecord("Responsible_User1", "Responsible", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _teamId);

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_teamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_teamId, "Medical Care", new DateTime(2020, 1, 1));

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _teamId);

            #endregion

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Ynys Mon - Local Health Board - Provider", _teamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_teamId, _providerId_Carer, _teamId, "Ynys Mon - Local Health Board - Primary Team", "Created by Health Appointments");
            dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_teamId, _communityAndClinicTeamId, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Community Case record

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_teamId, _personId, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                            _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date, "a relevant directory where a user would be entitled to make thoroughly clear what is",
                            _systemUserId, DateTime.Now.Date, null, null, null);

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_teamId, "Contact Centre", DateTime.Now.Date.AddYears(-1), true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_teamId, "Routine");

            #endregion

            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_teamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Contact Record

            Guid contactID = dbHelper.contact.CreateContact(_teamId, _personId, _contactTypeId, _contactReasonId, _presentingPriorityId,
                                        _contactStatus, _systemUserId, _personId, "person", _personFullName, new DateTime(2020, 9, 1), "Need1", 2, 2);

            #endregion

            #region Contact Case Note

            Guid contactCaseNoteID = dbHelper.contactCaseNote.CreateContactCaseNote(_teamId, _responsibleUserId, "Contact Case Note 001",
                                        "Contact Case Note Description", contactID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                                        _activityReasonTypeId, _activityPriorityId, true, _eventCategoryId, _eventSubCategoryId,
                                        significanteventdate, casenotedate, true);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContactsPage();

            personContactsPage
                .WaitForPersonContactsPageToLoad()
                .OpenContactRecord(contactID.ToString());

            personContactRecordPage
                .WaitForPersonContactRecordPageToLoad()
                .NavigateToContactCaseNotesArea();

            personContactCaseNotesPage
                .WaitForPersonContactCaseNotesPageToLoad()
                .OpenPersonContactCaseNoteRecord(contactCaseNoteID.ToString());

            personContactCaseNoteRecordPage
                .WaitForPersonContactCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");


            Assert.AreEqual("Contact Case Note 001", fields["subject"]);
            Assert.AreEqual("Contact Case Note Description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonTypeId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_responsibleUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(casenotedate.ToLocalTime(), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_eventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_eventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(contactCaseNoteID.ToString(), fields["clonedfromid"].ToString());

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

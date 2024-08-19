using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_PhoneCalls_UITestCases : FunctionalTest
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
        private string _systemUserFullName;
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

                _systemUserName = "PersonPhoneCallUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Phone Call", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Phone Call User1";

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

        [TestProperty("JiraIssueID", "CDV6-11186")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PhoneCalls_Cloning_UITestMethod01()
        {
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

            #region Phone Call Record

            DateTime phonecalldate = new DateTime(2021, 7, 5, 8, 20, 0, DateTimeKind.Utc);
            DateTime significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);

            var personPhoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01 All Fields Setup", "Phone Call 01 description",
                                        _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "965478284", _personId, _personFullName, _teamId,
                                        _responsibleUserId, phonecalldate, _activityReasonTypeId, _activityPriorityId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                                        "person", true, true, true, significanteventdate, _eventCategoryId, _eventSubCategoryId);

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(personPhoneCallID.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phone Call 01 All Fields Setup")
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

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(records[0], "callerid", "phonenumber", "recipientid", "directionid", "statusid",
                "subject", "notes", "regardingid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "phonecalldate", "activitysubcategoryid", "informationbythirdparty", "activityoutcomeid", "iscasenote",
                "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual(_personId.ToString(), fields["callerid"].ToString());
            Assert.AreEqual("965478284", fields["phonenumber"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["recipientid"]);
            Assert.AreEqual(1, fields["directionid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual("Phone Call 01 All Fields Setup", fields["subject"]);
            Assert.AreEqual("Phone Call 01 description", fields["notes"]);
            Assert.AreEqual(_caseId.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonTypeId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_responsibleUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(phonecalldate.ToLocalTime(), ((DateTime)fields["phonecalldate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["iscasenote"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_eventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_eventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personPhoneCallID.ToString(), fields["clonedfromid"].ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11568

        [TestProperty("JiraIssueID", "CDV6-11664")]
        [Description("Automation for test method CDV6-11664 - Trying to save a new record without inserting any information on any field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod01()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ValidateNotificationMessageVisibility(false)

                .ClickSaveButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateCallerErrorLabelVisibility(false)
                .ValidateRecipientErrorLabelVisibility(true)
                .ValidateDirectionErrorLabelVisibility(false)
                .ValidateStatusErrorLabelVisibility(false)
                .ValidateSubjectErrorLabelVisibility(true)
                .ValidateRegardingErrorLabelVisibility(false)
                .ValidateResponsibleTeamErrorLabelVisibility(false)
                .ValidateEventDateErrorLabelVisibility(false)
                .ValidateEventCategoryErrorLabelVisibility(false)

                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateRecipientErrorLabelText("Please fill out this field.")
                .ValidateSubjectErrorLabelText("Please fill out this field.")

                .SelectDirection("")
                .SelectStatus("")
                .ClickRegardingRemoveButton()
                .ClickResponsibleTeamRemoveButton()
                .ClickSignificantEvent_YesRadioButton()
                .ClickSaveButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateRecipientErrorLabelVisibility(false)
                .ValidateDirectionErrorLabelVisibility(true)
                .ValidateStatusErrorLabelVisibility(true)
                .ValidateSubjectErrorLabelVisibility(true)
                .ValidateRegardingErrorLabelVisibility(true)
                .ValidateResponsibleTeamErrorLabelVisibility(true)
                .ValidateEventDateErrorLabelVisibility(true)
                .ValidateEventCategoryErrorLabelVisibility(true)

                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateStatusErrorLabelText("Please fill out this field.")
                .ValidateDirectionErrorLabelText("Please fill out this field.")
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateRegardingErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-11665")]
        [Description("Automation for test method CDV6-11665 - Validate auto populated fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod02()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")
                .ValidateRegardingFieldLinkText(_personFullName)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText(_systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-11666")]
        [Description("Automation for test method CDV6-11666 - Create a record by setting values in all fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod03()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .InsertDescription("line 1\nline 2")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonTypeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickIsCaseNote_YesRadioButton()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickBackButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var newPhoneCallId = records.FirstOrDefault();

            personPhoneCallsPage
                .OpenPersonPhoneCallRecord(newPhoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ValidateCallerFieldLinkText(_systemUserFullName)
                .ValidatePhoneNumber("987654321")
                .ValidateRecipientFieldLinkText(_personFullName)
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")

                .ValidateSubject("Phil Shields 109899 Phone Call 001")
                .ValidateDescription("line 1\r\nline 2")

                .ValidateRegardingFieldLinkText(_personFullName)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateReasonFieldLinkText("Assessment")
                .ValidateResponsibleUserFieldLinkText(_systemUserFullName)
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidatePhoneCallDate("29/07/2021", "08:35")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(true)
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateIsCaseNoteCheckedOption(true)

                .ValidateSignificantEventCheckedOption(false);

        }

        [TestProperty("JiraIssueID", "CDV6-11667")]
        [Description("Automation for test method CDV6-11667 - Open existing record and update all fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod04()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789", _personId, _personFullName, _teamId,
                                null, "person");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertSubject("Phil Shields 109899 Phone Call 001 Updated")
                .InsertDescription("line 1\nline 2")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonTypeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickIsCaseNote_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSignificantEvent_YesRadioButton()
                .InsertEventDate("28/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_eventCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_2").TapSearchButton().SelectResultElement(_eventSubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001 Updated")
                .ValidateCallerFieldLinkText(_systemUserFullName)
                .ValidatePhoneNumber("987654321")
                .ValidateRecipientFieldLinkText(_personFullName)
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")

                .ValidateSubject("Phil Shields 109899 Phone Call 001 Updated")
                .ValidateDescription("line 1\r\nline 2")

                .ValidateRegardingFieldLinkText(_personFullName)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateReasonFieldLinkText("Assessment")
                .ValidateResponsibleUserFieldLinkText(_systemUserFullName)
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidatePhoneCallDate("29/07/2021", "08:35")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(true)
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateIsCaseNoteCheckedOption(true)

                .ValidateSignificantEventCheckedOption(true)
                .InsertEventDate("28/07/2021")
                .ValidateEventCategoryFieldLinkText("Category 1")
                .ValidateEventSubCategoryFieldLinkText("Sub Cat 1_2");

        }

        [TestProperty("JiraIssueID", "CDV6-11668")]
        [Description("Automation for test method CDV6-11668 - Change the status to Completed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod05()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .SelectStatus("Completed")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001");

        }

        [TestProperty("JiraIssueID", "CDV6-11669")]
        [Description("Automation for test method CDV6-11669 - Change the status to Cancelled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod06()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                    _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                    _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .SelectStatus("Cancelled")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001");

        }

        [TestProperty("JiraIssueID", "CDV6-11670")]
        [Description("Automation for test method CDV6-11670 - Set direction to Outgoing and leave Caller field empty")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod07()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneNumber("987654321")
                .SelectDirection("Outgoing")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .InsertDescription("line 1\nline 2")
                .ClickSaveButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")

                .ValidateCallerErrorLabelVisibility(true)
                .ValidateCallerErrorLabelText("Please fill out this field.")

                .ValidateRecipientErrorLabelVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-11671")]
        [Description("Automation for test method CDV6-11671 - Create a new Outgoing phone call record with all fields set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod08()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .InsertDescription("line 1\nline 2")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonTypeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickIsCaseNote_YesRadioButton()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSignificantEvent_YesRadioButton()
                .InsertEventDate("28/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_eventCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_2").TapSearchButton().SelectResultElement(_eventSubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var newPhoneCallId = records.FirstOrDefault();

            personPhoneCallsPage
                .ValidateSubjectCellText(newPhoneCallId.ToString(), "Phil Shields 109899 Phone Call 001")
                .ValidateCallerCellText(newPhoneCallId.ToString(), _systemUserFullName)
                .ValidateRecipientCellText(newPhoneCallId.ToString(), _personFullName)
                .ValidateDirectionCellText(newPhoneCallId.ToString(), "Incoming")
                .ValidatePriorityCellText(newPhoneCallId.ToString(), "Normal")
                .ValidateReasonCellText(newPhoneCallId.ToString(), "Assessment")
                .ValidateRegardingCellText(newPhoneCallId.ToString(), _personFullName)
                .ValidateStatusCellText(newPhoneCallId.ToString(), "In Progress")
                .ValidatePhoneCallNumberCellText(newPhoneCallId.ToString(), "987654321")
                .ValidatePhoneCallDateCellText(newPhoneCallId.ToString(), "29/07/2021 08:35:00")
                .ValidateResponsibleTeamCellText(newPhoneCallId.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(newPhoneCallId.ToString(), _systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-11672")]
        [Description("Automation for test method CDV6-11672 - Validate Significant Event Date fields visibility")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod09()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")

                .ValidateEventDateFieldVisibility(false)
                .ValidateEventCategoryFieldVisibility(false)
                .ValidateEventSubCategoryFieldVisibility(false)

                .ClickSignificantEvent_YesRadioButton()

                .ValidateEventDateFieldVisibility(true)
                .ValidateEventCategoryFieldVisibility(true)
                .ValidateEventSubCategoryFieldVisibility(true)

                .InsertEventDate("28/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_eventCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_2").TapSearchButton().SelectResultElement(_eventSubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New");

        }

        [TestProperty("JiraIssueID", "CDV6-11673")]
        [Description("Automation for test method CDV6-11673 - Validate the creation of the significant event record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod10()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickSignificantEvent_YesRadioButton()
                .InsertEventDate("28/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_eventCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_2").TapSearchButton().SelectResultElement(_eventSubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var newPhoneCallId = records.FirstOrDefault();

            var significantEventRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEventRecords.Count);
            var newSignificantEventRecordId = significantEventRecords.FirstOrDefault();

            var fields = dbHelper.personSignificantEvent.GetPersonSignificantEventByID(newSignificantEventRecordId,
                "ownerid", "owningbusinessunitid", "title", "inactive", "eventdate", "eventdetails", "significanteventcategoryid", "significanteventsubcategoryid"
                , "sourceactivityid", "sourceactivityidtablename", "sourceactivityidname", "iscloned");


            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            StringAssert.Contains((string)fields["title"], "Significant Event for " + _personFullName + " created by " + _systemUserFullName + " on");
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(new DateTime(2021, 07, 28), fields["eventdate"]);
            Assert.AreEqual(false, fields.ContainsKey("eventdetails"));
            Assert.AreEqual(_eventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(_eventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(newPhoneCallId.ToString(), fields["sourceactivityid"].ToString());
            Assert.AreEqual("phonecall", fields["sourceactivityidtablename"]);
            Assert.AreEqual("Phil Shields 109899 Phone Call 001", fields["sourceactivityidname"]);
            Assert.AreEqual(false, fields["iscloned"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11674")]
        [Description("Automation for test method CDV6-11674 - Activate a completed record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod11()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                    _personId, "person", _personFullName, _systemUserId, "systemuser", _personFullName, "123456789",
                                    _personId, _personFullName, _teamId, null, "person");

            var statusId = 2; //Completed
            dbHelper.phoneCall.UpdateStatus(phoneCallId, statusId);

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertSubject("Phil Shields 109899 Phone Call 001 Update") //if we can change the subject then it means the record is reactivated
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-11675")]
        [Description("Automation for test method CDV6-11675 - Create a record using the advance search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod12()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Phone Calls")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();



            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .InsertDescription("line 1\nline 2")
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonTypeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickIsCaseNote_YesRadioButton()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch("New")
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForResultsPageToLoad();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var newPhoneCallId = records.FirstOrDefault();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(newPhoneCallId.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11676")]
        [Description("Automation for test method CDV6-11676 - Search for a record using the advance search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod13()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Phone Calls")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(phoneCallId.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11677")]
        [Description("Automation for test method CDV6-11677 - Validate Timeline information for newly created records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod14()
        {
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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .ClickNewRecordButton();

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("New")
                .InsertSubject("Phil Shields 109899 Phone Call 001")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var newPhoneCallId = records.FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(newPhoneCallId.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11678")]
        [Description("Automation for test method CDV6-11678 - Test Complete button on toolbar")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod15()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCompleteButton()
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ValidateStatus("Completed");

        }

        [TestProperty("JiraIssueID", "CDV6-11679")]
        [Description("Automation for test method CDV6-11679 - Test Cancel button on toolbar")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod16()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCancelButton()
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ValidateStatus("Cancelled");

        }

        [TestProperty("JiraIssueID", "CDV6-11680")]
        [Description("Automation for test method CDV6-11680 - Validate the audit of changes to a record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod17()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCancelButton()
                .WaitForInactivePersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoad("phonecall");

            System.Threading.Thread.Sleep(2000);


            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = phoneCallId.ToString(),
                ParentTypeName = "phonecall",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName, auditResponseData.GridData[0].cols[1].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-11681")]
        [Description("Automation for test method CDV6-11681 - Delete a record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod18()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .SelectPersonPhoneCallRecord(phoneCallId.ToString())
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            var records = dbHelper.phoneCall.GetPhoneCallByRegardingID(_personId);
            Assert.AreEqual(0, records.Count());
        }

        [TestProperty("JiraIssueID", "CDV6-11682")]
        [Description("Automation for test method CDV6-11682 - Update all fields and Save and Close the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod19()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCallerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("System Users").TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertPhoneNumber("987654321")
                .ClickRecipientLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertSubject("Phil Shields 109899 Phone Call 001 Updated")
                .InsertDescription("line 1\nline 2")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonTypeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .InsertPhoneCallDate("29/07/2021", "08:35")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickIsCaseNote_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserFullName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSignificantEvent_YesRadioButton()
                .InsertEventDate("28/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_eventCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_2").TapSearchButton().SelectResultElement(_eventSubCategoryId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallId.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("Phil Shields 109899 Phone Call 001 Updated")
                .ValidateCallerFieldLinkText(_systemUserFullName)
                .ValidatePhoneNumber("987654321")
                .ValidateRecipientFieldLinkText(_personFullName)
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")

                .ValidateSubject("Phil Shields 109899 Phone Call 001 Updated")
                .ValidateDescription("line 1\r\nline 2")

                .ValidateRegardingFieldLinkText(_personFullName)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateReasonFieldLinkText("Assessment")
                .ValidateResponsibleUserFieldLinkText(_systemUserFullName)
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidatePhoneCallDate("29/07/2021", "08:35")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(true)
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateIsCaseNoteCheckedOption(true)

                .ValidateSignificantEventCheckedOption(true)
                .InsertEventDate("28/07/2021")
                .ValidateEventCategoryFieldLinkText("Category 1")
                .ValidateEventSubCategoryFieldLinkText("Sub Cat 1_2")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-11683")]
        [Description("Automation for test method CDV6-11683 - Export records to excel")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonPhoneCalls_UITestMethod20()
        {
            #region Phone Call Record

            var phoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phil Shields 109899 Phone Call 001", "phone call description ...",
                                _personId, "person", _personFullName, _systemUserId, "systemuser", _systemUserFullName, "123456789",
                                _personId, _personFullName, _teamId, null, "person");

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
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .SelectPersonPhoneCallRecord(phoneCallId.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "PhoneCalls.csv");
            Assert.IsTrue(fileExists);
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

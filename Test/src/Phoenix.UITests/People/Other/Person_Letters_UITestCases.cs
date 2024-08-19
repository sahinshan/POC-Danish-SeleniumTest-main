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
    public class Person_Letters_UITestCases : FunctionalTest
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

                #region System User PersonLettersUser1
                _systemUsername = "PersonLettersUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PersonLetters", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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


        [TestProperty("JiraIssueID", "CDV6-11181")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_Cloning_UITestMethod01()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

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

            #region Case
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 0, 0), 20);

            #endregion

            #region Letter
            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            //Create Letter 
            Guid personLetterID = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Letter 01 address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 01 All Fields Setup", "Letter 01 description", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true);

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
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(personLetterID.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 01 All Fields Setup")
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

            var records = dbHelper.letter.GetLetterByRegardingID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.letter.GetLetterByID(records[0], "senderid", "address", "recipientid", "directionid", "statusid",
                "subject", "notes", "regardingid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "letterdate", "activitysubcategoryid", "informationbythirdparty", "activityoutcomeid", "iscasenote",
                "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //In Progress
            Assert.AreEqual(senderID.ToString(), fields["senderid"].ToString());
            Assert.AreEqual("Letter 01 address", fields["address"]);
            Assert.AreEqual(recipientID.ToString(), fields["recipientid"].ToString());
            Assert.AreEqual(1, fields["directionid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual("Letter 01 All Fields Setup", fields["subject"]);
            Assert.AreEqual("Letter 01 description", fields["notes"]);
            Assert.AreEqual(caseID.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid".ToString()]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(date.ToLocalTime(), ((DateTime)fields["letterdate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["iscasenote"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personLetterID.ToString(), fields["clonedfromid"].ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11652

        [TestProperty("JiraIssueID", "CDV6-11584")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen - Click on the Save Button- Validate the Error Message upon saving the Letter")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod01()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickSaveButton()
                .ValidateMessageAreaVisible(true)

                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateRecipientErrorLabelVisible(true)
                .ValidateRecipientMessageAreaText("Please fill out this field.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.");


        }

        [TestProperty("JiraIssueID", "CDV6-11585")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Enter the fields: Direction and Subject - Click on the Save Button- Validate the Error Message upon saving the Letter")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod02()
        {

            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Incoming")
                .InsertSubject("Letter 001")
                .ClickSaveButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateRecipientErrorLabelVisible(true)
                .ValidateRecipientMessageAreaText("Please fill out this field.");



        }

        [TestProperty("JiraIssueID", "CDV6-11586")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Enter all Mandatory fields - Click on the Save Button- Validate the created Letter")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod03()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickRecipientLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny" + " " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Incoming")
                .InsertSubject("Letter 001")
                .ClickSaveButton()
                .ClickBackButton();

            personLettersPage
                .WaitForPersonLettersPageToLoad();



            var letter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(1, letter.Count);
            var personletter = letter.First();


            personLettersPage
              .OpenPersonLetterRecord(personletter.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .ValidateSubjectFieldText("Letter 001")
                .ValidateRecipientLookUpText("Johnny " + _lastName)
                .ValidateDirectionFieldTextValue("1")
                .ValidateStatusFieldText("In Progress");



        }

        [TestProperty("JiraIssueID", "CDV6-11587")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Update the subject  - Click on the Save and close Button- Validate the created Letter")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod04()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            #endregion            

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 20);

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId
                    , _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);


            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());


            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .InsertSubject("Letter 001 updated")
                .ClickSaveAndCloseButton();

            var letter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(1, letter.Count);
            var personletter = letter.First();


            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001 updated")
                .ValidateSubjectFieldText("Letter 001 updated");


        }

        [TestProperty("JiraIssueID", "CDV6-11588")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Select the Direction as Outgoing -" +
           "Enter all the optional and Mandatory fields except Sender- Click on the Save and close Button- Validate the Error Message Displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod05()
        {

            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Outgoing")
                .ClickRecipientLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .InsertAddress("Nottingham")
                  .InsertSubject("Letter 001")
                  .InsertDescription("Person Letter 001")
                  .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("New")
                 .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personLetterRecordPage
              .WaitForPersonLetterRecordPageToLoad("New")
              .ClickSaveAndCloseButton()
              .ValidateMessageAreaVisible(true)
              .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
              .ValidateSenderMessageAreaVisible(true)
              .ValidateSenderMessageAreaText("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "CDV6-11589")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Select the Direction:Outgoing , Status: InProgress,-" +
           "Enter all the optional and Mandatory fields - Click on the Save and close Button- Validate the created record row and the validate the sender field is mandatory ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod06()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();
            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Outgoing")
                .ClickRecipientLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .InsertAddress("Nottingham")
                  .InsertSubject("Letter 001")
                  .InsertDescription("Person Letter 001")
                  .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("New")
                 .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personLetterRecordPage
              .WaitForPersonLetterRecordPageToLoad("New")
              .ValidateSenderMandatoryVisibility(true)
              .ClickSenderLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Ralph " + _lastName).TapSearchButton().SelectResultElement(senderID.ToString());

            personLetterRecordPage
              .WaitForPersonLetterRecordPageToLoad("New")
              .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var letter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(1, letter.Count);



        }

        [TestProperty("JiraIssueID", "CDV6-11590")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Select the Significant Event as Yes-" +
          "Enter all the Significant fields - Click on the Save and close Button- Validate the created record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod07()
        {

            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Incoming")
                .ClickRecipientLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .InsertAddress("Nottingham")
                  .InsertSubject("Letter 001")
                  .InsertDescription("Person Letter 001")
                  .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personLetterRecordPage
                   .WaitForPersonLetterRecordPageToLoad("New")
                   .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("New")
                 .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personLetterRecordPage
               .WaitForPersonLetterRecordPageToLoad("New")
               .ClickSignificantEvent_YesRadioButton()
               .ClickSignificantEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category").TapSearchButton().SelectResultElement(_significantEventCategoryId.ToString());

            personLetterRecordPage
              .WaitForPersonLetterRecordPageToLoad("New")
              .InsertSignificantEventDate("20/07/2021")
              .ClickSignificantEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Category 1_2").TapSearchButton().SelectResultElement(_significantEventSubCategoryId.ToString());

            personLetterRecordPage
             .WaitForPersonLetterRecordPageToLoad("New")
             .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            var letter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(1, letter.Count);

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ValidateRecordPresent(letter[0].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11591")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Change the Status to Completed-" +
          "Update any field  - Validate the user is getting a message(User should not able to do changes when the Status is Completed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod08()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);


            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("Letter 001")
                 .SelectStatus("Completed")
                 .ClickSaveButton();



            var fields = dbHelper.letter.GetLetterID(Letter, "inactive");
            Assert.AreEqual(true, fields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11592")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Change the Status to Cancelled-" +
          "Update any field  - Validate the user is getting a message(User should not able to do changes when the Status is Completed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod09()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "John " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                   .WaitForPersonLetterRecordPageToLoad("Letter 001")
                   .SelectStatus("Cancelled")
                   .ClickSaveButton();

            var fields = dbHelper.letter.GetLetterID(Letter, "inactive");
            Assert.AreEqual(true, fields["inactive"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11593")]
        [Description("Open a person record (person has no Letters linked to it) - Navigate to the Person Letters screen -Select the Significant Event as Yes-" +
          "Enter all the Significant fields - Click on the Save and close Button- Navigate Letter page and Go to Menu-->Other information --->Significant Event and validate the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod10()
        {

            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .ClickNewRecordButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .SelectDirection("Incoming")
                .ClickRecipientLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                  .WaitForPersonLetterRecordPageToLoad("New")
                  .InsertAddress("Nottingham")
                  .InsertSubject("Letter 001")
                  .InsertDescription("Person Letter 001")
                  .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personLetterRecordPage
                   .WaitForPersonLetterRecordPageToLoad("New")
                   .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("New")
                 .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personLetterRecordPage
               .WaitForPersonLetterRecordPageToLoad("New")
               .ClickSignificantEvent_YesRadioButton()
               .ClickSignificantEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category").TapSearchButton().SelectResultElement(_significantEventCategoryId.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("New")
                .InsertSignificantEventDate("20/07/2021")
                .ClickSignificantEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Category 1_2").TapSearchButton().SelectResultElement(_significantEventSubCategoryId.ToString());

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoad("New")
                 .ClickSignificantEvent_NoRadioButton()
                 .ValidateEventDateFieldVisibility(false)
                 .ValidateEventCategoryFieldVisibility(false)
                 .ValidateEventSubCategoryFieldVisibility(false);


        }

        [TestProperty("JiraIssueID", "CDV6-11601")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Select the status to Completed-Click save button" +
          "Click Activate button - Click on the Save and close Button-  validate the record are editable and change the status to In progress and save.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod11()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");


            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                   .WaitForPersonLetterRecordPageToLoad("Letter 001")
                   .SelectStatus("Completed")
                   .ClickSaveButton();

            var fields = dbHelper.letter.GetLetterID(Letter, "inactive");
            Assert.AreEqual(true, fields["inactive"]);


            personLetterRecordPage
                .ClickActivateButton()
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .InsertSubject("Letter 001 updated"); //if the driver can insert text it means that the record is active

            fields = dbHelper.letter.GetLetterID(Letter, "inactive");
            Assert.AreEqual(false, fields["inactive"]);

        }

        [DeploymentItem("Files\\DocToUpload.txt")]
        [DeploymentItem("Files\\Doc2ToUpload.txt")]
        [DeploymentItem("chromedriver.exe")]
        [TestProperty("JiraIssueID", "CDV6-11594")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Validate user able to upload the attachment")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod12()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .ClickFileIcon()
                .ClickFile1UploadDocument(this.TestContext.DeploymentDirectory + "\\DocToUpload.txt")
                .ClickFileUpload()
                .ClickFile1UploadDocument(this.TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt")
                .ClickFileUpload()
                .ValidateLatestFileLink(true)
                .ClickSaveButton();

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .ValidateLatestFileLinkText("Doc2ToUpload.txt");

        }

        [TestProperty("JiraIssueID", "CDV6-11602")]
        [Description("Create a record using the advance search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod13()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Letters")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(personNumber).TapSearchButton().SelectResultElement(personID.ToString());

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .ClickSearchButton()
               .WaitForResultsPageToLoad()
               .ClickNewRecordButton_ResultsPage();


            personLetterRecordPage
               .WaitForPersonLetterRecordPageToLoadFromAdvanceSearch("New")
               .ClickRecipientLookUp();


            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery("Johnny " + _lastName).TapSearchButton().SelectResultElement(recipientID.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoadFromAdvanceSearch("New")
                .SelectDirection("Incoming")
                .InsertSubject("Letter 001");

            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoadFromAdvanceSearch("New")
                 .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(personNumber).TapSearchButton().SelectResultElement(personID.ToString());


            personLetterRecordPage
                 .WaitForPersonLetterRecordPageToLoadFromAdvanceSearch("New")
                 .ClickSaveAndCloseButton();


            advanceSearchPage
               .WaitForResultsPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var records = dbHelper.letter.GetLetterByRegardingID(personID);
            Assert.AreEqual(1, records.Count);
            var newLetterId = records.FirstOrDefault();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(newLetterId.ToString());




        }

        [TestProperty("JiraIssueID", "CDV6-11657")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Letters screen -Validate user able to delete the Letter Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod14()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .ClickDeleteButton();


            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();


            personLettersPage
               .WaitForPersonLettersPageToLoad()
               .ValidateRecordNotVisible(Letter.ToString());

            var personLetter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(0, personLetter.Count);




        }

        [TestProperty("JiraIssueID", "CDV6-11604")]
        [Description("Open a person record (person has a Letters linked to it) - Navigate to the Person Screen   -Validate the Letter Created.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod15()
        {

            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var personLetter = dbHelper.letter.GetLetterByPersonID(personID);
            Assert.AreEqual(1, personLetter.Count);
            var LetterId = personLetter.FirstOrDefault();


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
               .WaitForPersonTimelineSubPageToLoad()
               .ValidateRecordPresent(LetterId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11603")]
        [Description("Search for a record using the advance search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod16()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Letters")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(personNumber).TapSearchButton().SelectResultElement(personID.ToString());

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .ClickSearchButton()
               .WaitForResultsPageToLoad()
               .ValidateSearchResultRecordPresent(Letter.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11656")]
        [Description("Validate the audit of changes to a record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod17()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                .WaitForPersonLettersPageToLoad()
                .OpenPersonLetterRecord(Letter.ToString());

            personLetterRecordPage
                .WaitForPersonLetterRecordPageToLoad("Letter 001")
                .ClickCancelButton()
                 .NavigateToAuditSubPage();

            System.Threading.Thread.Sleep(4000);

            auditListPage
                .WaitForAuditListPageToLoad("letter");

            System.Threading.Thread.Sleep(2000);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = Letter.ToString(),
                ParentTypeName = "letter",
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
            Assert.AreEqual("PersonLetters User1", auditResponseData.GridData[0].cols[1].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-11658")]
        [Description("Export records to excel")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod18()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);

            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                 .WaitForPersonLettersPageToLoad()
                 .SelectPersonLetterRecord(Letter.ToString())
                 .ClickExportToExcelButton();

            exportDataPopup
               .WaitForExportDataPopupToLoad()
               .SelectRecordsToExport("Selected Records")
               .SelectExportFormat("Csv (comma separated with quotes)")
               .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Letters.csv");
            Assert.IsTrue(fileExists);


        }

        [TestProperty("JiraIssueID", "CDV6-11605")]
        [Description("Assign the Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Letters_UITestMethod19()
        {
            #region Person

            var _firstName = "Kristine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();
            var recipientID = commonMethodsDB.CreatePersonRecord("Johnny", _lastName, _ethnicityId, _teamId);
            var senderID = commonMethodsDB.CreatePersonRecord("Ralph", _lastName, _ethnicityId, _teamId);
            var advancedId = commonMethodsDB.CreateTeam("Advanced", null, _businessUnitId, "957618", "AdvancedQA@careworkstempmail.com", "Advanced QA", "040 123456");


            CreateReferenceDataForRecordData();

            DateTime date = new DateTime(2020, 5, 20);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(2020, 5, 21);

            #endregion

            //Create Letter 
            Guid Letter = dbHelper.letter.CreateLetter(senderID.ToString(), "Ralph " + _lastName, "person", "Address", recipientID.ToString(), "Johnny " + _lastName, "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, personID, date, personID, _personFullname, "person", IsSignificantEvent,
                    significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
             .GoToLoginPage()
             .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                 .WaitForPersonLettersPageToLoad()
                 .SelectPersonLetterRecord(Letter.ToString())
                 .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Advanced")
                .TapSearchButton().SelectResultElement(advancedId.ToString());

            assignRecordPopup.SelectResponsibleUserDecision("Do not change").TapOkButton();


            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToLettersPage();

            personLettersPage
                 .WaitForPersonLettersPageToLoad()
                 .ValidateRecordCellText(Letter.ToString(), 2, "Letter 001")
                 .ValidateRecordCellText(Letter.ToString(), 3, "Ralph " + _lastName)
                 .ValidateRecordCellText(Letter.ToString(), 4, "Incoming")
                 .ValidateRecordCellText(Letter.ToString(), 5, "Johnny " + _lastName)
                 .ValidateRecordCellText(Letter.ToString(), 6, "In Progress")
                 .ValidateRecordCellText(Letter.ToString(), 7, "Address")
                 //.ValidateRecordCellText(Letter.ToString(), 8, "")
                 .ValidateRecordCellText(Letter.ToString(), 9, "High")
                 .ValidateRecordCellText(Letter.ToString(), 10, "Assessment")
                 .ValidateRecordCellText(Letter.ToString(), 11, "Advanced");


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
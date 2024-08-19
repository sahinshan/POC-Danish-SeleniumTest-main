using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]
    public class Person_PersonFormLetters_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _firstName;
        private string _lastName;
        private Guid _personId;
        private string _personNumber;
        private Guid _documentId1;
        private Guid _activityPriorityId_Normal;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityOutcomeId;
        string document1Name = "Automation - Person Form 1";
        private Guid personFormID;
        private string personFormTitle;

        [TestInitialize()]
        public void TestsSetupMethod()
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

                #region System User PFLettersUser1

                _systemUsername = "PFLettersUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PFLetters", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                _firstName = "Ralph";
                _lastName = "LN_" + _currentDateString;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

                #endregion

                #region Document

                _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

                #endregion

                #region Person Form
                personFormID = dbHelper.personForm.CreatePersonForm(_teamId, _personId, _documentId1, new DateTime(2021, 2, 19), true);
                personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

                #endregion

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

                _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2019, 1, 1), _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8614

        [TestProperty("JiraIssueID", "CDV6-24892")]
        [Description(
           "Login in the web app - Open a Person Form record - Navigate to the Person Form Letters area - " +
            "Validate that the Person Form Letters page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormLetters_UITestMethod01()
        {
            Guid senderID = commonMethodsDB.CreatePersonRecord("Ryan", _lastName, _ethnicityId, _teamId);
            var personFormLetter1ID = dbHelper.letter.CreateLetter(senderID, "Ryan " + _lastName, "person", "", _personId, "Ralph " + _lastName, "person", 1, "In Progress", "Person Form Letter 01", "Person Form Letter 01 description", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, _personId, new DateTime(2021, 02, 22), personFormID, personFormTitle, "personform", false,
                    null, null, null);
            var personFormLetter2ID = dbHelper.letter.CreateLetter(senderID, "Ryan " + _lastName, "person", "", _personId, "Ralph " + _lastName, "person", 1, "In Progress", "Person Form Letter 02", "", null, _teamId, _systemUserId, null,
                    null, null, null, null, _personId, null, personFormID, personFormTitle, "personform", false,
                    null, null, null);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormLettersArea();

            personFormLettersPage
                .WaitForPersonFormLettersPageToLoad()

                .ValidateSubjectCellText(personFormLetter1ID.ToString(), "Person Form Letter 01")
                .ValidateDirectionCellText(personFormLetter1ID.ToString(), "Incoming")
                .ValidateRecipientCellText(personFormLetter1ID.ToString(), "Ralph " + _lastName)
                .ValidateStatusCellText(personFormLetter1ID.ToString(), "In Progress")
                .ValidateSentReceivedDateCellText(personFormLetter1ID.ToString(), "22/02/2021")
                .ValidateResponsibleTeamCellText(personFormLetter1ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(personFormLetter1ID.ToString(), "PFLetters User1")

                .ValidateSubjectCellText(personFormLetter2ID.ToString(), "Person Form Letter 02")
                .ValidateDirectionCellText(personFormLetter2ID.ToString(), "Incoming")
                .ValidateRecipientCellText(personFormLetter2ID.ToString(), "Ralph " + _lastName)
                .ValidateStatusCellText(personFormLetter2ID.ToString(), "In Progress")
                .ValidateSentReceivedDateCellText(personFormLetter2ID.ToString(), "")
                .ValidateResponsibleTeamCellText(personFormLetter2ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(personFormLetter2ID.ToString(), "PFLetters User1");
        }

        [TestProperty("JiraIssueID", "CDV6-24893")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Letters area - " +
            "Search for Person Form Letter record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormLetters_UITestMethod02()
        {
            Guid senderID = commonMethodsDB.CreatePersonRecord("Ryan", _lastName, _ethnicityId, _teamId);
            var personFormLetter1ID = dbHelper.letter.CreateLetter(senderID, "Ryan " + _lastName, "person", "", _personId, "Ralph " + _lastName, "person", 1, "In Progress", "Person Form Letter 01", "Person Form Letter 01 description", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, _personId, new DateTime(2021, 02, 22), personFormID, personFormTitle, "personform", false,
                    null, null, null);
            var personFormLetter2ID = dbHelper.letter.CreateLetter(senderID, "Ryan " + _lastName, "person", "", _personId, "Ralph " + _lastName, "person", 1, "In Progress", "Person Form Letter 02", "", null, _teamId, _systemUserId, null,
                    null, null, null, null, _personId, null, personFormID, personFormTitle, "personform", false,
                    null, null, null);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormLettersArea();

            personFormLettersPage
                .WaitForPersonFormLettersPageToLoad()
                .SearchPersonFormLetterRecord("Person Form Letter 01")
                .ValidateRecordPresent(personFormLetter1ID.ToString())
                .ValidateRecordNotPresent(personFormLetter2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24894")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Letters area - Open a Person Form Letter record (all fields must have values) - " +
            "Validate that the user is redirected to the Person Form Letters record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormLetters_UITestMethod03()
        {
            Guid senderID = commonMethodsDB.CreatePersonRecord("Ryan", _lastName, _ethnicityId, _teamId);
            var personFormLetter1ID = dbHelper.letter.CreateLetter(senderID, "Ryan " + _lastName, "person", "", _personId, "Ralph " + _lastName, "person", 1, "In Progress", "Person Form Letter 01", "Person Form Letter 01 description", null, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, _personId, new DateTime(2021, 02, 22), personFormID, personFormTitle, "personform", false,
                    null, null, null);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormLettersArea();

            personFormLettersPage
                .WaitForPersonFormLettersPageToLoad()
                .OpenPersonFormLetterRecord(personFormLetter1ID.ToString());

            personFormLetterRecordPage
                .WaitForPersonFormLetterRecordPageToLoad("Person Form Letter 01")

                .ValidateRecipient("Ralph " + _lastName)
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")
                .ValidateSubject("Person Form Letter 01")
                .ValidateDescription("Person Form Letter 01 description")

                .ValidateRegardingFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateSentReceivedDate("22/02/2021")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("PFLetters User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateOutcomeFieldLinkText("More information needed")

                .ValidateSignificantEventCheckedOption(false);
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

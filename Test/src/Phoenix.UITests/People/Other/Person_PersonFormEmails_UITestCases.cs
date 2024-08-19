using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]
    public class Person_PersonFormEmails_UITestCases : FunctionalTest
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
        private List<Guid> userSecProfiles;

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

                #region System User PFEmailUser1

                _systemUsername = "PFEmailUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PFEmail", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                _firstName = "John";
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
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private void ActivityReferenceDataSetup()
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

            _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2019, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8614


        [TestProperty("JiraIssueID", "CDV6-24889")]
        [Description(
           "Login in the web app - Open a Person Form record - Navigate to the Person Form Emails area - " +
            "Validate that the Person Form Emails page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormEmails_UITestMethod01()
        {
            #region Activity Reason

            _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            Guid personFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personId, _systemUserId, _systemUserId, "PFEmail User1", "systemuser", personFormID, "personform",
                 personFormTitle, "Person Form Email 01", "Note 01", 2, new DateTime(2021, 02, 22, 14, 00, 00), _activityReasonId, null, null, null, null);
            Guid personFormEmail2ID = dbHelper.email.CreateEmail(_teamId, _personId, _systemUserId, _systemUserId, "PFEmail User1", "systemuser", personFormID, "personform",
                 personFormTitle, "Person Form Email 02", "Note 02", 2);

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
                .NavigateToPersonFormEmailsArea();

            personFormEmailsPage
                .WaitForPersonFormEmailsPageToLoad()

                .ValidateSubjectCellText(personFormEmail1ID.ToString(), "Person Form Email 01")
                .ValidateRegardingCellText(personFormEmail1ID.ToString(), personFormTitle)
                .ValidateResponsibleTeamCellText(personFormEmail1ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(personFormEmail1ID.ToString(), "PFEmail User1")
                .ValidateDueCellText(personFormEmail1ID.ToString(), "22/02/2021 14:00:00")
                .ValidateStatusCellText(personFormEmail1ID.ToString(), "Sent")
                .ValidateReasonCellText(personFormEmail1ID.ToString(), "Assessment")

                .ValidateSubjectCellText(personFormEmail2ID.ToString(), "Person Form Email 02")
                .ValidateRegardingCellText(personFormEmail2ID.ToString(), personFormTitle)
                .ValidateResponsibleTeamCellText(personFormEmail2ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(personFormEmail2ID.ToString(), "PFEmail User1")
                .ValidateDueCellText(personFormEmail2ID.ToString(), "")
                .ValidateStatusCellText(personFormEmail2ID.ToString(), "Sent")
                .ValidateReasonCellText(personFormEmail2ID.ToString(), "");
        }

        [TestProperty("JiraIssueID", "CDV6-24890")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Emails area - " +
            "Search for Person Form Email record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormEmails_UITestMethod02()
        {
            #region Activity Reason

            _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            Guid personFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personId, _systemUserId, _systemUserId, "PFEmail User1", "systemuser", personFormID, "personform",
                 personFormTitle, "Person Form Email 01", "Note 01", 2, new DateTime(2021, 02, 22, 14, 00, 00), _activityReasonId, null, null, null, null);
            Guid personFormEmail2ID = dbHelper.email.CreateEmail(_teamId, _personId, _systemUserId, _systemUserId, "PFEmail User1", "systemuser", personFormID, "personform",
                 personFormTitle, "Person Form Email 02", "Note 02", 2);

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
                .NavigateToPersonFormEmailsArea();

            personFormEmailsPage
                .WaitForPersonFormEmailsPageToLoad()
                .SearchPersonFormEmailRecord("Person Form Email 01")
                .ValidateRecordPresent(personFormEmail1ID.ToString())
                .ValidateRecordNotPresent(personFormEmail2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24891")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Emails area - Open a Person Form Email record (all fields must have values) - " +
            "Validate that the user is redirected to the Person Form Emails record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormEmails_UITestMethod03()
        {
            ActivityReferenceDataSetup();

            Guid personFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personId, _systemUserId, _systemUserId, "PFEmail User1", "systemuser", personFormID, "personform",
                 personFormTitle, "Person Form Email 01", "Person Form Email 01 description", 2, new DateTime(2021, 02, 22, 14, 00, 00), _activityReasonId, _activityPriorityId_Normal, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);

            userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };

            var EmailToSystemUserId = commonMethodsDB.CreateSystemUserRecord("ALBERTEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            dbHelper.emailTo.CreateEmailTo(personFormEmail1ID, EmailToSystemUserId, "systemuser", "ALBERT Einstein");

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
                .NavigateToPersonFormEmailsArea();

            personFormEmailsPage
                .WaitForPersonFormEmailsPageToLoad()
                .OpenPersonFormEmailRecord(personFormEmail1ID.ToString());

            personFormEmailRecordPage
                .WaitForPersonFormEmailRecordPageToLoad("Person Form Email 01")

                .ValidateFrom("PFEmail User1")
                .ValidateTo(EmailToSystemUserId.ToString(), "ALBERT Einstein\r\nRemove")
                .ValidateSubject("Person Form Email 01")
                .ValidateDescription("Person Form Email 01 description")

                .ValidateRegardingFieldLinkText(personFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateDue("22/02/2021", "14:00")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText("PFEmail User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateStatus("Sent")

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

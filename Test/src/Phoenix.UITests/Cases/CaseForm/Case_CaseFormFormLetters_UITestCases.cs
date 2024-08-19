using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("Files\\Testing CDV6-8614.Zip"), DeploymentItem("chromedriver.exe")]
    public class Case_CaseFormFormLetters_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _documentId;
        private Guid _ethnicityId;
        private Guid _personID;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _dataFormId;
        private Guid _caseFormId;
        private string _caseFormTitle;
        private Guid _activityPriorityId;

        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityOutcomeId;
        private Guid _activityReasonId;

        #endregion

        [TestInitialize]
        public void TestInitializationMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

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

                #region System User

                _systemUserName = "Case_Form_Letters_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseFormLetters", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "fullname")["fullname"];

                #endregion

                #region Document 

                var DocumentExist = dbHelper.document.GetDocumentByName("Testing CDV6-8614").Any();
                if (!DocumentExist)
                {
                    var documentByteArray1 = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\Testing CDV6-8614.Zip");

                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray1, "Testing CDV6-8614.Zip");

                    _documentId = dbHelper.document.GetDocumentByName("Testing CDV6-8614").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published 
                }

                if (_documentId == Guid.Empty)
                    _documentId = dbHelper.document.GetDocumentByName("Testing CDV6-8614").FirstOrDefault();

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Person

                var firstName = "Person";
                var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fullName = firstName + " " + lastName;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                #endregion

                #region Case Form

                int assessmentstatusid = 1; //In Progress
                var assessmentStartDate = new DateTime(2021, 3, 19);
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_teamId, _personID, fullName, _systemUserId, _caseId, _caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
                _caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"]);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority("Low", new DateTime(2022, 1, 1), _teamId);

                #endregion

                #region Activity Category

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Sub Category

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2021, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2021, 1, 1), _teamId);

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

        [TestProperty("JiraIssueID", "CDV6-9945")]
        [Description("Login in the web app - Open a Case Form record - Navigate to the Case Form Letters area - " +
            "Validate that the Case Form Letters page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormLetters_UITestMethod01()
        {
            #region System User CaseFormLetterUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Letter

            Guid caseFormLetter1ID = dbHelper.letter.CreateLetter("", "", "", "", AaronKirk_SystemUserID.ToString(), "Aaron Kirk", "systemuser", 1, "1", "Case Form Letter 01", "Case Form Letter 01 description", _caseId, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _personID, new DateTime(2021, 2, 22), _caseFormId, _caseFormTitle, "caseform", false, null, null, null);

            Guid caseFormLetter2ID = dbHelper.letter.CreateLetter("", "", "", "", AaronKirk_SystemUserID.ToString(), "Aaron Kirk", "systemuser", 1, "1", "Case Form Letter 02", "", _caseId, _teamId, _systemUserId, null,
                    null, null, null, null, _personID, null, _caseFormId, _caseFormTitle, "caseform", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormLettersArea();

            caseFormLettersPage
                .WaitForCaseFormLettersPageToLoad()

                .ValidateSubjectCellText(caseFormLetter1ID.ToString(), "Case Form Letter 01")
                .ValidateDirectionCellText(caseFormLetter1ID.ToString(), "Incoming")
                .ValidateRecipientCellText(caseFormLetter1ID.ToString(), "Aaron Kirk")
                .ValidateStatusCellText(caseFormLetter1ID.ToString(), "In Progress")
                .ValidateSentReceivedDateCellText(caseFormLetter1ID.ToString(), "22/02/2021")
                .ValidateResponsibleTeamCellText(caseFormLetter1ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(caseFormLetter1ID.ToString(), _systemUserFullName)

                .ValidateSubjectCellText(caseFormLetter2ID.ToString(), "Case Form Letter 02")
                .ValidateDirectionCellText(caseFormLetter2ID.ToString(), "Incoming")
                .ValidateRecipientCellText(caseFormLetter2ID.ToString(), "Aaron Kirk")
                .ValidateStatusCellText(caseFormLetter2ID.ToString(), "In Progress")
                .ValidateSentReceivedDateCellText(caseFormLetter2ID.ToString(), "")
                .ValidateResponsibleTeamCellText(caseFormLetter2ID.ToString(), "CareDirector QA")
                .ValidateResponsibleUserCellText(caseFormLetter2ID.ToString(), _systemUserFullName);
        }

        [TestProperty("JiraIssueID", "CDV6-9946")]
        [Description("Login in the web app - Open a Case Form record - Navigate to the Case Form Letters area - " +
            "Search for Case Form Letter record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormLetters_UITestMethod02()
        {
            #region System User CaseFormLetterUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Letter

            Guid caseFormLetter1ID = dbHelper.letter.CreateLetter("", "", "", "", AaronKirk_SystemUserID.ToString(), "Aaron Kirk", "systemuser", 1, "1", "Case Form Letter 01", "Case Form Letter 01 description", _caseId, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _personID, new DateTime(2021, 2, 22), _caseFormId, _caseFormTitle, "caseform", false, null, null, null);

            Guid caseFormLetter2ID = dbHelper.letter.CreateLetter("", "", "", "", AaronKirk_SystemUserID.ToString(), "Aaron Kirk", "systemuser", 1, "1", "Case Form Letter 02", "", _caseId, _teamId, _systemUserId, null,
                    null, null, null, null, _personID, null, _caseFormId, _caseFormTitle, "caseform", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormLettersArea();

            caseFormLettersPage
                .WaitForCaseFormLettersPageToLoad()
                .SearchCaseFormLetterRecord("Case Form Letter 01")
                .ValidateRecordPresent(caseFormLetter1ID.ToString())
                .ValidateRecordNotPresent(caseFormLetter2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9947")]
        [Description("Login in the web app - Open a Case Form record - Navigate to the Case Form Letters area - Open a Case Form Letter record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Letters record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormLetters_UITestMethod03()
        {
            #region System User CaseFormLetterUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Letter

            Guid caseFormLetter1ID = dbHelper.letter.CreateLetter("", "", "", "", AaronKirk_SystemUserID.ToString(), "Aaron Kirk", "systemuser", 1, "1", "Case Form Letter 01", "Case Form Letter 01 description", _caseId, _teamId, _systemUserId, _activityCategoryId,
                    _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _personID, new DateTime(2021, 2, 22), _caseFormId, _caseFormTitle, "caseform", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormLettersArea();

            caseFormLettersPage
                .WaitForCaseFormLettersPageToLoad()
                .OpenCaseFormLetterRecord(caseFormLetter1ID.ToString());

            caseFormLetterRecordPage
                .WaitForCaseFormLetterRecordPageToLoad("Case Form Letter 01")

                .ValidateRecipient("Aaron Kirk")
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")
                .ValidateSubject("Case Form Letter 01")
                .ValidateDescription("Case Form Letter 01 description")

                .ValidateRegardingFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Low")
                .ValidateSentReceivedDate("22/02/2021")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateResponsibleUserFieldLinkText(_systemUserFullName)
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

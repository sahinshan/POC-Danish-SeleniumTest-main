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
    public class Case_CaseFormFormPhoneCalls_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _businessUnitId;
        private Guid _languageId;
        private string _teamName;
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

                _teamName = "CareDirector QA";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User

                _systemUserName = "Case_Form_Phone_Calls_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseFormPhoneCalls", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
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


        [TestProperty("JiraIssueID", "CDV6-9948")]
        [Description(
           "Login in the web app - Open a Case Form record - Navigate to the Case Form Phone Calls area - " +
            "Validate that the Case Form Phone Calls page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormPhoneCalls_UITestMethod01()
        {
            #region System User CaseFormPhoneCallsUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var AlbertEinstein_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AlbertEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Phone Call

            Guid caseFormPhoneCall1ID = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_teamId, _systemUserId, "Case Form Phone Call 01", _caseFormId, _caseFormTitle, "caseform", "Case Form Phone Call 01 description", _activityCategoryId, _activitySubCategoryId,
                _activityOutcomeId, _activityReasonId, _activityPriorityId, AaronKirk_SystemUserID, "systemuser", "Aaron Kirk", 1, new DateTime(2021, 02, 21, 13, 00, 00), 1, _personID, _caseId);

            Guid caseFormPhoneCall2ID = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_teamId, _systemUserId, "Case Form Phone Call 02", _caseFormId, _caseFormTitle, "caseform", "", null, null,
                null, null, null, AlbertEinstein_SystemUserID, "systemuser", "ALBERT Einstein", 1, null, 1, _personID, _caseId);

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
                .NavigateToCaseFormPhoneCallsArea();

            caseFormPhoneCallsPage
                .WaitForCaseFormPhoneCallsPageToLoad()

                .ValidateSubjectCellText(caseFormPhoneCall1ID.ToString(), "Case Form Phone Call 01")
                .ValidateRecipientCellText(caseFormPhoneCall1ID.ToString(), "Aaron Kirk")
                .ValidateDirectionCellText(caseFormPhoneCall1ID.ToString(), "Incoming")
                .ValidateStatusCellText(caseFormPhoneCall1ID.ToString(), "In Progress")
                .ValidatePhoneCallDateCellText(caseFormPhoneCall1ID.ToString(), "21/02/2021 13:00:00")
                .ValidateResponsibleTeamCellText(caseFormPhoneCall1ID.ToString(), _teamName)
                .ValidateResponsibleUserCellText(caseFormPhoneCall1ID.ToString(), _systemUserFullName)

                .ValidateSubjectCellText(caseFormPhoneCall2ID.ToString(), "Case Form Phone Call 02")
                .ValidateRecipientCellText(caseFormPhoneCall2ID.ToString(), "ALBERT Einstein")
                .ValidateDirectionCellText(caseFormPhoneCall2ID.ToString(), "Incoming")
                .ValidateStatusCellText(caseFormPhoneCall2ID.ToString(), "In Progress")
                .ValidatePhoneCallDateCellText(caseFormPhoneCall2ID.ToString(), "")
                .ValidateResponsibleTeamCellText(caseFormPhoneCall2ID.ToString(), _teamName)
                .ValidateResponsibleUserCellText(caseFormPhoneCall2ID.ToString(), _systemUserFullName);
        }

        [TestProperty("JiraIssueID", "CDV6-9949")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Phone Calls area - " +
            "Search for Case Form Phone Call record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormPhoneCalls_UITestMethod02()
        {
            #region System User CaseFormPhoneCallsUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var AlbertEinstein_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AlbertEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Phone Call

            Guid caseFormPhoneCall1ID = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_teamId, _systemUserId, "Case Form Phone Call 01", _caseFormId, _caseFormTitle, "caseform", "Case Form Phone Call 01 description", _activityCategoryId, _activitySubCategoryId,
                _activityOutcomeId, _activityReasonId, _activityPriorityId, AaronKirk_SystemUserID, "systemuser", "Aaron Kirk", 1, new DateTime(2021, 02, 21, 13, 00, 00), 1, _personID, _caseId);

            Guid caseFormPhoneCall2ID = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_teamId, _systemUserId, "Case Form Phone Call 02", _caseFormId, _caseFormTitle, "caseform", "", null, null,
                null, null, null, AlbertEinstein_SystemUserID, "systemuser", "ALBERT Einstein", 1, null, 1, _personID, _caseId);

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
                .NavigateToCaseFormPhoneCallsArea();

            caseFormPhoneCallsPage
                .WaitForCaseFormPhoneCallsPageToLoad()
                .SearchCaseFormPhoneCallRecord("Case Form Phone Call 01")
                .ValidateRecordPresent(caseFormPhoneCall1ID.ToString())
                .ValidateRecordNotPresent(caseFormPhoneCall2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9950")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Phone Calls area - Open a Case Form Phone Call record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Phone Calls record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormPhoneCalls_UITestMethod03()
        {
            #region System User CaseFormPhoneCallsUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Phone Call

            Guid caseFormPhoneCall1ID = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_teamId, _systemUserId, "Case Form Phone Call 01", _caseFormId, _caseFormTitle, "caseform", "Case Form Phone Call 01 description", _activityCategoryId, _activitySubCategoryId,
                _activityOutcomeId, _activityReasonId, _activityPriorityId, AaronKirk_SystemUserID, "systemuser", "Aaron Kirk", 1, new DateTime(2021, 02, 21, 13, 00, 00), 1, _personID, _caseId);

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
                .NavigateToCaseFormPhoneCallsArea();

            caseFormPhoneCallsPage
                .WaitForCaseFormPhoneCallsPageToLoad()
                .OpenCaseFormPhoneCallRecord(caseFormPhoneCall1ID.ToString());

            caseFormPhoneCallRecordPage
                .WaitForCaseFormPhoneCallRecordPageToLoad("Case Form Phone Call 01")

                .ValidateRecipient("Aaron Kirk")
                .ValidateDirection("Incoming")
                .ValidateStatus("In Progress")
                .ValidateSubject("Case Form Phone Call 01")
                .ValidateDescription("Case Form Phone Call 01 description")

                .ValidateRegardingFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Low")
                .ValidatePhoneCallDate("21/02/2021", "13:00")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText(_teamName)
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

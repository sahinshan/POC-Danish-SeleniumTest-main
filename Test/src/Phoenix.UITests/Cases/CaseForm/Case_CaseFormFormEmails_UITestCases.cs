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
    public class Case_CaseFormFormEmails_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private Guid _documentId;
        private Guid _ethnicityId;
        private Guid _personID;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _closedCaseStatusId;
        private Guid _contactReasonId;
        private Guid _dataFormId;
        private Guid _contactSourceId;
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CaseFormEmail BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CaseFormEmail Team", null, _businessUnitId, "907678", "CaseFormEmailTeam@careworkstempmail.com", "CaseFormEmail Team", "020 123456");

                #endregion

                #region System User CaseFormEmailsUser1

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("CaseFormEmailsUser1", "CaseFormEmails", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

                var firstName = "Pat";
                var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fullName = firstName + " " + lastName;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

                #endregion

                #region Case Status

                _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Anonymus", _teamId);

                #endregion

                #region Case

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];


                #endregion

                #region Case Form

                int assessmentstatusid = 1; //In Progress
                var assessmentStartDate = new DateTime(2021, 3, 19);
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_teamId, _personID, fullName, _systemUserId, _caseId, _caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
                _caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"]);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1cd32346-9d45-e911-a2c5-005056926fe4"), "Low", new DateTime(2022, 1, 1), _teamId);

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


        [TestProperty("JiraIssueID", "CDV6-9942")]
        [Description(
           "Login in the web app - Open a Case Form record - Navigate to the Case Form Emails area - " +
            "Validate that the Case Form Emails page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormEmails_UITestMethod01()
        {
            #region Case Form Email 1

            var duedate = new DateTime(2021, 2, 22, 14, 0, 0); ;
            var caseFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId,
                _systemUserId, "CaseFormEmails User1", "systemuser",
                _caseFormId, "caseform", _caseFormTitle,
                "Case Form Email 01", "<p>Case Form Email 01 description</p>", 1, duedate,
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);

            #endregion

            #region Case Form Email 2

            var caseFormEmail2ID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId,
                _systemUserId, "CaseFormEmails User1", "systemuser",
                _caseFormId, "caseform", _caseFormTitle,
                "Case Form Email 02", "<p>Case Form Email 01 description</p>", 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormEmailsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

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
                .NavigateToCaseFormEmailsArea();

            caseFormEmailsPage
                .WaitForCaseFormEmailsPageToLoad()

                .ValidateSubjectCellText(caseFormEmail1ID.ToString(), "Case Form Email 01")
                .ValidateRegardingCellText(caseFormEmail1ID.ToString(), _caseFormTitle)
                .ValidateResponsibleTeamCellText(caseFormEmail1ID.ToString(), "CaseFormEmail Team")
                .ValidateResponsibleUserCellText(caseFormEmail1ID.ToString(), "CaseFormEmails User1")
                .ValidateDueCellText(caseFormEmail1ID.ToString(), "22/02/2021 14:00:00")
                .ValidateReasonCellText(caseFormEmail1ID.ToString(), "Assessment")

                .ValidateSubjectCellText(caseFormEmail2ID.ToString(), "Case Form Email 02")
                .ValidateRegardingCellText(caseFormEmail2ID.ToString(), _caseFormTitle)
                .ValidateResponsibleTeamCellText(caseFormEmail2ID.ToString(), "CaseFormEmail Team")
                .ValidateResponsibleUserCellText(caseFormEmail2ID.ToString(), "CaseFormEmails User1")
                .ValidateDueCellText(caseFormEmail2ID.ToString(), "")
                .ValidateReasonCellText(caseFormEmail2ID.ToString(), "");
        }

        [TestProperty("JiraIssueID", "CDV6-9943")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Emails area - " +
            "Search for Case Form Email record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormEmails_UITestMethod02()
        {
            #region Case Form Email 1

            var duedate = new DateTime(2021, 2, 22, 14, 0, 0); ;
            var caseFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId,
                _systemUserId, "CaseFormEmails User1", "systemuser",
                _caseFormId, "caseform", _caseFormTitle,
                "Case Form Email 01", "<p>Case Form Email 01 description</p>", 1, duedate,
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);

            #endregion

            #region Case Form Email 2

            var caseFormEmail2ID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId,
                _systemUserId, "CaseFormEmails User1", "systemuser",
                _caseFormId, "caseform", _caseFormTitle,
                "Case Form Email 02", "<p>Case Form Email 01 description</p>", 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormEmailsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

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
                .NavigateToCaseFormEmailsArea();

            caseFormEmailsPage
                .WaitForCaseFormEmailsPageToLoad()
                .SearchCaseFormEmailRecord("Case Form Email 01")
                .ValidateRecordPresent(caseFormEmail1ID.ToString())
                .ValidateRecordNotPresent(caseFormEmail2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9944")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Emails area - Open a Case Form Email record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Emails record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormEmails_UITestMethod03()
        {
            #region System User 

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var AlbertEinstein_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AlbertEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Case Form Email 1

            var duedate = new DateTime(2021, 2, 22, 14, 0, 0); ;
            var caseFormEmail1ID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId,
                AaronKirk_SystemUserID, "Aaron Kirk", "systemuser",
                _caseFormId, "caseform", _caseFormTitle,
                "Case Form Email 01", "<p>Case Form Email 01 description</p>", 1, duedate,
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);

            #endregion

            #region Email To

            dbHelper.emailTo.CreateEmailTo(caseFormEmail1ID, AlbertEinstein_SystemUserID, "systemuser", "ALBERT Einstein");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CaseFormEmailsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

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
                .NavigateToCaseFormEmailsArea();

            caseFormEmailsPage
                .WaitForCaseFormEmailsPageToLoad()
                .OpenCaseFormEmailRecord(caseFormEmail1ID.ToString());

            caseFormEmailRecordPage
                .WaitForCaseFormEmailRecordPageToLoad("Case Form Email 01")

                .ValidateFrom("Aaron Kirk")
                .ValidateTo(AlbertEinstein_SystemUserID.ToString(), "ALBERT Einstein\r\nRemove")
                .ValidateSubject("Case Form Email 01")
                .ValidateDescription("<p>Case Form Email 01 description</p>")

                .ValidateRegardingFieldLinkText(_caseFormTitle)
                .ValidateReasonFieldLinkText("Assessment")
                .ValidatePriorityFieldLinkText("Low")
                .ValidateDue("22/02/2021", "14:00")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateResponsibleTeamFieldLinkText("CaseFormEmail Team")
                .ValidateResponsibleUserFieldLinkText("CaseFormEmails User1")
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")

                .ValidateSignificantEventCheckedOption(false);
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

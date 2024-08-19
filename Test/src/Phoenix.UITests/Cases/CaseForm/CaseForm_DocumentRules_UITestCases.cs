using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Cases.CaseForm
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\D_Flag.zip")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    [DeploymentItem("Files\\Automated UI Test Document 1.zip")]
    [DeploymentItem("Files\\Automated UI Test Document 1 Rules.zip")]
    [DeploymentItem("Files\\Person Automation Form 1.Zip")]
    [DeploymentItem("Files\\Automated UI Test Document 2.Zip")]
    public class CaseForm_DocumentRules_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private Guid _documentId;
        private string _documentName;
        private Guid _ethnicityId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _dataFormId;
        private Guid _contactSourceId;


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

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Document Rules BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Document Rules T1", null, _businessUnitId, "907678", "DocumentRulesT1@careworkstempmail.com", "Document Rules T1", "020 123456");

                #endregion

                #region System User

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("CaseFormDocRulesUser1", "CaseFormDocRules", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                var CW_Forms_Test_User_2_UserId = commonMethodsDB.CreateSystemUserRecord("CW_Forms_Test_User_2", "CW Forms Test", "User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Security Profile

                var CW_Activities_PCBU_Edit_securityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Activities (PCBU Edit)").First();

                #endregion

                #region User Security Profile

                commonMethodsDB.CreateUserSecurityProfile(CW_Forms_Test_User_2_UserId, CW_Activities_PCBU_Edit_securityProfileId);

                #endregion

                #region Document

                commonMethodsDB.CreateDocumentIfNeeded("D_Flag", "D_Flag.Zip"); //Import Document

                commonMethodsDB.ImportFormula("Sum two values_minus a third.Zip"); //Formula Import

                commonMethodsDB.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-10345", "WF Automated Testing - CDV6-10345.Zip"); //Workflow Import

                _documentName = "Automated UI Test Document 1";
                _documentId = commonMethodsDB.CreateDocumentIfNeeded(_documentName, "Automated UI Test Document 1.Zip");//Import Document

                var _personAutomationForm1Id = commonMethodsDB.CreateDocumentIfNeeded("Person Automation Form 1", "Person Automation Form 1.Zip");//Import Document

                commonMethodsDB.ImportDocumentRules("Automated UI Test Document 1 Rules.zip");//Import Document Rules

                var _document2Id = commonMethodsDB.CreateDocumentIfNeeded("Automated UI Test Document 2", "Automated UI Test Document 2.Zip");

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team")[0];

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



            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        #region Rule Type Test Cases

        [TestProperty("JiraIssueID", "CDV6-6042")]
        [Description("Automated UI Test 0001 - Validation for the Client Side Rule Type - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 1 - Rule Type Testing - Client Side - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0001()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 1 - Rule Type Testing - Client Side - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Rule Activated - UI Testing - 1 - Rule Type Testing - Client Side - 0001");

        }

        [TestProperty("JiraIssueID", "CDV6-6043")]
        [Description("Automated UI Test 0002 - Validation for the Server Side Rule Type - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 2 - Rule Type Testing - Server Side - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0002()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 2 - Rule Type Testing - Server Side - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Rule Activated - UI Testing - 2 - Rule Type Testing - Server Side - 0001");

        }

        #endregion

        #region Rule Events Test Cases

        [TestProperty("JiraIssueID", "CDV6-6044")]
        [Description("Automated UI Test 0003 - Validation for the Load Document Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 3 - Rule Events Testing - Load Document - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated (WF Short Field should be emptied)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0003()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 3 - Rule Events Testing - Load Document - 0001")
                .TapSaveButton()
                .ValidateWFShortAnswer("");

        }


        [TestProperty("JiraIssueID", "CDV6-6045")]
        [Description("Automated UI Test 0004 - Validation for the Load Section Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 4 - Rule Events Testing - Load Section - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Side rule is activated (Section 1.1 should be hidden)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0004()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 4 - Rule Events Testing - Load Section - 0001")
                .TapSaveButton()
                .WaitForSection1_1Removed();

        }


        [TestProperty("JiraIssueID", "CDV6-6046")]
        [Description("Automated UI Test 0005 - Validation for the Change Field Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 5 - Rule Events Testing - Change Field - 0001' on the WF Short Answer textbox - " +
            "Set a value in the 'GA CMS Field (Write Back) Test 1 [CaseForm ReviewDate]' question - " +
            "Validate that the Client Side rule is activated (section question 'Gui-PersonDOB' should be hidden)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0005()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 5 - Rule Events Testing - Change Field - 0001")
                .InsertIGACMSFieldWriteBackTest1Value("01/08/2019")
                .TapLeftMenuSection1()
                .WaitForGuiPersonDOBQuestionNotVisible();

        }


        [TestProperty("JiraIssueID", "CDV6-6047")]
        [Description("Automated UI Test 0006 - Validation for the Before Saving Document Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 6 - Rule Events Testing - Before Saving Document - 0001' on the WF Short Answer textbox - " +
            "Tap the Save button - Validate that the user is prevented from saving the record (question WF Decimal should be set as mandatory)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0006()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 6 - Rule Events Testing - Before Saving Document - 0001")
                .TapSaveButton()
                .ValidateNotificationAreaVisible("Some data is not correct. Please review the data in the Form.")
                .WaitForWFDecimalErrorLabelVisible("Please fill out this field.");

        }


        [TestProperty("JiraIssueID", "CDV6-6048")]
        [Description("Automated UI Test 0007 - Validation for the After Saving Document Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 7 - Rule Events Testing - After Saving Document - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Rule is activated by the After Saving Document Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0007()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 7 - Rule Events Testing - After Saving Document - 0001")
                .TapSaveButton(false)
                .ValidateNotificationAreaVisible("UI Testing - 7 - Rule Events Testing - After Saving Document - 0001 - Condition Activated")
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("There are unsaved changes on this form, continuing will disregard the changes. Would you like to continue?");


        }


        [TestProperty("JiraIssueID", "CDV6-6049")]
        [Description("Automated UI Test 0008 - Validation for the On Complete of Section Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 8 - Rule Events Testing - On Complete of Section - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Rule is activated by the On Complete of Section Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0008()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 8 - Rule Events Testing - On Complete of Section - 0001")
                .InsertWFDecimalValue("10")
                .TapSaveButton();

            automatedUITestDocument1EditAssessmentPage
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton()
                .WaitForDinamicDialogeToOpen("Complete Section", "Complete operation have been cancelled by OnComplete Rules")
                .DinamicDialogeTapOKButton()
                .WaitForDinamicDialogeToClose("Complete Section", "Complete operation have been cancelled by OnComplete Rules");

        }


        [TestProperty("JiraIssueID", "CDV6-6050")]
        [Description("Automated UI Test 0008.1 - Validation for the On Complete of Section Rule Event - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 8 - Rule Events Testing - On Complete of Section - 0002' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Server Rule is NOT activated by the On Complete of Section Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0008_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 8 - Rule Events Testing - On Complete of Section - 0002")
                .InsertWFDecimalValue("10")
                .TapSaveButton()
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton()
                .WaitForSection1SectionCompletedMessageDisplayed();

        }


        [TestProperty("JiraIssueID", "CDV6-6051")]
        [Description("Automated UI Test 0009 - Validation for the On Complete of Assessment Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Rule is activated by the On Complete of Assessment Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0009()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0001")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveButton()
                .WaitForDinamicDialogeToOpen("Complete Assessment", "UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0001 - Cancel Save Activated")
                .DinamicDialogeTapOKButton()
                .WaitForDinamicDialogeToClose("Complete Assessment", "UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0001 - Cancel Save Activated")
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                ;

        }


        [TestProperty("JiraIssueID", "CDV6-6052")]
        [Description("Automated UI Test 0009.1 - Validation for the On Complete of Assessment Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Rule is NOT activated by the On Complete of Assessment Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0009_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 9 - Rule Events Testing - On Complete of Assessment - 0002")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

        }


        [TestProperty("JiraIssueID", "CDV6-6053")]
        [Description("Automated UI Test 0010 - Validation for the On Close of Assessment Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 10 - Rule Events Testing - On Close of Assessment - 0001' on the WF Short Answer textbox" +
            "Set the Value '10.91' in the WF Decimal question - Set the Value '4' in the WF Numeric question" +
            "Tap the save button - Validate that the Server Rule is activated by the On Close of Assessment Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0010()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 10 - Rule Events Testing - On Close of Assessment - 0001")
                .InsertWFDecimalValue("10.91")
                .InsertWFNumericValue("4")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Closed")
                .WaitForCompletionDetailsAreaToBeVisible()
                .TapSignedOffByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CaseFormDocRulesUser1")
                .TapSearchButton()
                .SelectResultElement(_systemUserId);

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertSignedOffDate(DateTime.Now.ToString("dd/MM/yyyy"))
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Close operation have been cancelled by OnClose Rules")
                .TapCloseButton();

            caseFormPage
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.");
        }


        [TestProperty("JiraIssueID", "CDV6-6054")]
        [Description("Automated UI Test 0010.1 - Validation for the On Close of Assessment Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 10 - Rule Events Testing - On Close of Assessment - 0001' on the WF Short Answer textbox" +
            "Set the Value '7' in the WF Decimal question - Set the Value '4' in the WF Numeric question" +
            "Tap the save button - Validate that the Server Rule is NOT activated by the On Close of Assessment Event")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0010_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 10 - Rule Events Testing - On Close of Assessment - 0001")
                .InsertWFDecimalValue("7")
                .InsertWFNumericValue("4")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Closed")
                .WaitForCompletionDetailsAreaToBeVisible()
                .TapSignedOffByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CaseFormDocRulesUser1")
                .TapSearchButton()
                .SelectResultElement(_systemUserId);

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertSignedOffDate(DateTime.Now.ToString("dd/MM/yyyy"))
                .TapSaveButton()
                .WaitForRecordToBeClosedAndSaved();

            caseFormPage
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();
        }


        [TestProperty("JiraIssueID", "CDV6-6055")]
        [Description("Automated UI Test 0011 - Validation for the Print Document Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 11 - Rule Events Testing - Print Document - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Tap the Print button and wait for the popup to load - Print the entire assessment" +
            "Validate that the Client Rule is activated by the Print Document Event (WF Date question should not exist in the printed document )")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0011()
        {
            commonMethodsDB.CreateSystemSetting("Form.PrintFormat", "Word", "desc ...", false, null);

            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 11 - Rule Events Testing - Print Document - 0001")
                .TapSaveButton()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectTemplate("Templace Will All Questions")
                .TapOnPrintButton()
                .TapOnCloseButton();

            System.Threading.Thread.Sleep(4000);

            fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "TemplaceWithAllQuestions.docx");
            msWordHelper.ValidateWordsNotPresent(DownloadsDirectory + "\\TemplaceWithAllQuestions.docx", "WF Date");
            msWordHelper.ValidateWordsPresent(DownloadsDirectory + "\\TemplaceWithAllQuestions.docx", "WF Lookup");
        }


        [TestProperty("JiraIssueID", "CDV6-6056")]
        [Description("Automated UI Test 0012 - Validation for the Print Section Rule Event - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 12 - Rule Events Testing - Print Section - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Tap the Print button and wait for the popup to load - Print the entire assessment" +
            "Validate that the Client Rule is activated by the Print Section Event (WF Lookup question should not exist in the printed document )")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0012()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 12 - Rule Events Testing - Print Section - 0001")
                .TapSaveButton()
                .TapSection1MenuButton()
                .TapSection1PrintButton();

            printAssessmentPopup
                .WaitForPopupToLoadedForSection1()
                .SelectSection("   Section 1")
                .TapOnPrintButton()
                .TapOnCloseButton();

            System.Threading.Thread.Sleep(4000);

            fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Section 1 - TemplaceWithAllQuestions.docx");
            msWordHelper.ValidateWordsPresent(DownloadsDirectory + "\\Section 1 - TemplaceWithAllQuestions.docx", "WF Date");
            msWordHelper.ValidateWordsNotPresent(DownloadsDirectory + "\\Section 1 - TemplaceWithAllQuestions.docx", "WF Lookup");

        }

        #endregion

        #region Rule Conditions Testing

        #region Date Question



        [TestProperty("JiraIssueID", "CDV6-6057")]
        [Description("Automated UI Test 0013 - Validation for the Date Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0013()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6058")]
        [Description("Automated UI Test 0013.1 - Validation for the Date Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0001' on the WF Short Answer textbox" +
            "Set a value in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0013_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0001")
                .InsertWFDateValue("01/08/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        [TestProperty("JiraIssueID", "CDV6-6059")]
        [Description("Automated UI Test 0014 - Validation for the Date Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0002' on the WF Short Answer textbox" +
            "Set a value in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0014()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0002")
                .InsertWFDateValue("01/08/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6060")]
        [Description("Automated UI Test 0014.1 - Validation for the Date Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0002' on the WF Short Answer textbox" +
            "Set NO value in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0014_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0002")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6061")]
        [Description("Automated UI Test 0015 - Validation for the Date Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0003' on the WF Short Answer textbox" +
            "Set the value '01/09/2019' in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0015()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0003")
                .InsertWFDateValue("01/09/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6062")]
        [Description("Automated UI Test 0015.1 - Validation for the Date Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0003' on the WF Short Answer textbox" +
            "Set the value '15/08/2019' in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0015_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0003")
                .InsertWFDateValue("15/08/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
               .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6063")]
        [Description("Automated UI Test 0016 - Validation for the Date Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0004' on the WF Short Answer textbox" +
            "Set the value '14/08/2019' in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0016()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0004")
                .InsertWFDateValue("14/08/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6064")]
        [Description("Automated UI Test 0016.1 - Validation for the Date Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0004' on the WF Short Answer textbox" +
            "Set the value '15/08/2019' in the WF Date Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0016_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0004")
                .InsertWFDateValue("15/08/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6065")]
        [Description("Automated UI Test 0017 - Validation for the Date Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0005' on the WF Short Answer textbox" +
           "Set the value '17/08/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0017()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0005")
                .InsertWFDateValue("17/08/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6066")]
        [Description("Automated UI Test 0017.1 - Validation for the Date Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0005' on the WF Short Answer textbox" +
           "Set the value '16/08/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0017_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0005")
                .InsertWFDateValue("16/08/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6067")]
        [Description("Automated UI Test 0018 - Validation for the Date Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0006' on the WF Short Answer textbox" +
           "Set the value '20/08/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0018()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0006")
                .InsertWFDateValue("20/08/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6068")]
        [Description("Automated UI Test 0018.1 - Validation for the Date Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0006' on the WF Short Answer textbox" +
           "Set the value '19/08/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0018_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0006")
                .InsertWFDateValue("19/08/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }





        [TestProperty("JiraIssueID", "CDV6-6069")]
        [Description("Automated UI Test 0019 - Validation for the Date Question & Is Less Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0007' on the WF Short Answer textbox" +
           "Set the value '09/07/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0019()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0007")
                .InsertWFDateValue("09/07/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0007 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6070")]
        [Description("Automated UI Test 0019.1 - Validation for the Date Question & Is Less Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0007' on the WF Short Answer textbox" +
           "Set the value '10/07/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0019_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0007")
                .InsertWFDateValue("10/07/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6071")]
        [Description("Automated UI Test 0020 - Validation for the Date Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0008' on the WF Short Answer textbox" +
           "Set the value '05/07/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0020()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0008")
                .InsertWFDateValue("05/07/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 13 - Rule Conditions Testing - Date Question - 0008 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6072")]
        [Description("Automated UI Test 0020.1 - Validation for the Date Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 13 - Rule Conditions Testing - Date Question - 0008' on the WF Short Answer textbox" +
           "Set the value '06/07/2019' in the WF Date Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0020_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 13 - Rule Conditions Testing - Date Question - 0008")
                .InsertWFDateValue("06/07/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        #endregion

        #region Decimal Question


        [TestProperty("JiraIssueID", "CDV6-6073")]
        [Description("Automated UI Test 0021 - Validation for the Decimal Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0001' on the WF Short Answer textbox" +
            "Set the value '12.3' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0021()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0001")
                .InsertWFDecimalValue("12.3")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6074")]
        [Description("Automated UI Test 0021.1 - Validation for the Decimal Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0001' on the WF Short Answer textbox" +
            "Set the value '12.2' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0021_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0001")
                .InsertWFDecimalValue("12.2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6075")]
        [Description("Automated UI Test 0022 - Validation for the Decimal Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0002' on the WF Short Answer textbox" +
            "Set the value '8' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0022()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0002")
                .InsertWFDecimalValue("8")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6076")]
        [Description("Automated UI Test 0022.1 - Validation for the Decimal Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0002' on the WF Short Answer textbox" +
            "Set the value '7.3' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0022_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0002")
                .InsertWFDecimalValue("7.3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6077")]
        [Description("Automated UI Test 0023 - Validation for the Decimal Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0023()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6078")]
        [Description("Automated UI Test 0023.1 - Validation for the Decimal Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0003' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0023_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0003")
                .InsertWFDecimalValue("5")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6079")]
        [Description("Automated UI Test 0024 - Validation for the Decimal Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0004' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0024()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0004")
                .InsertWFDecimalValue("5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6080")]
        [Description("Automated UI Test 0024.1 - Validation for the Decimal Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0024_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6081")]
        [Description("Automated UI Test 0025 - Validation for the Decimal Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005' on the WF Short Answer textbox" +
            "Set the value '1' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0025()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005")
                .InsertWFDecimalValue("1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6082")]
        [Description("Automated UI Test 0025.1 - Validation for the Decimal Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005' on the WF Short Answer textbox" +
            "Set the value '3' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0025_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005")
                .InsertWFDecimalValue("3")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6083")]
        [Description("Automated UI Test 0025.2 - Validation for the Decimal Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0025_2()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005")
                .InsertWFDecimalValue("5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6084")]
        [Description("Automated UI Test 0025.3 - Validation for the Decimal Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005' on the WF Short Answer textbox" +
            "Set the value '0.9' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0025_3()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005")
                .InsertWFDecimalValue("0.9")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        [TestProperty("JiraIssueID", "CDV6-6085")]
        [Description("Automated UI Test 0025.4 - Validation for the Decimal Question & Between rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005' on the WF Short Answer textbox" +
           "Set the value '5.1' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0025_4()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0005")
                .InsertWFDecimalValue("5.1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6086")]
        [Description("Automated UI Test 0026 - Validation for the Decimal Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006' on the WF Short Answer textbox" +
            "Set the value '5.1' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0026()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006")
                .InsertWFDecimalValue("5.1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6087")]
        [Description("Automated UI Test 0026.1 - Validation for the Decimal Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006' on the WF Short Answer textbox" +
            "Set the value '9.7' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0026_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006")
                .InsertWFDecimalValue("9.7")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6088")]
        [Description("Automated UI Test 0026.2 - Validation for the Decimal Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006' on the WF Short Answer textbox" +
            "Set the value '6.8' in the WF Decimal Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0026_2()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0006")
                .InsertWFDecimalValue("6.8")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6089")]
        [Description("Automated UI Test 0027 - Validation for the Decimal Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0007' on the WF Short Answer textbox" +
           "Set the value '4' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0027()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0007")
                .InsertWFDecimalValue("4")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0007 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6090")]
        [Description("Automated UI Test 0027.1 - Validation for the Decimal Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0007' on the WF Short Answer textbox" +
           "Set the value '3' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0027_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0007")
                .InsertWFDecimalValue("3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6091")]
        [Description("Automated UI Test 0028 - Validation for the Decimal Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0008' on the WF Short Answer textbox" +
           "Set the value '6' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0028()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0008")
                .InsertWFDecimalValue("6")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0008 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6092")]
        [Description("Automated UI Test 0028.1 - Validation for the Decimal Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0008' on the WF Short Answer textbox" +
           "Set the value '5.9' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0028_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0008")
                .InsertWFDecimalValue("5.9")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6093")]
        [Description("Automated UI Test 0029 - Validation for the Decimal Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0009' on the WF Short Answer textbox" +
           "Set the value '7.5' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0029()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0009")
                .InsertWFDecimalValue("7.5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0009 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6094")]
        [Description("Automated UI Test 0029.1 - Validation for the Decimal Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0009' on the WF Short Answer textbox" +
           "Set the value '7.51' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0029_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0009")
                .InsertWFDecimalValue("7.51")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6095")]
        [Description("Automated UI Test 0030 - Validation for the Decimal Question & Is Less Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0010' on the WF Short Answer textbox" +
           "Set the value '9.8' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0030()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0010")
                .InsertWFDecimalValue("9.8")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0010 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6096")]
        [Description("Automated UI Test 0030.1 - Validation for the Decimal Question & Is Less Than To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0010' on the WF Short Answer textbox" +
           "Set the value '9.9' in the WF Decimal Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0030_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 14 - Rule Conditions Testing - Decimal Question - 0010")
                .InsertWFDecimalValue("9.9")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        #endregion

        #region Multiple Choice


        [TestProperty("JiraIssueID", "CDV6-6097")]
        [Description("Automated UI Test 0031 - Validation for the Multiple Choice Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Option 2' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0031()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0001")
                .SelectWFMultipleChoice(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6098")]
        [Description("Automated UI Test 0031.1 - Validation for the Multiple Choice Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Option 1' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0031_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0001")
                .SelectWFMultipleChoice(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6099")]
        [Description("Automated UI Test 0032 - Validation for the Multiple Choice Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Option 2' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0032()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0002")
                .SelectWFMultipleChoice(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6100")]
        [Description("Automated UI Test 0032.1 - Validation for the Multiple Choice Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Option 1' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0032_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0002")
                .SelectWFMultipleChoice(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6101")]
        [Description("Automated UI Test 0033 - Validation for the Multiple Choice Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0033()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6102")]
        [Description("Automated UI Test 0033.1 - Validation for the Multiple Choice Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'Option 1' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0033_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0003")
                .SelectWFMultipleChoice(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6103")]
        [Description("Automated UI Test 0034 - Validation for the Multiple Choice Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'Option 2' in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0034()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0004")
                .SelectWFMultipleChoice(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6104")]
        [Description("Automated UI Test 0034.1 - Validation for the Multiple Choice Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Multiple Choice Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0034_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 15 - Rule Conditions Testing - Multiple Choice Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        #endregion

        #region Multiple Response


        [TestProperty("JiraIssueID", "CDV6-6105")]
        [Description("Automated UI Test 0035 - Validation for the Multiple Response Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Day 2' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0035()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0001")
                .SelectWFMultipleResponse(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6106")]
        [Description("Automated UI Test 0035.1 - Validation for the Multiple Response Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Day 1' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0035_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0001")
                .SelectWFMultipleResponse(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6107")]
        [Description("Automated UI Test 0036 - Validation for the Multiple Response Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Day 2' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0036()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0002")
                .SelectWFMultipleResponse(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6108")]
        [Description("Automated UI Test 0036.1 - Validation for the Multiple Response Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Day 1' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0036_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0002")
                .SelectWFMultipleResponse(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6109")]
        [Description("Automated UI Test 0037 - Validation for the Multiple Response Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0037()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6110")]
        [Description("Automated UI Test 0037.1 - Validation for the Multiple Response Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'Day 1' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0037_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0003")
                .SelectWFMultipleResponse(1)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6111")]
        [Description("Automated UI Test 0038 - Validation for the Multiple Response Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'Day 2' in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0038()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0004")
                .SelectWFMultipleResponse(2)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6112")]
        [Description("Automated UI Test 0038.1 - Validation for the Multiple Response Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Multiple Response Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0038_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 16 - Rule Conditions Testing - Multiple Response Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        #endregion

        #region Lookup Question


        [TestProperty("JiraIssueID", "CDV6-6113")]
        [Description("Automated UI Test 0039 - Validation for the Lookup Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Lookup Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0039()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6114")]
        [Description("Automated UI Test 0039.1 - Validation for the Lookup Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0003' on the WF Short Answer textbox" +
            "Set a value in the WF Lookup Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0039_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0003")
                .TapWFLookupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(personNumber)
                .TapSearchButton()
                .SelectResultElement(personID);

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6115")]
        [Description("Automated UI Test 0040 - Validation for the Lookup Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0004' on the WF Short Answer textbox" +
            "Set a value in the WF Lookup Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0040()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0004")
                .TapWFLookupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(personNumber)
                .TapSearchButton()
                .SelectResultElement(personID);

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6116")]
        [Description("Automated UI Test 0040.1 - Validation for the Lookup Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Lookup Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0040_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 17 - Rule Conditions Testing - Lookup Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        #endregion

        #region Numeric Question


        [TestProperty("JiraIssueID", "CDV6-6117")]
        [Description("Automated UI Test 0041 - Validation for the Numeric Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0001' on the WF Short Answer textbox" +
            "Set the value '7' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0041()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0001")
                .InsertWFNumericValue("7")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6118")]
        [Description("Automated UI Test 0041.1 - Validation for the Numeric Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0001' on the WF Short Answer textbox" +
            "Set the value '62' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0041_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0001")
                .InsertWFNumericValue("6")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6119")]
        [Description("Automated UI Test 0042 - Validation for the Numeric Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0042()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004")
                .InsertWFNumericValue("5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6120")]
        [Description("Automated UI Test 0042.1 - Validation for the Numeric Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0042_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6121")]
        [Description("Automated UI Test 0043 - Validation for the Numeric Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0043()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6122")]
        [Description("Automated UI Test 0043.1 - Validation for the Numeric Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0003' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0043_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0003")
                .InsertWFNumericValue("5")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6123")]
        [Description("Automated UI Test 0044 - Validation for the Numeric Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0044()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004")
                .InsertWFNumericValue("5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6124")]
        [Description("Automated UI Test 0044.1 - Validation for the Numeric Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0044_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6125")]
        [Description("Automated UI Test 0045 - Validation for the Numeric Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005' on the WF Short Answer textbox" +
            "Set the value '2' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0045()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005")
                .InsertWFNumericValue("2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6126")]
        [Description("Automated UI Test 0045.1 - Validation for the Numeric Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005' on the WF Short Answer textbox" +
            "Set the value '3' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0045_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005")
                .InsertWFNumericValue("3")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5957")]
        [Description("Automated UI Test 0045.2 - Validation for the Numeric Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005' on the WF Short Answer textbox" +
            "Set the value '5' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0045_2()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005")
                .InsertWFNumericValue("5")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5958")]
        [Description("Automated UI Test 0045.3 - Validation for the Numeric Question & Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005' on the WF Short Answer textbox" +
            "Set the value '1' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0045_3()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005")
                .InsertWFNumericValue("1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        [TestProperty("JiraIssueID", "CDV6-5959")]
        [Description("Automated UI Test 0045.4 - Validation for the Numeric Question & Between rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005' on the WF Short Answer textbox" +
           "Set the value '6' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0045_4()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0005")
                .InsertWFNumericValue("6")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-5960")]
        [Description("Automated UI Test 0046 - Validation for the Numeric Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006' on the WF Short Answer textbox" +
            "Set the value '4' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0046()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006")
                .InsertWFNumericValue("4")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5961")]
        [Description("Automated UI Test 0046.1 - Validation for the Numeric Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006' on the WF Short Answer textbox" +
            "Set the value '9' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0046_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006")
                .InsertWFNumericValue("9")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5962")]
        [Description("Automated UI Test 0046.2 - Validation for the Numeric Question & Not Between rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006' on the WF Short Answer textbox" +
            "Set the value '6' in the WF Numeric Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0046_2()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0006")
                .InsertWFNumericValue("6")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5963")]
        [Description("Automated UI Test 0047 - Validation for the Numeric Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0007' on the WF Short Answer textbox" +
           "Set the value '40' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0047()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0007")
                .InsertWFNumericValue("40")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0007 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5964")]
        [Description("Automated UI Test 0047.1 - Validation for the Numeric Question & Is Greater Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0007' on the WF Short Answer textbox" +
           "Set the value '39' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0047_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0007")
                .InsertWFNumericValue("39")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5965")]
        [Description("Automated UI Test 0048 - Validation for the Numeric Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0008' on the WF Short Answer textbox" +
           "Set the value '16' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0048()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0008")
                .InsertWFNumericValue("16")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0008 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5966")]
        [Description("Automated UI Test 0048.1 - Validation for the Numeric Question & Is Greater Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0008' on the WF Short Answer textbox" +
           "Set the value '15' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0048_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0008")
                .InsertWFNumericValue("15")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5967")]
        [Description("Automated UI Test 0049 - Validation for the Numeric Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0009' on the WF Short Answer textbox" +
           "Set the value '31' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0049()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0009")
                .InsertWFNumericValue("31")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0009 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5968")]
        [Description("Automated UI Test 0049.1 - Validation for the Numeric Question & Is Less Than Or Equal To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0009' on the WF Short Answer textbox" +
           "Set the value '32' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0049_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0009")
                .InsertWFNumericValue("32")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5969")]
        [Description("Automated UI Test 0050 - Validation for the Numeric Question & Is Less Than rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0010' on the WF Short Answer textbox" +
           "Set the value '49' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0050()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0010")
                .InsertWFNumericValue("49")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0010 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5970")]
        [Description("Automated UI Test 0050.1 - Validation for the Numeric Question & Is Less Than To rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0010' on the WF Short Answer textbox" +
           "Set the value '50' in the WF Numeric Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0050_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 18 - Rule Conditions Testing - Numeric Question - 0010")
                .InsertWFNumericValue("50")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        #endregion

        #region Paragraph Question


        [TestProperty("JiraIssueID", "CDV6-5971")]
        [Description("Automated UI Test 0051 - Validation for the Paragraph Question & Does Not Contains Data rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0001' on the WF Short Answer textbox" +
           "Set NO value in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0051()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5972")]
        [Description("Automated UI Test 0051.1 - Validation for the Paragraph Question & Does Not Contains Data rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0001' on the WF Short Answer textbox" +
           "Set the value 'Word 1' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0051_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0001")
                .InsertWFParagraph("Word 1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5973")]
        [Description("Automated UI Test 0052 - Validation for the Paragraph Question & Contains Data rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0002' on the WF Short Answer textbox" +
           "Set the value 'Word 1' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0052()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0002")
                .InsertWFParagraph("Word 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5974")]
        [Description("Automated UI Test 0052.1 - Validation for the Paragraph Question & Contains Data rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0002' on the WF Short Answer textbox" +
           "Set NO value in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0052_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0002")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5975")]
        [Description("Automated UI Test 0053 - Validation for the Paragraph Question & Equals rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0003' on the WF Short Answer textbox" +
           "Set the value 'Value 1 Value 2' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0053()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0003")
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5976")]
        [Description("Automated UI Test 0053.1 - Validation for the Paragraph Question & Equals rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0003' on the WF Short Answer textbox" +
           "Set the value 'Value 1 Value 3' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0053_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0003")
                .InsertWFParagraph("Value 1\nValue 3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5977")]
        [Description("Automated UI Test 0054 - Validation for the Paragraph Question & Does Not Equal rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0004' on the WF Short Answer textbox" +
           "Set the value 'Value 1 Value 3' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0054()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0004")
                .InsertWFParagraph("Value 1\nValue 3")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5978")]
        [Description("Automated UI Test 0054.1 - Validation for the Paragraph Question & Does Not Equal rule condition - " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0004' on the WF Short Answer textbox" +
           "Set the value 'Value 1 Value 2' in the WF Paragraph Textbox" +
           "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0054_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 19 - Rule Conditions Testing - Paragraph Question - 0004")
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        #endregion

        #region PickList Question


        [TestProperty("JiraIssueID", "CDV6-5979")]
        [Description("Automated UI Test 0055 - Validation for the PickList Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Budist' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0055()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0001")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5980")]
        [Description("Automated UI Test 0055.1 - Validation for the PickList Question & In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Atheist' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0055_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0001")
                .SelectWFPicklistByText("Atheist")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-5981")]
        [Description("Automated UI Test 0056 - Validation for the PickList Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Christian' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0056()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0002")
                .SelectWFPicklistByText("Christian")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5982")]
        [Description("Automated UI Test 0056.1 - Validation for the PickList Question & Not In rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Budist' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0056_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0002")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-5983")]
        [Description("Automated UI Test 0057 - Validation for the PickList Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0057()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5984")]
        [Description("Automated UI Test 0057.1 - Validation for the PickList Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'Budist' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0057_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0003")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-5985")]
        [Description("Automated UI Test 0058 - Validation for the PickList Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'Budist' in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0058()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0004")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5986")]
        [Description("Automated UI Test 0058.1 - Validation for the PickList Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 20 - Rule Conditions Testing - PickList Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF PickList Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0058_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 20 - Rule Conditions Testing - PickList Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        #endregion

        #region Boolean Question


        [TestProperty("JiraIssueID", "CDV6-5987")]
        [Description("Automated UI Test 0059 - Validation for the Boolean Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0059()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0001")
                .TapSaveButton(false);

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5988")]
        [Description("Automated UI Test 0059.1 - Validation for the Boolean Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0001' on the WF Short Answer textbox" +
            "Set the value 'Yes' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0059_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0001")
                .SelectWFBoolean(true)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        [TestProperty("JiraIssueID", "CDV6-5989")]
        [Description("Automated UI Test 0060 - Validation for the Boolean Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0002' on the WF Short Answer textbox" +
            "Set the value 'Yes' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0060()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0002")
                .SelectWFBoolean(true)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5990")]
        [Description("Automated UI Test 0060.1 - Validation for the Boolean Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0002' on the WF Short Answer textbox" +
            "(Boolean fields is empty by default)" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0060_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0002")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();
        }



        [TestProperty("JiraIssueID", "CDV6-5991")]
        [Description("Automated UI Test 0061 - Validation for the Boolean Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'Yes' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0061()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0003")
                .SelectWFBoolean(true)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5992")]
        [Description("Automated UI Test 0061.1 - Validation for the Boolean Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'No' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0061_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0003")
                .SelectWFBoolean(false)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
               .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5993")]
        [Description("Automated UI Test 0062 - Validation for the Boolean Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'False' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0062()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0004")
                .SelectWFBoolean(false)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5994")]
        [Description("Automated UI Test 0062.1 - Validation for the Boolean Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'True' in the WF Boolean Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0062_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - 0004")
                .SelectWFBoolean(true)
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        #endregion

        #region Short Answer


        [TestProperty("JiraIssueID", "CDV6-5995")]
        [Description("Automated UI Test 0067 - Validation for the ShortAnswer Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0067()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5996")]
        [Description("Automated UI Test 0067.1 - Validation for the ShortAnswer Question & Does Not Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0001' on the WF Short Answer textbox" +
            "Set a value in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0067_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0001")
                .InsertWFShortAnswer("Value 1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        [TestProperty("JiraIssueID", "CDV6-5997")]
        [Description("Automated UI Test 0064 - Validation for the ShortAnswer Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0002' on the WF Short Answer textbox" +
            "Set a value in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0064()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0002")
                .InsertWFShortAnswer("Value 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5998")]
        [Description("Automated UI Test 0064.1 - Validation for the ShortAnswer Question & Contains Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0002' on the WF Short Answer textbox" +
            "Set NO value in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0064_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0002")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5999")]
        [Description("Automated UI Test 0065 - Validation for the ShortAnswer Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'V1' in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0065()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0003")
                .InsertWFShortAnswer("V1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6000")]
        [Description("Automated UI Test 0065.1 - Validation for the ShortAnswer Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0003' on the WF Short Answer textbox" +
            "Set the value 'V2' in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0065_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0003")
                .InsertWFShortAnswer("V2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
               .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-6001")]
        [Description("Automated UI Test 0066 - Validation for the ShortAnswer Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'V2' in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0066()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0004")
                .InsertWFShortAnswer("V2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6002")]
        [Description("Automated UI Test 0066.1 - Validation for the ShortAnswer Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0004' on the WF Short Answer textbox" +
            "Set the value 'V1' in the WF ShortAnswer Question" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0066_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 22 - Rule Conditions Testing - Short Answer Question - 0004")
                .InsertWFShortAnswer("V1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        #endregion

        #region Time Question


        [TestProperty("JiraIssueID", "CDV6-6003")]
        [Description("Automated UI Test 0068 - Validation for the Time Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0001' on the WF Short Answer textbox" +
            "Set the value '13:15' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0068()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0001")
                .InsertWFTime("13:15")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 23 - Rule Conditions Testing - Time Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6004")]
        [Description("Automated UI Test 0068.1 - Validation for the Time Question & Equals rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0001' on the WF Short Answer textbox" +
            "Set the value '13:16' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0068_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0001")
                .InsertWFTime("13:16")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6005")]
        [Description("Automated UI Test 0069 - Validation for the Time Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0002' on the WF Short Answer textbox" +
            "Set the value '13:19' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0069()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0002")
                .InsertWFTime("13:19")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 23 - Rule Conditions Testing - Time Question - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6006")]
        [Description("Automated UI Test 0069.1 - Validation for the Time Question & Does Not Equal rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0002' on the WF Short Answer textbox" +
            "Set the value '13:20' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0069_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0002")
                .InsertWFTime("13:20")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6007")]
        [Description("Automated UI Test 0070 - Validation for the Time Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0003' on the WF Short Answer textbox" +
            "Set NO value in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0070()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 23 - Rule Conditions Testing - Time Question - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6008")]
        [Description("Automated UI Test 0070.1 - Validation for the Time Question & Does Not Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0003' on the WF Short Answer textbox" +
            "Set the value '10:00' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0070_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0003")
                .InsertWFTime("10:00")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6009")]
        [Description("Automated UI Test 0071 - Validation for the Time Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0004' on the WF Short Answer textbox" +
            "Set the value '10:00' in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0071()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0004")
                .InsertWFTime("10:00")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 23 - Rule Conditions Testing - Time Question - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6010")]
        [Description("Automated UI Test 0071.1 - Validation for the Time Question & Contain Data rule condition - " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 23 - Rule Conditions Testing - Time Question - 0004' on the WF Short Answer textbox" +
            "Set NO value in the WF Time Textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0071_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 23 - Rule Conditions Testing - Time Question - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        #endregion

        #region Table Question


        [TestProperty("JiraIssueID", "CDV6-6011")]
        [Description("Automated UI Test 0072 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0001' on the WF Short Answer textbox" +
            "Set the value 'V1 V2' in the Table PQ - Question 1 - Contribution Notes" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0072()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0001")
                .InsertQuestion1ContributionNotes("V1\nV2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6012")]
        [Description("Automated UI Test 0072.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0001' on the WF Short Answer textbox" +
            "Set the value 'V1 V3' in the Table PQ - Question 1 - Contribution Notes" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0072_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0001")
                .InsertQuestion1ContributionNotes("V1\nV3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6013")]
        [Description("Automated UI Test 0073 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0002' on the WF Short Answer textbox" +
            "Set the value 'V2' in the Table PQ - Question 2 - Role" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0073()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0002")
                .InsertQuestion2Role("V2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6014")]
        [Description("Automated UI Test 0073.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0002' on the WF Short Answer textbox" +
            "Set the value 'V3' in the Table PQ - Question 2 - Role" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0073_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0002")
                .InsertQuestion2Role("V3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6015")]
        [Description("Automated UI Test 0074 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0003' on the WF Short Answer textbox" +
            "Set the value 'Value 1' in the Test HQ - Location - Row 1" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0074()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0003")
                .InsertTestHQRow1Column1Value("Value 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6016")]
        [Description("Automated UI Test 0074.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0003' on the WF Short Answer textbox" +
            "Set the value 'Value 2' in the Test HQ - Location - Row 1" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0074_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0003")
                .InsertTestHQRow1Column1Value("Value 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }





        [TestProperty("JiraIssueID", "CDV6-6017")]
        [Description("Automated UI Test 0075 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0004' on the WF Short Answer textbox" +
            "Set the value '5.3' in the Test HQ - Test Dec - Row 2" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0075()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0004")
                .InsertTestHQRow2Column2Value("5.3")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6018")]
        [Description("Automated UI Test 0075.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0004' on the WF Short Answer textbox" +
            "Set the value '5.4' in the Test HQ - Test Dec - Row 2" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0075_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0004")
                .InsertTestHQRow2Column2Value("5.4")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6019")]
        [Description("Automated UI Test 0076 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0005' on the WF Short Answer textbox" +
            "Set the value 'Value 1' in the Test QPC - Outcome - Row 1" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0076()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0005")
                .InsertTestQPCOutcomeAnswer("Value 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6020")]
        [Description("Automated UI Test 0076.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0005' on the WF Short Answer textbox" +
            "Set the value 'Value 2' in the Test QPC - Outcome - Row 1" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0076_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0005")
                .InsertTestQPCOutcomeAnswer("Value 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }





        [TestProperty("JiraIssueID", "CDV6-6021")]
        [Description("Automated UI Test 0077 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0006' on the WF Short Answer textbox" +
            "Set the value 'Value 2' in the Test QPC - Who - Row 2" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0077()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0006")
                .InsertTestQPCWhoAnswer("Value 2")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6022")]
        [Description("Automated UI Test 0077.1 - Validation for the Table Question (with primary question)" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 24 - Rule Conditions Testing - Table Questions - 0006' on the WF Short Answer textbox" +
            "Set the value 'Value 1' in the Test QPC - Who - Row 2" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0077_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 24 - Rule Conditions Testing - Table Questions - 0006")
                .InsertTestQPCWhoAnswer("Value 1")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        #endregion

        #region Security Profile


        [TestProperty("JiraIssueID", "CDV6-6023")]
        [Description("Automated UI Test 0078 - Validation for the Security Profiles & In rule condition" +
            "Login with user that contains CW Activities (PCBU Edit) security profile" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 25 - Rule Conditions Testing - Security Profile - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0078()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6024")]
        [Description("Automated UI Test 0078.1 - Validation for the Security Profiles & In rule condition" +
            "Login with user that DO NOT contain CW Activities (PCBU Edit) security profile" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 25 - Rule Conditions Testing - Security Profile - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0078_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0001")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-6025")]
        [Description("Automated UI Test 0079 - Validation for the Security Profiles & Not In rule condition" +
            "Login with user that DO NOT contains CW Activities (PCBU Edit) security profile" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 25 - Rule Conditions Testing - Security Profile - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0079()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormDocRulesUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0002")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0002 - Condition Activated");

        }



        [TestProperty("JiraIssueID", "CDV6-6026")]
        [Description("Automated UI Test 0079.1 - Validation for the Security Profiles & Not In rule condition" +
            "Login with user that contains CW Activities (PCBU Edit) security profile" +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 25 - Rule Conditions Testing - Security Profile - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is NOT activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0079_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 25 - Rule Conditions Testing - Security Profile - 0002")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        #endregion

        #region Entity: Person


        [TestProperty("JiraIssueID", "CDV6-6027")]
        [Description("Automated UI Test 0080 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0080()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            int accommodationstatusid = 1; //Settled
            dbHelper.person.UpdateAccommodationStatus(personID, accommodationstatusid);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6028")]
        [Description("Automated UI Test 0080.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should NOT have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0080_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            int accommodationstatusid = 2; //Unsettled
            dbHelper.person.UpdateAccommodationStatus(personID, accommodationstatusid);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0001")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }




        [TestProperty("JiraIssueID", "CDV6-6029")]
        [Description("Automated UI Test 0081 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Property Name set to PName" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0081()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateContactDetails(personID, "PName", "1", "2", "3", "4", "5", "6", "7", "987654321");

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0002")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0002 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6030")]
        [Description("Automated UI Test 0081.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should NOT have the Property Name set to PName" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0081_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateContactDetails(personID, "OtherName", "1", "2", "3", "4", "5", "6", "7", "987654321");

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0002")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6031")]
        [Description("Automated UI Test 0082 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0082()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateAllowEmail(personID, true);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0003 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6032")]
        [Description("Automated UI Test 0082.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0082_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateAllowEmail(personID, false);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0003")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6033")]
        [Description("Automated UI Test 0083 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0083()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2000, 9, 1));

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0004")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6034")]
        [Description("Automated UI Test 0083.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0083_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2000, 9, 2));

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0004")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6035")]
        [Description("Automated UI Test 0084 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0005' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0084()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            int dobandagetypeid = 3; //Estimated Age
            dbHelper.person.UpdateDOBAndAgeTypeId(personID, dobandagetypeid, null, 15);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0005")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6036")]
        [Description("Automated UI Test 0084.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Person associated with the case should have the Accommodation Status set to Settled" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0005' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0084_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            int dobandagetypeid = 3; //Estimated Age
            dbHelper.person.UpdateDOBAndAgeTypeId(personID, dobandagetypeid, null, 14);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0005")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        #endregion

        #region Entity: Case


        [TestProperty("JiraIssueID", "CDV6-6037")]
        [Description("Automated UI Test 0085 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should have the Case Date/Time set to 01/09/2019" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0006' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0085()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var startDateTime = new DateTime(2019, 9, 1);
            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), startDateTime, 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0006")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6038")]
        [Description("Automated UI Test 0085.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should NOT have the Case Date/Time set to 01/09/2019" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0006' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0085_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var startDateTime = new DateTime(2019, 9, 2);
            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), startDateTime, 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0006")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6039")]
        [Description("Automated UI Test 0086 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should have the Additional Information set to 'Value 1'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0007' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0086()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];
            dbHelper.Case.UpdateAdditionalInformation(caseid, "Value 1");

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0007")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0007 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-6040")]
        [Description("Automated UI Test 0086.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should NOT have the Additional Information set to 'Value 1'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0007' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0086_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];
            dbHelper.Case.UpdateAdditionalInformation(caseid, "Value 2");

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0007")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }



        [TestProperty("JiraIssueID", "CDV6-6041")]
        [Description("Automated UI Test 0087 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should have the Case Origin set to 'In Person'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0008' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0087()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];
            var caseOriginId = 6;//In Person
            dbHelper.Case.UpdateCaseOrigin(caseid, caseOriginId);

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0008")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0008 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5872")]
        [Description("Automated UI Test 0087.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Case record should have the Case Origin set to 'Letter'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0008' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0087_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];
            var caseOriginId = 2;//Email
            dbHelper.Case.UpdateCaseOrigin(caseid, caseOriginId);

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0008")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }


        [TestProperty("JiraIssueID", "CDV6-5873")]
        [Description("Automated UI Test 0088 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Form Start Date should be set to '01/09/2017'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0009' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0088()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2019, 9, 1);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0009")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0009 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5874")]
        [Description("Automated UI Test 0088.1 - Validation for the Entity: Person " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Form Start Date should be set to '15/09/2017'" +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 26 - Rule Conditions Testing - Entity Person - 0009' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0088_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 26 - Rule Conditions Testing - Entity Person - 0009")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        #endregion



        #endregion

        #region Rule Action Testing

        #region Cancel Save action


        [TestProperty("JiraIssueID", "CDV6-5875")]
        [Description("Automated UI Test 0089 - Validation for the Cancel Save action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 27 - Rule Actions Testing - Cancel Save - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0089()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 27 - Rule Actions Testing - Cancel Save - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 27 - Rule Actions Testing - Cancel Save - 0001 - Condition Activated")
                .TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .ValidateNotificationAreaVisible("Some data is not correct. Please review the data in the Form.")
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("There are unsaved changes on this form, continuing will disregard the changes. Would you like to continue?");

        }

        #endregion

        #region Empty field action


        [TestProperty("JiraIssueID", "CDV6-5876")]
        [Description("Automated UI Test 0090 - Validation for the Empty Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 28 - Rule Actions Testing - Empty Field - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0090()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 28 - Rule Actions Testing - Empty Field - 0001")
                .InsertWFDateValue("01/01/2019")
                .InsertWFDecimalValue("9.5")
                .SelectWFMultipleChoice(2)
                .SelectWFMultipleResponse(2)
                .SelectWFMultipleResponse(3)
                .InsertWFNumericValue("6")
                .InsertIGACMSFieldWriteBackTest1Value("01/05/2019")
                .TapWFLookupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(personNumber)
                .TapSearchButton()
                .SelectResultElement(personID);

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveButton()
                .ValidateWFDateAnswer("")
                .ValidateWFDecimalAnswer("")
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionNotSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionNotChecked(3)
                .ValidateWFNumericAnswer("")
                .ValidateIGACMSFieldWriteBackTest1Answer("")
                .ValidateWFLookupLookupValue("")
                ;


        }


        [TestProperty("JiraIssueID", "CDV6-5877")]
        [Description("Automated UI Test 0091 - Validation for the Empty Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 28 - Rule Actions Testing - Empty Field - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0091()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 28 - Rule Actions Testing - Empty Field - 0002")
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveButton()
                .ValidateWFParagraphAnswer("");


        }


        [TestProperty("JiraIssueID", "CDV6-5878")]
        [Description("Automated UI Test 0092 - Validation for the Empty Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 28 - Rule Actions Testing - Empty Field - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0092()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 28 - Rule Actions Testing - Empty Field - 0003")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .ValidateWFPicklistSelectedValue("");


        }


        [TestProperty("JiraIssueID", "CDV6-5879")]
        [Description("Automated UI Test 0093 - Validation for the Empty Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 28 - Rule Actions Testing - Empty Field - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0093()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 28 - Rule Actions Testing - Empty Field - 0004")
                .TapSaveButton()
                .ValidateWFShortAnswer("");


        }


        [TestProperty("JiraIssueID", "CDV6-5880")]
        [Description("Automated UI Test 0094 - Validation for the Empty Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 28 - Rule Actions Testing - Empty Field - 0005' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0094()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 28 - Rule Actions Testing - Empty Field - 0005")
                .InsertTestHQRow1Column1Value("Location 1")
                .InsertTestHQRow1Column2Value("1.5")
                .InsertTestHQRow2Column1Value("Location 2")
                .InsertTestHQRow2Column2Value("2.3")
                .InsertTestQPCOutcomeAnswer("Outcome 1")
                .InsertTestQPCTypeOfInvolvementAnswer("TOI 1")
                .InsertTestQPCWFTimeAnswer("10:05")
                .InsertTestQPCWhoAnswer("Who ???")
                .SelectWFBoolean(true)
                .InsertWFTime("09:55")
                .InsertDateBecameInvolvedValue(1, "01/07/2019")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .InsertQuestion1ContributionNotes("CN 1")
                .InsertQuestion1Role("R 1")
                .InsertQuestion2ContributionNotes("CN 2")
                .InsertQuestion2Role("R 2")
                .TapSaveButton()
                .ValidateTestHQRow1Column1Question("")
                .ValidateTestHQRow1Column2Question("")
                .ValidateTestHQRow2Column1Question("")
                .ValidateTestHQRow2Column2Question("")
                .ValidateTestQPCOutcomeAnswer("")
                .ValidateTestQPCTypeOfInvolvementAnswer("")
                .ValidateTestQPCWFTimeAnswer("")
                .ValidateTestQPCWhoAnswer("")
                .ValidateWFBooleanNoOptionSelected()
                .ValidateWFTimeQuestion("")
                .ValidateQuestion1ContributionNotes("")
                .ValidateQuestion1Role("")
                .ValidateQuestion2ContributionNotes("")
                .ValidateQuestion2Role("");
        }

        #endregion

        #region Evaluate Formula


        [TestProperty("JiraIssueID", "CDV6-5881")]
        [Description("Automated UI Test 0095 - Validation for the Evaluate Formula action (Divide) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0095()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0001")
                .InsertWFDecimalValue("10")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("5");


        }


        [TestProperty("JiraIssueID", "CDV6-5882")]
        [Description("Automated UI Test 0096 - Validation for the Evaluate Formula action (Divide) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0096()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0002")
                .InsertWFDecimalValue("10")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("5");


        }


        [TestProperty("JiraIssueID", "CDV6-5883")]
        [Description("Automated UI Test 0097 - Validation for the Evaluate Formula action (Divide) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0097()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0003")
                .InsertWFDecimalValue("10")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("5");


        }


        [TestProperty("JiraIssueID", "CDV6-5884")]
        [Description("Automated UI Test 0098 - Validation for the Evaluate Formula action (Divide) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0098()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0004")
                .InsertWFDecimalValue("10")
                .SelectWFMultipleChoice(2)
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("5");


        }


        [TestProperty("JiraIssueID", "CDV6-5885")]
        [Description("Automated UI Test 0099 - Validation for the Evaluate Formula action (Divide) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0005' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0099()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0005")
                .InsertWFDecimalValue("10")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("5");


        }



        [TestProperty("JiraIssueID", "CDV6-5886")]
        [Description("Automated UI Test 0100 - Validation for the Evaluate Formula action (Percentage) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0006' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0100()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0006")
                .InsertWFDecimalValue("200")
                .InsertWFNumericValue("50")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("100");


        }



        [TestProperty("JiraIssueID", "CDV6-5887")]
        [Description("Automated UI Test 0101 - Validation for the Evaluate Formula action (Multiply) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0007' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0101()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0007")
                .InsertWFDecimalValue("5")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("10");


        }



        [TestProperty("JiraIssueID", "CDV6-5888")]
        [Description("Automated UI Test 0101 - Validation for the Evaluate Formula action (Sum) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0008' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0102()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0008")
                .InsertWFDecimalValue("5")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("7");
        }



        [TestProperty("JiraIssueID", "CDV6-5889")]
        [Description("Automated UI Test 0103 - Validation for the Evaluate Formula action (Subtract) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0009' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0103()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0009")
                .InsertWFDecimalValue("5")
                .InsertWFNumericValue("2")
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("3");
        }


        [TestProperty("JiraIssueID", "CDV6-5890")]
        [Description("Automated UI Test 0104 - Validation for the Evaluate Formula action (Sum two values minus a third) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0010' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0104()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 29 - Rule Actions Testing - Evaluate Formula - 0010")
                .InsertWFDecimalValue("5")
                .InsertWFNumericValue("2")
                .SelectWFMultipleChoice(3)
                .TapSaveButton()
                .ValidateTestHQRow1Column2Question("4");
        }

        #endregion

        #region Focus Field


        [TestProperty("JiraIssueID", "CDV6-5891")]
        [Description("Automated UI Test 0105 - Validation for the Focus Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 30 - Rule Actions Testing - Focus Field - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0105()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 30 - Rule Actions Testing - Focus Field - 0001")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
               .ValidateWFParagraphVisible();

        }


        [TestProperty("JiraIssueID", "CDV6-5892")]
        [Description("Automated UI Test 0106 - Validation for the Focus Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 30 - Rule Actions Testing - Focus Field - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0106()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 30 - Rule Actions Testing - Focus Field - 0002")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
               .ValidateWFMultipleResponseVisible();

        }


        [TestProperty("JiraIssueID", "CDV6-5893")]
        [Description("Automated UI Test 0107 - Validation for the Focus Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 30 - Rule Actions Testing - Focus Field - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0107()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 30 - Rule Actions Testing - Focus Field - 0003")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
               .ValidateDateBecameInvolvedVisible(1)
               .ValidateReasonForAssessmentVisible(1);

        }


        [TestProperty("JiraIssueID", "CDV6-5894")]
        [Description("Automated UI Test 0108 - Validation for the Focus Field action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 30 - Rule Actions Testing - Focus Field - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0108()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 30 - Rule Actions Testing - Focus Field - 0004")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
               .ValidateWFBooleanVisible();

        }

        #endregion

        #region Hide/Show Control


        [TestProperty("JiraIssueID", "CDV6-5895")]
        [Description("Automated UI Test 0109 - Validation for the Hide/Show Control action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 31 - Rule Actions Testing - Hide Controls - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0109()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 31 - Rule Actions Testing - Hide Controls - 0001")
                .TapSaveButton()
                .ValidateWFTableWithUnlimitedRowsNewButtonDoNotExist()
                .InsertWFShortAnswer("UI Testing - 31 - Rule Actions Testing - Hide Controls - 0002")
                .TapSaveButton()
                .ValidateWFTableWithUnlimitedRowsNewButtonExists();
        }


        #endregion

        #region Run Another Rule


        [TestProperty("JiraIssueID", "CDV6-5896")]
        [Description("Automated UI Test 0110 - Validation for the Run Another Rule action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 32 - Rule Actions Testing - Run another Rule - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0110()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 32 - Rule Actions Testing - Run another Rule - 0001")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 32 - Rule Actions Testing - Run another Rule - 0001 - Condition Activated");

        }

        #endregion

        #region Set Placeholder


        [TestProperty("JiraIssueID", "CDV6-5897")]
        [Description("Automated UI Test 0111 - Validation for the Set Placeholder (Boolean) action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001' on the WF Short Answer textbox" +
            "Set the WF Boolean question to 'Yes'" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0111()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001")
                .SelectWFBoolean(true)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001 - Condition Activated (Yes)");

        }


        [TestProperty("JiraIssueID", "CDV6-5898")]
        [Description("Automated UI Test 0112 - Validation for the Set Placeholder (Boolean) action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001' on the WF Short Answer textbox" +
            "Set the WF Boolean question to 'No'" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0112()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001")
                .SelectWFBoolean(false)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0001 - Condition Activated (No)");

        }



        [TestProperty("JiraIssueID", "CDV6-5899")]
        [Description("Automated UI Test 0113 - Validation for the Set Placeholder (Boolean) action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0002' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0113()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0002")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0002 - Condition Activated");

        }



        [TestProperty("JiraIssueID", "CDV6-5900")]
        [Description("Automated UI Test 0114 - Validation for the Set Placeholder (Boolean) action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0003' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0114()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0003")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0003 - Condition Activated");

        }



        [TestProperty("JiraIssueID", "CDV6-5901")]
        [Description("Automated UI Test 0115 - Validation for the Set Placeholder (Boolean) action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'I Testing - 33 - Rule Actions Testing - Set Placeholder - 0004' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0115()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("I Testing - 33 - Rule Actions Testing - Set Placeholder - 0004")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0004 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5902")]
        [Description("Automated UI Test 0116 - Validation for the Set Placeholder (Date) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0005' on the WF Short Answer textbox" +
            "Set a value in the (Unlimited Table) Date Became Involved field" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0116()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0005")
                .InsertWFDateValue("24/09/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0005 - Rule Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5903")]
        [Description("Automated UI Test 0117 - Validation for the Set Placeholder (Date) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0006' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0117()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0006")
                .TapSaveButton()
                .ValidateWFDateAnswer("01/09/2019");

        }


        [TestProperty("JiraIssueID", "CDV6-5904")]
        [Description("Automated UI Test 0118 - Validation for the Set Placeholder (Decimal) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0007' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0118()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0007")
                .InsertTestHQRow1Column2Value("9.21")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("9.21");

        }


        [TestProperty("JiraIssueID", "CDV6-5905")]
        [Description("Automated UI Test 0119 - Validation for the Set Placeholder (Decimal) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0008' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0119()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0008")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("5.9");

        }



        [TestProperty("JiraIssueID", "CDV6-5906")]
        [Description("Automated UI Test 0120 - Validation for the Set Placeholder (Numeric) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0009' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0120()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0009")
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .ValidateWFNumericAnswer("2");

        }


        [TestProperty("JiraIssueID", "CDV6-5907")]
        [Description("Automated UI Test 0121 - Validation for the Set Placeholder (Numeric) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0010' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0121()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0010")
                .TapSaveButton()
                .ValidateWFNumericAnswer("12");

        }


        [TestProperty("JiraIssueID", "CDV6-5908")]
        [Description("Automated UI Test 0122 - Validation for the Set Placeholder (Text) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0011' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0122()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0011")
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveButton()
                .ValidateWFShortAnswer("Value 1Value 2");

        }


        [TestProperty("JiraIssueID", "CDV6-5909")]
        [Description("Automated UI Test 0123 - Validation for the Set Placeholder (Text) action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0012' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0123()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 33 - Rule Actions Testing - Set Placeholder - 0012")
                .TapSaveButton()
                .ValidateWFParagraphAnswer("Value 1 Value 2");

        }

        #endregion

        #region Set Question Mandatory


        [TestProperty("JiraIssueID", "CDV6-5910")]
        [Description("Automated UI Test 0124 - Validation for the Set Question Mandatory action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 34 - Rule Actions Testing - Set Question Mandatory - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0124()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 34 - Rule Actions Testing - Set Question Mandatory - 0001")
                .TapSaveButton()
                .InsertWFNumericValue("8")
                .TapSaveButton()
                .ValidateNotificationAreaVisible("Some data is not correct. Please review the data in the Form.")
                .WaitForWFDecimalErrorLabelVisible("Please fill out this field.");

        }


        [TestProperty("JiraIssueID", "CDV6-5911")]
        [Description("Automated UI Test 0125 - Validation for the Set Question Not Mandatory action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 34 - Rule Actions Testing - Set Question Mandatory - 0002' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0125()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 34 - Rule Actions Testing - Set Question Mandatory - 0001")
                .TapSaveButton()
                .InsertWFNumericValue("8")//insert answer to trigger the save operation
                .TapSaveButton()//save the form
                .WaitForWFDecimalErrorLabelVisible("Please fill out this field.")//decimal question should be mandatory
                .InsertWFShortAnswer("UI Testing - 34 - Rule Actions Testing - Set Question Mandatory - 0002")//this update should make que decimal question not mandatory
                .InsertWFDecimalValue("5.5")//insert the decimal answer
                .TapSaveButton()
                .InsertWFDecimalValue("")//remove the value from the decimal question
                .TapSaveAndCloseButton();//must be possible to save the form without the decimal answer now

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }


        #endregion

        #region Set Question Readonly / Writable


        [TestProperty("JiraIssueID", "CDV6-5912")]
        [Description("Automated UI Test 0126 - Validation for the Set Question Readonly action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 35 - Rule Actions Testing - Set Question Readonly - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0126()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 35 - Rule Actions Testing - Set Question Readonly - 0001")
                .TapSaveButton()
                .ValidateIGACMSFieldWriteBackTest1Disabled()
                .ValidateWFMultipleChoiceOptionDisabled()
                .ValidateWFDecimalDisabled()
                .ValidateWFMultipleResponseDisabled()
                .ValidateWFNumericDisabled()
                .ValidateWFLookupDisabled()
                .ValidateWFDateDisabled()
                .ValidateWFParagraphDisabled()
                .ValidateWFPicklistDisabled()
                .ValidateTestHQRow1Column1Disabled()
                .ValidateTestHQRow2Column2Disabled()
                .ValidateDateBecameInvolvedDisabled(1)
                .ValidateReasonForAssessmentDisabled(1)
                .ValidateQuestion1ContributionDisabled()
                .ValidateQuestion2RoleDisabled()
                .ValidateTestQPCTypeOfInvolvementDisabled()
                .ValidateTestQPCWhoDisabled()
                .ValidateWFBooleanDisabled()
                .ValidateWFTimeDisabled();

        }


        [TestProperty("JiraIssueID", "CDV6-5913")]
        [Description("Automated UI Test 0127 - Validation for the Set Question Writable action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 35 - Rule Actions Testing - Set Question Readonly - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0127()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 35 - Rule Actions Testing - Set Question Readonly - 0001")
                .TapSaveButton()
                .InsertWFShortAnswer("UI Testing - 35 - Rule Actions Testing - Set Question Readonly - 0002")
                .TapSaveButton()
                .ValidateIGACMSFieldWriteBackTest1Enabled()
                .ValidateWFMultipleChoiceOptionEnabled()
                .ValidateWFDecimalEnabled()
                .ValidateWFMultipleResponseEnabled()
                .ValidateWFNumericEnabled()
                .ValidateWFLookupEnabled()
                .ValidateWFDateEnabled()
                .ValidateWFParagraphEnabled()
                .ValidateWFPicklistEnabled()
                .ValidateTestHQRow1Column1Enabled()
                .ValidateTestHQRow2Column2Enabled()
                .ValidateDateBecameInvolvedEnabled(1)
                .ValidateReasonForAssessmentEnabled(1)
                .ValidateQuestion1ContributionEnabled()
                .ValidateQuestion2RoleEnabled()
                .ValidateTestQPCTypeOfInvolvementEnabled()
                .ValidateTestQPCWhoEnabled()
                .ValidateWFBooleanEnabled()
                .ValidateWFTimeEnabled();

        }

        #endregion

        #region Set Question Text / Set Section Text


        [TestProperty("JiraIssueID", "CDV6-5914")]
        [Description("Automated UI Test 0128 - Validation for the Set Question Text action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 36 - Rule Actions Testing - Set Question Text - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0128()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 36 - Rule Actions Testing - Set Question Text - 0001")
                .TapSaveButton()
                .ValidateGuiPersonDOBQuestionLabelQuestionText("UI Testing - 36 - Rule Actions Testing - Set Question Text - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5915")]
        [Description("Automated UI Test 0129 - Validation for the Set Section Text action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 36 - Rule Actions Testing - Set Section Text - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Server Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0129()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 36 - Rule Actions Testing - Set Section Text - 0001")
                .TapSaveButton()
                .ValidateLeftMenuSection1_1Text("UI Testing - 36 - Rule Actions Testing - Set Section Text - 0001 - Condition Activated");

        }

        #endregion

        #region Set Question Value


        [TestProperty("JiraIssueID", "CDV6-5916")]
        [Description("Automated UI Test 0130 - Validation for the Set Question Value action (Date Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0130()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0001")
                .TapSaveButton()
                .ValidateWFDateAnswer("01/09/2019");

        }


        [TestProperty("JiraIssueID", "CDV6-5917")]
        [Description("Automated UI Test 0131 - Validation for the Set Question Value action (Decimal Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0131()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0002")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("9.1");

        }


        [TestProperty("JiraIssueID", "CDV6-5918")]
        [Description("Automated UI Test 0132 - Validation for the Set Question Value action (Multiple Choice Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0003' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0132()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0003")
                .TapSaveButton()
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3);


        }


        [TestProperty("JiraIssueID", "CDV6-5919")]
        [Description("Automated UI Test 0133 - Validation for the Set Question Value action (Multiple Response Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0004' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0133()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0004")
                .TapSaveButton()
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3);


        }


        [TestProperty("JiraIssueID", "CDV6-5920")]
        [Description("Automated UI Test 0134 - Validation for the Set Question Value action (Numeric Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0005' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0134()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0005")
                .TapSaveButton()
                .ValidateWFNumericAnswer("4");


        }


        [TestProperty("JiraIssueID", "CDV6-5921")]
        [Description("Automated UI Test 0135 - Validation for the Set Question Value action (Paragraph Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0007' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0135()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0007")
                .TapSaveButton()
                .ValidateWFParagraphAnswer("Value 1\r\nValue 2");


        }


        [TestProperty("JiraIssueID", "CDV6-5922")]
        [Description("Automated UI Test 0136 - Validation for the Set Question Value action (Picklist Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0008' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0136()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0008")
                .TapSaveButton()
                .ValidateWFPicklistSelectedValue("Christian");
        }


        [TestProperty("JiraIssueID", "CDV6-5923")]
        [Description("Automated UI Test 0137 - Validation for the Set Question Value action (Short Answer Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0009' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0137()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0009")
                .TapSaveButton()
                .ValidateWFShortAnswer("Value 1 Value 2");
        }


        [TestProperty("JiraIssueID", "CDV6-5924")]
        [Description("Automated UI Test 0138 - Validation for the Set Question Value action (Table HQ Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0010' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0138()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0010")
                .TapSaveButton()
                .InsertTestHQRow1Column1Value("Value 1");
        }


        [TestProperty("JiraIssueID", "CDV6-5925")]
        [Description("Automated UI Test 0139 - Validation for the Set Question Value action (Table HQ Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0011' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0139()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0011")
                .TapSaveButton()
                .InsertTestHQRow2Column2Value("9.1");
        }


        [TestProperty("JiraIssueID", "CDV6-5926")]
        [Description("Automated UI Test 0140 - Validation for the Set Question Value action (Table QPC Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0012' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0140()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0012")
                .TapSaveButton()
                .ValidateTestQPCOutcomeAnswer("Value 1");
        }


        [TestProperty("JiraIssueID", "CDV6-5927")]
        [Description("Automated UI Test 0141 - Validation for the Set Question Value action (Table QPC Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0013' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0141()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0013")
                .TapSaveButton()
                .ValidateTestQPCWFTimeAnswer("08:55");
        }


        [TestProperty("JiraIssueID", "CDV6-5928")]
        [Description("Automated UI Test 0142 - Validation for the Set Question Value action (Boolean Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0014' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0142()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0014")
                .TapSaveButton()
                .ValidateWFBoolean(true);
        }


        [TestProperty("JiraIssueID", "CDV6-5929")]
        [Description("Automated UI Test 0143 - Validation for the Set Question Value action (Time Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0015' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0143()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0015")
                .TapSaveButton()
                .ValidateWFTimeQuestion("08:05");
        }


        [TestProperty("JiraIssueID", "CDV6-5930")]
        [Description("Automated UI Test 0144 - Validation for the Set Question Value action (Table PQ Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0016' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0144()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0016")
                .TapSaveButton()
                .ValidateQuestion1ContributionNotes("V1\r\nV2");
        }


        [TestProperty("JiraIssueID", "CDV6-5931")]
        [Description("Automated UI Test 0145 - Validation for the Set Question Value action (Table PQ Question) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0017' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0145()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0017")
                .TapSaveButton()
                .ValidateQuestion2Role("V1 V2");
        }



        [TestProperty("JiraIssueID", "CDV6-5932")]
        [Description("Automated UI Test 0146 - Validation for the Set Question Value action (Use Placeholder to set answers) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0018' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0146()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0018")
                .TapSaveButton()
                .ValidateWFDateAnswer("04/09/2019")
                .ValidateWFDecimalAnswer("1.01")
                .ValidateWFNumericAnswer("2")
                .ValidateWFParagraphAnswer("V1")
                .ValidateWFShortAnswer("V1")
                .ValidateWFBoolean(true);
        }


        [TestProperty("JiraIssueID", "CDV6-5933")]
        [Description("Automated UI Test 0147 - Validation for the Set Question Value action (Extract value from other questions) " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 37 - Rule Actions Testing - Set Question Value - 0019' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0147()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 37 - Rule Actions Testing - Set Question Value - 0019")
                .InsertTestHQRow1Column2Value("10.9")
                .InsertQuestion1ContributionNotes("Value 1 Value 2")
                .InsertTestHQRow1Column1Value("Value 3 Value 4")
                .InsertTestQPCWFTimeAnswer("09:55")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("10.9")
                .ValidateWFParagraphAnswer("Value 1 Value 2")
                .ValidateWFShortAnswer("Value 3 Value 4")
                .ValidateWFTimeQuestion("09:55");
        }

        #endregion

        #region Hide / Show Section


        [TestProperty("JiraIssueID", "CDV6-5934")]
        [Description("Automated UI Test 0148 - Validation for the Hide Section action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 38 - Rule Actions Testing - Hide Section - 0001' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0148()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 38 - Rule Actions Testing - Hide Section - 0001")
                .TapSaveButton()
                .WaitForSection1_1Hidden(false)
                .WaitForLeftMenuSection1_1Hiden();
        }


        [TestProperty("JiraIssueID", "CDV6-5935")]
        [Description("Automated UI Test 0149 - Validation for the Show Section action " +
            "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the text 'UI Testing - 38 - Rule Actions Testing - Hide Section - 0002' on the WF Short Answer textbox" +
            "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0149()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 38 - Rule Actions Testing - Hide Section - 0001")
                .TapSaveButton()
                .InsertWFShortAnswer("UI Testing - 38 - Rule Actions Testing - Hide Section - 0002")
                .TapSaveButton()
                .InsertWFParagraph("Value 1 Value 2")
                .WaitForSection1_1Visible();
        }

        #endregion

        #region Hide / Show Section Question


        [TestProperty("JiraIssueID", "CDV6-5936")]
        [Description("Automated UI Test 0150 - Validation for the Hide Section Question action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 39 - Rule Actions Testing - Hide Section Question - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0150()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 39 - Rule Actions Testing - Hide Section Question - 0001")
                .TapSaveButton()
                .TapLeftMenuSection1_1()
                .ValidateWFParagraphNotVisible();
        }



        [TestProperty("JiraIssueID", "CDV6-5937")]
        [Description("Automated UI Test 0151 - Validation for the Show Section Question action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 39 - Rule Actions Testing - Hide Section Question - 0002' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0151()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 39 - Rule Actions Testing - Hide Section Question - 0001")
                .TapSaveButton()
                .InsertWFShortAnswer("UI Testing - 39 - Rule Actions Testing - Hide Section Question - 0002")
                .TapSaveButton()
                .InsertWFParagraph("Test if textarea is visible");
        }

        #endregion

        #region Hide / Show Single Question


        [TestProperty("JiraIssueID", "CDV6-5938")]
        [Description("Automated UI Test 0152 - Validation for the Hide Single Question action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 40 - Rule Actions Testing - Hide Single Question - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0152()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 40 - Rule Actions Testing - Hide Single Question - 0001")
                .TapSaveButton()
                .TapLeftMenuSection1_1()
                .ValidateWFParagraphNotVisible();
        }



        [TestProperty("JiraIssueID", "CDV6-5939")]
        [Description("Automated UI Test 0153 - Validation for the Show Single Question action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 40 - Rule Actions Testing - Hide Single Question - 0002' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0153()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 40 - Rule Actions Testing - Hide Single Question - 0001")
                .TapSaveButton()
                .InsertWFShortAnswer("UI Testing - 40 - Rule Actions Testing - Hide Single Question - 0002")
                .TapSaveButton()
                .InsertWFParagraph("Test if textarea is visible");
        }

        #endregion

        #region Stop Rule 


        [TestProperty("JiraIssueID", "CDV6-5940")]
        [Description("Automated UI Test 0154 - Validation for the Stop Rule action " +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 41 - Rule Actions Testing - Stop Rule - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is Stopped before the message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0154()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 41 - Rule Actions Testing - Stop Rule - 0001")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
              .WaitForCaseFormPageToLoad();
        }

        #endregion

        #region Sum


        [TestProperty("JiraIssueID", "CDV6-5941")]
        [Description("Automated UI Test 0155 - Validation for the Sum Rule action (no fields used by the action have values)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 42 - Rule Actions Testing - Sum - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0155()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 42 - Rule Actions Testing - Sum - 0001")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("0");

        }


        [TestProperty("JiraIssueID", "CDV6-5942")]
        [Description("Automated UI Test 0156 - Validation for the Sum Rule action (only one field used by the action have a value)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 42 - Rule Actions Testing - Sum - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0156()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 42 - Rule Actions Testing - Sum - 0001")
                .SelectWFMultipleChoice(3)
                .TapSaveButton()
                .ValidateWFDecimalAnswer("3");

        }


        [TestProperty("JiraIssueID", "CDV6-5943")]
        [Description("Automated UI Test 0157 - Validation for the Sum Rule action (all fields used by the action have values)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 42 - Rule Actions Testing - Sum - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0157()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 42 - Rule Actions Testing - Sum - 0001")
                .SelectWFMultipleChoice(3)
                .InsertWFNumericValue("7")
                .SelectWFPicklistByText("Budist")
                .InsertTestHQRow1Column2Value("5.2")
                .InsertTestHQRow2Column2Value("10.3")
                .TapSaveButton()
                .ValidateWFDecimalAnswer("27.5");

        }

        #endregion

        #endregion

        #region Rule Conditions


        [TestProperty("JiraIssueID", "CDV6-5944")]
        [Description("Automated UI Test 0158 - Validation for Rule Conditions - Compare question to question - Questions Will Match (Decimal Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0158()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0001")
                .InsertWFDecimalValue("7.31")
                .InsertTestHQRow1Column2Value("7.31")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0001 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5945")]
        [Description("Automated UI Test 0158.1 - Validation for Rule Conditions - Compare question to question - Questions Will NOT Match (Decimal Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0001' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0158_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0001")
                .InsertWFDecimalValue("7.31")
                .InsertTestHQRow1Column2Value("5.31")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5946")]
        [Description("Automated UI Test 0159 - Validation for Rule Conditions - Compare question to question - Questions Will Match (Paragraph Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0005' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0159()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0005")
                .InsertWFParagraph("UI Testing - 43 - Rule Conditions Operator - Question - 0005")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0005 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5947")]
        [Description("Automated UI Test 0159.1 - Validation for Rule Conditions - Compare question to question - Questions Will NOT Match (Paragraph Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0005' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0159_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0005")
                .InsertWFParagraph("Value 1 Value 2")
                .InsertWFShortAnswer("Value 1 Value 3")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5948")]
        [Description("Automated UI Test 0160 - Validation for Rule Conditions - Compare question to question - Questions Will Match (Short Answer Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0006' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0160()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0006")
                .InsertWFParagraph("UI Testing - 43 - Rule Conditions Operator - Question - 0006")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0006 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5949")]
        [Description("Automated UI Test 0159.1 - Validation for Rule Conditions - Compare question to question - Questions Will NOT Match (Short Answer Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0006' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0160_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0006")
                .InsertWFParagraph("Value 1 Value 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5950")]
        [Description("Automated UI Test 0161 - Validation for Rule Conditions - Compare question to question - Questions Will Match (Time Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0007' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0161()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("UI Testing - 43 - Rule Conditions Operator - Question - 0007")
                .InsertWFShortAnswer("09:20")
                .InsertWFTime("09:20")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0007");

        }


        [TestProperty("JiraIssueID", "CDV6-5951")]
        [Description("Automated UI Test 0162 - Validation for Rule Conditions - Compare question to question - Questions Will Match (Time Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0008' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0162()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0008")
                .InsertWFTime("09:20")
                .InsertTestQPCWFTimeAnswer("09:20")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0008 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5952")]
        [Description("Automated UI Test 0162.1 - Validation for Rule Conditions - Compare question to question - Questions Will NOT Match (Time Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0008' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0162_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0008")
                .InsertWFTime("09:20")
                .InsertTestQPCWFTimeAnswer("09:25")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5953")]
        [Description("Automated UI Test 0163 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Date Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0009' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0163()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0009")
                .InsertWFDateValue("01/09/2019")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0009 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5954")]
        [Description("Automated UI Test 0163.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Date Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0009' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0163_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0009")
                .InsertWFDateValue("02/09/2019")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5955")]
        [Description("Automated UI Test 0164 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Numeric Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0010' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0164()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0010")
                .InsertWFNumericValue("8")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0010 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5956")]
        [Description("Automated UI Test 0164.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Numeric Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0010' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0164_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0010")
                .InsertWFNumericValue("9")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }






        [TestProperty("JiraIssueID", "CDV6-5863")]
        [Description("Automated UI Test 0165 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Decimal Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0011' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0165()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0011")
                .InsertWFDecimalValue("9.50")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0011 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5864")]
        [Description("Automated UI Test 0165.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Decimal Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0011' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0165_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0011")
                .InsertWFDecimalValue("9.51")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5865")]
        [Description("Automated UI Test 0166 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Boolean Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0012' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0166()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0012")
                .SelectWFBoolean(true)
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0012 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5866")]
        [Description("Automated UI Test 0166.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Boolean Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0012' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0166_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0012")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-5867")]
        [Description("Automated UI Test 0167 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Paragraph Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0013' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0167()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0013")
                .InsertWFParagraph("V 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0013 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5868")]
        [Description("Automated UI Test 0167.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Paragraph Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0013' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0167_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0013")
                .InsertWFParagraph("V 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }




        [TestProperty("JiraIssueID", "CDV6-5869")]
        [Description("Automated UI Test 0168 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison Will Match (Short Aswer Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0014' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0168()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0014")
                .InsertQuestion1Role("V 1")
                .TapSaveButton(false);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("UI Testing - 43 - Rule Conditions Operator - Question - 0014 - Condition Activated");

        }


        [TestProperty("JiraIssueID", "CDV6-5870")]
        [Description("Automated UI Test 0168.1 - Validation for Rule Conditions - Compare Question to Placeholder - Comparison NOT Will Match (Short Aswer Questions)" +
           "Open a Case Record - Navigate to the Cases Section - Open Case Form record - " +
           "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the text 'UI Testing - 43 - Rule Conditions Operator - Question - 0014' on the WF Short Answer textbox" +
           "Tap the save button - Validate that the Client Side rule is executed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0168_1()
        {
            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 43 - Rule Conditions Operator - Question - 0014")
                .InsertQuestion1Role("V 2")
                .TapSaveButton()
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        #endregion


        #region Bug Fixing - 44 - Bug Fix Testing


        [TestProperty("JiraIssueID", "CDV6-5871")]
        [Description("Automated UI Test 0158 - ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_DocRules_UITestMethod0169()
        {
            commonMethodsDB.CreateSystemSetting("Form.PrintFormat", "Word", "desc ...", false, null);

            #region Person

            var firstName = "Gordon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

            #endregion

            #region Case

            var caseid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
            var caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            int assessmentstatusid = 1; //In Progress
            var assessmentStartDate = new DateTime(2021, 3, 19);
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_teamId, personID, fullName, _systemUserId, caseid, caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_2", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("UI Testing - 44 - Bug Fix Testing - 0001")
                .InsertWFNumericValue("22")
                .InsertWFDecimalValue("5.90")
                .TapSaveButton(true)
                .ValidateTestHQRow1Column2Question("3.73")
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectTemplate("Templace Will All Questions")
                .TapOnPrintButton()
                .TapOnCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString());

            Thread.Sleep(3000);

            object fileName = Path.Combine(DownloadsDirectory + "\\TemplaceWithAllQuestions.docx");
            Application wordApp = new Application { Visible = false };
            Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: true, Visible: false);
            aDoc.Activate();

            object findText = "3.73";
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            //execute find and replace
            bool resultFound = wordApp.Selection.Find.Execute(
                ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            aDoc.Close();

            Assert.IsTrue(resultFound);
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

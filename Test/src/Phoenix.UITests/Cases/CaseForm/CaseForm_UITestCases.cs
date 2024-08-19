using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\D_Flag.Zip"), DeploymentItem("Files\\Automated UI Test Document 1.Zip")]
    [DeploymentItem("Files\\Automated UI Test Document 2.Zip"), DeploymentItem("Files\\Person Automation Form 1.Zip")]
    [DeploymentItem("Files\\Automated UI Test Document 1 Rules.zip"), DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    [DeploymentItem("Files\\SCA - Brokerage.Zip"), DeploymentItem("Files\\Social Care Assessment Demo.Zip")]
    [DeploymentItem("Files\\Social Care Assessment Demo - BrokerageEpisode.Zip"), DeploymentItem("Files\\WF Automated Testing - CDV6-9273.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-9098.Zip")]
    public class CaseForm_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _ethnicityId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _defaultLoginUserID;
        private string _loginUsername;
        private string _loginUserFullName;
        private Guid _defaultUserId;
        private string _defaultUsername;
        private string _defaultUserFullname;
        private Guid _dataFormId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _personId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _caseId;
        private string _caseNumber;
        private string _caseTitle;
        private string _documentName;
        private Guid _documentId;
        private Guid _document2Id;
        private Guid _personAutomationForm1Id;
        private string firstName;
        private string lastName;
        private Guid systemSettingID1;
        private Guid systemSettingID2;

        #endregion

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

                _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_defaultUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region System User

                _loginUsername = "CaseFormsUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CaseForms", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

                #endregion

                #region Form.OnClosePrintFormat // Form.PrintFormat

                systemSettingID1 = commonMethodsDB.CreateSystemSetting("Form.OnClosePrintFormat", "Word", "Describe print format for assessment forms when assessment is closed. Valid values PDF or Word", false, "false");
                systemSettingID2 = commonMethodsDB.CreateSystemSetting("Form.PrintFormat", "Word", "Describe print format for assessment forms. Valid values PDF or Word", false, "false");

                dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
                dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "Word");

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Inpatient)", _careDirectorQA_TeamId);

                #endregion

                #region Document

                commonMethodsDB.CreateDocumentIfNeeded("D_Flag", "D_Flag.Zip"); //Import Document

                commonMethodsDB.ImportFormula("Sum two values_minus a third.Zip"); //Formula Import

                commonMethodsDB.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-10345", "WF Automated Testing - CDV6-10345.Zip"); //Workflow Import

                _documentName = "Automated UI Test Document 1";
                _documentId = commonMethodsDB.CreateDocumentIfNeeded(_documentName, "Automated UI Test Document 1.Zip");//Import Document

                _personAutomationForm1Id = commonMethodsDB.CreateDocumentIfNeeded("Person Automation Form 1", "Person Automation Form 1.Zip");//Import Document

                commonMethodsDB.ImportDocumentRules("Automated UI Test Document 1 Rules.zip");//Import Document Rules

                _document2Id = commonMethodsDB.CreateDocumentIfNeeded("Automated UI Test Document 2", "Automated UI Test Document 2.Zip");

                #endregion

                #region Person

                firstName = "Automation";
                lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

                #endregion

                #region Case

                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId, _defaultLoginUserID, _defaultLoginUserID, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
                _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Add new Case Form related tests


        [TestProperty("JiraIssueID", "CDV6-10101")]
        [Description("Automated UI Test 0001 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Validate the 'Form (Case): New' page field labels")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateAllFieldLabelsVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-10102")]
        [Description("Automated UI Test 0002 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Validate the 'Form (Case): New' page fields for the General area")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateCaseField(_caseTitle.ToString(), true, true)
                .ValidateFormTypeField("", false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", true, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("");


        }

        [TestProperty("JiraIssueID", "CDV6-10103")]
        [Description("Automated UI Test 0003 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Validate the 'Form (Case): New' page fields for the Additional Information area")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateSeparateAssessmentField(false)
                .ValidateCarerDeclinedJointAssessmentsField(false)
                .ValidateDelayReasonField("", false, true)
                .ValidateTargetStartDateField("")
                .ValidateTriggerDateField("")
                .ValidateJointCarerAssessmentField(false)
                .ValidateJointCarerField("")
                .ValidateNewPersonField(false)
                .ValidateTerminatedDateField("");


        }

        [TestProperty("JiraIssueID", "CDV6-10104")]
        [Description("Automated UI Test 0004 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Set the Form Type and Start Date - Tap on the Save button - Validate that the record is Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod004()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
            .WaitForLookupPopupToLoad()
            .TypeSearchQuery(_documentName)
            .TapSearchButton()
            .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateCaseField(_caseTitle, false, true)
                .ValidateFormTypeField(_documentName, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("");

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-10105")]
        [Description("Automated UI Test 0005 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Set values in all fields on the General Section - Tap on the Save button - " +
            "Validate that the record is Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod005()
        {
            DateTime CurrentDate = DateTime.Now;

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .TapResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_loginUsername)
                .TapSearchButton()
                .SelectResultElement(_defaultLoginUserID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertDueDate(CurrentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertReviewDate(CurrentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateCaseField(_caseTitle, false, true)
                .ValidateFormTypeField(_documentName, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField(_loginUserFullName, true, true)
                .ValidateDueDateField(CurrentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateReviewDateField(CurrentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"));

            dbHelper = new DBHelper.DatabaseHelper();
            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-10106")]
        [Description("Automated UI Test 0006 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Set values in all fields on the Additional Information Section - " +
            "Tap on the Save button - Validate that the record is Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod006()
        {
            DateTime CurrentDate = DateTime.Now;

            #region Form Delay Reason

            string formDelayReasonName = "Test_FDR";
            var formDelayReasonId = commonMethodsDB.CreateFormDelayReason(formDelayReasonName, new DateTime(2023, 1, 1), _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(CurrentDate.ToString("dd'/'MM'/'yyyy"))
                .SelectSeparateAssessment(true)
                .SelectCarerDeclinedJointAssessments(true)
                .TapDelayReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formDelayReasonName)
                .TapSearchButton()
                .SelectResultElement(formDelayReasonId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertTargetStartDate(CurrentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTriggerDate(CurrentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .SelectJointCarerAssessment(true)
                .SelectNewPerson(true)
                .InsertTerminateDate(CurrentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"))
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateSeparateAssessmentField(true)
                .ValidateCarerDeclinedJointAssessmentsField(true)
                .ValidateDelayReasonField(formDelayReasonName, true, true)
                .ValidateTargetStartDateField(CurrentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateTriggerDateField(CurrentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateJointCarerAssessmentField(true)
                .ValidateJointCarerField("")
                .ValidateNewPersonField(true)
                .ValidateTerminatedDateField(CurrentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"));

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-10107")]
        [Description("Automated UI Test 0007 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Set values in all mandatory fields - Tap on the Save and Close button - " +
            "Re-open the record - Validate that the record is Saved and the user is redirected back to the Forms (Case) page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod007()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            dbHelper = new DBHelper.DatabaseHelper();
            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);
            Guid recordID = caseformIDs[0];

            caseCasesFormPage
                .OpenRecord(recordID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateCaseField(_caseTitle, false, true)
                .ValidateFormTypeField(_documentName, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("");

        }

        [TestProperty("JiraIssueID", "CDV6-10108")]
        [Description("Automated UI Test 0008 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - Tap on the back button - Validate that the user is redirected to the Forms (Case) page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod008()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-10109")]
        [Description("Automated UI Test 0011 - Open a Case Record - Navigate to the Cases Section - " +
           "Tap on the Add New button - " +
           "Set no data in any of the fields -" +
           " Tap on the Save button -" +
           " Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod011()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("")
                .TapSaveButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateFormTypeErrorMessage("Please fill out this field.")
                .ValidateStartDateErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10110")]
        [Description("Automated UI Test 0012 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            " Select a Form Type" +
            " Tap on the Save button -" +
            " Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod012()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("")
                .TapSaveButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateStartDateErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10111")]
        [Description("Automated UI Test 0013 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            " Remove the value from the Case field" +
            " Validate that an error message is displayed below the Case field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod013()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapClearCaseButton()
                .ValidateCaseErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10112")]
        [Description("Automated UI Test 0014 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            " Remove the value from the Responsible Team field" +
            " Validate that an error message is displayed below the Responsible Team field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod014()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapClearResponsibleTeamButton()
                .ValidateResponsibleTeamErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10113")]
        [Description("Automated UI Test 0015 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            "Select a Form Type and Start Date - " +
            "Remove the value from the Case field " +
            "Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod015()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToShortDateString())
                .TapClearCaseButton()
                .TapSaveButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateCaseErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10114")]
        [Description("Automated UI Test 0016 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            "Select a Form Type and Start Date - " +
            "Remove the value from the Responsible Team field " +
            "Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod016()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToShortDateString())
                .TapClearResponsibleTeamButton()
                .TapSaveButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateResponsibleTeamErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10115")]
        [Description("Automated UI Test 0017 - Open a Case Record - Navigate to the Cases Section - " +
            "Tap on the Add New button - " +
            "Select a Form Type and Start Date - " +
            "Remove the value from the Responsible Team field " +
            "Tap on the Save and Close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod017()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToShortDateString())
                .TapClearResponsibleTeamButton()
                .TapSaveAndCloseButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateResponsibleTeamErrorMessage("Please fill out this field.");

        }

        #endregion

        #region Case Form Toolbar related tests

        [TestProperty("JiraIssueID", "CDV6-10116")]
        [Description("Automated UI Test 0009 - Open a Case Record - Navigate to the Cases Section - " +
            "Open a Case Form Record - Tap on the back button - Validate that the user is redirected to the Forms (Case) page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod9()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-10117")]
        [Description("Automated UI Test 0010 - Open a Case Record - Navigate to the Cases Section - " +
            "Open a Case Form Record - Tap on the toolbar (3 dots) button - " +
            "Validate that the Print History; Share; Assign; Restrict Access; Delete buttons are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod10()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .WaitForAllToolbarIconsToBeVisible();


        }

        #endregion

        #region Printing related tests

        [TestProperty("JiraIssueID", "CDV6-10118")]
        [Description("Automated UI Test 0018 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - Validate that the Print Assessment window is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod18()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-10119")]
        [Description("Automated UI Test 0019 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select an inner section - Validate that the 'Sections to Include:' only contains the selected section (with sub-sections)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod19()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("   Section 1")
                .CheckSection1Visibility(true)
                .CheckSection2Visibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-10120")]
        [Description("Automated UI Test 0020 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select an inner section - Validate that the 'Template' element is filtered accoarding to the selected section")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod20()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("      Section 1.1")
                .ValidateListOfTemplates("Generic Template for Section 1.1");

        }

        [TestProperty("JiraIssueID", "CDV6-10121")]
        [Description("Automated UI Test 0021 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select an inner section - Re-Select the default (All) section option - " +
            "Validate that the popup displais all options in the 'Sections to Include' area ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod21()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("      Section 1.1")
                .SelectSection("All")
                .ValidatePopupLoadedForAllSections();

        }

        [TestProperty("JiraIssueID", "CDV6-10122")]
        [Description("Automated UI Test 0022 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the 'Print blank document?' Option - " +
            "Validate that the 'Print only selected values?' and 'Save Document in Print History?' checkboxes get disabled ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod22()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnPrintBlankDocumentCheckBox()
                .CheckIfPrintOnlySelectedValuesCheckboxDisabled(true)
                .CheckIfSaveDocumentInPrintHistoryCheckboxDisabled(true);

        }

        [TestProperty("JiraIssueID", "CDV6-10123")]
        [Description("Automated UI Test 0022.1 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the 'Save Document in Print History?' Option - " +
            "Validate that the Comments textbox is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod22_1()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .CheckCommentsTextboxVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-10124")]
        [Description("Automated UI Test 0022.2 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the 'Save Document in Print History?' Option (check it) - " +
            "Select the 'Save Document in Print History?' Option a seconds time (un-check it) - " +
            "Validate that the Comments textbox is no longer displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod22_2()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .CheckCommentsTextboxVisibility(false);
        }


        [TestProperty("JiraIssueID", "CDV6-10125")]
        [Description("Automated UI Test 0023 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select a section that do not allow for the print of blank documents - " +
            "Validate that the 'Print blank document?' option is not visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod23()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("   Section 1")
                .CheckPrintBlankDocumentVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-10126")]
        [Description("Automated UI Test 0024 - Open a Case Record - Navigate to the Cases Section - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Tap on the Print button - " +
            "Validate that the template file is downloaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod24()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnPrintButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "Phoenix Project Test Plan.docx");
            Assert.IsTrue(fileExists);
        }

        [TestProperty("JiraIssueID", "CDV6-10127")]
        [Description("Automated UI Test 0025 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Tap on the close button - " +
            "Validate that the user is redirected back to the Case Form")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod25()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-10128")]
        [Description("Automated UI Test 0026 - Open a Case Record - Navigate to the Cases Section - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select a sub-section of the Assessment - Tap on the Print button - " +
            "Validate that the correct template file (for the selected section) is downloaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod26()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("      Section 1.1")
                .TapOnPrintButton();

            System.Threading.Thread.Sleep(2000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Phoenix Project - Test Design Specification.docx");
            Assert.IsTrue(fileExists);
        }


        #region Issue https://advancedcsg.atlassian.net/browse/CDV6-3571


        [TestProperty("JiraIssueID", "CDV6-10129")]
        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = PDF & Form.OnClosePrintFormat = PDF - " +
            "Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select a sub-section of the Assessment - Tap on the Print button - " +
            "Validate that a new popup window is open with the PDF file")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_PrintPDFForAssessmenets_UITestMethod01()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "PDF");

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("      Section 1.1")
                .TapOnPrintButton()
                .ValidatePDFPopupIsOpen();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Phoenix Project - Test Design Specification.pdf");
            Assert.IsTrue(fileExists);
        }

        [TestProperty("JiraIssueID", "CDV6-10130")]
        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = PDF & Form.OnClosePrintFormat = PDF - " +
            "Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select a sub-section of the Assessment - Tap on the Print button - " +
            "Validate that no file is downloaded into the browser downloads folder")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_PrintPDFForAssessmenets_UITestMethod02()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "PDF");

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectSection("      Section 1.1")
                .TapOnPrintButton()
                .ValidatePDFPopupIsOpen();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.pdf");
            Assert.IsTrue(fileExists);

            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsFalse(fileExists);
        }

        #endregion


        #endregion

        #region Print history Related Tests

        [TestProperty("JiraIssueID", "CDV6-10131")]
        [Description("Automated UI Test 0027 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Print Record - Validate that the printing info is stored in the Print History section")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod27()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, false, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();

            string auditTitle = "Audit Record";
            string auditLine1Text = "Record is created for auditing purposes only.";
            string auditCreationDate = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;
            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .ValidateHistoryRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName);
        }

        [TestProperty("JiraIssueID", "CDV6-10132")]
        [Description("Automated UI Test 0028 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Validate that the printing info is stored in the Print History section")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod28()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();

            string auditTitle = "Phoenix Project Test Plan.docx";
            string auditLine1Text = "Comment 1 \nComment 2";
            string auditCreationDate = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;
            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .ValidateHistoryWithSavedRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName);
        }

        [TestProperty("JiraIssueID", "CDV6-10133")]
        [Description("Automated UI Test 0029 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Navigate to the Print History popup - Tap on the Audit Record Link - Validate that the print document is downloaded again")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod29()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            string auditTitle = "Phoenix Project Test Plan.docx";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapAuditLink(recordID.ToString(), auditTitle);

            System.Threading.Thread.Sleep(2000);
            bool file1Exists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "Phoenix Project Test Plan.docx");
            bool file2Exists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "Phoenix Project Test Plan (1).docx");
            Assert.IsTrue(file1Exists);
            Assert.IsTrue(file2Exists);
        }

        [TestProperty("JiraIssueID", "CDV6-10134")]
        [Description("Automated UI Test 0030 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Navigate to the Print History popup - Tap on the Audit Record delete button - Confirm Delete - " +
            "Validate that audit record is deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod30()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapDeletebutton(recordID.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to delete this record?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            int totalNumberOfFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid").Count;
            Assert.AreEqual(0, totalNumberOfFields);
        }

        [TestProperty("JiraIssueID", "CDV6-10135")]
        [Description("Automated UI Test 0031 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Navigate to the Print History popup - Tap on the Audit Record delete button - Cancel Delete - " +
            "Validate that audit record is not deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod31()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapDeletebutton(recordID.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to delete this record?")
                .TapCancelButton();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoadAfterAlert();

            int totalNumberOfFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid").Count;
            Assert.IsTrue(totalNumberOfFields > 0);
        }

        [TestProperty("JiraIssueID", "CDV6-10136")]
        [Description("Automated UI Test 0031.1 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Navigate to the Print History popup - Tap on the Audit Record Edit button - Insert a new Title and Comments - " +
            "Tap on the Save button - Validate that the Audit record is updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod31_1()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdon = ((DateTime)recordFields["createdon"]).ToLocalTime();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapEditbutton(recordID.ToString());

            editPrintAuditRecordPopup
                .WaitForEditPrintAuditRecordPopupToLoad()
                .InsertTitle("Updated Title")
                .InsertComments("Comment 1/nComment 2/nComment 3")
                .TapSaveButton();

            string auditTitle = "Updated Title";
            string auditLine1Text = "Comment 1/nComment 2/nComment 3";
            string auditCreationDate = createdon.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;

            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoadAfterAlert()
                .ValidateHistoryWithSavedRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName);
        }

        [TestProperty("JiraIssueID", "CDV6-10137")]
        [Description("Automated UI Test 0031.1 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Print assessment button - " +
            "Select the option 'Save Document in Print History?' - Insert a comment in the Comments textbox - Print Record - " +
            "Navigate to the Print History popup - Tap on the Audit Record Edit button - wait for the Edit popup to open - " +
            "Tap on the Close Button - Validate that the popup is closed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod31_2()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdon = ((DateTime)recordFields["createdon"]).ToLocalTime();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapEditbutton(recordID.ToString());

            editPrintAuditRecordPopup
                .WaitForEditPrintAuditRecordPopupToLoad()
                .InsertTitle("Updated Title")
                .InsertComments("Comment 1/nComment 2/nComment 3")
                .TapCloseButton();

            string auditTitle = "Phoenix Project Test Plan.docx";
            string auditLine1Text = "Comment 1 \nComment 2";
            string auditCreationDate = createdon.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;

            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoadAfterAlert()
                .ValidateHistoryWithSavedRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName);
        }

        [TestProperty("JiraIssueID", "CDV6-10138")]
        [Description("Automated UI Test 0032 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (with no print history) - Navigate to the Print History popup - " +
            "Validate that a no records message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod32()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();


            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .CheckNoPrintRecordsMessageVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-10139")]
        [Description("Automated UI Test 0033 - Open a Case Record - Navigate to the Cases Section - " +
        "Open Case Form record (with print history records) - Navigate to the Print History popup - " +
        "No Records message should not be displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod33()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .SelectTemplate("Templace Will All Questions")
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .CheckNoPrintRecordsMessageVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-10140")]
        [Description("Automated UI Test 0034 - Open a Case Record - Navigate to the Cases Section - " +
        "Open Case Form record (with print history records) - Navigate to the Print History popup - " +
        "Filter the print results for a Specific Section")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod34()
        {
            #region Case Form

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            #endregion

            #region Document Section

            Guid DocumentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1.1").FirstOrDefault();

            #endregion

            #region Assessment Section

            Guid AssessmentSectionId = dbHelper.assessmentSection.GetAssessmentSection(caseFormID, DocumentSectionId)[0];

            #endregion

            #region Assessment Print Record

            var recordID1 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Section 1.1", AssessmentSectionId, caseFormID, "caseform", 1);

            var recordFieldstest = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID1, "createdon");
            DateTime createdOntest = ((DateTime)recordFieldstest["createdon"]).ToLocalTime();
            var createdOnDateTime = createdOntest.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            var recordID2 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Document", null, caseFormID, "caseform", 1);

            var recordID3 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Document", null, caseFormID, "caseform", 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            string auditTitle = "Audit Record";
            string auditLineText = "Record is created for auditing purposes only.";
            string auditTemplateName = "Generic Template for Section 1.1";
            string additionalComment = "Section print of \"Section 1.1\"";
            string defaultUserFullName = "System Administrator";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .SelectSection("      Section 1.1")
                .TapFilterButton()
                .ValidateHistoryRecordPresent(recordID1.ToString(), auditTitle, auditLineText, createdOnDateTime, defaultUserFullName, auditTemplateName, additionalComment)
                .ValidateHistoryRecordNotPresent(recordID2.ToString())
                .ValidateHistoryRecordNotPresent(recordID3.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-10141")]
        [Description("Automated UI Test 0035 - Open a Case Record - Navigate to the Cases Section - " +
        "Open Case Form record (with print history records) - Navigate to the Print History popup - " +
        "Select a specific Section and Tap the Filter Button - Select 'All' sections and tap the filter button again - " +
        "All records should be displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod35()
        {
            #region Case Form

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            #endregion

            #region Document Section

            Guid DocumentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1.1").FirstOrDefault();

            #endregion

            #region Assessment Section

            Guid AssessmentSectionId = dbHelper.assessmentSection.GetAssessmentSection(caseFormID, DocumentSectionId)[0];

            #endregion

            #region Assessment Print Record

            var recordID1 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Section 1.1", AssessmentSectionId, caseFormID, "caseform", 1);

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID1, "createdon");
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();
            var createdOnDateTime = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            var recordID2 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Document", null, caseFormID, "caseform", 1);

            var recordFields2 = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID2, "createdon");
            DateTime createdOn2 = ((DateTime)recordFields2["createdon"]).ToLocalTime();
            var createdOnDateTime2 = createdOn2.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            string auditTitle = "Audit Record";
            string auditLineText = "Record is created for auditing purposes only.";
            string auditTemplateName = "Generic Template for Section 1.1";
            string additionalComment = "Section print of \"Section 1.1\"";
            string auditTemplateName2 = "Generic Template for Document";
            string defaultUserFullName = "System Administrator";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .SelectSection("      Section 1.1")
                .TapFilterButton()
                .SelectSection("All")
                .TapFilterButton()
                .ValidateHistoryRecordPresent(recordID1.ToString(), auditTitle, auditLineText, createdOnDateTime, defaultUserFullName, auditTemplateName, additionalComment)
                .ValidateHistoryRecordPresent(recordID2.ToString(), auditTitle, auditLineText, createdOnDateTime2, defaultUserFullName, auditTemplateName2);

        }

        [TestProperty("JiraIssueID", "CDV6-10142")]
        [Description("Automated UI Test 0036 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (with print history records) - Navigate to the Print History popup - " +
            "Tap on the 'Exclude Print Audit Records?' checkbox - Tap on the Filter button " +
            "Only the documents saved in Print History should be displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod36()
        {
            #region Case Form

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            #endregion

            #region Document Section

            Guid DocumentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1.1").FirstOrDefault();

            #endregion

            #region Assessment Section

            Guid AssessmentSectionId = dbHelper.assessmentSection.GetAssessmentSection(caseFormID, DocumentSectionId)[0];

            #endregion

            #region Assessment Print Record

            var recordID1 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Phoenix Project Test Plan.docx", "Document saved in print history",
                            "Generic Template for Document", null, caseFormID, "caseform", 0);

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID1, "createdon");
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();
            var createdOnDateTime = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            var recordID2 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Section 1.1", AssessmentSectionId, caseFormID, "caseform", 1);

            var recordID3 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Document", null, caseFormID, "caseform", 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            string auditTitle = "Phoenix Project Test Plan.docx";
            string auditLine1Text = "Document saved in print history";
            string auditUserName = "System Administrator";
            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapExcludePrintAuditRecordsCheckbox()
                .TapFilterButton()
                .ValidateHistoryWithSavedRecordPresent(recordID1.ToString(), auditTitle, auditLine1Text, createdOnDateTime, auditUserName, auditTemplateName)
                .ValidateHistoryRecordNotPresent(recordID2.ToString())
                .ValidateHistoryRecordNotPresent(recordID3.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-10143")]
        [Description("Automated UI Test 0037 - Open a Case Record - Navigate to the Cases Section - " +
        "Open Case Form record (with print history records) - Navigate to the Print History popup - " +
        "Tap on the 'Exclude Print Audit Records?' checkbox - Tap on the Filter button - " +
        "Uncheck the check box and tap on the filter button again" +
        "All audit records should be displayed should be displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod37()
        {
            #region Case Form

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            #endregion

            #region Document Section

            Guid DocumentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1.1").FirstOrDefault();

            #endregion

            #region Assessment Section

            Guid AssessmentSectionId = dbHelper.assessmentSection.GetAssessmentSection(caseFormID, DocumentSectionId)[0];

            #endregion

            #region Assessment Print Record

            var recordID1 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Phoenix Project Test Plan.docx", "Document saved in print history",
                            "Generic Template for Document", null, caseFormID, "caseform", 0);

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID1, "createdon");
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();
            var createdOnDateTime = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            var recordID2 = dbHelper.assessmentPrintRecord.CreateAssessmentPrintRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Audit Record", "Record is created for auditing purposes only.",
                            "Generic Template for Document", null, caseFormID, "caseform", 1);

            var recordFields2 = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordByID(recordID2, "createdon");
            DateTime createdOn2 = ((DateTime)recordFields2["createdon"]).ToLocalTime();
            var createdOnDateTime2 = createdOn2.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            string auditTitle = "Phoenix Project Test Plan.docx";
            string auditLine1Text = "Document saved in print history";
            string auditUserName = "System Administrator";
            string auditTemplateName = "Generic Template for Document";

            string auditTitle2 = "Audit Record";
            string auditLine1Text2 = "Record is created for auditing purposes only.";
            string auditTemplateName2 = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .TapExcludePrintAuditRecordsCheckbox()
                .TapFilterButton()
                .TapExcludePrintAuditRecordsCheckbox()
                .TapFilterButton()
                .ValidateHistoryWithSavedRecordPresent(recordID1.ToString(), auditTitle, auditLine1Text, createdOnDateTime, auditUserName, auditTemplateName)
                .ValidateHistoryRecordPresent(recordID2.ToString(), auditTitle2, auditLine1Text2, createdOnDateTime2, auditUserName, auditTemplateName2);
        }


        #region issue https://advancedcsg.atlassian.net/browse/CDV6-3571

        [TestProperty("JiraIssueID", "CDV6-10144")]
        [Description("Issue - https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word - " +
            "Open Case Form record - Tap on the Print assessment button - Print Record - " +
            "Validate that the printing info is stored in the Print History section - Print History Icon must identify that the printed document was a word document")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_PrintPDFForAssessmenets_UITestMethod03()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();

            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();

            string auditTitle = "Phoenix Project Test Plan.docx";
            string auditLine1Text = "Comment 1 \nComment 2";
            string auditCreationDate = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;
            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .ValidateHistoryWithSavedRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName)
                .ValidatePrintHistoryRecordWordIcon(recordID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-10145")]
        [Description("Issue - https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = PDF & Form.OnClosePrintFormat = PDF - " +
            "Open Case Form record - Tap on the Print assessment button - Print Record - " +
            "Validate that the printing info is stored in the Print History section - Print History Icon must identify that the printed document was a PDF document")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_PrintPDFForAssessmenets_UITestMethod04()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "PDF");

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapPrintButton();

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad()
                .TapOnSaveDocumentInPrintHistoryCheckbox()
                .InsertPrintCommentsInTextbox("Comment 1 \nComment 2")
                .TapOnPrintButton()
                .TapOnCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapPrintHistoryButton();


            var recordFields = dbHelper.assessmentPrintRecord.GetAssessmentPrintRecordForCaseForm(caseFormID, true, "assessmentprintrecordid", "createdon");
            var recordID = (recordFields["assessmentprintrecordid"]).ToString();
            DateTime createdOn = ((DateTime)recordFields["createdon"]).ToLocalTime();

            string auditTitle = "Phoenix Project Test Plan.pdf";
            string auditLine1Text = "Comment 1 \nComment 2";
            string auditCreationDate = createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string auditUserName = _loginUserFullName;

            string auditTemplateName = "Generic Template for Document";

            assessmentPrintHistoryPopup
                .WaitForAssessmentPrintHistoryPopupToLoad()
                .ValidateHistoryWithSavedRecordPresent(recordID.ToString(), auditTitle, auditLine1Text, auditCreationDate, auditUserName, auditTemplateName)
                .ValidatePrintHistoryRecordPDFIcon(recordID.ToString());
        }

        #endregion


        #endregion

        #region Case Form Share related tests

        [TestProperty("JiraIssueID", "CDV6-10146")]
        [Description("Automated UI Test 0038 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Share Button - Validate that the Share popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod38()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapShareButton()
                .WaitForShareRecordPopupToLoad();
        }


        //[TestProperty("JiraIssueID", "CDV6-10147")]
        //[Description("Automated UI Test 0039 - Open a Case Record - Navigate to the Cases Section - " +
        //"Open Case Form record (shared with user) - Tap on the Share Button - " +
        //"Validate the User share information")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod39()
        //{
        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    string userFullName = "TestUser 1987";
        //    string username = "TestUser1987";
        //    Guid userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName, true, true, true);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString());

        //    caseFormPage
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton();

        //    shareRecordPopup
        //        .WaitForShareRecordPopupToLoad()
        //        .ValidateUserOrTeamInformationPresent(recordLevelAccessID.ToString(), userFullName)
        //        .ValidateViewAccessToUserOrTeam(recordLevelAccessID.ToString(), true)
        //        .ValidateEditAccessToUserOrTeam(recordLevelAccessID.ToString(), true)
        //        .ValidateDeleteAccessToUserOrTeam(recordLevelAccessID.ToString(), true)
        //        .ValidateShareAccessToUserOrTeam(recordLevelAccessID.ToString(), false);
        //}

        //[TestProperty("JiraIssueID", "CDV6-10148")]
        //[Description("Automated UI Test 0040 - Open a Case Record - Navigate to the Cases Section - " +
        //"Open Case Form record (shared with team) - Tap on the Share Button - " +
        //"Validate the Team share information")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod40()
        //{
        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    string teamFullName = "TorfaenSocialCare";
        //    Guid teamID = commonMethodsDB.CreateTeam(teamFullName, null, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithTeam(caseFormID, teamID, teamFullName, true, true, true);
        //    Guid recordLevelAccesID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString());

        //    caseFormPage
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton();

        //    shareRecordPopup
        //        .WaitForShareRecordPopupToLoad()
        //        .ValidateUserOrTeamInformationPresent(recordLevelAccesID.ToString(), teamFullName)
        //        .ValidateViewAccessToUserOrTeam(recordLevelAccesID.ToString(), true)
        //        .ValidateEditAccessToUserOrTeam(recordLevelAccesID.ToString(), true)
        //        .ValidateDeleteAccessToUserOrTeam(recordLevelAccesID.ToString(), true)
        //        .ValidateShareAccessToUserOrTeam(recordLevelAccesID.ToString(), false);
        //}


        [TestProperty("JiraIssueID", "CDV6-10149")]
        [Description("Automated UI Test 0041 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (not shared with any user or team) - Tap on the Share Button - " +
            "Validate the 'No Records' Message ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod41()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapShareButton()
                .WaitForShareRecordPopupToLoad()
                .ValidateNoRecordsMessages();
        }

        [TestProperty("JiraIssueID", "CDV6-10150")]
        [Description("Automated UI Test 0042 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (not shared with any user or team) - Tap on the Share Button - " +
            "Search for a user record - Validate the Share Record Results Popup information")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod42()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            string userFullName = "TestUser 1987";
            string username = "TestUser1987";
            var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            string teamFullName = "TorfaenSocialCare";
            commonMethodsDB.CreateTeam(teamFullName, userID, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapShareButton()
                .WaitForShareRecordPopupToLoad()
                .SearchForUserRecord(userFullName);

            shareRecordResultsPopup
                .WaitForShareRecordResultsPopupToLoad()
                .ValidateUserRecordPresent(userID.ToString(), userFullName, "CareDirector QA", username, false);
        }

        //[TestProperty("JiraIssueID", "CDV6-10151")]
        //[Description("Automated UI Test 0043 - Open a Case Record - Navigate to the Cases Section - " +
        //   "Open Case Form record (shared with a user) - Tap on the Share Button - " +
        //   "Search for the user record already added in the share - Validate the Share Record Results Popup information")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod43()
        //{
        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    string userFullName = "TestUser 1987";
        //    string username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    string teamFullName = "TorfaenSocialCare";
        //    commonMethodsDB.CreateTeam(teamFullName, userID, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName, true, true, true);


        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .SearchForUserRecord(userFullName);


        //    shareRecordResultsPopup
        //        .WaitForShareRecordResultsPopupToLoad()
        //        .ValidateUserRecordPresent(userID.ToString(), userFullName, "CareDirector QA", username, true);
        //}

        //[TestProperty("JiraIssueID", "CDV6-10152")]
        //[Description("Automated UI Test 0044 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (not shared with any user or team) - Tap on the Share Button - " +
        //    "Search for a Team record - Validate the Share Record Results Popup information")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod44()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var teamFullName = "Torfaen - Social Care";
        //    var teamID = commonMethodsDB.CreateTeam(teamFullName, _defaultUserId, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .SearchForTeamRecord(teamFullName);


        //    shareRecordResultsPopup
        //        .WaitForShareRecordResultsPopupToLoad()
        //        .ValidateTeamRecordPresent(teamID.ToString(), teamFullName, false);
        //}

        //[TestProperty("JiraIssueID", "CDV6-10153")]
        //[Description("Automated UI Test 0045 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (Shared with a team) - Tap on the Share Button - " +
        //    "Search for the Team record already added in the share - Validate the Share Record Results Popup information")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod45()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var teamFullName = "Torfaen - Social Care";
        //    var teamID = commonMethodsDB.CreateTeam(teamFullName, _defaultUserId, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithTeam(caseFormID, teamID, teamFullName);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .SearchForTeamRecord(teamFullName);

        //    shareRecordResultsPopup
        //        .WaitForShareRecordResultsPopupToLoad()
        //        .ValidateTeamRecordPresent(teamID.ToString(), teamFullName, true);
        //}

        [TestProperty("JiraIssueID", "CDV6-10154")]
        [Description("Automated UI Test 0046 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Share Button - " +
            "Search for a User record - Use a search query that will return no results - " +
            "Validate the Share Record Results Popup no results message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod46()
        {
            var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapShareButton()
                .WaitForShareRecordPopupToLoad()
                .SearchForUserRecord("NotValidSearchQuery");

            shareRecordResultsPopup
                .WaitForShareRecordResultsPopupToLoad()
                .ValidateNoResultsMessage(true);
        }

        [TestProperty("JiraIssueID", "CDV6-10155")]
        [Description("Automated UI Test 0047 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Share Button - " +
            "Search for a Team record - Use a search query that will return no results - " +
            "Validate the Share Record Results Popup no results message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod47()
        {
            var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapShareButton()
                .WaitForShareRecordPopupToLoad()
                .SearchForTeamRecord("NotValidSearchQuery");

            shareRecordResultsPopup
                .WaitForShareRecordResultsPopupToLoad()
                .ValidateNoResultsMessage(true);
        }

        //[TestProperty("JiraIssueID", "CDV6-10156")]
        //[Description("Automated UI Test 0048 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record - Tap on the Share Button - " +
        //    "Search for a User record - Add the user record to the Share - Close the results popup" +
        //    "Validate that the record is shared with the user")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod48()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .SearchForUserRecord(userFullName);


        //    shareRecordResultsPopup
        //        .WaitForShareRecordResultsPopupToLoad()
        //        .TapAddUserButton(userID.ToString());

        //    List<Guid> shares = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID);
        //    Assert.AreEqual(1, shares.Count);
        //    Guid recordLevelAccessID = shares[0];

        //    shareRecordPopup
        //        .WaitForResultsPopupToClose()
        //        .ValidateViewAccessToUserOrTeam(recordLevelAccessID.ToString(), true)
        //        .ValidateEditAccessToUserOrTeam(recordLevelAccessID.ToString(), false)
        //        .ValidateDeleteAccessToUserOrTeam(recordLevelAccessID.ToString(), false)
        //        .ValidateShareAccessToUserOrTeam(recordLevelAccessID.ToString(), false);
        //}

        //[TestProperty("JiraIssueID", "CDV6-10157")]
        //[Description("Automated UI Test 0049 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record - Tap on the Share Button - " +
        //    "Search for a Team record - Add the team record to the Share - Close the results popup" +
        //    "Validate that the record is shared with the user")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod49()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var teamFullName = "Torfaen - Social Care";
        //    var teamID = commonMethodsDB.CreateTeam(teamFullName, _defaultUserId, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .SearchForTeamRecord(teamFullName);

        //    shareRecordResultsPopup
        //        .WaitForShareRecordResultsPopupToLoad()
        //        .TapAddTeamButton(teamID.ToString());

        //    List<Guid> shares = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID);
        //    Assert.AreEqual(1, shares.Count);
        //    Guid recordLevelAccessID = shares[0];

        //    shareRecordPopup
        //        .WaitForResultsPopupToClose()
        //        .ValidateViewAccessToUserOrTeam(recordLevelAccessID.ToString(), true)
        //        .ValidateEditAccessToUserOrTeam(recordLevelAccessID.ToString(), false)
        //        .ValidateDeleteAccessToUserOrTeam(recordLevelAccessID.ToString(), false)
        //        .ValidateShareAccessToUserOrTeam(recordLevelAccessID.ToString(), false);
        //}

        //[TestProperty("JiraIssueID", "CDV6-10158")]
        //[Description("Automated UI Test 0050 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (Shared to a User) - Tap on the Share Button - " +
        //    "Tap on the delete button for the user share - Validate that the share was removed")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod50()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .RemoveShare(recordLevelAccessID.ToString())
        //        .ValidateNoRecordsMessages();

        //    int totalShares = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID).Count;
        //    Assert.AreEqual(0, totalShares);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10159")]
        //[Description("Automated UI Test 0051 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (Shared to a Team) - Tap on the Share Button - " +
        //    "Tap on the delete button for the user share - Validate that the share was removed")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod51()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var teamFullName = "Torfaen - Social Care";
        //    var teamID = commonMethodsDB.CreateTeam(teamFullName, _defaultUserId, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithTeam(caseFormID, teamID, teamFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .RemoveShare(recordLevelAccessID.ToString())
        //        .ValidateNoRecordsMessages();

        //    int totalShares = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID).Count;
        //    Assert.AreEqual(0, totalShares);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10160")]
        //[Description("Automated UI Test 0052 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (Shared to a User) - Tap on the Share Button - " +
        //    "Enable Edit access - Tap on the save button - validate that the Edit permission is added to the share")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod52()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .TapEditCheckbox(recordLevelAccessID.ToString())
        //        .TapSaveButton();

        //    bool viewPermission = dbHelper.recordLevelAccess.GetViewPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool editPermission = dbHelper.recordLevelAccess.GetEditPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool deletePermission = dbHelper.recordLevelAccess.GetDeletePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool sharePermission = dbHelper.recordLevelAccess.GetSharePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);

        //    Assert.AreEqual(true, viewPermission);
        //    Assert.AreEqual(true, editPermission);
        //    Assert.AreEqual(false, deletePermission);
        //    Assert.AreEqual(false, sharePermission);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10161")]
        //[Description("Automated UI Test 0053 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record (Shared to a User) - Tap on the Share Button - " +
        //    "Enable Delete access - Tap on the save button - validate that the Delete permission is added to the share")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod53()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .TapDeleteCheckbox(recordLevelAccessID.ToString())
        //        .TapSaveButton();

        //    bool viewPermission = dbHelper.recordLevelAccess.GetViewPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool editPermission = dbHelper.recordLevelAccess.GetEditPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool deletePermission = dbHelper.recordLevelAccess.GetDeletePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool sharePermission = dbHelper.recordLevelAccess.GetSharePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);

        //    Assert.AreEqual(true, viewPermission);
        //    Assert.AreEqual(false, editPermission);
        //    Assert.AreEqual(true, deletePermission);
        //    Assert.AreEqual(false, sharePermission);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10162")]
        //[Description("Automated UI Test 0054 - Open a Case Record - Navigate to the Cases Section - " +
        //  "Open Case Form record (Shared to a User) - Tap on the Share Button - " +
        //  "Enable Share access - Tap on the save button - validate that the Share permission is added to the share")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod54()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .TapShareCheckbox(recordLevelAccessID.ToString())
        //        .TapSaveButton();

        //    bool viewPermission = dbHelper.recordLevelAccess.GetViewPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool editPermission = dbHelper.recordLevelAccess.GetEditPermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool deletePermission = dbHelper.recordLevelAccess.GetDeletePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);
        //    bool sharePermission = dbHelper.recordLevelAccess.GetSharePermissionForRecordLevelAccess(recordLevelAccessID, caseFormID);

        //    Assert.AreEqual(true, viewPermission);
        //    Assert.AreEqual(false, editPermission);
        //    Assert.AreEqual(false, deletePermission);
        //    Assert.AreEqual(true, sharePermission);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10163")]
        //[Description("Automated UI Test 0055 - Open a Case Record - Navigate to the Cases Section - " +
        //  "Open Case Form record (Shared to a User with View permission only) - Tap on the Share Button - " +
        //  "Enable View permission checkbox - Tap on the save button - validate that the Share was removed from the user")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod55()
        //{
        //    var caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    var userFullName = "TestUser 1987";
        //    var username = "TestUser1987";
        //    var userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    Guid recordLevelAccessID = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .TapViewCheckbox(recordLevelAccessID.ToString())
        //        .TapSaveButton();

        //    int numberOfShares = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID).Count;

        //    Assert.AreEqual(0, numberOfShares);

        //}

        //[TestProperty("JiraIssueID", "CDV6-10164")]
        //[Description("Automated UI Test 0056 - Open a Case Record - Navigate to the Cases Section - " +
        //  "Open Case Form record (Shared to a User and Team with View permission only) - Tap on the Share Button - " +
        //  "Tap on the Edit permission header link - Tap the Save button - Validate that all shares now include the Edit permission")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void Phoenix_CaseForms_UITestMethod56()
        //{
        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

        //    string userFullName = "TestUser 1987";
        //    string username = "TestUser1987";
        //    Guid userID = commonMethodsDB.CreateSystemUserRecord(username, "TestUser", "1987", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);            

        //    string teamFullName = "TorfaenSocialCare";
        //    Guid teamID = commonMethodsDB.CreateTeam(teamFullName, userID, _careDirectorQA_BusinessUnitId, "TSC", "tsc@someemail.com", "TEAM TSC", "0987654321");

        //    dbHelper.recordLevelAccess.RemoveAllSharesForCaseFormRecord(caseFormID);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithUser(caseFormID, userID, userFullName);
        //    dbHelper.recordLevelAccess.ShareCaseFormRecordWithTeam(caseFormID, teamID, teamFullName);
        //    Guid recordLevelAccessID1 = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[0];
        //    Guid recordLevelAccessID2 = dbHelper.recordLevelAccess.GetRecordLevelAccessForCaseForm(caseFormID)[1];


        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection()
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(_caseNumber, _caseId.ToString())
        //        .OpenCaseRecord(_caseId.ToString(), _caseNumber)
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase()
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString())
        //        .WaitForCaseFormPageToLoad()
        //        .TapAdditionalToolbarElementsbutton()
        //        .TapShareButton()
        //        .WaitForShareRecordPopupToLoad()
        //        .TapEditHeaderLink()
        //        .TapSaveButton();

        //    bool viewPermissionRecord1 = dbHelper.recordLevelAccess.GetViewPermissionForRecordLevelAccess(recordLevelAccessID1, caseFormID);
        //    bool editPermissionRecord1 = dbHelper.recordLevelAccess.GetEditPermissionForRecordLevelAccess(recordLevelAccessID1, caseFormID);
        //    bool deletePermissionRecord1 = dbHelper.recordLevelAccess.GetDeletePermissionForRecordLevelAccess(recordLevelAccessID1, caseFormID);
        //    bool sharePermissionRecord1 = dbHelper.recordLevelAccess.GetSharePermissionForRecordLevelAccess(recordLevelAccessID1, caseFormID);

        //    bool viewPermissionRecord2 = dbHelper.recordLevelAccess.GetViewPermissionForRecordLevelAccess(recordLevelAccessID2, caseFormID);
        //    bool editPermissionRecord2 = dbHelper.recordLevelAccess.GetEditPermissionForRecordLevelAccess(recordLevelAccessID2, caseFormID);
        //    bool deletePermissionRecord2 = dbHelper.recordLevelAccess.GetDeletePermissionForRecordLevelAccess(recordLevelAccessID2, caseFormID);
        //    bool sharePermissionRecord2 = dbHelper.recordLevelAccess.GetSharePermissionForRecordLevelAccess(recordLevelAccessID2, caseFormID);

        //    Assert.AreEqual(true, viewPermissionRecord1);
        //    Assert.AreEqual(true, editPermissionRecord1);
        //    Assert.AreEqual(false, deletePermissionRecord1);
        //    Assert.AreEqual(false, sharePermissionRecord1);

        //    Assert.AreEqual(true, viewPermissionRecord2);
        //    Assert.AreEqual(true, editPermissionRecord2);
        //    Assert.AreEqual(false, deletePermissionRecord2);
        //    Assert.AreEqual(false, sharePermissionRecord2);

        //}

        #endregion

        #region Delete Case Form Record

        [TestProperty("JiraIssueID", "CDV6-10165")]
        [Description("Automated UI Test 0057 - Open a Case Record - Navigate to the Cases Section - " +
         "Open Case Form record - Tap on the Delete Button - Confirm the Delete - Validate that the recor is no longer available")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod57()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            int totalCaseForms = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId).Count;
            Assert.AreEqual(0, totalCaseForms);
        }

        [TestProperty("JiraIssueID", "CDV6-10166")]
        [Description("Automated UI Test 0058 - Open a Case Record - Navigate to the Cases Section - " +
         "Open Case Form record - Tap on the Delete Button - Cancel the Delete - Validate that the recor is not delete")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod58()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapAdditionalToolbarElementsbutton()
                .TapDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapCancelButton();

            int totalCaseForms = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId).Count;
            Assert.AreEqual(1, totalCaseForms);
        }

        #endregion

        #region Edit Assessment

        #region Other

        [TestProperty("JiraIssueID", "CDV6-10167")]
        [Description("Automated UI Test 0059 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Validate the top menu, left menu and all question labels ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod59()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString());


        }

        [TestProperty("JiraIssueID", "CDV6-10168")]
        [Description("Automated UI Test 0060 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Collapse/Expand left menu button - Validate that the left menu is not visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod60()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapCollapseExpandLeftMenuButton()
                .WaitForLeftMenuHidden();
        }

        [TestProperty("JiraIssueID", "CDV6-10169")]
        [Description("Automated UI Test 0061 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Collapse/Expand left menu button (to collapse) - Tap on the Collapse/Expand button a second time (to expand) - " +
            "Validate that the left menu is visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod61()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapCollapseExpandLeftMenuButton()
                .WaitForLeftMenuHidden()
                .TapCollapseExpandLeftMenuButton()
                .WaitForLeftMenuVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-10170")]
        [Description("Automated UI Test 0062 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Additional Toolbar Items button on the top menu - Validate the Additional buttons are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod62()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapAdditionaToolbarItemsButton()
                .WaitForAdditionalToolbarItemsDisplayed();
        }

        [TestProperty("JiraIssueID", "CDV6-10171")]
        [Description("Automated UI Test 0063 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 1.1 on the left menu - Validate that the browser scrolls to the Section 1.1")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod63()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection1_1()
                .WaitForSection1_1Visible();
        }

        [TestProperty("JiraIssueID", "CDV6-10172")]
        [Description("Automated UI Test 0064 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 1.2 on the left menu - Validate that the browser scrolls to the Section 1.2")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod64()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection1_2()
                .WaitForSection1_2Visible();
        }

        [TestProperty("JiraIssueID", "CDV6-10173")]
        [Description("Automated UI Test 0065 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 2 on the left menu - Validate that the browser scrolls to the Section 2")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod65()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection2()
                .WaitForSection2Visible();
        }

        [TestProperty("JiraIssueID", "CDV6-10174")]
        [Description("Automated UI Test 0066 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 2.1 on the left menu - Validate that the browser scrolls to the Section 2.1")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod66()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection2_1()
                .WaitForSection2_1Visible();
        }

        [TestProperty("JiraIssueID", "CDV6-10175")]
        [Description("Automated UI Test 0067 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 1 Menu button - Validate that the Section 1 menu items are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod67()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .WaitForSection1MenuItemsVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-10176")]
        [Description("Automated UI Test 0068 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section 1 Collapse button - Validate that Section 1 is collapsed and that the questions are not visible - " +
            "Tap on the Section 1 Collapse button - Validate that Section 1 is expanded and that the questions are visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod68()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString())
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton()
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1TitleArea()
                .WaitForSection1Hidden()
                .TapSection1TitleArea()
                .WaitForSection1Visible();
        }

        [TestProperty("JiraIssueID", "CDV6-10177")]
        [Description("Automated UI Test 0068.1 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Scroll to Section 1.1 - Tap on the Section 1.1 Collapse button - " +
            "Validate that Section 1.1 is collapsed and that the questions are not visible - ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod68_1()
        {

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection1_1()
                .TapSection1_1TitleArea()
                .WaitForSection1_1Hidden();
        }

        [TestProperty("JiraIssueID", "CDV6-10178")]
        [Description("Automated UI Test 0068.2 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Scroll to Section 1.1 - Tap on the Section 1.1 Collapse button - " +
            "Validate that Section 1.1 is collapsed and that the questions are not visible - " +
            "Tap on the Section 1.1 Collapse button again - Validate that Section 1.1 is Visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod68_2()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapLeftMenuSection1_1()
                .TapSection1_1TitleArea()
                .WaitForSection1_1Hidden()
                .TapSection1_1TitleArea()
                .WaitForSection1_1Visible();
        }

        #endregion

        #region Save Questions

        [TestProperty("JiraIssueID", "CDV6-10179")]
        [Description("Automated UI Test 0069 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Select an image file in the Image file control - Tap on the upload button - Validate that the image was uploaded")]
        [DeploymentItem("Files\\FileToUpload.jpg"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod69()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapImageQuestionEditButton()
                .ImageQuestionUploadFile(TestContext.DeploymentDirectory + "\\FileToUpload.jpg");

            automatedUITestDocument1EditAssessmentPage
                .WaitForImageQuestionImage();

        }

        [TestProperty("JiraIssueID", "CDV6-10180")]
        [Description("Automated UI Test 0070 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set a value in the CMS Field (Write Back) - Save the assessment - Validate the Case Form Review date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod70()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertIGACMSFieldWriteBackTest1Value("15/07/2019")
                .TapSaveButton();

            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            string reviewdate = ((DateTime)fields["reviewdate"]).ToString("dd'/'MM'/'yyyy");
            Assert.AreEqual("15/07/2019", reviewdate);
        }

        [TestProperty("JiraIssueID", "CDV6-10181")]
        [Description("Automated UI Test 0071 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set a value in the CMS Field (Write Back) - Tap on the Save & Close button - Re-open the assessment - " +
            "Validate that the answer is set in date field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod71()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertIGACMSFieldWriteBackTest1Value("15/07/2019")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateIGACMSFieldWriteBackTest1Answer("15/07/2019");

            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            string reviewdate = ((DateTime)fields["reviewdate"]).ToString("dd'/'MM'/'yyyy");
            Assert.AreEqual("15/07/2019", reviewdate);
        }

        [TestProperty("JiraIssueID", "CDV6-10182")]
        [Description("Automated UI Test 0072 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set a value in the multiple choice; decimal; multiple response; numeric, date - " +
            "Tap on the Save & Close button - Re-open the assessment - Validate that the answers were saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod72()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFMultipleChoice(2)
                .InsertWFDecimalValue("10.3")
                .SelectWFMultipleResponse(1)
                .SelectWFMultipleResponse(3)
                .InsertWFNumericValue("8")
                .InsertWFDateValue("17/07/2019")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionSelected(2)
                .ValidateWFDecimalAnswer("10.30")
                .ValidateWFMultipleResponseOptionChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3)
                .ValidateWFNumericAnswer("8")
                .ValidateWFDateAnswer("17/07/2019");

        }

        [TestProperty("JiraIssueID", "CDV6-10183")]
        [Description("Automated UI Test 0073 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (person associated with the case must have 1 relationship) - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Validate the person relationship is displayed in the 'IGA CMS List Editable Test 1' Field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod73()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10183", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];
            string insideHousehold = "No";
            string relationshipWithCurrentPerson = "Friend";

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateIGACMSListEditableTest1Row(personRelationshipID.ToString(), relatedPerson_firstName + " " + _relatedPerson_lastName, insideHousehold, relationshipWithCurrentPerson, startDate.ToString("dd'/'MM'/'yyyy"));
        }

        [TestProperty("JiraIssueID", "CDV6-10184")]
        [Description("Automated UI Test 0074 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (person associated with the case must have 1 relationship) - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Validate the person relationship is displayed in the 'Gui-ClientRelationshipList' Field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod74()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10184", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            int rowNumber = 1;
            string insideHousehold = "No";
            string relationshipWithCurrentPerson = "Friend";

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateGuiClientRelationshipListRow(rowNumber, relatedPerson_firstName + " " + _relatedPerson_lastName, insideHousehold, relationshipWithCurrentPerson, startDate.ToString("dd'/'MM'/'yyyy"));
        }

        [TestProperty("JiraIssueID", "CDV6-10185")]
        [Description("Automated UI Test 0075 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record (person associated with the case must have 1 relationship) - " +
            "Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Validate the person relationship is displayed in the 'IGA CMS List with Related Data Test 1' Field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod75()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10185", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];
            string insideHousehold = "No";
            string relationshipWithCurrentPerson = "Friend";


            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateIGACMSListWithRelatedDataTest1Headers(personRelationshipID.ToString())
                .ValidateIGACMSListWithRelatedDataTest1(personRelationshipID.ToString(), relatedPerson_firstName + " " + _relatedPerson_lastName, insideHousehold, relationshipWithCurrentPerson, startDate.ToString("dd'/'MM'/'yyyy"));
        }

        [TestProperty("JiraIssueID", "CDV6-10016")]
        [Description("Automated UI Test 0077 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Select a value for the WFLookup Question - Tap on the Save & Close Button - Re-open the assessment in edit mode - " +
            "Validate that the WFLookup answer is set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod77()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFLookupLookupButton();

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;

            var searchPersonID = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            string personName = relatedPerson_firstName + " " + _relatedPerson_lastName;

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(personName)
                .TapSearchButton()
                .SelectResultElement(searchPersonID.ToString());

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFLookupLookupValue(personName);

        }

        [TestProperty("JiraIssueID", "CDV6-10017")]
        [Description("Automated UI Test 0078 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Select a value for the WF Paragraph Question - Tap on the Save & Close Button - Re-open the assessment in edit mode - " +
            "Validate that the Paragraph answer is set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod78()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("Value 1\nValue 2\nValue 3")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("Value 1\nValue 2\nValue 3");

        }

        [TestProperty("JiraIssueID", "CDV6-10018")]
        [Description("Automated UI Test 0079 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Select a value for the WF Picklist Question - Tap on the Save & Close Button - Re-open the assessment in edit mode - " +
            "Validate that the Picklist answer is set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod79()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFPicklistByText("Budist")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFPicklistSelectedValue("Budist");

        }

        [TestProperty("JiraIssueID", "CDV6-10019")]
        [Description("Automated UI Test 0080 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Select a value for the WF Short Answer; WF Boolean, WF Time - Tap on the Save & Close Button - Re-open the assessment in edit mode - " +
            "Validate that the answers set previously are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod80()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("Value 1 Value 2 Value 3 ...")
                .SelectWFBoolean(true)
                .InsertWFTime("13:01")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFShortAnswer("Value 1 Value 2 Value 3 ...")
                .ValidateWFBoolean(true)
                .ValidateWFTimeQuestion("13:01");

        }

        [TestProperty("JiraIssueID", "CDV6-10020")]
        [Description("Automated UI Test 0081 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List Editable Test 1' first row - Validate that the user is redirected to the Person Relationship page ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod81()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10022", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListEditableTestRow(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm();

        }

        [TestProperty("JiraIssueID", "CDV6-10021")]
        [Description("Automated UI Test 0082 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List Editable Test 1' first row - Validate that the user is redirected to the Person Relationship page -" +
            "Tap on the back button - Validate that the user is redirected back to the edit assessment page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod82()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10022", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];
            string insideHousehold = "No";
            string relationshipWithCurrentPerson = "Friend";

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListEditableTestRow(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm()
                .TapBackButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateIGACMSListWithRelatedDataTest1(personRelationshipID.ToString(), relatedPerson_firstName + " " + _relatedPerson_lastName, insideHousehold, relationshipWithCurrentPerson, startDate.ToString("dd'/'MM'/'yyyy"));

        }

        [TestProperty("JiraIssueID", "CDV6-10022")]
        [Description("Automated UI Test 0083 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List Editable Test 1' first row - Validate the relationship information on the Person Relationship page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod83()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship10022", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListEditableTestRow(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm()
                .ValidateRelatedPersonText(relatedPerson_firstName + " " + _relatedPerson_lastName)
                .ValidateInsideHouseholdSelectedValue("No")
                .ValidatePersonRelationshipType("Friend")
                .ValidateStartDate(startDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate("");

        }

        [TestProperty("JiraIssueID", "CDV6-10023")]
        [Description("Automated UI Test 0084 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List Editable Test 1' seconds row - Validate the relationship information on the Person Relationship page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod84()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, startDate);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                relatedPerson_firstName + " " + _relatedPerson_lastName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship", 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListEditableTestRow(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm()
                .ValidateRelatedPersonText(relatedPerson_firstName + " " + _relatedPerson_lastName)
                .ValidateInsideHouseholdSelectedValue("Yes")
                .ValidatePersonRelationshipType("Friend")
                .ValidateStartDate(startDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate("");
        }

        [TestProperty("JiraIssueID", "CDV6-10024")]
        [Description("Automated UI Test 0085 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List Editable Test 1' Add New Button - User is redirected to the Person Relationship page - " +
            "Create a new relationship for the person - Save and Close the relationship Window - " +
            "Validate that the 'IGA CMS List Editable Test 1' CMS field is automatically updated with the new record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod85()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            var relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();


            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListEditableTestNewButton();

            personRelationshipPage
                .WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapPrimaryPersonFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(firstName + " " + lastName)
                .TapSearchButton()
                .SelectResultElement(_personId.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
                .TapRelationshipTypeFieldLookupButton();

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(relationshipTypeName)
                .TapSearchButton()
                .SelectResultElement(relationshipTypeId);

            personRelationshipPage
                .WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
                .TapRelatedPersonFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(relatedPerson_firstName + " " + _relatedPerson_lastName)
                .TapSearchButton()
                .SelectResultElement(_relatedPersonId.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
                .TapRelatedPersonRelationshipTypeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(relationshipTypeName)
                .TapSearchButton()
                .SelectResultElement(relationshipTypeId);

            personRelationshipPage
               .WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
               .SelectInsideHouseholdValue("Yes")
               .TapSaveAndCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString());

            Guid personrelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .ValidateIGACMSListEditableTest1Row(personrelationshipID.ToString(), relatedPerson_firstName + " " + _relatedPerson_lastName, "Yes", "Friend", DateTime.Now.ToString("dd'/'MM'/'yyyy"));
        }

        [TestProperty("JiraIssueID", "CDV6-10025")]
        [Description("Automated UI Test 0086 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List with Related Data Test 1' Edit Button - " +
            "Validate that the user is redirected to the Person Relationship page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod86()
        {
            var startDate = new DateTime(2019, 02, 27);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            var _relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPerson_fullName = _relatedPerson_firstName + " " + _relatedPerson_lastName;

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(_relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";

            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship", 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListWithRelatedDataEditButton(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm()
                .ValidateRelatedPersonText(_relatedPerson_firstName + " " + _relatedPerson_lastName)
                .ValidateInsideHouseholdSelectedValue("No")
                .ValidatePersonRelationshipType("Friend")
                .ValidateStartDate("27/02/2019")
                .ValidateEndDate("");

        }

        [TestProperty("JiraIssueID", "CDV6-10026")]
        [Description("Automated UI Test 0087 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'IGA CMS List with Related Data Test 1' Edit Button - " +
            "Validate that the user is redirected to the Person Relationship page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod87()
        {
            var startDate = new DateTime(2019, 08, 01);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            var _relatedPerson_firstName = "Related_Person";
            var _relatedPerson_lastName = _currentDateSuffix;
            var _relatedPerson_fullName = _relatedPerson_firstName + " " + _relatedPerson_lastName;

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord(_relatedPerson_firstName, _relatedPerson_lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            string relationshipTypeName = "Friend";

            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, firstName + " " + lastName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                startDate, "Relationship", 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            Guid personRelationshipID = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId)[0];

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapIGACMSListWithRelatedDataEditButton(personRelationshipID.ToString());

            personRelationshipPage
                .WaitForPageToLoadAfterNavigatingFromCaseForm()
                .ValidateRelatedPersonText(_relatedPerson_fullName)
                .ValidateInsideHouseholdSelectedValue("Yes")
                .ValidatePersonRelationshipType("Friend")
                .ValidateStartDate("01/08/2019")
                .ValidateEndDate("");
        }

        [TestProperty("JiraIssueID", "CDV6-10027")]
        [Description("Automated UI Test 0088 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set answer data in the 'Test HQ' question - save and re-open the assessment - Validate that the answers are set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod88()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow1Column1Value("Value 1")
                .InsertTestHQRow1Column2Value("11.5")
                .InsertTestHQRow2Column1Value("Value 2")
                .InsertTestHQRow2Column2Value("9.4")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateTestHQRow1Column1Question("Value 1")
                .ValidateTestHQRow1Column2Question("11.50")
                .ValidateTestHQRow2Column1Question("Value 2")
                .ValidateTestHQRow2Column2Question("9.40");
        }


        [TestProperty("JiraIssueID", "CDV6-10028")]
        [Description("Automated UI Test 0089 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set answer data in the 'WF Table With Unlimited Rows' first row - save and re-open the assessment - " +
            "Validate that the answers are set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod89()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertDateBecameInvolvedValue(1, "02/08/2019")
                .SelectReasonForAssessmentValue(1, "Reason 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "02/08/2019")
                .ValidateReasonForAssessmentAnswer(1, "Reason 2");
        }

        [TestProperty("JiraIssueID", "CDV6-10029")]
        [Description("Automated UI Test 0090 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set answer data in the 'WF Table With Unlimited Rows' first row - Tap on the add new Row button - " +
            "Set answer data in the 'WF Table With Unlimited Rows' second row - Save and Re-Open the record" +
            "Validate that the answers are set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod90()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertDateBecameInvolvedValue(1, "02/08/2019")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(2, "03/08/2019")
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "02/08/2019")
                .ValidateReasonForAssessmentAnswer(1, "Reason 1")
                .ValidateDateBecameInvolvedAnswer(2, "03/08/2019")
                .ValidateReasonForAssessmentAnswer(2, "Reason 2");
        }

        [TestProperty("JiraIssueID", "CDV6-10030")]
        [Description("Automated UI Test 0091 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the add new Row button - Only set answer data in the 'WF Table With Unlimited Rows' second row - " +
            "Save and Re-Open the record - Validate that only the 2nd row answers are set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod91()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(2, "03/08/2019")
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "")
                .ValidateReasonForAssessmentAnswer(1, "")
                .ValidateDateBecameInvolvedAnswer(2, "03/08/2019")
                .ValidateReasonForAssessmentAnswer(2, "Reason 2");
        }

        [TestProperty("JiraIssueID", "CDV6-10031")]
        [Description("Automated UI Test 0092 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set answer data in the 'WF Table With Unlimited Rows' First Row - Tap on the 1st row Delete button" +
           "Save and Re-Open the record - Validate that no answer is set for the table question")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod92()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(1, "03/08/2019")
                .SelectReasonForAssessmentValue(1, "Reason 2")
                .TapWFTableWithUnlimitedRowsDeleteRowButton(1);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Do you wish to clear this row? Changes will be applied when the assessment is saved.")
                .TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "")
                .ValidateReasonForAssessmentAnswer(1, "");
        }

        [TestProperty("JiraIssueID", "CDV6-10032")]
        [Description("Automated UI Test 0090 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set answer data in the 'WF Table With Unlimited Rows' first row - Tap on the add new Row button - " +
            "Set answer data in the 'WF Table With Unlimited Rows' second row - Tap on the 1st Row delete button (clear the 1st row)" +
            "Save and Re-Open the Assessment record - Validate the data in the 1st row (must be empty) and 2nd row (must contains data)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod93()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(1, "01/08/2019")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .InsertDateBecameInvolvedValue(2, "02/08/2019")
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapWFTableWithUnlimitedRowsDeleteRowButton(1);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Do you wish to clear this row? Changes will be applied when the assessment is saved.")
                .TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "")
                .ValidateReasonForAssessmentAnswer(1, "")
                .ValidateDateBecameInvolvedAnswer(2, "02/08/2019")
                .ValidateReasonForAssessmentAnswer(2, "Reason 2");
        }

        [TestProperty("JiraIssueID", "CDV6-10033")]
        [Description("Automated UI Test 0094 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set answer data in the 'WF Table With Unlimited Rows' first row - Tap on the add new Row button - " +
            "Set answer data in the 'WF Table With Unlimited Rows' second row - Tap on the 2nd Row Delete button (remove the 2nd row)" +
            "Save and Re-Open the Assessment record - Validate the answer in the 1st row of the table - Validate that no answer is set in the 2nd row")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod94()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(1, "01/08/2019")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .InsertDateBecameInvolvedValue(2, "02/08/2019")
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapWFTableWithUnlimitedRowsDeleteRowButton(2);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Do you wish to delete this row? This action cannot be undone.")
                .TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateDateBecameInvolvedAnswer(1, "01/08/2019")
                .ValidateReasonForAssessmentAnswer(1, "Reason 1")
                .ValidateDateBecameInvolvedRowDoNotExist(2)
                .ValidateReasonForAssessmentRowDoNotExist(2);
        }

        [TestProperty("JiraIssueID", "CDV6-10034")]
        [Description("Automated UI Test 0095 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the answers in the 'Table PQ' table question - Save and Re-Open the Assessment record - Validate the answers in the 'Table PQ' table")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod95()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertQuestion1ContributionNotes("Contribution Notes Value 1")
                .InsertQuestion1Role("Role Value 1")
                .InsertQuestion2ContributionNotes("Contribution Notes Value 2")
                .InsertQuestion2Role("Role Value 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateQuestion1ContributionNotes("Contribution Notes Value 1")
                .ValidateQuestion1Role("Role Value 1")
                .ValidateQuestion2ContributionNotes("Contribution Notes Value 2")
                .ValidateQuestion2Role("Role Value 2");
        }

        [TestProperty("JiraIssueID", "CDV6-10035")]
        [Description("Automated UI Test 0096 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the answers in the 'Table QPQ' table question - Save and Re-Open the Assessment record - Validate the answers in the 'Table QPC' table")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod96()
        {

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestQPCOutcomeAnswer("Value 1")
                .InsertTestQPCTypeOfInvolvementAnswer("Value 2")
                .InsertTestQPCWFTimeAnswer("10:35")
                .InsertTestQPCWhoAnswer("Value 4")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateTestQPCOutcomeAnswer("Value 1")
                .ValidateTestQPCTypeOfInvolvementAnswer("Value 2")
                .ValidateTestQPCWFTimeAnswer("10:35")
                .ValidateTestQPCWhoAnswer("Value 4");
        }

        [TestProperty("JiraIssueID", "CDV6-10036")]
        [Description("Automated UI Test 0097 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'WF Hiperlink 1' - Validate that the browser scrolls to the WF Date Question")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod97()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFHiperlink1()
                .WaitForWFDateQuestionVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-10037")]
        [Description("Automated UI Test 0098 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'WF Hiperlink Group' - Tap on the 'WF Hiperlink 2' - Validate that the browser scrolls to the WF PickList Question")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod98()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFHiperlinkGroup()
                .TapWFHiperlink2()
                .WaitForWFPicklistQuestionVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-10038")]
        [Description("Automated UI Test 0099 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the 'WF Hiperlink Group' - Tap on the 'WF Hiperlink 3' - Validate that the browser scrolls to the WF Paragraph Question")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod99()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFHiperlinkGroup()
                .TapWFHiperlink3()
                .ValidateWFParagraphVisible();
        }

        #endregion

        #region Section Menu Operations


        #region Section Information

        [TestProperty("JiraIssueID", "CDV6-10039")]
        [Description("Automated UI Test 0100 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section Information link for section 1 - Validate that Section Information popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod100()
        {

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();


            Guid DocumentSectionId = new Guid("9AA3C9A5-3DA9-E911-A2C6-005056926FE4");
            Guid assessmentSectionID = dbHelper.assessmentSection.GetAssessmentSection(caseFormID, DocumentSectionId)[0];
            var fields = dbHelper.assessmentSection.GetAssessmentSectionByID(assessmentSectionID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .ValidateCreatedByInformation(_defaultUserFullname)
                .ValidateCreatedOnInformation(createdon)
                .ValidateModifiedByInformation(_defaultUserFullname)
                .ValidateModifiedOnInformation(modifiedon)
                .ValidateIdentifierInformation("QA-DS-66")
                .ValidateOwnerInformation("CareDirector QA")
                .ValidateExcludeFromPrintCheckboxNotChecked();

        }

        [TestProperty("JiraIssueID", "CDV6-10040")]
        [Description("Automated UI Test 0101 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section Information link for section 1 - Wait for the section information to load - " +
            "Check the 'Exclude From Print' check box - Tap the save button and wait for the popup to close - re-open the section information popup -" +
            "Validate that 'Exclude From Print' check box is checked")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod101()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .TapExcludeFromPrintCheckbox()
                .TapSaveButton()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .ValidateExcludeFromPrintCheckboxChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-10041")]
        [Description("Automated UI Test 0102 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section Information link for section 1 - Wait for the section information to load - " +
            "Check the 'Exclude From Print' check box - Tap the Close button and wait for the popup to close - re-open the section information popup -" +
            "Validate that 'Exclude From Print' check box is NOT checked")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod102()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .TapExcludeFromPrintCheckbox()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .ValidateExcludeFromPrintCheckboxNotChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-10042")]
        [Description("Automated UI Test 0103 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Section Information link for section 1 - Wait for the section information to load - " +
            "Check the 'Exclude From Print' check box - Save and re-open the popup - Uncheck the 'Exclude From Print' check box - Save and re-open the popup" +
            "Validate that 'Exclude From Print' check box is NOT checked")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod103()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            //Check the "Exclude From Print" checkbox
            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .TapExcludeFromPrintCheckbox()
                .TapSaveButton()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            //Un-Check the "Exclude From Print" checkbox
            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .TapExcludeFromPrintCheckbox()
                .TapSaveButton()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SectionInformationButton();

            sectionInformationDialoguePopup
                .WaitForSectionInformationDialoguePopupToLoad("Section 1")
                .ValidateExcludeFromPrintCheckboxNotChecked();
        }

        #endregion

        #region Print

        [TestProperty("JiraIssueID", "CDV6-10043")]
        [Description("Automated UI Test 0104 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Print link for section 1 - Validate that print popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod104()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1PrintButton();

            printAssessmentPopup
                 .WaitForPopupToLoadedForSection1()
                 .ValidateSelectedSection("   Section 1")
                 .ValidateListOfTemplates("Print Template for Section 1")
                 ;

        }

        #endregion

        #region Print History

        [TestProperty("JiraIssueID", "CDV6-10044")]
        [Description("Automated UI Test 0105 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Print History link for section 1 - Validate that Print History popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod105()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1PrintHistoryButton();

            assessmentPrintHistoryPopup
                 .WaitForAssessmentPrintHistoryPopupToLoad("Print records for Section 1")
                 .CheckNoPrintRecordsForSectionMessageVisibility(true)
                 .ValidateSectionSelectedElement("   Section 1")
                 ;

        }

        #endregion

        #region Spell Check

        [TestProperty("JiraIssueID", "CDV6-10045")]
        [Description("Automated UI Test 0106 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the SpellCheck link for section 1 - Validate that the 'All the fields checked are spelled correctly' message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod106()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1SpellCheckButton()
                .ValidateNotificationAreaVisible("All the fields checked are spelled correctly.");

        }

        [TestProperty("JiraIssueID", "CDV6-10046")]
        [Description("Automated UI Test 0107 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Insert a gramatically correct text in the WF Short Answer question - Tap on the SpellCheck link for Section 2 - " +
            "Validate that the 'All the fields checked are spelled correctly' message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod107()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("This text should contain no errors.")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .ValidateNotificationAreaVisible("All the fields checked are spelled correctly.");

        }

        [TestProperty("JiraIssueID", "CDV6-10047")]
        [Description("Automated UI Test 0108 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set the WF Short Answer question with one incorrect word - Tap on the SpellCheck link for Section 2 - " +
           "Validat that the spelling spell bar is displayed and visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod108()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("This text contains one eeerror.")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("eeerror")
                .SpellingBarWaitForSuggestedWord("error")
                .SpellingBarValidateChangeAllButtonNotVisible()
                .SpellingBarValidateIgnoreAllButtonNotVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-10048")]
        [Description("Automated UI Test 0109 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set the WF Short Answer question with one incorrect word (can have multiple correct options) - Tap on the SpellCheck link for Section 2 - " +
          "validate that the Suggestions textbox offers multiple options for a user to select")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod109()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("blast")
                .SpellingBarWaitForSuggestedWord("slant")
                .SpellingBarWaitForSuggestedWord("blanc")
                .SpellingBarWaitForSuggestedWord("bland")
                .SpellingBarWaitForSuggestedWord("blunt")
                .SpellingBarWaitForSuggestedWord("plant");

        }

        [TestProperty("JiraIssueID", "CDV6-10049")]
        [Description("Automated UI Test 0110 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set the WF Short Answer question with one incorrect word (can have multiple correct options) - Tap on the SpellCheck link for Section 2 - " +
          "Tap on a Suggestion in the Suggestions textbox - Validate that the suggestion word gets selected")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod110()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarClickSuggestedWord("slant")
                .SpellingBarWaitForSuggestedWordSelected("slant");

        }

        [TestProperty("JiraIssueID", "CDV6-10050")]
        [Description("Automated UI Test 0111 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set the WF Short Answer question with one incorrect word (can have multiple correct options) - Tap on the SpellCheck link for Section 2 - " +
          "Select the 1st suggested word - Tap the Change Button - Validate that the word was updated in the WF Short Answer field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod111()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("blast")
                .SpellingBarClickSuggestedWord("blast")
                .SpellingBarClickChangeButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. blast");

        }

        [TestProperty("JiraIssueID", "CDV6-10051")]
        [Description("Automated UI Test 0112 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set the WF Short Answer question with one incorrect word (can have multiple correct options) - Tap on the SpellCheck link for Section 2 - " +
          "Select the 2nd suggested word - Tap the Change Button - Validate that the word was updated in the WF Short Answer field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod112()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("slant")
                .SpellingBarClickSuggestedWord("slant")
                .SpellingBarClickChangeButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. slant");

        }

        [TestProperty("JiraIssueID", "CDV6-10052")]
        [Description("Automated UI Test 0113 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with two incorrect words - Tap on the SpellCheck link for Section 2 - " +
            "Select the 1st suggested word - Tap the Change Button - Select the 1st suggested word - Tap the Change Button - " +
            "Validate that both incorrect words are updated in the WF Short Answer field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod113()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant zant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("blast")
                .SpellingBarClickSuggestedWord("blast")
                .SpellingBarClickChangeButton()
                .SpellingBarWaitForHighlightedIncorrectWord("zant")
                .SpellingBarWaitForSuggestedWord("rant")
                .SpellingBarClickSuggestedWord("rant")
                .SpellingBarClickChangeButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. blast rant");

        }

        [TestProperty("JiraIssueID", "CDV6-10053")]
        [Description("Automated UI Test 0114 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with two incorrect words - Tap on the SpellCheck link for Section 2 - " +
            "Validate that the Change All and Ignore All buttons are not visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod114()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant zant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("blast")
                .SpellingBarValidateChangeAllButtonNotVisible()
                .SpellingBarValidateIgnoreAllButtonNotVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-10054")]
        [Description("Automated UI Test 0115 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with an incorrect word - Set the Test HQ Location field with one incorrect word (same as in WF Short Answer ) - " +
            "Tap on the SpellCheck link for Section 2 - Select one of the Suggestied words - Tap the change all button - " +
            "Validate that the word was updated on both fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod115()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .InsertTestHQRow1Column1Value("This text also contains the same incorrect word: blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("brant")
                .SpellingBarClickSuggestedWord("brant")
                .SpellingBarClickChangeAllButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. brant")
                .ValidateTestHQRow1Column1Question("This text also contains the same incorrect word: brant");
        }

        [TestProperty("JiraIssueID", "CDV6-10055")]
        [Description("Automated UI Test 0116 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with an incorrect word - Set the Test HQ Location field with one incorrect word (same as in WF Short Answer but repeated twice) - " +
            "Tap on the SpellCheck link for Section 2 - Select one of the Suggestied words - Tap the Change All button - " +
            "Validate that the word is updated on both fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod116()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .InsertTestHQRow1Column1Value("This text also contains the same incorrect word: blant and blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("brant")
                .SpellingBarClickSuggestedWord("brant")
                .SpellingBarClickChangeAllButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. brant")
                .ValidateTestHQRow1Column1Question("This text also contains the same incorrect word: brant and brant");
        }

        [TestProperty("JiraIssueID", "CDV6-10056")]
        [Description("Automated UI Test 0117 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with an incorrect word - Set the Test HQ Location field with one incorrect word (same as in WF Short Answer but repeated twice) - " +
            "Tap on the SpellCheck link for Section 2 - Tap the Ignore All button - " +
            "Validate that the word is not updated on any field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod117()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .InsertTestHQRow1Column1Value("This text also contains the same incorrect word: blant and blant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarClickIgnoreAllButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. blant")
                .ValidateTestHQRow1Column1Question("This text also contains the same incorrect word: blant and blant");
        }

        [TestProperty("JiraIssueID", "CDV6-10057")]
        [Description("Automated UI Test 0118 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the WF Short Answer question with an incorrect word - Set the Test HQ Location field with one incorrect word (different word) - " +
            "Tap on the SpellCheck link for Section 2 - Select one of the Suggestied words - Tap the Accept button  - " +
            "Tap on the Ignore button (for the 2nd suggestion) - Validate that only the 1st word is updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod118()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("The last word is incorrect. blant")
                .InsertTestHQRow1Column1Value("This text also contains the same incorrect word: xlant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("blant")
                .SpellingBarWaitForSuggestedWord("brant")
                .SpellingBarClickSuggestedWord("brant")
                .SpellingBarClickChangeButton()
                .SpellingBarWaitForHighlightedIncorrectWord("xlant")
                .SpellingBarClickIgnoreButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateWFShortAnswer("The last word is incorrect. brant")
                .ValidateTestHQRow1Column1Question("This text also contains the same incorrect word: xlant");
        }

        [TestProperty("JiraIssueID", "CDV6-10058")]
        [Description("Automated UI Test 0119 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set the Test HQ Location field with one incorrect word - " +
            "Tap on the SpellCheck link for Section 2 - Tap the Ignore button  -  Validate that the word is not updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod119()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow1Column1Value("This text also contains the same incorrect word: xlant")
                .TapSection2MenuButton()
                .TapSection2SpellCheckButton()
                .WaitForSpellingBarToBeDisplayed()
                .SpellingBarWaitForHighlightedIncorrectWord("xlant")
                .SpellingBarClickIgnoreButton()
                .WaitForSpellingBarToBeClosed()
                .ValidateTestHQRow1Column1Question("This text also contains the same incorrect word: xlant");
        }

        ////Tests Phoenix_CaseForms_UITestMethod120 and Phoenix_CaseForms_UITestMethod121 had to be deactivated because en-CA language is no longer available
        //[Description("Automated UI Test 0120 - Open a Case Record - Navigate to the Cases Section - " +
        //    "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
        //    "Set the WF Short Answer field with English Canadian words (not valid for UK english) - " +
        //    "Tap on the SpellCheck link for Section 2 - Change the language to English (Canada) - Validate that the Spelling Bar is closed - " +
        //    "Validate that a warning message is displayed with the text 'All the fields checked are spelled correctly.' ")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void Phoenix_CaseForms_UITestMethod120()
        //{
        //    Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
        //    string caseNumber = "CAS-3-220793";
        //    Guid caseid = new Guid("393c0209-ea3a-e911-a2c5-005056926fe4");
        //    Guid personID = new Guid("080b3536-5d7d-4b3c-99f3-bacbe561b56c");
        //    Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

        //    //remove all Forms for the Case record
        //    foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
        //    {
        //        foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
        //            dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

        //        dbHelper.caseForm.DeleteCaseForm(assessmentID);
        //    }

        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, DateTime.Now.Date);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(caseNumber, caseid.ToString())
        //        .OpenCaseRecord(caseid.ToString(), caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString());

        //    caseFormPage
        //        .WaitForCaseFormPageToLoad()
        //        .TapEditAssessmentButton();

        //    automatedUITestDocument1EditAssessmentPage
        //        .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
        //        .InsertWFShortAnswer("Text with correct Canadian words - washroom")
        //        .TapSection2MenuButton()
        //        .TapSection2SpellCheckButton()
        //        .WaitForSpellingBarToBeDisplayed()
        //        .SpellingBarSelectLanguageByValue("en-CA")
        //        .ValidateNotificationAreaVisible("All the fields checked are spelled correctly.")
        //        .WaitForSpellingBarToBeClosed();
        //}

        //[Description("Automated UI Test 0121 - Open a Case Record - Navigate to the Cases Section - " +
        //   "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
        //   "Set the WF Short Answer field with one Portuguese sentence (with an incorrect word) - " +
        //   "Tap on the SpellCheck link for Section 2 - Change the language to Portuguese - Validate that the spell checker can identify the incorrect word - " +
        //   "Select a Suggested word - Tap the Change button  - validate that the word is changed in the question field")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void Phoenix_CaseForms_UITestMethod121()
        //{
        //    Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
        //    string caseNumber = "CAS-3-220793";
        //    Guid caseid = new Guid("393c0209-ea3a-e911-a2c5-005056926fe4");
        //    Guid personID = new Guid("080b3536-5d7d-4b3c-99f3-bacbe561b56c");
        //    Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

        //    //remove all Forms for the Case record
        //    foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
        //    {
        //        foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
        //            dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

        //        dbHelper.caseForm.DeleteCaseForm(assessmentID);
        //    }

        //    Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, DateTime.Now.Date);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(caseNumber, caseid.ToString())
        //        .OpenCaseRecord(caseid.ToString(), caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(caseFormID.ToString());

        //    caseFormPage
        //        .WaitForCaseFormPageToLoad()
        //        .TapEditAssessmentButton();

        //    automatedUITestDocument1EditAssessmentPage
        //        .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
        //        .InsertWFShortAnswer("Text with incorrect Canadian word - washroomm")
        //        .TapSection2MenuButton()
        //        .TapSection2SpellCheckButton()
        //        .WaitForSpellingBarToBeDisplayed()
        //        .SpellingBarSelectLanguageByValue("en-CA")
        //        .SpellingBarWaitForHighlightedIncorrectWord("washroomm")
        //        .SpellingBarWaitForSuggestedWord("washroom")
        //        .SpellingBarClickSuggestedWord("washroom")
        //        .SpellingBarClickChangeButton()
        //        .WaitForSpellingBarToBeClosed()
        //        .InsertWFShortAnswer("Text with incorrect Canadian word - washroom");

        //}


        [TestProperty("JiraIssueID", "CDV6-10059")]
        [Description("Automated UI Test 0122 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "On Section 2 set a text in the WF Short Answer (with an incorrect word) - " +
          "Tap on the SpellCheck link for Section 2.1 - Validate that the message 'All the fields checked are spelled correctly.' is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod122()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("This ttexts hhave aa lot of wwrong worddds")
                .TapSection2_1MenuButton()
                .TapSection2_1SpellCheckButton()
                .ValidateNotificationAreaVisible("All the fields checked are spelled correctly.");

        }


        #endregion

        #region CompleteSection

        [TestProperty("JiraIssueID", "CDV6-10060")]
        [Description("Automated UI Test 0123 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Validate that the message 'This section has been marked as completed.' is not displayed for any section or sub-section")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod123()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .WaitForSection1SectionCompletedMessageNotDisplayed()
                .WaitForSection1_1SectionCompletedMessageNotDisplayed();

        }

        [TestProperty("JiraIssueID", "CDV6-10061")]
        [Description("Automated UI Test 0124 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Tap on the Complete Section link for Section 1 - Validate that the message 'This section has been marked as completed.' is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod124()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton()
                .WaitForSection1SectionCompletedMessageDisplayed();

        }

        [TestProperty("JiraIssueID", "CDV6-10062")]
        [Description("Automated UI Test 0125 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Insert a value in the WF Decimal Question (do not save the answer)" +
            "Tap on the Complete Section link for Section 1 - " +
            "Validate that an alert message is displayed with the text 'There are unsaved changes on this form, continuing will disregard the changes. Would you like to continue?' ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod125()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFDecimalValue("9")
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("There are unsaved changes on this form, continuing will disregard the changes. Would you like to continue?");

        }

        [TestProperty("JiraIssueID", "CDV6-10063")]
        [Description("Automated UI Test 0126 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Insert a value in the WF Decimal Question (do not save the answer) - Tap on the Complete Section link for Section 1 - " +
            "Click OK on the alert message - Validate that the message 'This section has been marked as completed.' is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod126()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFDecimalValue("9")
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .WaitForSection1SectionCompletedMessageDisplayed();
        }

        [TestProperty("JiraIssueID", "CDV6-10064")]
        [Description("Automated UI Test 0127 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Insert a value in the WF Decimal Question (do not save the answer) - Tap on the Complete Section link for Section 1 - " +
            "Click cancel on the alert message - Validate that the message 'This section has been marked as completed.' is not displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod127()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFDecimalValue("9")
                .TapSection1MenuButton()
                .TapSection1CompleteSectionButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapCancelButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .WaitForSection1SectionCompletedMessageNotDisplayed();
        }

        [TestProperty("JiraIssueID", "CDV6-10065")]
        [Description("Automated UI Test 0128 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Complete Section link for Section 1.1 - Validate that the message 'This section has been marked as completed.' is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod128()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSection1_1MenuButton()
                .TapSection1_1CompleteSectionButton()
                .WaitForSection1_1SectionCompletedMessageDisplayed();

        }

        #endregion

        #endregion

        #region Questions Menu

        #region Question Information

        [TestProperty("JiraIssueID", "CDV6-10066")]
        [Description("Automated UI Test 0129 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Question information - Validate that the Information Dialogue Popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod129()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionQuestionInformationLink();

            Guid DocumentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-167")[0];
            Guid documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(caseFormID, DocumentQuestionIdentifierId)[0];
            var fields = dbHelper.documentAnswer.GetDocumentAnswerByID(documentAnswerID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionInformationDialoguePopup
                .WaitForQuestionInformationDialoguePopupToLoad("Image; Order:7")
                .ValidateIdentifierInformation("QA-DSQ-146")
                .ValidateCreatedByInformation(_defaultUserFullname)
                .ValidateCreatedOnInformation(createdon)
                .ValidateModifiedByInformation(_defaultUserFullname)
                .ValidateModifiedOnInformation(modifiedon)
                .ValidateOwnerInformation("CareDirector QA")
                .ValidateExcludeFromPrintCheckboxNotChecked();

        }

        [TestProperty("JiraIssueID", "CDV6-10067")]
        [Description("Automated UI Test 0130 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Question information - Check the Exclude From Print checkbox" +
            "Tap on the Save button - Tap on the close button - Validate that the popup is closed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod130()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionQuestionInformationLink();

            questionInformationDialoguePopup
                .WaitForQuestionInformationDialoguePopupToLoad("Image; Order:7")
                .TapExcludeFromPrintCheckbox()
                .TapSaveButton()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString());

        }

        #endregion

        #region Question Comments

        [TestProperty("JiraIssueID", "CDV6-10068")]
        [Description("Automated UI Test 0131 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Validate that the No Comments message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod131()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .ValidateNotificationMessageVisible("No comments are available for this question.");

        }

        [TestProperty("JiraIssueID", "CDV6-10069")]
        [Description("Automated UI Test 0132 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Tap on the Add Comment button - Validate that the 'Please fill out this field.' message is displayed - " +
            "Validate that the No Comments message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod132()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .TapAddCommentButton()
                .ValidateNewCommentMessageVisible("Please fill out this field.")
                .ValidateNotificationMessageVisible("No comments are available for this question.");

        }

        [TestProperty("JiraIssueID", "CDV6-10070")]
        [Description("Automated UI Test 0133 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Tap on the Add Comment button - Validate that the 'Please fill out this field.' message is displayed - " +
            "Insert a Text in the new commment text box - Validate that the 'Please fill out this field.' message is not displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod133()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .TapAddCommentButton()
                .ValidateNewCommentMessageVisible("Please fill out this field.")
                .InsertComment("New Comment 1")
                .ValidateNewCommentMessageNotVisible("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10071")]
        [Description("Automated UI Test 0134 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a Comment Text - Tap on the Add Comment button - Validate that the comment is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod134()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment("New Comment 1")
                .TapAddCommentButton();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);
            Assert.AreEqual(1, assessmentSectionQuestionComments.Count);
            Guid commentid = assessmentSectionQuestionComments[0];
            var fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid, "createdon", "modifiedon");

            var userLocalTime_createdOn = (DateTime)fields["createdon"];
            var userLocalTime_modifiedOn = (DateTime)fields["modifiedon"];

            string createdon = userLocalTime_createdOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = userLocalTime_modifiedOn.ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionCommentsDialoguePopup
                .ValidateCommentPresent(commentid.ToString(), "New Comment 1", createdon, _loginUserFullName, modifiedon, _loginUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-10072")]
        [Description("Automated UI Test 0135 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a Text in the new commment text box - Tap the Cancel button - Tap on the Add Comment Button - " +
            "Validate that the 'Please fill out this field.' message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod135()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment("New Comment 1")
                .TapCancelButton()
                .TapAddCommentButton()
                .ValidateNewCommentMessageVisible("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-10073")]
        [Description("Automated UI Test 0136 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a Comment Text - Tap on the Add Comment button - Close and Reopen the Comments popop - " +
            "Validate that the comment is created and Visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod136()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            string questionComment = "New Comment - Test 136";

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment(questionComment)
                .TapAddCommentButton()
                .TapCloseButton();

            automatedUITestDocument1EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
               .ClickImageQuestionMenuButton()
               .ClickImageQuestionCommentsLink();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);
            Assert.AreEqual(1, assessmentSectionQuestionComments.Count);
            Guid commentid = assessmentSectionQuestionComments[0];
            var fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string userfullName = _loginUserFullName;

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .ValidateCommentPresent(commentid.ToString(), questionComment, createdon, userfullName, modifiedon, userfullName);

        }

        [TestProperty("JiraIssueID", "CDV6-10074")]
        [Description("Automated UI Test 0137 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a Comment Text - Tap on the Add Comment button - Insert a seconds Comment - Tap on the Add Comment button - " +
            "Validate that both comments are created and visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod137()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            string questionComment1 = "New Comment - Test 137 - 1";
            string questionComment2 = "New Comment - Test 137 - 2";

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment(questionComment1)
                .TapAddCommentButton()
                .InsertComment(questionComment2)
                .TapAddCommentButton();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);
            Assert.AreEqual(2, assessmentSectionQuestionComments.Count);

            List<string> allCommentsText = new List<string>();
            foreach (Guid commentid in assessmentSectionQuestionComments)
            {
                var fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid, "comment", "createdon", "modifiedon");
                string questionComment = (string)fields["comment"];
                string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
                string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
                string userfullName = _loginUserFullName;

                questionCommentsDialoguePopup
                        .ValidateCommentPresent(commentid.ToString(), questionComment, createdon, userfullName, modifiedon, userfullName);

                allCommentsText.Add(questionComment);
            }

            Assert.IsTrue(allCommentsText.Contains(questionComment1));
            Assert.IsTrue(allCommentsText.Contains(questionComment2));

        }

        [TestProperty("JiraIssueID", "CDV6-10075")]
        [Description("Automated UI Test 0138 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a Comment Text - Tap on the Add Comment button - Validate that the comment is created - " +
            "Tap on the Edit Button and update the comment text - Tap on the Modify Command button - Validate that the comment was updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod138()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment("New Comment 1")
                .TapAddCommentButton();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);

            Assert.AreEqual(1, assessmentSectionQuestionComments.Count);
            Guid commentid = assessmentSectionQuestionComments[0];

            var fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid, "comment", "createdon", "modifiedon");
            string comment = (string)fields["comment"];
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string userFullName = _loginUserFullName;

            Assert.AreEqual("New Comment 1", comment);

            questionCommentsDialoguePopup
                .ValidateCommentPresent(commentid.ToString(), comment, createdon, userFullName, modifiedon, userFullName)
                .TapCommentEditButton(commentid.ToString())
                .InsertComment("New Comment 1 - Update")
                .TapAddCommentButton();

            fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid, "comment", "createdon", "modifiedon");
            comment = (string)fields["comment"];
            createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            Assert.AreEqual("New Comment 1 - Update", comment);

            questionCommentsDialoguePopup
                .ValidateCommentPresent(commentid.ToString(), comment, createdon, userFullName, modifiedon, userFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-10076")]
        [Description("Automated UI Test 0139 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a two Comments Text - Tap on the Edit Button and update the first comment text - Tap on the Modify Command button - " +
            "Validate that only the first comment is updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod139()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment("New Comment 1")
                .TapAddCommentButton()
                .InsertComment("New Comment 2")
                .TapAddCommentButton();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);

            Assert.AreEqual(2, assessmentSectionQuestionComments.Count);
            Guid commentid1 = assessmentSectionQuestionComments[0];
            Guid commentid2 = assessmentSectionQuestionComments[1];

            var fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid1, "comment");
            string comment1 = (string)fields["comment"];
            fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid2, "comment");
            string comment2 = (string)fields["comment"];

            Assert.AreEqual("New Comment 1", comment1);
            Assert.AreEqual("New Comment 2", comment2);

            questionCommentsDialoguePopup
                .TapCommentEditButton(commentid1.ToString())
                .InsertComment("Comment 1 - Updated")
                .TapAddCommentButton();

            fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid1, "createdon", "modifiedon");
            string createdon1 = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon1 = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            fields = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentByID(commentid2, "createdon", "modifiedon");
            string createdon2 = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon2 = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            string userFullName = _loginUserFullName;

            questionCommentsDialoguePopup
                .ValidateCommentPresent(commentid1.ToString(), "Comment 1 - Updated", createdon1, userFullName, modifiedon1, userFullName)
                .ValidateCommentPresent(commentid2.ToString(), "New Comment 2", createdon2, userFullName, modifiedon2, userFullName);
        }

        [TestProperty("JiraIssueID", "CDV6-10077")]
        [Description("Automated UI Test 0140 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the Image Question Menu Button - Tap on the Comments link - Wait for the Comments popup to load - " +
            "Insert a comment - Tap on the Delete Button  - Confirm the delete - validate that the comment is removed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod140()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickImageQuestionMenuButton()
                .ClickImageQuestionCommentsLink();

            questionCommentsDialoguePopup
                .WaitForQuestionCommentsDialoguePopupToLoad("Image; Order:7")
                .InsertComment("New Comment 1")
                .TapAddCommentButton();

            string imageSectionQuestionIdentifier = "QA-DSQ-146";
            List<Guid> assessmentSectionQuestionComments = dbHelper.assessmentSectionQuestionComment.GetAssessmentSectionQuestionCommentsForAssessmentQuestion(caseFormID, imageSectionQuestionIdentifier);
            Assert.AreEqual(1, assessmentSectionQuestionComments.Count);
            Guid commentID = assessmentSectionQuestionComments[0];


            questionCommentsDialoguePopup
                .TapCommentDeleteButton(commentID.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to delete this comment?")
                .TapOKButton();

            questionCommentsDialoguePopup
                .ValidateCommentNotVisible(commentID.ToString());
        }


        #endregion

        #region Question Audit

        [TestProperty("JiraIssueID", "CDV6-10078")]
        [Description("Automated UI Test 0141 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Audit link - Wait for the Audit popup to load - " +
            "Validate that the audit records are present")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod141()
        {
            string caseNumber = "QA-CAS-000001-00588844";
            Guid caseid = new Guid("b8f62ecc-073b-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("1ecaa594-baba-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .WaitForAuditRecordVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "");
        }

        [TestProperty("JiraIssueID", "CDV6-10079")]
        [Description("Automated UI Test 0142 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Audit link - Wait for the Audit popup to load - " +
            "Filter audit records by date - Validate that only the audits recorded betweent the filter dates are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod142()
        {
            string caseNumber = "QA-CAS-000001-00588844";
            Guid caseid = new Guid("b8f62ecc-073b-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("1ecaa594-baba-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .InsertDateFrom("01/08/2019")
                .InsertDateTo("03/08/2019")
                .TapSearchButton()
                .WaitForAuditRecordNotVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordNotVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "");
        }

        [TestProperty("JiraIssueID", "CDV6-10080")]
        [Description("Automated UI Test 0143 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Audit link - Wait for the Audit popup to load - " +
            "Filter audit records by a date that will not match any record - Validate that the no audit records message is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod143()
        {
            string caseNumber = "QA-CAS-000001-00588844";
            Guid caseid = new Guid("b8f62ecc-073b-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("1ecaa594-baba-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .InsertDateFrom("01/07/2019")
                .InsertDateTo("01/08/2019")
                .TapSearchButton()
                .WaitForNotificationMessageVisible("No audit records are available for this question.")
                .WaitForAuditRecordNotVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordNotVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordNotVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "");
        }

        [TestProperty("JiraIssueID", "CDV6-10081")]
        [Description("Automated UI Test 0144 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Audit link - Wait for the Audit popup to load - " +
            "Filter audit records by date - Validate that only the audits recorded betweent the filter dates are visible - " +
            "Tap on the Clear button - Validate that all records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod144()
        {
            string caseNumber = "QA-CAS-000001-00588844";
            Guid caseid = new Guid("b8f62ecc-073b-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("1ecaa594-baba-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .InsertDateFrom("01/08/2019")
                .InsertDateTo("03/08/2019")
                .TapSearchButton()
                .WaitForAuditRecordNotVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordNotVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "")
                .TapClearButton()
                .WaitForAuditRecordVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "");
        }

        [TestProperty("JiraIssueID", "CDV6-10082")]
        [Description("Automated UI Test 0145 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Audit link - Wait for the Audit popup to load - " +
            "Filter audit records by User - Validate that only the audits recorded for the user are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod145()
        {
            string caseNumber = "QA-CAS-000001-00588844";
            Guid caseid = new Guid("b8f62ecc-073b-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("1ecaa594-baba-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .InsertUser("test user 1")
                .TapSearchButton()
                .WaitForAuditRecordVisible("test user 1", "09/08/2019 16:30:37", "30.00", "20.00")
                .WaitForAuditRecordNotVisible("Test User 3", "05/08/2019 16:30:33", "20.00", "10.00")
                .WaitForAuditRecordNotVisible("Test User 5", "02/08/2019 16:30:29", "10.00", "");
        }

        [TestProperty("JiraIssueID", "CDV6-10083")]
        [Description("Automated UI Test 0146 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set an answer in the WF multiple choice question - Tap the Save button - " +
            "Tap on the WF multiple choice Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
            "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod146()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFMultipleChoice(3)
                .TapSaveButton()
                .ClickWFMultipleChoiceQuestionMenuButton()
                .ClickWFMultipleChoiceQuestionAuditLink();

            var auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-169")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Multiple Choice; Order:8")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Option 3", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10084")]
        [Description("Automated UI Test 0147 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set an answer in the WF decimal question - Tap the Save button - " +
            "Tap on the WF Decimal Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
            "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod147()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date); ;

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFDecimalValue("12.21")
                .TapSaveButton()
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-164")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Decimal; Order:9")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "12.21", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10085")]
        [Description("Automated UI Test 0148 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Set an answer in the WF Multiple Response question - Tap the Save button - " +
            "Tap on the WF Multiple Response Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
            "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod148()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFMultipleResponse(1)
                .SelectWFMultipleResponse(3)
                .TapSaveButton()
                .ClickWFMultipleResponseQuestionMenuButton()
                .ClickWFMultipleResponseQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-170")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Multiple Response; Order:10")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Day 1, Day 3", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10086")]
        [Description("Automated UI Test 0149 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Numeric question - Tap the Save button - " +
           "Tap on the WF Numeric Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod149()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFNumericValue("21")
                .TapSaveButton()
                .ClickWFNumericQuestionMenuButton()
                .ClickWFNumericQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-171")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Numeric; Order:11")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "21", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10087")]
        [Description("Automated UI Test 0150 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Lookup Question - Tap the Save button - " +
           "Tap on the WF Lookup Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod150()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            #region Second Person Record

            Guid searchPersonID = commonMethodsDB.CreatePersonRecord("FN", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFLookupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery("LN_" + _currentDateSuffix)
                .TapSearchButton()
                .SelectResultElement(searchPersonID);

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapSaveButton()
                .ClickWFLookupQuestionMenuButton()
                .ClickWFLookupQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-168")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Lookup; Order:13")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "FN LN_" + _currentDateSuffix, "");


        }

        [TestProperty("JiraIssueID", "CDV6-10088")]
        [Description("Automated UI Test 0151 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Date Question - Tap the Save button - " +
           "Tap on the WF Date Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod151()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFDateValue("01/08/2019")
                .TapSaveButton()
                .ClickWFDateQuestionMenuButton()
                .ClickWFDateQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-163")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Date; Order:14")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "01/08/2019", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10089")]
        [Description("Automated UI Test 0152 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Paragraph Question - Tap the Save button - " +
           "Tap on the WF Paragraph Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod152()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveButton()
                .ClickWFParagraphQuestionMenuButton()
                .ClickWFParagraphQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-172")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Paragraph; Order:1")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 1\nValue 2", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10090")]
        [Description("Automated UI Test 0153 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Picklist Question - Tap the Save button - " +
           "Tap on the WF Picklist Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod153()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFPicklistByText("Budist")
                .TapSaveButton()
                .ClickWFPicklistQuestionMenuButton()
                .ClickWFPicklistQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-173")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF PickList; Order:1")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Budist", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10091")]
        [Description("Automated UI Test 0154 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Short Answer Question - Tap the Save button - " +
           "Tap on the WF Short Answer Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod154()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("Value 1 Value 2")
                .TapSaveButton()
                .ClickWFShortAnswerQuestionMenuButton()
                .ClickWFShortAnswerQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-174")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Short Answer; Order:1")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 1 Value 2", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10092")]
        [Description("Automated UI Test 0155 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test HQ Question (Row 1, Column  1) - Tap the Save button - " +
           "Tap on the WF Test HQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod155()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow1Column1Value("1")
                .TapSaveButton()
                .ClickTestHQQuestionMenuButton()
                .ClickTestHQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-177")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test HQ; Order:2")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "1", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10093")]
        [Description("Automated UI Test 0156 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test HQ Question (Row 1, Column  2) - Tap the Save button - " +
           "Tap on the WF Test HQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod156()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow1Column2Value("2.00")
                .TapSaveButton()
                .ClickTestHQQuestionMenuButton()
                .ClickTestHQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-178")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test HQ; Order:2")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "2.00", "");


        }

        [TestProperty("JiraIssueID", "CDV6-10094")]
        [Description("Automated UI Test 0157 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test HQ Question (Row 2, Column  1) - Tap the Save button - " +
           "Tap on the WF Test HQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod157()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow2Column1Value("3")
                .TapSaveButton()
                .ClickTestHQQuestionMenuButton()
                .ClickTestHQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-179")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test HQ; Order:2")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "3", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10095")]
        [Description("Automated UI Test 0158 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test HQ Question (Row 2, Column  2) - Tap the Save button - " +
           "Tap on the WF Test HQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod158()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestHQRow2Column2Value("4.00")
                .TapSaveButton()
                .ClickTestHQQuestionMenuButton()
                .ClickTestHQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-180")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test HQ; Order:2")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "4.00", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10096")]
        [Description("Automated UI Test 0159 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table with unlimited Rows Question (Row 1, Column 1) - Tap the Save button - " +
           "Tap on the WF Table with unlimited Rows Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod159()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertDateBecameInvolvedValue(1, "01/08/2019")
                .TapSaveButton()
                .ClickWFTableWithUnlimitedRowsQuestionMenuButton()
                .ClickWFTableWithUnlimitedRowsQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-245")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Table With Unlimited Rows; Order:3")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "01/08/2019", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10097")]
        [Description("Automated UI Test 0160 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table with unlimited Rows Question (Row 1, Column 2) - Tap the Save button - " +
           "Tap on the WF Table with unlimited Rows Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod160()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .TapSaveButton()
                .ClickWFTableWithUnlimitedRowsQuestionMenuButton()
                .ClickWFTableWithUnlimitedRowsQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-246")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Table With Unlimited Rows; Order:3")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Reason 1", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10098")]
        [Description("Automated UI Test 0161 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table with unlimited Rows Question (Row 2, Column 1) - Tap the Save button - " +
           "Tap on the WF Table with unlimited Rows Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod161()
        {
            //Assert.Inconclusive("Currently only the 1st row is auditable");
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(2, "02/08/2019")
                .TapSaveButton()
                .ClickWFTableWithUnlimitedRowsQuestionMenuButton()
                .ClickWFTableWithUnlimitedRowsQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-245")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Table With Unlimited Rows; Order:3")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "02/08/2019", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10099")]
        [Description("Automated UI Test 0162 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table with unlimited Rows Question (Row 2, Column 2) - Tap the Save button - " +
           "Tap on the WF Table with unlimited Rows Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod162()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapWFTableWithUnlimitedRowsNewButton()
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapSaveButton()
                .ClickWFTableWithUnlimitedRowsQuestionMenuButton()
                .ClickWFTableWithUnlimitedRowsQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-246")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            //string createdon = TimeZoneInfo.ConvertTime((DateTime)fields["createdon"], TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")).ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Table With Unlimited Rows; Order:3")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Reason 2", "");

        }

        [TestProperty("JiraIssueID", "CDV6-10100")]
        [Description("Automated UI Test 0163 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table PQ Question(Question 1 - Contribution Notes) - Tap the Save button - " +
           "Tap on the WF Table PQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod163()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertQuestion1ContributionNotes("Value 1\nValue 2")
                .TapSaveButton()
                .ClickTablePQQuestionMenuButton()
                .ClickTablePQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-253")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Table PQ; Order:4")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 1\nValue 2", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9979")]
        [Description("Automated UI Test 0164 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table PQ Question(Question 1 - Role) - Tap the Save button - " +
           "Tap on the WF Table PQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod164()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertQuestion1Role("Value 2 Value 3")
                .TapSaveButton()
                .ClickTablePQQuestionMenuButton()
                .ClickTablePQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-255")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Table PQ; Order:4")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 2 Value 3", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9980")]
        [Description("Automated UI Test 0165 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table PQ Question(Question 2 - Contribution Notes) - Tap the Save button - " +
           "Tap on the WF Table PQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod165()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertQuestion2ContributionNotes("Value 5\nValue 6")
                .TapSaveButton()
                .ClickTablePQQuestionMenuButton()
                .ClickTablePQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-254")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Table PQ; Order:4")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 5\nValue 6", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9981")]
        [Description("Automated UI Test 0166 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Table PQ Question(Question 2 - Role) - Tap the Save button - " +
           "Tap on the WF Table PQ Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod166()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertQuestion2Role("Value 7 Value 8")
                .TapSaveButton()
                .ClickTablePQQuestionMenuButton()
                .ClickTablePQQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-256")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Table PQ; Order:4")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 7 Value 8", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9982")]
        [Description("Automated UI Test 0167 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test QPC Question (Outcome) - Tap the Save button - " +
           "Tap on the WF Test QPC Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod167()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestQPCOutcomeAnswer("Value 1 Value 2")
                .TapSaveButton()
                .ClickTestQPCQuestionMenuButton()
                .ClickTestQPCQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-185")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test QPC; Order:5")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 1 Value 2", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9983")]
        [Description("Automated UI Test 0168 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Set an answer in the WF Test QPC Question (Type of Involvement) - Tap the Save button - " +
           "Tap on the WF Test QPC Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
           "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod168()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestQPCTypeOfInvolvementAnswer("Value 3 Value 4")
                .TapSaveButton()
                .ClickTestQPCQuestionMenuButton()
                .ClickTestQPCQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-258")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test QPC; Order:5")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 3 Value 4", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9984")]
        [Description("Automated UI Test 0169 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set an answer in the WF Test QPC Question (WF Time) - Tap the Save button - " +
          "Tap on the WF Test QPC Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
          "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod169()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestQPCWFTimeAnswer("08:00")
                .TapSaveButton()
                .ClickTestQPCQuestionMenuButton()
                .ClickTestQPCQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-260")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test QPC; Order:5")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "08:00", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9985")]
        [Description("Automated UI Test 0170 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set an answer in the WF Test QPC Question (Who) - Tap the Save button - " +
          "Tap on the WF Test QPC Question Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
          "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod170()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertTestQPCWhoAnswer("Value 5 Value 6")
                .TapSaveButton()
                .ClickTestQPCQuestionMenuButton()
                .ClickTestQPCQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-262")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: Test QPC; Order:5")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Value 5 Value 6", "");

        }

        [TestProperty("JiraIssueID", "CDV6-9986")]
        [Description("Automated UI Test 0171 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set an answer in the WF Boolean - Tap the Save button - " +
          "Tap on the WF Boolean Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
          "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod171()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .SelectWFBoolean(true)
                .TapSaveButton()
                .ClickWFBooleanQuestionMenuButton()
                .ClickWFBooleanQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-186")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Boolean; Order:6")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "Yes", "No");

        }

        [TestProperty("JiraIssueID", "CDV6-9987")]
        [Description("Automated UI Test 0172 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Set an answer in the WF Boolean - Tap the Save button - " +
          "Tap on the WF Boolean Menu Button - Tap on the Audit link - Wait for the Audit popup to load -" +
          "Validate tha the answer set previously is audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_UITestMethod172()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFTime("15:21")
                .TapSaveButton()
                .ClickWFTimeQuestionMenuButton()
                .ClickWFTimeQuestionAuditLink();

            Guid auditID = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditForAssessmentQuestion(caseFormID, "QA-DQ-188")[0];
            var fields = dbHelper.documentAnswerAudit.GetDocumentAnswerAuditByID(auditID, "createdon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            questionAuditDialoguePopup
                .WaitForQuestionAuditDialoguePopupToLoad("Audit: WF Time; Order:8")
                .WaitForAuditRecordVisible(_loginUserFullName, createdon, "15:21", "");

        }

        #endregion

        #region Previous Answers

        [TestProperty("JiraIssueID", "CDV6-9988")]
        [Description("Automated UI Test 0173 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Multiple Choice Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Multiple Choice Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod173()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFMultipleChoiceQuestionMenuButton()
                .ClickWFMultipleChoiceQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Multiple Choice; Order:8'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFMultipleChoiceCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFMultipleChoiceCompareFirstOption1Checked(false)
                .ValidateWFMultipleChoiceCompareFirstOption2Checked(false)
                .ValidateWFMultipleChoiceCompareFirstOption3Checked(true)
                .ValidateWFMultipleChoiceCompareSecondOption1Checked(false)
                .ValidateWFMultipleChoiceCompareSecondOption2Checked(true)
                .ValidateWFMultipleChoiceCompareSecondOption3Checked(false);

        }

        [TestProperty("JiraIssueID", "CDV6-9989")]
        [Description("Automated UI Test 0174 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Decimal Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Decimal Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod174()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDecimalQuestionMenuButton()
                .ClickWFDecimalQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Decimal; Order:9'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFDecimalCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFDecimalCompareFirstInputValue("15.00")
                .ValidateWFDecimalCompareSecondInputValue("2.00");

        }

        [TestProperty("JiraIssueID", "CDV6-9990")]
        [Description("Automated UI Test 0175 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Multiple Response Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Multiple Response Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod175()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFMultipleResponseQuestionMenuButton()
                .ClickWFMultipleResponseQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Multiple Response; Order:10'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFMultipleResponseCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFMultipleResponseCompareFirstDay1Checked(false)
                .ValidateWFMultipleResponseCompareFirstDay2Checked(true)
                .ValidateWFMultipleResponseCompareFirstDay3Checked(true)
                .ValidateWFMultipleResponseCompareSecondDay1Checked(true)
                .ValidateWFMultipleResponseCompareSecondDay2Checked(true)
                .ValidateWFMultipleResponseCompareSecondDay3Checked(false);

        }

        [TestProperty("JiraIssueID", "CDV6-9991")]
        [Description("Automated UI Test 0176 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Numeric Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Numeric Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod176()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFNumericQuestionMenuButton()
                .ClickWFNumericQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Numeric; Order:11'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFNumericCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFNumericCompareFirstInputValue("15")
                .ValidateWFNumericCompareSecondInputValue("2");

        }

        [TestProperty("JiraIssueID", "CDV6-9992")]
        [Description("Automated UI Test 0177 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Lookup Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Lookup Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod177()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFLookupQuestionMenuButton()
                .ClickWFLookupQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Lookup; Order:13'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFLookupCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFLookupCompareFirstInputValue("Luella Abbott")
                .ValidateWFLookupCompareSecondInputValue("Inez Abbott");

        }

        [TestProperty("JiraIssueID", "CDV6-9993")]
        [Description("Automated UI Test 0178 - Open a Case Record - Navigate to the Cases Section - " +
            "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
            "Tap on the WF Date Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
            "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
            "Validate that the Previous Answers popup displays the current WF Date Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod178()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFDateQuestionMenuButton()
                .ClickWFDateQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Date; Order:14'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFDateCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFDateCompareFirstInputValue("09/08/2019")
                .ValidateWFDateCompareSecondInputValue("01/08/2019");

        }

        [TestProperty("JiraIssueID", "CDV6-9994")]
        [Description("Automated UI Test 0179 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the WF Paragraph Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current WF Paragraph Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod179()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFParagraphQuestionMenuButton()
                .ClickWFParagraphQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Paragraph; Order:1'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFParagraphCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFParagraphCompareFirstInputValue("V 1\r\nV 2")
                .ValidateWFParagraphCompareSecondInputValue("Value 1\r\nValue 2");

        }

        [TestProperty("JiraIssueID", "CDV6-9995")]
        [Description("Automated UI Test 0180 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the WF PickList Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current WF PickList Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod180()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFPicklistQuestionMenuButton()
                .ClickWFPicklistQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF PickList; Order:1'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFPicklistCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFPicklistCompareFirstInputValue("Christian")
                .ValidateWFPicklistCompareSecondInputValue("Atheist");

        }

        [TestProperty("JiraIssueID", "CDV6-9996")]
        [Description("Automated UI Test 0181 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the WF Short Answer Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current WF Short Answer Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod181()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFShortAnswerQuestionMenuButton()
                .ClickWFShortAnswerQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Short Answer; Order:1'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFShortAnswerCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFShortAnswerCompareFirstInputValue("SHA")
                .ValidateWFShortAnswerCompareSecondInputValue("Value 1 Value 2");

        }

        [TestProperty("JiraIssueID", "CDV6-9997")]
        [Description("Automated UI Test 0182 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the Test HQ Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current Test HQ Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod182()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickTestHQQuestionMenuButton()
                .ClickTestHQQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'Test HQ; Order:2'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForTestHQCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateTestHQCompareFirstInputValue("a", "8.00", "b", "9.00")
                .ValidateTestHQCompareSecondInputValue("1", "2.00", "3", "4.00");

        }

        [TestProperty("JiraIssueID", "CDV6-9998")]
        [Description("Automated UI Test 0183 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the WF Table With Unlimited Rows Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current WF Table With Unlimited Rows Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod183()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFTableWithUnlimitedRowsQuestionMenuButton()
                .ClickWFTableWithUnlimitedRowsQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Table With Unlimited Rows; Order:3'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFTableWithUnlimitedRowsCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFTableWithUnlimitedRowsCompareFirstInputValue("09/08/2019", "Reason 2", "10/08/2019", "Reason 2")
                .ValidateWFTableWithUnlimitedRowsCompareSecondInputValue("01/08/2019", "Reason 1", "02/08/2019", "Reason 1");

        }

        [TestProperty("JiraIssueID", "CDV6-9999")]
        [Description("Automated UI Test 0184 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the Table PQ Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current Table PQ Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod184()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickTablePQQuestionMenuButton()
                .ClickTablePQQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'Table PQ; Order:4'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForTablePQCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateTablePQCompareFirstInputValue("C1\r\nC2", "R1", "C3\r\nC4", "R2")
                .ValidateTablePQCompareSecondInputValue("CN 1\r\nCN 2", "R1 R2", "CN 3\r\nCN 4", "R3 R4");

        }

        [TestProperty("JiraIssueID", "CDV6-10000")]
        [Description("Automated UI Test 0185 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the Test QPC Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current Test QPC Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod185()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickTestQPCQuestionMenuButton()
                .ClickTestQPCQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'Test QPC; Order:5'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForTestQPCCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateTestQPCCompareFirstInputValue("a", "b", "17:00", "v")
                .ValidateTestQPCCompareSecondInputValue("1", "2", "08:00", "3");

        }

        [TestProperty("JiraIssueID", "CDV6-10001")]
        [Description("Automated UI Test 0186 - Open a Case Record - Navigate to the Cases Section - " +
           "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
           "Tap on the WF Boolean Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
           "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
           "Validate that the Previous Answers popup displays the current WF Boolean Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod186()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFBooleanQuestionMenuButton()
                .ClickWFBooleanQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Boolean; Order:6'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFBooleanCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFBooleanCompareFirstYesChecked(true)
                .ValidateWFBooleanCompareFirstNoChecked(false)
                .ValidateWFBooleanCompareSecondYesChecked(false)
                .ValidateWFBooleanCompareSecondNoChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-10002")]
        [Description("Automated UI Test 0187 - Open a Case Record - Navigate to the Cases Section - " +
          "Open Case Form record - Tap on the Edit Assessment Button - Wait for the Edit assessment page to load - " +
          "Tap on the WF Time Question Menu Button (with Audit History) - Tap on the Previous Answers link - " +
          "Wait for the Previous Answers popup to load - Select the assessment prior to the current one for compare" +
          "Validate that the Previous Answers popup displays the current WF Time Question information for the current and the previous assessment")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_UITestMethod187()
        {
            string caseNumber = "QA-CAS-000001-00289555";
            Guid caseid = new Guid("ff69ee76-ed3a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("4aceed97-d1bd-e911-a2c7-005056926fe4");
            Guid caseFormIDBase = new Guid("6bcc1f45-d1bd-e911-a2c7-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickWFTimeQuestionMenuButton()
                .ClickWFTimeQuestionPreviousAnswersLink();

            questionCompareDialoguePopup
                .WaitForQuestionCompareDialoguePopupToLoad("Previous Answers for 'WF Time; Order:8'")
                .SelectCompareWithByValue(caseFormIDBase.ToString())
                .WaitForWFTimeCompareInformationToLoad()
                .ValidateCompareFirstTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 09/08/2019 created by CW Forms Test User 1 (09/08/2019)")
                .ValidateCompareSecondTitle("Automated UI Test Document 1 for Avila Norman - (1988-09-02 00:00:00) [QA-CAS-000001-00289555] Starting 01/08/2019 created by CW Forms Test User 1 (01/08/2019)")
                .ValidateWFTimeCompareFirstInputValue("18:00")
                .ValidateWFTimeCompareSecondInputValue("09:00");

        }

        #endregion

        #endregion

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5001

        [TestProperty("JiraIssueID", "CDV6-10003")]
        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Case Record - Navigate to the Case Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Validate that the popup displays only case form records for the current form")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_PrecedingForms_UITestMethod001()
        {

            var current_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now);
            var past_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));

            #region Second Person Record

            Guid _personId2 = commonMethodsDB.CreatePersonRecord(firstName, "FN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion

            #region Second Case Record

            Guid _caseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId2, _defaultLoginUserID, _defaultLoginUserID, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form

            var caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, past_Date);
            var caseFormID2 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, current_Date);
            var caseFormID3 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId2, _caseId2, current_Date); // Case Form that belongs to Second Case and Person.

            #endregion

            #region Person Form

            var personFormId1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, current_Date);
            var personFormId2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, past_Date);
            var personFormId3 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId2, _documentId, past_Date); // Person Form that belongs to Second Person Record.

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrecedingFormLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()

                .ValidateResultElementPresent(caseFormID1.ToString())
                .ValidateResultElementPresent(caseFormID2.ToString())

                .ValidateResultElementNotPresent(caseFormID3.ToString())
                .ValidateResultElementNotPresent(personFormId1.ToString())
                .ValidateResultElementNotPresent(personFormId2.ToString())
                .ValidateResultElementNotPresent(personFormId3.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-10004")]
        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Case Record - Navigate to the Case Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - Select 'Forms (Person)' in the Lookup for picklist - " +
            "Validate that the popup displays only person form records for the person associated with the Case")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_PrecedingForms_UITestMethod002()
        {
            var current_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now);
            var past_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));

            #region Second Person Record

            Guid _personId2 = commonMethodsDB.CreatePersonRecord(firstName, "FN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion

            #region Second Case Record

            Guid _caseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId2, _defaultLoginUserID, _defaultLoginUserID, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form

            var caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, past_Date);
            var caseFormID2 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, current_Date);
            var caseFormID3 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId2, _caseId2, current_Date); // Case Form that belongs to Second Case and Person.

            #endregion

            #region Person Form

            var personFormId1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, current_Date);
            var personFormId2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, past_Date);
            var personFormId3 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId2, _documentId, past_Date); // Person Form that belongs to Second Person Record.

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapPrecedingFormLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectBusinessObjectByText("Forms (Person)")

                .ValidateResultElementPresent(personFormId1.ToString())
                .ValidateResultElementPresent(personFormId2.ToString())

                .ValidateResultElementNotPresent(personFormId3.ToString())
                .ValidateResultElementNotPresent(caseFormID1.ToString())
                .ValidateResultElementNotPresent(caseFormID2.ToString())
                .ValidateResultElementNotPresent(caseFormID3.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-10005")]
        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Case Record - Navigate to the Case Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Select one Case form record - Save the Case Form - Validate that all data is saved correctly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_PrecedingForms_UITestMethod003()
        {
            var current_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now);

            #region Document

            var _documentName2 = "Automated UI Test Document 2";
            var _documentId2 = commonMethodsDB.CreateDocumentIfNeeded(_documentName2, "Automated UI Test Document 2.Zip");

            #endregion

            #region Case Form

            var caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, current_Date);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName2.ToString()).TapSearchButton().SelectResultElement(_documentId2.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapPrecedingFormLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(caseFormID1.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId2);
            Assert.AreEqual(1, caseformIDs.Count);

            string _caseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(caseFormID1, "title")["title"];

            caseCasesFormPage
                .OpenRecord(caseformIDs[0].ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateCaseField(_caseTitle, false, true)
                .ValidateFormTypeField(_documentName2, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidatePrecedingFormFieldLinkText(_caseFormTitle);

        }

        [TestProperty("JiraIssueID", "CDV6-10006")]
        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Case Record - Navigate to the Case Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Select one Case form record - Save the Case Form - Validate that all data is saved correctly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_PrecedingForms_UITestMethod004()
        {
            var current_Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #region Document

            var _documentName2 = "Automated UI Test Document 2";
            var _documentId2 = commonMethodsDB.CreateDocumentIfNeeded(_documentName2, "Automated UI Test Document 2.Zip");

            #endregion

            #region Person Form

            var personFormId1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, current_Date);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName2.ToString()).TapSearchButton().SelectResultElement(_documentId2.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapPrecedingFormLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Forms (Person)").SelectResultElement(personFormId1.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId2);
            Assert.AreEqual(1, caseformIDs.Count);

            string _personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormId1, "title")["title"];

            caseCasesFormPage
                .OpenRecord(caseformIDs[0].ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateCaseField(_caseTitle, false, true)
                .ValidateFormTypeField(_documentName2.ToString(), false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("CaseForms User1", true, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidatePrecedingFormFieldLinkText(_personFormTitle);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8270

        [TestProperty("JiraIssueID", "CDV6-10007")]
        [Description("Open a Case Record - Navigate to the Case Forms Section - " +
            "Open a Case Form record (status set to closed and with no valies in the 'Completion Details' section fields - " +
            "Validate that the Case Form is displayed with empty 'Completion Details' fields ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ReferralFormsWithStatusClosed_UITestMethod001()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);
            dbHelper.caseForm.UpdateStatus(caseFormID, 3);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateCompletedByFieldLinkVisibility(true)
                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateText("")
                .ValidateCompletionDateValue("")
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();



        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8320

        //[TestProperty("JiraIssueID", "CDV6-10008")]
        //[Description("Open a Case Record - Navigate to the Case Forms Section - " +
        //    "Validate that the forms with status = 'Not Started' display with status text of 'Not Initialized' ")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void LocalisedTextChangeBeingIgnored_AssessmentStatus_UITestMethod001()
        //{
        //    #region Localized String

        //    var localizedStringId = dbHelper.localizedString.GetByName("OptionSetValue.Text.8819F5FC-C855-E311-B694-1803731F3EE3")[0];

        //    #endregion

        //    #region Localized String Value

        //    foreach (var localizedStringValueId in dbHelper.localizedStringValue.GetByLocalizedStringId(localizedStringId))
        //        dbHelper.localizedStringValue.DeleteLocalizedStringValue(localizedStringValueId);

        //    dbHelper.localizedStringValue.CreateLocalizedStringValue(localizedStringId, _languageId, "Not Initialized");

        //    #endregion

        //    string caseNumber = "CAS-000005-0897";
        //    var caseid = new Guid("33a5b8b9-2871-eb11-a30a-005056926fe4");
        //    var formID = new Guid("e3c208c3-2871-eb11-a30a-005056926fe4");

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(caseNumber, caseid.ToString())
        //        .OpenCaseRecord(caseid.ToString(), caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .ValidateStatusCellText(formID.ToString(), "Not Initialized");

        //    #region Localized String Value

        //    foreach (var localizedStringValueId in dbHelper.localizedStringValue.GetByLocalizedStringId(localizedStringId))
        //        dbHelper.localizedStringValue.DeleteLocalizedStringValue(localizedStringValueId);

        //    #endregion
        //}

        //[TestProperty("JiraIssueID", "CDV6-10009")]
        //[Description("Open a Case Record - Navigate to the Case Forms Section - " +
        //    "Validate that the forms with status = 'Not Started' display with status text of 'Not Initialized' ")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void LocalisedTextChangeBeingIgnored_AssessmentStatus_UITestMethod002()
        //{
        //    #region Localized String

        //    var localizedStringId = dbHelper.localizedString.GetByName("OptionSetValue.Text.8819F5FC-C855-E311-B694-1803731F3EE3")[0];

        //    #endregion

        //    #region Localized String Value

        //    foreach (var localizedStringValueId in dbHelper.localizedStringValue.GetByLocalizedStringId(localizedStringId))
        //        dbHelper.localizedStringValue.DeleteLocalizedStringValue(localizedStringValueId);

        //    dbHelper.localizedStringValue.CreateLocalizedStringValue(localizedStringId, _languageId, "Not Initialized");

        //    #endregion


        //    string caseNumber = "CAS-000005-0897";
        //    var caseid = new Guid("33a5b8b9-2871-eb11-a30a-005056926fe4");
        //    var formID = new Guid("e3c208c3-2871-eb11-a30a-005056926fe4");

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToCasesSection();

        //    casesPage
        //        .WaitForCasesPageToLoad()
        //        .SearchByCaseNumber(caseNumber, caseid.ToString())
        //        .OpenCaseRecord(caseid.ToString(), caseNumber);

        //    caseRecordPage
        //        .WaitForCaseRecordPageToLoad()
        //        .NavigateToFormsCase();

        //    caseCasesFormPage
        //        .WaitForCaseCaseFormPageToLoad()
        //        .OpenRecord(formID.ToString());

        //    caseFormPage
        //        .WaitForCaseFormPageToLoad()
        //        .ValidateStatusField("Not Initialized");

        //    #region Localized String Value

        //    foreach (var localizedStringValueId in dbHelper.localizedStringValue.GetByLocalizedStringId(localizedStringId))
        //        dbHelper.localizedStringValue.DeleteLocalizedStringValue(localizedStringValueId);

        //    #endregion
        //}

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-7377

        [TestProperty("JiraIssueID", "CDV6-10010")]
        [Description("Login on Caredirector - Enter a Case Form record Hyperlink in the browser URL - Validate that the user is redirected to the Case Form record page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseForm_OpenPageHyperlink_UITestMethod01()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);
            var CaseFormTitle = (string)dbHelper.caseForm.GetCaseFormByID(caseFormID, "title")["title"];
            var CaseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];
            string businessObjectName = "caseform";
            string expectedHyperlink = string.Format(appURL + "/pages/Default.aspx?ReturnUrl=..%2Fpages%2Feditpage.aspx%3Fid%3D{0}%26type%3D{1}", caseFormID.ToString(), businessObjectName);

            //login in the app
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            caseFormPage
                .OpenCaseFormRecordHyperlink(expectedHyperlink)
                .WaitForCaseFormRecordPageToLoadFromHyperlink(CaseFormTitle)
                .ValidateCaseFieldLinkText(CaseTitle);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9098

        [TestProperty("JiraIssueID", "CDV6-10011")]
        [Description("Validate that workflow actions are audited. This test will trigger the workflow 'WF Automated Testing - CDV6-9098'. " +
            "This workflow will create a new case form and will update and assign it. All those actions must be audited and identify the workflow that cause them.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Workflow_AuditRecord_UITestMethod001()
        {
            #region Workflow

            commonMethodsDB.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9098", "WF Automated Testing - CDV6-9098.Zip");

            #endregion

            #region Team

            commonMethodsDB.CreateTeam(new Guid("890eb091-6f10-ea11-a2c8-005056926fe4"), "Assessment Team", _defaultUserId, _careDirectorQA_BusinessUnitId, "at99", "at@somarmail.com", "desc...", "123456");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("05/04/2021")
                .InsertDueDate("06/04/2021")
                .InsertReviewDate("07/04/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId, new DateTime(2021, 4, 8));
            Assert.AreEqual(1, caseformIDs.Count);


            WebAPIHelper.Security.Authenticate();

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //Create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = caseformIDs[0].ToString(),
                ParentTypeName = "caseform",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("WF Automated Testing - CDV6-9098", auditResponseData.GridData[0].cols[3].Text);


            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = caseformIDs[0].ToString(),
                ParentTypeName = "caseform",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("WF Automated Testing - CDV6-9098", auditResponseData.GridData[0].cols[3].Text);


            auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 6, //assign operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = caseformIDs[0].ToString(),
                ParentTypeName = "caseform",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("WF Automated Testing - CDV6-9098", auditResponseData.GridData[0].cols[3].Text);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9099

        [TestProperty("JiraIssueID", "CDV6-10012")]
        [Description("This test will trigger the workflow 'WF Automated Testing - CDV6-9099'. " +
            "Create a new case form record that will trigger the workflow. " +
            "This workflow will try to create a new health appointment for a slot that is not valid. When saving the case form an informative error message should be displayed to the user")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MessageToUserWithinSystemIfWorkflowIsCausingAnError_UITestMethod001()
        {
            string caseNumber = "CAS-3-220793";
            Guid caseid = new Guid("393c0209-ea3a-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            //remove all Forms for the Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("01/04/2021")
                .InsertDueDate("02/04/2021")
                .InsertReviewDate("03/04/2021")
                .TapSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Error in Workflow 'WF Automated Testing - CDV6-9099': There are no slots")
                .TapCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9273

        [TestProperty("JiraIssueID", "CDV6-10013")]
        [Description("Test for the incident identified in https://advancedcsg.atlassian.net/browse/CDV6-9273 - " +
            "System user TestUserCDV69273 has no security profile that grants access to Task business object - " +
            "User will create a new Case Form record that will trigger the workflow 'WF Automated Testing - CDV6-9273' - The workflow will create a new Task record - " +
            "Validate that the task record is created.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WorkflowIssueWithSecurityProfile_UITestMethod001()
        {
            #region SecurityProfiles

            var userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Case Module (Org Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Forms (Org View)")[0]
            };


            #endregion

            #region System User

            var _username = "TestUserCDV69273";
            var _userID = commonMethodsDB.CreateSystemUserRecord(_username, "TestUser", "CDV69273", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);

            #endregion

            #region Workflow

            commonMethodsDB.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-9273", "WF Automated Testing - CDV6-9273.Zip");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("TestUserCDV69273", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("12/04/2021")
                .InsertDueDate("13/04/2021")
                .InsertReviewDate("14/04/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

            var personTasks = dbHelper.task.GetTaskByPersonID(_personId);
            Assert.AreEqual(1, personTasks.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9271

        [TestProperty("JiraIssueID", "CDV6-10014")]
        [Description("Test for the incident identified in https://advancedcsg.atlassian.net/browse/CDV6-9271 - " +
            "Validate that the responsible team in 'create brokerage episode' action can set the owner of the brokerage episode record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WorkflowAssignBrokerageEpisode_UITestMethod001()
        {
            #region Document

            var newDocumentId = commonMethodsDB.CreateDocumentIfNeeded("Social Care Assessment Demo", "Social Care Assessment Demo.Zip");

            #endregion

            #region Workflow

            var workflowid = commonMethodsDB.CreateWorkflowIfNeeded("SCA - Brokerage", "SCA - Brokerage.Zip");

            #endregion

            #region Document / Document Business Object Mapping

            commonMethodsDB.CreateDocumentBusinessObjectMapping("Social Care Assessment Demo - BrokerageEpisode", "Social Care Assessment Demo - BrokerageEpisode.Zip");

            #endregion

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, newDocumentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();

            //get all "Not Started" workflow jobs
            var workflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid, 1).FirstOrDefault();

            //authenticate against the web api
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            WebAPIHelper.WorkflowJob.Execute(workflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(workflowJobId);

            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(_caseId);
            Assert.AreEqual(1, episodes.Count);

            var brokerageEpisodeResponsibleTeamId = Guid.Parse((dbHelper.brokerageEpisode.GetBrokerageEpisodeByID(episodes[0], "ownerid")["ownerid"]).ToString());
            Assert.AreEqual(_careDirectorQA_TeamId, brokerageEpisodeResponsibleTeamId);
        }

        [TestProperty("JiraIssueID", "CDV6-10015")]
        [Description("Test for the incident identified in https://advancedcsg.atlassian.net/browse/CDV6-9271 - " +
            "Validate that the responsible team in 'create brokerage episode' action can set the owner of the brokerage episode record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WorkflowAssignBrokerageEpisode_UITestMethod002()
        {
            #region Document

            var newDocumentId = commonMethodsDB.CreateDocumentIfNeeded("Social Care Assessment Demo", "Social Care Assessment Demo.Zip");

            #endregion

            #region Workflow

            var workflowid = commonMethodsDB.CreateWorkflowIfNeeded("SCA - Brokerage", "SCA - Brokerage.Zip");

            #endregion

            #region Document / Document Business Object Mapping

            commonMethodsDB.CreateDocumentBusinessObjectMapping("Social Care Assessment Demo - BrokerageEpisode", "Social Care Assessment Demo - BrokerageEpisode.Zip");

            #endregion

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, newDocumentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Not Started")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();

            //get all "Not Started" workflow jobs
            var workflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid, 1).FirstOrDefault();

            //authenticate against the web api
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            WebAPIHelper.WorkflowJob.Execute(workflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(workflowJobId);

            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(_caseId);
            Assert.AreEqual(0, episodes.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10217 && https://advancedcsg.atlassian.net/browse/CDV6-13072

        [TestProperty("JiraIssueID", "CDV6-11421")]
        [Description("Trigger the 'WF Automated Testing - CDV6-10217' workflow with a case form with start date = '01/05/2021' and review date = '02/05/2021' - " +
            "Validate that only the Consent To Treatment records linked to the Case that is linked to the Case Form are updated")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UpdateMultipleRecordsBasedOnAnSingleAction_UITestMethod001()
        {
            var consentToTreatmentCompletionDate = new DateTime(2021, 5, 3);
            var consentToTreatment1_Person1_Case1 = new Guid("0d5f4015-65b2-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 1)
            var consentToTreatment2_Person1_Case1 = new Guid("457592bd-64b2-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 1)
            var consentToTreatment1_Person1_Case2 = new Guid("fa6f1629-65b2-eb11-a323-005056926fe4"); //consent to treatment for Case 2 (Person 1)
            var consentToTreatment1_Person2_Case1 = new Guid("9a193cdc-12b3-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 2)

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var caseNumber = "CAS-3-413259";
            var caseid = new Guid("3992a0a9-3740-e911-a2c5-005056926fe4");
            var personID = new Guid("3b5813c3-318a-4353-9fc0-4ec0960ed056");
            var responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            var documentid = new Guid("9290d446-3da9-e911-a2c6-005056926fe4"); //Automated UI Test Document 1
            int assessmentstatusid = 1; //In Progress
            var startDate = new DateTime(2021, 5, 1);
            var reviewDate = new DateTime(2021, 5, 2);

            //remove all case forms for the case record
            foreach (var caseformid in dbHelper.caseForm.GetCaseFormByCaseID(caseid))
                dbHelper.caseForm.DeleteCaseForm(caseformid);

            //Create a new case form
            var caseFormID = dbHelper.caseForm.CreateCaseForm(OwnerID, personID, "Elsie Hebert", responsibleuserid, caseid, caseNumber, documentid, "Automated UI Test Document 1", assessmentstatusid, startDate, null, reviewDate);

            //reset the Consent to Treatment records dates
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person1_Case1, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment2_Person1_Case1, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person1_Case2, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person2_Case1, consentToTreatmentCompletionDate);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            //first consent to treatment for the first case should be updated (Person 1)
            var fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person1_Case1, "completiondatetime");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["completiondatetime"]).ToLocalTime().Date);

            //second consent to treatment for the first case should be updated (Person 1)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment2_Person1_Case1, "completiondatetime");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["completiondatetime"]).ToLocalTime().Date);

            //first consent to treatment for the second case should NOT be updated (Person 1)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person1_Case2, "completiondatetime");
            Assert.AreEqual(consentToTreatmentCompletionDate, ((DateTime)fields["completiondatetime"]).ToLocalTime());

            //first consent to treatment for the first case should NOT be updated (Person 2)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person2_Case1, "completiondatetime");
            Assert.AreEqual(consentToTreatmentCompletionDate, ((DateTime)fields["completiondatetime"]).ToLocalTime());

        }

        [TestProperty("JiraIssueID", "CDV6-11422")]
        [Description("Trigger the 'WF Automated Testing - CDV6-10217' workflow with a case form with start date = '03/05/2021' and review date = '04/05/2021' - " +
            "Validate that all Consent To Treatment records linked to the Person that is linked to the Case Form are updated")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UpdateMultipleRecordsBasedOnAnSingleAction_UITestMethod002()
        {
            var consentToTreatmentCompletionDate = new DateTime(2021, 5, 3);
            var consentToTreatment1_Person1_Case1 = new Guid("0d5f4015-65b2-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 1)
            var consentToTreatment2_Person1_Case1 = new Guid("457592bd-64b2-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 1)
            var consentToTreatment1_Person1_Case2 = new Guid("fa6f1629-65b2-eb11-a323-005056926fe4"); //consent to treatment for Case 2 (Person 1)
            var consentToTreatment1_Person2_Case1 = new Guid("9a193cdc-12b3-eb11-a323-005056926fe4"); //consent to treatment for Case 1 (Person 2)

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var caseNumber = "CAS-3-413259";
            var caseid = new Guid("3992a0a9-3740-e911-a2c5-005056926fe4");
            var personID = new Guid("3b5813c3-318a-4353-9fc0-4ec0960ed056");
            var responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            var documentid = new Guid("9290d446-3da9-e911-a2c6-005056926fe4"); //Automated UI Test Document 1
            int assessmentstatusid = 1; //In Progress
            var startDate = new DateTime(2021, 5, 3);
            var reviewDate = new DateTime(2021, 5, 4);

            //remove all case forms for the case record
            foreach (var caseformid in dbHelper.caseForm.GetCaseFormByCaseID(caseid))
                dbHelper.caseForm.DeleteCaseForm(caseformid);

            //Create a new case form
            var caseFormID = dbHelper.caseForm.CreateCaseForm(OwnerID, personID, "Elsie Hebert", responsibleuserid, caseid, caseNumber, documentid, "Automated UI Test Document 1", assessmentstatusid, startDate, null, reviewDate);

            //reset the Consent to Treatment records dates
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person1_Case1, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment2_Person1_Case1, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person1_Case2, consentToTreatmentCompletionDate);
            dbHelper.personConsentToTreatment.UpdateCompletionDatetime(consentToTreatment1_Person2_Case1, consentToTreatmentCompletionDate);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            //first consent to treatment for the first case should be updated (Person 1)
            var fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person1_Case1, "completiondatetime");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["completiondatetime"]).ToLocalTime().Date);

            //second consent to treatment for the first case should be updated (Person 1)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment2_Person1_Case1, "completiondatetime");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["completiondatetime"]).ToLocalTime().Date);

            //first consent to treatment for the second case should NOT be updated (Person 1)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person1_Case2, "completiondatetime");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["completiondatetime"]).ToLocalTime().Date);

            //first consent to treatment for the first case should NOT be updated (Person 2)
            fields = dbHelper.personConsentToTreatment.GetPersonConsentToTreatmentByID(consentToTreatment1_Person2_Case1, "completiondatetime");
            Assert.AreEqual(consentToTreatmentCompletionDate, ((DateTime)fields["completiondatetime"]).ToLocalTime());

        }

        [TestProperty("JiraIssueID", "CDV6-11423")]
        [Description("Trigger the 'WF Automated Testing - CDV6-10217' workflow with a case form with start date = '06/05/2021' and review date = '07/05/2021' - " +
            "Validate that all Case Note records linked to the Case that is linked to the Case Form are updated")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UpdateMultipleRecordsBasedOnAnSingleAction_UITestMethod003()
        {
            var defaultOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed

            var caseNote1_Person1_Case1 = new Guid("1f295d0f-05b5-ec11-a335-005056926fe4"); //Case Note for Case 1(Person 1)
            var caseNote2_Person1_Case1 = new Guid("e8d98c17-05b5-ec11-a335-005056926fe4"); //Case Note for Case 1 (Person 1)
            var caseNote1_Person1_Case2 = new Guid("d2eed882-05b5-ec11-a335-005056926fe4"); //Case Note for Case 2 (Person 1)
            var caseNote1_Person2_Case1 = new Guid("637961bd-05b5-ec11-a335-005056926fe4"); //Case Note for Case 1 (Person 2)

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var caseNumber = "CAS-3-413259";
            var caseid = new Guid("3992a0a9-3740-e911-a2c5-005056926fe4");
            var personID = new Guid("3b5813c3-318a-4353-9fc0-4ec0960ed056");
            var responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            var documentid = new Guid("9290d446-3da9-e911-a2c6-005056926fe4"); //Automated UI Test Document 1
            int assessmentstatusid = 1; //In Progress
            var startDate = new DateTime(2021, 5, 6);
            var reviewDate = new DateTime(2021, 5, 7);

            //remove all case forms for the case record
            foreach (var caseformid in dbHelper.caseForm.GetCaseFormByCaseID(caseid))
                dbHelper.caseForm.DeleteCaseForm(caseformid);

            //Create a new case form
            var caseFormID = dbHelper.caseForm.CreateCaseForm(OwnerID, personID, "Elsie Hebert", responsibleuserid, caseid, caseNumber, documentid, "Automated UI Test Document 1", assessmentstatusid, startDate, null, reviewDate);

            //reset the case note records outcomes
            dbHelper.caseCaseNote.UpdateCaseNote(caseNote1_Person1_Case1, defaultOutcomeID, "");
            dbHelper.caseCaseNote.UpdateCaseNote(caseNote2_Person1_Case1, null, "");
            dbHelper.caseCaseNote.UpdateCaseNote(caseNote1_Person1_Case2, defaultOutcomeID, "");
            dbHelper.caseCaseNote.UpdateCaseNote(caseNote1_Person2_Case1, defaultOutcomeID, "");


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();



            System.Threading.Thread.Sleep(2000);

            var expectedOutcomeID = new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"); //Completed

            //first case note for the first case should be updated (Person 1)
            var fields = dbHelper.caseCaseNote.GetByID(caseNote1_Person1_Case1, "activityoutcomeid", "notes");
            Assert.AreEqual(expectedOutcomeID, fields["activityoutcomeid"]);
            Assert.AreEqual("<p>06/05/2021</p>", fields["notes"]);

            //second case note for the first case should be updated (Person 1)
            fields = dbHelper.caseCaseNote.GetByID(caseNote2_Person1_Case1, "activityoutcomeid", "notes");
            Assert.AreEqual(expectedOutcomeID, fields["activityoutcomeid"]);
            Assert.AreEqual("<p>06/05/2021</p>", fields["notes"]);

            //first case note for the second case should NOT be updated (Person 1)
            fields = dbHelper.caseCaseNote.GetByID(caseNote1_Person1_Case2, "activityoutcomeid", "notes");
            Assert.AreEqual(defaultOutcomeID, fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields.ContainsKey("notes"));

            //first case note for the first case should NOT be updated (Person 2)
            fields = dbHelper.caseCaseNote.GetByID(caseNote1_Person2_Case1, "activityoutcomeid", "notes");
            Assert.AreEqual(defaultOutcomeID, fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields.ContainsKey("notes"));

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10220

        [TestProperty("JiraIssueID", "CDV6-11424")]
        [Description("Open a case form record (document = 'Automated UI Test Document 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = Yes are displayed - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = No and linked to the current document type are displayed - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = No and with no link to the current document type are NOT displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FilterActionsOutcomesByFormDocument_UITestMethod001()
        {
            var outcome1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_1", new DateTime(2021, 1, 1), null, false, true);
            var outcome2 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_2", new DateTime(2021, 1, 1), null, false, true);
            var outcome3 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_3", new DateTime(2021, 1, 1), null, false, false);
            var outcome4 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_4", new DateTime(2021, 1, 1), null, false, false);
            var outcome5 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_5", new DateTime(2021, 1, 1), null, false, false);
            var outcome6 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_6", new DateTime(2021, 1, 1), null, false, false);
            commonMethodsDB.CreateDocumentOutcomeType(outcome3, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome4, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome5, _document2Id);
            commonMethodsDB.CreateDocumentOutcomeType(outcome6, _document2Id);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad();

            caseFormActionsOutcomesPageFrame
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(outcome1.ToString())
                .ValidateResultElementPresent(outcome2.ToString())
                .ValidateResultElementPresent(outcome3.ToString())
                .ValidateResultElementPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11425")]
        [Description("Open a case form record (document = 'Automated UI Test Document 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = Yes - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FilterActionsOutcomesByFormDocument_UITestMethod002()
        {
            var outcome1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_1", new DateTime(2021, 1, 1), null, false, true);
            var outcome2 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_2", new DateTime(2021, 1, 1), null, false, true);
            var outcome3 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_3", new DateTime(2021, 1, 1), null, false, false);
            var outcome4 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_4", new DateTime(2021, 1, 1), null, false, false);
            var outcome5 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_5", new DateTime(2021, 1, 1), null, false, false);
            var outcome6 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_6", new DateTime(2021, 1, 1), null, false, false);
            commonMethodsDB.CreateDocumentOutcomeType(outcome3, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome4, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome5, _document2Id);
            commonMethodsDB.CreateDocumentOutcomeType(outcome6, _document2Id);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad();

            caseFormActionsOutcomesPageFrame
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("outcome type_1")
                .TapSearchButton()
                .ValidateResultElementPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementNotPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11426")]
        [Description("Open a case form record (document = 'Automated UI Test Document 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = No and linked to the current document type - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FilterActionsOutcomesByFormDocument_UITestMethod003()
        {
            var outcome1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_1", new DateTime(2021, 1, 1), null, false, true);
            var outcome2 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_2", new DateTime(2021, 1, 1), null, false, true);
            var outcome3 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_3", new DateTime(2021, 1, 1), null, false, false);
            var outcome4 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_4", new DateTime(2021, 1, 1), null, false, false);
            var outcome5 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_5", new DateTime(2021, 1, 1), null, false, false);
            var outcome6 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_6", new DateTime(2021, 1, 1), null, false, false);
            commonMethodsDB.CreateDocumentOutcomeType(outcome3, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome4, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome5, _document2Id);
            commonMethodsDB.CreateDocumentOutcomeType(outcome6, _document2Id);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad();

            caseFormActionsOutcomesPageFrame
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("outcome type_3")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-11427")]
        [Description("Open a case form record (document = 'Automated UI Test Document 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = No and with no link to the current document type - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FilterActionsOutcomesByFormDocument_UITestMethod004()
        {
            var outcome1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_1", new DateTime(2021, 1, 1), null, false, true);
            var outcome2 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_2", new DateTime(2021, 1, 1), null, false, true);
            var outcome3 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_3", new DateTime(2021, 1, 1), null, false, false);
            var outcome4 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_4", new DateTime(2021, 1, 1), null, false, false);
            var outcome5 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_5", new DateTime(2021, 1, 1), null, false, false);
            var outcome6 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_6", new DateTime(2021, 1, 1), null, false, false);
            commonMethodsDB.CreateDocumentOutcomeType(outcome3, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome4, _documentId);
            commonMethodsDB.CreateDocumentOutcomeType(outcome5, _document2Id);
            commonMethodsDB.CreateDocumentOutcomeType(outcome6, _document2Id);

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);



            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad();

            caseFormActionsOutcomesPageFrame
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("outcome type_5")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementNotPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString())
                ;
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10301

        [TestProperty("JiraIssueID", "CDV6-11428")]
        [Description("Open a Case Form (Automated UI Test Document 1) - Click on the edit assessment button - Wait for the edit assessment page to load - " +
            "Validate that the 'Gui-PersonDOB' CMS Readonly Field correctly displays the person DOB.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CMSReadonlyField_DrillThrough_UITestMethod001()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            dbHelper.person.UpdateDateOfBirth(_personId, new DateTime(2000, 8, 13));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateGuiPersonDOBQuestionText("13/08/2000");
        }

        [TestProperty("JiraIssueID", "CDV6-11429")]
        [Description("Open a Case Form (Automated UI Test Document 1) - Click on the edit assessment button - Wait for the edit assessment page to load - " +
            "Click on the 'Gui-PersonDOB' question link - Validate that the person page details popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CMSReadonlyField_DrillThrough_UITestMethod002()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            dbHelper.person.UpdateDateOfBirth(_personId, new DateTime(2000, 8, 13));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickGuiPersonDOBQuestion();

            personRecordPage
                .WaitForPersonRecordPopupPageToLoadFromCaseForm();
        }

        [TestProperty("JiraIssueID", "CDV6-11430")]
        [Description("Open a Case Form (Automated UI Test Document 1) - Click on the edit assessment button - Wait for the edit assessment page to load - " +
            "Click on the 'Gui-PersonDOB' question link - Wait for the person page details popup to be displayed - Click on the back button - " +
            "Validate that the person page details popup is closed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CMSReadonlyField_DrillThrough_UITestMethod003()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            dbHelper.person.UpdateDateOfBirth(_personId, new DateTime(2000, 8, 13));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickGuiPersonDOBQuestion();

            personRecordPage
                .WaitForPersonRecordPopupPageToLoadFromCaseForm()
                .TapBackButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-11431")]
        [Description("Open a Case Form (Automated UI Test Document 1) - Click on the edit assessment button - Wait for the edit assessment page to load - " +
            "Click on the 'Gui-PersonDOB' question link - Wait for the person page details popup to be displayed - Click on the Edit button - " +
            "Edit the person DOB - Save the changes - Navigate back to the edit assessment page - Validate that the 'Gui-PersonDOB' field is automatically updated with the new DOB")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CMSReadonlyField_DrillThrough_UITestMethod004()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            dbHelper.person.UpdateDateOfBirth(_personId, new DateTime(2000, 8, 13));

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickGuiPersonDOBQuestion();

            personRecordPage
                .WaitForPersonRecordPopupPageToLoadFromCaseForm()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(_personId.ToString(), firstName + " " + lastName)
                .InsertDOB("14/09/2001")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPopupPageToLoadFromCaseForm()
                .TapBackButton();

            Sleep(3000);

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateGuiPersonDOBQuestionText("14/09/2001");

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10345

        [TestProperty("JiraIssueID", "CDV6-11432")]
        [Description("Trigger the document rule '46 - CDV6-10345' - Validate that the Case Form record 'Review Date' is updated accordingly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExecuteWorkflowOnServicerSideRule_UITestMethod01()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("Rules Testing - CDV6-10345")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            DateTime expectedReviewDate = new DateTime(2021, 5, 24, 0, 00, 0);
            var fields = dbHelper.caseForm.GetCaseFormByID(caseFormID, "reviewdate");
            Assert.AreEqual(expectedReviewDate, ((DateTime)fields["reviewdate"]).ToLocalTime().Date);
        }

        [TestProperty("JiraIssueID", "CDV6-11433")]
        [Description("Trigger the document rule '46 - CDV6-10345' - Validate that the Case record 'Review Date' is updated accordingly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExecuteWorkflowOnServicerSideRule_UITestMethod02()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("Rules Testing - CDV6-10345")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            DateTime expectedReviewDate = new DateTime(2021, 5, 24, 0, 00, 0);
            var fields = dbHelper.Case.GetCaseByID(_caseId, "reviewdate");
            Assert.AreEqual(expectedReviewDate, ((DateTime)fields["reviewdate"]).ToLocalTime().Date);
        }

        [TestProperty("JiraIssueID", "CDV6-11434")]
        [Description("Trigger the document rule '46 - CDV6-10345' - Validate that the Person record 'Telephone 3' is updated accordingly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExecuteWorkflowOnServicerSideRule_UITestMethod03()
        {
            //reset review date for the case record
            dbHelper.person.UpdateTelephone3(_personId, "123454321");

            //Create a new case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .InsertWFShortAnswer("Rules Testing - CDV6-10345")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            var fields = dbHelper.person.GetPersonById(_personId, "telephone3");
            Assert.AreEqual("9876543210", fields["telephone3"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10735

        [TestProperty("JiraIssueID", "CDV6-11435")]
        [Description("Validate that the workflow 'WF Automated Testing - CDV6-10735' do not trigger an infinite loop of workflow jobs creation")]
        [TestMethod]
        [TestCategory("UITest")]
        public void InfinitLoopTesting_UITestMethod01()
        {
            var workflowId = new Guid("b04fe4a5-8cc3-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-10735

            string caseNumber = "CAS-3-136953";
            Guid caseid = new Guid("58309d6b-e63a-e911-a2c5-005056926fe4");
            Guid caseFormID = new Guid("7aa982c3-8dc3-eb11-a323-005056926fe4");

            //remove all workflow jobs for the workflow
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            //open the form and save it twice to generate 2 workflow jobs
            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())

                .InsertWFShortAnswer("WF Testing CDV6-10735")
                .InsertWFParagraph(Guid.NewGuid().ToString())

                .TapSaveButton(true)

                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())

                .InsertWFParagraph(Guid.NewGuid().ToString())

                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            //check that we have 2 workflow jobs
            var workflowJobs = dbHelper.workflowJob.GetByWorkflowIdAndRegardingId(workflowId, caseFormID);
            Assert.AreEqual(2, workflowJobs.Count);

            //wait for the jobs to finish
            foreach (var jobid in workflowJobs)
                dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(jobid);

            //check that we do not have more jobs created for the case form
            workflowJobs = dbHelper.workflowJob.GetByWorkflowIdAndRegardingId(workflowId, caseFormID);
            Assert.AreEqual(2, workflowJobs.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8710

        [TestProperty("JiraIssueID", "CDV6-11436")]
        [Description("Create a new case form record so that the workflow 'WF Automated Testing - CDV6-8710 (Published)' can be triggered (workflow contains actions with Custom Field titles) - " +
            "Validate that the workflow is executed correctly")]
        [TestMethod]
        [TestCategory("UITest")]
        public void OptionalTextboxForActionName_UITestMethod026()
        {
            string caseNumber = "CAS-3-97106";
            Guid caseid = new Guid("531e273b-e53a-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            //remove all Forms for the Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                foreach (var phonecallid in dbHelper.phoneCall.GetPhoneCallByRegardingID(caseFormID))
                    dbHelper.phoneCall.DeletePhoneCall(phonecallid);

                foreach (var attachmentid in dbHelper.caseFormAttachment.GetByCaseFormID(caseFormID))
                    dbHelper.caseFormAttachment.DeleteCaseFormAttachment(attachmentid);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton()
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("01/06/2021")
                .InsertDueDate("02/06/2021")
                .InsertReviewDate("03/06/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();


            System.Threading.Thread.Sleep(2000);
            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID);
            Assert.AreEqual(1, caseformIDs.Count);
            var caseformid = caseformIDs[0];

            var phoneCallRecords = dbHelper.phoneCall.GetPhoneCallByRegardingID(caseformid);
            Assert.AreEqual(1, phoneCallRecords.Count);

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallRecords[0], "subject", "phonecalldate", "notes");
            Assert.AreEqual("New Phone Call CDV6-8710", fields["subject"]);
            Assert.AreEqual(DateTime.Now.Date.AddDays(3), ((DateTime)fields["phonecalldate"]).Date);
            Assert.AreEqual("<p>desc...</p>", fields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11437")]
        [Description("Create a new case form record so that the workflow 'WF Automated Testing - CDV6-8710 (Published)' IS NOT triggered (workflow contains actions with Custom Field titles) - " +
            "Validate that the workflow not executed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void OptionalTextboxForActionName_UITestMethod027()
        {
            string caseNumber = "CAS-3-97106";
            Guid caseid = new Guid("531e273b-e53a-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            //remove all Forms for the Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                foreach (var phonecallid in dbHelper.phoneCall.GetPhoneCallByRegardingID(caseFormID))
                    dbHelper.phoneCall.DeletePhoneCall(phonecallid);

                foreach (var attachmentid in dbHelper.caseFormAttachment.GetByCaseFormID(caseFormID))
                    dbHelper.caseFormAttachment.DeleteCaseFormAttachment(attachmentid);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton()
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("01/06/2021")
                .InsertDueDate("02/06/2021")
                .InsertReviewDate("06/06/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID);
            Assert.AreEqual(1, caseformIDs.Count);
            var caseformid = caseformIDs[0];

            var phoneCallRecords = dbHelper.phoneCall.GetPhoneCallByRegardingID(caseformid);
            Assert.AreEqual(0, phoneCallRecords.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10855

        [TestProperty("JiraIssueID", "CDV6-11438")]
        [Description("Open a case record (Responsible Team is not Caredirector QA) - Linked person record has 1 case that belong to Caredirector QA team - " +
            "Create a case form that will trigger the workflow 'WF Automated Testing - CDV6-10855' (first if condition) - " +
            "Validate that a new case note record is created for the case that belongs to Caredirector QA team")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetLatestOpenCaseForPerson_OwningBusinessUnit_UITestMethod001()
        {
            string caseNumber = "CAS-3-267350";
            Guid caseid = new Guid("aae16784-3540-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            Guid caseIdBelongingToCaredirectorQA = new Guid("035d352f-e53a-e911-a2c5-005056926fe4"); //

            //remove all Forms for the main Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }

            //remove all case notes for main case
            foreach (var mainCaseCaseNoteId in dbHelper.caseCaseNote.GetByCaseID(caseid))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(mainCaseCaseNoteId);

            //remove all case notes for case that belongs to Caredirector QA team
            foreach (var secundaryCaseCaseNoteId in dbHelper.caseCaseNote.GetByCaseID(caseIdBelongingToCaredirectorQA))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(secundaryCaseCaseNoteId);



            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton()
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("04/06/2021")
                .InsertDueDate("05/06/2021")
                .InsertReviewDate("06/06/2021")
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID);
            Assert.AreEqual(1, caseformIDs.Count);

            var caseNoteRecords = dbHelper.caseCaseNote.GetByCaseID(caseid);
            Assert.AreEqual(0, caseNoteRecords.Count); //main case should have no case notes

            caseNoteRecords = dbHelper.caseCaseNote.GetByCaseID(caseIdBelongingToCaredirectorQA);
            Assert.AreEqual(1, caseNoteRecords.Count); //secondary case should have 1 case notes (case belongs to Caredirector QA)

            var fields = dbHelper.caseCaseNote.GetByID(caseNoteRecords[0], "subject");
            Assert.AreEqual("CN - Scenario 1", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11439")]
        [Description("Open a case record (Responsible Team is not Caredirector QA) - Linked person record has NO cases that belong to Caredirector QA team - " +
            "Create a case form that will trigger the workflow 'WF Automated Testing - CDV6-10855' (first if condition) - " +
            "Validate that an exception is displayed to the user after saving the case form record")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetLatestOpenCaseForPerson_OwningBusinessUnit_UITestMethod002()
        {
            string caseNumber = "CAS-3-267351";
            Guid caseid = new Guid("abe16784-3540-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            //remove all Forms for the main Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }

            //remove all case notes for main case
            foreach (var mainCaseCaseNoteId in dbHelper.caseCaseNote.GetByCaseID(caseid))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(mainCaseCaseNoteId);



            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton()
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("04/06/2021")
                .InsertDueDate("05/06/2021")
                .InsertReviewDate("06/06/2021")
                .TapSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Error in Workflow 'WF Automated Testing - CDV6-10855': Please provide a non null value for Case.")
                .TapCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID);
            Assert.AreEqual(0, caseformIDs.Count);

            var caseNoteRecords = dbHelper.caseCaseNote.GetByCaseID(caseid);
            Assert.AreEqual(0, caseNoteRecords.Count); //main case should have no case notes
        }

        [TestProperty("JiraIssueID", "CDV6-11440")]
        [Description("Open a case record (Responsible Team is not Caredirector QA) - Linked person record has 1 case that belong to Caredirector QA team - " +
            "Create a case form that will trigger the workflow 'WF Automated Testing - CDV6-10855' (second if condition) - " +
            "Validate that a new case note record is created for the case linked to the form ('Owning Business Unit' is not set in the workflow action) ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetLatestOpenCaseForPerson_OwningBusinessUnit_UITestMethod003()
        {
            string caseNumber = "CAS-3-267350";
            Guid caseid = new Guid("aae16784-3540-e911-a2c5-005056926fe4");
            string formTypeName = "automated ui test document 1";
            Guid formTypeID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4");

            Guid caseIdBelongingToCaredirectorQA = new Guid("035d352f-e53a-e911-a2c5-005056926fe4"); //

            //remove all Forms for the main Case record
            foreach (var caseFormID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(caseFormID);
            }

            //remove all case notes for main case
            foreach (var mainCaseCaseNoteId in dbHelper.caseCaseNote.GetByCaseID(caseid))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(mainCaseCaseNoteId);

            //remove all case notes for case that belongs to Caredirector QA team
            foreach (var secundaryCaseCaseNoteId in dbHelper.caseCaseNote.GetByCaseID(caseIdBelongingToCaredirectorQA))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(secundaryCaseCaseNoteId);



            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber)
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase()
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton()
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(formTypeName)
                .TapSearchButton()
                .SelectResultElement(formTypeID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("07/06/2021")
                .InsertDueDate("08/06/2021")
                .InsertReviewDate("09/06/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID);
            Assert.AreEqual(1, caseformIDs.Count);

            var caseNoteRecords = dbHelper.caseCaseNote.GetByCaseID(caseid);
            Assert.AreEqual(1, caseNoteRecords.Count); //main case should have no case notes

            var fields = dbHelper.caseCaseNote.GetByID(caseNoteRecords[0], "subject");
            Assert.AreEqual("CN - Scenario 2", fields["subject"]);

            caseNoteRecords = dbHelper.caseCaseNote.GetByCaseID(caseIdBelongingToCaredirectorQA);
            Assert.AreEqual(0, caseNoteRecords.Count); //secondary case should have 1 case notes (case belongs to Caredirector QA)            
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10470

        [TestProperty("JiraIssueID", "CDV6-11441")]
        [Description("Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Validate that, by default, no option is checked for the WF Boolean question - " +
            "Validate that no value is set on the TrueFalseAnswer column in the DocumentAnswer table for the matching answer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBooleanNoOptionSelected();

            var documentQuestionIdentifierid = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-186");
            Assert.AreEqual(1, documentQuestionIdentifierid.Count);

            var documentAnswerid = dbHelper.documentAnswer.GetDocumentAnswer(caseformID, documentQuestionIdentifierid[0]);
            Assert.AreEqual(1, documentAnswerid.Count);

            var documentAnswers = dbHelper.documentAnswer.GetDocumentAnswerByID(documentAnswerid[0], "truefalseanswer");
            Assert.IsFalse(documentAnswers.ContainsKey("truefalseanswer"));
        }

        [TestProperty("JiraIssueID", "CDV6-11442")]
        [Description("Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Set an answer in the 'WF Time' question - Save, close and reopen the assessment - " +
            "Validate that no option is checked for the WF Boolean question - " +
            "Validate that no value is set on the TrueFalseAnswer column in the DocumentAnswer table for the matching answer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .InsertWFTime("07:15")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFTimeQuestion("07:15")
                .ValidateWFBooleanNoOptionSelected();

            var documentQuestionIdentifierid = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-186");
            Assert.AreEqual(1, documentQuestionIdentifierid.Count);

            var documentAnswerid = dbHelper.documentAnswer.GetDocumentAnswer(caseformID, documentQuestionIdentifierid[0]);
            Assert.AreEqual(1, documentAnswerid.Count);

            var documentAnswers = dbHelper.documentAnswer.GetDocumentAnswerByID(documentAnswerid[0], "truefalseanswer");
            Assert.IsFalse(documentAnswers.ContainsKey("truefalseanswer"));
        }

        [TestProperty("JiraIssueID", "CDV6-11443")]
        [Description("Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Set  'WF Boolean' answer to 'Yes' - Save, close and reopen the assessment - " +
            "Validate that the Yes option is checked for the WF Boolean question - " +
            "Validate that '1' is set on the TrueFalseAnswer column in the DocumentAnswer table for the matching answer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .SelectWFBoolean(true)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBoolean(true);

            var documentQuestionIdentifierid = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-186");
            Assert.AreEqual(1, documentQuestionIdentifierid.Count);

            var documentAnswerid = dbHelper.documentAnswer.GetDocumentAnswer(caseformID, documentQuestionIdentifierid[0]);
            Assert.AreEqual(1, documentAnswerid.Count);

            var documentAnswers = dbHelper.documentAnswer.GetDocumentAnswerByID(documentAnswerid[0], "truefalseanswer");
            Assert.AreEqual(true, documentAnswers["truefalseanswer"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11444")]
        [Description("Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Set  'WF Boolean' answer to 'No' - Save, close and reopen the assessment - " +
            "Validate that the ''No' option is checked for the WF Boolean question - " +
            "Validate that '0' is set on the TrueFalseAnswer column in the DocumentAnswer table for the matching answer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod004()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .SelectWFBoolean(false)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBoolean(false);

            var documentQuestionIdentifierid = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-186");
            Assert.AreEqual(1, documentQuestionIdentifierid.Count);

            var documentAnswerid = dbHelper.documentAnswer.GetDocumentAnswer(caseformID, documentQuestionIdentifierid[0]);
            Assert.AreEqual(1, documentAnswerid.Count);

            var documentAnswers = dbHelper.documentAnswer.GetDocumentAnswerByID(documentAnswerid[0], "truefalseanswer");
            Assert.AreEqual(false, documentAnswers["truefalseanswer"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11445")]
        [Description("Test for the document rule '1 - Rule Conditions Testing - Boolean Question' Step 5 (Step 5 - CDV6-10470) - " +
            "Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Insert 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470' in the WF Short Answer question - " +
            "Save, close and re-open the assessment - Validate that no alert popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod005()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBooleanNoOptionSelected();
        }

        [TestProperty("JiraIssueID", "CDV6-11446")]
        [Description("Test for the document rule '1 - Rule Conditions Testing - Boolean Question' Step 5 (Step 5 - CDV6-10470) - " +
            "Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Insert 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470' in the WF Short Answer question - Set WF Boolean answer to 'Yes' - " +
            "Save, close and re-open the assessment - Validate that an alert popup is displayed with the text 'CDV6-10470 - Scenario 1 activated' ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod006()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470")
                .SelectWFBoolean(true)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10470 - Scenario 1 activated").TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBoolean(true);
        }

        [TestProperty("JiraIssueID", "CDV6-11447")]
        [Description("Test for the document rule '1 - Rule Conditions Testing - Boolean Question' Step 5 (Step 5 - CDV6-10470) - " +
            "Create a new case form record of type 'automated ui test document 1' - Open the Assessment in edit mode - " +
            "Insert 'UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470' in the WF Short Answer question - Set WF Boolean answer to 'No' - " +
            "Save, close and re-open the assessment - Validate that an alert popup is displayed with the text 'CDV6-10470 - Scenario 2 activated' ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void NoDefaultValuesInRadioButtons_UITestMethod007()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentName).TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCaseFormPageToLoad()
                .TapBackButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            var caseformID = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId)[0];

            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .InsertWFShortAnswer("UI Testing - 21 - Rule Conditions Testing - Boolean Question - CDV6-10470")
                .SelectWFBoolean(false)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10470 - Scenario 2 activated").TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString())
                .ValidateWFBoolean(false);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11014

        [TestProperty("JiraIssueID", "CDV6-11448")]
        [Description("Open a case form record (document = 'Automated UI Test Document 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Validate that Inactive records are not displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void InactiveActionOutcomeTypes_UITestMethod001()
        {
            var outcome7Id = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Outcome Type_7", new DateTime(2021, 1, 1), null);
            dbHelper.caseFormOutcomeType.UpdateInactive(outcome7Id, true);

            int assessmentstatusid = 1; //In Progress
            var startDate = new DateTime(2021, 5, 1);
            var reviewDate = new DateTime(2021, 5, 2);

            //Create a new case form
            var caseFormID = dbHelper.caseForm.CreateCaseForm(_careDirectorQA_TeamId, _personId, "Elsie Hebert", _defaultLoginUserID, _caseId, _caseNumber, _documentId, "Automated UI Test Document 1", assessmentstatusid, startDate, null, reviewDate);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad();

            caseFormActionsOutcomesPageFrame
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(outcome7Id.ToString())
                .TypeSearchQuery("Outcome Type_7")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome7Id.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-12375

        [TestProperty("JiraIssueID", "CDV6-24598")]
        [Description("Open a new Assessment in edit mode - Click on the edit button for the signature question - " +
            "Wait for the signature popup to load - Draw a line - Click on the save button - Wait for the assessment page to load - " +
            "Validate that the signature delete button is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_Bug12375_UITestMethod01()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureEditButton();

            editSignaturePopup
                .WaitForEditSignaturePopupToLoad()
                .ClicAndDragonOnCanvas(0, 40)
                .ClickSaveButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateSignatureDeleteButtonVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24599")]
        [Description("Open a new Assessment in edit mode - Click on the edit button for the signature question - " +
            "Wait for the signature popup to load - Draw a line - Click on the save button - Wait for the assessment page to load - " +
            "Click on the edit button a second time - Wait for the signature popup to load - Click on the Clear button -  Draw a line in a different direction - " +
            "Click on the Save button - Wait for the assessment page to load - Click on the signature delete button - Confirm the delete operation - " +
            "Validate that the signature delete button is NOT displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_Bug12375_UITestMethod02()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureEditButton();

            editSignaturePopup
                .WaitForEditSignaturePopupToLoad()
                .ClicAndDragonOnCanvas(0, 40)
                .ClickSaveButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureEditButton();

            editSignaturePopup
                .WaitForEditSignaturePopupToLoad()
                .ClickClearButton()
                .ClicAndDragonOnCanvas(40, 0)
                .ClickSaveButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to delete the signature?").TapOKButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateSignatureDeleteButtonVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24600")]
        [Description("Open a new Assessment in edit mode - Click on the edit button for the signature question - " +
            "Wait for the signature popup to load - Draw a line - Click on the save button - Wait for the assessment page to load - " +
            "Click on the edit button a second time - Wait for the signature popup to load -  Draw a line in a different direction - " +
            "Click on the Close button - Wait for the assessment page to load - Click on the back button - Validate that the user is redirected back to the case form page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Phoenix_CaseForms_Bug12375_UITestMethod03()
        {
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

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
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureEditButton();

            editSignaturePopup
                .WaitForEditSignaturePopupToLoad()
                .ClicAndDragonOnCanvas(0, 40)
                .ClickSaveButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ClickSignatureEditButton();

            editSignaturePopup
                .WaitForEditSignaturePopupToLoad()
                .ClicAndDragonOnCanvas(40, 0)
                .ClickCloseButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();
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
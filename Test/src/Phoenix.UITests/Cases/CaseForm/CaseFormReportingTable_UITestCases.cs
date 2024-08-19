using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// 
    /// https://advancedcsg.atlassian.net/browse/CDV6-4614
    /// 
    /// Test methods to validate the Test Forms reporting
    /// 
    /// </summary>
    [DeploymentItem("Files\\D_Flag.Zip"), DeploymentItem("Files\\Automated UI Test Document 1.Zip"), DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    [TestClass]
    public class CaseFormReportingTable_UITestCases : FunctionalTest
    {
        public Guid FormsReportingJob { get { return new Guid("3c4f6a97-04de-ea11-aa91-461ca8e306ef"); } }

        private string _tenantName;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private Guid _ethnicityId;
        private Guid _dataFormId;
        private Guid _personId;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _allocateToTeam_caseStatusId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _presentingPriorityId;
        private Guid _casePriorityId;
        private Guid _documentId;
        private Guid _caseFormId;
        private string _caseFormTitle;
        private Guid _formsReportingJobId;

        [TestInitialize()]
        public void SmokeTests_SetupTest()
        {

            #region Tenant

            _tenantName = ConfigurationManager.AppSettings["TenantName"];
            dbHelper = new DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

            #endregion

            #region Internal

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Scheduled Job
            _formsReportingJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Forms Reporting Job")[0];

            dbHelper.scheduledJob.UpdateScheduledJobInactiveStatus(_formsReportingJobId, false);

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

            _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

            #endregion

            #region Team

            _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

            #endregion

            #region System User

            _systemUserId = commonMethodsDB.CreateSystemUserRecord("CaseFormReportingTableUser1", "CaseForm", "ReportingTableUser1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Document 
            commonMethodsDB.CreateDocumentIfNeeded("D_Flag", "D_Flag.Zip"); //Import Document

            commonMethodsDB.ImportFormula("Sum two values_minus a third.Zip"); //Formula Import

            commonMethodsDB.CreateWorkflowIfNeeded("WF Automated Testing - CDV6-10345", "WF Automated Testing - CDV6-10345.Zip"); //Workflow Import

            _documentId = commonMethodsDB.CreateDocumentIfNeeded("Automated UI Test Document 1", "Automated UI Test Document 1.Zip");//Import Document

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Dan";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            #region Case Status

            _allocateToTeam_caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team")[0];

            #endregion

            #region Contact Reason

            _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Presenting Priority

            _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Routine");

            #endregion

            #region Case Priority

            _casePriorityId = commonMethodsDB.CreateCasePriority("Very High", new DateTime(2020, 1, 1), 2, _careDirectorQA_TeamId);

            #endregion

            #region Data Form

            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Source

            _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("TestRPT", _careDirectorQA_TeamId);

            #endregion

            #region Case

            _caseId =
                dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _systemUserId, _allocateToTeam_caseStatusId,
                _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 01, 01), new DateTime(2021, 01, 01), 21,
                "Minerva led the way and Telemachus followed her.", "CaseFormReportingTableUser1", _presentingPriorityId);
            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Case Form

            _caseFormId = dbHelper.caseForm.CreateCaseForm(_careDirectorQA_TeamId, _personId, firstName + lastName, _systemUserId, _caseId, _caseNumber.ToString(), _documentId, "Automated UI Test Document 1", 1, new DateTime(2021, 02, 02), null, new DateTime(2021, 02, 02));

            #endregion

        }



        [TestProperty("JiraIssueID", "CDV6-10240")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the assessment information is stored in the forms reporting table associated with the document")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod01()
        {

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(_formsReportingJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(_formsReportingJobId);



            //at this point we should have only 1 row in the reports table for the assessment 
            var totalReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountReportDataByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalReportRecords);

            //we should have only 1 row in the unlimited row question reports table for the assessment  
            var totalUnlimitedRowsReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountWFTableWithUnlimitedRowsByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalUnlimitedRowsReportRecords);

            //all fields should be null in the main reports table
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);

        }

        [TestProperty("JiraIssueID", "CDV6-10241")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the assessment information is stored in the forms reporting table associated with the document - " +
            "Delete the case form records - Execute the schedule job 'Forms Reporting Job' - Validate that the records are removed from the forms reporting table")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod02()
        {

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);



            //at this point we should have only 1 row in the reports table for the assessment 
            var totalReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountReportDataByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalReportRecords);

            //we should have only 1 row in the unlimited row question reports table for the assessment  
            var totalUnlimitedRowsReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountWFTableWithUnlimitedRowsByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalUnlimitedRowsReportRecords);





            //remove all Forms for the Case record
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(_caseId, _documentId))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);



            //at this point we should have only 0 rows in the reports table for the assessment 
            totalReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountReportDataByAssessmentId(_caseFormId);
            Assert.AreEqual(0, totalReportRecords);

            //we should have only 0 rows in the unlimited row question reports table for the assessment  
            totalUnlimitedRowsReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountWFTableWithUnlimitedRowsByAssessmentId(_caseFormId);
            Assert.AreEqual(0, totalUnlimitedRowsReportRecords);


        }

        [TestProperty("JiraIssueID", "CDV6-10242")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the assessment information is stored in the forms reporting table associated with the document - " +
            "Edit the assessment and set a value in one of the questions - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod03()
        {

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);



            //at this point we should have only 1 row in the reports table for the assessment 
            var totalReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountReportDataByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalReportRecords);

            //we should have only 1 row in the unlimited row question reports table for the assessment  
            var totalUnlimitedRowsReportRecords = this.dbHelper.automated_UI_Test_Document_1.CountWFTableWithUnlimitedRowsByAssessmentId(_caseFormId);
            Assert.AreEqual(1, totalUnlimitedRowsReportRecords);



            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFDecimalValue("10.3")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(10.3M, (decimal)allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10243")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Date question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod04()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!")
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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFDateValue("03/09/2020")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(new DateTime(2020, 9, 3), (DateTime)allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10244")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Decimal question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod05()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFDecimalValue("12.3")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(12.3m, (decimal)allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10245")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Lookup question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod06()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .TapWFLookupLookupButton();

            #region Search Person

            var firstName = "SearchDan";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var personName = firstName + " " + lastName;
            var _searchPersonId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(personName)
                .TapSearchButton()
                .SelectResultElement(_searchPersonId.ToString());

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(_searchPersonId, (Guid)allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(personName, (string)allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10246")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Multiple Choice question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod07()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .SelectWFMultipleChoice(1)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(new Guid("e92f3c2d-3f52-e911-a2c5-005056926fe4"), (Guid)allReportsFields["QA-DQ-169"]);
            Assert.AreEqual("Option 1", (string)allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10247")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Multiple Response question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod08()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .SelectWFMultipleResponse(1)
                .SelectWFMultipleResponse(3)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual("ff47d74d-3f52-e911-a2c5-005056926fe4, 0f48d74d-3f52-e911-a2c5-005056926fe4", (string)allReportsFields["QA-DQ-170"]);
            Assert.AreEqual("Day 1, Day 3", (string)allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10248")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Numeric question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod09()
        {
            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFNumericValue("7")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(7, (int)allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10249")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Paragraph question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod10()
        {
            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!")
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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFParagraph("Value 1\nValue 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual("Value 1\nValue 2", (string)allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10250")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Picklist question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod11()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .SelectWFPicklistByText("Budist")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(new Guid("15bd1c69-4252-e911-a2c5-005056926fe4"), (Guid)allReportsFields["QA-DQ-173"]);
            Assert.AreEqual("Budist", (string)allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10251")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF ShortAnswer question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod12()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFShortAnswer("value 1 value 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual("value 1 value 2", (string)allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10252")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Boolean question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod13()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .SelectWFBoolean(true)
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(true, (bool)allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10253")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Time question - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod14()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFTime("12:30")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var userLocalTime = ((DateTime)allReportsFields["QA-DQ-188"]);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(DateTime.Now.Date.AddHours(12).AddMinutes(30), userLocalTime);

            //all fields should be null in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(dbnull, allUnlimitedRowsReportsFields["QA-DQ-246"]);
        }




        [TestProperty("JiraIssueID", "CDV6-10254")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Table With Unlimited Rows question (row 1) - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod15()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertDateBecameInvolvedValue(1, "01/09/2020")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be set in the main unlimited rows reports table
            var allUnlimitedRowsReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(new DateTime(2020, 9, 1), (DateTime)allUnlimitedRowsReportsFields["QA-DQ-245"]);
            Assert.AreEqual(new Guid("3449C399-4BB4-E911-A2C6-005056926FE4"), (Guid)allUnlimitedRowsReportsFields["QA-DQ-246"]);
            Assert.AreEqual("Reason 1", (string)allUnlimitedRowsReportsFields["QA-DQ-246Name"]);
        }

        [TestProperty("JiraIssueID", "CDV6-10255")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
            "Edit the assessment and set a value in the WF Table With Unlimited Rows question (row 1 and row 2) - Execute the schedule job 'Forms Reporting Job' - " +
            "Validate that the reports table contains the information for the saved answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod16()
        {

            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertDateBecameInvolvedValue(1, "01/09/2020")
                .SelectReasonForAssessmentValue(1, "Reason 1")
                .TapWFTableWithUnlimitedRowsNewButton()
                .InsertDateBecameInvolvedValue(2, "02/09/2020")
                .SelectReasonForAssessmentValue(2, "Reason 2")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

            //all fields should be set in the main unlimited rows reports table
            var allUnlimitedRowsReportsFieldsRow1 = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 1);
            Assert.AreEqual(new DateTime(2020, 9, 1), (DateTime)allUnlimitedRowsReportsFieldsRow1["QA-DQ-245"]);
            Assert.AreEqual(new Guid("3449C399-4BB4-E911-A2C6-005056926FE4"), (Guid)allUnlimitedRowsReportsFieldsRow1["QA-DQ-246"]);
            Assert.AreEqual("Reason 1", (string)allUnlimitedRowsReportsFieldsRow1["QA-DQ-246Name"]);

            var allUnlimitedRowsReportsFieldsRow2 = this.dbHelper.automated_UI_Test_Document_1.GetWFTableWithUnlimitedRowsByAssessmentAndRowId(_caseFormId, 2);
            Assert.AreEqual(new DateTime(2020, 9, 2), (DateTime)allUnlimitedRowsReportsFieldsRow2["QA-DQ-245"]);
            Assert.AreEqual(new Guid("3C49C399-4BB4-E911-A2C6-005056926FE4"), (Guid)allUnlimitedRowsReportsFieldsRow2["QA-DQ-246"]);
            Assert.AreEqual("Reason 2", (string)allUnlimitedRowsReportsFieldsRow2["QA-DQ-246Name"]);
        }





        [TestProperty("JiraIssueID", "CDV6-10256")]
        [Description("Case Forms Reporting Table - Create a new Case Form record - Document used has Enable Reporting = Yes - " +
           "Edit the assessment and set a value in the WF Date question - Execute the schedule job 'Forms Reporting Job' - " +
           "Validate that the reports table contains the information for the saved answer - " +
            "Edit the assessment a second time (change the WF Date question value) - Execute the schedule job - " +
            "Validate that the reports table is updated with the most recent answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormReportingTable_UITestMethod17()
        {
            //Edit the assessment
            loginPage
                .GoToLoginPage()
                .Login("CaseFormReportingTableUser1", "Passw0rd_!");

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
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFDateValue("03/09/2020")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            var allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            var dbnull = DBNull.Value;
            Assert.AreEqual(new DateTime(2020, 9, 3), (DateTime)allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);


            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(_caseFormId.ToString())
                .InsertWFDateValue("04/09/2020")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Forms Reporting Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FormsReportingJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FormsReportingJob);


            //validate that the report table is updated with the answer
            allReportsFields = this.dbHelper.automated_UI_Test_Document_1.GetReportDataByAssessmentId(_caseFormId);
            Assert.AreEqual(new DateTime(2020, 9, 4), (DateTime)allReportsFields["QA-DQ-163"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-164"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-168Name"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-169"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-170"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-171"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-172"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-173"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-174"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-186"]);
            Assert.AreEqual(dbnull, allReportsFields["QA-DQ-188"]);

        }



        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

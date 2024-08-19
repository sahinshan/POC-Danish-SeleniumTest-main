using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automated UI Test Document 1.Zip")]
    [DeploymentItem("Files\\D_Flag.Zip")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    public class Case_FormsCase_UITestCases : FunctionalTest
    {
        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _contactReasonId;
        private Guid _systemUserId;
        private Guid _personID;
        private Guid caseid;
        private string caseNumber;
        private Guid _caseStatusId;
        private Guid caseFormID;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _documentId;

        #endregion

        [TestInitialize()]
        public void CaseFormCaseNotes_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion                

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region System User "CaseFormCaseNoteUser1"

                _systemUserName = "CaseFormsCaseUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseFormsCase", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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

                _documentId = commonMethodsDB.CreateDocumentIfNeeded("Automated UI Test Document 1", "Automated UI Test Document 1.Zip");//Import Document

                #endregion

                #region Person

                var firstName = "Automation";
                var lastName = _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

                #endregion

                #region Case

                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                caseid = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

                #endregion

                #region Case Form

                caseFormID = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personID, caseid, startDate);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12830

        [TestProperty("JiraIssueID", "CDV6-13039")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                     "Open the Forms(case) Record" +
                     "Updating Review Date Before Start Date" +
                     "Validating Record is Saved )")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod01()
        {
            var pastDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-2).Day).ToString("dd'/'MM'/'yyyy");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertReviewDate(pastDate)
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateReviewDateField(pastDate);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-13040")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                    "Open the Forms(case) Record" +
                    "Updating Status='Completed'" +
                    "Save the Record and validate Completion Details Section is Visible" +
                    "Validating Record is Saved ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
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
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusField("Complete")
                .ValidateCompletionDetailsSectionVisibility(true);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-13076")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                    "Open the Forms(case) Record" +
                    "Updating Status='Not Initialized'" +
                    "Validate User Is able update the record when status is Not Initialized" +
                    "Validating Record is Saved )")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Not Started")
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusField("Not Started");


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

            var CaseformRecordFields = dbHelper.caseForm.GetCaseFormByID(caseformIDs[0], "Inactive");

            Assert.AreEqual(false, CaseformRecordFields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13085")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                   "Open the Forms(case) Record" +
                   "Updating from  Status='Inprogress' to  Status='Approved'" +
                   "Validate User Is able update the record when status is Approved" +
                   "Validate SignOff By Field and SignOff Date" +
                   "Validating Record is Saved )")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod04()
        {
            #region System User (Signed Off By)

            var signedOffByUserName = "CDV6_13085_User" + _currentDateSuffix;
            Guid signedOffByUserId = commonMethodsDB.CreateSystemUserRecord(signedOffByUserName, "CDV6_13085_User", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Approved")
                .TapSignedOffByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(signedOffByUserName)
                .TapSearchButton()
                .SelectResultElement(signedOffByUserId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertSignedOffDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TapSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateSignOffDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateStatusField("Approved");

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);
            var CaseformRecordFields = dbHelper.caseForm.GetCaseFormByID(caseformIDs[0], "SignedOffById");

            Assert.AreEqual(signedOffByUserId.ToString(), CaseformRecordFields["signedoffbyid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13089")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                   "Open the Forms(case) Record" +
                   "Updating from  Status='Inprogress' to  Status='Closed'" +
                   "Validate User Is able update the record when status is Closed" +
                   "Validating Record is Saved " +
                   "Validate user is not Able to Update any fields in Form After record saved with Status=Closed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod05()
        {
            #region System User (Signed Off By)

            var signedOffByUserName = "CDV6_13085_User" + _currentDateSuffix;
            Guid signedOffByUserId = commonMethodsDB.CreateSystemUserRecord(signedOffByUserName, "CDV6_13085_User", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Closed")
                .TapSignedOffByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(signedOffByUserName)
                .TapSearchButton()
                .SelectResultElement(signedOffByUserId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertSignedOffDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TapSaveButton()
                .WaitForCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);
            var CaseformRecordFields = dbHelper.caseForm.GetCaseFormByID(caseformIDs[0], "SignedOffById", "Inactive", "AssessmentStatusId");

            Assert.AreEqual(signedOffByUserId.ToString(), CaseformRecordFields["signedoffbyid"]);
            Assert.AreEqual(true, CaseformRecordFields["inactive"]);
            Assert.AreEqual(3, CaseformRecordFields["assessmentstatusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13090")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                  "Open the Forms(case) Record" +
                  "Updating from  Status='Inprogress' to  Status='Cancelled'" +
                  "Validate User Is able update the record when status is Cancelled" +
                  "Validating Record is Saved " +
                  "Validate user is not Able to Update any fields in Form After record saved with Status=Cancelled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod06()
        {
            #region Form Cancellation Reason

            var FormCancellationReasonName = "Test Reason";
            var FormCancellationReasonId = commonMethodsDB.CreateFormCancellationReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, FormCancellationReasonName, new DateTime(2022, 1, 1), null, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Cancelled")
                .TapCancelledReasonButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(FormCancellationReasonName)
                .TapSearchButton()
                .SelectResultElement(FormCancellationReasonId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapSaveButton();

            System.Threading.Thread.Sleep(2000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);
            var CaseformRecordFields = dbHelper.caseForm.GetCaseFormByID(caseformIDs[0], "FormCancellationReasonId", "Inactive", "AssessmentStatusId");


            Assert.AreEqual(FormCancellationReasonId.ToString(), CaseformRecordFields["formcancellationreasonid"].ToString());
            Assert.AreEqual(true, CaseformRecordFields["inactive"]);
            Assert.AreEqual(5, CaseformRecordFields["assessmentstatusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13091")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                  "Open the Forms(case) Record" +
                  "tap New Record button in case action/outcome frame " +
                  "Validate User is redirected to ActionOutcome Page" +
                  "Enter all mandatory fields and click Save option " +
                  "Validate Action Outcome Record is Created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod07()
        {
            #region Case Form Outcome Type

            var outcomeTypeName = "Outcome Type_2";
            var outcomeTypeId = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, outcomeTypeName, new DateTime(2022, 1, 1), null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .WaitForActionOutcomePageToLoad()
                .TapNewRecordButton();

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(outcomeTypeName)
               .TapSearchButton()
               .SelectResultElement(outcomeTypeId.ToString());

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .InsertOutcomeDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

            var caseformoutcomIDs = dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID);
            Assert.AreEqual(1, caseformoutcomIDs.Count);

            var CaseformoutcomeIdRecordFields = dbHelper.caseFormOutcome.GetCaseFormOutcomeByID(caseformoutcomIDs[0], "CaseFormOutcomeTypeId", "OutcomeDate");


            Assert.AreEqual(outcomeTypeId.ToString(), CaseformoutcomeIdRecordFields["caseformoutcometypeid"]);
            Assert.AreEqual(DateTime.Now.Date, CaseformoutcomeIdRecordFields["outcomedate"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13093")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                     "create case action outcome Record with DB Helper" +
                     "Open the Forms(case) Record" +
                     "tap on Existing Record  in case action/outcome frame " +
                      "Validate User is redirected to ActionOutcome Page" +
                      "update any value in mandatory fields and click Save option " +
                       "Validate Action Outcome Record is Updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod08()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #region Case Form Outcome Type

            var outcomeTypeName1 = "Outcome Type_1";
            var outcomeTypeId1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, outcomeTypeName1, new DateTime(2022, 1, 1), null);

            var outcomeTypeName2 = "Outcome Type_2";
            var outcomeTypeId2 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, outcomeTypeName2, new DateTime(2022, 1, 1), null);

            #endregion

            #region Case Form Action Outcome

            Guid ActionOutcomeRecord = dbHelper.caseFormOutcome.CreateActionOutcomeRecord(_careDirectorQA_TeamId, _personID, outcomeTypeId1, startDate, caseid, caseFormID);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .WaitForActionOutcomePageToLoad()
                .OpenOtcomeRecord(ActionOutcomeRecord.ToString());

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(outcomeTypeName2)
               .TapSearchButton()
               .SelectResultElement(outcomeTypeId2.ToString());

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad();

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

            var caseformoutcomIDs = dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID);
            Assert.AreEqual(1, caseformoutcomIDs.Count);

            var CaseformoutcomeIdRecordFields = dbHelper.caseFormOutcome.GetCaseFormOutcomeByID(caseformoutcomIDs[0], "CaseFormOutcomeTypeId");


            Assert.AreEqual(outcomeTypeId2.ToString(), CaseformoutcomeIdRecordFields["caseformoutcometypeid"]);


        }

        [TestProperty("JiraIssueID", "CDV6-13107")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                     "create case action outcome Record with DB Helper" +
                     "Open the Forms(case) Record" +
                     "tap on Existing Record  in case action/outcome frame " +
                      "Validate User is redirected to ActionOutcome Page" +
                      "click Menu and select Audit from sub menu" +
                       "Validate Action Outcome record creation History is present in Audit List")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod09()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #region Case Form Outcome Type

            var outcomeTypeName1 = "Outcome Type_1";
            var outcomeTypeId1 = commonMethodsDB.CreateCaseFormOutcomeType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, outcomeTypeName1, new DateTime(2022, 1, 1), null);

            #endregion

            #region Case Form Action Outcome

            Guid ActionOutcomeRecord = dbHelper.caseFormOutcome.CreateActionOutcomeRecord(_careDirectorQA_TeamId, _personID, outcomeTypeId1, startDate, caseid, caseFormID);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .WaitForActionOutcomePageToLoad()
                .OpenOtcomeRecord(ActionOutcomeRecord.ToString());

            formActionOutcomePage
                .WaitForFormActionOutcomePageToLoad()
                .NavigateToAuditPage();


            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(1, caseformIDs.Count);

            var caseformoutcomIDs = dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(caseFormID);
            Assert.AreEqual(1, caseformoutcomIDs.Count);

            auditListPage
               .WaitForAuditListPageToLoad("caseformoutcome");

            System.Threading.Thread.Sleep(2000);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1",
                IsGeneralAuditSearch = false,
                Operation = 1,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                ParentId = caseformoutcomIDs[0].ToString(),
                ParentTypeName = "CaseFormOutcome",
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Create", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("System Administrator", auditResponseData.GridData[0].cols[1].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-13108")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                    "Open the Forms(case) Record" +
                    "Click on Menu and Related Items and AssessmentFactor " +
                     "Validate User is redirected to AssessmentFactor Page" +
                     "click New Record Button and Fill all Mandatory Fields and hit Save Button" +
                      "Validate Assessment Factor Record is Created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod10()
        {
            #region Assessment Factor Type

            var assessmentFactorTypeName = "CDV6_13108_Assessment_Factor_Type";
            var assessmentFactorTypeId = commonMethodsDB.CreateAssessmentFactorType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, assessmentFactorTypeName, new DateTime(2022, 1, 1), null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAssessmentFactorsArea();

            caseFormAssessmentFactorPage
                .WaitForCaseFormAssessmentFactorPageToLoad()
                .ClickNewRecordButton();

            caseFormAssessmentFactorRecordPage
                .WaitForCaseFormAssessmentFactorRecordPageToLoad()
                .ClickFactorLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(assessmentFactorTypeName)
                .TapSearchButton()
                .SelectResultElement(assessmentFactorTypeId.ToString());

            caseFormAssessmentFactorRecordPage
                .WaitForCaseFormAssessmentFactorRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseFormAssessmentFactorPage
                .WaitForCaseFormAssessmentFactorPageToLoad();

            System.Threading.Thread.Sleep(2000);

            //validating  Assessment Factor Record is created
            var caseformAssessmentFactor = dbHelper.caseFormAssessmentFactor.GetByCaseID(caseid);
            Assert.AreEqual(1, caseformAssessmentFactor.Count);

            //deleting  Assessment Factor Record
            foreach (var assessmentFactorID in dbHelper.caseFormAssessmentFactor.GetByCaseID(caseid))
            {
                dbHelper.caseFormAssessmentFactor.DeleteCaseFormAssessmentFactor(assessmentFactorID);
            }



        }


        [TestProperty("JiraIssueID", "CDV6-13109")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                    "Open the Forms(case) Record" +
                    "Click on Menu and Related Items and Members " +
                     "Validate User is redirected to Member Page" +
                     "click New Record Button and Fill all Mandatory Fields and hit Save Button" +
                      "Validate Member Record is Created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod11()
        {
            #region Person (Member)

            var _memberId = commonMethodsDB.CreatePersonRecord("Person_Member", _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var memberName = "Person_Member " + _currentDateSuffix;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormMembersArea();

            caseFormMembersPage
                .WaitForCaseFormMembersPageToLoad()
                .ClickNewRecordButton();

            caseFormMembersRecordPage
                .WaitForCaseFormMembersRecordPageToLoad()
                .ClickMemberLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("My Team Records")
                .TypeSearchQuery(memberName)
                .TapSearchButton()
                .SelectResultElement(_memberId.ToString());

            caseFormMembersRecordPage
                 .WaitForCaseFormMembersRecordPageToLoad()
                 .InsertStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            caseFormMembersPage
                .WaitForCaseFormMembersPageToLoad();

            //validating Member Record is created
            var caseformMemberID = dbHelper.caseFormMember.GetByCaseID(caseid);
            Assert.AreEqual(1, caseformMemberID.Count);



            //deleting Member Record 
            foreach (var MemberID in dbHelper.caseFormMember.GetByCaseID(caseid))
            {
                dbHelper.caseFormMember.DeleteCaseFormMember(MemberID);
            }



        }

        [TestProperty("JiraIssueID", "CDV6-13110")]
        [Description("Creating Case Forms(case) Record with DB Helper" +
                     "Open the Forms(case) Record" +
                     "select the record and hit Delete Button " +
                     "Validate Case Form Record is Deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormsCase_UITestMethod12()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .SelectCaseFormRecord(caseFormID.ToString())
                .TapDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            caseCasesFormPage
               .WaitForCaseCaseFormPageToLoad();

            var caseformIDs = dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, _documentId);
            Assert.AreEqual(0, caseformIDs.Count);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion

    }
}


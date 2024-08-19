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
    [DeploymentItem("Files\\Document.docx"), DeploymentItem("Files\\Document2.docx"), DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automated UI Test Document 1.Zip")]
    [DeploymentItem("Files\\D_Flag.Zip")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    public class Case_CaseFormAttachments_UITestCases : FunctionalTest
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
        private Guid _caseId;
        private string _caseNumber;
        private Guid _caseStatusId;
        private Guid _caseFormId;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _documentId;
        private Guid _attachDocumentTypeId;
        private Guid _attachDocumentSubTypeId;

        #endregion

        [TestInitialize()]
        public void Case_CaseFormAttachments_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                if (!dbHelper.businessUnit.GetByName("CareDirector QA").Any())
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                if (!dbHelper.team.GetTeamIdByName("CareDirector QA").Any())
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Language

                if (!dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any())
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                if (_languageId == Guid.Empty)
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("English").Any())
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Case_Form_Attachment_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Case Form Attachement", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

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

                #region Case & Case Form

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 06, 29), new DateTime(2021, 11, 11), 20, "Care Form Record to Check Bulk Attachment");
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

                // Create a new case form
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_careDirectorQA_TeamId, _personID, firstName + lastName, _systemUserId, _caseId, _caseNumber.ToString(), _documentId, "Automated UI Test Document 1", 1, new DateTime(2021, 02, 06), null, new DateTime(2021, 06, 06));

                #endregion

                #region Attach Document Type

                if (!dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan").Any())
                    dbHelper.attachDocumentType.CreateAttachDocumentType(new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4"), _careDirectorQA_TeamId, "Bladder Scan", new DateTime(2020, 1, 1));
                _attachDocumentTypeId = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];

                #endregion

                #region Attach Document Sub Type

                if (!dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results").Any())
                    dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"), _careDirectorQA_TeamId, "Bladder Scan Results", new DateTime(2020, 1, 1), _attachDocumentTypeId);
                _attachDocumentSubTypeId = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-10647

        [TestProperty("JiraIssueID", "CDV6-11408")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - validate that the bulk upload popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-11409")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document Type and Document SubType ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(true)
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelVisibility(true)
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "CDV6-11410")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document SubType ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(true)
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "CDV6-11411")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document sub type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document SubType ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(true)
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-11412")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a file to be uploaded - Validate that the file is displayed in the popup with name, size, upload button and cancel button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod05()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)

                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB");

        }

        [TestProperty("JiraIssueID", "CDV6-11413")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - Validate that both file are displayed in the popup with name, size, upload button and cancel button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod06()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)

                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, true)
                .ValidateAttachedFileSizeVisibility(2, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, true)

                .ValidateAttachedFileNameText(2, "Document2.docx")
                .ValidateAttachedFileSizeText(2, "11.86 KB");

        }

        [TestProperty("JiraIssueID", "CDV6-11414")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - For the last file click on the Cancel Upload button - Validate that the file is removed from the list")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod07()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)
                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, true)
                .ValidateAttachedFileSizeVisibility(2, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, true)
                .ValidateAttachedFileNameText(2, "Document2.docx")
                .ValidateAttachedFileSizeText(2, "11.86 KB")

                .ClickAttachedFileCancelUploadButton(2)

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)
                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, false)
                .ValidateAttachedFileSizeVisibility(2, false)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false);

        }

        [TestProperty("JiraIssueID", "CDV6-11415")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - For the first file click on the Cancel Upload button - Validate that the file is removed from the list")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)
                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, true)
                .ValidateAttachedFileSizeVisibility(2, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, true)
                .ValidateAttachedFileNameText(2, "Document2.docx")
                .ValidateAttachedFileSizeText(2, "11.86 KB")

                .ClickAttachedFileCancelUploadButton(1)

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)
                .ValidateAttachedFileNameText(1, "Document2.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, false)
                .ValidateAttachedFileSizeVisibility(2, false)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false);

        }

        [TestProperty("JiraIssueID", "CDV6-11416")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - Click on the main cancel upload button - Validate that all files are removed from the list")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)
                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB")

                .ValidateAttachedFileNameVisibility(2, true)
                .ValidateAttachedFileSizeVisibility(2, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, true)
                .ValidateAttachedFileNameText(2, "Document2.docx")
                .ValidateAttachedFileSizeText(2, "11.86 KB")

                .ClickCancelUploadButton()

                .ValidateAttachedFileNameVisibility(1, false)
                .ValidateAttachedFileSizeVisibility(1, false)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, false)

                .ValidateAttachedFileNameVisibility(2, false)
                .ValidateAttachedFileSizeVisibility(2, false)
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false);

        }

        [TestProperty("JiraIssueID", "CDV6-11417")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document type - select a document sub type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that no error message is displayed - Validate that a new person attachment record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod15()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(false)

                .ClickCloseButton();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var attachmentrecords = dbHelper.caseFormAttachment.GetByCaseFormID(_caseFormId);
            Assert.AreEqual(1, attachmentrecords.Count);

            var fields = dbHelper.caseFormAttachment.GetByID(attachmentrecords[0], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(_attachDocumentTypeId.ToString(), fields["documenttypeid"]);
            Assert.AreEqual(_attachDocumentSubTypeId.ToString(), fields["documentsubtypeid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);

        }

        [TestProperty("JiraIssueID", "CDV6-11418")]
        [Description("Open a case form record - Navigate to the case form attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
           "Select a document type - select a document sub type - Select two files to be uploaded - Click on the Start Upload button - " +
           "Validate that no error message is displayed - Validate that two new person attachment records are created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void BulkLoadCaseFormAttachments_UITestMethod16()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
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
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(_attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(_attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(false)
                .ClickCloseButton();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var attachmentrecords = dbHelper.caseFormAttachment.GetByCaseFormID(_caseFormId);
            Assert.AreEqual(2, attachmentrecords.Count);

            var fields = dbHelper.caseFormAttachment.GetByID(attachmentrecords[0], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(_attachDocumentTypeId.ToString(), fields["documenttypeid"]);
            Assert.AreEqual(_attachDocumentSubTypeId.ToString(), fields["documentsubtypeid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);

            fields = dbHelper.caseFormAttachment.GetByID(attachmentrecords[1], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(_attachDocumentTypeId.ToString(), fields["documenttypeid"]);
            Assert.AreEqual(_attachDocumentSubTypeId.ToString(), fields["documentsubtypeid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8419

        [TestProperty("JiraIssueID", "CDV6-11419")]
        [Description("Open a Case Form Attachment (For Case) record (case form attachment Business Object should contain multiple custom fields that, for this record, have data set in them) - " +
            "Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select a Person record as the destination record - Confirm the clone operation - Validate that the Case Form Attachment record is properly cloned")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormAttachments_Cloning_UITestMethod01()
        {
            var personID = new Guid("1f5ebadb-0a81-4978-aaa5-9d1afb30b75b"); //Adele Reyes
            var caseID = new Guid("d389aeba-f63a-e911-a2c5-005056926fe4"); //QA-CAS-000001-00417307
            var caseNumber = "QA-CAS-000001-00417307";
            var caseFormID = new Guid("0bc5728b-b6e0-eb11-a325-005056926fe4"); //Automated UI Test Document 1
            var caseFormAttachmentID = new Guid("b883b1aa-b6e0-eb11-a325-005056926fe4"); //Adele Reyes Attachment (Case Form) 001
            var linkedCaseFormAttachmentID = new Guid("67ec04b3-b6e0-eb11-a325-005056926fe4"); //Adele Reyes Attachment (Case Form) 002



            //remove all person attachments from the person linked to the case form
            foreach (var personAttachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personAttachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .OpenCaseFormAttachmentsRecord(caseFormAttachmentID.ToString());

            caseFormAttachmentRecordPage
                .WaitForCaseFormAttachmentRecordPageToLoad("Adele Reyes Attachment (Case Form) 001")
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .InsertStartDate("05/07/2021")
                .SelectRecord(personID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();


            var records = dbHelper.personAttachment.GetByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.personAttachment.GetByID(records[0],
                "personid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid", "availableoncitizenportal", "createdonportal",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");


            var attachmentDate = new DateTime(2021, 7, 4, 23, 0, 0, DateTimeKind.Utc);
            var documentType = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var documentSubType = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results    
            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            var CloneTestingDateAndTime = new DateTime(2021, 7, 5, 10, 15, 0, DateTimeKind.Utc);
            var CloneTestingDate = new DateTime(2021, 7, 8, 0, 0, 0, DateTimeKind.Utc);


            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual("Adele Reyes Attachment (Case Form) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(false, fields["availableoncitizenportal"]);
            Assert.AreEqual(false, fields["createdonportal"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(caseFormAttachmentID, fields["clonedfromid"]);

            Assert.AreEqual(linkedCaseFormAttachmentID, fields["qa_cloningtestborid"]);
            Assert.AreEqual(CloneTestingDateAndTime.ToLocalTime(), ((DateTime)fields["qa_clonetestingdateandtime"]).ToLocalTime());
            Assert.AreEqual(12.91m, fields["qa_clonetestingdecimal"]);
            Assert.AreEqual("somemail@themail.com", fields["qa_clonetestingemail"]);
            Assert.AreEqual("line 1\nline 2", fields["qa_clonetestingmldt"]);
            Assert.AreEqual(21.23m, fields["qa_clonetestingmoney"]);
            Assert.AreEqual("l1\nl2", fields["qa_clonetestingmultilinetextbox"]);
            Assert.AreEqual(56, fields["qa_clonetestingdnumeric"]);
            Assert.AreEqual(CloneTestingDate.ToLocalTime(), ((DateTime)fields["qa_cloningtestdate"]).ToLocalTime());
            Assert.AreEqual("965478284", fields["qa_clonetestingphone"]);
            Assert.AreEqual(3, fields["qa_clonetestingpickliistid"]);
            Assert.AreEqual("line 1 text", fields["qa_clonetestingsingleline"]);
            //Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11420")]
        [Description("Open a Case Form Attachment (For Case) record (case form attachment Business Object should contain multiple custom fields that, for this record, have data set in them) - " +
            "Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select Multiple Person record as the destination records - Confirm the clone operation - Validate that the Case Form Attachment record is properly cloned into each selected person record")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseFormAttachments_Cloning_UITestMethod02()
        {
            var personID = new Guid("1f5ebadb-0a81-4978-aaa5-9d1afb30b75b"); //Adele Reyes
            var relatedPersonID = new Guid("0e170c04-2483-483c-9af1-e6f4e28b4c55"); //Emmett Watts
            var caseID = new Guid("d389aeba-f63a-e911-a2c5-005056926fe4"); //QA-CAS-000001-00417307
            var caseNumber = "QA-CAS-000001-00417307";
            var caseFormID = new Guid("0bc5728b-b6e0-eb11-a325-005056926fe4"); //Automated UI Test Document 1
            var caseFormAttachmentID = new Guid("b883b1aa-b6e0-eb11-a325-005056926fe4"); //Adele Reyes Attachment (Case Form) 001
            var linkedCaseFormAttachmentID = new Guid("67ec04b3-b6e0-eb11-a325-005056926fe4"); //Adele Reyes Attachment (Case Form) 002


            //remove all person attachments from the person linked to the case form
            foreach (var personAttachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personAttachmentid);

            //remove all person attachments from the related person (Brother of Adele Reyes)
            foreach (var personAttachmentid in dbHelper.personAttachment.GetByPersonID(relatedPersonID))
                dbHelper.personAttachment.DeletePersonAttachment(personAttachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAttachmentsArea();

            caseFormAttachmentsPage
                .WaitForCaseFormAttachmentsPageToLoad()
                .OpenCaseFormAttachmentsRecord(caseFormAttachmentID.ToString());

            caseFormAttachmentRecordPage
                .WaitForCaseFormAttachmentRecordPageToLoad("Adele Reyes Attachment (Case Form) 001")
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .InsertStartDate("05/07/2021")
                .SelectRecord(personID.ToString())
                .SelectRecord(relatedPersonID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();


            var records = dbHelper.personAttachment.GetByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.personAttachment.GetByID(records[0],
                "personid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid", "availableoncitizenportal", "createdonportal",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");


            var attachmentDate = new DateTime(2021, 7, 4, 23, 0, 0, DateTimeKind.Utc);
            var documentType = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var documentSubType = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results    
            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            var CloneTestingDateAndTime = new DateTime(2021, 7, 5, 10, 15, 0, DateTimeKind.Utc);
            var CloneTestingDate = new DateTime(2021, 7, 8, 0, 0, 0, DateTimeKind.Utc);


            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual("Adele Reyes Attachment (Case Form) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(false, fields["availableoncitizenportal"]);
            Assert.AreEqual(false, fields["createdonportal"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(caseFormAttachmentID, fields["clonedfromid"]);

            Assert.AreEqual(linkedCaseFormAttachmentID, fields["qa_cloningtestborid"]);
            Assert.AreEqual(CloneTestingDateAndTime.ToLocalTime(), ((DateTime)fields["qa_clonetestingdateandtime"]).ToLocalTime());
            Assert.AreEqual(12.91m, fields["qa_clonetestingdecimal"]);
            Assert.AreEqual("somemail@themail.com", fields["qa_clonetestingemail"]);
            Assert.AreEqual("line 1\nline 2", fields["qa_clonetestingmldt"]);
            Assert.AreEqual(21.23m, fields["qa_clonetestingmoney"]);
            Assert.AreEqual("l1\nl2", fields["qa_clonetestingmultilinetextbox"]);
            Assert.AreEqual(56, fields["qa_clonetestingdnumeric"]);
            Assert.AreEqual(CloneTestingDate.ToLocalTime(), ((DateTime)fields["qa_cloningtestdate"]).ToLocalTime());
            Assert.AreEqual("965478284", fields["qa_clonetestingphone"]);
            Assert.AreEqual(3, fields["qa_clonetestingpickliistid"]);
            Assert.AreEqual("line 1 text", fields["qa_clonetestingsingleline"]);
            //Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);



            records = dbHelper.personAttachment.GetByPersonID(relatedPersonID);
            Assert.AreEqual(1, records.Count);

            fields = dbHelper.personAttachment.GetByID(records[0],
                "personid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid", "availableoncitizenportal", "createdonportal",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");

            Assert.AreEqual(relatedPersonID, fields["personid"]);
            Assert.AreEqual("Adele Reyes Attachment (Case Form) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(false, fields["availableoncitizenportal"]);
            Assert.AreEqual(false, fields["createdonportal"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(caseFormAttachmentID, fields["clonedfromid"]);

            Assert.AreEqual(linkedCaseFormAttachmentID, fields["qa_cloningtestborid"]);
            Assert.AreEqual(CloneTestingDateAndTime.ToLocalTime(), ((DateTime)fields["qa_clonetestingdateandtime"]).ToLocalTime());
            Assert.AreEqual(12.91m, fields["qa_clonetestingdecimal"]);
            Assert.AreEqual("somemail@themail.com", fields["qa_clonetestingemail"]);
            Assert.AreEqual("line 1\nline 2", fields["qa_clonetestingmldt"]);
            Assert.AreEqual(21.23m, fields["qa_clonetestingmoney"]);
            Assert.AreEqual("l1\nl2", fields["qa_clonetestingmultilinetextbox"]);
            Assert.AreEqual(56, fields["qa_clonetestingdnumeric"]);
            Assert.AreEqual(CloneTestingDate.ToLocalTime(), ((DateTime)fields["qa_cloningtestdate"]).ToLocalTime());
            Assert.AreEqual("965478284", fields["qa_clonetestingphone"]);
            Assert.AreEqual(3, fields["qa_clonetestingpickliistid"]);
            Assert.AreEqual("line 1 text", fields["qa_clonetestingsingleline"]);
            // Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);
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

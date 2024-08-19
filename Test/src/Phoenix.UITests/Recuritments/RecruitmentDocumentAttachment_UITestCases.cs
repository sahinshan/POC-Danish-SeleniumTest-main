using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class RecruitmentDocumentAttachment_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private string tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _anotherTeamId;
        private Guid _defaultLoginUserID;
        private Guid _applicantId;
        private string _teamName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _loginUsername = "Login_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _staffRecruitmentItemId;
        private Guid _RecruitmentDocumentAttachmentId;
        private Guid _RecruitmentDocumentAttachmentId2;
        private string _staffRecruitmentItemName;
        private string _RecruitmentDocumentAttachmentsName = "Automation_";

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CareProviders";
                _careProviders_TeamId = commonMethodsDB.CreateTeam(_teamName, null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_loginUsername, "Login_", "Automation_User", "Login Automation User", "Passw0rd_!", "Login_Automation_User@somemail.com", "Login_Automation_User@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Staff Recruitment Item

                _staffRecruitmentItemName = "CDV6_21092_Item_1_" + currentDateTime;
                _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-21092

        [TestProperty("JiraIssueID", "ACC-3496")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Create Recruitment Documents and fill different diffrent fields and verify Recruitment Documents Satus after saving record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Document Attachments")]
        public void RecruitmentDocumentAttachment_UITestCases001()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6_21091_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding");

            #endregion

            #region Step 3 & 4

            recruitmentDocumentsRecordPage
                .NavigateToAttachmentsPage();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ClickCreateNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad();

            #endregion

            #region Step 5

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveButton()
                .ValidateNameFieldByNotificationMessageText("Please fill out this field.")
                .ValidateFileFieldByNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 6

            recruitmentDocumentAttachmentRecordPage
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateResponsibleTeamName(_teamName)
                .InsertName(_RecruitmentDocumentAttachmentsName + currentDateTime)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad();

            #endregion

            #region Step 7

            System.Threading.Thread.Sleep(3000);
            _RecruitmentDocumentAttachmentId = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(_RecruitmentDocumentAttachmentsName + currentDateTime).First();

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 8

            recruitmentDocumentAttachmentPage
                .ClickCreateNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            var AttachementName = _RecruitmentDocumentAttachmentsName + DateTime.Now.ToString("yyyyMMddHHmmss");

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachementName)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document2.txt")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad();

            Thread.Sleep(3000);
            _RecruitmentDocumentAttachmentId2 = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(AttachementName).First();

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId2.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3497")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Create Recruitment Documents and fill different diffrent fields and verify Recruitment Documents Satus after saving record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Document Attachments")]
        public void RecruitmentDocumentAttachment_UITestCases002()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6-21163_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);

            #endregion

            #region Create Another Team

            _anotherTeamId = commonMethodsDB.CreateTeam("CDV6-21163", null, _careProviders_BusinessUnitId, "90400", "CCDV6-21163@careworkstempmail.com", "CDV6-21163", "020 123456");

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding");

            recruitmentDocumentsRecordPage
                .NavigateToAttachmentsPage();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ClickCreateNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            var AttachementName = _RecruitmentDocumentAttachmentsName + currentDateTime;

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachementName)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            _RecruitmentDocumentAttachmentId = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(AttachementName).First();

            recruitmentDocumentAttachmentRecordPage
                .ClickBackButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 9

            recruitmentDocumentAttachmentPage
                .OpenRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            AttachementName = _RecruitmentDocumentAttachmentsName + DateTime.Now.ToString("yyyyMMddHHmmss");

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachementName)
                .ClickUploadNewFileButton()
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document2.txt")
                .ClickUploadButton()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 11

            recruitmentDocumentAttachmentPage
                .SelectRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Excel")
                .ClickExportButton(); System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "RecruitmentDocumentAttachments.xlsx");
            Assert.IsTrue(fileExists);

            exportDataPopup
                .TapCloseButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ClickAssignToAnotherTeamButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPrimarySupportToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("CDV6-21163").TapSearchButton().SelectResultElement(_anotherTeamId.ToString()).ClickAssignOkButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .OpenRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .ValidateResponsibleTeamName("CDV6-21163")
                .ClickBackButton();

            #endregion

            #region Step 10

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .SelectRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString())
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            recruitmentDocumentAttachmentPage
                .WaitForRecruitmentDocumentAttachmentPageToLoad()
                .ValidateRecruitmentDocumentAttachmentRecordIsNotPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-21084

        [TestProperty("JiraIssueID", "ACC-3498")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Create Recruitment Documents and fill different fields and verify Recruitment Documents Status after saving record." +
                     "The sub grid 'Recruitment Document Attachments' should be present for a Document." +
                     "The Related item - Attachment should be present" +
                     "A New Recruitment Document Attachment page should get displayed" +
                     "An alert message should get displayed. And the record should not get saved." +
                     "The Record should get displayed and the title should get filled with the Name entered." +
                     "Newly added attachment should get displayed in the sub grid." +
                     "Should able to add more than one attachments for a same document.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Document Attachments")]
        public void RecruitmentDocumentAttachment_UITestCases003()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6_21083_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_03", _careProviders_TeamId);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding");

            Guid complianceId = dbHelper.compliance.GetComplianceByRegardingId(_applicantId).First();

            #endregion

            #region Step 2, 3 and 4

            recruitmentDocumentsRecordPage
                .ClickRelatedItemsLeftMenu()
                .ValidateAttachmentLeftSubMenuItemAvailable(true)
                .ClickMenuButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentAttachementGridVisible(true)
                .ClickCreateNewRecruitmentDocumentSubRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad();

            #endregion

            #region Step 5

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveButton()
                .ValidateNameFieldByNotificationMessageText("Please fill out this field.")
                .ValidateFileFieldByNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 6

            recruitmentDocumentAttachmentRecordPage
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateResponsibleTeamName(_teamName)
                .InsertName(_RecruitmentDocumentAttachmentsName + currentDateTime)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .ValidateRecruitmentDocumentAttachmentRecordPageTitle(_RecruitmentDocumentAttachmentsName + currentDateTime);

            #endregion

            #region Step 7

            _RecruitmentDocumentAttachmentId = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(_RecruitmentDocumentAttachmentsName + currentDateTime).First();

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 8

            recruitmentDocumentAttachmentPage
                .ClickCreateNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            string AttachmentRecord2Name = _RecruitmentDocumentAttachmentsName + DateTime.Now.ToString("yyyyMMddHHmmss");

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachmentRecord2Name)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document2.txt")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .ValidateRecruitmentDocumentAttachmentRecordPageTitle(AttachmentRecord2Name);

            _RecruitmentDocumentAttachmentId2 = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(AttachmentRecord2Name).First();

            recruitmentDocumentAttachmentRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId2.ToString());

            int attachments = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentByComplianceItemId(complianceId).Count;
            Assert.AreEqual(2, attachments);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-21090

        [TestProperty("JiraIssueID", "ACC-3499")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application. Create a Recruitment Document." +
             "user should be allowed to edit the attachment, change the name and the file and Save." +
             "The attachment should get deleted." +
             "User should able to export the details and should able to assign to different team.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Document Attachments")]
        public void RecruitmentDocumentAttachment_UITestCases004()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6-21805_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_04", _careProviders_TeamId);

            #endregion

            #region Create Another Team
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            _anotherTeamId = commonMethodsDB.CreateTeam("CDV6-21805", null, _careProviders_BusinessUnitId, "90499", "CCDV6-21805@careworkstempmail.com", "CDV6-21805", "020 123456");

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ClickCreateNewRecruitmentDocumentSubRecordButton();

            string AttachmentName = _RecruitmentDocumentAttachmentsName + currentDateTime;

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachmentName)
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            _RecruitmentDocumentAttachmentId = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(AttachmentName).First();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .ClickBackButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 9

            recruitmentDocumentsRecordPage
                .OpenRecruitmentDocumentPageSubRecord(_RecruitmentDocumentAttachmentId.ToString());

            AttachmentName = _RecruitmentDocumentAttachmentsName + DateTime.Now.ToString("yyyyMMddHHmmss");

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .InsertName(AttachmentName)
                .ClickUploadNewFileButton()
                .UploadRecruitmentDocumentAttachmentFile(TestContext.DeploymentDirectory + "\\Document2.txt")
                .ClickUploadButton()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            _RecruitmentDocumentAttachmentId = dbHelper.complianceItemAttachment.GetComplianceItemAttachmentIdByTitle(AttachmentName).First();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

            #region Step 11

            recruitmentDocumentsRecordPage
                .SelectRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Excel")
                .ClickExportButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "RecruitmentDocumentAttachments.xlsx");
            Assert.IsTrue(fileExists);

            exportDataPopup
                .TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ClickAssignButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPrimarySupportToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CDV6-21805")
                .TapSearchButton()
                .SelectResultElement(_anotherTeamId.ToString())
                .ClickAssignOkButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .OpenRecruitmentDocumentPageSubRecord(_RecruitmentDocumentAttachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("complianceitemattachment")
                .ClickOnExpandIcon();

            recruitmentDocumentAttachmentRecordPage
                .WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
                .ValidateResponsibleTeamName("CDV6-21805")
                .ClickBackButton();

            #endregion

            #region Step 10

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .SelectRecruitmentDocumentAttachmentsRecord(_RecruitmentDocumentAttachmentId.ToString())
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            System.Threading.Thread.Sleep(2000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
                .ValidateRecruitmentDocumentPageSubRecordIsNotPresent(_RecruitmentDocumentAttachmentId.ToString());

            #endregion

        }

        #endregion

    }
}
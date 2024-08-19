using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("Files\\WF Workflow Testing - Is On Demand Process - WF 1.Zip"),
    DeploymentItem("Files\\WF Workflow Testing - Is On Demand Process - WF 2.Zip"),
    DeploymentItem("Files\\WF Workflow Testing - Is On Demand Process - WF 3.Zip"),
    DeploymentItem("chromedriver.exe")]
    public class Person_Email_UITestCases : FunctionalTest
    {

        private string environmentName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private List<Guid> userSecProfiles;
        private Guid _ethnicityId;
        private Guid _personID;
        private string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _personNumber;
        private string _firstName;
        private string _personFullname;
        private Guid _systemUserId;
        private Guid fromId;
        private Guid fromId2;
        private Guid toId;
        private Guid to1Id;
        private string _systemUsername;
        private string _systemUserFullname;
        private string _defaultUserFullname;


        [TestInitialize()]
        public void UITests_SetupTest()
        {
            try
            {
                #region Environment

                environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

                #endregion

                #region Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
                string decodedUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(decodedUsername)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

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
                userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };
                _systemUsername = "PersonEmailUser1";
                _systemUserFullname = "PersonEmail" + " " + "User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PersonEmail", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

                fromId = commonMethodsDB.CreateSystemUserRecord("HealthTest12", "HealthTest", "12", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
                to1Id = commonMethodsDB.CreateSystemUserRecord("CareCoordinatorTestUser", "CareCoordinator", "TestUser", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
                toId = commonMethodsDB.CreateSystemUserRecord("CareCoordinatorTestUser2", "Care Coordinator", "Test User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_11716").Any();
                if (!adminUserExists)
                {
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_11716", "Testing", "CDV6-11716", "Testing CDV6-11716", "Passw0rd_!", "", "", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _businessUnitId, _teamId);
                }

                if (Guid.Empty == fromId2)
                    fromId2 = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_11716").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(fromId2, DateTime.Now.Date);
                #endregion

                #region Ethnicity
                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Person
                _personID = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_17923", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
                _personNumber = dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"].ToString();


                _firstName = "Testing_CDV6_17923";
                _personFullname = _firstName + " " + currentDate;
                _personID = commonMethodsDB.CreatePersonRecord(_firstName, currentDate, _ethnicityId, _teamId);
                _personNumber = (dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]).ToString();
                #endregion

            }

            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Email Attachment https://advancedcsg.atlassian.net/browse/CDV6-3932


        #region View Record

        [TestProperty("JiraIssueID", "CDV6-4058")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - " +
            "Validate that a user is redirected to the email attachments sub page")]
        [TestMethod]
        [DeploymentItem("Files\\Doc2ToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod01()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");
            Guid attachmentID = dbHelper.EmailAttachment.CreateEmailAttachment(_teamId, false, emailID, TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt", "Doc2ToUpload");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            DateTime createdOnField = (DateTime)dbHelper.EmailAttachment.GetEmailAttachmentByID(attachmentID, "createdon")["createdon"];

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .ValidateFileCellText(attachmentID.ToString(), "Doc2ToUpload")
                .ValidateCreatedByCellText(attachmentID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(attachmentID.ToString(), createdOnField.ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss"));

        }

        [TestProperty("JiraIssueID", "CDV6-4059")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
           "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - Open an attachment record - " +
           "Validate that a user is redirected to the email attachment page")]
        [TestMethod]
        [DeploymentItem("Files\\Doc2ToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod02()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");
            Guid attachmentID = dbHelper.EmailAttachment.CreateEmailAttachment(_teamId, false, emailID, TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt", "Doc2ToUpload");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("");
        }

        [TestProperty("JiraIssueID", "CDV6-4060")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
           "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - Open an attachment record - " +
           "Validate that all field titles are correctly displayed")]
        [TestMethod]
        [DeploymentItem("Files\\Doc2ToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod03()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");
            Guid attachmentID = dbHelper.EmailAttachment.CreateEmailAttachment(_teamId, false, emailID, TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt", "Doc2ToUpload");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("")
                .ValidateEmailFieldLabelVisible()
                .ValidateFileFieldLabelVisible()
                .ValidateResponsibleTeamFieldLabelVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-4061")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
           "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - Open an attachment record - " +
           "Validate that all fields are correctly displayed")]
        [TestMethod]
        [DeploymentItem("Files\\Doc2ToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod04()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .UploadFile(this.TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt")
                .ClickSaveAndCloseButton();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .ClickSearchButton();

            Guid attachmentID = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID)[0];

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("")
                .ValidateEmailFieldLinkText("Email 01")
                .ValidateFileFieldLinkText("Doc2ToUpload.txt (28 B)")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-4062")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - Open an attachment record - " +
            "Tap on the Document field - Validate that the attached document is downloaded into the browser download folder")]
        [TestMethod]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod05()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .UploadFile(this.TestContext.DeploymentDirectory + "\\Document.docx")
                .ClickSaveAndCloseButton();

            Guid attachmentID = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID)[0];

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("")
                .ClickFileFieldLink();

            System.Threading.Thread.Sleep(2000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsTrue(fileExists);

        }

        #endregion

        #region Create Record

        [TestProperty("JiraIssueID", "CDV6-4063")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - " +
            "Tap on the add button - Select a file to upload - Tap on the save button" +
            "Validate that the Attachment is saved and is associated with the Email")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS"), DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        public void Person_Emails_UITestMethod06()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton()
                .WaitForPersonEmailAttachmentRecordPageToLoad();

            var attachments = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID);
            Assert.AreEqual(1, attachments.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-4064")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - " +
            "Tap on the add button - Select a file to upload - Tap on the save and close button" +
            "Validate that the user is redirected to the email attachments page and that the Attachment is saved and is associated with the Email")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS"), DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        public void Person_Emails_UITestMethod07()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad();

            var attachments = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID);
            Assert.AreEqual(1, attachments.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-4066")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - " +
            "Tap on the add button - DO NOT select a file to upload - Tap on the save and close button" +
            "Validate than an error message is presented to the user and that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod09()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .ClickSaveAndCloseButtonWithoutWaitingForCWRefreshPanel()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateFileFieldErrorMessage("Please fill out this field.");

            var attachments = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID);
            Assert.AreEqual(0, attachments.Count);
        }

        #endregion

        #region Delete Record

        [DeploymentItem("Files\\Document.txt")]
        [DeploymentItem("chromedriver.exe")]
        [TestProperty("JiraIssueID", "CDV6-4065")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3932 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - Open an attachment record - " +
            "Tap on the Delete button - Confirm the delete operation - Validate that the record is deleted and the user is redirected to the Email attachments page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestMethod08()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .TapOnAddButton();

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("New")
                .UploadFile(this.TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad();

            Guid attachmentID = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID)[0];

            personEmailAttachmentsPage
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad();

            var attachments = dbHelper.EmailAttachment.GetEmailAttachmentByEmailID(emailID);
            Assert.AreEqual(0, attachments.Count);
        }

        #endregion


        #endregion

        #region Execute on demand Workflows : https://advancedcsg.atlassian.net/browse/CDV6-4904

        [TestProperty("JiraIssueID", "CDV6-24992")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - " +
            "Validate that the Run Workflow button is visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod01()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 2);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ValidateRunOnDemandWorkflowButtonVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-24993")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Validate that the Workflow lookup popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod02()
        {
            commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 2);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup.
                WaitForLookupPopupToLoad("Workflows");
        }

        [TestProperty("JiraIssueID", "CDV6-24994")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Validate that the Workflow lookup popup only displays on demand workflows")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod03()
        {
            Guid workflowID1 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");
            Guid workflowID2 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 2", "WF Workflow Testing - Is On Demand Process - WF 2.Zip");
            Guid workflowID3 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 3", "WF Workflow Testing - Is On Demand Process - WF 3.Zip");
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 2);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup.
                WaitForLookupPopupToLoad("Workflows")
                .ValidateResultElementPresent(workflowID1.ToString())
                .ValidateResultElementNotPresent(workflowID2.ToString())
                .ValidateResultElementPresent(workflowID3.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24995")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Perform a search that should match only one workflow (by name) - validate that only the matching workflow is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod04()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 2);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            Guid workflowID1 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");
            Guid workflowID2 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 2", "WF Workflow Testing - Is On Demand Process - WF 2.Zip");
            Guid workflowID3 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 3", "WF Workflow Testing - Is On Demand Process - WF 3.Zip");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .TypeSearchQuery("WF Workflow Testing - Is On Demand Process - WF 1")
                .TapSearchButton()
                .ValidateResultElementPresent(workflowID1.ToString())
                .ValidateResultElementNotPresent(workflowID2.ToString())
                .ValidateResultElementNotPresent(workflowID3.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24996")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Perform a search that should match one workflow that is NOT of on demand type - validate that no results are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod05()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 2);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            Guid workflowID1 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");
            Guid workflowID2 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 2", "WF Workflow Testing - Is On Demand Process - WF 2.Zip");
            Guid workflowID3 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 3", "WF Workflow Testing - Is On Demand Process - WF 3.Zip");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .TypeSearchQuery("WF Workflow Testing - Is On Demand Process - WF 2")
                .TapSearchButton()
                .ValidateResultElementNotPresent(workflowID1.ToString())
                .ValidateResultElementNotPresent(workflowID2.ToString())
                .ValidateResultElementNotPresent(workflowID3.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24997")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Perform a search that should match multiple workflows - validate that only results for oon demand workflows are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod06()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            Guid workflowID1 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");
            Guid workflowID2 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 2", "WF Workflow Testing - Is On Demand Process - WF 2.Zip");
            Guid workflowID3 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 3", "WF Workflow Testing - Is On Demand Process - WF 3.Zip");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .TypeSearchQuery("WF Workflow Testing - Is On Demand Process - WF")
                .TapSearchButton()
                .ValidateResultElementPresent(workflowID1.ToString())
                .ValidateResultElementNotPresent(workflowID2.ToString())
                .ValidateResultElementPresent(workflowID3.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24998")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - Tap on the Run On Demand Workflow button" +
            "Select a on Demand Workflow - Tap on the OK button - Validate that the workflow job record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod07()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            Guid workflowID1 = commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");

            //Remove any existing workflow job
            foreach (var jobid in this.dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID1))
                dbHelper.workflowJob.DeleteWorkflowJob(jobid);

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowID1.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1");


            var workflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID1);
            Assert.AreEqual(1, workflowJobs.Count);


            var fields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobs[0], "inactive", "validforexport", "statusid", "completedon", "businessdata",
                "workflowid", "regardingid", "regardingidtablename", "regardingidname", "startedon", "runordered", "statusid");

            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(false, (bool)fields["validforexport"]);
            Assert.AreEqual(workflowID1.ToString(), (fields["workflowid"]).ToString());
            Assert.AreEqual(emailID.ToString(), (fields["regardingid"]).ToString());
            Assert.AreEqual("email", (string)fields["regardingidtablename"]);
            Assert.AreEqual("WF Workflow Testing - Is On Demand Process - WF 1", (string)fields["regardingidname"]);
            Assert.AreEqual(false, (bool)fields["runordered"]);
            Assert.AreEqual(1, fields["statusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24999")]
        [Description("linked jira development issue https://advancedcsg.atlassian.net/browse/CDV6-4904 - " +
            "Login with a user account that has no security profile with 'Can Execute Workflows' privilege - Open Person Record -> Navigate to the Emails sub-section - Open a Email record - tap on additional items menu button - " +
            "Validate that the Run Workflow button is not present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Email_OnDemandWorkflows_UITestMethod08()
        {
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var nonAdminuserSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("Activities (BU Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Activities (Org Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)")[0]

            };
            commonMethodsDB.CreateSystemUserRecord("WorkflowTestUser4", "WorkflowTest", "User4", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, nonAdminuserSecProfiles);

            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "WF Workflow Testing - Is On Demand Process - WF 1", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");

            commonMethodsDB.CreateWorkflowIfNeeded("WF Workflow Testing - Is On Demand Process - WF 1", "WF Workflow Testing - Is On Demand Process - WF 1.Zip");

            loginPage
               .GoToLoginPage()
               .Login("WorkflowTestUser4", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("WF Workflow Testing - Is On Demand Process - WF 1")
                .ClickAdditionalItemsMenuButton()
                .ValidateRunOnDemandWorkflowButtonNotVisible();

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418


        [TestProperty("JiraIssueID", "CDV6-11189")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person email record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Emails_Cloning_UITestMethod01()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Significant Event Category
            Guid _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category", new DateTime(2020, 1, 1), _teamId, null, null, null);

            #endregion

            #region Significant Event Sub Category

            if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").Any())
            {
                dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_teamId, "Sub Category 1_2", _significantEventCategoryId, new DateTime(2020, 1, 1), null, null);
            }
            Guid _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").FirstOrDefault();

            #endregion

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);

            var SystemUserId2 = commonMethodsDB.CreateSystemUserRecord("ALBERTEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId3 = commonMethodsDB.CreateSystemUserRecord("AlbertoJimenezz", "Alberto", "Jimenezz", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId4 = commonMethodsDB.CreateSystemUserRecord("AlbertBlue", "Albert", "Blue", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId5 = commonMethodsDB.CreateSystemUserRecord("JohnStones", "John", "Stones", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            Guid personEmailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "person", _personFullname,
                "Email 01 All Fields Setup", "Email 01 All Fields Setup description", 2, new DateTime(2020, 9, 1),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId, true,
                new DateTime(2020, 9, 1), _significantEventCategoryId, _significantEventSubCategoryId, false, true);

            dbHelper.emailCc.CreateEmailCc(personEmailID, SystemUserId2, "systemuser", "ALBERT Einstein");
            dbHelper.emailCc.CreateEmailCc(personEmailID, SystemUserId3, "systemuser", "Alberto Jimenezz");
            dbHelper.emailTo.CreateEmailTo(personEmailID, SystemUserId4, "systemuser", "Albert Blue");
            dbHelper.emailTo.CreateEmailTo(personEmailID, SystemUserId5, "systemuser", "John Stones");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(personEmailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01 All Fields Setup")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            System.Threading.Thread.Sleep(2000);
            var records = dbHelper.email.GetEmailByRegardingID(caseID);
            Assert.AreEqual(1, records.Count);

            var emailFields = dbHelper.email.GetEmailByID(records[0], "emailfromlookupid",
                "subject", "notes", "regardingid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "informationbythirdparty", "statusid", "activityoutcomeid", "iscasenote",
                "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual(_systemUserId.ToString(), emailFields["emailfromlookupid"].ToString());
            Assert.AreEqual(2, emailFields["statusid"]);
            Assert.AreEqual("Email 01 All Fields Setup", emailFields["subject"]);
            Assert.AreEqual("Email 01 All Fields Setup description", emailFields["notes"]);
            Assert.AreEqual(caseID.ToString(), emailFields["regardingid"].ToString());
            Assert.AreEqual(_teamId.ToString(), emailFields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), emailFields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), emailFields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), emailFields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), emailFields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)emailFields["duedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), emailFields["activitysubcategoryid"]);
            Assert.AreEqual(false, emailFields["informationbythirdparty"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), emailFields["activityoutcomeid"]);
            Assert.AreEqual(true, emailFields["iscasenote"]);
            Assert.AreEqual(true, emailFields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), emailFields["significanteventcategoryid"]);
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)emailFields["significanteventdate"]));
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), emailFields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, emailFields["iscloned"]);
            Assert.AreEqual(personEmailID.ToString(), emailFields["clonedfromid"].ToString());

            var emailCc = dbHelper.emailCc.GetByEmailID(records[0]);
            Assert.AreEqual(2, emailCc.Count);

            emailCc = dbHelper.emailCc.GetByEmailAndRegardingID(records[0], SystemUserId2);
            Assert.AreEqual(1, emailCc.Count);

            emailCc = dbHelper.emailCc.GetByEmailAndRegardingID(records[0], SystemUserId3);
            Assert.AreEqual(1, emailCc.Count);

            var emailTo = dbHelper.emailTo.GetByEmailID(records[0]);
            Assert.AreEqual(2, emailTo.Count);

            emailTo = dbHelper.emailTo.GetByEmailAndRegardingID(records[0], SystemUserId4);
            Assert.AreEqual(1, emailTo.Count);

            emailTo = dbHelper.emailTo.GetByEmailAndRegardingID(records[0], SystemUserId5);
            Assert.AreEqual(1, emailTo.Count);
        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11653


        [TestProperty("JiraIssueID", "CDV6-11711")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandatory fields and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases01()

        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveAndCloseButton();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var email = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, email.Count);

        }


        [TestProperty("JiraIssueID", "CDV6-11712")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Leave any Mandatory Field blank-Validate the pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases02()
        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickSaveAndCloseButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateFromMessageAreaVisible(true)
                .ValidateFromMessageAreaText("Please fill out this field.")
                .ValidateToMessageAreaVisible(true)
                .ValidateToMessageAreaText("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "CDV6-11715")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all Mandatory Field blank-Select multiple users in to field-Save and close the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases03()
        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());


            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("Care Coordinator Test User").TapSearchButton().ClickAddSelectedButton(toId.ToString());


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveAndCloseButton();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad();

        }



        [TestProperty("JiraIssueID", "CDV6-11716")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -select the person without email and validate the pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases04()

        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("Testing_CDV6_11716").TapSearchButton().SelectResultElement(fromId2.ToString());

            personEmailRecordPage
              .WaitForPersonEmailRecordPageToLoad("New")
              .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Testing CDV6-11716 does not contain an email address.");




        }


        [TestProperty("JiraIssueID", "CDV6-11717")]
        [Description("Open a person case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases05()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Significant Event Category
            Guid _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category", new DateTime(2020, 1, 1), _teamId, null, null, null);

            #endregion

            #region Significant Event Sub Category

            if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").Any())
            {
                dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_teamId, "Sub Category 1_2", _significantEventCategoryId, new DateTime(2020, 1, 1), null, null);
            }
            Guid _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").FirstOrDefault();

            #endregion

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);

            var SystemUserId2 = commonMethodsDB.CreateSystemUserRecord("ALBERTEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId3 = commonMethodsDB.CreateSystemUserRecord("AlbertoJimenezz", "Alberto", "Jimenezz", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId4 = commonMethodsDB.CreateSystemUserRecord("AlbertBlue", "Albert", "Blue", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var SystemUserId5 = commonMethodsDB.CreateSystemUserRecord("JohnStones", "John", "Stones", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            Guid personEmailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "person", _personFullname,
                "Email 01 All Fields Setup", "Email 01 All Fields Setup description", 2, new DateTime(2020, 9, 1),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId, true,
                new DateTime(2020, 9, 1), _significantEventCategoryId, _significantEventSubCategoryId, false, true);

            dbHelper.emailCc.CreateEmailCc(personEmailID, SystemUserId2, "systemuser", "ALBERT Einstein");
            dbHelper.emailCc.CreateEmailCc(personEmailID, SystemUserId3, "systemuser", "Alberto Jimenezz");
            dbHelper.emailTo.CreateEmailTo(personEmailID, SystemUserId4, "systemuser", "Albert Blue");
            dbHelper.emailTo.CreateEmailTo(personEmailID, SystemUserId5, "systemuser", "John Stones");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(personEmailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01 All Fields Setup")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            System.Threading.Thread.Sleep(2000);

            var records = dbHelper.email.GetEmailByRegardingID(caseID);
            Assert.AreEqual(1, records.Count);

            var emailFields = dbHelper.email.GetEmailByID(records[0], "emailfromlookupid",
                "subject", "notes", "regardingid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "informationbythirdparty", "statusid", "activityoutcomeid", "iscasenote",
                "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual(_systemUserId.ToString(), emailFields["emailfromlookupid"].ToString());

            Assert.AreEqual(2, emailFields["statusid"]);
            Assert.AreEqual("Email 01 All Fields Setup", emailFields["subject"]);
            Assert.AreEqual("Email 01 All Fields Setup description", emailFields["notes"]);
            Assert.AreEqual(caseID.ToString(), emailFields["regardingid"]);
            Assert.AreEqual(_teamId.ToString(), emailFields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId.ToString(), emailFields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), emailFields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId.ToString(), emailFields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), emailFields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)emailFields["duedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), emailFields["activitysubcategoryid"].ToString());
            Assert.AreEqual(false, emailFields["informationbythirdparty"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), emailFields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, emailFields["iscasenote"]);
            Assert.AreEqual(true, emailFields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), emailFields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2020, 9, 1), ((DateTime)emailFields["significanteventdate"]));
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), emailFields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, emailFields["iscloned"]);
            Assert.AreEqual(personEmailID.ToString(), emailFields["clonedfromid"].ToString());

            var emailCc = dbHelper.emailCc.GetByEmailID(records[0]);
            Assert.AreEqual(2, emailCc.Count);

            emailCc = dbHelper.emailCc.GetByEmailAndRegardingID(records[0], SystemUserId2);
            Assert.AreEqual(1, emailCc.Count);

            emailCc = dbHelper.emailCc.GetByEmailAndRegardingID(records[0], SystemUserId3);
            Assert.AreEqual(1, emailCc.Count);

            var emailTo = dbHelper.emailTo.GetByEmailID(records[0]);
            Assert.AreEqual(2, emailTo.Count);

            emailTo = dbHelper.emailTo.GetByEmailAndRegardingID(records[0], SystemUserId4);
            Assert.AreEqual(1, emailTo.Count);

            emailTo = dbHelper.emailTo.GetByEmailAndRegardingID(records[0], SystemUserId5);
            Assert.AreEqual(1, emailTo.Count);
        }



        [TestProperty("JiraIssueID", "CDV6-11718")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Open the Email - Update and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases06()

        {
            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(personEmail.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("Email 001")
                 .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("PersonEmailUser1")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                  .WaitForPersonEmailRecordPageToLoad("Email 001")
                  .InsertSubject("Person Email 001 Updated")
                  .ClickSaveAndCloseButton();

            personEmailsPage
                 .WaitForPersonEmailsPageToLoad();



            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);


            var emailFields = dbHelper.email.GetEmailByID(records[0], "emailfromlookupid",
               "subject", "notes", "regardingid", "ownerid", "activityreasonid", "responsibleuserid",
               "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "informationbythirdparty", "statusid", "activityoutcomeid", "iscasenote",
               "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
               "iscloned", "clonedfromid");


            Assert.AreEqual("Person Email 001 Updated", emailFields["subject"]);






        }

        [TestProperty("JiraIssueID", "CDV6-11719")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandatory Fields- Validate Save and return to previous page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases07()

        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("PersonEmailUser1")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                  .WaitForPersonEmailRecordPageToLoad("New")
                  .InsertSubject("Email 001")
                  .ClickSaveAndCloseButton();

            personEmailsPage
                 .WaitForPersonEmailsPageToLoad();



            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var emailFields = dbHelper.email.GetEmailByID(records[0], "emailfromlookupid",
               "subject", "regardingid");

            var emailToId = dbHelper.emailTo.GetByEmailID(records[0]);
            var emailToIdFields = dbHelper.emailTo.GetByID(emailToId[0], "regardingid")["regardingid"];

            Assert.AreEqual("Email 001", emailFields["subject"]);
            Assert.AreEqual(_systemUserId.ToString(), emailFields["emailfromlookupid"].ToString());
            Assert.AreEqual(to1Id.ToString(), emailToIdFields.ToString());
            Assert.AreEqual(_personID.ToString(), emailFields["regardingid"].ToString());
        }


        [TestProperty("JiraIssueID", "CDV6-11720")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases08()
        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().SelectResultElement(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ValidateFromFieldEmail(true)
                .ValidateFromFieldText("CareCoordinator TestUser");


        }

        [TestProperty("JiraIssueID", "CDV6-11721")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields except From and save the record - Validate the pop up")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases09()

        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();


            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveAndCloseButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateFromMessageAreaVisible(true)
                .ValidateFromMessageAreaText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-11723")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields except To field and save the record- validate the Pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases10()

        {

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveAndCloseButton()
                 .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateToMessageAreaVisible(true)
                .ValidateToMessageAreaText("Please fill out this field.");


        }



        [TestProperty("JiraIssueID", "CDV6-11724")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields and CC field should be blank- Click Save and close button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases11()
        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSubject("Email 001")
                .ClickSaveButton()
                .ValidateCCAreaText("")
                .ClickCCLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("Care Coordinator Test User").TapSearchButton().ClickAddSelectedButton(toId.ToString());

            personEmailRecordPage
              .WaitForPersonEmailRecordPageToLoad("Email 001")
              .ClickSaveAndCloseButton();

            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var emailFields = dbHelper.email.GetEmailByID(records[0], "emailfromlookupid",
               "subject", "regardingid");

            var emailToId = dbHelper.emailTo.GetByEmailID(records[0]);
            var emailToIdFields = dbHelper.emailTo.GetByID(emailToId[0], "regardingid")["regardingid"];

            var emailCcId = dbHelper.emailCc.GetByEmailID(records[0]);
            var emailCcIdFields = dbHelper.emailCc.GetByID(emailCcId[0], "regardingid")["regardingid"];

            Assert.AreEqual("Email 001", emailFields["subject"]);
            Assert.AreEqual(fromId.ToString(), emailFields["emailfromlookupid"].ToString());
            Assert.AreEqual(to1Id.ToString(), emailToIdFields.ToString());
            Assert.AreEqual(toId.ToString(), emailCcIdFields.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11727")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields and SignificiantEvent to Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases12()

        {
            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Significant Event Category
            Guid _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category", new DateTime(2020, 1, 1), _teamId, null, null, null);

            #endregion

            #region Significant Event Sub Category

            if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").Any())
            {
                dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_teamId, "Sub Category 1_2", _significantEventCategoryId, new DateTime(2020, 1, 1), null, null);
            }
            Guid _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("Sub Category 1_2").FirstOrDefault();

            #endregion

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
              .WaitForPersonEmailRecordPageToLoad("New")
              .InsertSubject("Email 001")
              .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PersonEmailUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickSignificantEvent_YesRadioButton()
                 .ClickSignificantEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category").TapSearchButton().SelectResultElement(_significantEventCategoryId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .InsertSignificantEventDate("20/07/2021")
              .ClickSignificantEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Category 1_2").TapSearchButton().SelectResultElement(_significantEventSubCategoryId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickSaveButton();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);
            var emailId = records.FirstOrDefault();

            var significantEventRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personID);
            Assert.AreEqual(1, significantEventRecords.Count);
            var newSignificantEventRecordId = significantEventRecords.FirstOrDefault();

            var fields = dbHelper.personSignificantEvent.GetPersonSignificantEventByID(newSignificantEventRecordId,
              "ownerid", "owningbusinessunitid", "title", "inactive", "eventdate", "eventdetails", "significanteventcategoryid", "significanteventsubcategoryid"
              , "sourceactivityid", "sourceactivityidtablename", "sourceactivityidname", "iscloned");

            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            StringAssert.Contains((string)fields["title"], "Significant Event for " + _personFullname + " created by " + _systemUserFullname + " on");
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(new DateTime(2021, 07, 20), fields["eventdate"]);
            Assert.AreEqual(false, fields.ContainsKey("eventdetails"));
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(emailId.ToString(), fields["sourceactivityid"].ToString());
            Assert.AreEqual("email", fields["sourceactivityidtablename"]);
            Assert.AreEqual("Email 001", fields["sourceactivityidname"]);
            Assert.AreEqual(false, fields["iscloned"]);
        }

        [TestProperty("JiraIssueID", "CDV6-11728")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields and SignificiantEvent to No")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases13()

        {

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
               .WaitForPersonEmailRecordPageToLoad("New")
               .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
              .WaitForPersonEmailRecordPageToLoad("New")
              .InsertSubject("Email 001")
              .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .InsertSentRecievedDate("20/07/2021")
                 .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PersonEmailUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("New")
                 .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New")
                .ClickSignificantEvent_NoRadioButton();


            var significantEventRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personID);
            Assert.AreEqual(0, significantEventRecords.Count);


        }

        [TestProperty("JiraIssueID", "CDV6-11729")]
        [Description("Create Record using Advance search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases14()

        {

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Emails")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personNumber).TapSearchButton().SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                .ClickFromIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("HealthTest12").TapSearchButton().SelectResultElement(fromId.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                .ClickToIdLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
             .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
             .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(_personNumber).TapSearchButton().SelectResultElement(_personID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                .InsertSubject("Email 001")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                 .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("High").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personEmailRecordPage
                  .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                  .InsertSentRecievedDate("20/07/2021")
                  .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PersonEmailUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                 .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());


            personEmailRecordPage
                  .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                 .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                 .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());


            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoadFromAdvanceSearch("New")
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForResultsPageToLoad();

            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);
            var email = records.FirstOrDefault();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(email.ToString());


        }

        [TestProperty("JiraIssueID", "CDV6-11730")]
        [Description("Open a person Email and validate Export to Excel ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases15()

        {
            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .SelectPersonEmailRecord(personEmail.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
             .WaitForExportDataPopupToLoad()
             .SelectRecordsToExport("Selected Records")
             .SelectExportFormat("Csv (comma separated with quotes)")
             .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Emails.csv");
            Assert.IsTrue(fileExists);

        }


        [TestProperty("JiraIssueID", "CDV6-11731")]
        [Description("Open a person Email and Assign ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases16()

        {

            var _advancedResponsibleTeamId = commonMethodsDB.CreateTeam("Advanced", null, _businessUnitId, "957618", "AdvancedQA@careworkstempmail.com", "Advanced QA", "020 123456");

            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .SelectPersonEmailRecord(personEmail.ToString())
                .ClickAssignButton();


            assignRecordPopup.WaitForAssignRecordPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Advanced")
                .TapSearchButton().SelectResultElement(_advancedResponsibleTeamId.ToString());

            assignRecordPopup.SelectResponsibleUserDecision("Do not change").TapOkButton();


            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ValidateRecordCellText(personEmail.ToString(), 2, "Email 001")
                .ValidateRecordCellText(personEmail.ToString(), 4, "Advanced");

        }


        [TestProperty("JiraIssueID", "CDV6-11732")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandator fields and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases17()

        {
            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .ClickNewRecordButton();

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("New");




        }


        [TestProperty("JiraIssueID", "CDV6-11733")]
        [Description("Open a person Email- Update the record - Validate the Audit details")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases18()

        {

            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(personEmail.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickFromIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("PersonEmailUser1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .InsertSubject("Person Email 001 Updated")
                .ClickSaveButton()
                .NavigateToAuditSubPage();

            auditListPage
               .WaitForAuditListPageToLoad("email");

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = personEmail.ToString(),
                ParentTypeName = "email",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("PersonEmail User1", auditResponseData.GridData[0].cols[1].Text);


        }


        [TestProperty("JiraIssueID", "CDV6-11734")]
        [Description("Open a person record (person has no Emails linked to it) - Navigate to the Person Email screen -Enter all the Mandatory Fields- Validate Save and return to previous page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases19()

        {


            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(personEmail.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton().ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickSaveButton()
                .ClickSendEmailButton();



        }

        [TestProperty("JiraIssueID", "CDV6-11735")]
        [Description("Open a person Email- Update the record - Validate the Audit details")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases20()

        {

            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();


            personEmailsPage
               .WaitForPersonEmailsPageToLoad()
               .SelectPersonEmailRecord(personEmail.ToString())
               .ClickDeleteButton();
            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();
            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            personEmailsPage
               .WaitForPersonEmailsPageToLoad()
               .ValidateRecordNotVisible(personEmail.ToString());

            var record = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(0, record.Count);


        }



        [TestProperty("JiraIssueID", "CDV6-11726")]
        [Description("Open a person Email- Update the Protective Marking Scheme")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases21()

        {

            var protectiveMarkingSchemeId = commonMethodsDB
                .CreateProtectiveMarkingScheme(new Guid("403d234d-27f5-eb11-a325-005056926fe4"), "For Official Use Only", _teamId, _businessUnitId,
                true, false, false, false, 1, 3, 11, 5, 12, null, "Protective Marking Scheme 02 Text Line 01");

            var personEmail = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 001", "Person Activity Email", 1);

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
               .WaitForPersonEmailsPageToLoad()
               .OpenPersonEmailRecord(personEmail.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 001")
                .ClickToIdLookUpButton();
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareCoordinatorTestUser").TapSearchButton()
                .ClickAddSelectedButton(to1Id.ToString());

            personEmailRecordPage
                 .WaitForPersonEmailRecordPageToLoad("Email 001")
                 .ClickProtectiveMarkingSchemeLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("For Official Use Only").TapSearchButton().SelectResultElement(protectiveMarkingSchemeId.ToString());

            personEmailRecordPage
              .WaitForPersonEmailRecordPageToLoad("Email 001")
              .ClickSaveAndCloseButton();

            var records = dbHelper.email.GetEmailByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var emailFields = dbHelper.email.GetEmailByID(records[0],
                "subject", "protectivemarkingschemeid");

            Assert.AreEqual("Email 001", emailFields["subject"]);
            Assert.AreEqual(protectiveMarkingSchemeId.ToString(), emailFields["protectivemarkingschemeid"].ToString());
        }



        [TestProperty("JiraIssueID", "CDV6-11725")]
        [Description("Open Person Record -> Navigate to the Emails sub-section - Open a Email record - Tap on the Attachments tab - " +
            "Validate that a user is redirected to the email attachments sub page- update the attachment")]
        [TestMethod]
        [DeploymentItem("Files\\DocToUpload.txt"), DeploymentItem("Files\\Doc2ToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Emails_UITestCases22()
        {
            Guid emailID = dbHelper.email.CreateEmail(_teamId, _personID, _systemUserId, _systemUserId, "PersonEmail User1", "systemuser", _personID, "regardingidtablename", _personFullname, "Email 01", "Person Activity Email", 1);
            dbHelper.emailTo.CreateEmailTo(emailID, to1Id, "systemuser", "CareCoordinator TestUser");
            Guid attachmentID = dbHelper.EmailAttachment.CreateEmailAttachment(_teamId, false, emailID, TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt", "Doc2ToUpload");

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToEmailsPage();

            personEmailsPage
                .WaitForPersonEmailsPageToLoad()
                .OpenPersonEmailRecord(emailID.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Email 01")
                .NavigateToAttachmentsPage();

            personEmailAttachmentsPage
                .WaitForPersonEmailAttachmentsPageToLoad()
                .OpenEmailAttachmentRecord(attachmentID.ToString());

            personEmailAttachmentRecordPage
                .WaitForPersonEmailAttachmentRecordPageToLoad("")

                .ClickFileIcon()
                .ClickFile1UploadDocument(TestContext.DeploymentDirectory + "\\DocToUpload.txt")
                .ClickFileUpload()
                .ClickSaveButton()
                .ClickFile1UploadDocument(TestContext.DeploymentDirectory + "\\Doc2ToUpload.txt")
                .ClickFileUpload()
                .ValidateLatestFileLink(true)
                .ValidateLatestFileLinkText("Doc2ToUpload.txt");




        }
        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

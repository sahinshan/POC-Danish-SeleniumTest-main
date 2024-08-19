using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Attachments_UITestCases : FunctionalTest
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-8649

        [TestProperty("JiraIssueID", "CDV6-24666")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - validate that the bulk upload popup is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BulkLoadAttachments_UITestMethod01()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24667")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document Type and Document SubType ")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod02()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(true)
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelVisibility(true)
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24668")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document SubType ")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod03()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()

                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(true)
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24669")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document sub type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that the user is prevented from uploading the files without selecting a Document SubType ")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod04()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()

                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(true)
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24670")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a file to be uploaded - Validate that the file is displayed in the popup with name, size, upload button and cancel button")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod05()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")

                .ValidateAttachedFileNameVisibility(1, true)
                .ValidateAttachedFileSizeVisibility(1, true)
                .ValidateAttachedFileCancelUploadButtonVisibility(1, true)

                .ValidateAttachedFileNameText(1, "Document.docx")
                .ValidateAttachedFileSizeText(1, "11.86 KB");

        }

        [TestProperty("JiraIssueID", "CDV6-24671")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - Validate that both file are displayed in the popup with name, size, upload button and cancel button")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("Files\\Document2.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod06()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

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

        [TestProperty("JiraIssueID", "CDV6-24672")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - For the last file click on the Cancel Upload button - Validate that the file is removed from the list")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("Files\\Document2.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod07()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

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
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24673")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - For the first file click on the Cancel Upload button - Validate that the file is removed from the list")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("Files\\Document2.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod08()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

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
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24674")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select two files to be uploaded - Click on the main cancel upload button - Validate that all files are removed from the list")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("Files\\Document2.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod09()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

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
                .ValidateAttachedFileCancelUploadButtonVisibility(2, false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24675")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
            "Select a document type - select a document sub type - Select a file to be uploaded - Click on the Start Upload button - " +
            "Validate that no error message is displayed - Validate that a new person attachment record is created")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod15()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(false)


                .ClickCloseButton();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad();

            var attachmentrecords = dbHelper.personAttachment.GetByPersonID(personID);
            Assert.AreEqual(1, attachmentrecords.Count);

            var fields = dbHelper.personAttachment.GetByID(attachmentrecords[0], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(attachDocumentTypeId, fields["documenttypeid"]);
            Assert.AreEqual(attachDocumentSubTypeId, fields["documentsubtypeid"]);
            Assert.AreEqual(teamID, fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);

        }

        [TestProperty("JiraIssueID", "CDV6-24676")]
        [Description("Open a person record - Navigate to the person attachments page - click on the bulk upload button - Wait for the bulk upload popup to displayed - " +
           "Select a document type - select a document sub type - Select two files to be uploaded - Click on the Start Upload button - " +
           "Validate that no error message is displayed - Validate that two new person attachment records are created")]
        [TestMethod]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        public void BulkLoadAttachments_UITestMethod16()
        {
            var personID = new Guid("96da3c20-2cd3-47b0-b42f-5f10e312d97b"); //Garry Gallagher
            var personNumber = "206782";
            var attachDocumentTypeId = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var attachDocumentSubTypeId = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            //remove all person attachments from the person
            foreach (var personattachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(personattachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan").SelectResultElement(attachDocumentTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bladder Scan Results").SelectResultElement(attachDocumentSubTypeId.ToString());

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToReload()

                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.docx")
                .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.docx")
                .ValidateAttachedFileNameVisibility(1, true)
                .ClickStartUploadButton()

                .ValidateDocumentTypeErrorLabelVisibility(false)
                .ValidateDocumentSubTypeErrorLabelVisibility(false)


                .ClickCloseButton();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad();

            var attachmentrecords = dbHelper.personAttachment.GetByPersonID(personID);
            Assert.AreEqual(2, attachmentrecords.Count);

            var fields = dbHelper.personAttachment.GetByID(attachmentrecords[0], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(attachDocumentTypeId, fields["documenttypeid"]);
            Assert.AreEqual(attachDocumentSubTypeId, fields["documentsubtypeid"]);
            Assert.AreEqual(teamID, fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);

            fields = dbHelper.personAttachment.GetByID(attachmentrecords[1], "documenttypeid", "documentsubtypeid", "ownerid", "date");
            Assert.AreEqual(attachDocumentTypeId, fields["documenttypeid"]);
            Assert.AreEqual(attachDocumentSubTypeId, fields["documentsubtypeid"]);
            Assert.AreEqual(teamID, fields["ownerid"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).Date);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8419

        [TestProperty("JiraIssueID", "CDV6-24677")]
        [Description("Open a Person Attachment record (person attachment Business Object should contain multiple custom fields that, for this record, have data set in them) - " +
            "Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select a case form record as the destination record - Confirm the clone operation - Validate that the Person Attachment record is properly cloned")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonAttachments_Cloning_UITestMethod01()
        {
            var personID = new Guid("60aa21f1-3b3e-4c79-8cf8-eb6cc8f6c12a"); //Pam MacDonald
            var personNumber = "208650";
            var personAttachmentID = new Guid("55935e94-bee0-eb11-a325-005056926fe4"); //Pam MacDonald Attachment (For Person) 001
            var caseFormId = new Guid("967fac69-bfe0-eb11-a325-005056926fe4"); //02/07/2021



            //remove all attachments from the target case form
            foreach (var caseFormAttachmentid in dbHelper.caseFormAttachment.GetByCaseFormID(caseFormId))
                dbHelper.caseFormAttachment.DeleteCaseFormAttachment(caseFormAttachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .OpenPersonAttachmentsRecord(personAttachmentID.ToString());

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("Pam MacDonald Attachment (For Person) 001")
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Form (Case)")
                .InsertStartDate("05/07/2021")
                .SelectRecord(caseFormId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();


            var records = dbHelper.caseFormAttachment.GetByCaseFormID(caseFormId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseFormAttachment.GetByID(records[0],
                "caseformid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");


            var attachmentDate = new DateTime(2021, 7, 4, 23, 0, 0, DateTimeKind.Utc);
            var documentType = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var documentSubType = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results    
            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var linkedCaseFormAttachmentID = new Guid("253fe636-bfe0-eb11-a325-005056926fe4"); //Pam MacDonald Attachment (Case Form) 001

            var CloneTestingDateAndTime = new DateTime(2021, 7, 5, 10, 15, 0, DateTimeKind.Utc);
            var CloneTestingDate = new DateTime(2021, 7, 8, 0, 0, 0, DateTimeKind.Utc);


            Assert.AreEqual(caseFormId, fields["caseformid"]);
            Assert.AreEqual("Pam MacDonald Attachment (For Person) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personAttachmentID, fields["clonedfromid"]);

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
            Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24678")]
        [Description("Open a Person Attachment record (person attachment Business Object should contain multiple custom fields that, for this record, have data set in them) - " +
            "Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select multiple case form record as the destination records - Confirm the clone operation - Validate that the Person Attachment record is properly cloned into each selected record")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonAttachments_Cloning_UITestMethod02()
        {
            var personID = new Guid("60aa21f1-3b3e-4c79-8cf8-eb6cc8f6c12a"); //Pam MacDonald
            var personNumber = "208650";
            var personAttachmentID = new Guid("55935e94-bee0-eb11-a325-005056926fe4"); //Pam MacDonald Attachment (For Person) 001
            var caseForm1Id = new Guid("967fac69-bfe0-eb11-a325-005056926fe4"); //02/07/2021
            var caseForm2Id = new Guid("1912b874-bfe0-eb11-a325-005056926fe4"); //01/07/2021


            //remove all attachments from the target case form
            foreach (var caseFormAttachmentid in dbHelper.caseFormAttachment.GetByCaseFormID(caseForm1Id))
                dbHelper.caseFormAttachment.DeleteCaseFormAttachment(caseFormAttachmentid);

            //remove all attachments from the target case form
            foreach (var caseFormAttachmentid in dbHelper.caseFormAttachment.GetByCaseFormID(caseForm2Id))
                dbHelper.caseFormAttachment.DeleteCaseFormAttachment(caseFormAttachmentid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .OpenPersonAttachmentsRecord(personAttachmentID.ToString());

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("Pam MacDonald Attachment (For Person) 001")
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Form (Case)")
                .InsertStartDate("05/07/2021")
                .SelectRecord(caseForm1Id.ToString())
                .SelectRecord(caseForm2Id.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();


            var records = dbHelper.caseFormAttachment.GetByCaseFormID(caseForm1Id);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseFormAttachment.GetByID(records[0],
                "caseformid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");


            var attachmentDate = new DateTime(2021, 7, 4, 23, 0, 0, DateTimeKind.Utc);
            var documentType = new Guid("6a3ee339-6ff6-e911-a2c7-005056926fe4"); //Bladder Scan
            var documentSubType = new Guid("0309c96f-71f6-e911-a2c7-005056926fe4"); //Bladder Scan Results    
            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var linkedCaseFormAttachmentID = new Guid("253fe636-bfe0-eb11-a325-005056926fe4"); //Pam MacDonald Attachment (Case Form) 001

            var CloneTestingDateAndTime = new DateTime(2021, 7, 5, 10, 15, 0, DateTimeKind.Utc);
            var CloneTestingDate = new DateTime(2021, 7, 8, 0, 0, 0, DateTimeKind.Utc);


            Assert.AreEqual(caseForm1Id, fields["caseformid"]);
            Assert.AreEqual("Pam MacDonald Attachment (For Person) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personAttachmentID, fields["clonedfromid"]);

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
            Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);


            records = dbHelper.caseFormAttachment.GetByCaseFormID(caseForm2Id);
            Assert.AreEqual(1, records.Count);

            fields = dbHelper.caseFormAttachment.GetByID(records[0],
                "caseformid", "title", "date", "documenttypeid", "documentsubtypeid", "ownerid", "fileid",
                "iscloned", "clonedfromid",
                "qa_cloningtestborid", "qa_clonetestingdateandtime", "qa_clonetestingdecimal", "qa_clonetestingemail", "qa_clonetestingmldt", "qa_clonetestingmoney", "qa_clonetestingmultilinetextbox", "qa_clonetestingdnumeric",
                "qa_cloningtestdate", "qa_clonetestingphone", "qa_clonetestingpickliistid", "qa_clonetestingsingleline", "qa_clonetestingtime", "qa_clonetestingurl", "qa_cloningtestboolean");

            Assert.AreEqual(caseForm2Id, fields["caseformid"]);
            Assert.AreEqual("Pam MacDonald Attachment (For Person) 001", fields["title"]);
            Assert.AreEqual(attachmentDate.ToLocalTime(), ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(true, fields.ContainsKey("fileid"));
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personAttachmentID, fields["clonedfromid"]);

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
            Assert.AreEqual(new TimeSpan(12, 55, 0), fields["qa_clonetestingtime"]);
            Assert.AreEqual("www.google.pt", fields["qa_clonetestingurl"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolean"]);
        }

        #endregion
    }
}

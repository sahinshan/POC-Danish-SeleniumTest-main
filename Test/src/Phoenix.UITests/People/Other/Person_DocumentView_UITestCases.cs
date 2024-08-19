using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\D_Flag.Zip"), DeploymentItem("Files\\Automated UI Test Document 1.Zip")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    public class Person_DocumentView_UITestCases : FunctionalTest
    {

        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _ethnicityId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _defaultLoginUserID;
        private string _loginUsername;
        private Guid _dataFormId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _documentName;
        private Guid _documentId;
        private Guid _attachDocumentTypeId;
        private Guid _attachDocumentTypeId2;
        private Guid _attachDocumentSubTypeId;
        private Guid _attachDocumentSubTypeId2;

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

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region System User

                _loginUsername = "PersonDocumentViewUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "PersonDocumentView", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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

                #endregion

                #region Attach Document Type

                _attachDocumentTypeId = commonMethodsDB.CreateAttachDocumentType(_careDirectorQA_TeamId, "All Attached Documents", new DateTime(2020, 1, 1));
                _attachDocumentTypeId2 = commonMethodsDB.CreateAttachDocumentType(_careDirectorQA_TeamId, "Attached Doc View", new DateTime(2022, 1, 1));

                #endregion

                #region Attach Document Sub Type

                _attachDocumentSubTypeId = commonMethodsDB.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "All Attached Sub Document", new DateTime(2022, 1, 1), _attachDocumentTypeId);
                _attachDocumentSubTypeId2 = commonMethodsDB.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Attached Sub Doc View", new DateTime(2022, 1, 1), _attachDocumentTypeId2);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Print to PDF for Assessments (IG Risk) https://advancedcsg.atlassian.net/browse/CDV6-3571

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Open Person Record -> Navigate to the Document View section - Select all case form records - Tap on the Download Selected button - " +
            "Validate that a Zip file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24454")]
        public void Person_DocumentView_UITestMethod01()
        {
            Guid personID = new Guid("576a75a4-67bf-ea11-a2cd-005056926fe4"); //Ms Juliana Cardina
            string personNumber = "504085";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()
                .ClickSelectAllCaseFormsCheckBox()
                .ClickDownloadSelectedButton();

            System.Threading.Thread.Sleep(3000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "DocumentViewFiles.zip");
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = PDF & Form.OnClosePrintFormat = PDF" +
            "Open Person Record -> Navigate to the Document View section - Select all case form records - Tap on the Download Selected button - " +
            "Unzip the downloaded file - Validate that only 1 PDF file is extracted")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24455")]
        public void Person_DocumentView_UITestMethod02()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.OnClosePrintFormat").FirstOrDefault();
            Guid systemSettingID2 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "PDF");

            Guid personID = new Guid("576a75a4-67bf-ea11-a2cd-005056926fe4"); //Ms Juliana Cardina
            string personNumber = "504085";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()
                .ClickSelectAllCaseFormsCheckBox()
                .ClickDownloadSelectedButton();

            bool fileExists = fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "DocumentViewFiles.zip", 30);
            Assert.IsTrue(fileExists);

            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);

            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "MultipleFormsData.pdf");
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word" +
            "Open Person Record -> Navigate to the Document View section - Select all case form records - Tap on the Download Selected button - " +
            "Unzip the downloaded file - Validate that only 1 Word file is extracted")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24456")]
        public void Person_DocumentView_UITestMethod03()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.OnClosePrintFormat").FirstOrDefault();
            Guid systemSettingID2 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "Word");

            Guid personID = new Guid("576a75a4-67bf-ea11-a2cd-005056926fe4"); //Ms Juliana Cardina
            string personNumber = "504085";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()
                .ClickSelectAllCaseFormsCheckBox()
                .ClickDownloadSelectedButton();

            System.Threading.Thread.Sleep(3000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "DocumentViewFiles.zip");
            Assert.IsTrue(fileExists);

            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);

            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "MultipleFormsData.docx");
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word" +
            "Open Person Record -> Navigate to the Document View section - Select all case form records - Tap on the Download Selected button - " +
            "Unzip the downloaded file - open the extracted docx file - Validate that the file contains data from all selected case forms")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24457")]
        public void Person_DocumentView_UITestMethod04()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.OnClosePrintFormat").FirstOrDefault();
            Guid systemSettingID2 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "Word");

            Guid personID = new Guid("576a75a4-67bf-ea11-a2cd-005056926fe4"); //Ms Juliana Cardina
            string personNumber = "504085";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()
                .ClickSelectAllCaseFormsCheckBox()
                .ClickDownloadSelectedButton();


            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "DocumentViewFiles.zip", 10);
            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);


            string fileName = Path.Combine(DownloadsDirectory + "\\MultipleFormsData.docx");

            var wordCounter = msWordHelper.CountWordsInDocument(fileName, "1997");

            Assert.AreEqual(2, wordCounter);

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3571 - " +
            "Form.PrintFormat = Word & Form.OnClosePrintFormat = Word" +
            "Open Person Record -> Navigate to the Document View section - Select one case form records - Tap on the Download Selected button - " +
            "Unzip the downloaded file - open the extracted docx file - Validate that the file contains data only from the selected case form")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24458")]
        public void Person_DocumentView_UITestMethod05()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.OnClosePrintFormat").FirstOrDefault();
            Guid systemSettingID2 = dbHelper.systemSetting.GetSystemSettingIdByName("Form.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID2, "Word");



            Guid personID = new Guid("576a75a4-67bf-ea11-a2cd-005056926fe4"); //Ms Juliana Cardina
            Guid caseFormID1 = new Guid("97ce567c-51c0-ea11-a2cd-005056926fe4");
            string personNumber = "504085";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()
                .SelectFormDocument(caseFormID1.ToString())
                .ClickDownloadSelectedButton();


            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "DocumentViewFiles.zip", 15);
            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);


            String fileName = Path.Combine(DownloadsDirectory + "\\MultipleFormsData.docx");

            var wordCounter = msWordHelper.CountWordsInDocument(fileName, "1997");

            Assert.AreEqual(1, wordCounter); //only 1 result should be found

        }

        #endregion


        #region Tested in the context of this story : https://advancedcsg.atlassian.net/browse/CDV6-8641


        #region Clear Filters and view all records

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that all Case Form records are displayed (for all case records linked to the person)")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24459")]
        public void Person_DocumentView_UITestMethod06()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin
            Guid caseForm2 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 01/03/2021 created by Security Test User Admin
            Guid caseForm3 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin

            /*CASE 2*/
            Guid caseForm4 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3 for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 27/02/2021 created by Security Test User Admin
            Guid caseForm5 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 28/02/2021 created by Security Test User Admin


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .ValidateFormRecordVisible(caseForm2.ToString())
                .ValidateFormRecordVisible(caseForm3.ToString())
                .ValidateFormRecordVisible(caseForm4.ToString())
                .ValidateFormRecordVisible(caseForm5.ToString())

                .ValidateFormLinkText(caseForm1.ToString(), "Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open")
                .ValidateFormLinkText(caseForm2.ToString(), "Automated UI Test Document 2, Start Date: 01/03/2021, Status: Open")
                .ValidateFormLinkText(caseForm3.ToString(), "Assessment Form, Start Date: 02/03/2021, Status: Closed")
                .ValidateFormLinkText(caseForm4.ToString(), "Automated UI Test Document 3, Start Date: 27/02/2021, Status: Open")
                .ValidateFormLinkText(caseForm5.ToString(), "CIN Plan, Start Date: 28/02/2021, Status: Open")

                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that all Person Form records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24460")]
        public void Person_DocumentView_UITestMethod07()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(personForm1.ToString())
                .ValidateFormRecordVisible(personForm2.ToString())
                .ValidateFormRecordVisible(personForm3.ToString())
                .ValidateFormRecordVisible(personForm4.ToString())

                .ValidateFormLinkText(personForm1.ToString(), "Automation - Rules - Person Form 1, Start Date: 02/03/2021, Status: Open")
                .ValidateFormLinkText(personForm2.ToString(), "Automation - Person Form 1, Start Date: 01/03/2021, Status: Open")
                .ValidateFormLinkText(personForm3.ToString(), "COVID-19, Start Date: 09/02/2021, Status: Open")
                .ValidateFormLinkText(personForm4.ToString(), "CP Review (Person), Start Date: 04/02/2021, Status: Open")
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that all Attachment (For Case) records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24461")]
        public void Person_DocumentView_UITestMethod08()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment7 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton();

            personDocumentViewSubPage
                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString());

            personDocumentViewSubPage
                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString());

            personDocumentViewSubPage
                .ValidateAttachmentLinkText(caseAttachment1.ToString(), "Title: Case 01 Attachment 04, Date: 02/03/2021 17:15:00")
                .ValidateAttachmentLinkText(caseAttachment2.ToString(), "Title: Case 01 Attachment 01, Date: 01/03/2021 09:20:00")
                .ValidateAttachmentLinkText(caseAttachment3.ToString(), "Title: Case 02 Attachment 01, Date: 28/02/2021 08:20:00")
                .ValidateAttachmentLinkText(caseAttachment4.ToString(), "Title: Case 02 Attachment 03, Date: 27/02/2021 08:15:00")
                .ValidateAttachmentLinkText(caseAttachment5.ToString(), "Title: Case 02 Attachment 02, Date: 02/03/2021 13:15:00")
                .ValidateAttachmentLinkText(caseAttachment6.ToString(), "Title: Case 01 Attachment 03, Date: 01/03/2021 09:35:00")
                .ValidateAttachmentLinkText(caseAttachment7.ToString(), "Title: Case 01 Attachment 02, Date: 28/02/2021 07:00:00")
                .ValidateAttachmentLinkText(caseAttachment8.ToString(), "Title: Case 02 Attachment 04, Date: 27/02/2021 06:50:00");
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that all Attachment (For Person) records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24462")]
        public void Person_DocumentView_UITestMethod09()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid caseAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid caseAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid caseAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid caseAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid caseAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid caseAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())

                .ValidateAttachmentLinkText(caseAttachment1.ToString(), "Title: Person Attachment 03, Date: 01/03/2021 14:20:00")
                .ValidateAttachmentLinkText(caseAttachment2.ToString(), "Title: Person Attachment 01, Date: 01/03/2021 07:00:00")
                .ValidateAttachmentLinkText(caseAttachment3.ToString(), "Title: Person Attachment 05, Date: 05/02/2021 00:00:00")

                .ValidateAttachmentLinkText(caseAttachment4.ToString(), "Title: Person Attachment 02, Date: 02/03/2021 10:20:00")
                .ValidateAttachmentLinkText(caseAttachment5.ToString(), "Title: Person Attachment 04, Date: 02/03/2021 08:55:00")
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that Letter (for the Person and Case records) records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24463")]
        public void Person_DocumentView_UITestMethod10()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personLettert4 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Person Letter 04
            Guid case02Lettert2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Case 02 Letter 01
            Guid case02Lettert1 = new Guid("98ff9744-417b-eb11-a313-005056926fe4"); //Case 02 Letter 04
            Guid case01Lettert2 = new Guid("ff1dc61e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 02
            Guid case01Lettert1 = new Guid("dd2e560e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 04
            Guid personLettert3 = new Guid("9673d2e6-407b-eb11-a313-005056926fe4"); //Person Letter 03
            Guid personLettert2 = new Guid("cdb75af0-3f7b-eb11-a313-005056926fe4"); //Person Letter 02

            Guid personLettert4DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");
            Guid case02Lettert2DocumentId = new Guid("337ba249-4cca-ed11-a336-005056926fe4");
            Guid case02Lettert1DocumentId = new Guid("2d7ba249-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert2DocumentId = new Guid("5d9c9433-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert1DocumentId = new Guid("579c9433-4cca-ed11-a336-005056926fe4");
            Guid personLettert3DocumentId = new Guid("38c487de-4bca-ed11-a336-005056926fe4");
            Guid personLettert2DocumentId = new Guid("9a26fee7-4bca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateLetterRecordVisible(personLettert4.ToString(), personLettert4DocumentId.ToString())
                .ValidateLetterRecordVisible(case02Lettert2.ToString(), case02Lettert2DocumentId.ToString())
                .ValidateLetterRecordVisible(case02Lettert1.ToString(), case02Lettert1DocumentId.ToString())
                .ValidateLetterRecordVisible(case01Lettert2.ToString(), case01Lettert2DocumentId.ToString())
                .ValidateLetterRecordVisible(case01Lettert1.ToString(), case01Lettert1DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert3.ToString(), personLettert3DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert2.ToString(), personLettert2DocumentId.ToString())

                .ValidateLetterLinkText(personLettert4.ToString(), "Subject: Person Letter 04, Filename: log (2).txt, Created On: 02/03/2021 14:44:36")
                .ValidateLetterLinkText(case02Lettert2.ToString(), "Subject: Case 02 Letter 02, Filename: log (2).txt, Created On: 02/03/2021 10:23:16")
                .ValidateLetterLinkText(case02Lettert1.ToString(), "Subject: Case 02 Letter 01, Filename: log (2).txt, Created On: 02/03/2021 10:23:02")
                .ValidateLetterLinkText(case01Lettert2.ToString(), "Subject: Case 01 Letter 02, Filename: log (2).txt, Created On: 02/03/2021 10:21:59")
                .ValidateLetterLinkText(case01Lettert1.ToString(), "Subject: Case 01 Letter 01, Filename: log (2).txt, Created On: 02/03/2021 10:21:31")
                .ValidateLetterLinkText(personLettert3.ToString(), "Subject: Person Letter 03, Filename: log (2).txt, Created On: 02/03/2021 10:20:25")
                .ValidateLetterLinkText(personLettert2.ToString(), "Subject: Person Letter 02, Filename: log (2).txt, Created On: 02/03/2021 10:13:32");
        }

        #endregion

        #region Collapse Buttons

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Forms collapse button - Validate that all Case and Person forms get hidden.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24464")]
        public void Person_DocumentView_UITestMethod11()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin
            Guid caseForm2 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 01/03/2021 created by Security Test User Admin
            Guid caseForm3 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin

            /*CASE 2*/
            Guid caseForm4 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3 for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 27/02/2021 created by Security Test User Admin
            Guid caseForm5 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 28/02/2021 created by Security Test User Admin

            /*PERSON */
            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())

                .ClickFormsExpandButton()

                .ValidateFormRecordNotVisible(caseForm1.ToString())
                .ValidateFormRecordNotVisible(caseForm2.ToString())
                .ValidateFormRecordNotVisible(caseForm3.ToString())
                .ValidateFormRecordNotVisible(caseForm4.ToString())
                .ValidateFormRecordNotVisible(caseForm5.ToString())
                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordNotVisible(personForm2.ToString())
                .ValidateFormRecordNotVisible(personForm3.ToString())
                .ValidateFormRecordNotVisible(personForm4.ToString())

                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Case Form collapse button - " +
            "Validate that all Case Forms get hidden - Validate that all Person Forms are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24465")]
        public void Person_DocumentView_UITestMethod12()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin
            Guid caseForm2 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 01/03/2021 created by Security Test User Admin
            Guid caseForm3 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin

            /*CASE 2*/
            Guid caseForm4 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3 for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 27/02/2021 created by Security Test User Admin
            Guid caseForm5 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 28/02/2021 created by Security Test User Admin

            /*PERSON */
            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())

                .ClickCaseFormExpandButton()

                .ValidateFormRecordNotVisible(caseForm1.ToString())
                .ValidateFormRecordNotVisible(caseForm2.ToString())
                .ValidateFormRecordNotVisible(caseForm3.ToString())
                .ValidateFormRecordNotVisible(caseForm4.ToString())
                .ValidateFormRecordNotVisible(caseForm5.ToString())

                .ValidateFormRecordVisible(personForm1.ToString())
                .ValidateFormRecordVisible(personForm2.ToString())
                .ValidateFormRecordVisible(personForm3.ToString())
                .ValidateFormRecordVisible(personForm4.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Person Form collapse button - " +
            "Validate that all Case Forms are visible - Validate that all Person Forms get hidden")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24466")]
        public void Person_DocumentView_UITestMethod13()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin
            Guid caseForm2 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 01/03/2021 created by Security Test User Admin
            Guid caseForm3 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin

            /*CASE 2*/
            Guid caseForm4 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3 for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 27/02/2021 created by Security Test User Admin
            Guid caseForm5 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan for King Britney - (1973-07-21 00:00:00) [QA-CAS-000001-0037820] Starting 28/02/2021 created by Security Test User Admin

            /*PERSON */
            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())

                .ClickPersonFormExpandButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .ValidateFormRecordVisible(caseForm2.ToString())
                .ValidateFormRecordVisible(caseForm3.ToString())
                .ValidateFormRecordVisible(caseForm4.ToString())
                .ValidateFormRecordVisible(caseForm5.ToString())

                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordNotVisible(personForm2.ToString())
                .ValidateFormRecordNotVisible(personForm3.ToString())
                .ValidateFormRecordNotVisible(personForm4.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Attachments collapse button - " +
            "Validate that all Attachments (For Case) and Attachments (For Person) records get hidden ")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24467")]
        public void Person_DocumentView_UITestMethod14()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsExpandButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Attachments (For Case) collapse button - " +
            "Validate that all Attachments (For Case) are hidden and all Attachments (For Person) records are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24468")]
        public void Person_DocumentView_UITestMethod15()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsForCaseExpandButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the 'ALL ATTACHED DOCUMENTS' for Attachments (For Case) collapse button - " +
            "Validate that all Attachments (For Case) with document type set to 'All Attached Documents' are hidden - " +
            "Validate that all other attachments are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24469")]
        public void Person_DocumentView_UITestMethod16()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsForCase_AllAttachedDocumentsExpandButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the 'CLINICAL CHEMISTRY RESULTS' for Attachments (For Case) collapse button - " +
            "Validate that all Attachments (For Case) with document type set to 'Clinical Chemistry Results' are hidden - " +
            "Validate that all other attachments are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24470")]
        public void Person_DocumentView_UITestMethod17()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsForCase_ClinicalChemistryResultsExpandButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Attachments (For Person) collapse button - " +
            "Validate that all Attachments (For Person) are hidden - " +
            "Validate that all other attachments are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24471")]
        public void Person_DocumentView_UITestMethod18()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsForPersonExpandButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the 'ALL ATTACHED DOCUMENTS' for Attachments (For Person) collapse button - " +
            "Validate that all Attachments (For Person) with document type set to 'All Attached Documents' are hidden - " +
            "Validate that all other attachments are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24472")]
        public void Person_DocumentView_UITestMethod19()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickattAchmentsForPerson_AllAttachedDocumentsExpandButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the 'CLINICAL CHEMISTRY RESULTS' for Attachments (For Person) collapse button - " +
            "Validate that all Attachments (For Person) with document type set to 'Clinical Chemistry Results' are hidden - " +
            "Validate that all other attachments are visible")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24473")]
        public void Person_DocumentView_UITestMethod20()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*Attachments for Case*/
            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment7 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            /*Attachments for Person*/
            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04

            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickAttachmentsForPerson_ClinicalChemistryResultsExpandButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on the Letters collapse button - Validate that all Letters are hidden")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24474")]
        public void Person_DocumentView_UITestMethod21()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personLettert4 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Person Letter 04
            Guid case02Lettert2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Case 02 Letter 01
            Guid case02Lettert1 = new Guid("98ff9744-417b-eb11-a313-005056926fe4"); //Case 02 Letter 04
            Guid case01Lettert2 = new Guid("ff1dc61e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 02
            Guid case01Lettert1 = new Guid("dd2e560e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 04
            Guid personLettert3 = new Guid("9673d2e6-407b-eb11-a313-005056926fe4"); //Person Letter 03
            Guid personLettert2 = new Guid("cdb75af0-3f7b-eb11-a313-005056926fe4"); //Person Letter 02

            Guid personLettert4DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");
            Guid case02Lettert2DocumentId = new Guid("337ba249-4cca-ed11-a336-005056926fe4");
            Guid case02Lettert1DocumentId = new Guid("2d7ba249-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert2DocumentId = new Guid("5d9c9433-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert1DocumentId = new Guid("579c9433-4cca-ed11-a336-005056926fe4");
            Guid personLettert3DocumentId = new Guid("38c487de-4bca-ed11-a336-005056926fe4");
            Guid personLettert2DocumentId = new Guid("9a26fee7-4bca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ClickLettersExpandButton()

                .ValidateLetterRecordNotVisible(personLettert4.ToString(), personLettert4DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert2.ToString(), case02Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert1.ToString(), case02Lettert1DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert2.ToString(), case01Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert1.ToString(), case01Lettert1DocumentId.ToString())
                .ValidateLetterRecordNotVisible(personLettert3.ToString(), personLettert3DocumentId.ToString())
                .ValidateLetterRecordNotVisible(personLettert2.ToString(), personLettert2DocumentId.ToString())
                ;
        }

        #endregion

        #region Download Buttons

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on a Case Form download icon - Validate that Print Assessment popup is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24475")]
        public void Person_DocumentView_UITestMethod22()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1 for King, Britney - (21/07/1973) [QA-CAS-000001-009459] Starting 02/03/2021 created by Security Test User Admin


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .ClickFormDownloadIcon(caseForm1.ToString());

            printAssessmentPopup
                .WaitForPrintAssessmentPopupToLoad();
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on a Attachment download icon - Validate that file linked to the attachment record is downloaded")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24476")]
        public void Person_DocumentView_UITestMethod23()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ClickAttachmentDownloadIcon(caseAttachment1DocumentId.ToString());

            System.Threading.Thread.Sleep(3000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "log (2).txt");
            Assert.IsTrue(fileExists);


        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Click on a Letter download icon - Validate that file linked to the Letter record is downloaded")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24477")]
        public void Person_DocumentView_UITestMethod24()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid letter1 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid letter1DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateLetterRecordVisible(letter1.ToString(), letter1DocumentId.ToString())
                .ClickLetterDownloadIcon(letter1DocumentId.ToString());

            System.Threading.Thread.Sleep(3000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "log (2).txt");
            Assert.IsTrue(fileExists);


        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Select two Case Form records - Click on the download selected button - " +
            "Validate that a new zip file is downloaded")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24478")]
        public void Person_DocumentView_UITestMethod25()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open
            Guid caseForm2 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form, Start Date: 02/03/2021, Status: Closed


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .SelectFormDocument(caseForm1.ToString())
                .SelectFormDocument(caseForm2.ToString())
                .ClickDownloadSelectedButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "DocumentViewFiles.zip", 30);
            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "MultipleFormsData.*");
            Assert.IsTrue(fileExists);
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Select two Attachment records - Click on the download selected button - " +
            "Validate that a new zip file is downloaded")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24479")]
        public void Person_DocumentView_UITestMethod26()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid attachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Title: Case 01 Attachment 04, Date: 02/03/2021
            Guid attachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Title: Case 01 Attachment 01, Date: 01/03/2021

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .SelectAttachmentDocument(attachment1.ToString())
                .SelectAttachmentDocument(attachment2.ToString())
                .ClickDownloadSelectedButton();

            System.Threading.Thread.Sleep(4000);
            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "8555930a-3f7b-eb11-a313-005056926fe4_log (2).txt");
            Assert.IsTrue(fileExists);
            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "log (2).txt");
            Assert.IsTrue(fileExists);
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Wait for all records to be loaded - Select two Letter records - Click on the download selected button - " +
            "Validate that a new zip file is downloaded")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24480")]
        public void Person_DocumentView_UITestMethod27()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid letter1 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Subject: Person Letter 04, Filename: BulkCreate-configuration-202008031105.txt, Created On: 02/03/2021 14:44:36
            Guid letter2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Subject: Case 02 Letter 02, Filename: CD-V6-PerformanceRequirement ChecklistV1.0.xlsx, Created On: 02/03/2021 10:23:16

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .SelectLetterDocument(letter1.ToString())
                .SelectLetterDocument(letter2.ToString())
                .ClickDownloadSelectedButton();

            System.Threading.Thread.Sleep(4000);
            fileIOHelper.UnzipFile(this.DownloadsDirectory + "\\DocumentViewFiles.zip", this.DownloadsDirectory);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "aff2b64c-417b-eb11-a313-005056926fe4_log (2).txt");
            Assert.IsTrue(fileExists);
            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "log (2).txt");
            Assert.IsTrue(fileExists);
        }

        #endregion

        #region Search

        #region Common Filters

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Insert a Date From and Date To - Tap on the Search button - " +
            "Validate that only the Case Form records that match the inserted dates are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24481")]
        public void Person_DocumentView_UITestMethod28()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open
            Guid caseForm2 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form, Start Date: 02/03/2021, Status: Closed
            Guid caseForm3 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2, Start Date: 01/03/2021, Status: Open
            Guid caseForm4 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan, Start Date: 28/02/2021, Status: Open
            Guid caseForm5 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3, Start Date: 27/02/2021, Status: Open

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .InsertDateFrom("28/02/2021")
                .InsertDateTo("01/03/2021")
                .ClickSearchButton()

                .ValidateFormRecordNotVisible(caseForm1.ToString())
                .ValidateFormRecordNotVisible(caseForm2.ToString())
                .ValidateFormRecordVisible(caseForm3.ToString())
                .ValidateFormRecordVisible(caseForm4.ToString())
                .ValidateFormRecordNotVisible(caseForm5.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Insert a Date From and Date To - Tap on the Search button - " +
            "Validate that only the Person Form records that match the inserted dates are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24482")]
        public void Person_DocumentView_UITestMethod29()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .InsertDateFrom("09/02/2021")
                .InsertDateTo("01/03/2021")
                .ClickSearchButton()

                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordVisible(personForm2.ToString())
                .ValidateFormRecordVisible(personForm3.ToString())
                .ValidateFormRecordNotVisible(personForm4.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Insert a Date From and Date To - Tap on the Search button - " +
            "Validate that only the Attachment (For Case) records that match the inserted dates are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24483")]
        public void Person_DocumentView_UITestMethod30()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment7 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .InsertDateFrom("28/02/2021")
                .InsertDateTo("01/03/2021")
                .ClickSearchButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Insert a Date From and Date To - Tap on the Search button - " +
            "Validate that only the Attachment (For Person) records that match the inserted dates are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24484")]
        public void Person_DocumentView_UITestMethod31()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid caseAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid caseAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 05

            Guid caseAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid caseAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid caseAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid caseAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .InsertDateFrom("01/03/2021")
                .InsertDateTo("02/03/2021")
                .ClickSearchButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Insert a Date From and Date To - Tap on the Search button - " +
            "Validate that only the Letter records that match the inserted dates are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24485")]
        public void Person_DocumentView_UITestMethod32()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personLettert5 = new Guid("b1636da8-397c-eb11-a314-005056926fe4"); //Person Letter 05
            Guid personLettert4 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Person Letter 04
            Guid case02Lettert2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Case 02 Letter 01
            Guid case02Lettert1 = new Guid("98ff9744-417b-eb11-a313-005056926fe4"); //Case 02 Letter 04
            Guid case01Lettert2 = new Guid("ff1dc61e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 02
            Guid case01Lettert1 = new Guid("dd2e560e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 04
            Guid personLettert3 = new Guid("9673d2e6-407b-eb11-a313-005056926fe4"); //Person Letter 03
            Guid personLettert2 = new Guid("cdb75af0-3f7b-eb11-a313-005056926fe4"); //Person Letter 02

            Guid personLettert5DocumentId = new Guid("31c060be-397c-eb11-a314-005056926fe4");
            Guid personLettert4DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");
            Guid case02Lettert2DocumentId = new Guid("337ba249-4cca-ed11-a336-005056926fe4");
            Guid case02Lettert1DocumentId = new Guid("2d7ba249-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert2DocumentId = new Guid("5d9c9433-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert1DocumentId = new Guid("579c9433-4cca-ed11-a336-005056926fe4");
            Guid personLettert3DocumentId = new Guid("38c487de-4bca-ed11-a336-005056926fe4");
            Guid personLettert2DocumentId = new Guid("9a26fee7-4bca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .InsertDateFrom("01/03/2021")
                .InsertDateTo("02/03/2021")
                .ClickSearchButton()

                .ValidateLetterRecordNotVisible(personLettert5.ToString(), personLettert5DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert4.ToString(), personLettert4DocumentId.ToString())
                .ValidateLetterRecordVisible(case02Lettert2.ToString(), case02Lettert2DocumentId.ToString())
                .ValidateLetterRecordVisible(case02Lettert1.ToString(), case02Lettert1DocumentId.ToString())
                .ValidateLetterRecordVisible(case01Lettert2.ToString(), case01Lettert2DocumentId.ToString())
                .ValidateLetterRecordVisible(case01Lettert1.ToString(), case01Lettert1DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert3.ToString(), personLettert3DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert2.ToString(), personLettert2DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set 'Profession Type' to Doctor - Tap on the Search button - " +
            "Validate that only the Form records created by a user with profession type set to 'Doctor' are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24486")]
        public void Person_DocumentView_UITestMethod33()
        {
            Guid professionType = new Guid("961f03e7-6f3a-e911-a2c5-005056926fe4"); //Doctor

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickProfessionTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Doctor").TapSearchButton().SelectResultElement(professionType.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordNotVisible(personForm2.ToString())
                .ValidateFormRecordNotVisible(personForm3.ToString())
                .ValidateFormRecordVisible(personForm4.ToString())

                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set 'Profession Type' to Doctor - Tap on the Search button - " +
            "Validate that only the Attachment records created by a user with profession type set to 'Doctor' are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24487")]
        public void Person_DocumentView_UITestMethod34()
        {
            Guid professionType = new Guid("961f03e7-6f3a-e911-a2c5-005056926fe4"); //Doctor

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid caseAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid caseAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 05
            Guid caseAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid caseAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid caseAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickProfessionTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Doctor").TapSearchButton().SelectResultElement(professionType.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set 'Profession Type' to Doctor - Tap on the Search button - " +
            "Validate that only the Letter records created by a user with profession type set to 'Doctor' are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24488")]
        public void Person_DocumentView_UITestMethod35()
        {
            Guid professionType = new Guid("961f03e7-6f3a-e911-a2c5-005056926fe4"); //Doctor

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personLettert5 = new Guid("b1636da8-397c-eb11-a314-005056926fe4"); //Person Letter 05
            Guid personLettert4 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Person Letter 04
            Guid case02Lettert2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Case 02 Letter 01
            Guid case02Lettert1 = new Guid("98ff9744-417b-eb11-a313-005056926fe4"); //Case 02 Letter 04
            Guid case01Lettert2 = new Guid("ff1dc61e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 02
            Guid case01Lettert1 = new Guid("dd2e560e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 04
            Guid personLettert3 = new Guid("9673d2e6-407b-eb11-a313-005056926fe4"); //Person Letter 03
            Guid personLettert2 = new Guid("cdb75af0-3f7b-eb11-a313-005056926fe4"); //Person Letter 02

            Guid personLettert5DocumentId = new Guid("22f016ef-4bca-ed11-a336-005056926fe4");
            Guid personLettert4DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");
            Guid case02Lettert2DocumentId = new Guid("337ba249-4cca-ed11-a336-005056926fe4");
            Guid case02Lettert1DocumentId = new Guid("2d7ba249-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert2DocumentId = new Guid("5d9c9433-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert1DocumentId = new Guid("579c9433-4cca-ed11-a336-005056926fe4");
            Guid personLettert3DocumentId = new Guid("38c487de-4bca-ed11-a336-005056926fe4");
            Guid personLettert2DocumentId = new Guid("9a26fee7-4bca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickProfessionTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Doctor").TapSearchButton().SelectResultElement(professionType.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateLetterRecordVisible(personLettert5.ToString(), personLettert5DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert4.ToString(), personLettert4DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert2.ToString(), case02Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert1.ToString(), case02Lettert1DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert2.ToString(), case01Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert1.ToString(), case01Lettert1DocumentId.ToString())
                .ValidateLetterRecordNotVisible(personLettert3.ToString(), personLettert3DocumentId.ToString())
                .ValidateLetterRecordNotVisible(personLettert2.ToString(), personLettert2DocumentId.ToString())
                ;
        }

        #endregion

        #region Form Filters

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set Document Category to 'Case Form' - Tap on the Search button - " +
            "Validate that only the Case Form records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24489")]
        public void Person_DocumentView_UITestMethod36()
        {
            Guid documentCategoryID = new Guid("50820cc3-f8d6-e111-807c-00155d16010a"); //Case Form

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open
            Guid caseForm2 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form, Start Date: 02/03/2021, Status: Closed
            Guid caseForm3 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2, Start Date: 01/03/2021, Status: Open
            Guid caseForm4 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan, Start Date: 28/02/2021, Status: Open
            Guid caseForm5 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3, Start Date: 27/02/2021, Status: Open

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickFormFiltersTitle()
                .ClickDocumentCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(documentCategoryID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .ValidateFormRecordVisible(caseForm2.ToString())
                .ValidateFormRecordVisible(caseForm3.ToString())
                .ValidateFormRecordVisible(caseForm4.ToString())
                .ValidateFormRecordVisible(caseForm5.ToString())

                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordNotVisible(personForm2.ToString())
                .ValidateFormRecordNotVisible(personForm3.ToString())
                .ValidateFormRecordNotVisible(personForm4.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set Document Category to 'Person Form' - Tap on the Search button - " +
            "Validate that only the Person Form records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24490")]
        public void Person_DocumentView_UITestMethod37()
        {
            Guid documentCategoryID = new Guid("75a232e1-9bed-e811-80dc-0050560502cc"); //Person Form

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open
            Guid caseForm2 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form, Start Date: 02/03/2021, Status: Closed
            Guid caseForm3 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2, Start Date: 01/03/2021, Status: Open
            Guid caseForm4 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan, Start Date: 28/02/2021, Status: Open
            Guid caseForm5 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3, Start Date: 27/02/2021, Status: Open

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickFormFiltersTitle()
                .ClickDocumentCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(documentCategoryID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateFormRecordNotVisible(caseForm1.ToString())
                .ValidateFormRecordNotVisible(caseForm2.ToString())
                .ValidateFormRecordNotVisible(caseForm3.ToString())
                .ValidateFormRecordNotVisible(caseForm4.ToString())
                .ValidateFormRecordNotVisible(caseForm5.ToString())

                .ValidateFormRecordVisible(personForm1.ToString())
                .ValidateFormRecordVisible(personForm2.ToString())
                .ValidateFormRecordVisible(personForm3.ToString())
                .ValidateFormRecordVisible(personForm4.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Set Document to 'Automated UI Test Document 1' - Tap on the Search button - " +
            "Validate that only Form records that match the document are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24491")]
        public void Person_DocumentView_UITestMethod38()
        {
            Guid documentCategoryID = new Guid("9290d446-3da9-e911-a2c6-005056926fe4"); //Automated UI Test Document 1

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid caseForm1 = new Guid("c64f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 1, Start Date: 02/03/2021, Status: Open
            Guid caseForm2 = new Guid("d7a07e73-457b-eb11-a313-005056926fe4"); //Assessment Form, Start Date: 02/03/2021, Status: Closed
            Guid caseForm3 = new Guid("d24f1f56-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 2, Start Date: 01/03/2021, Status: Open
            Guid caseForm4 = new Guid("7458688c-3f7b-eb11-a313-005056926fe4"); //CIN Plan, Start Date: 28/02/2021, Status: Open
            Guid caseForm5 = new Guid("08793d84-3f7b-eb11-a313-005056926fe4"); //Automated UI Test Document 3, Start Date: 27/02/2021, Status: Open

            Guid personForm1 = new Guid("14b3005f-3e7b-eb11-a313-005056926fe4"); //Automation - Rules - Person Form 1 for Britney King Starting 02/03/2021 created by Security Test User Admin
            Guid personForm2 = new Guid("5c713b58-3e7b-eb11-a313-005056926fe4"); //Automation - Person Form 1 for Britney King Starting 01/03/2021 created by Security Test User Admin
            Guid personForm3 = new Guid("881d4197-437b-eb11-a313-005056926fe4"); //COVID-19 for Britney King Starting 09/02/2021 created by Security Test User Admin
            Guid personForm4 = new Guid("52882c99-657b-eb11-a313-005056926fe4"); //CP Review (Person) for Britney King Starting 04/02/2021 created by José Brazeta

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickFormFiltersTitle()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document 1").TapSearchButton().SelectResultElement(documentCategoryID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateFormRecordVisible(caseForm1.ToString())
                .ValidateFormRecordNotVisible(caseForm2.ToString())
                .ValidateFormRecordNotVisible(caseForm3.ToString())
                .ValidateFormRecordNotVisible(caseForm4.ToString())
                .ValidateFormRecordNotVisible(caseForm5.ToString())

                .ValidateFormRecordNotVisible(personForm1.ToString())
                .ValidateFormRecordNotVisible(personForm2.ToString())
                .ValidateFormRecordNotVisible(personForm3.ToString())
                .ValidateFormRecordNotVisible(personForm4.ToString())
                ;
        }

        #endregion

        #region Attachment Filters

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Expand the Attachment Filters area - Set a value in the Title search field - Tap on the Search button - " +
            "Validate that only the Case Attachment records that match the title are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24492")]
        public void Person_DocumentView_UITestMethod39()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment7 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickAttachmentFiltersTitle()
                .InsertAttachmentTitle("Case 01")
                .ClickSearchButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Expand the Attachment Filters area - Set a value in the Title search field - Tap on the Search button - " +
            "Validate that only the Person Attachment records that match the title are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24493")]
        public void Person_DocumentView_UITestMethod40()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid caseAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid caseAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 05
            Guid caseAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid caseAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid caseAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid caseAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickAttachmentFiltersTitle()
                .InsertAttachmentTitle("Person Attachment 01")
                .ClickSearchButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Expand the Attachment Filters area - Set a Document Type to 'All Attached Documents' - Tap on the Search button - " +
            "Validate that only the Attachment records that match the Document Type are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24494")]
        public void Person_DocumentView_UITestMethod41()
        {
            Guid documentTypeID = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment7 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 05

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickAttachmentFiltersTitle()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateAttachmentRecordVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Expand the Attachment Filters area - Set a Document Sub Type to 'Confirmed' - Tap on the Search button - " +
            "Validate that only the Attachment records that match the Document Sub Type are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24495")]
        public void Person_DocumentView_UITestMethod42()
        {
            Guid documentTypeID = new Guid("a1451ca0-70f6-e911-a2c7-005056926fe4"); //Confirmed

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid caseAttachment1 = new Guid("9804f938-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 04
            Guid caseAttachment2 = new Guid("8555930a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 01
            Guid caseAttachment3 = new Guid("f716569b-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 01
            Guid caseAttachment4 = new Guid("813bc7af-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 03

            Guid caseAttachment5 = new Guid("cf21c3a1-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 02
            Guid caseAttachment6 = new Guid("95276929-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 03
            Guid caseAttachment7 = new Guid("56679a1a-3f7b-eb11-a313-005056926fe4"); //Case 01 Attachment 02
            Guid caseAttachment8 = new Guid("2d0c09b9-3f7b-eb11-a313-005056926fe4"); //Case 02 Attachment 04

            Guid caseAttachment1DocumentId = new Guid("927ae630-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment2DocumentId = new Guid("6acd3c23-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment3DocumentId = new Guid("0bf8d548-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment4DocumentId = new Guid("8cdd284f-3dca-ed11-a336-005056926fe4");

            Guid caseAttachment5DocumentId = new Guid("85dd284f-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment6DocumentId = new Guid("d20c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment7DocumentId = new Guid("b70c6b2a-3dca-ed11-a336-005056926fe4");
            Guid caseAttachment8DocumentId = new Guid("154ac355-3dca-ed11-a336-005056926fe4");


            Guid personAttachment1 = new Guid("9a1cb7cd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 03
            Guid personAttachment2 = new Guid("15ce1b9b-3e7b-eb11-a313-005056926fe4"); //Person Attachment 01
            Guid personAttachment3 = new Guid("0ffeadb8-657b-eb11-a313-005056926fe4"); //Person Attachment 05

            Guid personAttachment4 = new Guid("7f1ff1a7-3e7b-eb11-a313-005056926fe4"); //Person Attachment 02
            Guid personAttachment5 = new Guid("c4decedd-3e7b-eb11-a313-005056926fe4"); //Person Attachment 04


            Guid personAttachment1DocumentId = new Guid("8799ff60-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment2DocumentId = new Guid("a62ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment3DocumentId = new Guid("f1ce19fa-49ca-ed11-a336-005056926fe4"); //

            Guid personAttachment4DocumentId = new Guid("ac2ae15a-49ca-ed11-a336-005056926fe4"); //
            Guid personAttachment5DocumentId = new Guid("37d8ef67-49ca-ed11-a336-005056926fe4"); //


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickAttachmentFiltersTitle()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Confirmed").TapSearchButton().SelectResultElement(documentTypeID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton()

                .ValidateAttachmentRecordNotVisible(caseAttachment1.ToString(), caseAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment2.ToString(), caseAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment3.ToString(), caseAttachment3DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment4.ToString(), caseAttachment4DocumentId.ToString())

                .ValidateAttachmentRecordVisible(caseAttachment5.ToString(), caseAttachment5DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(caseAttachment6.ToString(), caseAttachment6DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment7.ToString(), caseAttachment7DocumentId.ToString())
                .ValidateAttachmentRecordVisible(caseAttachment8.ToString(), caseAttachment8DocumentId.ToString())

                .ValidateAttachmentRecordNotVisible(personAttachment1.ToString(), personAttachment1DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment2.ToString(), personAttachment2DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment3.ToString(), personAttachment3DocumentId.ToString())

                .ValidateAttachmentRecordVisible(personAttachment4.ToString(), personAttachment4DocumentId.ToString())
                .ValidateAttachmentRecordNotVisible(personAttachment5.ToString(), personAttachment5DocumentId.ToString())
                ;
        }

        #endregion

        #region Letter Filters

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - " +
            "Expand the Letter Filters area - Set a value in the Title search field - Tap on the Search button - " +
            "Validate that only the Letter records that match the title are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24496")]
        public void Person_DocumentView_UITestMethod43()
        {
            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            Guid personLettert5 = new Guid("b1636da8-397c-eb11-a314-005056926fe4"); //Person Letter 05
            Guid personLettert4 = new Guid("9b91dace-657b-eb11-a313-005056926fe4"); //Person Letter 04
            Guid case02Lettert2 = new Guid("aff2b64c-417b-eb11-a313-005056926fe4"); //Case 02 Letter 01
            Guid case02Lettert1 = new Guid("98ff9744-417b-eb11-a313-005056926fe4"); //Case 02 Letter 04
            Guid case01Lettert2 = new Guid("ff1dc61e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 02
            Guid case01Lettert1 = new Guid("dd2e560e-417b-eb11-a313-005056926fe4"); //Case 01 Letter 04
            Guid personLettert3 = new Guid("9673d2e6-407b-eb11-a313-005056926fe4"); //Person Letter 03
            Guid personLettert2 = new Guid("cdb75af0-3f7b-eb11-a313-005056926fe4"); //Person Letter 02

            Guid personLettert5DocumentId = new Guid("22f016ef-4bca-ed11-a336-005056926fe4");
            Guid personLettert4DocumentId = new Guid("a026fee7-4bca-ed11-a336-005056926fe4");
            Guid case02Lettert2DocumentId = new Guid("337ba249-4cca-ed11-a336-005056926fe4");
            Guid case02Lettert1DocumentId = new Guid("2d7ba249-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert2DocumentId = new Guid("5d9c9433-4cca-ed11-a336-005056926fe4");
            Guid case01Lettert1DocumentId = new Guid("579c9433-4cca-ed11-a336-005056926fe4");
            Guid personLettert3DocumentId = new Guid("38c487de-4bca-ed11-a336-005056926fe4");
            Guid personLettert2DocumentId = new Guid("9a26fee7-4bca-ed11-a336-005056926fe4");


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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickLetterFiltersTitle()
                .InsertLetterTitleField("Person Letter")
                .ClickSearchButton()

                .ValidateLetterRecordVisible(personLettert5.ToString(), personLettert5DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert4.ToString(), personLettert4DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert2.ToString(), case02Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case02Lettert1.ToString(), case02Lettert1DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert2.ToString(), case01Lettert2DocumentId.ToString())
                .ValidateLetterRecordNotVisible(case01Lettert1.ToString(), case01Lettert1DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert3.ToString(), personLettert3DocumentId.ToString())
                .ValidateLetterRecordVisible(personLettert2.ToString(), personLettert2DocumentId.ToString())
                ;
        }

        #endregion

        #endregion

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that Forms with Status = Cancelled will not be included in Document View")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24497")]
        public void Person_DocumentView_UITestMethod44()
        {
            Guid documentCategoryID = new Guid("50820cc3-f8d6-e111-807c-00155d16010a"); //Case Form

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid personForm1 = new Guid("7d085b71-427b-eb11-a313-005056926fe4"); //Ad-hoc Visit Record

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormRecordNotVisible(personForm1.ToString())
                ;
        }

        [Description("Open a person record - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that Letters with File = Null are not included in Document View.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24498")]
        public void Person_DocumentView_UITestMethod45()
        {
            Guid documentCategoryID = new Guid("50820cc3-f8d6-e111-807c-00155d16010a"); //Case Form

            Guid personID = new Guid("30d4363c-c581-4b73-bfbb-652dbb13c4cd"); //Britney King
            string personNumber = "18903";

            /*CASE 1*/
            Guid letter1 = new Guid("d5d527e1-3f7b-eb11-a313-005056926fe4"); //Person Letter 01

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickSearchButton();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ValidateLetterRecordNotVisible(letter1.ToString(), "...")
                ;
        }

        [Description("Open a person record (no data to be displayed in the document view) - Navigate to the Document View page - Tap on the Clear Filters button - Tap on the Search button - " +
            "Validate that the no records messages are displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24499")]
        public void Person_DocumentView_UITestMethod46()
        {
            Guid personID = new Guid("6b65a724-41bb-4c78-a962-0ba319e5ca1a"); //Lupe Garrison
            string personNumber = "18904";

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
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()

                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateFormNoRecordsPresentMessageVisible()
                .ValidateAttachmentsNoRecordsPresentMessageVisible()
                .ValidateLettersNoRecordsPresentMessageVisible()
                ;
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/ACC-670

        [TestProperty("JiraIssueID", "CDV6-2572")]
        [Description("Validate system displays the Form section is filtered by Document Category, the Person (Form) and Case (Form) records." +
                     "Records are sorted by Start Date latest to oldest in the Document view custom screen.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt")]
        public void Person_DocumentView_UITestMethod47()
        {

            var past_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var current_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;

            #region Document Category

            Guid case_documentCategoryID = dbHelper.documentCategory.GetByName("Case Form").FirstOrDefault();
            Guid person_documentCategoryID = dbHelper.documentCategory.GetByName("Person Form").FirstOrDefault();

            #endregion

            #region Person

            var firstName = "Automation";
            var lastName = _currentDateSuffix;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId, _defaultLoginUserID, _defaultLoginUserID, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form & Case Form Attachment

            Guid caseFormId = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, current_Date);
            Guid caseFormId2 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, past_Date);

            dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form First", past_Date, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form Second", current_Date, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");

            #endregion

            #region Person Form & Person Form Attachment

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, current_Date);
            var personFormId2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, past_Date);

            dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form First", past_Date, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form Second", current_Date, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            #endregion

            #region Step 3

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            #endregion

            #region Step 4

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton();

            #endregion

            #region Step 5

            personDocumentViewSubPage
                .ClickFormFiltersTitle()
                .ClickDocumentCategoryLookupButton();

            #endregion

            #region Step 6

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(case_documentCategoryID.ToString())
                .ValidateResultElementPresent(person_documentCategoryID.ToString())
                .SelectResultElement(person_documentCategoryID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton();

            personDocumentViewSubPage
                .ValidateFormRecordVisible(personFormId.ToString())
                .ValidateFormRecordVisible(personFormId2.ToString())

                .ValidateFormRecordNotVisible(caseFormId.ToString())
                .ValidateFormRecordNotVisible(caseFormId2.ToString());

            personDocumentViewSubPage
                .ValidatePerson_CaseFormRecordPosition(1, personFormId.ToString())
                .ValidatePerson_CaseFormRecordPosition(2, personFormId2.ToString())
                .ClickDocumentCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(case_documentCategoryID.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton();

            personDocumentViewSubPage
                .ValidateFormRecordNotVisible(personFormId.ToString())
                .ValidateFormRecordNotVisible(personFormId2.ToString())

                .ValidateFormRecordVisible(caseFormId.ToString())
                .ValidateFormRecordVisible(caseFormId2.ToString());

            personDocumentViewSubPage
                .ValidatePerson_CaseFormRecordPosition(1, caseFormId.ToString())
                .ValidatePerson_CaseFormRecordPosition(2, caseFormId2.ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-674

        [TestProperty("JiraIssueID", "CDV6-2580")]
        [Description("Validate system displays the Form section is filtered by Document Category, the Person (Form) and Case (Form) records." +
                     "Records are sorted by Start Date latest to oldest in the Document view custom screen.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt")]
        public void Person_DocumentView_UITestMethod48()
        {
            var past_Date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5)).Date;
            var past_Date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10)).Date;
            var past_Date3 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3)).Date;
            var current_Date = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;

            #region Person

            var firstName = "Automation";
            var lastName = _currentDateSuffix;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId, _defaultLoginUserID, _defaultLoginUserID, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form & Case Form Attachment

            Guid caseFormId = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, current_Date);

            var caseFormAttachmentId1 = dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form First", past_Date1, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            var caseFormAttachmentId2 = dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form Second", past_Date2, _attachDocumentTypeId2, _attachDocumentSubTypeId2, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");
            var caseFormAttachmentId3 = dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form Third", past_Date3, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            var caseFormAttachmentId4 = dbHelper.caseFormAttachment.CreateCaseFormAttachment(_careDirectorQA_TeamId, _personId, _caseId, caseFormId, "Case Form Fourth", current_Date, _attachDocumentTypeId2, _attachDocumentSubTypeId2, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");

            var caseFormAttachment_FileId1 = dbHelper.caseFormAttachment.GetByID(caseFormAttachmentId1, "fileId");
            var caseFormAttachment_FileId2 = dbHelper.caseFormAttachment.GetByID(caseFormAttachmentId2, "fileId");
            var caseFormAttachment_FileId3 = dbHelper.caseFormAttachment.GetByID(caseFormAttachmentId3, "fileId");
            var caseFormAttachment_FileId4 = dbHelper.caseFormAttachment.GetByID(caseFormAttachmentId4, "fileId");

            #endregion

            #region Person Form & Person Form Attachment

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _documentId, past_Date1);

            var personFormAttachmentId1 = dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form First", past_Date1, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            var personFormAttachmentId2 = dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form Second", past_Date2, _attachDocumentTypeId2, _attachDocumentSubTypeId2, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");
            var personFormAttachmentId3 = dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form Third", past_Date3, _attachDocumentTypeId, _attachDocumentSubTypeId, TestContext.DeploymentDirectory + "\\Document.txt", "Document");
            var personFormAttachmentId4 = dbHelper.personFormAttachment.CreatePersonFormAttachment(_careDirectorQA_TeamId, _personId, personFormId, "Person Form Fourth", current_Date, _attachDocumentTypeId2, _attachDocumentSubTypeId2, TestContext.DeploymentDirectory + "\\Document2.txt", "Document2");

            var personFormAttachment_FileId1 = dbHelper.personFormAttachment.GetByID(personFormAttachmentId1, "fileId");
            var personFormAttachment_FileId2 = dbHelper.personFormAttachment.GetByID(personFormAttachmentId2, "fileId");
            var personFormAttachment_FileId3 = dbHelper.personFormAttachment.GetByID(personFormAttachmentId3, "fileId");
            var personFormAttachment_FileId4 = dbHelper.personFormAttachment.GetByID(personFormAttachmentId4, "fileId");

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            #endregion

            #region Step 3

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            #endregion

            #region Step 4

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton();

            #endregion

            #region Step 5 & 6

            personDocumentViewSubPage
                .ClickAttachmentFiltersTitle()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(_attachDocumentTypeId.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton();

            personDocumentViewSubPage
                .ValidateAttachmentRecordVisible(caseFormAttachmentId1.ToString(), caseFormAttachment_FileId1["fileid"].ToString())
                .ValidateAttachmentRecordVisible(caseFormAttachmentId3.ToString(), caseFormAttachment_FileId3["fileid"].ToString())

                .ValidateAttachmentRecordVisible(personFormAttachmentId1.ToString(), personFormAttachment_FileId1["fileid"].ToString())
                .ValidateAttachmentRecordVisible(personFormAttachmentId3.ToString(), personFormAttachment_FileId3["fileid"].ToString());

            personDocumentViewSubPage
                .ValidateAttachmentRecordNotVisible(caseFormAttachmentId2.ToString(), caseFormAttachment_FileId2["fileid"].ToString())
                .ValidateAttachmentRecordNotVisible(caseFormAttachmentId4.ToString(), caseFormAttachment_FileId4["fileid"].ToString())

                .ValidateAttachmentRecordNotVisible(personFormAttachmentId2.ToString(), personFormAttachment_FileId2["fileid"].ToString())
                .ValidateAttachmentRecordNotVisible(personFormAttachmentId4.ToString(), personFormAttachment_FileId4["fileid"].ToString());

            personDocumentViewSubPage
               .ValidateCaseFormAttachedDocumentPosition(1, caseFormAttachmentId3.ToString())
               .ValidateCaseFormAttachedDocumentPosition(2, caseFormAttachmentId1.ToString())

               .ValidatePersonFormAttachedDocumentPosition(1, personFormAttachmentId3.ToString())
               .ValidatePersonFormAttachedDocumentPosition(2, personFormAttachmentId1.ToString());

            personDocumentViewSubPage
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Attached Doc View").TapSearchButton().SelectResultElement(_attachDocumentTypeId2.ToString());

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickSearchButton();

            personDocumentViewSubPage
                .ValidateAttachmentRecordNotVisible(caseFormAttachmentId1.ToString(), caseFormAttachment_FileId1["fileid"].ToString())
                .ValidateAttachmentRecordNotVisible(caseFormAttachmentId3.ToString(), caseFormAttachment_FileId3["fileid"].ToString())

                .ValidateAttachmentRecordNotVisible(personFormAttachmentId1.ToString(), personFormAttachment_FileId1["fileid"].ToString())
                .ValidateAttachmentRecordNotVisible(personFormAttachmentId3.ToString(), personFormAttachment_FileId3["fileid"].ToString());

            personDocumentViewSubPage
                .ValidateAttachmentRecordVisible(caseFormAttachmentId2.ToString(), caseFormAttachment_FileId2["fileid"].ToString())
                .ValidateAttachmentRecordVisible(caseFormAttachmentId4.ToString(), caseFormAttachment_FileId4["fileid"].ToString())

                .ValidateAttachmentRecordVisible(personFormAttachmentId2.ToString(), personFormAttachment_FileId2["fileid"].ToString())
                .ValidateAttachmentRecordVisible(personFormAttachmentId4.ToString(), personFormAttachment_FileId4["fileid"].ToString());

            personDocumentViewSubPage
               .ValidateCaseFormAttachedDocumentPosition(1, caseFormAttachmentId4.ToString())
               .ValidateCaseFormAttachedDocumentPosition(2, caseFormAttachmentId2.ToString())

               .ValidatePersonFormAttachedDocumentPosition(1, personFormAttachmentId4.ToString())
               .ValidatePersonFormAttachedDocumentPosition(2, personFormAttachmentId2.ToString());

            #endregion
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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    [TestClass]
    public class SmokeTests_Level2_UITestCases : FunctionalTest
    {
        #region Properties

        private string environmentName;
        private bool bedManagementModuleInactive;
        private bool communityClinicModuleInactive;
        private bool socialCareCaseModuleInactive;

        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _AutomationSmokeTestUser1_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _attachDocumentTypeId;
        private Guid _attachDocumentSubTypeId;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        //private Guid _personCaseNoteBusinessObjectId;
        //private Guid _workflowId;
        //private string _workflowXml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"6cc64203-c011-48bd-9814-4ce9d37960be\" ProcessOrder=\"0\">          <GroupCondition Id=\"2eadb8e2-0029-4f06-b2a5-c9567cc4c0b0\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [SmokeTest_Workflow_CDV6_13736_420623e0]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"f206a28f-da7b-424b-b07c-ebc5ca02aabd\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"SmokeTest_Workflow_CDV6_13736_420623e0\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"eab5c154-c0fd-4058-a998-3505d0b1d7e5\" Name=\"Update Record: 'Person Case Note'\" CustomTitle=\"false\" ActionType=\"2\" BusinessObject=\"BusinessObjectIdToReplaceHere\">                <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">                  <WorkflowActionValue ValueCustom=\"Smoke Test - Workflow CDV6-13736 Activated\" />                </WorkflowActionField>              </WorkflowAction>            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _caseStatusId;
        private Guid _caseId;
        private string _caseNumber;

        #endregion

        [TestInitialize()]
        public void SmokeTests_SetupTest()
        {
            //try
            //{

            #region Environment

            environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            #endregion

            #region Business Modules

            var bedManagementModuleId = dbHelper.businessModule.GetBusinessModuleByName("Bed Management")[0];
            bedManagementModuleInactive = (bool)(dbHelper.businessModule.GetBusinessModuleByID(bedManagementModuleId, "inactive")["inactive"]);

            var communityClinicModuleId = dbHelper.businessModule.GetBusinessModuleByName("Community/Clinic")[0];
            communityClinicModuleInactive = (bool)(dbHelper.businessModule.GetBusinessModuleByID(communityClinicModuleId, "inactive")["inactive"]);

            var socialCareCaseModuleId = dbHelper.businessModule.GetBusinessModuleByName("Social Care Case")[0];
            socialCareCaseModuleInactive = (bool)(dbHelper.businessModule.GetBusinessModuleByID(socialCareCaseModuleId, "inactive")["inactive"]);

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region System User

            var automationSmokeTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_Smoke_Test_User_1").Any();
            if (!automationSmokeTestUser1Exists)
            {
                _AutomationSmokeTestUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_Smoke_Test_User_1", "Automation", "Smoke Test User 1", "Automation Smoke Test User 1", "Passw0rd_!", "Automation_Smoke_Test_User_1@somemail.com", "Automation_Smoke_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationSmokeTestUser1_SystemUserId, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationSmokeTestUser1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
            }
            if (_AutomationSmokeTestUser1_SystemUserId == Guid.Empty)
            {
                _AutomationSmokeTestUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_Smoke_Test_User_1").FirstOrDefault();
            }

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_AutomationSmokeTestUser1_SystemUserId, DateTime.Now.Date);

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "SmokeTest_Ethnicity", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity")[0];

            #endregion



            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("SmokeTest_DocumentType").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "SmokeTest_DocumentType", new DateTime(2020, 1, 1));
            _attachDocumentTypeId = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("SmokeTest_DocumentType")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("SmokeTest_DocumentSubType").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "SmokeTest_DocumentSubType", new DateTime(2020, 1, 1), _attachDocumentTypeId);
            _attachDocumentSubTypeId = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("SmokeTest_DocumentSubType")[0];

            #endregion

            #region Question Catalogue

            var questionCatalogueExists = dbHelper.questionCatalogue.GetByQuestionName("SmokeTest_NumericQuestion").Any();
            if (!questionCatalogueExists)
                dbHelper.questionCatalogue.CreateNumericQuestion("SmokeTest_NumericQuestion", "SmokeTest_NumericQuestion Sub Heading");
            _questionCatalogueId = dbHelper.questionCatalogue.GetByQuestionName("SmokeTest_NumericQuestion").First();

            #endregion

            #region Document

            var documentExists = dbHelper.document.GetDocumentByName("SmokeTest_CaseForm").Any();
            if (!documentExists)
            {
                var documentCategoryId = dbHelper.documentCategory.GetByName("Case Form")[0];
                var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                _documentId = dbHelper.document.CreateDocument("SmokeTest_CaseForm", documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId);
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

            }
            if (_documentId == Guid.Empty)
            {
                _documentId = dbHelper.document.GetDocumentByName("SmokeTest_CaseForm")[0];
                _documentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1")[0];
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId, _questionCatalogueId)[0];
            }

            #endregion

            //#region Business Object

            //_personCaseNoteBusinessObjectId = dbHelper.businessObject.GetBusinessObjectByName("personcasenote")[0];

            //#endregion

            //#region Workflow

            //var workflowExists = dbHelper.workflow.GetWorkflowByName("SmokeTest_Workflow_CDV6_13736").Any();
            //if (!workflowExists)
            //{
            //    _workflowId = dbHelper.workflow.CreatePostSyncWorkflowRecord("SmokeTest_Workflow_CDV6_13736", "used for smoke testing", _careDirectorQA_TeamId, _personCaseNoteBusinessObjectId, _workflowXml.Replace("BusinessObjectIdToReplaceHere", _personCaseNoteBusinessObjectId.ToString()));
            //}
            //if (_workflowId == Guid.Empty)
            //    _workflowId = dbHelper.workflow.GetWorkflowByName("SmokeTest_Workflow_CDV6_13736")[0];

            //#endregion

            #region Person

            var personRecordExists = dbHelper.person.GetByFirstName("SmokeTest_PersonRecord_9b655b390911").Any();
            if (!personRecordExists)
            {
                _personID = dbHelper.person.CreatePersonRecord("", "SmokeTest_PersonRecord_9b655b390911", "", "SmokeTestUserLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                System.Threading.Thread.Sleep(2000);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstName("SmokeTest_PersonRecord_9b655b390911").FirstOrDefault();
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            _personFullName = "SmokeTest_PersonRecord_9b655b390911 SmokeTestUserLastName";

            #endregion

            //}
            //catch 
            //{
            //    if (driver != null)
            //        driver.Quit();

            //    throw;
            //}

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13736


        [Description("Create a person record")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        public void SmokeTest_UITestMethod04()
        {

            var personFirstName = "Automated Testing " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var personLastName = "Smoke Testing " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");

            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                    .WaitForPersonSearchPageToLoad()
                    .InsertFirstName(personFirstName)
                    .InsertLastName(personLastName)
                    .ClickSearchButton()
                    .ClickNewRecordButton();

            personRecordNewPage
                    .WaitForNewPersonRecordPageToLoad()
                    //.SelectPersonType("Person We Support")
                    .InsertFirstName(personFirstName)
                    .InsertLastName(personLastName)
                    .SelectStatedGender("Female")
                    .InsertDOB("02/11/2000")
                    .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("SmokeTest_Ethnicity").TapSearchButton().SelectResultElement(_ethnicityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .TapSaveAndCloseButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad();

            System.Threading.Thread.Sleep(2000);

            personSearchPage
                .ClickBackButton();

            peoplePage
                .WaitForPeoplePageToLoad();

            var matchingRecords = dbHelper.person.GetByFirstName(personFirstName);
            Assert.AreEqual(1, matchingRecords.Count);
        }

        //[Description("Create case note for person record")]
        //[TestMethod]
        //[TestCategory("SmokeTestLevel2")]
        //public void SmokeTest_UITestMethod05()
        //{
        //    foreach (var personCaseNoteId in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID))
        //        dbHelper.personCaseNote.DeletePersonCaseNote(personCaseNoteId);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
        //        .WaitFormHomePageToLoad(false, false, false);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage.
        //        WaitForPeoplePageToLoad()
        //        .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
        //        .OpenPersonRecord(_personID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad(false, false)
        //        .NavigateToPersonCaseNotesPage();

        //    personCaseNotesPage
        //        .WaitForPersonCaseNotesPageToLoad()
        //        .ClickNewRecordButton();

        //    personCaseNoteRecordPage
        //        .WaitForPersonCaseNoteRecordPageToLoad("New")
        //        .InsertSubject("Smoke Test 01 for Automation")
        //        .InsertDate("19/07/2021", "12:30")
        //        .ClickSaveAndCloseButton();

        //    personCaseNotesPage
        //        .WaitForPersonCaseNotesPageToLoad();

        //    System.Threading.Thread.Sleep(2000);
        //    var personCaseNoteRecords = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
        //    Assert.AreEqual(1, personCaseNoteRecords.Count);
        //}

        [Description("Check Person Summary dashboard")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        public void SmokeTest_UITestMethod06()
        {
            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapSummaryTab()
                .WaitForSummaryTabToLoad()
                .SelectDashboard("Activities");

            personRecordPage_SummaryArea
                .WaitForPersonRecordPage_SummaryAreaToLoad();
        }

        [Description("Open a person record and validate that the record gets audited in the 'Recently Viewed Records' functionality")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        public void SmokeTest_UITestMethod07()
        {
            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickRecentlyViewdButton();

            mainMenu
                .WaitForRecentlyViewedAreaToLoad()
                .ValidateRecentlyViewdAreaLinkElementVisible(_personFullName);
        }

        [Description("Create a person Attachment")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        [DeploymentItem("Files\\Document.docx")]
        [DeploymentItem("chromedriver.exe")]
        public void SmokeTest_UITestMethod08()
        {
            foreach (var personAttachmentId in dbHelper.personAttachment.GetByPersonID(_personID))
                dbHelper.personAttachment.DeletePersonAttachment(personAttachmentId);

            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad()
                .ClickNewRecordButton();

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("New")
                .InsertTitle("person attachment 01")
                .InsertDate("17/03/2021", "08:35")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("SmokeTest_DocumentType").TapSearchButton().SelectResultElement(_attachDocumentTypeId.ToString());

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("New")
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("SmokeTest_DocumentSubType").TapSearchButton().SelectResultElement(_attachDocumentSubTypeId.ToString());

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.docx")
                .ClickSaveAndCloseButton();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var attachments = dbHelper.personAttachment.GetByPersonID(_personID);
            Assert.AreEqual(1, attachments.Count);

        }

        [Description("Create a Case record")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        public void SmokeTest_UITestMethod09()
        {
            #region Contact Reason
            try
            {

                var contactReasonExists = dbHelper.contactReason.GetByName("SmokeTest_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "SmokeTest_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("SmokeTest_ContactReason")[0];

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to find the requested business object contactreason"))
                    return; //we shut down the test without failing if the current tenant do not have the 'Contact/Case' module active

                throw ex;
            }
            #endregion

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("SmokeTest_ContactSource").Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "SmokeTest_ContactSource", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("SmokeTest_ContactSource")[0];

            #endregion

            #region Case

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var caseRecordsExist = dbHelper.Case.GetCasesByPersonID(_personID).Any();
            if (!caseRecordsExist)
            {
                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _AutomationSmokeTestUser1_SystemUserId, _AutomationSmokeTestUser1_SystemUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            }
            if (_caseId == Guid.Empty)
            {
                _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            }


            #endregion

            if (socialCareCaseModuleInactive)
                return;

            var personFirstName = "Automated Testing " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var personLastName = "Smoke Testing " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var newPersonID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            System.Threading.Thread.Sleep(3000);
            var newPersonNumber = (int)dbHelper.person.GetPersonById(newPersonID, "personnumber")["personnumber"];

            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(newPersonNumber.ToString(), newPersonID.ToString())
                .OpenPersonRecord(newPersonID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (!bedManagementModuleInactive || !communityClinicModuleInactive)
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();


            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate("02/11/2021")
                .InsertDateContactReceived("01/11/2021")
                .ClickContactReceivedByLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("Automation_Smoke_Test_User_1").TapSearchButton().SelectResultElement(_AutomationSmokeTestUser1_SystemUserId.ToString());

            caseRecordPage
              .WaitForPersonCaseRecordPageToLoad()
              .ClickContactReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("SmokeTest_ContactReason").TapSearchButton().SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
              .WaitForPersonCaseRecordPageToLoad()
              .SelectIsThePersonAwareOfTheContact("Yes")
              .SelectDoesPersonAgreeSupportThisContact("Yes")
              .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad();

            System.Threading.Thread.Sleep(5000);

            var cases = dbHelper.Case.GetCasesByPersonID(newPersonID);
            Assert.AreEqual(1, cases.Count);

        }

        [Description("Create a Case / Update / Delete a Case Form record")]
        [TestMethod]
        [TestCategory("SmokeTestLevel2")]
        public void SmokeTest_UITestMethod10()
        {
            #region Contact Reason
            try
            {

                var contactReasonExists = dbHelper.contactReason.GetByName("SmokeTest_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "SmokeTest_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("SmokeTest_ContactReason")[0];

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to find the requested business object contactreason"))
                    return;//we shut down the test without failing if the current tenant do not have the 'Contact/Case' module active

                throw;
            }
            #endregion

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("SmokeTest_ContactSource").Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "SmokeTest_ContactSource", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("SmokeTest_ContactSource")[0];

            #endregion

            #region Case

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var caseRecordsExist = dbHelper.Case.GetCasesByPersonID(_personID).Any();
            if (!caseRecordsExist)
            {
                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _AutomationSmokeTestUser1_SystemUserId, _AutomationSmokeTestUser1_SystemUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            }
            if (_caseId == Guid.Empty)
            {
                _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
            }


            #endregion

            foreach (var caseFormId in dbHelper.caseForm.GetCaseFormByCaseID(_caseId))
                dbHelper.caseForm.DeleteCaseForm(caseFormId);


            loginPage
                .GoToLoginPage()
                .Login("Automation_Smoke_Test_User_1", "Passw0rd_!", environmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber.ToString(), _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("SmokeTest_CaseForm").TapSearchButton().SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("24/06/2021")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var caseforms = dbHelper.caseForm.GetCaseFormByCaseID(_caseId);
            Assert.AreEqual(1, caseforms.Count);
            var caseformID = caseforms[0];


            var sectionIdentifier = (string)dbHelper.documentSection.GetDocumentSectionByID(_documentSectionId, "SectionIdentifier")["sectionidentifier"];
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionIdAndQuestionCatalogueId(_documentSectionQuestionId, _questionCatalogueId).FirstOrDefault();
            var documentQuestionIdentifier = (string)dbHelper.documentQuestionIdentifier.GetDocumentQuestionIdentifierByID(documentQuestionIdentifierId, "identifier")["identifier"];


            caseCasesFormPage
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateFormTypeField("SmokeTest_CaseForm", false, true)
                .ValidateStartDateField("24/06/2021")
                .TapEditAssessmentButton();

            smokeTest_CaseFormEditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString(), sectionIdentifier)
                .InsertSmokeTest_NumericQuestion(documentQuestionIdentifier, "23")
                .TapSaveAndCloseButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            smokeTest_CaseFormEditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseformID.ToString(), sectionIdentifier)
                .ValidateSmokeTest_NumericQuestion(documentQuestionIdentifier, "23")
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Closed")
                 .TapSignedOffByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Automation_Smoke_Test_User_1").TapSearchButton().SelectResultElement(_AutomationSmokeTestUser1_SystemUserId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertSignedOffDate("26/06/2021")
                .TapSaveAndCloseButton()
                .WaitForRecordToBeSaved();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(caseformID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateSignOffDateField("26/06/2021");
        }


        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class CaseAttachments_UITestCases : FunctionalTest
    {

        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _defaultSystemUserId;
        private string _systemUserName;
        private Guid _systemUserId;

        #endregion

        [TestInitialize()]
        public void Case_CaseNotes_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CaseAttachments BU1");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CaseAttachments T1", null, _businessUnitId, "907678", "CaseAttachmentsT1@careworkstempmail.com", "CaseAttachments T1", "020 123456");

                #endregion                

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region System User "CaseFormCaseNoteUser1"

                _systemUserName = "CaseAttachmentUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseAttachment", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2340

        [TestProperty("JiraIssueID", "CDV6-1967")]
        [Description("Step(s) 1 to 6 from the original test")]
        [TestMethod]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseAttachments_UITestMethod01()
        {
            #region Person

            var personFirstName = "Wain";
            var personLastName = _currentDateString;
            var _personFullName = "Wain " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            var relatedPersonFirstName = "Flint";
            var relatedPersonLastName = _currentDateString;
            var relatedPersonFullName = "Flint " + personLastName;
            var _relatedPersonID = commonMethodsDB.CreatePersonRecord(relatedPersonFirstName, relatedPersonLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var relatedPersonNumber = dbHelper.person.GetPersonById(_relatedPersonID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Relationship Type

            var relationshipTypeId = dbHelper.personRelationshipType.GetByName("Friend").FirstOrDefault();

            #endregion

            #region Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_teamId,
                _personID, _personFullName, relationshipTypeId, "Friend",
                _relatedPersonID, relatedPersonFullName, relationshipTypeId, "Friend",
                new DateTime(2023, 1, 1), "Person Relationship Created", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            #endregion

            #region Case Status

            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _defaultSystemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Case

            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var caseid = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                _teamId, _personID, currentDate, _defaultSystemUserId, "presenting needs ...",
                _defaultSystemUserId, _admission_CaseStatusId, _contactReasonId, currentDate, _dataFormId,
                _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                _inpationAdmissionMethodId, _defaultSystemUserId, currentDate, _provider_HospitalId,
                _inpatientWardId, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false,
                null, null, null);

            var caseFields = dbHelper.Case.GetCaseByID(caseid, "casenumber", "title");
            var caseNumber = (string)caseFields["casenumber"];
            var caseTitle = (string)caseFields["title"];

            #endregion

            #region Attach Document Type

            var documentTypeID = commonMethodsDB.CreateAttachDocumentType(_teamId, "All Attached Documents", new DateTime(2021, 2, 3));

            #endregion

            #region Attach Document Sub Type

            var documentSubTypeID = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Independent Living Grant", new DateTime(2021, 2, 3), documentTypeID);

            #endregion



            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToAttachments();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ClickNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ValidateErrorMessageVisible(false)
                .ClickSaveAndCloseButton()
                .ValidateErrorMessageVisible(true)
                .ValidateErrorMessageText("Some data is not correct. Please review the data in the Form.");

            #endregion

            #region Step 2

            caseAttachmentRecordPage
                .InsertTextOnTitle("Attachment 01")
                .InsertTextOnDate("01/06/2023")
                .InsertTextOnDate_TimeField("09:30")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ClickBackButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ClickSearchButton();

            var caseattachments = dbHelper.caseAttachment.GetCaseAttachmentByCaseID(caseid);
            Assert.AreEqual(1, caseattachments.Count);

            caseAttachmentsPage
                .OpenRecord(caseattachments[0]);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ValidateCaseLinkText(caseTitle)
                .ValidateTitleText("Attachment 01")
                .ValidateDateText("01/06/2023")
                .ValidateDate_TimeFieldText("09:30")
                .ValidateDocumentTypeLinkText("All Attached Documents")
                .ValidateDocumentSubTypeLinkText("Independent Living Grant")
                .ValidateResponsibleTeamLinkText("CaseAttachments T1")
                .ValidateFile_FileLinkText("Document.txt (13 B)");

            #endregion

            #region Step 3

            caseAttachmentRecordPage
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .ClickCloseButton();

            #endregion

            #region Step 4

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .ClickCloneButton()
                .ValidateStartDateErrorLabelVisibility(true)
                .ValidateStartDateErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 5

            cloneAttachmentsPopup
                .InsertStartDate("02/06/2023")
                .SelectRecord(_relatedPersonID.ToString())
                .ClickCloneButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad();

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(relatedPersonNumber)
                .ClickSearchButton()
                .OpenPersonRecord(_relatedPersonID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAttachmentsPage();

            personAttachmentsPage
                .WaitForPersonAttachmentsPageToLoad();

            var clonnedAttachments = dbHelper.personAttachment.GetByPersonID(_relatedPersonID);
            Assert.AreEqual(1, clonnedAttachments.Count);

            personAttachmentsPage
                .OpenPersonAttachmentsRecord(clonnedAttachments[0].ToString());

            personAttachmentRecordPage
                .WaitForPersonAttachmentRecordPageToLoad("Attachment 01")
                .ValidatePersonLinkText(relatedPersonFullName)
                .ValidateTitleFieldValue("Attachment 01")
                .ValidateDateFieldValue("02/06/2023", "00:00")
                .ValidateDocumentTypeLinkText("All Attached Documents")
                .ValidateDocumentSubTypeLinkText("Independent Living Grant")
                .ValidateResponsibleTeamLinkText("CaseAttachments T1")
                .ValidateFileLinkText("Document.txt (13 B)");

            #endregion


        }

        [TestProperty("JiraIssueID", "CDV6-25676")]
        [Description("Step(s) 7 from the original test")]
        [TestMethod]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseAttachments_UITestMethod02()
        {
            #region Person

            var personFirstName = "Wain";
            var personLastName = _currentDateString;
            var _personFullName = "Wain " + personLastName;
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            var relatedPersonFirstName = "Flint";
            var relatedPersonLastName = _currentDateString;
            var relatedPersonFullName = "Flint " + personLastName;
            var _relatedPersonID = commonMethodsDB.CreatePersonRecord(relatedPersonFirstName, relatedPersonLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var relatedPersonNumber = dbHelper.person.GetPersonById(_relatedPersonID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Relationship Type

            var relationshipTypeId = dbHelper.personRelationshipType.GetByName("Friend").FirstOrDefault();

            #endregion

            #region Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_teamId,
                _personID, _personFullName, relationshipTypeId, "Friend",
                _relatedPersonID, relatedPersonFullName, relationshipTypeId, "Friend",
                new DateTime(2023, 1, 1), "Person Relationship Created", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            #endregion

            #region Case Status

            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Inpatient Management Contact Reason", new DateTime(2020, 1, 1), 140000001, 2, false);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default Contact Source", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Provider

            var _provider_Name = "RTT_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _defaultSystemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Case

            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var caseid = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(
                _teamId, _personID, currentDate, _defaultSystemUserId, "presenting needs ...",
                _defaultSystemUserId, _admission_CaseStatusId, _contactReasonId, currentDate, _dataFormId,
                _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId,
                _inpationAdmissionMethodId, _defaultSystemUserId, currentDate, _provider_HospitalId,
                _inpatientWardId, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false,
                null, null, null);

            var caseFields = dbHelper.Case.GetCaseByID(caseid, "casenumber", "title");
            var caseNumber = (string)caseFields["casenumber"];
            var caseTitle = (string)caseFields["title"];

            #endregion

            #region Attach Document Type

            var documentTypeID = commonMethodsDB.CreateAttachDocumentType(_teamId, "All Attached Documents", new DateTime(2021, 2, 3));

            #endregion

            #region Attach Document Sub Type

            var documentSubTypeID = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Independent Living Grant", new DateTime(2021, 2, 3), documentTypeID);

            #endregion



            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToAttachments();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ClickNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .InsertTextOnTitle("Attachment 02")
                .InsertTextOnDate("01/06/2023")
                .InsertTextOnDate_TimeField("09:30")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad()
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ClickSearchButton();

            var caseattachments = dbHelper.caseAttachment.GetCaseAttachmentByCaseID(caseid);
            Assert.AreEqual(1, caseattachments.Count);
            var caseAttachment = caseattachments[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Attachments (For Case)")
                .SelectFilter("1", "Case")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(caseNumber).TapSearchButton().SelectResultElement(caseid.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(caseAttachment.ToString())
                .ClickNewRecordButton_ResultsPage();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoadFromAdvancedSearch()
                .InsertTextOnTitle("Attachment 01")
                .InsertTextOnDate("01/06/2023")
                .InsertTextOnDate_TimeField("09:30")
                .ClickCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(caseNumber).TapSearchButton().SelectResultElement(caseid);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoadFromAdvancedSearch()
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoadFromAdvancedSearch()
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoadFromAdvancedSearch()
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad();

            caseattachments = dbHelper.caseAttachment.GetCaseAttachmentByCaseID(caseid);
            Assert.AreEqual(2, caseattachments.Count);
            var newCaseAttachment = caseattachments.Where(c => c != caseAttachment).First();

            advanceSearchPage
                .ValidateSearchResultRecordPresent(newCaseAttachment.ToString());

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

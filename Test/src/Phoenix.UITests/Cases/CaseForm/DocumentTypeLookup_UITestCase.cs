using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for Form Document Type Lookup
    /// </summary>
    [TestClass]
    public class DocumentTypeLookup_UITestCase : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid AutomationUser_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private Guid _dataFormId_SocialCareCase;
        private Guid _caseStatusID;
        private Guid _caseId;
        private Guid _documentCategoryId;
        private Guid _documentTypeId;
        private Guid _documentId;
        private string _automationUserLoginName;
        private string _caseNumber;
        private string _documentName = "Test_17907_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        #region https://advancedcsg.atlassian.net/browse/CDV6-17907

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

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

            #endregion  Business Unit

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion Team

            #region System User

            var automationTestUserExists = dbHelper.systemUser.GetSystemUserByUserName("AutomationTest_User").Any();
            if (!automationTestUserExists)
            {
                AutomationUser_SystemUserId = dbHelper.systemUser.CreateSystemUser("AutomationTest_User", "Automation", " Test User", "Automation Test User", "Passw0rd_!", "AutomationTest_User@somemail.com", "AutomationTest_User@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                var systemUserUnscheduledAppointmentsProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Unscheduled Health Appointments").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationUser_SystemUserId, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationUser_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(AutomationUser_SystemUserId, systemUserUnscheduledAppointmentsProfileId);
            }
            if (AutomationUser_SystemUserId == Guid.Empty)
            {
                AutomationUser_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("AutomationTest_User").FirstOrDefault();
            }
            dbHelper.systemUser.UpdateLastPasswordChangedDate(AutomationUser_SystemUserId, DateTime.Today);
            _automationUserLoginName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(AutomationUser_SystemUserId, "username")["username"];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Appointment_Ethnicity", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity")[0];

            #endregion

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("Case_ContactReason").Any();
            if (!contactReasonExists)
                dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "Case_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
            _contactReasonId = dbHelper.contactReason.GetByName("Case_ContactReason")[0];

            #endregion Contact Reason

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("Case_ContactSource").Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "Case_ContactSource", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("Case_ContactSource")[0];

            #endregion

            #region Data Form

            _dataFormId_SocialCareCase = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            #endregion

            #region Case Status
            _caseStatusID = dbHelper.caseStatus.GetByName("Allocate To Team").FirstOrDefault();

            #endregion

            #region Person
            _personID = dbHelper.person.CreatePersonRecord("", "AutomationPerson_17907" + DateTime.Now, "", "AutomationAppointmentLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Case record
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, AutomationUser_SystemUserId,
                    AutomationUser_SystemUserId, _caseStatusID, _contactReasonId, _dataFormId_SocialCareCase, _contactSourceId,
                    DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, 40);
            _caseId = dbHelper.Case.GetCasesByPersonID(_personID).FirstOrDefault();
            _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            #endregion

            #region Document Category
            _documentCategoryId = dbHelper.documentCategory.GetByName("Case Form").FirstOrDefault();

            #endregion

            #region Document Type
            _documentTypeId = dbHelper.documentType.GetByName("Initial Assessment").FirstOrDefault();

            #endregion

            #region Document 
            _documentId = dbHelper.document.CreateDocument(_documentName, _documentCategoryId, _documentTypeId, _careDirectorQA_TeamId, 100000000);
            #endregion

        }


        [TestProperty("JiraIssueID", "CDV6-18318")]
        [Description("Automated UI Test for Form Type Lookup - " +
            "Navigate to Menu > Related Items > Forms (Case) > click + to create a new form. Click the lookup for Form Type and enter the name of a deactivated form - " +
            "Validate the Users should not see inactive form types ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void FormDocumentTypeLookup_UITestMethod()
        {
            loginPage
                .GoToLoginPage()
                  .Login(_automationUserLoginName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber)
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

            caseFormPage
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateFormTypeField(_documentName, true, true);

            #region Inactive Form Type Document

            dbHelper.document.UpdateStatus(_documentId, 1);

            #endregion

            caseFormPage
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_documentId.ToString());
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
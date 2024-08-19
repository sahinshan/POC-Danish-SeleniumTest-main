using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\D_Flag.Zip"), DeploymentItem("Files\\Automated UI Test Document 1.Zip"), DeploymentItem("Files\\Automated UI Test Document 1 Rules.zip")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip"), DeploymentItem("Files\\Automation - Rules - Person Form 1.Zip")]
    [DeploymentItem("Files\\WF Person Form Testing - Jira ID CDV6-5001.Zip")]
    [DeploymentItem("Files\\Sum two values_minus a third.Zip")]
    [DeploymentItem("Files\\WF Automated Testing - CDV6-10345.Zip")]
    [DeploymentItem("Files\\Doc MR.Zip")]
    public class PersonForms_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _ethnicityId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _systemUserFullName;
        private Guid _defaultUserId;
        private string _defaultUsername;
        private string _defaultUserFullname;
        private Guid _dataFormId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _caseId;
        private string _caseNumber;
        private string _caseTitle;
        private string _documentName;
        private Guid _documentId;
        private string _automationPersonForm1Name;
        private Guid _automationPersonForm1Id;
        private string _automationRulesPersonForm1Name;
        private Guid _automationRulesPersonForm1Id;
        private string firstName;
        private string lastName;

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

                _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

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

                _systemUsername = "PersonFormsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PersonForms", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "fullname")["fullname"];

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

                commonMethodsDB.CreateWorkflowIfNeeded("WF Person Form Testing - Jira ID CDV6-5001", "WF Person Form Testing - Jira ID CDV6-5001.Zip"); //needed to import the rules

                _automationPersonForm1Name = "Automation - Person Form 1";
                _automationPersonForm1Id = commonMethodsDB.CreateDocumentIfNeeded(_automationPersonForm1Name, "Automation - Person Form 1.Zip");

                _automationRulesPersonForm1Name = "Automation - Rules - Person Form 1";
                _automationRulesPersonForm1Id = commonMethodsDB.CreateDocumentIfNeeded(_automationRulesPersonForm1Name, "Automation - Rules - Person Form 1.Zip");


                #endregion

                #region Person

                firstName = "Automation";
                lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                #endregion

                #region Case

                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
                _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-5001

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Person Record - Navigate to the Person Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Validate that the popup displays only Person Form records for the current Person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24566")]
        public void PersonForms_PrecedingForms_UITestMethod001()
        {
            #region Person

            var _personId2 = commonMethodsDB.CreatePersonRecord("Second", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case

            var _caseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId2, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form

            Guid caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(2020, 11, 1));
            Guid caseFormID2 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(2020, 11, 2));
            Guid caseFormID3 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId2, new DateTime(2020, 11, 1)); // Control Form. Form that belongs to another Case Form.

            #endregion

            #region Person Form

            var personFormID1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2020, 11, 1));
            var personFormID2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2020, 11, 2));
            var personFormID3 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId2, _automationPersonForm1Id, new DateTime(2020, 11, 1)); //Control Form. Form that belongs to another Person.

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapPrecedingFormLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectBusinessObjectByText("Forms (Person)")

                .ValidateResultElementPresent(personFormID1.ToString())
                .ValidateResultElementPresent(personFormID2.ToString())
                .ValidateResultElementNotPresent(personFormID3.ToString())

                .ValidateResultElementNotPresent(caseFormID1.ToString())
                .ValidateResultElementNotPresent(caseFormID2.ToString())
                .ValidateResultElementNotPresent(caseFormID3.ToString());

        }

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Person Record - Navigate to the Person Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - Select 'Forms (Case)' in the Lookup for picklist - " +
            "Validate that the popup displays only Case Form records for the Cases associated with the Person Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24567")]
        public void PersonForms_PrecedingForms_UITestMethod002()
        {
            #region Person

            var _personId2 = commonMethodsDB.CreatePersonRecord("Second", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion

            #region Case

            var _caseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId2, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2015, 10, 6), new DateTime(2015, 10, 6), 20, "Care Form Record For Case Note");

            #endregion

            #region Case Form

            Guid caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(2020, 11, 1));
            Guid caseFormID2 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(2020, 11, 2));
            Guid caseFormID3 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId2, _caseId2, new DateTime(2020, 11, 1)); // Control Form. Form that belongs to another Case Form.

            #endregion

            #region Person Form

            var personFormID1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2020, 11, 1));
            var personFormID2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2020, 11, 2));
            var personFormID3 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId2, _automationPersonForm1Id, new DateTime(2020, 11, 1)); //Control Form. Form that belongs to another Person.

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapPrecedingFormLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectBusinessObjectByText("Forms (Case)")

                .ValidateResultElementPresent(caseFormID1.ToString())
                .ValidateResultElementPresent(caseFormID2.ToString())
                .ValidateResultElementNotPresent(caseFormID3.ToString())

                .ValidateResultElementNotPresent(personFormID1.ToString())
                .ValidateResultElementNotPresent(personFormID2.ToString())
                .ValidateResultElementNotPresent(personFormID3.ToString());

        }

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Person Record - Navigate to the Person Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Select one Person Form record - Save the Person Form - Validate that all data is saved correctly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24568")]
        public void PersonForms_PrecedingForms_UITestMethod003()
        {
            #region Person Form

            var personFormID1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2020, 11, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_automationRulesPersonForm1Name.ToString()).TapSearchButton().SelectResultElement(_automationRulesPersonForm1Id.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapPrecedingFormLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Forms (Person)").SelectResultElement(personFormID1.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            var personformIDs = dbHelper.personForm.GetPersonFormByPersonIDAndFormType(_personId, _automationRulesPersonForm1Id);
            Assert.AreEqual(1, personformIDs.Count);

            personFormsPage
                .OpenRecord(personformIDs[0].ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .ValidatePersonField(_person_fullName.ToString(), false, true)
                .ValidateFormTypeField(_automationRulesPersonForm1Name, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidatePrecedingFormFieldLinkText(_automationPersonForm1Name + " for " + _person_fullName + " Starting 01/11/2020 created by " + _defaultUserFullname);

        }

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Person Record - Navigate to the Person Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Select one Case form record - Save the Person Form - Validate that all data is saved correctly")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24569")]
        public void PersonForms_PrecedingForms_UITestMethod004()
        {
            #region Case Form

            Guid caseFormID1 = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId, _personId, _caseId, new DateTime(2020, 11, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_automationRulesPersonForm1Name.ToString()).TapSearchButton().SelectResultElement(_automationRulesPersonForm1Id.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapPrecedingFormLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(caseFormID1.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            var personformIDs = dbHelper.personForm.GetPersonFormByPersonIDAndFormType(_personId, _automationRulesPersonForm1Id);
            Assert.AreEqual(1, personformIDs.Count);

            personFormsPage
                .OpenRecord(personformIDs[0].ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .ValidatePersonField(_person_fullName.ToString(), false, true)
                .ValidateFormTypeField(_automationRulesPersonForm1Name.ToString(), false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidatePrecedingFormFieldLinkText(_documentName + " for " + _caseTitle + " Starting 01/11/2020 created by " + _defaultUserFullname);

        }

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Validate that the preceding form field can be set using a workflow" +
            "Open a Person Record - Navigate to the Person Forms Section - Open a person form - Update the status field to Completed - Save and close the record - " +
            "Validate that a new record is automatically created - Open the new record - " +
            "Validate that the Preceding Form field is automatically set and points to the previous form record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24570")]
        public void PersonForms_PrecedingForms_UITestMethod005()
        {
            #region Person

            var _personId2 = commonMethodsDB.CreatePersonRecord(new Guid("304f7607-35c7-4375-9453-aa8a7ab4f726"), "Belinda", "", "Yang", _ethnicityId, _careDirectorQA_TeamId);
            var _personNumber2 = (int)dbHelper.person.GetPersonById(_personId2, "personnumber")["personnumber"];
            var _person_fullName2 = "Belinda Yang";

            #endregion

            //unlink preceding forms
            foreach (var recordID in dbHelper.personForm.GetPersonFormByPersonIDAndFormType(_personId2, _automationRulesPersonForm1Id))
                dbHelper.personForm.UpdatePersonForm(recordID, null);

            //remove all Forms for the Case record
            foreach (var recordID in dbHelper.personForm.GetPersonFormByPersonIDAndFormType(_personId2, _automationRulesPersonForm1Id))
                dbHelper.personForm.DeletePersonForm(recordID);

            #region Person Form

            Guid personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId2, _automationRulesPersonForm1Id, new DateTime(2020, 11, 3));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber2.ToString(), _personId2.ToString())
                .OpenPersonRecord(_personId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .SelectStatus("Complete")
                .TapSaveButton()
                .TapBackButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            var records = dbHelper.personForm.GetPersonFormByPersonIDAndFormType(_personId2, _automationRulesPersonForm1Id);
            Assert.AreEqual(2, records.Count);

            var newPersonForm = records.Where(c => c != personFormID).FirstOrDefault();

            personFormsPage
                .OpenRecord(newPersonForm.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .ValidatePersonField(_person_fullName2, false, true)
                .ValidateFormTypeField(_automationRulesPersonForm1Name, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidatePrecedingFormFieldLinkText(_automationRulesPersonForm1Name + " for " + _person_fullName2 + " Starting 03/11/2020 created by " + _defaultUserFullname);

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-8420

        [Description("Jira Story ID  - https://advancedcsg.atlassian.net/browse/CDV6-5001 - " +
            "Open a Person Record - Navigate to the Person Forms Section - " +
            "Tap on the Add New button - Set data in all mandatory fields - Tap on the Preceding Form lookup button - " +
            "Validate that the popup displays only Person Form records for the current Person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24571")]
        public void InactiveReferenceDataAvailableToSelectInAdvancedSearch_UITestMethod001()
        {
            #region Form Cancellation Reason

            var FormCancellationReason = commonMethodsDB.CreateFormCancellationReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Duplicated", new DateTime(2022, 1, 1), null, true);
            dbHelper.formCancellationReason.UpdateInactive(FormCancellationReason, true);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_automationRulesPersonForm1Name.ToString()).TapSearchButton().SelectResultElement(_automationRulesPersonForm1Id.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .SelectStatus("Cancelled")
                .TapCancelledReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateViewElementNotPresent("Inactive Records")
                .ValidateResultElementNotPresent(FormCancellationReason.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-12503

        [TestProperty("JiraIssueID", "CDV6-12549")]
        [Description("Try to save record with missing mandatory fields - " +
            "Set mandatory fields and save the record - " +
            "Set review date and re-save the record - " +
            "Update all fields and save the record - " +
            "Set the status to Complete and validate that the record is still editable")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonForms_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("New", false)
                .TapSaveButton()

                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.") //validate the error messages
                .ValidateFormTypeErrorMessage("Please fill out this field.")
                .ValidateStartDateErrorMessage("Please fill out this field.")

                .ValidateStatusField("In Progress") //validate that the status field is set to In Progress by default

                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_automationPersonForm1Name.ToString()).TapSearchButton().SelectResultElement(_automationPersonForm1Id.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("New", false)
                .InsertStartDate("02/09/2021")
                .TapSaveButton()
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false) //validate that the record is saved correctly
                .ValidatePersonField(_person_fullName, false, true)
                .ValidateFormTypeField(_automationPersonForm1Name, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField("02/09/2021")
                .ValidatePrecedingFormFieldLinkText("")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidateSourceCaseField("");

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad(); //validate that the Actions/Outcomes sub area is loaded

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .InsertReviewDate("01/09/2021") //validate that we can set a review date prior to the start date
                .TapSaveAndCloseButton();

            var formRecords = dbHelper.personForm.GetPersonFormByPersonID(_personId);
            Assert.AreEqual(1, formRecords.Count);

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(formRecords[0].ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)  //validate that we can update and save the form
                .SelectStatus("Not Started")
                .InsertStartDate("03/09/2021")
                .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .InsertDueDate("04/09/2021")
                .InsertReviewDate("05/09/2021")
                .ClickSourceCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(_caseId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .TapSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(formRecords[0].ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidatePersonField(_person_fullName, false, true)
                .ValidateFormTypeField(_automationPersonForm1Name, false, true)
                .ValidateStatusField("Not Started")
                .ValidateStartDateField("03/09/2021")
                .ValidatePrecedingFormFieldLinkText("")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField(_systemUserFullName, true, true)
                .ValidateDueDateField("04/09/2021")
                .ValidateReviewDateField("05/09/2021")
                .ValidateSourceCaseField(_caseTitle)

                .SelectStatus("Complete") // validate that after completing the record it is still editable
                .TapSaveButton()
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .InsertStartDate("01/09/2021") // if the record is editable we can change one of the fields
                .TapSaveButton()
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidateStartDateField("01/09/2021");

        }

        [TestProperty("JiraIssueID", "CDV6-12557")]
        [Description("Validate that is not possible to edit a closed form")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonForms_UITestMethod002()
        {
            #region Person Form

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2021, 9, 5));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .SelectStatus("Approved") //change the status to approved
                .TapSignedOffByLookupButton(); //set the sign off fields

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .InsertSignedOffDate("06/09/2021")
                .TapSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidatePersonField(_person_fullName, false, true)
                .ValidateFormTypeField(_automationPersonForm1Name, false, true)
                .ValidateStatusField("Approved")
                .ValidateStartDateField("05/09/2021")
                .ValidatePrecedingFormFieldLinkText("")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidateSourceCaseField("")
                .ValidateSignedOffByLinkField(_systemUserFullName)
                .ValidateSignedOffDateField("06/09/2021")

                .InsertStartDate("01/09/2021") // if the record is editable we can change one of the fields
                .SelectStatus("Closed")
                .TapSaveButton()
                .WaitForPersonFormRecordPageToLoadDisabled(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidateStartDateField("01/09/2021")
                .ValidateStatusField("Closed");

        }

        [TestProperty("JiraIssueID", "CDV6-12558")]
        [Description("Validate that is not possible to edit a Canceled form")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonForms_UITestMethod003()
        {
            #region Form Cancellation Reason

            var FormCancellationReason = commonMethodsDB.CreateFormCancellationReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Created in error", new DateTime(2022, 1, 1), null, false);

            #endregion

            #region

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2021, 9, 5));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidatePersonField(_person_fullName, false, true)
                .ValidateFormTypeField(_automationPersonForm1Name, false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField("05/09/2021")
                .ValidatePrecedingFormFieldLinkText("")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("")
                .ValidateReviewDateField("")
                .ValidateSourceCaseField("")

                .InsertStartDate("01/09/2021") // if the record is editable we can change one of the fields
                .SelectStatus("Cancelled")
                .TapCancelledReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Created in error").TapSearchButton().SelectResultElement(FormCancellationReason.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .TapSaveButton()
                .WaitForPersonFormRecordPageToLoadDisabled(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .ValidateStartDateField("01/09/2021")
                .ValidateStatusField("Cancelled")
                .ValidateCancelledReasonLinkField("Created in error");

        }

        [TestProperty("JiraIssueID", "CDV6-12559")]
        [Description("Test Days To Complete calculation from document - " +
            "Validate that Outcomes/Actions cannot be added with missing mandatory fields - " +
            "Set the mandatory fields and save the Outcome/Action record - Validate that the record is saved - " +
            "Edit the record - Validate that the update actions are correctly saved")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonForms_UITestMethod004()
        {
            var personID = new Guid("fc7474c3-c9a6-4bb4-86a1-05922003024a"); //Ramiro Bowen
            var personNumber = "43850";
            var formTypeID = new Guid("B4A46C79-E3A2-E911-A2C6-005056926FE4"); //Doc MR
            var PersonFormActionOutcomeTypeID = new Guid("70b44d74-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type one

            //remove all Forms for the person record
            foreach (var personFormID in dbHelper.personForm.GetPersonFormByPersonID(personID))
            {
                foreach (var outcomeid in dbHelper.personFormOutcome.GetByPersonFormId(personFormID))
                    dbHelper.personFormOutcome.DeletePersonFormOutcome(outcomeid);

                dbHelper.personForm.DeletePersonForm(personFormID);
            }


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
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("New")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Doc MR").TapSearchButton().SelectResultElement(formTypeID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("New")
                .InsertStartDate("02/09/2021")

                .TapSaveButton()

                .WaitForPersonFormRecordPageToLoad("Doc MR for Ramiro Bowen")
                .ValidatePersonField("Ramiro Bowen", false, true)
                .ValidateFormTypeField("Doc MR", false, true)
                .ValidateStatusField("In Progress")
                .ValidateStartDateField("02/09/2021")
                .ValidatePrecedingFormFieldLinkText("")
                .ValidateCreatedOnProviderPortalOptionChecked(false)
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateResponsibleUserField("", false, true)
                .ValidateDueDateField("05/09/2021") // due date should be calculated automatically
                .ValidateReviewDateField("")
                .ValidateSourceCaseField("");

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad("New")
                .TapSaveButton()
                .ValidateNotificationMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateActionsOutcomeErrorLabelText("Please fill out this field.")
                .ValidateDateErrorLabelText("Please fill out this field.");

            var personForms = dbHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(1, personForms.Count());

            var outcomes = dbHelper.personFormOutcome.GetByPersonFormId(personForms[0]);
            Assert.AreEqual(0, outcomes.Count);


            personFormActionOutcomePage
                .TapActionsOutcomesLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person Action / Outcome Type one").TapSearchButton().SelectResultElement(PersonFormActionOutcomeTypeID.ToString());

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad("New")
                .InsertDate("05/09/2021")
                .InsertComments("comments ...")
                .TapSaveButton()
                .WaitForPersonFormActionOutcomePageToLoad()
                .WaitForPersonFormActionOutcomePageToLoad("Action/Outcome for Doc MR for Ramiro Bowen Starting")
                .TapBackButton();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad();

            outcomes = dbHelper.personFormOutcome.GetByPersonFormId(personForms[0]);
            Assert.AreEqual(1, outcomes.Count);

            personFormActionsOutcomesPageFrame
                .OpenRecord(outcomes[0].ToString());

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad("Action/Outcome for Doc MR for Ramiro Bowen Starting")
                .ValidatePersonFormField("Doc MR for Ramiro Bowen Starting 02/09/2021 created by CW Forms Test User 1", false, true)
                .ValidateActionsOutcomesField("Person Action / Outcome Type one", true, true)
                .ValidateCommentsField("comments ...")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateDateField("05/09/2021")

                .InsertDate("06/09/2021")
                .InsertComments("comments updated ...")
                .TapSaveAndCloseButton();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .OpenRecord(outcomes[0].ToString());

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad("Action/Outcome for Doc MR for Ramiro Bowen Starting")
                .ValidatePersonFormField("Doc MR for Ramiro Bowen Starting 02/09/2021 created by CW Forms Test User 1", false, true)
                .ValidateActionsOutcomesField("Person Action / Outcome Type one", true, true)
                .ValidateCommentsField("comments updated ...")
                .ValidateResponsibleTeamField("CareDirector QA", false, true)
                .ValidateDateField("06/09/2021");

        }

        [TestProperty("JiraIssueID", "CDV6-12565")]
        [Description("Delete a person form record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonForms_UITestMethod006()
        {
            #region Person Form

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, _automationPersonForm1Id, new DateTime(2021, 9, 5));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(_automationPersonForm1Name.ToString() + " for " + _person_fullName, false)
                .TapDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var personForms = dbHelper.personForm.GetPersonFormByPersonID(_personId);
            Assert.AreEqual(0, personForms.Count());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-18001


        [TestProperty("JiraIssueID", "CDV6-18791")]
        [Description("Test related with the defect CDV6-17721")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonForms_MultiResponseAndTableQuestion_UITestMethod001()
        {
            #region Question Catalogue - Multi Response Question

            Guid multiResponseQuestionId;
            Guid multiOptionAnswer1;
            Guid multiOptionAnswer2;
            Guid multiOptionAnswer3;
            if (!dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_MR").Any())
            {
                multiResponseQuestionId = dbHelper.questionCatalogue.CreateQuestion("CDV6_17721_MR", "", 4);
                dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 1", multiResponseQuestionId, 1, 1, 1);
                dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 2", multiResponseQuestionId, 1, 1, 1);
                dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 3", multiResponseQuestionId, 1, 1, 1);
            }

            multiResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_MR")[0];
            multiOptionAnswer1 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 1", multiResponseQuestionId)[0];
            multiOptionAnswer2 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 2", multiResponseQuestionId)[0];
            multiOptionAnswer3 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 3", multiResponseQuestionId)[0];

            #endregion

            #region Question Catalogue - Table (With Question Per Cell)

            Guid tableQuestionId;
            if (!dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_QPC").Any())
            {
                tableQuestionId = dbHelper.questionCatalogue.CreateQuestion("CDV6_17721_QPC", "", 17);
                dbHelper.tableQuestionCell.CreateTableQuestionCell(tableQuestionId, multiResponseQuestionId, 1, 1, 1, 1);
            }

            tableQuestionId = dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_QPC")[0];

            #endregion

            #region Document

            Guid documentId;
            if (!dbHelper.document.GetDocumentByName("Testing CDV6-17721").Any())
            {
                var documentCategoryId = dbHelper.documentCategory.GetByName("Person Form")[0];
                var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                documentId = dbHelper.document.CreateDocument("Testing CDV6-17721", documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                var sectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", documentId);
                dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(tableQuestionId, sectionId);
                dbHelper.document.UpdateStatus(documentId, 100000000); //publish the document
            }

            documentId = dbHelper.document.GetDocumentByName("Testing CDV6-17721")[0];

            #endregion

            #region Person Form

            var personFormId = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personId, documentId, DateTime.Now.Date);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormId.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .TapEditAssessmentButton();

            testing_CDV6_17721EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormId.ToString())
                .Click_CDV6_17721_MR_Checkbox(multiOptionAnswer1.ToString().ToUpper())
                .Click_CDV6_17721_MR_Checkbox(multiOptionAnswer2.ToString().ToUpper())
                .Click_CDV6_17721_MR_Checkbox(multiOptionAnswer3.ToString().ToUpper());

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

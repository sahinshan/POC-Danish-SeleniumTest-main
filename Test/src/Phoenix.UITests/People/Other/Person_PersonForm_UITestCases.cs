using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_PersonForm_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private Guid _documentCategoryId;
        private Guid _documentTypeId;
        private Guid _documentId;
        private string _documentName = "Test_19719" + DateTime.Now;
        private string _personFullName;

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {
            try
            {
                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

                #endregion

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "PersonCareTest_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason")[0];

                #endregion

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "PersonCareTest_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource")[0];

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_CDV6_13981", "CW", "Forms_Test_User_CDV6_13981", "CW" + "Kumar", "Passw0rd_!", "CW_Forms_Test_User_CDV6_13981@somemail.com", "CW_Forms_Test_User_CDV6_13981@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").FirstOrDefault();
                }
                #endregion

                #region Person

                var personRecordExists = dbHelper.person.GetByFirstName("CDV6_13981_").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "CDV6_13981_", "", "Person", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("CDV6_13981_").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "CDV6_13981_Person";

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
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-10540

        [TestProperty("JiraIssueID", "CDV6-25005")]
        [Description("Open a person form record (document = 'Automation - Person Form 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = Yes are displayed - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = No and linked to the current document type are displayed - " +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = No and with no link to the current document type are NOT displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FilterActionsOutcomesByPersonFormDocument_UITestMethod001()
        {
            var outcome1 = new Guid("70b44d74-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type one
            var outcome2 = new Guid("62c36b85-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type two
            var outcome3 = new Guid("e825b125-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type three
            var outcome4 = new Guid("deafe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type four
            var outcome5 = new Guid("e2afe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type five
            var outcome6 = new Guid("efba0d39-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type six

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("5b390688-6d05-41b9-b7ac-860c4af81932");
            var personNumber = "196543";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
               .WaitFormHomePageToLoad(true, false, true);

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(outcome1.ToString())
                .ValidateResultElementPresent(outcome2.ToString())
                .ValidateResultElementPresent(outcome3.ToString())
                .ValidateResultElementPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-25006")]
        [Description("Open a person form record (document = 'Automation - Person Form 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = Yes - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FilterActionsOutcomesByPersonFormDocument_UITestMethod002()
        {
            var outcome1 = new Guid("70b44d74-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type one
            var outcome2 = new Guid("62c36b85-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type two
            var outcome3 = new Guid("e825b125-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type three
            var outcome4 = new Guid("deafe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type four
            var outcome5 = new Guid("e2afe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type five
            var outcome6 = new Guid("efba0d39-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type six

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("5b390688-6d05-41b9-b7ac-860c4af81932");
            var personNumber = "196543";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Person Action / Outcome Type one")
                .TapSearchButton()
                .ValidateResultElementPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementNotPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-25007")]
        [Description("Open a person form record (document = 'Automation - Person Form 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = No and linked to the current document type - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FilterActionsOutcomesByPersonFormDocument_UITestMethod003()
        {
            var outcome1 = new Guid("70b44d74-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type one
            var outcome2 = new Guid("62c36b85-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type two
            var outcome3 = new Guid("e825b125-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type three
            var outcome4 = new Guid("deafe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type four
            var outcome5 = new Guid("e2afe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type five
            var outcome6 = new Guid("efba0d39-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type six

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("5b390688-6d05-41b9-b7ac-860c4af81932");
            var personNumber = "196543";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);



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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Person Action / Outcome Type three")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-25008")]
        [Description("Open a person form record (document = 'Automation - Person Form 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Search by record marked with 'Applicable To All Documents' = No and with no link to the current document type - " +
            "Validate that the matching records are displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FilterActionsOutcomesByPersonFormDocument_UITestMethod004()
        {
            var outcome1 = new Guid("70b44d74-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type one
            var outcome2 = new Guid("62c36b85-62ad-eb11-a323-005056926fe4"); //Person Action / Outcome Type two
            var outcome3 = new Guid("e825b125-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type three
            var outcome4 = new Guid("deafe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type four
            var outcome5 = new Guid("e2afe12c-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type five
            var outcome6 = new Guid("efba0d39-b8b7-eb11-a323-005056926fe4"); //Person Action / Outcome Type six

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("5b390688-6d05-41b9-b7ac-860c4af81932");
            var personNumber = "196543";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);



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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Person Action / Outcome Type five")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome1.ToString())
                .ValidateResultElementNotPresent(outcome2.ToString())
                .ValidateResultElementNotPresent(outcome3.ToString())
                .ValidateResultElementNotPresent(outcome4.ToString())
                .ValidateResultElementNotPresent(outcome5.ToString())
                .ValidateResultElementNotPresent(outcome6.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10462

        [TestProperty("JiraIssueID", "CDV6-24958")]
        [Description("Testing server side rules for person forms - Scenario 1 - Rule references the person record associated with the Form - " +
            "Person DOB matches a specific date - Rule should be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod001()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");

            //update the person DOB 
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2003, 2, 1));

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 1").TapOKButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24959")]
        [Description("Testing server side rules for person forms - Scenario 1 - Rule references the person record associated with the Form - " +
            "Person DOB DOT NOT matches a specific date - Rule should NOT BE triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod002()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");

            //update the person DOB 
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2003, 2, 2));

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24960")]
        [Description("Testing server side rules for person forms - Scenario 2 - Rule references the person record associated with the Form - " +
            "Person Mobile Phone matches a specific value - Rule should be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod003()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonMobilePhone(personID, "965478284");

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 2").TapOKButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24961")]
        [Description("Testing server side rules for person forms - Scenario 2 - Rule references the person record associated with the Form - " +
            "Person Mobile Phone DO NOT matches a specific value - Rule should NOT be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod004()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonMobilePhone(personID, "965478285");

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24962")]
        [Description("Testing server side rules for person forms - Scenario 3 - Rule references the person record associated with the Form - " +
            "Person Business Phone matches the Person Mobile Phone - Rule should be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod005()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 3");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonBusinessPhone(personID, "965478284");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonMobilePhone(personID, "965478284");

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 3").TapOKButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24963")]
        [Description("Testing server side rules for person forms - Scenario 3 - Rule references the person record associated with the Form - " +
            "Person Business Phone DO NOT matches the Person Mobile Phone - Rule should NOT be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod006()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 3");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonBusinessPhone(personID, "965478284");

            //update the person Mobile Phone
            dbHelper.person.UpdatePersonMobilePhone(personID, "965478285");

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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24964")]
        [Description("Testing server side rules for person forms - Scenario 4 - Rule references the Form record itself - " +
            "Person Form start date matches a specific date - Rule should be triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod007()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 6, 8);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 4");


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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 4").TapOKButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24965")]
        [Description("Testing server side rules for person forms - Scenario 4 - Rule references the Form record itself - " +
            "Person Form start date DO NOT matches a specific date - Rule should NOT BE triggered")]
        [TestMethod]
        [TestCategory("UITest")]
        public void EnableServerSideRules_UITestMethod008()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("7902656b-e46f-403b-bdf7-a486c0635802");
            var personNumber = "98789";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 6, 10);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-127")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 4");


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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .TapEditAssessmentButton();

            automationPersonForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(personFormID.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11014

        [TestProperty("JiraIssueID", "CDV6-25009")]
        [Description("Open a person form record (document = 'Automation - Person Form 1') - Add a new Form Action/Outcome - " +
            "Wait for the Form Action/Outcome new page to load - Click on the Actions/Outcomes lookup button - " +
            "Wait for the lookup popup to load - Validate that Inactive records are not displayed" +
            "Validate that 'Form Action/Outcome Type' records marked with 'Applicable To All Documents' = Yes are displayed - ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void InactiveActionOutcomeTypes_UITestMethod001()
        {
            var outcome7 = new Guid("6e437dde-c0d8-eb11-a325-005056926fe4"); //Person Action / Outcome Type seven

            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("5b390688-6d05-41b9-b7ac-860c4af81932");
            var personNumber = "196543";
            var documentid = new Guid("7127ef1e-4d9e-e911-a2c6-005056926fe4"); //Automation - Person Form 1
            var startDate = new DateTime(2021, 5, 1);

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);



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
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad();

            personFormActionsOutcomesPageFrame
                .WaitForPersonFormActionsOutcomesPageFrameToLoad()
                .TapNewButton();

            personFormActionOutcomePage
                .WaitForPersonFormActionOutcomePageToLoad()
                .TapActionsOutcomesLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementNotPresent(outcome7.ToString())
                .TypeSearchQuery("Person Action / Outcome Type seven")
                .TapSearchButton()
                .ValidateResultElementNotPresent(outcome7.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11559

        [TestProperty("JiraIssueID", "CDV6-11744")]
        [Description("Login CD--> Click WorkPlace--> Select People under My Work--> Select people with at least one Person Form record-> Click Menu-->Click Related Items--> Click Forms ( Person )--> Click Create New Record Icon" +
            "Should redirect to Forms(Person) record creation screen." +
            "Verify the new field Review Status" +
            "Should not show review status field.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BOPersonForms_UITestMethod001()
        {

            //Create a new person form
            //var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapNewButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .ValidateReviewStatusFieldTitleVisible(false);



        }

        [TestProperty("JiraIssueID", "CDV6-11924")]
        [Description("Login CD--> Click WorkPlace--> Select People under My Work--> Select people with at least one Person Form record-> Click Menu-->Click Related Items--> Click Forms ( Person )--> Open any existing Person Forms created through Provider Portal." +
           "Should display the existing person forms details page." +
           "Verify the new field Review Status" +
           "Should display a optional field Review status with two options Accepted / Rejected.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BOPersonForms_UITestMethod002()
        {
            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form with created on portal set to true
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .ValidateReviewStatusFieldTitleVisible(true)
                .ValidateReviewStatusFieldOption("Accepted")
                .ValidateReviewStatusFieldOption("Rejected");


        }


        [TestProperty("JiraIssueID", "CDV6-11722")]
        [Description("Login CD -> Click Settings -> Portals -> Websites -> Create New Record icon" +
           "Should redirect to New website creation page" +
           "Tap on Look up of the filed Applications" +
           "App Name list should contain a New option Provider Portal")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BOPersonForms_UITestMethod003()
        {
            var application1ID = dbHelper.application.GetByName("Provider Portal")[0]; //CareDirector

            loginPage
               .GoToLoginPage()
               .Login("CW_Forms_Test_User_1", "Passw0rd_!")
               .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                 .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Provider Portal").TapSearchButton().SelectResultElement(application1ID.ToString());


        }
        #endregion


        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

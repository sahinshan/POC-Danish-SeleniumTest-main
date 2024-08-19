using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]


    public class Person_PersonFormInvolvement_UITestCases : FunctionalTest
    {

        private string EnvironmentName;
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
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _caseStatusId;
        private Guid _personCarePlanID;
        private Guid _carePlanType01;
        private Guid _dataFormId;
        private Guid _newPersonID;
        private int _newPersonNumber;
        private Guid _newCaseId;
        private Guid _documentId;
        private Guid _documentCategoryId;
        private Guid _documentTypeId;
        private Guid _involvementRoleId;
        private string involvementName = "Responsible Team" + DateTime.Now.ToString("yyyyMMddHHmm_FFFFFFF");
        private string _documentName = "Test_19425" + DateTime.Now;

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

                #region Create SystemUser Record

                commonMethodsDB.CreateSystemUserRecord("CW_Forms_Test_User_1", "CW", "Forms Test User 1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                commonMethodsDB.CreateSystemUserRecord("CW_Admin_Test_User_2", "CW_Admin_Test", "User_2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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

                #region NewPerson

                var personFirstName = "CDV6_14299_";
                var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                _personFullName = personFirstName + " " + personLastName;

                #endregion

                #region Care Plan Type

                var carePlanExist = dbHelper.carePlanType.GetByName("Activities of Daily Living").Any();
                if (!carePlanExist)
                {
                    _carePlanType = dbHelper.carePlanType.CreateCarePlanTypeId("Activities of Daily Living", DateTime.Now, _careDirectorQA_TeamId);
                }
                if (_carePlanType == Guid.Empty)
                {
                    _carePlanType = dbHelper.carePlanType.GetByName("Activities of Daily Living").FirstOrDefault();
                }

                carePlanExist = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").Any();
                if (!carePlanExist)
                {
                    _carePlanType01 = dbHelper.carePlanType.CreateCarePlanTypeId("Mental Health Crisis Plan", DateTime.Now, _careDirectorQA_TeamId);
                }
                if (_carePlanType01 == Guid.Empty)
                {
                    _carePlanType01 = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").FirstOrDefault();
                }

                #endregion

                #region Document Category
                _documentCategoryId = dbHelper.documentCategory.GetByName("Case Form").FirstOrDefault();

                #endregion

                #region Document Type
                _documentTypeId = dbHelper.documentType.GetByName("Initial Assessment").FirstOrDefault();

                #endregion

                #region Document 
                var DocumentExist = dbHelper.document.GetDocumentByName(_documentName).Any();
                if (!DocumentExist)
                {
                    _documentId = dbHelper.document.CreateDocument(_documentName, _documentCategoryId, _documentTypeId, _careDirectorQA_TeamId, 100000000);
                }

                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName(_documentName).FirstOrDefault();
                }

                #endregion

                #region InvolvementRole 
                _involvementRoleId = dbHelper.InvolvementRole.CreateInvolvementRole(_careDirectorQA_TeamId, involvementName, "CW_1", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11558


        [TestProperty("JiraIssueID", "CDV6-11695")]
        [Description(
           "Verify the new option Person Form Involvement created under Related Items of a selected Person Form in CD. ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormTasks_UITestMethod01()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
            .GoToLoginPage()
            .Login("CW_Admin_Test_User_2", "Passw0rd_!")
            .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad();


        }


        [TestProperty("JiraIssueID", "CDV6-11696")]
        [Description(
           "Verify the existing Person Form Involvement records of the new option Person Form Involvement under Related Items of a selected Person Form.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod02()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString());
            //                .OpenRecord(personforminvolvementId.ToString());


        }

        [TestProperty("JiraIssueID", "CDV6-11697")]
        [Description(
           "Verify the No Records text in Person Form Involvement screen when there is no existing records to display.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod03()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateNoRecordMessageVisibile(true);
            //                .OpenRecord(personforminvolvementId.ToString());


        }


        [TestProperty("JiraIssueID", "CDV6-11699")]
        [Description(
           "Verify the fields of Person Form Involvement creation")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod04()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .TapNewRecord();

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ValidatePersonFormField("Test_19425")
                .ValidateInvolvementMemberField("")
                .ValidateCreatedOnPortalCheckedOption(true);

            //                .OpenRecord(personforminvolvementId.ToString());


        }

        [TestProperty("JiraIssueID", "CDV6-11704")]
        [Description(
            "Verify the delete functionality of Person Form Involvement records")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod05()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString())
                .TapDeleteButton();
            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();


            //                .OpenRecord(personforminvolvementId.ToString());


        }

        [TestProperty("JiraIssueID", "CDV6-11705")]
        [Description(
           "Person Form Involvement-Verify the Cancel option during Delete functionality ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod06()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString())
                .TapDeleteButton();
            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapCancelButton();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
            .ValidateRecordPresent(personforminvolvementId.ToString());

            //                .OpenRecord(personforminvolvementId.ToString());


        }



        [TestProperty("JiraIssueID", "CDV6-11706")]
        [Description(
           "Verify the Create new record feature of Person Form Involvement")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod07()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .TapNewRecord();

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CW_Forms_Test_User_CDV6_13981").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberRoleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery(involvementName).TapSearchButton().SelectResultElement(_involvementRoleId.ToString());

            personFormInvolvementRecordSubPage
                            .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                            .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                            .TapSaveButton()
                            .TapBackButton();

            var personforminvolvementId = dbHelper.personFormInvolvement.GetByPersonFormInvolvementId(personFormID);

            Assert.AreEqual(1, personforminvolvementId.Count);
        }



        [TestProperty("JiraIssueID", "CDV6-11707")]
        [Description(
           "Verify the Create new record feature of Form Involvement using Save and Return to Previous page")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod08()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .TapNewRecord();

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CW_Forms_Test_User_CDV6_13981").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberRoleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery(involvementName).TapSearchButton().SelectResultElement(_involvementRoleId.ToString());

            personFormInvolvementRecordSubPage
                            .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                            .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                            .TapSaveAndCloseButton();

            var personforminvolvementId = dbHelper.personFormInvolvement.GetByPersonFormInvolvementId(personFormID);

            Assert.AreEqual(1, personforminvolvementId.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-11708")]
        [Description(
           "Verify the Create new record feature of Form Involvement without filling one or more mandatory fields")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod09()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .TapNewRecord();

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapSaveAndCloseButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.");


        }

        [TestProperty("JiraIssueID", "CDV6-11709")]
        [Description(
          "Verify the Create new record feature of Person Form Involvement without filling optional  fields")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod010()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .TapNewRecord();

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CW_Forms_Test_User_CDV6_13981").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .ClickInvolvementMemberRoleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery(involvementName).TapSearchButton().SelectResultElement(_involvementRoleId.ToString());

            personFormInvolvementRecordSubPage
                .WaitForPersonFormInvolvementRecordSubPageToLoad("New")
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapSaveAndCloseButton();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ClickRefreshButton(); ;

            var personforminvolvementId = dbHelper.personFormInvolvement.GetByPersonFormInvolvementId(personFormID);

            Assert.AreEqual(1, personforminvolvementId.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-11749")]
        [Description(
           "Verify the Edit functionality of existing Person Form Involvement records")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod011()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString())
               .OpenRecord(personforminvolvementId.ToString());

            personFormInvolvementRecordSubPage
              .WaitForPersonFormInvolvementRecordSubPageToLoadWithnoHeader()
             .InsertStartDate(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"))
              .TapSaveAndCloseButton();

            var personforminvolvementIdcount = dbHelper.personFormInvolvement.GetByPersonFormInvolvementId(personFormID);

            Assert.AreEqual(1, personforminvolvementIdcount.Count);



        }

        [TestProperty("JiraIssueID", "CDV6-11752")]
        [Description(
           "Verify the Edit functionality of existing Person Form Involvement records by leaving mandatory details as blank")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod012()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString())
               .OpenRecord(personforminvolvementId.ToString());

            personFormInvolvementRecordSubPage
              .WaitForPersonFormInvolvementRecordSubPageToLoadWithnoHeader()
             .InsertStartDate("")
              .TapSaveAndCloseButton()

                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.");



        }

        [TestProperty("JiraIssueID", "CDV6-11755")]
        [Description(
         "Verify the Delete functionality of  Person Form Involvement record from the details page")]
        [TestMethod]
        [TestCategory("UITest")]
        public void PersonFormInvolvement_UITestMethod013()
        {

            //remove all Person Forms for the case record
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //Create a new person form
            var personFormID = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId, DateTime.Now);
            var personforminvolvementId = dbHelper.personFormInvolvement.CreatePersonFormInvolement(_careDirectorQA_TeamId, _personID, personFormID, _systemUserId, _involvementRoleId, DateTime.Today);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad()
                .NavigateToPersonFormsInvolvementPage();

            personFormInvolvementRecordPage
                .WaitForPersonFormInvolvementRecordPageToLoad()
                .ValidateRecordPresent(personforminvolvementId.ToString())
               .OpenRecord(personforminvolvementId.ToString());

            personFormInvolvementRecordSubPage
              .WaitForPersonFormInvolvementRecordSubPageToLoadWithnoHeader()
              .TapDeleteRecordButton();
            System.Threading.Thread.Sleep(2000);
            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();



            System.Threading.Thread.Sleep(2000);
            var personforminvolvementIdcount = dbHelper.personFormInvolvement.GetByPersonFormInvolvementId(personFormID);

            Assert.AreEqual(0, personforminvolvementIdcount.Count);



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

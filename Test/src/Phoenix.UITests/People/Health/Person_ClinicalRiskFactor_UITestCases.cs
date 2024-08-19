using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Health
{

    [TestClass]
    public class Person_ClinicalRiskFactor_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _systemUserId;
        private string _systemUserFullName;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _clinicalRiskFactorTypeName;
        private Guid _clinicalRiskFactorTypeId;
        private string _clinicalRiskFactorEndReasonName;
        private Guid _clinicalRiskFactorEndReasonId;

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

                #region Create SystemUser Record

                _systemUserName = "Person_Clinical_Risk_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Clinical Risk", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Clinical Risk User1";

                #endregion

                #region Person

                var firstName = "Automation";
                var lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = (string)dbHelper.person.GetPersonById(_personId, "fullname")["fullname"];

                #endregion

                #region Cinical Risk Factor Type

                _clinicalRiskFactorTypeName = "3. Green";
                _clinicalRiskFactorTypeId = commonMethodsDB.CreateClinicalRiskFactorType(_careDirectorQA_TeamId, _clinicalRiskFactorTypeName, new DateTime(2021, 1, 1), null);

                #endregion

                #region Clinical Risk Factor End Reason

                _clinicalRiskFactorEndReasonName = "End Reason_1";
                _clinicalRiskFactorEndReasonId = commonMethodsDB.CreateClinicalRiskfactorEndReason(_careDirectorQA_TeamId, _clinicalRiskFactorEndReasonName, new DateTime(2021, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11951

        [TestProperty("JiraIssueID", "CDV6-12341")]
        [Description("Save record with Date Identified after the End Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("26/08/2021")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertEndDate("25/08/2021")
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Identified cannot be after End Date.").TapOKButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New");

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12343")]
        [Description("Save record with Mitigation Actioned after the End Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("26/08/2021")
                .InsertEndDate("25/08/2021")
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Mitigation Actioned cannot be after End Date.").TapOKButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New");

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12344")]
        [Description("Set Date Identified as future date and try to save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Identified cannot be a future date.").TapOKButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New");

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12346")]
        [Description("Set Date Mitigation Actioned as future date and try to save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertDateMitigationActioned(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Mitigation Actioned cannot be a future date.").TapOKButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New");

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12347")]
        [Description("Set End Date as future date and try to save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases05()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertEndDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date cannot be a future date.").TapOKButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New");

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12348")]
        [Description("Create new record with data in all mandatory fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases06()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("25/08/2021")
                .InsertEndDate("26/08/2021")
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad();

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            personClinicalRiskFactorsPage
                .OpenPersonClinicalRiskFactorRecord(records[0].ToString());

            personClinicalRiskFactorRecordPage
                .WaitForInactivePersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ValidatePersonLinkFieldText(_person_fullName)
                .ValidateRelatedCaseLinkFieldText("")
                .ValidateLinkedFormLinkFieldText("")
                .ValidateRiskFactorTypeLinkFieldText(_clinicalRiskFactorTypeName)
                .ValidateRiskFactorSubTypeLinkFieldText("")
                .ValidateSensitiveInformationYesOptionChecked(false)
                .ValidateSensitiveInformationNoOptionChecked(true)
                .ValidateDescription("Description info ...")
                .ValidateTriggers("")
                .ValidateRiskLevelLinkFieldText("")
                .ValidateDateIdentifiedText("24/08/2021")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateResponsibleParty("")
                .ValidateRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ValidateDateMitigationActionedText("25/08/2021")
                .ValidateSignedOffYesOptionChecked(false)
                .ValidateSignedOffNoOptionChecked(true)
                .ValidateEndDate("26/08/2021")
                .ValidateReasonEndedLinkFieldText(_clinicalRiskFactorEndReasonName)
                .ValidateEndedByLinkFieldText(_systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-12349")]
        [Description("Create new record with data in all mandatory fields and delete it afterwards")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases07()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("25/08/2021")
                .InsertEndDate("26/08/2021")
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForInactivePersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ValidatePersonLinkFieldText(_person_fullName)
                .ValidateRelatedCaseLinkFieldText("")
                .ValidateLinkedFormLinkFieldText("")
                .ValidateRiskFactorTypeLinkFieldText(_clinicalRiskFactorTypeName)
                .ValidateRiskFactorSubTypeLinkFieldText("")
                .ValidateSensitiveInformationYesOptionChecked(false)
                .ValidateSensitiveInformationNoOptionChecked(true)
                .ValidateDescription("Description info ...")
                .ValidateTriggers("")
                .ValidateRiskLevelLinkFieldText("")
                .ValidateDateIdentifiedText("24/08/2021")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateResponsibleParty("")
                .ValidateRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ValidateDateMitigationActionedText("25/08/2021")
                .ValidateSignedOffYesOptionChecked(false)
                .ValidateSignedOffNoOptionChecked(true)
                .ValidateEndDate("26/08/2021")
                .ValidateReasonEndedLinkFieldText(_clinicalRiskFactorEndReasonName)
                .ValidateEndedByLinkFieldText(_systemUserFullName);


            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            personClinicalRiskFactorRecordPage
                .ClickDeleteButtonOnInactiveRecord();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad();

            records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12350")]
        [Description("Create new record with data in all fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases08()
        {
            #region Clinical Risk Factor Type

            var _clinicalRiskFactorTypeName2 = "1. Red";
            var _clinicalRiskFactorTypeId2 = commonMethodsDB.CreateClinicalRiskFactorType(_careDirectorQA_TeamId, _clinicalRiskFactorTypeName2, new DateTime(2021, 1, 1), null);

            #endregion

            #region Clinical Risk Factor Sub Type

            var _clinicalRiskFactorSubTypeName = "Red Test";
            var _clinicalRiskFactorSubTypeId = commonMethodsDB.CreateClinicalRiskFactorSubType(_careDirectorQA_TeamId, _clinicalRiskFactorSubTypeName, new DateTime(2020, 1, 1), _clinicalRiskFactorTypeId2);

            #endregion

            #region Clinical Risk Level

            var _clinicalRiskLevelName = "Confirmed Low";
            var _clinicalRiskLevelId = commonMethodsDB.CreateClinicalRiskLevel(_careDirectorQA_TeamId, _clinicalRiskLevelName, new DateTime(2021, 1, 1));

            #endregion

            #region Provider (Carer)

            var _providerId_Carer = commonMethodsDB.CreateProvider("Gwynedd - Adult Support Team - Provider", _careDirectorQA_TeamId, 7);

            #endregion

            #region Community and Clinic Team

            var _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Carer, _careDirectorQA_TeamId, "Gwynedd - Adult Support Team - Primary Team", "Created by Health Appointments");
            dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, _communityAndClinicTeamId, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Closed Under Review").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "NHS Patient", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Consultants and Support", new DateTime(2020, 1, 1));

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion Contact Source

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Automation");

            string _caseNumber = (string)dbHelper.Case.GetCaseById(_caseId, "casenumber")["casenumber"];
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRelatedCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_caseNumber).SelectResultElement(_caseId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName2).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId2.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorSubTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorSubTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSensitiveInformationYesRadioButton()
                .InsertDescription("Description info ...")
                .InsertTriggers("Triggers info ...")
                .ClickRiskLevelLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskLevelName).TapSearchButton().SelectResultElement(_clinicalRiskLevelId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDateIdentified("24/08/2021")
                .InsertResponsibleParty("Responsible party info ...")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("25/08/2021")
                .ClickSignedOffYesRadioButton()
                .InsertEndDate("26/08/2021")
                .ClickReasonEndedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorEndReasonName).TapSearchButton().SelectResultElement(_clinicalRiskFactorEndReasonId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickEndedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad();

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            personClinicalRiskFactorsPage
                .ValidateRecordCellText(records[0].ToString(), 2, _clinicalRiskFactorTypeName2)
                .ValidateRecordCellText(records[0].ToString(), 3, _clinicalRiskFactorSubTypeName)
                .ValidateRecordCellText(records[0].ToString(), 4, _clinicalRiskLevelName)
                .ValidateRecordCellText(records[0].ToString(), 5, "24/08/2021")
                .ValidateRecordCellText(records[0].ToString(), 6, "Yes")
                .ValidateRecordCellText(records[0].ToString(), 7, "26/08/2021")
                .ValidateRecordCellText(records[0].ToString(), 8, "CareDirector QA")
                .OpenPersonClinicalRiskFactorRecord(records[0].ToString());

            personClinicalRiskFactorRecordPage
                .WaitForInactivePersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ValidatePersonLinkFieldText(_person_fullName)
                .ValidateRelatedCaseLinkFieldText(_caseTitle)
                .ValidateLinkedFormLinkFieldText("")
                .ValidateRiskFactorTypeLinkFieldText(_clinicalRiskFactorTypeName2)
                .ValidateRiskFactorSubTypeLinkFieldText(_clinicalRiskFactorSubTypeName)
                .ValidateSensitiveInformationYesOptionChecked(true)
                .ValidateSensitiveInformationNoOptionChecked(false)
                .ValidateDescription("Description info ...")
                .ValidateTriggers("Triggers info ...")
                .ValidateRiskLevelLinkFieldText("Confirmed Low")
                .ValidateDateIdentifiedText("24/08/2021")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateResponsibleParty("Responsible party info ...")
                .ValidateRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ValidateDateMitigationActionedText("25/08/2021")
                .ValidateSignedOffYesOptionChecked(true)
                .ValidateSignedOffNoOptionChecked(false)
                .ValidateEndDate("26/08/2021")
                .ValidateReasonEndedLinkFieldText(_clinicalRiskFactorEndReasonName)
                .ValidateEndedByLinkFieldText(_systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-12352")]
        [Description("Validate Clinical Risk Status History record after update")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertResponsibleParty("Responsible party info ...")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("25/08/2021")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .InsertResponsibleParty("Responsible party info updated ...")
                .ClickSaveButton()
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ValidateResponsibleParty("Responsible party info updated ...")
                .NavigateToPersonClinicalRiskFactorHistory();

            personClinicalRiskFactorHistoryPage
                .WaitForPersonClinicalRiskFactorHistoryPageToLoad();

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            var historyRecords = dbHelper.personClinicalRiskFactorHistory.GetByPersonClinicalRiskFactorId(records[0]);
            Assert.AreEqual(1, historyRecords.Count);

            personClinicalRiskFactorHistoryPage
                .OpenPersonClinicalRiskFactorRecord(historyRecords[0].ToString());

            personClinicalRiskFactorHistoryRecordPage
                .WaitForInactivePersonClinicalRiskFactorHistoryRecordPageToLoad("Clinical Risk Factor history for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ValidatePersonLinkFieldText(_person_fullName)
                .ValidateLinkedFormLinkFieldText("")
                .ValidateRiskFactorTypeLinkFieldText(_clinicalRiskFactorTypeName)
                .ValidateRiskFactorSubTypeLinkFieldText("")
                .ValidateSensitiveInformationYesOptionChecked(false)
                .ValidateSensitiveInformationNoOptionChecked(true)
                .ValidateDescription("Description info ...")
                .ValidateTriggers("")
                .ValidateRiskLevelLinkFieldText("")
                .ValidateDateIdentifiedText("24/08/2021")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateResponsibleParty("Responsible party info ...")
                .ValidateRiskManagementMitigation("Risk Management/Mitigation info ...")
                .ValidateDateMitigationActionedText("25/08/2021")
                .ValidateSignedOffYesOptionChecked(false)
                .ValidateSignedOffNoOptionChecked(true)
                .ValidateEndDate("")
                .ValidateReasonEndedLinkFieldText("")
                .ValidateEndedByLinkFieldText("");

        }

        [TestProperty("JiraIssueID", "CDV6-12353")]
        [Description("Delete Clinical Risk Status History record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_ClinicalRiskFactor_UITestCases10()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToClinicalRiskFactorPage();

            personClinicalRiskFactorsPage
                .WaitForPersonClinicalRiskFactorsPageToLoad()
                .ClickNewRecordButton();

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .ClickRiskFactorTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_clinicalRiskFactorTypeName).TapSearchButton().SelectResultElement(_clinicalRiskFactorTypeId.ToString());

            personClinicalRiskFactorRecordPage
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("New")
                .InsertDescription("Description info ...")
                .InsertDateIdentified("24/08/2021")
                .InsertResponsibleParty("Responsible party info ...")
                .InsertRiskManagementMitigation("Risk Management/Mitigation info ...")
                .InsertDateMitigationActioned("25/08/2021")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .InsertResponsibleParty("Responsible party info updated ...")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForPersonClinicalRiskFactorRecordPageToLoad("Clinical Risk Factor for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .NavigateToPersonClinicalRiskFactorHistory();

            personClinicalRiskFactorHistoryPage
                .WaitForPersonClinicalRiskFactorHistoryPageToLoad();

            var records = dbHelper.personClinicalRiskFactor.GetPersonClinicalRiskFactorByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

            var historyRecords = dbHelper.personClinicalRiskFactorHistory.GetByPersonClinicalRiskFactorId(records[0]);
            Assert.AreEqual(1, historyRecords.Count);

            personClinicalRiskFactorHistoryPage
                .OpenPersonClinicalRiskFactorRecord(historyRecords[0].ToString());

            personClinicalRiskFactorHistoryRecordPage
                .WaitForInactivePersonClinicalRiskFactorHistoryRecordPageToLoad("Clinical Risk Factor history for " + _person_fullName + " created by " + _systemUserFullName + " ")
                .ClickDeleteButtonOnInactiveRecord();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            System.Threading.Thread.Sleep(2000);

            personClinicalRiskFactorHistoryPage
                .WaitForPersonClinicalRiskFactorHistoryPageToLoad();

            historyRecords = dbHelper.personClinicalRiskFactorHistory.GetByPersonClinicalRiskFactorId(records[0]);
            Assert.AreEqual(0, historyRecords.Count);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }
}

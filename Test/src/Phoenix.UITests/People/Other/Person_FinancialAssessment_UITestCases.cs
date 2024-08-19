using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_FinancialAssessment_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _defaultLoginUserID;
        string _loginUser_Username;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid caseNoteID;
        private Guid _authenticationproviderid;
        private Guid _financialAssessmentId;
        private Guid _financialAssessmentStatusId_Draft;
        private Guid _financialAssessmentStatusId_ReadyForAuthorisation;
        private Guid _financialAssessmentStatusId_Authorised;
        private Guid _chargingRuleTypeId;
        private Guid _incomeSupportTypeId;
        private Guid _financeScheduleTypeId;
        private Guid _financialAssessmentTypeId;
        private Guid _caseId;
        private Guid _dataFormId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _closedCaseStatusId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");



        [TestInitialize()]
        public void TestInitializationMethod()
        {

            try
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

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, "907678", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion Team

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any();
                if (!defaultLoginUserExists)
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true, 4, null, DateTime.Now.Date);

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                _loginUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

                #endregion  Create default system user

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Appointment_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Appointment_Ethnicity")[0];

                #endregion Ethnicity

                #region Person record

                var personRecordExists = dbHelper.person.GetByFirstName("Person_Assessment_11192").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "Person_PhysicalObservation_11193", "", "AutomationPhysicalObservationLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("Person_PhysicalObservation_11193").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "Person_PhysicalObservation_11193 AutomationPhysicalObservationLastName";

                #endregion Person record


                #region Data Form
                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix).Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "ContactReason" + _currentDateSuffix, new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix)[0];

                #endregion

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix).Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "ContactSource" + _currentDateSuffix, new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix)[0];

                #endregion

                #region Case Status

                _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

                #endregion

                #region Case
                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _defaultLoginUserID, _defaultLoginUserID, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

                #endregion

                #region Financial Assessment Status

                _financialAssessmentStatusId_Draft = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Draft").FirstOrDefault();
                _financialAssessmentStatusId_ReadyForAuthorisation = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Ready for Authorisation").FirstOrDefault();
                _financialAssessmentStatusId_Authorised = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Authorised").FirstOrDefault();

                #endregion

                #region Income Support Type

                _incomeSupportTypeId = dbHelper.incomeSupportType.GetByName("IS (Manual Value)").FirstOrDefault();

                #endregion

                #region Finance Schedule Type

                _financeScheduleTypeId = dbHelper.financeScheduleType.GetByName("Residential").FirstOrDefault();

                #endregion

                #region Charging Rule Type

                var chargingRuleTypeExists = dbHelper.chargingRuleType.GetChargingRuleTypeByName("Residential Permanent Stay").Any();
                if (!chargingRuleTypeExists)
                {
                    _chargingRuleTypeId = dbHelper.chargingRuleType.CreateChargingRuleType("Residential Permanent Stay", _careDirectorQA_TeamId, DateTime.Now);
                    dbHelper.incomeSupportTypeChargingRuleTypes.CreateIncomeSupportTypeChargingRuleTypes(_incomeSupportTypeId, _chargingRuleTypeId);
                    dbHelper.scheduleSetup.CreateScheduleSetup(_careDirectorQA_TeamId, _financeScheduleTypeId, _chargingRuleTypeId, new DateTime(2020, 1, 1), 100);
                    dbHelper.chargingRuleSetup.CreateChargingRuleSetup(_careDirectorQA_TeamId, _chargingRuleTypeId, 3, new DateTime(020, 1, 1));
                }
                if (_chargingRuleTypeId == Guid.Empty)
                {
                    _chargingRuleTypeId = dbHelper.chargingRuleType.GetChargingRuleTypeByName("Residential Permanent Stay").FirstOrDefault();
                }

                #endregion

                #region Financial Assessment Type

                _financialAssessmentTypeId = dbHelper.financialAssessmentType.GetByName("Full Assessment").FirstOrDefault();

                #endregion

                #region financial assessent record
                var financialAssessmentExist = dbHelper.financialAssessment.GetFinancialAssessmentByPersonID(_personID.ToString()).Any();
                if (!financialAssessmentExist)
                {
                    _financialAssessmentId = dbHelper.financialAssessment.CreateFinancialAssessment(_careDirectorQA_TeamId, _defaultLoginUserID, _personID, _financialAssessmentStatusId_Draft, _chargingRuleTypeId, null, null, _incomeSupportTypeId,
                                                                                          _defaultLoginUserID, _financialAssessmentTypeId, DateTime.Now, DateTime.Now, DateTime.Now);


                }
                if (_financialAssessmentId == Guid.Empty)
                {
                    _personID = dbHelper.financialAssessment.GetFinancialAssessmentByPersonID(_personID.ToString()).FirstOrDefault();
                }
                #endregion financial assessent record

                #region case notes

                //remove all case notes

                foreach (var recordid in dbHelper.financialAssessmentCaseNote.GetFinancialAssessmentCaseNoteByPersonID(_personID))
                    dbHelper.financialAssessmentCaseNote.DeletePersonCaseNote(recordid);

                caseNoteID = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Case Note For Physical Observation", "Notes Test", _financialAssessmentId, _personID, DateTime.Now);

                #endregion case notes
            }

            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11267

        [TestProperty("JiraIssueID", "CDV6-11204")]
        [Description("To Verify Add local fields to Activities for Clone--financialassessmentcasenote")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_FinancialAssessment_Cloning_UITestMethod01()
        {

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFinancialAssessmentsPage();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .OpenRecord(_financialAssessmentId.ToString());

            financialAssessmentRecordPage
                .WaitForFinancialAssessmentRecordPageToLoad()
                .NavigateToFinacialAssessentCaseNotesPage();


            personFinacialAssessmentCaseNotesPage
                .WaitForPersonFinancialAssessmentCaseNotesPageToLoad()
                .OpenPersonFinacialAssessmentCaseNoteRecord(caseNoteID.ToString());

            personFinancialAssessmentCaseNoteRecordPage
                .WaitForPersonFinancialAssessmentCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecordbypersonID(_personNumber.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
               "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
               "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
               "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
               "iscloned", "clonedfromid");

            var statusid = 1; //Open



            Assert.AreEqual("Case Note For Physical Observation", fields["subject"]);
            Assert.AreEqual("Notes Test", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(false, fields["issignificantevent"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(caseNoteID.ToString(), fields["clonedfromid"].ToString());

        }



        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_PhysicalObservationCaseNotes_UITestCases : FunctionalTest
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
        private Guid _personPhyscalObservationID;
        private Guid caseNoteID;
        private Guid _authenticationproviderid;
        private Guid dataformid;
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

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

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

                var firstName = "Anthony";
                var lastName = "LN_" + _currentDateSuffix;
                _personFullName = firstName + " " + lastName;

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion Person record

                #region Create Physical Observation Record

                dataformid = dbHelper.dataForm.GetByName("NEWS").FirstOrDefault();
                var personPhysicalObservationRecordExists = dbHelper.personPhysicalObservation.GetPersonPhysicalObservationByPersonID(_personID).Any();
                if (!personPhysicalObservationRecordExists)
                {
                    _personPhyscalObservationID = dbHelper.personPhysicalObservation.CreatePersonPhysicalObservation(_careDirectorQA_TeamId, _personID, DateTime.Now, "Test", DateTime.Now, dataformid);
                }
                if (_personPhyscalObservationID == Guid.Empty)
                {
                    _personPhyscalObservationID = dbHelper.personPhysicalObservation.GetPersonPhysicalObservationByPersonID(_personID).FirstOrDefault();
                }

                #endregion Person Observation Record

                #region case notes

                //remove all case notes

                foreach (var recordid in dbHelper.personPhysicalObservationCaseNote.GetPersonCaseNoteByPersonID(_personID))
                    dbHelper.personPhysicalObservationCaseNote.DeletePersonCaseNote(recordid);

                caseNoteID = dbHelper.personPhysicalObservationCaseNote.CreatePersonPhysicalObservationCaseNote(_careDirectorQA_TeamId, "Case Note For Physical Observation", "Notes Test", _personPhyscalObservationID, DateTime.Now);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-11193

        [Description("To verify Add local fields to Activities for Clone --personphysicalobservationcasenotePre Requisite:An active Person's record must exist." +
        " Active person Physical Observation Record must exist.Activities 'Case Notes ' should be available for the person Physical Observation Record.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24565")]
        public void Person_ClonePhysicalObservations_UITestMethod01()
        {
            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix).Any();
            if (!contactReasonExists)
                dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "ContactReason" + _currentDateSuffix, new DateTime(2020, 1, 1), 110000000, false);
            var _contactReasonId = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix)[0];

            #endregion

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix).Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "ContactSource" + _currentDateSuffix, new DateTime(2020, 1, 1));
            var _contactSourceId = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix)[0];

            #endregion

            #region Case Status

            var _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _defaultLoginUserID, _defaultLoginUserID, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!")
                .WaitFormHealthNLocalAuthHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .SelectView("Related (for Bed Management)")
                .OpenPersonPhysicalObservationRecord(_personPhyscalObservationID.ToString());

            personPhysicalObservationsRecordPage
              .WaitForPersonPhysicalObservationsRecordPageToLoad()
              .NavigateToPersonPhysicalObservationCaseNotesPage();

            personPhysicalObservationCaseNotesPage
                .WaitForPersonPhysicalObservationCaseNotesPageToLoad()
                .OpenPersonPhysicalObservationCaseNoteRecord(caseNoteID.ToString());

            personPhysicalObservationCaseNoteRecordPage
                .WaitForPersonPhysicalObservationCaseNoteRecordPageToLoad("Case Note For Physical Observation")
                 .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, records.Count);

        }

        #endregion

    }
}

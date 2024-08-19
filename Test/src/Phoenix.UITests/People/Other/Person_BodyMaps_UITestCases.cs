using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_BodyMaps_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;



        [TestInitialize()]
        public void Person_BodyMaps_SetupTest()
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

                #region Person

                var personRecordExists = dbHelper.person.GetByFirstName("Person_BodyMaps").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "Person_BodyMaps", "", "CDV6-13499", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("Person_BodyMaps").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "Person_BodyMaps CDV6-13499";

                #endregion

                #region System User AllActivitiesUser1

                commonMethodsDB.CreateSystemUserRecord("PersonBodyMapsUser1", "PersonBodyMaps", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13499
        [TestProperty("JiraIssueID", "CDV6-14866")]
        [Description("Login CD Web -> Work Place -> Select any existing person -> Menu -> Health -> Body Maps ->Create a new record " +
            "Date and Time of event should be pre populated with current date, time and Responsible User should be pre populate with the logged in user.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_BodyMaps_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("PersonBodyMapsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToBodyMapsPage();

            personBodyMaps
                .WaitForPersonBodyMapsPageToLoad()
                .ClickNewRecordButton();

            personBodyRecordPage
               .WaitForPersonBodyMapRecordPageToLoad()
               .ValidateResponsibleUser("PersonBodyMaps User1")
               .ValidateDateOfEvent(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

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

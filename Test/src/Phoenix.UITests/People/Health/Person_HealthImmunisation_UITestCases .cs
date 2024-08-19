using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_HealthImmunisation_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _personId;
        private int _personNumber;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Person_HealthImmunisation_User_1";

                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Health Immunisation", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "Health";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12509

        [TestProperty("JiraIssueID", "CDV6-12702")]
        [Description("Testing persons Immunisation Record Creation " +
                     "- Trying To save Immunisation Record without filling mandatory fields" +
                     "-validating Alert Message is displayed with(Some data is not correct. Please review the data in the Form.)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthImmunisation_UITestMethod01()
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
                .NavigateToImmunisationPage();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .SelectNewRecordButton();

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateNotificationErrorMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateImmunisationFieldErrorMessage("Please fill out this field.")
                .ValidatePersonResponseFieldErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-12703")]
        [Description("Testing persons Immunisation Record Creation " +
                    "-Save Immunisation Record with filling mandatory fields" +
                    "-validating Immunisation record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthImmunisation_UITestMethod02()
        {
            #region Disability Type

            var _ImmunisationTypeName = "Pertussis (whooping cough)";
            var _ImmunisationTypeId = commonMethodsDB.CreateImmunisationType(_teamId, _ImmunisationTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToImmunisationPage();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .SelectNewRecordButton();

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .ClickImmunisationTypeIdLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_ImmunisationTypeName)
                .TapSearchButton()
                .SelectResultElement(_ImmunisationTypeId.ToString());

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .SelectPersonResponseId("Accepted")
                .ClickSaveAndCloseButton();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personHealthImmunisationRecords = dbHelper.personImmunisation.GetPersonImmunisationByPersonID(_personId);
            Assert.AreEqual(1, personHealthImmunisationRecords.Count);

            var personHealthImmunisationRecordFields = dbHelper.personImmunisation.GetPersonImmunisationByID(personHealthImmunisationRecords[0], "ownerid", "personID", "immunisationtypeId", "personResponseId");
            Assert.AreEqual(_teamId.ToString(), personHealthImmunisationRecordFields["ownerid"].ToString());
            Assert.AreEqual(_personId.ToString(), personHealthImmunisationRecordFields["personid"].ToString());
            Assert.AreEqual(_ImmunisationTypeId.ToString(), personHealthImmunisationRecordFields["immunisationtypeid"].ToString());
            Assert.AreEqual(1, personHealthImmunisationRecordFields["personresponseid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12724")]
        [Description("Testing persons Immunisation Record Creation " +
                     "-Save Immunisation Record with filling mandatory fields and GivenDate and RelatedDate" +
                     "-validating Immunisation record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthImmunisation_UITestMethod03()
        {
            var date = DateTime.Now.Date;

            #region Disability Type

            var _ImmunisationTypeName = "Pertussis (whooping cough)";
            var _ImmunisationTypeId = commonMethodsDB.CreateImmunisationType(_teamId, _ImmunisationTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToImmunisationPage();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .SelectNewRecordButton();

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .ClickImmunisationTypeIdLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_ImmunisationTypeName)
                .TapSearchButton()
                .SelectResultElement(_ImmunisationTypeId.ToString());

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .SelectPersonResponseId("Accepted")
                .InsertGivenDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRelatedDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personHealthImmunisationRecords = dbHelper.personImmunisation.GetPersonImmunisationByPersonID(_personId);
            Assert.AreEqual(1, personHealthImmunisationRecords.Count);

            var personHealthImmunisationRecordFields = dbHelper.personImmunisation.GetPersonImmunisationByID(personHealthImmunisationRecords[0], "ownerid", "personID", "immunisationtypeId", "personResponseId", "dateGiven", "relatedDate");
            Assert.AreEqual(_teamId.ToString(), personHealthImmunisationRecordFields["ownerid"].ToString());
            Assert.AreEqual(_personId.ToString(), personHealthImmunisationRecordFields["personid"].ToString());
            Assert.AreEqual(_ImmunisationTypeId.ToString(), personHealthImmunisationRecordFields["immunisationtypeid"].ToString());
            Assert.AreEqual(1, personHealthImmunisationRecordFields["personresponseid"]);
            Assert.AreEqual(date.Date, personHealthImmunisationRecordFields["dategiven"]);
            Assert.AreEqual(date.Date, personHealthImmunisationRecordFields["relateddate"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12725")]
        [Description("Testing persons Immunisation Record Creation " +
                    "-Save Immunisation Record with filling mandatory fields and GivenDate and RelatedDate" +
                    "-validating Immunisation record is created" +
                    "-Validating Immunisation record is deleted ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthImmunisation_UITestMethod04()
        {
            var date = DateTime.Now.Date;

            #region Disability Type

            var _ImmunisationTypeName = "Pertussis (whooping cough)";
            var _ImmunisationTypeId = commonMethodsDB.CreateImmunisationType(_teamId, _ImmunisationTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToImmunisationPage();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .SelectNewRecordButton();

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .ClickImmunisationTypeIdLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_ImmunisationTypeName)
                .TapSearchButton()
                .SelectResultElement(_ImmunisationTypeId.ToString());

            personHealthImmunisationRecordPage
                .WaitForPersonHealthImmunisationRecordPageToLoad()
                .SelectPersonResponseId("Accepted")
                .InsertGivenDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRelatedDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personHealthImmunisationRecords = dbHelper.personImmunisation.GetPersonImmunisationByPersonID(_personId);
            Assert.AreEqual(1, personHealthImmunisationRecords.Count);

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .SelectPersonImmunisationRecord(personHealthImmunisationRecords[0].ToString())
                .SelectDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personHealthImmunisationPage
                .WaitForPersonHealthImmunisationPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            var personHealthImmunisationRecords1 = dbHelper.personImmunisation.GetPersonImmunisationByPersonID(_personId);
            Assert.AreEqual(0, personHealthImmunisationRecords1.Count);

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

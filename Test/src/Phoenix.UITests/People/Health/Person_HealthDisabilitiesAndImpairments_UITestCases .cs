using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_HealthDisabilitiesAndImpairments_UITestCases : FunctionalTest
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

                _systemUserName = "Person_HealthDisabilities_User_1";

                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Health Disabilities", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-12505

        [TestProperty("JiraIssueID", "CDV6-12584")]
        [Description("Testing persons disability Record Creation " +
                     "- Creating the disability record With End Date lesser than Start date" +
                     "-validating Alert popup is displayed with(End Date cannot be before Start Date)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod01()
        {
            var date = DateTime.Now.Date;
            var pastdate = DateTime.Now.Date.AddDays(-5);

            #region Disability Type

            var _DisabilityTypeName = "Cancer";
            var _DisabilityTypeId = commonMethodsDB.CreateDisabilityType(_teamId, _DisabilityTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Disability")
                .ClickDisabilityTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_DisabilityTypeName)
                .TapSearchButton()
                .SelectResultElement(_DisabilityTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(pastdate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be before Start Date")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12585")]
        [Description("Testing persons disability Record Creation " +
                    "- Creating the disability record With End Date greater than Start date and onset date greater than end date" +
                    "-validating Alert popup is displayed with(End Date cannot be before Onset Date)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod02()
        {
            var date = DateTime.Now.Date;
            var futuredate = DateTime.Now.Date.AddDays(5);
            var futuredate1 = DateTime.Now.Date.AddDays(10);

            #region Disability Type

            var _DisabilityTypeName = "Cancer";
            var _DisabilityTypeId = commonMethodsDB.CreateDisabilityType(_teamId, _DisabilityTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Disability")
                .ClickDisabilityTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_DisabilityTypeName)
                .TapSearchButton()
                .SelectResultElement(_DisabilityTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(futuredate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertOnSetDate(futuredate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be before Onset Date")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12587")]
        [Description("Testing persons disability Record Creation " +
                    "- Creating the disability record With End Date greater than Start date and onset date less than end date" +
                    "-validating Person Disability record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod03()
        {
            var date = DateTime.Now.Date;
            var futuredate = DateTime.Now.Date.AddDays(5);
            var futuredate1 = DateTime.Now.Date.AddDays(2);

            #region Disability Type

            var _DisabilityTypeName = "Cancer";
            var _DisabilityTypeId = commonMethodsDB.CreateDisabilityType(_teamId, _DisabilityTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Disability")
                .ClickDisabilityTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_DisabilityTypeName)
                .TapSearchButton()
                .SelectResultElement(_DisabilityTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(futuredate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertOnSetDate(futuredate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personDisabilityRecords = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(1, personDisabilityRecords.Count);

            var personDisabilityRecordFields = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByID(personDisabilityRecords[0], "ownerid", "personID", "StartDate", "EndDate", "DisabilityTypeId", "OnsetDate");
            Assert.AreEqual(_teamId.ToString(), personDisabilityRecordFields["ownerid"].ToString());
            Assert.AreEqual(date.Date, personDisabilityRecordFields["startdate"]);
            Assert.AreEqual(futuredate.Date, personDisabilityRecordFields["enddate"]);
            Assert.AreEqual(futuredate1.Date, personDisabilityRecordFields["onsetdate"]);
            Assert.AreEqual(_DisabilityTypeId.ToString(), personDisabilityRecordFields["disabilitytypeid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12588")]
        [Description("Testing persons disability Record Creation " +
                   "- Creating the disability record With End Date greater than Start date and onset date less than end date" +
                   "- Person Disability record is created+" +
                   "Select the person Disability record and click Delete Button" +
                   "Validate Person Disability record is deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod04()
        {
            var date = DateTime.Now.Date;
            var futuredate = DateTime.Now.Date.AddDays(5);
            var futuredate1 = DateTime.Now.Date.AddDays(2);

            #region Disability Type

            var _DisabilityTypeName = "Cancer";
            var _DisabilityTypeId = commonMethodsDB.CreateDisabilityType(_teamId, _DisabilityTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Disability")
                .ClickDisabilityTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_DisabilityTypeName)
                .TapSearchButton()
                .SelectResultElement(_DisabilityTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(futuredate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertOnSetDate(futuredate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personDisabilityRecords = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(1, personDisabilityRecords.Count);

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .SelectPersonDisabilityRecord(personDisabilityRecords[0].ToString())
                .SelectDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            var personDisabilityRecords1 = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(0, personDisabilityRecords1.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12591")]
        [Description("Testing persons Impairment Record Creation " +
           "- Creating the disability record With End Date greater than Start date and onset date less than end date" +
           "-Validate Person Impairment record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod05()
        {
            var date = DateTime.Now.Date;
            var futuredate = DateTime.Now.Date.AddDays(5);
            var futuredate1 = DateTime.Now.Date.AddDays(2);

            #region Impairment Type

            var _impairmentTypeName = "NO DISABILITY";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(futuredate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertOnSetDate(futuredate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personImpairmentRecords = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(1, personImpairmentRecords.Count);

            var personImpairmentRecordFields = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByID(personImpairmentRecords[0], "ownerid", "personID", "StartDate", "EndDate", "ImpairmentTypeId", "OnsetDate");
            Assert.AreEqual(_teamId.ToString(), personImpairmentRecordFields["ownerid"].ToString());
            Assert.AreEqual(date.Date, personImpairmentRecordFields["startdate"]);
            Assert.AreEqual(futuredate.Date, personImpairmentRecordFields["enddate"]);
            Assert.AreEqual(futuredate1.Date, personImpairmentRecordFields["onsetdate"]);
            Assert.AreEqual(_impairmentTypeId.ToString(), personImpairmentRecordFields["impairmenttypeid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12592")]
        [Description("Testing persons Impairment Record Creation " +
                     "- Creating the disability record With End Date greater than Start date and onset date less than end date" +
                     "- Person Impairment record is created" +
                     "Select the person Impairment record and click Delete Button" +
                     "Validate Person impairment record is deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod06()
        {
            var date = DateTime.Now.Date;
            var futuredate = DateTime.Now.Date.AddDays(5);
            var futuredate1 = DateTime.Now.Date.AddDays(2);

            #region Impairment Type

            var _impairmentTypeName = "NO DISABILITY";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(futuredate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertOnSetDate(futuredate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var personImpairmentRecords = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(1, personImpairmentRecords.Count);

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .SelectPersonDisabilityRecord(personImpairmentRecords[0].ToString())
                .SelectDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            var personImpairmentRecords1 = dbHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(_personId);
            Assert.AreEqual(0, personImpairmentRecords1.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12614")]
        [Description("Testing persons Disability Record Creation " +
                     "-Creating the  Disability record With End Date Null and Disability Type(Is None Known=No)" +
                     "-Validating Person Disability record is created and Navigate to Records Page" +
                     "-Validate Disability Icon is displayed in Records Page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod07()
        {
            var date = DateTime.Now.Date;

            #region Disability Type

            var _DisabilityTypeName = "disability none=no ";
            var _DisabilityTypeId = commonMethodsDB.CreateDisabilityType(_teamId, _DisabilityTypeName, new DateTime(2022, 1, 2));

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
                .ValidatePersonDisability_Icon(false)
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Disability")
                .ClickDisabilityTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_DisabilityTypeName)
                .TapSearchButton()
                .SelectResultElement(_DisabilityTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .ValidatePersonDisability_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12615")]
        [Description("Testing persons Impairment Record Creation " +
                     "-Creating the  Impairment record With End Date Null and Impairment Type(Is None Known=No)" +
                     "-Validating Person Disability record is created and Navigate to Records Page" +
                     "-Validate Impairment Icon is displayed in Records Page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod08()
        {
            var date = DateTime.Now.Date;

            #region Impairment Type

            var _impairmentTypeName = "impairment no";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .ValidatePersonImpariment_Icon(false)
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .ValidatePersonImpariment_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12618")]
        [Description("Testing persons Impairment Record Creation " +
                     "-Creating the Impairment record With End Date as Null and Impairment Type (Is NooN Known=No)" +
                     "-Validating Person Impairment record is created and Navigate Cases Tab -" +
                     "-Validate  Impairment Icon Is displayed in Cases Tab")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod09()
        {
            var date = DateTime.Now.Date;

            #region Impairment Type

            var _impairmentTypeName = "impairment no";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .ValidatePersonImpariment_Icon(false)
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab()
                .ValidatePersonImpariment_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12619")]
        [Description("Testing persons Impairment Record Creation " +
                     "-Creating the Impairment record With End Date as current date and Impairment Type (Is NooN Known=No)" +
                     "-Validating Person Impairment record is created and Navigate FinanceDetails Page -" +
                     "-Validate  Impairment Icon Is displayed in Finance Details Page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod10()
        {
            var date = DateTime.Now.Date;

            #region Impairment Type

            var _impairmentTypeName = "impairment no";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .ValidatePersonImpariment_Icon(false)
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFinancialAssessmentsPage();

            personFinancialDetailRecordPage
                .ValidatePersonImpariment_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12620")]
        [Description("Testing persons Impairment Record Creation " +
                     "-Creating the Impairment record With End Date as current date and Impairment Type (Is NooN Known=No)" +
                     "-Validating Person Impairment record is created and Navigate Cases Tab -" +
                     "-Validate No Impairment Icon Is displayed In Cases Tab")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthDisabilitiesAndImpairments_UITestMethod11()
        {
            var date = DateTime.Now.Date;

            #region Impairment Type

            var _impairmentTypeName = "impairment no";
            var _impairmentTypeId = commonMethodsDB.CreateImpairmentType(_teamId, _impairmentTypeName, new DateTime(2022, 1, 2));

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
                .ValidatePersonImpariment_Icon(false)
                .NavigateToPersonDisabilityImpairmentsPage();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .SelectNewRecordButton();

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .SelectDisabilityImpairment("Impairment")
                .ClickImpairmentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_impairmentTypeName)
                .TapSearchButton()
                .SelectResultElement(_impairmentTypeId.ToString());

            personHealthDisabilityImpairmentsRecordPage
                .WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personHealthDisabilityImpairmentsPage
                .WaitForPersonHealthDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab()
                .ValidatePersonImpariment_Icon(false);

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

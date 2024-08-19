using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_HealthProfessionals_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _personId;
        private int _personNumber;
        private Guid _ProfessionTypeId;
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
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Health Immunisation", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "Health";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

                #endregion

                #region Profession Type 

                _ProfessionTypeId = commonMethodsDB.CreateProfessionType(_teamId, "General Practitioner", new DateTime(2022, 10, 10));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }
        public object CW_Forms_Test_User_1 { get; private set; }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12508

        [TestProperty("JiraIssueID", "CDV6-12543")]
        [Description("Testing persons Health Professional Record Creation " +
                     "- Creating Health Professional record with help of DB Helper" +
                     "- Trying to create the second Health Professional Record for same Person" +
                     "-validating Alert popup(A Health professional of this type already exists for this person.Click OK.) is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod01()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var date = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());
            var freetext = "test";
            var phoneNo = "123456789";

            #region Provider

            var _providerId = commonMethodsDB.CreateProvider("02juldeb", _teamId, 2);

            #endregion

            #region Professional 

            var _ProfessionTypeId_Lawyer = commonMethodsDB.CreateProfessionType(_teamId, "Lawyer", new DateTime(2022, 10, 10));
            var _ProfessionalId = commonMethodsDB.CreateProfessional(_teamId, _ProfessionTypeId_Lawyer, "Mr", "Demo-8734", "User1", "professionaluser1@testmail.com");

            #endregion

            #region Health Professional Record

            dbHelper.personHealthProfessional.CreatePersonHealthProfessional(_teamId, _ProfessionTypeId, _providerId, _personId, _systemUserId, date, date, _ProfessionalId, freetext, phoneNo);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("General Practitioner")
               .TapSearchButton()
               .SelectResultElement(_ProfessionTypeId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProvideridLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("02juldeb")
                .TapSearchButton()
                .SelectResultElement(_providerId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionalUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_systemUserName)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertFreeTextName("test")
                .InsertPhoneNo("123456789")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Health professional of this type already exists for this person. Click OK.")
                .TapCloseButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12547")]
        [Description("Testing persons Health Professional Record Creation " +
                     "- creating the Health Professional Record with Start Date in future date" +
                     "-validating Error message is displayed-" +
                     "creating the Health Professional Record with start Date in present date and End Date in future Date-" +
                     "-validate the Alert popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod02()
        {
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            #region Provider

            var _providerId = commonMethodsDB.CreateProvider("02juldeb", _teamId, 2);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("General Practitioner")
               .TapSearchButton()
               .SelectResultElement(_ProfessionTypeId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProvideridLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("02juldeb")
                .TapSearchButton()
                .SelectResultElement(_providerId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate("06/09/2040")
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Start Date cannot be a future date")
                .TapOKButton();

            personHealthProfessionalsRecordPage
                .ValidateStartDateFieldErrorMessage("Please fill out this field.")
                .InsertStartDate(currentdate)
                .InsertEndDate("06/09/2040")
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be a future date")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12551")]
        [Description("Testing persons Health Professional Record Creation " +
                     "creating the Health Professional Record with start Date in present date and End Date in Past Date-" +
                    "-validate the Alert popup(End Date cannot be prior to Start Date) is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod03()
        {
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("General Practitioner")
               .TapSearchButton()
               .SelectResultElement(_ProfessionTypeId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate("06/09/2020")
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be prior to Start Date")
                .TapOKButton();


        }

        [TestProperty("JiraIssueID", "CDV6-12553")]
        [Description("Testing persons Health Professional Record Creation " +
                    "- Trying to create  Health Professional Record with missing mandatory Fields" +
                    "-validating Alert popup(Provider, Professional, Professional User or Freetext Name have to be filled) is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod04()
        {
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("General Practitioner")
               .TapSearchButton()
               .SelectResultElement(_ProfessionTypeId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate("05/09/2021")
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Provider, Professional, Professional User or Freetext Name have to be filled")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12554")]
        [Description("Testing persons Health Professional Record Creation " +
                    "- Creating Health Professional record with help of DB Helper" +
                    "- Trying to create the sceond Health Professional Record for same Person with profession type care coordinator" +
                    "-validating person Health Profession Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod05()
        {
            DateTime date = new DateTime(2022, 1, 1);
            string freetext = "test";
            string phoneNo = "123456789";

            #region Profession Type 

            var professionatypeId1 = commonMethodsDB.CreateProfessionType(_teamId, "Care Coordinatior", new DateTime(2022, 10, 10));

            #endregion

            #region Professional

            var professionalId = commonMethodsDB.CreateProfessional(_teamId, professionatypeId1, "", "CDV6-12554", "1", "CDV6_12554@mail.com");

            #endregion

            #region Create Professional User

            var professionaluserId = commonMethodsDB.CreateSystemUserRecord("ProfessionalUser1", "Professional", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Provider (Supplier)

            var providerId = commonMethodsDB.CreateProvider("CDV6-12554_Provider", _teamId, 2);

            #endregion

            #region  Health Professional Record

            dbHelper.personHealthProfessional.CreatePersonHealthProfessional(_teamId, _ProfessionTypeId, providerId, _personId, professionaluserId, date, date, professionalId, freetext, phoneNo);

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
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care Coordinatior")
                .TapSearchButton()
                .SelectResultElement(professionatypeId1.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProvideridLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CDV6-12554_Provider")
                .TapSearchButton()
                .SelectResultElement(providerId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionalUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("ProfessionalUser1")
                .TapSearchButton()
                .SelectResultElement(professionaluserId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertFreeTextName("test")
                .InsertPhoneNo("123456789")
                .ClickSaveAndCloseButton();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectRefreshButton();

            var personHealthProfessionalRecords = dbHelper.personHealthProfessional.GetPersonHealthProfessionalByPersonID(_personId);
            Assert.AreEqual(2, personHealthProfessionalRecords.Count);

            var personHealthProfessionalRecordFields = dbHelper.personHealthProfessional.GetPersonHealthProfessionalByID(personHealthProfessionalRecords[1], "ownerid", "ProfessionTypeId", "providerId", "personID", "professionaluserId", "startdate", "enddate", "professionalId", "freetextname", "phone");
            Assert.AreEqual(_teamId.ToString(), personHealthProfessionalRecordFields["ownerid"].ToString());
            Assert.AreEqual(date.Date, personHealthProfessionalRecordFields["startdate"]);
            Assert.AreEqual(date.Date, personHealthProfessionalRecordFields["enddate"]);
            Assert.AreEqual(professionatypeId1.ToString(), personHealthProfessionalRecordFields["professiontypeid"].ToString());
            Assert.AreEqual(providerId.ToString(), personHealthProfessionalRecordFields["providerid"].ToString());
            Assert.AreEqual(professionaluserId.ToString(), personHealthProfessionalRecordFields["professionaluserid"].ToString());
            Assert.AreEqual(freetext, personHealthProfessionalRecordFields["freetextname"]);
            Assert.AreEqual(phoneNo, personHealthProfessionalRecordFields["phone"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12556")]
        [Description("Testing persons Health Professional Record Creation " +
                    "- Creating Health Professional record with help of DB Helper using profession type as care coordinator" +
                    "- Trying to create the second Health Professional Record for same Person with profession type other than General Practitioner" +
                    "-validating person Health Profession Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_HealthProfessionals_UITestMethod06()
        {
            DateTime pastDate = new DateTime(2022, 11, 10);
            DateTime date = new DateTime(2022, 11, 11);
            string freetext = "test";
            string phoneNo = "123456789";

            #region Profession Type 

            var professionatypeId1 = commonMethodsDB.CreateProfessionType(_teamId, "Care Coordinatior", new DateTime(2022, 10, 10));

            #endregion

            #region Professional

            var professionalId = commonMethodsDB.CreateProfessional(_teamId, professionatypeId1, "", "CDV6-12556", "1", "CDV6_12556@mail.com");

            #endregion

            #region Create Professional User

            var professionaluserId = commonMethodsDB.CreateSystemUserRecord("ProfessionalUser1", "Professional", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Provider (Supplier)

            var providerId = commonMethodsDB.CreateProvider("CDV6-12556_Provider", _teamId, 2);

            #endregion

            #region  Health Professional Record

            dbHelper.personHealthProfessional.CreatePersonHealthProfessional(_teamId, professionatypeId1, providerId, _personId, professionaluserId, date, date, professionalId, freetext, phoneNo);

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
                .NavigateToPersonHealthProfessionalsPage();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad()
                .SelectNewRecordButton();

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionaTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care Coordinatior")
                .TapSearchButton()
                .SelectResultElement(professionatypeId1.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProvideridLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CDV6-12556_Provider")
                .TapSearchButton()
                .SelectResultElement(providerId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertStartDate(pastDate.ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(date.ToString("dd'/'MM'/'yyyy"))
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .ClickProfessionalUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("ProfessionalUser1")
                .TapSearchButton()
                .SelectResultElement(professionaluserId.ToString());

            personHealthProfessionalsRecordPage
                .WaitForPersonHealthProfessionalsRecordPageToLoad()
                .InsertFreeTextName("test")
                .InsertPhoneNo("123456789")
                .ClickSaveAndCloseButton();

            personHealthProfessionalsPage
                .WaitForPersonHealthProfessionalsPageToLoad();

            var personHealthProfessionalRecords = dbHelper.personHealthProfessional.GetPersonHealthProfessionalByPersonID(_personId);
            Assert.AreEqual(2, personHealthProfessionalRecords.Count);

            var personHealthProfessionalRecordFields = dbHelper.personHealthProfessional.GetPersonHealthProfessionalByID(personHealthProfessionalRecords[1], "ownerid", "ProfessionTypeId", "providerId", "personID", "professionaluserId", "startdate", "enddate", "professionalId", "freetextname", "phone");
            Assert.AreEqual(_teamId.ToString(), personHealthProfessionalRecordFields["ownerid"].ToString());
            Assert.AreEqual(pastDate.Date, personHealthProfessionalRecordFields["startdate"]);
            Assert.AreEqual(date.Date, personHealthProfessionalRecordFields["enddate"]);
            Assert.AreEqual(professionatypeId1.ToString(), personHealthProfessionalRecordFields["professiontypeid"].ToString());
            Assert.AreEqual(providerId.ToString(), personHealthProfessionalRecordFields["providerid"].ToString());
            Assert.AreEqual(professionaluserId.ToString(), personHealthProfessionalRecordFields["professionaluserid"].ToString());
            Assert.AreEqual(freetext, personHealthProfessionalRecordFields["freetextname"]);
            Assert.AreEqual(phoneNo, personHealthProfessionalRecordFields["phone"]);

        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion




    }
}

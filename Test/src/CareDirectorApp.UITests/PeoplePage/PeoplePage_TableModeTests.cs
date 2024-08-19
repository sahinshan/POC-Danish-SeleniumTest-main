using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People
{
    /// <summary>
    /// All tests in this validate the mobile app when it is NOT displayed in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PeoplePage_TableModeTests : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");


            //start the APP
            uIHelper = new UIHelper();
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .TapChangeUserButton();

                warningPopup
                    .WaitForWarningPopupToLoad()
                    .TapOnYesButton();

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName("Mobile_Test_User_1")
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

                //if the offline mode warning is displayed, then close it
                warningPopup.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .InsertUserName("Mobile_Test_User_1")
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                pinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
        }

        [SetUp]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            //if the person body map injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the person body map review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error popup is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning popup is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the People page
            mainMenu.NavigateToPeoplePage();
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7281")]
        [Description("UI Test for 'People' Scenario 1 - Open the People page")]
        public void PeoplePage_TestMethod01()
        {
            //wait for the PIN page to load
            peoplePage
                .WaitForPeoplePageToLoad();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7282")]
        [Description("UI Test for 'People' Scenario 2 - Validate 'People whom with I have an active involvement with' view")]
        public void PeoplePage_TestMethod02()
        {
            //
            peoplePage
                .WaitForPeoplePageToLoad()
                .VerifyThatTextVisible("Mathews MCSenna")
                .VerifyThatTextNotVisible("Airton Senna");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7283")]
        [Description("UI Test for 'People' Scenario 3 - Validate 'My Team Records' view")]
        public void PeoplePage_TestMethod03()
        {
            //
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            peoplePage
               .WaitForPeoplePageToLoad("My Team Records")
               .TypeInSearchTextBox("MCNamara")
               .TapSearchButton()
               .VerifyThatTextVisible("Pavel MCNamara", "3301e8c2-4591-ea11-a2cd-005056926fe4")
               .VerifyThatTextVisible("PNA, PNO, ST, VLG, TOW, COU, 3830-309", "3301e8c2-4591-ea11-a2cd-005056926fe4");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7284")]
        [Description("UI Test for 'People' Scenario 4 - Validate refresh button")]
        public void PeoplePage_TestMethod04()
        {
            //find and remove the person used by the test method
            var fields = this.PlatformServicesHelper.person.GetPersonByName("AAMartha", "MT.P.003", "Stevenson", "personid");
            if(fields != null && fields.Count > 0)
            {
                Guid personID = (Guid)fields["personid"];

                PlatformServicesHelper.person.UpdateLinkedAddress(personID, null);

                foreach (Guid personLanguageId in this.PlatformServicesHelper.personLanguage.GetPersonLanguageIdForPerson(personID))
                    this.PlatformServicesHelper.personLanguage.DeletePersonLanguage(personLanguageId);

                foreach (Guid personAddressID in this.PlatformServicesHelper.personAddress.GetPersonAddressIdForPerson(personID))
                    this.PlatformServicesHelper.personAddress.DeletePersonAddress(personAddressID);

                this.PlatformServicesHelper.person.DeletePerson(personID);
            }
            

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad();


            string Title = "Ms";
            string FirstName = "AAMartha";
            string MiddleName = "MT.P.003";
            string LastName = "Stevenson";
            string PreferredName = "Steve";

            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = this.PlatformServicesHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = this.PlatformServicesHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = this.PlatformServicesHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = this.PlatformServicesHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];

            string PropertyName = "PROPERTY NAME";
            string AddressLine1 = "PROPERTY NO";
            string Country = "COUNTRY";
            string AddressLine2 = "STREET";
            string AddressLine3 = "VLG";
            string UPRN = "UPRN";
            string AddressLine4 = "TOWN";
            string AddressLine5 = "COUNTY";
            string Postcode = "POSTCODE";

            string nhsNumber = "9876543210";
            string BusinessPhone = "965478281";
            string Telephone2 = "965478285";
            string Telephone3 = "965478286";
            string Telephone1 = "965478284";
            string HomePhone = "965478282";
            string MobilePhone = "965478283";
            string PrimaryEmail = "MT.P.003@mail.com";
            string SecondayEmail = "MT.P.003@mail2.com";

            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create the person record
            Guid _personID = this.PlatformServicesHelper.person.CreatePersonRecord(Title, FirstName, MiddleName, LastName, PreferredName,
                DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID,
                PropertyName, AddressLine1, Country, AddressLine2, AddressLine3, UPRN, AddressLine4, AddressLine5, Postcode,
                nhsNumber, BusinessPhone, Telephone2, Telephone3, Telephone1, HomePhone, MobilePhone, PrimaryEmail, SecondayEmail,
                AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //refresh the page and validate that the record is present
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapRefreshButton()
                .VerifyThatTextVisible("AAMartha Stevenson", _personID.ToString())
                .VerifyThatTextVisible("PROPERTY NAME, PROPERTY NO, STREET, VLG, TOWN, COUNTY, POSTCODE", _personID.ToString());
                //.VerifyThatTextVisible("AAMartha", _personID.ToString())
                //.VerifyThatTextVisible("Stevenson", _personID.ToString())
                //.VerifyThatTextVisible("Stevenson", _personID.ToString());

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7285")]
        [Description("UI Test for 'People' Scenario 5 - Validate search button")]
        public void PeoplePage_TestMethod05()
        {
            //find and remove the person used by the test method
            var fields = this.PlatformServicesHelper.person.GetPersonByName("AAMartha", "MT.P.003", "Stevenson", "personid");
            if (fields != null && fields.Count > 0)
            {
                Guid personID = (Guid)fields["personid"];

                PlatformServicesHelper.person.UpdateLinkedAddress(personID, null);

                foreach (Guid personLanguageId in this.PlatformServicesHelper.personLanguage.GetPersonLanguageIdForPerson(personID))
                    this.PlatformServicesHelper.personLanguage.DeletePersonLanguage(personLanguageId);

                foreach (Guid personAddressID in this.PlatformServicesHelper.personAddress.GetPersonAddressIdForPerson(personID))
                    this.PlatformServicesHelper.personAddress.DeletePersonAddress(personAddressID);

                this.PlatformServicesHelper.person.DeletePerson(personID);
            }

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad();


            string Title = "Ms";
            string FirstName = "AAMartha";
            string MiddleName = "MT.P.003";
            string LastName = "Stevenson";
            string PreferredName = "Mart";

            DateTime DateOfBirth = new DateTime(2000, 1, 1);
            Guid Ethnicity = this.PlatformServicesHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = this.PlatformServicesHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = this.PlatformServicesHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = this.PlatformServicesHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];

            string PropertyName = "PROPERTY NAME";
            string AddressLine1 = "PROPERTY NO";
            string Country = "COUNTRY";
            string AddressLine2 = "STREET";
            string AddressLine3 = "VLG";
            string UPRN = "UPRN";
            string AddressLine4 = "TOWN";
            string AddressLine5 = "COUNTY";
            string Postcode = "POSTCODE";

            string NHSNumber = "9876543210";
            string BusinessPhone = "965478281";
            string Telephone2 = "965478285";
            string Telephone3 = "965478286";
            string Telephone1 = "965478284";
            string HomePhone = "965478282";
            string MobilePhone = "965478283";
            string PrimaryEmail = "MT.P.003@mail.com";
            string SecondaryEmail = "MT.P.003@mail2.com";

            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create the person record
            Guid _personID = this.PlatformServicesHelper.person.CreatePersonRecord(
                Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID,
                PropertyName, AddressLine1, Country, AddressLine2, AddressLine3, UPRN, AddressLine4, AddressLine5, Postcode,
                NHSNumber, BusinessPhone, Telephone2, Telephone3, Telephone1, HomePhone, MobilePhone, PrimaryEmail, SecondaryEmail,
                AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //refresh the page and validate that the record is present
            peoplePage
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .VerifyThatTextVisible("AAMartha Stevenson", _personID.ToString())
                .VerifyThatTextVisible("PROPERTY NAME, PROPERTY NO, STREET, VLG, TOWN, COUNTY, POSTCODE", _personID.ToString());
                //.VerifyThatTextVisible("AAMartha", _personID.ToString())
                //.VerifyThatTextVisible("Stevenson", _personID.ToString());

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7286")]
        [Description("UI Test for 'People' Scenario 6 - Validate search button")]
        public void PeoplePage_TestMethod06()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TypeInSearchTextBox("MCNamara")
                .TapSearchButton()
                .VerifyThatTextVisible("Pavel MCNamara", _personid.ToString())
                .VerifyThatTextNotVisible("Mathews MCSenna");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7287")]
        [Description("UI Test for 'People' Scenario 7 - Validate search button")]
        public void PeoplePage_TestMethod07()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            string _fullAddress = "PNA, PNO, ST, VLG, TOW, COU, 3830-309";
            string _personNumber = "503366";
            string _firstname = "Pavel";
            string _lastname = "MCNamara";

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .VerifyThatTextVisible("Pavel MCNamara", _personid.ToString(), "fullname")
                .VerifyThatTextVisible(_fullAddress, _personid.ToString(), "fulladdress")
                .VerifyThatTextVisible(_personNumber.ToString(), _personid.ToString(), "personnumber")
                .VerifyThatTextVisible(_firstname.ToString(), _personid.ToString(), "firstname")
                .VerifyThatTextVisible(_lastname.ToString(), _personid.ToString(), "lastname")
                .VerifyThatTextVisible("Yes", _personid.ToString(), "representalertorhazard")
                .VerifyThatTextVisible("Male", _personid.ToString(), "genderid")
                .VerifyThatTextVisible("01/05/1960", _personid.ToString(), "dateofbirth")
                .VerifyThatTextVisible("987 654 3210", _personid.ToString(), "nhsnumber")
                .VerifyThatTextVisible("3830-309", _personid.ToString(), "postcode")
                .VerifyThatTextVisible("Mobile Test User 1", _personid.ToString(), "createdby")
                .VerifyThatTextVisible("08/05/2020 17:05", _personid.ToString(), "createdon");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7288")]
        [Description("UI Test for 'People' Scenario 8 - Validate person record sync")]
        public void PeoplePage_TestMethod08()
        {
            //find and remove the person used by the test method
            var fields = this.PlatformServicesHelper.person.GetPersonByName("AAAngler", "MT.P.005", "Robertson", "personid");
            if (fields != null && fields.Count > 0)
            {
                Guid personID = (Guid)fields["personid"];

                PlatformServicesHelper.person.UpdateLinkedAddress(personID, null);

                foreach (Guid personLanguageId in this.PlatformServicesHelper.personLanguage.GetPersonLanguageIdForPerson(personID))
                    this.PlatformServicesHelper.personLanguage.DeletePersonLanguage(personLanguageId);

                foreach (Guid personAddressID in this.PlatformServicesHelper.personAddress.GetPersonAddressIdForPerson(personID))
                    this.PlatformServicesHelper.personAddress.DeletePersonAddress(personAddressID);

                this.PlatformServicesHelper.person.DeletePerson(personID);
            }

            string Title = "Mr";
            string FirstName = "AAAngler";
            string MiddleName = "MT.P.005";
            string LastName = "Robertson";
            string PreferredName = "Ang";

            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = (DateTime.Now - DateOfBirth);
            int personAge = (zeroTime + span).Year - 1;

            Guid Ethnicity = this.PlatformServicesHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = this.PlatformServicesHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = this.PlatformServicesHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = this.PlatformServicesHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];

            string PropertyName = "PROPERTY NAME 5";
            string AddressLine1 = "PROPERTY NO 5";
            string Country = "COUNTRY 5";
            string AddressLine2 = "STREET 5";
            string AddressLine3 = "VLG 5";
            string UPRN = "UPRN 5";
            string AddressLine4 = "TOWN 5";
            string AddressLine5 = "COUNTY 5";
            string Postcode = "POSTCODE 5";

            string NHSNumber = "9876543210";
            string BusinessPhone = "965478281";
            string Telephone2 = "965478285";
            string Telephone3 = "965478286";
            string Telephone1 = "965478284";
            string HomePhone = "965478282";
            string MobilePhone = "965478283";
            string PrimaryEmail = "MT.P.005@mail.com";
            string SecondaryEmail = "MT.P.005@mail2.com";

            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create the person record
            Guid _personID = this.PlatformServicesHelper.person.CreatePersonRecord(
                Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID,
                PropertyName, AddressLine1, Country, AddressLine2, AddressLine3, UPRN, AddressLine4, AddressLine5, Postcode,
                NHSNumber, BusinessPhone, Telephone2, Telephone3, Telephone1, HomePhone, MobilePhone, PrimaryEmail, SecondaryEmail,
                AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            fields = this.PlatformServicesHelper.person.GetPersonByName("AAAngler", "MT.P.005", "Robertson", "personnumber");
            int _personNumber = (int)fields["personnumber"];

            peoplePage
               .WaitForPeoplePageToLoad()
               .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();



            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapSearchButton()
                .TapOnPersonRecordButton("AAAngler Robertson", _personID.ToString())
                .WaitForPersonPageToLoad("AAAngler Robertson")
                .ValidatePersonIDField(_personNumber.ToString())
                .ValidateTitleField("Mr")
                .ValidateFirstNameField("AAAngler")
                .ValidateMidleNameField("MT.P.005")
                .ValidateLastNameField("Robertson")
                .ValidatePreferredNameField("Ang")
                .ValidateStatedGenderField("Male")
                .ValidateDOBField("01/01/1999")
                .ValidateDateOfDeathField("")
                .ValidatePreferredLanguageField("English")
                .ValidateResponsibleTeamField("Mobile Team 1")
               // .ValidatePictureAreaField()
                .ValidateNHSNoField("9876543210")
                .ValidateReasonsForNoNHSNoField("")
                .ValidateEthnicityField("English")
                .ValidateMaritalStatusField("Married")
                .ValidateAgeField(personAge.ToString())
                .ValidateAddressTypeField("Primary")
                .ValidatePropertyTypeField("Other")
                .ValidatePropertyNameField("PROPERTY NAME 5")
                .ValidatePropertyNoField("PROPERTY NO 5")
                .ValidateStreetField("STREET 5")
                .ValidateVlgDistrictField("VLG 5")
                .ValidateTownCityField("TOWN 5")
                .ValidateCountyField("COUNTY 5")
                .ValidatePostCodeField("POSTCODE 5")
                .ValidateUPRNField("UPRN 5")
                .ValidateBusinessPhoneField("965478281")
                .ValidateHomePhoneField("965478282")
                .ValidateMobilePhoneField("965478283")
                .ValidatePrimaryEmailField("MT.P.005@mail.com")
                .ValidateTelephone1Field("965478284")
                .ValidateTelephone2Field("965478285")
                .ValidateTelephone3Field("965478286")
                .ValidateSecondaryEmailField("MT.P.005@mail2.com");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7289")]
        [Description("UI Test for 'People' Scenario 9 - validate footer data")]
        public void PeoplePage_TestMethod09()
        {
            //find and remove the person used by the test method
            var fields = this.PlatformServicesHelper.person.GetPersonByName("AAAngler", "MT.P.005", "Robertson", "personid");
            if (fields != null && fields.Count > 0)
            {
                Guid personID = (Guid)fields["personid"];

                PlatformServicesHelper.person.UpdateLinkedAddress(personID, null);

                foreach (Guid personLanguageId in this.PlatformServicesHelper.personLanguage.GetPersonLanguageIdForPerson(personID))
                    this.PlatformServicesHelper.personLanguage.DeletePersonLanguage(personLanguageId);

                foreach (Guid relationshipId in this.PlatformServicesHelper.personRelationship.GetPersonRelationshipByPersonID(personID))
                    this.PlatformServicesHelper.personRelationship.DeletePersonRelationship(relationshipId);

                foreach (Guid personAddressID in this.PlatformServicesHelper.personAddress.GetPersonAddressIdForPerson(personID))
                    this.PlatformServicesHelper.personAddress.DeletePersonAddress(personAddressID);

                this.PlatformServicesHelper.person.DeletePerson(personID);
            }

            string Title = "Mr";
            string FirstName = "AAAngler";
            string MiddleName = "MT.P.005";
            string LastName = "Robertson";
            string PreferredName = "Ang";

            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = (DateTime.Now - DateOfBirth);
            int personAge = (zeroTime + span).Year - 1;

            Guid Ethnicity = this.PlatformServicesHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = this.PlatformServicesHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = this.PlatformServicesHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = this.PlatformServicesHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];

            string PropertyName = "PROPERTY NAME 5";
            string AddressLine1 = "PROPERTY NO 5";
            string Country = "COUNTRY 5";
            string AddressLine2 = "STREET 5";
            string AddressLine3 = "VLG 5";
            string UPRN = "UPRN 5";
            string AddressLine4 = "TOWN 5";
            string AddressLine5 = "COUNTY 5";
            string Postcode = "POSTCODE 5";

            string NHSNumber = "9876543210";
            string BusinessPhone = "965478281";
            string Telephone2 = "965478285";
            string Telephone3 = "965478286";
            string Telephone1 = "965478284";
            string HomePhone = "965478282";
            string MobilePhone = "965478283";
            string PrimaryEmail = "MT.P.005@mail.com";
            string SecondaryEmail = "MT.P.005@mail2.com";

            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create the person record
            Guid _personID = this.PlatformServicesHelper.person.CreatePersonRecord(
                Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID,
                PropertyName, AddressLine1, Country, AddressLine2, AddressLine3, UPRN, AddressLine4, AddressLine5, Postcode,
                NHSNumber, BusinessPhone, Telephone2, Telephone3, Telephone1, HomePhone, MobilePhone, PrimaryEmail, SecondaryEmail,
                AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            fields = this.PlatformServicesHelper.person.GetPersonByName("AAAngler", "MT.P.005", "Robertson", "CreatedOn", "ModifiedOn");
            var _createdOn = usersettings.ConvertTimeFromUtc((DateTime)fields["createdon"]);
            var _modifiedOn = usersettings.ConvertTimeFromUtc((DateTime)fields["modifiedon"]);

            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            peoplePage
               .WaitForPeoplePageToLoad()
               .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();

            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapSearchButton()
                .TapOnPersonRecordButton("AAAngler Robertson", _personID.ToString())
                .WaitForPersonPageToLoad("AAAngler Robertson")
                .ValidateCreatedOnFooterField(_createdOn.Value.ToString("dd'/'MM'/'yyyy HH:mm"))
                .ValidateCreatedByFooterField("Mobile Test User 1")
                .ValidateModifiedOnFooterField(_modifiedOn.Value.ToString("dd'/'MM'/'yyyy HH:mm"))
                .ValidateModifiedByFooterField("Mobile Test User 1")
                ;

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7290")]
        [Description("UI Test for 'People' Scenario 10 - Validate SMS button for person record")]
        public void PeoplePage_TestMethod10()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", _personid.ToString())
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .ValidateBusinessPhoneField("")
                .ValidateHomePhoneField("")
                .ValidateMobilePhoneField("")
                .ValidateTelephone1Field("")
                .ValidateTelephone2Field("")
                .ValidateTelephone3Field("")
                .ValidateBusinessPhoneSMSButtonEnabled(true) //the buttons are allways enabled, but the tap action may or may not result in going to the sms/phone apps
                .ValidateHomePhoneSMSButtonEnabled(true)
                .ValidateMobilePhoneSMSButtonEnabled(true)
                .ValidateTelephone1SMSButtonEnabled(true)
                .ValidateTelephone2SMSButtonEnabled(true)
                .ValidateTelephone3SMSButtonEnabled(true)
                .ValidateBusinessPhoneCallButtonEnabled(true)
                .ValidateHomePhoneCallButtonEnabled(true)
                .ValidateMobilePhoneCallButtonEnabled(true)
                .ValidateTelephone1CallButtonEnabled(true)
                .ValidateTelephone2CallButtonEnabled(true)
                .ValidateTelephone3CallButtonEnabled(true);

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7291")]
        [Description("UI Test for 'People' Scenario 11 - Validate SMS button for person record")]
        public void PeoplePage_TestMethod11()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid");
            Guid _personid = (Guid)fields["personid"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .ValidateBusinessPhoneField("12341")
                .ValidateHomePhoneField("12342")
                .ValidateMobilePhoneField("12343")
                .ValidateTelephone1Field("12344")
                .ValidateTelephone2Field("12345")
                .ValidateTelephone3Field("12346")
                .ValidateBusinessPhoneSMSButtonEnabled(true)
                .ValidateHomePhoneSMSButtonEnabled(true)
                .ValidateMobilePhoneSMSButtonEnabled(true)
                .ValidateTelephone1SMSButtonEnabled(true)
                .ValidateTelephone2SMSButtonEnabled(true)
                .ValidateTelephone3SMSButtonEnabled(true)
                .ValidateBusinessPhoneCallButtonEnabled(true)
                .ValidateHomePhoneCallButtonEnabled(true)
                .ValidateMobilePhoneCallButtonEnabled(true)
                .ValidateTelephone1CallButtonEnabled(true)
                .ValidateTelephone2CallButtonEnabled(true)
                .ValidateTelephone3CallButtonEnabled(true)
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7292")]
        [Description("UI Test for 'People' Scenario 14 - Validate top screen area for person record")]
        public void PeoplePage_TestMethod14()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid");
            Guid _personid = (Guid)fields["personid"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7293")]
        [Description("UI Test for 'People' Scenario 15 - open person record and validate top banner")]
        public void PeoplePage_TestMethod15()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .ValidateMainTopBannerLabelsVisible()
                .ValidateSecondayTopBannerLabelsNotVisible()
                .ValidatePersonNameAndId_TopBanner("MCSENNA, Mathews (Id " + _personNumber.ToString() + ")")
                .ValidateBornText_TopBanner(new DateTime(2000, 1, 1))
                .ValidateGenderText_TopBanner("Male")
                .ValidateNHSNoText_TopBanner("987 654 3210")
                .ValidatePreferredNameText_TopBanner("Alcordy")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7293")]
        [Description("UI Test for 'People' Scenario 15 - open person record and validate that all field labels are coorrectly displayed")]
        public void PeoplePage_TestMethod15_1()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString());
            
            personPage
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .ValidateFieldLabelsVisible();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7294")]
        [Description("UI Test for 'People' Scenario 16 - open person record and expand top banner")]
        public void PeoplePage_TestMethod16()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .ExpandTopBanner()
                .ValidateMainTopBannerLabelsVisible()
                .ValidateSecondayTopBannerLabelsVisible()
                .ValidatePersonNameAndId_TopBanner("MCSENNA, Mathews (Id " + _personNumber.ToString() + ")")
                .ValidateBornText_TopBanner(new DateTime(2000, 1, 1))
                .ValidateGenderText_TopBanner("Male")
                .ValidateNHSNoText_TopBanner("987 654 3210")
                .ValidatePreferredNameText_TopBanner("Alcordy")
                .ValidateAddressText_TopBanner("PNA\nPNO ST \nVlg\nTO CO \nPC")
                .ValidateHomePhoneText_TopBanner("12342")
                .ValidateWorkPhoneText_TopBanner("12341")
                .ValidateMobilePhoneText_TopBanner("12343")
                .ValidateEmailText_TopBanner("MCSenna@mail.com")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7295")]
        [Description("UI Test for 'People' Scenario 17 - Expand and collapse top banner")]
        public void PeoplePage_TestMethod17()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .ExpandTopBanner()
                .CollapseTopBanner()
                .ValidateMainTopBannerLabelsVisible()
                .ValidateSecondayTopBannerLabelsNotVisible()
                .ValidatePersonNameAndId_TopBanner("MCSENNA, Mathews (Id " + _personNumber.ToString() + ")")
                .ValidateBornText_TopBanner(new DateTime(2000, 1, 1))
                .ValidateGenderText_TopBanner("Male")
                .ValidateNHSNoText_TopBanner("987 654 3210")
                .ValidatePreferredNameText_TopBanner("Alcordy")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7296")]
        [Description("UI Test for 'People' Scenario 18 - Tap Responsible Team Field")]
        public void PeoplePage_TestMethod18()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapResponsibleTeamField()
                .WaitForTeamPageToLoad("Mobile Team 1")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7297")]
        [Description("UI Test for 'People' Scenario 21 - Tap Responsible Team Field - Tap on the Back button")]
        public void PeoplePage_TestMethod21()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapResponsibleTeamField()
                .WaitForTeamPageToLoad("Mobile Team 1")
                .TapBackButton()
                ;

            this.personPage
                .WaitForPersonPageToLoad("Mathews MCSenna");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7298")]
        [Description("UI Test for 'People' Scenario 22 - Validate related items menu")]
        public void PeoplePage_TestMethod22()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .ValidateActivitiesAreaElementsVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
                .ValidateHealthAreaElementsNotVisible_RelatedItems();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7299")]
        [Description("UI Test for 'People' Scenario 25 - Tap related Items area in Related Items sub menu")]
        public void PeoplePage_TestMethod25()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .TapRelatedItemsArea_RelatedItems()
                .ValidateActivitiesAreaElementsNotVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsVisible_RelatedItems()
                .ValidateHealthAreaElementsNotVisible_RelatedItems();
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7300")]
        [Description("UI Test for 'People' Scenario 26 - Tap related Items area in Related Items sub menu")]
        public void PeoplePage_TestMethod26()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                .TapRelatedItemsButton()
                .WaitForRelatedItemsSubMenuToOpen()
                .TapHealthArea_RelatedItems()
                .ValidateActivitiesAreaElementsNotVisible_RelatedItems()
                .ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
                .ValidateHealthAreaElementsVisible_RelatedItems();
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        [Property("JiraIssueID", "")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}

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
    [Category("MobileOffline")]
    public class PeoplePage_OfflineTableModeTests : TestBase
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

            //close the lookup popup if it is open
            lookupPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the settings page
            mainMenu.NavigateToSettingsPage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if the APP is in offline mode change it to online mode
            settingsPage.SetTheAppInOnlineMode();
        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7265")]
        [Description("UI Test for 'People' Scenario 1 - Open the People page")]
        public void PeoplePage_OfflineTestMethod01()
        {
            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People");
        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7266")]
        [Description("UI Test for 'People' Scenario 2 - Validate 'My Pinned People' view")]
        public void PeoplePage_OfflineTestMethod02()
        {
            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .VerifyThatTextVisible("Mathews MCSenna")
                .VerifyThatTextNotVisible("Airton Senna");

        }


        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7267")]
        [Description("UI Test for 'People' Scenario 6 - Validate search button")]
        public void PeoplePage_OfflineTestMethod06()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TypeInSearchTextBox("MCNamara")
                .TapSearchButton()
                .VerifyThatTextVisible("Pavel MCNamara", _personid.ToString())
                .VerifyThatTextNotVisible("Mathews MCSenna");

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7268")]
        [Description("UI Test for 'People' Scenario 7 - Validate search button")]
        public void PeoplePage_OfflineTestMethod07()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            string _fullAddress = "PNA, PNO, ST, VLG, TOW, COU, 3830-309";
            string _personNumber = "503366";
            string _firstname = "Pavel";
            string _lastname = "MCNamara";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
                .VerifyThatTextVisible("27/05/2020 16:57", _personid.ToString(), "createdon");

        }



        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7269")]
        [Description("UI Test for 'People' Scenario 9 - validate footer data")]
        public void PeoplePage_OfflineTestMethod09()
        {
            Guid mobile_test_user_1_UserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile_test_user_1

            //find and remove the person used by the test method
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Angler", "MT.P.005", "Robertson", "personid");
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
            string FirstName = "Angler";
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

            this.PlatformServicesHelper.userFavouriteRecord.CreateUserFavouriteRecord(mobile_test_user_1_UserId, _personID, "person");

            fields = this.PlatformServicesHelper.person.GetPersonByName("Angler", "MT.P.005", "Robertson", "CreatedOn", "ModifiedOn");
            DateTime _createdOn = (DateTime)fields["CreatedOn".ToLower()];
            DateTime _modifiedOn = (DateTime)fields["ModifiedOn".ToLower()];

            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
               .WaitForPeoplePageToLoad("My Pinned People")
               .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad("My Team Records")
                .TapSearchButton()
                .TapOnPersonRecordButton("Angler Robertson", _personID.ToString())
                .WaitForPersonPageToLoad("Angler Robertson")
                .ValidateCreatedOnFooterField(usersettings.ConvertTimeFromUtc(_createdOn).Value.ToString("dd'/'MM'/'yyyy HH:mm"))
                .ValidateCreatedByFooterField("Mobile Test User 1")
                .ValidateModifiedOnFooterField(usersettings.ConvertTimeFromUtc(_modifiedOn).Value.ToString("dd'/'MM'/'yyyy HH:mm"))
                .ValidateModifiedByFooterField("Mobile Test User 1")
                ;

        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7270")]
        [Description("UI Test for 'People' Scenario 10 - Validate SMS button for person record")]
        public void PeoplePage_OfflineTestMethod10()
        {
            Guid _personid = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //refresh the page and validate that the record is present
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7271")]
        [Description("UI Test for 'People' Scenario 11 - Validate SMS button for person record")]
        public void PeoplePage_OfflineTestMethod11()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid");
            Guid _personid = (Guid)fields["personid"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7272")]
        [Description("UI Test for 'People' Scenario 14 - Validate top screen area for person record")]
        public void PeoplePage_OfflineTestMethod14()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid");
            Guid _personid = (Guid)fields["personid"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Mathews MCSenna", _personid.ToString())
                .WaitForPersonPageToLoad("Mathews MCSenna")
                ;


        }

        /// <summary>
        /// Open the "Test Cases - V6 Mobile app.xlsx" for more information on the Test Scenario.
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7273")]
        [Description("UI Test for 'People' Scenario 15 - open person record and validate top banner")]
        public void PeoplePage_OfflineTestMethod15()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7274")]
        [Description("UI Test for 'People' Scenario 16 - open person record and expand top banner")]
        public void PeoplePage_OfflineTestMethod16()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();
            var usersettings = this.PlatformServicesHelper.GetMetadataUserSettings();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7275")]
        [Description("UI Test for 'People' Scenario 17 - Expand and collapse top banner")]
        public void PeoplePage_OfflineTestMethod17()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7276")]
        [Description("UI Test for 'People' Scenario 18 - Tap Responsible Team Field")]
        public void PeoplePage_OfflineTestMethod18()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7277")]
        [Description("UI Test for 'People' Scenario 21 - Tap Responsible Team Field - Tap on the Back button")]
        public void PeoplePage_OfflineTestMethod21()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7278")]
        [Description("UI Test for 'People' Scenario 22 - Validate related items menu")]
        public void PeoplePage_OfflineTestMethod22()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7279")]
        [Description("UI Test for 'People' Scenario 25 - Tap related Items area in Related Items sub menu")]
        public void PeoplePage_OfflineTestMethod25()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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
        [Property("JiraIssueID", "CDV6-7280")]
        [Description("UI Test for 'People' Scenario 26 - Tap related Items area in Related Items sub menu")]
        public void PeoplePage_OfflineTestMethod26()
        {
            var fields = this.PlatformServicesHelper.person.GetPersonByName("Mathews", "MT.P.001", "MCSenna", "personid", "PersonNumber");
            Guid _personid = (Guid)fields["personid"];
            int _personNumber = (int)fields["personnumber"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            //wait for the page to load
            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
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

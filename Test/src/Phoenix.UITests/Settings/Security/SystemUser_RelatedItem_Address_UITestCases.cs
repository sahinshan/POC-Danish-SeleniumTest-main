using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-11075
    ///
    /// </summary>
    [TestClass]
    public class SystemUser_RelatedItem_Address_UITestCases : FunctionalTest
    {

        private Guid _environmentId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private string _systemUserName01;
        private string _systemUserName02;
        private Guid _systemUserId01;
        private Guid _systemUserId02;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid adminUserId;
        private string adminUserName;

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {
                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion Team

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion Business Unit

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Lanuage

                #region Create Admin user

                adminUserName = "CW_Admin_Test_User_1";
                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName(adminUserName).Any();
                if (!adminUserExists)
                {
                    adminUserId = dbHelper.systemUser.CreateSystemUser(adminUserName, "CW", "Admin Test User 1", "CW Admin Test User 1", "Passw0rd_!", adminUserName + "@somemail.com", adminUserName + "@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                }

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName(adminUserName).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion

                #region Environment

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper("CW_Admin_Test_User_2", "Passw0rd_!", tenantName);

                #endregion

                #region Create SystemUser01 Record: CW_Test_User_1

                _systemUserName01 = "CW_Test_User_1_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId01 = dbHelper.systemUser.CreateSystemUser(_systemUserName01, "CW Test", "User 1", "Address1", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                #endregion  Create SystemUser01 Record : CW_Test_User_1

                #region Create SystemUser02 Record : CW_Test_User_2

                _systemUserName02 = "CW_Test_User_2_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId02 = dbHelper.systemUser.CreateSystemUser(_systemUserName02, "CW Test", "User 1", "Address2", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);


                #endregion  Create SystemUser02 Record :CW_Test_User_2

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careProviders_TeamId, "SmokeTest_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity")[0];

                #endregion

                #region Person

                var personRecordExists = dbHelper.person.GetByFirstName("CW Automation").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "CW Automation", "", "Test User 1", "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("CW Automation").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "CW Automation Test User 1";

                #endregion


                dbHelper = new DBHelper.DatabaseHelper();

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-13416

        [TestProperty("JiraIssueID", "ACC-3277")]
        [Description("Navigate to the system users page - perform a quick search using a user first name - Validate that only the matching results are displayed" +
            "Open the user record and validate the fields in Left side as well as in Right side" + "Verify the Address type and Start date fields as Mandatory fields" +
            "Verify that the user able to select only one option from Adress type." + "Validate the Address type option as Home and work" +
            "Validate the System user field is auto populated and the field is editable.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateLeftSideFields()
                .ValidateRightSideFields();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateAddressType_MandatoryField()
                .ValidateStartDateMandatoryFields()
                .ValidateAddressType_Options("Home")
                .ValidateAddressType_Options("Work");

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .SelectAddressType_Options("Home")
                .SelectAddressType_Options("Work");

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateSystemUser_LinkField("CW Test User 1");

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateSystemUser_LinkField("CW Test User 1");

        }

        [TestProperty("JiraIssueID", "ACC-3278")]
        [Description("Navigate to the system users page - perform a quick search using a user first name - Validate that only the matching results are displayed" +
            "Open the user record Validate the auto populated system user id and Select different user and enter the mandatory fields and save the record" +
            "Open the newly created record and validate the System user field is not editable")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod002()
        {
            var startDate = DateTime.Now.AddDays(-3);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateSystemUser_LinkField("CW Test User 1")
                .ClickSystemUserLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_systemUserName02)
                .TapSearchButton()
                .SelectResultElement(_systemUserId02.ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId02);
            Assert.AreEqual(1, userAddress.Count);
            var newUserAddressId = userAddress[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .OpenRecord(newUserAddressId.ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateSystemUser_Editable(false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateLeftSideFields()
                .ValidateRightSideFields()
                .ValidateStartDateMandatoryFields()
                .ValidateAddressType_MandatoryField()
                .SelectAddressType_Options("Home")
                .SelectAddressType_Options("Work");

        }

        [TestProperty("JiraIssueID", "ACC-3279")]
        [Description("Navigate to the system users page - perform a quick search using a user first name - Open the User and Navigate to Address module" +
                    "Enter all the Fields and save the record" + "Open the same record and edit the fields" + "Save and validate the edited values are updated successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod003()
        {
            var startDate = DateTime.Now.AddDays(-4);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertPropertyName("MorningSide")
                .InsertPropertyNo("12")
                .InsertStreet("via Linda")
                .InsertVillageDistrict("Scottsdale")
                .InsertTownCity("Scottsdale")
                .InsertPostCode("85258")
                .InsertCounty("Maricopa")
                .InsertCountry("USA")
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(1, userAddress.Count);

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertCountry("UK")
                .InsertTownCity("Nottingham")
                .InsertVillageDistrict("Chandler")
                .InsertStreet("Jhon Parkway")
                .InsertPropertyName("PB Bell")
                .InsertPropertyNo("100")
                .InsertPostCode("56743")
                .InsertCounty("Coconio")
                .SelectAddressType_Options("Work")
                .ClickSaveButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(userAddress[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateCountryText("UK")
                .ValidateTownCityText("Nottingham")
                .ValidateVillageDistrictText("Chandler")
                .ValidateStreetText("Jhon Parkway")
                .ValidatePropertyNameText("PB Bell")
                .ValidatePropertyNoText("100")
                .ValidateCountyText("Coconio")
                .ValidatePostCodeText("56743")
                .ValidateAddressType_Options("Work");

        }

        [TestProperty("JiraIssueID", "ACC-3280")]
        [Description("Open the System user record and Navigate to Address Module." + "Validate Related records as the default view" +
            "Validate the start date sort")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod004()
        {
            //To create System User Addresses
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                    DateTime.Now.AddDays(-2), null);

            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "La priveda", "1", "via sahora", "Chandler", "Phoenix", "Cocino", "85634", "US", 2,
                                                                   DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRelatedRecords_Option("Related Records");

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[0].ToString())
                .ValidateRecordPosition(2, userAddress[1].ToString())
                .ClickStartDateSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[1].ToString())
                .ValidateRecordPosition(2, userAddress[0].ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3281")]
        [Description("Open the System user record and Navigate to Address Module." + "Validate column fields header" +
          "Navigate to people record Address module and validate the column header fields")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod005()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            System.Threading.Thread.Sleep(1000);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateColumnHeader(2, "Start Date", "Start Date")
                .ValidateColumnHeader(3, "End Date", "End Date")
                .ValidateColumnHeader(4, "Address Type", "Address Type")
                .ValidateColumnHeader(5, "Property Name", "Property Name")
                .ValidateColumnHeader(6, "Property No", "Property No")
                .ValidateColumnHeader(7, "Street", "Street")
                .ValidateColumnHeader(8, "Village / District", "Village / District")
                .ValidateColumnHeader(9, "Town / City", "Town / City")
                .ValidateColumnHeader(10, "County", "County")
                .ValidateColumnHeader(11, "Postcode", "Postcode");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToPersonAddressPage();

            System.Threading.Thread.Sleep(3000);

            personAddressesPage
                .WaitForPersonAddressesPageToLoad()
                .ValidateColumnHeader(2, "Start Date", "Start Date")
                .ValidateColumnHeader(3, "End Date", "End Date")
                .ValidateColumnHeader(4, "Address Type", "Address Type")
                .ValidateColumnHeader(5, "Property No", "Property No")
                .ValidateColumnHeader(6, "Property Name", "Property Name")
                .ValidateColumnHeader(7, "Street", "Street")
                .ValidateColumnHeader(8, "Vlg/District", "Vlg/District")
                .ValidateColumnHeader(9, "Town/City", "Town/City")
                .ValidateColumnHeader(10, "Postcode", "Postcode")
                .ValidateColumnHeader(11, "County", "County");

        }

        //Bug -CDV6-11769
        [TestProperty("JiraIssueID", "ACC-3282")]
        [Description("Open the System user record and Navigate to Address Module." + "Enter the Start date earlier than the end date and verify the alert pop up" +
            "Enter the start date earlier than the DOB of the user, Start date should be accepted")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod006()
        {

            //To create System User Addresses
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                    DateTime.Now.AddDays(-2), null);

            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "La priveda", "1", "via sahora", "Chandler", "Phoenix", "Cocino", "85634", "US", 2,
                                                                   DateTime.Now.Date, DateTime.Now);


            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRelatedRecords_Option("Related Records");

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(userAddress[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddYears(-15).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton()
                .WaitForSystemUserAddressRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateStartDateText(DateTime.Now.AddYears(-15).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(userAddress[1].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(1).ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TabToNextElement();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End Date cannot be before Start Date.");

        }

        [TestProperty("JiraIssueID", "ACC-3283")]
        [Description("Open the System user record and Navigate to Address Module." + "Delete single record" +
            "Delete Multiple records")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod007()
        {
            //To create System User Addresses
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                    DateTime.Now.AddDays(-2), null);

            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "La priveda", "1", "via sahora", "Chandler", "Phoenix", "Cocino", "85634", "US", 2,
                                                                   DateTime.Now.Date, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRelatedRecords_Option("Related Records");

            systemUserAddressPage
               .WaitForSystemUserAddressPageToLoad();

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .SelectRecord(userAddress[0].ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress1 = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(1, userAddress1.Count);

            //To create System User Address
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                DateTime.Now.AddDays(-12), null);


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var userAddress2 = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress2.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickSelectAllCheckBox()
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            System.Threading.Thread.Sleep(4000);

            var userAddressDeleted = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(0, userAddressDeleted.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

        }

        [TestProperty("JiraIssueID", "ACC-3284")]
        [Description("Open the System user record and Navigate to Address Module." + "Validate the sort for Property No,Property Name,Street,Village / District,Town / City,County,Post Code")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod008()
        {
            //To create System User Addresses
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                    DateTime.Now.AddDays(-2), null);

            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "La priveda", "1", "via sahora", "Chandler", "Phoenix", "Cocino", "85634", "US", 2,
                                                                   DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickRefreshButton()
                .ValidateRelatedRecords_Option("Related Records");

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPosition(1, userAddress[0].ToString())
                .ValidateRecordPosition(2, userAddress[1].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickPropertyNoSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[1].ToString())
                .ValidateRecordPosition(2, userAddress[0].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickPropertyNameSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[0].ToString())
                .ValidateRecordPosition(2, userAddress[1].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickStreetSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[1].ToString())
                .ValidateRecordPosition(2, userAddress[0].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickVillageDistrictSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[0].ToString())
                .ValidateRecordPosition(2, userAddress[1].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickTownCitySort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[1].ToString())
                .ValidateRecordPosition(2, userAddress[0].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickCountySort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[0].ToString())
                .ValidateRecordPosition(2, userAddress[1].ToString())
                .WaitForSystemUserAddressPageToLoad()
                .ClickPostCodeSort()
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRecordPosition(1, userAddress[1].ToString())
                .ValidateRecordPosition(2, userAddress[0].ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3285")]
        [Description("Open the System user record and Navigate to Address Module." + "Validate the tooltip for Address record page and User Address section")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod009()
        {
            //To create System User Address
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1, DateTime.Now.AddDays(-2), null);


            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ValidateRelatedRecords_Option("Related Records");

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(1, userAddress.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(userAddress[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateToolTipTextForSystemUser("CW Test User 1")
                .ValidateToolTipTextForPropertyName("Defines the Property name")
                .ValidateToolTipTextForPropertyNo("Defines the Property No")
                .ValidateToolTipTextForStreet("Defines the Street of the Address")
                .ValidateToolTipTextForVillageDistrict("Defines the Village or District of the Address")
                .ValidateToolTipTextForTownCity("Defines the Town of the Address")
                .ValidateToolTipTextForPostCode("Defines the Post Code of the Address")
                .ValidateToolTipTextForCounty("Defines the County of the Address")
                .ValidateToolTipTextForCountry("Defines the Country of the Address")
                .ValidateToolTipTextForAddressType("Defines the Address Type of the Address")
                .ValidateToolTipTextForStartDate("Indicates the Start Date of the address info")
                .ValidateToolTipTextForEndDate("Indicates the End Date of the address info");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName01)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateToolTipTextForPropertyName("Defines the Property name")
                .ValidateToolTipTextForPropertyNo("Defines the Property No")
                .ValidateToolTipTextForStreet("Defines the Street of the Address")
                .ValidateToolTipTextForVillageDistrict("Defines the Village or District of the Address")
                .ValidateToolTipTextForTownCity("Defines the Town of the Address")
                .ValidateToolTipTextForPostCode("Defines the Post Code of the Address")
                .ValidateToolTipTextForCounty("Defines the County of the Address")
                .ValidateToolTipTextForCountry("Defines the Country of the Address")
                .ValidateToolTipTextForAddressType("Defines the Address Type of the Address")
                .ValidateToolTipTextForStartDate("Indicates the Start Date of the address info");

        }

        [TestProperty("JiraIssueID", "ACC-3286")]
        [Description("Navigate to the system users page - perform a quick search using a user first name - Open the User and Navigate to Address module" +
                  "Verify the page title")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod010()
        {
            var startDate = DateTime.Now.AddDays(-4);
            var endDate = DateTime.Now.Date;

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertPropertyName("MorningSide")
                .InsertPropertyNo("12")
                .InsertStreet("via Linda")
                .InsertVillageDistrict("Scottsdale")
                .InsertTownCity("Scottsdale")
                .InsertPostCode("85258")
                .InsertCounty("Maricopa")
                .InsertCountry("USA")
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertEndDate(endDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(1, userAddress.Count);

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateSystemUserAddressRecordPageTitle("CW Test User 1");

        }

        [TestProperty("JiraIssueID", "ACC-3287")]
        [Description("Open the System user record and Navigate to Address Module." + "Click add new button and create the Address record with overlapping dates" + "Validate the Address Dates overlap with another Address of the same Type.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Addresses")]
        public void SystemUser_RelatedItem_Address_UITestMethod011()
        {
            var startDate = DateTime.Now.AddDays(-3);

            //To create System User Address
            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "Cabrillo", "12", "via frys", "Scottsdale", "Edison", "Maricopa", "85674", "USA", 1,
                                                                    DateTime.Now.AddDays(-12), null);

            dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _systemUserId01, "La priveda", "1", "via sahora", "Chandler", "Phoenix", "Cocino", "85634", "US", 2,
                                                                   DateTime.Now.Date, null);

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId01);
            Assert.AreEqual(2, userAddress.Count);

            loginPage
                .GoToLoginPage()
                .Login(adminUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .InsertPropertyName("MorningSide")
                .InsertPropertyNo("12")
                .InsertStreet("via Linda")
                .InsertVillageDistrict("Scottsdale")
                .InsertTownCity("Scottsdale")
                .InsertPostCode("85258")
                .InsertCounty("Maricopa")
                .InsertCountry("USA")
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Address Dates overlap with another Address of the same Type.");

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

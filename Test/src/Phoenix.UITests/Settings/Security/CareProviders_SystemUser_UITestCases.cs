using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// 
    ///
    /// </summary>
    [TestClass]
    public class CareProviders_SystemUser_UITestCases1 : FunctionalTest
    {
        private const string Format = "yyyyMMddHHmms";
        private string _environmentName;
        private Guid _careProviders_BusinessUnitId;
        private Guid _SouthEastRegion_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private Guid _systemUserId;
        private string _systemUsername;
        private Guid testHM_001UserId;
        private Guid testHM_002UserId;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _demographicsTitleId;
        public Guid maritalStatusId = new Guid("81f1f13a-6d96-e811-9c00-1866da1e3bda");//Civil Partner
        public Guid religionId = new Guid("3e044dd3-1e97-e811-80dc-005056050630");//Hindu
        private Guid _ethnicityId;
        public Guid nationalityId = new Guid("130443e2-7396-e811-80dc-005056050630");//Austrian
        public Guid countryId = new Guid("46bca7d6-2097-e811-80dc-005056050630");//india
        private Guid _transportTypeId;
        public Guid professionType = new Guid("1002981d-01b5-e811-80dc-0050560502cc");//General Practitioner
        public Guid adminUserId;
        public string adminUsername;
        public string userLastName = DateTime.Now.ToString("ddMMyyyy_HHmmss_FFFFF");
        public string yyyyMMddHHmmss { get; private set; }

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {

                #region Environment

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion Environment

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                var businessUnitExists2 = dbHelper.businessUnit.GetByName("South East Region").Any();
                if (!businessUnitExists2)
                    dbHelper.businessUnit.CreateBusinessUnit("South East Region");
                _SouthEastRegion_BusinessUnitId = dbHelper.businessUnit.GetByName("South East Region")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Title

                var demographicsTitleId = dbHelper.demographicsTitle.GetByName("Dr.").Any();
                if (!demographicsTitleId)
                    dbHelper.demographicsTitle.CreateDemographicsTitle("Dr.", DateTime.Now, _careProviders_TeamId);
                _demographicsTitleId = dbHelper.demographicsTitle.GetByName("Dr.")[0];

                #endregion Title

                #region Ethnicity

                var ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Asian or Asian British - Indian").Any();
                if (!ethnicityId)
                    dbHelper.ethnicity.CreateEthnicity(_careProviders_TeamId, "Asian or Asian British - Indian", DateTime.Now);
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Asian or Asian British - Indian")[0];

                #endregion Ethnicity

                #region TransportType

                var transportTypeId = dbHelper.transportType.GetTransportTypeByName("AutomationTransportTest").Any();
                if (!transportTypeId)
                    dbHelper.transportType.CreateTransportType(_careProviders_TeamId, "AutomationTransportTest", DateTime.Now, 1, "50", 5);
                _transportTypeId = dbHelper.transportType.GetTransportTypeByName("AutomationTransportTest")[0];

                #endregion



                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13415_User_01_" + userLastName).Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_13415_User_01_" + userLastName, "Testing", "CDV6_13415_User_01_" + userLastName, "Testing CDV6_13415_User_01_" + userLastName, "Summer2013@",
                        "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null,
                        _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 4, null, new DateTime(2022, 1, 1));

                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13415_User_01_" + userLastName)[0];
                _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];

                #endregion

                #region Create SystemUser Record 2


                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").Any();
                if (!adminUserExists)
                {
                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_3", "CW", "Admin_Test_User_3", "CW Admin Test User 3", "Passw0rd_!", "CW_Admin_Test_User_3@somemail.com", "CW_Admin_Test_User_3@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    //foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(systemAdministratorSecurityProfileId))
                    //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemUserSecureFieldsSecurityProfileId);
                }


                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);
                adminUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "username")["username"];
                #endregion

                #region Create SystemUser Record 3

                adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("TestHM_001").Any();
                if (!adminUserExists)
                {
                    testHM_001UserId = dbHelper.systemUser.CreateSystemUser("TestHM_001", "Test", "HM 001", "Test HM 001", "Passw0rd_!", "TestHM_001@somemail.com", "TestHM_001@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 2);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (View)").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(testHM_001UserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(testHM_001UserId, systemUserSecureFieldsSecurityProfileId);
                }

                adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("TestHM_002").Any();
                if (!adminUserExists)
                {
                    testHM_002UserId = dbHelper.systemUser.CreateSystemUser("TestHM_002", "Test", "HM 002", "Test HM 002", "Passw0rd_!", "TestHM_002@somemail.com", "TestHM_002@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 3);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(testHM_002UserId, systemAdministratorSecurityProfileId);
                }

                testHM_001UserId = dbHelper.systemUser.GetSystemUserByUserName("TestHM_001").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(testHM_001UserId, DateTime.Now.Date);
                dbHelper.systemUser.UpdatePassword(testHM_001UserId, "Passw0rd_!");
                dbHelper.systemUser.UpdateCommentLegacyField(testHM_001UserId, "This is a Test Text for Legacy System Data comment.");

                testHM_002UserId = dbHelper.systemUser.GetSystemUserByUserName("TestHM_002").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(testHM_002UserId, DateTime.Now.Date);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }



        #region https://advancedcsg.atlassian.net/browse/CDV6-11075

        [TestProperty("JiraIssueID", "ACC-3106")]
        [Description("Navigate to the system users page -Open existing user Record-Validate the Left side Fields and Right Side Fields in Address Area" +
            "Validate Clear Address Button and Address Search Button")]
        [TestMethod, TestCategory("UITest")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod001()
        {

            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
                dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
                userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
                _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(userName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateLeftSideFields()
                .ValidateRightSideFields()
                .ValidateAddressTypeMandatoryFieldSignVisible(false)
                .ValidateStartDateMandatoryFieldSignVisible(false)
                .InsertPostCode("456123")
                .InserttownCity("hamburger")
                .ValidateAddressType_Options("Home")
                .ValidateAddressType_Options("Work")
                .ValidateAddressTypeMandatoryFieldSignVisible(true)
                .ValidateStartDateMandatoryFieldSignVisible(true)
                .ValidateTabForSearchAddress_Button();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("No matches found. Please refine your search address and try again.")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateTabForClearAddress_Button();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to clear address fields?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .validateStartDate_Field("");


        }

        //Below is the Duplicate of CDV6-13760
        /* [TestProperty("JiraIssueID", "CDV6-13761")]
         [Description("Navigate to the system users page -Open New Record-Validate the Left side Fields and Right Side Fields in Address Area" +
                      "Validate Clear Address Button and Address Search Button")]
         [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
         public void SystemUser_UITestMethod002()
         {

             loginPage
                 .GoToLoginPage()
                 .Login(adminUsername, "Passw0rd_!", _environmentName)
                 .WaitForCareProvidermHomePageToLoad();

             mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

             systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

             systemUserRecordPage
                 .WaitForNewSystemUserRecordPageToLoad()
                 .ValidateLeftSideFields()
                 .ValidateRightSideFields()
                 .ValidateAddressTypeMandatoryFieldSignVisible(false)
                 .ValidateStartDateMandatoryFieldSignVisible(false)
                 .InsertPostCode("456123")
                 .InserttownCity("hamburger")
                 .ValidateAddressType_Options("Home")
                 .ValidateAddressType_Options("Work")
                 .ValidateAddressTypeMandatoryFieldSignVisible(true)
                 .ValidateStartDateMandatoryFieldSignVisible(true)
                 .ValidateTabForSearchAddress_Button();

             alertPopup
                 .WaitForAlertPopupToLoad()
                 .ValidateAlertText("No matches found. Please refine your search address and try again.")
                 .TapOKButton();

             systemUserRecordPage
                 .WaitForNewSystemUserRecordPageToLoad()
                 .ValidateTabForClearAddress_Button();

             alertPopup
                 .WaitForAlertPopupToLoad()
                 .ValidateAlertText("Are you sure you want to clear address fields?")
                 .TapOKButton();

             systemUserRecordPage
                 .WaitForNewSystemUserRecordPageToLoad()
                 .validateStartDate_Field("");


         }*/

        [TestProperty("JiraIssueID", "ACC-3107")]
        [Description("Navigate to the system users page -Open New Record-Fill all the Fields and Save the Record" +
                     "Validate New Record is Created")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod003()
        {

            var birthDate = DateTime.Now.AddYears(-20);
            var startDate = DateTime.Now.AddYears(-10);
            var time = DateTime.Now.TimeOfDay;


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("System Administrator")
                .ClickTitleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Dr.")
                .TapSearchButton()
                .SelectResultElement(_demographicsTitleId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertFirstName("Testing_CDV6_13415_" + time)
                .InsertMiddleName("System")
                .InsertLastName("Oliver")
                .InsertBirthDate(birthDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertPropertyName("field")
                .InsertPropertyNo("45")
                .InsertStreetName("Howard")
                .InsertVillageDistrict("Framingham Pigot")
                .InsertTownCity("Norwich")
                .InsertPostCode("NR14 7PZ")
                .InsertCounty("Fox")
                .InsertCountry("UK")
                //.SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Civil Partner")
                .TapSearchButton()
                .SelectResultElement(maritalStatusId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hindu")
                .TapSearchButton()
                .SelectResultElement(religionId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectBrithishCitizenship_Options("No")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Asian or Asian British - Indian")
                .TapSearchButton()
                .SelectResultElement(_ethnicityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Austrian")
                .TapSearchButton()
                .SelectResultElement(nationalityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("india")
                .TapSearchButton()
                .SelectResultElement(countryId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertYearOfEntryToUK("2014")
                .SelectDisabilityStatus_Options("4")
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationTransportTest")
                .TapSearchButton()
                .SelectResultElement(_transportTypeId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertWorkEmail("abcd@gmail.com")
                .InsertPersonalEmail("ghf@gmail.com")
                .InsertSecureEmail("dfg@gmail.com")
                .InsertWorkPhoneLandline("24567891")
                .InsertWorkPhoneMobile("9784567861")
                .InsertPersonalPhoneLandline("45612345")
                .InsertPersonalPhoneMobile("8756489224")
                .WaitForNewSystemUserRecordPageToLoad()
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectBusinessUnitByValue(_careProviders_BusinessUnitId.ToString())
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectAuthenticationProviderid_Options("Internal")
                .InsertUserName("Testing_CDV6_13415_" + time)
                .InsertPassword("Summer2013@")
                .InsertAccountLookedOutDate(DateTime.Now.AddMonths(05).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertAccountLookedOutTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .InsertFailedPasswordAttemptCount("10")
                .InsertLastFailedPasswordAttemptDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertLastFailedPasswordAttemptTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickProfessionTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("General Practitioner")
                .TapSearchButton()
                .SelectResultElement(professionType.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertProfessionalRegistrationNumber("546234a")
                .InsertJobTitle("Emergrncy Care")
                .SelectRecordsPerPage_Options("100")
                .SelectSystemLanguage_Options("English (UK)")
                .SelectTimeZone_Options("GMT Standard Time")
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUsersPage
               .WaitForSystemUsersPageToLoad();

            System.Threading.Thread.Sleep(5000);


            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserNamePrefix("Testing_CDV6_13415_" + time);
            Assert.AreEqual(1, systemUserRecord.Count);

            var systemUserRecordFields = dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserRecord[0], "FirstName", "LastName", "MiddleName", "DateOfBirth", "StartDate", "UserName", "WorkEmail", "JobTitle", "WorkPhoneLandline", "WorkPhoneMobile", "FailedPasswordAttemptCount", "AuthenticationProviderId", "OwningBusinessUnitId",
                "DefaultTeamId", "TimeZoneId", "RecordsPerPageId", "ProfessionalRegistrationNumber", "ProfessionTypeId", "MaritalStatusId", "ReligionId", "EthnicityId",
                "NationalityId", "PersonGenderId", "PropertyName", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressLine5",
                "Postcode", "Country", "AddressTypeId", "PersonalPhoneLandline", "PersonalPhoneMobile", "BritishCitizenshipId", "CountryOfBirthId",
                "DisabilityStatusId", "YearOfEntry", "EmploymentStatusId", "SecureEmailAddress", "PersonalEmail");

            Assert.AreEqual("Testing_CDV6_13415_" + time, systemUserRecordFields["firstname"]);
            Assert.AreEqual("Oliver", systemUserRecordFields["lastname"]);
            Assert.AreEqual("System", systemUserRecordFields["middlename"]);
            Assert.AreEqual(birthDate.Date, systemUserRecordFields["dateofbirth"]);
            Assert.AreEqual(startDate.Date, systemUserRecordFields["startdate"]);
            Assert.AreEqual("Testing_CDV6_13415_" + time, systemUserRecordFields["username"]);
            Assert.AreEqual("abcd@gmail.com", systemUserRecordFields["workemail"]);
            Assert.AreEqual("Emergrncy Care", systemUserRecordFields["jobtitle"]);
            Assert.AreEqual("24567891", systemUserRecordFields["workphonelandline"]);
            Assert.AreEqual("9784567861", systemUserRecordFields["workphonemobile"]);
            Assert.AreEqual(10, systemUserRecordFields["failedpasswordattemptcount"]);
            Assert.AreEqual(authenticationproviderid.ToString(), systemUserRecordFields["authenticationproviderid"].ToString());
            Assert.AreEqual(_careProviders_BusinessUnitId.ToString(), systemUserRecordFields["owningbusinessunitid"].ToString());
            Assert.AreEqual(_careProviders_TeamId.ToString(), systemUserRecordFields["defaultteamid"].ToString());
            Assert.AreEqual("GMT Standard Time", systemUserRecordFields["timezoneid"]);
            Assert.AreEqual(100, systemUserRecordFields["recordsperpageid"]);
            Assert.AreEqual("546234a", systemUserRecordFields["professionalregistrationnumber"]);
            Assert.AreEqual(professionType.ToString(), systemUserRecordFields["professiontypeid"].ToString());
            Assert.AreEqual(maritalStatusId.ToString(), systemUserRecordFields["maritalstatusid"].ToString());
            Assert.AreEqual(religionId.ToString(), systemUserRecordFields["religionid"].ToString());
            Assert.AreEqual(_ethnicityId.ToString(), systemUserRecordFields["ethnicityid"].ToString());
            Assert.AreEqual(nationalityId.ToString(), systemUserRecordFields["nationalityid"].ToString());
            Assert.AreEqual(1, systemUserRecordFields["persongenderid"]);
            Assert.AreEqual("field", systemUserRecordFields["propertyname"]);
            Assert.AreEqual("45", systemUserRecordFields["addressline1"]);
            Assert.AreEqual("Howard", systemUserRecordFields["addressline2"]);
            Assert.AreEqual("Framingham Pigot", systemUserRecordFields["addressline3"]);
            Assert.AreEqual("Norwich", systemUserRecordFields["addressline4"]);
            Assert.AreEqual("Fox", systemUserRecordFields["addressline5"]);
            Assert.AreEqual("NR14 7PZ", systemUserRecordFields["postcode"]);
            Assert.AreEqual("UK", systemUserRecordFields["country"]);
            Assert.AreEqual(1, systemUserRecordFields["addresstypeid"]);
            Assert.AreEqual("45612345", systemUserRecordFields["personalphonelandline"]);
            Assert.AreEqual("8756489224", systemUserRecordFields["personalphonemobile"]);
            Assert.AreEqual(2, systemUserRecordFields["britishcitizenshipid"]);
            Assert.AreEqual(countryId.ToString(), systemUserRecordFields["countryofbirthid"].ToString());
            Assert.AreEqual(4, systemUserRecordFields["disabilitystatusid"]);
            Assert.AreEqual(2014, systemUserRecordFields["yearofentry"]);
            Assert.AreEqual(1, systemUserRecordFields["employmentstatusid"]);
            Assert.AreEqual("dfg@gmail.com", systemUserRecordFields["secureemailaddress"]);
            Assert.AreEqual("ghf@gmail.com", systemUserRecordFields["personalemail"]);





        }

        [TestProperty("JiraIssueID", "ACC-3108")]
        [Description("Navigate to the system users page -Open existing Record-Update the mandatory Fields" +
                     "Validate System Allow the user to Update the Mandatory Fields")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod004()
        {

            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
                dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
                userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
                _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            var systemuseraddressId = dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _newSystemUserId, "pna", "pno", "", "", "", "", "pc", "co", 1, DateTime.Now.Date.AddYears(-10), null);

            dbHelper.systemUser.UpdateLinkedAddress(_newSystemUserId, systemuseraddressId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .InsertFirstName("Kishore")
                .InsertLastName("Kumar")
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectBusinessUnitByText("South East Region");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectRecordsPerPage_Options("150")
                .SelectPets_Option("No Pets")
                .SelectSmoker_Option("No")
                .ClickSaveAndCloseButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Update existing address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            System.Threading.Thread.Sleep(2000);

            systemUsersPage
             .WaitForSystemUsersPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateFirstNameFieldValue("Kishore")
                .ValidateLastNameFieldValue("Kumar")
                .validateStartDate_Field(DateTime.Now.AddYears(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateAddressType_Options("Home")
                .ValidateBusinessType_FieldText(_SouthEastRegion_BusinessUnitId.ToString())
                .ValidateRecordsPerPage_FieldValue("150");
        }

        [TestProperty("JiraIssueID", "ACC-3109")]
        [Description("Navigate to the system users page -Open New Record-Create New Record with Start Date Earlier than Date Of Birth" +
                    "Validate System allows the user to Create the Record ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod005()
        {
            var birthDate = DateTime.Now.AddYears(-20);
            var startDate = DateTime.Now.AddYears(-30);
            var time = DateTime.Now.TimeOfDay;


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickTitleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Dr.")
                .TapSearchButton()
                .SelectResultElement(_demographicsTitleId.ToString());

            systemUserRecordPage
               .WaitForNewSystemUserRecordPageToLoad()
               .InsertFirstName("Testing_CDV6_13415_" + time)
               .InsertMiddleName("System")
               .InsertLastName("Oliver")
               .InsertBirthDate(birthDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .SelectGender_Options("Male")
               .InsertPropertyName("field")
               .InsertPropertyNo("45")
               .InsertStreetName("Howard")
               .InsertVillageDistrict("Framingham Pigot")
               .InsertTownCity("Norwich")
               .InsertPostCode("NR14 7PZ")
               .InsertCounty("Fox")
               .InsertCountry("UK")
               .SelectAddressType_Options("Home")
               .InsertStartDate(startDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Civil Partner")
                .TapSearchButton()
                .SelectResultElement(maritalStatusId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hindu")
                .TapSearchButton()
                .SelectResultElement(religionId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectBrithishCitizenship_Options("No")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Asian or Asian British - Indian")
                .TapSearchButton()
                .SelectResultElement(_ethnicityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Austrian")
                .TapSearchButton()
                .SelectResultElement(nationalityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("india")
                .TapSearchButton()
                .SelectResultElement(countryId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertYearOfEntryToUK("2014")
                .SelectDisabilityStatus_Options("4")
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationTransportTest")
                .TapSearchButton()
                .SelectResultElement(_transportTypeId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertWorkEmail("abcd@gmail.com")
                .InsertSecureEmail("dfg@gmail.com")
                .InsertWorkPhoneLandline("24567891")
                .InsertWorkPhoneMobile("9784567861")
                .InsertPersonalPhoneLandline("45612345")
                .InsertPersonalPhoneMobile("8756489224")
                .SelectBusinessUnitByText("CareProviders")
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            systemUserRecordPage
               .WaitForNewSystemUserRecordPageToLoad()
               .SelectAuthenticationProviderid_Options("Internal")
               .InsertUserName("Testing_CDV6_13415_" + time)
               .InsertPassword("Summer2013@")
               .InsertAccountLookedOutDate(DateTime.Now.AddMonths(05).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertAccountLookedOutTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
               .InsertFailedPasswordAttemptCount("10")
               .InsertLastFailedPasswordAttemptDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertLastFailedPasswordAttemptTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
               .ClickProfessionTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("General Practitioner")
                .TapSearchButton()
                .SelectResultElement(professionType.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertProfessionalRegistrationNumber("546234a")
                .InsertJobTitle("Emergrncy Care")
                .SelectRecordsPerPage_Options("100")
                .SelectSystemLanguage_Options("English (UK)")
                .SelectTimeZone_Options("GMT Standard Time")
                .SelectEmployeeType("System Administrator")
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .SelectPets_Option("No Pets")
                .SelectSmoker_Option("No")
                .ClickSaveAndCloseButton();

            systemUsersPage
               .WaitForSystemUsersPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13415_" + time);
            Assert.AreEqual(1, systemUserRecord.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3110")]
        [Description("Navigate to the system users page -Open New Record-Create New Record without Filling Address Fields" +
                    "Validate System allows the User to Create the Record ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod006()
        {
            var time = DateTime.Now.TimeOfDay;

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickTitleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Dr.")
                .TapSearchButton()
                .SelectResultElement(_demographicsTitleId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertFirstName("Testing_CDV6_13415_" + time)
                .InsertMiddleName("System")
                .InsertLastName("Oliver")
                .InsertBirthDate(DateTime.Now.AddYears(-20).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectGender_Options("Male")
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Civil Partner")
                .TapSearchButton()
                .SelectResultElement(maritalStatusId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hindu")
                .TapSearchButton()
                .SelectResultElement(religionId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectBrithishCitizenship_Options("No")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Asian or Asian British - Indian")
                .TapSearchButton()
                .SelectResultElement(_ethnicityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Austrian")
                .TapSearchButton()
                .SelectResultElement(nationalityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("india")
                .TapSearchButton()
                .SelectResultElement(countryId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertYearOfEntryToUK("2014")
                .SelectDisabilityStatus_Options("4")
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationTransportTest")
                .TapSearchButton()
                .SelectResultElement(_transportTypeId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertWorkEmail("abcd@gmail.com")
                .InsertSecureEmail("dfg@gmail.com")
                .InsertWorkPhoneLandline("24567891")
                .InsertWorkPhoneMobile("9784567861")
                .InsertPersonalPhoneLandline("45612345")
                .InsertPersonalPhoneMobile("8756489224")
                .SelectBusinessUnitByText("CareProviders")
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectAuthenticationProviderid_Options("Internal")
                .InsertUserName("Testing_CDV6_13415_" + time)
                .InsertPassword("Summer2013@")
                .InsertAccountLookedOutDate(DateTime.Now.AddMonths(05).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertAccountLookedOutTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .InsertFailedPasswordAttemptCount("10")
                .InsertLastFailedPasswordAttemptDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertLastFailedPasswordAttemptTime(DateTime.Now.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickProfessionTypeLookupButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("General Practitioner")
                .TapSearchButton()
                .SelectResultElement(professionType.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertProfessionalRegistrationNumber("546234a")
                .InsertJobTitle("Emergrncy Care")
                .SelectRecordsPerPage_Options("100")
                .SelectSystemLanguage_Options("English (UK)")
                .SelectTimeZone_Options("GMT Standard Time")
                .SelectEmployeeType("System Administrator")
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectPets_Option("No Pets")
                .SelectSmoker_Option("No")
                .ClickSaveAndCloseButton();

            systemUsersPage
               .WaitForSystemUsersPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13415_" + time);
            Assert.AreEqual(1, systemUserRecord.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3111")]
        [Description("Navigate to the system users page -Open existing Record-Clear the address Fields and try to save the Record" +
                     "Validate System Should not allows the User to Save the Record ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod007()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
                dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
                userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
                _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, new DateTime(2020, 1, 1));

            var systemuseraddressId = dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _newSystemUserId, "pna", "pno", "", "", "", "", "pc", "co", 1, DateTime.Now.Date.AddYears(-10), null);

            dbHelper.systemUser.UpdateLinkedAddress(_newSystemUserId, systemuseraddressId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickClearAddressButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to clear address fields?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateAddressTypeFieldErrorMessage("Please fill out this field.")
                .ValidatestartDateFieldErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "ACC-3112")]
        [Description("Navigate to the system users page -Open New Record" +
                    "Validate Address Area Fields MaxLenth Values")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod008()
        {

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidatePropertyNameMaximumLimitText("100")
                .ValidatePropertyNoMaximumLimitText("100")
                .ValidateStreetMaximumLimitText("250")
                .ValidateVillageMaximumLimitText("250")
                .ValidateTownCityMaximumLimitText("100")
                .ValidatePostcodeMaximumLimitText("20")
                .ValidateCountyMaximumLimitText("100")
                .ValidateCountryMaximumLimitText("100");



        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11616

        [TestProperty("JiraIssueID", "ACC-3113")]
        [Description("Navigate to the system users page -Open existing Record-Update Start Date in Address Fields" +
                     "Click on save button select Create New Address record option in the lookup" +
                     "Navigate to System user Address page  " +
                     "Validate two address records Created and old Start date Record is updated with End Date (previous date of new" +
            " start date)" +
                     "Validate New Start Date Record is created without End Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "System User Addresses")]
        public void SystemUser_UITestMethod0001()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var startDate = DateTime.Now.AddYears(-9).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var UpdatedDate = DateTime.Now.AddYears(-8).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var EndDate = DateTime.Now.AddYears(-8).AddDays(-1).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
                dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
                userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
                _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, new DateTime(2020, 1, 1));


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate)
                .ClickSaveButton();

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .SelectAddressType_Options("Home")
               .InsertStartDate(UpdatedDate)
               .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Create new address record")
                .TapOkButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .InsertUserName(userName)
                 .ClickSearchButton()
                 .WaitForResultsGridToLoad()
                 .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);


            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(systemUserAddressRecord[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateStartDateText(startDate)
                .ValidateEndDate(EndDate)
                .ClickBackButton();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(systemUserAddressRecord[1].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateStartDateText(UpdatedDate)
                .ValidateEndDate("");



        }

        [TestProperty("JiraIssueID", "ACC-3114")]
        [Description("Navigate to the system users page -Open existing Record-Update Start Date in Address Fields" +
                     "Click on save button select the update the existing record option in the lookup" +
                     "Navigate to System user Address page  " +
                     "Validate only Start date is updated and End Date is Empty")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "System User Addresses")]
        public void SystemUser_UITestMethod0002()
        {

            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
               dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
               userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
               _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, new DateTime(2020, 1, 1));

            var systemuseraddressId = dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _newSystemUserId, "pna", "pno", "", "", "", "", "pc", "co", 1, DateTime.Now.Date.AddYears(-10), null);

            dbHelper.systemUser.UpdateLinkedAddress(_newSystemUserId, systemuseraddressId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Update existing address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .InsertFirstName("ram")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Update existing address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            System.Threading.Thread.Sleep(3000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);
            Assert.AreEqual(1, systemUserAddressRecord.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(systemUserAddressRecord[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ValidateStartDateText(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateEndDate("");


        }

        [TestProperty("JiraIssueID", "ACC-3115")]
        [Description("Navigate to the system users page -Open existing Record-Update the Address Fields" +
                     "Navigate to System User Address Related Items Page" +
                     "Click the Delete option" +
                     "Validate Address Record linked with System user Cannot be delete")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "System User Addresses")]
        public void SystemUser_UITestMethod0003()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
               dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
               userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
               _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, new DateTime(2020, 1, 1));



            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);
            Assert.AreEqual(1, systemUserAddressRecord.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(systemUserAddressRecord[0].ToString());


            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Address cannot be deleted as it is linked to the parent SystemUser record.")
                .TapCloseButton();


        }


        [TestProperty("JiraIssueID", "ACC-3116")]
        [Description("Navigate to the system users page -Open existing Record-Update the Address Fields" +
                     "Navigate to System User Address Related Items Page" +
                     "Open the Address Record " +
                     "Validate All the Fields are non editable")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "System User Addresses")]
        public void SystemUser_UITestMethod0004()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
               dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
               userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
               _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, new DateTime(2020, 1, 1));


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);
            Assert.AreEqual(1, systemUserAddressRecord.Count);

            var SystemUserAddressRecordFields = dbHelper.systemUserAddress.GetSystemUserAddressBySystemUserID(systemUserAddressRecord[0], "inactive");
            Assert.AreEqual(true, SystemUserAddressRecordFields["inactive"]);

        }

        [TestProperty("JiraIssueID", "ACC-3117")]
        [Description("Navigate to the system users page -Open existing Record-Update Start Date in Address Fields" +
                    "Click on save button select the update the existing record option in the lookup" +
                    "Navigate to System user Address page and Navigate to Audit Page " +
                    "Validate Update operation is Recorded in Audit Page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "System User Addresses")]
        public void SystemUser_UITestMethod0005()
        {

            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
               dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
               userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
               _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            var systemuseraddressId = dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _newSystemUserId, "pna", "pno", "", "", "", "", "pc", "co", 1, DateTime.Now.Date.AddYears(-10), null);

            dbHelper.systemUser.UpdateLinkedAddress(_newSystemUserId, systemuseraddressId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertAvailableFromDateField(DateTime.Now.AddYears(-30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Update existing address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            System.Threading.Thread.Sleep(4000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .InsertFirstName("Ram")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Update existing address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            System.Threading.Thread.Sleep(3000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);
            Assert.AreEqual(1, systemUserAddressRecord.Count);

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .OpenRecord(systemUserAddressRecord[0].ToString());

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageToLoad()
                .NavigateToMenuSubPage_Aduit();

            auditListPage
                .WaitForAuditListPageToLoad("systemuseraddress");

            auditListPage
                .WaitForAuditListPageToLoad("systemuseraddress")
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("CW Admin_Test_User_3");


        }

        [TestProperty("JiraIssueID", "ACC-3118")]
        [Description("Navigate to the system users page -Open existing Record-Update Start Date in Address Fields" +
                     "Click on save button select Create New Address record option in the lookup" +
                     "again update the System User Record with new start date and select 'create New Record option in the lookup'" +
                     "Again try to update the System User Record with Start Date In between previous Start and End Date and then Select Create New Record Option In the lookup" +
                     "Validate Error Message is Displayed with 'Address Dates overlap with another Address of the same Type.'")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod0006()
        {

            var currentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            var userName = "Testing_CDV6_13415_User_" + currentDate;

            var _newSystemUserId =
               dbHelper.systemUser.CreateSystemUser(userName, "Testing", "CDV6_13415_User" + currentDate, "Testing CDV6_13415_User_" + currentDate, "Passw0rd_!",
               userName + "@somemail.com", userName + "@other.com", "GMT Standard Time", null, null,
               _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(userName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectGender_Options("Male")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddYears(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            //addressActionPopUp
            //   .WaitForAddressActionPopUpToLoad()
            //   .SelectViewByText("Create new address record")
            //   .WaitForAddressActionPopUpToLoad()
            //   .TapOkButton();


            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .InsertFirstName("Ram")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Create new address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            System.Threading.Thread.Sleep(3000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .InsertFirstName("Alex")
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.AddDays(-10).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Create new address record")
                .WaitForAddressActionPopUpToLoad()
                .TapOkButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Address Dates overlap with another Address of the same Type.")
                .TapCloseButton();

            var linkedAddressId = dbHelper.systemUser.GetLinkedAddressByUserName(userName);
            var systemUserAddressRecord = dbHelper.systemUserAddress.GetBySystemUserAddressId(linkedAddressId[0]);
            Assert.AreEqual(2, systemUserAddressRecord.Count);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11076

        [TestProperty("JiraIssueID", "ACC-3119")]
        [Description("Navigate to the system users page -Open New Record" +
                     "Validate General Section and Additional Demographics Section Field" +
                     "Validate British Citizenship Field options ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00001()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateGeneralSectionFields()
                .ValidateAdditionalDemographicsSectionFields()
                .ValidateCountryOfBirthNotKnownDefaultValue("0")
                .ValidateNotBornInUKButCountryUnknownDefaultValue("0")
                .ValidateBritishCitizenshipPickListValues("No")
                .ValidateBritishCitizenshipPickListValues("Not known")
                .ValidateBritishCitizenshipPickListValues("Yes");

        }

        [TestProperty("JiraIssueID", "ACC-3120")]
        [Description("Navigate to the system users page -Open New Record" +
                     "Set Country Of Birth Not Known to 'Yes'" +
                     "Validate Not Born In UK But country Unknown Field is Not Visible ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00002()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateAdditionalDemographicsSectionFields()
                .ValidateNotBornInUKButCountryUnknownFieldVisible(true)
                .SelectCountryofBirthNotKnown_yes()
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateNotBornInUKButCountryUnknownFieldVisible(false);

        }

        [TestProperty("JiraIssueID", "ACC-3121")]
        [Description("Navigate to the system users page -Open New Record" +
                    "Set Country Of Birth Not Known to 'Yes' or Not Born In Uk But Country Unknown to'Yes'" +
                    "Validate Country Of Birth Field is Not Visible ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00003()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateAdditionalDemographicsSectionFields()
                .ValidateNotBornInUKButCountryUnknownFieldVisible(true)
                .SelectCountryofBirthNotKnown_yes()
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateNotBornInUKButCountryUnknownFieldVisible(false)
                .ValidateCountryOfBirthFieldVisible(false)
                .SelectCountryofBirthNotKnown_no()
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectNotBornInUKButCountryUnknown_YesOption()
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateCountryOfBirthFieldVisible(false);

        }

        [TestProperty("JiraIssueID", "ACC-3122")]
        [Description("Navigate to the system users page -Open New Record" +
                    "Set Country Of Birth Not Known to 'Yes' or Not Born In Uk But Country Unknown to'Yes'" +
                    "Validate Country Of Birth Field is Not Visible ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00004()
        {
            var countryOfBirth = new Guid("cabca7d6-2097-e811-80dc-005056050630");//United Kingdom of Great Britain and Northern Ireland

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("United Kingdom of Great Britain and Northern Ireland")
                .TapSearchButton()
                .SelectResultElement(countryOfBirth.ToString());

            systemUserRecordPage
               .WaitForNewSystemUserRecordPageToLoad()
               .ValidateYearOfEntryToUkFieldVisible(false);

        }

        [TestProperty("JiraIssueID", "ACC-3123")]
        [Description("Navigate to the system users page -Open Existing Record" +
                    "Set Date of entry eariler than Date of Birth" +
                    "Validate Error Message is Displayed 'Year of Entry cannot be earlier than the System User’s Birth Year.'" +
                    "Set Date of entry less than 1000" +
                    "Validate Error Message is Displayed 'Please enter a value between 1000 and 9999.'")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00005()
        {

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .InsertBirthDate("01/12/2021")
                .InsertYearOfEntryToUK("2014")
                .SelectGender_Options("Male")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Year of Entry cannot be earlier than the System User’s Birth Year.")
                .TapCloseButton();

            systemUserRecordPage
               .WaitForNewSystemUserRecordPageToLoad()
               .InsertYearOfEntryToUK("201")
               .ClickSaveButton()
               .ValidateYearOfEntryFieldErrorMessage("Please enter a value between 1000 and 9999.");

        }

        [TestProperty("JiraIssueID", "ACC-3124")]
        [Description("Navigate to the system users page -Open Existing Record" +
                     "Set Country Of Birth Not Known or Not Born in Uk But Country Unknown to'Yes'" +
                     "Validate Year of Entry Field Is visible and user is able to save the record with valide value")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod00006()
        {

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectNotBornInUKButCountryUnknown_NoOption()
                .WaitForSystemUserRecordPageToLoad()
                .SelectCountryofBirthNotKnown_yes()
                .WaitForSystemUserRecordPageToLoad()
                .ValidateNotBornInUKButCountryUnknownFieldVisible(false)
                .ValidateYearOfEntryToUkFieldVisible(true)
                .InsertYearOfEntryToUK("2014")
                .SelectGender_Options("Male")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidateYearOfEntryFieldValue("2014");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectCountryofBirthNotKnown_no()
                .SelectNotBornInUKButCountryUnknown_YesOption()
                .ValidateYearOfEntryToUkFieldVisible(true)
                .InsertYearOfEntryToUK("2014")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidateYearOfEntryFieldValue("2014")
                .WaitForSystemUserRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateDisabilityStatusPickListValues("No disability")
                .ValidateDisabilityStatusPickListValues("Has disability")
                .ValidateDisabilityStatusPickListValues("Undisclosed")
                .ValidateDisabilityStatusPickListValues("Not Known")
                .ValidatePhoneAndEmailSectionFields();

        }

        /// <summary>
        /// This Step is removed from manual test steps (Validation is not working)
        /// </summary>
        //[TestProperty("JiraIssueID", "CDV6-14326")]
        //[Description("Navigate to the system users page -Open Existing Record" +
        //             "Set Date of Birth In future and then save the record" +
        //             "Validate Error message is displayed")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //public void SystemUser_UITestMethod00007()
        //{

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Admin_Test_User_3", "Passw0rd_!", _environmentName)
        //        .WaitForCareProvidermHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //     .WaitForSystemUsersPageToLoad()
        //     .InsertUserName("Testing_CDV6_13415_User_01")
        //     .ClickSearchButton()
        //     .WaitForResultsGridToLoad()
        //     .OpenRecord(_systemUserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .InsertBirthDate(DateTime.Now.AddDays(04).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //        .ClickSaveButton();

        //    dynamicDialogPopup
        //        .WaitForDynamicDialogPopupToLoad()
        //        .ValidateMessage("")
        //        .TapCloseButton();

        //}


        [TestProperty("JiraIssueID", "ACC-3125")]
        [Description("Navigate to Advance Search Page" +
                     "Validate Under System user all the new fields are included")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Advanced Search")]
        public void SystemUser_UITestMethod00008()
        {

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .WaitForAdvanceSearchPageToLoad();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Title")
                .SelectFilter("1", "Middle Name")
                .SelectFilter("1", "Stated Gender")
                .SelectFilter("1", "Date of Birth")
                .SelectFilter("1", "Marital Status")
                .SelectFilter("1", "Religion")
                .SelectFilter("1", "Ethnicity")
                .SelectFilter("1", "Nationality")
                .SelectFilter("1", "British Citizenship")
                .SelectFilter("1", "Country of Birth")
                .SelectFilter("1", "Country of Birth Not Known")
                .SelectFilter("1", "Not Born in UK but Country Unknown")
                .SelectFilter("1", "Disability Status")
                .SelectFilter("1", "Personal Email")
                .SelectFilter("1", "Personal Phone (Landline)")
                .SelectFilter("1", "Personal Phone (Mobile)");



        }

        [TestProperty("JiraIssueID", "ACC-3126")]
        [Description("Navigate to Advance Search Page" +
                     "Validate Advance Search is perform with new fields ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Advanced Search")]
        public void SystemUser_UITestMethod00009()
        {

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .InsertMiddleName("CDV6_13415")
                .InsertPersonalEmail("dfg@gmail.com")
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFieldOption("0", "Middle Name")
                .InsertFieldOptionValue("CDV6_13415")
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_systemUserId.ToString());

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFieldOption("0", "Personal Email")
                .InsertFieldOptionValue("dfg@gmail.com")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_systemUserId.ToString());

        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11116

        [TestProperty("JiraIssueID", "ACC-3127")]
        [Description("Navigate to the system users page -Open New Record" +
                     "Validate System User New Record All Sections and under Each Section Field Header ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000001()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateSystemUserSectionHeaders(true, true, true, true, true, true, true, true)
                .ValidateGeneralSectionFieldHeaders(true, true, true, true, true, true, true, true, true)
                .ValidateAddressSectionFieldHeaders(true, true, true, true, true, true, true, true, true, true, true, true)
                .ValidateAdditionalDemographicsSectionFieldHeaders(true, true, true, true, true, true, true, true, true, true, true)
                .ValidatePhoneEmailSectionFieldHeaders(true, true, true, true, true, true, true)
                .ValidateAccountSectionFieldHeaders(true, true, true, true, true, true, true, true, true, true)
                .ValidateEmploymentDetailsSectionFieldHeaders(true, true, true, true, true)
                .ValidateSettingsSectionFieldHeaders(true, true, true, true, true, true, true, true, true)
                .ValidateLegacySystemDataSectionFieldHeaders(true)
                .ValidateCommentFieldDisabled(true)
                .ClickSaveButton()
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateNotifactionErrorMessage("Some data is not correct. Please review the data in the Form.");
        }

        [TestProperty("JiraIssueID", "ACC-3128")]
        [Description("Navigate to the system users page -Open New Record" +
                     "Validate Profile Photo file Types ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000002()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateProfilePhotoFileType(".jpeg,.jpg,.gif,.png,.bmp");
        }

        [TestProperty("JiraIssueID", "ACC-3129")]
        [Description("Navigate to the system users page -Open New Record" +
                    "Validate Minimum Password Length" +
                    "Validate Password criteria ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000003()
        {
            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                 .WaitForSystemUsersPageToLoad()
                 .ClickNewRecordButton();


            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectAuthenticationProviderid_Options("Internal")
                .ValidatePasswordFieldHeaderVisible(true)
                .ValidatePasswordFieldHeaderVisible(true)
                .InsertPassword("abcde")
                .InsertUserName("abcd")
                .ValidatePasswordFieldErrorMessage("Please enter at least 6 characters.")
                .InsertPassword("abcdef")
                .InsertUserName("")
                .ValidatePasswordFieldErrorMessage("Password should have at least one number, one upper case letter and one lower case letter. Also, it should have a length between 6 and 30 characters.");

        }

        [TestProperty("JiraIssueID", "ACC-3130")]
        [Description("Navigate to the system users page -Open New Record-Fill all the Fields and Save the Record" +
                     "Validate New Record is Created")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000004()
        {
            var birthDate = DateTime.Now.AddYears(-20);
            var startDate = DateTime.Now.AddYears(-10);
            var firstName = "Testing_CDV6_13415_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var userName = "Testing_CDV6_13415_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");


            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickTitleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Dr.")
                .TapSearchButton()
                .SelectResultElement(_demographicsTitleId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateActionToolBarOptionsVisiblity(true, true, false)
                .SelectEmployeeType("System Administrator")
                .InsertFirstName(firstName)
                .InsertMiddleName("System")
                .InsertLastName("Oliver")
                .InsertBirthDate(birthDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectGender_Options("Male")
                .InsertPropertyName("field")
                .InsertPropertyNo("45")
                .InsertStreetName("Howard")
                .InsertVillageDistrict("Framingham Pigot")
                .InsertTownCity("Norwich")
                .InsertPostCode("NR14 7PZ")
                .InsertCounty("Fox")
                .InsertCountry("UK")
                .SelectAddressType_Options("Home")
                .InsertStartDate(startDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Civil Partner")
                .TapSearchButton()
                .SelectResultElement(maritalStatusId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Hindu")
                .TapSearchButton()
                .SelectResultElement(religionId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectBrithishCitizenship_Options("No")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Asian or Asian British - Indian")
                .TapSearchButton()
                .SelectResultElement(_ethnicityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Austrian")
                .TapSearchButton()
                .SelectResultElement(nationalityId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("india")
                .TapSearchButton()
                .SelectResultElement(countryId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertYearOfEntryToUK("2014")
                .SelectDisabilityStatus_Options("4")
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("AutomationTransportTest")
                .TapSearchButton()
                .SelectResultElement(_transportTypeId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertWorkEmail("abcd@gmail.com")
                .InsertPersonalEmail("ghf@gmail.com")
                .InsertSecureEmail("dfg@gmail.com")
                .InsertWorkPhoneLandline("24567891")
                .InsertWorkPhoneMobile("9784567861")
                .InsertPersonalPhoneLandline("45612345")
                .InsertPersonalPhoneMobile("8756489224")
                .SelectBusinessUnitByText("CareProviders")
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectAuthenticationProviderid_Options("Internal")
                .InsertUserName(userName)
                .InsertPassword("Summer2013@")
                .InsertAccountLookedOutDate(DateTime.Now.AddMonths(05).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertAccountLookedOutTime("00:00")
                .InsertFailedPasswordAttemptCount("10")
                .InsertLastFailedPasswordAttemptDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertLastFailedPasswordAttemptTime("00:00")
                .ClickProfessionTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("General Practitioner")
                .TapSearchButton()
                .SelectResultElement(professionType.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertProfessionalRegistrationNumber("546234a")
                .InsertJobTitle("Emergrncy Care")
                .SelectRecordsPerPage_Options("100")
                .SelectSystemLanguage_Options("English (UK)")
                .SelectTimeZone_Options("GMT Standard Time")
                .InsertAvailableFromDateField(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUsersPage
               .WaitForSystemUsersPageToLoad();

            System.Threading.Thread.Sleep(3000);


            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserName(userName);
            Assert.AreEqual(1, systemUserRecord.Count);
            var userid = systemUserRecord[0];

            systemUsersPage
                .InsertUserName(userName)
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateActionToolBarOptionsVisiblity(true, true, true);
        }

        [TestProperty("JiraIssueID", "ACC-3131")]
        [Description("Login with SystemUser which has security profile'CW System User - Secure Fields (View)'" +
                    "Navigate to the system users page -Open exisiting Record with User Name='test_HM02'" +
                    "Verify that when user with Security Profile sees the Comment field as empty when there is no data" +
                    "Validate the max length of comment Field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000005()
        {

            loginPage
                .GoToLoginPage()
                .Login("testHM_001", "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("TestHM_002")
                .ClickSearchButton()
                .OpenRecord(testHM_002UserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateCommentFieldText("")
                .ValidateCommentFieldMaximumLimitText("8000");
        }

        ////Bug id: CDV6-16346
        //[TestProperty("JiraIssueID", "CDV6-14503")]
        //[Description("Login with User Name='TestHM_001'" +
        //             "Navigate to the system users page -Open existing Record with User Name='TestHM_001'" +
        //            "Validate user can see the Legacy Field Data In the System User Page" +
        //            "Validate System user has the security profile")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //public void SystemUser_UITestMethod000006()
        //{
        //    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (View)").First();
        //    var userSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(testHM_001UserId, systemUserSecureFieldsSecurityProfileId).First();


        //    loginPage
        //         .GoToLoginPage()
        //         .Login("TestHM_001", "Passw0rd_!", _environmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //     .WaitForSystemUsersPageToLoad()
        //     .InsertUserName("TestHM_001")
        //     .ClickSearchButton()
        //     .OpenRecord(testHM_001UserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .ValidateCommentFieldText("This is a Test Text for Legacy System Data comment.")
        //        .NavigateToSecurityProfilePage();

        //    securityProfilesPage
        //        .WaitForSystemUserSecurityProfilesPageToLoad()
        //        .InsertUserName("CW System User - Secure Fields (View)")
        //        .ClickSearchButton()
        //        .ValidateNoRecordMessageVisibile(false)
        //        .OpenRecord(userSecurityProfileId.ToString());

        //    securityProfileRecordPage
        //        .WaitForSystemUserSecurityProfileRecordPageToLoad()
        //        .ValidateSecurityProfileField("CW System User - Secure Fields (View)");


        //}


        [TestProperty("JiraIssueID", "ACC-3132")]
        [Description("Navigate to the system users page -Open exisiting Record with User Name='test_HM02'" +
                    "Validate user cannot able to see the Legacy Field Data In the System User Page only can see '****'" +
                    "Validate user has no CWsecurity profile")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_UITestMethod000007()
        {
            loginPage
                .GoToLoginPage()
                .Login("TestHM_002", "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("TestHM_002")
                .ClickSearchButton()
                .OpenRecord(testHM_002UserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateCommentFieldTextWithoutSecurityProfile("****")
                .NavigateToSecurityProfilePage();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .InsertQuickSearchText("CW System User - Secure Fields (View)")
                .ClickQuickSearchButton()
                .ValidateTextNotPresentInResultsGrid("CW System User - Secure Fields (View)");

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
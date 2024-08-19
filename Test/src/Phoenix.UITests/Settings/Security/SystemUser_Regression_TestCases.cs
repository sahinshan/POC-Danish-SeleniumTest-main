using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-14430
    ///
    /// </summary>
    [TestClass]

    public class SystemUser_Regression_TestCases : FunctionalTest
    {
        private string EnvironmentName;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _systemUserAliasType;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _demographicsTitleId;
        public Guid maritalStatusId;
        public Guid religionId = new Guid("3e044dd3-1e97-e811-80dc-005056050630");//Hindu
        private Guid _ethnicityId;
        public Guid nationalityId = new Guid("130443e2-7396-e811-80dc-005056050630");//Austrian
        public Guid countryId = new Guid("46bca7d6-2097-e811-80dc-005056050630");//india
        public Guid professionType = new Guid("1002981d-01b5-e811-80dc-0050560502cc");//General Practitioner
        private Guid _personLanguageId;
        public Guid _enableLockingAccountId;
        public Guid EmploymentContractTypeid = new Guid("07671F2F-5DFB-EB11-A33C-0050569231CF");//Salaried
        public Guid _bookingTypeId;
        public string currentDateTime;
        private string adminUsername;

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {

                #region Environment

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Marital Status

                maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careProviders_TeamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region SystemUser for the login operation

                adminUsername = "RegressionUser1";
                commonMethodsDB.CreateSystemUserRecord("RegressionUser1", "Regression", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion

                #region Reset DBHelper

                dbHelper = new Phoenix.DBHelper.DatabaseHelper(adminUsername, "Passw0rd_!", tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Person Language

                _personLanguageId = commonMethodsDB.CreateLanguage("English (UK)", _careProviders_TeamId, "2345", "001", DateTime.Now.AddDays(-30), null);

                #endregion Person Language

                #region Title

                _demographicsTitleId = commonMethodsDB.CreateDemographicsTitle("Dr.", DateTime.Now, _careProviders_TeamId);

                #endregion Title

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId, "Asian or Asian British - Indian", DateTime.Now);

                #endregion Ethnicity

                #region Create SystemUser Record

                currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserName = "Testing_CDV6_14430_User01_" + currentDateTime;

                _systemUserId = dbHelper.systemUser.CreateSystemUser(_systemUserName, "Testing_CDV6_14430", "User01_" + currentDateTime, "Testing_CDV6_14430 User01_" + currentDateTime, "Summer2013@", _systemUserName + "@gmail.com", _systemUserName + "@othergmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

                #endregion

                #region SystemUserAliasType

                _systemUserAliasType = commonMethodsDB.CreateSystemUserAliasType(_careProviders_TeamId, _careProviders_BusinessUnitId, "Birth Name", DateTime.Now.Date.AddDays(-30));


                #endregion SystemUserAliasType

                #region System Settings EnableAccountLocking

                _enableLockingAccountId = commonMethodsDB.CreateSystemSetting("EnableAccountLocking", "true", "When set to true the organization will be able to decide", false, null);
                dbHelper.systemSetting.UpdateSystemSettingValue(_enableLockingAccountId, "true");

                #endregion System Settings EnableAccountLocking

                dbHelper = new DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11921

        [TestProperty("JiraIssueID", "ACC-3321")]
        [Description("Navigate to the system users page" + "Create a System user account and validate the id field is autopupulated" +
            "Validate the ID field , Employment status and Legacy comments field are disabled" +
            "Validate the change password link" + "Validate the New password link and confirm new password field functionalities.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod001()
        {
            var birthDate = DateTime.Now.AddYears(-20);
            var startDate = DateTime.Now.AddYears(-10);
            var userName = "Testing_CDV6_14430_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ValidateId_Field_Disabled(true)
                .ValidateEmploymentStatus_Field_Disabled(true)
                .ValidateLegacyComments_Field_Disabled(true)
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
                .InsertFirstName("Van")
                .InsertMiddleName("Arthur")
                .InsertLastName("Oliver")
                .InsertBirthDate(birthDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
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
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
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
                .SelectDisabilityStatus_Options("4");

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
                .InsertAccountLookedOutDate(DateTime.Now.AddMonths(05).ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertAccountLookedOutTime("00:00")
                .InsertFailedPasswordAttemptCount("10")
                .InsertLastFailedPasswordAttemptDate(DateTime.Now.AddMonths(01).ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
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
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .WaitForResultsGridToLoad();

            System.Threading.Thread.Sleep(5000);


            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserName(userName);
            Assert.AreEqual(1, systemUserRecord.Count);

            var systemUserRecordFields = dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserRecord[0], "FirstName", "LastName", "MiddleName", "DateOfBirth", "StartDate", "UserName", "WorkEmail", "JobTitle", "WorkPhoneLandline", "WorkPhoneMobile", "FailedPasswordAttemptCount", "AuthenticationProviderId", "OwningBusinessUnitId",
              "DefaultTeamId", "TimeZoneId", "RecordsPerPageId", "ProfessionalRegistrationNumber", "ProfessionTypeId", "MaritalStatusId", "ReligionId", "EthnicityId",
              "NationalityId", "PersonGenderId", "PropertyName", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressLine5",
              "Postcode", "Country", "AddressTypeId", "PersonalPhoneLandline", "PersonalPhoneMobile", "BritishCitizenshipId", "CountryOfBirthId",
              "DisabilityStatusId", "YearOfEntry", "EmploymentStatusId", "SecureEmailAddress", "PersonalEmail");

            Assert.AreEqual("Van", systemUserRecordFields["firstname"]);
            Assert.AreEqual("Oliver", systemUserRecordFields["lastname"]);
            Assert.AreEqual("Arthur", systemUserRecordFields["middlename"]);
            Assert.AreEqual(birthDate.Date, systemUserRecordFields["dateofbirth"]);
            Assert.AreEqual(startDate.Date, systemUserRecordFields["startdate"]);
            Assert.AreEqual(userName, systemUserRecordFields["username"]);
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


            #endregion Step 1

            #region Step 2

            systemUsersPage
                .InsertUserName(userName)
                .ClickSearchButton()
                .OpenRecord(systemUserRecord[0].ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateId_Field_Disabled(true)
                .ValidateId_FieldValue(true)
                .ValidateEmploymentStatus_Field_Disabled(true)
                .ValidateLegacyComments_Field_Disabled(true);

            #endregion Step 2

            #region Step 3

            systemUserRecordPage
                .ValidateChangePasswordLink("Change Password")
                .ClickChangePasswordLink();

            changePasswordPopup
                .WaitForChangePasswordPopupToLoad()
                .ValidateNewPasswordField(true)
                .ValidateConfirmNewPasswordField(true);

            #endregion Step 3

            #region Step 4

            changePasswordPopup
                .InsertNewPassword("abcd")
                .TapSaveButton()
                .ValidateNewPasswordField_ErrorLabel("Please enter at least 6 characters.");

            #endregion Step 4

            #region Step 5

            changePasswordPopup
                .InsertNewPassword("abcdef")
                .TapSaveButton()
                .ValidateNewPasswordField_ErrorLabel("Password should have at least one number, one upper case letter and one lower case letter. Also, it should have a length between 6 and 30 characters.");

            #endregion Step 5

            #region Step 6 

            changePasswordPopup
                .InsertNewPassword("Summer@2021")
                .InsertConfirmNewPassword("abcd")
                .ValidateConfirmNewPasswordField_ErrorLabel("Passwords do not match.");

            #endregion Step 6 

        }


        [TestProperty("JiraIssueID", "ACC-3322")]
        [Description("Navigate to the system users page" + "Create a System user account with Inactive to Yes option" + "Click save button and validate the created account is inactive." +
            "Select the inactive users from the system user page and validate the inactive record is displayed" + "Reactivate the account and validate the fields are retained with values and Inactive is No option")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod002()
        {
            var birthDate = DateTime.Now.AddYears(-20);
            var startDate = DateTime.Now.AddYears(-10);
            var userName = "Testing_CDV6_14430_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Step 7 

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

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
                .InsertFirstName("Van")
                .InsertMiddleName("Arthur")
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
                .SelectDisabilityStatus_Options("4");

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
                .ClickInactiveStatus_YesOption()
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .WaitForResultsGridToLoad();

            System.Threading.Thread.Sleep(5000);


            #endregion Step 7 

            #region Step 8 

            var systemUserRecord = dbHelper.systemUser.GetSystemUserByUserName(userName);
            Assert.AreEqual(1, systemUserRecord.Count);

            var systemUserRecordFields = dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserRecord[0], "FirstName", "LastName", "MiddleName", "DateOfBirth", "StartDate", "UserName", "WorkEmail", "JobTitle", "WorkPhoneLandline", "WorkPhoneMobile", "FailedPasswordAttemptCount", "AuthenticationProviderId", "OwningBusinessUnitId",
              "DefaultTeamId", "TimeZoneId", "RecordsPerPageId", "ProfessionalRegistrationNumber", "ProfessionTypeId", "MaritalStatusId", "ReligionId", "EthnicityId",
              "NationalityId", "PersonGenderId", "PropertyName", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressLine5",
              "Postcode", "Country", "AddressTypeId", "PersonalPhoneLandline", "PersonalPhoneMobile", "BritishCitizenshipId", "CountryOfBirthId",
              "DisabilityStatusId", "YearOfEntry", "EmploymentStatusId", "SecureEmailAddress", "PersonalEmail", "inactive");

            Assert.AreEqual("Van", systemUserRecordFields["firstname"]);
            Assert.AreEqual("Oliver", systemUserRecordFields["lastname"]);
            Assert.AreEqual("Arthur", systemUserRecordFields["middlename"]);
            Assert.AreEqual(birthDate.Date, systemUserRecordFields["dateofbirth"]);
            Assert.AreEqual(startDate.Date, systemUserRecordFields["startdate"]);
            Assert.AreEqual(userName, systemUserRecordFields["username"]);
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
            Assert.AreEqual(true, systemUserRecordFields["inactive"]);

            #endregion Step 8

            #region Step 9

            systemUsersPage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(userName)
                .ClickSearchButton()
                .OpenRecord(systemUserRecord[0].ToString());

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .SelectSystemView("Inactive Users")
               .InsertUserName(userName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(systemUserRecord[0].ToString());

            #endregion Step 9 

            #region Step 10 

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad_ActivateRecord()
                .NavigateToDetailsPage()
                .ClickActivateButton();

            #endregion Step 10 

            #region Step 11

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            #endregion Step 11

            #region Step 12

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad();

            var systemUserRecord_Activate = dbHelper.systemUser.GetSystemUserByUserName(userName);
            Assert.AreEqual(1, systemUserRecord.Count);

            var systemUserRecordFields_Activate = dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserRecord_Activate[0], "FirstName", "LastName", "MiddleName", "DateOfBirth", "StartDate", "UserName", "WorkEmail", "JobTitle", "WorkPhoneLandline", "WorkPhoneMobile", "FailedPasswordAttemptCount", "AuthenticationProviderId", "OwningBusinessUnitId",
          "DefaultTeamId", "TimeZoneId", "RecordsPerPageId", "ProfessionalRegistrationNumber", "ProfessionTypeId", "MaritalStatusId", "ReligionId", "EthnicityId",
          "NationalityId", "PersonGenderId", "PropertyName", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "AddressLine5",
          "Postcode", "Country", "AddressTypeId", "PersonalPhoneLandline", "PersonalPhoneMobile", "BritishCitizenshipId", "CountryOfBirthId",
          "DisabilityStatusId", "YearOfEntry", "EmploymentStatusId", "SecureEmailAddress", "PersonalEmail", "inactive");

            Assert.AreEqual("Van", systemUserRecordFields_Activate["firstname"]);
            Assert.AreEqual("Oliver", systemUserRecordFields_Activate["lastname"]);
            Assert.AreEqual("Arthur", systemUserRecordFields_Activate["middlename"]);
            Assert.AreEqual(birthDate.Date, systemUserRecordFields_Activate["dateofbirth"]);
            Assert.AreEqual(startDate.Date, systemUserRecordFields_Activate["startdate"]);
            Assert.AreEqual(userName, systemUserRecordFields_Activate["username"]);
            Assert.AreEqual("abcd@gmail.com", systemUserRecordFields_Activate["workemail"]);
            Assert.AreEqual("Emergrncy Care", systemUserRecordFields_Activate["jobtitle"]);
            Assert.AreEqual("24567891", systemUserRecordFields_Activate["workphonelandline"]);
            Assert.AreEqual("9784567861", systemUserRecordFields_Activate["workphonemobile"]);
            Assert.AreEqual(10, systemUserRecordFields_Activate["failedpasswordattemptcount"]);
            Assert.AreEqual(authenticationproviderid.ToString(), systemUserRecordFields_Activate["authenticationproviderid"].ToString());
            Assert.AreEqual(_careProviders_BusinessUnitId.ToString(), systemUserRecordFields_Activate["owningbusinessunitid"].ToString());
            Assert.AreEqual(_careProviders_TeamId.ToString(), systemUserRecordFields_Activate["defaultteamid"].ToString());
            Assert.AreEqual("GMT Standard Time", systemUserRecordFields_Activate["timezoneid"]);
            Assert.AreEqual(100, systemUserRecordFields_Activate["recordsperpageid"]);
            Assert.AreEqual("546234a", systemUserRecordFields_Activate["professionalregistrationnumber"]);
            Assert.AreEqual(professionType.ToString(), systemUserRecordFields_Activate["professiontypeid"].ToString());
            Assert.AreEqual(maritalStatusId.ToString(), systemUserRecordFields_Activate["maritalstatusid"].ToString());
            Assert.AreEqual(religionId.ToString(), systemUserRecordFields_Activate["religionid"].ToString());
            Assert.AreEqual(_ethnicityId.ToString(), systemUserRecordFields_Activate["ethnicityid"].ToString());
            Assert.AreEqual(nationalityId.ToString(), systemUserRecordFields_Activate["nationalityid"].ToString());
            Assert.AreEqual(1, systemUserRecordFields_Activate["persongenderid"]);
            Assert.AreEqual("field", systemUserRecordFields_Activate["propertyname"]);
            Assert.AreEqual("45", systemUserRecordFields_Activate["addressline1"]);
            Assert.AreEqual("Howard", systemUserRecordFields_Activate["addressline2"]);
            Assert.AreEqual("Framingham Pigot", systemUserRecordFields_Activate["addressline3"]);
            Assert.AreEqual("Norwich", systemUserRecordFields_Activate["addressline4"]);
            Assert.AreEqual("Fox", systemUserRecordFields_Activate["addressline5"]);
            Assert.AreEqual("NR14 7PZ", systemUserRecordFields_Activate["postcode"]);
            Assert.AreEqual("UK", systemUserRecordFields_Activate["country"]);
            Assert.AreEqual(1, systemUserRecordFields_Activate["addresstypeid"]);
            Assert.AreEqual("45612345", systemUserRecordFields_Activate["personalphonelandline"]);
            Assert.AreEqual("8756489224", systemUserRecordFields_Activate["personalphonemobile"]);
            Assert.AreEqual(2, systemUserRecordFields_Activate["britishcitizenshipid"]);
            Assert.AreEqual(countryId.ToString(), systemUserRecordFields_Activate["countryofbirthid"].ToString());
            Assert.AreEqual(4, systemUserRecordFields_Activate["disabilitystatusid"]);
            Assert.AreEqual(2014, systemUserRecordFields_Activate["yearofentry"]);
            Assert.AreEqual(1, systemUserRecordFields_Activate["employmentstatusid"]);
            Assert.AreEqual("dfg@gmail.com", systemUserRecordFields_Activate["secureemailaddress"]);
            Assert.AreEqual("ghf@gmail.com", systemUserRecordFields_Activate["personalemail"]);
            Assert.AreEqual(false, systemUserRecordFields_Activate["inactive"]);


            #endregion Step 12

        }


        [TestProperty("JiraIssueID", "ACC-3323")]
        [Description("Navigate to the system users page" + "Validat the system user Sub entities and Related Items entities")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod003()
        {

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateAvailability_Navigation("Availability")
                .ValidateTeams_Navigation("Teams")
                .ValidateSecurityProfiles_Navigation("Security Profiles");

            #endregion Step 13

            #region Step 14
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateRelatedItems();

            //Newly updated test step in Manual test case
            System.Threading.Thread.Sleep(2000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .ValidateEmploymentSubMenuItems();

            #endregion Step 14



        }



        [TestProperty("JiraIssueID", "ACC-3324")]
        [Description("Navigate to the system users page" + "Open the system user record and Create an Address for the user and do Update the user details and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod004()
        {

            var startDate = DateTime.Now.AddDays(-3);

            #region Step 15 

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

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

            var userAddress = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId);
            Assert.AreEqual(1, userAddress.Count);

            var userAddressFields = dbHelper.systemUserAddress.GetSystemUserAddressBySystemUserID(userAddress[0], "propertyname", "addressline1",
                        "addressline2", "addressline3", "addressline4", "addressline5", "postcode", "country", "addresstypeid", "StartDate", "enddate");


            Assert.AreEqual("MorningSide", userAddressFields["propertyname"]);
            Assert.AreEqual("12", userAddressFields["addressline1"]);
            Assert.AreEqual("via Linda", userAddressFields["addressline2"]);
            Assert.AreEqual("Scottsdale", userAddressFields["addressline3"]);
            Assert.AreEqual("Scottsdale", userAddressFields["addressline4"]);
            Assert.AreEqual("Maricopa", userAddressFields["addressline5"]);
            Assert.AreEqual("85258", userAddressFields["postcode"]);
            Assert.AreEqual("USA", userAddressFields["country"]);
            Assert.AreEqual(1, userAddressFields["addresstypeid"]);


            systemUserAddressRecordPage
              .WaitForSystemUserAddressRecordPageToLoad()
              .InsertPropertyName("MorningSide_Updated")
              .ClickSaveAndCloseButton();

            systemUserAddressPage
               .WaitForSystemUserAddressPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress_Updated = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId);
            Assert.AreEqual(1, userAddress.Count);

            var userAddressFields_Updated = dbHelper.systemUserAddress.GetSystemUserAddressBySystemUserID(userAddress_Updated[0], "propertyname");

            Assert.AreEqual("MorningSide_Updated", userAddressFields_Updated["propertyname"]);

            systemUserAddressPage
              .WaitForSystemUserAddressPageToLoad()
              .SelectRecord(userAddress_Updated[0].ToString())
              .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            systemUserAddressPage
              .WaitForSystemUserAddressPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAddress_Records = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId);
            Assert.AreEqual(0, userAddress_Records.Count);

            systemUserAddressPage
             .WaitForSystemUserAddressPageToLoad()
             .ValidateNoRecordMessageVisibile(true);

            #endregion Step 15
        }

        [TestProperty("JiraIssueID", "ACC-3325")]
        [Description("Navigate to the system users page" + "Open the system user record and Create an Aliases record for the user and do Update the user details and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod005()
        {
            var userName = "Testing_CDV6_14430_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var userid = dbHelper.systemUser.CreateSystemUser(userName, "Ram", "Kumar", "Ram Kumar", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);



            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(userName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                 .WaitForSystemUserAliasesPageToLoad()
                 .ValidatePageTitle("System User Aliases")
                 .ClickAddNewButton();


            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .InsertFirstName("Test")
               .InsertMiddleName("QA")
               .InsertLastName("Automation")
               .ClickAliasTypeLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Birth Name")
                .TapSearchButton()
                .SelectResultElement(_systemUserAliasType.ToString());

            systemUserAliasesRecordPage
                 .WaitForSystemUserAliasesRecordPageToLoad()
                 .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var userAlias = dbHelper.systemUserAlias.GetBySystemUserAliasId(userid);
            Assert.AreEqual(1, userAlias.Count);


            systemUserAliasesRecordPage
              .WaitForSystemUserAliasesRecordPageToLoad();

            var systemUserAliasesRecordFields = dbHelper.systemUserAlias.GetAliasBySystemUserAliasID(userAlias[0], "systemuserid", "firstname", "middlename", "lastname", "systemuseraliastypeid", "preferredname");

            Assert.AreEqual(userid.ToString(), systemUserAliasesRecordFields["systemuserid"].ToString());
            Assert.AreEqual("Test", systemUserAliasesRecordFields["firstname"]);
            Assert.AreEqual("QA", systemUserAliasesRecordFields["middlename"]);
            Assert.AreEqual("Automation", systemUserAliasesRecordFields["lastname"]);
            Assert.AreEqual(_systemUserAliasType.ToString(), systemUserAliasesRecordFields["systemuseraliastypeid"].ToString());
            Assert.AreEqual(false, systemUserAliasesRecordFields["preferredname"]);

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .InsertFirstName("Test Updated")
                .ClickSaveAndCloseButton();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var userAlias_Updated = dbHelper.systemUserAlias.GetBySystemUserAliasId(userid);
            Assert.AreEqual(1, userAlias_Updated.Count);



            var systemUserAliasesRecordFields_Updated = dbHelper.systemUserAlias.GetAliasBySystemUserAliasID(userAlias[0], "firstname");


            Assert.AreEqual("Test Updated", systemUserAliasesRecordFields_Updated["firstname"]);


            systemUserAliasesPage
               .WaitForSystemUserAliasesPageToLoad()
               .SelectRecord(userAlias_Updated[0].ToString())
               .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();


            systemUserAliasesPage
                 .WaitForSystemUserAliasesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAliasesRecords = dbHelper.systemUserAddress.GetBySystemUserAddressId(userid);
            Assert.AreEqual(0, userAliasesRecords.Count);

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #endregion Step 16

        }

        [TestProperty("JiraIssueID", "ACC-3326")]
        [Description("Navigate to the system users page" + "Open the system user record and Create an Emergency Contacts record for the user and do Update the user details and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod006()
        {

            #region Step 17 

            loginPage
                  .GoToLoginPage()
                  .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                  .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmergencyContactsSubPage();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ValidateNoRecordMessageVisibile(true)
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_demographicsTitleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertEndDate(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton();


            System.Threading.Thread.Sleep(3000);

            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, emergencyContacts.Count);


            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3");


            Assert.AreEqual(_demographicsTitleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Adam", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Michel", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("123456", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("000", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("111", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("222", systemUserEmergencyContactsFields["contacttelephoneother3"]);

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam Gil")
               .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var emergencyContacts_Updated = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, emergencyContacts_Updated.Count);


            var systemUserEmergencyContactsFields_Updated = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "firstname");

            Assert.AreEqual("Adam Gil", systemUserEmergencyContactsFields_Updated["firstname"]);

            systemUserEmergencyContactsPage
             .WaitForSystemUserEmergencyContactsPageToLoad()
             .SelectRecord(emergencyContacts[0].ToString())
             .ClickDeletedButton();


            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();



            System.Threading.Thread.Sleep(2000);

            var emergencyContacts_User = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(0, emergencyContacts_User.Count);

            systemUserEmergencyContactsPage
             .WaitForSystemUserEmergencyContactsPageToLoad()
             .ValidateNoRecordMessageVisibile(true);

            #endregion Step 17 
        }

        [TestProperty("JiraIssueID", "ACC-3327")]
        [Description("Navigate to the system users page" + "Open the system user record and Create a Language record for the user and do Update the user details and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod007()
        {

            #region Step 18 

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToLanguagesSubPage();

            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .ClickNewRecordButton();

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad()
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("English (UK)").TapSearchButton().SelectResultElement(_personLanguageId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad()
                .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            var userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, userLanguages.Count);

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad()
                .ValidateSystemUserLinkFieldText("Testing_CDV6_14430 User01_" + currentDateTime)
                .ValidateLanguageLinkFieldText("English (UK)")
                .ValidateStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton()

                .WaitForSystemUserLanguageRecordPageToLoad()
                .ValidateStartDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .SelectRecord(userLanguages[0].ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            System.Threading.Thread.Sleep(2000);


            var userLanguages_Delete = dbHelper.systemUserLanguage.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(0, userLanguages_Delete.Count);

            #endregion

        }


        //Removed the test step from Regression test case CDV6-11921.
        /* [TestProperty("JiraIssueID", "CDV6-14696")]
         [Description("Navigate to the system users page" + "Open the system user record and Create a Employment Contracts record for the user and do Update the user details and validate the Delete functionality")]

         [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"),TestCategory("UK Care Providers Combined")]
         public void SystemUser_RegressionTest_UITestMethod008()
         {


             foreach (var EmploymentContract in dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId))
             dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(EmploymentContract);



             loginPage
                 .GoToLoginPage()
                 .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                 .WaitForCareProvidermHomePageToLoad();

             mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

             systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("Testing_CDV6_14430_User01_Automation")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

             systemUserRecordPage
                 .WaitForSystemUserRecordPageToLoad()
                 .NavigateToEmploymentContractsSubPage();

             systemUserEmploymentContractsPage
                 .WaitForSystemUserEmploymentContractsPageToLoad()
                 .ClickAddNewButton();

             systemUserEmploymentContractsRecordPage
                 .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                 .InsertStartDate(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .InsertContractSignedDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .InsertContractHoursPerWeek("10")
                 .InsertMaximumHoursPerWeek("12")
                 .ClickRoleLoopUpButton();

             lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("AutomationTest")
                 .TapSearchButton()
                 .SelectResultElement(_careProviderStaffRoleTypeid.ToString());


             systemUserEmploymentContractsRecordPage
                 .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                 .ClickTypeLookUpButton();

             lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Salaried")
                 .TapSearchButton()
                 .SelectResultElement(EmploymentContractTypeid.ToString());


             systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveButton();

             System.Threading.Thread.Sleep(2000);

             var employementContract = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
             Assert.AreEqual(1, employementContract.Count);

             systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateContactHoursPerWeek_FieldText("10.00")
                .ValidateMaximumHoursPerWeek_FieldText("12.00")
                .ValidateContractSignDate_FieldText(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateStartDate_FieldText(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateName_FieldText("CDV6-11921")
                .ValidateRole_LinkField("AutomationTest")
                .ValidateType_LinkField("Salaried");

             systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .InsertStartDate(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton();

             systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStartDate_FieldText(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

             systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .SelectRecord(employementContract[0].ToString())
                .ClickDeletedButton();

             alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

             alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

             System.Threading.Thread.Sleep(2000);

             var employementContract_afterDelete = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
             Assert.AreEqual(0, employementContract_afterDelete.Count);




         }

         This method is Failling due Bug Id:CDV6-14689
         [TestProperty("JiraIssueID", "CDV6-14697")]
         [Description("Navigate to the system users page" + "Open the system user record and Create a Suspension record for the user and do Update the user details and validate the Delete functionality")]

         [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"),TestCategory("UK Care Providers Combined")]
         public void SystemUser_RegressionTest_UITestMethod009()
         {
             //deleting EmploymentContract
             foreach (var EmploymentContract in dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId))
                 dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(EmploymentContract);


             var Reasonid = new Guid("26f5b194-fe06-ec11-a327-f90a4322a942");//Injury

             var systemUserEmploymentContractid= dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, DateTime.Now.AddDays(-01), _careProviderStaffRoleTypeid, _careProviders_TeamId, EmploymentContractTypeid);



             loginPage
                 .GoToLoginPage()
                 .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                 .WaitForCareProvidermHomePageToLoad();

             mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

             systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("Testing_CDV6_14430_User01_Automation")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

             systemUserRecordPage
                 .WaitForSystemUserRecordPageToLoad()
                 .NavigateToSuspensionsSubPage();

             systemUserSuspensionsPage
                 .WaitForSystemUserSuspensionsPageToLoad()
                 .ClickAddNewButton();

             systemUserSuspensionsRecordPage
                 .WaitForSystemUserSuspensionsRecordPageToLoad()
                 .InsertSuspensionStartDate(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

             systemUserSuspensionsRecordPage
                 .WaitForSystemUserSuspensionsRecordPageToLoad()
                 .InsertsuspensionStartTime("00:00")
                 .ClickSuspensionReasonLoopUpButton();

             lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Injury")
                .TapSearchButton()
                .SelectResultElement(Reasonid.ToString());

             systemUserSuspensionsRecordPage
                 .WaitForSystemUserSuspensionsRecordPageToLoad()
                 .ClickSaveButton();

             System.Threading.Thread.Sleep(2000);

             var suspensionrecord = dbHelper.systemUserSuspension.GetSystemUserSuspensionBySystemUserId(_systemUserId);
             Assert.AreEqual(1, suspensionrecord.Count);

             systemUserSuspensionsRecordPage
                 .WaitForSystemUserSuspensionsRecordPageToLoad()
                 .ValidateContracts_LinkField("CDV6-11921")
                 .ValidateSuspensionStartDate_Field(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ValidateSuspensionReason_LinkField("Injury");

             systemUserSuspensionsRecordPage
                 .WaitForSystemUserSuspensionsRecordPageToLoad()
                 .InsertSuspensionStartDate(DateTime.Now.AddDays(02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickSaveButton();

             systemUserSuspensionsRecordPage
                .WaitForSystemUserSuspensionsRecordPageToLoad()
                .ValidateSuspensionStartDate_Field(DateTime.Now.AddDays(02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

             systemUserSuspensionsPage
                 .WaitForSystemUserSuspensionsPageToLoad()
                 .SelectRecord(suspensionrecord[0].ToString())
                .ClickDeletedButton();

             alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

             alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

             System.Threading.Thread.Sleep(2000);

             var suspensionRecord_afterDelete = dbHelper.systemUserSuspension.GetSystemUserSuspensionBySystemUserId(_systemUserId);
             Assert.AreEqual(0, suspensionRecord_afterDelete.Count);



         }*/

        [TestProperty("JiraIssueID", "ACC-3328")]
        [Description("Navigate to the system users page" + "Open the system user record and Create a SystemUserEmergencyContacts record for the user and validate the Export To Excel Option")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod010()
        {

            foreach (var EmploymentContract in dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId))
                dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(EmploymentContract);


            loginPage
                 .GoToLoginPage()
                 .Login("RegressionUser1", "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmergencyContactsSubPage();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ValidateNoRecordMessageVisibile(true)
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_demographicsTitleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertEndDate(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();


            System.Threading.Thread.Sleep(3000);

            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, emergencyContacts.Count);


            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3");


            Assert.AreEqual(_demographicsTitleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Adam", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Michel", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("123456", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("000", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("111", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("222", systemUserEmergencyContactsFields["contacttelephoneother3"]);


            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .SelectRecord(emergencyContacts[0].ToString())
               .ClickExportToExcelButton();

            exportDataPopup
             .WaitForExportDataPopupToLoad()
             .SelectRecordsToExport("Selected Records")
             .SelectExportFormat("Csv (comma separated with quotes)")
             .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "SystemUserEmergencyContacts.csv");
            Assert.IsTrue(fileExists);



        }

        [TestProperty("JiraIssueID", "ACC-3329")]
        [Description("Navigate to the system users page" + "Open the system user record and Create a Emergency contacts record for the user and do Update the user details and validate Audit Caputure the update")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod011()
        {



            loginPage
                  .GoToLoginPage()
                  .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmergencyContactsSubPage();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ValidateNoRecordMessageVisibile(true)
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_demographicsTitleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertEndDate(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton();


            System.Threading.Thread.Sleep(3000);

            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, emergencyContacts.Count);


            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3");


            Assert.AreEqual(_demographicsTitleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Adam", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Michel", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("123456", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("000", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("111", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("222", systemUserEmergencyContactsFields["contacttelephoneother3"]);

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam Gil")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var emergencyContacts_Updated = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, emergencyContacts_Updated.Count);


            var systemUserEmergencyContactsFields_Updated = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "firstname");

            Assert.AreEqual("Adam Gil", systemUserEmergencyContactsFields_Updated["firstname"]);

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .NavigateToAuditPage();

            auditListPage
               .WaitForAuditListPageToLoad("systemuseremergencycontacts");

            //var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            //{
            //    CurrentPage = "0",
            //    TypeName = "audit",
            //    ParentId = emergencyContacts[0].ToString(),
            //    ParentTypeName = "systemuseremergencycontacts",
            //    RecordsPerPage = "100",
            //    ViewType = "0",
            //    AllowMultiSelect = "false",
            //    ViewGroup = "1",
            //    IsGeneralAuditSearch = false,
            //    UsePaging = true,
            //    PageNumber = 1,
            //    Operation = 2,
            //    Year = DateTime.Now.Year,
            //};

            //WebAPIHelper.Security.Authenticate();
            //var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            //Assert.AreEqual(1, auditResponseData.GridData.Count);
            //Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);

            System.Threading.Thread.Sleep(5000);

            auditListPage
                    .WaitForAuditListPageToLoad("systemuseremergencycontacts")
                    .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("Regression User1");


        }

        ////Since it is a Duplicate of 14699, I am checking out the code.
        //[TestProperty("JiraIssueID", "CDV6-14700")]
        //[Description("Navigate to the system users page" + "Open the system user record and Create a Employment Contracts record for the user and do Update the user details and validate the Delete functionality")]

        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"),TestCategory("UK Care Providers Combined")]
        //public void SystemUser_RegressionTest_UITestMethod0012()
        //{

        //    foreach (var EmploymentContract in dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId))
        //        dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(EmploymentContract);


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
        //        .WaitForCareProvidermHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //       .WaitForSystemUsersPageToLoad()
        //       .InsertUserName(_systemUserName)
        //       .ClickSearchButton()
        //       .WaitForResultsGridToLoad()
        //       .OpenRecord(_systemUserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .NavigateToEmploymentContractsSubPage();

        //    systemUserEmploymentContractsPage
        //        .WaitForSystemUserEmploymentContractsPageToLoad()
        //        .ClickAddNewButton();

        //    systemUserEmploymentContractsRecordPage
        //        .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //        .InsertName("CDV6-11921")
        //        .InsertStartDate(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //        .InsertContractSignedDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //        .InsertContractHoursPerWeek("10")
        //        .InsertMaximumHoursPerWeek("12")
        //        .ClickRoleLoopUpButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad()
        //        .TypeSearchQuery("AutomationTest")
        //        .TapSearchButton()
        //        .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

        //    systemUserEmploymentContractsRecordPage
        //        .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //        .ClickTypeLookUpButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad()
        //        .TypeSearchQuery("Salaried")
        //        .TapSearchButton()
        //        .SelectResultElement(EmploymentContractTypeid.ToString());

        //    systemUserEmploymentContractsRecordPage
        //       .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //       .ClickSaveButton();

        //    System.Threading.Thread.Sleep(2000);

        //    var employementContract = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
        //    Assert.AreEqual(1, employementContract.Count);

        //    systemUserEmploymentContractsRecordPage
        //       .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //       .ClickAuditButton();

        //    auditListPage
        //       .WaitForAuditListPageToLoad("systemuseremploymentcontract");

        //    var updateAudits = dbHelper.audit.GetAuditByRecordID(employementContract[0], 2); //get all update operations
        //    Assert.AreEqual(1, updateAudits.Count);

        //    auditListPage
        //        .WaitForAuditListPageToLoad("systemuseremploymentcontract")
        //        .ValidateRecordPresent(updateAudits.First().ToString())
        //        .ClickOnAuditRecord(updateAudits.First().ToString());

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //       .WaitForSystemUsersPageToLoad()
        //       .InsertUserName(_systemUserName)
        //       .ClickSearchButton()
        //       .WaitForResultsGridToLoad()
        //       .OpenRecord(_systemUserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .NavigateToEmploymentContractsSubPage();

        //    systemUserEmploymentContractsPage
        //     .WaitForSystemUserEmploymentContractsPageToLoad()
        //     .SelectRecord(employementContract[0].ToString())
        //     .ClickDeletedButton();

        //    alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

        //    alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();





        //}

        [TestProperty("JiraIssueID", "ACC-3330")]
        [Description("Navigate to the system users page" + "Create the System User Record and then Navigate to Advance Search and Validate user is able to Search via advance Search")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod013()
        {


            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFieldOption("0", "User Name")
                .InsertFieldOptionValue(_systemUserName)
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_systemUserId.ToString());

        }


        [TestProperty("JiraIssueID", "ACC-3331")]
        [Description("Navigate to the system users page" + "Create System User Record And then Try to login with that system user with wrong Password for one time and then try to login with correct Password" +
            "Navigate to system user and validate the Failed Password attempt count is Recorded")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod014()
        {

            _enableLockingAccountId = dbHelper.systemSetting.GetSystemSettingIdByName("EnableAccountLocking")[0];

            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickSystemSettingLink();

            systemSettingsPage
                .WaitForSystemSettingsPageToLoad()
                .InsertQuickSearchText("EnableAccountLocking")
                .ClickQuickSearchButton()
                .WaitForSystemSettingsPageToLoad()
                .OpenRecord(_enableLockingAccountId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("systemsetting")
                .ClickOnExpandIcon();

            systemSettingRecordPage
                .WaitForSystemSettingRecordPageToLoad()
                .InsertSettingValue("true")
                .ClickSaveButton()
                .WaitForSystemSettingRecordPageToLoad()
                .ValidateSettingValueFieldText("true");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Summer2013", "Care Providers");

            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateFailedPasswordAttemptCountFieldValue("1")
                .ValidateLastFailedPasswordAttemptDateFieldValue(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));



        }

        [TestProperty("JiraIssueID", "ACC-3332")]
        [Description("Navigate to the system users page" + "Create System user Record" + "NAvigate to System Setting Link and open EnableAccountLocking Record" +
            "Change the Setting Value='true'and then save the Record" + "try to login with new system user but with wrong password for 3 times and then login with other system user " +
            "Navigate to system user page+open the Previously created Record and Validate the Failed Password Attempt count is Recorded and is account locked Status is changed to 'Yes'" +
            "Validate Failed Password Attempt Date is auto populate and change the Is Account Locked='No' and then save the Record and Try to Login to Login " +
            "Validate that user is succesfully Loged In ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_RegressionTest_UITestMethod015()
        {
            //  var AccountLockedThresholdvalue = dbHelper.authenticationProvider.GetFieldsByAuthenticationProviderID(authenticationproviderid);
            var _accountLockedThresholdvalue = (int)dbHelper.authenticationProvider.GetFieldsByAuthenticationProviderID(authenticationproviderid, "accountlockoutthreshold")["accountlockoutthreshold"];

            dbHelper.systemUser.UpdateIsAccountLocked(_systemUserId, false);
            dbHelper.systemUser.UpdateFailedPasswordAttemptCount(_systemUserId, null);

            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickSystemSettingLink();

            systemSettingsPage
                .WaitForSystemSettingsPageToLoad()
                .InsertQuickSearchText("EnableAccountLocking")
                .ClickQuickSearchButton()
                .WaitForSystemSettingsPageToLoad()
                .OpenRecord(_enableLockingAccountId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("systemsetting")
                .ClickOnExpandIcon();

            systemSettingRecordPage
                .WaitForSystemSettingRecordPageToLoad()
                .InsertSettingValue("true")
                .ClickSaveButton()
                .WaitForSystemSettingRecordPageToLoad()
                .ValidateSettingValueFieldText("true");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            int loginattempt;

            for (loginattempt = 0; loginattempt <= _accountLockedThresholdvalue; loginattempt++)
            {
                loginPage
                   .GoToLoginPage()
                   .Login(_systemUserName, "Summer2013", "Care Providers");
            }

            loginPage
                .GoToLoginPage()
                .Login("RegressionUser1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateFailedPasswordAttemptCountFieldValue(loginattempt.ToString())
                .ValidateLastFailedPasswordAttemptDateFieldValue(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateAccountLockedOutDateFieldValue(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateIsAccountLock_YesOptionChecked(true)
                .ClickIsAccountLocked_NoOption()
                .SelectGender_Options("Male")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .WaitForResultsGridToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickSignOutButton();

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Summer2013@", "Care Providers")
                .WaitFormHomePageToLoad(false, false, false);
        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
    #endregion


}



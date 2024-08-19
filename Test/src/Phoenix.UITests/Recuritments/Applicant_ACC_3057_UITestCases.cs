using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    [TestClass]
    public class Applicant_ACC_3057_UITestCases : FunctionalTest
    {
        #region Private Properties
        private string EnvironmentName;
        private string _tenantName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _businessUnitId;
        private Guid _productLanguageId;
        private Guid _languageId2;
        private Guid _fluencyId;
        private Guid _teamId;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private string _username = "ACC3057User";

        #endregion

        [TestInitialize()]
        public void ACC3057_Setup()
        {
            try
            {
                #region Tenant

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareProviders", null, _businessUnitId, null, "CareProviderQA@careworkstempmail.com", "Default team for business unit", null);

                #endregion

                #region Product Language

                _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Create SystemUser Record

                commonMethodsDB.CreateSystemUserRecord(_username, "ACC", "3057User", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);

                #endregion

                #region Language

                _languageId2 = dbHelper.language.CreateLanguage("English (US)" + currentDateTime, _teamId, "3245", "010", new DateTime(2016, 1, 1), null);

                #endregion

                #region Fluency

                _fluencyId = dbHelper.languageFluency.CreateLanguageFluency("Fluently" + currentDateTime, _teamId, DateTime.Now.AddYears(-3));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-3048

        [TestProperty("JiraIssueID", "ACC-3616")]
        [Description("Test case for CDV6-11686-Add a new Recruitment Applicant")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        public void Applicant_ACC_3057_UITestMethod001()
        {
            #region Step 1            

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            #endregion

            #region Step 2
            //Enabling and disabling modules cannot be done through automation.Therefore, this step would not be automated.
            #endregion

            #region Step 3

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FName")
                .InsertLastName("LN_" + currentDateTime)
                .InsertAvailableFromDateField("01/01/2020")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FName")
                .ValidateLastName("LN_" + currentDateTime)
                .ValidateAvailableFrom("01/01/2020");

            #endregion

            #region Step 4

            applicantRecordPage
                .ClickBackButton();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateFirstNameFieldErrorMeessage("Please fill out this field.")
                .ValidateLastNameFieldErrorMeessage("Please fill out this field.")
                .ValidateAvailableFromFieldErrorMeessage("Please fill out this field.");

            #endregion

            #region Step 5

            applicantRecordPage
                .ClickBackButton();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FName2")
                .InsertLastName("LN2_" + currentDateTime)
                .InsertAvailableFromDateField("01/01/2020")
                .InsertTextInPersonalPhoneMobileField("0987654321")
                .InsertTextInPersonalPhoneLandlineField("0987654321")
                .InsertPersonalEmail("11012023@xmail.com")
                .InsertPostcode("3057")
                .InsertTownOrCity("TOW")
                .SelectAddressType("Home")
                .InsertStartDate("01/01/2020")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FName2")
                .ValidateLastName("LN2_" + currentDateTime)
                .ValidateAvailableFrom("01/01/2020")
                .ValidatePersonalPhoneMobileField("0987654321")
                .ValidatePersonalPhoneLandlineField("0987654321")
                .ValidatePersonalEmail("11012023@xmail.com")
                .ValidatePostcode("3057")
                .ValidateTownOrCity("TOW")
                .ValidateAddressType("Home")
                .ValidateStartDate("01/01/2020");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3617")]
        [Description("Test case for CDV6-11686-Add a new Recruitment Applicant")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Applicant Languages")]
        public void Applicant_ACC_3057_UITestMethod002()
        {
            #region Step 6 

            #region Create Applicant

            var applicantFirstName = "Test_3057";
            var applicantLastName = "User_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName);

            applicantPage
                .WaitForApplicantsPageToLoad()
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Languages")
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .InsertStartDate("01/01/2020")
                .ClickLanguageLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("English (US)" + currentDateTime)
                .TapSearchButton()
                .SelectResultElement(_languageId2.ToString());

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .ClickFluencyLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Fluently" + currentDateTime)
                .TapSearchButton()
                .SelectResultElement(_fluencyId.ToString());

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidatApplicantLinkFieldText(applicantFirstName + " " + applicantLastName)
                .ValidateLanguageLinkFieldText("English (US)" + currentDateTime)
                .ValidateFluencyLinkFieldText("Fluently" + currentDateTime)
                .ValidateStartDate("01/01/2020");

            applicantLanguageRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Aliases")
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            applicantAliasRecordPage
                .WaitForApplicantAliasRecordPageToLoad()
                .InsertLastName("LN_" + currentDateTime)
                .InsertMiddleName("MN_" + currentDateTime)
                .InsertFirstName("FN_" + currentDateTime)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateLastNameFieldValue("LN_" + currentDateTime)
                .ValidateFirstNameFieldValue("FN_" + currentDateTime)
                .ValidateMiddleNameFieldValue("MN_" + currentDateTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3618")]
        [Description("Test case for CDV6-11686-Add a new Recruitment Applicant")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        public void Applicant_ACC_3057_UITestMethod003()
        {
            var _pronoun_HeHim = dbHelper.pronouns.GetByName("He/Him").First();
            var _maritalStatus_NotApplicable = dbHelper.maritalStatus.GetMaritalStatusIdByName("Not applicable").First();
            var _religion_Agnostic = dbHelper.religion.GetByName("Agnostic").First();
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish").First();
            var _nationalityId = dbHelper.nationality.GetNationalityByName("Afghan").First();
            var _countryOfBirthId = dbHelper.country.GetByName("Afghanistan").First();

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FName3")
                .InsertLastName("LN3_" + currentDateTime)
                .InsertDateOfBirth("01/01/1975")
                .SelectStatedGender("Male")
                .InsertRecruitmentApplicantNotes("Notes...")
                .InsertAvailableFromDateField("01/01/2020")
                .InsertTextInPersonalPhoneMobileField("0987654321")
                .InsertTextInPersonalPhoneLandlineField("0987654321")
                .InsertPersonalEmail("26072023@xmail.com")
                .InsertPropertyName("PNc")
                .InsertPropertyNo("PNOc")
                .InsertStreetEN("STRc")
                .InsertVillageOrDistrict("VLGc")
                .InsertPostcode("3057c")
                .InsertTownOrCity("TOW")
                .InsertCounty("COc")
                .InsertCountry("EN")
                .SelectAddressType("Home")
                .InsertStartDate("01/01/2020")
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_pronoun_HeHim.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_maritalStatus_NotApplicable.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_religion_Agnostic.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .SelectBritishCitizenship("No")
                .ClickCountryOfBirthNotKnown_NoOption()
                .ClickNotBornInUKButCountryUnknown_NoOption()
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_ethnicityId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_nationalityId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickCountryOfBirthLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_countryOfBirthId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertYearOfEntryToUK("2021")
                .SelectDisablityStatus("Not Known")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FName3")
                .ValidateLastName("LN3_" + currentDateTime)
                .ValidateDateOfBirth("01/01/1975")
                .ValidateStatedGender("Male")
                .ValidateRecruitmentApplicantNotes("Notes...")
                .ValidateAvailableFrom("01/01/2020")
                .ValidatePersonalPhoneMobileField("0987654321")
                .ValidatePersonalPhoneLandlineField("0987654321")
                .ValidatePersonalEmail("26072023@xmail.com")
                .ValidatePropertyName("PNc")
                .ValidatePropertyNo("PNOc")
                .ValidateStreetEN("STRc")
                .ValidateVillageOrDistrict("VLGc")
                .ValidatePostcode("3057c")
                .ValidateTownOrCity("TOW")
                .ValidateCounty("COc")
                .ValidateCountry("EN")
                .ValidateAddressType("Home")
                .ValidateStartDate("01/01/2020")
                .ValidatePronounFieldLinkText("He/Him")
                .ValidateMaritalStatusFieldLinkText("Not applicable")
                .ValidateReligionFieldLinkText("Agnostic")
                .ValidateBritishCitizenship("No")
                .ValidateCountryOfBirthNotKnown_NoOptionSelected()
                .ValidateNotBornInUKButCountryUnknown_NoOptionSelected()
                .ValidateEthnicityFieldLinkText("Irish")
                .ValidateNationalityFieldLinkText("Afghan")
                .ValidateCountryOfBirthFieldLinkText("Afghanistan")
                .ValidateYearOfEntryToUK("2021")
                .ValidateDisabilityStatus("Not Known");

            #endregion

            #region Step 8
            //Address Type field is not auto-populated by default and also not read only. The step is not applicable and would not be automated.

            #endregion

            #region Step 9

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FN3_Update")
                .InsertLastName("LN3_Update" + currentDateTime)
                .SelectStatedGender("Female")
                .InsertPostcode("3057c_update")
                .InsertTownOrCity("TOW_Update")
                .InsertTextInPersonalPhoneMobileField("7654321098")
                .InsertTextInPersonalPhoneLandlineField("5432109876")
                .InsertPersonalEmail("update@ymail.com")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FN3_Update")
                .ValidateLastName("LN3_Update" + currentDateTime)
                .ValidateStatedGender("Female")
                .ValidatePostcode("3057c_update")
                .ValidateTownOrCity("TOW_Update")
                .ValidatePersonalPhoneMobileField("7654321098")
                .ValidatePersonalPhoneLandlineField("5432109876")
                .ValidatePersonalEmail("update@ymail.com");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3620")]
        [Description("Test case for CDV6-11686-Add a new Recruitment Applicant")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        public void Applicant_ACC_3057_UITestMethod004()
        {
            #region Create Applicant

            var applicantFirstName = "Test_3057";
            var applicantLastName = "User4_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Non-admin user

            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            //var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Scheduling (Edit)").First();
            var securityProfileId8 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First();
            var securityProfileId13 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            var securityProfileId14 = dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First();
            var securityProfileId15 = dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First();
            var securityProfileId16 = dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First();
            var securityProfileId17 = dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First();
            var securityProfileId18 = dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application").First();

            var securityProfilesList = new List<Guid> {securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId8, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15,
                securityProfileId16, securityProfileId17, securityProfileId18 };

            var NonAdminLoginUser = "NonAdmin_" + currentDateTime;
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "Nonadmin", currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid, securityProfilesList, 2);

            //if (!dbHelper.systemUser.GetSystemUserByUserName(NonAdminLoginUser).Any())
            //{
            //    dbHelper.systemUser.CreateSystemUser(NonAdminLoginUser, "ACCNonAdmin", "User1", "ACCNonAdmin User1", "Passdw0rd_!",
            //                    "acc@workemail.com", "acc@secureemail.com", "GMT Standard Time", new DateTime(2020, 1, 1), null, _productLanguageId,
            //                    _authenticationproviderid, _businessUnitId, _teamId, false, 2, null, new DateTime(2020, 1, 1), 1);
            //}
            //var NonAdminUserId = dbHelper.systemUser.GetSystemUserByUserName(NonAdminLoginUser).FirstOrDefault();
            //dbHelper.systemUser.UpdateLastPasswordChangedDate(NonAdminUserId, DateTime.Now.Date);
            //dbHelper.userSecurityProfile.CreateMultipleUserSecurityProfile(NonAdminUserId, securityProfilesList);

            #endregion

            #region Step 11    

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString());

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            #endregion

            #region Step 10

            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var securityProfileExists = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId6).Any();

            if (securityProfileExists)
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId6).First());

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickStaffMenu()
                .ValidateApplicantsMenuLinkVisible(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3624")]
        [Description("Test case for CDV6-11686-Add a new Recruitment Applicant")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Advanced Search")]
        public void Applicant_ACC_3057_UITestMethod005()
        {
            #region Step 12 

            #region Create Applicant

            var applicantFirstName = "Test_3624";
            var applicantLastName = "User_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Applicants")
                .SelectFilter("1", "Applicant")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("*" + applicantLastName + "*", _applicantId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_applicantId.ToString());

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "First Name")
                .ResultsPageValidateHeaderCellText(3, "Last Name")
                .ResultsPageValidateHeaderCellText(4, "Postcode")
                .ResultsPageValidateHeaderCellText(5, "Town / City")
                .ResultsPageValidateHeaderCellText(6, "Date of Birth")
                .ResultsPageValidateHeaderCellText(7, "Stated Gender")
                .ResultsPageValidateHeaderCellText(8, "Personal Phone (Landline)")
                .ResultsPageValidateHeaderCellText(9, "Personal Phone (Mobile)")
                .ResultsPageValidateHeaderCellText(10, "Personal Email");

            #endregion

            #region Step 13 and Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidatePropertyNameFieldIsVisble(true)
                .ValidatePropertyNoFieldIsVisble(true)
                .ValidateStreetENFieldIsVisble(true)
                .ValidateVillageDistrictFieldIsVisble(true)
                .ValidateTownCityFieldIsVisble(true)
                .ValidatePostcodeFieldIsVisble(true)
                .ValidateCountyFieldIsVisble(true)
                .ValidateCountryFieldIsVisble(true)
                .ValidateAddressTypeFieldIsVisble(true)
                .ValidateStartDateFieldIsVisble(true)
                .ValidateClearAddressButtonIsVisble(true);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FName5")
                .InsertLastName("LN5_" + currentDateTime)
                .InsertAvailableFromDateField("01/01/2020")
                .InsertPostcode("3624")
                .InsertTownOrCity("TOW")
                .SelectAddressType("Home")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidateStartDateFieldErrorMeessage("Please fill out this field.");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertStartDate("01/01/2020")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FName5")
                .ValidateLastName("LN5_" + currentDateTime)
                .ValidateAvailableFrom("01/01/2020")
                .ValidatePostcode("3624")
                .ValidateTownOrCity("TOW")
                .ValidateAddressType("Home")
                .ValidateStartDate("01/01/2020");

            #endregion

        }

        #endregion

    }
}

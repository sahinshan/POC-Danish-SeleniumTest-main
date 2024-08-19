using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Applicant UI Page
    /// </summary>
    [TestClass]
    public class ApplicantLanguageAndAlias_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _productLanguageId;
        private Guid _languageId;
        private Guid _languageId2;
        private Guid _fluencyId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _applicantId;
        private Guid _applicantLanguageId;
        private Guid _applicantAliasId;
        public Guid environmentid;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss_FFFFFFF");
        private string applicantFirstName;
        private string applicantLastName;

        #endregion


        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");


                #endregion

                #region Product Language

                _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Language

                _languageId = dbHelper.language.CreateLanguage("English (UK)" + currentTimeString, _careProviders_TeamId, "2345", "001", new DateTime(2015, 1, 1), null);
                _languageId2 = dbHelper.language.CreateLanguage("English (US)" + currentTimeString, _careProviders_TeamId, "3245", "010", new DateTime(2016, 1, 1), null);

                #endregion

                #region Fluency

                _fluencyId = dbHelper.languageFluency.CreateLanguageFluency("Fluently" + currentTimeString, _careProviders_TeamId, DateTime.Now.AddYears(-3));

                #endregion Fluency

                #region Create default system user

                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("Login_User" + currentTimeString, "Login_", "Automation_User_" + currentTimeString, "Login Automation User_" + currentTimeString, "Passw0rd_!", "Login_Automation_User@somemail.com", "Login_Automation_User@somemail.com", "GMT Standard Time", null, null, _productLanguageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Create Applicant
                applicantFirstName = "Testing_CDV6_20560";
                applicantLastName = "User_" + currentTimeString;
                _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

                #endregion

                #region Applicant Language

                _applicantLanguageId = dbHelper.applicantLanguage.CreateApplicantLanguage(_careProviders_TeamId, _applicantId, _languageId, _fluencyId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

                #endregion

                #region Applicant Alias

                _applicantAliasId = dbHelper.applicantAlias.CreateApplicantAlias(_careProviders_TeamId, _applicantId, "AliasFN_" + currentTimeString, "AliasLN_" + currentTimeString);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-20843

        [TestProperty("JiraIssueID", "ACC-3487")]
        [Description("Login CD as a Care Provider. Go to Workplace - Recruitment - Applicants. Open an applicant who has some Languages and Aliases added to it." +
            "Scroll down and from the grid, click on a Language record." +
            "The applicant lookup is non editable." +
            "The lookup fields, Applicant and Responsible Team should become read-only on save.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicant Languages")]
        public void ApplicantLanguageAndAlias_UITestCases001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .ValidateApplicantSubRecordIsPresent(_applicantLanguageId.ToString())
                .OpenApplicantSubRecord(_applicantLanguageId.ToString());

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .ValidateApplicantLookUpButtonDisabled(true)
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Languages")
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .InsertStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickLanguageLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("English (US)" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_languageId2.ToString());

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .ClickFluencyLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Fluently" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_fluencyId.ToString());

            applicantLanguageRecordPage
                .WaitForApplicantLanguageRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApplicantLookUpButtonDisabled(true)
                .ValidateResponsibleTeamLookUpButtonDisabled(true);

        }

        [TestProperty("JiraIssueID", "ACC-3488")]
        [Description("Login CD as a Care Provider. Go to Workplace - Recruitment - Applicants. Open an applicant who has some Languages and Aliases added to it." +
            "Scroll down and from the grid, click on a Alias record." +
            "The applicant lookup is non editable." +
            "The lookup fields, Applicant and Responsible Team should become read-only on save.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicant Aliases")]
        public void ApplicantLanguageAndAlias_UITestCases002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Aliases")
                .ValidateApplicantSubRecordIsPresent(_applicantAliasId.ToString())
                .OpenApplicantSubRecord(_applicantAliasId.ToString());

            applicantAliasRecordPage
                .WaitForApplicantAliasRecordPageToLoad()
                .ValidateApplicantLookUpButtonDisabled(true)
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Aliases")
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            applicantAliasRecordPage
                .WaitForApplicantAliasRecordPageToLoad()
                .InsertLastName("LN_" + currentTimeString)
                .InsertMiddleName("MN_" + currentTimeString)
                .InsertFirstName("FN_" + currentTimeString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApplicantLookUpButtonDisabled(true)
                .ValidateResponsibleTeamLookUpButtonDisabled(true)
                .ValidateLastNameFieldValue("LN_" + currentTimeString);

        }

        #endregion
    }
}

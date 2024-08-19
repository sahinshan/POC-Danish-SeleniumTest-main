using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_RecentlyViewed_UITestCase : FunctionalTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-18000

        #region Properties

        private string environmentName;

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _AutomationTestUser_CDV618000;
        private string _testUser_PartialName = DateTime.Now.ToString("yyyyMMddHHmmss_FFFFF");
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;

        private Guid _personID;
        private int _personNumber;
        private string _personFirstName = "PersonRecord_18000" + DateTime.Now;
        private string _personFullName;
        private Guid _caseStatusId;
        private Guid _caseId;
        private string _caseNumber;
        private string _caseTitle;
        private string _loginUsername;

        #endregion       

        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Environment

                environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
                if (dataEncoded.Equals("true"))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(username);
                    username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                }

                var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region System User

                _AutomationTestUser_CDV618000 = dbHelper.systemUser.CreateSystemUser("Automation_TestUser_18000_" + _testUser_PartialName, "Automation", "TestUser 18000 " + _testUser_PartialName, "Automation TestUser 18000 " + _testUser_PartialName, "Passw0rd_!", "Automation_TestUser_18000_" + _testUser_PartialName + "@somemail.com", "Automation_TestUser_18000@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_AutomationTestUser_CDV618000, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_AutomationTestUser_CDV618000, DateTime.Now.Date);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("18000_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "18000_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("18000_Ethnicity")[0];

                #endregion

                #region Person

                _personID = dbHelper.person.CreatePersonRecord("", _personFirstName, "", "LastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                _personFullName = _personFirstName + " LastName";

                #endregion

                #region Contact Reason
                try
                {

                    var contactReasonExists = dbHelper.contactReason.GetByName("18000_ContactReason").Any();
                    if (!contactReasonExists)
                        dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "18000_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
                    _contactReasonId = dbHelper.contactReason.GetByName("18000_ContactReason")[0];

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unable to find the requested business object contactreason"))
                        return; //we shut down the test without failing if the current tenant do not have the 'Contact/Case' module active

                    throw ex;
                }
                #endregion

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("18000_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "18000_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("18000_ContactSource")[0];

                #endregion

                #region Case

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
                var dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _AutomationTestUser_CDV618000, _AutomationTestUser_CDV618000, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];
                _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        [TestProperty("JiraIssueID", "CDV6-18317")]
        [Description("Navigate to Workplace. Choose some Person records, Case Records and provider records to open on screen. Click on 'Recently Viewed' button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void RecentlyViewedRecord_UITestMethod01()
        {

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickRecentlyViewdButton();

            mainMenu
                .WaitForRecentlyViewedAreaToLoad()
                .ValidateRecentlyViewdAreaLinkElementVisible(_personFullName, _personID.ToString());

            mainMenu
                .ClickRecentlyViewdButton()
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickRecentlyViewdButton();

            mainMenu
                .WaitForRecentlyViewedAreaToLoad()
                .ValidateRecentlyViewdAreaLinkElementVisible(_caseTitle, _caseId.ToString());

        }

        #endregion
    }
}

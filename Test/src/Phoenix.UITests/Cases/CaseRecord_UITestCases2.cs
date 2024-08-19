using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class CaseRecord_UITestCases2 : FunctionalTest
    {

        private string _environmentName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _careDirector_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _caseStatusId;

        private Guid _dataFormId;

        [TestInitialize()]
        public void TestsInitializationMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region System Settings

                var systemSettingExists = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").Any();
                if (!systemSettingExists)
                {
                    dbHelper.systemSetting.CreateSystemSetting("AllowMultipleActiveSocialCareCase", "true", "When set to true the organization will be able to decide if they want to allow multiple active social care referrals", false, null);
                }
                else
                {
                    var systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName("AllowMultipleActiveSocialCareCase").First();
                    dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, "true");
                }

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Teams

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                teamsExist = dbHelper.team.GetTeamIdByName("CareDirector2").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector2", null, _careDirectorQA_BusinessUnitId, "907670", "CareDirector2@careworkstempmail.com", "CareDirector2", "020 123400");
                _careDirector_TeamId = dbHelper.team.GetTeamIdByName("CareDirector2")[0];

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

                #endregion

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "PersonCareTest_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason")[0];

                #endregion

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "PersonCareTest_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource")[0];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region DataForm

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-14960

        [TestProperty("JiraIssueID", "CDV6-25001")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_AssignCaseRecord_UITestMethod01()
        {

            #region Create SystemUser Record if needed 

            Guid _cw_Test_User_CDV6_14960_SystemUserId;

            var userExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_14960").Any();
            if (!userExists)
            {
                _cw_Test_User_CDV6_14960_SystemUserId = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_14960", "CW", "Test_User_CDV6_14960", "CW Test_User_CDV6_14960", "Passw0rd_!", "CW_Test_User_CDV6_14960@somemail.com", "CW_Test_User_CDV6_14960@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Assign Records")[0];
                var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
                var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Case Module (BU Edit)")[0];
                var secProfile4 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0];
                var secProfile5 = dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)")[0];

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_cw_Test_User_CDV6_14960_SystemUserId, secProfile1);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_cw_Test_User_CDV6_14960_SystemUserId, secProfile2);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_cw_Test_User_CDV6_14960_SystemUserId, secProfile3);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_cw_Test_User_CDV6_14960_SystemUserId, secProfile4);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_cw_Test_User_CDV6_14960_SystemUserId, secProfile5);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_cw_Test_User_CDV6_14960_SystemUserId, DateTime.Now.Date);
            }

            _cw_Test_User_CDV6_14960_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_14960").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_cw_Test_User_CDV6_14960_SystemUserId, DateTime.Now.Date);

            #endregion

            #region Create Person record

            var personFirstName = "Testing_CDV6_14956";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");

            var _newPersonID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, personLastName, new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _newPersonNumber = (int)dbHelper.person.GetPersonById(_newPersonID, "personnumber")["personnumber"];

            #endregion

            #region Create Case Record

            var _newCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _newPersonID, _cw_Test_User_CDV6_14960_SystemUserId, _cw_Test_User_CDV6_14960_SystemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
            var _newCaseNumber = (string)dbHelper.Case.GetCaseByID(_newCaseId, "casenumber")["casenumber"];

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Test_User_CDV6_14960", "Passw0rd_!", _environmentName)
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(_newCaseNumber, _newCaseId.ToString())
                .SelectCaseRecord(_newCaseId.ToString())
                .ClickAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("CareDirector2").TapSearchButton().SelectResultElement(_careDirector_TeamId.ToString());

            casesPage.
                WaitForCasesPageToLoad();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad()
                .SelectResponsibleUserDecision("Do not change")
                .TapOkButton();

            casesPage.
                WaitForCasesPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var responsibleTeamId = Guid.Parse(dbHelper.Case.GetCaseByID(_newCaseId, "ownerid")["ownerid"].ToString());
            Assert.AreEqual(_careDirector_TeamId, responsibleTeamId);

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

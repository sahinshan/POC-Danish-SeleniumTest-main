using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.QualityAndCompliance
{
    /// <summary>
    /// </summary>
    [TestClass]
    public class OrganizationalRisks_UITestCase : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _riskTypeId;
        private Guid _organisationalRisk1ID;
        private Guid _organisationalRisk2ID;
        private Guid _organisationalRisk3ID;

        private Guid _systemUserId;
        private Guid _environmentId;
        private Guid adminUserId;


        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Connecting Database : CareProvider

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString = connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString.Replace("&quot;", "\"").Replace("CareDirectorQA_CD", "CareProviders_CD");
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                #endregion Connecting Database : CareProvider

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Create Admin user

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").Any();
                if (!adminUserExists)
                {
                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2", "CW", "Admin Test User 2", "CW Admin Test User 2", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemUserSecureFieldsSecurityProfileId);
                }



                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion Create Admin User

                #region System User

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User_03").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_User_03", "Testing", "CDV6_User_03", "Testing CDV6_User_03", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User_03")[0];




                #endregion

                #region Organizational Risk Category

                foreach (var organizationalRiskCategoryId in dbHelper.organisationalRiskCategory.GetByAll())
                {
                    dbHelper.organisationalRiskCategory.Delete(organizationalRiskCategoryId);
                }

                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Initial update", "", 1, 1);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "inactive check", "", 2, 2);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Low", "", 3, 3);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Moderately low", "", 4, 4);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Moderately High", "", 6, 6);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Same Value", "", 7, 7);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "High updated", "", 8, 14);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Extreme", "", 15, 16);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Very extreme", "", 18, 21);
                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Final", "", 22, 25);

                #endregion

                #region Organizational Risk Type

                var riskTypeExists = dbHelper.organisationalRiskType.GetByName("Standard").Any();
                if (!riskTypeExists)
                    dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "Standard", new DateTime(2022, 1, 1));
                _riskTypeId = dbHelper.organisationalRiskType.GetByName("Standard").First();

                #endregion

                #region Organizational Risk

                foreach (var organisationalRiskId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Testing CDV6-14460"))
                    dbHelper.organisationalRisk.DeleteOrganisationalRisk(organisationalRiskId);

                _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-14460", null, 1, 1);

                _organisationalRisk2ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date.AddDays(-5), null, 1, 1, "Testing CDV6-14460", null, 1, 3);

                _organisationalRisk3ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date.AddDays(-10), null, 1, 1, "Testing CDV6-14460", null, 1, 1);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-15461

        [TestProperty("JiraIssueID", "ACC-3171")]
        [Description("Verifying the Risk Score and Risk Category based on Consequences and Likelihood for Initial and Residual Risk Score")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRisk_TestRiskScoreAndCategory_UITestMethod01()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad();

            #endregion

            #region Step 3

            organisationalRiskManagementPage
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .SelectCorporateRiskRegisterValue("No")
                .ClickRiskTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Standard").TapSearchButton().SelectResultElement(_riskTypeId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskConsequenceValue("1")
                .InsertRiskLikelihoodValue("1")

                .ValidateInitialRiskScoreValue("1")
                .ValidateInitialRiskCategoryValue("Initial update");

            #endregion

            #region Step 4

            organisationalRiskManagementRecordPage
                .InsertRiskConsequenceValue("6")
                .InsertRiskLikelihoodValue("7")
                .ClickSaveButton()

                .ValidateConsequenceErrorLabelVisibility(true)
                .ValidateConsequenceErrorLabelText("Please enter a value between 1 and 5.")
                .ValidateLikelihoodErrorLabelVisibility(true)
                .ValidateLikelihoodErrorLabelText("Please enter a value between 1 and 5.")

                ;

            #endregion

            #region Step 6

            organisationalRiskManagementRecordPage
                .InsertRiskConsequenceValue("3")
                .InsertRiskLikelihoodValue("2")

                .ValidateInitialRiskScoreValue("6")
                .ValidateInitialRiskCategoryValue("Moderately High");
            ;

            #endregion

            #region Step 7

            organisationalRiskManagementRecordPage
                .InsertRiskConsequenceValue("5")
                .InsertRiskLikelihoodValue("1")

                .ValidateInitialRiskScoreValue("5")
                .ValidateInitialRiskCategoryValue("Not Assigned"); ;

            #endregion

            #region Step 8

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .OpenRiskRecord(_organisationalRisk1ID.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()

                .InsertRiskConsequenceValue("1")
                .InsertRiskLikelihoodValue("4")

                .InsertResidualConsequenceValue("2")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .OpenRiskRecord(_organisationalRisk1ID.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()

                .ValidateInitialRiskScoreValue("4")
                .ValidateInitialRiskCategoryValue("Moderately low")

                .ValidateResidualRiskScoreValue("6")
                .ValidateResidualRiskCategoryValue("Moderately High");

            #endregion

            #region Step 9

            var organisationalRiskCategoryId = dbHelper.organisationalRiskCategory.GetByName("Moderately low").FirstOrDefault();
            dbHelper.organisationalRiskCategory.UpdateName(organisationalRiskCategoryId, "Moderately low Updated");

            #endregion

            #region Step 10

            organisationalRiskCategoryId = dbHelper.organisationalRiskCategory.GetByName("Moderately High").FirstOrDefault();
            dbHelper.organisationalRiskCategory.UpdateInactive(organisationalRiskCategoryId, true);

            #endregion

            #region Step 11

            organisationalRiskManagementRecordPage
               .ClickBackButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .OpenRiskRecord(_organisationalRisk1ID.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription("Testing CDV6-14460 Updated....")
                .ClickSaveAndCloseButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .OpenRiskRecord(_organisationalRisk1ID.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateInitialRiskScoreValue("4")
                .ValidateInitialRiskCategoryValue("Moderately low")

                .ValidateResidualRiskScoreValue("6")
                .ValidateResidualRiskCategoryValue("Moderately High");

            #endregion

            #region Step 12 & 13

            organisationalRiskManagementRecordPage

                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")

                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateInitialRiskScoreValue("4")
                .ValidateInitialRiskCategoryValue("Moderately low Updated")

                .ValidateResidualRiskScoreValue("6")
                .ValidateResidualRiskCategoryValue("Moderately High")


                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("2")

                .ClickSaveButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateInitialRiskScoreValue("4")
                .ValidateInitialRiskCategoryValue("Moderately low Updated")

                .ValidateResidualRiskScoreValue("6")
                .ValidateResidualRiskCategoryValue("Not Assigned");

            #endregion

            #region Step 14

            organisationalRiskManagementRecordPage
                .ClickBackButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .SelectAvailableViewByText("Inactive Risk")
                .OpenRiskRecord(_organisationalRisk2ID.ToString());

            organisationalRiskManagementRecordPage
                .WaitForDisabledRiskRecordPageToLoad()
                .ValidateInitialConsequenceEnabled(false)
                .ValidateInitialLikelihoodEnabled(false)
                .ValidateResidualConsequenceEnabled(false)
                .ValidateResidualLikelihoodEnabled(false);

            #endregion

            #region Step 15

            organisationalRiskManagementRecordPage
                .ClickBackButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .SelectAvailableViewByText("Active Risk")
                .WaitForOrganisationalRisksPageToLoad()

                .TypeSearchQuery("Moderately low Updated")
                .WaitForOrganisationalRisksPageToLoad()
                .ValidateRecordPresent(_organisationalRisk1ID.ToString())
                .ValidateRecordNotPresent(_organisationalRisk3ID.ToString())

                .TypeSearchQuery("Initial update")
                .WaitForOrganisationalRisksPageToLoad()
                .ClickTableHeaderCell(2)
                .WaitForOrganisationalRisksPageToLoad()
                .ClickTableHeaderCell(2)
                .WaitForOrganisationalRisksPageToLoad()
                .ValidateRecordNotPresent(_organisationalRisk1ID.ToString())
                .ValidateRecordPresent(_organisationalRisk3ID.ToString());

            #endregion
        }

        #endregion



    }
}

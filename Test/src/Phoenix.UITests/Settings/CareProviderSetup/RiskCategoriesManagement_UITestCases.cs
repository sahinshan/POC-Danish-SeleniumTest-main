using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.CareProviderSetup
{
    /// <summary>
    /// This class contains Automated UI test scripts for Risk Categories Management Application Page
    /// </summary>
    [TestClass]
    public class RiskCategoriesManagement_UITestCases : FunctionalTest
    {
        #region Properties
        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private Guid _environmentId;
        private Guid adminUserId;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);

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

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_15460_User").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_15460_User", "Testing", "CDV6_15460_User", "Testing CDV6_15460_User", "Summer2013@",
                        "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null,
                        _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_15460_User")[0];


                #endregion

                #region Risk Category

                foreach (var organisationalRiskCategoryId in dbHelper.organisationalRiskCategory.GetByAll())
                {
                    dbHelper.organisationalRiskCategory.Delete(organisationalRiskCategoryId);
                }

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-15460

        [TestProperty("JiraIssueID", "ACC-3581")]
        [Description("Verify that the system displays and alert when outside of range 1 - 25 values are entered for Value From and Value To fields." +
                     "Validate that the system saves the record when Value From and Value To field values are same.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risk Categories")]
        public void RiskCategoriesManagement_UITestMethod_001()
        {

            var lowRiskId = dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Low", 1, 3, _careProviders_TeamId);
            var moderateRiskId = dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Moderate", 4, 7, _careProviders_TeamId);
            var highRiskId = dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("High", 8, 14, _careProviders_TeamId);
            var extremeRiskId = dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Extreme", 15, 25, _careProviders_TeamId);


            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRiskCategoriesManagementPage();

            /*
             * Verify that the user is able to edit the Categories.
             * Enter values outside 1-25 and check that the system gives an alert.
             */
            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("Low")
                .OpenRiskCategoryRecord(lowRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage.
                WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromField("77")
                .ValidateValueFromFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueFromField("0")
                .ValidateValueFromFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueToField("0")
                .ValidateValueToFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueToField("26")
                .ValidateValueToFieldErrorLabelText("Please enter a value between 1 and 25.")
                .ClickBackButton()
                .AcceptBrowserAlert();


            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("Extreme")
                .OpenRiskCategoryRecord(extremeRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromField("0")
                .ValidateValueFromFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueFromField("26")
                .ValidateValueFromFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueToField("26")
                .ValidateValueToFieldErrorLabelText("Please enter a value between 1 and 25.")
                .InsertValueToField("0")
                .ValidateValueToFieldErrorLabelText("Please enter a value between 1 and 25.")
                .ClickBackButton()
                .AcceptBrowserAlert();

            /*
             * Verify that the user can save the Categories.
             * Enter values withing range 1-25 and check that the system accepts them on Save.
             */
            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("Low")
                .OpenRiskCategoryRecord(lowRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromField("1")
                .InsertValueToField("1")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .ValidateValueFromField("1")
                .ValidateValueToField("1")
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("High")
                .OpenRiskCategoryRecord(highRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromField("8")
                .InsertValueToField("8")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .ValidateValueFromField("8")
                .ValidateValueToField("8")
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("Extreme")
                .OpenRiskCategoryRecord(extremeRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromField("25")
                .InsertValueToField("25")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .ValidateValueFromField("25")
                .ValidateValueToField("25")
                .ClickBackButton();

        }

        [TestProperty("JiraIssueID", "ACC-3582")]
        [Description("Login to CD -> Settings -> Care Provider Setup -> Create a new category overlapping  existing values and Validate that proper alert popup gets displayed." +
                     "Create categories which is not overlapping any existing ones and validate that it is getting saved." +
                     "Edit the name of a category to an existing name and validate that an alert message is displayed that the name is not unique.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risk Categories")]
        public void RiskCategoriesManagement_UITestMethod_002()
        {
            var lowRiskId = dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Low", 1, 3, _careProviders_TeamId);
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRiskCategoriesManagementPage();

            /*
             * Verify that Category Ranges Cannot Overlap alert message is displayed when
             * a new risk category is created with overlapping existing values
             */
            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertRiskCategoryName("Low Mid")
                .InsertValueFromAndValueToField("3", "3")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("Category ranges cannot overlap")
                 .TapCloseButton();

            organisationalRiskCategoriesRecordPage
                .ClickBackButton()
                .AcceptBrowserAlert();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertRiskCategoryName("Low Mid")
                .InsertValueFromAndValueToField("5", "5")
                .ClickSaveAndCloseButton();

            /*
             * Verify that a new risk category is created when the categories values do not overlap.
             */

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ValidateRiskCategoryNameIsAvailable("Low Mid")
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertRiskCategoryName("Mid High")
                .InsertValueFromAndValueToField("17", "17")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ValidateRiskCategoryNameIsAvailable("Mid High")
                .SearchRiskCategoryRecord("Low")
                .OpenRiskCategoryRecord(lowRiskId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            /*
             * Verify that the system displays a an alert message that Category name must be unique when
             * the name of the category is changed to same as another existing category.                        
             */
            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertRiskCategoryName("Low Mid")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("All active categories must have unique name")
                 .TapCloseButton();
        }

        [TestProperty("JiraIssueID", "ACC-3583")]
        [Description("Login to CD -> Settings -> Care Provider Setup -> Create two categories (Ext1: 5,5 and Ext2: 17, 17) that are not overlapping." +
                      "Make the category 1 inactive and validate it is getting saved." +
                      "Edit the From and To Values of category 2 to that of inactive category 1 > Validate that it is saved. " +
                      "Edit the Name of category 2 as that of inactive category 1 > Validate that it is saved." +
                      "Make the category 2 inactive and validate that it is saved.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risk Categories")]
        public void RiskCategoriesManagement_UITestMethod_003()
        {

            var newCategory1 =
                dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Extreme 1", 5, 5, _careProviders_TeamId);


            var newCategory2 =
                dbHelper.organisationalRiskCategory.CreateOrganisationalRiskCategory("Extreme 2", 17, 17, _careProviders_TeamId);



            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRiskCategoriesManagementPage();

            /*
             * Verify that two new Risk Categories are created if the From and To range values are not Overlapping with existing values.             
             */
            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ValidateRecord(newCategory1.ToString(), "Extreme 1", "5", "5")
                .ValidateRecord(newCategory2.ToString(), "Extreme 2", "17", "17");

            organisationalRiskCategoriesPage
                .SearchRiskCategoryRecord("Extreme 1")
                .OpenRiskCategoryRecord(newCategory1.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            /*
             * Verify that the Risk category 1 is edited and is set to Inactive.
             * It is available under Inactive Records
             */
            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .SelectInactiveRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .WaitForInactiveRiskCategoryRecordPageToLoad()
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SelectAvailableViewByText("Inactive Records")
                .ValidateRiskCategoryNameIsAvailable("Extreme 1")
                .ValidateRecord(newCategory1.ToString(), "Extreme 1", "5", "5")
                .SelectAvailableViewByText("Active Records")
                .SearchRiskCategoryRecord("Extreme 2")
                .OpenRiskCategoryRecord(newCategory2.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            /*
             * Verify that the From and To Values of Risk category 2 is edited as that of Risk Category 1
             * and it is saved because Risk Category 1 is Inactive and values are not overlapping.
             * 
             */
            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertValueFromAndValueToField("5", "5")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .ClickBackButton();

            Thread.Sleep(1500);
            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ValidateRecord(newCategory2.ToString(), "Extreme 2", "5", "5")
                .SearchRiskCategoryRecord("Extreme 2")
                .OpenRiskCategoryRecord(newCategory2.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            /*
             * Verify that the Risk category 2 Name is edited to that of Inactive Risk Category 1 and it is saved as it is unique.
             */
            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .InsertRiskCategoryName("Extreme 1")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .ValidateRiskCategoryName("Extreme 1")
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SearchRiskCategoryRecord("Extreme 1")
                .OpenRiskCategoryRecord(newCategory2.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("organisationalriskcategory")
                .ClickOnExpandIcon();

            /*
             * Verify that the Risk category 2 is edited and is set to Inactive. It is saved.
             * It is available under Inactive Records
             */
            organisationalRiskCategoriesRecordPage
                .WaitForRiskCategoryRecordPageToLoad()
                .SelectInactiveRadioButton()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessageTextContains("The following values are not configured yet. Do you want to continue?")
                .TapOKButton();

            organisationalRiskCategoriesRecordPage
                .WaitForInactiveRiskCategoryRecordPageToLoad()
                .ClickBackButton();

            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .SelectAvailableViewByText("Inactive Records")
                .ValidateRecord(newCategory2.ToString(), "Extreme 1", "5", "5");

        }



        #endregion
    }
}

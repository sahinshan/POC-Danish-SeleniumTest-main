
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    [TestClass]
    public class SmokeTests_Level1_UITestCases : FunctionalTest
    {
        private string environmentName;
        private Guid userid;
        private string username;
        private string password;
        private string DataEncoded;

        [TestInitialize()]
        public void SmokeTests_SetupTest()
        {
            try
            {

                #region Environment

                environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

                #endregion

                #region Authentication Data

                username = ConfigurationManager.AppSettings["Username"];
                password = ConfigurationManager.AppSettings["Password"];
                DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                if (DataEncoded.Equals("true"))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(username);
                    username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    base64EncodedBytes = System.Convert.FromBase64String(password);
                    password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                }

                userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-13736

        [Description("Check for the correct version number on login screen")]
        [TestMethod]
        [TestCategory("SmokeTestLevel1"), TestCategory("AllProductTypes")]
        public void SmokeTest_UITestMethod01()
        {
            var versionNumber = ConfigurationManager.AppSettings["CareDirectorVersionNumber"];

            loginPage
                .GoToLoginPage()
                .ValidateVersionLabelText("Version: " + versionNumber);
        }

        [Description("Check Cache Monitor loading data")]
        [TestMethod]
        [TestCategory("SmokeTestLevel1"), TestCategory("AllProductTypes")]
        public void SmokeTest_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(username, password, environmentName)
                .WaitFormHomePageToLoad(true, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickCacheMonitorLink();

            cacheMonitorPage
                .WaitForCacheMonitorPageToLoad()
                .ValidateCacheElementVisible("systemuser");
        }

        [Description("Perform Advance Find and export to Excel")]
        [TestMethod]
        [TestCategory("SmokeTestLevel1"), TestCategory("AllProductTypes")]
        public void SmokeTest_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(username, password, environmentName)
                .WaitFormHomePageToLoad(true, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .SelectFilter("1", "User Name")
                .SelectOperator("1", "Like")
                .InsertRuleValueText("1", username)
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(userid.ToString())
                .SelectSearchResultRecord(userid.ToString())
                .ClickExportToExcelButton_ResultsPage();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Excel")
                .ClickExportButton();

            System.Threading.Thread.Sleep(7000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "SystemUsers.xlsx");
            Assert.IsTrue(fileExists);

        }

        [Description("Validate the Default Shipped Risk Category: Low: 1 - 3 && Moderate - 4 - 7 && High - 8 - 14 && Extreme - 15 - 25")]
        [TestMethod]
        [TestCategory("SmokeTestLevel1"), TestCategory("UKCommunityAndMentalHealthNHS")]
        public void SmokeTest_UITestMethod04()
        {
            Guid _lowRiskCategoryId;
            Guid _moderateRiskCategoryId;
            Guid _highRiskCategoryId;
            Guid _extremeRiskCategoryId;

            try
            {
                _lowRiskCategoryId = dbHelper.organisationalRiskCategory.GetOrganisationalRiskCategoryIdByCategoryName("Low")[0];
                _moderateRiskCategoryId = dbHelper.organisationalRiskCategory.GetOrganisationalRiskCategoryIdByCategoryName("Moderate")[0];
                _highRiskCategoryId = dbHelper.organisationalRiskCategory.GetOrganisationalRiskCategoryIdByCategoryName("High")[0];
                _extremeRiskCategoryId = dbHelper.organisationalRiskCategory.GetOrganisationalRiskCategoryIdByCategoryName("Extreme")[0];
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to find the requested business object"))
                    return; //We are trying to run the test in a environment that do not contain the Clinical Risk Module active
                else
                    throw ex;
            }

            loginPage
               .GoToLoginPage()
               .Login(username, password, environmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRiskCategoriesManagementPage();


            organisationalRiskCategoriesPage
                .WaitForRiskCategoriesManagementPageToLoad()
                .ValidateRiskCategoryNameIsAvailable("Low")
                .ValidateRiskCategoryNameIsAvailable("Moderate")
                .ValidateRiskCategoryNameIsAvailable("High")
                .ValidateRiskCategoryNameIsAvailable("Extreme")
                .ValidateRecord(_lowRiskCategoryId.ToString(), "Low", "1", "3")
                .ValidateRecord(_moderateRiskCategoryId.ToString(), "Moderate", "4", "7")
                .ValidateRecord(_highRiskCategoryId.ToString(), "High", "8", "14")
                .ValidateRecord(_extremeRiskCategoryId.ToString(), "Extreme", "15", "25");
        }

        #endregion

    }
}

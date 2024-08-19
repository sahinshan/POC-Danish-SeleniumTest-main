using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;


namespace Phoenix.UITests.AdvancedSearch.AdvancedSearch
{
    /// <summary>
    ///  
    /// </summary>
    [TestClass]
    public class AdvancedSearch_UITestCases2 : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Advanced Search BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Advanced Search Team", null, _businessUnitId, "907678", "AdvancedSearchTeam@careworkstempmail.com", "Advanced Search Team", "020 123456");

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region System User AllActivitiesUser1

                commonMethodsDB.CreateSystemUserRecord("AdvancedSearchUser1", "AdvancedSearch", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-23150

        [TestProperty("JiraIssueID", "CDV6-23315")]
        [Description("Test the scenarios for the Last Month operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod001()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Last Month")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23316")]
        [Description("Test the scenarios for the This Month operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod002()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "This Month")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23317")]
        [Description("Test the scenarios for the Next Month operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod003()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Next Month")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordPresent(task3Id.ToString());
        }




        [TestProperty("JiraIssueID", "CDV6-23318")]
        [Description("Test the scenarios for the Last Year operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod004()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Last Year")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23319")]
        [Description("Test the scenarios for the This Year operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod005()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "This Year")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23320")]
        [Description("Test the scenarios for the Next Year operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod006()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddYears(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Next Year")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordPresent(task3Id.ToString());
        }




        [TestProperty("JiraIssueID", "CDV6-23321")]
        [Description("Test the scenarios for the Last Week operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod007()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Last Week")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23322")]
        [Description("Test the scenarios for the This Week operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod008()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "This Week")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23323")]
        [Description("Test the scenarios for the Next Week operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod009()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Next Week")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordPresent(task3Id.ToString());
        }




        [TestProperty("JiraIssueID", "CDV6-23324")]
        [Description("Test the scenarios for the Equals Month Number operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod010()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(-1);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Equals Month Number")
                .InsertRuleValueText("2", DateTime.Now.Month.ToString())
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }



        [TestProperty("JiraIssueID", "CDV6-23325")]
        [Description("Test the scenarios for the Last X Hours operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod011()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(-4);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(4);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Last X Hours")
                .InsertRuleValueText("2", "5")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23326")]
        [Description("Test the scenarios for the Next X Hours operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod012()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(-4);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(4);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Next X Hours")
                .InsertRuleValueText("2", "5")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordPresent(task3Id.ToString());
        }



        [TestProperty("JiraIssueID", "CDV6-23327")]
        [Description("Test the scenarios for the Last X Minutes operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod013()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMinutes(-4);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMinutes(4);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Last X Minutes")
                .InsertRuleValueText("2", "5")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(task1Id.ToString())
                .ValidateSearchResultRecordPresent(task2Id.ToString())
                .ValidateSearchResultRecordNotPresent(task3Id.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-23328")]
        [Description("Test the scenarios for the Next X Minutes operator")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ValidateNewConditionOperators_UITestMethod014()
        {
            #region Person

            var _firstName = "Matt";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Task

            var taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMinutes(-4);
            var task1Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 01", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var task2Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 02", "", _teamId, taskDate);

            taskDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMinutes(4);
            var task3Id = dbHelper.task.CreatePersonTask(_personID, _personFullName, "Task 03", "", _teamId, taskDate);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AdvancedSearchUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Tasks")

                .SelectFilter("1", "Regarding")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookFor("People")
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Due")
                .SelectOperator("2", "Next X Minutes")
                .InsertRuleValueText("2", "5")
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(task1Id.ToString())
                .ValidateSearchResultRecordNotPresent(task2Id.ToString())
                .ValidateSearchResultRecordPresent(task3Id.ToString());
        }

        #endregion

    }
}

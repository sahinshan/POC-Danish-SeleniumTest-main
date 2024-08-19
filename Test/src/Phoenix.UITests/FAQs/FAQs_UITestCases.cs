using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.FAQs

{
    /// <summary>
    /// This class contains Automated UI test scripts for Summary Dashboards
    /// </summary>
    [TestClass]
    public class FAQs_UITestCases : FunctionalTest
    {

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;

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

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User AllActivitiesUser1

                commonMethodsDB.CreateSystemUserRecord("FaqsUser1", "Faqs", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-5056


        #region Viewing records

        [TestProperty("JiraIssueID", "CDV6-6193")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Validate that the FAQs page is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-6194")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Insert a search query in the search text box - " +
            "tap the search button - Validate that the matching record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod02()
        {
            Guid faqid = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC21")
                .ValidateFAQRecordPresent(faqid.ToString())
                .ValidateFAQNameCell(faqid.ToString(), "Security - Question SEC21")
                .ValidateFAQCategoryCell(faqid.ToString(), "Security")
                .ValidateFAQStatusCell(faqid.ToString(), "Published")
                .ValidateFAQUpVotesCell(faqid.ToString(), "50001")
                .ValidateFAQDownVotesCell(faqid.ToString(), "3")
                .ValidateFAQCreatedByCell(faqid.ToString(), "CW Forms Test User 1")
                .ValidateFAQCreatedOnCell(faqid.ToString(), "24/11/2020 13:49:17");
        }

        [TestProperty("JiraIssueID", "CDV6-6195")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Insert a search query in the search text box (query should match several records) - " +
            "tap the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod03()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21
            Guid faqid2 = new Guid("a6dcbfd0-edc2-440d-80ca-7e988ad97674"); //Security - Question SEC20
            Guid faqid3 = new Guid("cc00d316-a608-eb11-a2cd-005056926fe4"); //Security - Question SEC2


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC2")

                .ValidateFAQNameCell(faqid1.ToString(), "Security - Question SEC21")
                .ValidateFAQCategoryCell(faqid1.ToString(), "Security")
                .ValidateFAQStatusCell(faqid1.ToString(), "Published")
                .ValidateFAQUpVotesCell(faqid1.ToString(), "50001")
                .ValidateFAQDownVotesCell(faqid1.ToString(), "3")
                .ValidateFAQCreatedByCell(faqid1.ToString(), "CW Forms Test User 1")
                .ValidateFAQCreatedOnCell(faqid1.ToString(), "24/11/2020 13:49:17")

                .ValidateFAQNameCell(faqid2.ToString(), "Security - Question SEC20")
                .ValidateFAQCategoryCell(faqid2.ToString(), "Security")
                .ValidateFAQStatusCell(faqid2.ToString(), "Published")
                .ValidateFAQUpVotesCell(faqid2.ToString(), "49000")
                .ValidateFAQDownVotesCell(faqid2.ToString(), "1")
                .ValidateFAQCreatedByCell(faqid2.ToString(), "CW Forms Test User 1")
                .ValidateFAQCreatedOnCell(faqid2.ToString(), "24/11/2020 13:49:17")

                .ValidateFAQNameCell(faqid3.ToString(), "Security - Question SEC2")
                .ValidateFAQCategoryCell(faqid3.ToString(), "Security")
                .ValidateFAQStatusCell(faqid3.ToString(), "Published")
                .ValidateFAQUpVotesCell(faqid3.ToString(), "31000")
                .ValidateFAQDownVotesCell(faqid3.ToString(), "0")
                .ValidateFAQCreatedByCell(faqid3.ToString(), "CW Forms Test User 1")
                .ValidateFAQCreatedOnCell(faqid3.ToString(), "24/11/2020 13:49:17");
        }

        [TestProperty("JiraIssueID", "CDV6-6196")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - " +
            "Validate that the record page is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod04()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC2")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-6197")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - " +
            "Validate that all fields have the values correctly set")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod05()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC2")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()

                .ValidateNameValue("Security - Question SEC21")
                .ValidateLanguageValue("English (UK)")
                .ValidateStatusValue("Published")
                .ValidateKeywordsValue("Security, service, portal, question, sec21")
                .ValidateCategoryValue("Security")
                .ValidateResponsibleTeamValue("CareDirector QA")
                .ValidateUpvotesValue("50001")
                .ValidateDownVotesValue("3");
        }

        [TestProperty("JiraIssueID", "CDV6-6198")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - " +
            "Tap on the Applications tab - Validate that the user is redirected to the application components page")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod06()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC21")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-6199")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record (connected to an application component) - " +
            "Tap on the Applications tab - Validate that the application component record is present")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod07()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21
            Guid applicationComponent1 = new Guid("4f50ebfe-6d01-4519-8424-3d0caa783beb"); //CareDirector
            Guid applicationComponent2 = new Guid("3ae52bf0-032f-eb11-a2d0-005056926fe4"); //CareDirector App

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC21")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()

                .ValidateApplicationCell(applicationComponent1.ToString(), "CareDirector")
                .ValidateComponentCell(applicationComponent1.ToString(), "Security - Question SEC21")
                .ValidateCreatedByCell(applicationComponent1.ToString(), "CW Forms Test User 1")
                .ValidateCreatedOnCell(applicationComponent1.ToString(), "25/11/2020 09:32:54")
                .ValidateModifiedByCell(applicationComponent1.ToString(), "CW Forms Test User 1")
                .ValidateModifiedOnCell(applicationComponent1.ToString(), "25/11/2020 09:32:54")
                .ValidateValidForExportCell(applicationComponent1.ToString(), "No")


                .ValidateApplicationCell(applicationComponent2.ToString(), "CareDirector App")
                .ValidateComponentCell(applicationComponent2.ToString(), "Security - Question SEC21")
                .ValidateCreatedByCell(applicationComponent2.ToString(), "CW Forms Test User 1")
                .ValidateCreatedOnCell(applicationComponent2.ToString(), "25/11/2020 09:52:36")
                .ValidateModifiedByCell(applicationComponent2.ToString(), "CW Forms Test User 1")
                .ValidateModifiedOnCell(applicationComponent2.ToString(), "25/11/2020 09:53:57")
                .ValidateValidForExportCell(applicationComponent2.ToString(), "No")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6200")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record (connected to an application component) - " +
            "Tap on the Applications tab - Open the application component record - validate that the application component page is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod08()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21
            Guid applicationComponent1 = new Guid("4f50ebfe-6d01-4519-8424-3d0caa783beb"); //CareDirector

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC21")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()
                .OpenApplicationComponentRecord(applicationComponent1.ToString());

            faqApplicationComponentRecordPage
                .WaitForFAQApplicationComponentRecordPageToLoad()

                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6201")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record (connected to an application component) - " +
            "Tap on the Applications tab - Open the application component record - validate that the application component page field values")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod09()
        {
            Guid faqid1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"); //Security - Question SEC21
            Guid applicationComponent1 = new Guid("4f50ebfe-6d01-4519-8424-3d0caa783beb"); //CareDirector

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()

                .SearchFAQRecord("Security - Question SEC21")
                .OpenFAQRecord(faqid1.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()
                .OpenApplicationComponentRecord(applicationComponent1.ToString());

            faqApplicationComponentRecordPage
                .WaitForFAQApplicationComponentRecordPageToLoad()
                .ValidateApplicationValue("CareDirector")
                .ValidateComponentValue("Security - Question SEC21")
                .ValidateValidForExportNoRadioBuuttonChecked();
        }


        #endregion

        #region Editing Record

        [TestProperty("JiraIssueID", "CDV6-6202")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - " +
            "Update all editable fields in the FAQ record page - Tap on the save button - Validate that the record was correctly saved")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod10()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentID in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }

            Guid ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            Guid languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];
            Guid faqID = dbHelper.faq.CreateFAQ(ownerID, categoryID, languageID, "Security - Question SEC22", "security, service, portal, question, sec22", "Contents ....", 2, 1, "Security-Question-SEC22");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC22")
                .OpenFAQRecord(faqID.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .InsertName("Security - Question SEC22 Updated")
                .TapLanguageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Spanish");

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .SelectStatus("Draft")
                .InsertKeywords("security, service, portal, question, sec22, updated")
                .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Finance Transactions");

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapSaveAndCloseButton();

            faqsPage
                .WaitForFAQsPageToLoad();


            Guid newcategoryID = dbHelper.faqCategory.GetByName("Finance Transactions")[0];
            Guid newlanguageID = dbHelper.productLanguage.GetProductLanguageIdByName("Spanish")[0];
            var fields = dbHelper.faq.GetFAQByID(faqID, "ownerid", "faqcategoryid", "languageid", "title", "keywords", "contents", "numberofupvotes", "numberofdownvotes", "validforexport", "statusid", "inactive");

            Assert.AreEqual(ownerID, fields["ownerid"]);
            Assert.AreEqual(newcategoryID, fields["faqcategoryid"]);
            Assert.AreEqual(newlanguageID, fields["languageid"]);
            Assert.AreEqual("Security - Question SEC22 Updated", fields["title"]);
            Assert.AreEqual("security, service, portal, question, sec22, updated", fields["keywords"]);
            Assert.AreEqual("Contents ....", fields["contents"]);
            Assert.AreEqual(2, fields["numberofupvotes"]);
            Assert.AreEqual(1, fields["numberofdownvotes"]);
            Assert.AreEqual(false, fields["validforexport"]);
            Assert.AreEqual(1, fields["statusid"]);
            Assert.AreEqual(false, fields["inactive"]);
        }

        [TestProperty("JiraIssueID", "CDV6-6203")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - Navigate to the application components tab - Open an application component - " +
            "Update all editable fields in the application component page - Tap on the save button - Validate that the record was correctly saved")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod11()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentIDToDelete in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentIDToDelete);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }

            Guid ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            Guid languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];
            Guid faqID = dbHelper.faq.CreateFAQ(ownerID, categoryID, languageID, "Security - Question SEC22", "security, service, portal, question, sec22", "Contents ....", 2, 1, "Security-Question-SEC22");

            Guid applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            Guid applicationComponentID = dbHelper.applicationComponent.CreateApplicationComponent(applicationID, faqID, "faq", "Security - Question SEC22");


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC22")
                .OpenFAQRecord(faqID.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()
                .OpenApplicationComponentRecord(applicationComponentID.ToString());

            faqApplicationComponentRecordPage
                .WaitForFAQApplicationComponentRecordPageToLoad()
                .TapValidForExportYesRadioButton()
                .TapSaveAndCloseButton();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad();

            var fields = dbHelper.applicationComponent.GetApplicationComponentByID(applicationComponentID, "ApplicationId", "ComponentId", "componentidtablename", "componentidname", "validforexport");
            Assert.AreEqual(applicationID, fields["applicationid"]);
            Assert.AreEqual(faqID, fields["componentid"]);
            Assert.AreEqual("faq", fields["componentidtablename"]);
            Assert.AreEqual("Security - Question SEC22", fields["componentidname"]);
            Assert.AreEqual(true, fields["validforexport"]);

        }

        #endregion

        #region Create Record

        [TestProperty("JiraIssueID", "CDV6-6204")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Tap on the add new record button - Set data in all fields - Tap on the save button - " +
            "Validate that the record was correctly saved")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod12()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentID in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentID);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .TapNewRecordButton();

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .InsertName("Security - Question SEC22")
                .TapLanguageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("English (UK)");

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .SelectStatus("Published")
                .InsertKeywords("security, service, portal, question, sec22")
                .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Security");

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapSaveAndCloseButton();

            faqsPage
                .WaitForFAQsPageToLoad();



            var faqids = dbHelper.faq.GetByTitle("Security - Question SEC22");
            Assert.AreEqual(1, faqids.Count());

            var ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            var languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            var fields = dbHelper.faq.GetFAQByID(faqids[0], "ownerid", "faqcategoryid", "languageid", "title", "keywords", "contents", "numberofupvotes", "numberofdownvotes", "validforexport", "statusid", "inactive");

            Assert.AreEqual(ownerID, fields["ownerid"]);
            Assert.AreEqual(categoryID, fields["faqcategoryid"]);
            Assert.AreEqual(languageID, fields["languageid"]);
            Assert.AreEqual("Security - Question SEC22", fields["title"]);
            Assert.AreEqual("security, service, portal, question, sec22", fields["keywords"]);
            Assert.AreEqual(false, fields.ContainsKey("contents"));
            Assert.AreEqual(0, fields["numberofupvotes"]);
            Assert.AreEqual(0, fields["numberofdownvotes"]);
            Assert.AreEqual(false, fields["validforexport"]);
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(false, fields["inactive"]);
        }

        [TestProperty("JiraIssueID", "CDV6-6205")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - Navigate to the applications tap - Tap on the add button - Set data in all fields - Tap on the save button - " +
            "Validate that the record was correctly saved")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod13()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentIDToDelete in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentIDToDelete);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }

            Guid ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            Guid languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];
            Guid faqID = dbHelper.faq.CreateFAQ(ownerID, categoryID, languageID, "Security - Question SEC22", "security, service, portal, question, sec22", "Contents ....", 2, 1, "Security-Question-SEC22");



            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC22")
                .OpenFAQRecord(faqID.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()
                .TapNewRecordButton();

            faqApplicationComponentRecordPage
                .WaitForFAQApplicationComponentRecordPageToLoad()
                .SelectApplication("CareDirector")
                .TapSaveAndCloseButton();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad();



            var applicationComponents = dbHelper.applicationComponent.GetByComponentID(faqID);
            Assert.AreEqual(1, applicationComponents.Count);
            var fields = dbHelper.applicationComponent.GetApplicationComponentByID(applicationComponents[0], "ApplicationId", "ComponentId", "componentidtablename", "componentidname", "validforexport");

            Guid applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector

            Assert.AreEqual(applicationID, fields["applicationid"]);
            Assert.AreEqual(faqID, fields["componentid"]);
            Assert.AreEqual("faq", fields["componentidtablename"]);
            Assert.AreEqual("Security - Question SEC22", fields["componentidname"]);
            Assert.AreEqual(true, fields["validforexport"]);

        }

        #endregion

        #region Delete

        [TestProperty("JiraIssueID", "CDV6-6206")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - Tap on the delete button - confirm the delete operation - " +
            "Validate that the record was deleted")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod14()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentIDToDelete in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentIDToDelete);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }

            Guid ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            Guid languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];
            Guid faqID = dbHelper.faq.CreateFAQ(ownerID, categoryID, languageID, "Security - Question SEC22", "security, service, portal, question, sec22", "Contents ....", 2, 1, "Security-Question-SEC22");


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC22")
                .OpenFAQRecord(faqID.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            faqsPage
                .WaitForFAQsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var applicationComponents = dbHelper.faq.GetByKeywords("sec22");
            Assert.AreEqual(0, applicationComponents.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-6207")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Navigate to Settings; Views & Configuration; FAQs - Open a FAQ record - Navigate to the applications tab  - Open an application component record - Tap on the delete button - confirm the delete operation - " +
            "Validate that the record was deleted")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQs_Questions_UITestMethod15()
        {
            foreach (Guid faqIDToRemove in dbHelper.faq.GetByKeywords("sec22"))
            {
                foreach (var applicationComponentIDToDelete in dbHelper.applicationComponent.GetByComponentID(faqIDToRemove))
                    dbHelper.applicationComponent.DeleteApplicationComponent(applicationComponentIDToDelete);

                dbHelper.faq.DeleteFAQ(faqIDToRemove);
            }

            Guid ownerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid categoryID = dbHelper.faqCategory.GetByName("Security")[0];
            Guid languageID = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];
            Guid faqID = dbHelper.faq.CreateFAQ(ownerID, categoryID, languageID, "Security - Question SEC22", "security, service, portal, question, sec22", "Contents ....", 2, 1, "Security-Question-SEC22");

            Guid applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            Guid applicationComponentID = dbHelper.applicationComponent.CreateApplicationComponent(applicationID, faqID, "faq", "Security - Question SEC22");


            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsSection();

            faqsPage
                .WaitForFAQsPageToLoad()
                .SearchFAQRecord("Security - Question SEC22")
                .OpenFAQRecord(faqID.ToString());

            faqRecordPage
                .WaitForFAQRecordPageToLoad()
                .TapApplicationsTab();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad()
                .OpenApplicationComponentRecord(applicationComponentID.ToString());

            faqApplicationComponentRecordPage
                .WaitForFAQApplicationComponentRecordPageToLoad()
                .TapDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            faqApplicationComponentsPage
                .WaitForFAQApplicationComponentsPageToLoad();



            var records = dbHelper.applicationComponent.GetByComponentID(faqID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        #region FAQs Page

        [TestProperty("JiraIssueID", "CDV6-6208")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Validate that the FAQ page is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-6209")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Validate that the page displays the first 10 most up-voted records")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod02()
        {
            Guid applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            var allFAQs = dbHelper.faq.GetAllPublishedFAQsOrderedByUpvotes(applicationID);

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .ValidateRecordPresent(allFAQs[0].ToString())
                .ValidateRecordPresent(allFAQs[1].ToString())
                .ValidateRecordPresent(allFAQs[2].ToString())
                .ValidateRecordPresent(allFAQs[3].ToString())
                .ValidateRecordPresent(allFAQs[4].ToString())
                .ValidateRecordPresent(allFAQs[5].ToString())
                .ValidateRecordPresent(allFAQs[6].ToString())
                .ValidateRecordPresent(allFAQs[7].ToString())
                .ValidateRecordPresent(allFAQs[8].ToString())
                .ValidateRecordPresent(allFAQs[9].ToString())
                .ValidateRecordNotPresent(allFAQs[10].ToString())

                .ValidateLoadMoreRecordsButtonVisibility(true)

                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6210")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Tap on the Load More Records button - Validate that the page displays the first 20 most up-voted records")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod03()
        {
            Guid applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            var allFAQs = dbHelper.faq.GetAllPublishedFAQsOrderedByUpvotes(applicationID);

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .ValidateRecordPresent(allFAQs[0].ToString())
                .ValidateRecordPresent(allFAQs[1].ToString())
                .ValidateRecordPresent(allFAQs[2].ToString())
                .ValidateRecordPresent(allFAQs[3].ToString())
                .ValidateRecordPresent(allFAQs[4].ToString())
                .ValidateRecordPresent(allFAQs[5].ToString())
                .ValidateRecordPresent(allFAQs[6].ToString())
                .ValidateRecordPresent(allFAQs[7].ToString())
                .ValidateRecordPresent(allFAQs[8].ToString())
                .ValidateRecordPresent(allFAQs[9].ToString())
                .ValidateRecordNotPresent(allFAQs[10].ToString())

                .ClickLoadMoreRecordsButton()

                .ValidateRecordPresent(allFAQs[10].ToString())
                .ValidateRecordPresent(allFAQs[11].ToString())
                .ValidateRecordPresent(allFAQs[12].ToString())
                .ValidateRecordPresent(allFAQs[13].ToString())
                .ValidateRecordPresent(allFAQs[14].ToString())
                .ValidateRecordPresent(allFAQs[15].ToString())
                .ValidateRecordPresent(allFAQs[16].ToString())
                .ValidateRecordPresent(allFAQs[17].ToString())
                .ValidateRecordPresent(allFAQs[18].ToString())
                .ValidateRecordPresent(allFAQs[19].ToString())
                .ValidateRecordNotPresent(allFAQs[20].ToString())

                .ValidateLoadMoreRecordsButtonVisibility(true)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6211")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set 'Security' as the category - tap on the search button - Tap on the Load More Records button until all records are loaded - validate that the load more records buttons is removed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod04()
        {
            var applicationID = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            var faqCategoryID = dbHelper.faqCategory.GetByName("Security")[0];
            var allFAQs = dbHelper.faq.GetAllPublishedFAQsOrderedByUpvotes(applicationID, faqCategoryID);

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("sec").ClickResultElementByText("Security");

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickSearchButton()

                .ValidateRecordPresent(allFAQs[0].ToString())
                .ValidateRecordPresent(allFAQs[1].ToString())
                .ValidateRecordPresent(allFAQs[2].ToString())
                .ValidateRecordPresent(allFAQs[3].ToString())
                .ValidateRecordPresent(allFAQs[4].ToString())
                .ValidateRecordPresent(allFAQs[5].ToString())
                .ValidateRecordPresent(allFAQs[6].ToString())
                .ValidateRecordPresent(allFAQs[7].ToString())
                .ValidateRecordPresent(allFAQs[8].ToString())
                .ValidateRecordPresent(allFAQs[9].ToString())
                .ValidateRecordNotPresent(allFAQs[10].ToString())

                .ClickLoadMoreRecordsButton()

                .ValidateRecordPresent(allFAQs[10].ToString())
                .ValidateRecordPresent(allFAQs[11].ToString())
                .ValidateRecordPresent(allFAQs[12].ToString())
                .ValidateRecordPresent(allFAQs[13].ToString())
                .ValidateRecordPresent(allFAQs[14].ToString())
                .ValidateRecordPresent(allFAQs[15].ToString())
                .ValidateRecordPresent(allFAQs[16].ToString())
                .ValidateRecordPresent(allFAQs[17].ToString())
                .ValidateRecordPresent(allFAQs[18].ToString())
                .ValidateRecordPresent(allFAQs[19].ToString())
                .ValidateRecordNotPresent(allFAQs[20].ToString())

                .ClickLoadMoreRecordsButton()

                .ValidateRecordPresent(allFAQs[20].ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6212")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set 'Forms' as the category - tap on the search button - Validate that only the matching records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod05()
        {
            var faqQuestion1 = new Guid("d1cb52d8-a508-eb11-a2cd-005056926fe4");
            var faqQuestion2 = new Guid("8d7930b1-a508-eb11-a2cd-005056926fe4");
            var controlQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");
            var controlQuestion2 = new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("form").ClickResultElementByText("Forms");

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ValidateRecordPresent(faqQuestion2.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6213")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set 'Forms' as the category - Set a keyword that should only match one of the FAQ records - tap on the search button - Validate that only the matching record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod06()
        {
            var faqQuestion1 = new Guid("d1cb52d8-a508-eb11-a2cd-005056926fe4");
            var faqQuestion2 = new Guid("8d7930b1-a508-eb11-a2cd-005056926fe4");
            var controlQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");
            var controlQuestion2 = new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("form").ClickResultElementByText("Forms");

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("form2")
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ValidateRecordNotPresent(faqQuestion2.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6214")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set 'Forms' as the category - Set a keyword that should not match any FAQ records - tap on the search button - Validate that no record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod07()
        {
            var faqQuestion1 = new Guid("d1cb52d8-a508-eb11-a2cd-005056926fe4");
            var faqQuestion2 = new Guid("8d7930b1-a508-eb11-a2cd-005056926fe4");
            var controlQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");
            var controlQuestion2 = new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("form").ClickResultElementByText("Forms");

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("NotMatchingKeyword")
                .ClickSearchButton()

                .ValidateRecordNotPresent(faqQuestion1.ToString())
                .ValidateRecordNotPresent(faqQuestion2.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)

                .ValidateNoRecordsMessageVisibility(true)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6215")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set a keyword that should only match one of the FAQ records - tap on the search button - Validate that only the matching record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod08()
        {
            var faqQuestion1 = new Guid("d1cb52d8-a508-eb11-a2cd-005056926fe4");
            var faqQuestion2 = new Guid("8d7930b1-a508-eb11-a2cd-005056926fe4");
            var controlQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");
            var controlQuestion2 = new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("form2")
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ValidateRecordNotPresent(faqQuestion2.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6216")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set a keyword that should only match one record not linked to the CareDirector application - tap on the search button - Validate that no record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod09()
        {
            var faqQuestion1 = new Guid("23915a4b-a308-eb11-a2cd-005056926fe4"); //question not linked to any application
            var controlQuestion1 = new Guid("3c009d13-a308-eb11-a2cd-005056926fe4");
            var controlQuestion2 = new Guid("15752528-f0fc-ea11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("ft3")
                .ClickSearchButton()

                .ValidateRecordNotPresent(faqQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)

                .ValidateNoRecordsMessageVisibility(true);


        }

        [TestProperty("JiraIssueID", "CDV6-6217")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set a keyword that should only match one record that is not publish - tap on the search button - Validate that no record is displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod10()
        {
            var faqQuestion1 = new Guid("6f3f6c51-a608-eb11-a2cd-005056926fe4"); //draft question
            var controlQuestion1 = new Guid("70f3b589-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("fd2")
                .ClickSearchButton()

                .ValidateRecordNotPresent(faqQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)

                .ValidateNoRecordsMessageVisibility(true);


        }

        [TestProperty("JiraIssueID", "CDV6-6218")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Set a keyword that should match multiple FAQ records - tap on the search button - Validate that only the matching records are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod11()
        {
            var faqQuestion1 = new Guid("d1cb52d8-a508-eb11-a2cd-005056926fe4");
            var faqQuestion2 = new Guid("8d7930b1-a508-eb11-a2cd-005056926fe4");
            var controlQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");
            var controlQuestion2 = new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("forms, service, portal, question")
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ValidateRecordPresent(faqQuestion2.ToString())
                .ValidateRecordNotPresent(controlQuestion1.ToString())
                .ValidateRecordNotPresent(controlQuestion2.ToString())

                .ValidateLoadMoreRecordsButtonVisibility(false)
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-6219")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - search for a FAQ record - tap on the search button - Upvote the record - validate that the upvote is saved to the database")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod12()
        {
            var faqQuestion1 = new Guid("b9de52cd-7509-eb11-a2cd-005056926fe4");

            dbHelper.faq.UpdateFAQ(faqQuestion1, 0, 0);

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("form3")
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ClickYesButton(faqQuestion1.ToString())

                .ValidateThankYouMessageVisible(faqQuestion1.ToString())

                .ValidateYesButtonNotVisible(faqQuestion1.ToString())
                .ValidateNoButtonNotVisible(faqQuestion1.ToString())
                .ValidateWasThisHelpfulMessageNotVisible(faqQuestion1.ToString())
                ;


            var fields = dbHelper.faq.GetFAQByID(faqQuestion1, "numberofupvotes", "numberofdownvotes");
            Assert.AreEqual(1, fields["numberofupvotes"]);
            Assert.AreEqual(0, fields["numberofdownvotes"]);

        }

        [TestProperty("JiraIssueID", "CDV6-6220")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - search for a FAQ record - tap on the search button - Downvote the record - validate that the downvote is saved to the database")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod13()
        {
            var faqQuestion1 = new Guid("b9de52cd-7509-eb11-a2cd-005056926fe4");

            dbHelper.faq.UpdateFAQ(faqQuestion1, 0, 0);

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()
                .InsertText("form3")
                .ClickSearchButton()

                .ValidateRecordPresent(faqQuestion1.ToString())
                .ClickNoButton(faqQuestion1.ToString())

                .ValidateThankYouMessageVisible(faqQuestion1.ToString())

                .ValidateYesButtonNotVisible(faqQuestion1.ToString())
                .ValidateNoButtonNotVisible(faqQuestion1.ToString())
                .ValidateWasThisHelpfulMessageNotVisible(faqQuestion1.ToString())
                ;


            var fields = dbHelper.faq.GetFAQByID(faqQuestion1, "numberofupvotes", "numberofdownvotes");
            Assert.AreEqual(0, fields["numberofupvotes"]);
            Assert.AreEqual(1, fields["numberofdownvotes"]);

        }

        [TestProperty("JiraIssueID", "CDV6-6221")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - Validate the record title, category, content of the top level question")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod14()
        {
            var faqQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .ValidateRecordTitle(faqQuestion1.ToString(), "Security - Question SEC21")
                .ValidateRecordCategory(faqQuestion1.ToString(), "Category: Security")
                .ValidateWasThisHelpfulMessageVisible(faqQuestion1.ToString())
                .ValidateThankYouMessageNotVisible(faqQuestion1.ToString())
                .ValidateRecordContent(faqQuestion1.ToString(), "1", "SEC21 Line 1")
                .ValidateRecordContent(faqQuestion1.ToString(), "2", "SEC21 Line 2")
                .ValidateRecordContent(faqQuestion1.ToString(), "3", "SEC21 Line 3")
                .ValidateRecordContentVisibility(faqQuestion1.ToString(), "4", false)
                .ValidateRecordContentVisibility(faqQuestion1.ToString(), "5", false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-6222")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - tap on the expand button for the top level question - Validate the record title, category, content of the top level question")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod15()
        {
            var faqQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .TapRecordExpandButton(faqQuestion1.ToString())

                .ValidateRecordTitle(faqQuestion1.ToString(), "Security - Question SEC21")
                .ValidateRecordCategory(faqQuestion1.ToString(), "Category: Security")
                .ValidateWasThisHelpfulMessageVisible(faqQuestion1.ToString())
                .ValidateThankYouMessageNotVisible(faqQuestion1.ToString())
                .ValidateRecordContent(faqQuestion1.ToString(), "1", "SEC21 Line 1")
                .ValidateRecordContent(faqQuestion1.ToString(), "2", "SEC21 Line 2")
                .ValidateRecordContent(faqQuestion1.ToString(), "3", "SEC21 Line 3")
                .ValidateRecordContent(faqQuestion1.ToString(), "4", "SEC21 Line 4")
                .ValidateRecordContent(faqQuestion1.ToString(), "5", "SEC21 Line 5")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-6223")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - search for a question record - Validate the record title, category, content for the question")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod16()
        {
            var faqQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .InsertText("sec21")
                .ClickSearchButton()

                .ValidateRecordTitle(faqQuestion1.ToString(), "Security - Question SEC21")
                .ValidateRecordCategory(faqQuestion1.ToString(), "Category: Security")
                .ValidateWasThisHelpfulMessageVisible(faqQuestion1.ToString())
                .ValidateThankYouMessageNotVisible(faqQuestion1.ToString())
                .ValidateRecordContent(faqQuestion1.ToString(), "1", "SEC21 Line 1")
                .ValidateRecordContent(faqQuestion1.ToString(), "2", "SEC21 Line 2")
                .ValidateRecordContent(faqQuestion1.ToString(), "3", "SEC21 Line 3")
                .ValidateRecordContentVisibility(faqQuestion1.ToString(), "4", false)
                .ValidateRecordContentVisibility(faqQuestion1.ToString(), "5", false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-6224")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5056 - " +
            "Login in the web app - Open the FAQ search page - search for a question record - tap on the expand button for the top level question - Validate the record title, category, content of the top level question")]
        [TestMethod]
        [TestCategory("UITest")]
        public void FAQsPage_UITestMethod17()
        {
            var faqQuestion1 = new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b");

            loginPage
                .GoToLoginPage()
                .Login("FaqsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFAQsPage();

            faqSearchPage
                .WaitForFAQSearchPageToLoad()

                .InsertText("sec21")
                .ClickSearchButton()

                .TapRecordExpandButton(faqQuestion1.ToString())

                .ValidateRecordTitle(faqQuestion1.ToString(), "Security - Question SEC21")
                .ValidateRecordCategory(faqQuestion1.ToString(), "Category: Security")
                .ValidateWasThisHelpfulMessageVisible(faqQuestion1.ToString())
                .ValidateThankYouMessageNotVisible(faqQuestion1.ToString())
                .ValidateRecordContent(faqQuestion1.ToString(), "1", "SEC21 Line 1")
                .ValidateRecordContent(faqQuestion1.ToString(), "2", "SEC21 Line 2")
                .ValidateRecordContent(faqQuestion1.ToString(), "3", "SEC21 Line 3")
                .ValidateRecordContent(faqQuestion1.ToString(), "4", "SEC21 Line 4")
                .ValidateRecordContent(faqQuestion1.ToString(), "5", "SEC21 Line 5")
                ;

        }

        #endregion

        #endregion



        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.Portal
{
    /// <summary>
    /// This class contains Automated UI test scripts for Summary Dashboards
    /// </summary>
    [TestClass]
    public class WebSite_UITestCases : FunctionalTest
    {

        public Guid ClearExpiredPinRecordsScheduledJob { get { return new Guid("07332448-e61d-eb11-a2ce-0050569231cf"); } }
        public Guid ClearExpiredWebsiteUserPasswordResetScheduledJob { get { return new Guid("4daa77bc-8e1e-eb11-a2ce-0050569231cf"); } }
        public Guid ClearWebsiteUserPasswordHistoryJobScheduledJob { get { return new Guid("2831aa5b-1c20-eb11-a2ce-0050569231cf"); } }

        internal String portalWebsiteName = "Local Authority Citizen Portal";

        [TestInitialize]
        public void TestInitializationMethod()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-4628

        [TestProperty("JiraIssueID", "CDV6-5759")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Validate that the WebSites page is reached")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5760")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Search for Website records using the search query 'Automation - Web Site' - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod02()
        {
            var website1ID = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var website2ID = new Guid("37397330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02
            var website3ID = new Guid("95018057-6e18-eb11-a2cd-005056926fe4"); //Automation - Site 03

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()

                .ValidateRecordPresent(website1ID.ToString())
                .ValidateNameCellText(website1ID.ToString(), "Automation - Web Site 01")
                .ValidateApplicationCellText(website1ID.ToString(), "CareDirector")
                .ValidateUserRecordTypeCellText(website1ID.ToString(), "Person")
                .ValidateCreatedByCellText(website1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText(website1ID.ToString(), "27/10/2020 12:17:36")

                .ValidateRecordPresent(website2ID.ToString())
                .ValidateNameCellText(website2ID.ToString(), "Automation - Web Site 02")
                .ValidateApplicationCellText(website2ID.ToString(), "CareDirector App")
                .ValidateUserRecordTypeCellText(website2ID.ToString(), "Case")
                .ValidateCreatedByCellText(website2ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText(website2ID.ToString(), "27/10/2020 16:05:11")
                .ValidateRecordNotPresent(website3ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-5761")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record (all fields must have values) - " +
            "Validate that the user is redirected to the WebSite record page")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod03()
        {
            var website1ID = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(website1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 01")
                .ValidateHomePageFieldText("Page_1")
                .ValidateMemberHomePageFieldText("Page_1_1")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person");
        }

        [TestProperty("JiraIssueID", "CDV6-5762")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the WebSite record page")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod04()
        {
            var website1ID = new Guid("37397330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(website1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 02")
                .ValidateHomePageFieldText("")
                .ValidateMemberHomePageFieldText("")
                .ValidateApplicationFieldLinkText("CareDirector App")
                .ValidateUserRecordTypeFieldLinkText("Case");
        }

        [TestProperty("JiraIssueID", "CDV6-5763")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record - " +
            "Update all fields - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod05()
        {
            var websiteID = new Guid("af29ddb9-6f18-eb11-a2cd-005056926fe4"); //Automation - Web Site 04

            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var application2ID = dbHelper.application.GetByName("CareDirector App")[0]; //CareDirector App
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person
            var caseBusinessObjectID = new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"); //Case

            //update the website record
            dbHelper.website.UpdateWebsite(websiteID, application1ID, personBusinessObjectID, "Automation - Web Site 04", "", "", "", "", "");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 04 (Update)")
                .InsertDisplayName("Automation - Web Site 04 (Update)")
                .InsertVersion("1")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .InsertHomePage("Page_1")
                .ClickOnHomePageAutoCompleteOption("Page_1")
                .InsertMemberHomePage("Page_1_1")
                .ClickOnMemberHomePageAutoCompleteOption("Page_1_1")
                .InsertPasswordResetExpireInMinutes("5")
                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteWithSearchResultsToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 04 (Update)")
                .ValidateDisplayNameFieldText("Automation - Web Site 04 (Update)")
                .ValidateHomePageFieldText("Page_1")
                .ValidateMemberHomePageFieldText("Page_1_1")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person");

        }

        [TestProperty("JiraIssueID", "CDV6-5764")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod06()
        {

            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertVersion("1")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .InsertHomePage("Page_1")
                .InsertMemberHomePage("Page_1_1")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
               .WaitForWebSiteRecordPageToLoad()
               .InsertWebSiteURL("https://AutomationWebSite05.com/")
               .InsertMinimumPasswordLength("5")
               .InsertPasswordResetExpireInMinutes("5")
               .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
               .ClickSaveAndCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, websites.Count);

            webSitePage
                .WaitForWebSiteWithSearchResultsToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websites[0].ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 05")
                .ValidateDisplayNameFieldText("Automation - Web Site 05")
                .ValidateHomePageFieldText("Page_1")
                .ValidateMemberHomePageFieldText("Page_1_1")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person");


        }

        [TestProperty("JiraIssueID", "CDV6-5765")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a website record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod07()
        {

            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 06"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }

            //create the website to be deleted
            var websiteID = dbHelper.website.CreateWebsite("Automation - Web Site 06", application1ID, personBusinessObjectID, "", "", "", "");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSitePage
                .WaitForWebSiteWithSearchResultsToLoad();

            System.Threading.Thread.Sleep(2000);
            var websites = dbHelper.website.GetWebSiteByName("Automation - Web Site 06");
            Assert.AreEqual(0, websites.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5766")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record - " +
            "Set a value in the HomePage field that should match one or more website page records - validate that the auto-complete feature will display the matching results")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod08()
        {
            var websiteID = new Guid("af29ddb9-6f18-eb11-a2cd-005056926fe4"); //Automation - Web Site 04
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //update the website record
            dbHelper.website.UpdateWebsite(websiteID, application1ID, personBusinessObjectID, "Automation - Web Site 04", "", "", "", "", "");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertHomePage("Page_1")
                .ValidateHomePageAutoCompleteOptionVisibility("Page_1", true)
                .ValidateHomePageAutoCompleteOptionVisibility("Page_1_1", true)
                .ValidateHomePageAutoCompleteOptionVisibility("Page_2", false)
                ;



        }

        [TestProperty("JiraIssueID", "CDV6-5767")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record - " +
            "Set a value in the Member Home Page field that should match one or more website page records - validate that the auto-complete feature will display the matching results")]
        [TestMethod, TestCategory("UITest")]
        public void Website_UITestMethod09()
        {
            var websiteID = new Guid("af29ddb9-6f18-eb11-a2cd-005056926fe4"); //Automation - Web Site 04
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //update the website record
            dbHelper.website.UpdateWebsite(websiteID, application1ID, personBusinessObjectID, "Automation - Web Site 04", "", "", "", "", "");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertMemberHomePage("Page_1")
                .ValidateMemberHomePageAutoCompleteOptionVisibility("Page_1", true)
                .ValidateMemberHomePageAutoCompleteOptionVisibility("Page_1_1", true)
                .ValidateMemberHomePageAutoCompleteOptionVisibility("Page_2", false)
                ;



        }

        //[TestProperty("JiraIssueID", "CDV6-5768")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-4628 - " +
        //   "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record - " +
        //   "Set a value in the Footer field that should match one or more website page records - validate that the auto-complete feature will display the matching results")]
        //[TestMethod, TestCategory("UITest")]
        //public void Website_UITestMethod11()
        //{
        //    var websiteID = new Guid("af29ddb9-6f18-eb11-a2cd-005056926fe4"); //Automation - Web Site 04
        //    var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
        //    var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

        //    //update the website record
        //    dbHelper.website.UpdateWebsite(websiteID, application1ID, personBusinessObjectID, "Automation - Web Site 04", "", "", "", "", "");


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .InsertSearchQuery("Automation - Web Site")
        //        .ClickSearchButton()
        //        .ClickOnWebSiteRecord(websiteID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertFooter("Automation - Widget Footer")
        //        .ValidateFooterAutoCompleteOptionVisibility("Automation - Widget Footer 1.htm", true)
        //        .ValidateFooterAutoCompleteOptionVisibility("Automation - Widget Footer 2.htm", true)
        //        .ValidateFooterAutoCompleteOptionVisibility("Automation - Widget Header 1.htm", false)
        //        .ValidateFooterAutoCompleteOptionVisibility("Automation - Widget Header 2.htm", false)
        //        ;



        //}

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-4630

        [TestProperty("JiraIssueID", "CDV6-5769")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Pages area - " +
            "Validate that the Website Pages page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5770")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Pages area - Search for Website page record using the search query 'Page_1' - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var websitePage1ID = new Guid("2b8b1ef7-2e19-eb11-a2cd-005056926fe4"); //Page_1
            var websitePage1_1ID = new Guid("452f77ca-3019-eb11-a2cd-005056926fe4"); //Page_1_1
            var websitePage2ID = new Guid("7b530879-3219-eb11-a2cd-005056926fe4"); //Page_2


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .InsertSearchQuery("Page_1")
                .ClickSearchButton()

                .ValidateNameCellText_SearchResultArea(websitePage1ID.ToString(), "Page_1")
                .ValidateCreatedByCellText_SearchResultArea(websitePage1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText_SearchResultArea(websitePage1ID.ToString(), "28/10/2020 15:05:08")

                .ValidateNameCellText_SearchResultArea(websitePage1_1ID.ToString(), "Page_1_1")
                .ValidateCreatedByCellText_SearchResultArea(websitePage1_1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText_SearchResultArea(websitePage1_1ID.ToString(), "28/10/2020 15:18:12")

                .ValidateRecordNotPresent(websitePage2ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5771")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (all fields must have values) - " +
            "Validate that the user is redirected to the WebSite Page record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitePage1_1ID = new Guid("452f77ca-3019-eb11-a2cd-005056926fe4"); //Page_1_1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage1_1ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .ValidateNameFieldText("Page_1_1")
                .ValidateParentPageFieldLinkText("Page_1")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsSecureYesRadioButtonChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-5772")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the WebSite Page record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitePage1ID = new Guid("2b8b1ef7-2e19-eb11-a2cd-005056926fe4"); //Page_1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage1ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .ValidateNameFieldText("Page_1")
                .ValidateParentPageFieldLinkText("")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsSecureNoRadioButtonChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-5773")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website Page record - " +
            "Update all fields - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitePage2_2ID = new Guid("5e2e4764-c033-eb11-a2d5-005056926fe4"); //Page_2_2
            var localizedStringId = new Guid("5F2E4764-C033-EB11-A2D5-005056926FE4"); //Page 2.2
            var localizedStringValueId = new Guid("0638CAE0-B874-409D-9E20-E72276F17743"); //Page 2.2

            //update the website page record
            dbHelper.websitePage.UpdateWebsitePage(websitePage2_2ID, "Page_2_2", null, localizedStringId, "");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage2_2ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .InsertName("Page_2_2_Update")
                .TapSitemapOrBreadCrumbTextLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Page1_1.Sitemap");

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .TapIsSecureYesOption()
                .TapParentPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Page_2");

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage2_2ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .ValidateNameFieldText("Page_2_2_Update")
                .ValidateSitemapOrBreadCrumbTextFieldLink("Page1_1.Sitemap")
                .ValidateParentPageFieldLinkText("Page_2")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsSecureYesRadioButtonChecked();

        }

        [TestProperty("JiraIssueID", "CDV6-5774")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_2_3"))
                dbHelper.websitePage.DeleteWebsitePage(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClicAddNewRecordButton();

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .InsertName("Page_2_3")
                .TapParentPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Page_2");

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .TapIsSecureYesOption()
                .TapParentPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Page_2");

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .TapSitemapOrBreadCrumbTextLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Page1_1.Sitemap");

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var pages = dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_2_3");
            Assert.AreEqual(1, pages.Count);

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(pages[0].ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .ValidateNameFieldText("Page_2_3")
                .ValidateSitemapOrBreadCrumbTextFieldLink("Page1_1.Sitemap")
                .ValidateParentPageFieldLinkText("Page_2")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsSecureYesRadioButtonChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-5775")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4630 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a website page record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePages_UITestMethod07()
        {

            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_3"))
                dbHelper.websitePage.DeleteWebsitePage(websitepageid);

            var websitePageID = dbHelper.websitePage.CreateWebsitePage("Page_3", websiteID, null, null, "");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePageID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDetailsTab()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var pages = dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_3");
            Assert.AreEqual(0, pages.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5409

        [TestProperty("JiraIssueID", "CDV6-5776")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Settings area - " +
            "Validate that the Website Settings page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5777")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Search for Website Setting record using the search query 'Setting 1' - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var Setting1ID = new Guid("fcdd06a8-eb1d-eb11-a2cd-005056926fe4"); //Setting 1
            var Setting2ID = new Guid("ce12e0e5-eb1d-eb11-a2cd-005056926fe4"); //Setting 2
            var Setting3ID = new Guid("7c813aae-ec1d-eb11-a2cd-005056926fe4"); //Setting 3


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .InsertSearchQuery("Setting 1")
                .ClickSearchButton()

                .ValidateNameSearchCellText(Setting1ID.ToString(), "Setting 1")
                .ValidateCreatedBySearchCellText(Setting1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnSearchCellText(Setting1ID.ToString(), "03/11/2020 15:45:54")

                .ValidateRecordNotPresent(Setting2ID.ToString())
                .ValidateRecordNotPresent(Setting3ID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5778")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a Website Setting record (all fields must have values) - " +
            "Validate that the user is redirected to the WebSite Setting record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Setting1ID = new Guid("fcdd06a8-eb1d-eb11-a2cd-005056926fe4"); //Setting 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting1ID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateNameFieldText("Setting 1")
                .ValidateDescriptionFieldText("Setting 1 Description")
                .ValidateSettingValueFieldText("Setting 1 Value")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsEncryptedNoRadioButtonChecked()
                .ValidateChangeEncryptedValueLinkVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5779")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a Website Setting record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the WebSite Setting record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Setting2ID = new Guid("ce12e0e5-eb1d-eb11-a2cd-005056926fe4"); //Setting 2

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting2ID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateNameFieldText("Setting 2")
                .ValidateDescriptionFieldText("Setting 2 Description")
                .ValidateSettingValueFieldText("")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsEncryptedNoRadioButtonChecked()
                .ValidateChangeEncryptedValueLinkVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5780")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a Website Setting record (Is Encrypted is set to Yes) - " +
            "Validate that the user is redirected to the WebSite Setting record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Setting3ID = new Guid("7c813aae-ec1d-eb11-a2cd-005056926fe4"); //Setting 3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting3ID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateNameFieldText("Setting 3")
                .ValidateDescriptionFieldText("Setting 3 Description")
                .ValidateSettingValueFieldText("")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsEncryptedYesRadioButtonChecked()
                .ValidateChangeEncryptedValueLinkVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5781")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a Website Setting record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Setting4ID = new Guid("F1A43EDC-F51D-EB11-A2CD-005056926FE4"); //Setting 4

            dbHelper.websiteSetting.UpdateWebsiteSetting(Setting4ID, "Setting 4", "Setting 4 Description", false, null, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting4ID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .InsertName("Setting 4 - Update")
                .InsertDescription("Setting 4 Description Update")
                .InsertSettingValue("Setting 4 Value Update")
                .ClickSaveAndCloseButton();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting4ID.ToString());

            webSiteSettingRecordPage
               .WaitForWebSiteSettingRecordPageToLoad()
               .ValidateNameFieldText("Setting 4 - Update")
               .ValidateDescriptionFieldText("Setting 4 Description Update")
               .ValidateSettingValueFieldText("Setting 4 Value Update")
               .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
               .ValidateIsEncryptedNoRadioButtonChecked()
               .ValidateChangeEncryptedValueLinkVisibility(false)
               ;

        }

        [TestProperty("JiraIssueID", "CDV6-5782")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a Website Setting record - " +
            "Update all fields (Set Is Encrypted to Yes) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod07()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Setting4ID = new Guid("F1A43EDC-F51D-EB11-A2CD-005056926FE4"); //Setting 4

            dbHelper.websiteSetting.UpdateWebsiteSetting(Setting4ID, "Setting 4", "Setting 4 Description", false, null, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting4ID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .InsertName("Setting 4 - Update")
                .InsertDescription("Setting 4 Description Update")
                .TapIsEncryptedYesOption()
                .ValidateSettingValueFieldVisibility(false)
                .TapChangeEncryptedValueLink();

            changeEncryptedValuePopup
                .WaitForChangeEncryptedValuePopupToLoad()
                .InsertNewEncryptedValue("Setting 4 Encrypted Value")
                .InsertConfirmNewEncryptedValue("Setting 4 Encrypted Value")
                .TapSaveButton();

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(Setting4ID.ToString());

            webSiteSettingRecordPage
               .WaitForWebSiteSettingRecordPageToLoad()
               .ValidateNameFieldText("Setting 4 - Update")
               .ValidateDescriptionFieldText("Setting 4 Description Update")
               .ValidateSettingValueFieldVisibility(false)
               .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
               .ValidateIsEncryptedYesRadioButtonChecked()
               .ValidateChangeEncryptedValueLinkVisibility(true)
               ;

            var fields = dbHelper.websiteSetting.GetByID(Setting4ID, "EncryptedValue");

            Assert.IsFalse(((string)fields["encryptedvalue"]).Equals("Setting 4 Encrypted Value")); // the value should be encrypted, therefore the comparison must be false

        }

        [TestProperty("JiraIssueID", "CDV6-5783")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Tap on the add new record button - " +
            "Set data in all fields (set Is Encrypted to No) - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5"))
                dbHelper.websiteSetting.DeleteWebsiteSetting(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateIsEncryptedNoRadioButtonChecked()
                .ValidateEncryptedValueFieldVisibility(false)
                .InsertName("Setting 5")
                .InsertDescription("Setting 5 Description")
                .InsertSettingValue("Setting 5 Value")
                .ClickSaveAndCloseButton();

            var settings = dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5");
            Assert.AreEqual(1, settings.Count);

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(settings[0].ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateNameFieldText("Setting 5")
                .ValidateDescriptionFieldText("Setting 5 Description")
                .ValidateSettingValueFieldText("Setting 5 Value")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsEncryptedNoRadioButtonChecked()
                .ValidateEncryptedValueFieldVisibility(false)
                .ValidateChangeEncryptedValueLinkVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5784")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Tap on the add new record button - " +
            "Set data in all fields (set Is Encrypted to Yes) - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod09()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5"))
                dbHelper.websiteSetting.DeleteWebsiteSetting(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .TapIsEncryptedYesOption()
                .ValidateSettingValueFieldVisibility(false)
                .InsertName("Setting 5")
                .InsertDescription("Setting 5 Description")
                .InsertEncryptedValue("Setting 5 Value")
                .ClickSaveAndCloseButton();

            var settings = dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5");
            Assert.AreEqual(1, settings.Count);

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(settings[0].ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ValidateNameFieldText("Setting 5")
                .ValidateDescriptionFieldText("Setting 5 Description")
                .ValidateWebsiteFieldLinkText("Automation - Web Site 07")
                .ValidateIsEncryptedYesRadioButtonChecked()
                .ValidateEncryptedValueFieldVisibility(false)
                .ValidateChangeEncryptedValueLinkVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5785")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Open a website Setting record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5"))
                dbHelper.websiteSetting.DeleteWebsiteSetting(websitepageid);

            Guid settingID = dbHelper.websiteSetting.CreateWebsiteSetting("Setting 5", "Setting 5 Description", websiteID, false, "Setting 5 Value", null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClickOnWebSiteSettingRecord(settingID.ToString());

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad();

            var records = dbHelper.websiteSetting.GetByWebSiteIDAndName(websiteID, "Setting 5");
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5786")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5409 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Settings area - Tap on the add new record button - " +
            "Set data in all mandatory fields - Set a value in the name field that match an existing setting records - Tap on the save button - " +
            "Validate that the user is prevented from saving the record.")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSettings_UITestMethod11()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSettings();

            webSiteSettingsPage
                .WaitForWebSiteSettingsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSettingRecordPage
                .WaitForWebSiteSettingRecordPageToLoad()
                .InsertName("Setting 1")
                .InsertDescription("Setting 1 Description")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Website Setting with same combination already exist: Name = Setting 1 AND Website = Automation - Web Site 07.")
                .TapCloseButton();
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5408

        [TestProperty("JiraIssueID", "CDV6-5787")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - " +
            "Open a Website record (all fields in the areas Administration; Password Complexity; Password Policy; Account Lockout Policy; Two Factor Authentication must have values) - " +
            "Validate that the user is redirected to the WebSite record page")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod01()
        {
            var website1ID = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(website1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 01")
                .ValidateHomePageFieldText("Page_1")
                .ValidateMemberHomePageFieldText("Page_1_1")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person")

                .ValidateWebSiteURLLink("http://AutomationWebSite01.com/")
                .ValidateEmailVerificationRequiredYesOptionChecked()
                .ValidateUserApprovalRequiredYesOptionChecked()

                .ValidateMinimumPasswordLength("7")
                .ValidateMinimumNumericCharacters("1")
                .ValidateMinimumSpecialCharacters("2")
                .ValidateMinimumUppercaseLetters("3")

                .ValidateMaximumPasswordAgeDays("365")
                .ValidateMinimumpasswordAgeDays("30")
                .ValidateEnforcePasswordHistory("5")

                .ValidateEnableAccountLockingYesOptionChecked()
                .ValidateAccountLockoutDuration("20")
                .ValidateAccountLockoutThreshold("16")
                .ValidateResetAccountLockoutCounterAfter("")

                .ValidateEnableTwoFactorAuthenticationYesOptionChecked()
                .ValidateDefaultPINReceivingMethod("SMS")
                .ValidatePINExpireInMinutes("10")
                .ValidateNumberOfPINDigits("5")
                .ValidateMaxInvalidPinAttemptAllowed("5")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5788")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - " +
            "Open a Website record (only mandatory fields in the areas Administration; Password Complexity; Password Policy; Account Lockout Policy; Two Factor Authentication; have values) - " +
            "Validate that the user is redirected to the WebSite record page")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod02()
        {
            var website1ID = new Guid("37397330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(website1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 02")
                .ValidateHomePageFieldText("")
                .ValidateMemberHomePageFieldText("")
                .ValidateApplicationFieldLinkText("CareDirector App")
                .ValidateUserRecordTypeFieldLinkText("Case")

                .ValidateWebSiteURLLink("https://AutomationWebSite02.com")
                .ValidateEmailVerificationRequiredNoOptionChecked()
                .ValidateUserApprovalRequiredNoOptionChecked()

                .ValidateMinimumPasswordLength("5")
                .ValidateMinimumNumericCharacters("")
                .ValidateMinimumSpecialCharacters("")
                .ValidateMinimumUppercaseLetters("")

                .ValidateMaximumPasswordAgeDays("")
                .ValidateMinimumpasswordAgeDays("")
                .ValidateEnforcePasswordHistory("")

                .ValidateEnableAccountLockingNoOptionChecked()
                .ValidateAccountLockoutDuration("")
                .ValidateAccountLockoutThreshold("")
                .ValidateResetAccountLockoutCounterAfter("")

                .ValidateEnableTwoFactorAuthenticationNoOptionChecked()
                .ValidateDefaultPINReceivingMethod("")
                .ValidatePINExpireInMinutes("")
                .ValidateNumberOfPINDigits("");
            ;

        }

        [TestProperty("JiraIssueID", "CDV6-5789")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites - Open a Website record - " +
            "Validate that the Record Type field is disabled and Application field is enabled")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod03()
        {
            var website1ID = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(website1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateApplicationLookupButtonEnabled()
                .ValidateApplicationRemoveButtonVisibility(true)
                .ValidateUserRecordTypeLookupButtonDisabled()
                .ValidateUserRecordTypeRemoveButtonVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-5790")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all fields - " +
            "Tap on the save button - Validate the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod04()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertHomePage("Page_1")
                .InsertMemberHomePage("Page_1_1")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .InsertVersion("1")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()

                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .ClickEmailVerificationRequiredYesOptionChecked()
                .ClickUserApprovalRequiredYesOptionChecked()

                .InsertMinimumPasswordLength("7")
                .InsertMinimumNumericCharacters("1")
                .InsertMinimumSpecialCharacters("2")
                .InsertMinimumUppercaseLetters("3")

                .InsertMaximumPasswordAgeDays("365")
                .InsertMinimumpasswordAgeDays("30")
                .InsertEnforcePasswordHistory("5")
                .InsertPasswordResetExpireInMinutes("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutDuration("20")
                .InsertAccountLockoutThreshold("16")

                .ClickEnableTwoFactorAuthenticationYesOptionChecked()
                .InsertDefaultPINReceivingMethod("SMS")
                .InsertPINExpireInMinutes("10")
                .InsertNumberOfPINDigits("5")
                .InsertMaxInvalidPinAttemptAllowed("6")
                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, websites.Count);

            webSitePage
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websites[0].ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 05")
                .ValidateDisplayNameFieldText("Automation - Web Site 05")
                .ValidateHomePageFieldText("Page_1")
                .ValidateMemberHomePageFieldText("Page_1_1")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person")

                .ValidateWebSiteURLLink("https://AutomationWebSite05.com/")
                .ValidateEmailVerificationRequiredYesOptionChecked()
                .ValidateUserApprovalRequiredYesOptionChecked()

                .ValidateMinimumPasswordLength("7")
                .ValidateMinimumNumericCharacters("1")
                .ValidateMinimumSpecialCharacters("2")
                .ValidateMinimumUppercaseLetters("3")

                .ValidateMaximumPasswordAgeDays("365")
                .ValidateMinimumpasswordAgeDays("30")
                .ValidateEnforcePasswordHistory("5")
                .ValidatePasswordResetExpireInMinutes("5")

                .ValidateEnableAccountLockingYesOptionChecked()
                .ValidateAccountLockoutDuration("20")
                .ValidateAccountLockoutThreshold("16")

                .ValidateEnableTwoFactorAuthenticationYesOptionChecked()
                .ValidateDefaultPINReceivingMethod("SMS")
                .ValidatePINExpireInMinutes("10")
                .ValidateNumberOfPINDigits("5");

        }

        [TestProperty("JiraIssueID", "CDV6-5791")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in the mandatory fields only - " +
            "Tap on the save button - Validate the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod05()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .InsertVersion("1")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("7")
                .InsertPasswordResetExpireInMinutes("5")
                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, websites.Count);

            webSitePage
                .InsertSearchQuery("Automation - Web Site")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websites[0].ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ValidateNameFieldText("Automation - Web Site 05")
                .ValidateDisplayNameFieldText("Automation - Web Site 05")
                .ValidateHomePageFieldText("")
                .ValidateMemberHomePageFieldText("")
                .ValidateApplicationFieldLinkText("CareDirector")
                .ValidateUserRecordTypeFieldLinkText("Person")

                .ValidateWebSiteURLLink("https://AutomationWebSite05.com/")
                .ValidateEmailVerificationRequiredNoOptionChecked()
                .ValidateUserApprovalRequiredNoOptionChecked()

                .ValidateMinimumPasswordLength("7")
                .ValidateMinimumNumericCharacters("")
                .ValidateMinimumSpecialCharacters("")
                .ValidateMinimumUppercaseLetters("")

                .ValidateMaximumPasswordAgeDays("")
                .ValidateMinimumpasswordAgeDays("")
                .ValidateEnforcePasswordHistory("")

                .ValidateEnableAccountLockingNoOptionChecked()
                .ValidateAccountLockoutDuration("")
                .ValidateAccountLockoutThreshold("")
                .ValidateResetAccountLockoutCounterAfter("")

                .ValidateEnableTwoFactorAuthenticationNoOptionChecked()
                .ValidateDefaultPINReceivingMethod("")
                .ValidatePINExpireInMinutes("")
                .ValidateNumberOfPINDigits("");

        }

        [TestProperty("JiraIssueID", "CDV6-5792")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Don´t set data in any field - " +
            " Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod06()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateApplicationFieldErrorLabelVisibility(true)
                .ValidateUserRecordTypeFieldErrorLabelVisibility(true)
                .ValidateWebSiteURLFieldErrorLabelVisibility(true)
                .ValidateMinimumPasswordLengthFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateApplicationFieldErrorLabel("Please fill out this field.")
                .ValidateUserRecordTypeFieldErrorLabel("Please fill out this field.")
                .ValidateWebSiteURLFieldErrorLabel("Please fill out this field.")
                .ValidateMinimumPasswordLengthFieldErrorLabel("Please fill out this field.")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5793")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Minimum Password Length' smaller than 5 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod07()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("4")
                .ClickSaveAndCloseButton()
                .ValidateNameFieldErrorLabelVisibility(false)
                .ValidateApplicationFieldErrorLabelVisibility(false)
                .ValidateUserRecordTypeFieldErrorLabelVisibility(false)
                .ValidateWebSiteURLFieldErrorLabelVisibility(false)
                .ValidateMinimumPasswordLengthFieldErrorLabelVisibility(true)
                .ValidateMinimumPasswordLengthFieldErrorLabel("Please enter a value between 5 and 2147483647.")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5794")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Minimum password age (Days)' greater than 998 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod08()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")
                .InsertMaximumPasswordAgeDays("1001")
                .InsertMinimumpasswordAgeDays("999")
                .ClickSaveAndCloseButton()
                .ValidateMinimumPasswordAgeDaysFieldErrorLabelVisibility(true)
                .ValidateMinimumPasswordAgeDaysFieldErrorLabel("Please enter a value between 0 and 998.")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5795")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Minimum password age (Days)' greater than Maximum password age -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod09()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")
                .InsertMaximumPasswordAgeDays("300")
                .InsertMinimumpasswordAgeDays("400");

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Minimum Password Age must be less than Maximum Password Age.").TapOKButton();
        }

        [TestProperty("JiraIssueID", "CDV6-5796")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Enforce Password History' smaller than 0 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod10()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")
                .InsertEnforcePasswordHistory("-1")
                .ClickSaveAndCloseButton()
                .ValidateEnforcePasswordHistoryFieldErrorLabelVisibility(true)
                .ValidateEnforcePasswordHistoryFieldErrorLabel("Please enter a value between 0 and 30.");
        }

        [TestProperty("JiraIssueID", "CDV6-5797")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Enforce Password History' greater than 10 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod11()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")
                .InsertEnforcePasswordHistory("31")
                .ClickSaveAndCloseButton()
                .ValidateEnforcePasswordHistoryFieldErrorLabelVisibility(true)
                .ValidateEnforcePasswordHistoryFieldErrorLabel("Please enter a value between 0 and 30.");
        }

        [TestProperty("JiraIssueID", "CDV6-5798")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Account lockout threshold' equal to 15 - Set 'Account lockout duration' equal to 10 - Set 'Reset account lockout counter after' equal to 10 " +
            "Tap on the save button - Validate that the user is allowed to save the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod12()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertVersion("1")
                .InsertMinimumPasswordLength("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutThreshold("15")
                .InsertAccountLockoutDuration("10")

                .InsertPasswordResetExpireInMinutes("5")

                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)

                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5799")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Account lockout threshold' equal to 15 - Set 'Account lockout duration' equal to 11 - Set 'Reset account lockout counter after' equal to 10 " +
            "Tap on the save button - Validate that the user is allowed to save the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod13()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertVersion("1")
                .InsertMinimumPasswordLength("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutThreshold("15")
                .InsertAccountLockoutDuration("11")
                .InsertPasswordResetExpireInMinutes("5")

                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)

                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, records.Count);
        }

        //[TestProperty("JiraIssueID", "CDV6-5800")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
        //    "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
        //    "Set a 'Account lockout threshold' equal to 15 - Set 'Account lockout duration' equal to 9 - Set 'Reset account lockout counter after' equal to 10 " +
        //    "Tap on the save button - Validate that the user is NOT allowed to save the record")]
        //[TestMethod, TestCategory("UITest")]
        //public void Website_PasswordAndAccount_UITestMethod14()
        //{
        //    var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
        //    var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

        //    //remove all matching websites
        //    foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
        //    {
        //        foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
        //            dbHelper.websitePage.DeleteWebsitePage(websitepageid);
        //        dbHelper.website.DeleteWebsite(websiteid);
        //    }


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .ClicAddNewRecordButton();

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertName("Automation - Web Site 05")
        //        .TapApplicationLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .TapUserRecordTypeLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertWebSiteURL("https://AutomationWebSite05.com/")
        //        .InsertMinimumPasswordLength("5")

        //        .ClickEnableAccountLockingYesOptionChecked()
        //        .InsertAccountLockoutThreshold("15")
        //        .InsertAccountLockoutDuration("9")

        //        .InsertPasswordResetExpireInMinutes("5")

        //        .ClickSaveAndCloseButton();

        //    dynamicDialogPopup
        //        .WaitForDynamicDialogPopupToLoad()
        //        .ValidateMessage("Account Lockout Duration must be greater than Reset Account Lockout Counter After.")
        //        .TapCloseButton();

        //    var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
        //    Assert.AreEqual(0, records.Count);
        //}

        [TestProperty("JiraIssueID", "CDV6-5801")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Account lockout threshold' equal to 0 - " +
            "Tap on the save button - Validate that the user is NOT allowed to save the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod15()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertVersion("1")
                .InsertMinimumPasswordLength("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutThreshold("0")
                .ClickSaveAndCloseButton()
                .ValidateAccountLockoutThresholdFieldErrorLabelVisibility(true)
                .ValidateAccountLockoutThresholdFieldErrorLabel("Please enter a value between 1 and 999.")
;

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(0, records.Count);
        }

        //[TestProperty("JiraIssueID", "CDV6-5802")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
        //    "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
        //    "Set a 'Account lockout threshold' equal to 5 - Set 'Account lockout duration' equal to 10 - Set 'Reset account lockout counter after' equal to 15 " +
        //    "Tap on the save button - Validate that the user is NOT allowed to save the record")]
        //[TestMethod, TestCategory("UITest")]
        //public void Website_PasswordAndAccount_UITestMethod16()
        //{
        //    var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
        //    var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

        //    //remove all matching websites
        //    foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
        //    {
        //        foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
        //            dbHelper.websitePage.DeleteWebsitePage(websitepageid);
        //        dbHelper.website.DeleteWebsite(websiteid);
        //    }


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .ClicAddNewRecordButton();

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertName("Automation - Web Site 05")
        //        .TapApplicationLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .TapUserRecordTypeLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertWebSiteURL("https://AutomationWebSite05.com/")
        //        .InsertMinimumPasswordLength("5")

        //        .ClickEnableAccountLockingYesOptionChecked()
        //        .InsertAccountLockoutThreshold("5")
        //        .InsertAccountLockoutDuration("10")
        //        .InsertPasswordResetExpireInMinutes("5")
        //        .ClickSaveAndCloseButton();

        //    dynamicDialogPopup
        //        .WaitForDynamicDialogPopupToLoad()
        //        .ValidateMessage("Account Lockout Threshold must be greater than Reset Account Lockout Counter After.")
        //        .TapCloseButton();

        //    var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
        //    Assert.AreEqual(0, records.Count);
        //}

        [TestProperty("JiraIssueID", "CDV6-5803")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Account lockout threshold' equal to 10 - Set 'Account lockout duration' equal to 10 - Set 'Reset account lockout counter after' equal to 10 " +
            "Tap on the save button - Validate that the user is allowed to save the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod18()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertVersion("1")
                .InsertMinimumPasswordLength("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutThreshold("10")
                .InsertAccountLockoutDuration("10")
                .InsertPasswordResetExpireInMinutes("5")
                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5804")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Account lockout threshold' equal to 10 - Set 'Account lockout duration' equal to 11 - Set 'Reset account lockout counter after' equal to 10 " +
            "Tap on the save button - Validate that the user is allowed to save the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod19()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .InsertDisplayName("Automation - Web Site 05")
                .InsertRegistrationFormName("registration")
                .InsertRecordsPerPage("10")
                .InsertSessionTimeoutInMinutes("10")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertVersion("1")
                .InsertMinimumPasswordLength("5")

                .ClickEnableAccountLockingYesOptionChecked()
                .InsertAccountLockoutThreshold("10")
                .InsertAccountLockoutDuration("11")
                .InsertPasswordResetExpireInMinutes("5")
                .SelectAuthenticationType("Internal")
                .SelectType(portalWebsiteName)
                .ClickSaveAndCloseButton();

            webSitePage
                .WaitForWebSiteToLoad();

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(1, records.Count);
        }

        //[TestProperty("JiraIssueID", "CDV6-5805")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
        //    "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
        //    "Set a 'Account lockout threshold' equal to 10 - Set 'Account lockout duration' equal to 9 - Set 'Reset account lockout counter after' equal to 10 " +
        //    "Tap on the save button - Validate that the user is NOT allowed to save the record")]
        //[TestMethod, TestCategory("UITest")]
        //public void Website_PasswordAndAccount_UITestMethod20()
        //{
        //    var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
        //    var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

        //    //remove all matching websites
        //    foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
        //    {
        //        foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
        //            dbHelper.websitePage.DeleteWebsitePage(websitepageid);
        //        dbHelper.website.DeleteWebsite(websiteid);
        //    }


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .ClicAddNewRecordButton();

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertName("Automation - Web Site 05")
        //        .TapApplicationLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .TapUserRecordTypeLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .InsertWebSiteURL("https://AutomationWebSite05.com/")
        //        .InsertMinimumPasswordLength("5")

        //        .ClickEnableAccountLockingYesOptionChecked()
        //        .InsertAccountLockoutThreshold("10")
        //        .InsertAccountLockoutDuration("9")
        //        .InsertResetAccountLockoutCounterAfter("10")

        //        .InsertPasswordResetExpireInMinutes("5")

        //        .ClickSaveAndCloseButton();

        //    dynamicDialogPopup
        //        .WaitForDynamicDialogPopupToLoad()
        //        .ValidateMessage("Account Lockout Duration must be greater than Reset Account Lockout Counter After.")
        //        .TapCloseButton();

        //    var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
        //    Assert.AreEqual(0, records.Count);
        //}

        [TestProperty("JiraIssueID", "CDV6-5806")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Number of PIN Digits' smaller than 4 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod21()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")
                .InsertPasswordResetExpireInMinutes("5")

                .ClickEnableTwoFactorAuthenticationYesOptionChecked()
                .InsertPINExpireInMinutes("5")
                .InsertDefaultPINReceivingMethod("Email")
                .InsertNumberOfPINDigits("3")
                .InsertMaxInvalidPinAttemptAllowed("6")

                .ClickSaveAndCloseButton()
                .ValidateNumberOfPINDigitsFieldErrorLabelVisibility(true)
                .ValidateNumberOfPINDigitsFieldErrorLabel("Please enter a value between 4 and 6.");

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5807")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5408 - " +
            "Login in the web app - Navigate to Settings; Portals; WebSites -  Tap on the Add new record button - Set data in all mandatory fields - " +
            "Set a 'Number of PIN Digits' greater than 6 -  Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void Website_PasswordAndAccount_UITestMethod22()
        {
            var application1ID = dbHelper.application.GetByName("CareDirector")[0]; //CareDirector
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Person

            //remove all matching websites
            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Web Site 05"))
            {
                foreach (var websitepageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.DeleteWebsitePage(websitepageid);
                dbHelper.website.DeleteWebsite(websiteid);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .ClicAddNewRecordButton();

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertName("Automation - Web Site 05")
                .TapApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector").TapSearchButton().SelectResultElement(application1ID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .TapUserRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Person").TapSearchButton().SelectResultElement(personBusinessObjectID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .InsertWebSiteURL("https://AutomationWebSite05.com/")
                .InsertMinimumPasswordLength("6")

                .ClickEnableTwoFactorAuthenticationYesOptionChecked()
                .InsertPINExpireInMinutes("5")
                .InsertDefaultPINReceivingMethod("SMS")
                .InsertNumberOfPINDigits("7")
                .InsertMaxInvalidPinAttemptAllowed("6")

                .InsertPasswordResetExpireInMinutes("5")

                .ClickSaveAndCloseButton()
                .ValidateNumberOfPINDigitsFieldErrorLabelVisibility(true)
                .ValidateNumberOfPINDigitsFieldErrorLabel("Please enter a value between 4 and 6.");

            var records = dbHelper.website.GetWebSiteByName("Automation - Web Site 05");
            Assert.AreEqual(0, records.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5439

        [TestProperty("JiraIssueID", "CDV6-5808")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Announcements area - " +
            "Validate that the Website Announcements page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5809")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var Announcement1ID = new Guid("23412303-7a1f-eb11-a2cd-005056926fe4"); //Website Announcement 01
            var Announcement2ID = new Guid("afeaed0b-7a1f-eb11-a2cd-005056926fe4"); //Website Announcement 02
            var Announcement3ID = new Guid("e4abece5-7d1f-eb11-a2cd-005056926fe4"); //Website Announcement 03


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()

                .ValidateNameCellText(Announcement1ID.ToString(), "Website Announcement 01")
                .ValidateExpiresOnCellText(Announcement1ID.ToString(), "01/11/2039 08:00:00")
                .ValidateCreatedByCellText(Announcement1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText(Announcement1ID.ToString(), "05/11/2020 15:17:27")

                .ValidateRecordPresent(Announcement2ID.ToString())
                .ValidateRecordPresent(Announcement3ID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5810")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Open a Website Announcement record (all fields must have values) - " +
            "Validate that the user is redirected to the WebSite Announcement record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Announcement1ID = new Guid("23412303-7a1f-eb11-a2cd-005056926fe4"); //Website Announcement 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(Announcement1ID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateNameFieldText("Website Announcement 01")

                .LoadContentsRichTextBox()
                .ValidateContentsFieldText("1", "Content 1")
                .ValidateContentsFieldText("2", "Content 2")

                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateStatusValueFieldText("Published")
                .ValidateExpiresOnValueFieldText("01/11/2039", "08:00")
                .ValidatePublishedOnValueFieldText("01/11/2020", "07:00")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5811")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Open a Website Announcement record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the WebSite Announcement record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Announcement1ID = new Guid("afeaed0b-7a1f-eb11-a2cd-005056926fe4"); //Website Announcement 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(Announcement1ID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateNameFieldText("Website Announcement 02")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateStatusValueFieldText("Draft")
                .ValidateExpiresOnValueFieldText("", "")
                .ValidatePublishedOnValueFieldText("", "")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5812")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Open a Website Announcement record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Announcement4ID = new Guid("312e541e-801f-eb11-a2cd-005056926fe4"); //Website Announcement 04

            dbHelper.websiteAnnouncement.UpdateWebsiteAnnouncement(Announcement4ID, "Website Announcement 04", null, null, null, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(Announcement4ID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .InsertName("Website Announcement 04 - Update")
                .InsertContent("1", "Content 1")
                .SelectStatus("Published")
                .InsertExpiresOn("01/11/2039", "08:00")
                .InsertPublishedOn("01/11/2020", "07:00")
                .ClickSaveAndCloseButton();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(Announcement4ID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateNameFieldText("Website Announcement 04 - Update")

                .LoadContentsRichTextBox()
                .ValidateContentsFieldText("1", "Content 1")

                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateStatusValueFieldText("Published")
                .ValidateExpiresOnValueFieldText("01/11/2039", "08:00")
                .ValidatePublishedOnValueFieldText("01/11/2020", "07:00")
               ;

        }

        [TestProperty("JiraIssueID", "CDV6-5813")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Open a Website Announcement record - " +
            "Remove the values from the mandatory fields - Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod07()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var Announcement4ID = new Guid("312e541e-801f-eb11-a2cd-005056926fe4"); //Website Announcement 04

            dbHelper.websiteAnnouncement.UpdateWebsiteAnnouncement(Announcement4ID, "Website Announcement 04", null, null, null, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(Announcement4ID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .InsertName("")
                .ClickSaveButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-5814")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching announcements
            foreach (var websitepageid in dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05"))
                dbHelper.websiteAnnouncement.DeleteWebsiteAnnouncement(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .InsertName("Website Announcement 05")
                .InsertContent("1", "Content 1")
                .SelectStatus("Published")
                .InsertExpiresOn("01/11/2040", "08:00")
                .InsertPublishedOn("01/11/2020", "07:00")
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05");
            Assert.AreEqual(1, records.Count);

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(records[0].ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateNameFieldText("Website Announcement 05")

                .LoadContentsRichTextBox()
                .ValidateContentsFieldText("1", "Content 1")

                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateStatusValueFieldText("Published")
                .ValidateExpiresOnValueFieldText("01/11/2040", "08:00")
                .ValidatePublishedOnValueFieldText("01/11/2020", "07:00")
               ;
        }

        [TestProperty("JiraIssueID", "CDV6-5815")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Tap on the add new record button - " +
            "Set data in mandatory fields only - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod09()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching announcements
            foreach (var websitepageid in dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05"))
                dbHelper.websiteAnnouncement.DeleteWebsiteAnnouncement(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .InsertName("Website Announcement 05")
                .SelectStatus("Published")
                .InsertPublishedOn("01/11/2020", "08:00")
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05");
            Assert.AreEqual(1, records.Count);

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(records[0].ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ValidateNameFieldText("Website Announcement 05")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateStatusValueFieldText("Published")
                .ValidateExpiresOnValueFieldText("", "")
                .ValidatePublishedOnValueFieldText("01/11/2020", "08:00")
               ;
        }

        [TestProperty("JiraIssueID", "CDV6-5816")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Tap on the add new record button - " +
            "Dont set any data in the mandatory fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching announcements
            foreach (var websitepageid in dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05"))
                dbHelper.websiteAnnouncement.DeleteWebsiteAnnouncement(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ClickSaveAndCloseButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateStatusFieldErrorLabelVisibility(true)
                .ValidateStatusFieldErrorLabelText("Please fill out this field.");

            var records = dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05");
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-5817")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5439 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Announcements area - Open a website Announcement record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteAnnouncement_UITestMethod11()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching website pages
            foreach (var websitepageid in dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05"))
                dbHelper.websiteAnnouncement.DeleteWebsiteAnnouncement(websitepageid);

            Guid AnnouncementID = dbHelper.websiteAnnouncement.CreateWebsiteAnnouncement(websiteID, "Website Announcement 05", null, null, null, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteAnnouncements();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad()
                .ClickOnWebSiteAnnouncementRecord(AnnouncementID.ToString());

            webSiteAnnouncementRecordPage
                .WaitForWebSiteAnnouncementRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteAnnouncementsPage
                .WaitForWebSiteAnnouncementsPageToLoad();

            var records = dbHelper.websiteAnnouncement.GetByWebSiteIDAndName(websiteID, "Website Announcement 05");
            Assert.AreEqual(0, records.Count);
        }



        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5410

        [TestProperty("JiraIssueID", "CDV6-5818")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
           "Login in the web app - Open a Website record - Navigate to the Website User Pins area - " +
            "Validate that the Website UserPins page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5819")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
            "Login in the web app - Open a Website record - Navigate to the Website UserPins area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var pinrecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ValidatePINCellText(pinrecordID.ToString(), "1234")
                .ValidateExpireOnCellText(pinrecordID.ToString(), expireon.ToString("dd/MM/yyyy HH:mm:ss"))
                .ValidateCreatedByCellText(pinrecordID.ToString(), "Admin User")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5820")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
            "Login in the web app - Open a Website record - Navigate to the Website UserPins area - Open a Website UserPin record - " +
            "Validate that the user is redirected to the WebSite UserPin record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var pinrecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ClickOnWebSiteUserPinRecord(pinrecordID.ToString());

            webSiteUserPinRecordPage
                .WaitForWebSiteUserPinRecordPageToLoad()
                .ValidatePINFieldText("1234")
                .ValidateExpiresOnValueFieldText(expireon.ToString("dd'/'MM'/'yyyy"), expireon.ToString("HH:mm"))
                .ValidateWebSiteUserFieldLinkText("Hattie Abbott")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5821")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
            "Create a website user pin record with (set the expire on date in the past) - Execute the 'Clear Expired Pin Records' scheduled job" +
            "Login in the web app - Open a Website record - Navigate to the Website UserPins area - " +
            "Validate that the record was deleted by the scheduled job")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddDays(-1);
            var seton = DateTime.Now.AddDays(-3);
            var userPinRecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Pin Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredPinRecordsScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredPinRecordsScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ValidateRecordNotPresent(userPinRecordID.ToString()); //record should not be present (should have been deleted by the scheduled job)

            var records = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5822")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
            "Create a website user pin record with (set the expire on date in the future) - Execute the 'Clear Expired Pin Records' scheduled job" +
            "Login in the web app - Open a Website record - Navigate to the Website UserPins area - " +
            "Validate that the record is stil present")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddDays(1);
            var seton = DateTime.Now;
            var userPinRecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Pin Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredPinRecordsScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredPinRecordsScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ClickOnWebSiteUserPinRecord(userPinRecordID.ToString());

            webSiteUserPinRecordPage
                .WaitForWebSiteUserPinRecordPageToLoad()
                .ValidatePINFieldText("1234")
                .ValidateExpiresOnValueFieldText(expireon.ToString("dd/MM/yyyy"), expireon.ToString("HH:mm"))
                .ValidateWebSiteUserFieldLinkText("Hattie Abbott");

            var records = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5823")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
            "Login in the web app - Open a Website record - Navigate to the Website UserPins area - Open a website UserPin record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPin_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var pinrecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ClickOnWebSiteUserPinRecord(pinrecordID.ToString());

            webSiteUserPinRecordPage
                .WaitForWebSiteUserPinRecordPageToLoad()
                .ClickOnDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad();

            var records = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5411


        [TestProperty("JiraIssueID", "CDV6-5824")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
           "Login in the web app - Open a Website record - Navigate to the Website User Password Resets area - " +
            "Validate that the Website User Password Resets page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5825")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password Resets area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all Password Reset records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(recordid);

            //Create a new Password Reset record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var PasswordResetRecordID = dbHelper.websiteUserPasswordReset.CreateWebsiteUserPasswordReset(websiteUserID, expireon, seton, "Some error ...", "https://website07.com/resetpasslink");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad()

                .ValidateWebsiteUserCellText(PasswordResetRecordID.ToString(), "Hattie Abbott")
                .ValidateResetPasswordLinkCellText(PasswordResetRecordID.ToString(), "https://website07.com/resetpasslink")
                .ValidateExpireOnCellText(PasswordResetRecordID.ToString(), expireon.ToString("dd/MM/yyyy HH:mm:ss"))
                .ValidateCreatedByCellText(PasswordResetRecordID.ToString(), "Admin User")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5826")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password Resets area - Open a Website User Password Reset record - " +
            "Validate that the user is redirected to the WebSite User Password Reset record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all PasswordReset records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(recordid);

            //Create a new PasswordReset record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var PasswordResetRecordID = dbHelper.websiteUserPasswordReset.CreateWebsiteUserPasswordReset(websiteUserID, expireon, seton, "Some error ...", "https://website07.com/resetpasslink");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad()
                .ClickOnWebSiteUserPasswordResetRecord(PasswordResetRecordID.ToString());

            webSiteUserPasswordResetRecordPage
                .WaitForWebSiteUserPasswordResetRecordPageToLoad()
                .ValidateExpiresOnValueFieldText(expireon.ToString("dd'/'MM'/'yyyy"), expireon.ToString("HH:mm"))
                .ValidateResetPasswordLinkFieldText("https://website07.com/resetpasslink")
                .ValidateWebSiteUserFieldLinkText("Hattie Abbott");

        }

        [TestProperty("JiraIssueID", "CDV6-5827")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
            "Create a website user Password Reset record with (set the expire on date in the past) - Execute the 'Clear Expired Website User Password Reset' scheduled job" +
            "Login in the web app - Open a Website record - Navigate to the Website Password Resets area - " +
            "Validate that the record was deleted by the scheduled job")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all PasswordReset records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(recordid);

            //Create a new PasswordReset record
            var expireon = DateTime.Now.AddDays(-1);
            var seton = DateTime.Now.AddDays(-3);
            var userPasswordResetRecordID = dbHelper.websiteUserPasswordReset.CreateWebsiteUserPasswordReset(websiteUserID, expireon, seton, "Some error ...", "https://website07.com/resetpasslink");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Website User Password Reset" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredWebsiteUserPasswordResetScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredWebsiteUserPasswordResetScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad()
                .ValidateRecordNotPresent(userPasswordResetRecordID.ToString()); //record should not be present (should have been deleted by the scheduled job)

            var records = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5828")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
            "Create a website user Password Reset record with (set the expire on date in the future) - Execute the 'Clear Expired Website User Password Reset' scheduled job" +
            "Login in the web app - Open a Website record - Navigate to the Website Password Resets area - " +
            "Validate that the record is stil present")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all PasswordReset records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(recordid);

            //Create a new PasswordReset record
            var expireon = DateTime.Now.AddDays(1);
            var seton = DateTime.Now;
            var userPasswordResetRecordID = dbHelper.websiteUserPasswordReset.CreateWebsiteUserPasswordReset(websiteUserID, expireon, seton, "Some error ...", "https://website07.com/resetpasslink");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Website User Password Reset" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredWebsiteUserPasswordResetScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredWebsiteUserPasswordResetScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad()
                .ClickOnWebSiteUserPasswordResetRecord(userPasswordResetRecordID.ToString());

            webSiteUserPasswordResetRecordPage
                .WaitForWebSiteUserPasswordResetRecordPageToLoad()
                .ValidateExpiresOnValueFieldText(expireon.ToString("dd/MM/yyyy"), expireon.ToString("HH:mm"))
                .ValidateResetPasswordLinkFieldText("https://website07.com/resetpasslink")
                .ValidateWebSiteUserFieldLinkText("Hattie Abbott");

            var records = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5829")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5411 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password Resets area - Open a website User Password Reset record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordReset_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all PasswordReset records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(recordid);

            //Create a new PasswordReset record
            var expireon = DateTime.Now.AddHours(2);
            var seton = DateTime.Now;
            var PasswordResetrecordID = dbHelper.websiteUserPasswordReset.CreateWebsiteUserPasswordReset(websiteUserID, expireon, seton, "Some error ...", "https://website07.com/resetpasslink");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordReset();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad()
                .ClickOnWebSiteUserPasswordResetRecord(PasswordResetrecordID.ToString());

            webSiteUserPasswordResetRecordPage
                .WaitForWebSiteUserPasswordResetRecordPageToLoad()
                .ClickOnDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            websiteUserPasswordResetsPage
                .WaitForWebsiteUserPasswordResetsPageToLoad();

            var records = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }


        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5412

        [TestProperty("JiraIssueID", "CDV6-5830")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
           "Login in the web app - Open a Website record - Navigate to the Website User Password History area - " +
            "Validate that the Website User Password History page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5831")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password History area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com
            var websiteUserPasswordHistoryID = new Guid("b77fbc1a-7922-eb11-a2cd-005056926fe4");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ValidateCreatedByCellText(websiteUserPasswordHistoryID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText(websiteUserPasswordHistoryID.ToString(), "09/11/2020 10:48:30")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5832")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password History area - Open a Website User Password History record - " +
            "Validate that the user is redirected to the WebSite User Password History record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com
            var websiteUserPasswordHistoryID = new Guid("b77fbc1a-7922-eb11-a2cd-005056926fe4");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ClickOnWebSiteUserPasswordHistoryRecord(websiteUserPasswordHistoryID.ToString());

            websiteUserPasswordHistoryRecordPage
                .WaitForWebSiteUserPasswordHistoryRecordPageToLoad()
                .ValidateWebSiteUserFieldLinkText("Hattie Abbott")
                .ValidatePasswordValueFieldText("vrBiayQFOLS0SfIFH0gmJtu/wDX+uo/FDlqHUt4ouOU=")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5833")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Create 3 website User Password History records with for a website with 'Enforce Password History' set to 2 - " +
            "Execute the 'Clear Website User Password History Job' scheduled job" +
            "Login in the web App - Open a Website record - Open the user record - Navigate to the Website Password History area - " +
            "Validate that only the 2 most recent records are present")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ec49a919-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser2@mail.com

            //Remove all PasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            var userPasswordHistoryRecordID1 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password1");
            System.Threading.Thread.Sleep(1500);
            var userPasswordHistoryRecordID2 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password2");
            System.Threading.Thread.Sleep(1500);
            var userPasswordHistoryRecordID3 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password3");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Website User Password History Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearWebsiteUserPasswordHistoryJobScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearWebsiteUserPasswordHistoryJobScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ValidateRecordNotPresent(userPasswordHistoryRecordID1.ToString()) //record should have been deleted as it is the oldest 
                .ValidateRecordPresent(userPasswordHistoryRecordID2.ToString())
                .ValidateRecordPresent(userPasswordHistoryRecordID3.ToString());

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(2, records.Count);
            Assert.IsTrue(records.Contains(userPasswordHistoryRecordID2));
            Assert.IsTrue(records.Contains(userPasswordHistoryRecordID3));

        }

        [TestProperty("JiraIssueID", "CDV6-5834")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Create 2 website User Password History records with for a website with 'Enforce Password History' set to 2 - " +
            "Execute the 'Clear Website User Password History Job' scheduled job" +
            "Login in the web App - Open a Website record - Open the user record - Navigate to the Website Password History area - " +
            "Validate that both records are present")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ec49a919-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser2@mail.com

            //Remove all PasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            var userPasswordHistoryRecordID1 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password1");
            System.Threading.Thread.Sleep(1500);
            var userPasswordHistoryRecordID2 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password2");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Website User Password History Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearWebsiteUserPasswordHistoryJobScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearWebsiteUserPasswordHistoryJobScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ValidateRecordPresent(userPasswordHistoryRecordID1.ToString())
                .ValidateRecordPresent(userPasswordHistoryRecordID2.ToString());

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(2, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5835")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Create 3 website User Password History records with for a website with 'Enforce Password History' set to 0 - " +
            "Execute the 'Clear Website User Password History Job' scheduled job" +
            "Login in the web App - Open a Website record - Open the user record - Navigate to the Website Password History area - " +
            "Validate that all records are deleted")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod06()
        {
            var websiteID = new Guid("aceee021-8822-eb11-a2cd-005056926fe4"); //Automation - Web Site 08
            var websiteUserID = new Guid("a91e50f6-8822-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser4@mail.com

            //Remove all PasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            var userPasswordHistoryRecordID1 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password1");
            System.Threading.Thread.Sleep(1500);
            var userPasswordHistoryRecordID2 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password2");
            System.Threading.Thread.Sleep(1500);
            var userPasswordHistoryRecordID3 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password3");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Website User Password History Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearWebsiteUserPasswordHistoryJobScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearWebsiteUserPasswordHistoryJobScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 08")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ValidateRecordNotPresent(userPasswordHistoryRecordID1.ToString())
                .ValidateRecordNotPresent(userPasswordHistoryRecordID2.ToString())
                .ValidateRecordNotPresent(userPasswordHistoryRecordID3.ToString());

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-5836")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Password History area - Open a website User Password History record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod07()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ec49a919-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser2@mail.com

            //Remove all PasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            var userPasswordHistoryRecordID1 = dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "Password1");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPasswordHistory();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad()
                .ClickOnWebSiteUserPasswordHistoryRecord(userPasswordHistoryRecordID1.ToString());

            websiteUserPasswordHistoryRecordPage
                .WaitForWebSiteUserPasswordHistoryRecordPageToLoad()
                .ClickOnDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            websiteUserPasswordHistoryPage
                .WaitForWebsiteUserPasswordHistoryPageToLoad();

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5837")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record (Enforce Password History > 0) - Navigate to the Website Users area - Change the Password for the user - " +
            "Validate that the old password is saved in the password history area")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("a1bdff2b-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser3@mail.com

            //Remove all websiteUserPasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickChangePasswordLink();

            var newPassword = DateTime.Now.ToString("ddMMyyyyHHmmss");
            changePasswordPopup
                .WaitForChangePasswordPopupToLoad()
                .InsertNewPassword(newPassword)
                .InsertConfirmNewPassword(newPassword)
                .TapSaveButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.websiteUserPasswordHistory.GetByID(records[0], "websiteuserid", "password");
            Assert.AreEqual(websiteUserID, fields["websiteuserid"]);
            Assert.IsFalse(string.IsNullOrEmpty((string)fields["password"]));
        }

        [TestProperty("JiraIssueID", "CDV6-5838")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record (Enforce Password History = 0) - Navigate to the Website Users area - Change the Password for the user - " +
            "Validate that the old password is NOT saved in the password history area")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod09()
        {
            var websiteID = new Guid("aceee021-8822-eb11-a2cd-005056926fe4"); //Automation - Web Site 08
            var websiteUserID = new Guid("a91e50f6-8822-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser4@mail.com

            //Remove all websiteUserPasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 08")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickChangePasswordLink();

            var newPassword = DateTime.Now.ToString("ddMMyyyyHHmmss");
            changePasswordPopup
                .WaitForChangePasswordPopupToLoad()
                .InsertNewPassword(newPassword)
                .InsertConfirmNewPassword(newPassword)
                .TapSaveButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad();


            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5839")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record (Enforce Password History > 0) - Navigate to the Website Users area - " +
            "Change the Password for the user (Set a password that was already used previously and is saved in the password history) - " +
            "validate that the user is prevented from changing the password")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("91e87387-8a22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser5@mail.com

            //Remove all websiteUserPasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "NEIn+2eV+S6Pfl983813FZW9tpyTPgLPQGmpwxj0jqk=");
            dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "GniiLT3rgaBGhGjmn7EDJASyxW6E7Keg94z/bLFcJeM=");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickChangePasswordLink();

            changePasswordPopup
                .WaitForChangePasswordPopupToLoad()
                .InsertNewPassword("Password_2")
                .InsertConfirmNewPassword("Password_2")
                .TapSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The password you entered has been already used.").TapOKButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad();


            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(2, records.Count);
        }


        [TestProperty("JiraIssueID", "CDV6-5840")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5412 - " +
            "Login in the web app - Open a Website record (Enforce Password History = 0) - Navigate to the Website Users area - " +
            "Change the Password for the user (Set a password that was already used previously and is saved in the password history) - " +
            "validate that the user is allowed to changing the password")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserPasswordHistory_UITestMethod11()
        {
            var websiteID = new Guid("aceee021-8822-eb11-a2cd-005056926fe4"); //Automation - Web Site 08
            var websiteUserID = new Guid("22eeb9c1-8b22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser6@mail.com

            //Remove all websiteUserPasswordHistory records from the user
            foreach (var recordid in dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordHistory.DeleteWebsiteUserPasswordHistory(recordid);

            //Create a new Password History record
            dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "NEIn+2eV+S6Pfl983813FZW9tpyTPgLPQGmpwxj0jqk=");
            dbHelper.websiteUserPasswordHistory.CreateWebsiteUserPasswordHistory(websiteUserID, "GniiLT3rgaBGhGjmn7EDJASyxW6E7Keg94z/bLFcJeM=");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 08")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickChangePasswordLink();

            changePasswordPopup
                .WaitForChangePasswordPopupToLoad()
                .InsertNewPassword("Password_2")
                .InsertConfirmNewPassword("Password_2")
                .TapSaveButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUserPasswordHistory.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(2, records.Count); //Enforce Password History = 0 no additional record is created 
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5414

        [TestProperty("JiraIssueID", "CDV6-5679")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5414 - " +
           "Login in the web app - Open a Website record - Navigate to the Website User Access Audit area - " +
            "Validate that the Website User Access Audit page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserAccessAudit_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUserAccessAudit();

            webSiteUserAccessAuditPage
                .WaitForWebSiteUserAccessAuditPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5680")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5414 - " +
           "Login in the web app - Open a Website record (webbsite has 1 user with Website User Access Audit) - Navigate to the Website User Access Audit area - " +
            "Validate that the Website User Access Audit record is displayed in the page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserAccessAudit_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserAccessAuditId = new Guid("0f2c9100-fe78-46aa-84b7-424f631ccfcd");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUserAccessAudit();

            webSiteUserAccessAuditPage
                .WaitForWebSiteUserAccessAuditPageToLoad()

                .ValidateWebsiteUserCellText(websiteUserAccessAuditId.ToString(), "Hattie Abbott")
                .ValidateBrowserTypeCellText(websiteUserAccessAuditId.ToString(), "Chrome")
                .ValidateBrowserVersionCellText(websiteUserAccessAuditId.ToString(), "87.0.4280.88")
                .ValidateLoginDateTimeCellText(websiteUserAccessAuditId.ToString(), "10/12/2020 10:18:28")
                .ValidateLogoutDateTimeCellText(websiteUserAccessAuditId.ToString(), "10/12/2020 15:18:28")
                .ValidateTokenExpireOnCellText(websiteUserAccessAuditId.ToString(), "15/12/2020 15:18:28")
                .ValidateWebsiteSecurityProfileCellText(websiteUserAccessAuditId.ToString(), "Security Profile 1")
                .ValidateIsMobileDeviceCellText(websiteUserAccessAuditId.ToString(), "No")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5681")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5414 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Users page - Open a Website user record - " +
            "Navigate to the Website User Access Audit tab - Validate that the Website User Access Audit tab area is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserAccessAudit_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserAccessAuditTab();

            webSiteUserAccessAuditTab
                .WaitForWebSiteUserAccessAuditTabToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5682")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5414 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Users page - Open a Website user record (Website User has 1 Website User Access Audit record) - " +
            "Navigate to the Website User Access Audit tab - Validate that the Website User Access Audit record is displayed in the page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserAccessAudit_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com
            var websiteUserAccessAuditId = new Guid("0f2c9100-fe78-46aa-84b7-424f631ccfcd");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserAccessAuditTab();

            webSiteUserAccessAuditTab
                .WaitForWebSiteUserAccessAuditTabToLoad()

                .ValidateWebsiteUserCellText(websiteUserAccessAuditId.ToString(), "Hattie Abbott")
                .ValidateBrowserTypeCellText(websiteUserAccessAuditId.ToString(), "Chrome")
                .ValidateBrowserVersionCellText(websiteUserAccessAuditId.ToString(), "87.0.4280.88")
                .ValidateLoginDateTimeCellText(websiteUserAccessAuditId.ToString(), "10/12/2020 10:18:28")
                .ValidateLogoutDateTimeCellText(websiteUserAccessAuditId.ToString(), "10/12/2020 15:18:28")
                .ValidateTokenExpireOnCellText(websiteUserAccessAuditId.ToString(), "15/12/2020 15:18:28")
                .ValidateWebsiteSecurityProfileCellText(websiteUserAccessAuditId.ToString(), "Security Profile 1")
                .ValidateIsMobileDeviceCellText(websiteUserAccessAuditId.ToString(), "No")
                ;
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-4627

        [TestProperty("JiraIssueID", "CDV6-5683")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Users area - " +
            "Validate that the Users page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5684")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var WebsiteUser1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com
            var WebsiteUser2ID = new Guid("ec49a919-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser2@mail.com
            var WebsiteUser3ID = new Guid("a1bdff2b-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser3@mail.com


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()

                .ValidateUserNameCellText(WebsiteUser1ID.ToString(), "WebSiteAutomationUser1@mail.com")
                .ValidateProfileCellText(WebsiteUser1ID.ToString(), "Hattie Abbott")
                .ValidateStatusCellText(WebsiteUser1ID.ToString(), "Waiting for Approval")
                .ValidateSecurityProfileCellText(WebsiteUser1ID.ToString(), "Security Profile 1")
                .ValidateLastLoginDateCellText(WebsiteUser1ID.ToString(), "11/11/2020 15:04:01")
                .ValidateCreatedByCellText(WebsiteUser1ID.ToString(), "José Brazeta")
                .ValidateCreatedOnCellText(WebsiteUser1ID.ToString(), "06/11/2020 11:57:25")

                .ValidateRecordPresent(WebsiteUser2ID.ToString())
                .ValidateRecordPresent(WebsiteUser3ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5685")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record (all fields must have values) - " +
            "Validate that the user is redirected to the Website User record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var User1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(User1ID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateUserNameFieldText("WebSiteAutomationUser1@mail.com")
                .ValidateProfileFieldLinkText("Hattie Abbott")
                .ValidateStatusFieldText("Waiting for Approval")
                .ValidateSecurityProfileFieldLinkText("Security Profile 1")
                .ValidateEmailVerifiedNoRadioButtonChecked()

                .ValidateLastLoginDateFieldText("11/11/2020", "15:04")
                .ValidateLastPasswordChangedDateFieldText("04/01/2023", "15:02")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("01/11/2020", "15:04")
                .ValidateFailedPasswordAttemptCountFieldText("1")
                .ValidateLastFailedPasswordAttemptDateFieldText("03/11/2020", "15:04")
                .ValidateFailedPINAttemptCountFieldText("2")

                .ValidateTwoFactorAuthenticationTypeSelectedText("SMS")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5686")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the Website User record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var User1ID = new Guid("ec49a919-6d22-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser2@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(User1ID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateUserNameFieldText("WebSiteAutomationUser2@mail.com")
                .ValidateProfileFieldLinkText("Lynnette Abbott")
                .ValidateStatusFieldText("Waiting for Approval")
                .ValidateSecurityProfileFieldLinkText("")
                .ValidateEmailVerifiedNoRadioButtonChecked()

                .ValidateLastLoginDateFieldText("", "")
                .ValidateLastPasswordChangedDateFieldText("04/01/2023", "15:02")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("", "")
                .ValidateFailedPasswordAttemptCountFieldText("")
                .ValidateLastFailedPasswordAttemptDateFieldText("", "")
                .ValidateFailedPINAttemptCountFieldText("")

                .ValidateTwoFactorAuthenticationTypeSelectedText("")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5687")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var User7ID = new Guid("fe592d76-4324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser7@mail.com
            var personid = new Guid("8142A162-FE47-4BFD-92E3-BE966B13CEC4"); //Grady Acevedo
            var personid2 = new Guid("7157c2f9-6e28-4e75-af30-120322f8a0a3"); //Elvin Hensley
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            dbHelper.websiteUser.UpdateWebsiteUser(User7ID, "Grady Acevedo", "WebSiteAutomationUser7@mail.com", "qwertyuiop", false, null, 1, personid, "person", "Grady Acevedo", null, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("*WebSiteAutomationUser7@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(User7ID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Approved")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("12220").TapSearchButton().SelectResultElement(personid2.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Security Profile 1").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("*WebSiteAutomationUser7@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(User7ID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateUserNameFieldText("WebSiteAutomationUser7@mail.com")
                .ValidateProfileFieldLinkText("Elvin Hensley")
                .ValidateStatusFieldText("Approved")
                .ValidateSecurityProfileFieldLinkText("Security Profile 1")
                .ValidateEmailVerifiedYesRadioButtonChecked()

                .ValidateLastLoginDateFieldText("", "")
                .ValidateLastPasswordChangedDateFieldText("04/01/2023", "15:02")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("", "")
                .ValidateFailedPasswordAttemptCountFieldText("")
                .ValidateLastFailedPasswordAttemptDateFieldText("", "")
                .ValidateFailedPINAttemptCountFieldText("")

                .ValidateTwoFactorAuthenticationTypeSelectedText("SMS");
        }

        [TestProperty("JiraIssueID", "CDV6-5688")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record - " +
            "Remove the values from the mandatory fields - Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod07()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var User7ID = new Guid("fe592d76-4324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser7@mail.com
            var personid = new Guid("8142A162-FE47-4BFD-92E3-BE966B13CEC4"); //Grady Acevedo
            var personid2 = new Guid("7157c2f9-6e28-4e75-af30-120322f8a0a3"); //Elvin Hensley
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            dbHelper.websiteUser.UpdateWebsiteUser(User7ID, "Grady Acevedo", "WebSiteAutomationUser7@mail.com", "qwertyuiop", false, null, 1, personid, "person", "Grady Acevedo", null, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("WebSiteAutomationUser7@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(User7ID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickProfileRemoveButton()
                .SelectStatus("")
                .ClickSaveButton()

                .ValidateProfileFieldErrorLabelVisibility(true)
                .ValidateProfileFieldErrorLabelText("Please fill out this field.")
                .ValidateStatusFieldErrorLabelVisibility(true)
                .ValidateStatusFieldErrorLabelText("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "CDV6-5689")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var personid = new Guid("52732b32-5264-43ac-8222-cb58e2c1124a"); //Lora Castro
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUser8@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("12221").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Security Profile 1").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com");
            Assert.AreEqual(1, records.Count);

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(records[0].ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateUserNameFieldText("WebSiteAutomationUser8@mail.com")
                .ValidateProfileFieldLinkText("Lora Castro")
                .ValidateStatusFieldText("Waiting for Approval")
                .ValidateSecurityProfileFieldLinkText("Security Profile 1")
                .ValidateEmailVerifiedYesRadioButtonChecked()

                .ValidateLastLoginDateFieldText("", "")
                .ValidateLastPasswordChangedDateFieldText("", "")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("", "")
                .ValidateFailedPasswordAttemptCountFieldText("")
                .ValidateLastFailedPasswordAttemptDateFieldText("", "")
                .ValidateFailedPINAttemptCountFieldText("")

                .ValidateTwoFactorAuthenticationTypeSelectedText("SMS");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-5690")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (set profile to a professional) - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod08_1()
        {
            var websiteID = new Guid("afbacf66-79da-eb11-a325-005056926fe4"); //Automation - Web Site 18
            var professionalid = new Guid("8cc6b6e9-0925-eb11-a2cd-005056926fe4"); //Mr Antony McCan
            var securityProfile = new Guid("70f13fb1-79da-eb11-a325-005056926fe4"); //Website18 Full Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 18")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUser8@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectBusinessObjectByText("Professionals").TypeSearchQuery("mccan").TapSearchButton().SelectResultElement(professionalid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Website18 Full Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com");
            Assert.AreEqual(1, records.Count);

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(records[0].ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()

                .ValidateWebSiteFieldLinkText("Automation - Web Site 18")
                .ValidateUserNameFieldText("WebSiteAutomationUser8@mail.com")
                .ValidateProfileFieldLinkText("Mr Antony McCan")
                .ValidateStatusFieldText("Waiting for Approval")
                .ValidateSecurityProfileFieldLinkText("Website18 Full Access")
                .ValidateEmailVerifiedYesRadioButtonChecked()

                .ValidateLastLoginDateFieldText("", "")
                .ValidateLastPasswordChangedDateFieldText("", "")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("", "")
                .ValidateFailedPasswordAttemptCountFieldText("")
                .ValidateLastFailedPasswordAttemptDateFieldText("", "")
                .ValidateFailedPINAttemptCountFieldText("")

                .ValidateTwoFactorAuthenticationTypeSelectedText("SMS");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-5691")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in mandatory fields only - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod09()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var personid = new Guid("52732b32-5264-43ac-8222-cb58e2c1124a"); //Lora Castro
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUser8@mail.com")
                .InsertPassword("Passw0rd_!")
                .SelectStatus("Waiting for Approval")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("12221").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com");
            Assert.AreEqual(1, records.Count);

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(records[0].ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateUserNameFieldText("WebSiteAutomationUser8@mail.com")
                .ValidateProfileFieldLinkText("Lora Castro")
                .ValidateStatusFieldText("Waiting for Approval")
                .ValidateSecurityProfileFieldLinkText("")
                .ValidateEmailVerifiedNoRadioButtonChecked()

                .ValidateLastLoginDateFieldText("", "")
                .ValidateLastPasswordChangedDateFieldText("", "")
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateLockedOutDateFieldText("", "")
                .ValidateFailedPasswordAttemptCountFieldText("")
                .ValidateLastFailedPasswordAttemptDateFieldText("", "")
                .ValidateFailedPINAttemptCountFieldText("")

                .ValidateTwoFactorAuthenticationTypeSelectedText("");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-5692")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Tap on the add new record button - " +
            "Dont set any data in the mandatory fields - Tap on the save button - Validate that the record is NOT saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var personid = new Guid("52732b32-5264-43ac-8222-cb58e2c1124a"); //Lora Castro
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertPassword("Passw0rd_!")
                .ClickSaveAndCloseButton()

                .ValidateUserNameFieldErrorLabelVisibility(true)
                .ValidateUserNameFieldErrorLabelText("Please fill out this field.")
                .ValidateProfileFieldErrorLabelVisibility(true)
                .ValidateProfileFieldErrorLabelText("Please fill out this field.")
                .ValidateStatusFieldErrorLabelVisibility(false);

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser8@mail.com");
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-5693")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod11()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var personid = new Guid("9fff48cd-3772-4a95-989d-b7e6ecf583e3"); //Rosalyn Petersen
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser9@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            var userID = dbHelper.websiteUser.CreateWebsiteUser(websiteID, "Rosalyn Petersen", "WebSiteAutomationUser9@mail.com", "qwertyuiop", true, 1, personid, "person", "Rosalyn Petersen", securityProfile);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("WebSiteAutomationUser9@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(userID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser9@mail.com");
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5694")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Select the 'Users Waiting For Approval' view - " +
            "Validate that only the records with the matching status are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod12()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var WebsiteUser1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com (Waiting for Approval)
            var WebsiteUser2ID = new Guid("cc861ade-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser10@mail.com (De-Activated User)
            var WebsiteUser3ID = new Guid("b6de7ff2-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser11@mail.com (Approved User)
            var WebsiteUser4ID = new Guid("d7d3e206-d324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser12@mail.com (Suspended User)


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .SelectView("Users Waiting For Approval")

                .ValidateRecordPresent(WebsiteUser1ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser2ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser3ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser4ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5695")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Select the 'Account De-Activated Users' view - " +
            "Validate that only the records with the matching status are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod13()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var WebsiteUser1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com (Waiting for Approval)
            var WebsiteUser2ID = new Guid("cc861ade-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser10@mail.com (De-Activated User)
            var WebsiteUser3ID = new Guid("b6de7ff2-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser11@mail.com (Approved User)
            var WebsiteUser4ID = new Guid("d7d3e206-d324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser12@mail.com (Suspended User)


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .SelectView("Account De-Activated Users")

                .ValidateRecordNotPresent(WebsiteUser1ID.ToString())
                .ValidateRecordPresent(WebsiteUser2ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser3ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser4ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5696")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Select the 'Approved Users' view - " +
            "Validate that only the records with the matching status are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod14()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var WebsiteUser1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com (Waiting for Approval)
            var WebsiteUser2ID = new Guid("cc861ade-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser10@mail.com (De-Activated User)
            var WebsiteUser3ID = new Guid("b6de7ff2-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser11@mail.com (Approved User)
            var WebsiteUser4ID = new Guid("d7d3e206-d324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser12@mail.com (Suspended User)


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .SelectView("Approved Users")

                .ValidateRecordNotPresent(WebsiteUser1ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser2ID.ToString())
                .ValidateRecordPresent(WebsiteUser3ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser4ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5697")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Select the 'Suspended Users' view - " +
            "Validate that only the records with the matching status are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod15()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var WebsiteUser1ID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com (Waiting for Approval)
            var WebsiteUser2ID = new Guid("cc861ade-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser10@mail.com (De-Activated User)
            var WebsiteUser3ID = new Guid("b6de7ff2-d224-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser11@mail.com (Approved User)
            var WebsiteUser4ID = new Guid("d7d3e206-d324-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser12@mail.com (Suspended User)


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .SelectView("Suspended Users")

                .ValidateRecordNotPresent(WebsiteUser1ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser2ID.ToString())
                .ValidateRecordNotPresent(WebsiteUser3ID.ToString())
                .ValidateRecordPresent(WebsiteUser4ID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-5698")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record (Is Account Locked set to Yes) - Tap on the Unlock button - " +
            "Validate that the website user account gets unlocked")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod16()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var personid = new Guid("800aa31d-072b-4fa4-8b32-640ffcbc6ace"); //Deana Acosta
            var securityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            //remove all matching Users
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser13@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteuserid))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);
            }

            var userID = dbHelper.websiteUser.CreateWebsiteUser(websiteID, "Rosalyn Petersen", "WebSiteAutomationUser13@mail.com", "qwertyuiop", true, 1, personid, "person", "Rosalyn Petersen", securityProfile);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("WebSiteAutomationUser13@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(userID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickUnlockButton()
                .WaitForWebSiteUserRecordPageToLoad()
                .ValidateIsAccountLockedNoRadioButtonChecked();
        }

        [TestProperty("JiraIssueID", "CDV6-5699")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record - Tap on the Password Reset button - " +
            "Validate that a password reset record is created")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUsers_UITestMethod17()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("70867865-d624-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser14@mail.com

            //remove all matching Users
            foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("WebSiteAutomationUser14@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickResetPasswordButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Reset Password record has been successfully created").TapCloseButton();

            var records = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5445

        [TestProperty("JiraIssueID", "CDV6-5700")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (No widget is set in the page designer) - " +
            "Validate that the 'Website Page Designer' area is rendered empty")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod01()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage1ID = new Guid("32ac0903-0c24-eb11-a2cd-005056926fe4"); //Page_1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage1ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5701")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created but not set up) - " +
            "Validate that the 'Website Page Designer' area is rendered and the widget is visible")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod02()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage2ID = new Guid("3bac0903-0c24-eb11-a2cd-005056926fe4"); //Page_2

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage2ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            System.Threading.Thread.Sleep(2000);

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ValidateWidgetTitle("1", "Not Setup Widget");
        }

        [TestProperty("JiraIssueID", "CDV6-5702")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created but not set up) - " +
            "Tap on the widget settings button - Validate that the widget settings popup is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod03()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage2ID = new Guid("3bac0903-0c24-eb11-a2cd-005056926fe4"); //Page_2

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage2ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()
                .ValidateWidgetTypeFieldVisibility(true)
                .ValidateRecordTypeFieldVisibility(false)
                .ValidateFormFieldVisibility(false)
                .ValidateWidgetTitleFieldVisibility(true)
                .ValidateViewFieldVisibility(false)
                .ValidateWidgetFieldVisibility(false)
                .ValidateHTMLFileFieldVisibility(false)
                .ValidateStylesheetFileFieldVisibility(false)
                .ValidateScriptFileFieldVisibility(false)

                .ValidateWidgetTypeSelectedValue("");
        }

        [TestProperty("JiraIssueID", "CDV6-5703")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as DataForm) - " +
            "Tap on the widget settings button - Validate that the widget settings popup is displayed with the correct information setup")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod04()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage3ID = new Guid("ab904b0c-0c24-eb11-a2cd-005056926fe4"); //Page_3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage3ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()
                .ValidateWidgetTypeFieldVisibility(true)
                .ValidateRecordTypeFieldVisibility(true)
                .ValidateFormFieldVisibility(true)
                .ValidateWidgetTitleFieldVisibility(true)
                .ValidateViewFieldVisibility(false)
                .ValidateWidgetFieldVisibility(false)
                .ValidateHTMLFileFieldVisibility(false)
                .ValidateStylesheetFileFieldVisibility(false)
                .ValidateScriptFileFieldVisibility(false)

                .ValidateWidgetTypeSelectedValue("DataForm")
                .ValidateRecordTypeSelectedValue("Person")
                .ValidateFormSelectedValue("Registration");


        }

        [TestProperty("JiraIssueID", "CDV6-5704")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as List View) - " +
            "Tap on the widget settings button - Validate that the widget settings popup is displayed with the correct information setup")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod05()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage3ID = new Guid("ab904b0c-0c24-eb11-a2cd-005056926fe4"); //Page_3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage3ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("2");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()
                .ValidateWidgetTypeFieldVisibility(true)
                .ValidateRecordTypeFieldVisibility(true)
                .ValidateFormFieldVisibility(false)
                .ValidateWidgetTitleFieldVisibility(true)
                .ValidateViewFieldVisibility(true)
                .ValidateWidgetFieldVisibility(false)
                .ValidateHTMLFileFieldVisibility(false)
                .ValidateStylesheetFileFieldVisibility(false)
                .ValidateScriptFileFieldVisibility(false)

                .ValidateWidgetTypeSelectedValue("List View")
                .ValidateRecordTypeSelectedValue("Case")
                .ValidateViewSelectedValue("Main");


        }

        [TestProperty("JiraIssueID", "CDV6-5705")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as Widget) - " +
            "Tap on the widget settings button - Validate that the widget settings popup is displayed with the correct information setup")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod06()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage3ID = new Guid("ab904b0c-0c24-eb11-a2cd-005056926fe4"); //Page_3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage3ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("3");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()
                .ValidateWidgetTypeFieldVisibility(true)
                .ValidateRecordTypeFieldVisibility(false)
                .ValidateFormFieldVisibility(false)
                .ValidateWidgetTitleFieldVisibility(true)
                .ValidateViewFieldVisibility(false)
                .ValidateWidgetFieldVisibility(true)
                .ValidateHTMLFileFieldVisibility(false)
                .ValidateStylesheetFileFieldVisibility(false)
                .ValidateScriptFileFieldVisibility(false)

                .ValidateWidgetTypeSelectedValue("Widget")
                .ValidateWidgetSelectedValue("Automation - Widget Footer 1");


        }

        [TestProperty("JiraIssueID", "CDV6-5706")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as Html) - " +
            "Tap on the widget settings button - Validate that the widget settings popup is displayed with the correct information setup")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod07()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage3ID = new Guid("ab904b0c-0c24-eb11-a2cd-005056926fe4"); //Page_3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage3ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("4");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()
                .ValidateWidgetTypeFieldVisibility(true)
                .ValidateRecordTypeFieldVisibility(false)
                .ValidateFormFieldVisibility(false)
                .ValidateWidgetTitleFieldVisibility(true)
                .ValidateViewFieldVisibility(false)
                .ValidateWidgetFieldVisibility(false)
                .ValidateHTMLFileFieldVisibility(true)
                .ValidateStylesheetFileFieldVisibility(false)
                .ValidateScriptFileFieldVisibility(false)

                .ValidateWidgetTypeSelectedValue("Html")
                .ValidateHTMLFileSelectedValue("File.htm");


        }

        [TestProperty("JiraIssueID", "CDV6-5707")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as DataForm) - " +
            "Update the widget (change to a List View) - Save the changes - Validate that the widget is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod08()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage4ID = new Guid("283b16ac-e224-eb11-a2cd-005056926fe4"); //Page_4

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"Person - Portal Form\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\"}}]}";

            //update widget and set default values
            dbHelper.websitePage.UpdateWebsitePage(websitePage4ID, "Page_4", null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage4ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()

                .SelectWidgetType("List View")

                .ValidateFormFieldVisibility(false)

                .SelectRecordType("Case")
                .SelectView("Main")
                .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Case - Main\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\",\"ListActionId\":1}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage4ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);

        }

        [TestProperty("JiraIssueID", "CDV6-5708")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as DataForm) - " +
            "Update the widget (change to a Widget) - Save the changes - Validate that the widget is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod09()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage4ID = new Guid("283b16ac-e224-eb11-a2cd-005056926fe4"); //Page_4

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"Person - Portal Form\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\"}}]}";

            //update widget and set default values
            dbHelper.websitePage.UpdateWebsitePage(websitePage4ID, "Page_4", null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage4ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()

                .SelectWidgetType("Widget")

                .ValidateRecordTypeFieldVisibility(false)
                .ValidateFormFieldVisibility(false)

                .SelectWidget("Automation - Widget Footer 1")
                .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Automation - Widget Footer 1\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":3,\"WebsiteWidgetId\":\"6c8d7a21-8a3e-4568-9b05-8c0f272b7af4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage4ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);

        }

        [TestProperty("JiraIssueID", "CDV6-5709")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created and set up as DataForm) - " +
            "Update the widget (change to a Html) - Save the changes - Validate that the widget is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod10()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage4ID = new Guid("283b16ac-e224-eb11-a2cd-005056926fe4"); //Page_4

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"Person - Portal Form\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\"}}]}";

            //update widget and set default values
            dbHelper.websitePage.UpdateWebsitePage(websitePage4ID, "Page_4", null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage4ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
                .WaitForWebsitePageWidgetSettingsPopupToLoad()

                .SelectWidgetType("Html")

                .ValidateRecordTypeFieldVisibility(false)
                .ValidateFormFieldVisibility(false)

                .SelectHTMLFile("File.htm")
                .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"File.htm\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":4,\"HtmlFileId\":\"d7033b8a-d724-eb11-a2cd-005056926fe4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage4ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);

        }

        [TestProperty("JiraIssueID", "CDV6-5710")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (one widget is created) - " +
            "Delete the Widget - Save the changes - Validate that the widget is correctly deleted")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod11()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage4ID = new Guid("283b16ac-e224-eb11-a2cd-005056926fe4"); //Page_4

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"Person - Portal Form\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\",\"ScriptFileId\":\"77e48a96-d724-eb11-a2cd-005056926fe4\",\"StylesheetFileId\":\"42c164a6-d724-eb11-a2cd-005056926fe4\"}}]}";

            //update widget and set default values
            dbHelper.websitePage.UpdateWebsitePage(websitePage4ID, "Page_4", null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage4ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetDeleteButton("1");

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage4ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);

        }

        [TestProperty("JiraIssueID", "CDV6-5711")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (two widget are created) - " +
            "Delete one Widget - Save the changes - Validate that the widget is correctly deleted and the other widget is not changed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod12()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var websitePage4ID = new Guid("283b16ac-e224-eb11-a2cd-005056926fe4"); //Page_4

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"Person - Portal Form\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\",\"ScriptFileId\":\"77e48a96-d724-eb11-a2cd-005056926fe4\",\"StylesheetFileId\":\"42c164a6-d724-eb11-a2cd-005056926fe4\"}},{\"title\":\"Case - Main\",\"x\":3.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\",\"ScriptFileId\":\"77e48a96-d724-eb11-a2cd-005056926fe4\",\"StylesheetFileId\":\"42c164a6-d724-eb11-a2cd-005056926fe4\"}}]}";

            //update widget and set default values
            dbHelper.websitePage.UpdateWebsitePage(websitePage4ID, "Page_4", null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage4ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickWidgetDeleteButton("1");

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Case - Main\",\"x\":3.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":null,\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage4ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-5712")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (without widgets) - " +
            "Add a new widget (DataForm) - Save the widget - Validate that the widget is correctly Saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod13()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09

            var defaultWidgetLayout = "{\"Widgets\":[]}";

            //update widget and set default values
            foreach (var pageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_5"))
                dbHelper.websitePage.DeleteWebsitePage(pageid);

            var websitePage5ID = dbHelper.websitePage.CreateWebsitePage("Page_5", websiteID, null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage5ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickAddWidgetButton()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
               .WaitForWebsitePageWidgetSettingsPopupToLoad()

               .SelectWidgetType("DataForm")
               .SelectRecordType("Person")
               .SelectForm("Registration")
               .SelectFormSaveAction("Show success message")
               .SelectLocalizedString("SaveForm.SucceessMessage")
               .InsertId("UI_Test")
               .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Person - Registration\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\",\"DataFormSaveActionId\":2,\"LocalizedStringId\":\"b6d63164-ef75-eb11-a30c-005056926fe4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage5ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-5713")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (page already contains one widgets) - " +
            "Add a new widget (DataForm) - Save the widget - Validate that the widget is correctly Saved (and the previous one is unchanged)")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod14()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09

            var defaultWidgetLayout = "{\"Widgets\":[{\"title\":\"File.htm\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Type\":4,\"HtmlFileId\":\"d7033b8a-d724-eb11-a2cd-005056926fe4\",\"ScriptFileId\":\"77e48a96-d724-eb11-a2cd-005056926fe4\",\"StylesheetFileId\":\"42c164a6-d724-eb11-a2cd-005056926fe4\"}}]}";

            //update widget and set default values
            foreach (var pageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_5"))
                dbHelper.websitePage.DeleteWebsitePage(pageid);

            var websitePage5ID = dbHelper.websitePage.CreateWebsitePage("Page_5", websiteID, null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage5ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickAddWidgetButton()
                .ClickWidgetSettingsButton("2");

            websitePageWidgetSettingsPopup
               .WaitForWebsitePageWidgetSettingsPopupToLoad()

               .SelectWidgetType("DataForm")
               .SelectRecordType("Person")
               .SelectForm("Registration")
               .SelectFormSaveAction("Show success message")
               .SelectLocalizedString("SaveForm.SucceessMessage")
               .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"File.htm\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":null,\"Type\":4,\"HtmlFileId\":\"d7033b8a-d724-eb11-a2cd-005056926fe4\"}},{\"title\":\"Person - Registration\",\"x\":3.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":1,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataFormId\":\"3e0a350e-6722-eb11-a2ce-0050569231cf\",\"DataFormSaveActionId\":2,\"LocalizedStringId\":\"b6d63164-ef75-eb11-a30c-005056926fe4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage5ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-5714")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (without widgets) - " +
            "Add a new widget (List View) - Save the widget - Validate that the widget is correctly Saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod15()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09

            var defaultWidgetLayout = "{\"Widgets\":[]}";

            //update widget and set default values
            foreach (var pageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_5"))
                dbHelper.websitePage.DeleteWebsitePage(pageid);

            var websitePage5ID = dbHelper.websitePage.CreateWebsitePage("Page_5", websiteID, null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage5ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickAddWidgetButton()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
               .WaitForWebsitePageWidgetSettingsPopupToLoad()

               .SelectWidgetType("List View")
               .SelectRecordType("Case")
               .SelectView("Main")
               .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Case - Main\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\",\"ListActionId\":1}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage5ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-5715")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (without widgets) - " +
            "Add a new widget (Widget) - Save the widget - Validate that the widget is correctly Saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod16()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09

            var defaultWidgetLayout = "{\"Widgets\":[]}";

            //update widget and set default values
            foreach (var pageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_5"))
                dbHelper.websitePage.DeleteWebsitePage(pageid);

            var websitePage5ID = dbHelper.websitePage.CreateWebsitePage("Page_5", websiteID, null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage5ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickAddWidgetButton()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
               .WaitForWebsitePageWidgetSettingsPopupToLoad()

               .SelectWidgetType("Widget")
               .SelectWidget("Automation - Widget Footer 1")
               .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"Automation - Widget Footer 1\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":3,\"WebsiteWidgetId\":\"6c8d7a21-8a3e-4568-9b05-8c0f272b7af4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage5ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-5716")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5445 - " +
            "Login in the web app - Open a Website record - Navigate to the Website pages area - Open a Website page record (without widgets) - " +
            "Add a new widget (Html) - Save the widget - Validate that the widget is correctly Saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePageDesigner_UITestMethod17()
        {
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09

            var defaultWidgetLayout = "{\"Widgets\":[]}";

            //update widget and set default values
            foreach (var pageid in dbHelper.websitePage.GetByWebSiteIDAndPageName(websiteID, "Page_5"))
                dbHelper.websitePage.DeleteWebsitePage(pageid);

            var websitePage5ID = dbHelper.websitePage.CreateWebsitePage("Page_5", websiteID, null, null, defaultWidgetLayout);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 09")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePages();

            webSitePagesPage
                .WaitForWebSitePagesPageToLoad()
                .ClickOnWebSitePageRecord(websitePage5ID.ToString());

            webSitePageRecordPage
                .WaitForWebSitePageRecordPageToLoad()
                .ClickDesignerTab();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickAddWidgetButton()
                .ClickWidgetSettingsButton("1");

            websitePageWidgetSettingsPopup
               .WaitForWebsitePageWidgetSettingsPopupToLoad()

               .SelectWidgetType("Html")
               .SelectHTMLFile("File.htm")
               .InsertId("UI_Test")
                .TapSaveSettingsButton();

            webSitePageRecordDesignerSubPage
                .WaitForWebSitePageRecordDesignerSubPageToLoad()
                .ClickSaveButton();

            string expectedLayout = "{\"Widgets\":[{\"title\":\"File.htm\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"UI_Test\",\"Type\":4,\"HtmlFileId\":\"d7033b8a-d724-eb11-a2cd-005056926fe4\"}}]}";

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.websitePage.GetByID(websitePage5ID, "LayoutJson");
            Assert.AreEqual(expectedLayout, fields["layoutjson"]);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-4640

        [TestProperty("JiraIssueID", "CDV6-5717")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a person record (with no primary email address) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Person' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that NO website user record is created")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod01()
        {

            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var personID = new Guid("9832341c-4fd1-4ce7-b693-d38ada451dbe");  //Ernestine Acosta
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("171512", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Ernestine Acosta")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Ernestine Acosta");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the person has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(0, users.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5718")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a person record (has primary email address but no mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Person' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that a website user record is created and linked to the person record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod02()
        {

            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var personID = new Guid("7d5ab042-1652-4928-88a7-cc835c2b741b");  //Jeff Cote
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("171513", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeff Cote")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeff Cote");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the person has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            Assert.AreEqual("Jeff Cote", fields["name"]);
            Assert.AreEqual("JeffCote914199316@temp.com", fields["username"]);
            Assert.AreEqual(false, fields["emailverified"]);
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(personID, fields["profileid"]);
            Assert.AreEqual("person", fields["profileidtablename"]);
            Assert.AreEqual("Jeff Cote", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));

        }

        [TestProperty("JiraIssueID", "CDV6-5719")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a person record (has primary email and mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Person' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that a website user record is created and linked to the person record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod03()
        {

            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var personID = new Guid("05fa7f99-f29b-4e29-82cf-85e79b72d708");  //Jeannine Cooke
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("171514", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeannine Cooke")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeannine Cooke");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the person has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("a86f783e-2e24-eb11-a2cd-005056926fe4"); //Security Profile 1

            Assert.AreEqual("Jeannine Cooke", fields["name"]);
            Assert.AreEqual("JeannineCooke649170360@temp.com", fields["username"]);
            Assert.AreEqual(false, fields["emailverified"]);
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(personID, fields["profileid"]);
            Assert.AreEqual("person", fields["profileidtablename"]);
            Assert.AreEqual("Jeannine Cooke", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));
        }

        [TestProperty("JiraIssueID", "CDV6-5720")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a person record (has primary email and mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Person' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that a website user record is created and the user password reset record has a timout of 20 minutes")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod04()
        {

            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var personID = new Guid("05fa7f99-f29b-4e29-82cf-85e79b72d708");  //Jeannine Cooke
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("171514", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeannine Cooke")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Jeannine Cooke");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the person has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, users.Count);

            var passwordResets = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(users[0]);
            Assert.AreEqual(1, passwordResets.Count);

            //we should have a 20 minutes difference
            var fields = dbHelper.websiteUserPasswordReset.GetByID(passwordResets[0], "createdon", "expireon");
            var seton = (DateTime)fields["createdon"];
            var expireon = (DateTime)fields["expireon"];
            var difference = (expireon - seton);
            Assert.AreEqual(20, difference.Minutes);


        }

        [TestProperty("JiraIssueID", "CDV6-5721")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a professional record (with no primary email address) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Professional' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that NO website user record is created")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod05()
        {

            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var professionalID = new Guid("e615df19-a025-eb11-a2cd-005056926fe4");  //Cristiano McQuin
            var workflowid = new Guid("491ee1f7-0a25-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Professional

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProfessionalsSection();

            professionalsPage.
                WaitForProfessionalsPageToLoad()
                .SearchProfessionalRecord("Cristiano McQuin", professionalID.ToString())
                .OpenProfessionalRecord(professionalID.ToString());

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad();


            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the professional has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(0, users.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5722")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a professional record (has primary email address but no mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Professional' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that a website user record is created and linked to the professional record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod06()
        {

            var websiteID = new Guid("afbacf66-79da-eb11-a325-005056926fe4"); //Automation - Web Site 18
            var professionalID = new Guid("3d1c4007-a325-eb11-a2cd-005056926fe4");  //Joana MCFin
            var workflowid = new Guid("491ee1f7-0a25-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Professional

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProfessionalsSection();

            professionalsPage.
                WaitForProfessionalsPageToLoad()
                .SearchProfessionalRecord("Joana MCFin", professionalID.ToString())
                .OpenProfessionalRecord(professionalID.ToString());

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad();

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the professional has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("70f13fb1-79da-eb11-a325-005056926fe4"); //Website18 Full Access

            Assert.AreEqual("Joana MCFin", fields["name"]);
            Assert.AreEqual("JoanaMcFin@fakemail.com", fields["username"]);
            Assert.AreEqual(false, fields["emailverified"]);
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(professionalID, fields["profileid"]);
            Assert.AreEqual("professional", fields["profileidtablename"]);
            Assert.AreEqual("Joana MCFin", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));

        }

        [TestProperty("JiraIssueID", "CDV6-5723")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4640 - " +
            "Open a Professional record (has primary email and mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Automation - Website On Demand Workflow - Professional' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - Validate that a website user record is created and linked to the Professional record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUser_OnDemandWorkflow_UITestMethod07()
        {

            var websiteID = new Guid("afbacf66-79da-eb11-a325-005056926fe4"); //Automation - Web Site 18
            var ProfessionalID = new Guid("5c35e93a-a425-eb11-a2cd-005056926fe4");  //Peter McFly
            var workflowid = new Guid("491ee1f7-0a25-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Professional

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteID(websiteID))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProfessionalsSection();

            professionalsPage.
                WaitForProfessionalsPageToLoad()
                .SearchProfessionalRecord("Peter McFly", ProfessionalID.ToString())
                .OpenProfessionalRecord(ProfessionalID.ToString());

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad()
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            professionalRecordPage
                .WaitForProfessionalRecordPageToLoad();

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have no website user records because the Professional has no email address
            var users = dbHelper.websiteUser.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("70f13fb1-79da-eb11-a325-005056926fe4"); //Website18 Full Access

            Assert.AreEqual("Peter McFly", fields["name"]);
            Assert.AreEqual("PeterMcFly@fakemail.com", fields["username"]);
            Assert.AreEqual(false, fields["emailverified"]);
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(ProfessionalID, fields["profileid"]);
            Assert.AreEqual("professional", fields["profileidtablename"]);
            Assert.AreEqual("Peter McFly", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5442

        [TestProperty("JiraIssueID", "CDV6-5724")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - " +
            "Validate that the Website Sitemaps page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod01()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5725")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod02()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10

            var Sitemap1ID = new Guid("d81a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 01
            var Sitemap2ID = new Guid("da1a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 02


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()

                .ValidateNameCellText(Sitemap1ID.ToString(), "Sitemap 01")
                .ValidateWebsiteCellText(Sitemap1ID.ToString(), "Automation - Web Site 10")
                .ValidateTypeCellText(Sitemap1ID.ToString(), "Anonymous User")
                .ValidateCreatedByCellText(Sitemap1ID.ToString(), "Security Test User Admin")
                .ValidateCreatedOnCellText(Sitemap1ID.ToString(), "13/11/2020 14:55:23")

                .ValidateNameCellText(Sitemap2ID.ToString(), "Sitemap 02")
                .ValidateWebsiteCellText(Sitemap2ID.ToString(), "Automation - Web Site 10")
                .ValidateTypeCellText(Sitemap2ID.ToString(), "Authenticated User")
                .ValidateCreatedByCellText(Sitemap2ID.ToString(), "Security Test User Admin")
                .ValidateCreatedOnCellText(Sitemap2ID.ToString(), "13/11/2020 14:55:23")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5726")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record - " +
            "Validate that the user is redirected to the WebSite Sitemap record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod03()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("d81a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .ValidateNameFieldText("Sitemap 01")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 10")
                .ValidateTypeValueFieldText("Anonymous User");
        }

        [TestProperty("JiraIssueID", "CDV6-5727")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record - " +
            "Update all fields - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod04()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[]}", 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .InsertName("Sitemap 01 - Update")
                .SelectType("Authenticated User")
                .ClickSaveAndCloseButton();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .ValidateNameFieldText("Sitemap 01 - Update")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 11")
                .ValidateTypeValueFieldText("Authenticated User");

        }

        [TestProperty("JiraIssueID", "CDV6-5728")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record - " +
            "Remove the values from the mandatory fields - Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod05()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[]}", 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .InsertName("")
                .SelectType("")
                .ClickSaveButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateTypeFieldErrorLabelVisibility(true)
                .ValidateTypeFieldErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-5729")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod06()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .InsertName("Sitemap 01")
                .SelectType("Authenticated User")
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteSitemap.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, records.Count);

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(records[0].ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .ValidateNameFieldText("Sitemap 01")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 11")
                .ValidateTypeValueFieldText("Authenticated User");

            var fields = dbHelper.websiteSitemap.GetByID(records[0], "sitemapjson");
            Assert.IsFalse(fields.ContainsKey("sitemapjson"));
        }

        [TestProperty("JiraIssueID", "CDV6-5730")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record (already has 1 sitemap of type 'Authenticated User') - " +
            "Navigate to the Website Sitemaps area - Tap on the add new record button - " +
            "Set data in all fields (set Type to 'Authenticated User' ) - Tap on the save button - Validate that the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod07()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .InsertName("Sitemap 02")
                .SelectType("Authenticated User")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Website Sitemap with same combination already exist: Website = Automation - Web Site 11 AND Type = Authenticated User.").TapCloseButton();

            var records = dbHelper.websiteSitemap.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-5731")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Tap on the add new record button - " +
            "Dont set any data in the mandatory fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod08()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClicAddNewRecordButton();

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .ClickSaveAndCloseButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateTypeFieldErrorLabelVisibility(true)
                .ValidateTypeFieldErrorLabelText("Please fill out this field.");

            var records = dbHelper.websiteSitemap.GetByWebSiteID(websiteID);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-5732")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a website Sitemap record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod09()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDetailsTab()
                .WaitForDetailsTabToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var records = dbHelper.websiteSitemap.GetByWebSiteID(websiteID);
            Assert.AreEqual(0, records.Count);
        }





        [TestProperty("JiraIssueID", "CDV6-5733")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record - " +
            "Tap on the Designer Tab - Validate that the user is redirected to the designer page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod10()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("d81a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad();


        }

        [TestProperty("JiraIssueID", "CDV6-5734")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with no nodes) - " +
            "Tap on the Designer Tab - Validate that only the new note button is visible")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod11()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("d81a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ValidateNewNodeButtonVisibility(true)
                .ValidateNewChildNodeButtonVisibility(false, "1")

                .ValidateNodeLinkVisibility(false, "1")

                .ValidateTypeFieldVisibility(false)
                .ValidateTitleFieldVisibility(false)
                .ValidatePageFieldVisibility(false)
                .ValidateLinkFieldVisibility(false)
                .ValidateDisplayOrderFieldVisibility(false);


        }

        [TestProperty("JiraIssueID", "CDV6-5735")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes for pages and links) - " +
            "Tap on the Designer Tab - Validate the position of all nods, sub-nods, links and sub-links. Validate that by default the DetailsContainer area is not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod12()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("da1a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ValidateNewNodeButtonVisibility(true);
            webSiteSitemapRecordDesignerPage
                .ValidateNewChildNodeButtonVisibility(true, "1")
                .ValidateNewChildNodeButtonVisibility(true, "2")
                .ValidateNewChildNodeButtonVisibility(true, "3")
                .ValidateNewChildNodeButtonVisibility(true, "4")
                .ValidateNewChildNodeButtonVisibility(true, "5");
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkVisibility(true, "1")
                .ValidateChildNodeLinkVisibility(true, "1", "1")
                .ValidateChildNodeLinkVisibility(true, "1", "2")
                .ValidateChildNodeLinkVisibility(false, "1", "3"); //non existing node
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkVisibility(true, "2")
                .ValidateChildNodeLinkVisibility(false, "2", "1"); //non existing node
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkVisibility(true, "3")
                .ValidateChildNodeLinkVisibility(false, "3", "1"); //non existing node

            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkVisibility(true, "4")
                .ValidateChildNodeLinkVisibility(true, "4", "1")
                .ValidateChildNodeLinkVisibility(true, "4", "2")
                .ValidateChildNodeLinkVisibility(false, "4", "3"); //non existing node
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkVisibility(true, "5")
                .ValidateChildNodeLinkVisibility(false, "5", "1"); //non existing node

            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkText("1", "Page 1")
                .ValidateChildNodeLinkText("1", "1", "Page 1.1")
                .ValidateChildNodeLinkText("1", "2", "Page 1.2");
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkText("2", "Page 2");
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkText("3", "Page 3");
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkText("4", "Link 1")
                .ValidateChildNodeLinkText("4", "1", "Link 1.1")
                .ValidateChildNodeLinkText("4", "2", "Link 1.2");
            webSiteSitemapRecordDesignerPage
                .ValidateNodeLinkText("5", "Link 2");

            //DetailsContainer area is not visible by default
            webSiteSitemapRecordDesignerPage
                .ValidateTypeFieldVisibility(false)
                .ValidateTitleFieldVisibility(false)
                .ValidatePageFieldVisibility(false)
                .ValidateLinkFieldVisibility(false)
                .ValidateDisplayOrderFieldVisibility(false);


        }

        [TestProperty("JiraIssueID", "CDV6-5736")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes for pages and links) - " +
            "Tap on the Designer Tab - Click on a Page Node Link - Validate that the DetailsContainer area is displayed with the node information")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod13()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("da1a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNodeLink("1")

                .WaitForDetailsContainerToLoad()

                .ValidateTypeFieldVisibility(true)
                .ValidateTitleFieldVisibility(true)
                .ValidatePageFieldVisibility(true)
                .ValidateLinkFieldVisibility(false)
                .ValidateDisplayOrderFieldVisibility(true)

                .ValidateTypeSelectedValue("Page")
                .ValidateTitleText("Page 1")
                .ValidatePageSelectedValue("Page_1")
                .ValidateDisplayOrderText("1")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5737")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes for pages and links) - " +
            "Tap on the Designer Tab - Click on a Page Node - Validate that the DetailsContainer area is displayed with the node information - " +
            "Tap on a different Link Node - Validate that the DetailsContainer contains the information for the last clicked node")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod14()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("da1a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNodeLink("1")

                .WaitForDetailsContainerToLoad()

                .ValidateTypeSelectedValue("Page")
                .ValidateTitleText("Page 1")
                .ValidatePageSelectedValue("Page_1")
                .ValidateDisplayOrderText("1")

                .ClickNodeLink("4")

                .ValidateTypeFieldVisibility(true)
                .ValidateTitleFieldVisibility(true)
                .ValidatePageFieldVisibility(false)
                .ValidateLinkFieldVisibility(true)
                .ValidateDisplayOrderFieldVisibility(true)

                .ValidateTypeSelectedValue("Link")
                .ValidateTitleText("Link 1")
                .ValidateLinkText("https://www.google.com/")
                .ValidateDisplayOrderText("4")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5738")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes for pages and links) - " +
            "Tap on the Designer Tab - Click on a Page Child Node Link - Validate that the DetailsContainer area is displayed with the child node information")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod15()
        {
            var websiteID = new Guid("e77c74df-0825-eb11-a2cd-005056926fe4"); //Automation - Web Site 10
            var Sitemap1ID = new Guid("da1a5441-c025-eb11-a2cd-005056926fe4"); //Sitemap 02

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 10")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickChildNodeLink("1", "2")

                .WaitForDetailsContainerToLoad()

                .ValidateTypeFieldVisibility(true)
                .ValidateTitleFieldVisibility(true)
                .ValidatePageFieldVisibility(true)
                .ValidateLinkFieldVisibility(false)
                .ValidateDisplayOrderFieldVisibility(true)

                .ValidateTypeSelectedValue("Page")
                .ValidateTitleText("Page 1.2")
                .ValidatePageSelectedValue("Page_1_2")
                .ValidateDisplayOrderText("2")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5739")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with no nodes) - " +
            "Tap on the Designer Tab - Click on the New Node button - On the DetailsContainer area tap on the save button - " +
            "Validate that the user is prevented from saving the record without inserting any information")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod16()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", null, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .ClickDetailsContainerSaveButton()


                .ValidateTypeFieldErrorLabelVisibility(false)
                .ValidatePageFieldErrorLabelVisibility(false)
                .ValidateLinkFieldErrorLabelVisibility(false)

                .ValidateTitleErrorLabelText("Please fill out this field.")
                .ValidateDisplayOrderErrorLabelText("Please fill out this field.");


        }

        [TestProperty("JiraIssueID", "CDV6-5740")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with no nodes) - " +
            "Tap on the Designer Tab - Click on the New Node button - Select 'Page' in the Type picklist - set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod17()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Page")
                .InsertTitle("Page 1")
                .SelectPage("Page_1")
                .InsertDisplayOrderText("1")
                .ClickDetailsContainerSaveButton()

                .ValidateNodeLinkText("1", "Page 1")
                .ValidateNewChildNodeButtonVisibility(true, "1")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5741")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes) - " +
            "Tap on the Designer Tab - Click on the New Node button - Select 'Page' in the Type picklist - set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod18()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Page")
                .InsertTitle("Page 4")
                .SelectPage("Page_4")
                .InsertDisplayOrderText("4")
                .ClickDetailsContainerSaveButton()

                .ValidateNodeLinkText("4", "Page 4")
                .ValidateNewChildNodeButtonVisibility(true, "4")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5742")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with 3 nodes) - " +
            "Tap on the Designer Tab - Click on the New Node button - Select 'Page' in the Type picklist - Set display order to 3 - set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved (note must be saved as the one before last)")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod19()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Page")
                .InsertTitle("Page 4")
                .SelectPage("Page_4")
                .InsertDisplayOrderText("3")
                .ClickDetailsContainerSaveButton();

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .ClickBackButton();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickRefreshButton()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ValidateNodeLinkText("3", "Page 3")
                .ValidateNewChildNodeButtonVisibility(true, "3")
                .ValidateNodeLinkText("4", "Page 4")
                .ValidateNewChildNodeButtonVisibility(true, "4");


        }

        [TestProperty("JiraIssueID", "CDV6-5743")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with a nodes with 2 Child Nodes) - " +
            "Tap on the Designer Tab - Click on the New Child Node button - Select 'Page' in the Type picklist - set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod20()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //Remove all Sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewChildNodeButton("1")

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Page")
                .InsertTitle("Page 4")
                .SelectPage("Page_4")
                .InsertDisplayOrderText("3")
                .ClickDetailsContainerSaveButton()

                .ValidateChildNodeLinkText("1", "3", "Page 4")
                .ValidateNewChildNodeButtonVisibility(true, "1");

        }

        [TestProperty("JiraIssueID", "CDV6-5744")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with a nodes with 2 Child Nodes) - " +
            "Tap on the Designer Tab - Click on the New Child Node button - Select 'Page' in the Type picklist - Set display order to 2 - Set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod21()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //Remove all Sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewChildNodeButton("1")

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Page")
                .InsertTitle("Page 4")
                .SelectPage("Page_4")
                .InsertDisplayOrderText("2")
                .ClickDetailsContainerSaveButton()

                .ValidateChildNodeLinkText("1", "2", "page 1.2")
                .ValidateNewChildNodeButtonVisibility(true, "1");

        }


        [TestProperty("JiraIssueID", "CDV6-5745")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes) - " +
            "Tap on the Designer Tab - Click on the New Node button - Select 'Link' in the Type picklist - set all mandatory fields - " +
            "On the DetailsContainer area tap on the save button - " +
            "Validate that the Sitemap is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod22()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()

                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .InsertName("Page_1")
                .SelectType("Link")
                .InsertTitle("Link 1")
                .InsertLink("www.google.com")
                .InsertDisplayOrderText("4")
                .ClickDetailsContainerSaveButton()

                .ValidateNodeLinkText("4", "Link 1")
                .ValidateNewChildNodeButtonVisibility(true, "4")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-5746")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes) - " +
            "Tap on the Designer Tab - Select a Node - Tap on the delete button - Confirm the delete operation - " +
            "Validate that the node was removed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod23()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ClickNodeLink("3")
                .WaitForDetailsContainerToLoad()
                .ClickDetailsContainerDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ValidateNodeLinkVisibility(false, "3");
        }

        [TestProperty("JiraIssueID", "CDV6-5747")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with nodes and sub-nodes) - " +
            "Tap on the Designer Tab - Select a Child Node - Tap on the delete button - Confirm the delete operation - " +
            "Validate that the child node was removed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod24()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", "{\"NodeCollection\":[{\"Title\":\"Page 1\",\"PageId\":\"9626da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":[{\"Title\":\"Page 1.1\",\"PageId\":\"9c26da7a-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"page 1.2\",\"PageId\":\"2cbc8e81-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]},{\"Title\":\"Page 2\",\"PageId\":\"407a2b88-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null},{\"Title\":\"Page 3\",\"PageId\":\"6b8c6891-c225-eb11-a2cd-005056926fe4\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ClickChildNodeLink("1", "2")
                .WaitForDetailsContainerToLoad()
                .ClickDetailsContainerDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ValidateChildNodeLinkVisibility(false, "1", "2");
        }

        [TestProperty("JiraIssueID", "CDV6-5748")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5442 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Sitemaps area - Open a Website Sitemap record (sitemap with no nodes; Type = Anonymous User) - " +
            "Tap on the Designer Tab - Click on the New Node button - Set Type equals to Page - Validiate that the Page picklist only displays pages where Is Secure = No")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteSitemaps_UITestMethod26()
        {
            var websiteID = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            //remove all sitemaps
            foreach (var sitemap in dbHelper.websiteSitemap.GetByWebSiteID(websiteID))
                dbHelper.websiteSitemap.DeleteWebsiteSitemap(sitemap);

            var Sitemap1ID = dbHelper.websiteSitemap.CreateWebsiteSitemap(websiteID, "Sitemap 01", null, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 11")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteSitemaps();

            webSiteSitemapsPage
                .WaitForWebSiteSitemapsPageToLoad()
                .ClickOnWebSiteSitemapRecord(Sitemap1ID.ToString());

            webSiteSitemapRecordPage
                .WaitForWebSiteSitemapRecordPageToLoad()
                .TapDesignerTab();

            webSiteSitemapRecordDesignerPage
                .WaitForWebSiteSitemapRecordDesignerPageToLoad()
                .ClickNewNodeButton()

                .WaitForDetailsContainerToLoad()

                .SelectType("Page")
                .ValidatePageTextPresent(false, "Page_1")
                .ValidatePageTextPresent(false, "Page_1_1")
                .ValidatePageTextPresent(false, "Page_1_2")
                .ValidatePageTextPresent(false, "Page_2")
                .ValidatePageTextPresent(true, "Page_3")
                .ValidatePageTextPresent(false, "Page_4")
                ;


        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5583

        [TestProperty("JiraIssueID", "CDV6-5749")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - " +
            "Validate that the Website Point Of Contacts page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-5750")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var PointOfContact1ID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1
            var PointOfContact2ID = new Guid("0da2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 2


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()

                .ValidateNameWebsiteCellText(PointOfContact1ID.ToString(), "Automation - Web Site 07")
                .ValidateNameCellText(PointOfContact1ID.ToString(), "Point Of Contact 1")
                .ValidateAddressCellText(PointOfContact1ID.ToString(), "line 1 line 2")
                .ValidatePhoneCellText(PointOfContact1ID.ToString(), "987654321")
                .ValidateEmailCellText(PointOfContact1ID.ToString(), "PointOfContact1@mail.com")
                .ValidateStatusCellText(PointOfContact1ID.ToString(), "Published")
                .ValidateResponsibleTeamCellText(PointOfContact1ID.ToString(), "CareDirector QA")
                .ValidateCreatedByCellText(PointOfContact1ID.ToString(), "CW Forms Test User 1")
                .ValidateCreatedOnCellText(PointOfContact1ID.ToString(), "17/11/2020 11:01:32")

                .ValidateRecordPresent(PointOfContact2ID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-5751")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Open a Website Point Of Contact record (all fields must have values) - " +
            "Validate that the user is redirected to the WebSite Point Of Contact record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var PointOfContact1ID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContact1ID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()

                .ValidateNameFieldValue("Point Of Contact 1")
                .ValidateAddressFieldValue("line 1\r\nline 2")
                .ValidatePhoneFieldValue("987654321")
                .ValidateEmailFieldValue("PointOfContact1@mail.com")

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateStatusSelectedText("Published");
        }

        [TestProperty("JiraIssueID", "CDV6-5752")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Open a Website Point Of Contacts record (only mandatory fields have values) - " +
            "Validate that the user is redirected to the WebSite PointOfContact record page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var PointOfContact1ID = new Guid("0da2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContact1ID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()

                .ValidateNameFieldValue("Point Of Contact 2")
                .ValidateAddressFieldValue("")
                .ValidatePhoneFieldValue("")
                .ValidateEmailFieldValue("")

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateStatusSelectedText("Published");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-5753")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Open a Website PointOfContact record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod06()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var PointOfContact3ID = new Guid("0c178867-d028-eb11-a2cd-005056926fe4"); //Point Of Contact 3

            dbHelper.websitePointOfContact.UpdateWebsitePointOfContact(PointOfContact3ID, "Point Of Contact 3", 1, "", "", "");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContact3ID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .InsertName("Point Of Contact 3 - Update")
                .InsertAddress("Address Update")
                .InsertPhone("12345")
                .InsertEmail("PointOfContact3@mail.com")
                .SelectStatus("Published")
                .ClickSaveAndCloseButton();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContact3ID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()

                .ValidateNameFieldValue("Point Of Contact 3 - Update")
                .ValidateAddressFieldValue("Address Update")
                .ValidatePhoneFieldValue("12345")
                .ValidateEmailFieldValue("PointOfContact3@mail.com")

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateStatusSelectedText("Published");

        }

        [TestProperty("JiraIssueID", "CDV6-5754")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Open a Website Point Of Contact record - " +
            "Remove the values from the mandatory fields - Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod07()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var PointOfContact3ID = new Guid("0c178867-d028-eb11-a2cd-005056926fe4"); //Point Of Contact 3

            dbHelper.websitePointOfContact.UpdateWebsitePointOfContact(PointOfContact3ID, "Point Of Contact 3", 1, "", "", "");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContact3ID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .InsertName("")
                .SelectStatus("")
                .ClickSaveButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateStatusFieldErrorLabelVisibility(true)
                .ValidateStatusFieldErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-5755")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching PointOfContacts
            foreach (var websitepageid in dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4"))
                dbHelper.websitePointOfContact.DeleteWebsitePointOfContact(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClicAddNewRecordButton();

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .InsertName("Point Of Contact 4")
                .InsertAddress("Point Of Contact 4 Address")
                .InsertPhone("12345")
                .InsertEmail("PointOfContact4@mail.com")
                .SelectStatus("Published")
                .ClickSaveAndCloseButton();

            var records = dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4");
            Assert.AreEqual(1, records.Count);

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(records[0].ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()

                .ValidateNameFieldValue("Point Of Contact 4")
                .ValidateAddressFieldValue("Point Of Contact 4 Address")
                .ValidatePhoneFieldValue("12345")
                .ValidateEmailFieldValue("PointOfContact4@mail.com")

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateStatusSelectedText("Published");
        }

        [TestProperty("JiraIssueID", "CDV6-5756")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Tap on the add new record button - " +
            "Set data in mandatory fields only - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod09()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching PointOfContacts
            foreach (var websitepageid in dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4"))
                dbHelper.websitePointOfContact.DeleteWebsitePointOfContact(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClicAddNewRecordButton();

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .InsertName("Point Of Contact 4")
                .SelectStatus("Published")
                .ClickSaveAndCloseButton();

            var records = dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4");
            Assert.AreEqual(1, records.Count);

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(records[0].ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()

                .ValidateNameFieldValue("Point Of Contact 4")
                .ValidateAddressFieldValue("")
                .ValidatePhoneFieldValue("")
                .ValidateEmailFieldValue("")

                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateStatusSelectedText("Published");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-5757")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Tap on the add new record button - " +
            "Dont set any data in the mandatory fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching PointOfContacts
            foreach (var websitepageid in dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4"))
                dbHelper.websitePointOfContact.DeleteWebsitePointOfContact(websitepageid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClicAddNewRecordButton();

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .ClickSaveButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateStatusFieldErrorLabelVisibility(true)
                .ValidateStatusFieldErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-5758")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Point Of Contacts area - Open a website PointOfContact record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsitePointsOfContact_UITestMethod11()
        {
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching PointOfContacts
            foreach (var websitepageid in dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4"))
                dbHelper.websitePointOfContact.DeleteWebsitePointOfContact(websitepageid);

            Guid PointOfContactID = dbHelper.websitePointOfContact.CreateWebsitePointOfContact(teamID, "Point Of Contact 4", websiteID, 1, "", "", "");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsitePointsOfContact();

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad()
                .ClickOnWebSitePointsOfContactRecord(PointOfContactID.ToString());

            webSitePointOfContactRecordPage
                .WaitForWebSitePointOfContactRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();
            System.Threading.Thread.Sleep(3000);

            webSitePointsOfContactPage
                .WaitForWebSitePointsOfContactPageToLoad();

            var records = dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Point Of Contact 4");
            Assert.AreEqual(0, records.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5586

        [TestProperty("JiraIssueID", "CDV6-6239")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5586 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Feedbacks area - " +
            "Validate that the Website Feedbacks page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteFeedback_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteFeedback();

            webSiteFeedbackPage
                .WaitForWebSiteFeedbackPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-6240")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5586 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Feedbacks area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteFeedback_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var feedback1ID = new Guid("23b94f99-9081-41c5-aebc-a1a76fc9483e"); //Feedback 1
            var feedback2ID = new Guid("66bcc8f0-59fc-47c4-9664-adb8cb2d8726"); //Feedback 2


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteFeedback();

            webSiteFeedbackPage
                .WaitForWebSiteFeedbackPageToLoad()

                .ValidateNameCellText(feedback1ID.ToString(), "Feedback 1")
                .ValidateEmailCellText(feedback1ID.ToString(), "person1@mail.com")
                .ValidateWebsiteFeedbackTypeCellText(feedback1ID.ToString(), "Generic Opinion")
                .ValidateMessageCellText(feedback1ID.ToString(), "Message information goes here ....")
                .ValidateCreatedOnCellText(feedback1ID.ToString(), "18/11/2020 17:30:22")

                .ValidateRecordPresent(feedback2ID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6241")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5586 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Feedbacks area - Open a Website Feedback record (all fields must have values) - " +
            "Validate that the user is redirected to the Website Feedback record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteFeedback_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var feedback1ID = new Guid("23b94f99-9081-41c5-aebc-a1a76fc9483e"); //Feedback 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteFeedback();

            webSiteFeedbackPage
                .WaitForWebSiteFeedbackPageToLoad()
                .ClickOnWebSiteFeedbackRecord(feedback1ID.ToString());

            webSiteFeedbackRecordPage
                .WaitForWebSiteFeedbackRecordPageToLoad()

                .ValidateNameFieldValue("Feedback 1")
                .ValidateEmailFieldValue("person1@mail.com")
                .ValidateWebSiteFeedbackTypeFieldLinkText("Generic Opinion")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidateMessage("Message information goes here ....");
        }

        [TestProperty("JiraIssueID", "CDV6-6242")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5586 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Feedbacks area - Open a website Feedback record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteFeedback_UITestMethod04()
        {
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var websiteFeedbackTypeId = new Guid("56546cc7-bf29-eb11-a2cd-005056926fe4"); //Generic Opinion
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteIDAndName(websiteID, "Feedback 3"))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            Guid feedbackID = dbHelper.websiteFeedback.CreateWebsiteFeedback(teamID, "Feedback 3", "person@mail.com", websiteID, "message goes here.", websiteFeedbackTypeId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteFeedback();

            webSiteFeedbackPage
                .WaitForWebSiteFeedbackPageToLoad()
                .ClickOnWebSiteFeedbackRecord(feedbackID.ToString());

            webSiteFeedbackRecordPage
                .WaitForWebSiteFeedbackRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();
            System.Threading.Thread.Sleep(3000);

            webSiteFeedbackPage
                .WaitForWebSiteFeedbackPageToLoad();

            var records = dbHelper.websiteFeedback.GetByWebSiteIDAndName(websiteID, "Feedback 3");
            Assert.AreEqual(0, records.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5584

        [TestProperty("JiraIssueID", "CDV6-6228")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
           "Login in the web app - Open a Website record - Navigate to the Website Contacts area - " +
            "Validate that the Website Contacts page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord();
        }

        [TestProperty("JiraIssueID", "CDV6-6229")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - " +
            "Validate that all records for the current website are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07

            var contact1ID = new Guid("95bbfded-532a-eb11-a2cd-005056926fe4"); //Website 07 - Contact 01
            var contact2ID = new Guid("2c59e05e-432b-eb11-a2ce-005056926fe4"); //Website 07 - Contact 02


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ValidateWebsiteCellText(contact1ID.ToString(), "Automation - Web Site 07")
                .ValidateSubjectCellText(contact1ID.ToString(), "Contact 01 Subject")
                .ValidateNameCellText(contact1ID.ToString(), "Website 07 - Contact 01")
                .ValidateEmailCellText(contact1ID.ToString(), "Contact01@mail.com")
                .ValidatePointOfContactCellText(contact1ID.ToString(), "Point Of Contact 1")
                .ValidateCreatedOnCellText(contact1ID.ToString(), "19/11/2020 10:42:35")
                .ValidateResponsibleTeamCellText(contact1ID.ToString(), "CareDirector QA")

                .ValidateRecordPresent(contact2ID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6230")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Open a Website Contact record - " +
            "Validate that the user is redirected to the WebSite Contact record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod03()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var contact1ID = new Guid("95bbfded-532a-eb11-a2cd-005056926fe4"); //Website 07 - Contact 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contact1ID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()

                .ValidateNameFieldValue("Website 07 - Contact 01")
                .ValidateEmailFieldValue("Contact01@mail.com")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidatePointOfContactLinkText("Point Of Contact 1")
                .ValidateSubjectFieldValue("Contact 01 Subject")

                .LoadMessageRichTextBox()
                .ValidateMessageFieldText("1", "Contact 01 Message");
        }

        [TestProperty("JiraIssueID", "CDV6-6231")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Open a Website Contact record - " +
            "Update all fields (leave Is Encrypted set to No) - Tap on the save button - Validate that the website record is saved")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod04()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var contactID = new Guid("8cca6fac-442b-eb11-a2ce-005056926fe4"); //Website 07 - Contact 03
            var websitePointOfContac1tID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1
            var websitePointOfContact2ID = new Guid("0da2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 2

            dbHelper.websiteContact.UpdateWebsiteContact(contactID, "Website 07 - Contact 03", "Contact 03 Subject", "Contac03@mail.com", websitePointOfContac1tID, "Contact 03 Message");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contactID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .InsertName("Website 07 - Contact 03 - Update")
                .InsertEmail("Contac03Update@mail.com")
                .TapPointOfContactLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(websitePointOfContact2ID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .InsertSubject("Contact 03 Subject - Update")
                .InsertMessage("Contact 03 Message - Update")
                .ClickSaveAndCloseButton();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contactID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()

                .ValidateNameFieldValue("Website 07 - Contact 03 - Update")
                .ValidateEmailFieldValue("Contac03Update@mail.com")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
                .ValidatePointOfContactLinkText("Point Of Contact 2")
                .ValidateSubjectFieldValue("Contact 03 Subject - Update")

                .LoadMessageRichTextBox()
                .ValidateMessageFieldText("1", "Contact 03 Message - Update");

        }

        [TestProperty("JiraIssueID", "CDV6-6232")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Open a Website Contact record - " +
            "Remove the values from the mandatory fields - Tap on the save button - Validate the user is prevented from saving the record")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod05()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var contactID = new Guid("8cca6fac-442b-eb11-a2ce-005056926fe4"); //Website 07 - Contact 03
            var websitePointOfContac1tID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1

            dbHelper.websiteContact.UpdateWebsiteContact(contactID, "Website 07 - Contact 03", "Contact 03 Subject", "Contac03@mail.com", websitePointOfContac1tID, "Contact 03 Message");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contactID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .InsertName("")
                .InsertEmail("")
                .TapPointOfContactRemoveButton()
                .InsertSubject("")
                .InsertMessage("")
                .ClickSaveAndCloseButton()

                .ValidateNameFieldErrorLabelVisibility(true)
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateEmailFieldErrorLabelVisibility(true)
                .ValidateEmailFieldErrorLabelText("Please fill out this field.")
                .ValidatePointOfContactFieldErrorLabelVisibility(true)
                .ValidatePointOfContactFieldErrorLabelText("Please fill out this field.")
                .ValidateSubjectFieldErrorLabelVisibility(true)
                .ValidateSubjectFieldErrorLabelText("Please fill out this field.");

        }

        //[TestProperty("JiraIssueID", "")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
        //    "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Tap on the add new record button - " +
        //    "Set data in all fields - Tap on the save button - Validate that the record is saved")]
        //[TestMethod, TestCategory("UITest")]
        //public void WebsiteContacts_UITestMethod06()
        //{
        //    var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
        //    var websitePointOfContact1ID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1

        //    //remove all matching Contacts
        //    foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
        //        dbHelper.websiteContact.DeleteWebsiteContact(recordid);


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .InsertSearchQuery("Automation - Web Site 07")
        //        .ClickSearchButton()
        //        .ClickOnWebSiteRecord(websiteID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .NavigateToWebsiteContact();

        //    webSiteContactsPage
        //        .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
        //        .ClicAddNewRecordButton();

        //    webSiteContactRecordPage
        //        .WaitForWebSiteContactRecordPageToLoad()
        //        .InsertName("Website 07 - Contact 04")
        //        .InsertEmail("Contac04@mail.com")
        //        .SelectWebsitePointOfContactByvalue(websitePointOfContact1ID.ToString())
        //        .InsertSubject("Contact 04 Subject")
        //        .InsertMessage("Contact 04 Message")
        //        .ClickSaveAndCloseButton();

        //    var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
        //    Assert.AreEqual(1, records.Count);

        //    webSiteContactsPage
        //        .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
        //        .ClickOnWebSiteContactRecord(records[0].ToString());

        //    webSiteContactRecordPage
        //        .WaitForWebSiteContactRecordPageToLoad()

        //        .ValidateNameFieldValue("Website 07 - Contact 04")
        //        .ValidateEmailFieldValue("Contac04@mail.com")
        //        .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
        //        .ValidateWebSiteFieldLinkText("Automation - Web Site 07")
        //        .ValidateWebsitePointOfContactSelectedText("Point Of Contact 1")
        //        .ValidateSubjectFieldValue("Contact 04 Subject")

        //        .LoadMessageRichTextBox()
        //        .ValidateMessageFieldText("1", "Contact 04 Message");
        //}

        //[TestProperty("JiraIssueID", "")]
        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
        //    "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Tap on the add new record button - " +
        //    "Dont set any data in the mandatory fields - Tap on the save button - Validate that the record is not saved")]
        //[TestMethod, TestCategory("UITest")]
        //public void WebsiteContacts_UITestMethod07()
        //{
        //    var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07


        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToWebSitesSection();

        //    webSitePage
        //        .WaitForWebSiteToLoad()
        //        .InsertSearchQuery("Automation - Web Site 07")
        //        .ClickSearchButton()
        //        .ClickOnWebSiteRecord(websiteID.ToString());

        //    webSiteRecordPage
        //        .WaitForWebSiteRecordPageToLoad()
        //        .NavigateToWebsiteContact();

        //    webSiteContactsPage
        //        .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
        //        .ClicAddNewRecordButton();

        //    webSiteContactRecordPage
        //        .WaitForWebSiteContactRecordPageToLoad()
        //        .ClickSaveButton()

        //        .ValidateNameFieldErrorLabelVisibility(true)
        //        .ValidateNameFieldErrorLabelText("Please fill out this field.")
        //        .ValidateEmailFieldErrorLabelVisibility(true)
        //        .ValidateEmailFieldErrorLabelText("Please fill out this field.")
        //        .ValidatePointOfContactFieldErrorLabelVisibility(true)
        //        .ValidatePointOfContactFieldErrorLabelText("Please fill out this field.")
        //        .ValidateSubjectFieldErrorLabelVisibility(true)
        //        .ValidateSubjectFieldErrorLabelText("Please fill out this field.");

        //}

        [TestProperty("JiraIssueID", "CDV6-6233")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Open a website Contact record - Tap on the delete button - " +
            "Confirm the delete operation - Validate that the record is deleted from the database")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod08()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitePointOfContac1tID = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Point Of Contact 1
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            Guid contactID = dbHelper.websiteContact.CreateWebsiteContact(teamID, "Website 07 - Contact 04", "Contact 04 subject", "Contact 04 message", websitePointOfContac1tID, "Contact04@mail.com", websiteID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contactID.ToString());

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord();

            var records = dbHelper.websitePointOfContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-6234")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Open existing record - " +
            "Validate that the Website Point of Contact picklist only displays published values")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod09()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var contact01 = new Guid("95bbfded-532a-eb11-a2cd-005056926fe4"); //Website 07 - Contact 01


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contact01.ToString());

            var pointOfContact1 = "09a2c43f-c428-eb11-a2cd-005056926fe4";// Automation - Web Site 07  -- Point Of Contact 1
            var pointOfContact2 = "0da2c43f-c428-eb11-a2cd-005056926fe4";// Automation - Web Site 07  -- Point Of Contact 2
            var pointOfContact7 = "5857b2c7-512b-eb11-a2ce-005056926fe4";// Automation - Web Site 07  -- Point Of Contact 7

            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .TapPointOfContactLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(pointOfContact1)
                .ValidateResultElementPresent(pointOfContact2)
                .ValidateResultElementNotPresent(pointOfContact7);
        }

        [TestProperty("JiraIssueID", "CDV6-6235")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
            "Login in the web app - Open a Website record - Navigate to the Website Contacts area - Tap on the add new record button - " +
            "Validate that the Website Point of Contact picklist do not display draft values")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod10()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var contact01 = new Guid("95bbfded-532a-eb11-a2cd-005056926fe4"); //Website 07 - Contact 01


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteContact();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromWebsiteRecord()
                .ClickOnWebSiteContactRecord(contact01.ToString());

            var WebSite01PointOfContact1 = "ec49edd7-742a-eb11-a2cd-005056926fe4";// Automation - Web Site 01  -- Point Of Contact 1
            var WebSite01PointOfContact2 = "f049edd7-742a-eb11-a2cd-005056926fe4";// Automation - Web Site 01  -- Point Of Contact 2
            var WebSite07PointOfContact1 = "09a2c43f-c428-eb11-a2cd-005056926fe4";// Automation - Web Site 07  -- Point Of Contact 1
            var WebSite07PointOfContact2 = "0da2c43f-c428-eb11-a2cd-005056926fe4";// Automation - Web Site 07  -- Point Of Contact 2


            webSiteContactRecordPage
                .WaitForWebSiteContactRecordPageToLoad()
                .TapPointOfContactLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(WebSite07PointOfContact1)
                .ValidateResultElementPresent(WebSite07PointOfContact2)
                .ValidateResultElementNotPresent(WebSite01PointOfContact1)
                .ValidateResultElementNotPresent(WebSite01PointOfContact2)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-6236")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
           "Login in the web app - Tap on Settings, Portals, Website Contacts - Validate that the Website Contacts page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod11()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteContactsSection();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromSettingsLink();
        }

        [TestProperty("JiraIssueID", "CDV6-6237")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
           "Login in the web app - Tap on Settings, Portals, Website Contacts - " +
            "Validate that the Website Contacts page displays contacts from all Websites")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod12()
        {
            var contact1 = new Guid("4cd1d143-522b-eb11-a2ce-005056926fe4"); //Website 01 - Contact 01
            var contact2 = new Guid("95bbfded-532a-eb11-a2cd-005056926fe4"); //Website 07 - Contact 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteContactsSection();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromSettingsLink()

                .InsertSearchQuery("Website 01 - Contact 01")
                .ClickSearchButton()

                .ValidateNameCellText_SearchResultsPage(contact1.ToString(), "Website 01 - Contact 01")
                .ValidatePointOfContactCellText_SearchResultsPage(contact1.ToString(), "Point Of Contact 1")
                .ValidateCreatedByCellText_SearchResultsPage(contact1.ToString(), "CW Forms Test User 1")
                .ValidateCreatedOnCellText_SearchResultsPage(contact1.ToString(), "20/11/2020 17:03:10")


                .InsertSearchQuery("Website 07 - Contact 01")
                .ClickSearchButton()

                .ValidateNameCellText_SearchResultsPage(contact2.ToString(), "Website 07 - Contact 01")
                .ValidatePointOfContactCellText_SearchResultsPage(contact2.ToString(), "Point Of Contact 1")
                .ValidateCreatedByCellText_SearchResultsPage(contact2.ToString(), "CW Forms Test User 1")
                .ValidateCreatedOnCellText_SearchResultsPage(contact2.ToString(), "19/11/2020 10:42:35");
        }

        [TestProperty("JiraIssueID", "CDV6-6238")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5583 - " +
           "Login in the web app - Tap on Settings, Portals, Website Contacts - Open a Website Contact record - " +
            "Validate that the user is redirected to the WebSite Contact record Page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteContacts_UITestMethod13()
        {
            var contact1 = new Guid("4cd1d143-522b-eb11-a2ce-005056926fe4"); //Website 01 - Contact 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteContactsSection();

            webSiteContactsPage
                .WaitForWebsiteContactsPageToLoadFromSettingsLink()
                .InsertSearchQuery("Website 01 - Contact 01")
                .ClickSearchButton()
                .ClickOnWebSiteContactRecord(contact1.ToString());

            webSiteContactRecordPage
               .WaitForWebSiteContactRecordPageToLoad()

               .ValidateNameFieldValue("Website 01 - Contact 01")
               .ValidateEmailFieldValue("Contact01@mail.com")
               .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
               .ValidateWebSiteFieldLinkText("Automation - Web Site 01")
               .ValidatePointOfContactLinkText("Point Of Contact 1")
               .ValidateSubjectFieldValue("Website 01 - Contact 01 subject")

               .LoadMessageRichTextBox()
               .ValidateMessageFieldText("1", "Contact 01 message");
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-6616

        [TestProperty("JiraIssueID", "CDV6-24722")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record (Email Verification Required = Yes) - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - " +
            "Validate that the record is saved - " +
            "Validate that the Website User Email Verification record is created - " +
            "Validate that an email record is created and linked to the website user")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserEmailVerification_UITestMethod01()
        {
            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var personid = new Guid("1b6bf252-d9d5-4057-9fff-5454dc44dbbb"); //Bridgett Nicholson
            var securityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 17")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("website17user4@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedNoOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("Email")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("76183").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("FullAccess").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var websiteUsers = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com");
            Assert.AreEqual(1, websiteUsers.Count);

            var emailVerificationRecords = dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUsers[0]);
            Assert.AreEqual(1, emailVerificationRecords.Count);
            var emailVerificationFields = dbHelper.websiteUserEmailVerification.GetByID(emailVerificationRecords[0], "link");
            string expectedVerificationLink = "https://automationwebsite17.com/email-verification?id=" + emailVerificationRecords[0].ToString().ToLower();
            Assert.AreEqual(expectedVerificationLink, emailVerificationFields["link"]);

            var emails = dbHelper.email.GetEmailByRegardingID(websiteUsers[0]);
            Assert.AreEqual(1, emails.Count);
            var emailFields = dbHelper.email.GetEmailByID(emails[0], "notes");
            Assert.AreEqual(expectedVerificationLink, emailFields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24723")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record (Email Verification Required = Yes) - Navigate to the Users area - Open an existing record (Email Verified = Yes) - " +
            "update Email Verified to No - Tap on the Save and Close button - " +
            "Validate that a Website User Email Verification record is created - " +
            "Validate that an email record is created and linked to the website user")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserEmailVerification_UITestMethod02()
        {
            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var personid = new Guid("1b6bf252-d9d5-4057-9fff-5454dc44dbbb"); //Bridgett Nicholson
            var securityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            var userID = dbHelper.websiteUser.CreateWebsiteUser(websiteID, "Bridgett Nicholson", "website17user4@mail.com", "Passw0rd_!", true, 1, personid, "person", "Bridgett Nicholson", securityProfile);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 17")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(userID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickEmailVerifiedNoOption()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var emailVerificationRecords = dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(userID);
            Assert.AreEqual(1, emailVerificationRecords.Count);
            var emailVerificationFields = dbHelper.websiteUserEmailVerification.GetByID(emailVerificationRecords[0], "link");
            string expectedVerificationLink = "https://automationwebsite17.com/email-verification?id=" + emailVerificationRecords[0].ToString().ToLower();
            Assert.AreEqual(expectedVerificationLink, emailVerificationFields["link"]);

            var emails = dbHelper.email.GetEmailByRegardingID(userID);
            Assert.AreEqual(1, emails.Count);
            var emailFields = dbHelper.email.GetEmailByID(emails[0], "notes");
            Assert.AreEqual(expectedVerificationLink, emailFields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24724")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record (Email Verification Required = No) - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - Tap on the save button - " +
            "Validate that the record is saved - " +
            "Validate that NO Website User Email Verification record is created - " +
            "Validate that NO email record is created")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserEmailVerification_UITestMethod03()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var personid = new Guid("6133661e-3e77-4307-8bf0-f662a9be2055"); //Pablo Greene
            var securityProfile = new Guid("c90260cf-6240-eb11-a2e4-005056926fe4"); //Full Access

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website16user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 16")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("website16user4@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("Email")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("76184").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Full Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var websiteUsers = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website16user4@mail.com");
            Assert.AreEqual(1, websiteUsers.Count);

            var emailVerificationRecords = dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUsers[0]);
            Assert.AreEqual(0, emailVerificationRecords.Count);

            var emails = dbHelper.email.GetEmailByRegardingID(websiteUsers[0]);
            Assert.AreEqual(0, emails.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-24725")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4627 - " +
            "Login in the web app - Open a Website record (Email Verification Required = Yes) - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - Set Email Verified to Yes - Tap on the save button - " +
            "Validate that the record is saved - " +
            "Validate that NO Website User Email Verification record is created - " +
            "Validate that NO email record is createduser")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserEmailVerification_UITestMethod04()
        {
            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var personid = new Guid("1b6bf252-d9d5-4057-9fff-5454dc44dbbb"); //Bridgett Nicholson
            var securityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 17")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("website17user4@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("Email")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("76183").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("FullAccess").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var websiteUsers = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com");
            Assert.AreEqual(1, websiteUsers.Count);

            var emailVerificationRecords = dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUsers[0]);
            Assert.AreEqual(0, emailVerificationRecords.Count);

            var emails = dbHelper.email.GetEmailByRegardingID(websiteUsers[0]);
            Assert.AreEqual(0, emails.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-6291

        [TestProperty("JiraIssueID", "CDV6-24726")]
        [Description("Open a person record - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Website User - OnDemand Workflow - WS16' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - " +
            "Validate that a website user record is created, linked to the person record and has 'Email Verified' set to NO")]
        [TestMethod, TestCategory("UITest")]
        public void ManuallyTriggeredWorkflowToCreateWebsiteUserAccount_UITestMethod01()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var personID = new Guid("ffdeac58-6e99-4d4f-b776-dc77141826bc");  //Robin Dalton
            var workflowid = new Guid("cd61406b-7459-eb11-a2ff-005056926fe4"); //Website User - OnDemand Workflow - WS16

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "RobinDalton779267267@temp.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("410934", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Robin Dalton")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Robin Dalton");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have 1 website user record
            var users = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "RobinDalton779267267@temp.com");
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("c90260cf-6240-eb11-a2e4-005056926fe4"); //Full Access

            Assert.AreEqual("Robin Dalton", fields["name"]);
            Assert.AreEqual("RobinDalton779267267@temp.com", fields["username"]);
            Assert.AreEqual(false, fields["emailverified"]); //EMAIL VERIFIED SHOULD BE SET TO FALSE BECAUSE WEBSITE 16 HAS Email Verification Required = FALSE
            Assert.IsFalse(fields.ContainsKey("mobilephonenumber"));
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(personID, fields["profileid"]);
            Assert.AreEqual("person", fields["profileidtablename"]);
            Assert.AreEqual("Robin Dalton", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));

        }

        [TestProperty("JiraIssueID", "CDV6-24727")]
        [Description("Open a person record - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Website User - OnDemand Workflow - WS17' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - " +
            "Validate that a website user record is created, linked to the person record and has 'Email Verified' set to YES")]
        [TestMethod, TestCategory("UITest")]
        public void ManuallyTriggeredWorkflowToCreateWebsiteUserAccount_UITestMethod02()
        {

            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var personID = new Guid("495d3a83-70e7-44aa-8d00-ab4453e52035");  //Savannah Cooke
            var workflowid = new Guid("1d88f13b-9c59-eb11-a2ff-005056926fe4"); //Website User - OnDemand Workflow - WS17

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "SavannahCooke907475578@temp.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("410935", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Savannah Cooke")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Savannah Cooke");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have 1 website user record
            var users = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "SavannahCooke907475578@temp.com");
            Assert.AreEqual(1, users.Count);

            var fields = dbHelper.websiteUser.GetByID(users[0], "name", "username", "password", "emailverified", "statusid", "profileid", "profileidtablename", "profileidname", "securityprofileid", "twofactorauthenticationtypeid");

            var expectedSecurityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            Assert.AreEqual("Savannah Cooke", fields["name"]);
            Assert.AreEqual("SavannahCooke907475578@temp.com", fields["username"]);
            Assert.AreEqual(true, fields["emailverified"]); //EMAIL VERIFIED SHOULD BE SET TO TRUE BECAUSE WEBSITE 17 HAS Email Verification Required = TRUE
            Assert.IsFalse(fields.ContainsKey("mobilephonenumber"));
            Assert.AreEqual(2, fields["statusid"]);
            Assert.AreEqual(personID, fields["profileid"]);
            Assert.AreEqual("person", fields["profileidtablename"]);
            Assert.AreEqual("Savannah Cooke", fields["profileidname"]);
            Assert.AreEqual(expectedSecurityProfile, fields["securityprofileid"]);
            Assert.IsFalse(fields.ContainsKey("twofactorauthenticationtypeid"));

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-6292

        [TestProperty("JiraIssueID", "CDV6-24728")]
        [Description("Open a person record (has primary email and mobile phone) - Tap on the edit button - " +
            "Tap on the execute on demand workflow button - Select the 'Website User - OnDemand Workflow - WS16' workflow - " +
            "Validate that the Workflow Job is created - Trigger the workflow job - " +
            "Validate that a website user record is created, reset password record is created and email record with reset password info is created.")]
        [TestMethod, TestCategory("UITest")]
        public void EmailInvitationUponManuallyTriggeringWorkflow_UITestMethod01()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var personID = new Guid("ffdeac58-6e99-4d4f-b776-dc77141826bc");  //Robin Dalton
            var workflowid = new Guid("cd61406b-7459-eb11-a2ff-005056926fe4"); //Website User - OnDemand Workflow - WS16

            //remove all website users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "RobinDalton779267267@temp.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                foreach (var emailToDelete in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(emailToDelete);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            //remove all workflow jobs
            foreach (var workflowjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(workflowjobid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("410934", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Robin Dalton")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Robin Dalton");

            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //we should have 1 website user record
            var users = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "RobinDalton779267267@temp.com");
            Assert.AreEqual(1, users.Count);

            //we should have 1 password reset
            var passwordResets = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(users[0]);
            Assert.AreEqual(1, passwordResets.Count);

            //we should have a 1 email for the website user
            var emails = dbHelper.email.GetEmailByRegardingID(users[0]);
            Assert.AreEqual(1, emails.Count);

            string expectedPasswordResetLink = "https://AutomationWebSite16.com/reset-password?id=" + passwordResets[0].ToString().ToLower();
            var fields = dbHelper.email.GetEmailByID(emails[0], "subject", "notes");
            Assert.AreEqual("Reset password", fields["subject"]);
            Assert.AreEqual("<p>Here is the link for resetting the password&nbsp;<a href=\"" + expectedPasswordResetLink.ToLower() + "\" target=\"_blank\">Reset Password</a></p>", fields["notes"]);


        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-8455

        [TestProperty("JiraIssueID", "CDV6-24729")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Validate that the Portal Tasks page is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod01()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-24730")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Validate that portal task records with any of the available Actions are correctly displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod02()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("Lamont")
                .ClickSearchButton()
                .ValidateRecordPresent(portalTaskRecordID3.ToString())
                .ValidateRecordPresent(portalTaskRecordID2.ToString())
                .ValidateRecordPresent(portalTaskRecordID1.ToString())

                .ValidateTitleSearchCellText(portalTaskRecordID3.ToString(), "Lamont Stevens Portal Task 03")
                .ValidateActionSearchCellText(portalTaskRecordID3.ToString(), "View Record")
                .ValidateStatusSearchCellText(portalTaskRecordID3.ToString(), "New")
                .ValidateDueDateSearchCellText(portalTaskRecordID3.ToString(), "28/03/2021")

                .ValidateTitleSearchCellText(portalTaskRecordID2.ToString(), "Lamont Stevens Portal Task 02")
                .ValidateActionSearchCellText(portalTaskRecordID2.ToString(), "Upload File")
                .ValidateStatusSearchCellText(portalTaskRecordID2.ToString(), "New")
                .ValidateDueDateSearchCellText(portalTaskRecordID2.ToString(), "18/03/2021")

                .ValidateTitleSearchCellText(portalTaskRecordID1.ToString(), "Lamont Stevens Portal Task 01")
                .ValidateActionSearchCellText(portalTaskRecordID1.ToString(), "Run Workflow")
                .ValidateStatusSearchCellText(portalTaskRecordID1.ToString(), "New")
                .ValidateDueDateSearchCellText(portalTaskRecordID1.ToString(), "28/03/2021");

        }

        [TestProperty("JiraIssueID", "CDV6-24731")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Perform a quick search using one of the records title - Validate that only the matching records are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod03()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01
            var ToDoListTask1 = new Guid("5a5f3ff1-b809-46de-ad1c-e3d708472b91"); //To-do list - task 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()

                .InsertSearchQuery("Lamont Stevens Portal Task 03")
                .ClickSearchButton()

                .ValidateRecordPresent(portalTaskRecordID3.ToString())
                .ValidateRecordNotPresent(portalTaskRecordID2.ToString())
                .ValidateRecordNotPresent(portalTaskRecordID1.ToString())
                .ValidateRecordNotPresent(ToDoListTask1.ToString())

                .ValidateTitleSearchCellText(portalTaskRecordID3.ToString(), "Lamont Stevens Portal Task 03")
                .ValidateDueDateSearchCellText(portalTaskRecordID3.ToString(), "28/03/2021")
                .ValidateActionSearchCellText(portalTaskRecordID3.ToString(), "View Record")
                .ValidateStatusSearchCellText(portalTaskRecordID3.ToString(), "New");


        }

        [TestProperty("JiraIssueID", "CDV6-24732")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on a portal task record - Validate that the user is redirected to the portal task record page")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod04()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Lamont*")
                .ClickSearchButton()
                .ClickOnRecord(portalTaskRecordID1.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-24733")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on a portal task record (Action = 'View Record') - " +
            "Validate that the user is redirected to the portal task record page - " +
            "Validate that both Record and Target Page fields are displayed - " +
            "Validate that the Workflow field is not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod05()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Lamont*")
                .ClickSearchButton()
                .ClickOnRecord(portalTaskRecordID3.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Lamont Stevens")
                .ValidateTitleFieldText("Lamont Stevens Portal Task 03")
                .ValidateActionSelectedText("View Record")
                .ValidateDueDateFieldText("28/03/2021")
                .ValidateRecordFieldLinkText("Attachment 003")
                .ValidateStatusSelectedText("New")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24734")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on a portal task record (Action = 'Upload File') - " +
            "Validate that the user is redirected to the portal task record page - " +
            "Validate that the Record field is displayed - " +
            "Validate that the Workflow and Target Page fields are not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod06()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Lamont*")
                .ClickSearchButton()
                .ClickOnRecord(portalTaskRecordID2.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(false)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Lamont Stevens")
                .ValidateTitleFieldText("Lamont Stevens Portal Task 02")
                .ValidateActionSelectedText("Upload File")
                .ValidateDueDateFieldText("18/03/2021")
                .ValidateRecordFieldLinkText("Attachment 003")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24735")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on a portal task record (Action = 'Run Workflow') - " +
            "Validate that the user is redirected to the portal task record page - " +
            "Validate that the Workflow field is displayed - " +
            "Validate that the Workflow and Record fields are not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod07()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Lamont*")
                .ClickSearchButton()
                .ClickOnRecord(portalTaskRecordID1.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(true)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Lamont Stevens")
                .ValidateTitleFieldText("Lamont Stevens Portal Task 01")
                .ValidateActionSelectedText("Run Workflow")
                .ValidateDueDateFieldText("28/03/2021")
                .ValidateWorkflowFieldLinkText("Automation - Website On Demand Workflow - Person")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24736")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Validate that the new record page is displayed - " +
            "Validate that by default the fields Record, Target page and Workflow are not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod08()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(false)
                .ValidateWorkflowFieldVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-24737")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - wait for the new record page to be displayed - " +
            "Set Action to 'Run Workflow' - Validate that the Workflow and Target Page fields are displayed - Validate that the record field is not displayed" +
            "Change Action to 'Upload Record' - Validate that the record field is displayed - Validate that the Workflow and Target Page fields are Not displayed - " +
            "Change Action to 'View Record' - Validate that the Record and Target Page fields are displayed - Validate that the Workflow field is Not displayed - ")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod09()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .SelectAction("Run Workflow")
                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(true)

                .SelectAction("Upload File")
                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(false)
                .ValidateWorkflowFieldVisibility(false)

                .SelectAction("View Record")
                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)
                ;
            ;

        }

        [TestProperty("JiraIssueID", "CDV6-24738")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'Run Workflow' - Set data in all mandatory fields - Tap on the save button - Validate that the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod10()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("Run Workflow")
                .TapTargetUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery("317348").TapSearchButton().SelectResultElement(personid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapWorkflowLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automation - Website On Demand Workflow - Person").TapSearchButton().SelectResultElement(workflowid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("attachment").TapSearchButton().SelectResultElement(websitepageid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(records[0].ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(true)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001")
                .ValidateActionSelectedText("Run Workflow")
                .ValidateDueDateFieldText(dueDate)
                .ValidateWorkflowFieldLinkText("Automation - Website On Demand Workflow - Person")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24739")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'Upload File' - Set data in all mandatory fields - Tap on the save button - Validate that the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod11()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
            var personAttachmentID = new Guid("f4339a45-ca6a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 001


            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("Upload File")
                .TapTargetUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery("317348").TapSearchButton().SelectResultElement(personid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Elvia Lancaster Person Attachment 001").TapSearchButton().SelectResultElement(personAttachmentID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(records[0].ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(false)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001")
                .ValidateActionSelectedText("Upload File")
                .ValidateDueDateFieldText(dueDate)
                .ValidateRecordFieldLinkText("Elvia Lancaster Person Attachment 001")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24740")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'View Record' - Set data in all mandatory fields - Tap on the save button - Validate that the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod12()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
            var personAttachmentID = new Guid("f4339a45-ca6a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 001
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment


            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("View Record")
                .TapTargetUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery("317348").TapSearchButton().SelectResultElement(personid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Elvia Lancaster Person Attachment 001").TapSearchButton().SelectResultElement(personAttachmentID.ToString());

            websitePortalTaskRecordPage
               .WaitForWebsitePortalTaskRecordPageToLoad()
               .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("attachment").TapSearchButton().SelectResultElement(websitepageid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(records[0].ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001")
                .ValidateActionSelectedText("View Record")
                .ValidateDueDateFieldText(dueDate)
                .ValidateRecordFieldLinkText("Elvia Lancaster Person Attachment 001")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24741")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Open existing record (Action = 'Run Workflow') - Wait for the record page to be displayed - " +
            "Update all editable fields - tap on the save button - validate that the changes are correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod14()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2);
            var dueDateUpdated = dueDate.AddDays(1);
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person
            var workflowidUpdated = new Guid("8b82f28e-dd24-eb11-a2cd-005056926fe4"); //Website User - OnDemand Workflow - WS09
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment
            var websitepageidUpdated = new Guid("09b4014a-6057-eb11-9c25-1866da1e4209"); //assessment
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);

            //Create a new record
            var taskRecordID = dbHelper.portalTask.CreatePortalTask(teamid, dueDate, 1, 1, "Elvia Lancaster Portal Task 001", websiteid,
                personid, "person", "Elvia Lancaster",
                null, null, null,
                websitepageid, workflowid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001 Updated")
                .InsertDueDate(dueDateUpdated.ToString("dd'/'MM'/'yyyy"))
                .SelectStatus("Completed")
                .TapWorkflowLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Website User - OnDemand Workflow - WS09").TapSearchButton().SelectResultElement(workflowidUpdated.ToString());

            websitePortalTaskRecordPage
               .WaitForWebsitePortalTaskRecordPageToLoad()
               .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("assessment").TapSearchButton().SelectResultElement(websitepageidUpdated.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(true)
                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001 Updated")
                .ValidateActionSelectedText("Run Workflow")
                .ValidateDueDateFieldText(dueDateUpdated.ToString("dd'/'MM'/'yyyy"))
                .ValidateWorkflowFieldLinkText("Website User - OnDemand Workflow - WS09")
                .ValidateTargetPageFieldLinkText("assessment")
                .ValidateStatusSelectedText("Completed")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24742")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Open existing record (Action = 'Upload File') - Wait for the record page to be displayed - " +
            "Update all editable fields - tap on the save button - validate that the changes are correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod15()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2);
            var dueDateUpdated = dueDate.AddDays(1);

            var recordid = new Guid("f4339a45-ca6a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 001
            var recordidUpdated = new Guid("9f26c5ff-e16a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 002

            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            //remove all portal tasks for the person
            foreach (var taskrecordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(taskrecordid);

            //Create a new record
            var taskRecordID = dbHelper.portalTask.CreatePortalTask(teamid, dueDate, 2, 1, "Elvia Lancaster Portal Task 001", websiteid,
                personid, "person", "Elvia Lancaster",
                recordid, "personattachment", "Elvia Lancaster Person Attachment 001",
                null, null);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001 Updated")
                .InsertDueDate(dueDateUpdated.ToString("dd/MM/yyyy"))
                .SelectStatus("Completed")
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Elvia Lancaster Person Attachment 002").TapSearchButton().SelectResultElement(recordidUpdated.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(false)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001 Updated")
                .ValidateActionSelectedText("Upload File")
                .ValidateDueDateFieldText(dueDateUpdated.ToString("dd/MM/yyyy"))
                .ValidateRecordFieldLinkText("Elvia Lancaster Person Attachment 002")
                .ValidateStatusSelectedText("Completed")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24743")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Open existing record (Action = 'View Record') - Wait for the record page to be displayed - " +
            "Update all editable fields - tap on the save button - validate that the changes are correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod16()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2);
            var dueDateUpdated = dueDate.AddDays(1);
            var recordid = new Guid("f4339a45-ca6a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 001
            var recordidUpdated = new Guid("9f26c5ff-e16a-eb11-a308-005056926fe4"); //Elvia Lancaster Person Attachment 002
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment
            var websitepageidUpdated = new Guid("09b4014a-6057-eb11-9c25-1866da1e4209"); //assessment
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            //remove all portal tasks for the person
            foreach (var taskrecordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(taskrecordid);

            //Create a new record
            var taskRecordID = dbHelper.portalTask.CreatePortalTask(teamid, dueDate, 3, 1, "Elvia Lancaster Portal Task 001", websiteid,
                personid, "person", "Elvia Lancaster",
                recordid, "personattachment", "Elvia Lancaster Person Attachment 001",
                websitepageid, null);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001 Updated")
                .InsertDueDate(dueDateUpdated.ToString("dd/MM/yyyy"))
                .SelectStatus("Completed")
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Elvia Lancaster Person Attachment 002").TapSearchButton().SelectResultElement(recordidUpdated.ToString());

            websitePortalTaskRecordPage
               .WaitForWebsitePortalTaskRecordPageToLoad()
               .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("assessment").TapSearchButton().SelectResultElement(websitepageidUpdated.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001 Updated")
                .ValidateActionSelectedText("View Record")
                .ValidateDueDateFieldText(dueDateUpdated.ToString("dd/MM/yyyy"))
                .ValidateRecordFieldLinkText("Elvia Lancaster Person Attachment 002")
                .ValidateTargetPageFieldLinkText("assessment")
                .ValidateStatusSelectedText("Completed")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24744")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the record page to be displayed - " +
            "Click on the save button - Validate that the user is prevented from saving the record without inserting the mandatory fields")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod17()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveButton()

                .ValidateTitleFieldErrorLabelVisibility(true)
                .ValidateTargetUserFieldErrorLabelVisibility(true)
                .ValidateActionFieldErrorLabelVisibility(true)

                .ValidateTitleFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetUserFieldErrorLabelText("Please fill out this field.")
                .ValidateActionFieldErrorLabelText("Please fill out this field.");


        }

        [TestProperty("JiraIssueID", "CDV6-24745")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the record page to be displayed - Set Action = Run Workflow - " +
            "Click on the save button - Validate that the user is prevented from saving the record without inserting the mandatory fields")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod18()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .SelectAction("Run Workflow")
                .ClickSaveButton()

                .ValidateTitleFieldErrorLabelVisibility(true)
                .ValidateTargetUserFieldErrorLabelVisibility(true)
                .ValidateActionFieldErrorLabelVisibility(false)
                .ValidateWorkflowFieldErrorLabelVisibility(true)
                .ValidateTargetPageFieldErrorLabelVisibility(true)

                .ValidateTitleFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetUserFieldErrorLabelText("Please fill out this field.")
                .ValidateWorkflowFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetPageFieldErrorLabelText("Please fill out this field.")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-24746")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the record page to be displayed - Set Action = Upload File - " +
            "Click on the save button - Validate that the user is prevented from saving the record without inserting the mandatory fields")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod19()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .SelectAction("Upload File")
                .ClickSaveButton()

                .ValidateTitleFieldErrorLabelVisibility(true)
                .ValidateTargetUserFieldErrorLabelVisibility(true)
                .ValidateActionFieldErrorLabelVisibility(false)
                .ValidateWorkflowFieldErrorLabelVisibility(false)
                .ValidateTargetPageFieldErrorLabelVisibility(false)
                .ValidateRecordFieldErrorLabelVisibility(true)

                .ValidateTitleFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetUserFieldErrorLabelText("Please fill out this field.")
                .ValidateRecordFieldErrorLabelText("Please fill out this field.")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-24747")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the record page to be displayed - Set Action = View Record - " +
            "Click on the save button - Validate that the user is prevented from saving the record without inserting the mandatory fields")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod20()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .SelectAction("View Record")
                .ClickSaveButton()

                .ValidateTitleFieldErrorLabelVisibility(true)
                .ValidateTargetUserFieldErrorLabelVisibility(true)
                .ValidateActionFieldErrorLabelVisibility(false)
                .ValidateWorkflowFieldErrorLabelVisibility(false)
                .ValidateTargetPageFieldErrorLabelVisibility(true)
                .ValidateRecordFieldErrorLabelVisibility(true)

                .ValidateTitleFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetUserFieldErrorLabelText("Please fill out this field.")
                .ValidateRecordFieldErrorLabelText("Please fill out this field.")
                .ValidateTargetPageFieldErrorLabelText("Please fill out this field.")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-24748")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'Upload File' - Select a Case Form in the Record field - Set data in all mandatory fields - Tap on the save button - " +
            "Validate that the user is prevented from saving the record (Combination of Upload File can only be used with Person Attachment records) ")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod21()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
            var caseformID = new Guid("2b139760-e56a-eb11-a308-005056926fe4"); //Automated UI Test Document 1 for Lancaster, Elvia - (10/03/2009) [CAS-000005-0846] Starting 09/02/2021 created by Security Test User Admin


            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("Upload File")
                .TapTargetUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery("317348").TapSearchButton().SelectResultElement(personid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Forms (Case)").TypeSearchQuery("*CAS-000005-0846*").TapSearchButton(30).SelectResultElement(caseformID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("When the action selected is \"Upload File\", the selected record must be an attachment.").TapCloseButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-24749")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'Upload File' - Select a Case Form in the Record field - Set data in all mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod22()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
            var caseformID = new Guid("2b139760-e56a-eb11-a308-005056926fe4"); //Automated UI Test Document 1 for Lancaster, Elvia - (10/03/2009) [CAS-000005-0846] Starting 09/02/2021 created by Security Test User Admin
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("View Record")
                .TapTargetUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery("317348").TapSearchButton().SelectResultElement(personid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .TapRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Forms (Case)").TypeSearchQuery("*CAS-000005-0846*").TapSearchButton(30).SelectResultElement(caseformID.ToString());

            websitePortalTaskRecordPage
               .WaitForWebsitePortalTaskRecordPageToLoad()
               .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("attachment").TapSearchButton().SelectResultElement(websitepageid.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            websitePortalTasksPage
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(records[0].ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001")
                .ValidateActionSelectedText("View Record")
                .ValidateDueDateFieldText(dueDate)
                .ValidateRecordFieldLinkText("Automated UI Test Document 1 for Lancaster, Elvia - (10/03/2009) [CAS-000005-0846] Starting 09/02/2021 created by Security Test User Admin")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }


        [TestProperty("JiraIssueID", "CDV6-24750")]
        [Description("Open a Person record - Navigate to the Portal Tasks subpage - Validate that only portal task records linked to the person are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod23()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personID = new Guid("21c65d8c-e0af-4d11-8a98-1adb27a6dd30"); //Lamont Stevens

            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03
            var portalTaskRecordID2 = new Guid("23bc6b86-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 02
            var portalTaskRecordID1 = new Guid("40cae447-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 01
            var portalTaskRecordDifferentPersonID1 = new Guid("9b02bfcc-732c-495d-b090-636878477edf"); //To-do list - task 3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("23176", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPortalTasksPage();

            PersonPortalTasksPage
                .WaitForPersonPortalTasksPageToLoad()
                .ValidateRecordPresent(portalTaskRecordID3.ToString())
                .ValidateRecordPresent(portalTaskRecordID2.ToString())
                .ValidateRecordPresent(portalTaskRecordID1.ToString())
                .ValidateRecordNotPresent(portalTaskRecordDifferentPersonID1.ToString())

                .ValidateTitleCellText(portalTaskRecordID3.ToString(), "Lamont Stevens Portal Task 03")
                .ValidateTargetUserCellText(portalTaskRecordID3.ToString(), "Lamont Stevens")
                .ValidateActionCellText(portalTaskRecordID3.ToString(), "View Record")
                .ValidateStatusCellText(portalTaskRecordID3.ToString(), "New")
                .ValidatedueDateCellText(portalTaskRecordID3.ToString(), "28/03/2021")

                .ValidateTitleCellText(portalTaskRecordID2.ToString(), "Lamont Stevens Portal Task 02")
                .ValidateTargetUserCellText(portalTaskRecordID2.ToString(), "Lamont Stevens")
                .ValidateActionCellText(portalTaskRecordID2.ToString(), "Upload File")
                .ValidateStatusCellText(portalTaskRecordID2.ToString(), "New")
                .ValidatedueDateCellText(portalTaskRecordID2.ToString(), "18/03/2021")

                .ValidateTitleCellText(portalTaskRecordID1.ToString(), "Lamont Stevens Portal Task 01")
                .ValidateTargetUserCellText(portalTaskRecordID1.ToString(), "Lamont Stevens")
                .ValidateActionCellText(portalTaskRecordID1.ToString(), "Run Workflow")
                .ValidateStatusCellText(portalTaskRecordID1.ToString(), "New")
                .ValidatedueDateCellText(portalTaskRecordID1.ToString(), "28/03/2021");

        }

        [TestProperty("JiraIssueID", "CDV6-24751")]
        [Description("Open a Person record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on a portal task record (Action = 'View Record') - " +
            "Validate that the user is redirected to the portal task record page - " +
            "Validate that both Record and Target Page fields are displayed - " +
            "Validate that the Workflow field is not displayed")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod24()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personID = new Guid("21c65d8c-e0af-4d11-8a98-1adb27a6dd30"); //Lamont Stevens
            var portalTaskRecordID3 = new Guid("6020949e-c46a-eb11-a308-005056926fe4"); //Lamont Stevens Portal Task 03

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("23176", personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPortalTasksPage();

            PersonPortalTasksPage
                .WaitForPersonPortalTasksPageToLoad()
                .ClickOnRecord(portalTaskRecordID3.ToString());

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(true)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(false)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Lamont Stevens")
                .ValidateTitleFieldText("Lamont Stevens Portal Task 03")
                .ValidateActionSelectedText("View Record")
                .ValidateDueDateFieldText("28/03/2021")
                .ValidateRecordFieldLinkText("Attachment 003")
                .ValidateStatusSelectedText("New")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24752")]
        [Description("Open a Person record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Click on the add new record button - Wait for the new record page to be displayed - " +
            "Set Action to 'Run Workflow' - Set data in all mandatory fields - Tap on the save button - Validate that the record is correctly saved")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod25()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy");
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment

            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("317348", personid.ToString())
                .OpenPersonRecord(personid.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPortalTasksPage();

            PersonPortalTasksPage
                .WaitForPersonPortalTasksPageToLoad()
                .ClicAddNewRecordButton();

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()
                .InsertTitle("Elvia Lancaster Portal Task 001")
                .InsertDueDate(dueDate)
                .SelectAction("Run Workflow")
                .TapWebsiteLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(portalWebsiteName).TapSearchButton().SelectResultElement(websiteid.ToString());

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()
                .TapWorkflowLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automation - Website On Demand Workflow - Person").TapSearchButton().SelectResultElement(workflowid.ToString());

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()
                .TapTargetPageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("attachment").TapSearchButton().SelectResultElement(websitepageid.ToString());

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()
                .ClickSaveAndCloseButton();

            PersonPortalTasksPage
                .WaitForPersonPortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(1, records.Count);

            PersonPortalTasksPage
                .ClickOnRecord(records[0].ToString());

            PersonPortalTaskRecordPage
                .WaitForPersonPortalTaskRecordPageToLoad()

                .ValidateRecordFieldVisibility(false)
                .ValidateTargetPageFieldVisibility(true)
                .ValidateWorkflowFieldVisibility(true)

                .ValidateWebSiteFieldLinkText(portalWebsiteName)
                .ValidateTargetUserFieldLinkText("Elvia Lancaster")
                .ValidateTitleFieldText("Elvia Lancaster Portal Task 001")
                .ValidateActionSelectedText("Run Workflow")
                .ValidateDueDateFieldText(dueDate)
                .ValidateWorkflowFieldLinkText("Automation - Website On Demand Workflow - Person")
                .ValidateTargetPageFieldLinkText("attachment")
                .ValidateStatusSelectedText("New")
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA");


        }

        [TestProperty("JiraIssueID", "CDV6-24753")]
        [Description("Open a Website record - Navigate to the Portal Tasks page - Wait for the Portal Tasks page to load - " +
            "Open existing record - Wait for the record page to be displayed - " +
            "Tap on the Delete button - Confirm the delete operation - validate that the record is removed from the Database")]
        [TestMethod, TestCategory("UITest")]
        public void PortalTaskBOImplementation_UITestMethod26()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserid = new Guid("c54cc3eb-c45f-eb11-a306-005056926fe4"); //StaffordshireCitizenPortalUser3@mail.com
            var personid = new Guid("9dbd921b-58de-4e54-bb56-a57732b73ac3"); //Elvia Lancaster
            var dueDate = DateTime.Now.AddDays(2);
            var workflowid = new Guid("920f1ccf-0425-eb11-a2cd-005056926fe4"); //Automation - Website On Demand Workflow - Person
            var websitepageid = new Guid("21b55b8a-a360-eb11-9c1e-989096c9c0a8"); //attachment
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            //remove all portal tasks for the person
            foreach (var recordid in dbHelper.portalTask.GetByTargetUserId(personid))
                dbHelper.portalTask.DeletePortalTask(recordid);

            //Create a new record
            var taskRecordID = dbHelper.portalTask.CreatePortalTask(teamid, dueDate, 1, 1, "Elvia Lancaster Portal Task 001", websiteid,
                personid, "person", "Elvia Lancaster",
                null, null, null,
                websitepageid, workflowid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteid.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToPortalTasks();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad()
                .InsertSearchQuery("*Elvia*")
                .ClickSearchButton()
                .ClickOnRecord(taskRecordID.ToString());

            websitePortalTaskRecordPage
                .WaitForWebsitePortalTaskRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            websitePortalTasksPage
                .WaitForWebsitePortalTasksPageToLoad();

            var records = dbHelper.portalTask.GetByTargetUserId(personid);
            Assert.AreEqual(0, records.Count);

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-8405

        [TestProperty("JiraIssueID", "CDV6-24754")]
        [Description("Login in the web app - Open a Website record - Navigate to the Users area - Open a Website User record - Tap on the Password Reset button - " +
            "Validate that a password reset record is created (Validate all fields for the created record)")]
        [TestMethod, TestCategory("UITest")]
        public void ResetPassword_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("70867865-d624-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser14@mail.com

            //remove all matching Users
            foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("WebSiteAutomationUser14@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickResetPasswordButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Reset Password record has been successfully created").TapCloseButton();

            var records = dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);

            string expectedResetPassLink = "http://AutomationWebSite07.com/reset-password?id=" + records[0].ToString();
            var fields = dbHelper.websiteUserPasswordReset.GetByID(records[0], "websiteuserid", "resetpasswordlink", "expireon");
            Assert.AreEqual(websiteUserID, fields["websiteuserid"]);
            Assert.AreEqual(expectedResetPassLink, fields["resetpasswordlink"]);
            Assert.IsTrue(((DateTime)fields["expireon"]).ToLocalTime() > DateTime.Now);

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-7959

        [TestProperty("JiraIssueID", "CDV6-24755")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5410 - " +
           "Create a website user pin record with (set the expire on date to 50 minutes in the past) - Execute the 'Clear Expired Pin Records' scheduled job" +
           "Validate that the pin record is not removed")]
        [TestMethod, TestCategory("UITest")]
        public void CDV6_7959_UITestMethod01()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddMinutes(-50);
            var seton = DateTime.Now.AddDays(-50);
            var userPinRecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Pin Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredPinRecordsScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredPinRecordsScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ValidateRecordPresent(userPinRecordID.ToString()); //record should be present (Job should only remove records older than 1 hour)

            var records = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(1, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-24756")]
        [Description("Create a website user pin record with (set the expire on date to 90 minutes in the past) - Execute the 'Clear Expired Pin Records' scheduled job" +
           "Validate that the pin record is not removed")]
        [TestMethod, TestCategory("UITest")]
        public void CDV6_7959_UITestMethod02()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websiteUserID = new Guid("ff61af3b-2720-eb11-a2cd-005056926fe4"); //WebSiteAutomationUser1@mail.com

            //Remove all pin records from the user
            foreach (var recordid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(recordid);

            //Create a new pin record
            var expireon = DateTime.Now.AddMinutes(-90);
            var seton = DateTime.Now.AddDays(-90);
            var userPinRecordID = dbHelper.websiteUserPin.CreateWebsiteUserPin(websiteUserID, "1234", expireon, seton, "Some error ...");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Clear Expired Pin Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(ClearExpiredPinRecordsScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(ClearExpiredPinRecordsScheduledJob);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 07")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClickOnWebSiteUserRecord(websiteUserID.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .NavigateToWebsiteUserPIN();

            webSiteUserPinsPage
                .WaitForWebSiteUserPinsPageToLoad()
                .ValidateRecordNotPresent(userPinRecordID.ToString()); //record should not be present (Job should only remove records older than 1 hour)

            var records = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserID);
            Assert.AreEqual(0, records.Count);
        }
        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-10309

        [TestProperty("JiraIssueID", "CDV6-24757")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-10309 - " +
            "Login in the web app - Open a Website record (Email Verification Required = Yes) - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - For the profile field select a BO record that match the website 'User Record Type' - Tap on the save button - " +
            "Validate that the record is saved ")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserProfilesToPointToWebsiteUserRecordType_UITestMethod01()
        {
            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var personid = new Guid("1b6bf252-d9d5-4057-9fff-5454dc44dbbb"); //Bridgett Nicholson
            var securityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 17")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("website17user4@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedNoOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("Email")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("76183").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("FullAccess").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var websiteUsers = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com");
            Assert.AreEqual(1, websiteUsers.Count);

            var emailVerificationRecords = dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUsers[0]);
            Assert.AreEqual(1, emailVerificationRecords.Count);
            var emailVerificationFields = dbHelper.websiteUserEmailVerification.GetByID(emailVerificationRecords[0], "link");
            string expectedVerificationLink = "https://automationwebsite17.com/email-verification?id=" + emailVerificationRecords[0].ToString().ToLower();
            Assert.AreEqual(expectedVerificationLink, emailVerificationFields["link"]);

            var emails = dbHelper.email.GetEmailByRegardingID(websiteUsers[0]);
            Assert.AreEqual(1, emails.Count);
            var emailFields = dbHelper.email.GetEmailByID(emails[0], "notes");
            Assert.AreEqual(expectedVerificationLink, emailFields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24758")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-10309 - " +
            "Login in the web app - Open a Website record (Email Verification Required = Yes) - Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields - For the profile field select a BO record that DO NOT match the website 'User Record Type' - Tap on the save button - " +
            "Validate that an error is displayed to the user preventing him from saving the record ")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserProfilesToPointToWebsiteUserRecordType_UITestMethod02()
        {
            var websiteID = new Guid("1e8dabce-8a40-eb11-a2e4-005056926fe4"); //Automation - Web Site 17
            var professionalid = new Guid("128aaba7-443f-e911-a2c5-005056926fe4"); //DR Rena Morris
            var securityProfile = new Guid("fc7747b2-1e41-eb11-a2e5-005056926fe4"); //FullAccess

            //Remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserEmailVerification.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserEmailVerification.DeleteWebsiteUserEmailVerification(passwordReset);

                foreach (var email in dbHelper.email.GetEmailByRegardingID(websiteUserID))
                    dbHelper.email.DeleteEmail(email);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site 17")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("website17user4@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedNoOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("Email")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectBusinessObjectByText("Professionals").TypeSearchQuery("rena morris").TapSearchButton().SelectResultElement(professionalid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("FullAccess").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Website User 'Profile Id' should be the same as Website 'User Record Type', in this case 'Person'")
                .TapCloseButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad();

            var websiteUsers = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "website17user4@mail.com");
            Assert.AreEqual(0, websiteUsers.Count);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-10520

        [TestProperty("JiraIssueID", "CDV6-24759")]
        [Description("Login in the web App - Click on the Settings menu button - Click on the Portals Area - Click on Website Users link - " +
            "Validate that the user is redirected to the Website Users page")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserGeneralPage_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteUserSubLink();

            genericWebSiteUsersPage
                .WaitForGenericWebSiteUsersPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24760")]
        [Description("Login in the web App - Click on the Settings menu button - Click on the Portals Area - Click on Website Users link - " +
            "Wait for the Website Users page to load - Search for a website user belonging to website 'Staffordshire Citizen Portal' - " +
            "Validate that the record is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserGeneralPage_UITestMethod02()
        {
            var wbesiteUser1 = new Guid("42aeddc4-7e77-eb11-a30e-005056926fe4"); //StaffordshireCitizenPortalUser26@mail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteUserSubLink();

            genericWebSiteUsersPage
                .WaitForGenericWebSiteUsersPageToLoad()
                .InsertSearchQuery("StaffordshireCitizenPortalUser26@mail.com")
                .ClickSearchButton()
                .ValidateRecordPresent(wbesiteUser1.ToString())
                .ValidateWebsiteCellText(wbesiteUser1.ToString(), portalWebsiteName)
                .ValidateUserNameCellText(wbesiteUser1.ToString(), "StaffordshireCitizenPortalUser26@mail.com")
                .ValidateProfileCellText(wbesiteUser1.ToString(), "Polly Norton")
                .ValidateStatusCellText(wbesiteUser1.ToString(), "Approved");
        }

        [TestProperty("JiraIssueID", "CDV6-24761")]
        [Description("Login in the web App - Click on the Settings menu button - Click on the Portals Area - Click on Website Users link - " +
            "Wait for the Website Users page to load - Search for a website user belonging to website 'Provider Portal' - " +
            "Validate that the record is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserGeneralPage_UITestMethod03()
        {
            var wbesiteUser1 = new Guid("f286423c-70a6-eb11-a323-005056926fe4"); //ProviderPortalUser2@somemail.com

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebsiteUserSubLink();

            genericWebSiteUsersPage
                .WaitForGenericWebSiteUsersPageToLoad()
                .InsertSearchQuery("ProviderPortalUser2@somemail.com")
                .ClickSearchButton()
                .ValidateRecordPresent(wbesiteUser1.ToString())
                .ValidateWebsiteCellText(wbesiteUser1.ToString(), "Local Authority Provider Portal")
                .ValidateUserNameCellText(wbesiteUser1.ToString(), "ProviderPortalUser2@somemail.com")
                .ValidateProfileCellText(wbesiteUser1.ToString(), "Mr Constantine McDonnald Staff Member of ProviderPortal01")
                .ValidateStatusCellText(wbesiteUser1.ToString(), "Approved");
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-10218

        [TestProperty("JiraIssueID", "CDV6-24762")]
        [Description("Open a Website record - Click on the Clone button - Validate that the clone popup is displayed")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod01()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24763")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Click on the Clone button - " +
            "Validate that an error is displayed to the user indicating that the website name field is mandatory")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod02()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .ClickCloneButton()

                .ValidateWebsiteNameErrorLabelVisibility(true)
                .ValidateWebsiteNameErrorLabelText("Please insert a name for the cloned website.");
        }

        [TestProperty("JiraIssueID", "CDV6-24764")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website General Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod03()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0], "name", "homepage", "memberhomepage", "footer", "stylesheet", "script", "hideloginregistration", "version",
                "applicationid", "userrecordtypeid", "registrationdataformname", "recordsperpage", "sessiontimeout", "hidefeedback");

            var expectedApplicationId = new Guid("256e6219-1925-eb11-a2ce-0050569231cf");
            var userRecordTypeId = new Guid("30f84b2d-b169-e411-bf00-005056c00008");
            Assert.AreEqual("Automation - Clone Website CDV610218", websitefields["name"]);
            Assert.AreEqual("home_page", websitefields["homepage"]);
            Assert.AreEqual("member_home_page", websitefields["memberhomepage"]);
            Assert.AreEqual("Footer.Html", websitefields["footer"]);
            Assert.AreEqual("sytlesheet.css", websitefields["stylesheet"]);
            Assert.AreEqual("script.js", websitefields["script"]);
            Assert.AreEqual(false, websitefields["hideloginregistration"]);
            Assert.AreEqual("1", websitefields["version"]);
            Assert.AreEqual(expectedApplicationId, websitefields["applicationid"]);
            Assert.AreEqual(userRecordTypeId, websitefields["userrecordtypeid"]);
            Assert.AreEqual("registration", websitefields["registrationdataformname"]);
            Assert.AreEqual(10, websitefields["recordsperpage"]);
            Assert.AreEqual(60, websitefields["sessiontimeout"]);
            Assert.AreEqual(false, websitefields["hidefeedback"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24765")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website Administration Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod04()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0], "websiteurl", "robotsecuritytypeid", "emailverificationrequired", "userapprovalrequired");

            Assert.AreEqual("https://AutomationWebSiteCloning.com", websitefields["websiteurl"]);
            Assert.AreEqual(2, websitefields["robotsecuritytypeid"]);
            Assert.AreEqual(true, websitefields["emailverificationrequired"]);
            Assert.AreEqual(true, websitefields["userapprovalrequired"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24766")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website Password Complexity Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod05()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0], "minimumpasswordlength", "minimumspecialcharacters", "specialcharactersallowed",
                "minimumnumericcharacters", "minimumuppercaseletters", "minimumlowercaseletters");

            Assert.AreEqual(5, websitefields["minimumpasswordlength"]);
            Assert.AreEqual(2, websitefields["minimumspecialcharacters"]);
            Assert.AreEqual("4", websitefields["specialcharactersallowed"]);
            Assert.AreEqual(1, websitefields["minimumnumericcharacters"]);
            Assert.AreEqual(3, websitefields["minimumuppercaseletters"]);
            Assert.AreEqual(5, websitefields["minimumlowercaseletters"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24767")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website Password Policy Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod06()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0], "maximumpasswordage", "enforcepasswordhistory", "minimumpasswordage", "passwordresetexpirein");

            Assert.AreEqual(365, websitefields["maximumpasswordage"]);
            Assert.AreEqual(2, websitefields["enforcepasswordhistory"]);
            Assert.AreEqual(0, websitefields["minimumpasswordage"]);
            Assert.AreEqual(5, websitefields["passwordresetexpirein"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24768")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website Account Lockout Policy Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod07()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0], "enableaccountlocking", "accountlockoutthreshold", "accountlockoutduration");

            Assert.AreEqual(true, websitefields["enableaccountlocking"]);
            Assert.AreEqual(2, websitefields["accountlockoutthreshold"]);
            Assert.AreEqual(1, websitefields["accountlockoutduration"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24769")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that the website Two Factor Authentication Area is correctly cloned")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod08()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websitefields = dbHelper.website.GetWebsiteByID(websites[0],
                "enabletwofactorauthentication", "pinexpirein", "maxinvalidpinattemptallowed", "defaultpinreceivingmethodid", "numberofpindigits");

            Assert.AreEqual(true, websitefields["enabletwofactorauthentication"]);
            Assert.AreEqual(5, websitefields["pinexpirein"]);
            Assert.AreEqual(6, websitefields["maxinvalidpinattemptallowed"]);
            Assert.AreEqual(1, websitefields["defaultpinreceivingmethodid"]);
            Assert.AreEqual(4, websitefields["numberofpindigits"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24770")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Localized Strings are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod09()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websiteLocalizedStrings = dbHelper.websiteLocalizedString.GetByWebSiteID(websites[0]);
            Assert.AreEqual(2, websiteLocalizedStrings.Count);



            websiteLocalizedStrings = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString1");
            Assert.AreEqual(1, websiteLocalizedStrings.Count);

            var fields = dbHelper.websiteLocalizedString.GetByID(websiteLocalizedStrings[0], "simpletext");
            Assert.AreEqual("LocalizedString1 Text", fields["simpletext"]);



            websiteLocalizedStrings = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString2");
            Assert.AreEqual(1, websiteLocalizedStrings.Count);

            fields = dbHelper.websiteLocalizedString.GetByID(websiteLocalizedStrings[0], "simpletext");
            Assert.AreEqual("LocalizedString2 Text", fields["simpletext"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24771")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Localized String Values are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod10()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var englishLanguageId = new Guid("8342d323-f800-4f4f-b4b0-103656524558"); //English (UK)
            var spanishLanguageId = new Guid("b18f6a82-1710-e711-9be0-1866da1e4209"); //Spanish
            var welshLanguageId = new Guid("0a386652-c008-490a-b479-26de84bcd10b"); //Welsh


            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");

            var websiteLocalizedStrings = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString1");
            Assert.AreEqual(1, websiteLocalizedStrings.Count);

            var websiteLocalizedStringValues = dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStrings[0]);
            Assert.AreEqual(2, websiteLocalizedStringValues.Count);



            websiteLocalizedStringValues = dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringIDAndLanguage(websiteLocalizedStrings[0], englishLanguageId);
            var fields = dbHelper.websiteLocalizedStringValue.GetByID(websiteLocalizedStringValues[0], "simpletext");
            Assert.AreEqual("LS1 English Text", fields["simpletext"]);



            websiteLocalizedStringValues = dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringIDAndLanguage(websiteLocalizedStrings[0], spanishLanguageId);
            fields = dbHelper.websiteLocalizedStringValue.GetByID(websiteLocalizedStringValues[0], "simpletext");
            Assert.AreEqual("LS1 Spanish Text", fields["simpletext"]);





            websiteLocalizedStrings = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString2");
            Assert.AreEqual(1, websiteLocalizedStrings.Count);

            websiteLocalizedStringValues = dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStrings[0]);
            Assert.AreEqual(1, websiteLocalizedStringValues.Count);

            websiteLocalizedStringValues = dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringIDAndLanguage(websiteLocalizedStrings[0], welshLanguageId);
            fields = dbHelper.websiteLocalizedStringValue.GetByID(websiteLocalizedStringValues[0], "simpletext");
            Assert.AreEqual("LS1 Welsh Text", fields["simpletext"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24772")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Record Types are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod11()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();


            var caseBusinessObjectID = new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"); //Case
            var personBusinessObjectID = new Guid("30f84b2d-b169-e411-bf00-005056c00008"); //Case
            var PortalCaseMainDataViewID = new Guid("c363a358-6722-eb11-a2ce-0050569231cf"); //PortalCaseMain
            var myRecordCitizenPortalDataViewID = new Guid("bf48f24b-4662-eb11-a312-0050569231cf"); //My Record (Citizen Portal)

            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");
            Assert.AreEqual(1, websites.Count);

            var websiteRecordTypes = dbHelper.websiteRecordType.GetByWebSiteID(websites[0]);
            Assert.AreEqual(2, websiteRecordTypes.Count);



            websiteRecordTypes = dbHelper.websiteRecordType.GetByWebSiteIdAndRecordTypeId(websites[0], caseBusinessObjectID);
            Assert.AreEqual(1, websiteRecordTypes.Count);

            var fields = dbHelper.websiteRecordType.GetByID(websiteRecordTypes[0], "issecure", "dataviewid", "cancreate", "canupdate", "canretrievebyid", "canretrievemultiple");
            Assert.AreEqual(false, fields["issecure"]);
            Assert.AreEqual(PortalCaseMainDataViewID, fields["dataviewid"]);
            Assert.AreEqual(false, fields["cancreate"]);
            Assert.AreEqual(false, fields["canupdate"]);
            Assert.AreEqual(false, fields["canretrievebyid"]);
            Assert.AreEqual(true, fields["canretrievemultiple"]);


            websiteRecordTypes = dbHelper.websiteRecordType.GetByWebSiteIdAndRecordTypeId(websites[0], personBusinessObjectID);
            Assert.AreEqual(1, websiteRecordTypes.Count);

            fields = dbHelper.websiteRecordType.GetByID(websiteRecordTypes[0], "issecure", "dataviewid", "cancreate", "canupdate", "canretrievebyid", "canretrievemultiple");
            Assert.AreEqual(true, fields["issecure"]);
            Assert.AreEqual(myRecordCitizenPortalDataViewID, fields["dataviewid"]);
            Assert.AreEqual(true, fields["cancreate"]);
            Assert.AreEqual(true, fields["canupdate"]);
            Assert.AreEqual(true, fields["canretrievebyid"]);
            Assert.AreEqual(false, fields["canretrievemultiple"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24773")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Pages are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod12()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var websites = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218");

            var LocalizedString1Id = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString1")[0];
            var LocalizedString2Id = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(websites[0], "LocalizedString2")[0];

            var websitePages = dbHelper.websitePage.GetByWebSiteID(websites[0]);
            Assert.AreEqual(2, websitePages.Count);




            var homepageId = dbHelper.websitePage.GetByWebSiteIDAndExactPageName(websites[0], "home_page");
            Assert.AreEqual(1, homepageId.Count);

            var fields = dbHelper.websitePage.GetByID(homepageId[0], "sitemaporbreadcrumbtextid", "parentpageid", "headertextid", "displayheadertitle", "widgetscriptfiles", "widgetstylesheets", "issecure", "layoutjson");

            Assert.AreEqual(LocalizedString1Id, fields["sitemaporbreadcrumbtextid"]);
            Assert.AreEqual(false, fields.ContainsKey("parentpageid"));
            Assert.AreEqual(false, fields.ContainsKey("headertextid"));
            Assert.AreEqual(true, fields["displayheadertitle"]);
            Assert.AreEqual("widget.js", fields["widgetscriptfiles"]);
            Assert.AreEqual("stylesheet.css", fields["widgetstylesheets"]);
            Assert.AreEqual(false, fields["issecure"]);
            Assert.AreEqual("{\"Widgets\":[{\"title\":\"Case - Main\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"Cases\",\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\",\"ListActionId\":1}},{\"title\":\"Person - My Record (Citizen Portal)\",\"x\":3.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"Person\",\"Type\":2,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataViewId\":\"bf48f24b-4662-eb11-a312-0050569231cf\",\"ListActionId\":1}}]}", fields["layoutjson"]);

            var websitePageFiles = dbHelper.websitePageFile.GetByWebsitePageId(homepageId[0]);
            Assert.AreEqual(0, websitePageFiles.Count);






            var member_home_page = dbHelper.websitePage.GetByWebSiteIDAndExactPageName(websites[0], "member_home_page");
            Assert.AreEqual(1, member_home_page.Count);

            fields = dbHelper.websitePage.GetByID(member_home_page[0], "sitemaporbreadcrumbtextid", "parentpageid", "headertextid", "displayheadertitle", "widgetscriptfiles", "widgetstylesheets", "issecure", "layoutjson");

            Assert.AreEqual(LocalizedString1Id, fields["sitemaporbreadcrumbtextid"]);
            Assert.AreEqual(homepageId[0], fields["parentpageid"]);
            Assert.AreEqual(LocalizedString2Id, fields["headertextid"]);
            Assert.AreEqual(false, fields["displayheadertitle"]);
            Assert.AreEqual(false, fields.ContainsKey("widgetscriptfiles"));
            Assert.AreEqual(false, fields.ContainsKey("widgetstylesheets"));
            Assert.AreEqual(true, fields["issecure"]);
            Assert.AreEqual("{\"Widgets\":[{\"title\":\"Person - My Record (Citizen Portal)\",\"x\":0.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"person\",\"Type\":2,\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"DataViewId\":\"bf48f24b-4662-eb11-a312-0050569231cf\",\"ListActionId\":1}},{\"title\":\"Case - Main\",\"x\":3.0,\"y\":0.0,\"width\":3,\"height\":3,\"settings\":{\"Id\":\"cases\",\"Type\":2,\"BusinessObjectId\":\"79f4efc4-bfb1-e811-80dc-0050560502cc\",\"DataViewId\":\"c363a358-6722-eb11-a2ce-0050569231cf\",\"ListActionId\":1}}]}", fields["layoutjson"]);

            websitePageFiles = dbHelper.websitePageFile.GetByWebsitePageId(member_home_page[0]);
            Assert.AreEqual(2, websitePageFiles.Count);


        }

        [TestProperty("JiraIssueID", "CDV6-24774")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Page Files are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod13()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var filejs = dbHelper.websiteResourceFile.GetByWebSiteIDAndName(clonedwebsiteid, "File.js")[0];
            var filecss = dbHelper.websiteResourceFile.GetByWebSiteIDAndName(clonedwebsiteid, "File.css")[0];

            var member_home_page = dbHelper.websitePage.GetByWebSiteIDAndExactPageName(clonedwebsiteid, "member_home_page")[0];

            var websitePageFiles = dbHelper.websitePageFile.GetByWebsitePageId(member_home_page);
            Assert.AreEqual(2, websitePageFiles.Count);


            var websitePageFile = dbHelper.websitePageFile.GetByWebsitePageId(member_home_page, filejs)[0];
            var fields = dbHelper.websitePageFile.GetByID(websitePageFile, "websiteid", "loadorder");
            Assert.AreEqual(clonedwebsiteid, fields["websiteid"]);
            Assert.AreEqual(1, fields["loadorder"]);


            websitePageFile = dbHelper.websitePageFile.GetByWebsitePageId(member_home_page, filecss)[0];
            fields = dbHelper.websitePageFile.GetByID(websitePageFile, "websiteid", "loadorder");
            Assert.AreEqual(clonedwebsiteid, fields["websiteid"]);
            Assert.AreEqual(2, fields["loadorder"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24775")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Resource Files are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod14()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var websiteResourceFiles = dbHelper.websiteResourceFile.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(4, websiteResourceFiles.Count);

            var websiteResourceFile = dbHelper.websiteResourceFile.GetByWebSiteIDAndName(clonedwebsiteid, "File.css")[0];
            var fields = dbHelper.websiteResourceFile.GetByID(websiteResourceFile, "filetypeid", "file", "description");
            Assert.AreEqual(1, fields["filetypeid"]);
            Assert.AreEqual(true, fields.ContainsKey("file"));
            Assert.AreEqual("css file", fields["description"]);

            websiteResourceFile = dbHelper.websiteResourceFile.GetByWebSiteIDAndName(clonedwebsiteid, "Capture.PNG")[0];
            fields = dbHelper.websiteResourceFile.GetByID(websiteResourceFile, "filetypeid", "file", "description");
            Assert.AreEqual(4, fields["filetypeid"]);
            Assert.AreEqual(true, fields.ContainsKey("file"));
            Assert.AreEqual("png file", fields["description"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24776")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Sitemaps are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod15()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var home_pageid = dbHelper.websitePage.GetByWebSiteIDAndPageName(clonedwebsiteid, "home_page")[0];
            var member_home_page = dbHelper.websitePage.GetByWebSiteIDAndPageName(clonedwebsiteid, "member_home_page")[0];

            var websiteSitemaps = dbHelper.websiteSitemap.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(2, websiteSitemaps.Count);

            var websiteSitemap = dbHelper.websiteSitemap.GetByWebSiteIDAndName(clonedwebsiteid, "sitemap 1")[0];
            var fields = dbHelper.websiteSitemap.GetByID(websiteSitemap, "typeid", "sitemapjson");
            Assert.AreEqual(1, fields["typeid"]);
            Assert.AreEqual("{\"NodeCollection\":[{\"Name\":\"home_page\",\"Title\":\"home_page\",\"PageId\":\"" + home_pageid.ToString() + "\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", fields["sitemapjson"]);

            websiteSitemap = dbHelper.websiteSitemap.GetByWebSiteIDAndName(clonedwebsiteid, "sitemap 2")[0];
            fields = dbHelper.websiteSitemap.GetByID(websiteSitemap, "typeid", "sitemapjson");
            Assert.AreEqual(2, fields["typeid"]);
            Assert.AreEqual("{\"NodeCollection\":[{\"Name\":\"member_home_page\",\"Title\":\"member_home_page\",\"PageId\":\"" + member_home_page.ToString() + "\",\"TypeId\":1,\"Link\":\"\",\"ChildNodes\":null}]}", fields["sitemapjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24777")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Security Profiles are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod16()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var home_pageid = dbHelper.websitePage.GetByWebSiteIDAndPageName(clonedwebsiteid, "home_page")[0];
            var member_home_page = dbHelper.websitePage.GetByWebSiteIDAndPageName(clonedwebsiteid, "member_home_page")[0];

            var websiteSecurityProfiles = dbHelper.websiteSecurityProfile.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(2, websiteSecurityProfiles.Count);

            var websiteSecurityProfile = dbHelper.websiteSecurityProfile.GetByWebSiteIdAndName(clonedwebsiteid, "Full Access")[0];
            var fields = dbHelper.websiteSecurityProfile.GetByID(websiteSecurityProfile, "pagesaccessjson", "recordsaccessjson");
            Assert.AreEqual("[{\"PageId\":\"" + member_home_page.ToString() + "\",\"Label\":\"LS1 English Text\",\"View\":true}]", fields["pagesaccessjson"]);
            Assert.AreEqual("[{\"BusinessObjectId\":\"30f84b2d-b169-e411-bf00-005056c00008\",\"Label\":\"Person\",\"View\":true,\"Create\":true,\"Edit\":true,\"Delete\":true}]", fields["recordsaccessjson"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24778")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Settings are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod17()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var websiteSettings = dbHelper.websiteSetting.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(2, websiteSettings.Count);

            var websiteSetting = dbHelper.websiteSetting.GetByWebSiteIDAndName(clonedwebsiteid, "Setting 1")[0];
            var fields = dbHelper.websiteSetting.GetByID(websiteSetting, "description", "settingvalue", "isencrypted");
            Assert.AreEqual("Setting 1 description", fields["description"]);
            Assert.AreEqual("Setting 1 Value", fields["settingvalue"]);
            Assert.AreEqual(false, fields["isencrypted"]);

            websiteSetting = dbHelper.websiteSetting.GetByWebSiteIDAndName(clonedwebsiteid, "Setting 2")[0];
            fields = dbHelper.websiteSetting.GetByID(websiteSetting, "description", "encryptedvalue", "isencrypted");
            Assert.AreEqual("Setting 2 Description", fields["description"]);
            Assert.AreEqual("IuHEYvg42hiQubZCBu5m2g==", fields["encryptedvalue"]);
            Assert.AreEqual(true, fields["isencrypted"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24779")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Handlers are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod18()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var websiteHandlers = dbHelper.websiteHandler.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(2, websiteHandlers.Count);

            var websiteHandler = dbHelper.websiteHandler.GetByWebSiteIDAndName(clonedwebsiteid, "handler1")[0];
            var fields = dbHelper.websiteHandler.GetByID(websiteHandler, "issecure");
            Assert.AreEqual(false, fields["issecure"]);

            websiteHandler = dbHelper.websiteHandler.GetByWebSiteIDAndName(clonedwebsiteid, "handler2")[0];
            fields = dbHelper.websiteHandler.GetByID(websiteHandler, "issecure");
            Assert.AreEqual(false, fields["issecure"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24780")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Splash Screen are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod19()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var websiteSplashScreens = dbHelper.websiteSplashScreen.GetByWebSiteID(clonedwebsiteid);
            Assert.AreEqual(1, websiteSplashScreens.Count);

            var websiteSplashScreen = dbHelper.websiteSplashScreen.GetByWebSiteIDAndName(clonedwebsiteid, "SplashScreen1")[0];
            Assert.IsNotNull(websiteSplashScreen);
        }

        [TestProperty("JiraIssueID", "CDV6-24781")]
        [Description("Open a Website record - Click on the Clone button - Wait for the clone popup to be displayed - Insert an name for the cloned website - " +
            "Click on the Clone button - Validate that a new website record is created - Validate that all Website Splash Screen Items are copied")]
        [TestMethod, TestCategory("UITest")]
        public void CLoneWebsite_UITestMethod20()
        {
            var mainWebsiteId = new Guid("c02bf731-a5b4-eb11-a323-005056926fe4"); //Automation - Web Site Cloning

            foreach (var websiteid in dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218"))
            {
                foreach (var websiteSplashScreenid in dbHelper.websiteSplashScreen.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteSplashScreenItemid in dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreenid))
                        dbHelper.websiteSplashScreenItem.DeleteWebsiteSplashScreenItem(websiteSplashScreenItemid);

                    dbHelper.websiteSplashScreen.DeleteWebsiteSplashScreen(websiteSplashScreenid);
                }

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                    dbHelper.websitePage.UpdateParentPage(websitePageid, null);

                foreach (var websitePageid in dbHelper.websitePage.GetByWebSiteID(websiteid))
                {
                    foreach (var websitePageFileid in dbHelper.websitePageFile.GetByWebsitePageId(websitePageid))
                        dbHelper.websitePageFile.DeleteWebsitePageFile(websitePageFileid);

                    dbHelper.websitePage.DeleteWebsitePage(websitePageid);
                }

                foreach (var websiteLocalizedStringid in dbHelper.websiteLocalizedString.GetByWebSiteID(websiteid))
                {
                    foreach (var websiteLocalizedStringValueid in dbHelper.websiteLocalizedStringValue.GetByWebsiteLocalizedStringID(websiteLocalizedStringid))
                        dbHelper.websiteLocalizedStringValue.DeleteWebsiteLocalizedStringValue(websiteLocalizedStringValueid);

                    dbHelper.websiteLocalizedString.DeleteWebsiteLocalizedString(websiteLocalizedStringid);
                }

                foreach (var websiteHandlerid in dbHelper.websiteHandler.GetByWebSiteID(websiteid))
                    dbHelper.websiteHandler.DeleteWebsiteHandler(websiteHandlerid);

                foreach (var websiteSettingid in dbHelper.websiteSetting.GetByWebSiteID(websiteid))
                    dbHelper.websiteSetting.DeleteWebsiteSetting(websiteSettingid);

                foreach (var websiteSecurityProfileid in dbHelper.websiteSecurityProfile.GetByWebSiteID(websiteid))
                    dbHelper.websiteSecurityProfile.DeleteWebsiteSecurityProfile(websiteSecurityProfileid);

                foreach (var websiteSitemapid in dbHelper.websiteSitemap.GetByWebSiteID(websiteid))
                    dbHelper.websiteSitemap.DeleteWebsiteSitemap(websiteSitemapid);

                foreach (var websiteResourceFileid in dbHelper.websiteResourceFile.GetByWebSiteID(websiteid))
                    dbHelper.websiteResourceFile.DeleteWebsiteResourceFile(websiteResourceFileid);

                foreach (var websiteRecordTypeid in dbHelper.websiteRecordType.GetByWebSiteID(websiteid))
                    dbHelper.websiteRecordType.DeleteWebsiteRecordType(websiteRecordTypeid);


                dbHelper.website.DeleteWebsite(websiteid);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery("Automation - Web Site Cloning")
                .ClickSearchButton()
                .ClickOnWebSiteRecord(mainWebsiteId.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .ClickCloneButton();

            cloneWebsitePopup
                .WaitForCloneWebsitePopupToLoad()
                .InsertWebsiteName("Automation - Clone Website CDV610218")
                .ClickCloneButton()

                .WaitForPercentageBar("100%")
                .WaitForNotifyArea("Clone finished")

                .ClickCloseButton();



            var clonedwebsiteid = dbHelper.website.GetWebSiteByName("Automation - Clone Website CDV610218")[0];

            var home_page = dbHelper.websitePage.GetByWebSiteIDAndExactPageName(clonedwebsiteid, "home_page")[0];
            var member_home_page = dbHelper.websitePage.GetByWebSiteIDAndExactPageName(clonedwebsiteid, "member_home_page")[0];
            var websiteResourceFile = dbHelper.websiteResourceFile.GetByWebSiteIDAndName(clonedwebsiteid, "Capture.PNG")[0];
            var LocalizedString1 = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(clonedwebsiteid, "LocalizedString1")[0];
            var LocalizedString2 = dbHelper.websiteLocalizedString.GetByWebSiteIDAndName(clonedwebsiteid, "LocalizedString2")[0];

            var websiteSplashScreen = dbHelper.websiteSplashScreen.GetByWebSiteIDAndName(clonedwebsiteid, "SplashScreen1")[0];

            var websiteSplashScreenItems = dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenId(websiteSplashScreen);
            Assert.AreEqual(2, websiteSplashScreenItems.Count);

            var websiteSplashScreenItem = dbHelper.websiteSplashScreenItem.GetByWebsiteSplashScreenIdAndPageId(websiteSplashScreen, home_page)[0];
            var fields = dbHelper.websiteSplashScreenItem.GetByID(websiteSplashScreenItem, "visible", "websiteresourceid", "order", "displaynameid", "descriptionid", "pageurltextid");
            Assert.AreEqual(true, fields["visible"]);
            Assert.AreEqual(websiteResourceFile, fields["websiteresourceid"]);
            Assert.AreEqual(1, fields["order"]);
            Assert.AreEqual(LocalizedString1, fields["displaynameid"]);
            Assert.AreEqual(LocalizedString2, fields["descriptionid"]);
            Assert.AreEqual(LocalizedString1, fields["pageurltextid"]);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-10554

        [TestProperty("JiraIssueID", "CDV6-24782")]
        [Description("Login in the web app - Open a Website record ('Email Verification Required' = Yes & 'User Approval Required' = Yes) - " +
            "Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (Status = Approved and Email Verified = Yes) - Tap on the save button - Validate that the record is saved - " +
            "Validate that the linked person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly set")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserIcon_AddFlagsToPerson_UITestMethod01()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personid = new Guid("5d914647-3bd7-417d-b06d-3588e66a151e"); //Tonya Lawson
            var securityProfile = new Guid("91d542df-c15f-eb11-a306-005056926fe4"); //CW Partial Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            dbHelper.website.UpdateAdministrationInformation(websiteID, true, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUserTestFlags01@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Approved")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("309709").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Partial Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(true, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(false, fields["showiconwebsiteuserwaitingforapproval"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24783")]
        [Description("Login in the web app - Open a Website record ('Email Verification Required' = Yes & 'User Approval Required' = Yes) - " +
            "Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (Status = Waiting for Approval and Email Verified = Yes) - Tap on the save button - Validate that the record is saved - " +
            "Validate that the linked person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly set")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserIcon_AddFlagsToPerson_UITestMethod02()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personid = new Guid("5d914647-3bd7-417d-b06d-3588e66a151e"); //Tonya Lawson
            var securityProfile = new Guid("91d542df-c15f-eb11-a306-005056926fe4"); //CW Partial Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            dbHelper.website.UpdateAdministrationInformation(websiteID, true, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUserTestFlags01@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("309709").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Partial Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dbHelper = new DBHelper.DatabaseHelper();
            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(false, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(true, fields["showiconwebsiteuserwaitingforapproval"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24784")]
        [Description("Login in the web app - Open a Website record ('Email Verification Required' = No & 'User Approval Required' = Yes) - " +
            "Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (Status = Approved and Email Verified = No) - Tap on the save button - Validate that the record is saved - " +
            "Validate that the linked person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly set")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserIcon_AddFlagsToPerson_UITestMethod03()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personid = new Guid("5d914647-3bd7-417d-b06d-3588e66a151e"); //Tonya Lawson
            var securityProfile = new Guid("91d542df-c15f-eb11-a306-005056926fe4"); //CW Partial Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            dbHelper.website.UpdateAdministrationInformation(websiteID, false, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUserTestFlags01@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedNoOption()
                .SelectStatus("Approved")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("309709").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Partial Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(true, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(false, fields["showiconwebsiteuserwaitingforapproval"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24785")]
        [Description("Login in the web app - Open a Website record ('Email Verification Required' = No & 'User Approval Required' = Yes) - " +
            "Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (Status = Approved and Email Verified = No) - Tap on the save button - Validate that the record is saved - " +
            "Validate that the linked person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly set")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserIcon_AddFlagsToPerson_UITestMethod04()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personid = new Guid("5d914647-3bd7-417d-b06d-3588e66a151e"); //Tonya Lawson
            var securityProfile = new Guid("91d542df-c15f-eb11-a306-005056926fe4"); //CW Partial Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            dbHelper.website.UpdateAdministrationInformation(websiteID, false, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUserTestFlags01@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedNoOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("309709").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Partial Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(false, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(true, fields["showiconwebsiteuserwaitingforapproval"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24786")]
        [Description("Login in the web app - Open a Website record ('Email Verification Required' = Yes & 'User Approval Required' = Yes) - " +
            "Navigate to the Users area - Tap on the add new record button - " +
            "Set data in all fields (Status = Waiting for Approval and Email Verified = Yes) - Tap on the save button - Validate that the record is saved - " +
            "Validate that the linked person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly set - " +
            "Update the User status to Approved - Validate that the person record 'ShowIconWebsiteUserApproved' and 'ShowIconWebsiteUserWaitingForApproval' are correctly updated")]
        [TestMethod, TestCategory("UITest")]
        public void WebsiteUserIcon_AddFlagsToPerson_UITestMethod05()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personid = new Guid("5d914647-3bd7-417d-b06d-3588e66a151e"); //Tonya Lawson
            var securityProfile = new Guid("91d542df-c15f-eb11-a306-005056926fe4"); //CW Partial Access

            //remove all matching Users
            foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com"))
            {
                foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                    dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
            }

            dbHelper.website.UpdateAdministrationInformation(websiteID, true, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .ClicAddNewRecordButton();

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .InsertUserName("WebSiteAutomationUserTestFlags01@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickEmailVerifiedYesOption()
                .SelectStatus("Waiting for Approval")
                .SelectTwoFactorAuthenticationType("SMS")
                .ClickProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery("309709").TapSearchButton().SelectResultElement(personid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSecurityProfileLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Partial Access").TapSearchButton().SelectResultElement(securityProfile.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            var records = dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUserTestFlags01@mail.com");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(false, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(true, fields["showiconwebsiteuserwaitingforapproval"]);

            webSiteUsersPage
                .InsertSearchQuery("WebSiteAutomationUserTestFlags01@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(records[0].ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .SelectStatus("Approved")
                .ClickSaveAndCloseButton();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad();

            fields = dbHelper.person.GetPersonById(personid, "showiconwebsiteuserapproved", "showiconwebsiteuserwaitingforapproval");
            Assert.AreEqual(true, fields["showiconwebsiteuserapproved"]);
            Assert.AreEqual(false, fields["showiconwebsiteuserwaitingforapproval"]);
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-10551

        [TestProperty("JiraIssueID", "CDV6-24787")]
        [Description("Open a website user record account that is locked - Click on the unlock button - " +
            "Validate that the account is unlocked, validate that the 'Failed Password Attempt Count' is reset to 0")]
        [TestMethod, TestCategory("UITest")]
        public void ResetFailedPasswordAttemptCount_UITestMethod01()
        {
            var websiteID = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteuserid = new Guid("83ffe538-57b9-eb11-a323-005056926fe4"); //StaffordshireCitizenPortalUser50@mail.com

            dbHelper.websiteUser.UpdateWebsiteUser(websiteuserid, 6, DateTime.Now, true);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWebSitesSection();

            webSitePage
                .WaitForWebSiteToLoad()
                .InsertSearchQuery(portalWebsiteName)
                .ClickSearchButton()
                .ClickOnWebSiteRecord(websiteID.ToString());

            webSiteRecordPage
                .WaitForWebSiteRecordPageToLoad()
                .NavigateToWebsiteUsers();

            webSiteUsersPage
                .WaitForWebSiteUsersPageToLoad()
                .InsertSearchQuery("StaffordshireCitizenPortalUser50@mail.com")
                .ClickSearchButton()
                .ClickOnWebSiteUserRecord(websiteuserid.ToString());

            webSiteUserRecordPage
                .WaitForWebSiteUserRecordPageToLoad()
                .ClickUnlockButton()
                .WaitForWebSiteUserRecordPageToLoad()
                .ValidateIsAccountLockedNoRadioButtonChecked()
                .ValidateFailedPasswordAttemptCountFieldText("0");


            var fields = dbHelper.websiteUser.GetByID(websiteuserid, "isaccountlocked", "failedpasswordattemptcount");
            Assert.AreEqual(false, fields["isaccountlocked"]);
            Assert.AreEqual(0, fields["failedpasswordattemptcount"]);
        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod, TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

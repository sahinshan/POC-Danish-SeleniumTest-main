using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsitePagesTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5655

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6359")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages' - supply a websiteid for a website that have Pages and Sub-Pages linked - " +
            "Validate that the website pages information returned is the correct one")]
        public void WebsitePages_TestMethod001()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var websitePagesInfo = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePagesInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(6, websitePagesInfo.Count);


            var websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_1")).FirstOrDefault();
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1", websitePage.Label);
            Assert.AreEqual("Page_1", websitePage.Name);
            Assert.AreEqual("Page 1", websitePage.HeaderTitle);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_1_1")).FirstOrDefault();
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.1", websitePage.Label);
            Assert.AreEqual("Page_1_1", websitePage.Name);
            Assert.AreEqual("Page 1.1 Header", websitePage.HeaderTitle);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(new Guid("9c26da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_1_2")).FirstOrDefault();
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.2", websitePage.Label);
            Assert.AreEqual("Page_1_2", websitePage.Name);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(new Guid("2cbc8e81-c225-eb11-a2cd-005056926fe4"), websitePage.Id);



            websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_2")).FirstOrDefault();
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 2", websitePage.Label);
            Assert.AreEqual("Page_2", websitePage.Name);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(new Guid("407a2b88-c225-eb11-a2cd-005056926fe4"), websitePage.Id);



            websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_3")).FirstOrDefault();
            Assert.AreEqual(false, websitePage.IsSecure);
            Assert.AreEqual("Page 3", websitePage.Label);
            Assert.AreEqual("Page_3", websitePage.Name);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(new Guid("6b8c6891-c225-eb11-a2cd-005056926fe4"), websitePage.Id);



            websitePage = websitePagesInfo.Where(c => c.Name.Equals("Page_4")).FirstOrDefault();
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 4", websitePage.Label);
            Assert.AreEqual("Page_4", websitePage.Name);
            Assert.AreEqual(null, websitePage.Layout);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(new Guid("853d6fd9-0828-eb11-a2cd-005056926fe4"), websitePage.Id);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6360")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages' - supply a websiteid for a website has no website page - " +
            "Validate that no page data is returned")]
        public void WebsitePages_TestMethod002()
        {
            var websiteid = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12

            var websitePagesInfo = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePagesInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, websitePagesInfo.Count);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6361")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages' - supply a websiteid that will not match any website - " +
            "Validate that no pages information is returned")]
        public void WebsitePages_TestMethod003()
        {
            var websiteid = new Guid("1111158a-7e2a-eb11-a2cd-005056926fe4"); //non existing website id

            var websitePagesInfo = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePagesInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, websitePagesInfo.Count);
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6362")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is not secured and has Widgets, User security profile grants him access to the page) - " +
            "Validate that the website page information returned is correct")]
        public void WebsitePages_TestMethod004()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_3", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(false, websitePage.IsSecure);
            Assert.AreEqual("Page 3", websitePage.Label);
            Assert.AreEqual("Page_3", websitePage.Name);
            Assert.AreEqual("Website Page 3", websitePage.HeaderTitle);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(new Guid("6b8c6891-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            Assert.AreEqual(1, websitePage.ScriptFiles.Count());
            Assert.AreEqual(1, websitePage.StyleSheets.Count());

            var columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "site 11 File.htm" select column).FirstOrDefault();
            Assert.AreEqual(4, columnInfo.Type);
            Assert.AreEqual("site 11 File.htm", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(null, columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("site 11 File.htm", columnInfo.HtmlFile);
            Assert.AreEqual(null, columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Automation - Widget Footer 1" select column).FirstOrDefault();
            Assert.AreEqual(3, columnInfo.Type);
            Assert.AreEqual("Automation - Widget Footer 1", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(null, columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("Automation - Widget Footer 1", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Person - Portal Form" select column).FirstOrDefault();
            Assert.AreEqual(1, columnInfo.Type);
            Assert.AreEqual("Person - Portal Form", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("30f84b2d-b169-e411-bf00-005056c00008"), columnInfo.BusinessObjectId);
            Assert.AreEqual(new Guid("3e0a350e-6722-eb11-a2ce-0050569231cf"), columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataFormControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Case - Main" select column).FirstOrDefault();
            Assert.AreEqual(2, columnInfo.Type);
            Assert.AreEqual("Case - Main", columnInfo.Title);
            Assert.AreEqual(6, columnInfo.ColumnSize);
            Assert.AreEqual(6, columnInfo.Height);
            Assert.AreEqual(new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"), columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(new Guid("c363a358-6722-eb11-a2ce-0050569231cf"), columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataViewListControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6363")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secure and has no Widgets, User security profile grants him access to the page) - " +
            "Validate that the website page information returned is correct")]
        public void WebsitePages_TestMethod005()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_4", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("", websitePage.HeaderTitle);
            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 4", websitePage.Label);
            Assert.AreEqual("Page_4", websitePage.Name);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(0, websitePage.Layout.Rows.Count);
            Assert.AreEqual(new Guid("853d6fd9-0828-eb11-a2cd-005056926fe4"), websitePage.Id);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6364")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secure and has no Widgets, User security profile DOES NOT grant him access to the page) - " +
            "Validate that an error message is returned preventing the user to access the page")]
        public void WebsitePages_TestMethod006()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The User has no access to this Website Page\",\"errorCode\":\"UnauthorisedAccess\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6365")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "DO NOT login any website user - " + 
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is not secured and has Widgets) - " +
            "Validate that the website page information returned is correct")]
        public void WebsitePages_TestMethod007()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11


            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_3", this.WebAPIHelper.PortalSecurityProxy.Token, null);

            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(false, websitePage.IsSecure);
            Assert.AreEqual("Page 3", websitePage.Label);
            Assert.AreEqual("Page_3", websitePage.Name);
            Assert.AreEqual("Website Page 3", websitePage.HeaderTitle);
            Assert.AreEqual(null, websitePage.ParentPageId);
            Assert.AreEqual(new Guid("6b8c6891-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            Assert.IsTrue(websitePage.ScriptFiles.Contains("Site 11 File.Js"));
            Assert.IsTrue(websitePage.StyleSheets.Contains("Site 11 File.Css"));

            var columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "site 11 File.htm" select column).FirstOrDefault();
            Assert.AreEqual(4, columnInfo.Type);
            Assert.AreEqual("site 11 File.htm", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(null, columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("site 11 File.htm", columnInfo.HtmlFile);
            Assert.AreEqual(null, columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Automation - Widget Footer 1" select column).FirstOrDefault();
            Assert.AreEqual(3, columnInfo.Type);
            Assert.AreEqual("Automation - Widget Footer 1", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(null, columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("Automation - Widget Footer 1", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Person - Portal Form" select column).FirstOrDefault();
            Assert.AreEqual(1, columnInfo.Type);
            Assert.AreEqual("Person - Portal Form", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("30f84b2d-b169-e411-bf00-005056c00008"), columnInfo.BusinessObjectId);
            Assert.AreEqual(new Guid("3e0a350e-6722-eb11-a2ce-0050569231cf"), columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataFormControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Case - Main" select column).FirstOrDefault();
            Assert.AreEqual(2, columnInfo.Type);
            Assert.AreEqual("Case - Main", columnInfo.Title);
            Assert.AreEqual(6, columnInfo.ColumnSize);
            Assert.AreEqual(6, columnInfo.Height);
            Assert.AreEqual(new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"), columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(new Guid("c363a358-6722-eb11-a2ce-0050569231cf"), columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataViewListControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(true, columnInfo.Visible);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6366")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "DO NOT login any website user - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secure) - " +
            "Validate that an error message is returned preventing the user to access the page")]
        public void WebsitePages_TestMethod008()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            try
            {
                var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1", this.WebAPIHelper.PortalSecurityProxy.Token, null);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The User has no access to this Website Page\",\"errorCode\":\"UnauthorisedAccess\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown.");


        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6367")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secured and has Widgets, User security profile grants him access to the page and to the Widget record types) - " +
            "Validate that the website page information returned is correct")]
        public void WebsitePages_TestMethod009()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1_1", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.1", websitePage.Label);
            Assert.AreEqual("Page_1_1", websitePage.Name);
            Assert.AreEqual("Page 1.1 Header", websitePage.HeaderTitle);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(new Guid("9c26da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            Assert.IsTrue(websitePage.ScriptFiles.Contains("Site 11 File.Js"));
            Assert.IsTrue(websitePage.StyleSheets.Contains("Site 11 File.Css"));

            var columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Person - Portal Form" select column).FirstOrDefault();
            Assert.AreEqual(1, columnInfo.Type);
            Assert.AreEqual("Person - Portal Form", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("30f84b2d-b169-e411-bf00-005056c00008"), columnInfo.BusinessObjectId);
            Assert.AreEqual(new Guid("3e0a350e-6722-eb11-a2ce-0050569231cf"), columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataFormControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(false, columnInfo.Visible);

            columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Case - Main" select column).FirstOrDefault();
            Assert.AreEqual(2, columnInfo.Type);
            Assert.AreEqual("Case - Main", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"), columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(new Guid("c363a358-6722-eb11-a2ce-0050569231cf"), columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataViewListControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(false, columnInfo.Visible);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6368")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secured and has Widgets, User security profile grants him access to the page but has NO permission to any record types) - " +
            "Validate that the website page information returned is correct but any widget linked to any record type should not be returned")]
        public void WebsitePages_TestMethod010()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user2@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1_1", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);


            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.1", websitePage.Label);
            Assert.AreEqual("Page_1_1", websitePage.Name);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(new Guid("9c26da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);
            Assert.AreEqual(1, websitePage.Layout.Rows.Count);


        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6369")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secured and has Widgets, User security profile grants him access to the page and only grants access to Case records) - " +
            "Validate that the website page information returned is correct and only widgets linked to Case records should be available")]
        public void WebsitePages_TestMethod011()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user3@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1_1", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);


            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.1", websitePage.Label);
            Assert.AreEqual("Page_1_1", websitePage.Name);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(new Guid("9c26da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);
            Assert.AreEqual(1, websitePage.Layout.Rows.Count);

            Assert.IsTrue(websitePage.ScriptFiles.Contains("Site 11 File.Js"));
            Assert.IsTrue(websitePage.StyleSheets.Contains("Site 11 File.Css"));

            var columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Case - Main" select column).FirstOrDefault();
            Assert.AreEqual(2, columnInfo.Type);
            Assert.AreEqual("Case - Main", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("79f4efc4-bfb1-e811-80dc-0050560502cc"), columnInfo.BusinessObjectId);
            Assert.AreEqual(null, columnInfo.DataFormId);
            Assert.AreEqual(new Guid("c363a358-6722-eb11-a2ce-0050569231cf"), columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataViewListControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(false, columnInfo.Visible);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6370")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5655 - " +
            "Login a website user and use his AccessToken - " +
            "Perform a call to 'services/api/portal/{websiteid}/pages/{PageName}' - " +
            "Supply a websiteid and a PageName (Page is Secured and has Widgets, User security profile grants him access to the page and only grants access to Person records) - " +
            "Validate that the website page information returned is correct and only widgets linked to Person records should be available")]
        public void WebsitePages_TestMethod012()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var websitePage = this.WebAPIHelper.WebsitePagesProxy.GetWebsitePage(websiteid, "Page_1_1", this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(false, websitePage.DisplayHeaderTitle);
            Assert.AreEqual(true, websitePage.IsSecure);
            Assert.AreEqual("Page 1.1", websitePage.Label);
            Assert.AreEqual("Page_1_1", websitePage.Name);
            Assert.AreEqual("Page 1.1 Header", websitePage.HeaderTitle);
            Assert.AreEqual(new Guid("9626da7a-c225-eb11-a2cd-005056926fe4"), websitePage.ParentPageId);
            Assert.AreEqual(new Guid("9c26da7a-c225-eb11-a2cd-005056926fe4"), websitePage.Id);

            Assert.IsTrue(websitePage.ScriptFiles.Contains("Site 11 File.Js"));
            Assert.IsTrue(websitePage.StyleSheets.Contains("Site 11 File.Css"));

            var columnInfo = (from row in websitePage.Layout.Rows from column in row.Columns where column.Title == "Person - Portal Form" select column).FirstOrDefault();
            Assert.AreEqual(1, columnInfo.Type);
            Assert.AreEqual("Person - Portal Form", columnInfo.Title);
            Assert.AreEqual(3, columnInfo.ColumnSize);
            Assert.AreEqual(3, columnInfo.Height);
            Assert.AreEqual(new Guid("30f84b2d-b169-e411-bf00-005056c00008"), columnInfo.BusinessObjectId);
            Assert.AreEqual(new Guid("3e0a350e-6722-eb11-a2ce-0050569231cf"), columnInfo.DataFormId);
            Assert.AreEqual(null, columnInfo.DataViewId);
            Assert.AreEqual("", columnInfo.HtmlFile);
            Assert.AreEqual("DataFormControl", columnInfo.WebsiteWidgetControl);
            Assert.AreEqual(false, columnInfo.Visible);

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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteAnnouncementsTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5653

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6344")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
            "Perform a call to 'services/api/portal/{websiteId}/announcements' - " +
            "Website has 1 published announcement with 'Expires On' date in the future - " +
            "Website has 1 published announcement with no 'Expires On' date - " +
            "Website has 1 published announcement with 'Expires On' date in the past - " +
            "Website has 1 Draft announcement - " +
            "Validate that only the published announcements with no 'Expires On' date or with 'Expires On' date in the future are returned ")]
        public void WebsiteAnnouncements_TestMethod001()
        {
            var websiteid = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var announcement01 = new Guid("ffd25cfa-6d2d-eb11-a2ce-005056926fe4"); //Website Announcement 01
            var announcement02 = new Guid("49b36f0a-6e2d-eb11-a2ce-005056926fe4"); //Website Announcement 02

            var websiteAnnouncements = this.WebAPIHelper.WebsiteAnnouncementsProxy.Announcements(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(2, websiteAnnouncements.Count()); //web should have the 2 published announcements
            
            Assert.AreEqual(announcement02, websiteAnnouncements[0].Id);//the first announcement should be the one with the most recent published date
            Assert.AreEqual("Website Announcement 02", websiteAnnouncements[0].Name);
            Assert.AreEqual("<p>line 1</p>\n\n<p>line 2</p>", websiteAnnouncements[0].Contents);
            Assert.AreEqual(new DateTime(2020, 11, 2, 13, 15, 0), websiteAnnouncements[0].PublishedOnDate);
            Assert.IsNull(websiteAnnouncements[0].ExpiresOnDate);

            Assert.AreEqual(announcement01, websiteAnnouncements[1].Id);
            Assert.AreEqual("Website Announcement 01", websiteAnnouncements[1].Name);
            Assert.AreEqual("<p>Value 1</p>\n\n<p>Value 2</p>", websiteAnnouncements[1].Contents);
            Assert.AreEqual(new DateTime(2020, 11, 1, 7, 0, 0), websiteAnnouncements[1].PublishedOnDate);
            Assert.AreEqual(new DateTime(2029, 11, 30, 9, 25, 0), websiteAnnouncements[1].ExpiresOnDate);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6345")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
           "Perform a call to 'services/api/portal/{websiteId}/announcements' - " +
           "Website has no Announcement records - Validate that no announcement record is returned ")]
        public void WebsiteAnnouncements_TestMethod002()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            var websiteAnnouncements = this.WebAPIHelper.WebsiteAnnouncementsProxy.Announcements(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, websiteAnnouncements.Count());

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6346")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
           "Perform a call to 'services/api/portal/{websiteId}/announcements' - " +
           "Supply a websiteId that will match no webbsite - Validat that no data is returned")]
        public void WebsiteAnnouncements_TestMethod003()
        {
            var websiteid = new Guid("1111a665-4e18-eb11-a2cd-005056926fa1"); //Id will not match any website

            var websiteAnnouncements = this.WebAPIHelper.WebsiteAnnouncementsProxy.Announcements(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, websiteAnnouncements.Count());

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6347")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
           "Perform a call to 'services/api/portal/{websiteId}/announcements' - " +
           "Supply an invalid GUID in the websiteId  - Validat that no data is returned")]
        public void WebsiteAnnouncements_TestMethod004()
        {
            var websiteid = "1111a665-4e18-eb11-a2cd-005056926xxx"; //invalid Guid format

            try
            {
                var websiteAnnouncements = this.WebAPIHelper.WebsiteAnnouncementsProxy.Announcements(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("IIS 10.0 Detailed Error - 404.0 - Not Found"));
                return;
            }

            Assert.Fail("No exception was thrown.");

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

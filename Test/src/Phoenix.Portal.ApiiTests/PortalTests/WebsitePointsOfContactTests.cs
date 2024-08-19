using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsitePointsOfContactTests: UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5651

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6371")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5651 - " +
            "Perform a call to 'portal/{websiteid}/pointsofcontact' - " +
            "Supply the website id (website 3 points of contact records, one is a draft, one has all fields set, one has mandatory fields only) - " +
            "validate that only the 2 published points of contact are returned")]
        public void WebsitePointsOfContact_TestMethod001()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            var allRecords = this.WebAPIHelper.WebsitePointsOfContactProxy.PointsOfContacts(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(2, allRecords.Count);

            var record = allRecords.Where(c => c.Name == "Point Of Contact 1").First();
            Assert.AreEqual(new Guid("ec49edd7-742a-eb11-a2cd-005056926fe4"), record.WebsitePointOfContactId);
            Assert.AreEqual("Point Of Contact 1", record.Name);
            Assert.AreEqual("line 1\nline 2", record.Address);
            Assert.AreEqual("PointOfContact1@mail.com", record.Email);
            Assert.AreEqual("965478284", record.Phone);

            record = allRecords.Where(c => c.Name == "Point Of Contact 2").First();
            Assert.AreEqual(new Guid("f049edd7-742a-eb11-a2cd-005056926fe4"), record.WebsitePointOfContactId);
            Assert.AreEqual("Point Of Contact 2", record.Name);
            Assert.AreEqual(null, record.Address);
            Assert.AreEqual(null, record.Email);
            Assert.AreEqual(null, record.Phone);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6372")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5651 - " +
            "Perform a call to 'portal/{websiteid}/pointsofcontact' - Supply a website id that will not match any website record - " +
            "validate that no data is returned")]
        public void WebsitePointsOfContact_TestMethod002()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926ab1"); //non existing website id

            var allRecords = this.WebAPIHelper.WebsitePointsOfContactProxy.PointsOfContacts(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6373")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5651 - " +
            "Perform a call to 'portal/{websiteid}/pointsofcontact' - Supply a website id that has no points of contact - " +
            "validate that no data is returned")]
        public void WebsitePointsOfContact_TestMethod003()
        {
            var websiteid = new Guid("37397330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02

            var allRecords = this.WebAPIHelper.WebsitePointsOfContactProxy.PointsOfContacts(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6374")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5651 - " +
            "Perform a call to 'portal/{websiteid}/pointsofcontact' - Supply a website id - Supply an invalid token - validate that no data is returned")]
        [ExpectedException(typeof(Exception))]
        public void WebsitePointsOfContact_TestMethod004()
        {
            var websiteid = new Guid("37397330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02

            var allRecords = this.WebAPIHelper.WebsitePointsOfContactProxy.PointsOfContacts(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token + "invalidtokeninfo");
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

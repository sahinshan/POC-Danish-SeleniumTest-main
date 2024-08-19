using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteFeedbackTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5652

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6354")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5652 - " +
            "Perform a call to 'portal/{websiteid}/feedback' - Send the feedback data with all fields set - Validate that the record is correctly saved in the CareDirector side")]
        public void WebsiteFeedback_TestMethod001()
        {
            var teamID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var websiteID = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var websiteFeedbackTypeId = new Guid("56546cc7-bf29-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteID(websiteID))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            var feedback = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteFeedback
            {
                name = "Feedback 1",
                email = "person@mail.com",
                message = "feedback message goes here ....",
                feedbacktypeid = websiteFeedbackTypeId
            };

            Guid feedbackid = WebAPIHelper.WebsiteFeedbackProxy.Feedback(feedback, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);

            var records = dbHelper.websiteFeedback.GetByWebSiteID(websiteID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.websiteFeedback.GetByID(records[0], "name", "email", "websitefeedbacktypeid", "ownerid", "websiteid", "message");
            Assert.AreEqual(feedbackid, fields["websitefeedbackid"]);
            Assert.AreEqual(feedback.name, fields["name"]);
            Assert.AreEqual(feedback.email, fields["email"]);
            Assert.AreEqual(feedback.feedbacktypeid, fields["websitefeedbacktypeid"]);
            Assert.AreEqual(teamID, fields["ownerid"]);
            Assert.AreEqual(websiteID, fields["websiteid"]);
            Assert.AreEqual(feedback.message, fields["message"]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6355")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5652 - " +
            "Perform a call to 'portal/{websiteid}/feedback' - Send the feedback data with all fields set except for Name - Validate that the record is not saved")]
        public void WebsiteFeedback_TestMethod002()
        {
            var websiteID = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var websiteFeedbackTypeId = new Guid("56546cc7-bf29-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteID(websiteID))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            var feedback = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteFeedback
            {
                name = "",
                email = "person@mail.com",
                message = "feedback message goes here ....",
                feedbacktypeid = websiteFeedbackTypeId
            };

            try
            {
                this.WebAPIHelper.WebsiteFeedbackProxy.Feedback(feedback, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Name.\"}", ex.Message);
                var records = dbHelper.websiteFeedback.GetByWebSiteID(websiteID);
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6356")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5652 - " +
            "Perform a call to 'portal/{websiteid}/feedback' - Send the feedback data with all fields set except for Email - Validate that the record is not saved")]
        public void WebsiteFeedback_TestMethod003()
        {
            var websiteID = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var websiteFeedbackTypeId = new Guid("56546cc7-bf29-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteID(websiteID))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            var feedback = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteFeedback
            {
                name = "Feedback 1",
                email = "",
                message = "feedback message goes here ....",
                feedbacktypeid = websiteFeedbackTypeId
            };

            try
            {
                this.WebAPIHelper.WebsiteFeedbackProxy.Feedback(feedback, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Email.\"}", ex.Message);
                var records = dbHelper.websiteFeedback.GetByWebSiteID(websiteID);
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6357")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5652 - " +
            "Perform a call to 'portal/{websiteid}/feedback' - Send the feedback data with all fields set except for Message - Validate that the record is not saved")]
        public void WebsiteFeedback_TestMethod004()
        {
            var websiteID = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var websiteFeedbackTypeId = new Guid("56546cc7-bf29-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteID(websiteID))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            var feedback = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteFeedback
            {
                name = "Feedback 1",
                email = "person@mail.com",
                message = "",
                feedbacktypeid = websiteFeedbackTypeId
            };

            try
            {
                this.WebAPIHelper.WebsiteFeedbackProxy.Feedback(feedback, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Message.\"}", ex.Message);
                var records = dbHelper.websiteFeedback.GetByWebSiteID(websiteID);
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6358")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5652 - " +
            "Perform a call to 'portal/{websiteid}/feedback' - Send the feedback data with all fields set but with an invalid Feedback Type - Validate that the record is not saved")]
        public void WebsiteFeedback_TestMethod005()
        {
            var websiteID = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12
            var websiteFeedbackTypeId = new Guid("33346cc7-bf29-eb11-a2cd-005056926fe1"); //this is an invalid id

            //remove all matching feedback records
            foreach (var websitepageid in dbHelper.websiteFeedback.GetByWebSiteID(websiteID))
                dbHelper.websiteFeedback.DeleteWebsiteFeedback(websitepageid);

            var feedback = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteFeedback
            {
                name = "Feedback 1",
                email = "person@mail.com",
                message = "feedback message goes here ....",
                feedbacktypeid = websiteFeedbackTypeId
            };

            try
            {
                this.WebAPIHelper.WebsiteFeedbackProxy.Feedback(feedback, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Unable to create websitefeedback.\"}", ex.Message);
                var records = dbHelper.websiteFeedback.GetByWebSiteID(websiteID);
                Assert.AreEqual(0, records.Count);
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
